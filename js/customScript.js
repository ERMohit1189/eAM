$(document).ready(function(){
var winHeight =$(window).height()/1.2-100;
$(".logoDiv").css("top",winHeight);


var int=setInterval("setLogo()",5000);
if ( $(".logoDiv").css('top') == 0) { 
		clearInterval(int);
	}

//=======document.ready ends here
});

function  setLogo(){	
// $(".logoDiv").animate({top: "0px"}, 3000 );
 
 
  $('.logoDiv').animate({top: '0px'}, 5000, function() {
	 $(".wrapper").slideDown(1000);
	 $(".footer").slideDown(1000);
  });
 
 
}


