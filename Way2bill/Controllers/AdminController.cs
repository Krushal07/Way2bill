using Microsoft.AspNetCore.Mvc;
using Way2bill.Models;

namespace Way2bill.Controllers
{
    public class AdminController : Controller
    {
        #region Default
        private readonly Way2BillDbContext bkDb;
        private readonly IWebHostEnvironment henv;

        public AdminController(Way2BillDbContext bkDB, IWebHostEnvironment henv)
        {
            bkDb = bkDB;
            this.henv = henv;
        }
        #endregion Default
        public IActionResult AdminHome()
        {
            return View();
        }

        public IActionResult AdminViewCompanyDetails()
        {
            
            var companydetails = bkDb.CompanyMasters.ToList();
            return View(companydetails);
        }

        public IActionResult AdminViewComplainDetails()
        {
            var complaindetails = bkDb.ComplainMasters.ToList();
            var companydetails = bkDb.CompanyMasters.ToList();
            ViewBag.compdetails = companydetails;           
            return View(complaindetails);
        }

        public IActionResult AdminViewFeedbackDetails()
        {
            var feedbackdetails = bkDb.FeedbackMasters.ToList();
            var companydetails = bkDb.CompanyMasters.ToList();
            ViewBag.compdetails = companydetails;
            return View(feedbackdetails);
        }
    }
}
