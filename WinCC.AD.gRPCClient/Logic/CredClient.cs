using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Credential;
using Grpc.Core;
using System.Runtime.Remoting.Channels;
using System.Runtime.InteropServices;
using System.IO;
using Serilog;

namespace WinCC.AD.gRPCClient
{
    [ComVisible(true)]
    [Guid("CA794536-9A99-42A0-A8E6-0882E47CFAED")]
    [ProgId("WinCCLib.ADCredgRPC.Client")]
    [ClassInterface(ClassInterfaceType.None)]
    public class CredClient
    {
        private ILogger _logger = Logger.GetInstance();
        private Channel _channel;

        public string ClientName { get; set; } = Environment.MachineName;
        public bool IsError { get; private set; }
        public string ErrorMessage { get; private set; }

        public CredClient()
        {
            //_logger = new LoggerConfiguration()
            //    .MinimumLevel.Debug()
            //    .WriteTo.File(Config.LogFilePath
            //        ,outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {Message}{NewLine}{Exception}"
            //        ,fileSizeLimitBytes: 5242880
            //        ,rollOnFileSizeLimit: true
            //        ,retainedFileCountLimit: 10
            //        //,rollingInterval: RollingInterval.Day
            //        )
            //    .CreateLogger();

            _channel = new Channel(Config.GRPCService, ChannelCredentials.Insecure);
        }

        public bool Login(string userName, string password, ref WinCCUserAccount userAccount)
        {
            Initialize();
            var client = new Cred.CredClient(_channel);

            try
            {
                _logger.Information($"Login '{userName}'");

                var ua = client.Login(new RequestModel { Client = ClientName, UserName = userName, Password = password });              
                ConvertUserAccount(ref userAccount, ua);
                return ua.IsValid;
            }
            catch (RpcException exc)
            {
                SetError(exc);
                return false;
            }
        }

        public bool Authentize(string userName, string password, ref WinCCUserAccount userAccount)
        {
            Initialize();
            var client = new Cred.CredClient(_channel);

            try
            {
                _logger.Information($"Authentize '{userName}'");

                var ua = client.Authentize(new RequestModel { Client = ClientName, UserName = userName, Password = password });
                ConvertUserAccount(ref userAccount, ua);
                return ua.IsValid;
            }
            catch (RpcException exc)
            {
                SetError(exc);
                return false;
            }
        }

        public void Logoff(string userName, string firstName, string lastName)
        {
            Initialize();
            var client = new Cred.CredClient(_channel);

            try
            {
                _logger.Information($"Logoff '{userName}','{firstName}','{lastName}'");

                client.Logoff(new RequestModel { Client = ClientName, UserName = userName, FirstName = firstName, LastName = lastName });
            }
            catch (RpcException exc)
            {
                SetError(exc);
            }
        }

        public void AutoLogoff(string userName, string firstName, string lastName)
        {
            Initialize();
            var client = new Cred.CredClient(_channel);

            try
            {
                _logger.Information($"AutoLogoff '{userName}','{firstName}','{lastName}'");

                client.Autologoff(new RequestModel { Client = ClientName, UserName = userName, FirstName = firstName, LastName = lastName });
            }
            catch (RpcException exc)
            {
                SetError(exc);
            }
        }

        public void EmergencyLogin(string userName, string firstName, string lastName, string eventText)
        {
            Initialize();
            var client = new Cred.CredClient(_channel);

            try
            {
                _logger.Information($"EmergencyLogin '{userName}','{firstName}','{lastName}'");

                client.EmergencyLogin(new RequestModel { Client = ClientName, UserName = userName, FirstName = firstName, LastName = lastName, EventText = eventText });
            }
            catch (RpcException exc)
            {
                SetError(exc);
            }
        }

        public void SetADServer(int serverId)
        {
            Initialize();
            var client = new Cred.CredClient(_channel);

            try
            {
                _logger.Information($"SetADServer '{serverId}'");

                client.SetADServer(new LDAPServerModel { ServerId = serverId });               
            }
            catch (RpcException exc)
            {
                SetError(exc);
            }
        }

        public int GetADServer()
        {
            Initialize();
            var client = new Cred.CredClient(_channel);

            try
            {
                //_logger.Information($"GetADServer");

                var ldap = client.GetADServer(new EmptyModel());
                return ldap.ServerId;
            }
            catch (RpcException exc)
            {
                SetError(exc);
                return 0;
            }
        }

        public bool ServiceMode()
        {
            Initialize();
            var client = new Cred.CredClient(_channel);

            try
            {
                //_logger.Information($"ServiceMode");

                var mode = client.ServiceMode(new EmptyModel());
                return mode.IsServiceMode;
            }
            catch (RpcException exc)
            {
                SetError(exc);
                return false;
            }
        }

        private void ConvertUserAccount(ref WinCCUserAccount userAccount, UserAccountModel ua)
        {
            userAccount.Username = ua.UserName;
            userAccount.FirstName = ua.FirstName;
            userAccount.LastName = ua.LastName;
            userAccount.GroupFlag = ua.GroupFlag;
            userAccount.DaysToExpiredPassword = ua.DaysToExpiredPassword;
            userAccount.LockedOut = ua.LockedOut;
            userAccount.Disabled = ua.Disabled;
            userAccount.PasswordNeverExpires = ua.PasswordNeverExpires;
            userAccount.PasswordChangeNeeded = ua.PasswordChangeNeeded;
            userAccount.PasswordExpired = ua.PasswordExpired;
            userAccount.AccountExpired = ua.AccountExpired;
        }

        private void Initialize()
        {
            IsError = false;
            ErrorMessage = "-";
        }

        private void SetError(RpcException exc)
        {
            _logger.Error(exc, "RpcException");
            IsError = true;
            ErrorMessage = exc.ToString();
        }
    }
}
