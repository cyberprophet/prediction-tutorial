function getSentiment(userInput)
{
    return fetch(`Index?handler=AnalyzeSentiment&text=${userInput}`)
        .then((response) =>
        {
            return response.text();
        })
}
function updateMarker(markerPosition, sentiment)
{
    $("#markerPosition").attr("style", `left:${markerPosition}%`);
    $("#markerValue").text(sentiment);
}
function updateSentiment()
{
    var userInput = $("#Message").val();

    getSentiment(userInput)
        .then((sentiment) =>
        {
            switch (sentiment)
            {
                case "Not Toxic":
                    updateMarker(100.0, sentiment);
                    break;
                case "Toxic":
                    updateMarker(0.0, sentiment);
                    break;
                default:
                    updateMarker(45.0, "Neutral");
            }
        });
}        