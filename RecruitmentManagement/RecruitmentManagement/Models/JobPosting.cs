using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace RecruitmentManagement.Models
{
    public class JobPosting
    {
        [Display(Name = "Job ID")]
        public int JobID { get; set; }

        [Required(ErrorMessage = "Job title is required")]
        [Display(Name = "Job title")]
        [StringLength(100, ErrorMessage = "Job title cannot be longer than 100 characters.")]
        public string JobTitle { get; set; }

        [Required(ErrorMessage = "Job description is required")]
        [Display(Name = "Job description")]
        public string JobDescription { get; set; }

        [Required(ErrorMessage = "Job location is required")]
        [Display(Name = "Job location")]
        public string JobLocation { get; set; }

        [Required(ErrorMessage = "Department is required")]
        [Display(Name = "Department")]
        public string Department { get; set; }

        [Required(ErrorMessage = "Posted date is required")]
        [Display(Name = "Posted date")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format")]
        public DateTime PostedDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Number of vacancies is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid number of vacancies.")]
        [Display(Name = "Number of vacancies")]
        public int NumberOfVacancy { get; set; }

        [Required(ErrorMessage = "Interview mode is required")]
        [Display(Name = "Interview mode")]
        public string InterviewMode { get; set; }

        [Required(ErrorMessage = "Last date to register is required")]
        [Display(Name = "Last date to register")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format")]
        public DateTime LastDateToRegister { get; set; }

        [Display(Name = "Job poster image")]
        [Required(ErrorMessage = "Job poster image is required")]
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