using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecruitmentManagement.Models
{
    public class Enquiry
    {
        [Display(Name = "Enquiry ID")]
        public int EnquiryID { get; set; }

        [Display(Name = "Full name")]
        public string Name { get; set; }

        [Display(Name = "Email address")]
        public string Email { get; set; }

        [Display(Name = "Message")]
        public string Message { get; set; }

        [Display(Name = "Enquiry date")]
        public DateTime EnquiryDate { get; set; }
    }

}