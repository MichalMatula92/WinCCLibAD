using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace LDAP
{
    public class LogonUserAccountModel : ILogonUserAccountModel
    {
        public string FirstName { get; set; } = "-";
        public string LastName { get; set; } = "-";
        public string UserName { get; set; } = "-";
        public string Description { get; set; } = "-";
        public string Office { get; set; } = "-";
        public string EmailAddress { get; set; } = "-";
        public string JobTitle { get; set; } = "-";
        public string Company { get; set; } = "-";
        public string Department { get; set; } = "-";
        public string DistinguishedName { get; set; } = "-";
        public DateTime? LastLogonDateTime { get; set; } = null;
        public DateTime? LastPasswordSet { get; set; } = null;
        public DateTime? WhenCreated { get; set; } = null;
        public DateTime? LastBadPasswordAttempt { get; set; } = null;
        public int DaysToExpiredPassword { get; set; } = 0;
        public bool LockedOut { get; set; } = false;
        public bool Disabled { get; set; } = false;
        public bool PasswordNeverExpires { get; set; } = false;
        public bool PasswordChangeNeeded { get; set; } = false;
        public bool PasswordExpired { get; set; } = false;
        public bool AccountExpired { get; set; } = false;
        public SecurityIdentifier Sid { get; set; } = null;
        public int UserAccountControl { get; set; } = 0;
        public List<string> MemberOf { get; set; } = new List<string>();
    }
}
