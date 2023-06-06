using System;
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
            WinApi.TimeBeginPeriod(1);
            Application.Run(new GUI());
            WinApi.TimeEndPeriod(1);
        }

        // Global ToDo:
        //
        // 1) Moog.Connect doesn't return 'false' when is not connected, always 'true', not good
        // 2) Add ApplicationCloseEvent to tottaly all excel background tasks
        // 3) If there is any changes in DataGridView table -> make 'save' btn edible => add 'SaveParameters' function
        // 4) Add scrollbars to textboxes
    }
}
