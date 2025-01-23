using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WinCC.AD.gRPCService
{
    public class LDAPConfigModel
    {
        public LDAPServerConnectionModel[] Servers { get; set; }
        public GroupModel[] Groups { get; set; }
    }
}
