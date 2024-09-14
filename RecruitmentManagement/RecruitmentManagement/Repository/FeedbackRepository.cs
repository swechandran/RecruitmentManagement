using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using RecruitmentManagement.Models;
using static System.Net.Mime.MediaTypeNames;

namespace RecruitmentManagement.Repositories
{
    public class FeedbackRepository
    {
        
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["RecruitmentManagementDB"].ConnectionString;

        
        public void AddFeedback(Feedback feedback)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // SQL command to insert feedback into the FeedbackForm table
                SqlCommand cmd = new SqlCommand("INSERT INTO FeedbackForm (ApplicationID,Communication, Attitude, TechnicalSkills, LogicalSkills, Overall, InterviewResult, Remarks) " +
                                                "VALUES (@ApplicationID,@Communication, @Attitude, @TechnicalSkills, @LogicalSkills, @Overall, @InterviewResult, @Remarks)", con);

                // Adding parameters to prevent SQL injection
                cmd.Parameters.AddWithValue("@ApplicationID", feedback.ApplicationID);
                cmd.Parameters.AddWithValue("@Communication", feedback.Communication);
                cmd.Parameters.AddWithValue("@Attitude", feedback.Attitude);
                cmd.Parameters.AddWithValue("@TechnicalSkills", feedback.TechnicalSkills);
                cmd.Parameters.AddWithValue("@LogicalSkills", feedback.LogicalSkills);
                cmd.Parameters.AddWithValue("@Overall", feedback.Overall);
                cmd.Parameters.AddWithValue("@InterviewResult", feedback.InterviewResult);
                cmd.Parameters.AddWithValue("@Remarks", feedback.Remarks);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Method to retrieve all feedback records
        public List<Feedback> GetAllFeedback()
        {
            List<Feedback> feedbacks = new List<Feedback>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // SQL command to retrieve feedback data
                SqlCommand cmd = new SqlCommand("SELECT * FROM FeedbackForm", con);
                con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    // Reading feedback data from the database
                    while (reader.Read())
                    {
                        Feedback feedback = new Feedback
                        {
                            FeedbackID = Convert.ToInt32(reader["FeedbackID"]),
                            ApplicationID = Convert.ToInt32(reader["ApplicationID"]),
                            Communication = reader["Communication"].ToString(),
                            Attitude = reader["Attitude"].ToString(),
                            TechnicalSkills = reader["TechnicalSkills"].ToString(),
                            LogicalSkills = reader["LogicalSkills"].ToString(),
                            Overall = reader["Overall"].ToString(),
                            InterviewResult = reader["InterviewResult"].ToString(),
                            Remarks = reader["Remarks"].ToString()
                        };
                        feedbacks.Add(feedback);
                    }
                }
            }

            return feedbacks;
        }
        public UserFeedback GetFeedbackById(int ApplicationID)
        {
            UserFeedback feedback = null;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetFeedbackById", con);
                cmd.CommandType= CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        feedback = new UserFeedback
                        {
                            FeedbackID = Convert.ToInt32(reader["FeedbackID"]),
                            ApplicationID = Convert.ToInt32(reader["ApplicationID"]),
                            Communication = reader["Communication"].ToString(),
                            Attitude = reader["Attitude"].ToString(),
                            TechnicalSkills = reader["TechnicalSkills"].ToString(),
                            LogicalSkills = reader["LogicalSkills"].ToString(),
                            Overall = reader["Overall"].ToString(),
                            InterviewResult = reader["InterviewResult"].ToString(),
                            Remarks = reader["Remarks"].ToString()
                        };
                    }
                    return feedback;
                }
            }

            return feedback;
        }
    }
}
