using DersurSolutionWebApplication.Models;
using DersurSolutionWebApplication.Models.Context;
using System;
using System.Collections.Generic;
using System.IO;
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
            if (Session["AdminID"] == null)
                return RedirectToAction("LoginPage");

            return View();
        }

        public ActionResult ContactMessagesPage()
        {
            return View();
        }

        public ActionResult ProjectManagement()
        {
            return View();
        }

        public ActionResult ServiceManagment()
        {
            return View();
        }

        public ActionResult AdminAccounts()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("LoginPage");
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




        // ======================
        // CONTACT MESSAGE CMS
        // ======================

        public JsonResult GetArchivedContact()
        {
            using (var db = new DersurSolutionContext())
            {
                var data = db.tbl_contact_message
                             .Where(x => x.IsArchived == 1)
                             .ToList();

                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult ArchiveContact(int id)
        {
            using (var db = new DersurSolutionContext())
            {
                var data = db.tbl_contact_message.FirstOrDefault(x => x.ContactID == id);
                if (data != null)
                {
                    data.IsArchived = 1;
                    data.UpdatedAt = DateTime.Now;
                    db.SaveChanges();
                }
                return Json(new { success = true });
            }
        }

        [HttpPost]
        public JsonResult UnarchiveContact(int id)
        {
            using (var db = new DersurSolutionContext())
            {
                var data = db.tbl_contact_message.FirstOrDefault(x => x.ContactID == id);
                if (data != null)
                {
                    data.IsArchived = 0;
                    data.UpdatedAt = DateTime.Now;
                    db.SaveChanges();
                }
                return Json(new { success = true });
            }
        }
        // Project

        [HttpPost]
        public JsonResult SaveProject(tbl_project_model model, HttpPostedFileBase ProjectImageFile)
        {
            try
            {
                using (var db = new DersurSolutionContext())
                {
                    if (ProjectImageFile != null && ProjectImageFile.ContentLength > 0)
                    {
                        var fileName = Guid.NewGuid() + Path.GetExtension(ProjectImageFile.FileName);
                        var path = Server.MapPath("~/Content/image/" + fileName);
                        ProjectImageFile.SaveAs(path);
                        model.ProjectImage = "/Content/image/" + fileName;
                    }

                    if (model.ProjectID == 0)
                    {
                        model.CreatedAt = DateTime.Now;
                        model.UpdatedAt = DateTime.Now;
                        model.IsArchived = 0;
                        db.tbl_project.Add(model);
                    }
                    else
                    {
                        var existing = db.tbl_project.First(x => x.ProjectID == model.ProjectID);
                        existing.ProjectTitle = model.ProjectTitle;
                        existing.ProjectDescription = model.ProjectDescription;
                        existing.ProjectLink = model.ProjectLink;
                        if (!string.IsNullOrEmpty(model.ProjectImage))
                            existing.ProjectImage = model.ProjectImage;

                        existing.UpdatedAt = DateTime.Now;
                    }

                    db.SaveChanges();
                }

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }


        public JsonResult GetProjects()
        {
            using (var db = new DersurSolutionContext())
            {
                var data = db.tbl_project
                    .Where(x => x.IsArchived == 0)
                    .ToList();

                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetArchivedProjects()
        {
            using (var db = new DersurSolutionContext())
            {
                var data = db.tbl_project
                    .Where(x => x.IsArchived == 1)
                    .ToList();

                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult UpdateProject(tbl_project_model model)
        {
            try
            {
                using (var db = new DersurSolutionContext())
                {
                    var project = db.tbl_project
                        .FirstOrDefault(x => x.ProjectID == model.ProjectID);

                    project.ProjectTitle = model.ProjectTitle;
                    project.ProjectDescription = model.ProjectDescription;
                    project.ProjectImage = model.ProjectImage;
                    project.ProjectLink = model.ProjectLink;
                    project.UpdatedAt = DateTime.Now;

                    db.SaveChanges();
                }

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult ArchiveProject(int id)
        {
            using (var db = new DersurSolutionContext())
            {
                var project = db.tbl_project.FirstOrDefault(x => x.ProjectID == id);
                project.IsArchived = 1;
                project.UpdatedAt = DateTime.Now;
                db.SaveChanges();
            }

            return Json(new { success = true });
        }

        [HttpPost]
        public JsonResult UnarchiveProject(int id)
        {
            using (var db = new DersurSolutionContext())
            {
                var project = db.tbl_project.FirstOrDefault(x => x.ProjectID == id);
                project.IsArchived = 0;
                project.UpdatedAt = DateTime.Now;
                db.SaveChanges();
            }

            return Json(new { success = true });
        }
        public JsonResult GetProjectStats()
        {
            using (var db = new DersurSolutionContext())
            {
                return Json(db.tbl_project_stat.ToList(),
                            JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetTestimonials()
        {
            using (var db = new DersurSolutionContext())
            {
                var data = db.tbl_project_testimonial
                             .Where(x => x.IsArchived == 0)
                             .ToList();

                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        //Services 
        [HttpPost]
        public JsonResult SaveService(tbl_service_model model)
        {
            using (var db = new DersurSolutionContext())
            {
                model.CreatedAt = DateTime.Now;
                model.UpdatedAt = DateTime.Now;
                model.IsArchived = 0;

                db.tbl_service.Add(model);
                db.SaveChanges();
            }

            return Json(new { success = true });
        }

        public JsonResult GetServices()
        {
            using (var db = new DersurSolutionContext())
            {
                var data = db.tbl_service
                    .Where(x => x.IsArchived == 0)
                    .ToList();

                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetArchivedServices()
        {
            using (var db = new DersurSolutionContext())
            {
                var data = db.tbl_service
                    .Where(x => x.IsArchived == 1)
                    .ToList();

                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult UpdateService(tbl_service_model model)
        {
            using (var db = new DersurSolutionContext())
            {
                var service = db.tbl_service
                    .FirstOrDefault(x => x.ServiceID == model.ServiceID);

                service.ServiceName = model.ServiceName;
                service.ServiceDescription = model.ServiceDescription;
                service.ServicePrice = model.ServicePrice;
                service.UpdatedAt = DateTime.Now;

                db.SaveChanges();
            }

            return Json(new { success = true });
        }

        [HttpPost]
        public JsonResult ArchiveService(int id)
        {
            using (var db = new DersurSolutionContext())
            {
                var service = db.tbl_service.FirstOrDefault(x => x.ServiceID == id);
                service.IsArchived = 1;
                service.UpdatedAt = DateTime.Now;
                db.SaveChanges();
            }

            return Json(new { success = true });
        }

        [HttpPost]
        public JsonResult UnarchiveService(int id)
        {
            using (var db = new DersurSolutionContext())
            {
                var service = db.tbl_service.FirstOrDefault(x => x.ServiceID == id);
                service.IsArchived = 0;
                service.UpdatedAt = DateTime.Now;
                db.SaveChanges();
            }

            return Json(new { success = true });
        }

        //Login

        [HttpPost]
        public JsonResult AdminLogin(string email, string password)
        {
            using (var db = new DersurSolutionContext())
            {
                var admin = db.tbl_admin_user
                    .FirstOrDefault(x => x.Email == email && x.Password == password && x.IsActive == 1);

                if (admin != null)
                {
                    Session["AdminID"] = admin.AdminID;
                    Session["AdminEmail"] = admin.Email;

                    return Json(new { success = true });
                }

                return Json(new { success = false, message = "Invalid credentials" });
            }
        }

        public JsonResult ContactMonthlyReport()
        {
            using (var db = new DersurSolutionContext())
            {
                var rawData = db.tbl_contact_message
                    .Select(x => x.CreatedAt)
                    .ToList(); // ← force execution in C#

                var data = rawData
                    .GroupBy(d => d.Month)
                    .Select(g => new
                    {
                        Month = g.Key,
                        Count = g.Count()
                    })
                    .OrderBy(x => x.Month)
                    .ToList();

                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult ProjectStatusReport()
        {
            using (var db = new DersurSolutionContext())
            {
                var active = db.tbl_project.Count(x => x.IsArchived == 0);
                var archived = db.tbl_project.Count(x => x.IsArchived == 1);

                return Json(new { active, archived }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ServiceReport()
        {
            using (var db = new DersurSolutionContext())
            {
                var data = db.tbl_service
                    .Where(x => x.IsArchived == 0)
                    .Select(x => x.ServiceName)
                    .ToList();

                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ContactStatusReport()
        {
            using (var db = new DersurSolutionContext())
            {
                var active = db.tbl_contact_message.Count(x => x.IsArchived == 0);
                var archived = db.tbl_contact_message.Count(x => x.IsArchived == 1);

                return Json(new { active, archived }, JsonRequestBehavior.AllowGet);
            }
        }







    }
}