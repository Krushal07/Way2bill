using iTextSharp.text;
using iTextSharp.text.pdf;
using Way2bill.Models;


namespace Way2bill.Report
{
    public class InvoiceReport
    {
        private IWebHostEnvironment _henv;
        private readonly Way2BillDbContext _invoiceDb;

        public InvoiceReport(IWebHostEnvironment henv, Way2BillDbContext invoiceDB)
        {
            _henv = henv;
            _invoiceDb = invoiceDB;
        }

        #region Declaration
        int _maxColumn = 5; // 5 as we have 4 cols in InvoiceDetails Table
        Document _document; //include using iTextSharp.text;
        Font _fontstyle;
        PdfPTable _pdfTable = new PdfPTable(5); //6 is a number of column
        PdfPCell _pdfCell;
        MemoryStream _memoryStream = new MemoryStream();
        double _grandTotal = 0.0;

        // List<InvoiceDetailsPDF> _invoiceDetailsPDF = new List<InvoiceDetailsPDF>();
        InvoiceDetailsPDF _invoiceDetailsPDF = new InvoiceDetailsPDF();
        List<InvoiceDetails> _invoicedetails = new List<InvoiceDetails>();
        List<ProductSubCategoryMaster> _productList = new List<ProductSubCategoryMaster>();
        #endregion Declaration

        public byte[] Report(InvoiceDetailsPDF invoiceDetailsPDF, List<InvoiceDetails> invoicedetails, List<ProductSubCategoryMaster> productList)
        {
            _invoiceDetailsPDF = invoiceDetailsPDF;

            _document = new Document();
            _document.SetPageSize(PageSize.A4);
            _document.SetMargins(5f, 5f, 20f, 5f);

            _pdfTable.WidthPercentage = 100;
            _pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;

            _fontstyle = FontFactory.GetFont("Tahoma", 8f, 1);

            PdfWriter docWrite = PdfWriter.GetInstance(_document, _memoryStream);

            _document.Open();

            float[] sizes = new float[_maxColumn];
            for (var i = 0; i < _maxColumn; i++) //this loop gives width to table coulmns
            {
                if (i == 0) sizes[i] = 40;
                else sizes[i] = 100;
            }

            _pdfTable.SetWidths(sizes);

            this.ReportHeader();
            this.EmptyRow(2);
            this.ReportBody();
            this.ReportFooter();
            _pdfTable.HeaderRows = 2;
            _document.Add(_pdfTable);
            _document.Close();

            return _memoryStream.ToArray();
        }

        private void ReportHeader()
        {

            _pdfCell = new PdfPCell(this.AddLogo());
            _pdfCell.Colspan = 1;
            _pdfCell.Border = 0;
            _pdfTable.AddCell(_pdfCell);

            //to change pos of logo paste above four lines after following four lines

            _pdfCell = new PdfPCell(this.SetPageTitle());
            _pdfCell.Colspan = _maxColumn - 1;
            _pdfCell.Border = 0;
            _pdfTable.AddCell(_pdfCell);
            _pdfTable.CompleteRow();

            this.EmptyRow(7);

            _pdfCell = new PdfPCell(this.SetPageSubTitle());
            _pdfCell.Colspan = _maxColumn;
            _pdfCell.Border = 0;
            _pdfTable.AddCell(_pdfCell);
            _pdfTable.CompleteRow();
        }

        private PdfPTable AddLogo()
        {
            int maxColumn = 5;
            PdfPTable pdfPTable = new PdfPTable(maxColumn);

            string path = _henv.WebRootPath + "/images";

            string imgCombine = Path.Combine(path, "logo.jpg");
            Image img = Image.GetInstance(imgCombine);


            _pdfCell = new PdfPCell(img);
            _pdfCell.Colspan = maxColumn;
            _pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            _pdfCell.Border = 0;
            _pdfCell.ExtraParagraphSpace = 0;
            pdfPTable.AddCell(_pdfCell);

            pdfPTable.CompleteRow();

            return pdfPTable;
        }

