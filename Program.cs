using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MOCU
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        ///  MOCU = Moog Oculus Cedrus Unity
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GUI());
        }

        // Global ToDo:
        //
        // 1) Moog.Connect doesn't return 'false' when is not connected, always 'true', not good
        // 2) Add ApplicationCloseEvent to tottaly all excel background tasks
        // 3) If there is any changes in DataGridView table -> make 'save' btn edible => add 'SaveParameters' function
    }
}
