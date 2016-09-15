/* Dynamic loads photo */
$(document).ready(function () {
    $(window).scroll(function () {
        if (($(window).scrollTop()) ==
     ($(document).height() - $(window).height())) {
            var lastPhotoId = $('div.results:last img').attr('id');
            getPhotos(lastPhotoId.substring(4));
        };
    });
});

function getPhotos(lastId) {
    $.ajax({
        type: 'POST',
        url: '/photos',
        data: 'id=' + lastId,
        dataType: "html",
        success: function (result) {
            var domElement = $(result);
            $('div.div-table').append(domElement);
        }
    });
};