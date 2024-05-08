using back_wachify.Business_Logic_Layer.Dto;
using back_wachify.Data.Model;
using back_wachify.Dto;
using back_wachify.Model;

namespace back_wachify.Services.UserService
{
    public interface IUserService
    {
        string GetMyName();
        Task<List<User>> GetAllUsersAsync();
        Task<List<User>> GetUsersByRoleAsync(Role role);
        Task<User> GetUserByIdAsync(int id);
        Task<User> UpdateUserAsync(int id, UserDto userDto);
        Task<bool> DeactivateUserAsync(int id);
        Task SendEmailAsync(emailDto emailDto, string recipient);
        Task<bool> VerifierCodeAsync(int codeVerification, string email);
        Task<bool> DeleteUser(int id); 

    }
}

