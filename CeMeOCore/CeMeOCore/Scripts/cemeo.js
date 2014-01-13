$("#signInForm").submit(function (event) {
    //event.preventDefault();
    var postData = JSON.stringify({
        "username": $("#username").val(),
        "password": $("#password").val()
    });
    $.ajax({
        type: "POST",
        url: "api/Account/Register",
        data: postdata,
        dataType: "json", //The dataType we get back from the server
        succes: 
            function (data, status) {
                console.log("The returned token:" + data.access_token + "    Status:" + status);
                document.cookie = "token=" + data.access_token;
                alert("succefully");
            },
        error: function () {
            alert("Error try again");
        }
    })
})