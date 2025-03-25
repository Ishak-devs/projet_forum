using Microsoft.EntityFrameworkCore;
using Forum.Models;
using Microsoft.AspNetCore.Identity; //Import de dbcontext !
using Microsoft.AspNetCore.SignalR;
using Forum.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Configuration de la base de données MySQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.Parse("8.0.32-mysql") 
    ));

// Ajouter les services au conteneur
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();
builder.Services.AddSingleton<PasswordHasher<Eleve>>();
builder.Services.AddSingleton<PasswordHasher<Professeur>>();

builder.Services.AddSession(options =>
{
    //options.IdleTimeout = TimeSpan.FromMinutes(30); 
    options.Cookie.HttpOnly = true; 
    options.Cookie.IsEssential = true; 
});

var app = builder.Build();

// Configurer le pipeline de traitement des requêtes
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthorization();


app.MapHub<ChatHub>("/chatHub");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
