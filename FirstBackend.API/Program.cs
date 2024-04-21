using FirstBackend.API.Extensions;
using FirstBackend.Buiseness;
using FirstBackend.DataLayer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureApiServices();
builder.Services.ConfigureBllServices();
builder.Services.ConfigureDalServices();
builder.Services.ConfigureDataBase();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
