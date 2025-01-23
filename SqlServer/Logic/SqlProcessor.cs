using Credential;
using LDAP;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlServer
{
    public class SqlProcessor : ISqlProcessor
    {
        private readonly ILogger<SqlProcessor> _logger;
        private readonly ISqlDataAccess _sql;

        public SqlProcessor(ILogger<SqlProcessor> logger, ISqlDataAccess sql)
        {
            _logger = logger;
            _sql = sql;
        }

        public void AuditTrail(ILogonUserAccountModel ua, string client, string eventText)
        {           
            AudittrailModel audittrail = new()
            {
                UserName = ua.UserName,
                FullName = $"{ua.FirstName} {ua.LastName}",
                ClientName = client,
                ServerName = LogonConfig.DomainName,
                Event = eventText,
            };

            string sql = $@"insert into ad_audit_trail
            (ts, user_name, full_name, client_name, server_name, event)
                values
            (sysdatetime(), @UserName, @FullName, @ClientName, @ServerName, @Event)";

            try
            {
                _sql.Execute(sql, audittrail);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, "");
            }
        }

        public void AuditTrailException(RequestModel request, string eventText)
        {         
            AudittrailModel audittrail = new()
            {
                UserName = request.UserName,
                FullName = "-",
                ClientName = request.Client,
                ServerName = LogonConfig.DomainName,
                Event = eventText,
            };
            
            string sql = $@"insert into ad_audit_trail
            (ts, user_name, full_name, client_name, server_name, event)
                values
            (sysdatetime(), @UserName, @FullName, @ClientName, @ServerName, @Event)";

            try
            {
                _sql.Execute(sql, audittrail);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, "");
            }
        }
    }
}
