using System.Data.SqlClient;
using System.Data;
using System.Web.Mvc;
using RecruitmentManagement.Models;
using RecruitmentManagement.Repository;
using System.Collections.Generic;
namespace RecruitmentManagement.Controllers
{
    public class InterviewController : Controller
    {
        private readonly InterviewRepository _interviewRepository;
        public InterviewController()
        {
            _interviewRepository = new InterviewRepository();
        }
        // GET: Interview
        public ActionResult Index()
        {
            return View();
        }
        // GET: ScheduleInterview
        [HttpGet]
        public ActionResult ScheduleInterview(int id)
        {
            CandidateRepository candidateRepository = new CandidateRepository();
            var list = candidateRepository.GetApplicationById(id);
            //List<Models.CandidateApplication> candidates = new List<ModCandidateApplication>();  
            return View(list);
        }
            
        // POST: ScheduleInterview
        [HttpPost]
        public ActionResult ScheduleInterview(ScheduleInterview model)
        {


            var resultMessage = _interviewRepository.ScheduleInterview(model);
            ViewBag.ResultMessage = resultMessage;

            return Redirect("~/Interview/ViewScheduledInterviews");
        }
        public ActionResult ScheduledInterview()
        {
            return View();
        }
        // GET: ViewScheduledInterviews
        [HttpGet]
        public ActionResult ViewScheduledInterviews()
        {
            var interviews = _interviewRepository.GetScheduledInterviews();
            return View(interviews);
        }
        // GET: ViewScheduledInterviewsByEmail
        [HttpGet]
        public ActionResult ViewScheduledInterviewsByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("ViewScheduledInterviews");
            }
            var interviews = _interviewRepository.GetScheduledInterviewsByEmail(email);
            return View("ViewScheduledInterviews", interviews); 
        }
        [HttpPost]
        public ActionResult UpdateInterviewStatus(string email, string status)
        {
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(status))
            {
                _interviewRepository.UpdateInterviewStatusByEmail(email, status);
            }           
            return RedirectToAction("ViewScheduledInterviews");
        }
    }
}
