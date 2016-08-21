using System;
using System.Collections.Generic;

/*
 * AUTHOR: SABARISH K R E
 * EMAIL: sabarish90@outlook.com
 * */
namespace ConferenceScheduleManagement.CoreClasses
{
    public abstract class Session
    {
        ///<summary>
        /// Counter to give ID to the Session 
        ///</summary>
        private static int id = 1;

        ///<summary>
        /// The ID
        ///</summary>
        public int ID { get; set; } = id++;

        ///<summary>
        /// The Session name
        ///</summary>
        public string Name { get; set; }

        ///<summary>
        /// The Session Start Time
        ///</summary>
        public DateTime StartTime { get; set; }

        ///<summary>
        /// The Session End Time
        ///</summary>
        public DateTime EndTime { get; set; }

        ///<summary>
        /// The List of talks scheduled in this Session
        ///</summary>
        public List<Talk> ScheduledTalks { get; set; }

        ///<summary>
        /// Constructor
        ///</summary>
        public Session(string name)
        {
            Name = name;
        }

        ///<summary>
        /// The schedule
        ///</summary>
        public virtual void Schedule(List<Talk> TalksList)
        {
            //It shouldn't usually come unless the derived class has chosen not to schedule any talk in it.
        }        
    }
}
