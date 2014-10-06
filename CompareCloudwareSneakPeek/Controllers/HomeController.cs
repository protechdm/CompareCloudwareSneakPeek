using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompareCloudwareSneakPeak.Models;
using System.Configuration;
//using Castle.Core;
//using Castle.Core.Logging;

//[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace CompareCloudwareSneakPeak.Controllers
{
    
    public class HomeController : BaseController
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        const int USER_VIDEO = 1;
        const int PROVIDER_VIDEO = 2;
        private int _videoType;
        
        //public ActionResult Index(int? video)
        //{
        //    log4net.Config.XmlConfigurator.Configure(); 
        //    return RedirectToAction("SneakPeek", video);
        //}

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult SneakPeek(string id, int? video)
        {
            
            ICustomSession cs = new CustomSession();
            this.CustomSession = cs;
            var bc = Request.Browser;
            //int x = bc.EcmaScriptVersion.Major;

            this.CustomSession.DetectedBrowser = bc.Browser;
            this.CustomSession.DetectedBrowserID = bc.Id;

            HttpRequestBase request = this.Request;

            this.CustomSession.DetectedAgent = request.UserAgent;
            this.CustomSession.DetectedBrowserIsAPhone = request.UserAgent.ToUpperInvariant().Contains("PHONE");
            this.CustomSession.DetectedBrowserIsAnIPAD = request.UserAgent.ToUpperInvariant().Contains("IPAD");
            //if (this.CustomSession.DetectedBrowserIsAnIPAD)
            //{
            //    this.CustomSession.DetectedBrowserIsAPhone = false;
            //}
            string requestedURL = request.Url.AbsoluteUri;
            string userAgent = request.UserAgent;
            string userLanguage = request.UserLanguages.FirstOrDefault();
            string userHostAddress = request.UserHostAddress;

            string message = "BROWSER," + bc.Browser;
            message += ",BROWSER ID," + bc.Id;
            message += ",ID," + (id == null ? "NULL" : id);
            message += ",VIDEO," + (video == null ? "NULL" : video.ToString());
            message += ",URL," + requestedURL;
            message += ",AGENT," + userAgent;
            message += ",LANGUAGE," + userLanguage;
            message += ",ADDRESS," + userHostAddress;

            try
            {
                if (log == null)
                {
                    throw new Exception("NO LOGGER");
                }
                log4net.Config.XmlConfigurator.Configure(); 

                if (log.IsInfoEnabled == false)
                {
                    throw new Exception("LOGGER INFO NOT ENABLED");
                }
                log.Info(message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            SneakPeekVideoModel cavm = new SneakPeekVideoModel(this.CustomSession);

            bool isLive = Convert.ToBoolean(ConfigurationManager.AppSettings["IsLive"]);
            if (isLive)
            {
                if (video == 1)
                {
                    _videoType = USER_VIDEO;
                }
                if (video == 2)
                {
                    _videoType = PROVIDER_VIDEO;
                }
                else
                {
                    _videoType = USER_VIDEO;
                }

                
                string videoFormatToUse = ConfigurationManager.AppSettings["VideoFormatToUse"];
                cavm.SneakPeekVideoFileFormat = videoFormatToUse;
                cavm.VideoType = _videoType;

                #region Choose video format
                switch (videoFormatToUse)
                {
                    case "MP4":
                        if (_videoType == PROVIDER_VIDEO)
                        {
                            cavm.SneakPeekVideoFileName = ConfigurationManager.AppSettings["ProviderVideoMP4"];
                        }
                        if (_videoType == USER_VIDEO)
                        {
                            cavm.SneakPeekVideoFileName = ConfigurationManager.AppSettings["UserVideoMP4"];
                        }
                        break;
                    case "OGV":
                        if (_videoType == PROVIDER_VIDEO)
                        {
                            cavm.SneakPeekVideoFileName = ConfigurationManager.AppSettings["ProviderVideoOGV"];
                        }
                        if (_videoType == USER_VIDEO)
                        {
                            cavm.SneakPeekVideoFileName = ConfigurationManager.AppSettings["UserVideoOGV"];
                        }
                        break;
                    case "WEBM":
                        if (_videoType == PROVIDER_VIDEO)
                        {
                            cavm.SneakPeekVideoFileName = ConfigurationManager.AppSettings["ProviderVideoWEBM"];
                        }
                        if (_videoType == USER_VIDEO)
                        {
                            cavm.SneakPeekVideoFileName = ConfigurationManager.AppSettings["UserVideoWEBM"];
                        }
                        break;
                    case "MOV":
                        if (_videoType == PROVIDER_VIDEO)
                        {
                            cavm.SneakPeekVideoFileName = ConfigurationManager.AppSettings["ProviderVideoMOV"];
                        }
                        if (_videoType == USER_VIDEO)
                        {
                            cavm.SneakPeekVideoFileName = ConfigurationManager.AppSettings["UserVideoMOV"];
                        }
                        break;
                    case "SWF":
                        if (_videoType == PROVIDER_VIDEO)
                        {
                            cavm.SneakPeekVideoFileName = ConfigurationManager.AppSettings["ProviderVideoSWF"];
                        }
                        if (_videoType == USER_VIDEO)
                        {
                            cavm.SneakPeekVideoFileName = ConfigurationManager.AppSettings["UserVideoSWF"];
                        }
                        break;
                }
                #endregion
                cavm.SneakPeekVideoFileFormat = ConfigurationManager.AppSettings["VideoFormatToUse"];
                cavm.IsLocalDomain = true;
                cavm.ReadyToPlay = true;
            }

            bool testMode = Convert.ToBoolean(ConfigurationManager.AppSettings["TestMode"]);
            if (testMode)
            {
                cavm.FudgeImagePath = "../";
            }

            ViewBag.JavaScriptEnabled = true;

            return View("SneakPeek",cavm);
        }

        [HttpPost]
        public ActionResult SneakPeek(SneakPeekVideoModel cavm)
        {
            Response.Redirect("http://www.comparecloudware.co.uk", false);
            return null;
        }

        [HttpPost]
        public ActionResult Register()
        {
            //Response.Redirect("http://www.comparecloudware.co.uk", false);
            return null;
        }
    }
}
