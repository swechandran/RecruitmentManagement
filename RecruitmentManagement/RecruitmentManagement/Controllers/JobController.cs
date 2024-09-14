using RecruitmentManagement.Models;
using RecruitmentManagement.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace RecruitmentManagement.Controllers
{
    public class JobController : Controller
    {
        private readonly JobRepository jobRepository;
        public JobController()
        {
            jobRepository = new JobRepository();
        }
        // GET: Jobs
        public ActionResult Index()
        {
            var jobs = jobRepository.GetJobPostings();
            return View(jobs);
        }
        // GET: Jobs/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: Jobs/Create
        [HttpPost]
        public ActionResult Create(JobPosting job, HttpPostedFileBase jobImage)
        {
                try
                {
                    if (jobImage != null && jobImage.ContentLength > 0)
                    {
                        job.JobPosterImage = new byte[jobImage.ContentLength];
                        jobImage.InputStream.Read(job.JobPosterImage, 0, jobImage.ContentLength);
                        
                    }

                    jobRepository.AddJob(job);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while creating the job. Please try again.");
                }
            TempData["Message"] = "Something error";
            return View(job);
        }

        // GET: Jobs/Edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            { 
                TempData["Message"] = "Job ID is required.";
                return RedirectToAction("Index");
            }

            JobPosting job = jobRepository.GetJobById(id.Value);
            if (job == null)
            {
                
                TempData["Message"] = "Job not found.";
                return RedirectToAction("Index");
            }

            return View(job);
        }


        // POST: Jobs/Edit
        [HttpPost]
        public ActionResult Edit(JobPosting job, HttpPostedFileBase jobImage)
        {
                try
                {
                    if (jobImage != null && jobImage.ContentLength > 0)
                    {
                        job.JobPosterImage = new byte[jobImage.ContentLength];
                        jobImage.InputStream.Read(job.JobPosterImage, 0, jobImage.ContentLength);
                    }

                    jobRepository.UpdateJob(job);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while updating the job. Please try again.");
                    TempData["Message"] = $"Something went wrong: {ex.Message}";
                }
                return View(job);
            }

           

        // GET: Jobs/Delete
        public ActionResult Delete(int id)
        {
            var job = jobRepository.GetJobById(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: Jobs/Delete
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int jobID)
        {
            try
            {
                var job = jobRepository.GetJobById(jobID);
                if (job == null)
                {
                    TempData["Error"] = "Job posting not found.";
                    return RedirectToAction("Index");
                }

                jobRepository.DeleteJob(jobID);
                TempData["Message"] = "Job posting deleted successfully.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while trying to delete the job posting.";
                return RedirectToAction("Index");
            }
        }

        // GET: Jobs/Details
        public ActionResult Details(int id)
        {
            var job = jobRepository.GetJobById(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }
    }
}
