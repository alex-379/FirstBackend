using FirstBackend.API.Extensions;
using FirstBackend.Buiseness;
using FirstBackend.DataLayer;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Logging.ClearProviders();

    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

    builder.Host.UseSerilog();
    // Add services to the container.
    builder.Services.ConfigureApiServices(builder.Configuration);
    builder.Services.ConfigureBllServices();
    builder.Services.ConfigureDalServices();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseExceptionHandler();
    app.UseSerilogRequestLogging();
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex.Message);
}
finally
{
    Log.CloseAndFlush();
}