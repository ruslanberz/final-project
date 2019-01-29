$(document).ready(function(){
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
        autoplay:true,
        autoplayTimeout:2000,
        rewind:true,
        

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
});