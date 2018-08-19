using AdminPanelIntegration.Models;
using AdminPanelIntegration.viewmodel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using VirtualShareMarket_Wcf;

namespace AdminPanelIntegration.DataAccess
{
    public class dataConnect : DBconnect
    {
        public List<Showuserdetails> getAllUsers()
        {
            SqlDataReader dr = this.executeread("select vu.uid, vu.uname, vu.mobileno, vt.lastudate, vt.token, vt.os, vt.osversion, vt.appversion from VSM_UserDetail vu join VSM_UserToken vt on vu.uid = vt.uid");
            List<Showuserdetails> ls = new List<Showuserdetails>();
            while (dr.Read())
            {
                Showuserdetails u = new Showuserdetails();
                UserDetail ud = new UserDetail();
                UserToken ut = new UserToken();

                int i = dr.FieldCount;

                ud.uid = Convert.ToInt32(dr["uid"]);
                ud.uname = (dr["uname"]).ToString();
                ud.mobileno = (dr["mobileno"]).ToString();
                ut.token = (dr["token"]).ToString();
                ut.os = (dr["os"]).ToString();
                ut.osversion = (dr["osversion"]).ToString();
                ut.appversion = (dr["appversion"]).ToString();
                ut.lastudate = Convert.ToDateTime(dr["lastudate"]);

                u.ud = ud;
                u.ut = ut;

                ls.Add(u);
            }
            return ls;
        }

        public Showuserdetails getUserDetails(int id)
        {
            sp = new List<System.Data.SqlClient.SqlParameter>();
            sp.Add(new SqlParameter("@id", id));
            SqlDataReader dr = this.executeread("select vu.uid, vu.uname, vu.mobileno, vu.profilepic, vt.lastudate, vt.token, vt.os, vt.osversion, vt.appversion from VSM_UserDetail vu join VSM_UserToken vt on vu.uid = vt.uid where vu.uid=@id");
            Showuserdetails u = new Showuserdetails();
            if (dr.Read())
            {
                UserDetail ud = new UserDetail();
                UserToken ut = new UserToken();

                ud.uid = Convert.ToInt32(dr["uid"]);
                ud.uname = (dr["uname"]).ToString();
                ud.mobileno = (dr["mobileno"]).ToString();
                ud.profilepic = (dr["profilepic"]).ToString();
                ut.lastudate = Convert.ToDateTime(dr["lastudate"]);
                ut.token = (dr["token"]).ToString();
                ut.os = (dr["os"]).ToString();
                ut.osversion = (dr["osversion"]).ToString();
                ut.appversion = (dr["appversion"]).ToString();

                u.ud = ud;
                u.ut = ut;
            }
            return u;
        }

        public Showuserdetails getUserDetails(string token)
        {
            sp = new List<System.Data.SqlClient.SqlParameter>();
            sp.Add(new SqlParameter("@token", token));
            SqlDataReader dr = this.executeread("select vu.uid, vt.token, vt.os from VSM_UserDetail vu join VSM_UserToken vt on vu.uid = vt.uid where vt.token=@token");
            Showuserdetails u = new Showuserdetails();

            if (dr.Read())
            {   
                UserDetail ud = new UserDetail();
                UserToken ut = new UserToken();
                
                ud.uid = Convert.ToInt32(dr["uid"]);
                //ud.uname = (dr["uname"]).ToString();
                //ud.mobileno = (dr["mobileno"]).ToString();
                //ud.profilepic = (dr["profilepic"]).ToString();
                //ut.lastudate = Convert.ToDateTime(dr["lastudate"]);
                //ut.token = (dr["token"]).ToString();
                ut.os = (dr["os"]).ToString();
                //ut.osversion = (dr["osversion"]).ToString();
                //ut.appversion = (dr["appversion"]).ToString();
                ut.token = token;

                u.ud = ud;
                u.ut = ut;
            }
            return u;
        }
        
        public List<UserSelectedDetail> getSelectedUsers(string query)
        {
            SqlDataReader dr = this.executeread(query);
           
            List<UserSelectedDetail> ls = new List<UserSelectedDetail>();
            int cnt = dr.FieldCount;
            
            while (dr.Read())
            {
                UserSelectedDetail u = new UserSelectedDetail();
                u.namerowid = 0;
                for(int i=0; i< cnt; i++)
                {
                    u.detail.Add((dr[i]).ToString());

                    u.cols.Add(dr.GetName(i));

                    if (dr.GetName(i).Equals("token"))
                        u.rowid = i;
                    else if (dr.GetName(i).Equals("uname"))
                        u.namerowid = i;
                }
                
                ls.Add(u);
            }
            
            return ls;
        }

        public int getCountofAllUsers()
        {
            int count = 0;
            SqlDataReader dr = this.executeread("Select COUNT (uid) from VSM_UserDetail");
            if (dr.Read())
                count = Convert.ToInt32(dr[0]);
                
            return count;
        }
        
    }
}