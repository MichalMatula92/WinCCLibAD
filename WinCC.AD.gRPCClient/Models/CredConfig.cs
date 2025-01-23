using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WinCC.AD.gRPCClient
{
    [ComVisible(true)]
    [Guid("D99599E3-FBD5-4C98-BD5E-FA3300DA9386")]
    [ProgId("WinCCLib.ADCredgRPC.ClientConfig")]
    [ClassInterface(ClassInterfaceType.None)]
    public class CredConfig
    {
        public string GRPCService
        {
            get { return Config.GRPCService; }
            set { Config.GRPCService = value; }
        }

        public string LogFilePath
        {
            get { return Config.LogFilePath; }
            set { Config.LogFilePath = value; }
        }
    }
}
