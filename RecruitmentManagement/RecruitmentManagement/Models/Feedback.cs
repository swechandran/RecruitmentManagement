using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecruitmentManagement.Models
{
    public class Feedback
    {
        public int FeedbackID { get; set; }
        public int ApplicationID { get; set; }
        public string Communication { get; set; }
        public string Attitude { get; set; }
        public string TechnicalSkills { get; set; }
        public string LogicalSkills { get; set; }
        public string Overall { get; set; }
        public string InterviewResult { get; set; }
        public string Remarks { get; set; }
    }
}