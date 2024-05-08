using back_wachify.Business_Logic_Layer.Model;

namespace back_wachify.Business_Logic_Layer.Interfaces
{
    public interface IPackRepo
    {
        Task<Pack> FindById(string id);

    }
}
