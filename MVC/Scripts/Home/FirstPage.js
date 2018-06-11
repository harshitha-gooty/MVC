$(document).ready(function () { });
$('#submit').click(function () {
    var UserName = $('#txtUserName').val();
    var Password = $('#txtPassword').val();
    $.ajax({
        type: 'POST',
        url: 'http://localhost/MVC/Home/Login?UserName=' + UserName + '&Password=' + Password,
        contentType: 'application/json;charset=utf-8',
        success: function (response) { },
        error: function (ex) { }
    });
});