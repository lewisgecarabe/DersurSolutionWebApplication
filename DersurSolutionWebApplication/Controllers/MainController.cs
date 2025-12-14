using DersurSolutionWebApplication.Models;
using DersurSolutionWebApplication.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DersurSolutionWebApplication.Controllers
{
    public class MainController : Controller
    {
        // GET: HomePage
        public ActionResult HomePage()
        {
            return View();
        }
        public ActionResult ServicePage()
        {
            return View();
        }

        public ActionResult PortfolioPage()
        {
            return View();
        }
        public ActionResult AboutPage()
        {
            return View();
        }
        public ActionResult ContactPage()
        {
            return View();
        }

        public ActionResult LoginPage()
        {
            return View();
        }

        public ActionResult Dashboard()
        {
            return View();
        }

        public string CollectiveParameter(userInfo UserInformation)
        {
            return UserInformation.FirstName + " " + UserInformation.LastName + " " + UserInformation.Email + " " + UserInformation.PhoneNumber + " " + UserInformation.Message;
        }

        public JsonResult JsonFunction(userInfo UserInformation)
        {
            try
            {
                UserInformation.FirstName = $"The firstname is {UserInformation.FirstName}";
                UserInformation.LastName = $"The lastname is {UserInformation.LastName}";
                UserInformation.Email = $"The email is {UserInformation.Email}";
                UserInformation.PhoneNumber = $"The PhoneNumber is {UserInformation.PhoneNumber}";
                UserInformation.Message = $"The firstname is {UserInformation.Message}";

                return Json(UserInformation, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw new ArgumentException($"There is an error");
            }
        }

        [HttpPost]
        public JsonResult Upsert(tbl_contact_message_model model)
        {
            try
            {
                using (var db = new DersurSolutionContext())
                {
                    model.CreatedAt = DateTime.Now;
                    model.UpdatedAt = DateTime.Now;
                    model.IsArchived = 0;

                    db.tbl_contact_message.Add(model);
                    db.SaveChanges();
                }

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }


        public void UpdateData()
        {
            try
            {
                 using (var db = new DersurSolutionContext())
                {
                    var updateData = db.tbl_contact_message.Where(x => x.ContactID.Equals(2)).FirstOrDefault();
                    updateData.FirstName = "Amado ";
                    updateData.LastName = "Hey";
                    updateData.UpdatedAt = DateTime.Now;
                    db.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Theres Error {ex.Message} : {ex.StackTrace} :{ex.InnerException} ");
            }
        }

        public JsonResult GetContact()
        {
            using (var db = new DersurSolutionContext())
            {
                var data = db.tbl_contact_message
                    .Where(x => x.IsArchived == 0)
                    .ToList();

                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }



    }
}