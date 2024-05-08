using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace back_wachify.Business_Logic_Layer.Model
{
    public class Pack
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        public String Name { get; set; }
        public string Durations { get; set; }
    }
}
