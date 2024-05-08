using back_wachify.Business_Logic_Layer.Interfaces;
using back_wachify.Business_Logic_Layer.Model;
using back_wachify.Data;
using Microsoft.EntityFrameworkCore;

namespace back_wachify.Data_Layer.Repositroy
{
    public class PackRepo :IPackRepo
    {

        private readonly ApplicationDbContext _dbContext;

        public PackRepo(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

        }
        public async Task<Pack> FindById(string id)
        {
            return await _dbContext.Pack.FirstOrDefaultAsync(u => u.Id.ToString() == id);

        }
    }
}
