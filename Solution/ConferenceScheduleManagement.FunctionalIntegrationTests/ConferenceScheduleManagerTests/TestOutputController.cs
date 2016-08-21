using System.Collections.Generic;

namespace ConferenceScheduleManagement.FunctionalIntegrationTests
{
    class TestOutputController : IOutputController
    {
        public int NoOfErrors = 0, NoOfWarnings = 0,NoOfProcessEvents = 0;

        public void Print(string str)
        {
            //DoNothing; this is just test controller
        }

        public void PrintTrackDetails(List<ConferenceScheduleManagement.CoreClasses.Track> trackList)
        {
            //DoNothing; this is just test controller
        }

        public void LogError(string str)
        {
            NoOfErrors++;
        }

        public void LogWarning(string str)
        {
            NoOfWarnings++;
        }

        public void LogProcessEvent(string str)
        {
            NoOfProcessEvents++;
        }
    }
}
