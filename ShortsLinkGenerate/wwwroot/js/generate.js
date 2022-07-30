
function Genshorten() {
    var fullUrl = $("#fullURL").val();

    if (fullUrl != "") {
        const data = { FullURL: fullUrl };

        fetch('/Generate/GenerateFormPost', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(data),
        })
            .then((response) => response.json())
            .then((data) => {
                console.log(data);
                console.log(data.shortURL)
                $("#shortLink").html("<a href=" + data.hostName + data.shortURL + ">" + data.hostName + data.shortURL + "</a>");
            })
            .catch((error) => {
                console.error('Error:', error);
            });
    } else {
        alert("Please input full url.")
    }

}