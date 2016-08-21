using ConferenceScheduleManagement.CoreClasses;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;
/*
 * AUTHOR: SABARISH K R E
 * EMAIL: sabarish90@outlook.com
 * */
namespace ConferenceScheduleManagement.IOControllers
{
 
    class ConsoleOutput : IOutputController
    {
        int NoOfEvents = 0, NoOfErros = 0, NoOfWarnings = 0;

        ///<summary>
        /// Normal Print to console
        ///</summary>
        public void Print(string str)
        {
            WriteLine(str);            
        }

        ///<summary>
        /// Prints all the track details of the given tracks list
        ///</summary>
        public void PrintTrackDetails(List<Track> trackList)
        {
            if (trackList == null || trackList.Count == 0)
                return;

            ForegroundColor = ConsoleColor.Green;
            WriteLine("\n------------------OUTPUT----------------");

            if (NoOfErros > 0 || NoOfWarnings > 0)
            {
                ForegroundColor = ConsoleColor.Cyan;
                WriteLine("Errors: " + NoOfErros + "\nWarnings: " + NoOfWarnings);
            }
            ResetColor();

            foreach (Track track in trackList)
            {
                Print("\n\nTRACK " + track.ID);

                DateTime curTime = new DateTime();

                //each track has multiple sessions.
                foreach (Session session in track.Sessions)
                { 
                    if (session is NoTalkSession)
                    {
                        Print(string.Format("{0:hh:mm tt}" + "     ", curTime > session.StartTime ? curTime : session.StartTime) + " " + session.Name.ToUpper());
                    }
                    else if(session is TalkSession)
                    {
                        curTime = session.StartTime;

                        //now each TalkSession will have multiple Sessions.
                        foreach (Talk talk in session.ScheduledTalks)
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.Append(string.Format("{0:hh:mm tt}" + "  ", curTime));
                            sb.Append(talk.TopicTitle);
                            Print(sb.ToString());                 
                            curTime = curTime.AddMinutes(talk.TimeDurationInMinutes);
                        }
                    }

                }                    
            }
        }

        ///<summary>
        /// Log Error to the console output window
        ///</summary>
        public void LogError(string str)
        {
            NoOfErros++;
            ForegroundColor = ConsoleColor.Red;
            WriteLine("******************\nERROR: \n" + str + "\n******************");
            ResetColor();
        }

        ///<summary>
        /// Log Warning to the console output window
        ///</summary>
        public void LogWarning(string str)
        {
            NoOfWarnings++;
            ForegroundColor = ConsoleColor.Yellow;
            WriteLine("Warning: " + str);
            ResetColor();
        }

        ///<summary>
        /// Log Process Event
        ///</summary>
        public void LogProcessEvent(string str)
        {
            NoOfEvents++;
            ForegroundColor = ConsoleColor.Green;
            WriteLine("Event: " + str);
            ResetColor();
        }        
    }
}
