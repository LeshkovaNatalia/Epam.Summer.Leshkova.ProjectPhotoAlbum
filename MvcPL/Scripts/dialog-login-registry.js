﻿$(function () {
    var dialogs = {};

    var getValidationSummaryErrors = function ($form) {
        var errorSummary = $form.find('.validation-summary-errors, .validation-summary-valid');
        if (!errorSummary.length) {
            errorSummary = $('<div class="validation-summary-errors"><span>Please correct the errors and try again.</span><ul></ul></div>')
                .prependTo($form);
        }
        return errorSummary;
    };

    var displayErrors = function (form, errors) {
        var errorSummary = getValidationSummaryErrors(form)
            .removeClass('validation-summary-valid')
            .addClass('validation-summary-errors');

        var items = $.map(errors, function (error) {
            return '<li>' + error + '</li>';
        }).join('');

        var ul = errorSummary
            .find('ul')
            .empty()
            .append(items);
    };

    var resetForm = function ($form) {
        $form[0].reset();
        getValidationSummaryErrors($form)
            .removeClass('validation-summary-errors')
            .addClass('validation-summary-valid')
            .innerHTML="";
    };

    var formSubmitHandler = function (e) {
        var $form = $(this);
        if (!$form.valid || $form.valid()) {
            $.post($form.attr('action'), $form.serializeArray())
                .done(function (json) {
                    json = json || {};
                    if (json.success) {
                        location = json.redirect || location.href;
                    } else if (json.errors) {
                        displayErrors($form, json.errors);
                    }
                })
                .error(function () {
                    displayErrors($form, ['An unknown error happened.']);
                });
        }
        e.preventDefault();
    };

    var loadAndShowDialog = function (id, link, url) {
        var separator = url.indexOf('?') >= 0 ? '&' : '?';

        dialogs[id] = $();

        // Load the dialog with the content=1 -> return result to partial view
        $.get(url + separator + 'content=1')
            .done(function (content) {
                dialogs[id] = $('<div class="modal-popup">' + content + '</div>')
                    .hide()
                    .appendTo(document.body)
                    .filter('div')
                    .dialog({ 
                        title: false,
                        modal: true,
                        resizable: false,
                        draggable: true,
                        width: link.data('dialog-width') || 600,
                        beforeClose: function () { resetForm($(this).find('form')); },
                        show: { effect: "fade", duration: 500 },
                        hide: { effect: "slide", duration: 1000 }
                    })
                    .find('form')
                        .submit(formSubmitHandler)
                    .end();
                $('#Email').keyup(function () {
                    var email = $(this).val();
                    $.ajax({
                        url: 'userexist',
                        type: "POST",
                        data: { Email: email }
                    });
                });
                $('#Name').keyup(function () {
                    var name = $(this).val();
                    $.ajax({
                        url: 'ValidateCategoryName',
                        type: "POST",
                        data: { Name: name },
                        success: function (data) {
                            if (data == 1) {
                                $("span.text-danger").text("");
                            } else {
                                $("span.text-danger").text("Category exist with this name!");
                            }
                        }
                    });
                });
            });
    };

    var links = ['#loginLink', '#registerLink', '#registerLink2', '#categoryLink', '#categoryLink2', '#contactLink', '#contactLink2'];

    $.each(links, function (i, id) {
        $(id).click(function (e) {
            var link = $(this),
                url = link.attr('href');
            if (!dialogs[id]) {
                loadAndShowDialog(id, link, url);
            } else {
                
                dialogs[id].dialog('open');
            }
            e.preventDefault();
        });
    });
});