        private PdfPTable SetPageTitle()
        {
            int maxColumn = 5;
            PdfPTable pdfPTable = new PdfPTable(maxColumn);

            _fontstyle = FontFactory.GetFont("Tahoma", 18f, 1);
            _pdfCell = new PdfPCell(new Phrase("Invoice", _fontstyle));
            _pdfCell.Colspan = maxColumn;
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.Border = 0;
            _pdfCell.ExtraParagraphSpace = 0;
            pdfPTable.AddCell(_pdfCell);
            pdfPTable.CompleteRow();

            _fontstyle = FontFactory.GetFont("Tahoma", 14f, 1);
            _pdfCell = new PdfPCell(new Phrase(_invoiceDetailsPDF.BranchName, _fontstyle));
            _pdfCell.Colspan = maxColumn;
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.Border = 0;
            _pdfCell.ExtraParagraphSpace = 0;
            pdfPTable.AddCell(_pdfCell);
            pdfPTable.CompleteRow();

            _fontstyle = FontFactory.GetFont("Tahoma", 10f, 1);
            _pdfCell = new PdfPCell(new Phrase(_invoiceDetailsPDF.BranchAddress, _fontstyle));
            _pdfCell.Colspan = maxColumn;
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.Border = 0;
            _pdfCell.ExtraParagraphSpace = 0;
            pdfPTable.AddCell(_pdfCell);
            pdfPTable.CompleteRow();


            _fontstyle = FontFactory.GetFont("Tahoma", 10f, 1);
            _pdfCell = new PdfPCell(new Phrase(_invoiceDetailsPDF.BranchPhone, _fontstyle));
            _pdfCell.Colspan = maxColumn;
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.Border = 0;
            _pdfCell.ExtraParagraphSpace = 0;
            pdfPTable.AddCell(_pdfCell);
            pdfPTable.CompleteRow();

            _fontstyle = FontFactory.GetFont("Tahoma", 10f, 1);
            _pdfCell = new PdfPCell(new Phrase(_invoiceDetailsPDF.BranchEmail, _fontstyle));
            _pdfCell.Colspan = maxColumn;
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.Border = 0;
            _pdfCell.ExtraParagraphSpace = 0;
            pdfPTable.AddCell(_pdfCell);
            pdfPTable.CompleteRow();



            return pdfPTable;
        }

        private PdfPTable SetPageSubTitle()
        {
            int maxColumn = 5;
            PdfPTable pdfPTable = new PdfPTable(maxColumn);

            var FontColour = new BaseColor(0, 0, 255);
            _fontstyle = FontFactory.GetFont("Tahoma", 14f, FontColour);
            _pdfCell = new PdfPCell(new Phrase("Invoice No: " + _invoiceDetailsPDF.InvoiceNo +
                                                "    Date: " + _invoiceDetailsPDF.InvoiceDate +
                                                "    Customer Name: " + _invoiceDetailsPDF.CustomerName
                                                /*"    Status: " + _invoiceDetailsPDF.Status,*/, _fontstyle));
            _pdfCell.Colspan = maxColumn;
            _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfCell.Border = 0;
            _pdfCell.ExtraParagraphSpace = 0;
            pdfPTable.AddCell(_pdfCell);
            pdfPTable.CompleteRow();

            //_fontstyle = FontFactory.GetFont("Tahoma", 10f, FontColour);
            //_pdfCell = new PdfPCell(new Phrase(_invoiceDetailsPDF.InvoiceDate, _fontstyle));
            //_pdfCell.Colspan = maxColumn;
            //// _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            //_pdfCell.Border = 0;
            //_pdfCell.ExtraParagraphSpace = 0;
            //pdfPTable.AddCell(_pdfCell);
            //// pdfPTable.CompleteRow();


            //_fontstyle = FontFactory.GetFont("Tahoma", 10f, FontColour);
            //_pdfCell = new PdfPCell(new Phrase(_invoiceDetailsPDF.CustomerName, _fontstyle));
            //_pdfCell.Colspan = maxColumn;
            ////_pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            //_pdfCell.Border = 0;
            //_pdfCell.ExtraParagraphSpace = 0;
            //pdfPTable.AddCell(_pdfCell);
            ////pdfPTable.CompleteRow();



            //_fontstyle = FontFactory.GetFont("Tahoma", 10f, FontColour);
            //_pdfCell = new PdfPCell(new Phrase(_invoiceDetailsPDF.Status, _fontstyle));
            //_pdfCell.Colspan = maxColumn;
            //// _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            //_pdfCell.Border = 0;
            //_pdfCell.ExtraParagraphSpace = 0;
            //pdfPTable.AddCell(_pdfCell);
            ////pdfPTable.CompleteRow();


            return pdfPTable;
        }

