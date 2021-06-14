using System;
using System.Collections.Generic;

#nullable disable

namespace CirculaireICTKeten.Models
{
    public partial class Transacty
    {
        public int ProfielId { get; set; }
        public DateTime Datum { get; set; }
        public int ArtikelId { get; set; }
        public int ArtikelAantal { get; set; }
        public string Serienummer { get; set; }
        public bool Donatie { get; set; }
        public bool Lening { get; set; }
        public int TransactieId { get; set; }

        public virtual Artikelen Artikel { get; set; }
        public virtual ProfileDatum Profiel { get; set; }
    }
}
