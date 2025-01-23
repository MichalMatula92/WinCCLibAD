using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinCC.AD.gRPCClient
{
    public static class Config
    {
        public static string GRPCService { get; set; } = "";
        public static string LogFilePath { get; set; } = "";
    }
}
