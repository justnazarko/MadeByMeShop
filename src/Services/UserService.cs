using MadeByMe.src.DTOs;
using MadeByMe.src.Models;
using Microsoft.EntityFrameworkCore;

namespace MadeByMe.src.Services
{
	public class UserService
	{
		private readonly ApplicationDbContext _context;

		public UserService(ApplicationDbContext context)
		{
			_context = context;
		}

		public void RegisterUser(RegisterUserDto registerUserDto)
		{
			var user = new User
			{
				Username = registerUserDto.Username,
				EmailAddress = registerUserDto.EmailAddress,
				Password = registerUserDto.Password // Захешуйте пароль перед збереженням
			};

			_context.Users.Add(user);
			_context.SaveChanges();
		}

		public User LoginUser(LoginUserDto loginUserDto)
		{
			var user = _context.Users
				.FirstOrDefault(u => u.EmailAddress == loginUserDto.EmailAddress && u.Password == loginUserDto.Password);

			return user;
		}

		public User GetCurrentUser()
		{
			// Логіка для отримання поточного користувача (наприклад, з кукі)
			return null;
		}

		public User UpdateUser(UpdateUserDto updateUserDto)
		{
			var user = _context.Users.Find(updateUserDto.UserId);
			if (user == null)
				return null;

			user.Username = updateUserDto.Username;
			user.EmailAddress = updateUserDto.EmailAddress;

			_context.SaveChanges();
			return user;
		}
	}
}