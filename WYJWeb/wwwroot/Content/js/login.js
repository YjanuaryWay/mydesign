//把弹窗放在body内部第一层
$(function () {
    $(".modal").appendTo("body");
})
//Tab切换
$(document).ready(function () {
    jQuery.jqtab = function (nav, content, click) {
        $(content).find(".tab__pane").hide();
        $(nav).find(".nav__item:first").addClass("active").show();
        $(content).find(".tab__pane:first").show();
        $(nav).find(".nav__item").bind(click, function () {
            $(this).addClass("active").siblings(".nav__item").removeClass("active");

            var activeindex = $(nav).find(".nav__item").index(this);
            $(content).children().eq(activeindex).show().siblings().hide();
            //return false;
        });
    };
    $.jqtab(".nav", ".tab-content", "click");
});
//下拉点击展开
var $menu_demo = $('.nav_stacked');
$menu_demo.on('click', 'li', function () {
    $(this).next('.nav_stacked').slideToggle().siblings('.nav_stacked').slideUp();
});
$(function () {
    $("#ShowModal1").click(function () {
        $("#Modal1").addClass("active");
    })
    $("#ShowModal2").click(function () {
        $("#Modal2").addClass("active");
    })
    $("#ShowModal3").click(function () {
        $("#Modal3").addClass("active");
    })
    $("#ShowModal4").click(function () {
        $("#Modal4").addClass("active");
    })
    $("#ShowModal5").click(function () {
        $("#Modal5").addClass("active");
    })
    $("#ShowModal6").click(function () {
        $("#Modal6").addClass("active");
    })
    $("#ShowModal7").click(function () {
        $("#Modal7").addClass("active");
    })
    $(".BtnClose-js").click(function () {
        $(".modal,.prompt,prompt-alert").removeClass("active");
    })
})
$(function () {
    $(".Js-Detail").hide();
    $(".Js-Edit")
        .click(function () {
            $(".Js-List").hide();
            $(".Js-Viewer").hide();
            $(".Js-Editor").show();
            $(".Js-Detail").show();
        })
    $(".Js-Add")
        .click(function () {
            $(".Js-List").hide();
            $(".Js-Viewer").hide();
            $(".Js-Editor").show();
            $(".Js-Detail").show();
        })
    $(".Js-View")
        .click(function () {
            $(".Js-List").hide();
            $(".Js-Editor").hide();
            $(".Js-Viewer").show();
            $(".Js-Detail").show();
        })
    $(".Js-Return")
        .click(function () {
            $(".Js-List").show();
            $(".Js-Detail").hide();
        });
    $(".Js-Save")
        .click(function () {
            $(".Js-List").show();
            $(".Js-Detail").hide();
        });
    $(".Js-divEditor").hide();
    $(".Js-ShowdivEditor")
        .click(function () {
            $(".Js-divViewer").hide();
            $(".Js-divEditor").show();
            $(".Js-Save")
                .click(function () {
                    $(".Js-divViewer").show();
                    $(".Js-divEditor").hide();

                })
        });
    //登录注册
    $("#Js-Forgot").hide();
    $("#Js-Registered").hide();
    $(".Js-ShowForgot").click(function () {
        $("#Js-Login").hide();
        $("#Js-Registered").hide();
        $("#Js-Forgot").show();
    })
    $(".Js-ShowRegistered").click(function () {
        $("#Js-Login").hide();
        $("#Js-Forgot").hide();
        $("#Js-Registered").show();
    })
    $(".Js-ShowLogin").click(function () {
        $("#Js-Forgot").hide();
        $("#Js-Registered").hide();
        $("#Js-Login").show();
    })
});
//下拉面板
$(function () {
    $(".panel__hd").click(function () {
        $(this).closest(".collapse").toggleClass("active");
    })
})
//下拉菜单
$(function () {
    $(".dropdown__toggle").click(function () {
        $(this).closest(".dropdown").toggleClass("active");
    })
})
//收缩菜单
$(function () {
    $(".menu__title").click(function () {
        $(this).closest(".menu").toggleClass("menu_open");
    })
})
//$(function () {
//    var $sidebar = $(".sidebar");
//    //菜单展开收缩
//    $sidebar.on("click", "li.topMenu", function () {
//        var $this = $(this);
//        var $submenu = $this.find('.submenus');

