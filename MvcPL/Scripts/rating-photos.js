/* Voting */

$("img.star").mouseover(function () {
    var span = $(this).parent("span");
    var newRating = $(this).attr("value");
    setRating(span, newRating);
});
$("img.star").mouseout(function () {
    var span = $(this).parent("span");
    var rating = span.attr("rating");
    setRating(span, rating);
});
function setRating(span, rating) {
    span.find('img.star').each(function () {
        var value = parseFloat($(this).attr("value"));
        var imgSrc = $(this).attr("src");
        if (value <= rating)
            $(this).attr("src", imgSrc.replace("starOff.png", "star.png"));
        else
            $(this).attr("src", imgSrc.replace("star.png", "starOff.png"));
    });
}
function clickStar(obj) {
    var url = "/Voting/AddVoting";
    var span = $(obj).parent("span");
    var rate = $(obj).attr("value");
    var photoId = span.attr("photo");
    var text = span.children("span");
    var user = $("#User").attr("value");
    var data = { userName: user, photoId: photoId, rating: rate };
    $.post(url, data, function (obj) {
        var spanId = 'span' + photoId;
        var elementSpan = document.getElementById(spanId);
        elementSpan.innerHTML = 'Thanks for vote!';
        var id = 'totalRating-' + photoId;
        var element = document.getElementById(id);
        element.innerHTML = obj.Rating;
        var idRaters = 'totalRaters-' + photoId;
        var elementRaters = document.getElementById(idRaters);
        elementRaters.innerHTML = obj.Raters;
    });
}