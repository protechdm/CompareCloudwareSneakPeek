using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompareCloudwareSneakPeak.Controllers
{
    public class BaseController : Controller
    {
        //
        // GET: /Base/

        protected ICustomSession CustomSession { get; set; }

    }
}
