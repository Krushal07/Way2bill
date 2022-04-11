using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Way2bill.Models
{
    public class InvoiceMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InvoiceNo { get; set; }

        [Column(TypeName = "varchar(25)")]
        public string InvoiceDate { get; set; }

        public int Branchid { get; set; }

        public int Customerid { get; set; }

        [Column(TypeName = "varchar(25)")]
        public string FinalTotal { get; set; }
    }
}
