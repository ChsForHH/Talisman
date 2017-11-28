function slideSizing(){
    var elSrc = $(".dataUrl");
    if (document.documentElement.clientWidth > 700) {
        
        for (i = 0; i < elSrc.length; i++) {
            elSrc[i].innerText = elSrc[i].innerText.replace("s1", "s0");
            elSrc[i].innerText = elSrc[i].innerText.replace("s2", "s0");            
        }
    }
    if (document.documentElement.clientWidth <= 700 && document.documentElement.clientWidth > 300) {        
        for (i = 0; i < elSrc.length; i++) {
            elSrc[i].innerText = elSrc[i].innerText.replace("s0", "s1");
            elSrc[i].innerText = elSrc[i].innerText.replace("s2", "s1");           
        }
    }
    else {
        if (document.documentElement.clientWidth <= 300) {
            for (i = 0; i < elSrc.length; i++) {
                elSrc[i].innerText = elSrc[i].innerText.replace("s0", "s2");
                elSrc[i].innerText = elSrc[i].innerText.replace("s1", "s2");               
            }
        }
    }           
    Slider.Init1();
}
$(document).ready(function () {
    slideSizing();
    Slider.StartSlide();
});
window.addEventListener('resize', function () {
    slideSizing();
});
var Slider = new Object();
Slider.sp = new Object();
Slider.cur = new Object();
Slider.i = 1;
Slider.stop = false;
Slider.s = 0;
Slider.old = 0;
Slider.p = false;
Slider.CatNames = new Array();
Slider.Urls = new Array();
Slider.Urls2 = new Array();
Slider.StartSlide =function(){       
    setInterval((function () {
        if (!Slider.stop) {
            Show(Slider.i);
        }
    }), 5000);
}
Slider.Stop = function () {
    Slider.Stop1();
    $("#stopPlay1").replaceWith('<span id="stopPlay2" class="glyphicon glyphicon-play con"  onclick="Slider.Play()" style="cursor:pointer;" title="Включить"></span>');
}
Slider.Play = function () {   
    Slider.stop = false;
    $("#stopPlay2").replaceWith('<span id="stopPlay1" class="glyphicon glyphicon-pause con"  onclick="Slider.Stop()" style="cursor:pointer;" title="Остановить"></span>');
}
Slider.Stop1 = function () {    
        Slider.stop = true;        
}
Slider.Play1 = function () {
    if ($("#stopPlay1").length > 0) {
        Slider.stop = false;       
    }        
}
Slider.Init1 = function () {
    var el1 = $(".dataCatName");
    var el2 = $(".dataUrl");
    var el3 = $(".dataUrl2");
    for (i = 0; i < el1.length; i++) {
        Slider.CatNames[i] = el1[i].innerText;
        Slider.Urls[i] = el2[i].innerText;
        Slider.Urls2[i] = el3[i].innerText;
    }
}
Slider.Init = function () {
    Slider.Init1();
    var Abs = $("#AbsSl");
    Abs.html('<a class="slLink" href="'+Slider.Urls2[1]+'">'+
                '<div class="remH4" id="remAbs"> ' + Slider.CatNames[1] + '</div>' +
                '<div class="imgSl">'+
                    '<img src="' + Slider.Urls[1] +'alt='+Slider.CatNames[1]+ '" id="imgAbs"/>' +
                '</div>'+
            '</a>');   
}
function Show(i) {    
    if (!Slider.p) {
        if (i != Slider.old) {
            Slider.p = true;
            Slider.i = i;           
            if (Slider.s === 0) {
                Slider.Init();
                Slider.s = 1;
            }
            $("#AbsSl").css("opacity", "0");
            $("#imgAbs").attr("src", Slider.Urls[i]);
            $("#remAbs").text(Slider.CatNames[i]);
            $(".slLink").attr("href", Slider.Urls2[i]);
            $("#AbsSl").animate({ opacity: 1 },
                {
                    duration: 700, complete: function () {
                        $("#statImg").attr("src", Slider.Urls[Slider.i == 0 ? 4 : Slider.i - 1]);
                        $("#statImg").attr("alt", Slider.CatNames[Slider.i == 0 ? 4 : Slider.i - 1]);
                        $(".imgSl").attr("alt", Slider.CatNames[Slider.i == 0 ? 4 : Slider.i - 1]);
                        $("#statH4").text(Slider.CatNames[Slider.i == 0 ? 4 : Slider.i - 1]);
                    }
                });
            Slider.sp = $(".currentS");
            Slider.sp.removeClass("currentS");
            Slider.cur = $("#sp" + (Slider.i < 5 ? Slider.i + 1 : 0).toString());
            Slider.cur.addClass("currentS");
            Slider.old = Slider.i;
            if (Slider.i === 4) Slider.i = 0;
            else Slider.i++;
            Slider.p = false;
        }
    }
}
