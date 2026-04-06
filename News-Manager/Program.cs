using Microsoft.EntityFrameworkCore;
using News_Manager.Data;
using News_Manager.Repositories;
using News_Manager.Services;

var builder = WebApplication.CreateBuilder(args);

// Configuração do Oracle
builder.Services.AddDbContext<NewsDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection")));

// Registro do Repositório (Scoped é o padrão para Web Apps com Banco de Dados)
builder.Services.AddScoped<INewsRepository, NewsRepository>();
builder.Services.AddScoped<INewsService, NewsService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=News}/{action=Index}/{id?}");

app.Run();