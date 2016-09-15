/* Function for search photos */
$(document).ready(function () {
    $("#photo").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "autocomplete",
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
    $('#get-photos').click(function () {
        $.getJSON('/Photo/PhotoListBySearch?search=' + $('#photo').val(), function (data) {
            var items = '<table id="search-photos">';
            var counter = 1;
            items += "<tr>";
            $.each(data, function (i, photo) {
                items += "<td><a onclick='aboutPhoto(this)' data-ajax='true' data-ajax-mode='replace' data-ajax-update='#photoTemplate' href='/Photo/Details?photoId=" + photo.Id + "'><img src='/images/" + photo.Id + "' width=\"100\" height=\"100\"/></a></td>";
                if (counter % 5 == 0)
                {
                    items += "</tr><tr>";
                }
                counter++;
            });
            items += "</table>";

            $('#search-photo').html(items);
        });
    });
})

function aboutPhoto(obj) {
    $("#indicator").show();

    var url = $(obj).attr('href');

    $.getJSON(url, null, function (photo) {
        $("#indicator").hide();
        var descr = photo.Description;
        var photoId = photo.Id;
        $('.selected-photo div p.photo-description').html(descr);
        $('.selected-photo div img.display-img').attr('src', '/images?photoId=' + photoId);
        $('.selected-photo').removeAttr('style');
    });
}

/* Load photo */
$().ready(function () {
    $('div.results img').load(function () {
        $('div.results img').fadeIn(2000);        
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
    $('#captcha-img').attr('src', '/captcha' + "?t=" + new Date().getTime());
}

$(function () {
    var wrapper = $(".file-upload"),
    inp = wrapper.find("#photo_path"),
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

$(document).ready(function () {
    $('.table tr td a.delete-user').removeAttr('style');
    $('.div-cell div a.delete-photo').removeAttr('style');
    $('.col-md-10 img.img-preview').removeAttr('style');
});

/* Dialog for confirm delete photo */
$().ready(function () {
    $('.delete-photo').click(function (e) {
        e.preventDefault();
        deleteItem(this, '<p>Are you sure you want to delete this photo?</p>');
    });
    $('.delete-user').click(function (e) {
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