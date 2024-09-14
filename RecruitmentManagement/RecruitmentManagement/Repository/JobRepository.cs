using RecruitmentManagement.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
namespace RecruitmentManagement.Repository
{
    public class JobRepository
    {
        private readonly string connectionString;
        public JobRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["RecruitmentManagementDB"].ConnectionString;
        }
        public List<JobPosting> GetJobPostings()
        {
            var jobs = new List<JobPosting>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SPR_JobPostings", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            jobs.Add(new JobPosting
                            {
                                JobID = Convert.ToInt32(reader["JobID"]),
                                JobTitle = reader["JobTitle"].ToString(),
                                JobDescription = reader["JobDescription"].ToString(),
                                JobLocation = reader["JobLocation"].ToString(),
                                Department = reader["Department"].ToString(),
                                PostedDate = Convert.ToDateTime(reader["PostedDate"]),
                                NumberOfVacancy = Convert.ToInt32(reader["NumberOfVacancy"]),
                                InterviewMode = reader["InterviewMode"].ToString(),
                                LastDateToRegister = Convert.ToDateTime(reader["LastDateToRegister"]),
                                JobPosterImage = reader["JobPosterImage"] as byte[]
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return jobs;
        }

        // Add a new job posting
        public void AddJob(JobPosting job)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SPI_JobPosting", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@JobTitle", job.JobTitle);
                    cmd.Parameters.AddWithValue("@JobDescription", job.JobDescription);
                    cmd.Parameters.AddWithValue("@JobLocation", job.JobLocation);
                    cmd.Parameters.AddWithValue("@Department", job.Department);
                    cmd.Parameters.AddWithValue("@NumberOfVacancy", job.NumberOfVacancy);
                    cmd.Parameters.AddWithValue("@InterviewMode", job.InterviewMode);
                    cmd.Parameters.AddWithValue("@LastDateToRegister", job.LastDateToRegister);
                    cmd.Parameters.AddWithValue("@PostedDate", job.PostedDate);
                    cmd.Parameters.Add("@JobPosterImage", SqlDbType.VarBinary).Value = job.JobPosterImage ?? (object)DBNull.Value;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Handle exception as needed, e.g., log error
                throw;
            }
        }

        // Get a specific job posting by ID
        public JobPosting GetJobById(int id)
        {
            JobPosting job = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM JobPostings WHERE JobID = @JobID", conn);
                    cmd.Parameters.AddWithValue("@JobID", id);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            job = new JobPosting
                            {
                                JobID = Convert.ToInt32(reader["JobID"]),
                                JobTitle = reader["JobTitle"].ToString(),
                                JobDescription = reader["JobDescription"].ToString(),
                                JobLocation = reader["JobLocation"].ToString(),
                                Department = reader["Department"].ToString(),
                                PostedDate = Convert.ToDateTime(reader["PostedDate"]),
                                NumberOfVacancy = Convert.ToInt32(reader["NumberOfVacancy"]),
                                InterviewMode = reader["InterviewMode"].ToString(),
                                LastDateToRegister = Convert.ToDateTime(reader["LastDateToRegister"]),
                                JobPosterImage = reader["JobPosterImage"] as byte[]
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception as needed, e.g., log error
                throw;
            }
            return job;
        }

        // Update an existing job posting
        public void UpdateJob(JobPosting job)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SPU_JobPosting", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    
                    cmd.Parameters.AddWithValue("@JobID", job.JobID);
                    cmd.Parameters.AddWithValue("@JobTitle", job.JobTitle);
                    cmd.Parameters.AddWithValue("@JobDescription", job.JobDescription);
                    cmd.Parameters.AddWithValue("@JobLocation", job.JobLocation);
                    cmd.Parameters.AddWithValue("@Department", job.Department);
                    cmd.Parameters.AddWithValue("@NumberOfVacancy", job.NumberOfVacancy);
                    cmd.Parameters.AddWithValue("@InterviewMode", job.InterviewMode);
                    cmd.Parameters.AddWithValue("@LastDateToRegister", job.LastDateToRegister);
                    cmd.Parameters.AddWithValue("@PostedDate", job.PostedDate);
                    cmd.Parameters.Add("@JobPosterImage", SqlDbType.VarBinary).Value = job.JobPosterImage ?? (object)DBNull.Value;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        // Delete a job posting by ID
        public void DeleteJob(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("DeleteJobPosting", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add("@JobID", SqlDbType.Int).Value = id;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Handle exception as needed, e.g., log error
                throw;
            }
        }
    }
}