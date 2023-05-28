using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace UsosAppMaui.Utility
{
    public static class NonceGenerator
    {
        private const int LENGTH = 15;
        private const string CHARS = "abcdefghijklmnoprtsquwxyz";
        private static Random random = new Random();

        public static string generateNonce()
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < LENGTH; i++)
                result.Append(CHARS.ToCharArray()[random.Next(CHARS.Length)]);
            return result.ToString();
        }
    }
}
