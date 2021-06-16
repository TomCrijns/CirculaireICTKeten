using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace CirculaireICTKeten.Models
{
    public partial class ArtikelSoortenModel
    {
        public ArtikelSoortenModel()
        {
            Artikelens = new HashSet<ArtikelenModel>();
        }
        public string ArtikelSoortNaam { get; set; }
        [Key]
        public int ArtikelSoortId { get; set; }

        public virtual ICollection<ArtikelenModel> Artikelens { get; set; }

    }
}