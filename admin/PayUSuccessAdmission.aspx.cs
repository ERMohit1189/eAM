using c4SmsNew;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;

public partial class PayUSuccessAdmission : System.Web.UI.Page
{
    public SqlConnection con;
    public SqlCommand cmd = new SqlCommand();
    Campus conCampus = new Campus();
    public void MakeConnection()
    {
        con = new SqlConnection();
        try
        {
            cmd = new SqlCommand();
            con = conCampus.dbGet_connection();
            con.Open();
        }
        catch { }
    }
    string sql = "";
    string Srno = "";
    string SessionName = "";
    string BranchCode = "";
    string txnid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["SessionName"] = Request.QueryString["SessionName"].ToString();
        Session["BranchCode"] = Request.QueryString["BranchCode"].ToString();
        Session["Srno"] = Request.QueryString["SrNo"].ToString();
        Session["Logintype"] = Request.QueryString["Logintype"].ToString();
        Session["LoginName"] = Request.QueryString["LoginName"].ToString();
        Session["ImageUrl"] = Request.QueryString["ImageUrl"].ToString();
        Session["UserMobile"] = Request.QueryString["SrNo"].ToString();
        Session["pass"] = "any";
        string amt = Request.QueryString["amt"].ToString();
        txnid = Request.Form["txnid"];
        Srno = Session["Srno"].ToString();
        SessionName = Session["SessionName"].ToString();
        BranchCode = Session["BranchCode"].ToString();

        string mode = Request.Form["mode"];
        string payuMoneyId = Request.Form["payuMoneyId"];
        string PG_Type = Request.Form["PG_Type"];
        string bank_ref_num = Request.Form["bank_ref_num"];
        string Error = Request.Form["Error"];
        decimal charges = 0;
        decimal.TryParse(Request.Form["additionalCharges"], out charges);
        InserApiResponseAdmission(Srno, txnid, charges, 1, mode, payuMoneyId, PG_Type, bank_ref_num, Error);
    }
    public string InserApiResponseAdmission(string Srno, string txnid, decimal charges, int status, string mode, string payuMoneyId, string PG_Type, string bank_ref_num, string Error)
    {
        string MSGs = "";
        try
        {
            MakeConnection();
            cmd.CommandText = "[USP_SetAPITransactionUpdate]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@TxnID", txnid.Trim());
            cmd.Parameters.AddWithValue("@Charges", charges);
            cmd.Parameters.AddWithValue("@Status", status);
            cmd.Parameters.AddWithValue("@Mode", mode);
            cmd.Parameters.AddWithValue("@PayUMoneyID", payuMoneyId);
            cmd.Parameters.AddWithValue("@PGType", PG_Type);
            cmd.Parameters.AddWithValue("@BankRefNo", bank_ref_num);
            cmd.Parameters.AddWithValue("@Error", Error);
            cmd.ExecuteNonQuery();
            InserManageResponse(txnid.Trim());
        }
        catch (Exception ex)
        {
            MSGs = ex.Message;
            con.Close();
        }
        con.Close();
        return MSGs;
    }
    public void InserManageResponse(string txnid)
    {
        try
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@txtno", txnid));

            DataSet ds = new DataSet();

            ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("GetAdmissionOnline", param);
            var recMobile = String.Empty;
            if (ds != null)
            {
                DataTable dt;
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    var recid = dt.Rows[0]["RecieptNo"].ToString();
                    var recStatus = dt.Rows[0]["Status"].ToString();
                    recMobile = dt.Rows[0]["Mobile"].ToString();
                    if (recStatus.Trim().ToLower() == "paid")
                    {
                        SendFeesSms(recMobile, recid);
                        Response.Redirect("../2/pafReciept.aspx?print=1&rid=" + recid + "", false);
                    }
                    else if (recStatus.Trim().ToLower() == "cancelled")
                    {
                        Response.Redirect("~/ap/Admission_Details.aspx?txtno=" + recMobile + "", false);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("../ap/default.aspx?from=ManageR" + ex.Message);
        }

    }

    public void SendFeesSms(string fmobileNo, string recieptNo)
    {
        try
        {
            var mess = "";
            string _sql = "Select HitValue from SMSActivateDeactivate where BranchCode=" + Session["BranchCode"] + "";
            if (conCampus.ReturnTag(_sql, "HitValue") == "") return;
            if (conCampus.ReturnTag(_sql, "HitValue") != "true") return;
            var sadpNew = new SMSAdapterNew();
            _sql = "Select StudentName+' '+LastName as StudentName, ReceivedAmount  from AdmissionFormCollection  where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and Srno='" + Srno + "'";
            mess = "INR " + conCampus.ReturnTag(_sql, "ReceivedAmount") + " received admission form fee for " + conCampus.ReturnTag(_sql, "StudentName") + " ( " + Srno + " ). Receipt No. " + recieptNo + "";
            if (fmobileNo == "") return;
            sadpNew.Send(mess, fmobileNo, "11");
        }
        catch
        {
            // ignored
        }
    }
}


   