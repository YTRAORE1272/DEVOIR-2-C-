using GesBanqueAspNet.Data;
using GesBanqueAspNet.Services;
using GesBanqueAspNet.Services.Impl;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// DbContext - InMemory pour que le projet fonctionne directement.
// Tu pourras remplacer par UseNpgsql / UseSqlServer plus tard.
builder.Services.AddDbContext<BanqueDbContext>(options =>
    options.UseInMemoryDatabase("BanqueDb"));

// Services métiers
builder.Services.AddScoped<ICompteService, CompteService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Seed des données de test au démarrage
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<BanqueDbContext>();
    DataSeeder.Seed(db);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Route par défaut vers la page d'accueil
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Compte}/{action=Home}/{id?}");

app.Run();
