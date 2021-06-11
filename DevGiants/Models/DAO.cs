using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
namespace DevGiants.Models
{
    public class DAO
    {
        //public static string UploadFileToFolder(Microsoft.AspNetCore.Http.IFormFile formFile, string folderLink)
        //{
        //    try
        //    {
        //        string filename = Path.GetFileName(formFile.FileName);
        //        string path = Path.Combine(folderLink, filename);
        //        int count = 1;
        //        while (File.Exists(path))
        //            path = Path.Combine(Path.GetFullPath("UploadedFiles"), $"{Path.GetFileNameWithoutExtension(filename)} ({count++}){Path.GetExtension(filename)}");
        //        FileStream fileStream = new FileStream(path, FileMode.CreateNew, FileAccess.Write);
        //        formFile.CopyTo(fileStream);
        //        return Path.GetFileName(path);
        //    }
        //    catch (Exception e)
        //    { 
        //        throw e;
        //    }
        //}
        public static void saveApplicationToDb(JobApplication application)
        {
            try
            {
                using (DevGiantsContext db = new DevGiantsContext())
                {
                    db.JobApplications.Add(application);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static int isAdminExist(Admin admin)
        {
            Admin dbAdmin = null;
            try
            {
                using (DevGiantsContext db = new DevGiantsContext())
                {
                    dbAdmin = db.Admins.FirstOrDefault(ad => ad.Username == admin.Username && ad.Password == admin.Password);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return dbAdmin != null ? dbAdmin.AdminId : -1;
        }
        public static List<JobApplication> GetJobApplications()
        {
            try
            {
                using(DevGiantsContext db = new DevGiantsContext())
                {
                    return db.JobApplications.ToList();
                }
            }catch(Exception e)
            {
                throw e;
            }
        }
        public static bool deleteApplication(int applicationID)
        {
            try
            {
                using (DevGiantsContext db = new DevGiantsContext())
                {
                    JobApplication application = db.JobApplications.FirstOrDefault(application => application.ApplicationId == applicationID);
                    if (application == null) return false;
                    db.JobApplications.Remove(application);
                    db.SaveChanges();
                    return true;
                }
            }catch(Exception e)
            {
                throw e;
            }
        }
        public static JobApplication GetJobApplication(int applicationID)
        {
            try
            {
                using(DevGiantsContext db = new DevGiantsContext())
                {
                    JobApplication application = db.JobApplications.FirstOrDefault(application => application.ApplicationId == applicationID);
                    return application;
                }
            }catch(Exception e)
            {
                throw e;
            }
        }
    }
}
