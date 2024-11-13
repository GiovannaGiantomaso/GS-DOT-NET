using Microsoft.EntityFrameworkCore;
using GsDotNet.Models;

namespace GsDotNet.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UsuarioEnergia> Usuarios { get; set; }
        public DbSet<ConsumoEnergia> Consumos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsuarioEnergia>()
                .HasMany(u => u.Consumos)
                .WithOne(c => c.Usuario)
                .HasForeignKey(c => c.IdUsuario);

            base.OnModelCreating(modelBuilder);
        }
    }
}
