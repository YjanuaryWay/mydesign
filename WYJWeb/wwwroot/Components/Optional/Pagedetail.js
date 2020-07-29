Vue.component("pagedetail", {
    template:"#Pagedetail",
    props: ['data','title'],
    data() {
        return {
            header:
                {
                    btns: [{
                        innerText: "<= 返回",
                        class: "btn btn_default",
                        act: (e) => { this.$root.Switch(0) }
                    }],
                },
            footer:
                {
                    btns: [{
                        innerText: "保存",
                        class: "btn btn_success",
                        act: () => { this.$root.Save() }
                    }, {
                        innerText: "取消",
                        class: "btn btn_default",
                            act: (e) => { this.$root.Switch(0) }
                    }],
                },
        }
    }
})
