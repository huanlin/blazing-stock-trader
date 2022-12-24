using System;
using System.Collections.Generic;
using System.Text;

namespace Huanlin.WebCrawler.Stock.Helpers
{
    public static class ConvertHelper
    {
        public static DateTime ParseRocDateString(string dateStr)
        {
            var parts = dateStr?.Split('/', '-', '.');
            if (parts == null || parts.Length < 3)
                throw new ArgumentException($"無效的日期字串: {dateStr}");

            int year = Convert.ToInt32(parts[0]) + 1911;
            int month = Convert.ToInt32(parts[1]);
            int day = Convert.ToInt32(parts[2]);
            return new DateTime(year, month, day);
        }

        private static string RemoveMoneyCharacters(string s)
        {
            return s.Replace(",", "").Replace("$", "").Trim();
        }

        public static int ToInt32(string str)
        {
            return Convert.ToInt32(RemoveMoneyCharacters(str));
        }

        public static long ToInt64(string str)
        {
            return Convert.ToInt64(RemoveMoneyCharacters(str));
        }

        public static double ToDouble(string str)
        {
            try
            {
                return Convert.ToDouble(RemoveMoneyCharacters(str));
            }
            catch (Exception ex)
            {
                throw new Exception($"無法轉換成 double： '{str}'", ex);
            }
        }

        public static double? ToDoubleNullable(string str)
        {
            double result;
            if (double.TryParse(RemoveMoneyCharacters(str), out result))
            {
                return result;
            }

            return null;
        }

        public static DateTime? ToDateTimeNullable(string str)
        {
            DateTime result;
            if (DateTime.TryParse(str, out result))
            {
                return result;
            }

            return null;
        }

        public static DateTime? ToDateTimeNullableForRoc(string str)
        {
            try
            {
                return ParseRocDateString(str);
            }
            catch
            {
                return null;
            }
        }

        public static int? ToInt32Nullable(string str)
        {
            try
            {
                return ToInt32(str);
            }
            catch
            {
                return null;
            }
        }

        public static long? ToInt64Nullable(string str)
        {
            try
            {
                return ToInt64(str);
            }
            catch
            {
                return null;
            }
        }

    }
}
