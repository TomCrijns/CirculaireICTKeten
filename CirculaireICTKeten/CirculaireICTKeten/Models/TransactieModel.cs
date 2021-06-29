using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace CirculaireICTKeten.Models
{
    public partial class TransactieModel
    {

        public static readonly DateTime StandardDateTimeOffset = new DateTime(1980, 1, 1, 0, 0, 0);
        public TransactieModel()
        {
            TransactieArtikelen = new HashSet<TransactieArtikelenModel>();
        }

        public TransactieModel(ILazyLoader lazyLoader)
        {
            LazyLoader = lazyLoader;
        }

        private ILazyLoader LazyLoader { get; set; }

        public int ProfielId { get; set; }
        public DateTime? Datum { get; set; }
        public string Serienummer { get; set; }
        public bool Donatie { get; set; }
        public bool Lening { get; set; }
        [Key]
        public int TransactieID { get; set; }


        private ICollection<TransactieArtikelenModel> _transactieArtikelen;
        public virtual ICollection<TransactieArtikelenModel> TransactieArtikelen
        {
            get => LazyLoader.Load(this, ref _transactieArtikelen);
            set => _transactieArtikelen = value;
        }

        public virtual ProfileDataModel Profiel { get; set; }
        //public virtual ICollection<TransactieArtikelenModel> TransactieArtikelen { get; set;  }
    }
}
