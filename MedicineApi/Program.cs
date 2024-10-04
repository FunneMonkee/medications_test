using MedicineApi.Models;
using MedicineApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables(); 
builder.Services.Configure<Settings>(
    builder.Configuration.GetSection("MedicationsDatabase"));
builder.Services.AddSingleton<MedicationService>();
builder.Services.AddControllers()
    .AddJsonOptions(
        options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

var app = builder.Build();

app.MapControllers();

app.Run();
