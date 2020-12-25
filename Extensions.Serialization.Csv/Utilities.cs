using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Extensions.Serialization.Csv
{
    public static class Utilities
    {
        /// <summary>
        /// Serializes <paramref name="input"/> to CSV using culture <paramref name="info"/> specified or invariant culture if null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input">The input.</param>
        /// <param name="separator">The separator.</param>
        /// <param name="quotation">The quotation.</param>
        /// <param name="info">The information.</param>
        /// <returns></returns>
        public static string SerializeToCsv<T>(this IEnumerable<T> input, string separator = ",", char? quotation = null, CultureInfo info = null)
        {
            return SerializeToCsv(input, null, separator, quotation, info);
        }

        // TODO: Async version        
        /// <summary>
        /// Serializes <paramref name="input"/> to CSV using culture <paramref name="info"/> specified or invariant culture if null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input">The input.</param>
        /// <param name="propertiesMap">The properties map.</param>
        /// <param name="separator">The separator.</param>
        /// <param name="quotation">The quotation.</param>
        /// <param name="info">The information.</param>
        /// <returns></returns>
        public static string SerializeToCsv<T>(this IEnumerable<T> input, ClassMap<T> propertiesMap, string separator = ",",
            char? quotation = null, CultureInfo info = null)
        {
            if (input == null) return null;
            info ??= CultureInfo.InvariantCulture;
            var stb = new StringBuilder();
            using var writer = new CsvWriter(new StringWriter(stb), info);
            writer.Configuration.Delimiter = separator;
            writer.Configuration.SanitizeForInjection = true;
            if (quotation.HasValue)
            {
                writer.Configuration.ShouldQuote = (s, context) => true;
                writer.Configuration.Quote = quotation.Value;
            }

            if (propertiesMap != null)
            {
                writer.Configuration.RegisterClassMap(propertiesMap);
            }
            writer.WriteRecords(input);
            writer.Flush();

            return stb.ToString();
        }

        // TODO: Async version        
        /// <summary>
        /// Deserializes <paramref name="input"/> from CSV using culture <paramref name="info"/> specified or invariant culture if null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input">The CSV contents.</param>
        /// <param name="propertiesMap">The properties map.</param>
        /// <param name="info">The information.</param>
        /// <returns></returns>
        public static IEnumerable<T> DeserializeFromCsv<T>(this string input, ClassMap<T> propertiesMap = null, CultureInfo info = null)
        {
            info ??= CultureInfo.InvariantCulture;
            using var csv = new CsvReader(new StringReader(input), info);
            if (propertiesMap != null) { csv.Configuration.RegisterClassMap(propertiesMap); }
            IEnumerable<T> result = csv.GetRecords<T>().ToList();

            return result;
        }
    }
}
