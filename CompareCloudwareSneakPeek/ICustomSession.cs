using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompareCloudwareSneakPeak
{
    public interface ICustomSession
    {
        string DetectedBrowser { get; set; }
        string DetectedBrowserID { get; set; }
        bool JavaScriptEnabled { get; set; }
        string DetectedAgent { get; set; }
        bool DetectedBrowserIsAPhone { get; set; }
        bool DetectedBrowserIsAnIPAD { get; set; }
    }
}
