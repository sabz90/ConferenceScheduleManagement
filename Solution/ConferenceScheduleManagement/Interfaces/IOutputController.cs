using ConferenceScheduleManagement.CoreClasses;
using System.Collections.Generic;

/*
 * AUTHOR: SABARISH K R E
 * EMAIL: sabarish90@outlook.com
 * */
namespace ConferenceScheduleManagement
{
    public interface IOutputController
    {
        void Print(string str);
        void PrintTrackDetails(List<Track> trackList);
        void LogError(string str);
        void LogWarning(string str);
        void LogProcessEvent(string str);
    }
}
