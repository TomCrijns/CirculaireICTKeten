using System;
using System.Collections.Generic;

#nullable disable

namespace CirculaireICTKeten.Models
{
    public partial class Leden
    {
        public int LidId { get; set; }
        public string Straat { get; set; }
        public int Huisnummer { get; set; }
        public string Huisnummertoevoeging { get; set; }
        public string Postcode { get; set; }
        public string Plaats { get; set; }
        public string Telefoonnummer { get; set; }
        public string Emailadres { get; set; }
        public int PuntenSaldo { get; set; }
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public string Tussenvoegsels { get; set; }
    }
}
