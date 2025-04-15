using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MadeByMe.src.Models
{
    public class User
    {
        [Key]
        [Column("user_id")]
        public int UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MaxLength(100)]
        public string EmailAddress { get; set; }

        [MaxLength(50)]
        public string? ModelNumber { get; set; }

        [Required]
        [MaxLength(100)]
        public string Password { get; set; }

        public bool IsBlocked { get; set; } = false;

        [MaxLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string? UserType { get; set; } // "Moderator", "Seller", "Buyer", "Admin"

        [MaxLength(255)]
        public string? ProfilePicture { get; set; }
    }
}