using System;
using Facebook;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace FacebookToDB
{
    class Program
    {
        static AppSettings s_settings;
        public static AppSettings Settings
        {
            get
            {
                if (s_settings == null)
                {
                    s_settings = AppSettings.Load("settings.xml");
                }

                return s_settings;
            }
        }

        static IDBStorage s_db;

        static void Main(string[] args)
        {
            s_db = new MySQLStorage(Settings.DBHost, Settings.DBUsername, Settings.DBPassword, Settings.DBDatabaseName, Settings.DBPort);

            Logger.Info(string.Format("Starting facebook stream monitor with access token {0} and poll interval {1}", Settings.FBAccessToken, Settings.FBPollInterval));
            FacebookStreamMonitor feedMonitor = new FacebookStreamMonitor(Settings.FBAccessToken, Settings.FBPollInterval);
            feedMonitor.NewStreamEntry += NewStreamEntry;

            while (true)
            {
                feedMonitor.Update();
                Thread.Sleep(10);
            }
        }

        static void NewStreamEntry(FacebookStreamEntry entry)
        {
            Logger.Info("Writing new post to database: " + entry.Message);
            try
            {
                s_db.CreateStreamEntry(entry);
            }
            catch (Exception ex)
            {
                Logger.Warn("Failed to write entry to database. Exception detail: " + ex.ToString());
            }
        }
    }
}