using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MadeByMe.src.DTOs
{
    public class UpdateProfileDto
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [MaxLength(50)]
        public string? MobileNumber { get; set; }

        [MaxLength(255)]
        public string? ProfilePicture { get; set; }

    }
}
