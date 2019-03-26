$(document).ready(function () {

    $(".popular").owlCarousel({
        items: 5,
        nav: true,

    });
   



    $(".go-to").click(function (e) {
        e.preventDefault();

        var position = $($(this).attr("href")).offset().top;

        $("body, html").animate({
            scrollTop: position
        } /* speed */);
    });

  var clicks = 0;
  $(".filter-li").click(function(ex){
    ex.preventDefault();
    

     
          if(clicks == 0){
            $(':nth-child(2)', this).css('display',"none");
            $(':nth-child(3)', this).css('display',"block");
          
            clicks++;
         
          }else{
            $(':nth-child(2)', this).css('display',"block");
            $(':nth-child(3)', this).css('display',"none");
            clicks--;
            
          }
        
       
  })
   $(".filter-show").click(function(ex){
   ex.preventDefault();
   $("#filter-form").css("display","block");
   $("#mobile-menu").css("display","none");
   });

   $(".sort-show").click(function(ex){
    ex.preventDefault();
    $("#sort-form").css("display","block");
    $("#mobile-menu").css("display","none");
    });
 

   $("#filter-close").click(function(ex){
    ex.preventDefault();
    $("#filter-form").css("display","none");
    $("#mobile-menu").css("display","block");
   });

   $("#sort-close").click(function(ex){
    ex.preventDefault();
    $("#sort-form").css("display","none");
    $("#mobile-menu").css("display","block");
   });
  $(".mobile-search").click(function(ex){
   ex.preventDefault();
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


    //  var delay = ( function() {
    //    var timer = 0;
    //    return function(callback, ms) {
    //        clearTimeout (timer);
    //        timer = setTimeout(callback, ms);
    //    };
    //})();

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
        
      });


      $("#searchinput").focusout(function(){
        
        $(".search-details").css("visibility", "hidden");
      });

      //$(".menu-main").hover(function(){


         
      //  //  $(".menu-cat-hover").slideToggle("slow");
      //   $(".menu-cat-hover").css("display","inline-flex");
      //  //  $(".menu-cat-hover").css("heigth","300px");

      //});


      //$(".for-menu-disappear").mouseleave(function (){


      //    // $(".menu-cat-hover").delay(5000).css("display","none");

      //    //$(".menu-cat-hover").fadeOut();

      //});


    $(".main-li").hover(function () {
        $(".menu-cat-hover2").each(function () {

            $(this).css("visibility", "hidden");

            

        });

        
       
        setTimeout(showMenu(this), 5000);
      
        //$(".menu-cat-hover2").css("display", "inline-flex");
    });

    $(".menu-cat-hover2").mouseleave(function () {

        $(this).delay(1000).css("visibility", "hidden");
    });
      $(".btn-more-offers").click(function(){
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
          $(".add-to-compare").find(("li[data-id='" + Number($(this).data("id")) + "']")).removeClass("d-none");
         var col = $(this).parent().parent().parent().children().index($(this).parent().parent());
          //$('table tr').find("td:eq(" + col + "),th:eq(" + col + ")").remove();
          $('table tr').find("td:eq(" + col + "),th:eq(" + col + ")").addClass("d-none");
          $('table tr').find("td:eq(" + col + "),th:eq(" + col + ")").removeClass("act");
          
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

        position: .0,
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


    var Items = JSON.parse(localStorage.getItem('items')) || [];
    console.log(Items);
    $.ajax({
        url: "/home/FillRecentlyViewed",
        type: "POST",
        data: JSON.stringify(Items),
        contentType: "application/json",
        traditional: true,
        dataType: "html",
        success: function (data) {
            $("#prepop").html(data);
            LoadOwl();
           
         
            $("#recently").removeClass("d-none");
           
        }
    });
});

function showMenu(control) {
   
    $(control).next().css("visibility", "visible");

}


function Filter(id) {
    var SpecIds = [];
    $(".fltr").each(function (index) {
        
        var temp = [];
        $(this).find(".filter-chk").each(function ()
        {
            if ($(this).is(':checked'))
            {
                temp.push($(this).data("id"));
            }
            
        });
      
        SpecIds.push
            ({   
                FilterID: $(this).data("id"),
                FilterValues: temp
            });   
    });

    var string = JSON.stringify({ id: 30, pageId: 1, SpecIds: SpecIds });
    $.ajax({
        url: "/home/catfilter",
        type: "POST",
        traditional: true,
        data: string,
        contentType: "application/json",
        dataType: "html",
        success: function (data) {
            $(".forfilter").empty();
            $(".forfilter").html(data);
            $("#cnt").text($(".itemcount").data("val") + ' məhsul göstərilir');
            $("html, body").animate({ scrollTop: 0 }, "slow");



        }
    });
}

//Next 3 functions are ajax helpers for navigation on category page -  when filters are active
function Berz(category,page) {
   
    var SpecIds = [];
    $(".fltr").each(function (index) {

        var temp = [];
        $(this).find(".filter-chk").each(function () {
            if ($(this).is(':checked')) {
                temp.push($(this).data("id"));
            }

        });

        SpecIds.push
            ({
                FilterID: $(this).data("id"),
                FilterValues: temp
            });
    });

   
    var string = JSON.stringify({ id: category, pageId: page, SpecIds: SpecIds });
    $.ajax({
        url: "/home/catfilter",
        type: "POST",
        traditional: true,
        data: string,
        contentType: "application/json",
        dataType: "html",
        success: function (data) {
            $(".forfilter").empty();
            $(".forfilter").html(data);
            $("html, body").animate({ scrollTop: 0 }, "slow");



        }
    });
};

function BerzPrev(category, page, currentPage) {
    if ((page < currentPage) && (Number(page) != 0)) {

        var SpecIds = [];
        $(".fltr").each(function (index) {

            var temp = [];
            $(this).find(".filter-chk").each(function () {
                if ($(this).is(':checked')) {
                    temp.push($(this).data("id"));
                }

            });

            SpecIds.push
                ({
                    FilterID: $(this).data("id"),
                    FilterValues: temp
                });
        });


        var string = JSON.stringify({ id: category, pageId: page, SpecIds: SpecIds });
        $.ajax({
            url: "/home/catfilter",
            type: "POST",
            traditional: true,
            data: string,
            contentType: "application/json",
            dataType: "html",
            success: function (data) {
                $(".forfilter").empty();
                $(".forfilter").html(data);
                $("html, body").animate({ scrollTop: 0 }, "slow");



            }
        });
    }

};

function BerzNext(category, page, currentPage, PagesCount) {
    if ((page > currentPage) && (Number(PagesCount) > Number(currentPage))) {

        var SpecIds = [];
        $(".fltr").each(function (index) {

            var temp = [];
            $(this).find(".filter-chk").each(function () {
                if ($(this).is(':checked')) {
                    temp.push($(this).data("id"));
                }

            });

            SpecIds.push
                ({
                    FilterID: $(this).data("id"),
                    FilterValues: temp
                });
        });

   
        var string = JSON.stringify({ id: category, pageId: page, SpecIds: SpecIds });
        $.ajax({
            url: "/home/catfilter",
            type: "POST",
            traditional: true,
            data: string,
            contentType: "application/json",
            dataType: "html",
            success: function (data) {
                $(".forfilter").empty();
                $(".forfilter").html(data);
                $("html, body").animate({ scrollTop: 0 }, "slow");



            }
        });
    }

};

//This function activates compare table items on item page-swaps if more than 2 items added, or ad- whenin table 0 or 1 item
function AddCompare(obj) {

    console.log($(".compare-item.d-none").length);


    if ($(".compare-item.d-none").length <=5)
    {
        var toenable = $(".compare-item.act").first().data("id");
        $(".add-to-compare").find(("li[data-id='" + Number(toenable) + "']")).removeClass("d-none");
        $(".compare-item.act").first().addClass("d-none");
        $(".compare-item.act").first().removeClass("act");
        $(".tf.act").first().addClass("d-none");
        $(".tf.act").first().removeClass("act");
        $("tr").each(function(){
            $(this).find(".tb.act").first().addClass("d-none");
            $(this).find(".tb.act").first().removeClass("act");
        });
        $(".compare-item[data-id='" + Number($(obj).data("id")) + "']").removeClass("d-none");
        $("tr").each(function () {
            $(this).find(("td[data-id='" + Number($(obj).data("id")) + "']")).removeClass("d-none");
        });
        $(".compare-item[data-id='" + Number($(obj).data("id")) + "']").addClass("act");
        $("tr").each(function () {
            $(this).find(("td[data-id='" + Number($(obj).data("id")) + "']")).addClass("act");
        });
        $(obj).parent().addClass("d-none");
    }
    else
    {
        
        $(".compare-item[data-id='" + Number($(obj).data("id")) + "']").removeClass("d-none");
        $("td[data-id='" + Number($(obj).data("id")) + "']").removeClass("d-none");
        $(".compare-item[data-id='" + Number($(obj).data("id")) + "']").addClass("act");
        $("td[data-id='" + Number($(obj).data("id")) + "']").addClass("act");
        $(obj).parent().addClass("d-none");
    }
    

};

function Local(obj)
{
    console.log($(obj).data("id"));
    var oldItems = JSON.parse(localStorage.getItem('items')) || [];
    console.log(oldItems);
    var newItem =
    {
        'id': $(obj).data("id")
    };
    oldItems.push(newItem);
    var result=[]
    $.each(oldItems, function (i, e) {
        var matchingItems = $.grep(result, function (item) {
            return item.id === e.id
        });
        if (matchingItems.length === 0) {
            result.push(e);
        }
    });
    

    localStorage.setItem('items', JSON.stringify(result));
}

function LoadOwl() {

    $(".myowlitem").each(function () {
        console.log($(this).html());
        $('#pop').owlCarousel().trigger('add.owl.carousel',
            [jQuery('<div class="owl-item">' + $(this).html() +
                '</div>')]).trigger('refresh.owl.carousel');

    });
};