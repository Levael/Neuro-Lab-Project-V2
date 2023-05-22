using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoogOcus
{
    public static class CustomConfig
    {
        public static HashSet<string> ignorePrintingExcelColumns = new(){ "description", "nice_name" };

        public static HashSet<string> ignorePrintingExcelRows = new() {};
    }
}
