using System;
using System.Data;
using System.Globalization;
using Maestria.Data.Extensions.Internal;

namespace Maestria.Data.Extensions
{
    public static class GetDecimalExtensions
    {
        public static decimal GetDecimal(
            this IDataRecord dataRecord, string column, IFormatProvider provider = null) =>
            dataRecord.GetValue(column, v => Convert.ToDecimal(v, provider ?? CultureInfo.InvariantCulture));

        public static decimal? GetDecimalSafe(
            this IDataRecord dataRecord, string column, IFormatProvider provider = null) =>
            dataRecord.GetValueSafe(column, v => Convert.ToDecimal(v, provider ?? CultureInfo.InvariantCulture));

        public static decimal GetDecimalSafe(
            this IDataRecord dataRecord, string column, decimal @default, IFormatProvider provider = null) =>
            dataRecord.GetDecimalSafe(column, provider) ?? @default;
    }
}