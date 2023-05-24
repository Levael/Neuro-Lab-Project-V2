using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MOCU
{
    public static class InfoPrinter
    {

        /*private static void PrintText (string text)
        {

        }*/
        public static void PrintWarning (Dictionary<string, Control> dict, string text)
        {
            dict["WARNINGS"].Text += text;
            dict["INFO"].Text += "\r\n";
        }

        public static void PrintInfo(Dictionary<string, Control> dict, string text)
        {
            dict["INFO"].Text += text;
            dict["INFO"].Text += "\r\n";
        }
    }
}
