using back_wachify.Business_Logic_Layer.Model;
using back_wachify.Data.Model;
using System.ComponentModel.DataAnnotations;

namespace back_wachify.Business_Logic_Layer.Dto
{
    public class SocialregisterDto
    {
        private const int MinimumPasswordLength = 8;

        [Required(ErrorMessage = "Username is required")]
        [EmailAddress(ErrorMessage = "Username must be a valid email address")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Role is required")]
        public Role Role { get; set; }
        [Required(ErrorMessage = "Firstname is required")]

        public string Name { get; set; }
        [Required(ErrorMessage = "phoneNumber is required")]

        public string PhoneNumber { get; set; }
        public string? facebookId { get; set; }
        public string? googleId { get; set; }
        public string? provider { get; set; }
        public Etat Etat { get; set; } = 0;

    }
}
