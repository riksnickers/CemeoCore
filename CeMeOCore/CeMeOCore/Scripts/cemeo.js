/*$("#signInForm").submit(function (event) {
    event.preventDefault();
    var account = { grant_type: "password", username: "Admin", password: "superadmin" }
    $.ajax({
        type: "POST",
        url: "/Token",
        data: account,
        dataType: "json", //The dataType we get back from the server
    })
    .done(function (data, status) {
        console.log("The returned token:" + data.access_token + "    Status:" + status);
        document.cookie = "token=" + data.access_token;
        if( status == "success" )
        {
            alert("hooray");
        }
    })
    .fail(function (jqXHR, textStatus) {
        console.error("error:" + textStatus);
    })
})*/

$("#signInForm").submit(function (e) {
   // e.preventDefault();
    var username = "admin"
    var password = "superadmin"
    // Log them in. 
    alert("try login");
    $.ajax({
        url: "/Token",
        type: 'post',
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        data: "{ 'username': '" + username + "', 'password':'" + password + "'}",
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