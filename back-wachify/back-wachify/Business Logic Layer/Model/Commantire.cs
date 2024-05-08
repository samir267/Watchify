using back_wachify.Data.Model;
using back_wachify.Model;
using MessagePack;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace back_wachify.Business_Logic_Layer.Model
{
	public class Commantire
	{

		[System.ComponentModel.DataAnnotations.Key]
		public int Id { get; set; }

		[Required]
		public string Contenu { get; set; }

		[ForeignKey("User")]
		public int UserId { get; set; }
		public virtual User User { get; set; }

		[ForeignKey("Film")]
		public int filmId { get; set; }
		public virtual Film Film { get; set; }
	}
}
