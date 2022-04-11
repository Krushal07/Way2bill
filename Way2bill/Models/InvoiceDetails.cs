using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Way2bill.Models
{
    public class InvoiceDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InvoiceDetailNo { get; set; }

        public int InvoiceNo { get; set; }

        public int Productid    { get; set; }

        [Column(TypeName = "varchar(25)")]
        public string ProductQty { get; set; }


    }
}
