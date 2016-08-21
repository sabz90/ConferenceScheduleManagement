using System;

/*
 * AUTHOR: SABARISH K R E
 * EMAIL: sabarish90@outlook.com
 * */
namespace ConferenceScheduleManagement.ConferenceScheduler
{
    public class ConferenceSchedulerException : Exception
    {
        public ConferenceSchedulerException(string msg)
            : base(msg)
        { }
    }
}
