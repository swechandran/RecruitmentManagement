using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecruitmentManagement.Models
{
    public class Enquiry
    {
        public int EnquiryID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public DateTime EnquiryDate { get; set; }
    }
}