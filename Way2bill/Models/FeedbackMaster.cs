using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Way2bill.Models
{
    public class FeedbackMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Fid { get; set; }

        public DateTime Fdate   { get; set; }
        
        public int Companyid    { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string Fdetails { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string Experiencerate { get; set; }

    }
}
