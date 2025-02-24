using Microsoft.EntityFrameworkCore;
using Forum.Models; // Assure-toi d'importer ton DbContext

var builder = WebApplication.CreateBuilder(args);

// Configuration de la base de données MySQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.Parse("8.0.32-mysql") 
    ));

// Ajouter les services au conteneur
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configurer le pipeline de traitement des requêtes
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
