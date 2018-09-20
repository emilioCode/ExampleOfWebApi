using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApiPaises.Models
{
    public partial class DbPaisesContext : DbContext
    {
        public virtual DbSet<Pais> Pais { get; set; }
        public virtual DbSet<Provincia> Provincia { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=ASUS;Database=DbPaises;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pais>(entity =>
            {
                entity.ToTable("pais");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Provincia>(entity =>
            {
                entity.ToTable("provincia");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdPais).HasColumnName("idPais");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasColumnType("nchar(30)");

                entity.HasOne(d => d.IdPaisNavigation)
                    .WithMany(p => p.Provincia)
                    .HasForeignKey(d => d.IdPais)
                    .HasConstraintName("FK_provincia_pais");
            });
        }
    }
}
