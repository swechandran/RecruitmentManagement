using RecruitmentManagement.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
namespace RecruitmentManagement.Repository
{
    public class InterviewRepository
    {
        private readonly string _connectionString;
        public InterviewRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["RecruitmentManagementDB"].ConnectionString;
        }
        public string ScheduleInterview(ScheduleInterview interview)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("ScheduleInterview", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@ApplicationID", interview.ApplicationID);
                command.Parameters.AddWithValue("@CandidateEmail", interview.Email);
                //command.Parameters.AddWithValue("@JobTitle", interview.JobTitle);
                command.Parameters.AddWithValue("@DateOfInterview", interview.InterviewDate);
                command.Parameters.AddWithValue("@TimeOfInterview", interview.InterviewTime);
                command.Parameters.AddWithValue("@Location", interview.InterviewLocation);
                connection.Open();
                var resultMessage = (string)command.ExecuteScalar();
                return resultMessage;
            }
        }
        public IEnumerable<ScheduledInterview> GetScheduledInterviews()
        {
            var interviews = new List<ScheduledInterview>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("GetScheduledInterviews", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        interviews.Add(new ScheduledInterview
                        {
                            ApplicationID = Convert.ToInt32(reader["ApplicationID"]),
                            CandidateEmail = reader["CandidateEmail"].ToString(),
                            //JobTitle = reader["JobTitle"].ToString(),
                            InterviewDate = (DateTime)reader["DateOfInterview"],
                            InterviewTime = reader["TimeOfInterview"].ToString(),
                            InterviewLocation = reader["Location"].ToString()
                        });
                    }
                }
            }
            return interviews;
        }
        public IEnumerable<ScheduledInterview> GetScheduledInterviewsByEmail(string email)
        {
            var interviews = new List<ScheduledInterview>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("GetScheduledInterviewByEmail", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@CandidateEmail", email);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        interviews.Add(new ScheduledInterview
                        {
                            ApplicationID = Convert.ToInt32(reader["ApplicationID"]),
                            CandidateEmail = reader["CandidateEmail"].ToString(),
                            //JobTitle = reader["JobTitle"].ToString(),
                            InterviewDate = (DateTime)reader["DateOfInterview"],
                            InterviewTime = reader["TimeOfInterview"].ToString(),
                            InterviewLocation = reader["Location"].ToString()
                        });
                    }
                }
            }
            return interviews;
        }
        public void UpdateInterviewStatusByEmail(string email, string status)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SP_UpdateInterviewStatusByEmail", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@CandidateEmail", email);
                command.Parameters.AddWithValue("@SelectionStatus", status);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }



    }
}