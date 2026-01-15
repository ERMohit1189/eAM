using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Web.Script.Serialization;

public partial class studashboard : System.Web.UI.Page
{
    Campus oo = new Campus();
    public SqlConnection con;
    public SqlCommand cmd = new SqlCommand();
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

        if (!IsPostBack)
        {
            getStudentSrno();
            BLL.BLLInstance.LoadControls("~/13/userControl/studentProfile.ascx", W1);
            //BLL.BLLInstance.LoadControls("~/13/userControl/classTeacherProfile.ascx", W2);
            //BLL.BLLInstance.LoadControls("~/13/userControl/attendanceataGlance.ascx", W3);
            //BLL.BLLInstance.LoadControls("~/13/userControl/stfeeataGlance.ascx", W4);
            BLL.BLLInstance.LoadControls("~/13/userControl/bulletin.ascx", W5);
            BLL.BLLInstance.LoadControls("~/13/userControl/planner.ascx", W6);

            if (Session["Srno"] != null)
            {
                BirthdayPopup(Session["Srno"].ToString());
                if (Session["Name"] != null)
                {
                    string studentname = Session["Name"].ToString();
                    if (studentname != string.Empty)
                    {
                        lblStudentName.Text = studentname.ToString();
                        Panel1_ModalPopupExtender.Show();
                    }
                }
            }
            string authorizationHeader = "", merchantKey = "";
            string sql1 = "select authorizationHeader, MarchentKEY from TblPayMentGateway where GateWayName='PayUMoney' and GateWayfor='Fee'";
            authorizationHeader = oo.ReturnTag(sql1, "authorizationHeader");
            merchantKey = oo.ReturnTag(sql1, "MarchentKEY");
            string sql = "select TxnID from TutionFeeTemp where GateWayName='PayUMoney'";
            var dt = oo.Fetchdata(sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                GetOnlineData(dt.Rows[i]["TxnID"].ToString(), authorizationHeader, merchantKey);
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
                            cmd.CommandText = "[SetAllFeeOnlineProc]";
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

    private void getStudentSrno()
    {
        Session["Srno"] = string.Empty;
        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@LoginId", Session["loginTypeId"].ToString()));
        param.Add(new SqlParameter("@UserName", Session["LoginName"].ToString()));
        param.Add(new SqlParameter("@Password", Session["Password"].ToString()));

        DataSet ds = new DataSet();
        ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("GetStudentSRNO_Proc", param);

        if (ds != null)
        {
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                Session["Srno"] = dt.Rows[0][0].ToString();

            }
        }
    }

    private void BirthdayPopup(string srno)
    {
        Session["Name"] = string.Empty;
        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@Srno", srno));
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));

        DataSet ds = new DataSet();
        ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_StudentBirthdayDetails", param);

        if (ds != null)
        {
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                Session["Name"] = dt.Rows[0][0].ToString();
            }
        }
    }
}