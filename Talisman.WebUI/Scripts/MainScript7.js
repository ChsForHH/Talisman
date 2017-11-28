﻿function Service() {
    var els = $(".liService");
    els.bind("click", clickFun);
}
function clickFun(e) {
    var s = $(this).data("num");
    var el = $("#" + s);
    var pos = Math.round(el.offset().top - 75);    
    $.scrollTo(pos + "px", 500, { axis: "y" });
}
function CBShow() {
   
    var el = $("#backCall");
    var w = el.width();       
    $("#outwr").width(w);    
    $("#outwr").load("/CB");
    $("#outwr").show();  
}
function closeCB() {
    $("#outwr").hide();   
}
function ShowLeft() {
    var wrnav = document.getElementById('wrnav');
    if ($(wrnav).hasClass("fixed")) {        
        $("#wr").css("top", window.pageYOffset+10+"px");        
    }
    else {
        $("#wr").css("top", "5px");
    }
    $("#wr").show();    
    $("#wr").animate({ left: 0 }, { duration: 400 });
    var el = $("#picList");
    el.attr("onclick", "HideLeft()");
    el.attr("title", "Скрыть боковое меню");
    el.text("-");    
}
function HideLeft() {
    $("#wr").animate({ left: -220 }, { duration: 400 }).hide(400);    
    var el = $("#picList");
    el.attr("onclick", "ShowLeft()");
    el.attr("title", "Показать боковое меню");
    el.text("+");
}

