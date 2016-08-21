using ConferenceScheduleManagement.ConferenceScheduler;
using ConferenceScheduleManagement.CoreClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ConferenceScheduleManager.UnitTests
{
    [TestClass]
    public class SessionFactoryTests
    {
        [TestMethod]
        [ExpectedException(typeof(ConferenceSchedulerException))]
        public void CreateSimpleSession_EndTime_LessThan_StartTime_ThrowsException()
        {
            SessionTypeData s1 = new SessionTypeData();
            s1.StartTime = new DateTime(1990, 01, 01, 09, 0, 0);
            s1.EndTime = new DateTime(1990, 01, 01, 08, 0, 0);
            s1.Type = SessionType.TalkSession;            
            Session ses1 = SessionFactory.GetInstance().CreateSession(s1);

            //should throw exception as EndTime < StartTime
        }

        [TestMethod]
        public void CreateSimpleSession_NoException()
        {
            SessionTypeData s1 = new SessionTypeData();
            s1.StartTime = new DateTime(1990, 01, 01, 09, 0, 0);
            s1.EndTime = new DateTime(1990, 01, 01, 10, 0, 0);
            s1.Type = SessionType.TalkSession;

            Session ses1 = SessionFactory.GetInstance().CreateSession(s1);

        }
    }
}
