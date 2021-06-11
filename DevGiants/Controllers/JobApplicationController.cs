using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevGiants.Models;
using System.IO;
namespace DevGiants.Controllers
{
    public class JobApplicationController : Controller
    {
        private static string UploadFileToFolder(Microsoft.AspNetCore.Http.IFormFile formFile, string folderLink)
        {
            try
            {

                string filename = Path.GetFileName(formFile.FileName);
                string path = Path.Combine(
                  Directory.GetCurrentDirectory(), "wwwroot", "UploadedFiles" ,
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
        public ActionResult Index()
        {
            ViewBag.Title = "DevGiants - Job Application";
            return View();
        }
        public ActionResult Error()
        {
            ViewBag.Title = "DevGiants - Error"; 
            return View();
        }
        public ActionResult Success()
        {
            ViewBag.Title = "DevGiants - Success";
            return View();
        }
        public ActionResult saveForm(JobApplication application)
        {
            try
            {
                string filename = UploadFileToFolder(Request.Form.Files[0], Url.Content("~/UploadedFiles"));
                application.Filename = filename;
                DAO.saveApplicationToDb(application);
                return RedirectToAction("Success", "JobApplication");
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "JobApplication");
            }

        }
    }
}
