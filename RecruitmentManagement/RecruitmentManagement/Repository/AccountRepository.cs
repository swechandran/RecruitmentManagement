using RecruitmentManagement.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration; 
using System.Linq;
using System.Web;
using RecruitmentManagement.Password_Encryption;
namespace RecruitmentManagement.Repository
{
    public class AccountRepository
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["RecruitmentManagementDb"].ConnectionString;
        

        public bool UserInsert(Registeration registeration)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Password password = new Password();
                connection.Open();
                using (SqlCommand command = new SqlCommand("SPC_User", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FirstName", registeration.FirstName);
                    command.Parameters.AddWithValue("@LastName", registeration.LastName);
                    command.Parameters.AddWithValue("@DateOfBirth", registeration.DateOfBirth);
                    command.Parameters.AddWithValue("@Gender", registeration.Gender);
                    command.Parameters.AddWithValue("@PhoneNumber", registeration.PhoneNumber);
                    command.Parameters.AddWithValue("@EmailAddress", registeration.EmailAddress);
                    command.Parameters.AddWithValue("@Address", registeration.Address);
                    command.Parameters.AddWithValue("@State", registeration.State);
                    command.Parameters.AddWithValue("@City", registeration.City);
                    command.Parameters.AddWithValue("@Username", registeration.Username);
                    command.Parameters.AddWithValue("@Password", registeration.Password);
                    //command.Parameters.AddWithValue("@Password", password.Encode(registeration.Password));
                    command.Parameters.AddWithValue("@UserType", registeration.UserType);
                    int result = command.ExecuteNonQuery();
                    return result >= 0;
                }
                
            }    
        }
        

        public bool UserUpdate(Registeration registeration)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SPU_User", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserID", registeration.UserID);
                        command.Parameters.AddWithValue("@FirstName", registeration.FirstName);
                        command.Parameters.AddWithValue("@LastName", registeration.LastName);
                        command.Parameters.AddWithValue("@DateOfBirth", registeration.DateOfBirth);
                        command.Parameters.AddWithValue("@Gender", registeration.Gender);
                        command.Parameters.AddWithValue("@PhoneNumber", registeration.PhoneNumber);
                        command.Parameters.AddWithValue("@EmailAddress", registeration.EmailAddress);
                        command.Parameters.AddWithValue("@Address", registeration.Address);
                        command.Parameters.AddWithValue("@State", registeration.State);
                        command.Parameters.AddWithValue("@City", registeration.City);
                        command.Parameters.AddWithValue("@Username", registeration.Username);
                        command.Parameters.AddWithValue("@Password", registeration.Password);
                        int result = command.ExecuteNonQuery();
                        return result >= 0;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
