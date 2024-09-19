using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace RecruitmentManagement.Models
{
    public class ScheduledInterview
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name = "Application ID")]
        public int ApplicationID { get; set; }

        [Display(Name = "Candidate email")]
        public string CandidateEmail { get; set; }

        [Display(Name = "Job title")]
        public string JobTitle { get; set; }

        [Display(Name = "Interview date")]
        public DateTime InterviewDate { get; set; }

        [Display(Name = "Interview time")]
        public string InterviewTime { get; set; }

        [Display(Name = "Interview location")]
        public string InterviewLocation { get; set; }

        [Display(Name = "Selection status")]
        public string SelectionStatus { get; set; }
    }
}