using System.ComponentModel.DataAnnotations;

namespace SLT_Site.Models.Home
{
    public class IndexModel
    {
        [Required(ErrorMessage = "Voer een gebruikersnaam in.")]
        [Display(Name = "Gebruikersnaam:")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Voer een wachtwoord in.")]
        [Display(Name = "Wachtwoord:")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
