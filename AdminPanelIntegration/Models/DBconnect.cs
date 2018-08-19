using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AdminPanelIntegration.Models
{
    public class DBconnect
    {
        SqlConnection con;
        protected List<SqlParameter> sp;
        public SqlConnection getconnection()
        {
            con = new SqlConnection("Data Source=Pratik ; Integrated Security=True ; Initial Catalog=VirtualShareMarket");
            //con = new SqlConnection("Data Source=198.12.156.82\\SQLEXPRESS ; Initial Catalog=VirtualShareMarket; User id=*****; Password=*****");
            //con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["VSMConnectionString"].ConnectionString);
            con.Open();
            return con;
        }

        public SqlCommand getcommand(string Query)
        {
            con = getconnection();
            SqlCommand cmd = new SqlCommand(Query, con);
            if (sp != null)
            {
                for (int i = 0; i < sp.Count; i++)
                {
                    cmd.Parameters.Add(sp.ElementAt(i));
                }
            }
            return cmd;
        }

        public int executenon(string Query)
        {
            SqlCommand cmd = getcommand(Query);
            int res = cmd.ExecuteNonQuery();
            return res;
        }

        public SqlDataReader executeread(string Query)
        {
            SqlCommand cmd = getcommand(Query);
            SqlDataReader dr = cmd.ExecuteReader();
            return dr;
        }
    }
}