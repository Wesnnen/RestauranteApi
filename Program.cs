using Microsoft.EntityFrameworkCore;
using RestauranteApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Adiciona o contexto do banco de dados (Entity Framework Core usando MySQL)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApiContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 25)))); // Ajuste para a versão do seu MySQL

// Adicionar os serviços ao container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTudo", policyBuilder =>
    {
        policyBuilder.AllowAnyOrigin()
                     .AllowAnyMethod()
                     .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configurações para desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors("PermitirTudo");
app.MapControllers();

app.Run();

