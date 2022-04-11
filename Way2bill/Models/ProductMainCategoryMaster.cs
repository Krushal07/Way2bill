using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Way2bill.Models
{
    public class ProductMainCategoryMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Catid { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Catname { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string Catimage { get; set; }
    }
}
