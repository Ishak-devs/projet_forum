using Forum.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Utilisateurs> Utilisateurs { get; set; }
    public DbSet<Classes> Classes { get; set; }
    public DbSet<Etablissements> Etablissements { get; set; }
    public DbSet<Modules> Modules { get; set; }
    
    public DbSet<Messages> Messages { get; set; }
    public DbSet<Evaluations> Evaluations { get; set; }
<<<<<<< HEAD
    public DbSet<Alertes> Alerte { get; set; }
=======
    public DbSet<Alertes> Alertes { get; set; }
>>>>>>> 444c98a63715962a62c856341a626d674a925ce1

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}