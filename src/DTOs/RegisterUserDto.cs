using System.ComponentModel.DataAnnotations;

namespace MadeByMe.src.DTOs
{
	public class RegisterUserDto
	{
		[Required]
		[MaxLength(255)]
		public string Username { get; set; }

		[Required]
		[EmailAddress]
		public string EmailAddress { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Паролі не співпадають.")]
		public string ConfirmPassword { get; set; }
	}
}