/* Load photo for user */
$().ready(function () {
    $(".page-number").on("click", function () {
        var page = parseInt($(this).html());
        $.ajax({
            url: '/Photo/PhotosList/)',
            data: { "page": page },
            success: function (data) {
                $("#photos-list").html(data);
            }
        });
    });
});