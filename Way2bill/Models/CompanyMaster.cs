using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Way2bill.Models
{
    public class CompanyMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Companyid { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Cname { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string Caddress { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string Cphone { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string Ccity { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string Cemail { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string Cpassword { get; set; }

        public int  Cqid { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Canswer { get; set; }

        public int Crollid { get; set; }
    }
}
