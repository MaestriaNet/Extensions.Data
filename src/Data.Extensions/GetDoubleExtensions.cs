using System;
using System.Data;
using Maestria.Data.Extensions.Internal;

namespace Maestria.Data.Extensions
{
    public static class GetDoubleExtensions
    {
        public static double GetDouble(this IDataRecord dataRecord, string column) =>
            dataRecord.GetValue(column, Convert.ToDouble, default, true);

        public static double? GetDoubleSafe(this IDataRecord dataRecord, string column) =>
            dataRecord.GetValueSafe(column, Convert.ToDouble, null, false);

        public static double GetDoubleSafe(this IDataRecord dataRecord, string column, double @default) =>
            dataRecord.GetValue(column, Convert.ToDouble, @default, false);
    }
}