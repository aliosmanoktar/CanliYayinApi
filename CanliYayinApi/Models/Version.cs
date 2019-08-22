using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CanliYayinApi.Models
{
    public class Version
    {
        public string version { set; get; }
        public string path {
            get {
                return $"{HttpContext.Current.Request.Url.Authority}/app_assets/apk/app-{version.Replace('.','_')}.apk";
            }
        }
    }
}