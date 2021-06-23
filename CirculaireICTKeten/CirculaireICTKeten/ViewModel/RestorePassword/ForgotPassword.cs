using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CirculaireICTKeten.ViewModel.RestorePassword
{
    public class ForgotPassword
    {
        [Required(ErrorMessage = "Er is geen e-mailadres ingevuld")]
        [Display(Name = "E-mailadres")]
        public string emailAddress { get; set; }
    }
}
