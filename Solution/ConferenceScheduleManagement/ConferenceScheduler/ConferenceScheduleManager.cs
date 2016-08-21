using ConferenceScheduleManagement.CoreClasses;
using System.Collections.Generic;

/*
 * AUTHOR: SABARISH K R E
 * EMAIL: sabarish90@outlook.com
 * */

namespace ConferenceScheduleManagement.ConferenceScheduler
{
    public class ConferenceScheduleManager
    {
        ///<summary>
        /// Settings object
        ///</summary>
        ScheduleConfigData Settings;

        ///<summary>
        /// Tracks List
        ///</summary>
        List<Track> TracksList;

        ///<summary>
        /// Valid Talks List
        ///</summary>
        List<Talk> ValidTalksList;

        ///<summary>
        /// Summary
        ///</summary>
        public ConferenceScheduleManager(List<Talk> TalkList, ScheduleConfigData settings)
        {
            Settings = settings;
            ValidTalksList = TalkList;
        }

        ///<summary>
        /// Perform Talk Scheduling. Returns a list of tracks with talks scheduled in theie respective sessions.
        ///</summary>
        public void PerformTalkScheduler()
        {
            TracksList = new List<Track>();

            //Make a new list of Tracks to keep all the track information.
            int noOfTalksToBeScheduled = ValidTalksList.Count;
            while (noOfTalksToBeScheduled > 0)
            {
                Track track = new Track();

                //Now based on the type of sessions configured, new session objects will be created and added to the track, which will then be populated with talks.
                ConfigureSessionsForTrack(track);

                //for each session, schedule tasks. After that make a track
                foreach (Session session in track.Sessions)
                {
                    //do the scheduling i.e., assign the tasks to this session depending on the time
                    session.Schedule(ValidTalksList);
                  
                    //after some talks are assigned to the session, remove them from the list.
                    RemoveScheduledTalksFromList(ref ValidTalksList, session);
                }
                TracksList.Add(track);
                noOfTalksToBeScheduled = ValidTalksList.Count;
            }
        }

        ///<summary>
        /// Returns the scheduled tracks
        ///</summary>
        public List<Track> GetScheduledTracks()
        {
            return TracksList;
        }

        ///<summary>
        /// Adds the sessions configured to the given track
        ///</summary>
        private void ConfigureSessionsForTrack(Track track)
        { 
            SessionFactory factory = SessionFactory.GetInstance();
            foreach (SessionTypeData sessionData in Settings.SessionTypeList)
            {
                track.AddSession(factory.CreateSession(sessionData));
            }
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
