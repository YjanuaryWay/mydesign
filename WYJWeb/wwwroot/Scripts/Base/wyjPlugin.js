
class WYJFrame {
    constructor(Methods) {
        this.Methods = Methods

    }
}
class Button {
    constructor(option) {
        this.name = option.name;
        this.act = option.act;
        this.class = option.class;
        this.id = option.id;
    }
}


function objToQuerystring (obj,pre='') {
    var str = '';
    var s = "&";
    for (var key in obj) {
        str += `${pre}${key}=${obj[key]}`;
        pre = s;
    }
    return str;
};

/**
 * 
 * @param {any} yesOrNo
 * @param {Function} yes
 * @param {any} no
 */
function when(yesOrNo, yes, no) {
    return yesOrNo instanceof Function
        ? yesOrNo()
        : yesOrNo
            ? yes()
            : no instanceof Function
                ? no()
                : null;
};


var Store;
    Store = {
        debug: 0,
        count: {},
        /**
         * 
         * @param {string} hook
         * @param {any} data
         * @param {string} log
         */
        set(hook, data, log) {
            when(this.debug, () => console.log(log));
            return when(this[hook], () => Object.assign(this[hook], data), () => this[hook] = data);
        },
        get(hook, log) {
            when(this.debug, () => console.error(log));
            return this[hook];
        },
        delete(hook) {
            delete this[hook];
        },

        /**
         * 注册/获取组件默认数据
         * @param {string} componentName:组件名称
         * @param {Function} fac:数据工厂
         */
        defineData(componentName, fac) {
            //var t = this.count[componentName] || 0;
            //t++;
            //this.count[componentName] = t;
            //console.log(componentName, 'defineData', t);
            when(this.count[componentName], () => this.count[componentName] += 1, () => this.count[componentName] = 1);
            console.log(componentName, 'defineData', this.count[componentName]);
            return fac && !this[componentName] ? this[componentName] = fac : this[componentName]();   
        },
        /**
         * 配置组件实例数据(用于Vue.data())
         * @param {string} componentName
         * @param {Vue} component
         */
        initComponentData(componentName, component) {
            var set = Object.assign(this.defineData(componentName), component.set);
            return this.set(component.hook, set, componentName + ' setHook: ' + component.hook);
        },

};

/**
 * 事件钩子
 */
Vue.prototype._listeners = {};
Vue.prototype.on = function (eType, act) {
    this._listeners[eType] = act;
};
Vue.prototype.trigger = function (eType, data) {
    when(this._listeners[eType], () => {
        this._listeners[eType](data);
        console.log('已处理事件 at:', this)
    }, () => {
            this.$parent ? this.$parent.trigger(eType, data) : console.log(this, '未处理事件:', eType, data);
    });
};

var App = function (set, id = 'Root') {
    return new Vue({
        el: '#' + id,
        data() {
            set = Object.assign({
                hooks:['listHead','listMain','detailHead','detailMain','detailFoot'],
                msg: '',
                active: true,
                viewMode: true,
                model: {},
                PageTitle:'',
                sets: [{
                    btns: [{
                        text: "新增",
                        class: "btn btn_success",
                        event:'btnAdd'
                    }],
                    
                    preText: set.PageTitles[0]//待优化
                }, {
                    btns: [{
                        text: "<= 返回",
                        class: "btn btn_default",
                        event: 'btnBack'
                    }],
                }, {
                    btns: [{
                        text: "保存",
                        class: "btn btn_success",
                        event: 'btnSave'
                    }, {
                        text: "取消",
                        class: "btn btn_default",
                        event:'btnCancle'
                    }],
                }],
            }, set);
            this.on('btnAdd', (e) => { this.Switch(1); });
            this.on('btnBack', (e) => { this.Switch(0); });
            this.on('btnSave', (e) => { this.Save(); });
            this.on('btnCancle', (e) => { this.Switch(0); });
            return set;
        },
        methods: {
            Switch(type, model) {
                this.active = type == 0;
                this.viewMode = type == 3;
                Store[this.hooks[2]].afterText = this.PageTitles[type];//待优化
                this.model = model || {};
            },
            Save: function () {
                var url = `/api/${this.Api}/Save`;
                Ajax.post(url, JSON.stringify(this.model)).then(res => {
                    when(res.IsSuccess, () => {
                        this.Switch(0);
                        Store[this.hooks[1]].queryTimes++;//待优化
                    }, () => {
                        this.ShowMsg(res.Msg);
                    });
                });
            },
            DeleteById: function (id, i) {
                var url = `/api/${this.Api}/DeleteById?Id=${id}`;
                Ajax.get(url).then(res => {
                    when(res.IsSuccess, () => {
                        Store[this.hooks[1]].queryTimes++;
                    }, () => {
                        this.ShowMsg(res.Msg);
                    });
                });
            },
            ShowMsg: function (msg) {
                this.msg = msg;
                setTimeout(() => this.msg = '', 3000);
            }
        },
    });

}
