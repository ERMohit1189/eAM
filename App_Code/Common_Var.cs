using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for Common_Var
/// </summary>
public class Common_Var
{
    public static int PaidID;
   
    public static bool b = false;
    public static string receipt_id;
    public static string gen_receipt_id;
    public static int rowindex;
    public static int update_id;
    public static DateTime from_date;
    public static DateTime To_date;
    public static int plan_type = 0;
    public static int Distributor_id = 0;
  
  
    public static int levelno;
   
    public static string Payment_statement_binary_date;
    public static string Payment_statement_binary_chqdate;
    public static string Payment_statement_level_date;
    public static string pair_statement_date;
    public static string Payout_detail_date;
    public static int b1 = 0, b2 = 0, b3 = 0, b4 = 0;
    public static string get_id;
    public static string tds_report_from_date;
    public static string tds_report_to_date;
    public static int tds_report_paid_id;
    public static bool a;
  

    public Common_Var()
    {

        //
        // TODO: Add constructor logic here
        //
    }
   

}
   