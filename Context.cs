using Forum.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Eleve> Eleves { get; set; }
    public DbSet<Professeur> Professeurs { get; set; }
    public DbSet<Classe> Classes { get; set; }
    public DbSet<Details_classe> Details_classe { get; set; }
    
    public DbSet<ChatMessage> ChatMessages { get; set; }
    public DbSet<Amis> Amis { get; set; }
    public DbSet<Admin> Admin { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}