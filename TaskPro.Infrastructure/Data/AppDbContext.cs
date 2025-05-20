using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPro.Domain.Entities;

namespace TaskPro.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<Tarea> Tareas => Set<Tarea>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(x => x.Email).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<Tarea>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Titulo)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(x => x.FechaCreacion)
                      .HasDefaultValueSql("GETUTCDATE()"); 

                entity.HasOne(x => x.Usuario)
                      .WithMany(u => u.Tareas)
                      .HasForeignKey(x => x.UsuarioId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

        }
    }
}
