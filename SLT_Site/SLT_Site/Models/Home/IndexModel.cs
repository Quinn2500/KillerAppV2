using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SLT_Site.Models
{
    public class IndexModel
    {
        [Required(ErrorMessage = "Voer een gebruikersnaam in.")]
        [Display(Name = "Gebruikersnaam:")]
        public string username { get; set; }

        [Required(ErrorMessage = "Voer een wachtwoord in.")]
        [Display(Name = "Wachtwoord:")]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}
