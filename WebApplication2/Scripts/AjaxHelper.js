
var xhr;
$.ajaxSetup({
    
    cache: false,
    beforeSend: function (xhr) {
        var sessionPageId = sessionStorage.getItem("uniquePageId");
        xhr.setRequestHeader("X-Custom-UniquePageId", sessionPageId);
    }
});

$(document).ajaxStart(function () {

});


$(document).ajaxComplete(function () {

});

$(document).ajaxStop(function () {

});
$(document).ajaxError(function (e, jqXHR) {

});

$(window).bind('beforeunload', function () {

});