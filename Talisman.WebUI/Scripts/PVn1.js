var GlobalFS = false;
var GlobalFS2 = false;
var GlobalFIsOut = 0;
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
    setByIndex();    
    setImg();    
    Effects();   
}
function nextImg() {
    if (GlobalFS) {        
        if (currentPI == data.Viewers[currentIndex].PhotoIds.length-1) {
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
        $("#addBm").css("opacity", "0");
    }
    else {
        $("#addBm").css("opacity", "1");
    } 
    document.getElementById("tovarNamem").innerText = data.Viewers[currentIndex].Tovar.Name;
    document.getElementById("currentMinim").innerText = "1";
    document.getElementById("countMinim").innerText = list2.length;
    var div = document.getElementById("wrMinim");
   
    div.scrollTop = 0;
    $(div).empty();

    if (list2.length > 4) {
        scroll = 0;
        iter = 1;
        bPass($("#wrTopPm"));
        bAct($("#wrBottomPm"));
    }
    else {
        bPass($("#wrTopPm"));
        bPass($("#wrBottomPm"));
    }
    
    countCurrentMini = list2.length;
    for (var i = 0; i < list2.length; i++) {
        var mimg = new Image();
        if (i == semM) {
            mimg.className = "allMinim selectedMinim";
        }
        else {
            mimg.className = "allMinim";
        } 
        mimg.src = "/Content/TovarPhotos/" + list2[i] + ".jpeg";        
       // mimg.style.height = MiniHeight + "px";        
        mimg.data = list2[i];
        mimg.ind = i + 1; 
        mimg.onclick = function () {           
            getImg(this.data);
            document.getElementById("currentMinim").innerText = this.ind;
            $(".selectedMinim").attr("class", "allMinim");
            this.className = "allMinim selectedMinim";
            currentPI = this.ind - 1;
        };                
        div.appendChild(mimg);       
    }    
    $(div).fadeTo(0, 0);
}
function scrollWidth()
{
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
    //var width = $("#wrMain").get(0).offsetWidth - $("#wrMain").get(0).clientWidth;
    
    div.remove();
    
    return width;
}
function Effects() {
    
    var sh = $("#shadow");
    sh.show().fadeTo(400, 0.7);
    $("body").css("overflow", "hidden");     
    var wpr = scrollWidth();
    //alert(wpr);
    var isIE = false || !!document.documentMode;
    if (!isIE) {
        $("body").css("padding-right", wpr + "px");       
    }
    var mDiv = $("#wrPlugm");    
    mDiv.animate({ top: 0, left: 0, width: "show", height: "show", opacity: 1 }, { duration: 500 });
    if (document.body.ontouchstart == undefined) {
        //alert('Touch Screen');
        $("#wrButs").css("opacity", "1");
    } 
}
function setImg() {

    var el = $("#wrwrm");
    el.css("opacity", "0");
   // $("#outLayer").css("opacity", "1");
    $("#plImgm").attr("src", "/Content/TovarPhotos/" + currentId + ".jpeg");
}

function f1() {    
    AddClasses(this);
    $("#wrwrm").fadeTo(400, 1, function () {              
        $("#plImgBm").attr("src" , "/Content/TovarPhotos/" + currentId + ".jpeg");        
        $("#wrMinim").fadeTo(400, 1);
    });

}
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
}

$("#wrPlugm").ready(function () {
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
    
    //window.onresize = function () {
        
    //    if ($(".wrAbs").length > 0) {
    //        var hi = $(".wrAbs").height();
    //        $(".mainImages").height(hi);
    //    }
    //    //alert(screen.width);
    //}   
    iter = 1;    
    $("#plImgm").bind("load", f1);   
    var elem = $("#mainPlugm");
    var el2 = $("#addBm");    
    $("#wrPlugm").click(function (e) {
        var div = document.getElementById("wrMinim");       
        if ((e.target != elem[0] && !elem.has(e.target).length)
            && (e.target != el2[0] && !el2.has(e.target).length)) {            
            div.scrollTop = 0;
            scroll = 0;             
            $("#plImgm").attr("src", "~/Content/Images/pixel.png");
            $("#plImgBm").attr("src", "~/Content/Images/pixel.png");
            var sh = $("#shadow");
            sh.fadeTo(400, 0).hide(200);
            $("#wrPlugm").animate({ top: "50vh", left: "50vw", width: "hide", height: "hide",opacity:0 }, { duration: 0 });
            //$("#wrPlugm").hide();
            $("body").css("padding-right", "0")
            $("body").css("overflow", "auto");                       
        }
    });    
    //$(".vsb").hover(function (e) {
    //    $("#outPlug").css("opacity", "0.5");
    //    $("#addB").css("opacity", "1");        
    //});
    //$(".vsb").mouseleave(function (e) {
    //    if ((e.target = elem[0] && !elem.has(e.target).length) && (e.target = el2[0] && !el2.has(e.target).length)) {
    //        $("#outPlug").css("opacity", "1");
    //        $("#addB").css("opacity", "0");
    //    }
    //});
});
var scroll = 0.0;
var iter = 1;
function topMini() {    
    if (countCurrentMini > 4) {
        var el = $("#wrMinim");
        var el2 = $("#wrBottomPm");
        var el3 = $("#wrTopPm");        
        if (iter > countCurrentMini - 4) {
            bAct(el2);
        }
        if (iter > 1) {
            scroll -= ($(".allMinim").height() + 7);
            iter--;
            if (iter == 1) {
                bPass(el3);
                scroll = 0.0;
            }           
            el.scrollTo(scroll + "px", 300, { axis: 'y' });            
            
        }

    }
}
function bottomMini() {
    var h = $(".allMinim").height();
    if (countCurrentMini > 4) {        
        var el = $("#wrMinim");
        var n = countCurrentMini - 4;
        if (iter == 1) {
            bAct($("#wrTopPm"));
        }
        if (iter <= n) {            
            iter++;
            scroll += ($(".allMinim").height() + 7);
            el.scrollTo(scroll + "px", 300, { axis: 'y' });            
            if (iter > n) {
                bPass($("#wrBottomPm"));
            }
        }
    }

};
function bAct(el) {
    el.css("opacity", "0.7");
    el.css("cursor", "pointer");
}
function bPass(el) {
    el.css("opacity", "0");
    el.css("cursor", "default");
};
/*--------------------------------------полноэкранный режим1-------------------------*/
var Andr = false;
function FS() {
    var el = document.getElementById("relPlugm");
    if (api.enabled()) {
        api.request(el);
        Change();
    }
    else {        
        Android();
        Change();
        Andr = true;
    }
}

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
    GetMini(currentPI);
    $("#wrMinim").css("opacity", "1");    
    setTimeout((function () { GlobalFS = false; }), 500);
}
/*----------------------------------------полноэкранный режим------------------------*/

function Change() {
    GlobalFS = true;
    GlobalFS2 = true;    
    $("#resFm").hide();
    $("#resF2m").show();
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

function Android() {
    var el = $("#relPlugm");
    el.css("position", "fixed");
    el.css("top", "0");
    el.css("left", "0");
    el.css("width", "100%");
    el.css("height", "100%");
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
}
function AndrExit() {
    var el = $("#relPlugm");
    el.css("position", "relative");
    $("#wrwrBm").css("background-color", "#e0dfde");
    $("#wrwrm").css("background-color", "#e0dfde");    
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
}
