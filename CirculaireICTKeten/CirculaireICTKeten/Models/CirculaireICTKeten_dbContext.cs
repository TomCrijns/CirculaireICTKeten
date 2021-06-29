using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using CirculaireICTKeten.Models;

#nullable disable

namespace CirculaireICTKeten.Models
{
    public partial class CirculaireICTKeten_dbContext : DbContext
    {
        public CirculaireICTKeten_dbContext()
        {
        }

        public CirculaireICTKeten_dbContext(DbContextOptions<CirculaireICTKeten_dbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccountDatum> AccountData { get; set; }
        public virtual DbSet<AccountTypeLt> AccountTypeLts { get; set; }
        public virtual DbSet<ArtikelSoorten> ArtikelSoortens { get; set; }
        public virtual DbSet<Artikelen> Artikelens { get; set; }
        public virtual DbSet<Leden> Ledens { get; set; }
        public virtual DbSet<LedenpasLt> LedenpasLts { get; set; }
        public virtual DbSet<ProfileDatum> ProfileData { get; set; }
        public virtual DbSet<Transacty> Transacties { get; set; }
        public virtual DbSet<Klacht> Klacht { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:circulaireictketendbserver.database.windows.net,1433;Initial Catalog=CirculaireICTKeten_db;Persist Security Info=False;User ID=test123;Password=groepE-3;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AccountDatum>(entity =>
            {
                entity.Property(e => e.DateBlocked).HasColumnType("date");

                entity.HasOne(d => d.Profile)
                    .WithMany(p => p.AccountData)
                    .HasForeignKey(d => d.ProfileId)
                    .HasConstraintName("FK_AccountData_ProfileData");
            });

            modelBuilder.Entity<AccountTypeLt>(entity =>
            {
                entity.ToTable("AccountType_LT");

                entity.Property(e => e.AccountType).HasMaxLength(50);
            });

            modelBuilder.Entity<ArtikelSoorten>(entity =>
            {
                entity.HasKey(e => e.ArtikelSoortId);

                entity.ToTable("ArtikelSoorten");

                entity.Property(e => e.ArtikelSoortId).HasColumnName("ArtikelSoortID");

                entity.Property(e => e.ArtikelSoortNaam)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Artikelen>(entity =>
            {
                entity.HasKey(e => e.ArtikelId);

                entity.ToTable("Artikelen");

                entity.Property(e => e.ArtikelId).HasColumnName("ArtikelID");

                entity.Property(e => e.ArtikelNaam)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ArtikelSoortId).HasColumnName("ArtikelSoortID");

                entity.Property(e => e.Serienummer)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.ArtikelSoort)
                    .WithMany(p => p.Artikelens)
                    .HasForeignKey(d => d.ArtikelSoortId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Artikelen_ArtikelSoorten");
            });

            modelBuilder.Entity<Leden>(entity =>
            {
                entity.HasKey(e => e.LidId);

                entity.ToTable("Leden");

                entity.Property(e => e.LidId)
                    .ValueGeneratedNever()
                    .HasColumnName("LidID");

                entity.Property(e => e.Achternaam)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Emailadres)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EMailadres");

                entity.Property(e => e.Huisnummertoevoeging)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.Plaats)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Postcode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Straat)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.Telefoonnummer)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Tussenvoegsels)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Voornaam)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LedenpasLt>(entity =>
            {
                entity.ToTable("Ledenpas_LT");

                entity.Property(e => e.Status).HasMaxLength(50);
            });

            modelBuilder.Entity<ProfileDatum>(entity =>
            {
                entity.Property(e => e.Achternaam).HasMaxLength(50);

                entity.Property(e => e.Balans).HasDefaultValueSql("((0))");

                entity.Property(e => e.DateCreated).HasColumnType("date");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Geboortedatum).HasColumnType("date");

                entity.Property(e => e.Huisnummer).HasMaxLength(10);

                entity.Property(e => e.Postcode).HasMaxLength(10);

                entity.Property(e => e.Straat).HasMaxLength(50);

                entity.Property(e => e.Voornaam).HasMaxLength(50);

                entity.Property(e => e.Woonplaats).HasMaxLength(50);

                entity.HasOne(d => d.AccountTypeNavigation)
                    .WithMany(p => p.ProfileData)
                    .HasForeignKey(d => d.AccountType)
                    .HasConstraintName("FK_ProfileData_AccountType_LT");

                entity.HasOne(d => d.LedenpasNavigation)
                    .WithMany(p => p.ProfileData)
                    .HasForeignKey(d => d.Ledenpas)
                    .HasConstraintName("FK_ProfileData_Ledenpas_LT");
            });

            modelBuilder.Entity<Transacty>(entity =>
            {
                entity.HasKey(e => e.TransactieId);

                entity.Property(e => e.TransactieId).HasColumnName("TransactieID");

                entity.Property(e => e.ArtikelId).HasColumnName("ArtikelID");

                entity.Property(e => e.Datum).HasColumnType("datetime");

                entity.Property(e => e.Serienummer)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Artikel)
                    .WithMany(p => p.Transacties)
                    .HasForeignKey(d => d.ArtikelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transacties_Artikelen");

                entity.HasOne(d => d.Profiel)
                    .WithMany(p => p.Transacties)
                    .HasForeignKey(d => d.ProfielId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transacties_ProfileData");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
