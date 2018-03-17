using System;
using System.Data;
using System.Data.SqlClient;

namespace SqlExceptionTalk.Helpers
{
    public static class SqlParameterHelper
    {
        public static SqlParameter CreateInputOutputIntParameter(string parameterName, int? intValue)
        {
            var idOutPram = new SqlParameter(parameterName, SqlDbType.Int);
            idOutPram.Direction = ParameterDirection.InputOutput;
            if (intValue == null)
                idOutPram.Value = DBNull.Value;
            else
                idOutPram.Value = intValue;

            return idOutPram;
        }
    }
}
