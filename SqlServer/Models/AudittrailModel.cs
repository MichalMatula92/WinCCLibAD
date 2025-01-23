using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlServer
{
    public class AudittrailModel : IAudittrailModel
    {
        public long Id { get; set; }
        public DateTime Ts { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string ClientName { get; set; }
        public string ServerName { get; set; }
        public string Event { get; set; }
    }
}
