using System;
using System.Collections.Generic;

#nullable disable

namespace CirculaireICTKeten.Models
{
    public partial class AccountDataModel
    {
        public int Id { get; set; }
        public int? ProfileId { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }
        public bool? Blocked { get; set; }
        public DateTime? DateBlocked { get; set; }

        public virtual ProfileDataModel Profile { get; set; }
    }
}
