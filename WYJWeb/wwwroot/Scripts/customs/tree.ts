// import { Validate } from "./Validate";

namespace Mainyf {

    function notNull(obj: any, message: string) {
        if (!obj) console.error(message);
    }

    function isTree(bool: boolean, message: string) {
        if (!bool) console.error(message);
    }

    export interface TreeSettings {
        tmplName: string;
        treeList: string;
        treeNode: string;
        treeItem: string;
        events: { [key: string]: Function; };
    }

    export class TreeGraph {

        _active: string = "active";
        $treeList: JQuery;
        _tmplName: string;
        _treeNode: string;
        _treeItem: string;
        _settings: TreeSettings;
        _rootData: any;
        _events: any;
        _clickCount: any;
        _timerId: any = null;

        constructor(settings: TreeSettings) {
            this._settings = $.extend({
                tmplName: "#treeTmpl",
                treeList: "#treeList",
                treeNode: ".treeNode",
                treeItem: ".tree__item",
                events: {}
            }, settings);
            this._tmplName = this._settings.tmplName;
            this.$treeList = $(this._settings.treeList);
            this._treeNode = this._settings.treeNode;
            this._treeItem = this._settings.treeItem;
            this._events = this._settings.events;
            this._clickCount = 0;
        }

        /**
         * constructor before execute
         */
        initialization(data: any) {
            notNull(data, "param 'data' cannot not null");
            this._rootData = data;
            this.$treeList.Link(this._tmplName, this._rootData);
            this._initEvents();
            this._initVisualFeedback();
            this.$treeList.find(this._treeNode).eq(0).trigger("click")
        }

        /**
         * initialization visual feedback
         */
        _initVisualFeedback() {
            var that = this;
            $(this._settings.treeList).on("dblclick", this._treeNode, function () {
                var item = $(this);
                item.closest(that._treeItem).toggleClass(that._active)
            })
            $(this._settings.treeList).on("click", this._treeNode, function () {
                var item = $(this);
                var nodes = $(that._treeNode);
                nodes.removeClass(that._active);
                item.addClass(that._active);
                var data = that.getActiveView();
                that.$treeList.trigger("node:click:after", item);
            })
        }

        _initEvents() {
            var that = this;
            for (let k in this._events) {

                let item = k;
                let act = that._events[k];

                this.$treeList.on("click", item, function (e) {
                    return that._events[item].call(this, $(e.target).view())
                })
                // this.$treeList.on("click", k, (e) => this._events[k].call(e.target, $(e.target).view()));
            }
        }

        addSubNode(element: any, data: any) {
            notNull(data, "param 'data' cannot not null");
            notNull(element, "param 'element' cannot not null");
            if ($(element).view().data.SubItem && $(element).view().data.SubItem.length > 0) return;
            $.observable(element.view().data).setProperty("SubItem", data);
        }

        getActive(): JQuery {
            return this.$treeList.find(`${this._treeNode}.${this._active}`);
        }

        setActive(element: JQuery) {
            element.addClass(this._active);
        }

        setActiveToFirst() {
            if (this.getRootNode().length == 0) {
                return;
            }
            var firstElement = this.getRootNode().eq(0);
            this.setActive(firstElement);
        }

        getActiveView(): any {
            return (this.getActive() && this.getActive().view()) || {};
        }

        getActiveData(): any {
            return (this.getActive() && this.getActive().view() && this.getActive().view().data) || {};
        }

        getTreeData(): any {
            return (this.$treeList && this.$treeList.view()) || {};
        }

        getNodes() {
            return this.$treeList.find(this._treeNode);
        }

        getRootNode() {
            return $(this._treeNode);
        }

        //TODO 
        getChildNodes(parentId: string): any {

        }

        addNode(parentNode: any, data: any) {
            if (!parentNode) {
                $.observable(this._rootData).insert(data);
            } else {
                if (!parentNode.SubItem) parentNode.SubItem = [];
                $.observable(parentNode.SubItem).insert(data);
            }
            this.$treeList.trigger("node:add:after", {parentNode, data});
        }

        isRemove(viewData: any) {
            return !(viewData.data.SubItem && viewData.data.SubItem.length !== 0);
        }

        removeNode(viewData: any) {
            var data = viewData.data;
            var index = viewData.index;
            if (!confirm("是否确定删除?")) return false;
            this.setActiveToFirst();
            if (!data.ParentId) {
                $.observable(this._rootData).remove(index)
            } else {
                $.observable(viewData.parent.parent.data).remove(viewData.parent.parent.data.indexOf(data));
            }
            return true;
        }

        updateNode(targetData: any, newData: any) {
            for(let tK in targetData) {
                for(let nK in newData) {
                    if(tK.toString() == nK.toString()) {
                        $.observable(targetData).setProperty(tK, newData[tK])
                    }
                }
            }

                // .setProperty("Name", newData.Name)
                // .setProperty("OrderByNo", newData.OrderByNo);
        }

        set treeList(value: JQuery) {
            this.$treeList = value;
        }

        get treeList(): JQuery {
            return this.$treeList;
        }

        get tmplName(): any {
            return this._tmplName;
        }

        set tmplName(value: any) {
            this._tmplName = value;
        }
    }
}