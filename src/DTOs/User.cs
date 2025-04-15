using System.ComponentModel.DataAnnotations;

namespace MadeByMe.src.DTOs
{
	// Для реєстрації

	public class RegisterUserDto
	{
		[Required]
		[MaxLength(50)]
		public string Username { get; set; }

		[Required]
		[EmailAddress]
		[MaxLength(100)]
		public string EmailAddress { get; set; }

		[Required]
		[MinLength(6)]
		public string Password { get; set; }

		[Required]
		[Compare("Password", ErrorMessage = "Паролі не співпадають")]
		public string ConfirmPassword { get; set; }
	}


	// Для логіну
	public class LoginUserDto
	{
		[Required]
		[EmailAddress]
		public string EmailAddress { get; set; }

		[Required]
		public string Password { get; set; }
	}

	// Для оновлення
	public class UpdateUserDto
	{
		[Required]
		public int UserId { get; set; }

		[MaxLength(50)]
		public string? Username { get; set; }

		[EmailAddress]
		[MaxLength(100)]
		public string? EmailAddress { get; set; }

		public string? ProfilePicture { get; set; }
	}

	// Для відображення
	public class UserResponseDto
	{
		public int UserId { get; set; }
		public string Username { get; set; }
		public string EmailAddress { get; set; }
		public string? ProfilePicture { get; set; }
	}
}