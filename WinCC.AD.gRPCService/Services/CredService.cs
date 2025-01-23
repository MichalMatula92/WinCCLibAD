using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Credential;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using LDAP;
using System.Runtime.Versioning;
using System.DirectoryServices.AccountManagement;
using Microsoft.Extensions.Options;
using Serilog.Core;
using SqlServer;

namespace WinCC.AD.gRPCService
{
    public class CredService : Cred.CredBase
    {
        private readonly ILogger<CredService> _logger;
        private readonly ILogonProcessor _logon;
        private readonly ILDAPSwitcher _switcher;
        private readonly ISqlProcessor _sql;
        private readonly LDAPConfigModel _config;

        public CredService(ILogger<CredService> logger, ILogonProcessor logon, IOptions<LDAPConfigModel> config, ILDAPSwitcher switcher, ISqlProcessor sql)
        {
            _logger = logger;
            _logon = logon;
            _switcher = switcher;
            _sql = sql;
            _config = config.Value;
        }
        
        public override Task<UserAccountModel> Login(RequestModel request, ServerCallContext context)
        {
            try
            {
                _logger.LogInformation($"Login '{request.UserName}' from '{request.Client}'");

                var ua = _logon.GetUserAccount(request.UserName);
                var reply = CreateUserAccountReply(ua);

                var isValid = _logon.ValidateCredentials(request.UserName, request.Password);
                reply.IsValid = isValid;

                if (isValid)
                {
                    _sql.AuditTrail(ua,request.Client, "Přihlášení OK");
                } 
                else
                {
                    _sql.AuditTrail(ua, request.Client, "Přihlášení NOK");
                }

                
                return Task.FromResult(reply);
            }
            catch (PrincipalServerDownException exc)
            {
                _sql.AuditTrailException(request, "Přihlášení NOK");
                _switcher.InvalidAttempts++;
                Console.WriteLine($"Počet neúspěšných pokusů Login Obecny: {_switcher.InvalidAttempts}");

                _logger.LogError(exc, "");
                throw new RpcException(new Status(StatusCode.Unavailable,$"Unable to connect to the LDAP server."));
            }
            catch (Exception exc)
            {
                _sql.AuditTrailException(request, "Přihlášení NOK");
                _switcher.InvalidAttempts++;
                Console.WriteLine($"Počet neúspěšných pokusů Login Obecny: {_switcher.InvalidAttempts}");
                _logger.LogError(exc, "");
                throw new RpcException(new Status(StatusCode.Internal,"Internal gRPC service error."));
            }
        }

        public override Task<UserAccountModel> Authentize(RequestModel request, ServerCallContext context)
        {
            try
            {
                _logger.LogInformation($"Authentize '{request.UserName}' from '{request.Client}'");

                var ua = _logon.GetUserAccount(request.UserName);
                var reply = CreateUserAccountReply(ua);

                var isValid = _logon.ValidateCredentials(request.UserName, request.Password);
                reply.IsValid = isValid;

                if (isValid)
                {
                    _sql.AuditTrail(ua, request.Client, "Ověření OK");
                }
                else
                {
                    _sql.AuditTrail(ua, request.Client, "Ověření NOK");
                }

                _switcher.InvalidAttempts = 0;
                return Task.FromResult(reply);
            }
            catch (PrincipalServerDownException exc)
            {
                _sql.AuditTrailException(request, "Ověření NOK");
                _switcher.InvalidAttempts++;
                Console.WriteLine($"Počet neúspěšných pokusů Authentize LDAP: {_switcher.InvalidAttempts}");
                _logger.LogError(exc, "");
                throw new RpcException(new Status(StatusCode.Unavailable, $"Unable to connect to the LDAP server."));
            }
            catch (Exception exc)
            {
                _sql.AuditTrailException(request, "Ověření NOK");
                _switcher.InvalidAttempts++;
                Console.WriteLine($"Počet neúspěšných pokusů Authentize Obecny: {_switcher.InvalidAttempts}");
                _logger.LogError(exc, "");
                throw new RpcException(new Status(StatusCode.Internal, "Internal gRPC service error."));
            }
        }

