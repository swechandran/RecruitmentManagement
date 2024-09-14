using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RecruitmentManagement.Models;
using RecruitmentManagement.Repository;
using CandidateApplication = RecruitmentManagement.Repository.CandidateApplication;
namespace RecruitmentManagement.Controllers
{
    public class UserController : Controller
    {
        private readonly CandidateRepository _repository;
        public UserController()
        {
            _repository = new CandidateRepository();
        }
        private readonly string[] AllowedFileExtensions = { ".pdf", ".doc", ".docx" };
        // GET: User
        public ActionResult Applyjob()
        {
            return View();
        }
        // GET: Candidate/Create 
        public ActionResult Create(int id)
        {
            Session["JobID"] = id;
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
                    UserRepository userRepository = new UserRepository(); 
                    userRepository.InsertApplication(application);
                    TempData["SuccessMessage"] = "Form submitted successfully!";
                    return Redirect("~/Account/About");
                }
                catch (Exception ex)
                {
                    System.IO.File.AppendAllText(Server.MapPath("~/App_Data/ErrorLog.txt"), $"{DateTime.Now}: {ex.Message} - {ex.StackTrace}\n");
                    ModelState.AddModelError("", "Unable to save changes. Please try again later.");
                }
            }
            return View(application);
        }
    }
}