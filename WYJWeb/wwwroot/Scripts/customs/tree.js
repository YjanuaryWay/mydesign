// import { Validate } from "./Validate";
var Mainyf;
(function (Mainyf) {
    function notNull(obj, message) {
        if (!obj)
            console.error(message);
    }
    function isTree(bool, message) {
        if (!bool)
            console.error(message);
    }
    var TreeGraph = /** @class */ (function () {
        function TreeGraph(settings) {
            this._active = "active";
            this._timerId = null;
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
        TreeGraph.prototype.initialization = function (data) {
            notNull(data, "param 'data' cannot not null");
            this._rootData = data;
            this.$treeList.Link(this._tmplName, this._rootData);
            this._initEvents();
            this._initVisualFeedback();
            this.$treeList.find(this._treeNode).eq(0).trigger("click");
        };
        /**
         * initialization visual feedback
         */
        TreeGraph.prototype._initVisualFeedback = function () {
            var that = this;
            $(this._settings.treeList).on("dblclick", this._treeNode, function () {
                var item = $(this);
                item.closest(that._treeItem).toggleClass(that._active);
            });
            $(this._settings.treeList).on("click", this._treeNode, function () {
                var item = $(this);
                var nodes = $(that._treeNode);
                nodes.removeClass(that._active);
                item.addClass(that._active);
                var data = that.getActiveView();
                that.$treeList.trigger("node:click:after", item);
            });
        };
        TreeGraph.prototype._initEvents = function () {
            var that = this;
            var _loop_1 = function (k) {
                var item = k;
                var act = that._events[k];
                this_1.$treeList.on("click", item, function (e) {
                    return that._events[item].call(this, $(e.target).view());
                });
            };
            var this_1 = this;
            for (var k in this._events) {
                _loop_1(k);
            }
        };
        TreeGraph.prototype.addSubNode = function (element, data) {
            notNull(data, "param 'data' cannot not null");
            notNull(element, "param 'element' cannot not null");
            if ($(element).view().data.SubItem && $(element).view().data.SubItem.length > 0)
                return;
            $.observable(element.view().data).setProperty("SubItem", data);
        };
        TreeGraph.prototype.getActive = function () {
            return this.$treeList.find(this._treeNode + "." + this._active);
        };
        TreeGraph.prototype.setActive = function (element) {
            element.addClass(this._active);
        };
        TreeGraph.prototype.setActiveToFirst = function () {
            if (this.getRootNode().length == 0) {
                return;
            }
            var firstElement = this.getRootNode().eq(0);
            this.setActive(firstElement);
        };
        TreeGraph.prototype.getActiveView = function () {
            return (this.getActive() && this.getActive().view()) || {};
        };
        TreeGraph.prototype.getActiveData = function () {
            return (this.getActive() && this.getActive().view() && this.getActive().view().data) || {};
        };
        TreeGraph.prototype.getTreeData = function () {
            return (this.$treeList && this.$treeList.view()) || {};
        };
        TreeGraph.prototype.getNodes = function () {
            return this.$treeList.find(this._treeNode);
        };
        TreeGraph.prototype.getRootNode = function () {
            return $(this._treeNode);
        };
        //TODO 
        TreeGraph.prototype.getChildNodes = function (parentId) {
        };
        TreeGraph.prototype.addNode = function (parentNode, data) {
            if (!parentNode) {
                $.observable(this._rootData).insert(data);
            }
            else {
                if (!parentNode.SubItem)
                    parentNode.SubItem = [];
                $.observable(parentNode.SubItem).insert(data);
            }
            this.$treeList.trigger("node:add:after", { parentNode: parentNode, data: data });
        };
        TreeGraph.prototype.isRemove = function (viewData) {
            return !(viewData.data.SubItem && viewData.data.SubItem.length !== 0);
        };
        TreeGraph.prototype.removeNode = function (viewData) {
            var data = viewData.data;
            var index = viewData.index;
            if (!confirm("是否确定删除?"))
                return false;
            this.setActiveToFirst();
            if (!data.ParentId) {
                $.observable(this._rootData).remove(index);
            }
            else {
                $.observable(viewData.parent.parent.data).remove(viewData.parent.parent.data.indexOf(data));
            }
            return true;
        };
        TreeGraph.prototype.updateNode = function (targetData, newData) {
            for (var tK in targetData) {
                for (var nK in newData) {
                    if (tK.toString() == nK.toString()) {
                        $.observable(targetData).setProperty(tK, newData[tK]);
                    }
                }
            }
            // .setProperty("Name", newData.Name)
            // .setProperty("OrderByNo", newData.OrderByNo);
        };
        Object.defineProperty(TreeGraph.prototype, "treeList", {
            get: function () {
                return this.$treeList;
            },
            set: function (value) {
                this.$treeList = value;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(TreeGraph.prototype, "tmplName", {
            get: function () {
                return this._tmplName;
            },
            set: function (value) {
                this._tmplName = value;
            },
            enumerable: true,
            configurable: true
        });
        return TreeGraph;
    }());
    Mainyf.TreeGraph = TreeGraph;
})(Mainyf || (Mainyf = {}));
//# sourceMappingURL=tree.js.map