using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Way2bill.Models;

namespace Way2bill.Controllers
{
    public class LoginController : Controller
    {
        #region Default
        private readonly Way2BillDbContext bkDb;
        private readonly IWebHostEnvironment henv;

        public LoginController(Way2BillDbContext bkDB, IWebHostEnvironment henv)
        {
            bkDb = bkDB;
            this.henv = henv;
        }
        #endregion Default
        public IActionResult Home()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(IFormCollection frm)
        {
            var usremail = Convert.ToString(frm["Email"]);
            var usrpasswd = Convert.ToString(frm["Password"]);
            var rdFound = bkDb.CompanyMasters.Where(usr => usr.Cemail == usremail && usr.Cpassword == usrpasswd).FirstOrDefault();
             if(rdFound == null)
            {
                TempData["ErrMsg"] = "Invalid EmailId or Password";
            }
            else if (rdFound.Cemail== "admin@gmail.com" && rdFound.Cpassword=="aa")
            {
                return RedirectToAction("AdminHome", "Admin");
            }
            else if (rdFound != null)
            {
                return RedirectToAction("CompanyHome","Company", new { rdFound.Companyid });
            }
          
            return View();
        }

        

        public IActionResult ChangePassword()
        {
            return View();
        }


        [HttpPost]
        public IActionResult ChangePassword(IFormCollection frm)
        {
            var usrEmail = Convert.ToString(frm["Email"]);
            var usrPasswd = Convert.ToString(frm["Password"]);
            var rdFound = bkDb.CompanyMasters.Where(usr => usr.Cemail == usrEmail && usr.Cpassword == usrPasswd).FirstOrDefault();

            if (rdFound != null) //record madyo
            {
                rdFound.Cpassword = Convert.ToString(frm["NPassword"]);
                bkDb.Entry(rdFound).State = EntityState.Modified;
                bkDb.SaveChanges();
                TempData["ErrMsg"] = "Password Updated Successfully";
            }
            else
            {
                TempData["ErrMsg"] = "Invalid Email-Id or Password";
            }
            return View();
        }

        [HttpGet]
        public IActionResult Signup()
        {
            var qList = bkDb.QuestionMasters.ToList();
            return View(qList);
        }

        [HttpPost]
        public IActionResult Signup(CompanyMaster companymaster)
        {
            companymaster.Crollid = 1;
            bkDb.Add(companymaster);
            bkDb.SaveChanges();
            return RedirectToAction("Login");
        }
        public JsonResult CheckEmail(String Cemail)
        {
            var chkEmail = bkDb.CompanyMasters.Where(q => q.Cemail == Cemail).Count();
            if (chkEmail > 0)
            {
                return Json(false);
            }
            else
            {
                return Json(true);
            }
        }
    }
}
