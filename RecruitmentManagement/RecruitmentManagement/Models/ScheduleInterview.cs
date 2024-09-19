using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace RecruitmentManagement.Models
{
    public class ScheduleInterview
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name = "Application ID")]
        public int ApplicationID { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email address")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Job title")]
        public string JobTitle { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Interview date")]
        public DateTime InterviewDate { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Interview time")]
        public string InterviewTime { get; set; }

        [Required]
        [Display(Name = "Interview location")]
        public string InterviewLocation { get; set; }

    }
}