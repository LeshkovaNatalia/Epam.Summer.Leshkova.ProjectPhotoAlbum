/* Upload File */
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