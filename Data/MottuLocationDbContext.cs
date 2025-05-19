using Microsoft.EntityFrameworkCore;
using MottuLocation.Entities;

namespace MottuLocation.Data
{
    public class MottuLocationDbContext : DbContext
    {
        public MottuLocationDbContext(DbContextOptions<MottuLocationDbContext> options) : base(options)
        {
        }

        public DbSet<Moto> Motos { get; set; }
        public DbSet<Sensor> Sensores { get; set; }
        public DbSet<Movimentacao> Movimentacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Moto>()
                .HasIndex(m => m.Placa)
                .IsUnique();

            modelBuilder.Entity<Moto>()
                .HasIndex(m => m.RfidTag)
                .IsUnique();

            modelBuilder.Entity<Sensor>()
                .HasIndex(s => s.Codigo)
                .IsUnique();

            modelBuilder.Entity<Movimentacao>()
                .HasOne(m => m.Moto)
                .WithMany(mo => mo.Movimentacoes)
                .HasForeignKey(m => m.MotoId)
                .OnDelete(DeleteBehavior.Cascade); // Exemplo de comportamento em cascata

            modelBuilder.Entity<Movimentacao>()
                .HasOne(m => m.Sensor)
                .WithMany(s => s.Movimentacoes)
                .HasForeignKey(m => m.SensorId)
                .OnDelete(DeleteBehavior.Restrict); // Exemplo de comportamento restrito
        }
    }
}