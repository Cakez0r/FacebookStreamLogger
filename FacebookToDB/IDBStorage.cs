using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FacebookToDB
{
    public interface IDBStorage
    {
        void CreateStreamEntry(FacebookStreamEntry entry);
    }
}
