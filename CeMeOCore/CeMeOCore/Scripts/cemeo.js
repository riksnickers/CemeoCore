$("#signInForm").submit(function (event) {
    event.preventDefault();
    var username = $("#username").val();
    var pass = $("#password").val();
    var account = { grant_type: "password", username: username, password: pass }
    $.ajax({
        type: "POST",
        url: "http://localhost:7651/Account/CheckLogin",
        data: account,
        dataType: "json", //The dataType we get back from the server
    })
    .done(function (data, status) {
        console.log("The returned token:" + data.access_token + "    Status:" + status);
        document.cookie = "token=" + data.access_token;
        if( status == "success" )
        {
            alert(username + " logged in succesfully");
        }
    })
    .fail(function (jqXHR, textStatus) {
        console.error("error:" + textStatus);
    })
})