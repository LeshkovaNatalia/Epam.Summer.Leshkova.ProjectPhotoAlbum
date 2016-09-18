/* Dialog for confirm delete photo */
$(document).on("click", ".delete-photo", function (e) {
    e.preventDefault();
    deleteItem(this, '<p>Are you sure you want to delete this photo?</p>');    
});

$(document).on("click", ".delete-user", function (e) {
    e.preventDefault();
    deleteItem(this, '<p>Are you sure you want to delete this user?</p>');
});

function deleteItem(obg, msg) {
    var href = $(obg).attr("data-href");
    var dialog = $(msg).dialog({
        buttons: {
            "Yes": function () {
                window.location.href = href;
                dialog.dialog('close');
            },
            "Cancel": function () {
                dialog.dialog('close');
            }
        },
        show: { effect: "fade", duration: 500 },
        hide: { effect: "slide", duration: 1000 }
    })
}