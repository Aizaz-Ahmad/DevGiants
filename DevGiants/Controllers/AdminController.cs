using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevGiants.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
namespace DevGiants.Controllers
{
    public class AdminController : Controller
    {
        private static string UploadFileToFolder(Microsoft.AspNetCore.Http.IFormFile formFile, string folderLink)
        {
            try
            {

                string filename = Path.GetFileName(formFile.FileName);
                string path = Path.Combine(
                  Directory.GetCurrentDirectory(), "wwwroot", "UploadedFiles",
                  filename);
                int count = 1;
                while (System.IO.File.Exists(path))
                    path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadedFiles",
                        $"{Path.GetFileNameWithoutExtension(filename)} ({count++}){Path.GetExtension(filename)}");
                FileStream fileStream = new FileStream(path, FileMode.CreateNew, FileAccess.Write);
                formFile.CopyTo(fileStream);
                return Path.GetFileName(path);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }
        public IActionResult Applications()
        {
            if (HttpContext.Session.GetInt32("user_id") == null)
                return RedirectToAction("Login");
            ViewBag.Title = "DevGiants - Job Applications";
            List<JobApplication> jobApplications = null;
            try
            {
                jobApplications = DAO.GetJobApplications();
            }
            catch (Exception)
            {
                ViewBag.error = true;
            }
            return View(jobApplications);
        }
        public IActionResult Login()
        {
            if (HttpContext.Session.GetInt32("user_id") == null)
            {
                ViewBag.Title = "DevGiants - Admin Login";
                return View();
            }
            else
                return RedirectToAction("Applications");

        }
        public IActionResult LoginCheck(Admin admin)
        {
            try
            {
                int adminid = DAO.isAdminExist(admin);
                if (adminid > 0)
                {
                    HttpContext.Session.SetInt32("user_id", adminid);
                    return RedirectToAction("Applications");
                }
                else
                    TempData["errorMessage"] = "Incorrect Username or Password";
            }
            catch (Exception)
            {
                TempData["errorMessage"] = "Error Occured While Logging in Try again!";
            }
            return RedirectToAction("Login");
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
        
        [HttpDelete]
        public JsonResult deleteApplication(int applicationID)
        {
            if (HttpContext.Session.GetInt32("user_id") == null)
            {
                Response.StatusCode = 403;
                return Json(new { error = true, msg = "You must be logged in to delete the user" });
            }
            try
            {
                if (DAO.deleteApplication(applicationID))
                {
                    Response.StatusCode = 200;
                    return Json(new { error = false, msg = $"Application with ID: {applicationID} successfully deleted" });
                }
                Response.StatusCode = 500;
                return Json(new { error = true, msg = "An Erorr Occured while deleting Application, Try Again" });
            }
            catch (Exception)
            {
                Response.StatusCode = 500;
                return Json(new { error = true, msg = "An Erorr Occured while deleting Application, Try Again" });
            }
        }
        [HttpPost]
        public IActionResult getApplicationView(int applicationID, bool edit)
        {
            if (HttpContext.Session.GetInt32("user_id") == null)
            {
                Response.StatusCode = 403;
                return Json(new { error = true, msg = "You must be logged in to View the Application" });
            }
            try
            {
                JobApplication application = DAO.GetJobApplication(applicationID);
                if(application == null)
                {
                    Response.StatusCode = 401;
                    return Json(new { error = true, msg = $"Application with Id {applicationID} Doesn't exist" });
                }
                else
                {
                    Response.StatusCode = 200;
                    ViewBag.edit = edit;
                    return View("FilledApplication", application);
                }
            }catch(Exception e)
            {
                Response.StatusCode = 500;
                return Json(new { error = true, msg = "An Error Occured while retrieving application" });
            }
        }
        [HttpPost]
        public IActionResult editApplication(JobApplication application)
        {
            if (HttpContext.Session.GetInt32("user_id") == null)
            {
                return RedirectToAction("Login");
            }
            try
            {
                JobApplication prev = DAO.GetJobApplication(application.ApplicationId);
                if (Request.Form.Files.Count != 0)
                    application.Filename = UploadFileToFolder(Request.Form.Files[0], Url.Content("~/UploadedFiles"));
                else
                    application.Filename = prev.Filename;
                application.AppliedAt = prev.AppliedAt;
                if (DAO.updateApplication(application))
                {
                    TempData["editMsg"] = $"Application with ID: {application.ApplicationId} successfully Edited";
                    return RedirectToAction("Applications");
                }
                throw new Exception();
            }
            catch (Exception)
            {
                TempData["editMsg"] = "An Erorr Occured while editing Application, Try Again";
                return RedirectToAction("Applications");
            }
        }
    }
}
