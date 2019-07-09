using System;
using System.Data;
using Maestria.Data.Extensions.Internal;

namespace Maestria.Data.Extensions
{
    public static class GetDecimalExtensions
    {
        public static decimal GetDecimal(this IDataRecord dataRecord, string column) =>
            dataRecord.GetValue(column, Convert.ToDecimal, default, true);

        public static decimal? GetDecimalSafe(this IDataRecord dataRecord, string column) =>
            dataRecord.GetValueSafe(column, Convert.ToDecimal, null, false);

        public static decimal GetDecimalSafe(this IDataRecord dataRecord, string column, decimal @default) =>
            dataRecord.GetValue(column, Convert.ToDecimal, @default, false);

    }
}