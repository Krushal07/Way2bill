namespace Way2bill.Models
{

    public class InvoiceProductDetailsPDF
    {
        public int Srno { get; set; } = 0;

        public string ProductName { get; set; } = "";

        public string ProductDesc { get; set; } = "";

        public int ProductQty { get; set; } = 0;

        public int ProductPrice { get; set; } = 0;

        public int ProductTotal { get; set; } = 0;

    }
}
