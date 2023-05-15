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
                    output += $"{inf.GetValue(obj) ?? "null"};";
                }
                output += "\r\n";
            }

            return output;
        }

    }
}
