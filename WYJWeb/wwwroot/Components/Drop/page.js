class V3 {
    constructor(hdata, mdata, fdata) {
        this.header = hdata;
        this.main = mdata;
        this.footer = fdata;
    }
};

Vue.component("V3", {
    template:
        `<div class="container">
                <slot name="header" :data="data.header"></slot>
                <slot name="main"   :data="data.main"></slot>
                <slot name="footer" :data="data.footer"></slot>
        </div>`,
    props: {
        data:Object
    }
});
Vue.component("V2", {
    template:
        `<div class="container">
                <slot name="header" :data="data.hdata"></slot>
                <slot name="main"   :data="data.mdata"></slot>
        </div>`,
    props: {
        data: V3
    }
});
