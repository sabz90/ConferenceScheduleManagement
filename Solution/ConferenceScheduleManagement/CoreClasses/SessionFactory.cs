using ConferenceScheduleManagement.ConferenceScheduler;
/*
 * AUTHOR: SABARISH K R E
 * EMAIL: sabarish90@outlook.com
 * */
namespace ConferenceScheduleManagement.CoreClasses
{
    public class SessionFactory
    {
        private static SessionFactory Instance = null;

        ///<summary>
        ///Private Constructor
        ///</summary>
        private SessionFactory()
        { 
            
        }

        ///<summary>
        ///Checks if the given Session Config data is valid, Throws an Exception if its Invalid
        ///</summary>
        private void ValidateSessionData(SessionTypeData sessionData)
        {
            if (sessionData.StartTime > sessionData.EndTime)
            {
                throw new ConferenceSchedulerException("Session Configuration Error: Start Time should not occur Later than End Time");
            }
        }

        ///<summary>
        ///Singleton..
        ///</summary>
        public static SessionFactory GetInstance()
        {
            if (Instance == null)
            {
                Instance = new SessionFactory();
            }
            return Instance;
        }

        ///<summary>
        ///Creates the session object based on the session type data given.
        ///</summary>
        public Session CreateSession(SessionTypeData sessionData)
        {
            //validate session data.
            ValidateSessionData(sessionData);

            Session newSession = null;
            switch (sessionData.Type)
            {
                case SessionType.TalkSession:
                    {
                        newSession = new TalkSession(sessionData.Name);
                        newSession.StartTime = sessionData.StartTime;
                        newSession.EndTime = sessionData.EndTime;
                    }
                    break;

                case SessionType.NoTalkSession:
                    {
                        newSession = new NoTalkSession(sessionData.Name);
                        newSession.StartTime = sessionData.StartTime;
                        newSession.EndTime = sessionData.EndTime;
                    }
                    break;

                default:
                    throw new ConferenceSchedulerException("Invalid Session Type received!");
            }
            return newSession;
        }
        
    }
}
