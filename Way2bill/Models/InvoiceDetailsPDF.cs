namespace Way2bill.Models
{
    public class InvoiceDetailsPDF
    {
        //it is not implemented in database, it will be used for report only
        public string BranchName { get; set; } = "";

        public string BranchAddress { get; set; } = "";

        public string BranchPhone { get; set; } = "";

        public string BranchEmail { get; set; } = "";

        public string InvoiceNo { get; set; } = "";

        public string InvoiceDate { get; set; } = "";

        public string CustomerName { get; set; } = "";

        //public string Status { get; set; }

        public string Gst { get; set; } = "";

        public string GrandTotal { get; set; } = "";
    }
}
