using System.ComponentModel.DataAnnotations;

namespace Company.Route.PL.ViewModels
{
	public class SignUpViewModel
	{
		[Required(ErrorMessage ="UserName Is Required")]
		public string UserName {  get; set; }
		[Required(ErrorMessage = "FirstName Is Required")]
		public string FirstName { get; set; }
		[Required(ErrorMessage = "LastName Is Required")]
		public string LastName { get; set; }
		[EmailAddress(ErrorMessage ="Invalid Email")]
		[Required(ErrorMessage = "Email Is Required")]
		public string Email { get; set; }
		[Required(ErrorMessage = "password Is REquired")]
		[DataType(DataType.Password)]
		[MaxLength(16)]
		[MinLength(4)]
		public string Password { get; set; }
		[Required(ErrorMessage = "ConfirmPassword Is REquired")]
		[DataType (DataType.Password)]
		[Compare(nameof(Password), ErrorMessage ="Confirmed password must be matching password")]
		public string ConfirmPassword { get; set; }
		public bool IsAgree { get; set; }
	}
}
