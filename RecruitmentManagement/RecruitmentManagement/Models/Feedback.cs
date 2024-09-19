using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecruitmentManagement.Models
{
    public class Feedback
    {
        [Display(Name = "Feedback ID")]
        public int FeedbackID { get; set; }

        [Display(Name = "Application ID")]
        public int ApplicationID { get; set; }

        [Display(Name = "Communication skills")]
        public string Communication { get; set; }

        [Display(Name = "Attitude")]
        public string Attitude { get; set; }

        [Display(Name = "Technical skills")]
        public string TechnicalSkills { get; set; }

        [Display(Name = "Logical skills")]
        public string LogicalSkills { get; set; }

        [Display(Name = "Overall evaluation")]
        public string Overall { get; set; }

        [Display(Name = "Interview result")]
        public string InterviewResult { get; set; }

        [Display(Name = "Remarks")]
        public string Remarks { get; set; }
    }

}