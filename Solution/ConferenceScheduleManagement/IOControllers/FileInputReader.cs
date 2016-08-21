using ConferenceScheduleManagement.ConferenceScheduler;
using ConferenceScheduleManagement.CoreClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

/*
 * AUTHOR: SABARISH K R E
 * EMAIL: sabarish90@outlook.com
 * */
namespace ConferenceScheduleManagement.IOControllers
{
    public class FileInputProcessor : IInputController
    {
        string InputFilePath;
        ScheduleConfigData GlobalSettings;
        IOutputController OutputController;

        ///<summary>
        /// Constructor
        ///</summary>
        public FileInputProcessor(string inputFile)
        {
            InputFilePath = inputFile;
            OutputController = null;
        }

        ///<summary>
        /// Reads from input and returns the validates the tasks based on settings and then returns the valid ones
        ///</summary>
        public List<Talk> GetValidTalks(ScheduleConfigData globalSettings)
        {
            GlobalSettings = globalSettings;
            ValidateInputFilePath();

            List<Talk> validTalks = GetValidTalksListFromFile(InputFilePath);
            return validTalks;
        }

        ///<summary>
        ///Validates and if its empty it gets a new input file
        ///</summary>
        private void ValidateInputFilePath()
        {
            if (InputFilePath.Trim() == string.Empty)
                InputFilePath = GetInputFilePath();

            if (!File.Exists(InputFilePath))
                throw new ConferenceSchedulerException("The specified input file does not exist!");
        }

        ///<summary>
        ///Gets the input file path.
        ///</summary>
        private string GetInputFilePath()
        {
            Console.SetWindowSize(150, 50);
            Console.WriteLine("Enter the full path of the input file:\n");
            return Console.ReadLine().Trim();
        }


        

       
        ///<summary>
        ///Fills the list of valid tasks with valid talks that are parsed from the input file.
        ///</summary>
        private List<Talk> GetValidTalksListFromFile(string InputFilePath)
        {
            string[] inputLines = null;
            List<Talk> listOfValidTalks = new List<Talk>();

            try
            {
                inputLines = File.ReadAllLines(InputFilePath);                
            }
            catch (IOException ex)
            {
                if (OutputController != null)
                    OutputController.LogError("ERROR Reading From input File!\nMessage:" + ex.Message + "\nStackTrace: " + ex.StackTrace);

                throw new ConferenceSchedulerException("Error Reading From input File. Terminating application.");
            }
            UpdateOutput(inputLines);
            ExtractValidTalksFromInputLines(inputLines, ref listOfValidTalks);

            return listOfValidTalks;
        }

        private void ExtractValidTalksFromInputLines(string[] inputLines, ref List<Talk> listOfValidTalks)
        {
            //parse each line and make a talk object.
            for (int i = 0; i < inputLines.Length; i++)
            {
                if (inputLines[i].Trim().Length == 0)
                    continue;

                Talk talk = ParseStringForTalk(inputLines[i]);
                if (talk != null)
                {
                    listOfValidTalks.Add(talk);
                }
                else
                {
                    //This task is Invalid! so throw a warning
                    if (OutputController != null)
                        OutputController.LogWarning("-------\nThe following Input Line was ignored due to invalid details : \n" + inputLines[i] + "\nPlease Verify if the input line is in form: topic 000min where 000 represents 3 digits (no. of minutes)\n--------------\n");
                }
            }
        }

        private void UpdateOutput(string[] inputLines)
        {
            if (OutputController != null)
            {
                OutputController.Print("------------------INPUT----------------");
                foreach (string line in inputLines)
                {
                    if (line.Trim().Length > 0)
                        OutputController.Print(line.Trim());
                }
                OutputController.Print("---------------------------------------");
            }
        }

        ///<summary>
        /// Tries to parse an input line and makes a talk object using the found data which is the title and the duration in minutes.
        /// Returns null object if parse failed.
        ///</summary>
        private Talk ParseStringForTalk(string str)
        {
            //pre processing
            string talk = str.Replace("\\s+", " ").Trim();

            //regex to match the string 
            string suffixes = GlobalSettings.MinuteSuffix + "|" +GlobalSettings.LightningSuffix;
            string pattern = @"(.*)(\s){1}([0-2]?[0-9]?[0-9]{1}" + suffixes + ")$";

            Match match = Regex.Match(talk, pattern);
            if (!match.Success)
            {
                return null;
            }

            double? talkTimeInMinutes = CalculateTalkTimeMinutesFromString(match.Groups[3].Value);
            if (talkTimeInMinutes == null || !talkTimeInMinutes.HasValue)
                return null;

            //check if valid.
            if (talkTimeInMinutes <= GlobalSettings.MaxTalktimeInMinutes && talkTimeInMinutes >= GlobalSettings.MinTalktimeInMinutes)
            {
                // Add talk to the valid talk List.
                //return new Talk(match.Groups[1].Value, talkTimeInMinutes);
                return new Talk(talk, talkTimeInMinutes.Value);
            }
            return null;                
        }

        ///<summary>
        /// Tries to parse string of format 20min, 30min, lightning and returns the no of minutes found.
        ///</summary>
        private double? CalculateTalkTimeMinutesFromString(string str)
        {
            //the input string will be in form "lightning" or 35min, 40min etc.
            String minuteSuffix = GlobalSettings.MinuteSuffix;
            String lightningSuffix = GlobalSettings.LightningSuffix;

            double talkTimeInMinutes = 0;

            if (str.EndsWith(minuteSuffix))
            {
                talkTimeInMinutes = double.Parse(str.Substring(0, str.IndexOf(minuteSuffix)));
            }
            else if (str.EndsWith(lightningSuffix))
            {
                talkTimeInMinutes = GlobalSettings.LightningTimeInMinutes;
            }
           
            return talkTimeInMinutes;
        }


        public void SetOutputController(IOutputController outputController)
        {
            OutputController = outputController;
        }

        
    }
}
