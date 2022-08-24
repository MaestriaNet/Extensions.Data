using System;
using System.Data;
using System.Globalization;
using Maestria.Extensions.Data.Internal;

namespace Maestria.Extensions.Data
{
    public static class GetStringExtensions
    {
        public static string GetString(
            this IDataRecord dataRecord, string column, IFormatProvider provider = null) =>
            dataRecord.GetValue(column, v => Convert.ToString(v, provider ?? CultureInfo.InvariantCulture));

        public static string GetStringSafe(
            this IDataRecord dataRecord, string column, IFormatProvider provider = null) =>
            dataRecord.GetValueSafeObject(column, v => Convert.ToString(v, provider ?? CultureInfo.InvariantCulture));

        public static string GetStringSafe(
            this IDataRecord dataRecord, string column, string @default, IFormatProvider provider = null) =>
            dataRecord.GetStringSafe(column, provider) ?? @default;
    }
}