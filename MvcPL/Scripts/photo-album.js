/* Function change menu for small screen */
function subMenuFunction() {
    var x = document.getElementById("topnav");
    if (x.className === "topnav") {
        x.className += " responsive";
    } else {
        x.className = "topnav";
    }                               
}

/* Function for search photos */
$(document).ready(function () {
    $("#photo").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/Photo/FindByDescription",
                type: "POST",
                dataType: "json",
                data: { term: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.label, value: item.label };
                    }))
                }
            })
        },
        messages: {
            noResults: "", results: ""
        }
    });
    $('#GetPhotos').click(function () {
        $.getJSON('/Photo/PhotoListBySearch?search=' + $('#photo').val(), function (data) {
            var items = '<table id="searchPhotos">';
            var counter = 1;
            items += "<tr>";
            $.each(data, function (i, photo) {
                items += "<td><a onclick='aboutPhoto(this)' data-ajax='true' data-ajax-mode='replace' data-ajax-update='#photoTemplate' href='/Photo/Details?photoId=" + photo.Id + "'><img src='/Photo/GetImage?photoId=" + photo.Id + "' width=\"100\" height=\"100\"/></a></td>";
                if (counter % 5 == 0)
                {
                    items += "</tr><tr>";
                }
                counter++;
            });
            items += "</table>";

            $('#searchPhoto').html(items);
        });
    });
})

function aboutPhoto(obj) {
    $(".selectedPhoto").hide().html('');
    $("#indicator").show();

    var url = $(obj).attr('href');

    $.getJSON(url, null, function (photo) {
        $("#indicator").hide();

        $("div #photoTemplate")
            .tmpl(photo)
            .appendTo('.selectedPhoto');

        $('.selectedPhoto').show();
    });
}

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
        var id = 'totalRating ' + photoId;
        var element = document.getElementById(id);
        element.innerHTML = obj.Rating;
        var idRaters = 'totalRaters ' + photoId;
        var elementRaters = document.getElementById(idRaters);
        elementRaters.innerHTML = obj.Raters;
    });
}

/* Load photo */
$().ready(function () {
    $('#results img').load(function () {
        $('#results img').fadeIn(2000);        
    });
});

/* Function for preview photo */
function previewFile() {
    var preview = document.getElementById('preview');
    var file = document.querySelector('input[type=file]').files[0];
    var reader = new FileReader();

    reader.addEventListener("load", function () {
        preview.src = reader.result;
    }, false);

    if (file) {
        reader.readAsDataURL(file);
    }
}

/* Refresh captcha */
function refreshCaptcha() {
    $('#captchaImg').attr('src', '/Account/Captcha' + "?t=" + new Date().getTime());
}

$(function () {
    var wrapper = $(".file_upload"),
    inp = wrapper.find("#photoPath"),
    btn = wrapper.find(".button"),
    lbl = wrapper.find("mark");       
        
    inp.change(function () {
        var file_name;
        file_name = inp.val().replace("C:\\fakepath\\", '');
        lbl.text(file_name);
    }).change();        
});

/* Paging */
$().ready(function () {
    $(".page-number").on("click", function () {
        var page = parseInt($(this).html());
        $.ajax({
            url: '/Photo/PhotosList',
            data: { "page": page },
            success: function (data) {
                $("#photos-list").html(data);
            }
        });
    });
});

/* Dialog for confirm delete photo */
$().ready(function () {
    $('.deletePhoto').click(function (e) {
        e.preventDefault();
        deleteItem(this, '<p>Are you sure you want to delete this photo?</p>');
    });
    $('.deleteUser').click(function (e) {
        e.preventDefault();
        deleteItem(this, '<p>Are you sure you want to delete this user?</p>');
    });
});

function deleteItem(obj, msg) {
    var href = $(obj).attr("href");
    var dialog = $(msg).dialog({
        buttons: {
            "Yes": function () {
                window.location.href = href;
                dialog.dialog('close');
            },
            "Cancel": function () {
                dialog.dialog('close');
            }
        }
    });
}