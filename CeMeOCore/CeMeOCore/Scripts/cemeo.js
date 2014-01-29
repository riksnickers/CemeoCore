$("#signInForm").submit(function (e) {
    event.preventDefault();
    var postdata = {
        grant_type: "password",
        username: "admin",
        password: "superadmin"
    };
   // alert("try login");
    $.ajax({
        type: "POST",
        url: "/Token",
        data: postdata,
        //taType: "json", //The dataType we get back from the server,
        //contentType: 'application/json; charset=utf-8',
        success: onLoginComplete,
        error: onError
    });
    //alert("made it here");
});

function onLoginComplete(result, data) {
    document.cookie = "token=" + result['access_token'];
    window.location = "/"

}

function onError(result) {
    $('#loginFailed').show();

}
