using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MOCU
{
    public static class MoogHandler
    {
        [DllImport(@"\MoogController.dll")]
        public static extern void Connect();


        [DllImport(@"\MoogController.dll")]
        public static extern void Engage();


        [DllImport(@"\MoogController.dll")]
        public static extern void Disengage();


        [DllImport(@"\MoogController.dll")]
        public static extern void Disconnect();


        [DllImport(@"\MoogController.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SendPosition(double surge, double heave, double lateral, double yaw, double roll, double pitch);
    }
}
