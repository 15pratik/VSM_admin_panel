using AdminPanelIntegration.DataAccess;
using AdminPanelIntegration.Models;
using AdminPanelIntegration.viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using VirtualShareMarket_Wcf;
using VirtualShareMarket_Wcf.DBClasses;
using VirtualShareMarket_Wcf.model;

namespace AdminPanelIntegration.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            dataConnect dc = new dataConnect();
            ViewBag.ClientCount = dc.getCountofAllUsers();
            return View();
        }

        public ActionResult AllUsers()
        {
            dataConnect dc = new dataConnect();
            ViewBag.list = dc.getAllUsers();
            return View();
        }
        
        public ActionResult SelectedUsers(String query)
        {
            try
            {
                if (query.Contains("token"))
                {
                    dataConnect dc = new dataConnect();
                    ViewBag.list = dc.getSelectedUsers(query);
                    ViewBag.query = query;
                }
                else
                   throw new Exception("Token Not Selected");
            }
            catch (Exception e)
            {
                ViewBag.list = "Token Not Selected";
                //throw;
            }
            
            return View();
        }

        public ActionResult getUserDetails(Models.UserDetail ud)
        {
            dataConnect dc = new dataConnect();
            ViewBag.result = dc.getUserDetails(ud.uid);
            return View();
        }

        public ActionResult Radio_check(Showuserdetails c1, String tokenlist, string title , string description, string link)
        {
            List<string> tokens = tokenlist.Split(',').ToList<string>();
            tokens.Reverse();

            dataConnect dc = new dataConnect();
           
            int i = 0;

            //SENXEX and NIFTY both are  down for the first time

            for(i=0; i< tokens.Count(); i++)
            {
                AddNotificationData obj1 = new AddNotificationData();
                obj1.startTransaction();

                Showuserdetails u = new Showuserdetails();
                u = dc.getUserDetails(tokens[i]);
                Responce res = new Responce();
                Req_NotificationData req = new Req_NotificationData();

                //req.baseobject.notificationcode = Constants.Notification_CUSTOM_GENERAL;

                //title = "SENXEX and NIFTY both are  down for the first time";
                //description = "Read More....";
                //link = "http://www.cantechletter.com/wp-content/uploads/2013/05/charging-bull-new-york-400x314.jpg";


                req.userid = (u.ud.uid).ToString();
                req.insertDT = CustomDate.getTodayDate();
                req.notificationcode = Constants.Notification_CUSTOM_GENERAL;
                req.message = title;

               
                CustomNotificationModel model = new CustomNotificationModel();
                model.title = title;
                model.notificationcode = Constants.Notification_CUSTOM_GENERAL;
                model.description = description;
                model.link = link;
                model.parent = "marketview";

                string json = new JavaScriptSerializer().Serialize(model);
                
                req.custom = json;
                req.os = u.ut.os;
                req.token = tokens[i];

                VSMService1 service1 = new VSMService1();
                obj1.execute(req, res);

                int r = res.result;

                if (r == 0)
                {
                    obj1.rollbackTransaction();
                }
                else
                {
                    obj1.commitTransaction();
                }
            }    
            
            return RedirectToAction("AllUsers");
        }

    }
}