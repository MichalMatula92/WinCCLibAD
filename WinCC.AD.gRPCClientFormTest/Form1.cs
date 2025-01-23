using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinCC.AD.gRPCClient;

namespace WinCC.AD.gRPCClientFormTest
{
    public partial class Form1 : Form
    {
        private CredClient cred;
        public Form1()
        {
            InitializeComponent();

            var conf = new CredConfig();
            conf.GRPCService = "192.168.128.228:50000";
            conf.LogFilePath = "C:\\workspace\\C#\\WinCCLibAD\\Logs\\WinCC.AD.gRPCClient.log";

            cred = new CredClient();
        }

        private void Login_Click(object sender, EventArgs e)
        {
            var ua = new WinCCUserAccount();          
            var valid = cred.Login(uname.Text, pass.Text, ref ua);

            if (cred.IsError)
            {
                Console.WriteLine(cred.ErrorMessage);
            }
            Console.WriteLine($"IsValid-{valid}-{ua}");
        }

        private void Authentize_Click(object sender, EventArgs e)
        {
            var ua = new WinCCUserAccount();
            var valid = cred.Authentize(uname.Text, pass.Text, ref ua);
          
            if (cred.IsError)
            {
                Console.WriteLine(cred.ErrorMessage);
            }
            Console.WriteLine($"IsValid-{valid}-{ua}");
        }

        private void Logoff_Click(object sender, EventArgs e)
        {
            cred.Logoff(uname.Text, "firstname", "lastname");
            if (cred.IsError)
            {
                Console.WriteLine(cred.ErrorMessage);
            }
        }

        private void Autologoff_Click(object sender, EventArgs e)
        {
            cred.AutoLogoff(uname.Text, "firstname", "lastname");
            if (cred.IsError)
            {
                Console.WriteLine(cred.ErrorMessage);
            }
        }

        private void SetADServer_Click(object sender, EventArgs e)
        {

            cred.SetADServer((int)server.Value);
            if (cred.IsError)
            {
                Console.WriteLine(cred.ErrorMessage);
            }
        }

        private void GetADServer_Click(object sender, EventArgs e)
        {
            var id = cred.GetADServer();
            if (cred.IsError)
            {
                Console.WriteLine(cred.ErrorMessage);
            }
            server.Value = id;
        }

        private void ServiceMode_Click(object sender, EventArgs e)
        {
            var mode = cred.ServiceMode();
            if (cred.IsError)
            {
                Console.WriteLine(cred.ErrorMessage);
            }
            Console.WriteLine(mode);
        }

        private void ELogin_Click(object sender, EventArgs e)
        {
            cred.EmergencyLogin(uname.Text, "firstname", "lastname", "OK");
            if (cred.IsError)
            {
                Console.WriteLine(cred.ErrorMessage);
            }
        }
    }
}
