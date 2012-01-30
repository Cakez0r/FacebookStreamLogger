using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Facebook;

namespace FacebookToDB
{
    class FacebookHelpers
    {
        public static List<FacebookStreamEntry> JSONToStreamEntries(JSONObject obj)
        {
            List<FacebookStreamEntry> entries = new List<FacebookStreamEntry>();

            try
            {
                JSONObject data = obj.Dictionary["data"];
                foreach (JSONObject dataEntry in data.Array)
                {
                    FacebookStreamEntry entry = new FacebookStreamEntry()
                    {
                        ActorID = dataEntry.Dictionary["actor_id"].Integer,
                        CreatedTime = TimeHelpers.UnixToDateTime(dataEntry.Dictionary["created_time"].Integer),
                        Message = dataEntry.Dictionary["message"].String,
                        PostID = dataEntry.Dictionary["post_id"].String
                    };

                    entries.Add(entry);
                }
            }
            catch (Exception ex)
            {
                Logger.Warn("Failed to parse a JSON object to a facebook entry. Exception detail: " + ex);
            }

            return entries;
        }
    }
}
