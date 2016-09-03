/*Dynamic loads photo*/
$(document).ready(function () {
    $(window).scroll(function () {
        if (($(window).scrollTop()) ==
     ($(document).height() - $(window).height())) {
            var lastPhotoId = $('div.results:last img').attr('id');
            GetPhotos(lastPhotoId);
        };
    });
});

function GetPhotos(lastId) {
    $.ajax({
        type: 'POST',
        url: '/Photo/Photos',
        data: 'id=' + lastId,
        dataType: "html",
        success: function (result) {
            var domElement = $(result);
            $('div.rTable').append(domElement);
        }
    });
};