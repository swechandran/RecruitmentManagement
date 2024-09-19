using RecruitmentManagement.Models;
using RecruitmentManagement.Repositories;
using RecruitmentManagement.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace RecruitmentManagement.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly FeedbackRepository feedbackRepository = new FeedbackRepository();
        // GET: Feedback
        public ActionResult Index()
        {
            var feedbacks = feedbackRepository.GetAllFeedback();

            return View(feedbacks);
            
        }
        public ActionResult Create(int applicationId)
        {
            var existingFeedback = feedbackRepository.GetFeedbackById(applicationId);
            if (existingFeedback != null)
            {
                TempData["FeedbackStatus"] = "Feedback already provided.";
                return Redirect("~/Feedback/Details?applicationId=" + applicationId);
            }

            CandidateRepository candidateRepository = new CandidateRepository();
            var list = candidateRepository.GetApplicationById(applicationId);
            ViewBag.RatingOptions = new List<string> { "Excellent", "Very Good", "Good", "Above Average", "Average", "Below Average" };
            ViewBag.InterviewResultOptions = new List<string> { "Selected", "Rejected" };
            return View(list);
        }

        // POST: Feedback/Create
        [HttpPost]
        public ActionResult Create(Feedback feedback)
        {
            
            if (ModelState.IsValid)
            {                                  
                    feedbackRepository.AddFeedback(feedback);
                    return Redirect("~/Feedback/Details?applicationId=" + feedback.ApplicationID);
            }
            ViewBag.RatingOptions = new List<string> { "Excellent", "Very Good", "Good", "Above Average", "Average", "Below Average" };
            ViewBag.InterviewResultOptions = new List<string> { "Selected", "Rejected" };
            return View(feedback);


           
            

        }
        public ActionResult Details(int applicationId)
        {
            var feedback = feedbackRepository.GetFeedbackById(applicationId);

            if (feedback == null)
            {
                TempData["FeedbackStatus"] = "Feecback still not provided";
                return Redirect("~/Interview/ViewScheduledInterviews");
            }

            return View(feedback);
        }
    }
}