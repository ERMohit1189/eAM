function hidesection(divid) {
    var getsection = document.getElementById(divid);

    var p = getsection.getElementsByTagName("p");
    var count=0;

    for (var i = 0; i < p.length; i++) {

        if(p[i].innerText!="")
        {
            count = 1;
        }
        if (count > 0) {
            break;
        }
    }
    if (count == 0) {
        getsection.style.display = "none";
    }
    else
    {
        getsection.style.display = "block";
    }
}

//////**********shoiw and hide div***********///////
