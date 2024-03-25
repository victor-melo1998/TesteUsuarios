using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using UsuariosTeste.Configuration;
using UsuariosTeste.Contexto;
using UsuariosTeste.Mapeamento;
using UsuariosTeste.Models;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
//builder.Services.AddDbContext<ContaContexto>(opt => opt.UseInMemoryDatabase("db_VBMFinanceiro"));
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ContextoDB>(o => o.UseSqlServer(connectionString));
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(Profile).Assembly);
//builder.Services.WebApiConfig();
builder.Services.ResolveDependencies();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddMvcCore().AddApiExplorer();
//builder.Services.AddSwaggerConfig();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});

app.MapControllers();

app.Run();


