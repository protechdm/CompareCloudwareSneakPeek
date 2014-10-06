using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompareCloudwareSneakPeak
{
    public class CustomSession : ICustomSession
    {
        public string DetectedBrowser { get; set; }
        public string DetectedBrowserID { get; set; }
        public bool JavaScriptEnabled { get; set; }
        public string DetectedAgent { get; set; }
        public bool DetectedBrowserIsAPhone { get; set; }
        public bool DetectedBrowserIsAnIPAD { get; set; }
    }
}