using ConferenceScheduleManagement.CoreClasses;
using System.Collections.Generic;

/*
 * AUTHOR: SABARISH K R E
 * EMAIL: sabarish90@outlook.com
 * */
namespace ConferenceScheduleManagement
{
    interface ISchedulingLogic
    {
        void Schedule(List<Talk> listOfTalks, Session session);
    }
}
