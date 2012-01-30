using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FacebookToDB
{
    public class FacebookStreamEntry
    {
        public string PostID
        {
            get;
            set;
        }

        public long ActorID
        {
            get;
            set;
        }

        public string Message
        {
            get;
            set;
        }

        public DateTime CreatedTime
        {
            get;
            set;
        }
    }
}
