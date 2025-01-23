namespace LDAP
{
    public interface ILogonProcessor
    {
        bool ValidateCredentials(string userName, string password);
        ILogonUserAccountModel GetUserAccount(string userName);
    }
}