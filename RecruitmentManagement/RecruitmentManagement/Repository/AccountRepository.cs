using RecruitmentManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RecruitmentManagement.Repository
{
    public class AccountRepository
    {
        private readonly string connectionString = ("data source=DESKTOP-7U2R9CR\\SQLEXPRESS; database=Recrutimentmanagement; integrated security= SSPI;");


        public bool UserInsert(UserModel usermodel)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO Users (FirstName, LastName, DateOfBirth, Gender, PhoneNumber, EmailAddress, Address, State, City, Username, Password) VALUES (@FirstName, @LastName, @DateOfBirth, @Gender, @PhoneNumber, @EmailAddress, @Address, @State, @City, @Username, @Password)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
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
                    int result = command.ExecuteNonQuery();
                    if (result >= 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return false;
        }
        
    }
}
