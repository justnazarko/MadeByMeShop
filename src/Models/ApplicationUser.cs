﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MadeByMe.src.Models
{
    public class ApplicationUser : IdentityUser
    {
        //[Key]
        //[Column("user_id")]
        //public int UserId { get; set; }

        //[Required]
        //[MaxLength(50)]
        //public string Name { get; set; }

        //[Required]
        //[MaxLength(100)]
        //public string EmailAddress { get; set; }

        //[MaxLength(50)]
        //public string? MobileNumber { get; set; }

        //[Required]
        //[MaxLength(100)]
        //public string Password { get; set; }

        public bool IsBlocked { get; set; } = false;

        [MaxLength(255)]
        public string? ProfilePicture { get; set; }
    }
}
