using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevGiants.Models;
using Microsoft.AspNetCore.Http;
namespace DevGiants.Controllers
{
    public class AdminController : Controller
    {
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
        public IActionResult getApplicationView(int applicationID)
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
                    return View("FilledApplication", application);
                }
            }catch(Exception)
            {
                Response.StatusCode = 500;
                return Json(new { error = true, msg = "An Error Occured while retrieving application" });
            }
        }
    }
}
