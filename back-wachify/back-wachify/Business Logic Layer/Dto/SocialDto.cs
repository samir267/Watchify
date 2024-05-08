using System.ComponentModel.DataAnnotations;

namespace back_wachify.Business_Logic_Layer.Dto
{
    public class SocialAuthDto
    {
        [Required(ErrorMessage = "Username is required")]
        [EmailAddress(ErrorMessage = "Username must be a valid email address")]
        public string Username { get; set; } = string.Empty;
        [Required(ErrorMessage = "must be loggedIn")]
        public string socialId { get; set; }
    }
}
