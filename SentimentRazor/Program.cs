using Microsoft.Extensions.ML;

using static SentimentRazor.SentimentAnalysis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPredictionEnginePool<ModelInput, ModelOutput>()
    .FromFile("SentimentAnalysis.zip");

builder.Services.AddRazorPages();

if (builder.Build() is WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {

    }
    else
    {
        app.UseExceptionHandler("/Error");

        app.UseHsts();
    }
    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapRazorPages();

    app.Run();
}