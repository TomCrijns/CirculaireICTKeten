using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CirculaireICTKeten.Models
{
    public partial class RuilwinkeldbContext : DbContext
    {
        public RuilwinkeldbContext()
        {
        }

        public RuilwinkeldbContext(DbContextOptions<RuilwinkeldbContext> options)
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
    }
}
