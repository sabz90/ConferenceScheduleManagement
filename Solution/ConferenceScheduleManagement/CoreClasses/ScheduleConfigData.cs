using System.Collections.Generic;

/*
 * AUTHOR: SABARISH K R E
 * EMAIL: sabarish90@outlook.com
 * */
namespace ConferenceScheduleManagement.CoreClasses
{
    public class ScheduleConfigData
    {
        ///<summary>
        /// Minute Suffix as per input
        ///</summary>
        public string MinuteSuffix { get; set; }

        ///<summary>
        /// Lightning Suffix
        ///</summary>
        public string LightningSuffix { get; set; }

        ///<summary>
        /// Max talk time
        ///</summary>
        public int MaxTalktimeInMinutes { get; set; }

        ///<summary>
        /// Min talk time
        ///</summary>
        public int MinTalktimeInMinutes { get; set; }

        ///<summary>
        /// Lightning Minutes (5 mins here)
        ///</summary>
        public int LightningTimeInMinutes { get; set; }

        ///<summary>
        ///List of SessionTypeData which tells us information about the sessions.
        ///</summary>
        public List<SessionTypeData> SessionTypeList { get; set; }
    }
}
