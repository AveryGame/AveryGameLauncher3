using System;
using System.Collections.Generic;
using System.Text;

namespace AgsLauncherV3.Services
{
    internal class Enums
    {
        public enum LauncherStatus
        {
            initializing,
            rpcInitialized
        }
        public static LauncherStatus status;
    }
}
