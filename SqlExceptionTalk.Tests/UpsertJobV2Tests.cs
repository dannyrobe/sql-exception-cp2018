using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Xunit;
using SqlExceptionTalk.Data;
using SqlExceptionTalk.Models;
using Xunit.Abstractions;

namespace SqlExceptionTalk.Tests
{
    public class UpsertJobV2Tests : IDisposable
    {
        #region Setup and Tear Down Tests

        private readonly ITestOutputHelper _outputHelper;

        public UpsertJobV2Tests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
            _outputHelper.WriteLine("[TRACE] Starting a tests...");
        }

        public void Dispose()
        {
            _outputHelper.WriteLine("[TRACE] Tests complete.");
        }

        #endregion

        [Fact]
        public void UpsertJobV2_AddNewUniqueJob()
        {
            //Arrange
            var messageList = new List<string>();
            var newJob = new JobDataModel
            {
                Id = null,
                Name = "New Unique Job #1",
                Type = "Electrical",
                Date = DateTime.Parse("2018-04-01"),
                Amount = 125.00M
            };

            //Act
            var newId = SqlData.UpsertJobV2(newJob, out messageList);
            _outputHelper.WriteLine("[INFO] New job created with Id = {0}", 
                newId?.ToString() ?? "null");

            //Assert
            Assert.NotNull(newId);
        }

        [Fact]
        public void UpsertJobV2_AddNewJobWithDuplicateName()
        {
            //Arrange
            var messageList = new List<string>();
            var newJob = new JobDataModel
            {
                Id = null,
                Name = "New Unique Job #1",
                Type = "Plumbing",
                Date = DateTime.Parse("2018-04-02"),
                Amount = 725.00M
            };

            //Act & Assert
            int? newId;
            var sqlException = Assert.Throws<SqlException>(() =>
            {
                newId = SqlData.UpsertJobV2(newJob, out messageList);
            });

            _outputHelper.WriteLine("[ERROR] Class: {0}; State: {1}; Message: {2}", 
                sqlException.Class, sqlException.State, sqlException.Message);

            //Assert
            Assert.Equal(14, sqlException.Class);
            Assert.Equal(1, sqlException.State);
            Assert.Contains("Cannot insert duplicate key row", sqlException.Message);
        }

    }
}
