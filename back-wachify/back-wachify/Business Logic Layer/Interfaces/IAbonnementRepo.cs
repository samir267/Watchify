using back_wachify.Business_Logic_Layer.Dto;
using back_wachify.Business_Logic_Layer.Model;
using back_wachify.Model;

namespace back_wachify.Business_Logic_Layer.Interfaces
{
    public interface IAbonnementRepo
    {
        Task<Abonnement> FindByPack(Pack pack);
        Task<Abonnement> FindByUser(User user);
        Task<Abonnement> add(Abonnement Ab);

    }
}
