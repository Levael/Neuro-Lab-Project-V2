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
        // DLL is 32b...
        // Connect successfully, but no cabel connected...
        // wtf



        //[DllImport(@"\MoogController.dll")]
        [DllImport(@"C:\Users\user\Documents\GitHub\V2_Levael\Code\Moog\MoogController.dll")]
        public static extern void Connect();


        [DllImport(@"C:\Users\user\Documents\GitHub\V2_Levael\Code\Moog\MoogController.dll")]
        public static extern void Engage();


        [DllImport(@"C:\Users\user\Documents\GitHub\V2_Levael\Code\Moog\MoogController.dll")]
        public static extern void Disengage();


        [DllImport(@"C:\Users\user\Documents\GitHub\V2_Levael\Code\Moog\MoogController.dll")]
        public static extern void Disconnect();


        [DllImport(@"C:\Users\user\Documents\GitHub\V2_Levael\Code\Moog\MoogController.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SendPosition(double surge, double heave, double lateral, double yaw, double roll, double pitch);
    }
}
