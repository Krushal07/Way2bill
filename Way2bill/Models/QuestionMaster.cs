using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Way2bill.Models
{
    public class QuestionMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Qid { get; set; }


        [Column(TypeName = "varchar(100)")]
        public string Questiontext { get; set; }
    }
}
