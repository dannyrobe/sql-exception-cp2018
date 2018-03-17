using System;
using System.Data.SqlClient;
using Xunit.Abstractions;

namespace SqlExceptionTalk.Tests
{
    public static class TestOutputHelperExtensions
    {
        public static void LogExceptionAccordingly(this ITestOutputHelper testOutputHelper, Exception exception)
        {
            var sqlException = exception as SqlException;

            if (sqlException != null)
            {
                if (sqlException.Class == 12 && sqlException.State == 255)
                {
                    testOutputHelper.WriteLine($"[WARN] {sqlException.Message}");
                }
                else
                {
                    testOutputHelper.WriteLine($"[ERROR] [SEVERITY: {sqlException.Class}] [STATE: {sqlException.State}] {sqlException.Message}");
                }
            }
            else
            {
                testOutputHelper.WriteLine($"[ERROR] {exception.Message}");
            }
        }
    }
}