//        $submenu.toggle();
//        if ($submenu.is(":visible")) {
//            $this.addClass("active");
//        } else {
//            $this.removeClass("active");
//        }
//    });

//    var current = '@(PageData[PageHelper.ItemId])';
//    if (current) {
//        var $item = $(".sidebar a[data-itemid=" + current + "]");
//        if ($item.length === 0) { window.location.href = "/nopower"; }
//        $item.parents("li").addClass("active");
//    }
//    else $(".sidebar .home").addClass("active");
//});
//收缩侧边菜单栏
$(function () {
    $(".menus__toggler").click(function () {
        $("body").toggleClass("page-sidebar-closed");
    })
})
//开关侧边窗口
$(function () {
    $(".Js-ShowSideview")
        .click(function () {
            $(".Js-Sideview").addClass("active");
            $(".Js-divEditor").hide();
            $(".Js-divViewer").show();
        })
    $(".Js-ShowSideviewEdit")
        .click(function () {
            $(".Js-Sideview").addClass("active");
            $(".Js-divEditor").show();
            $(".Js-divViewer").hide();
        })
    $(".Js-Save")
        .click(function () {
            $(".Js-divEditor").hide();
            $(".Js-divViewer").show();
        })
    $(".Js-Close")
        .click(function () {
            $(".Js-Sideview").removeClass("active");
            $(".Js-divEditor").hide();
            $(".Js-divViewer").hide();
        })
});

//$(function () {
//    $(".Js-ShowSideview").click(function (event) {
//        var e = window.event || event;
//        if (e.stopPropagation) {
//            e.stopPropagation();
//        } else {
//            e.cancelBubble = true;
//        }
//        $(".Js-Sideview").addClass("active");
//        $(".Js-divViewer").show();
//        $(".Js-divEditor").hide();
//    });
//    $(".Js-ShowSideviewEdit").click(function (event) {
//        var e = window.event || event;
//        if (e.stopPropagation) {
//            e.stopPropagation();
//        } else {
//            e.cancelBubble = true;
//        }
//        $(".Js-Sideview").addClass("active");
//        $(".Js-divViewer").hide();
//        $(".Js-divEditor").show();
//        $(".Js-Save")
//            .click(function () {
//                $(".Js-divViewer").show();
//                $(".Js-divEditor").hide();
//            })
//    });

//    $(".Js-Sideview").click(function (event) {
//        var e = window.event || event;
//        if (e.stopPropagation) {
//            e.stopPropagation();
//        } else {
//            e.cancelBubble = true;
//        }
//    });
//    document.onclick = function () {
//        $(".Js-Sideview").removeClass("active");
//    };
//    $(".Js-Close").click(function () {
//        $(".Js-Sideview").removeClass("active");
//    })
//})  

//select下拉选择出行内容填写区
$(function () {
    $('#div1,#div2,#div3').addClass('none');
    $('#type').change(function () {
        var value = $("#type").find("option:selected").val();
        if (value == '0') {
            $('#div3,#div2, #div1').addClass('none');
        }
        if (value == '1') {
            $('#div1').removeClass('none');
            $('#div2').addClass('none');
            $('#div3').addClass('none');
        }
        if (value == '2') {
            $('#div2').removeClass('none');
            $('#div1').addClass('none');
            $('#div3').addClass('none');
        }
        if (value == '3') {
            $('#div3').removeClass('none');
            $('#div1').addClass('none');
            $('#div2').addClass('none');
        }
    });
});

//下拉菜单
$(function () {
    $(".menus li").click(function () {
        $(this).toggleClass("active");
    })
})