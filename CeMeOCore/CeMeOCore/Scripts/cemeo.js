$("#signInForm").submit(function (event) {
    alert("Handler for .submit() called.");
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
            //TODO: nog doorsturen 
        }
    })
    .fail(function (jqXHR, textStatus) {
        console.error("error:" + textStatus);
    })
})