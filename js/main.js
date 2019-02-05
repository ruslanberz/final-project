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

      $(".remove").click(function(ex){
         ex.preventDefault();
         var col = $(this).parent().parent().parent().children().index($(this).parent().parent());
         $('table tr').find("td:eq("+col+"),th:eq("+col+")").remove();
      });
});