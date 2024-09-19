using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace RecruitmentManagement.Models
{
    public class CandidateApplication
    {
        [DisplayName("Application ID")]
        public int ApplicationID { get; set; }

        [DisplayName("Job ID")]
        public int JobID { get; set; }

        [DisplayName("Candidate name")]
        public string CandidateName { get; set; }

        [DisplayName("Email address")]
        public string Email { get; set; }

        [DisplayName("Phone number")]
        public string PhoneNumber { get; set; }

        [DisplayName("Date applied")]
        public DateTime AppliedDate { get; set; } = DateTime.Now;

        [DisplayName("Resume")]
        public byte[] Resume { get; set; }

        [DisplayName("Resume Path")]
        public string ResumePath { get; internal set; }
    }
}