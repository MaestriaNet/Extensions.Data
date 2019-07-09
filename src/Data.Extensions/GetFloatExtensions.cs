using System;
using System.Data;
using Maestria.Data.Extensions.Internal;

namespace Maestria.Data.Extensions
{
    public static class GetFloatExtensions
    {
        public static float GetFloat(this IDataRecord dataRecord, string column) =>
            dataRecord.GetValue(column, Convert.ToSingle, default, true);

        public static float? GetFloatSafe(this IDataRecord dataRecord, string column) =>
            dataRecord.GetValueSafe(column, Convert.ToSingle, null, false);

        public static float GetFloatSafe(this IDataRecord dataRecord, string column, float @default) =>
            dataRecord.GetValue(column, Convert.ToSingle, @default, false);
    }
}