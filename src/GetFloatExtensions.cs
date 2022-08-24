using System;
using System.Data;
using System.Globalization;
using Maestria.Extensions.Data.Internal;

namespace Maestria.Extensions.Data
{
    public static class GetFloatExtensions
    {
        public static float GetFloat(
            this IDataRecord dataRecord, string column, IFormatProvider provider = null) =>
            dataRecord.GetValue(column, v => Convert.ToSingle(v, provider ?? CultureInfo.InvariantCulture));

        public static float? GetFloatSafe(
            this IDataRecord dataRecord, string column, IFormatProvider provider = null) =>
            dataRecord.GetValueSafe(column, v => Convert.ToSingle(v, provider ?? CultureInfo.InvariantCulture));

        public static float GetFloatSafe(
            this IDataRecord dataRecord, string column, float @default, IFormatProvider provider = null) =>
            dataRecord.GetFloatSafe(column, provider) ?? @default;
    }
}