using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace CirculaireICTKeten.Models
{
    public partial class ArtikelenModel
    {
        public ArtikelenModel()
        {
            TransactieArtikelen = new HashSet<TransactieArtikelenModel>();
        }
        [Key]
        public int ArtikelID { get; set; }
        public string ArtikelNaam { get; set; }
        public int ArtikelSoortId { get; set; }
        public int ArtikelPunten { get; set; }
        public string Serienummer { get; set; }

        public virtual ArtikelSoortenModel ArtikelSoort { get; set; }
        public virtual ICollection<TransactieArtikelenModel> TransactieArtikelen { get; set; }
    }
}
