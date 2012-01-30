using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace FacebookToDB
{
    [Serializable]
    public class AppSettings
    {
        public string DBHost
        {
            get;
            set;
        }

        public int DBPort
        {
            get;
            set;
        }

        public string DBUsername
        {
            get;
            set;
        }

        public string DBPassword
        {
            get;
            set;
        }

        public string DBDatabaseName
        {
            get;
            set;
        }

        public string FBAccessToken
        {
            get;
            set;
        }

        public int FBPollInterval
        {
            get;
            set;
        }


        AppSettings()
        {
            DBHost = "SET ME!";
            DBUsername = "SET ME!";
            DBPassword = "SET ME!";
            DBDatabaseName = "SET ME!";
            FBAccessToken = "SET ME!";
            FBPollInterval = 10000;
        }


        public static AppSettings Load(string filepath)
        {
            AppSettings settings = new AppSettings();

            Stream fileStream = null;
            try
            {
                fileStream = File.OpenRead(filepath);
            }
            catch
            {
                Logger.Warn("Failed to load settings file. Creating a default one...");
                Save(settings, filepath);
                return settings;
            }

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(AppSettings));
                settings = (AppSettings)serializer.Deserialize(fileStream);
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("Failed to load settings file. Is it correct? Exception detail: {0}", ex));
            }

            fileStream.Close();

            return settings;
        }

        public static void Save(AppSettings settings, string filepath)
        {
            try
            {
                using (Stream fileStream = File.Open(filepath, FileMode.OpenOrCreate))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(AppSettings));
                    serializer.Serialize(fileStream, settings);
                }
            }
            catch
            {
                Logger.Error("Failed to save settings file to " + filepath);
            }
        }
    }
}
