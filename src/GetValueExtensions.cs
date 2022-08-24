using System.Data;
using Maestria.Extensions.Data.Internal;

namespace Maestria.Extensions.Data
{
    public static class GetValueExtensions
    {
        public static object GetValue(
            this IDataRecord dataRecord, string column) =>
            dataRecord.GetValue(column, v => v);

        public static object GetValueSafe(
            this IDataRecord dataRecord, string column) =>
            dataRecord.GetValueSafeObject(column, v => v);

        public static object GetValueSafe(
            this IDataRecord dataRecord, string column, object @default) =>
            dataRecord.GetValueSafe(column) ?? @default;
    }
}