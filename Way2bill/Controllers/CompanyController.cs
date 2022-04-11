using Microsoft.AspNetCore.Mvc;
using Way2bill.Models;

namespace Way2bill.Controllers
{
    public class CompanyController : Controller
    {
        #region Default
        private readonly Way2BillDbContext bkDb;
        private readonly IWebHostEnvironment henv;

        public CompanyController(Way2BillDbContext bkDB, IWebHostEnvironment henv)
        {
            bkDb = bkDB;
            this.henv = henv;
        }
        #endregion Default
        public IActionResult CompanyHome(int Companyid)
        {
            HttpContext.Session.SetString("Companyid", Convert.ToString(Companyid));
            TempData["compId"] = Convert.ToInt32(HttpContext.Session.GetString("Companyid"));
            return View();
        }

        public IActionResult AddBranch()
        {
            
            var qList = bkDb.QuestionMasters.ToList();
            return View(qList);
        }

        [HttpPost]
        public IActionResult AddBranch(BranchMaster branchmaster)
        {
            branchmaster.Companyid = Convert.ToInt32(HttpContext.Session.GetString("Companyid"));
            
            bkDb.Add(branchmaster);
            bkDb.SaveChanges();
            return RedirectToAction("CompanyHome", "Company", new {branchmaster.Companyid});
        }

        public JsonResult CheckEmail(String Email)
        {
            var chkEmail = bkDb.BranchMasters.Where(q => q.Bemail == Email).Count();
            if (chkEmail > 0)
            {
                return Json(false);
            }
            else
            {
                return Json(true);
            }
        }
        public IActionResult Complain()
        {
            TempData["compId"] = Convert.ToInt32(HttpContext.Session.GetString("Companyid"));
            return View();
        }

        [HttpPost]
        public IActionResult Complain(IFormCollection frm)
        {
           
            ComplainMaster complainmaster = new ComplainMaster();
            complainmaster.Cdetails = Convert.ToString(frm["comments"]);
            complainmaster.Companyid = Convert.ToInt32(HttpContext.Session.GetString("Companyid"));
            bkDb.ComplainMasters.Add(complainmaster);
            bkDb.SaveChanges();
            return RedirectToAction("ComplainPost", new { complainmaster.Companyid });
        }

        public IActionResult ComplainPost(int Companyid)
        {
            HttpContext.Session.SetString("Companyid", Convert.ToString(Companyid));
            TempData["compId"] = Convert.ToInt32(HttpContext.Session.GetString("Companyid"));
            return View();
        }

        public IActionResult Feedback()
        {
            TempData["compId"] = Convert.ToInt32(HttpContext.Session.GetString("Companyid"));
            return View();
        }

        [HttpPost]

        public JsonResult CompanyFeedback(string comments,string rdchk)
        {
            FeedbackMaster feedbackmaster = new FeedbackMaster();
            feedbackmaster.Fdetails = comments;
            feedbackmaster.Experiencerate = rdchk;
            feedbackmaster.Companyid = Convert.ToInt32(HttpContext.Session.GetString("Companyid"));
            feedbackmaster.Fdate = DateTime.Now;
            bkDb.FeedbackMasters.Add(feedbackmaster);
            bkDb.SaveChanges();
            return Json("Success");
        }

        public IActionResult Feedbackpost(int Companyid)
        {
            HttpContext.Session.SetString("Companyid", Convert.ToString(Companyid));
            TempData["compId"] = Convert.ToInt32(HttpContext.Session.GetString("Companyid"));
            return View();
        }

        public IActionResult BranchDetails()
        {
            TempData["compId"] = Convert.ToInt32(HttpContext.Session.GetString("Companyid"));
            var cid = Convert.ToInt32(TempData["compId"]);
            var branchdetails= bkDb.BranchMasters.Where(q=>q.Companyid==cid).ToList();
            return View(branchdetails);
        }

        public IActionResult CompanyViewMainCategory()
        {
            TempData["compId"] = Convert.ToInt32(HttpContext.Session.GetString("Companyid"));
            var categorydetails = bkDb.ProudctMainCategoryMasters.ToList();
            return View(categorydetails);
        }

        [HttpGet]
        public IActionResult CompanyAddNewCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CompanyAddNewCategory(ProductMainCategoryMaster maincategory, IFormFile Catimage)
        {
            string uniqueImageName;
            if (Catimage != null)
            {
                string uploadimgfoldername = Path.Combine(henv.WebRootPath, "images\\MainCategory");
                uniqueImageName = Guid.NewGuid().ToString() + "_" + Catimage.FileName;
                string finalPath = Path.Combine(uploadimgfoldername, uniqueImageName);
                Catimage.CopyTo(new FileStream(finalPath, FileMode.Create));
                maincategory.Catimage = "\\images\\MainCategory\\" + uniqueImageName;
            }
            bkDb.ProudctMainCategoryMasters.Add(maincategory);
            bkDb.SaveChanges();
            return RedirectToAction("CompanyViewMainCategory");
        }

