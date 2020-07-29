Vue.component("buttonlist", {
    props: ['hook', 'set'],
    template: "#ButtonList",
    data() {
        Store.defineData('buttonlist', () => {
            return {
                btns: [],
                preText: '',
                afterText: ''
            }
        });
        return Store.initComponentData('buttonlist',this);
    },
    methods: {
        sendEvent(eventHook,e) {
            e.data = {
                hook: eventHook,
                target:this
            };
            this.trigger(eventHook, e);
        }
    }
});

Vue.component("pagenation", {
    props: ['hook', 'hastoolbar', 'set', 'api'],
    template: "#Pagenation",
    data() {
        var hook = "pagenation";
        Store.defineData(hook, () => {
            return {
                queryKeys: {
                    key: '',
                },
                titles: [],
                Data: [],
                currentIndex: 1,
                pagesize: 10,
                step: 5,
                startIndex: 0,
                Count: 0,
                prebtns: [
                    {
                        id: "Home",
                        text: "首页",
                        act: () => { this.startIndex = 0 }
                    }, {
                        id: "PageUp",
                        text: '<<',
                        act: () => { this.startIndex -= this.step }
                    },
                ],
                nextbtns: [
                    {
                        id: "PageDown",
                        text: '>>',
                        act: () => { this.startIndex += this.step }
                    }, {
                        id: "End",
                        text: "尾页",
                        act: () => { this.startIndex = this.endIndex - (this.endIndex % this.step || this.step) }
                    }
                ],
                queryTimes: 1,
            }
        });
        return Store.initComponentData(hook, this);
    },
    methods: {
        SetPagesize: function (e) {
            if (e.target.value > 0) {
                this.pagesize = e.target.value;
            } else {
                e.target.value = this.pagesize;
            }
        },
        Reset: function () {
            for (var x in this.queryKeys) {
                this.queryKeys[x] = '';
            }
        },
        Query() {
            var queryStr = objToQuerystring(this.queryKeys);
            var url = `/api/${this.api}/getlist?pageIndex=${this.currentIndex}&pageSize=${this.pagesize}&${queryStr}`;
            Ajax.get(url).then(res => {
                this.Data = res.Data.Data;
                this.Count = res.Data.Count;
            });
        }
    },
    computed: {
        name: function () {
            var queryStr = objToQuerystring(this.queryKeys);
            var url = `/api/${this.api}/getlist?pageIndex=${this.currentIndex}&pageSize=${this.pagesize}&${queryStr}`;
            Ajax.get(url).then(res => {
                this.Data = res.Data.Data;
                this.Count = res.Data.Count;
            });
            return this.queryTimes;
        },
        endIndex: function () {
            return Math.ceil(this.Count / this.pagesize);
        },
        btns: function () {
            var arr = [];
            var stp = this.endIndex - this.startIndex; 
            if (stp > this.step) stp = this.step;
            for (var i = 0; i < stp;) {
                i++;
                let btn = {
                    text: i + this.startIndex,
                    act() { },
                };
                if (btn.text == this.currentIndex) btn.class = 'onPage';
                else btn.act = (e) => {
                    this.currentIndex = btn.text;
                }
                arr.push(btn);
            }
            return arr;
        },
    },
});

Vue.component("editor", {
    props: ['hook', 'set'],
    template: "#Editor",
    data() {
        return {
            labels: this.set
        }
    },
});







Vue.component("basetable", {
    props: ['hook','set'],
    template: "#Table",
    data() {
        Store.defineData('basetable', () => {
            return {
                ths: [],
                tbody: [],
            }
        });
        return Store.initComponentData('basetable', this);
    },
});


Vue.component("wyjdialog", {
    props: ['data', 'title', 'okTitle'],
    template: "#dialog",
    data() {
        Store.defineData('wyjdialog', () => {
            return {
                show: false,
            }
        });
        return Store.initComponentData('wyjdialog', this);
    },
    methods: {
        okEvent(e) {
            console.log(this);
            this.show = false;
        },
        cancelEvent(e) {
            this.show = false;
        }
    }
});