using System;
using System.Data;
using Maestria.Data.Extensions.Internal;

namespace Maestria.Data.Extensions
{
    public static class GetStringExtensions
    {
        public static string GetString(this IDataRecord dataRecord, string column) =>
            dataRecord.GetValueSafe(column, Convert.ToString, null, true);

        public static string GetStringSafe(this IDataRecord dataRecord, string column) =>
            dataRecord.GetValueSafe(column, Convert.ToString, null, false);

        public static string GetStringSafe(this IDataRecord dataRecord, string column, string @default) =>
            dataRecord.GetValueSafe(column, Convert.ToString, @default, false);
    }
}