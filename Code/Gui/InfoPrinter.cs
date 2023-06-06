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

        public static void PrintTextOfType(string type, string text)
        {
            GUI.textboxesDictionary[type.ToUpper()].Text += $"{text}\r\n";
        }

        /*public static void PrintWarning (string text)
        {
            PrintText("WARNINGS", text);
        }

        public static void PrintInfo(string text)
        {
            PrintText("INFO", text);
        }*/
    }
}
