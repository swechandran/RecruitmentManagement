using RecruitmentManagement.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RecruitmentManagement.Repository;
namespace RecruitmentManagement.Controllers
{
    public class CandidateController : Controller
    {
        private readonly CandidateRepository _repository;
        private readonly string[] AllowedFileExtensions = { ".pdf", ".doc", ".docx" };
        public CandidateController()
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["RecruitmentManagementDB"].ConnectionString;
            _repository = new CandidateRepository(connectionString);
        }
        // GET: Candidate 
        public ActionResult Index()
        {
            var applications = _repository.GetAllApplications();
            return View(applications);
        }
        // GET: Candidate/Details
        public ActionResult Details(int id)
        {
            var application = _repository.GetApplicationById(id);
            if (application == null)
            {
                return HttpNotFound(); 
            }
            return View(application);
        }
        // GET: Candidate/Create 
        public ActionResult Create()
        {
            return View();
        }
        // POST: Candidate/Create 
        [HttpPost]
        public ActionResult Create(CandidateApplication application, HttpPostedFileBase resumeFile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (resumeFile != null && resumeFile.ContentLength > 0)
                    {
                        var fileExtension = Path.GetExtension(resumeFile.FileName).ToLower();
                        if (!AllowedFileExtensions.Contains(fileExtension))
                        {
                            ModelState.AddModelError("", "Only PDF, DOC, and DOCX files are allowed.");
                            return View(application);
                        }

                        using (var memoryStream = new MemoryStream())
                        {
                            resumeFile.InputStream.CopyTo(memoryStream);
                            application.Resume = memoryStream.ToArray();
                        }
                    }
                    _repository.InsertApplication(application);
                    return RedirectToAction("Index");
                    
                }
                catch (Exception ex)
                {
                    System.IO.File.AppendAllText(Server.MapPath("~/App_Data/ErrorLog.txt"), $"{DateTime.Now}: {ex.Message} - {ex.StackTrace}\n");
                    ModelState.AddModelError("", "Unable to save changes. Please try again later.");
                }
            }
            return View(application);
        }
        // GET: Candidate/Edit
        public ActionResult Edit(int id)
        {
            var application = _repository.GetApplicationById(id);
            if (application == null)
            {
                return HttpNotFound(); 
            }
            return View(application);
        }
        // POST: Candidate/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CandidateApplication application)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _repository.UpdateApplication(application);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    System.IO.File.AppendAllText(Server.MapPath("~/App_Data/ErrorLog.txt"), $"{DateTime.Now}: {ex.Message} - {ex.StackTrace}\n");
                    ModelState.AddModelError("", "Unable to update the application. Please try again later.");
                }
            }
            return View(application); 
        }
        // GET: Candidate/Delete
        public ActionResult Delete(int id)
        {
            var application = _repository.GetApplicationById(id);
            if (application == null)
            {
                return HttpNotFound(); 
            }
            return View(application);
        }
        // POST: Candidate/Delete
        
        
        public ActionResult DeleteConfirmed(CandidateApplication application)
        {

            try
            {
                _repository.DeleteApplication(application.ApplicationID);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText(Server.MapPath("~/App_Data/ErrorLog.txt"), $"{DateTime.Now}: {ex.Message} - {ex.StackTrace}\n");
                ModelState.AddModelError("", "Unable to delete the application. Please try again later.");
            }
            return RedirectToAction("Index");
        }
        // GET: Candidate/DownloadResume
        public ActionResult DownloadResume(int id)
        {
            var application = _repository.GetApplicationById(id);
            if (application == null || application.Resume == null)
            {
                return HttpNotFound();
            }
            string fileName = $"Resume_{id}.pdf"; 
            string contentType = "application/pdf"; 
            return File(application.Resume, contentType, fileName);
        }
    }
}
