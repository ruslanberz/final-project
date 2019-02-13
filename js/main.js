$(document).ready(function(){

  var text = $('.typewriter').val();

  var length = text.length;
  var timeOut;
  var character = 0;


  (function typeWriter() {
      timeOut = setTimeout(function() {
          character++;
          var type = text.substring(0, character);
          $('.typewriter').val(type);
          typeWriter();

           if (character == length) {
               clearTimeout(timeOut);
           }

      }, 50);
  }());

  $(".mobile-search").click(function(ex){
   ex.preventDefault();
   console.log("clicked");
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

      $(".m-merch-popular").owlCarousel({
        
        nav:false,
        autoplay:false,
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


      $(".owl-carousel2").owlCarousel({
        items:1,
        nav:true,
        autoplay:false,



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


         $(".menu-cat-hover").css("display","inline-flex");

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
})


});