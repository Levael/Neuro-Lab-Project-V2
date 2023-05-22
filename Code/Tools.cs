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

        /// <summary>
        /// Converts a 2D array of objects to 2D dictionary of string type.
        /// </summary>
        /// <param name="array">The 2D object array.</param>
        /// <returns>The 2d string dictionary.</returns>
        public static Dictionary<string, Dictionary<string, string>> Convert2DObjectsTo2DStringDictionary(object[,] array)
        {
            // first value is rows, second -- columns
            // i starts from 2 becuase of: 1) +1 -- Excel starts from 1 and not 0, 2) +1 -- first row is columns names
            // j starts from 2 becuase of: 1) +1 -- Excel starts from 1 and not 0, 2) +1 -- first column is parameters  names

            var result = new Dictionary<string, Dictionary<string, string>>();

            for (int i = 2; i <= array.GetLength(0); i++)
            {
                var parameter_name = array[i, 1].ToString();
                result.Add(parameter_name, new Dictionary<string, string>());

                for (int j = 2; j <= array.GetLength(1); j++)
                {
                    var parameter_attribute_name = array[1, j].ToString();
                    var parameter_attribute_value = array[i, j].ToString();

                    result[parameter_name].Add(parameter_attribute_name, parameter_attribute_value);
                }
            }

            return result;
        }

        /*public static string[,] DictionaryToSingleDimensionalArray(Dictionary<string, Parameter> dict)
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

                var obj = row.Value;

                foreach (PropertyInfo inf in obj.GetType().GetProperties())
                {
                    result[,] = $"{inf.GetValue(obj) ?? "null"}";
                }
            }

            return result;
        }*/

        /*public static string[,] DictionaryToMultidimensionalArray (Dictionary<string, Parameter> dict)
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

                *//*var obj = row.Value;

                foreach (PropertyInfo inf in obj.GetType().GetProperties())
                {
                    result[,] = $"{inf.GetValue(obj) ?? "null"}";
                }*//*
            }

            return result;
        }*/

        /*public static string[,] DictionaryToSingleDimensionalArray(Dictionary<string, Parameter> dict)
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

                *//*var obj = row.Value;

                foreach (PropertyInfo inf in obj.GetType().GetProperties())
                {
                    result[,] = $"{inf.GetValue(obj) ?? "null"}";
                }*//*
            }

            return result;
        }*/



    }
}
