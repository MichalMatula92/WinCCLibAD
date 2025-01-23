using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WinCC.AD.gRPCService
{
    public class LDAPServerConnectionModel
    {
        public string DomainName { get; set; }
        public string Container { get; set; }
        public string AdminName { get; set; }
        public string AdminPassword { get; set; }
    }
}
