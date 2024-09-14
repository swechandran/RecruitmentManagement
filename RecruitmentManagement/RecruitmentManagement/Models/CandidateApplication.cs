using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace RecruitmentManagement.Models
{
    public class CandidateApplication
    {
        [Required]
        public int JobID { get; set; }
        [Required]
        public string CandidateName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public byte[] Resume { get; set; }
        public DateTime AppliedDate { get; set; } = DateTime.Now;
        public int ApplicationID { get; internal set; }
        public DateTime InterviewDate { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public string InterviewTime { get; set; }
        [Required]
        public string InterviewLocation { get; set; }
        public int FeedbackID { get; set; }
       
        
    }
}