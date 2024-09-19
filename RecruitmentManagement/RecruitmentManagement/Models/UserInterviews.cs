using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecruitmentManagement.Models
{
    public class UserInterviews
    {
      
        public int Id { get; set; }
        public string JobTitle { get; set; }
        public DateTime InterviewDate { get; set; } = DateTime.Now;
        public string InterviewTime { get; set; }
        public string InterviewLocation { get; set; }

    }
}