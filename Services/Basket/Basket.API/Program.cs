using Asp.Versioning;
using Basket.Application.Commands;
using Basket.Core.Repositories;
using Basket.Infraestructure.Repositories;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddApiVersioning(opt => { opt.DefaultApiVersion = new ApiVersion(1, 0); });

//Redis Settings
builder.Services.AddStackExchangeRedisCache(optiones =>
{
    optiones.Configuration = builder.Configuration.GetValue<string>("CacheSettings:ConnectionString");
});

builder.Services.AddMediatR((x => x.RegisterServicesFromAssemblies(typeof(CreateShoppingCartCommand).Assembly)));
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo() { Title = "Basket.API", Version = "v1" }); });
builder.Services.AddHealthChecks()
    .AddRedis(builder.Configuration["CacheSettings:ConnectionString"], "Redis Health", HealthStatus.Degraded);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Basket.API v1"));

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

//Ya no se usa app.UseEndpoints()
app.Run();
