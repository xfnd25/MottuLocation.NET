using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using MottuLocation.Data;
using MottuLocation.Repositories;
using MottuLocation.Services;
using MottuLocation.Profiles;
using System.IO;
using System;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using MottuLocation.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configuração do Versionamento
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

// ATUALIZAÇÃO DO SWAGGER ABAIXO
builder.Services.AddSwaggerGen(options =>
{
    var apiVersionDescriptionProvider = builder.Services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

    foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
    {
        options.SwaggerDoc(description.GroupName, new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = "Mottu Location API",
            Version = description.ApiVersion.ToString(),
            Description = "API para gerenciamento e localização de motos, sensores e suas movimentações."
        });
    }

    // Configura o Swagger para usar o arquivo XML gerado pelos comentários
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);

    // Adiciona a definição de segurança para a API Key
    options.AddSecurityDefinition("ApiKey", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "API Key a ser usada no header X-API-KEY",
        Name = "X-API-KEY",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "ApiKeyScheme"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "ApiKey"
                }
            },
            new string[] {}
        }
    });
});

// Configurando o DbContext para Oracle
builder.Services.AddDbContext<MottuLocationDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection")));

// Configurando o AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Registrando os Repositórios e Serviços
builder.Services.AddScoped<IMotoRepository, MotoRepository>();
builder.Services.AddScoped<ISensorRepository, SensorRepository>();
builder.Services.AddScoped<IMovimentacaoRepository, MovimentacaoRepository>();

builder.Services.AddScoped<IMotoService, MotoService>();
builder.Services.AddScoped<ISensorService, SensorService>();
builder.Services.AddScoped<IMovimentacaoService, MovimentacaoService>();

// Registrando o novo serviço de Machine Learning
builder.Services.AddSingleton<IPredictionService, PredictionService>();

// Configurando o Health Check
builder.Services.AddHealthChecks()
    .AddDbContextCheck<MottuLocation.Data.MottuLocationDbContext>("Oracle DB");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
        }
    });
}

// CORREÇÃO: Mapeia o endpoint de Health Check ANTES da segurança para que ele seja público
app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseHttpsRedirection();

// O middleware de segurança agora é aplicado DEPOIS do Health Check, protegendo os outros endpoints
app.UseMiddleware<ApiKeyAuthMiddleware>();

app.UseAuthorization();
app.MapControllers();

app.Run();

public partial class Program { }

