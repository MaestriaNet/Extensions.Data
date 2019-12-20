using System;
using System.Data;
using System.Globalization;
using Maestria.Data.Extensions.Internal;

namespace Maestria.Data.Extensions
{
    public static class GetInt64Extensions
    {
        public static long GetInt64(
            this IDataRecord dataRecord, string column, IFormatProvider provider = null) =>
            dataRecord.GetValue(column, v => Convert.ToInt64(
                Convert.ToSingle(v, provider ?? CultureInfo.InvariantCulture)));

        public static long? GetInt64Safe(
            this IDataRecord dataRecord, string column, IFormatProvider provider = null) =>
            dataRecord.GetValueSafe(column, v => Convert.ToInt64(
                Convert.ToSingle(v, provider ?? CultureInfo.InvariantCulture)));

        public static long GetInt64Safe(
            this IDataRecord dataRecord, string column, long @default, IFormatProvider provider = null) =>
            dataRecord.GetInt64Safe(column, provider) ?? @default;
    }
}