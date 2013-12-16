$("#signInForm").submit(function (event) {
    alert("Handler for .submit() called.");
    event.preventDefault();
    var account = {  username: "tom", password: "tomtom" }
  

    $.ajax({
        type: "POST",
        url: "http://localhost:12429/Token",
        data: account,
        timeout: 22000,
        crossDomain: true,
        dataType: "json", //The dataType we get back from the server
        success: function (data, status) {
            alert("The returned data:", data);
        },
        error: function (data, status) {
            alert("error ");
        }
    })
    .done(function (data) {
        console.log("data:" + data);
    })
    .fail(function (jqXHR, textStatus) {
        console.error("error:" + textStatus);
    })    
})