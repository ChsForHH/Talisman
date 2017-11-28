var GlobalFS = false;
var GlobalFS2 = false;
var GlobalFIsOut = 0;
var paddingMain = 10;//основные отступы
var marginBottomMini = 5;//нижний отступ миниатюр
var MainHeight = 0;
var MainImgHeight = 0;
var Main1Width = 0;
var MiniHeight = 0;
var MiniWidth = 0;
var textHeight = 26;
var countCurrentMini = 0;
var xTop = 0;
/*--------------------------------------------------------*/
var list = new Array();
var list2 = new Array();
var currentIndex = 0;
var currentPI = 0;
var currentIndex2 = 0;
var currentId;
var img;
var data;
function Init(model) {


    data = model;
    for (var i = 0; i < model.Viewers.length; i++) {
        list[i] = model.Viewers[i].PhotoIds[0];
        if (model.Viewers[i].Tovar.TovarId == model.CurrentTovarId) {
            currentIndex = i;
        }
    }
    currentPI = 0;
    sizing();
    setByIndex();
    setImg();
    Effects();
}
function nextImg() {
    if (GlobalFS) {
        if (currentPI == data.Viewers[currentIndex].PhotoIds.length - 1) {
            currentPI = 0;
            if (currentIndex == data.Viewers.length - 1) {
                currentIndex = 0;
            }
            else {
                currentIndex++;
            }
        }
        else {
            currentPI++;
        }
    }
    else {
        if (currentIndex < (list.length - 1)) {
            currentIndex++;
        }
        else {
            currentIndex = 0;
        }

        currentPI = 0;
    }
    setByIndex();
    setImg();
}
function prevImg() {
    if (GlobalFS) {
        if (currentPI == 0) {

            if (currentIndex == 0) {
                currentIndex = data.Viewers.length - 1;
            }
            else {
                currentIndex--;
            }
            currentPI = data.Viewers[currentIndex].PhotoIds.length - 1;
        }
        else {
            currentPI--;
        }
    }
    else {
        if (currentIndex > 0) {
            currentIndex--;
        }
        else { currentIndex = list.length - 1 }
        currentPI = 0;

    }
    setByIndex();
    setImg();
}
function getImg(id) {
    currentId = id;
    setImg();
}
function setByIndex() {
    if (GlobalFS) {
        currentId = data.Viewers[currentIndex].PhotoIds[currentPI];
    }
    else {
        currentId = data.Viewers[currentIndex].PhotoIds[0];
        GetMini(0);
    }
}

function GetMini(semM) {
    var str = "";
    list2 = data.Viewers[currentIndex].PhotoIds;
    if (list2.length == 1) {
        $("#addB").css("display", "none");
    }
    else {
        $("#addB").css("display", "block");
    }
    document.getElementById("tovarName").innerText = data.Viewers[currentIndex].Tovar.Name;
    document.getElementById("currentMini").innerText = "1";
    document.getElementById("countMini").innerText = list2.length;
    var div = document.getElementById("wrMini");

    div.scrollTop = 0;
    $(div).empty();

    if (list2.length > 4) {
        scroll = 0;
        iter = 1;
        bPass($("#wrTopP"));
        bAct($("#wrBottomP"));
    }
    else {
        bPass($("#wrTopP"));
        bPass($("#wrBottomP"));
    }

    countCurrentMini = list2.length;
    for (var i = 0; i < list2.length; i++) {
        var mimg = new Image();
        if (i == semM) {
            mimg.className = "selectedMini";
        }
        else {
            mimg.className = "allMini";
        }
        mimg.src = "/Content/TovarPhotos/" + list2[i] + ".jpeg";
        mimg.style.height = MiniHeight + "px";
        mimg.data = list2[i];
        mimg.ind = i + 1;
        mimg.onclick = function () {
            getImg(this.data);
            document.getElementById("currentMini").innerText = this.ind;
            $(".selectedMini").attr("class", "allMini");
            this.className = "selectedMini";
            currentPI = this.ind - 1;
        };
        if (i < list2.length - 1) {
            mimg.style.marginBottom = marginBottomMini + "px";
        }
        div.appendChild(mimg);
    }
    div.style.maxHeight = (4 * MiniHeight + 3 * marginBottomMini + 1) + "px";
    div.style.width = MiniWidth + "px";
    $(div).fadeTo(0, 0);
}

function Effects() {

    var sh = $("#shadow");
    sh.show().fadeTo(400, 0.8);


    $("body").css("overflow", "hidden");
    var mDiv = $("#wrPlug");
    var pw = $("#mainPlug");
    var pw2 = $("#main1");

    mDiv.show();
    pw.show().css("opacity", "1");
    pw2.css("margin-top", xTop + "px")
    pw2.show().animate({ width: Main1Width, height: MainHeight, opacity: 1 }, { duration: 500 });
}
function setImg() {

    var el = $("#wrwr");
    el.css("opacity", "0");
    $("#outLayer").css("opacity", "1");
    $("#plImg").attr("src", "/Content/TovarPhotos/" + currentId + ".jpeg");
}

function f1() {

    $("#wrwr").fadeTo(400, 1, function () {
        $("#outLayer").css("opacity", "0");
        $("#plImgB").attr("src", "/Content/TovarPhotos/" + currentId + ".jpeg");
        $("#wrMini").fadeTo(400, 1);
    });

}

