using ConferenceScheduleManagement.CoreClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace ConferenceScheduleManager.UnitTests
{
    [TestClass]
    public class ScheduleLogicTests
    {
        [TestMethod]        
        public void SimpleScheduleTest_AllTalksScheduled()
        {
            TalkSession ts1 = new TalkSession("TestSession");
            ts1.StartTime = new DateTime(1990,01,01,09,0,0);
            ts1.EndTime = new DateTime(1990, 01, 01, 12, 0, 0);
            
            List<Talk> talksList = new List<Talk>();
            talksList.Add(new Talk("TestTopic1", 20));
            talksList.Add(new Talk("TestTopic2", 20));
            talksList.Add(new Talk("TestTopic3", 20));
            talksList.Add(new Talk("TestTopic4", 20));

            ts1.Schedule(talksList);            

            //The number of scheduled talks should be 4
            Assert.AreEqual(4, ts1.ScheduledTalks.Count, "No. of scheduled talks do not match");

            RemoveScheduledTalksFromList(ref talksList, ts1);
            Assert.AreEqual(0, talksList.Count, "No of left over talks mismatch");
        }

        [TestMethod]
        public void SimpleScheduleTest_SubsetOfTalksScheduled()
        {
            TalkSession ts1 = new TalkSession("TestSession");
            ts1.StartTime = new DateTime(1990, 01, 01, 09, 0, 0);
            ts1.EndTime = new DateTime(1990, 01, 01, 12, 0, 0);

            List<Talk> talksList = new List<Talk>();
            talksList.Add(new Talk("TestTopic1", 60));
            talksList.Add(new Talk("TestTopic2", 60));
            talksList.Add(new Talk("TestTopic3", 60));
            talksList.Add(new Talk("TestTopic4", 60));

            ts1.Schedule(talksList);

            //The number of scheduled talks should be 3 (60 min each)
            Assert.AreEqual(3, ts1.ScheduledTalks.Count, "No. of scheduled talks do not match");

            RemoveScheduledTalksFromList(ref talksList, ts1);
            Assert.AreEqual(1, talksList.Count, "No of left over talks mismatch");
        }

        [TestMethod]
        public void SimpleScheduleTest_NoTalksScheduled_DueTo_TimeLimit()
        {
            TalkSession ts1 = new TalkSession("TestSession");
            ts1.StartTime = new DateTime(1990, 01, 01, 09, 0, 0);
            ts1.EndTime = new DateTime(1990, 01, 01, 12, 0, 0);

            List<Talk> talksList = new List<Talk>();
            talksList.Add(new Talk("TestTopic1", 200));
            talksList.Add(new Talk("TestTopic2", 200));
            talksList.Add(new Talk("TestTopic3", 200));
            talksList.Add(new Talk("TestTopic4", 200));

            ts1.Schedule(talksList);

            Assert.AreEqual(0, ts1.ScheduledTalks.Count, "No. of scheduled talks do not match");

            RemoveScheduledTalksFromList(ref talksList, ts1);
            Assert.AreEqual(4, talksList.Count, "No of left over talks mismatch");

        }

        [TestMethod]
        public void SimpleScheduleTest_NoTalksScheduled()
        {
            TalkSession ts1 = new TalkSession("TestSession");
            ts1.StartTime = new DateTime(1990, 01, 01, 09, 0, 0);
            ts1.EndTime = new DateTime(1990, 01, 01, 12, 0, 0);

            List<Talk> talksList = new List<Talk>();            

            ts1.Schedule(talksList);

            //The number of scheduled talks should be 3 (60 min each)
            Assert.AreEqual(0, ts1.ScheduledTalks.Count, "No. of scheduled talks do not match");

            RemoveScheduledTalksFromList(ref talksList, ts1);
            Assert.AreEqual(0, talksList.Count, "No of left over talks mismatch");
        }

        [TestMethod]
        public void SimpleScheduleTest_PerfectFitTalkSchedule()
        {
            TalkSession ts1 = new TalkSession("TestSession");
            ts1.StartTime = new DateTime(1990, 01, 01, 09, 0, 0);
            ts1.EndTime = new DateTime(1990, 01, 01, 10, 0, 0);

            List<Talk> talksList = new List<Talk>();
            talksList.Add(new Talk("TestTopic1", 10));
            talksList.Add(new Talk("TestTopic2", 10));
            talksList.Add(new Talk("TestTopic3", 10));
            talksList.Add(new Talk("TestTopic4", 10));
            talksList.Add(new Talk("TestTopic5", 10));
            talksList.Add(new Talk("TestTopic6", 3));
            talksList.Add(new Talk("TestTopic7", 7));

            ts1.Schedule(talksList);

            //The number of scheduled talks should be 7
            Assert.AreEqual(7, ts1.ScheduledTalks.Count, "No. of scheduled talks do not match");

            RemoveScheduledTalksFromList(ref talksList, ts1);
            Assert.AreEqual(0, talksList.Count, "No of left over talks mismatch");

        }

        [TestMethod]
        public void SimpleScheduleTest_SubSetOfTalksScheduled()
        {
            TalkSession ts1 = new TalkSession("TestSession");
            ts1.StartTime = new DateTime(1990, 01, 01, 09, 0, 0);
            ts1.EndTime = new DateTime(1990, 01, 01, 10, 0, 0);

            List<Talk> talksList = new List<Talk>();
            talksList.Add(new Talk("TestTopic1", 10));
            talksList.Add(new Talk("TestTopic2", 10));
            talksList.Add(new Talk("TestTopic3", 10));
            talksList.Add(new Talk("TestTopic4", 10));
            talksList.Add(new Talk("TestTopic5", 10));
            talksList.Add(new Talk("TestTopic6", 3));
            talksList.Add(new Talk("TestTopic7", 3));
            talksList.Add(new Talk("TestTopic8", 7));

            ts1.Schedule(talksList);

            //The number of scheduled talks should be 7
            Assert.AreEqual(7, ts1.ScheduledTalks.Count, "No. of scheduled talks do not match");

            RemoveScheduledTalksFromList(ref talksList, ts1);
            Assert.AreEqual(1, talksList.Count, "No of left over talks mismatch");

            //for the best fit, the left out session should be of 3 minutes
            Assert.AreEqual(3, talksList[0].TimeDurationInMinutes, "No of left over talks mismatch");
        }

        [TestMethod]
        public void SimpleNoTalkSession_NothingShouldBeScheduled()
        {
            NoTalkSession ts1 = new NoTalkSession("TestSession");
            ts1.StartTime = new DateTime(1990, 01, 01, 09, 0, 0);
            ts1.EndTime = new DateTime(1990, 01, 01, 16, 0, 0);

            List<Talk> talksList = new List<Talk>();
            talksList.Add(new Talk("TestTopic1", 10));
            talksList.Add(new Talk("TestTopic2", 10));
            talksList.Add(new Talk("TestTopic3", 10));
            talksList.Add(new Talk("TestTopic4", 10));
            talksList.Add(new Talk("TestTopic5", 10));
            talksList.Add(new Talk("TestTopic6", 3));
            talksList.Add(new Talk("TestTopic7", 3));
            talksList.Add(new Talk("TestTopic8", 7));

            ts1.Schedule(talksList);

            //The number of scheduled talks should be 7
            Assert.AreEqual(true, ts1.ScheduledTalks == null || ts1.ScheduledTalks.Count == 0, "No. of scheduled talks do not match");
           
        }

        ///<summary>
        /// After some talks are scheduled, we need to remove them from the main list.
        ///</summary>
        private void RemoveScheduledTalksFromList(ref List<Talk> ValidTalksList, Session session)
        {
            if (session.ScheduledTalks == null || session.ScheduledTalks.Count == 0)
                return;
            ValidTalksList.RemoveAll(item => session.ScheduledTalks.Contains(item));
        }

    }
}
