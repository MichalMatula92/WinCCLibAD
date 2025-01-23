using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace LDAP
{
    public interface ILogonUserAccountModel
    {
        bool AccountExpired { get; set; }
        string Company { get; set; }
        int DaysToExpiredPassword { get; set; }
        string Department { get; set; }
        string Description { get; set; }
        bool Disabled { get; set; }
        string DistinguishedName { get; set; }
        string EmailAddress { get; set; }
        string FirstName { get; set; }
        string JobTitle { get; set; }
        DateTime? LastBadPasswordAttempt { get; set; }
        DateTime? LastLogonDateTime { get; set; }
        string LastName { get; set; }
        DateTime? LastPasswordSet { get; set; }
        bool LockedOut { get; set; }
        List<string> MemberOf { get; set; }
        string Office { get; set; }
        bool PasswordExpired { get; set; }
        bool PasswordChangeNeeded { get; set; }
        bool PasswordNeverExpires { get; set; }
        SecurityIdentifier Sid { get; set; }
        int UserAccountControl { get; set; }
        string UserName { get; set; }
        DateTime? WhenCreated { get; set; }
    }
}