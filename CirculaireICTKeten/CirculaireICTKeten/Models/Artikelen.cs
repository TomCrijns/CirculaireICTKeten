using System;
using System.Collections.Generic;

#nullable disable

namespace CirculaireICTKeten.Models
{
    public partial class Artikelen
    {
        public Artikelen()
        {
            Transacties = new HashSet<Transacty>();
        }

        public int ArtikelId { get; set; }
        public string ArtikelNaam { get; set; }
        public int ArtikelSoortId { get; set; }
        public int ArtikelPunten { get; set; }
        public string Serienummer { get; set; }

        public virtual ArtikelSoorten ArtikelSoort { get; set; }
        public virtual ICollection<Transacty> Transacties { get; set; }
    }
}
