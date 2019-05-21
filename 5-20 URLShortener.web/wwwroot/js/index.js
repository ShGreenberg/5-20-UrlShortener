$(() => {
    $("#shorten-url").on('click', function () {
        const url = $("#url").val();
        $.get("/home/ShortenUrl", { url }, function (url) {
            $("#shorter").text(url.hashedUrl);
            $("#shorter").attr("href", url.hashedUrl);
        });
    });
});