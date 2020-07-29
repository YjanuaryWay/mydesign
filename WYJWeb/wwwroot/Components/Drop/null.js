/**   
 * 按列合并单元格
 * 自由模式：cols中的各列獨立合并，互不影響，
 * 左看齊模式：cols中的各列以其前面一列為參照。
 * 首列看齊模式：將cols中的第一列作爲其他列的參照。
 * @param {Int16Array} cols :列索引或索引數組
 * @param {any} type :默認為左看齊模式，type=1時為首列看齊模式，其它真值為自由模式
 *
 * 示例：
 * try 
 *	{ 
 *		$('#table').mergeColsCell([4, 2, 6, 5, 3], 1).mergeColsCell(1); 
 *	}
 *  catch (e) 
 *  {
 *		alert(e);
 *	}
 *  依赖jQuery
 */
jQuery.fn.mergeColsCell = function (cols, type) {
    if (Number.isInteger(cols) && cols >= 0) cols = [cols];
    if (!Array.isArray(cols)) throw "cols正確的參數格式為:列索引或列索引數組,比如0或[0,1]";
    cols = [...new Set(cols)];
    var merge = function (currentCells, baseCells, type) {
        for (let colIdx = 0; colIdx < currentCells.length; colIdx++) {
            if (currentCells[colIdx].innerText === baseCells[colIdx].innerText) {
                var n = Number(baseCells[colIdx].getAttribute("rowSpan")) || 1;
                baseCells[colIdx].setAttribute("rowSpan", n + 1);
                $(currentCells[colIdx]).remove();
                //currentCells[colIdx].setAttribute('hidden', true)
            } else {
                baseCells[colIdx] = currentCells[colIdx];
                if (!type || (type === 1 && colIdx === 0)) {
                    break
                }
            }
        }

    };

    return this.each(function (baseCells, cells, currentCells) {
        cells = $("tbody>tr", this).map(function () {
            return this.cells;
        });

        baseCells = cols.map(function (col) {
            if (cells[0][col]) {
                return cells[0][col]
            }
            throw `請檢查列索引‘${col}’是否正確/超出範圍？`;
        });

        for (let row = 1; row < cells.length; row++) {
            currentCells = cols.map(function (col) {
                return cells[row][col];
            });
            merge(currentCells, baseCells, type)
        };
    });
};

/**
 * 从表单获取实体
 * @param {string} ignoreClass
 * 依赖jQuery
 */
jQuery.fn.FormToModel = function (ignoreClass) {
    if (ignoreClass === undefined) ignoreClass = "ignore";
    var $form = $(this).find(`:input:not(:button,.${ignoreClass},:radio:not(:checked),:checkbox:not(:checked,.ValueToBool))`);
    var o = {};
    $form.each(function (index, ele) {
        var e = $(ele);
        var name = e.prop("name") || e.prop("id");
        if (name) o[name] = e.hasClass("ValueToBool") ? e.is(":checked") : e.val();
    });
    return o;
}

/**
 * 重置表单
 * 
 */
jQuery.fn.ResetForm = function () {
    var $form = $(this).find(`:input:not(:button,:radio,:checkbox)`);

}


$('[data-EnterToClick]').on("keyup", function (e) {
    if (e.keyCode == 13) {
        var id = '#' + $(this).data('entertoclick');
        $(id).click();
    }
});
