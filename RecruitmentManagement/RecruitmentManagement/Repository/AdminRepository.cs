using RecruitmentManagement.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
namespace RecruitmentManagement.Repository
{
    public class AdminRepository
    {
        private readonly string _connectionString = ("data source=DESKTOP-7U2R9CR\\SQLEXPRESS; database=Recrutimentmanagement; integrated security= SSPI;");
        public AdminRepository()
        {
            //_connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            //_connectionString = ConfigurationManager.ConnectionStrings["RecruitmentManagementConnectionString"].ConnectionString;
        }
        public int GetUserCount()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SPR_UsersCount", connection);
                command.CommandType = CommandType.StoredProcedure;
                return (int)command.ExecuteScalar();
            }
        }
        public IEnumerable<Registeration> GetAllUsers()
        {
            var users = new List<Registeration>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SPR_GetAllUsers", connection);
                command.CommandType = CommandType.StoredProcedure;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var user = new Registeration
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
                            Password = reader["Password"].ToString(),
                            ConfirmPassword = reader["ConfirmPassword"].ToString()
                        };
                        users.Add(user);
                    }
                }
            }
            return users;
        }
        public Registeration GetUserById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SPR_GetUserByID", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserID", id);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Registeration
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
                            Password = reader["Password"].ToString(),
                            ConfirmPassword = reader["ConfirmPassword"].ToString()
                        };
                    }
                }
            }
            return null;
        }
        public void UpdateUser(Registeration user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                //"UPDATE Registerations SET FirstName = @FirstName, LastName = @LastName, DateOfBirth = @DateOfBirth, Gender = @Gender, PhoneNumber = @PhoneNumber, EmailAddress = @EmailAddress, Address = @Address, State = @State, City = @City, Username = @Username, Password = @Password, ConfirmPassword = @ConfirmPassword WHERE UserID = @UserID",
                var command = new SqlCommand("SPU_User", connection);
                command.Parameters.AddWithValue("@UserID", user.UserID);
                command.Parameters.AddWithValue("@FirstName", user.FirstName);
                command.Parameters.AddWithValue("@LastName", user.LastName);
                command.Parameters.AddWithValue("@DateOfBirth", user.DateOfBirth);
                command.Parameters.AddWithValue("@Gender", user.Gender);
                command.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);
                command.Parameters.AddWithValue("@EmailAddress", user.EmailAddress);
                command.Parameters.AddWithValue("@Address", user.Address);
                command.Parameters.AddWithValue("@State", user.State);
                command.Parameters.AddWithValue("@City", user.City);
                command.Parameters.AddWithValue("@Username", user.Username);
                command.Parameters.AddWithValue("@Password", user.Password);
                command.Parameters.AddWithValue("@ConfirmPassword", user.ConfirmPassword);
                command.ExecuteNonQuery();
            }
        }
        public void DeleteUser(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("DELETE FROM Registerations WHERE UserID = @UserID", connection);
                command.Parameters.AddWithValue("@UserID", id);
                command.ExecuteNonQuery();
            }
        }

            public void InsertEnquiry(string name, string email, string message)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(_connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("sp_InsertEnquiry", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Name", name);
                            cmd.Parameters.AddWithValue("@Email", email);
                            cmd.Parameters.AddWithValue("@Message", message);

                            con.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
          
                    throw new Exception("Error inserting enquiry: " + ex.Message);
                }
            }

            
            public List<Enquiry> GetAllEnquiries()
            {
                List<Enquiry> enquiries = new List<Enquiry>();

                try
                {
                    using (SqlConnection con = new SqlConnection(_connectionString))
                    {
                        string query = "SELECT * FROM Enquiries ORDER BY EnquiryDate DESC";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            con.Open();
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    enquiries.Add(new Enquiry
                                    {
                                        EnquiryID = Convert.ToInt32(reader["EnquiryID"]),
                                        Name = reader["Name"].ToString(),
                                        Email = reader["Email"].ToString(),
                                        Message = reader["Message"].ToString(),
                                        EnquiryDate = Convert.ToDateTime(reader["EnquiryDate"])
                                    });
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    
                    throw new Exception("Error retrieving enquiries: " + ex.Message);
                }

                return enquiries;
            }

        }
    }
