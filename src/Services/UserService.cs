using MadeByMe.src.DTOs;
using MadeByMe.src.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace MadeByMe.src.Services
{
	public class UserService
	{
		private readonly ApplicationDbContext _context;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public UserService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
		{
			_context = context;
			_httpContextAccessor = httpContextAccessor;
		}

		public User RegisterUser(RegisterUserDto registerUserDto)
		{
			var user = new User
			{
				Username = registerUserDto.Username,
				EmailAddress = registerUserDto.EmailAddress,
				Password = registerUserDto.Password
			};

			_context.Users.Add(user);
			_context.SaveChanges();
			return user;
		}

		public User LoginUser(LoginUserDto loginUserDto)
		{
			return _context.Users
				.FirstOrDefault(u => u.EmailAddress == loginUserDto.EmailAddress
								  && u.Password == loginUserDto.Password);
		}

		public User UpdateUser(int userId, UpdateUserDto updateUserDto)
		{
			var user = _context.Users.Find(userId);
			if (user != null)
			{
				user.Username = updateUserDto.Username ?? user.Username;
				user.EmailAddress = updateUserDto.EmailAddress ?? user.EmailAddress;
				user.ProfilePicture = updateUserDto.ProfilePicture ?? user.ProfilePicture;
				_context.SaveChanges();
			}
			return user;
		}

		public User GetUserById(int id)
		{
			return _context.Users.Find(id);
		}

		public User GetCurrentUser()
		{
			var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (string.IsNullOrEmpty(userId)) return null;

			if (int.TryParse(userId, out int id))
			{
				return _context.Users.FirstOrDefault(u => u.UserId == id);
			}
			return null;
		}
	}
}