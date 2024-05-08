using back_wachify.Business_Logic_Layer.Dto;
using back_wachify.Business_Logic_Layer.Interfaces;
using back_wachify.Business_Logic_Layer.Model;
using back_wachify.Data;
using back_wachify.Model;
using Microsoft.EntityFrameworkCore;

namespace back_wachify.Data_Layer.Repositroy
{
    public class AbonnementRepo : IAbonnementRepo
    {
        private readonly ApplicationDbContext _dbContext;

        public AbonnementRepo(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

        }
        public async Task<Abonnement> add(Abonnement Ab)
        {
            try
            {
                // Add the user entity to the DbContext
                _dbContext.Abonnements.Add(Ab);

                // Save changes to the database
                await _dbContext.SaveChangesAsync();

                // Return the user entity as part of the response
                return Ab;

            }
            catch
            {
                return null;
            }


        }

        public async Task<Abonnement> FindByUser(User user)
        {
            return await _dbContext.Abonnements.FirstOrDefaultAsync(u => u.User == user);

        }

        public async Task<Abonnement> FindByPack(Pack pack)
        {
            return await _dbContext.Abonnements.FirstOrDefaultAsync(u => u.Pack == pack);

        }
    }
}
