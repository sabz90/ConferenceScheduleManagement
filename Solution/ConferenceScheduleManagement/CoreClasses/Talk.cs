
/*
 * AUTHOR: SABARISH K R E
 * EMAIL: sabarish90@outlook.com
 * */
namespace ConferenceScheduleManagement.CoreClasses
{
    ///<summary>
    /// A Type to hold the details of a "Talk"
    ///</summary>
    public class Talk
    {
        ///<summary>
        ///The Talk ID
        ///</summary>
        private static int id = 1;

        ///<summary>
        ///The talk ID
        ///</summary>
        public int ID { get; private set; }

        ///<summary>
        ///The talk's topic title
        ///</summary>
        public string TopicTitle { get; set; }

        ///<summary>
        ///The talk's time duration in Minutes
        ///</summary>
        public double TimeDurationInMinutes { get; set; }

        ///<summary>
        ///Constructor
        ///</summary>
        public Talk(string talkTopic, double timeDurationInMinutes)
        {
            ID = id++;
            TopicTitle = talkTopic;
            TimeDurationInMinutes = timeDurationInMinutes;
        }
    }
}
