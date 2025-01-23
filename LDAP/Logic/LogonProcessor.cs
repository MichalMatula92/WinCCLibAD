using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Runtime.Versioning;

namespace LDAP
{
    [SupportedOSPlatform("windows")]
    public class LogonProcessor : ILogonProcessor
    {
        private readonly ILogger<LogonProcessor> _logger;
 
        public LogonProcessor(ILogger<LogonProcessor> logger)
        {
            _logger = logger;
        }

        public bool ValidateCredentials(string userName, string password)
        {
            using (var pc = CreateContext())
            {
                var valid = pc.ValidateCredentials(userName, password);
                return valid;
            }
        }

        public ILogonUserAccountModel GetUserAccount(string userName)
        {
            var ua = new LogonUserAccountModel();

            using (var pc = CreateContext()) 
            {                       
                using (var user = UserPrincipal.FindByIdentity(pc, userName))
                {
                    if (user == null)
                    {                       
                        return ua;
                    }

                    ua.FirstName = user.GivenName;
                    ua.LastName = user.Surname;
                    ua.Description = user.Description;
                    ua.Disabled = (user.Enabled.HasValue && user.Enabled.Value == false);
                    ua.LockedOut = user.IsAccountLockedOut();
                    ua.PasswordNeverExpires = user.PasswordNeverExpires;
                    ua.LastLogonDateTime = user.LastLogon;
                    ua.EmailAddress = user.EmailAddress;
                    ua.Sid = user.Sid;
                    ua.UserName = user.SamAccountName;
                    ua.DistinguishedName = user.DistinguishedName;
                    ua.LastPasswordSet = user.LastPasswordSet;
                    ua.PasswordChangeNeeded = (user.LastPasswordSet == null);
                    ua.LastBadPasswordAttempt = user.LastBadPasswordAttempt;

                    if (user.AccountExpirationDate.HasValue)
                        ua.AccountExpired = DateTime.Compare((DateTime)user.AccountExpirationDate, DateTime.Now) <= 0;
                    else
                        ua.AccountExpired = false;

                    ua.MemberOf = GetAuthorizationGroups(user);

                    using (var directoryEntry = user.GetUnderlyingObject() as DirectoryEntry)
                    {
                        if (directoryEntry == null)
                        {
                            return ua;
                        }

                        var company = directoryEntry.Properties["company"].Value;
                        ua.Company = (company == null) ? "-" : company.ToString();

                        var department = directoryEntry.Properties["department"].Value;
                        ua.Department = (department == null) ? "-" : department.ToString();

                        var title = directoryEntry.Properties["title"].Value;
                        ua.JobTitle = (title == null) ? "-" : title.ToString();
         
                        var physicalDeliveryOfficeName = directoryEntry.Properties["physicalDeliveryOfficeName"].Value;
                        ua.Office = (physicalDeliveryOfficeName == null) ? "-" : physicalDeliveryOfficeName.ToString();

                        var userAccountControl = directoryEntry.Properties["userAccountControl"].Value;
                        ua.UserAccountControl = (userAccountControl == null) ? 0 : (int)userAccountControl;

                        var whenCreated = directoryEntry.Properties["whenCreated"].Value;
                        ua.WhenCreated = (whenCreated == null) ? null : Convert.ToDateTime(whenCreated);
                      
                        ActiveDs.IADsUser native = (ActiveDs.IADsUser)directoryEntry.NativeObject;
                        DateTime passwordExpirationDate = native.PasswordExpirationDate;
                        ua.PasswordExpired = (DateTime.Compare(passwordExpirationDate, DateTime.Now) <= 0);

                        if (ua.PasswordExpired)
                            ua.DaysToExpiredPassword = 0;
                        else
                        {
                            TimeSpan ts = passwordExpirationDate - DateTime.Now;
                            ua.DaysToExpiredPassword = (int)ts.TotalDays;
                        }

                        return ua;
                    }
                }
            }
        }

        private List<string> GetAuthorizationGroups(UserPrincipal user)
        {
            var memberOf = new List<string>();
            PrincipalSearchResult<Principal> groups = user.GetAuthorizationGroups();
            var iterGroup = groups.GetEnumerator();
            while (iterGroup.MoveNext())
            {
                memberOf.Add(iterGroup.Current.Name);
            }

            return memberOf;
        }

        private PrincipalContext CreateContext()
        {
            return new PrincipalContext(
                ContextType.Domain, 
                LogonConfig.DomainName, 
                LogonConfig.Container, 
                LogonConfig.AdminName, 
                LogonConfig.AdminPassword);
        }
    }
}