function f2() {
    setTimeout((function () { $("#outLayer").css("opacity", "1"); }), 100);
}
function sizing() {
    var H = Math.round($(window).height());
    MainHeight = H;
    var W = Math.round($(window).width());
    var D = W - MainHeight;
    var C = 2 * paddingMain + textHeight;
    MainImgHeight = MainHeight - C;
    var MainImgWidth = Math.round(MainImgHeight * (4.0 / 3));
    var mainPWidth = MainImgWidth + 4 * paddingMain;
    MiniHeight = Math.round((MainHeight - 3 * marginBottomMini) / 5) - 1;
    MiniWidth = Math.round(MiniHeight * (4.0 / 3)) - 1;
    Main1Width = MainImgWidth + 2 * paddingMain + 2 * (MiniWidth + paddingMain) - 2;
    var B = Main1Width - MainHeight;
    if (B > D) {
        MainImgWidth = Math.round((W - 4.0 * paddingMain - marginBottomMini) / 1.4);
        MainImgHeight = Math.round(MainImgWidth * 3.0 / 4.0);
        MainHeight = MainImgHeight + C;
        xTop = Math.round((H - MainHeight) / 2.0);
        MiniHeight = Math.round((MainHeight - 3 * marginBottomMini) / 5) - 1;
        MiniWidth = Math.round(MiniHeight * (4.0 / 3)) - 1;

        Main1Width = W;
        $("#mainPlug").css("width", (MainImgWidth + 2 * paddingMain) + "px");
        $("#relPlug").css("height", MainImgHeight + "px");

    }
    else {
        $("#mainPlug").css("width", (MainImgWidth + 2 * paddingMain) + "px");
        $("#relPlug").css("height", MainImgHeight);
        xTop = 0;
    }
}

$("#wrPlug").ready(function () {
    if (document.CancelFullScreen) {
        document.onfullscreenchange =
                function (event) {
                    alert("!");
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

    window.onresize = function () {
        if (!GlobalFS) {
            if ($("#main1").width() > 0) {
                sizing();

                setByIndex();
                setImg();
                Effects();
            };
        }
        if ($(".wrAbs").length > 0) {
            var hi = $(".wrAbs").height();
            $(".mainImages").height(hi);
        }
    }
    iter = 1;
    $("#plImg").bind("load", f1);
    var elem = $("#mainPlug");
    var el2 = $("#addB");
    $("#wrPlug").click(function (e) {
        var div = document.getElementById("wrMini");
        if ((e.target != elem[0] && !elem.has(e.target).length)
            && (e.target != el2[0] && !el2.has(e.target).length)) {
            div.scrollTop = 0;
            scroll = 0;
            $(div).empty();
            $("#main1").fadeTo(200, 0, function () {
                $("#wrPlug").hide();
                $("#main1").css("width", "0");
                $("#main1").css("height", "0");
                $("#main1").hide();
                document.getElementById("wrwr").style.backgroundImage = "none";
            });
            $("body").css("overflow", "auto");
            $("#wrwr").css("opacity", "0");
            $("#shadow").fadeTo(400, 0).hide(400);
        }
    });
    $(".vsb").hover(function (e) {
        $("#outPlug").css("opacity", "0.5");
        $("#addB").css("opacity", "1");
    });
    $(".vsb").mouseleave(function (e) {
        if ((e.target = elem[0] && !elem.has(e.target).length) && (e.target = el2[0] && !el2.has(e.target).length)) {
            $("#outPlug").css("opacity", "1");
            $("#addB").css("opacity", "0");
        }
    });
});
var scroll = 0;
var iter = 1;
function topMini() {
    if (countCurrentMini > 4) {
        var el = $("#wrMini");
        var el2 = $("#wrBottomP");
        var el3 = $("#wrTopP");
        if (iter > countCurrentMini - 4) {
            bAct(el2);
        }
        if (iter > 1) {
            iter--;
            scroll -= (MiniHeight + marginBottomMini);
            el.scrollTo(scroll + "px", 300, { axis: 'y' });
            if (iter == 1) {
                bPass(el3);
            }
        }

    }
}
function bottomMini() {
    if (countCurrentMini > 4) {
        var el = $("#wrMini");
        var n = countCurrentMini - 4;
        if (iter == 1) {
            bAct($("#wrTopP"));
        }
        if (iter <= n) {
            iter++;
            scroll += (MiniHeight + marginBottomMini);
            el.scrollTo(scroll + "px", 300, { axis: 'y' });
            if (iter > n) {
                bPass($("#wrBottomP"));
            }
        }
    }

};
function bAct(el) {
    el.css("opacity", "0.7");
    el.css("cursor", "pointer");
}
function bPass(el) {
    el.css("opacity", "0.3");
    el.css("cursor", "default");
};
/*--------------------------------------полноэкранный режим1-------------------------*/
function FS() {
    var el = document.getElementById("relPlug");
    if (api.enabled()) {
        api.request(el);
        Change();
    }
    else {
        alert("Браузер не поддерживает идею!");
    }
}

function Sm_S() {

    api.exit();
    FSOutWork();
}
function FSOutWork() {
    GlobalFS2 = false;
    $("#resF").show();
    $("#resF2").hide();
    GetMini(currentPI);
    $("#wrMini").css("opacity", "1");
    $("#relPlug").css("height", MainImgHeight + "px");
    setTimeout((function () { GlobalFS = false; }), 500);
}
/*----------------------------------------полноэкранный режим------------------------*/

function Change() {
    GlobalFS = true;
    GlobalFS2 = true;
    $("#relPlug").css("height", "100%");
    $("#resF").hide();
    $("#resF2").show();
}
/*----------------------------------новый FS----------------------*/
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
