using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FacebookToDB
{
    class TimeHelpers
    {
        public static DateTime UnixToDateTime(long timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }


        public static int DateTimeToUnix(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = date - origin;
            return (int)diff.TotalSeconds;
        }
    }
}
