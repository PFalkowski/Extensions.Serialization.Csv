using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Extensions.Serialization.Csv
{
    public static class Utilities
    {
        private static string ToCsvLine<T>(this IEnumerable<T> input, char separator = ',', char? quotation = null)
        {
            if (input == null) return null;
            var csv = new StringBuilder();
            foreach (var value in input)
            {
                csv.AppendFormat("{0}{1}{2}{3}", quotation, value, quotation, separator);
            }
            if (csv.Length != 0)
                return csv.ToString(0, csv.Length - 1);
            return string.Empty;
        }

        private static string ToCsvLine<T>(this IEnumerable<T> input, CultureInfo info, string format = "G",
            char separator = ',', char? quotation = null) where T : IFormattable
        {
            if (input == null) return null;
            var csv = new StringBuilder();
            foreach (var value in input)
            {
                csv.AppendFormat("{0}{1}{2}{3}", quotation, value.ToString(format, info), quotation, separator);
            }
            if (csv.Length != 0)
                return csv.ToString(0, csv.Length - 1);
            return string.Empty;
        }

        public static string SerializeToCsv<T>(this IEnumerable<T> input, string separator = ",", char? quotation = null, CultureInfo info = null)
        {
            return SerializeToCsv(input, null, separator, quotation, info);
        }

        public static string SerializeToCsv<T>(this IEnumerable<T> input, ClassMap<T> propertiesMap, string separator = ",",
            char? quotation = null, CultureInfo info = null)
        {
            if (input == null) return null;
            var stb = new StringBuilder();
            using (var writer = new CsvWriter(new StringWriter(stb), false))
            {
                writer.Configuration.Delimiter = separator;
                writer.Configuration.SanitizeForInjection = true;
                if (quotation.HasValue)
                {
                    writer.Configuration.QuoteAllFields = true;
                    writer.Configuration.Quote = quotation.Value;
                }
                else
                {
                    writer.Configuration.QuoteAllFields = false;
                }
                if (info != null)
                {
                    writer.Configuration.CultureInfo = info;
                }

                if (propertiesMap != null)
                {
                    writer.Configuration.RegisterClassMap(propertiesMap);
                }
                writer.WriteRecords(input);
                writer.Flush();
            }
            return stb.ToString();
        }

        public static IEnumerable<T> DeserializeFromCsv<T>(this string csvContents, ClassMap<T> propertiesMap = null, CultureInfo info = null)
        {
            IEnumerable<T> result;
            using (var csv = new CsvReader(new StringReader(csvContents), false))
            {
                if (propertiesMap != null) { csv.Configuration.RegisterClassMap(propertiesMap); }
                if (info != null) { csv.Configuration.CultureInfo = info; }
                result = csv.GetRecords<T>().ToList();
            }
            return result;
        }
    }
}
