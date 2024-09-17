 using RecruitmentManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using RecruitmentManagement.Repository;
using System.Data;
using RecruitmentManagement.Password_Encryption;
namespace RecruitmentManagement.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult ViewJobs()
        {
            JobRepository jobRepository = new JobRepository();
            var jobs = jobRepository.GetJobPostings();
            return View(jobs);
        }
        public ActionResult UserHomePage()
        {
            return View();
        }
        public ActionResult welcome()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Registeration registeration)
        {
            AccountRepository accountRepository = new AccountRepository();
        
            if (accountRepository.UserInsert(registeration))
            {
                return RedirectToAction("Login");
            }
            else
            {
                ViewBag.Message = "Something Error,try again";
                return View();
            }
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Contact() 
        {
            return View();
        }      
        // UPDATE
        [HttpGet]
        public ActionResult EditUser(Registeration registeration)
        {
            if (registeration.UserID <= 0) 
            {
                return HttpNotFound(); 
            }
            AccountRepository accountRepository = new AccountRepository();
            if (accountRepository.UserUpdate(registeration))
            {
                return RedirectToAction("Login");
            }
            else
            {
                ViewBag.Message = "Something Error,try again";
                return View();
            }
        }  
        [HttpPost]
        public ActionResult UpdateUser(Registeration registeration)
        {  
            if (registeration.UserID <= 0)
            {
                return RedirectToAction("Error");
            }
            AccountRepository accountRepository = new AccountRepository();
            if (accountRepository.UserUpdate(registeration))
            {
                return View();
            }
            else
            {
                return View();
            }         
            return RedirectToAction("GetAllUsers");
        }
        // DELETE
        [HttpGet]
        public string DeleteUser(Registeration registeration)
        {
            string connectionString = ("data source=DESKTOP-7U2R9CR\\SQLEXPRESS; database=Recrutimentmanagement; integrated security= SSPI;");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                //string query = "DELETE FROM Users WHERE UserID = @Id";

                using (SqlCommand command = new SqlCommand("SPD_User", connection))
                {
                    command.CommandType=CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserId", registeration.UserID);

                    int delResult = (int)command.ExecuteNonQuery();
                    if(delResult > 0)
                    {
                        return "Record deleted";
                    }
                    else
                    {
                        return "Some thing error";
                    }
                }
            }

        }
        // LOGIN
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Registeration registeration)
        {
            string connectionString = ("data source=DESKTOP-7U2R9CR\\SQLEXPRESS; database=Recrutimentmanagement; integrated security= SSPI;");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Users WHERE Username = @Username AND Password = @Password";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    Password password = new Password();
                    command.Parameters.AddWithValue("@Username", registeration.Username);
                    command.Parameters.AddWithValue("@Password", registeration.Password);
                    //command.Parameters.AddWithValue("@Password", password.Encode(registeration.Password));
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Session["Username"] = reader["Username"];
                            if ((string)reader["UserType"] == "Admin")
                            {
                                return Redirect("~/Admin/Index");
                            }
                            else
                            {
                                return Redirect("~/Account/welcome");

                            }
                        }
                        return View();        
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Invalid username or password";
                        return View();
                    }
                }
            }
        }
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }
    }
}

// READ
//[HttpGet]
//public ActionResult GetAllUsers()
//{
//    List<string> users = new List<string>();

//    using (SqlConnection connection = new SqlConnection(connectionString))
//    {
//        connection.Open();

//        string query = "SELECT * FROM Users";

//        using (SqlCommand command = new SqlCommand(query, connection))
//        {
//            SqlDataReader reader = command.ExecuteReader();

//            while (reader.Read())
//            {
//                users.Add(reader["Username"].ToString());
//            }
//        }
//    }

//    return View("GetAllUsers", users);
//}

