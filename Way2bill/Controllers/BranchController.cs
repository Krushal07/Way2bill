using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Way2bill.Models;
using Way2bill.Report;

namespace Way2bill.Controllers
{
    public class BranchController : Controller
    {
        #region Default
        private readonly Way2BillDbContext bkDb;
        private readonly IWebHostEnvironment henv;

        public BranchController(Way2BillDbContext bkDB, IWebHostEnvironment henv)
        {
            bkDb = bkDB;
            this.henv = henv;
        }
        #endregion Default
        public IActionResult BranchHome(int Branchid)
        {
            HttpContext.Session.SetString("Branchid", Convert.ToString(Branchid));
            TempData["BranchId"] = Convert.ToInt32(HttpContext.Session.GetString("Branchid"));
            return View();
        }
        public IActionResult BranchLogin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult BranchLogin(IFormCollection frm)
        {
            var usremail = Convert.ToString(frm["Email"]);
            var usrpasswd = Convert.ToString(frm["Password"]);
            var rdFound = bkDb.BranchMasters.Where(usr => usr.Bemail == usremail && usr.Bpassword == usrpasswd).FirstOrDefault();
            if (rdFound == null)
            {
                TempData["ErrMsg"] = "Invalid EmailId or Password";
            }
            else if (rdFound.Bemail == "admin@gmail.com" && rdFound.Bpassword == "aa")
            {
                return RedirectToAction("AdminHome", "Admin");
            }
            else if (rdFound != null)
            {
                return RedirectToAction("BranchHome", "Branch",new {rdFound.Branchid});
            }

            return View();
        }

        public IActionResult BranchLogout()
        {
            return RedirectToAction("Home","Login");
        }

        public IActionResult BranchCheckDetail()
        {
            return View();
        }

        [HttpPost]
        public IActionResult BranchCheckDetail(IFormCollection frm)
        {
            
            var cphone = Convert.ToString(frm["Customerphone"]);
            var rdfound = bkDb.CustomerMasters.Where(q => q.Customerphone == cphone).FirstOrDefault();
            if (rdfound != null)
            {
                //var customerdetail = bkDb.CustomerMasters.Where(q=>q.Customerphone == cphone).FirstOrDefault();
                TempData["name"] = rdfound.Customername;
                TempData["email"] = rdfound.Customeremail;
                TempData["phone"] = rdfound.Customerphone;
                TempData["address"] = rdfound.Customeraddress;
                TempData["Customerid"] = rdfound.Customerid;
            }
            else
            {
                return RedirectToAction("BranchCustomerSignup");
            }
            return View();
        }

        [HttpGet]
         public IActionResult BranchCustomerSignup()
        {
            TempData["BranchId"] = Convert.ToInt32(HttpContext.Session.GetString("Branchid"));
            return View();
        }

        [HttpPost]
        public IActionResult BranchCustomerSignup(CustomerMaster customermaster)
        {
            bkDb.CustomerMasters.Add(customermaster);
            bkDb.SaveChanges();
            return RedirectToAction("BranchViewProductCart", new {customermaster.Customerid});
        }

        [HttpGet]
        public IActionResult BranchViewProductCart(int Customerid)
        {
            TempData["Customerid"] = Customerid;
            HttpContext.Session.SetString("Customerid", Convert.ToString(Customerid));
            var productdetails = bkDb.ProductSubCategoryMasters.ToList();
            var cartdetails = bkDb.CartMasters.Where(q=>q.Customerid==Customerid).ToList();
            ViewBag.productdetails = productdetails;
            return View(cartdetails);
        }

        [HttpPost]
        public IActionResult BranchViewProductCart(CartMaster cartmaster)
        {
            var customerid = Convert.ToInt32(HttpContext.Session.GetString("Customerid"));
            cartmaster.Customerid  = customerid;
            bkDb.CartMasters.Add(cartmaster);
            bkDb.SaveChanges();            
            var productdetails = bkDb.ProductSubCategoryMasters.ToList();
            var cartdetails = bkDb.CartMasters.Where(q => q.Customerid == Convert.ToInt32(customerid)).ToList();
            ViewBag.productdetails = productdetails;
            return View(cartdetails);
        }

        [HttpGet]
        public IActionResult BranchEditProduct(int Cartid)
        {
            var rdfound = bkDb.CartMasters.Where(q => q.Cartid == Cartid).FirstOrDefault();

            return View(rdfound);
        }

        [HttpPost]
        public IActionResult BranchEditProduct(CartMaster cartmaster)
        {
            var rdfound = bkDb.CartMasters.Where(q => q.Cartid == cartmaster.Cartid).FirstOrDefault();
            if (rdfound != null)
            {
                rdfound.ScQty = cartmaster.ScQty;
            }
            bkDb.Entry(rdfound).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            bkDb.SaveChanges();
            return RedirectToAction("BranchViewProductCart", new {cartmaster.Customerid});
         
        }

       public IActionResult BranchDeleteProduct(int Cartid,int Customerid)
        {
            var customerid = Customerid;
            var rdfound = bkDb.CartMasters.Where(q => q.Cartid == Cartid).FirstOrDefault();
            bkDb.CartMasters.Remove(rdfound);
            bkDb.SaveChanges();
            return RedirectToAction("BranchViewProductCart",new {customerid});
        }


        #region PrintPDF

