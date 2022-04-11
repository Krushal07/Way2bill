using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Way2bill.Models
{
    public class CartMaster
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Cartid { get; set; }

        public int Customerid { get; set; }

        public int Scid { get; set; }

        public int ScQty { get; set; }

    }
}
