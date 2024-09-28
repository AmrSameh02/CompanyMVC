using System.ComponentModel.DataAnnotations;

namespace Company.Route.PL.ViewModels
{
    public class ForgetPasswordViewModel
    {
        [EmailAddress(ErrorMessage = "Invalid Email")]
        [Required(ErrorMessage = "Email Is Required")]
        public string Email { get; set; }
    }
}
