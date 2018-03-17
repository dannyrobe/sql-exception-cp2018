using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SqlExceptionTalk.Helpers;
using SqlExceptionTalk.Models;

namespace SqlExceptionTalk.Data
{
    public static class SqlData
    {
        private static readonly string _connectionString = @"Server=localhost\SQLEXPRESS;Database=SqlExceptionDemo;User Id=sa;Password=admin;";

        public static int? UpsertJob(JobDataModel job)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                using (var cmd = new SqlCommand("dbo.UpsertJob", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    var idOutPram = SqlParameterHelper.CreateInputOutputIntParameter("job_id", job.Id);
                    cmd.Parameters.Add(idOutPram);
                    cmd.Parameters.AddWithValue("job_name", job.Name);
                    cmd.Parameters.AddWithValue("job_type_name", job.Type);
                    cmd.Parameters.AddWithValue("job_date", job.Date);
                    cmd.Parameters.AddWithValue("job_amount", job.Amount);

                    cmd.ExecuteNonQuery();

                    return idOutPram.Value as int?;
                }
            }
        }

        public static int? UpsertJobV2(JobDataModel job, out List<string> outMessageList)
        {
            outMessageList = new List<string>();
            var messageList = new List<string>(); // need local holding list for InfoMessage event handler

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                conn.InfoMessage += (sender, args) =>
                {
                    messageList.Add(args.Message);
                };

                using (var cmd = new SqlCommand("dbo.UpsertJobV2", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    var idOutPram = SqlParameterHelper.CreateInputOutputIntParameter("job_id", job.Id);
                    cmd.Parameters.Add(idOutPram);
                    cmd.Parameters.AddWithValue("job_name", job.Name);
                    cmd.Parameters.AddWithValue("job_type_name", job.Type);
                    cmd.Parameters.AddWithValue("job_date", job.Date);
                    cmd.Parameters.AddWithValue("job_amount", job.Amount);

                    cmd.ExecuteNonQuery();

                    outMessageList.AddRange(messageList);

                    return idOutPram.Value as int?;
                }
            }
        }

        public static int? UpsertJobV3(JobDataModel job, out List<string> outMessageList)
        {
            outMessageList = new List<string>();
            var messageList = new List<string>(); // need local holding list for InfoMessage event handler

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                conn.InfoMessage += (sender, args) =>
                {
                    messageList.Add(args.Message);
                };

                using (var cmd = new SqlCommand("dbo.UpsertJobV3", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    var idOutPram = SqlParameterHelper.CreateInputOutputIntParameter("job_id", job.Id);
                    cmd.Parameters.Add(idOutPram);
                    cmd.Parameters.AddWithValue("job_name", job.Name);
                    cmd.Parameters.AddWithValue("job_type_name", job.Type);
                    cmd.Parameters.AddWithValue("job_date", job.Date);
                    cmd.Parameters.AddWithValue("job_amount", job.Amount);

                    cmd.ExecuteNonQuery();

                    outMessageList.AddRange(messageList);

                    return idOutPram.Value as int?;
                }
            }
        }

        public static void DeleteJobType(string jobTypeName)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                var trxn = conn.BeginTransaction();

                try
                {
                    object objJobTypeId;

                    using (var cmd = new SqlCommand("SELECT job_type_id FROM dbo.job_type WHERE job_type_name = @job_type_name", conn, trxn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("job_type_name", jobTypeName);
                        objJobTypeId = cmd.ExecuteScalar();
                    }

                    if (objJobTypeId != null)
                    {
                        var jobTypeId = (int) objJobTypeId;

                        using (var cmd = new SqlCommand("DELETE FROM dbo.job WHERE job_type_id = @job_type_id", conn, trxn))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("job_type_id", jobTypeId);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("DELETE FROM dbo.job_type WHERE job_type_id = @job_type_id", conn, trxn))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("job_type_id", jobTypeId);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    trxn.Commit();
                }
                catch (Exception)
                {
                    trxn.Rollback();

                    throw;
                }
            }
        }
    }
}
