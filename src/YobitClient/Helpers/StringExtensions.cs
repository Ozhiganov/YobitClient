using System;
using System.Runtime.InteropServices;
using System.Security;

namespace YobitClient.Helpers
{
    public static class StringExtensions
    {
        public static SecureString ToSecureString(this string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            var secureString = new SecureString();

            foreach (var c in value)
                secureString.AppendChar(c);

            secureString.MakeReadOnly();
            return secureString;
        }

        public static string ToUnsecureString(this SecureString value)
        {
            var valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                return Marshal.PtrToStringUni(valuePtr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }
    }
}