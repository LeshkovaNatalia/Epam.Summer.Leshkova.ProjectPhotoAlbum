/* Load photo for user */
$(document).ready(function () {
    $('.div-cell div a.delete-photo').removeAttr('style');
    $('tbody tr td a.delete-user').removeAttr('style');
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

function loadUsers(obg) {
    var page = parseInt($(obg).html());
    $.ajax({
        url: 'users',
        data: { "page": page },
        success: function (data) {
            $("#users-list").html(data);
        }
    });
}
