using System;
using System.Collections.Generic;

#nullable disable

namespace CirculaireICTKeten.Models
{
    public partial class ArtikelSoorten
    {
        public ArtikelSoorten()
        {
            Artikelens = new HashSet<Artikelen>();
        }

        public string ArtikelSoortNaam { get; set; }
        public int ArtikelSoortId { get; set; }

        public virtual ICollection<Artikelen> Artikelens { get; set; }
    }
}
