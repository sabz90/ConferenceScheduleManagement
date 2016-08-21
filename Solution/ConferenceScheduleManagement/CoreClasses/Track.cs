using System.Collections.Generic;

/*
 * AUTHOR: SABARISH K R E
 * EMAIL: sabarish90@outlook.com
 * */
namespace ConferenceScheduleManagement.CoreClasses
{
    public class Track
    {
        static int count = 1;
        public int ID { get; private set; } = count++;
        public List<Session> Sessions { get; private set; } = new List<Session>();

        public void AddSession(Session s)
        {
            Sessions.Add(s);
        }
    }
}
