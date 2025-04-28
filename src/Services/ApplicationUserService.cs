using MadeByMe.src.DTOs;
using MadeByMe.src.Models;
using Microsoft.EntityFrameworkCore;

namespace MadeByMe.src.Services
{
    public class ApplicationUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApplicationUserService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public ApplicationUser UpdateUser(int userId, UpdateProfileDto dto)
        {
            var user = _context.Users.Find(userId);
            if (user != null)
            {
                user.UserName = dto.UserName ?? user.UserName;
                user.Email = dto.Email ?? user.Email;
                user.ProfilePicture = dto.ProfilePicture ?? user.ProfilePicture;
                _context.SaveChanges();
            }
            return user;
        }
    }
}
