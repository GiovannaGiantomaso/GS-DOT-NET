using GsDotNet.Models;
using Microsoft.EntityFrameworkCore;

namespace GsDotNet.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UsuarioEnergia> Usuarios { get; set; }
        public DbSet<ConsumoEnergia> Consumos { get; set; }
        public DbSet<HistoricoConsumo> HistoricosConsumo { get; set; }
        public DbSet<FeedbackConsumo> FeedbacksConsumo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração para UsuarioEnergia
            modelBuilder.Entity<UsuarioEnergia>(entity =>
            {
                entity.ToTable("USUARIO_ENERGIA");
                entity.Property(e => e.IdUsuario).HasColumnName("ID_USUARIO");
                entity.Property(e => e.Nome).HasColumnName("NOME");
                entity.Property(e => e.Email).HasColumnName("EMAIL");
                entity.Property(e => e.Senha).HasColumnName("SENHA");
            });

            // Configuração para ConsumoEnergia
            modelBuilder.Entity<ConsumoEnergia>(entity =>
            {
                entity.ToTable("CONSUMO_ENERGIA");
                entity.Property(e => e.IdConsumo).HasColumnName("ID_CONSUMO");
                entity.Property(e => e.DataRegistro).HasColumnName("DATA_REGISTRO");
                entity.Property(e => e.ConsumoKwh).HasColumnName("CONSUMO_KWH");
                entity.Property(e => e.IdUsuario).HasColumnName("ID_USUARIO");

                entity.HasOne(c => c.Usuario)
                      .WithMany(u => u.Consumos)
                      .HasForeignKey(c => c.IdUsuario)
                      .OnDelete(DeleteBehavior.Cascade); // Exclui consumos ao excluir um usuário
            });

            // Configuração para HistoricoConsumo
            modelBuilder.Entity<HistoricoConsumo>(entity =>
            {
                entity.ToTable("HISTORICO_CONSUMO");
                entity.Property(e => e.IdHistorico).HasColumnName("ID_HISTORICO");
                entity.Property(e => e.TotalConsumo).HasColumnName("TOTAL_CONSUMO");
                entity.Property(e => e.MediaMensal).HasColumnName("MEDIA_MENSAL");
                entity.Property(e => e.IdUsuario).HasColumnName("ID_USUARIO");

                entity.HasOne(h => h.Usuario)
                      .WithMany() // Não cria relação reversa
                      .HasForeignKey(h => h.IdUsuario)
                      .OnDelete(DeleteBehavior.Cascade); // Exclui histórico ao excluir um usuário
            });

            // Configuração para FeedbackConsumo
            modelBuilder.Entity<FeedbackConsumo>(entity =>
            {
                entity.ToTable("FEEDBACK_CONSUMO");
                entity.Property(e => e.IdFeedback).HasColumnName("ID_FEEDBACK");
                entity.Property(e => e.MensagemFeedback).HasColumnName("MENSAGEM_FEEDBACK");
                entity.Property(e => e.IdUsuario).HasColumnName("ID_USUARIO");

                entity.HasOne(f => f.Usuario)
                      .WithMany() // Não cria relação reversa
                      .HasForeignKey(f => f.IdUsuario)
                      .OnDelete(DeleteBehavior.Cascade); // Exclui feedback ao excluir um usuário
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
