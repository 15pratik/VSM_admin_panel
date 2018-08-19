using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminPanelIntegration.Models
{
    public class UserDetail
    {
        public int uid { get; set; }
        public String uname { get; set; }
        public String mobileno { get; set; }
        public String verificationcode { get; set; }
        public String profilepic { get; set; }
    }
}