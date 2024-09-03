﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecruitmentManagement.Models
{
    public class UserModel
    {
       

        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string UserType { get; set; } = "User";

        public UserModel(int userID)
        {
            UserID = userID;
        }

        public UserModel()
        {
        }
    }
}