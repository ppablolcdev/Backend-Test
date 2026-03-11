
using Bsol.Business.Template.Api;
using Bsol.Business.Template.Api.Extensions;
using Bsol.Business.Template.Core;
using Bsol.Business.Template.Core.Entidades;
using Bsol.Business.Template.Core.Interfaces;
using Bsol.Business.Template.Core.UseCases.Cuentas;
using Bsol.Business.Template.Infrastructure;
using Bsol.Business.Template.Infrastructure.Data;
using Bsol.Business.Template.Infrastructure.Repositorios;
using Destructurama;
using FastEndpoints;
using FastEndpoints.Swagger;
using HealthChecks.UI.Client;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.EntityFrameworkCore;
using Serilog;  

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();
builder.Services.AddFastEndpoints();
builder.Services.AddCustomApiVersioning();
builder.Services.SwaggerDocument();
builder.Services.AddCors();
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
//builder.Services.AddPostgresDbContext(connectionString);
builder.Services.AddApiServices(builder.Configuration);
builder.Services.AddCoreServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

if (!builder.Environment.IsProduction())
{
    string[] origins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();
    builder.Services.AddCors(o => o.AddPolicy("AllowedOrigins", builder =>
        builder.WithOrigins(origins)
               .AllowAnyHeader()
               .AllowAnyMethod()
    ));
}

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(ObtenerCuentaHandler).Assembly));

//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("BancoDb"));

builder.Services.AddScoped<IRepositorioCuenta, RepositorioCuenta>();
builder.Services.AddScoped<IRepositorioTransaccion, RepositorioTransaccion>();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var contexto = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    contexto.Cuentas.AddRange(
     new Cuenta
     {
         Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
         NumeroCuenta = "1001",
         Saldo = 1000
     },
     new Cuenta
     {
         Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
         NumeroCuenta = "1002",
         Saldo = 500
     }
 );

    contexto.SaveChanges();
}

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(app.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.ApplicationInsights(app.Services.GetRequiredService<TelemetryConfiguration>(), TelemetryConverter.Traces)
    .Destructure.UsingAttributes()
    .CreateLogger();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseFastEndpoints(c =>
{
    c.Versioning.Prefix = "Bsol/v";
    c.Versioning.PrependToRoute = true;
});
app.UseSwaggerGen(); // FastEndpoints middleware

// Exception Middleware 
app.UseHealthChecks("/health",
    new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
    {
        Predicate = _ => true,
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

await app.RunAsync();

public partial class Program
{
    protected Program() { }
}
