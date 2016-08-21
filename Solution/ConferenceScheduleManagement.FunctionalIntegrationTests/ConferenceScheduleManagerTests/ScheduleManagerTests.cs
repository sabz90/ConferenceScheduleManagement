using ConferenceScheduleManagement.ConferenceScheduler;
using ConferenceScheduleManagement.CoreClasses;
using ConferenceScheduleManagement.IOControllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace ConferenceScheduleManagement.FunctionalIntegrationTests
{
    [TestClass]
    public class FunctionalIntegrationTests
    {
        [TestMethod]
        public void AllValidInputTests()
        {
            ScheduleConfigData settings = new ScheduleConfigData();
            ConfigureSettings(settings);
                       
            string str = @"..\..\TestFiles\original.txt";
            IInputController fr = new FileInputProcessor(str);
            TestOutputController outputController = new TestOutputController();
            fr.SetOutputController(outputController);

            var validTalkList = fr.GetValidTalks(settings);
            Assert.AreEqual(19, validTalkList.Count, "No of valid tasks not as expected");

            ConferenceScheduleManager Csm = new ConferenceScheduleManager(validTalkList, settings);
            Csm.PerformTalkScheduler();

            var trackList = Csm.GetScheduledTracks();
            Assert.AreEqual(2, trackList.Count, "No of Tracks scheduled not as expected");
            Assert.AreEqual(0, validTalkList.Count, "No of left over talks not as expected");
        }
        
        [TestMethod]
        public void AllValidInputTests_LargeInput()
        {
            ScheduleConfigData settings = new ScheduleConfigData();
            ConfigureSettings(settings);

            string str = @"..\..\TestFiles\largeValidInput.txt";
            IInputController fr = new FileInputProcessor(str);
            TestOutputController outputController = new TestOutputController();
            fr.SetOutputController(outputController);

            var validTalkList = fr.GetValidTalks(settings);
            Assert.AreEqual(5389, validTalkList.Count, "No of valid tasks not as expected");

            ConferenceScheduleManager Csm = new ConferenceScheduleManager(validTalkList, settings);
            Csm.PerformTalkScheduler();

            var trackList = Csm.GetScheduledTracks();
            Assert.AreEqual(531, trackList.Count, "No of Tracks scheduled not as expected");
            Assert.AreEqual(0, validTalkList.Count, "No of left over talks not as expected");
        }

        [TestMethod]
        public void AllValidInputTests_Expect6Tracks()
        {
            ScheduleConfigData settings = new ScheduleConfigData();
            ConfigureSettings(settings);

            string str = @"..\..\TestFiles\1.txt";
            IInputController fr = new FileInputProcessor(str);
            TestOutputController outputController = new TestOutputController();
            fr.SetOutputController(outputController);

            var validTalkList = fr.GetValidTalks(settings);
            Assert.AreEqual(57, validTalkList.Count, "No of valid tasks not as expected");

            ConferenceScheduleManager Csm = new ConferenceScheduleManager(validTalkList, settings);
            Csm.PerformTalkScheduler();

            var trackList = Csm.GetScheduledTracks();
            Assert.AreEqual(6, trackList.Count, "No of Tracks scheduled not as expected");
            Assert.AreEqual(0, validTalkList.Count, "No of left over talks not as expected");
        }
       
        [TestMethod]
        public void PartiallyValidInputTests_ExpectWarnings()
        {
            ScheduleConfigData settings = new ScheduleConfigData();
            ConfigureSettings(settings);

            string str = @"..\..\TestFiles\2.txt";
            IInputController fr = new FileInputProcessor(str);
            TestOutputController outputController = new TestOutputController();
            fr.SetOutputController(outputController);

            var validTalkList = fr.GetValidTalks(settings);
            Assert.AreEqual(1, validTalkList.Count, "No of valid tasks not as expected");

            Assert.AreEqual(15, outputController.NoOfWarnings, "No of warnings not as expected");

            ConferenceScheduleManager Csm = new ConferenceScheduleManager(validTalkList, settings);
            Csm.PerformTalkScheduler();

            var trackList = Csm.GetScheduledTracks();
            Assert.AreEqual(1, trackList.Count, "No of Tracks scheduled not as expected");
            Assert.AreEqual(0, validTalkList.Count, "No of left over talks not as expected");
        }

        [TestMethod]
        public void InvalidInput_EmptyInput()
        {
            ScheduleConfigData settings = new ScheduleConfigData();
            ConfigureSettings(settings);

            string str = @"..\..\TestFiles\3.txt";
            IInputController fr = new FileInputProcessor(str);
            TestOutputController outputController = new TestOutputController();
            fr.SetOutputController(outputController);

            var validTalkList = fr.GetValidTalks(settings);
            Assert.AreEqual(0, validTalkList.Count, "No of valid tasks not as expected");

            ConferenceScheduleManager Csm = new ConferenceScheduleManager(validTalkList, settings);
            Csm.PerformTalkScheduler();

            var trackList = Csm.GetScheduledTracks();
            Assert.AreEqual(0, trackList.Count, "No of Tracks scheduled not as expected");
            Assert.AreEqual(0, validTalkList.Count, "No of left over talks not as expected");
        }

        [TestMethod]
        public void PartiallyValidInput_WithManyJunkInputs()
        {
            ScheduleConfigData settings = new ScheduleConfigData();
            ConfigureSettings(settings);

            string str = @"..\..\TestFiles\4.txt";
            IInputController fr = new FileInputProcessor(str);
            TestOutputController outputController = new TestOutputController();
            fr.SetOutputController(outputController);

            var validTalkList = fr.GetValidTalks(settings);
            Assert.AreEqual(11, validTalkList.Count, "No of valid tasks not as expected");

            Assert.AreEqual(13, outputController.NoOfWarnings, "No of warnings not as expected");

            ConferenceScheduleManager Csm = new ConferenceScheduleManager(validTalkList, settings);
            Csm.PerformTalkScheduler();

            var trackList = Csm.GetScheduledTracks();
            Assert.AreEqual(2, trackList.Count, "No of Tracks scheduled not as expected");
            Assert.AreEqual(0, validTalkList.Count, "No of left over talks not as expected");
        }


        
        private void ConfigureSettings(ScheduleConfigData Settings)
        {
            //This can be later adapted to read the input from a config file instead of hard coding.
            Settings.LightningTimeInMinutes = 5;
            Settings.LightningSuffix = "lightning";
            Settings.MaxTalktimeInMinutes = 240;
            Settings.MinTalktimeInMinutes = 1;
            Settings.MinuteSuffix = "min";

            //Now add types of sessions possible.
            Settings.SessionTypeList = new List<SessionTypeData>();

            SessionTypeData morningSession;
            morningSession.Type = SessionType.TalkSession;
            morningSession.Name = "Morning";
            morningSession.StartTime = new DateTime(year: 1900, month: 1, day: 1, hour: 9, minute: 0, second: 0);
            morningSession.EndTime = new DateTime(year: 1900, month: 1, day: 1, hour: 12, minute: 0, second: 0);
            Settings.SessionTypeList.Add(morningSession);

            SessionTypeData lunchSession;
            lunchSession.Type = SessionType.NoTalkSession;
            lunchSession.Name = "Lunch";
            lunchSession.StartTime = new DateTime(year: 1900, month: 1, day: 1, hour: 12, minute: 0, second: 0);
            lunchSession.EndTime = new DateTime(year: 1900, month: 1, day: 1, hour: 13, minute: 0, second: 0);
            Settings.SessionTypeList.Add(lunchSession);

            SessionTypeData afternoonSession;
            afternoonSession.Type = SessionType.TalkSession;
            afternoonSession.Name = "Afternoon";
            afternoonSession.StartTime = new DateTime(year: 1900, month: 1, day: 1, hour: 13, minute: 0, second: 0); ;
            afternoonSession.EndTime = new DateTime(year: 1900, month: 1, day: 1, hour: 17, minute: 0, second: 0);
            Settings.SessionTypeList.Add(afternoonSession);

            SessionTypeData networkingSession;
            networkingSession.Type = SessionType.NoTalkSession;
            networkingSession.Name = "Networking Event";
            networkingSession.StartTime = new DateTime(year: 1900, month: 1, day: 1, hour: 16, minute: 0, second: 0); 
            networkingSession.EndTime = new DateTime(year: 1900, month: 1, day: 1, hour: 18, minute: 0, second: 0);

            Settings.SessionTypeList.Add(networkingSession);
            
        }
    }
}