        [HttpGet]
        public IActionResult CompanyEditCategory(int Catid)
        {
            var rdfound = bkDb.ProudctMainCategoryMasters.Where(q => q.Catid == Catid).FirstOrDefault();
            
            return View(rdfound);
        }


        [HttpPost]
        public IActionResult CompanyEditCategory( ProductMainCategoryMaster maincategory, IFormFile Catimage)
        {
            var rdfound = bkDb.ProudctMainCategoryMasters.Where(q => q.Catid == maincategory.Catid).FirstOrDefault();
            string uniqueImageName;
            if (Catimage != null) //select new image
            {
                string uploadimgfoldername = Path.Combine(henv.WebRootPath, "images\\MainCategory");
                uniqueImageName = Guid.NewGuid().ToString() + "_" + Catimage.FileName;
                string finalPath = Path.Combine(uploadimgfoldername, uniqueImageName);
                Catimage.CopyTo(new FileStream(finalPath, FileMode.Create));                
                if (rdfound != null)
                {
                    rdfound.Catimage = "\\images\\MainCategory\\" + uniqueImageName;
                }
                
            }
            
            if (rdfound != null)
            {
                rdfound.Catname = maincategory.Catname;
            }
            
            bkDb.Entry(rdfound).State = Microsoft.EntityFrameworkCore.EntityState.Modified;            
            bkDb.SaveChanges();
            return RedirectToAction("CompanyViewMainCategory");
            
        }

        public IActionResult CompanyDeleteCategory(int Catid)
        {
            var rdfound= bkDb.ProudctMainCategoryMasters.Where(q=>q.Catid==Catid).FirstOrDefault();
            bkDb.ProudctMainCategoryMasters.Remove(rdfound);
            bkDb.SaveChanges();
            return RedirectToAction("CompanyViewMainCategory");
        }
        
        [HttpGet]
        public IActionResult CompanyViewSubCategory(int Catid)
        {
            var categorydetails = bkDb.ProductSubCategoryMasters.Where(q=>q.Catid==Catid).ToList();
            TempData["Catid"] = Catid;
            return View(categorydetails);
        }

        [HttpGet]
        public IActionResult CompanyAddNewSubCategory(int Catid)
        {
            TempData["catid"] = Catid;
            return View();
            //var rdfound = bkDb.ProudctMainCategoryMasters.Where(q => q.Catid == Catid).FirstOrDefault();

            //return View(rdfound);

        }

        [HttpPost]
        public IActionResult CompanyAddNewSubCategory(ProductSubCategoryMaster subcategory, IFormFile Scimage)
        {
            string uniqueImageName;
            if (Scimage != null)
            {
                string uploadimgfoldername = Path.Combine(henv.WebRootPath, "images\\SubCategory");
                uniqueImageName = Guid.NewGuid().ToString() + "_" + Scimage.FileName;
                string finalPath = Path.Combine(uploadimgfoldername, uniqueImageName);
                Scimage.CopyTo(new FileStream(finalPath, FileMode.Create));
                subcategory.Scimage = "\\images\\SubCategory\\" + uniqueImageName;
            }
            bkDb.ProductSubCategoryMasters.Add(subcategory);
            bkDb.SaveChanges();
            return RedirectToAction("CompanyViewSubCategory");
        }

        [HttpGet]
        public IActionResult CompanyEditSubCategory(int Scid)
        {
            var rdfound = bkDb.ProductSubCategoryMasters.Where(q => q.Scid == Scid).FirstOrDefault();

            return View(rdfound);
        }


        [HttpPost]
        public IActionResult CompanyEditSubCategory(ProductSubCategoryMaster subcategory, IFormFile Scimage)
        {
            var rdfound = bkDb.ProductSubCategoryMasters.Where(q => q.Scid == subcategory.Scid).FirstOrDefault();
            string uniqueImageName;
            if (Scimage != null) //select new image
            {
                string uploadimgfoldername = Path.Combine(henv.WebRootPath, "images\\SubCategory");
                uniqueImageName = Guid.NewGuid().ToString() + "_" + Scimage.FileName;
                string finalPath = Path.Combine(uploadimgfoldername, uniqueImageName);
                Scimage.CopyTo(new FileStream(finalPath, FileMode.Create));
                if (rdfound != null)
                {
                    rdfound.Scimage = "\\images\\SubCategory\\" + uniqueImageName;
                }

            }

            if (rdfound != null)
            {
                rdfound.Scname = subcategory.Scname;
                rdfound.Scdesc= subcategory.Scdesc;
                rdfound.Scpriceperunit = subcategory.Scpriceperunit;
            }

            bkDb.Entry(rdfound).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            bkDb.SaveChanges();
            return RedirectToAction("CompanyViewSubCategory");

        }

        public IActionResult CompanyDeleteSubCategory(int Scid)
        {
            var rdfound = bkDb.ProductSubCategoryMasters.Where(q => q.Scid == Scid).FirstOrDefault();
            bkDb.ProductSubCategoryMasters.Remove(rdfound);
            bkDb.SaveChanges();
            return RedirectToAction("CompanyViewSubCategory");
        }
    }
}
