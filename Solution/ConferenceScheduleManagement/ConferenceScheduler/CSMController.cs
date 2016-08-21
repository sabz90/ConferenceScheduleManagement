using ConferenceScheduleManagement.ConfigurationLoader;
using ConferenceScheduleManagement.CoreClasses;
using System.Collections.Generic;

/*
 * AUTHOR: SABARISH K R E
 * EMAIL: sabarish90@outlook.com
 * */
namespace ConferenceScheduleManagement.ConferenceScheduler
{
    class CSMController
    {
        IInputController InputController;
        IOutputController OutputController;

        ///<summary>
        ///Construtor
        ///</summary>
        public CSMController(IInputController inputController, IOutputController outputController)
        {
            InputController = inputController;
            OutputController = outputController;
        }

        ///<summary>
        /// Coordinates with IO Controllers and the CSM Manager to perform the talk scheduling
        ///</summary>
        public void Run()
        {
            //configure settings according to our current requirement
            ScheduleConfigData GlobalSettings = GetConfiguredSettings();

            //if we need errors and warning logs: then set reference of output controller in the input controller.
            InputController.SetOutputController(OutputController);

            //get the valid talks based on settings
            List<Talk> validTalkList = InputController.GetValidTalks(GlobalSettings);
            
            if (validTalkList == null || validTalkList.Count == 0)
            {
                OutputController.LogProcessEvent("Nothing to process!");
                return;
            }

            //use the conference schedule manager according to the configured settings
            ConferenceScheduleManager Csm = new ConferenceScheduleManager(validTalkList, GlobalSettings);

            //Perform the actual scheduling
            Csm.PerformTalkScheduler();            

            //below is the scheduled track list
            var listOfScheduledTracks = Csm.GetScheduledTracks();
            OutputController.PrintTrackDetails(listOfScheduledTracks);           
        }

        ///<summary>
        ///Gets the settings configured in the config.xml. This XML is deserialized ScheduleConfigData object
        ///</summary>
        private ScheduleConfigData GetConfiguredSettings()
        {
            ScheduleConfigLoader settingsLoader = new ScheduleConfigLoader("config.xml");

            //A good idea is to validate the settings; i.e., to make sure the time is within range and so on.
            return settingsLoader.GetConfiguration();
        }        
    }
}
