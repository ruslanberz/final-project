$(document).ready(function(){



  $(".mobile-search").click(function(ex){
   ex.preventDefault();
   console.log("clicked");
   $(".stickynavbar").css("display", "none");
   $("#search-form").css("display","block");
   $("#search-form").css("opacity","1");
  })

    $('.main-carousel1').flickity({
        // options
        cellAlign: 'left',
        contain: true,
        percentPosition: false,
        autoPlay: true,
        resize: true,
        accessibility: true,
        pageDots: false,
      });


      var delay = ( function() {
        var timer = 0;
        return function(callback, ms) {
            clearTimeout (timer);
            timer = setTimeout(callback, ms);
        };
    })();

      $('.main-carousel2').flickity({
        // options
        // cellAlign: 'left',
        // contain: true,
        // percentPosition: true,
        cellAlign: 'center',
        // resize: true,
        // accessibility: true,
        pageDots: false,

      });

      $(".popular").owlCarousel({
        items:5,
        nav:true,



      });

      $(".wrapper-categories").owlCarousel({
        autoWidth:true,
        nav:false,
        dots:false,
        autoplay:true,
        autoplayTimeout:3000,
        rewind:true,


      });

      $(".owl-carousel1").owlCarousel({
        items:1,
        nav:true,
        autoplay:true,
        autoplayTimeout:5000,
        rewind:true,


      });

      $(".m-carousel-item-photo").owlCarousel({
        items:1,
        rewind:true,
        dots:true


      });
      

      $(".m-merch-popular").owlCarousel({
        
        nav:false,
        autoplay:true,
        autoplayTimeout:5000,
        rewind:true,
        nav:false,
        dots: false,
        margin:50,


      });



      $(".item-slider").owlCarousel({
        items:1,
        nav:true,
        autoplay:false,
        rewind:true,
        dots:false,


      });


       $(".categories-carousel").owlCarousel({

        items:4,
        nav:true,
        autoplay:false,
        dots:false,



      });


      $(".m-popular-search-requests").owlCarousel
      ({
       
        nav:false,
        autoplay:true,
        dots:false,
        autoplayTimeout:3000,
        autoWidth:true,
        rewind:true,
        items:2,


      });

      $(".owl-carousel2").owlCarousel({
        items:1,
        nav:true,
        autoplay:false,
      });

      $(".m-viewed-with-this-carousel").owlCarousel({
       
        nav:false,
        autoplay:true,
        dots:false,
        autoplayTimeout:3000,
        rewind:true,
       
        items:2,
        
      });


      $( ".owl-prev").html('<i class="fa fa-chevron-left"></i>');
      $( ".owl-next").html('<i class="fa fa-chevron-right"></i>');




      $("#searchinput").focus(function(){
        $(".search-details").css("visibility", "visible");
        console.log("ok1");
      });


      $("#searchinput").focusout(function(){
        console.log("ok");
        $(".search-details").css("visibility", "hidden");
      });

      $(".menu-main").hover(function(){


         
        //  $(".menu-cat-hover").slideToggle("slow");
         $(".menu-cat-hover").css("display","inline-flex");
        //  $(".menu-cat-hover").css("heigth","300px");

      });


      $(".for-menu-disappear").mouseleave(function (){


          // $(".menu-cat-hover").delay(5000).css("display","none");

          $(".menu-cat-hover").fadeOut();

      });

      $(".btn-more-offers").click(function(){
        console.log($(this).parent());
        $(this).parent().removeClass("mt-2");
        $(".btn-more-offers").removeClass("d-flex");
        $(".btn-more-offers").addClass("d-none");
        $( ".offertablerow" ).each(function( index ) {
         $(this).removeClass("d-none");
        });
      });
      $(".btn-more-comments").click(function(){

        $(this).parent().removeClass("mt-2");
        $(this).parent().removeClass("d-flex");
        $(this).parent().addClass("d-none");
        $( ".comment" ).each(function( index ) {
         $(this).removeClass("d-none");
        });
      });
      $(".remove").click(function(ex){
         ex.preventDefault();
         var col = $(this).parent().parent().parent().children().index($(this).parent().parent());
         $('table tr').find("td:eq("+col+"),th:eq("+col+")").remove();
      });


      $(".stars").starRating({
        starSize: 15,
        ratedColor: 'orange',
        
        callback: function(currentRating, $el){
            // make a server call here
        }
    });

    //set all star ratings to read only
    $(".stars").each(function(){
      $(this).starRating('setReadOnly', true);
      $(this).starRating('setRating',$(this).attr("data-rating"));
    });

    $('.progress').jsRapBar({

        position: .7,
        width: '40%',
          height: '12px',
          barColor: '#fec56f',
        enabled:false,

      });

$(".hide-mobile-search").click(function(ex){
ex.preventDefault();
$("#search-form").css("display","none");
   $("#search-form").css("opacity","0");
   $(".stickynavbar").css("display", "flex");
})

function fixDiv() {
  var $cache = $('#stickynavbar');
  if (($(window).scrollTop() > 0))
  {
    $cache.css({
      'position': 'fixed',
      'top': '-5px',
     'width': '100%',
      'z-index': '100',
      'box-shadow': '0px 4px 8px -3px rgba(17,17,17,0.2)',
    });
    $(".search-icon").css("display","block");
  }  
  else
  {
    $cache.css({
      'position': 'relative',
      'top': 'auto',
      'box-shadow': 'none',
    });
    $(".search-icon").css("display","none");
  }
    
}
$(window).scroll(fixDiv);
fixDiv();

$(".back-to-top").click(function(ex){
ex.preventDefault();
$("html, body").animate({ scrollTop: 0 }, "slow");
});

$(".search-icon").click(function(ex){
  ex.preventDefault();
  $("html, body").animate({ scrollTop: 0 }, "slow");
});

});