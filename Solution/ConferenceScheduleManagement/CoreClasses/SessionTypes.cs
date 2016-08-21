using ConferenceScheduleManagement.SchedulingLogic;
using System;
using System.Collections.Generic;
/*
 * AUTHOR: SABARISH K R E
 * EMAIL: sabarish90@outlook.com
 * */
namespace ConferenceScheduleManagement.CoreClasses
{
    ///<summary>
    ///A SessionType enum which indicates the type of the Session
    ///</summary>
    public enum SessionType
    { 
        TalkSession,
        NoTalkSession
    }

    ///<summary>
    ///A struct that holds the data based on which new Session objects will be created
    ///</summary>
    public struct SessionTypeData
    {
        public string Name;
        public SessionType Type;
        public DateTime StartTime;
        public DateTime EndTime;        
    }

    ///<summary>
    ///A TalkSession is a Session which can have some talks assigned scheduled in it.
    ///</summary>
    public class TalkSession : Session
    {
        ///<summary>
        ///Constructor
        ///</summary>
        public TalkSession(string name) 
            : base(name)
        {  
        
        }

        ///<summary>
        ///Picks some Talks, based on the times, from the given TalksList and adds them to the Session.
        ///</summary>
        public override void Schedule(List<Talk> TalksList)
        {
           //we need some type of scheduling Logic
           ISchedulingLogic scheduleLogic = new KnapSackSchedulingLogic();
           scheduleLogic.Schedule(TalksList, this);
        }
    }

    ///<summary>
    ///A NoTalkSession is a Session which has no Talks assigned to it like Networking, Lunch, Breaks, etc.
    ///</summary>
    public class NoTalkSession : Session
    {
        ///<summary>
        ///Constructor
        ///</summary>
        public NoTalkSession(string name)
            : base(name) 
        { 
        
        }
    }
    
}
