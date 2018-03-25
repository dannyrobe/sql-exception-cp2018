using System;
using System.Data.SqlClient;
using Xunit.Abstractions;

namespace SqlExceptionTalk.Tests.Support
{
    public static class TestOutputHelperExtensions
    {
        /// <summary>
        /// Log the specific SqlException as WARN and return message, 
        /// or log Exception as ERROR and return null.
        /// </summary>
        /// <param name="testOutputHelper">The test output helper.</param>
        /// <param name="exception">The exception.</param>
        /// <returns>Error message if specified SqlException, else null.</returns>
        public static string LogExceptionAccordingly(
            this ITestOutputHelper testOutputHelper, Exception exception)
        {
            string message = null;

            if (exception is SqlException sqlException)
            {
                // My special case: Severity = 12 AND State = 255
                if (sqlException.Class == 12 && sqlException.State == 255)
                {
                    message = sqlException.Message;
                    testOutputHelper.WriteLine($"[WARN] {sqlException.Message}");
                }
                else
                {
                    testOutputHelper.WriteLine(
                        $"[ERROR] [SEVERITY: {sqlException.Class}] " +
                        $"[STATE: {sqlException.State}] {sqlException.Message}");
                }
            }
            else
            {
                testOutputHelper.WriteLine($"[ERROR] {exception.Message}");
            }

            return message;
        }
    }
}
