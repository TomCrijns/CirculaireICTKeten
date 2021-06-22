using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace CirculaireICTKeten.Models
{
    public partial class TransactieArtikelenModel
    {
        [Key]
        public int TransactieArtikelID { get; set; }
        public int TransactieID { get; set; }
        public int ArtikelID { get; set; }
        //public bool IsVerkoop { get; set; }
        public int Punten { get; set; }
        public int Aantal { get; set; }



        public virtual TransactieModel Transactie { get; set; }
        public virtual ArtikelenModel Artikelen { get; set; }

    }
}
