using Microsoft.EntityFrameworkCore;
using Projet.Models.Entity;

namespace Projet.Models.Context;

public class MySqlContext : DbContext
{
    public MySqlContext(DbContextOptions<MySqlContext> options) : base(options) {}

    public DbSet<UtilisateurEntity> Utilisateurs { get; set; }
    public DbSet<CyberharceleurEntity> Cyberharceleurs { get; set; }
    public DbSet<DossierEntity> Dossiers { get; set; }
    public DbSet<Preuve> Preuves { get; set; }
    public DbSet<TypePreuveEntity> TypePreuves { get; set; }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UtilisateurEntity>()
            .HasIndex(u => u.Pseudo)
            .IsUnique();
    }
    public override int SaveChanges()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is BaseEntity && 
                        (e.State == EntityState.Added || e.State == EntityState.Modified));

        foreach (var entityEntry in entries)
        {
            ((BaseEntity)entityEntry.Entity).ModifiedAt = DateTime.Now;

            if (entityEntry.State == EntityState.Added)
            {
                ((BaseEntity)entityEntry.Entity).CreatedAt = DateTime.Now;
            }
        }

        return base.SaveChanges();
    }


}
