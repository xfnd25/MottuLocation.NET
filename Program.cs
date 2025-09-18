using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using MottuLocation.Data;
using MottuLocation.Repositories;
using MottuLocation.Services;
using MottuLocation.Profiles;
using System.IO; // Adicione esta linha
using System; // Adicione esta linha

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// ATUALIZAÇÃO DO SWAGGER ABAIXO
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Mottu Location API",
        Version = "v1",
        Description = "API para gerenciamento e localização de motos, sensores e suas movimentações."
    });

    // Configura o Swagger para usar o arquivo XML gerado pelos comentários
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
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