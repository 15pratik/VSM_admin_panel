using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminPanelIntegration.Models
{
    public class UserToken
    {
        public int usertokenid { get; set; }
        public int uid { get; set; }
        public String token { get; set; }
        public DateTime lastudate { get; set; }
        public String os { get; set; }
        public String osversion { get; set; }
        public String appversion { get; set; }
    }
}