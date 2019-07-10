using System;
using System.Data;
using System.Data.SqlTypes;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Maestria.Data.Extensions.Test")]
namespace Maestria.Data.Extensions.Internal
{
    internal static class DataReaderExtensions
    {
        public static T GetValue<T>(this IDataRecord dataRecord, string columnName, Func<object, T> convertFunction)
        {
            var index = dataRecord.GetOrdinal(columnName);
            if (index == -1)
                throw new IndexOutOfRangeException($"Invalid data field name \"{columnName}\".");

            if (dataRecord.IsDBNull(index))
                throw new SqlNullValueException($"Null value for field \"{columnName}\".");

//            var value = dataRecord.GetValue(index);
            return convertFunction(dataRecord.GetValue(index));
        }

        public static T? GetValueSafe<T>(this IDataRecord dataRecord, string columnName, Func<object, T> convertFunction)
            where T : struct
        {
            var index = dataRecord.GetOrdinal(columnName);
            if (index == -1)
                throw new IndexOutOfRangeException($"Invalid data field name \"{columnName}\".");

            if (dataRecord.IsDBNull(index))
                return null;

            var value = dataRecord.GetValue(index);
            try
            {
                return convertFunction(value);
            }
            catch
            {
                return null;
            }
        }

        public  static T GetValueSafeObject<T>(this IDataRecord dataRecord, string columnName, Func<object, T> convertFunction)
            where T : class
        {
            var index = dataRecord.GetOrdinal(columnName);
            if (index == -1)
                throw new IndexOutOfRangeException($"Invalid data field name \"{columnName}\".");

            if (dataRecord.IsDBNull(index))
                return null;

            var value = dataRecord.GetValue(index);
            try
            {
                return convertFunction(value);
            }
            catch
            {
                return null;
            }
        }
    }
}