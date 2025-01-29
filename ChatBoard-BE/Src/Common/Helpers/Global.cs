using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helpers
{
    public static class Global
    {
        public static int GetOtp()
        {
            return new Random().Next(1000, 9999);
        }
        public static int GetSixDigitOtp()
        {
            return new Random().Next(100000, 999999);
        }
    }
}
