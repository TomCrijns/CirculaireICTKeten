using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace CirculaireICTKeten.Models
{
    public partial class TransactieModel
    {
        public TransactieModel()
        {
            TransactieArtikelen = new HashSet<TransactieArtikelenModel>();
        }
        public int ProfielId { get; set; }
        public DateTime Datum { get; set; }
        public string Serienummer { get; set; }
        public bool Donatie { get; set; }
        public bool Lening { get; set; }
        [Key]
        public int TransactieID { get; set; }

        public virtual ProfileDataModel Profiel { get; set; }
        public virtual  ICollection<TransactieArtikelenModel> TransactieArtikelen { get; set;  }
    }
}
