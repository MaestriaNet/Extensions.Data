using System;
using System.Data;

namespace Maestria.Data.Extensions.Internal
{
    public static class DataReaderExtensions
    {
        public static T GetValue<T>(this IDataRecord dataRecord, string columnName, Func<object, T> convertFunc,
            T @default, bool throwIfNull)
            where T : struct
        {
            var result = GetValueSafe(dataRecord, columnName, convertFunc, @default, throwIfNull);
            return result ?? @default;
        }

        public static T? GetValueSafe<T>(this IDataRecord dataRecord, string columnName, Func<object, T> convertFunc,
            T? @default, bool throwIfNull)
            where T : struct
        {
            var columnIndex = dataRecord.GetOrdinal(columnName);
            if (columnIndex == -1)
                throw new Exception($"Coluna {columnName} não existe no data reader!");

            if (dataRecord.IsDBNull(columnIndex))
            {
                if (throwIfNull)
                    throw new Exception($"Valor nulo para coluna {columnName}!");
                return @default;
            }

            return convertFunc(dataRecord.GetValue(columnIndex));
        }

        public  static T GetValueSafe<T>(this IDataRecord dataRecord, string columnName, Func<object, T> convertFunc,
            T @default, bool throwIfNull)
            where T : class
        {
            var columnIndex = dataRecord.GetOrdinal(columnName);
            if (columnIndex == -1)
                throw new Exception($"Coluna {columnName} não existe no data reader!");

            if (dataRecord.IsDBNull(columnIndex))
            {
                if (throwIfNull)
                    throw new Exception($"Valor nulo para coluna {columnName}!");
                return @default;
            }

            return convertFunc(dataRecord.GetValue(columnIndex));
        }
    }
}