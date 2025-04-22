using Forum.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Utilisateurs> Utilisateurs { get; set; }
    public DbSet<Promos> Promos { get; set; }
    public DbSet<Etablissements> Etablissements { get; set; }
    public DbSet<Modules> Modules { get; set; }
    
<<<<<<< HEAD
    public DbSet<Messages> Messages { get; set; }
    public DbSet<Evaluations> Evaluations { get; set; }

=======
    public DbSet<ChatMessage> ChatMessages { get; set; }
    public DbSet<Amis> Amis { get; set; }
    public DbSet<Admin> Admin { get; set; }
>>>>>>> c126fb882254968bc4df4d839dcc8cde70282782

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}