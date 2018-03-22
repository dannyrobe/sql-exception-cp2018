using System;
using System.Data.SqlClient;
using Xunit;
using SqlExceptionTalk.Data;
using SqlExceptionTalk.Models;
using SqlExceptionTalk.Tests.Support;
using Xunit.Abstractions;
using static SqlExceptionTalk.Tests.Support.XunitTestPriority;

namespace SqlExceptionTalk.Tests
{
    /// <summary>
    /// Tests for Stored Procedure without error handling.
    /// </summary>
    public class UpsertJobTests : IDisposable
    {
        #region Setup and Tear Down Tests

        private readonly ITestOutputHelper _outputHelper;

        public UpsertJobTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
            _outputHelper.WriteLine("[TRACE] Starting tests...");
        }

        public void Dispose()
        {
            _outputHelper.WriteLine("[TRACE] Tests complete.");
        }

        #endregion

        [Fact, TestPriority(1)]
        public void UpsertJob_AddNewUniqueJob()
        {
            // reset database for test
            SqlData.DeleteJob("New Unique Job #1");

            //Arrange
            var newJob = new JobDataModel
            {
                Id = null,
                Name = "New Unique Job #1",
                Type = "Electrical",
                Date = DateTime.Parse("2018-04-01"),
                Amount = 125.00M
            };

            //Act
            var newId = SqlData.UpsertJob(newJob);
            _outputHelper.WriteLine("[INFO] New job created with Id = {0}", 
                newId?.ToString() ?? "null");

            //Assert
            Assert.NotNull(newId);
        }

        [Fact, TestPriority(2)]
        public void UpsertJob_AddNewJobWithDuplicateName() // must run UpsertJob_AddNewUniqueJob() before this test
        {
            //Arrange
            var newJob = new JobDataModel
            {
                Id = null,
                Name = "New Unique Job #1",
                Type = "Plumbing",
                Date = DateTime.Parse("2018-04-02"),
                Amount = 725.00M
            };

            //Act & Assert
            int? newId = null;
            var sqlException = Assert.Throws<SqlException>(() =>
            {
                newId = SqlData.UpsertJob(newJob);
            });

            var message = _outputHelper.LogExceptionAccordingly(sqlException)
                ?? "Generic error message.";

            //Assert
            Assert.Null(newId);
            Assert.Equal(14, sqlException.Class);
            Assert.Equal(1, sqlException.State);
            Assert.Equal("Generic error message.", message);
            Assert.Contains("Cannot insert duplicate key row", sqlException.Message);
        }

    }
}
