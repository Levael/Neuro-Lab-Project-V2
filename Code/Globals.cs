using System.Collections.Generic;
using System.Drawing;

namespace MOCU
{
    public static class Globals {
        public static SystemState systemState;

        public static bool isConnected = false;
        public static bool isEngaged = false;

        public static CedrusResponse lastResponse = CedrusResponse.WAIT_FOR_START;
    }


    public enum SystemState
    {
        /// <summary>
        /// The system is running now.
        /// </summary>
        RUNNING = 0,

        /// <summary>
        /// The system has been stopped by the user.
        /// </summary>
        STOPPED = 1,

        /// <summary>
        /// The system has been paused by the user.
        /// </summary>
        PAUSED = 2,

        /// <summary>
        /// The system is now warmed up.
        /// </summary>
        INITIALIZED = 4,

        /// <summary>
        /// The current experiment (all trials) over, waiting for the next command.
        /// </summary>
        FINISHED = 5
    }

    public enum CedrusResponse
    {
        START   = 0,
        LEFT    = 1,
        RIGHT   = 2,
        UP      = 3,
        DOWN    = 4,

        NOTHING = 5,
        WAIT_FOR_START = 6
    }
}
