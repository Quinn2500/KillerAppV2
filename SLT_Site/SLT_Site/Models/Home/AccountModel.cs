using System.ComponentModel.DataAnnotations;

namespace SLT_Site.Models.Home
{
    public class AccountModel
    {
        [Required(ErrorMessage = "Voer een gebruikersnaam in.")]
        [StringLength(50, ErrorMessage = "The First Name must be less than {1} characters.")]
        [Display(Name = "Gebruikersnaam:")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Voer een wachtwoord in.")]
        [Display(Name = "Wachtwoord:")]
        [MaxLength(32, ErrorMessage = "Wachtwoord mag maximaal 32 tekens lang zijn")]
        [MinLength(4, ErrorMessage = "Wachtwoord moet minimaal 4 tekens lang zijn")]
        [RegularExpression(@"^.*(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\@#$%^&+=]).*$", ErrorMessage = "Je wachtwoord moet mininaal 1 nummer, 1 hoofdletter, 1 kleine letter en een speciaal teken bevatten")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "De wachtwoorden komen niet overeen.")]
        [Display(Name = "Herhaal je Wachtwoord:")]
        public string Password2 { get; set; }

        [EmailAddress(ErrorMessage = "The Email Address is not valid")]
        [Required(ErrorMessage = "Voer een email adres in.")]
        [Display(Name = "Email Address:")]
        public string Email { get; set; }

        [Display(Name = "Voornaam:")]
        public string Fname { get; set; }

        [Display(Name = "Achternaam:")]
        public string Lname { get; set; }
    }
}
