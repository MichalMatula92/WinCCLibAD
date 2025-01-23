using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WinCC.AD.gRPCClient
{
    /// <summary>
	/// WinCC user account class.
	/// </summary>
    [ComVisible(true)]
    [Guid("F7553C7C-2F7C-48FC-ADBE-6E80EEBD6E7B")]
    [ProgId("WinCCLib.ADCredgRPC.UserAccount")]
    [ClassInterface(ClassInterfaceType.None)]
    public class WinCCUserAccount
    {
        /// <summary>
        /// User name.
        /// </summary>
        public string Username { get; set; } = "-";

        /// <summary>
        /// Fist name.
        /// </summary>
        public string FirstName { get; set; } = "-";

        /// <summary>
        /// Last name.
        /// </summary>
        public string LastName { get; set; } = "-";

        /// <summary>
        /// Group flag.
        /// </summary>
        public int GroupFlag { get; set; } = 0;

        /// <summary>
        /// Days to expired password.
        /// </summary>
        public int DaysToExpiredPassword { get; set; } = 0;

        /// <summary>
        /// Account locked out.
        /// </summary>
        public bool LockedOut { get; set; } = false;

        /// <summary>
        /// Account disabled.
        /// </summary>
        public bool Disabled { get; set; } = false;

        /// <summary>
        /// Password never expires.
        /// </summary>
        public bool PasswordNeverExpires { get; set; } = false;

        /// <summary>
        /// Password change needed.
        /// </summary>
        public bool PasswordChangeNeeded { get; set; } = false;

        /// <summary>
        /// Password expired.
        /// </summary>
        public bool PasswordExpired { get; set; } = false;

        /// <summary>
        /// Account expired.
        /// </summary>
        public bool AccountExpired { get; set; } = false;

        /// <summary>
        /// Method returns list of parameters value as string.
        /// </summary>
        /// <returns>List of parameters value.</returns>
        public override string ToString()
        {
            return Username + ";" + FirstName + ";" + LastName + ";" + GroupFlag + ";" + DaysToExpiredPassword + ";" +
                LockedOut + ";" + Disabled + ";" + PasswordNeverExpires + ";" + PasswordChangeNeeded + ";" + PasswordExpired + ";" +
                AccountExpired;
        }
    }
}
