﻿var GlobalFS = false;
var GlobalFS2 = false;
var GlobalFIsOut = 0;
var countCurrentMini = 0;
var xTop = 0;
var list = new Array();
var list2 = new Array();
var currentIndex = 0;
var currentPI = 0; 
var currentIndex2 = 0;
var currentId;
var img;
var data;
function Init2() {
    var el = $(".posMiniImg");
    if (el.length > 0) {
        for (var i = 0; i < el.length; i++) {
            list[i] = el[i].id;
        }
    }
    else {
        list[0] = $(".wrAbsm1").attr("data");
    }
    currentPI = $("#firstPhoto").attr("data");
    setByIndex();
    setImg();
    Effects();
};
function nextImg() {

    if (currentPI < (list.length - 1)) {
        currentPI++;
    }
    else {
        currentPI = 0;
    }
    setByIndex();
    setImg();
};
function prevImg() {
    if (currentPI > 0) {
        currentPI--;
    }
    else {
        currentPI = list.length - 1;
    }
    setByIndex();
    setImg();
};
function getImg(id) {
    currentId = id;
    setImg();
};
function setByIndex() {

    currentId = list[currentPI];

    var n = currentPI; n++;
    $("#num1").text(n.toString());
};

function scrollWidth() {
    var div = $('<div>').css({
        position: "absolute",
        top: "0px",
        left: "0px",
        width: "100px",
        height: "100px",
        visibility: "hidden",
        overflow: "scroll"
    });

    $('body').eq(0).append(div);
    var width = div.get(0).offsetWidth - div.get(0).clientWidth;
    div.remove();
    return width;
};
window.addEventListener("resize", function () {    
    Sizing(); Minisizing();
});
function Sizing() {
    var winWidth = document.documentElement.clientWidth;
    var winHeight = document.documentElement.clientHeight;
    var mainP = $("#mainP");
    var RP = $(".RP");
    var posNum = $("#posNum");
    $("#mainPlug").css("padding", winHeight / 100 + "px");
    if (winWidth / winHeight < 1.86) {
        mainP.css("height", Math.round(winWidth * 0.535) + "px");
        mainP.css("width", "100%");
        RP.css("width", Math.round(winWidth * 0.6867) + "px");
        RP.css("height", Math.round(winWidth * 0.515) + "px");
        posNum.css("width", 0.14 * winWidth + "px");
    }
    else {
        mainP.css("width", winHeight * 1.86 + "px");
        mainP.css("height", "100%");
        RP.css("height", winHeight * 0.98 + "px");
        RP.css("width", winHeight * 1.3067 + "px");
        posNum.css("width", winHeight * 0.26 + "px");
    }
};
function Effects() {
    Sizing();
    var sh = $("#shadow");
    sh.show().fadeTo(400, 0.7);
    $("body").css("overflow", "hidden");
    var wpr = scrollWidth();
    var isIE = false || !!document.documentMode;
    if (!isIE) {
        $("body").css("padding-right", wpr + "px");
    }
    var mDiv = $("#wrPlugm");
    mDiv.animate({ top: 0, left: 0, width: "show", height: "show", opacity: 1 }, { duration: 500 });
    mOver = true;
    $("#outPlugm").css("opacity", "0.5"); 
    if (!GlobalFS && !Andr) {
        $("#resFm").css("display", "inline");
    }
    $("#wrButs").css("opacity", "1");
};
function setImg() {
    var el = $("#wrwrm");
    el.css("opacity", "0");
    if (document.documentElement.clientWidth > 700) {
        $("#plImgm").attr("src", "/Content/TovarPhotos/" + currentId + ".jpeg");
    }
    else {
        $("#plImgm").attr("src", "/Content/TovarPhotos/" + currentId + "s1" + ".jpeg");
    }
};

function f1() {
    AddClasses(this);
    $("#wrwrm").fadeTo(400, 1, function () {
        if (document.documentElement.clientWidth > 700) {
            $("#plImgBm").attr("src", "/Content/TovarPhotos/" + currentId + ".jpeg");
        }
        else {
            $("#plImgBm").attr("src", "/Content/TovarPhotos/" + currentId + "s1" + ".jpeg");
        }
        $("#wrMinim").fadeTo(400, 1);
    });
};
function AddClasses(el) {
    $(el).removeClass();
    $("#plImgBm").removeClass();
    if (el.clientWidth > el.clientHeight) {
        if (Andr) {
            $(el).addClass("Hor2");
            $("#plImgBm").addClass("Hor2");
        }
        else {
            $(el).addClass("Hor");
            $("#plImgBm").addClass("Hor");
        }
    }
    else {
        if (Andr) {
            $(el).addClass("Vert2");
            $("#plImgBm").addClass("Vert2");
        }
        else {
            $(el).addClass("Vert");
            $("#plImgBm").addClass("Vert");
        }
    }
};
$("#posWrAllimg").ready(
    function () {        
        iter = 1;
        if ($("#poswrTop").length > 0) {           
            $("#posWrMini").scrollTo(0, 0, { axis: 'y' });
        }
    }
    );
