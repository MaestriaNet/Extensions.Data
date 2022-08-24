using System;
using System.Data;
using System.Globalization;
using Maestria.Extensions.Data.Internal;

namespace Maestria.Extensions.Data
{
    public static class GetInt32Extensions
    {
        public static int GetInt32(
            this IDataRecord dataRecord, string column, IFormatProvider provider = null) =>
            dataRecord.GetValue(column, v => Convert.ToInt32(
                Convert.ToSingle(v, provider ?? CultureInfo.InvariantCulture)));

        public static int? GetInt32Safe(
            this IDataRecord dataRecord, string column, IFormatProvider provider = null) =>
            dataRecord.GetValueSafe(column, v => Convert.ToInt32(
                Convert.ToSingle(v, provider ?? CultureInfo.InvariantCulture)));

        public static int GetInt32Safe(
            this IDataRecord dataRecord, string column, int @default, IFormatProvider provider = null) =>
            dataRecord.GetInt32Safe(column, provider) ?? @default;
    }
}