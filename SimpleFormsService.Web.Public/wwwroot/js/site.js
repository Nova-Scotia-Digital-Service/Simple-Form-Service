$(document).ready(function () {
    $('.input-validation-error').parents('.form-group').addClass('has-error');
    $('.input-validation-error').parents('.form-group').addClass('ns-fieldset-message-error');
    $('.field-validation-error').addClass('errMsg');
});

/*Print confirmation page*/
$("#printCopy").click(function () {
    $('#printSaveArea').hide();
    $('#confirm-hint').hide();
    $('#ns-logo').hide();
    $('#backToStart').hide();
    window.print();
    $('#backToStart').show();
    $('#confirm-hint').show();
    $('#ns-logo').show();
    $('#printSaveArea').show();
});

