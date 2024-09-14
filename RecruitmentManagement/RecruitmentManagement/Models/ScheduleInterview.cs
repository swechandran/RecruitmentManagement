using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace RecruitmentManagement.Models
{
    public class ScheduleInterview
    {
        public int Id { get; set; }
        public int ApplicationID { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string JobTitle { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime InterviewDate { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public string InterviewTime { get; set; }
        [Required]
        public string InterviewLocation { get; set; }

    }
}