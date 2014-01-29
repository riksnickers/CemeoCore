$("#signInForm").submit(function (e) {
    event.preventDefault();
    var postdate = {
        username: "admin",
        password: "superadmin"
    };
    alert("try login");
    $.ajax({
        type: "POST",
        url: "/Token",
        data: JSON.stringify(postdata),
        dataType: "json", //The dataType we get back from the server,
        contentType: 'application/json; charset=utf-8',
        success: onLoginComplete,
        error: onError
    });
    alert("made it here");
});

function onLoginComplete(result, data) {
    document.cookie = "token=" + data.access_token;
    alert("hooray");

}

function onError(result) {
    $('#loginFailed').show();

}
