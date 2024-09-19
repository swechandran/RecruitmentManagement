using RecruitmentManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;
using RecruitmentManagement.Repository;
using System.Data;
using RecruitmentManagement.Password_Encryption;
using System.Web;

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
        public ActionResult EditUser()
        {
            Registeration registeration = new Registeration();
            registeration.UserID = (int)Session["UserId"];
            AccountRepository accountRepository = new AccountRepository();

            var list = accountRepository.GetUserbyId(registeration);
            return View(list);
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
                TempData["UpdateStatus"] = "Updated successfully";
                 return RedirectToAction("EditUser", "Account", new {UserID=registeration.UserID});
            }
            else
            {
                TempData["UpdateStatus"] = "Error try again";
                return RedirectToAction("EditUser", "Account", new {UserID=registeration.UserID});
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
                            Session["Email"] = reader["EmailAddress"];
                            Session["UserId"] = reader["UserID"];
                            //FormsAuthentication.SetAuthCookie((string)reader["Username"], false);
                            //if (!Roles.IsUserInRole((string)reader["Username"], (string)reader["UserType"]))
                            //{
                            //    Roles.AddUserToRole((string)reader["Username"], (string)reader["UserType"]);
                            //}
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
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetNoStore();
            return RedirectToAction("Login", "Account");
        }

        public ActionResult GetUserByEmail(string email)
        {
            string Email = null;
            string connectionString = ("data source=DESKTOP-7U2R9CR\\SQLEXPRESS; database=Recrutimentmanagement; integrated security= SSPI;");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Users WHERE EmailAddress = @Email";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    //command.Parameters.AddWithValue("@Password", password.Encode(registeration.Password));
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {                           
                            Email = reader["EmailAddress"].ToString();                           
                        }
                        return Content(Email);
                    }
                    else
                    {
                        return Content(Email);
                    }
                }
            }
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

