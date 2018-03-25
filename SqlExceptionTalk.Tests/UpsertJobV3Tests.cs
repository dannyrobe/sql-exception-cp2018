using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Xunit;
using SqlExceptionTalk.Data;
using SqlExceptionTalk.Models;
using SqlExceptionTalk.Tests.Support;
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
            _outputHelper.WriteLine("[TRACE] Starting tests...");
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
            string message = null;
            SqlException sqlException = null;

            try
            {
                newId = SqlData.UpsertJobV3(newJob, out messageList);
            }
            catch (Exception e)
            {
                message = _outputHelper.LogExceptionAccordingly(e)
                          ?? "Generic error message.";

                sqlException = e as SqlException;
            }

            //Assert
            Assert.Null(newId);
            Assert.NotNull(sqlException);
            Assert.Equal(12, sqlException.Class);
            Assert.Equal(255, sqlException.State);
            Assert.Empty(messageList);
            Assert.Equal("A Job with name [New Unique Job #1] already exists.", message);
        }

    }
}
