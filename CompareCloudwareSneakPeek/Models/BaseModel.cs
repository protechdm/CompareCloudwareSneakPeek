using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompareCloudwareSneakPeak.Models
{

    public class BaseModel
    {
        private ICustomSession _session;
        public ICustomSession CustomSession 
        {
            get
            {
                return _session;
            }
            set
            {
                if (value != null)
                {
                    if (value.DetectedBrowserID == null)
                    {
                        //System.Web.HttpContext.Current.Server.Transfer("home.htm");
                        //System.Web.HttpContext.Current.Response.Redirect("home.htm",true);
                        throw new SessionExpiredException();
                    }
                    else
                    {
                        _session = value;
                    }
                }
            } 
        }
    }

    public class SessionExpiredException : Exception { };
}

