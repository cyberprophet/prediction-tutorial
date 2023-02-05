using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.ML;

using static SentimentRazor.SentimentAnalysis;

namespace SentimentRazor.Pages;

public class IndexModel : PageModel
{
    public IndexModel(ILogger<IndexModel> logger,
                      PredictionEnginePool<ModelInput, ModelOutput> predictionEnginePool)
    {
        this.logger = logger;
        this.predictionEnginePool = predictionEnginePool;
    }
    public void OnGet()
    {

    }
    public IActionResult OnGetAnalyzeSentiment([FromQuery] string text)
    {
        if (string.IsNullOrEmpty(text))
            return Content("Neutral");

        var input = new ModelInput
        {
            SentimentText = text
        };
        var prediction = predictionEnginePool.Predict(input);

        var sentiment = Convert.ToBoolean(prediction.PredictedLabel) ? "Toxic" : "Not Toxic";

        return Content(sentiment);
    }
    readonly PredictionEnginePool<ModelInput, ModelOutput> predictionEnginePool;
    readonly ILogger<IndexModel> logger;
}