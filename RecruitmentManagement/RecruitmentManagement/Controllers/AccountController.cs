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

namespace RecruitmentManagement.Controllers
{
    public class AccountController : Controller
    {
        private readonly string connectionString = ("data source=DESKTOP-7U2R9CR\\SQLEXPRESS; database=Recrutimentmanagement; integrated security= SSPI;");
        public ActionResult UserHomePage()
        {
            return View();
        }

        // CREATE
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserModel userModel)
        {
            AccountRepository accountRepository = new AccountRepository();
            if (accountRepository.UserInsert(userModel))
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

        //// READ
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

        // UPDATE
        [HttpGet]
        public ActionResult EditUser(UserModel usermodel)
        {
            if (usermodel.UserID <= 0) // Check if id is valid
            {
                return HttpNotFound(); // Handle invalid id
            }

            UserModel user = null;
          

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM Users WHERE UserID = @UserID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", usermodel.UserID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                user = new UserModel
                                {
                                    UserID = (int)reader["UserID"],
                                    FirstName = reader["FirstName"].ToString(),
                                    LastName = reader["LastName"].ToString(),
                                    DateOfBirth = (DateTime)reader["DateOfBirth"],
                                    Gender = reader["Gender"].ToString(),
                                    PhoneNumber = reader["PhoneNumber"].ToString(),
                                    EmailAddress = reader["EmailAddress"].ToString(),
                                    Address = reader["Address"].ToString(),
                                    State = reader["State"].ToString(),
                                    City = reader["City"].ToString(),
                                    Username = reader["Username"].ToString(),
                                    Password = reader["Password"].ToString()
                                };
                            }
                        }
                    }
                }

                if (user == null)
                {
                    return HttpNotFound(); // Use HttpNotFound if the user does not exist
                }

                return View(user);
            }
            catch (Exception ex)
            {
                // Consider logging the error
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Error retrieving user data.");
            }
        }
        [HttpPost]
        public ActionResult UpdateUser(UserModel usermodel)
        {
            if (usermodel.UserID <= 0)
            {
                return RedirectToAction("Error");
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "UPDATE Users SET FirstName = @FirstName, LastName = @LastName, DateOfBirth = @DateOfBirth, Gender = @Gender, PhoneNumber = @PhoneNumber, EmailAddress = @EmailAddress, Address = @Address, State = @State, City = @City, Username = @Username, Password = @Password WHERE UserID = @UserID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", usermodel.UserID);
                        command.Parameters.AddWithValue("@FirstName", usermodel.FirstName);
                        command.Parameters.AddWithValue("@LastName", usermodel.LastName);
                        command.Parameters.AddWithValue("@DateOfBirth", usermodel.DateOfBirth);
                        command.Parameters.AddWithValue("@Gender", usermodel.Gender);
                        command.Parameters.AddWithValue("@PhoneNumber", usermodel.PhoneNumber);
                        command.Parameters.AddWithValue("@EmailAddress", usermodel.EmailAddress);
                        command.Parameters.AddWithValue("@Address", usermodel.Address);
                        command.Parameters.AddWithValue("@State", usermodel.State);
                        command.Parameters.AddWithValue("@City", usermodel.City);
                        command.Parameters.AddWithValue("@Username", usermodel.Username);
                        command.Parameters.AddWithValue("@Password", usermodel.Password);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            // Handle the case where no rows were updated
                            return RedirectToAction("Error");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                // Consider logging the error
                throw new Exception("Error updating user data: " + ex.Message);
            }

            return RedirectToAction("GetAllUsers");
        }
 
        // DELETE
        [HttpGet]
        public string DeleteUser(UserModel usermodel)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                //string query = "DELETE FROM Users WHERE UserID = @Id";

                using (SqlCommand command = new SqlCommand("SPD_User", connection))
                {
                    command.CommandType=CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserId", usermodel.UserID);

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
        public ActionResult Login(UserModel usermodel)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Users WHERE Username = @Username AND Password = @Password";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", usermodel.Username);
                    command.Parameters.AddWithValue("@Password", usermodel.Password);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if ((string)reader["UserType"] == "Admin")
                            {
                                return Redirect("Admin/Index");
                            }
                            else
                            {
                                return RedirectToAction("UserHomePage");

                            }

                        }
                        return View();
                        
                    }
                    else
                    {
                        // Login failed, display error message
                        ViewBag.ErrorMessage = "Invalid username or password";
                        return View();
                    }
                }
            }
        }
    }
}

           