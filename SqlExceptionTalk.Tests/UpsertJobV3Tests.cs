using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Xunit;
using SqlExceptionTalk.Data;
using SqlExceptionTalk.Models;
using Xunit.Abstractions;

namespace SqlExceptionTalk.Tests
{
    /// <summary>
    /// Tests for Stored Procedure WITH both soft and hard error handling.
    /// </summary>
    public class UpsertJobV3Tests : IDisposable
    {
        #region Setup and Tear Down Tests

        private readonly ITestOutputHelper _outputHelper;

        public UpsertJobV3Tests(ITestOutputHelper outputHelper)
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
        public void UpsertJobV3_AddNewJobWithDuplicateName()
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
            int? newId = null;
            var sqlException = Assert.Throws<SqlException>(() =>
            {
                newId = SqlData.UpsertJobV3(newJob, out messageList);
            });

            var message = _outputHelper.LogExceptionAccordingly(sqlException)
                          ?? "Generic error message.";

            //Assert
            Assert.Null(newId);
            Assert.Equal(12, sqlException.Class);
            Assert.Equal(255, sqlException.State);
            Assert.Empty(messageList);
            Assert.Equal("A Job with name [New Unique Job #1] already exists.", message);
        }

    }
}
