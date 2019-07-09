using System;
using System.Data;
using Maestria.Data.Extensions.Internal;

namespace Maestria.Data.Extensions
{
    public static class GetDateTimeExtensions
    {
        public static DateTime GetDateTime(this IDataRecord dataRecord, string column) =>
            dataRecord.GetValue(column, Convert.ToDateTime, default, true);

        public static DateTime? GetDateTimeSafe(this IDataRecord dataRecord, string column) =>
            dataRecord.GetValueSafe(column, Convert.ToDateTime, null, false);

        public static DateTime GetDateTimeSafe(this IDataRecord dataRecord, string column, DateTime @default) =>
            dataRecord.GetValue(column, Convert.ToDateTime, @default, false);
    }
}