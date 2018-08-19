using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminPanelIntegration.Models
{
    public class CustomNotificationModel
    {
        public String title { get; set; }
        public String description { get; set; }
        public String link { get; set; }
        public String parent{ get; set; }
        public String notificationcode { get; set; }
    }
}