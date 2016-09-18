/* Dynamic loads photo */
$(document).ready(function () {
    $(window).scroll(function () {
        if (($(window).scrollTop()) ==
     ($(document).height() - $(window).height())) {
            var lastPhotoId = $('div.results:last img').attr('id');
            var select = document.getElementById('Id');
            $.ajax({
            type: 'POST',
            url: '/photos',
            data: { photoId: lastPhotoId.substring(4), Id: select.value },
            dataType: "html",
            success: function (result) {
                if (result != null) {
                    var domElement = $(result);
                    $('div.div-table').append(domElement);
                }
            }
        });
        };
    });
});

