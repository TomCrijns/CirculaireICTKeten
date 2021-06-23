using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

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

        public virtual DbSet<AccountDataModel> AccountData { get; set; }
        public virtual DbSet<AccountTypeLtModel> AccountTypeLt { get; set; }
        public virtual DbSet<ArtikelSoortenModel> ArtikelSoorten { get; set; }
        public virtual DbSet<ArtikelenModel> Artikelen { get; set; }
        public virtual DbSet<LedenpasLtModel> LedenpasLt { get; set; }
        public virtual DbSet<ProfileDataModel> ProfileData { get; set; }
        public virtual DbSet<TransactieModel> Transacties { get; set; }
        public virtual DbSet<TransactieArtikelenModel> TransactieArtikelen { get; set; }

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

            modelBuilder.Entity<AccountDataModel>(entity =>
            {
                entity.Property(e => e.DateBlocked).HasColumnType("date");

                entity.HasOne(d => d.Profile)
                    .WithMany(p => p.AccountData)
                    .HasForeignKey(d => d.ProfileId)
                    .HasConstraintName("FK_AccountData_ProfileData");
            });

            modelBuilder.Entity<AccountTypeLtModel>(entity =>
            {
                entity.ToTable("AccountType_LT");

                entity.Property(e => e.AccountType).HasMaxLength(50);
            });

            modelBuilder.Entity<ArtikelSoortenModel>(entity =>
            {
                entity.HasKey(e => e.ArtikelSoortId);

                entity.ToTable("ArtikelSoorten");

                entity.Property(e => e.ArtikelSoortId).HasColumnName("ArtikelSoortID");

                entity.Property(e => e.ArtikelSoortNaam)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ArtikelenModel>(entity =>
            {
                entity.ToTable("Artikelen");

                entity.HasKey(e => e.ArtikelID);

                entity.Property(e => e.ArtikelID).HasColumnName("ArtikelID");

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

            

            modelBuilder.Entity<LedenpasLtModel>(entity =>
            {
                entity.ToTable("Ledenpas_LT");

                entity.Property(e => e.Status).HasMaxLength(50);
            });

            modelBuilder.Entity<ProfileDataModel>(entity =>
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

            modelBuilder.Entity<TransactieModel>(entity =>
            {
                entity.HasKey(e => e.TransactieID);

                entity.Property(e => e.TransactieID).HasColumnName("TransactieID");

                entity.Property(e => e.Datum).HasColumnType("datetime");

                entity.Property(q => q.Lening)
                    .HasColumnName("Lening")
                    //.HasConversion(new BoolToZeroOneConverter<byte>())
                    .IsRequired();

                entity.Property(q => q.Donatie)
                    .HasColumnName("Donatie")
                    //.HasConversion(new BoolToZeroOneConverter<byte>())
                    .IsRequired();

                entity.Property(e => e.Serienummer)
                    .HasMaxLength(50)
                    .IsUnicode(false);


                entity.HasOne(d => d.Profiel)
                    .WithMany(p => p.Transacties)
                    .HasForeignKey(d => d.ProfielId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transacties_ProfileData");

            });

            modelBuilder.Entity<TransactieArtikelenModel>(e =>
            {
                
                e.ToTable("TransactieArtikelen");
                //e.HasKey(q => new { q.TransactieID, q.ArtikelID});


                e.HasKey(e => e.TransactieArtikelID);
                e.Property(q => q.TransactieID)
                    .HasColumnName("TransactieID")
                    .IsRequired();
                e.Property(q => q.ArtikelID)
                    .HasColumnName("ArtikelID")
                    .HasMaxLength(24)
                    .IsRequired();
                e.Property(q => q.IsVerkoop)
                    //.HasConversion(new BoolToZeroOneConverter<byte>())
                    .HasColumnName("IsVerkoop")
                    .IsRequired();
                e.Property(q => q.Punten)
                    .HasColumnName("Punten")
                    .IsRequired();
                e.Property(q => q.Aantal)
                    .HasColumnName("Aantal")
                    .IsRequired();



                e.HasOne(q => q.Transactie)
                    .WithMany(q => q.TransactieArtikelen)
                    .HasForeignKey(q => q.TransactieID);
                e.HasOne(q => q.Artikelen)
                    .WithMany(q => q.TransactieArtikelen)
                    .HasForeignKey(q => q.ArtikelID);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
