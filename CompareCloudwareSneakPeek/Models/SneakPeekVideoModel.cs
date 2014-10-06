using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using CompareCloudware.Web;
//using System;
//using System.Runtime.CompilerServices;
//using System.ComponentModel.DataAnnotations;
//using CompareCloudware.Web.Models;
//using System.Web.Mvc;
//using System.Collections.Generic;
//using System.Web;


namespace CompareCloudwareSneakPeak.Models
{
    #region SneakPeekVideoModel
    public class SneakPeekVideoModel : BaseModel
    {
        public SneakPeekVideoModel()
        {
            //this.CloudApplicationReviewURLDocumentExtensions = ConstructDocumentTypes();
        }

        public SneakPeekVideoModel(ICustomSession session)
        {
            base.CustomSession = session;
            //this.CloudApplicationReviewURLDocumentExtensions = ConstructDocumentTypes();
        }

        public int SneekPeekVideoID { get; set; }
        public bool IsLocalDomain { get; set; }
        public bool IsLive { get; set; }
        public bool IsYouTubeStream { get; set; }
        public string SneakPeekVideoURL { get; set; }
        public string SneakPeekVideoFileName { get; set; }
        public string SneakPeekVideoFileFormat { get; set; }
        public bool IsBrokenLink { get; set; }
        public bool ReadyToPlay { get; set; }
        public int VideoType { get; set; }
        public string FudgeImagePath { get; set; }

    }
    #endregion

}

