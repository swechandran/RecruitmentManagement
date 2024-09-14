using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace RecruitmentManagement.Models
{
    public class JobPosting
    {
        public int JobID { get; set; }
        
        public string JobTitle { get; set; }
        
        public string JobDescription { get; set; }
        
        public string JobLocation { get; set; }
        
        public string Department { get; set; }
        
        public DateTime PostedDate { get; set; } = DateTime.Now;
        public int NumberOfVacancy { get; set; }
        
        public string InterviewMode { get; set; }
        
        public DateTime LastDateToRegister { get; set; }
        
        public byte[] JobPosterImage { get; set; }

        public JobPosting()
        {
        }

        public JobPosting(int jobID, string jobTitle, string jobDescription, string jobLocation, string department, DateTime postedDate, int numberOfVacancy, string interviewMode, DateTime lastDateToRegister)
        {
            JobID = jobID;
            JobTitle = jobTitle;
            JobDescription = jobDescription;
            JobLocation = jobLocation;
            Department = department;
            PostedDate = postedDate;
            NumberOfVacancy = numberOfVacancy;
            InterviewMode = interviewMode;
            LastDateToRegister = lastDateToRegister;
        }
    }
}