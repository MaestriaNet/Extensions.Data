using System;
using System.Data;
using System.Globalization;
using Maestria.Extensions.Data.Internal;

namespace Maestria.Extensions.Data
{
    public static class GetDoubleExtensions
    {
        public static double GetDouble(
            this IDataRecord dataRecord, string column, IFormatProvider provider = null) =>
            dataRecord.GetValue(column, v => Convert.ToDouble(v, provider ?? CultureInfo.InvariantCulture));

        public static double? GetDoubleSafe(
            this IDataRecord dataRecord, string column, IFormatProvider provider = null) =>
            dataRecord.GetValueSafe(column, v => Convert.ToDouble(v, provider ?? CultureInfo.InvariantCulture));

        public static double GetDoubleSafe(
            this IDataRecord dataRecord, string column, double @default, IFormatProvider provider = null) =>
            dataRecord.GetDoubleSafe(column, provider) ?? @default;
    }
}