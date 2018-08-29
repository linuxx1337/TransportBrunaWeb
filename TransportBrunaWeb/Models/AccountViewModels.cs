using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TransportBrunaWeb.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "E-naslov")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "E-naslov")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Vnesite E-poštni naslov.")]
        [Display(Name = "E-naslov")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vnesite geslo.")]
        [DataType(DataType.Password)]
        [Display(Name = "Geslo")]
        public string Password { get; set; }

        [Display(Name = "Ostanite prijavljeni")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Vnesite E-poštni naslov.")]
        [EmailAddress]
        [Display(Name = "E-naslov")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vnesite uporabniško ime.")]
        [Remote("IsUserNameUnique", "Account", ErrorMessage = "Uporabniško ime je zasedeno.")]
        [Display(Name = "Uporabniško ime")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Vnesite geslo.")]
        [StringLength(100, ErrorMessage = "{0} mora imeti vsaj {2} znakov.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Geslo")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Ponovitev gesla")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Gesli se ne ujemata.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "E-naslov")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} mora imeti vsaj {2} znakov.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Geslo")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Ponovitev gesla")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Gesli se ne ujemata.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "E-naslov")]
        public string Email { get; set; }
    }
}
