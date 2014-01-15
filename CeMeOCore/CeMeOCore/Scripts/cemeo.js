$("#signInForm").submit(function (event) {
    event.preventDefault();
    var postdate = {
        username: "admin",
        password: "superadmin"
    };


    $.ajax({
        type: "POST",
        url: "/Token",
        data: JSON.stringify(postdata),
        dataType: "json", //The dataType we get back from the server,
        contentType: 'application/json; charset=utf-8',
        success: function (postdata) {
            alert("succes");
        },
        error: function () {
            alert("error");
        }
    });
});