$("#wrPlugm").ready(function () {
    if (document.CancelFullScreen) {
        document.onfullscreenchange =
                function (event) {                   
                    GlobalFIsOut++;
                    if (GlobalFIsOut == 2) {
                        GlobalFIsOut = 0;
                    }
                    if (GlobalFS2 && (GlobalFIsOut == 0)) {
                        FSOutWork();
                    }
                };
    } else if (document.mozCancelFullScreen) {
        document.onmozfullscreenchange =
                function (event) {
                    GlobalFIsOut++;
                    if (GlobalFIsOut == 2) {
                        GlobalFIsOut = 0;
                    }
                    if (GlobalFS2 && (GlobalFIsOut == 0)) {
                        FSOutWork();
                    }
                };
    } else if (document.webkitCancelFullScreen) {
        document.onwebkitfullscreenchange =
                function (event) {
                    GlobalFIsOut++;
                    if (GlobalFIsOut == 2) {
                        GlobalFIsOut = 0;
                    }
                    if (GlobalFS2 && (GlobalFIsOut == 0)) {
                        FSOutWork();
                    }
                };
    } else if (document.msExitFullscreen) {
        document.onmsfullscreenchange =
                function (event) {
                    GlobalFIsOut++;
                    if (GlobalFIsOut == 2) {
                        GlobalFIsOut = 0;
                    }
                    if (GlobalFS2 && (GlobalFIsOut == 0)) {
                        FSOutWork();
                    }
                };
    }

    $("#plImgm").bind("load", f1);    
    var elem = $("#mainPlug");   
    $("#wrPlugm").click(function (e) {       
        if (e.target != elem[0] && !elem.has(e.target).length)
            {           
            scroll = 0;
            $("#plImgm").attr("src", "~/Content/Images/pixel.png");
            $("#plImgBm").attr("src", "~/Content/Images/pixel.png");           
            $("#shadow").fadeTo(300, 0).hide(300);
            $("#wrPlugm").animate({ top: "50%", left: "50%", width: "hide", height: "hide", opacity: 0 }, { duration: 0 });            
            $("body").css("padding-right", "0")
            $("body").css("overflow", "auto");
        }
    });
    var mOver = true;    
    elem.hover(function (e) {        
        if (!mOver) {            
            mOver = true;
            $("#outPlugm").css("opacity", "0.5"); 
            if (!GlobalFS && !Andr) {               
                $("#resFm").css("display", "inline");
            }
            $("#wrButs").css("opacity", "1");
        }
    });
    elem.mouseleave(function (e) {        
        if (mOver) {            
            mOver = false;
            $("#outPlugm").css("opacity", "1");
            $("#resFm").css("display", "none");
            $("#wrButs").css("opacity", "0");
        }
    });
});

var Andr = false;
function FS() {
    var el = document.getElementById("relPlug");
    if (api.enabled()) {
        api.request(el);
        Change();
    }
    else {
        Android();
        Change();
        Andr = true;
    }
};

function Sm_S() {
    if (Andr) {
        FSOutWork();
        AndrExit();
        Andr = false;
    }
    else {
        api.exit();
        FSOutWork();
    }
}
function FSOutWork() {
    GlobalFS2 = false;
    $("#resFm").show();
    $("#resF2m").hide();   
    setTimeout((function () { GlobalFS = false; }), 500);
}
function Change() {
    GlobalFS = true;
    GlobalFS2 = true;
    $("#resFm").hide();
    $("#resF2m").show();   
}
var api = {};
api.enabled = function () {
    if ('fullscreenEnabled' in document) return document.fullscreenEnabled;
    if ('mozFullScreenEnabled' in document) return document.mozFullScreenEnabled;
    if ('webkitFullscreenEnabled' in document) return document.webkitFullscreenEnabled;
    if ('msFullscreenEnabled' in document) return document.msFullscreenEnabled;
    return false;
};
api.request = function (element) {
    if (element.requestFullscreen) element.requestFullscreen();
    if (element.mozRequestFullScreen) element.mozRequestFullScreen();
    if (element.webkitRequestFullscreen) element.webkitRequestFullscreen();
    if (element.msRequestFullscreen) element.msRequestFullscreen();
};

