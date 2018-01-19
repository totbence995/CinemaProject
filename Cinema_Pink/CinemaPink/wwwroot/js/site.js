// Write your JavaScript code.


//General AJAX call
function AjaxCall(url, data, callback) {

    var requestObj = {};
    requestObj = data;

    $.ajax({
        type: 'POST',
        url: url,
        data: JSON.stringify(requestObj),
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        cache: false,
        beforeSend: function () {
        },
        success: function ajaxSuccess(evt) {
            callback(evt, true);
        },
        complete: function () {
            console.log("ajax is working");
        },
        error: function ajaxError(xhr, status, error) {
            //DisplayError();
            callback(null, false);
        }
    });
}
