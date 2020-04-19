using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ApiFilmowe.Modele
{
    public partial class BazaFilmowaContext : DbContext
    {
        public BazaFilmowaContext()
        {
        }

        public BazaFilmowaContext(DbContextOptions<BazaFilmowaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Adres> Adres { get; set; }
        public virtual DbSet<Film> Film { get; set; }
        public virtual DbSet<Klient> Klient { get; set; }
        public virtual DbSet<Rezyser> Rezyser { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=BazaFilmowa;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Adres>(entity =>
            {
                entity.Property(e => e.KodPocztowy)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Miasto)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Ulica)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Film>(entity =>
            {
                entity.Property(e => e.Nazwa)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Rezyser)
                    .WithMany(p => p.Film)
                    .HasForeignKey(d => d.RezyserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Film__RezyserId__29572725");
            });

            modelBuilder.Entity<Klient>(entity =>
            {
                entity.Property(e => e.Imie)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Nazwisko)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Telefon)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Adres)
                    .WithMany(p => p.Klient)
                    .HasForeignKey(d => d.AdresId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Klient__AdresId__2A4B4B5E");

                entity.HasOne(d => d.Film)
                    .WithMany(p => p.Klient)
                    .HasForeignKey(d => d.FilmId)
                    .HasConstraintName("FK__Klient__FilmId__2B3F6F97");
            });

            modelBuilder.Entity<Rezyser>(entity =>
            {
                entity.Property(e => e.Imie)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.KrajPochodzenia)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Nazwisko)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