api.exit = function () {
    if (document.exitFullscreen) document.exitFullscreen();
    if (document.mozCancelFullScreen) document.mozCancelFullScreen();
    if (document.webkitCancelFullScreen) document.webkitCancelFullScreen();
    if (document.msExitFullscreen) document.msExitFullscreen();
};

function Android() {
    var el = $("#relPlug");
    $("#wrPlugm").css("position", "static");
    $("#mainPlug").css("position", "static");
    $("#mainP").css("position", "static");
    el.removeClass("RP");
    el.addClass("andr");
    $(".andr").css("width", "100%");
    $(".andr").css("height", "100%");
    $("#wrwrBm").css("background-color", "#1a191b");
    $("#wrwrm").css("background-color", "#1a191b");
    if ($("#plImgm").hasClass("Hor")) {
        $("#plImgm").removeClass("Hor");
        $("#plImgm").addClass("Hor2");
        $("#plImgBm").removeClass("Hor");
        $("#plImgBm").addClass("Hor2");
    }
    else {
        $("#plImgm").removeClass("Vert");
        $("#plImgm").addClass("Vert2");
        $("#plImgBm").removeClass("Vert");
        $("#plImgBm").addClass("Vert2");
    }
};
function AndrExit() {
    $("#wrPlugm").css("position", "fixed");
    $("#mainPlug").css("position", "relative");
    $("#mainP").css("position", "absolute");
    var el = $("#relPlug");
    el.removeClass("andr");
    el.addClass("RP");
    Sizing();
    $("#wrwrBm").css("background-color", "#69656d");
    $("#wrwrm").css("background-color", "#69656d");
    if ($("#plImgm").hasClass("Hor2")) {
        $("#plImgm").removeClass();
        $("#plImgm").addClass("Hor");
        $("#plImgBm").removeClass();
        $("#plImgBm").addClass("Hor");
    }
    else {
        $("#plImgm").removeClass();
        $("#plImgm").addClass("Vert");
        $("#plImgBm").removeClass();
        $("#plImgBm").addClass("Vert");
    }
};
function posShow(el, i) {    
    var id = $(el).attr("data");
    $("#firstPhoto").attr("data", i)    
    $(".pselectedMini").attr("class", "wrposMini pallMini");
    $(el).removeClass("pallMini").addClass("pselectedMini");   
    $(".wrAbsm2").css("opacity", "0");
    if (document.documentElement.clientWidth > 900) {
        $("#ff2").attr("src", $("#" + id).attr("src").replace("s2", ""));
        $(".wrAbsm2").fadeTo(400, 1, function () { $("#ff1").attr("src", $("#" + id).attr("src").replace("s2", "")); });
    }
    else {
        $("#ff2").attr("src", $("#" + id).attr("src").replace("s2", "s1"));
        $(".wrAbsm2").fadeTo(400, 1, function () { $("#ff1").attr("src", $("#" + id).attr("src").replace("s2", "s1")); });
    }    
}
var iter = 1;
function topMini() {
    var el = $("#wrposwr");
    var el2 = $("#poswrNext");
    var el3 = $("#poswrPrev");
    if (iter > $(".wrposMini").length - 4) {
        bAct(el2);
    }
    if (iter > 1) {
        iter--;
        if (iter == 1) {
            bPass(el3);
            scroll = 0.0;
        }
        el.scrollTo($(".tCell")[iter - 1], 400, { axis: 'x' });
    }
};
function bottomMini() {      
    var el = $("#wrposwr");
        var n = $(".wrposMini").length - 4;       
        if (iter == 1) {
            bAct($("#poswrPrev"));
        }
        if (iter <= n) {
            iter++;            
            el.scrollTo($(".tCell")[iter - 1], 400, { axis: 'x' });
            if (iter > n) {
               bPass($("#poswrNext"));
            }
        } 
};
function bAct(el) {
    el.css("opacity", "0.5");
    el.css("cursor", "pointer");
};
function bPass(el) {
    el.css("opacity", "0");
    el.css("cursor", "default");
};

function Minisizing() {
    var tabW = $("#posOutwrMini").width()*0.8;
    var inCellWidth = Math.round(tabW / 4);
    var inCellHeight = inCellWidth * 0.75;
    var wrPosMini = $(".wrposMini");
    wrPosMini.css("height", inCellHeight + "px");
    wrPosMini.css("width", inCellWidth + "px");
    wrPosMini.css("position", "relative");
    var tCell = $(".tCell");
    tCell.css("width", "auto");
    tCell.css("padding", "0");    
    $(".posWrMini").css("width", "auto");   
    $("#wrposwr").css("width", inCellWidth * 4 + 1 + "px");   
}
