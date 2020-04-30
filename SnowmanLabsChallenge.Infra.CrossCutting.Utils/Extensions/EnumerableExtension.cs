namespace SnowmanLabsChallenge.Infra.CrossCutting.Utils.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    public static class EnumerableExtension
    {
        public static string ToCsv<T>(this IEnumerable<T> data)
        {
            return ToCsv(data, ",");
        }

        public static string ToCsv<T>(this IEnumerable<T> objects, string separator)
        {
            Type t = typeof(T);
            PropertyInfo[] properties = t.GetProperties();

            var header = string.Join(separator, properties.Select(f => f.Name).ToArray());

            var csvdata = new StringBuilder();
            csvdata.AppendLine(header);

            foreach (var o in objects)
            {
                csvdata.AppendLine(ToCsvFields(separator, properties, o));
            }

            return csvdata.ToString();
        }

        private static string ToCsvFields(string separator, PropertyInfo[] properties, object o)
        {
            StringBuilder line = new StringBuilder();

            foreach (var f in properties)
            {
                if (line.Length > 0)
                {
                    line.Append(separator);
                }

                var x = f.GetValue(o);

                if (x != null)
                {
                    line.Append(x.ToString());
                }
            }

            return line.ToString();
        }
    }
}
