using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Way2bill.Models
{
    public class CustomerMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Customerid { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string Customername { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string Customeraddress { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string Customerphone { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string Customeremail { get; set; }

        public int Branchid { get; set; }
    }
}
