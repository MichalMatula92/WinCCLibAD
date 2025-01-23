using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDAP
{
    public static class LogonConfig
    {
        public static string DomainName { get; set; }
        public static string Container { get; set; }
        public static string AdminName { get; set; }
        public static string AdminPassword { get; set; }
    }
}
