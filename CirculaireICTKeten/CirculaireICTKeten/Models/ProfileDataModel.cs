using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace CirculaireICTKeten.Models
{
    public partial class ProfileDataModel
    {
        public ProfileDataModel()
        {
            AccountData = new HashSet<AccountDataModel>();

            Transacties = new HashSet<TransactieModel>();
        }
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public int? Balans { get; set; }
        public int? AccountType { get; set; }
        public int? Ledenpas { get; set; }
        public string Straat { get; set; }
        public string Huisnummer { get; set; }
        public string Woonplaats { get; set; }
        public string Postcode { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? Geboortedatum { get; set; }

        public virtual AccountTypeLtModel AccountTypeNavigation { get; set; }
        public virtual LedenpasLtModel LedenpasNavigation { get; set; }
        public virtual ICollection<AccountDataModel> AccountData { get; set; }
        public virtual ICollection<TransactieModel> Transacties { get; set; }
    }
}
