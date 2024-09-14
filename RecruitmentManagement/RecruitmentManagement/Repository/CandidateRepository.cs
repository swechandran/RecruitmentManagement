using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using RecruitmentManagement.Models;
namespace RecruitmentManagement.Repository
{
    public class CandidateRepository
    {
        private readonly string _connectionString;
        public CandidateRepository(string connectionString)
        {
            _connectionString = "data source=DESKTOP-7U2R9CR\\SQLEXPRESS; database=Recrutimentmanagement; integrated security= SSPI;";
        }
        public CandidateRepository()
        {
            _connectionString = "data source=DESKTOP-7U2R9CR\\SQLEXPRESS; database=Recrutimentmanagement; integrated security= SSPI;";

        }
        public List<CandidateApplication> GetAllApplications()
        {
            List<CandidateApplication> applications = new List<CandidateApplication>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SPR_GetAllApplications", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var application = new CandidateApplication
                                {
                                    ApplicationID = Convert.ToInt32(reader["ApplicationID"]),
                                    JobID = Convert.ToInt32(reader["JobID"]),
                                    CandidateName = reader["CandidateName"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    PhoneNumber = reader["PhoneNumber"].ToString(),
                                    AppliedDate = Convert.ToDateTime(reader["AppliedDate"]),
                                    Resume = reader["Resume"] != DBNull.Value ? (byte[])reader["Resume"] : null
                                };
                                applications.Add(application);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                LogException(ex);
                throw new Exception("Error fetching applications from the database.", ex);
            }

            return applications;
        }
        // Get application by ID
        public CandidateApplication GetApplicationById(int applicationId)
        {
            CandidateApplication application = new CandidateApplication();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SPR_GetApplicationById", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ApplicationID", applicationId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                application = new CandidateApplication
                                {
                                    ApplicationID = Convert.ToInt32(reader["ApplicationID"]),
                                    JobID = Convert.ToInt32(reader["JobID"]),
                                    CandidateName = reader["CandidateName"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    PhoneNumber = reader["PhoneNumber"].ToString(),
                                    AppliedDate = Convert.ToDateTime(reader["AppliedDate"]),
                                    Resume = reader["Resume"] != DBNull.Value ? (byte[])reader["Resume"] : null
                                  
                                };
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                LogException(ex);
                throw new Exception("Error fetching application from the database.", ex);
            }
            return application;
        }
        // Insert new application
        public void InsertApplication(CandidateApplication application)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("data source=DESKTOP-7U2R9CR\\SQLEXPRESS; database=Recrutimentmanagement; integrated security= SSPI;"))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SPI_CandidateApplication", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@JobID", application.JobID);
                        command.Parameters.AddWithValue("@CandidateName", application.CandidateName ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Email", application.Email ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@PhoneNumber", application.PhoneNumber ?? (object)DBNull.Value);

                        command.Parameters.Add("@Resume", SqlDbType.VarBinary).Value = application.Resume ?? (object)DBNull.Value;

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                LogException(ex);
                throw new Exception("Error inserting application into the database.", ex);
            }
        }
        // Update existing application
        public void UpdateApplication(CandidateApplication application)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SPU_CandidateApplication", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ApplicationID", application.ApplicationID);
                        command.Parameters.AddWithValue("@JobID", application.JobID);
                        command.Parameters.AddWithValue("@CandidateName", application.CandidateName ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Email", application.Email ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@PhoneNumber", application.PhoneNumber ?? (object)DBNull.Value);
                        command.Parameters.Add("@Resume", SqlDbType.VarBinary).Value = application.Resume ?? (object)DBNull.Value;
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                LogException(ex);
                throw new Exception("Error updating application in the database.", ex);
            }
        }
        // Delete application
        public void DeleteApplication(int applicationId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SPD_CandidateApplication", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ApplicationID", applicationId);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                LogException(ex);
                throw new Exception("Error deleting application from the database.", ex);
            }
        }
        private void LogException(SqlException ex)
        {
            string logPath = HttpContext.Current.Server.MapPath("~/App_Data/ErrorLog.txt");
            System.IO.File.AppendAllText(logPath, $"{DateTime.Now}: {ex.Message}\n{ex.StackTrace}\n");
        }
    }
    public class CandidateApplication
    {
        public int ApplicationID { get; set; }
        public int JobID { get; set; }
        public string CandidateName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime AppliedDate { get; set; }
        public byte[] Resume { get; set; }
        public string ResumePath { get; internal set; }
    }
}
