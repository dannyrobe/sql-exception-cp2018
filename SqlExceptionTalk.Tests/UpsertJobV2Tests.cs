using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using SqlExceptionTalk.Data;
using SqlExceptionTalk.Models;
using Xunit.Abstractions;

namespace SqlExceptionTalk.Tests
{
    /// <summary>
    /// Tests for Stored Procedure WITH only soft error handling.
    /// </summary>
    public class UpsertJobV2Tests : IDisposable
    {
        #region Setup and Tear Down Tests

        private readonly ITestOutputHelper _outputHelper;

        public UpsertJobV2Tests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
            _outputHelper.WriteLine("[TRACE] Starting a tests...");

            // reset database for test
            SqlData.DeleteJobType("Roofing");
        }

        public void Dispose()
        {
            _outputHelper.WriteLine("[TRACE] Tests complete.");
        }

        #endregion

        [Fact]
        public void UpsertJobV2_AddNewUniqueJobWithNewJobType()
        {
            //Arrange
            var messageList = new List<string>();
            var newJob = new JobDataModel
            {
                Id = null,
                Name = "New Unique Job #2",
                Type = "Roofing",
                Date = DateTime.Parse("2018-04-02"),
                Amount = 1250.00M
            };

            //Act
            var newId = SqlData.UpsertJobV2(newJob, out messageList);
            _outputHelper.WriteLine("[INFO] {0}", messageList.FirstOrDefault());
            _outputHelper.WriteLine("[INFO] New job created with Id = {0}", newId?.ToString() ?? "null");

            //Assert
            Assert.NotNull(newId);
            Assert.Single(messageList);
            var message = messageList.Count > 0 ? messageList.First() : string.Empty;
            Assert.Equal("A new Job Type [Roofing] was created.", message);
        }
    }
}
