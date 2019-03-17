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
});