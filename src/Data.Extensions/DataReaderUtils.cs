using System;
using System.Data;

// ReSharper disable UnusedMember.Global

namespace Maestria.Data.Extensions
{
    public static class DataReaderUtils
    {
        #region Leitura padrão interna

        private static T? GetValueSafe<T>(this IDataRecord dataRecord, string columnName, Func<object, T> convertFunc,
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

        private static T GetValue<T>(this IDataRecord dataRecord, string columnName, Func<object, T> convertFunc,
            T @default, bool throwIfNull)
            where T : struct
        {
            var result = GetValueSafe(dataRecord, columnName, convertFunc, @default, throwIfNull);
            return result ?? @default;
        }

        private static T GetValueSafe<T>(this IDataRecord dataRecord, string columnName, Func<object, T> convertFunc,
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

        #endregion

        #region Int16 (Short)

        public static int GetInt16(this IDataRecord dataRecord, string column) =>
            GetValue(dataRecord, column, Convert.ToInt16, default, true);

        public static int? GetInt16Safe(this IDataRecord dataRecord, string column) =>
            GetValueSafe(dataRecord, column, Convert.ToInt16, null, false);

        public static int GetInt16Safe(this IDataRecord dataRecord, string column, short @default) =>
            GetValue(dataRecord, column, Convert.ToInt16, @default, false);

        #endregion

        #region Int32 (Int)

        public static int GetInt32(this IDataRecord dataRecord, string column) =>
            GetValue(dataRecord, column, Convert.ToInt32, default, true);

        public static int? GetInt32Safe(this IDataRecord dataRecord, string column) =>
            GetValueSafe(dataRecord, column, Convert.ToInt32, null, false);

        public static int GetInt32Safe(this IDataRecord dataRecord, string column, int @default) =>
            GetValue(dataRecord, column, Convert.ToInt32, @default, false);

        #endregion

        #region Int64 (Long)

        public static long GetInt64(this IDataRecord dataRecord, string column) =>
            GetValue(dataRecord, column, Convert.ToInt64, default, true);

        public static long? GetInt64Safe(this IDataRecord dataRecord, string column) =>
            GetValueSafe(dataRecord, column, Convert.ToInt64, null, false);

        public static long GetInt64Safe(this IDataRecord dataRecord, string column, long @default) =>
            GetValue(dataRecord, column, Convert.ToInt64, @default, false);

        #endregion

        #region Decimal

        public static decimal GetDecimal(this IDataRecord dataRecord, string column) =>
            GetValue(dataRecord, column, Convert.ToDecimal, default, true);

        public static decimal? GetDecimalSafe(this IDataRecord dataRecord, string column) =>
            GetValueSafe(dataRecord, column, Convert.ToDecimal, null, false);

        public static decimal GetDecimalSafe(this IDataRecord dataRecord, string column, decimal @default) =>
            GetValue(dataRecord, column, Convert.ToDecimal, @default, false);

        #endregion

        #region Float (Single)

        public static float GetFloat(this IDataRecord dataRecord, string column) =>
            GetValue(dataRecord, column, Convert.ToSingle, default, true);

        public static float? GetFloatSafe(this IDataRecord dataRecord, string column) =>
            GetValueSafe(dataRecord, column, Convert.ToSingle, null, false);

        public static float GetFloatSafe(this IDataRecord dataRecord, string column, float @default) =>
            GetValue(dataRecord, column, Convert.ToSingle, @default, false);

        #endregion

        #region Double

        public static double GetDouble(this IDataRecord dataRecord, string column) =>
            GetValue(dataRecord, column, Convert.ToDouble, default, true);

        public static double? GetDoubleSafe(this IDataRecord dataRecord, string column) =>
            GetValueSafe(dataRecord, column, Convert.ToDouble, null, false);

        public static double GetDoubleSafe(this IDataRecord dataRecord, string column, double @default) =>
            GetValue(dataRecord, column, Convert.ToDouble, @default, false);

        #endregion

        #region DateTime

        public static DateTime GetDateTime(this IDataRecord dataRecord, string column) =>
            GetValue(dataRecord, column, Convert.ToDateTime, default, true);

        public static DateTime? GetDateTimeSafe(this IDataRecord dataRecord, string column) =>
            GetValueSafe(dataRecord, column, Convert.ToDateTime, null, false);

        public static DateTime GetDateTimeSafe(this IDataRecord dataRecord, string column, DateTime @default) =>
            GetValue(dataRecord, column, Convert.ToDateTime, @default, false);

        #endregion

        #region String

        public static string GetString(this IDataRecord dataRecord, string column) =>
            GetValueSafe(dataRecord, column, Convert.ToString, null, true);

        public static string GetStringSafe(this IDataRecord dataRecord, string column) =>
            GetValueSafe(dataRecord, column, Convert.ToString, null, false);

        public static string GetStringSafe(this IDataRecord dataRecord, string column, string @default) =>
            GetValueSafe(dataRecord, column, Convert.ToString, @default, false);

        #endregion
    }
}
