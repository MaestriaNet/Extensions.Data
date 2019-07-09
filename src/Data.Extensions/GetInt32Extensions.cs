using System;
using System.Data;
using System.Globalization;
using Maestria.Data.Extensions.Internal;

namespace Maestria.Data.Extensions
{
    public static class GetInt32Extensions
    {

        public static int GetInt32(this IDataRecord dataRecord, string column) =>
            dataRecord.GetValue(column, v => Convert.ToInt32(Convert.ToSingle(v, CultureInfo.InvariantCulture)), default, true);

        public static int? GetInt32Safe(this IDataRecord dataRecord, string column) =>
            dataRecord.GetValueSafe(column, v => Convert.ToInt32(Convert.ToSingle(v, CultureInfo.InvariantCulture)), null, false);

        public static int GetInt32Safe(this IDataRecord dataRecord, string column, int @default) =>
            dataRecord.GetValue(column, v => Convert.ToInt32(Convert.ToSingle(v, CultureInfo.InvariantCulture)), @default, false);

    }
}