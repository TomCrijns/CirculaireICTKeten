using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace CirculaireICTKeten.Models
{
    public partial class TransactieModel
    {
        public int ProfielId { get; set; }
        public DateTime Datum { get; set; }
        public int ArtikelId { get; set; }
        public int ArtikelAantal { get; set; }
        public string Serienummer { get; set; }
        public bool Donatie { get; set; }
        public bool Lening { get; set; }
        [Key]
        public int TransactieId { get; set; }

        public virtual ArtikelenModel Artikel { get; set; }
        public virtual ProfileDataModel Profiel { get; set; }
    }
}
