using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCU
{
    public static class CustomConfig
    {
        public static HashSet<string> ignorePrintingExcelColumns = new() { "description", "nice_name" };

        public static HashSet<string> ignorePrintingExcelRows = new() {};

        public static string defaultProtocolsDirPath = @"C:\Users\user\Documents\GitHub\V2_Levael\Protocols\";
    }
}
