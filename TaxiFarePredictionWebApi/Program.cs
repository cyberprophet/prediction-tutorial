using Microsoft.Extensions.ML;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPredictionEnginePool<TaxiFarePrediction.ModelInput,
                                         TaxiFarePrediction.ModelOutput>()
                .FromFile("TaxiFarePrediction.mlnet");

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
                 new OpenApiInfo
                 {
                     Title = "My API",
                     Description = "Docs for my API",
                     Version = "v1"
                 });
});
if (builder.Build() is WebApplication app)
{
    app.UseSwagger();

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");

        c.DocumentTitle = "Algorithmic-Trading";
        c.RoutePrefix = "prediction";
    });
    app.MapPost("/predict",
                async (PredictionEnginePool<TaxiFarePrediction.ModelInput,
                       TaxiFarePrediction.ModelOutput> predictionEnginePool,
                       TaxiFarePrediction.ModelInput input)

                => await Task.FromResult(predictionEnginePool.Predict(input)));

    app.Run();
}