using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common
{
    public class CHANNELS
    {
        public const string DIRECT = "Direct";
        public const string TRAVEL_AGENT = "Travel Agent";
        public const string ONLINE_TRAVEL_AGENT = "Online Travel Agent";
        public const string OPAQUE = "Opaque";
        public static readonly string[] list = new string[] { DIRECT, TRAVEL_AGENT, OPAQUE };
    }

    public class ATTRIBUTES
    {
        public const string SPA = "Spa";
        public const string FITNESS_CENTER = "Fitness Center";
        public const string BUSINESS_CENTER = "Business Center";
        public const string GOLF_COURSE = "Golf Course";
        public const string OTHER_FACILITES = "Other Recreation Facilities - Pools, game rooms, tennis courts, ect";
        public const string MANAGEMENT = "Management/Sales Attention";
        public const string RESTURANTS = "Resturants";
        public const string BARS = "Bars";
        public const string ROOM_SERVICS = "Room Service";
        public const string BANQUET = "Banquet & Catering";
        public const string MEETING_ROOMS = "Meeting Rooms";
        public const string ENTERTAINMENT = "Entertainment";
        public const string COURTESY_FB = "Courtesy(FB)";
        public const string GUEST_ROOM = "Guest Rooms";
        public const string RESERVATIONS = "Reservations";
        public const string GUEST_CHECKINOUT = "Guest Check in/Guest Check out";
        public const string CONCIERGE = "Concierge";
        public const string HOUSEKEEPING = "Housekeeping";
        public const string MAINTANENCE = "Maintanence and security";
        public const string COURTESY = "Courtesy (Rooms)";
        public const string BUILDING = "Building";
    }
}
