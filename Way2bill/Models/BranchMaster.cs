using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Way2bill.Models
{
    public class BranchMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Branchid { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Bname { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string Baddress { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string Bphone { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string Bcity { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string Barea { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string Bemail { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string Bpassword { get; set; }

        public int Bqid { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Banswer { get; set; }

        public int Companyid { get; set; }
    }
}
