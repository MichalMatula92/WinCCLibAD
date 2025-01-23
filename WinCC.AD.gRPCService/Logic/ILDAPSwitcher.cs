namespace WinCC.AD.gRPCService
{
    public interface ILDAPSwitcher
    {
        int InvalidAttempts { get; set; }
        bool IsServiceMode { get; }

        int GetLDAPServer();
        void SetLDAPServer(int serverId);
    }
}