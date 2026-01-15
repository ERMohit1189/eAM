using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Data.SqlClient;
using System.Data;

public partial class dashboard_main : System.Web.UI.Page
{
    Campus oo = new Campus();
    public SqlConnection con;
    public SqlCommand cmd = new SqlCommand();
    string sql = "";
    public void MakeConnection()
    {
        con = new SqlConnection();
        try
        {
            cmd = new SqlCommand();
            con = oo.dbGet_connection();
            con.Open();
        }
        catch { }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        if (!IsPostBack)
        {
            
            setwidgets();
            string sql1 = "select CollegeId from CollegeMaster where BranchCode=" + Session["BranchCode"] + " and isWebHook='Automatic' ";
            if (oo.Duplicate(sql1))
            {
                webhook();
            }
        }
    }

    
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton image = (ImageButton)sender;
        sql = "select Url from Menueam where MenuID='" + image.AlternateText.ToString() + "'";
        Response.Redirect(BAL.objBal.ReturnTag(sql, "Url"));

    }
    private void setwidgets()
    {
        BLL.BLLInstance.LoadBulletinWd(w1);

        string count="";

        sql = "Select Count(LoginName) count from LoginTab lt inner join menu_permission mp on lt.LoginId=Login_Id where lt.LoginName='" + Session["LoginName"].ToString() + "' and lt.BranchId=" + Session["BranchCode"].ToString() + " and (Menu_Id=68 or Menu_Id=880) and Permission_Value='Yes'";
        count = BAL.objBal.ReturnTag(sql, "count");

        if (count == "1")
        {
            BLL.BLLInstance.LoadAttWd(w2);
        }


        sql = "Select Count(LoginName) count from LoginTab lt inner join menu_permission mp on lt.LoginId=Login_Id where lt.LoginName='" + Session["LoginName"].ToString() + "' and lt.BranchId=" + Session["BranchCode"].ToString() + " and Menu_Id=285 and Permission_Value='Yes'";
        count = BAL.objBal.ReturnTag(sql, "count");

        if (count == "1")
        {
            BLL.BLLInstance.LoadStfAttWd(w3);
        }
        if (Session["Logintype"].ToString().ToLower() != "staff")
        {
            BLL.BLLInstance.LoadBirthDayAndAnniversary(w4);
        }

        sql = "Select Count(LoginName) count from LoginTab lt inner join menu_permission mp on lt.LoginId=Login_Id where lt.LoginName='" + Session["LoginName"].ToString() + "' and lt.BranchId=" + Session["BranchCode"].ToString() + " and Menu_Id=(select top(1) MenuID from Menueam where url='7/DayBookReport.aspx')  and Permission_Value='Yes'";
        count = BAL.objBal.ReturnTag(sql, "count");

        if (count == "1")
        {
            BLL.BLLInstance.LoadAccountsWd(w5);
        }

        BLL.BLLInstance.LoadPlannerWd(w6);
    }


    protected void webhook()
    {
        string sql1 = "select authorizationHeader, MarchentKEY,MerchantID from TblPayMentGateway where GateWayName='PayUMoney' and GateWayfor='Fee' and BranchCode=" + Session["BranchCode"] + " and Isactive=1";
        string sql2 = "select MerchantID from TblPayMentGateway where GateWayName='Eazypay' and GateWayfor='Fee' and BranchCode=" + Session["BranchCode"] + " and Isactive=1";
        if (oo.Duplicate(sql1) || oo.Duplicate(sql2))
        {
            string authorizationHeader = "", merchantKey = "", MerchantID = "";
            authorizationHeader = oo.ReturnTag(sql1, "authorizationHeader");
            merchantKey = oo.ReturnTag(sql1, "MarchentKEY");
            MerchantID = oo.ReturnTag(sql2, "MerchantID");
            string sql = "select distinct GateWayName, TxnID from CompositFeeDepositTemp where BranchCode=" + Session["BranchCode"] + "";
            if (oo.Duplicate(sql))
            {
                var dt = oo.Fetchdata(sql);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["GateWayName"].ToString().ToLower() == "payumoney")
                    {
                        GetOnlineData(dt.Rows[i]["TxnID"].ToString(), authorizationHeader, merchantKey);
                    }
                    if (dt.Rows[i]["GateWayName"].ToString().ToLower() == "easypay")
                    {
                        GetOnlineDataEasyPay(dt.Rows[i]["TxnID"].ToString(), MerchantID);
                    }
                }
                try
                {
                    MakeConnection();
                    cmd.CommandText = "[Sp_DeleteTempFeedataPayumoney]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                }

            }
        }
    }

    public string GetOnlineData(string merchantTransactionIds, string authorizationHeader, string merchantKey)
    {
        string result = "";
        try
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)(0xc0 | 0x300 | 0xc00);
            string URI = "https://www.payumoney.com/payment/payment/chkMerchantTxnStatus?";
            string myParameters = "merchantKey=" + merchantKey + "&merchantTransactionIds=" + merchantTransactionIds + "";
            using (WebClient wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                wc.Headers.Add("Authorization", authorizationHeader);
                var HtmlResult = wc.UploadString(URI, myParameters);
                result = HtmlResult.ToString();
                result = result.Replace("{", "").Replace("}", "").Replace("[", "").Replace("]", "").Replace("\"", "").Replace(",\"", ",").Replace("\":\"", ":").Replace("\",\"", ",");
                string[] res = result.Split(new string[] { "," }, StringSplitOptions.None);
                string[] status = res[0].Split(new string[] { ":" }, StringSplitOptions.None);
                string sts = status[1];
                string[] pstatus = res[4].Split(new string[] { ":" }, StringSplitOptions.None);
                string psts = pstatus[1].ToLower();
                try
                {
                    if (sts == "0")
                    {
                        if (psts == "money settled" || psts == "settlement in process")
                        {
                            string MSG = "";
                            MakeConnection();
                            cmd.CommandText = "[SetAllCompositFeeOnlineProc]";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Connection = con;
                            cmd.Parameters.AddWithValue("@TxnID", merchantTransactionIds);
                            cmd.Parameters.AddWithValue("@SessionNames", Session["SessionName"].ToString());
                            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                            cmd.Parameters.AddWithValue("@MSG", MSG);
                            cmd.Parameters["@MSG"].Direction = ParameterDirection.InputOutput;
                            cmd.Parameters["@MSG"].Size = 1000;
                            cmd.ExecuteNonQuery();
                            MSG = cmd.Parameters["@MSG"].Value.ToString();
                            con.Close();
                        }
                        else
                        {
                            string sqlsss = "SELECT top(1) DATEDIFF(MINUTE, RecordDate, GETDATE()) timediffer from CompositFeeDepositTemp where BranchCode=" + Session["BranchCode"] + " and TxnID='" + merchantTransactionIds + "'";
                            int timeMin = int.Parse(oo.ReturnTag(sqlsss, "timediffer"));
                            if (timeMin > 20)
                            {
                                string sqlsss1 = "delete from CompositFeeDepositTemp where BranchCode=" + Session["BranchCode"] + " and TxnID='" + merchantTransactionIds + "'";
                                MakeConnection();
                                cmd.CommandText = sqlsss1;
                                cmd.CommandType = CommandType.Text;
                                cmd.Connection = con;
                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
        }
        catch (Exception)
        {
            //throw;
        }
        return result;
    }
    public string GetOnlineDataEasyPay(string merchantTransactionIds, string MerchantID)
    {
        string result = "";
        try
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)(0xc0 | 0x300 | 0xc00);
            string URI = "https://eazypay.icicibank.com/EazyPGVerify?";
            string myParameters = "ezpaytranid=&amount=&paymentmode=&merchantid=" + MerchantID + "&trandate=&pgreferenceno=" + merchantTransactionIds + "";
            using (WebClient wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                var HtmlResult = wc.UploadString(URI, myParameters);
                result = HtmlResult.ToString();
                string[] res = result.Split(new string[] { "=" }, StringSplitOptions.None);
                string[] res1 = res[1].Split(new string[] { "&" }, StringSplitOptions.None);
                string sts1 = res1[0];
                try
                {
                    if (sts1.ToLower() == "success" || sts1.ToLower() == "rip" || sts1.ToLower() == "sip")
                    {
                        string MSG = "";
                        MakeConnection();
                        cmd.CommandText = "[SetAllCompositFeeOnlineProc]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@TxnID", merchantTransactionIds);
                        cmd.Parameters.AddWithValue("@SessionNames", Session["SessionName"].ToString());
                        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                        cmd.Parameters.AddWithValue("@MSG", MSG);
                        cmd.Parameters["@MSG"].Direction = ParameterDirection.InputOutput;
                        cmd.Parameters["@MSG"].Size = 1000;
                        cmd.ExecuteNonQuery();
                        MSG = cmd.Parameters["@MSG"].Value.ToString();
                        con.Close();
                    }
                    else
                    {
                        string sqlsss = "SELECT top(1) DATEDIFF(MINUTE, RecordDate, GETDATE()) timediffer from CompositFeeDepositTemp where BranchCode=" + Session["BranchCode"] + " and TxnID='" + merchantTransactionIds + "'";
                        int timeMin = int.Parse(oo.ReturnTag(sqlsss, "timediffer"));
                        if (timeMin > 20)
                        {
                            string sqlsss1 = "delete from CompositFeeDepositTemp where BranchCode=" + Session["BranchCode"] + " and TxnID='" + merchantTransactionIds + "'";
                            MakeConnection();
                            cmd.CommandText = sqlsss1;
                            cmd.CommandType = CommandType.Text;
                            cmd.Connection = con;
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    } 
                }
                catch (Exception)
                {
                }
            }
        }
        catch (Exception)
        {
            //throw;
        }
        return result;
    }
}