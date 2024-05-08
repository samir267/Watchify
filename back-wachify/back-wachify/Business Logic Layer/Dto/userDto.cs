using back_wachify.Business_Logic_Layer.Model;
using back_wachify.Data.Model;
using System.ComponentModel.DataAnnotations;

namespace back_wachify.Dto
{
    public class UserDto
    {
        private const int MinimumPasswordLength = 8;

        [Required(ErrorMessage = "Username is required")]
        [EmailAddress(ErrorMessage = "Username must be a valid email address")]
        public string Username { get; set; } = string.Empty;
        //contains at least one uppercase letter,one lowercase
        //letter, one digit, one special character,
        //and has a minimum length of 8 characters

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
            ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, one special character, and have a minimum length of 8")]
        public string Password { get; set; } = string.Empty;
        [Required(ErrorMessage = "Role is required")]
        public Role Role { get; set; }

        [Required(ErrorMessage = "Firstname is required")]

        public string Name { get; set; }
        [Required(ErrorMessage = "phoneNumber is required")]

        public string PhoneNumber { get; set; }
  
        public Etat Etat { get; set; }


    }
}
