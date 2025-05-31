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

    public DbSet<Alertes> Alerte { get; set; }

    public DbSet<Alertes> Alertes { get; set; }

    public DbSet<Notes> Notes { get; set; }

    public DbSet<Devoirs> Devoirs { get; set; }
    public DbSet<EffectifParClasse> EffectifParClasse { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<EffectifParClasse>(entity =>
        {
            entity.HasNoKey();  
            entity.ToView(null);
        });
    }
}