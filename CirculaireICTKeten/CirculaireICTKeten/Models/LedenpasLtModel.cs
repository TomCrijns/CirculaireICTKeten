using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace CirculaireICTKeten.Models
{
    public partial class LedenpasLtModel
    {
        public LedenpasLtModel()
        {
            ProfileData = new HashSet<ProfileDataModel>();
        }
        [Key]
        public int Id { get; set; }
        public string Status { get; set; }

        public virtual ICollection<ProfileDataModel> ProfileData { get; set; }
    }
}
