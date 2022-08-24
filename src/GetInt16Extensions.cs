using System;
using System.Data;
using System.Globalization;
using Maestria.Extensions.Data.Internal;

namespace Maestria.Extensions.Data
{
    public static class GetInt16Extensions
    {
        public static int GetInt16(
            this IDataRecord dataRecord, string column, IFormatProvider provider = null) =>
            dataRecord.GetValue(column, v => Convert.ToInt16(
                Convert.ToSingle(v, provider ?? CultureInfo.InvariantCulture)));

        public static int? GetInt16Safe(
            this IDataRecord dataRecord, string column, IFormatProvider provider = null) =>
            dataRecord.GetValueSafe(column, v => Convert.ToInt16(
                Convert.ToSingle(v, provider ?? CultureInfo.InvariantCulture)));

        public static int GetInt16Safe(
            this IDataRecord dataRecord, string column, short @default, IFormatProvider provider = null) =>
            dataRecord.GetInt16Safe(column, provider) ?? @default;
    }
}