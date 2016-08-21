using ConferenceScheduleManagement.ConferenceScheduler;
using ConferenceScheduleManagement.CoreClasses;
using System;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
namespace ConferenceScheduleManagement.ConfigurationLoader
{
    class ScheduleConfigLoader
    {
        string ConfigFileName;

        public ScheduleConfigLoader(string fileName)
        {
            ConfigFileName = fileName;           
        }

        public ScheduleConfigData GetConfiguration()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ScheduleConfigData));

                serializer.UnknownNode += new
                XmlNodeEventHandler(ScheduleConfigLoader_HandleUnknownNode);
                serializer.UnknownAttribute += new
                XmlAttributeEventHandler(ScheduleConfigLoader_HandleUnknownAttribute);

                string curExecDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                string xmlConfigPath = Path.Combine(curExecDir, ConfigFileName);
                xmlConfigPath = File.Exists(xmlConfigPath) ? xmlConfigPath : Path.Combine(curExecDir + @"\..\..\..\", ConfigFileName);

                if (!File.Exists(xmlConfigPath))
                {
                    throw new ConferenceSchedulerException("The Configuration File was not found!");
                }

                // A FileStream is needed to read the XML document.
                FileStream fs = new FileStream(xmlConfigPath, FileMode.Open);
                ScheduleConfigData scd = (ScheduleConfigData)serializer.Deserialize(fs);
                return scd;
            }
            catch (InvalidOperationException io)
            {
                throw new ConferenceSchedulerException("Error while reading config file!\n" + io.Message);
            }
        }

        private void ScheduleConfigLoader_HandleUnknownAttribute(object sender, XmlAttributeEventArgs e)
        {
            throw new ConferenceSchedulerException("Unknown Attribute Found in Config.XML File. Please Verify the file and try again.");
        }

        private void ScheduleConfigLoader_HandleUnknownNode(object sender, XmlNodeEventArgs e)
        {
            throw new ConferenceSchedulerException("Unknown Node Found in Config.XML File. Please Verify the file and try again.");
        }   
    }
}