        private void EmptyRow(int nCount)
        {
            for (int i = 1; i <= nCount; i++)
            {
                _pdfCell = new PdfPCell(new Phrase("", _fontstyle));
                _pdfCell.Colspan = _maxColumn;
                _pdfCell.Border = 0;
                _pdfCell.ExtraParagraphSpace = 10;
                _pdfTable.AddCell(_pdfCell);
                _pdfTable.CompleteRow();
            }
        }

        private void ReportBody()
        {

            var FontColour = new BaseColor(255, 255, 255);
            var fontStyleBold = FontFactory.GetFont("Tahoma", 9f, FontColour);



            #region Detail Table Header

            _pdfCell = new PdfPCell(new Phrase("Sr.No.", fontStyleBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.Gray;
            _pdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("Product Name", fontStyleBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.Gray;
            _pdfTable.AddCell(_pdfCell);

            //_pdfCell = new PdfPCell(new Phrase("Description", fontStyleBold));
            //_pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            //_pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            //_pdfCell.BackgroundColor = BaseColor.Gray;
            //_pdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("Quantity", fontStyleBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.Gray;
            _pdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("Price", fontStyleBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.Gray;
            _pdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("Total", fontStyleBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.Gray;
            _pdfTable.AddCell(_pdfCell);

            _pdfTable.CompleteRow();
            #endregion

            #region Detail table body

            var productDetailsList = _invoiceDb.InvoiceDetailsMasters.Join(_invoiceDb.ProductSubCategoryMasters, i => i.Productid,
                p => p.Scid, (i, p) => new { i.InvoiceNo,/* i.ProductDesc,*/ i.ProductQty, p.Scname, p.Scpriceperunit }).
                Where(i => i.InvoiceNo == Convert.ToInt32(_invoiceDetailsPDF.InvoiceNo)).ToList();
            //ewasteDb.PersonMasters.Join(ewasteDb.OrderMasters, p => p.Pid,
            //s => s.Pid, (p, s) => new { p.Pname, s.Ordgrandtotal, s.Orddate }).Where(i => i.Ordgrandtotal > 3000 && i.Orddate >= new DateTime(2020, 1, 1)
            //&& i.Orddate <= new DateTime(2021, 1, 1)).ToList();

            int i = 1;
            foreach (var product in productDetailsList)
            {

                _fontstyle = FontFactory.GetFont("Tahoma", 9f, 0);
                _pdfCell = new PdfPCell(new Phrase(i.ToString()));
                _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfCell.BackgroundColor = BaseColor.Yellow;
                _pdfTable.AddCell(_pdfCell);
                i = i + 1;


                _fontstyle = FontFactory.GetFont("Tahoma", 9f, 0);
                //_pdfCell = new PdfPCell(new Phrase(invoiceDetail.InvoiceDetailNo.ToString(), fontStyleBold));
                _pdfCell = new PdfPCell(new Phrase(product.Scname));
                _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfCell.BackgroundColor = BaseColor.White;
                _pdfTable.AddCell(_pdfCell);

                //_pdfCell = new PdfPCell(new Phrase(product.ProductDesc));
                //_pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                //_pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                //_pdfCell.BackgroundColor = BaseColor.White;
                //_pdfTable.AddCell(_pdfCell);

                _pdfCell = new PdfPCell(new Phrase(product.ProductQty));
                _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfCell.BackgroundColor = BaseColor.White;
                _pdfTable.AddCell(_pdfCell);

                _pdfCell = new PdfPCell(new Phrase(product.Scpriceperunit.ToString()));
                _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfCell.BackgroundColor = BaseColor.White;
                _pdfTable.AddCell(_pdfCell);

                int total = 0;
                total = Convert.ToInt32(product.ProductQty) * product.Scpriceperunit;
                _grandTotal = _grandTotal + total;
                _pdfCell = new PdfPCell(new Phrase(total.ToString()));
                _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfCell.BackgroundColor = BaseColor.White;
                _pdfTable.AddCell(_pdfCell);
                _pdfTable.CompleteRow();
            }
            #endregion  
        }

        private void ReportFooter()
        {
            /////////GST ROW STARTS
            _fontstyle = FontFactory.GetFont("Tahoma", 9f, 0);
            _pdfCell = new PdfPCell(new Phrase());
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.White;
            _pdfCell.Border = 0;
            _pdfTable.AddCell(_pdfCell);



            _fontstyle = FontFactory.GetFont("Tahoma", 9f, 0);
            _pdfCell = new PdfPCell(new Phrase());
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.White;
            _pdfCell.Border = 0;
            _pdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase());
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.White;
            _pdfCell.Border = 0;
            _pdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase());
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.White;
            _pdfCell.Border = 0;
            _pdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase());
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.White;
            _pdfCell.Border = 0;
            _pdfTable.AddCell(_pdfCell);


            _pdfCell = new PdfPCell(new Phrase("GST : 18%"));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.LightGray;
            _pdfCell.Border = 1;
            _pdfTable.AddCell(_pdfCell);

            _pdfTable.CompleteRow();
            /////////GST ROW ENDS
            ///
            /////////GranTotal ROW Stars
            _fontstyle = FontFactory.GetFont("Tahoma", 9f, 0);
            _pdfCell = new PdfPCell(new Phrase());
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.White;
            _pdfCell.Border = 0;
            _pdfTable.AddCell(_pdfCell);



            _fontstyle = FontFactory.GetFont("Tahoma", 9f, 0);
            _pdfCell = new PdfPCell(new Phrase());
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.White;
            _pdfCell.Border = 0;
            _pdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase());
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.White;
            _pdfCell.Border = 0;
            _pdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase());
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.White;
            _pdfCell.Border = 0;
            _pdfTable.AddCell(_pdfCell);

            //_pdfCell = new PdfPCell(new Phrase());
            //_pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            //_pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            //_pdfCell.BackgroundColor = BaseColor.White;
            //_pdfCell.Border = 0;
            //_pdfTable.AddCell(_pdfCell);

            _grandTotal = _grandTotal + (_grandTotal * .18);
            _pdfCell = new PdfPCell(new Phrase("Grand Total : " + _grandTotal.ToString()));
            _pdfCell.Colspan = 2;
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.Yellow;
            _pdfCell.Border = 1;
            _pdfTable.AddCell(_pdfCell);

            _pdfTable.CompleteRow();
            /////////GranTotal ROW ENDS
            ///
            ///Final line starts

            _pdfCell = new PdfPCell(new Phrase("Thanks for Shopping...."));
            _pdfCell.Colspan = 6;
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.LightGray;
            _pdfCell.Border = 0;
            _pdfTable.AddCell(_pdfCell);

            _pdfTable.CompleteRow();
            /////////Final line ENDS
        }
    }
}
