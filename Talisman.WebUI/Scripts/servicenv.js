//все можно включить в шаблон
function Service() {
    var els = $(".liService");
    els.bind("click",  clickFun);
}
function clickFun(e) {
    var s = $(this).data("num");    
    var el = $("#" + s);
    var pos = Math.round(el.offset().top - 75);
    //alert(pos);
    $.scrollTo(pos+"px",500, {axis:"y"});    
}

//jQuery(document).ready(function () {
//    //alert("!");
//    setInterval((function () {
//        //alert("!");
//        var request = new XMLHttpRequest();
//        request.open("POST", "/Home/Ceep");
//        request.setRequestHeader("Content-Type","text/plain;charset=UTF-8")
//        request.send("1");
//    }), 290000);

//});

function CBShow() {
    //alert("!");
    var el=$("#backCall");
    var w = el.width();
    // el.css("width", w);
    el.css("height", "auto");
    $("#outwr").width(w);
    var t = $("#navbar").height() * 0.075;
    el.css("top", t + "px");
    $("#outwr").show();
   
}
function closeCB() {
    $("#outwr").hide();
    var el = $("#backCall");
    el.css("height", "85%");
    el.css("top", "0");
}
function ShowLeft() {
    
    if ($("#wrnav").hasClass("fixed")) {
        $("#wr").css("top", (window.pageYOffset));
    }
    else {
        $("#wr").css("top", "1.5vw");
    }
    
    $("#wr").animate({ left: 0 }, { duration: 400 });
    var el = $("#picList");
    el.attr("onclick", "HideLeft()");
    el.attr("title", "Скрыть боковое меню");
    el.text("-");
   // $("#picList").html('<span id="picList"  onclick="HideLeft()" title="Скрыть боковое меню">-</span> ');
}
function HideLeft() {
    $("#wr").animate({ left: -220 }, { duration: 400 });
    //$("#picList").html('<span id="picList"  onclick="ShowLeft()" title="Показать боковое меню">+</span> ');
    var el = $("#picList");
    el.attr("onclick", "ShowLeft()");
    el.attr("title", "Показать боковое меню");
    el.text("+");        
}

function fbHeight() {
    if ($("#wrFB").height() < $("#wr").height()) {
        $("#wrFB").height($("#wr").height());
    }
}