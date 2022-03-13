using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace App.Data.Models
{
    public partial class pruebanetContext : DbContext
    {
        public pruebanetContext()
        {
        }

        public pruebanetContext(DbContextOptions<pruebanetContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblCatSexo> TblCatSexos { get; set; }
        public virtual DbSet<TblUsuario> TblUsuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //Actualizar cadena de conexión
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-HA97M3U;Initial Catalog=pruebanet;User ID=sa;Password=123456");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<TblCatSexo>(entity =>
            {
                entity.HasKey(e => e.IntIdsexo)
                    .HasName("XPKtblCatSexo");

                entity.ToTable("tblCatSexo");

                entity.Property(e => e.IntIdsexo).HasColumnName("intIDSexo");

                entity.Property(e => e.StrSexo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("strSexo");
            });

            modelBuilder.Entity<TblUsuario>(entity =>
            {
                entity.HasKey(e => e.IntIdusuario)
                    .HasName("XPKtblUsuario");

                entity.ToTable("tblUsuario");

                entity.HasIndex(e => e.StrCorreo, "UQ__tblUsuar__143603CAF1C8B293")
                    .IsUnique();

                entity.HasIndex(e => e.StrUsuario, "UQ__tblUsuar__784B73C8C8CE244A")
                    .IsUnique();

                entity.Property(e => e.IntIdusuario).HasColumnName("intIDUsuario");

                entity.Property(e => e.BitEstatus).HasColumnName("bitEstatus");

                entity.Property(e => e.DateCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("dateCreacion")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IntIdsexo).HasColumnName("intIDSexo");

                entity.Property(e => e.StrCorreo)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("strCorreo");

                entity.Property(e => e.StrPassword)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("strPassword");

                entity.Property(e => e.StrUsuario)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("strUsuario");

                entity.HasOne(d => d.IntIdsexoNavigation)
                    .WithMany(p => p.TblUsuarios)
                    .HasForeignKey(d => d.IntIdsexo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tblCatSexo_intIDSexo_tblUsuario");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
