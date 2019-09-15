using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegaBotConsole.Infrastructure.Models
{

    public class NextMatchRootObject
    {
        public string _type { get; set; }
        public Datum[] data { get; set; }
        public object[] errors { get; set; }
    }

    public class Datum
    {
        public string _type { get; set; }
        public string _entityId { get; set; }
        public string externalId { get; set; }
        public string externalSource { get; set; }
        public DateTime startDate { get; set; }
        public string status { get; set; }
        public State state { get; set; }
        public Venue venue { get; set; }
        public Teams teams { get; set; }
        public Competition competition { get; set; }
        public Link1 link { get; set; }
        public Results results { get; set; }
        public object[] sponsors { get; set; }
        public Tickets tickets { get; set; }
        public Customcta[] customCta { get; set; }
    }

    public class Venue
    {
        public string _type { get; set; }
        public string _entityId { get; set; }
        public string name { get; set; }
        public string city { get; set; }
    }

    public class Teams
    {
        public Home home { get; set; }
        public Away away { get; set; }
    }

    public class Away
    {
        public string _type { get; set; }
        public string _entityId { get; set; }
        public string externalId { get; set; }
        public string externalSource { get; set; }
        public string name { get; set; }
        public string shortName { get; set; }
        public string primaryColor { get; set; }
        public string secondaryColor { get; set; }
        public Country country { get; set; }
        public Link1 link { get; set; }
        public Logo logo { get; set; }
    }
    
    public class Tickets
    {
        public string _type { get; set; }
        public Link3 link { get; set; }
    }

    public class Link3
    {
        public string _type { get; set; }
        public string url { get; set; }
        public string target { get; set; }
    }

    public class Customcta
    {
        public string caption { get; set; }
        public string actionType { get; set; }
        public Link3 link { get; set; }
    }
    
    public class Competition
    {
        public string _type { get; set; }
        public string _entityId { get; set; }
        public string externalId { get; set; }
        public string externalSource { get; set; }
        public string name { get; set; }
        public string shortName { get; set; }
        public Logo logo { get; set; }
    }

    public class Logo
    {
        public string _type { get; set; }
        public string id { get; set; }
        public string focus { get; set; }
        public string crop { get; set; }
    }

    public class State
    {
        public string _type { get; set; }
        public string name { get; set; }
        public string shortName { get; set; }
    }

    public class Home
    {
        public string _type { get; set; }
        public string _entityId { get; set; }
        public string externalId { get; set; }
        public string externalSource { get; set; }
        public string name { get; set; }
        public string shortName { get; set; }
        public string primaryColor { get; set; }
        public string secondaryColor { get; set; }
        public Country country { get; set; }
        public Link link { get; set; }
        public Logo logo { get; set; }
    }

    public class Country
    {
        public string name { get; set; }
        public string code { get; set; }
    }

    public class Link
    {
        public string _type { get; set; }
        public string pageEntityId { get; set; }
        public string url { get; set; }
        public bool isWeb { get; set; }
    }
    
    public class Logo2
    {
        public string _type { get; set; }
        public string id { get; set; }
        public string focus { get; set; }
        public string crop { get; set; }
    }

    public class Duration
    {
        public string _type { get; set; }
        public int regularTime { get; set; }
        public int extraTime { get; set; }
        public int totalTime { get; set; }
    }

    public class Link1
    {
        public string _type { get; set; }
        public string pageEntityId { get; set; }
        public string url { get; set; }
        public bool isWeb { get; set; }
    }

    public class Results
    {
        public string _type { get; set; }
        public Scores scores { get; set; }
        public Period[] periods { get; set; }
    }

    public class Scores
    {
        public string _type { get; set; }
        public int home { get; set; }
        public int away { get; set; }
    }

    public class Period
    {
        public string _type { get; set; }
        public string name { get; set; }
        public string shortName { get; set; }
        public Scores scores { get; set; }
        public string status { get; set; }
        public Action[] actions { get; set; }
    }

    public class Action
    {
        public string _type { get; set; }
        public string actionType { get; set; }
        public string teamType { get; set; }
        public Time time { get; set; }
        public Action1[] actions { get; set; }
    }

    public class Time
    {
        public string _type { get; set; }
        public int regularTime { get; set; }
        public int extraTime { get; set; }
    }

    public class Action1
    {
        public string _type { get; set; }
        public string actionType { get; set; }
        public string teamType { get; set; }
        public Time1 time { get; set; }
        public Player[] players { get; set; }
    }

    public class Time1
    {
        public string _type { get; set; }
        public int regularTime { get; set; }
        public int extraTime { get; set; }
    }

    public class Player
    {
        public string _type { get; set; }
        public string _entityId { get; set; }
        public string externalId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int number { get; set; }
        public Position position { get; set; }
    }

    public class Position
    {
        public string _type { get; set; }
        public string name { get; set; }
        public string shortName { get; set; }
        public string category { get; set; }
        public int categoryOrder { get; set; }
        public int order { get; set; }
    }

}
