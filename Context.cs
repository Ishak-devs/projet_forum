using Forum.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Utilisateurs> Utilisateurs { get; set; }
    public DbSet<Promos> Promos { get; set; }
    public DbSet<Etablissements> Etablissements { get; set; }
    public DbSet<Modules> Modules { get; set; }
    
    public DbSet<Messages> Messages { get; set; }
    public DbSet<Evaluations> Evaluations { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}