        public void AddInvoiceMaster(int branchid, int customerid, double grandTotal)
        {
            InvoiceMaster invoicemaster = new InvoiceMaster();
            invoicemaster.InvoiceDate = DateTime.Now.ToString();
            invoicemaster.Branchid = branchid;
            invoicemaster.Customerid = customerid;
            invoicemaster.FinalTotal = grandTotal.ToString();            
            bkDb.InvoiceMasters.Add(invoicemaster);
            bkDb.SaveChanges();
        }

        public void AddInvoiceDetails(int invoiceNum, int customerid)
        {
            InvoiceDetails invoicedetails = new InvoiceDetails();
            CartMaster cm = new CartMaster();
            var scList = bkDb.CartMasters.Where(q => q.Customerid == customerid).ToList();


            //transfering data from one table to another
            foreach (var sc in scList)
            {
                invoicedetails.InvoiceNo = invoiceNum;
                invoicedetails.Productid = sc.Scid;
                invoicedetails.ProductQty = sc.ScQty.ToString();
                bkDb.InvoiceDetailsMasters.Add(invoicedetails);
                bkDb.SaveChanges();
                bkDb.Entry(invoicedetails).State = EntityState.Detached;
                invoicedetails.InvoiceDetailNo = 0;
            }

        }

        public void DeleteCurrentCart(int customerid)
        {
            var removecurrentCart = bkDb.CartMasters.Where(cm => cm.Customerid == customerid).ToList();
            bkDb.CartMasters.RemoveRange(removecurrentCart); //to remove more than one record...            
            bkDb.SaveChanges();
        }

        public IActionResult PrintInvoice()
        {

            int branchid = Convert.ToInt32(HttpContext.Session.GetString("Branchid"));
            int customerid = Convert.ToInt32(HttpContext.Session.GetString("Customerid"));

            //getting grandtotal - starts
            var grandList = bkDb.CartMasters.Where(cm => cm.Customerid == customerid).ToList();
            var totalprice = 0;
            foreach (var grand in grandList)
            {
                var screc = bkDb.ProductSubCategoryMasters.Where(sc => sc.Scid == grand.Scid).FirstOrDefault();
                if (screc != null)
                {
                    var total = Convert.ToInt32(grand.ScQty) * Convert.ToInt32(screc.Scpriceperunit);
                    totalprice = totalprice + total;
                }
            }
            var grandTotal = totalprice + totalprice * .18;
            //getting grandtotal - ends

            BranchController bc = new BranchController(bkDb, henv);

            //Adding record in invoicemaster table - starts            
            bc.AddInvoiceMaster(branchid, customerid, grandTotal);
            //Adding record in invoicemaster table - ends

            //getting latest invoice number
            int invoiceNum;
            InvoiceMaster maxInvoiceId = new InvoiceMaster();
            if (bkDb.InvoiceMasters.Count() > 0)
            {
                maxInvoiceId = bkDb.InvoiceMasters.OrderByDescending(q => q.InvoiceNo).First();
                invoiceNum = maxInvoiceId.InvoiceNo;
            }
            else
            {
                invoiceNum = 1;
            }

            //Adding record in invoicedetails table - starts            
            bc.AddInvoiceDetails(invoiceNum, customerid);
            //Adding record in invoicedetails table - ends

            InvoiceDetailsPDF invoiceDetailsPDF = new InvoiceDetailsPDF();
            var branchRec = bkDb.BranchMasters.Where(b => b.Branchid == branchid).FirstOrDefault();
            if (branchRec != null)
            {
                /*branch name and company name should be the same*/
                invoiceDetailsPDF.BranchName = branchRec.Bname;
                invoiceDetailsPDF.BranchAddress = branchRec.Baddress;
                invoiceDetailsPDF.BranchPhone = branchRec.Bphone;
                invoiceDetailsPDF.BranchEmail = branchRec.Bemail;
                invoiceDetailsPDF.InvoiceNo = invoiceNum.ToString();
                invoiceDetailsPDF.InvoiceDate = DateTime.Now.ToString();
                var custRec = bkDb.CustomerMasters.Where(cm => cm.Customerid == customerid).FirstOrDefault();
                if (custRec != null)
                {
                    invoiceDetailsPDF.CustomerName = custRec.Customername;
                }
                invoiceDetailsPDF.Gst = "18.0";
                invoiceDetailsPDF.GrandTotal = maxInvoiceId.FinalTotal;
                //Empting current cart from cartmaster-starts
                bc.DeleteCurrentCart(customerid);
                //Empting current cart from cartmaster-ends

                List<InvoiceDetails> invoiceDetails = new List<InvoiceDetails>();
                invoiceDetails = bkDb.InvoiceDetailsMasters.Where(id => id.InvoiceNo == maxInvoiceId.InvoiceNo).ToList();

                List<ProductSubCategoryMaster> productList = new List<ProductSubCategoryMaster>();
                productList = bkDb.ProductSubCategoryMasters.ToList();

                InvoiceProductDetailsPDF invoiceproductdetailsPDF = new InvoiceProductDetailsPDF();

                InvoiceReport rpt = new InvoiceReport(henv, bkDb);
                return File(rpt.Report(invoiceDetailsPDF, invoiceDetails, productList), "application/pdf");
            }
            return View();
        }

        #endregion PrintPDF


    }
}
