using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace RecruitmentManagement.Models
{
    public class ScheduledInterview
    {
        public int Id { get; set; }
        public int ApplicationID { get; set; }
        public string CandidateEmail { get; set; }
        public string JobTitle { get; set; }
        public DateTime InterviewDate { get; set; }
        public string InterviewTime { get; set; }
        public string InterviewLocation { get; set; }
        public string SelectionStatus { get; set; }
    }
}