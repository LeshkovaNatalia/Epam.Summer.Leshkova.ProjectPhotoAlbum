/* Load photo for user */
$(document).ready(function () {
    $('.div-cell div a.delete-photo').removeAttr('style');
    $('div.div-row a.btn-default').removeAttr('style');
});

function loadUserPhoto(obg) {
    var page = parseInt($(obg).html());
    $.ajax({
        url: 'loadmyphotos',
        data: { "page": page },
        success: function (data) {
            $("#photos-list").html(data);
        }
    });
}
