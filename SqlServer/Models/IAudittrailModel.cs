using System;

namespace SqlServer
{
    public interface IAudittrailModel
    {
        string ClientName { get; set; }
        string Event { get; set; }
        string FullName { get; set; }
        long Id { get; set; }
        string ServerName { get; set; }
        DateTime Ts { get; set; }
        string UserName { get; set; }
    }
}