using CanliYayinApi.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace CanliYayinApi.Controllers
{
    public class VersionController : ApiController
    {
        
        public Version Get()
        {
            return new Version
            {
                version = "2.1"
            };
        }
    }
}