using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsOfAllKinds.Fonbet
{

    public class RootModelObject
    {
        public int packetVersion { get; set; }
        public int fromVersion { get; set; }
        public int factorsVersion { get; set; }
        public int specialLineCatalogVersion { get; set; }
        public int siteVersion { get; set; }
        public Sport[] sports { get; set; }
        public Event[] events { get; set; }
        public Eventblock[] eventBlocks { get; set; }
        public Eventmisc[] eventMiscs { get; set; }
        public Customfactor[] customFactors { get; set; }
        public Announcement[] announcements { get; set; }
    }

    public class Sport
    {
        public int id { get; set; }
        public int parentId { get; set; }
        public string kind { get; set; }
        public int regionId { get; set; }
        public string sortOrder { get; set; }
        public string name { get; set; }
    }

    public class Event
    {
        public int id { get; set; }
        public int parentId { get; set; }
        public string sortOrder { get; set; }
        public int level { get; set; }
        public int num { get; set; }
        public int sportId { get; set; }
        public int kind { get; set; }
        public int rootKind { get; set; }
        public int team1Id { get; set; }
        public int team2Id { get; set; }
        public string name { get; set; }
        public string namePrefix { get; set; }
        public int startTime { get; set; }
        public string place { get; set; }
        public int priority { get; set; }
        public State state { get; set; }
        public string team1 { get; set; }
        public string team2 { get; set; }
    }

    public class State
    {
        public bool inHotList { get; set; }
        public bool willBeLive { get; set; }
    }

    public class Eventblock
    {
        public int eventId { get; set; }
        public string state { get; set; }
    }

    public class Eventmisc
    {
        public int id { get; set; }
        public int liveDelay { get; set; }
        public int score1 { get; set; }
        public int score2 { get; set; }
        public int servingTeam { get; set; }
        public string comment { get; set; }
        public int timerDirection { get; set; }
        public int timerSeconds { get; set; }
        public int[] tv { get; set; }
        public int timerUpdateTimestamp { get; set; }
        public long timerUpdateTimestampMsec { get; set; }
    }

    public class Customfactor
    {
        public int e { get; set; }
        public int f { get; set; }
        public float v { get; set; }
        public int p { get; set; }
        public string pt { get; set; }
        public bool isLive { get; set; }
        public int lo { get; set; }
        public int hi { get; set; }
    }

    public class Announcement
    {
        public int id { get; set; }
        public int num { get; set; }
        public int segmentId { get; set; }
        public string segmentName { get; set; }
        public int sportId { get; set; }
        public string segmentSortOrder { get; set; }
        public int team1Id { get; set; }
        public int team2Id { get; set; }
        public string team1 { get; set; }
        public string team2 { get; set; }
        public string name { get; set; }
        public string namePrefix { get; set; }
        public int startTime { get; set; }
        public string place { get; set; }
        public int regionId { get; set; }
        public int[] tv { get; set; }
        public bool liveHalf { get; set; }
    }

}
