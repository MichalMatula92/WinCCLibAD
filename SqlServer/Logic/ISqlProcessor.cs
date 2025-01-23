using Credential;
using LDAP;

namespace SqlServer
{
    public interface ISqlProcessor
    {
        void AuditTrail(ILogonUserAccountModel ua, string client, string eventText);
        void AuditTrailException(RequestModel request, string eventText);
    }
}