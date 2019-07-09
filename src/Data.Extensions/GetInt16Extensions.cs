using System;
using System.Data;
using System.Globalization;
using Maestria.Data.Extensions.Internal;

namespace Maestria.Data.Extensions
{
    public static class GetInt16Extensions
    {
        public static int GetInt16(this IDataRecord dataRecord, string column) =>
            dataRecord.GetValue(column, v => Convert.ToInt16(Convert.ToSingle(v, CultureInfo.InvariantCulture)), default, true);

        public static int? GetInt16Safe(this IDataRecord dataRecord, string column) =>
            dataRecord.GetValueSafe(column, v => Convert.ToInt16(Convert.ToSingle(v, CultureInfo.InvariantCulture)), null, false);

        public static int GetInt16Safe(this IDataRecord dataRecord, string column, short @default) =>
            dataRecord.GetValue(column, v => Convert.ToInt16(Convert.ToSingle(v, CultureInfo.InvariantCulture)), @default, false);
    }
}