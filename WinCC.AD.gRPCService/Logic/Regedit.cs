using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Win32;

namespace WinCC.AD.gRPCService
{
    [SupportedOSPlatform("windows")]
    public static class Regedit
    {
        private static string _subKey = "Software\\Taurid\\WinCC.AD.gRPCService";
            
        public static T GetValue<T>(string name, T defaultValue)
        {                              
            return (T)Registry.LocalMachine.OpenSubKey(_subKey).GetValue(name, defaultValue);                        
        }
    }
}
