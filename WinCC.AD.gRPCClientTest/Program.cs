using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinCC.AD.gRPCClient;
using TestSerilog;
using System.Threading;

namespace WinCC.AD.gRPCClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //var mylog = new MyLogger();

                //mylog.Info("test");
                var conf = new CredConfig();
                conf.GRPCService = "192.168.128.3:50000";
                conf.LogFilePath = "C:\\workspace\\C#\\WinCCLibAD\\Logs\\WinCCLib.ADCredgRPC.Client.log";

                var cred = new CredClient();
                var ua = new WinCCUserAccount();

                //cred.SetADServer(1);
                //Console.WriteLine($"serverId-{cred.GetADServer()}");
                //cred.SetADServer(2);
                //Console.WriteLine($"serverId-{cred.GetADServer()}");
                for (int i = 0; i < 10000; i++)
                {
                    var valid = cred.Login("lcernoch", "Taurid1*", ref ua);

                    Console.WriteLine($"IsValid-{valid}-{ua}");
                    if (cred.IsError)
                    {
                        Console.WriteLine(cred.ErrorMessage);
                    }

                    Thread.Sleep(2000);
                }


                //Console.WriteLine($"Username-{ua.Username}");
                //Console.WriteLine($"FirstName-{ua.FirstName}");
                //Console.WriteLine($"LastName-{ua.LastName}");
                //Console.WriteLine($"GroupFlag-{ua.GroupFlag}");
                //Console.WriteLine($"DaysToExpiredPassword-{ua.DaysToExpiredPassword}");
                //Console.WriteLine($"LockedOut-{ua.LockedOut}");
                //Console.WriteLine($"Disabled-{ua.Disabled}");
                //Console.WriteLine($"PasswordNeverExpires-{ua.PasswordNeverExpires}");
                //Console.WriteLine($"PasswordChangeNeeded-{ua.PasswordChangeNeeded}");
                //Console.WriteLine($"PasswordExpired-{ua.PasswordExpired}");
                //Console.WriteLine($"AccountExpired-{ua.AccountExpired}");

                //cred.SetADServer(2);
                //valid = cred.Login("rtkacik", "Taurid1*", ref ua);

            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            
        }
    }
}
