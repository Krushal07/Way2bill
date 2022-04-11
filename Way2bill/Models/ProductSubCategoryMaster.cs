using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Way2bill.Models
{
    public class ProductSubCategoryMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Scid { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Scname { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string Scimage { get; set; }

        public int Scpriceperunit { get; set; }

        public int Catid    { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string Scdesc { get; set; }

    }
}
