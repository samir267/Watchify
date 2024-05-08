using back_wachify.Business_Logic_Layer.Model;
using back_wachify.Model;
using System.ComponentModel.DataAnnotations;

namespace back_wachify.Data.Model
{
    public class Film
    {
        [Key]
        public int Id { get; set; }
        public string Titre { get; set; }
        public string Description { get; set; }

		public string Duree { get; set; }

		public string Genre { get; set; }

		public DateTime DateDeSortie { get; set; }

		public bool IsFree { get; set; }

		public int UserId { get; set; }

		public string VideoFilePath { get; set; } // Utilisé pour stocker le chemin du fichier vidéo sur le serveur

		public string  LogoFilePath { get; set; } // Utilisé pour stocker le chemin du fichier logo sur le serveur

		public virtual ICollection<Commantire> Commentaires { get; set; }





	}
}
