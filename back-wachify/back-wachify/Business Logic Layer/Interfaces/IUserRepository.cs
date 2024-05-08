using back_wachify.Business_Logic_Layer.Dto;
using back_wachify.Data.Model;
using back_wachify.Dto;
using back_wachify.Model;
using Microsoft.AspNetCore.Mvc;

namespace back_wachify.Data_Layer.Repositroy
{
    public interface IUserRepository
    {
        Task<User> add(User userDto);
        Task<User> FindByUsername(string username);
        Task<User> GetUserByRefreshTokenAsync(string refreshToken);
        Task<User> FindById(string id);


        Task<List<User>> GetAllAsync();
        Task<List<User>> GetByRoleAsync(Role role);
        Task<User> GetByIdAsync(int id);
        Task<bool> DeactivateAsync(int id);
        Task<User> UpdateAsync(int id, UserDto userDto);
       
        Task<bool> AffecterCode(string username,int code);

        Task<int?> GetSecretCodeByEmailAsync(string email);
        Task<bool> DeleteUser(int id); 


        Task<bool> ConfirmedEmail(string username);



    }
}
