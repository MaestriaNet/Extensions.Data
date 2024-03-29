using System;
using System.Data;
using System.Globalization;
using Maestria.Extensions.Data.Internal;

namespace Maestria.Extensions.Data
{
    public static class GetDateTimeExtensions
    {
        public static DateTime GetDateTime(
            this IDataRecord dataRecord, string column, IFormatProvider provider = null) =>
            dataRecord.GetValue(column, v => Convert.ToDateTime(v, provider ?? CultureInfo.InvariantCulture));

        public static DateTime? GetDateTimeSafe(
            this IDataRecord dataRecord, string column, IFormatProvider provider = null) =>
            dataRecord.GetValueSafe(column, v => Convert.ToDateTime(v, provider ?? CultureInfo.InvariantCulture));

        public static DateTime GetDateTimeSafe(
            this IDataRecord dataRecord, string column, DateTime @default, IFormatProvider provider = null) =>
            dataRecord.GetDateTimeSafe(column, provider) ?? @default;
    }
}