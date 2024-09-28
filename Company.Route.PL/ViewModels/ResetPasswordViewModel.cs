using System.ComponentModel.DataAnnotations;

namespace Company.Route.PL.ViewModels
{
	public class ResetPasswordViewModel
	{
		[Required(ErrorMessage = "password Is REquired")]
		[DataType(DataType.Password)]
		[MaxLength(16)]
		[MinLength(4)]
		public string Password { get; set; }
		[Required(ErrorMessage = "ConfirmPassword Is REquired")]
		[DataType(DataType.Password)]
		[Compare(nameof(Password), ErrorMessage = "Confirmed password must be matching password")]
		public string ConfirmPassword { get; set; }
	}
}
