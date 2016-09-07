function aboutPhoto(obj) {

    $(".selected-photo").hide().html('');
    $("#indicator").show();

    var url = $(obj).attr('href');

    $.getJSON(url, null, function (photo) {
        $("#indicator").hide();

        $("div #photoTemplate")
            .tmpl(photo)
            .appendTo('.selected-photo');

        $('.selected-photo').show();
    });
}