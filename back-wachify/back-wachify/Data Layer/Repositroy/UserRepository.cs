using back_wachify.Business_Logic_Layer.Dto;
using back_wachify.Business_Logic_Layer.Model;
using back_wachify.Data;
using back_wachify.Data.Model;
using back_wachify.Dto;
using back_wachify.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace back_wachify.Data_Layer.Repositroy
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

        }

        public async Task<User> add(User user)
        {
            try
            {
                // Add the user entity to the DbContext
                _dbContext.User.Add(user);

                // Save changes to the database
                await _dbContext.SaveChangesAsync();

                // Return the user entity as part of the response
                return user;

            }
            catch
            {
                return null;
            }


        }

        public async Task<User> FindByUsername(string username)
        {
            return await _dbContext.User.FirstOrDefaultAsync(u => u.Username == username);

        }
        public async Task<User> FindById(string id)
        {
            return await _dbContext.User.FirstOrDefaultAsync(u => u.Id.ToString() == id);

        }


        public async Task<User> GetUserByRefreshTokenAsync(string refreshToken)
        {
            return await _dbContext.User.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
        }



        public async Task<List<User>> GetAllAsync()
        {
            return await _dbContext.User.ToListAsync();
        }

        public async Task<List<User>> GetByRoleAsync(Role role)
        {
            return await _dbContext.User.Where(u => u.Role == role).ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _dbContext.User.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<bool> DeactivateAsync(int id)
        {
            var user = await _dbContext.User.FirstOrDefaultAsync(u => u.Id == id);

            if (user != null)
            {
                if (user.Etat == Etat.Active) {
                    user.Etat = Etat.Inactive; }
                else { user.Etat = Etat.Active; }

                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }
        public async Task<bool> ConfirmedEmail(string username)
        {
            var user = await _dbContext.User.FirstOrDefaultAsync(u => u.Username == username);

            if (user != null)
            {
                user.IsEmailConfirmed = true;
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }
        public async Task<User> UpdateAsync(int id, UserDto userDto)
        {
            var user = await _dbContext.User.FirstOrDefaultAsync(u => u.Id == id);

            if (user != null)
            {
                user.Username = userDto.Username;
                // Update other properties as needed
                await _dbContext.SaveChangesAsync();
            }

            return user;
        }



        public async Task<bool> DeleteUser(int id)
        {
            var user = await _dbContext.User.FindAsync(id);

            if (user != null)
            {
                _dbContext.User.Remove(user);
                await _dbContext.SaveChangesAsync();
                return true; // Suppression réussie
            }

            return false; // Utilisateur non trouvé ou déjà supprimé
        }




        public async Task<bool> AffecterCode(string username, int code)
        {
            var utilisateur = await _dbContext.User.FirstOrDefaultAsync(u => u.Username == username);
            if (utilisateur != null)
            {
                utilisateur.secretCode = code;
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }








        public async Task<int?> GetSecretCodeByEmailAsync(string email)
        {
            try
            {
                var utilisateur = await _dbContext.User.FirstOrDefaultAsync(u => u.Username == email);

                if (utilisateur != null)
                {
                    return utilisateur.secretCode;
                }
                else
                {
                    return null; // L'utilisateur n'existe pas
                }
            }
            catch (Exception ex)
            {
                // Log the exception and handle it appropriately
                Console.WriteLine($"Exception: {ex}");

                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                    Console.WriteLine($"Inner Exception: {ex}");
                }

                throw new Exception("Erreur lors de la récupération du secretCode par email.", ex);
            }
        }





    }
}
