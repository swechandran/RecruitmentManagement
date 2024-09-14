using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
namespace RecruitmentManagement.Repository
{
    public class UserRepository
    {
        private readonly string _connectionString;
        public UserRepository()
        {
            _connectionString = "data source=DESKTOP-7U2R9CR\\SQLEXPRESS; database=Recrutimentmanagement; integrated security= SSPI;";
        }
        public void InsertApplication(CandidateApplication application)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SPI_CandidateApplication", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@JobID", application.JobID);
                        command.Parameters.AddWithValue("@CandidateName", application.CandidateName ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Email", application.Email ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@PhoneNumber", application.PhoneNumber ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@AppliedDate",application.AppliedDate);
                        command.Parameters.Add("@Resume", SqlDbType.VarBinary).Value = application.Resume ?? (object)DBNull.Value;

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }
    }
}