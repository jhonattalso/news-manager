using Microsoft.EntityFrameworkCore;
using News_Manager.Data;
using News_Manager.Repositories;
using News_Manager.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();

// Configuração do Oracle
builder.Services.AddDbContext<NewsDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection")));

// ---------------- Helth Check para Oracle ----------------
builder.Services.AddHealthChecks()
    .AddOracle(
        connectionString: builder.Configuration.GetConnectionString("OracleConnection"),
        name: "oracle_db",
        failureStatus: Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Unhealthy,
        tags: new[] { "db", "oracle" }
    );
// ---------------------------------------------------------

builder.Services.AddScoped<INewsRepository, NewsRepository>();
builder.Services.AddScoped<INewsService, NewsService>();

builder.Services.AddControllersWithViews();

try {
    Log.Information("Iniciando a aplicação News-Manager");
    var app = builder.Build();

    if (!app.Environment.IsDevelopment()) {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();
    app.UseAuthorization();

    // Middleware para logar requisições HTTP (Opcional, mas recomendado para a Sprint)
    app.UseSerilogRequestLogging();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=News}/{action=Index}/{id?}");

    app.MapHealthChecks("/health");

    app.Run();
}
catch (Exception ex) {
    Log.Fatal(ex, "A aplicação falhou ao iniciar");
}
finally {
    Log.CloseAndFlush();
}