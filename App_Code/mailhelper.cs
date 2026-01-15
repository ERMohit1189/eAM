using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
/// <summary>
/// Summary description for mailhelper
/// </summary>
/// 

public  class mailhelper
{
    
    public mailhelper()
	{
        
	}
    /// <summary>
    ///  Used to insert/ update / delete record
    /// </summary>
    /// <param name="ss"></param>
    /// 	
   static  SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["UB"]);
   static  SqlCommand cmd;
    public static void anomalies(string ss)
    {
        if (con.State != ConnectionState.Open)
        {
            con.Open();
            cmd = new SqlCommand(ss, con);
            cmd.ExecuteNonQuery();
            con.Close();
            cmd.Dispose();
        }
        else
        {
            con.Close();
            con.Open();
            cmd = new SqlCommand(ss, con);
            cmd.ExecuteNonQuery();
            con.Close();
            cmd.Dispose();
        }
    }
    public static int newcount(string plc)
    {
        con.Open();
        string nc="select count(*) from Check_in where Place_name='"+plc+"'";
        cmd = new SqlCommand(nc, con);
        int c = Convert.ToInt32(cmd.ExecuteScalar());
        con.Close();
        cmd.Dispose();
        return c;
    }
    public static int chkcode(string plc)
    {
        con.Open();
        cmd = new SqlCommand(plc, con);
        int c = Convert.ToInt32(cmd.ExecuteScalar());
        con.Close();
        cmd.Dispose();
        return c;
    }
}
