using back_wachify.Business_Logic_Layer.Model;

namespace back_wachify.Business_Logic_Layer.Interfaces
{
    public interface IPackService
    {
        Task<IEnumerable<Pack>> GetAllPacks();
        Task<Pack> GetPackById(int id);
        Task<int> CreatePack(Pack pack);
        Task<bool> UpdatePack(int id, Pack pack);
        Task<bool> DeletePack(int id);
    }
}
