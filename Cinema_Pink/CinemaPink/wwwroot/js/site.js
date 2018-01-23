// Write your JavaScript code.

//function GetSeatsAjax(url, data, callback) {

//    var requestObj = {};
//    requestObj = data;

//    $.ajax({
//        type: 'GET',
//        url: url,
//        success: function (data) {
//            alert(data);
//        },
//        success: function ajaxSuccess(evt) {
//            callback(evt, true);
//        },
//        complete: function () {
//            console.log("ajax is working");
//        },
//        error: function ajaxError(xhr, status, error) {

//            callback(null, false);
//        }
//     });
//General AJAX call
function AjaxCall(url, data, callback) {

    var requestObj = {};
    requestObj.data = data.id;

    $.ajax({
        type: 'POST',
        url: url,
        data: JSON.stringify(data),
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