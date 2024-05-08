using back_wachify.Business_Logic_Layer.Dto;
using back_wachify.Business_Logic_Layer.Interfaces;
using back_wachify.Business_Logic_Layer.Model;
using back_wachify.Data;
using back_wachify.Data.Model;
using back_wachify.Data_Layer.Repositroy;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace back_wachify.Business_Logic_Layer.Services
{
    public class AbonnementService : IAbonnementService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserRepository userRepo;
        private readonly IPackRepo packRepo;
        private readonly IAbonnementRepo AbnRepo;





        public AbonnementService(ApplicationDbContext context, IUserRepository userRepo, IPackRepo packRepo, IAbonnementRepo AbnRepo)
        {
            _context = context;
            this.userRepo = userRepo;
            this.packRepo = packRepo;
            this.AbnRepo = AbnRepo;
        }

        public async Task<(Abonnement abn,String Msg)> CreateAbonnement(AbonnementDto abnDto)
        {
            

            // Vérifier si l'utilisateur existe
            if (abnDto.UserId == null)
{
                return (abn: null, Msg: "uuuuuuuuuuuu"); // Utilisateur non trouvé
            }
            var user = await _context.User.FirstOrDefaultAsync(u => u.Id.ToString() == abnDto.UserId);
            if (user == null )
            {

                return (abn:null,Msg:"User Not Found"); // Utilisateur non trouvé
            }

            // Vérifier si le pack existe
            var pack = await packRepo.FindById(abnDto.PackId);
            if (pack == null )
            {

                return (abn: null, Msg:"Pack Not Found"); // Pack non trouvé
            }

            // Créer un nouvel abonnement avec le pack choisi
            var abonnement = new Abonnement
            {
                User = user,
                Pack = pack ,// Utiliser la propriété de navigation Pack
                Name= abnDto.Name,
                DateAbonnement= abnDto.DateAbonnement,
            };

            // Ajouter l'abonnement à la base de données
           AbnRepo.add(abonnement);
            return (abn: abonnement, Msg: "Abonnement ajouté avec succeé"); // Pack non trouvé
        }


    }
}
