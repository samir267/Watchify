using back_wachify.Business_Logic_Layer.Model;
using back_wachify.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace back_wachify.Model
{

    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [EmailAddress(ErrorMessage = "Username must be a valid email address")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
           ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, one special character, and have a minimum length of 8")]
        public byte[]? PasswordHash { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string? facebookId { get; set; }
        public string? googleId { get; set; }
        public string? provider { get; set; }
        public byte[]? PasswordSalt { get; set; }

        public string RefreshToken { get; set; } = string.Empty;

        public DateTime TokenCreated { get; set; }

        public DateTime TokenExpires { get; set; }
        public bool IsEmailConfirmed { get; set; }
        [MaxLength(5)]
        public string ConfirmationCode { get; set; }

        public int secretCode { get; set; }

        public Role Role { get; set; }

        public Etat Etat { get; set; }




    }
}
