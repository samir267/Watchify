using back_wachify.Business_Logic_Layer.Dto;
using back_wachify.Business_Logic_Layer.Model;
using back_wachify.Data.Model;

namespace back_wachify.Business_Logic_Layer.Interfaces
{
    public interface IAbonnementService
    {
        Task<(Abonnement abn, String Msg)> CreateAbonnement(AbonnementDto abnDto);
    }
}