        public override Task<EmptyModel> Logoff(RequestModel request, ServerCallContext context)
        {
            LogonUserAccountModel ua = new() { UserName = request.UserName, FirstName = request.FirstName, LastName = request.LastName};

            _logger.LogInformation($"Logoff '{request.UserName}','{request.FirstName}','{request.LastName}'");
            _sql.AuditTrail(ua, request.Client, "Odhlášení OK");

            return Task.FromResult(new EmptyModel());
        }

        public override Task<EmptyModel> Autologoff(RequestModel request, ServerCallContext context)
        {
            LogonUserAccountModel ua = new() { UserName = request.UserName, FirstName = request.FirstName, LastName = request.LastName };
          
            _logger.LogInformation($"Autologoff '{request.UserName}','{request.FirstName}','{request.LastName}'");
            _sql.AuditTrail(ua, request.Client, "Automatické odhlášení OK");

            return Task.FromResult(new EmptyModel());
        }

        public override Task<EmptyModel> EmergencyLogin(RequestModel request, ServerCallContext context)
        {
            LogonUserAccountModel ua = new() { UserName = request.UserName, FirstName = request.FirstName, LastName = request.LastName };

            _logger.LogInformation($"EmergencyLogin '{request.UserName}','{request.FirstName}','{request.LastName}','{request.EventText}'");
            _sql.AuditTrail(ua, request.Client, request.EventText);

            return Task.FromResult(new EmptyModel());
        }

        public override Task<EmptyModel> SetADServer(LDAPServerModel request, ServerCallContext context)
        {
            try
            {
                _logger.LogInformation($"SetADServer '{request.ServerId}'");

                if (request.ServerId < 0 || request.ServerId >= _config.Servers.Length)
                {
                    throw new RpcException(new Status(StatusCode.InvalidArgument,$"Invalid argument, LDAP server Id ({request.ServerId})."));
                }
              
                _switcher.SetLDAPServer(request.ServerId);
                _switcher.InvalidAttempts = 0;
                return Task.FromResult(new EmptyModel());
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, "");
                throw new RpcException(new Status(StatusCode.Internal,"Internal gRPC service error."));
            }
        }

        public override Task<LDAPServerModel> GetADServer(EmptyModel request, ServerCallContext context)
        {
            //_logger.LogInformation($"GetADServer");
                
            var serverId = _switcher.GetLDAPServer();
            return Task.FromResult(new LDAPServerModel { ServerId = serverId });
        }

        public override Task<ServiceModel> ServiceMode(EmptyModel request, ServerCallContext context)
        {
            //_logger.LogInformation($"ServiceMode");
            return Task.FromResult(new ServiceModel { IsServiceMode = _switcher.IsServiceMode });
        }

        private UserAccountModel CreateUserAccountReply(ILogonUserAccountModel ua)
        {
            return new UserAccountModel
            {
                UserName = ua.UserName,
                FirstName = ua.FirstName,
                LastName = ua.LastName,
                GroupFlag = TranslatePrivileges(ua.MemberOf),
                DaysToExpiredPassword = ua.DaysToExpiredPassword,
                LockedOut = ua.LockedOut,
                Disabled = ua.Disabled,
                PasswordNeverExpires = ua.PasswordNeverExpires,
                PasswordChangeNeeded = ua.PasswordChangeNeeded,
                PasswordExpired = ua.PasswordExpired,
                AccountExpired = ua.AccountExpired,
            };
        }

        private int TranslatePrivileges(List<string> memberOf)
        {          
            int gflag = 0;
            foreach (var group in _config.Groups)
            {
                if (memberOf.Any(item => (item.Equals(group.Name))))
                {
                    gflag |= (1 << group.Bitn);
                }
            }

            return gflag;
        }
    }
}
