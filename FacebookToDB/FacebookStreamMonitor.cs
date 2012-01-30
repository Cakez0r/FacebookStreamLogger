using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Facebook;
using System.Threading;

namespace FacebookToDB
{
    class FacebookStreamMonitor
    {
        public event Action<FacebookStreamEntry> NewStreamEntry;


        const string GET_STREAM_ENDPOINT = "fql";
        const string STREAM_QUERY_STRING = "SELECT post_id, actor_id, message, created_time FROM stream WHERE filter_key = 'nf' AND created_time > {0}";


        public string AccessToken
        {
            get;
            private set;
        }

        public int PollInterval
        {
            get;
            private set;
        }


        FacebookAPI m_facebook;

        long m_lastPostSeenTime = 0;

        DateTime m_lastPollTime = DateTime.MinValue;


        public FacebookStreamMonitor(string accessToken, int pollInterval)
        {
            AccessToken = accessToken;
            PollInterval = pollInterval;

            m_facebook = new FacebookAPI(accessToken);
        }

        JSONObject GetFeed()
        {
            Dictionary<string, string> queryParameters = new Dictionary<string, string>()
            {
                { "q", string.Format(STREAM_QUERY_STRING, m_lastPostSeenTime) }
            };

            JSONObject feed = null;
            try
            {
                feed = m_facebook.Get(GET_STREAM_ENDPOINT, queryParameters);
            }
            catch (Exception ex)
            {
                Logger.Warn("Failed to fetch facebook feed! Exception detail: " + ex.ToString());
            }

            return feed;
        }

        public void Update()
        {
            TimeSpan timeSinceLastUpdate = DateTime.Now - m_lastPollTime;

            if (timeSinceLastUpdate.TotalMilliseconds > PollInterval)
            {
                JSONObject feed = GetFeed();

                if (feed != null)
                {
                    List<FacebookStreamEntry> feedEntries = FacebookHelpers.JSONToStreamEntries(feed);

                    foreach (FacebookStreamEntry entry in feedEntries)
                    {
                        long unixTimeCreated = TimeHelpers.DateTimeToUnix(entry.CreatedTime);

                        if (unixTimeCreated > m_lastPostSeenTime)
                        {
                            m_lastPostSeenTime = unixTimeCreated;
                        }

                        if (entry.Message != string.Empty)
                        {
                            if (NewStreamEntry != null)
                            {
                                NewStreamEntry(entry);
                            }
                        }
                    }
                }

                m_lastPollTime = DateTime.Now;
            }
        }
    }
}
