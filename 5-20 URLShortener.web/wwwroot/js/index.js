$(() => {
    $("#shorten-url").on('click', function () {
        const url = $("#url").val();
        $.get("/home/ShortenUrl", { url }, function (url) {
            //needs to be a url..... - did this wrong - need to use route
            $("#shorter").text(url.hashedUrl);
            //$("#shorter").attr("href", url.orgUrl);
            $("#shorter").attr("href", url.hashedUrl);
        });
    });
});