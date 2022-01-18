$(document).ready(function () {
    $('.input-validation-error').parents('.form-group').addClass('has-error');
    $('.input-validation-error').parents('.form-group').addClass('ns-fieldset-message-error');
    $('.field-validation-error').addClass('errMsg');
});

/*Print confirmation page*/
$("#printCopy").click(function () {
    window.print();
});

$("#saveCopy").click(function () {
    savelink.click();
});





