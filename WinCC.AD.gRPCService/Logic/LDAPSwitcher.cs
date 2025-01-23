using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using LDAP;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WinCC.AD.gRPCService
{
    public class LDAPSwitcher : ILDAPSwitcher
    {
        private readonly LDAPConfigModel _config;
        private readonly ILogger<LDAPSwitcher> _logger;
        private int _invalidAttempts = 0;
        private int _serverId = 0;
        private int _checkInvalidAttempts = 3;

        public LDAPSwitcher(IOptions<LDAPConfigModel> config, ILogger<LDAPSwitcher> logger)
        {
            _config = config.Value;
            SetLDAPServer(0);
            _logger = logger;
        }

        public bool IsServiceMode
        {
            get 
            {
                return (_serverId == _config.Servers.Length - 1) && (_invalidAttempts >= _checkInvalidAttempts);
            }
        }

        public int InvalidAttempts
        {
            get
            {
                return _invalidAttempts;
            }
            set
            {
                Console.WriteLine($"[InvalidAttempts] Před: {_invalidAttempts}, Nová hodnota: {value}");
                if (value >= _checkInvalidAttempts)
                {
                    _serverId++;

                    Console.WriteLine($"[InvalidAttempts] Přepínám na nový server: {_serverId}");
                    // if _serverId is out off array, then _serverId is set last server in array
                    if (_serverId >= _config.Servers.Length)
                    {
                        SetLDAPServer(_config.Servers.Length - 1);
                        _invalidAttempts = _checkInvalidAttempts;
                        return;
                    }

                    SetLDAPServer(_serverId);
                    _invalidAttempts = 0;
                }
                else
                {
                    _invalidAttempts = value;
                }
            }
        }

        public void SetLDAPServer(int serverId)
        {
            _serverId = serverId;

            if (_serverId < 0 || _serverId >= _config.Servers.Length)
            {
                _serverId = 0;
            }

            LogonConfig.DomainName = _config.Servers[_serverId].DomainName;
            LogonConfig.Container = _config.Servers[_serverId].Container;
            LogonConfig.AdminName = _config.Servers[_serverId].AdminName;
            LogonConfig.AdminPassword = _config.Servers[_serverId].AdminPassword;
        }

        public int GetLDAPServer()
        {
            return _serverId;
        }
    }
}
