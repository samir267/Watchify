using back_wachify.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace back_wachify.Business_Logic_Layer.Model
{
    public class Abonnement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateAbonnement { get; set; }

        // Propriété pour stocker l'ID de l'utilisateur lié à cet abonnement
        public User User { get; set; }
        public Pack Pack { get; set; }



    }
}
