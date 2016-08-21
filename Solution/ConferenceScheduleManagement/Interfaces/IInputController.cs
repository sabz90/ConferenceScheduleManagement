using ConferenceScheduleManagement.CoreClasses;
using System.Collections.Generic;
/*
 * AUTHOR: SABARISH K R E
 * EMAIL: sabarish90@outlook.com
 * */
namespace ConferenceScheduleManagement
{
    public interface IInputController
    {
        List<Talk> GetValidTalks(ScheduleConfigData globalSettings);
        void SetOutputController(IOutputController outputController);
    }
}