function fbHeight() {
    if ($("#wr").css("position") == "static") {        
        if ($("#wrFB").height() < $("#wr").height()) {
            $("#wrFB").css("height",$("#wr").height()+"px");
        }
    }
    else {
        $("#wrFB").css("height", "auto");
    }
}
function posHeight() {
    if ($("#wr").css("position") == "static") {        
        if ($("#posBlock").height()+35 < $("#wr").height()) {
            $("#posBlock").css("height",$("#wr").height()+"px");
        }
    }
    else {
        $("#posBlock").css("height", "auto");
    }
}
$(window).resize(function () {    
    if ($("#wr").css("position")=="static") {        
        var y = window.pageYOffset;
        if (y > 0) {
            $.scrollTo(y - 1 + "px", 0, { axis: "y" });
            if (document.getElementById("leftNavbar").getBoundingClientRect().bottom - document.documentElement.clientHeight <= 0) {
                $.scrollTo(y + 2 + "px", 200, { axis: "y" });               
            }                   
        }
    }
    if ($("#posBlock").length > 0) {
        posHeight();
    }
    if ($("#wrFB").length > 0) {
        fbHeight();
    }
    if (document.documentElement.clientWidth >= 500) {
        if ($("#wr").css("display") == "none") $("#wr").show();
    }
});
function leftNavBigger() {
    var el = document.getElementById("leftNavbar");
    var hel = el.clientHeight;   
    var marg = document.documentElement.clientWidth / 100 * 1.5;    
    var h = document.documentElement.clientHeight -
        document.getElementById("wrnav").clientHeight;   
    if ($("#wr").css("position") == "static") {
        if (hel < h) el.style.height = h + "px";
    }
}
$(document).ready(function () {    
    showVisible();
    if ($("#posBlock").length > 0) {
        posHeight();
    }    
    leftNavBigger();
    var y = window.pageYOffset;
    if (y > 0) {
        if (y > 50) {
            $.scrollTo(y - 50 + "px", 0, { axis: "y" });
            $.scrollTo(y + 1 + "px", 0, { axis: "y" });
        }
        else {
            $.scrollTo(y - 1 + "px", 0, { axis: "y" });
        }        
    }  
    var menuElem = document.getElementById("wrnav");
    var butUp = $(".bUp");
    var menuSourceBottom = menuElem.getBoundingClientRect().top + window.pageYOffset;
    var fix=false;
    var elF = $("#wr");
    window.addEventListener('scroll', function () {
        if (window.pageYOffset <= menuSourceBottom && menuElem.classList.contains('fixed')) {
            if (fix) {
                fix = false;                
                menuElem.classList.remove('fixed'); butUp.css("opacity", "0"); $("#buttonUp").css("cursor", "default");               
                $("#wrnav").css("box-shadow", "none");
            }
        }
        if (window.pageYOffset > menuSourceBottom && !menuElem.classList.contains('fixed')) {
            if (!fix) {
                fix = true;
                menuElem.classList.add('fixed'); butUp.css("opacity", "1"); $("#buttonUp").css("cursor", "pointer");
                $(menuElem).css("top", "0");               
                $("#wrnav").css("box-shadow", "0 4px 8px -4px #000");
            }
        }
    });    
    function Defaults() {        
        el_fl.style.top = "0";
        el_fl.style.position = "relative";
        el_fl.style.bottom = "auto";
    }
    function FixBottom() {        
        var fp1 = p01 = Math.round(document.documentElement.clientHeight - el_fl.getBoundingClientRect().height);        
        el_fl.style.top = fp1 + "px";
        el_fl.style.position = "fixed";
        el_fl.style.bottom = "auto";
    };
    function AbsBottom() {
        var vw15 = $(window).width() / 100 * 1.5 + "px";        
        el_fl.style.top = "auto";
        el_fl.style.position = "absolute";
        el_fl.style.bottom = vw15;
    };
    function FixTop() {        
        el_fl.style.top = document.getElementById('wrnav').clientHeight+ "px";
        el_fl.style.position = "fixed";
        el_fl.style.bottom = "auto";
    };
    function ChangeBottom() {
        var h = el_fl.getBoundingClientRect().height,
            d = h - document.documentElement.clientHeight,
            wrTop = document.getElementById('wr').getBoundingClientRect().top,            
            px = -wrTop - 30;        
        el_fl.style.top = px + "px";
        el_fl.style.position = "relative";
        el_fl.style.bottom = "auto";
    };
    function ChangeTop() {
        var h = el_fl.getBoundingClientRect().height,
            d = h - document.documentElement.clientHeight,
            wrTop = document.getElementById('wr').getBoundingClientRect().top,
            px = Math.round(-wrTop - d);        
        el_fl.style.top = px + "px";        
        el_fl.style.position = "relative";
        el_fl.style.bottom = "auto";        
    };
    var Delta = 0;
    var sost = 1;
    var Def = true;
    var FBot = false;
    var FTop = false;
    var ABot = false;
    var CTop = false;
    var CBot = false;
    var el_fl = document.getElementById('leftNavbar');
    var main_El = document.getElementById('main');    
    function Scrolling() {
        if ($("#wr").css("position") == "static") {            
            if (el_fl.clientHeight < main_El.clientHeight) {
                if (el_fl.clientHeight >= document.documentElement.clientHeight - document.getElementById("wrnav").clientHeight) {
                    if (main_El.getBoundingClientRect().top >= 0) {
                        if (!Def) {
                            Defaults();
                            Def = true;
                            FBot = false;
                            FTop = false;
                            ABot = false;
                            CTop = false;
                            CBot = false;
                        }
                    }
                    else {
                        if (main_El.getBoundingClientRect().bottom - document.documentElement.clientHeight <= 0) {
                            if (!ABot) {
                                AbsBottom();
                                Def = false;
                                FBot = false;
                                FTop = false;
                                ABot = true;
                                CTop = false;
                                CBot = false;
                            }
                        }
                        else {
                            if (document.getElementById('header').getBoundingClientRect().top < Delta) {                                            
                                if (FTop) {
                                    ChangeBottom();
                                    Def = false;
                                    FBot = false;
                                    FTop = false;
                                    ABot = false;
                                    CTop = true;
                                    CBot = false;
                                }
                                if (el_fl.getBoundingClientRect().bottom - document.documentElement.clientHeight <= 0) {                                    
                                    if (!FBot) {
                                        FixBottom();
                                        Def = false;
                                        FBot = true;
                                        FTop = false;
                                        ABot = false;
                                        CTop = false;
                                        CBot = false;
                                    }
                                }
                            }
                            else { 
                                if (FBot) {
                                    ChangeTop();
                                    Def = false;
                                    FBot = false;
                                    FTop = false;
                                    ABot = false;
                                    CTop = true;
                                    CBot = false;
                                }
                                if (el_fl.getBoundingClientRect().top >= 30) {
                                    if (!FTop) {
                                        FixTop();
                                        Def = false;
                                        FBot = false;
                                        FTop = true;
                                        ABot = false;
                                        CTop = false;
                                        CBot = false;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            Delta = document.getElementById('header').getBoundingClientRect().top;
        }
    }
    document.addEventListener('scroll', Scrolling, false);
    window.addEventListener('resize', function () {
        if ($("#wr").css("position") == "absolute") {
            $("#leftNavbar").css("position", "relative");
            $("#leftNavbar").css("top", "0");
        }
    }, false);   
    jQuery('.bUp').click(function () {
        $.scrollTo('0', 500);
    });
    if ($(".wrAbs").length > 0) {
        var hi = $(".wrAbs").height();
        $(".mainImages").height(hi);
    }
});
function completeCB() {
    if ($(".validation-summary-errors").length == 0) {
        setTimeout((function () {
            $("#outwr").hide();
            $("#outwr").load("/Home/CallBack");
        }), 1500)
    }
}
$(document).click(function (e) {
    var elem = $("#backCall");
    if (e.target != elem[0] && !elem.has(e.target).length) {
        closeCB();
    }
    var el2 = $("#smth");
    if (e.target != el2[0] && !el2.has(e.target).length) {
        if ($("#loginW").length > 0) {
            el2.remove();
            $("#wrLogin").html(GlobalEl);
        }
    }
});
function isVisible(elem) {
    var coords = elem.getBoundingClientRect();
    var windowHeight = document.documentElement.clientHeight;
    var extendedTop = -windowHeight / 2;
    var extendedBottom = windowHeight + windowHeight / 2;
    var topVisible = coords.top > extendedTop && coords.top < extendedBottom;
    var bottomVisible = coords.bottom < extendedBottom && coords.bottom > extendedTop;
    return topVisible || bottomVisible;
}
function showVisible() {
    var imgs = document.getElementsByClassName('mainImagesm');
    if (imgs.length > 0) {
        for (var i = 0; i < imgs.length; i++) {
            var img = imgs[i];
            var realsrc = img.getAttribute('data-path');
            if (!realsrc) continue;
            if (isVisible(img)) {
                if (document.documentElement.clientWidth > 700) {
                    img.src = realsrc;
                }
                else {
                    var src2 = realsrc.replace("s1", "s2");
                    img.src = src2;
                }
                img.setAttribute('realsrc', '');
            }
        }
    }
}
window.addEventListener('scroll', showVisible);


