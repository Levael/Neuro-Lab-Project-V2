using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MoogOcus
{
    public static class Tools
    {
        /// <summary>
        /// TODO: delete, it's temporary
        /// Translate Dictionary to String
        /// TBH, no clue how it works
        /// </summary>
        /// <typeparam name="S">key type</typeparam>
        /// <typeparam name="T">value type</typeparam>
        /// <param name="dict">dictionary</param>
        /// <returns></returns>
        public static string Stringify<S, T>(Dictionary<S, T> dict)
        {
            string output = "";

            foreach (var row in dict)
            {
                var obj = row.Value;

                foreach (PropertyInfo inf in obj.GetType().GetProperties())
                {
                    output += $"{inf.Name}: ";
                    output += $"{inf.GetValue(obj) ?? "null"}; ";
                }
                output += "\r\n";
            }

            return output;
        }

        public static string[,] DictionaryToMultidimensionalArray (Dictionary<string, Parameter> dict)
        {
            string[,] result = new string[dict.Count, typeof(Parameter).GetProperties().Length-1];
            int i = 0;

            foreach (var row in dict)
            {
                // temp
                result[i, 0] = $"{row.Value.name}";
                result[i, 1] = $"{row.Value.nice_name}";
                result[i, 2] = $"{row.Value.type}";
                result[i, 3] = $"{row.Value.editable}";
                result[i, 4] = $"{row.Value.value}";
                result[i, 5] = $"{row.Value.low_bound}";
                result[i, 6] = $"{row.Value.high_bound}";
                result[i, 7] = $"{row.Value.increment}";
                i++;

                /*var obj = row.Value;

                foreach (PropertyInfo inf in obj.GetType().GetProperties())
                {
                    result[,] = $"{inf.GetValue(obj) ?? "null"}";
                }*/
            }

            return result;
        }

        public static string[,] DictionaryToSingleDimensionalArray(Dictionary<string, Parameter> dict)
        {
            string[,] result = new string[dict.Count, typeof(Parameter).GetProperties().Length - 1];
            int i = 0;

            foreach (var row in dict)
            {
                // temp
                result[i, 0] = $"{row.Value.name}";
                result[i, 1] = $"{row.Value.nice_name}";
                result[i, 2] = $"{row.Value.type}";
                result[i, 3] = $"{row.Value.editable}";
                result[i, 4] = $"{row.Value.value}";
                result[i, 5] = $"{row.Value.low_bound}";
                result[i, 6] = $"{row.Value.high_bound}";
                result[i, 7] = $"{row.Value.increment}";
                i++;

                /*var obj = row.Value;

                foreach (PropertyInfo inf in obj.GetType().GetProperties())
                {
                    result[,] = $"{inf.GetValue(obj) ?? "null"}";
                }*/
            }

            return result;
        }

    }
}
