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

                conn.InfoMessage += (sender, args) => messageList.Add(args.Message);

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

                conn.InfoMessage += (sender, args) => messageList.Add(args.Message);

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
    }
}
