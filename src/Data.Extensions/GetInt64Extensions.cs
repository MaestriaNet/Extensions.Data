using System;
using System.Data;
using Maestria.Data.Extensions.Internal;

namespace Maestria.Data.Extensions
{
    public static class GetInt64Extensions
    {
        public static long GetInt64(this IDataRecord dataRecord, string column) =>
            dataRecord.GetValue(column, v => Convert.ToInt64(Convert.ToSingle(v)), default, true);

        public static long? GetInt64Safe(this IDataRecord dataRecord, string column) =>
            dataRecord.GetValueSafe(column, v => Convert.ToInt64(Convert.ToSingle(v)), null, false);

        public static long GetInt64Safe(this IDataRecord dataRecord, string column, long @default) =>
            dataRecord.GetValue(column, v => Convert.ToInt64(Convert.ToSingle(v)), @default, false);
    }
}