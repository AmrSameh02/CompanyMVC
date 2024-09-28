using System.ComponentModel.DataAnnotations;

namespace Company.Route.PL.ViewModels
{
	public class SignInViewModel
	{
		[EmailAddress(ErrorMessage = "Invalid Email")]
		[Required(ErrorMessage = "Email Is Required")]
		public string Email { get; set; }
		[Required(ErrorMessage = "password Is REquired")]
		[DataType(DataType.Password)]
		[MaxLength(16)]
		[MinLength(4)]
		public string Password { get; set; }
        
		public bool RememberMe { get; set; }
    }
}
