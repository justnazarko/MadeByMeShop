using System.ComponentModel.DataAnnotations;

namespace MadeByMe.src.DTOs
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

}
