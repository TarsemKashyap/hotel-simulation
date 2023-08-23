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
}
