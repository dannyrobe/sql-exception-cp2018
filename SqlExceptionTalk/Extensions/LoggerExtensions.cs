using System;
using System.Data.SqlClient;
using NLog;

namespace SqlExceptionTalk.Extensions
{
    public static class LoggerExtensions
    {
        /// <summary>
        /// Log the specific SqlException as WARN and return message, or log Exception as ERROR and return null.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        public static string HandleException(this ILogger logger, Exception exception)
        {
            string message = null;

            if (exception is SqlException sqlException && sqlException.Class == 12 && sqlException.State == 255)
            {
                message = sqlException.Message;
                logger.Warn(sqlException);
            }
            else
            {
                logger.Error(exception);
            }

            return message;
        }
    }
}
