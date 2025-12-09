using GesBanqueAspNet.Models;
using Microsoft.EntityFrameworkCore;

namespace GesBanqueAspNet.Data
{
    public class BanqueDbContext : DbContext
    {
        public BanqueDbContext(DbContextOptions<BanqueDbContext> options)
            : base(options)
        {
        }

        public DbSet<Compte> Comptes => Set<Compte>();
        public DbSet<TransactionCompte> Transactions => Set<TransactionCompte>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Compte>()
                .HasIndex(c => c.Numero)
                .IsUnique();

            modelBuilder.Entity<Compte>()
                .HasMany(c => c.Transactions)
                .WithOne(t => t.Compte)
                .HasForeignKey(t => t.CompteId);
        }
    }
}
