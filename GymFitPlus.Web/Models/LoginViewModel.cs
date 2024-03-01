using System.ComponentModel.DataAnnotations;
using static GymFitPlus.Core.ErrorMessages.ErrorMessages;

namespace GymFitPlus.Web.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = RequiredErrorMessage)]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
