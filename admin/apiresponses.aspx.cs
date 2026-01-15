using c4SmsNew;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Web;

public partial class apiresponses : System.Web.UI.Page
{
    public SqlConnection con;
    public SqlCommand cmd = new SqlCommand();
    string Srno = ""; string SessionName = ""; string BranchCode = "";
    Campus conCampus = new Campus();
    private readonly Campus _oo = new Campus();
    public void MakeConnection()
    {
        con = new SqlConnection();
        try
        {
            cmd = new SqlCommand();
            con = _oo.dbGet_connection();
            con.Open();
        }
        catch { }
    }
    string TransactionId = "";
    string ResponseCode = "";
    string TotalAmount = "";
    string feefor = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        ResponseCode = HttpContext.Current.Request.Form["Response Code"].ToString();
        if (ResponseCode == "E000")
        {
            try { TransactionId = HttpContext.Current.Request.Form["ReferenceNo"].ToString(); }
            catch (Exception ex) { lblErrorString.Text += ex.Message.ToString(); }
            string sql1 = "select sum(Paid) amount SrNo from CompositFeeDepositTemp where TxnID='" + TransactionId + "'";
            TotalAmount=_oo.ReturnTag(sql1, "amount");
            string sqlcnt = "select count(TxnID) cnt from CompositFeeDepositTemp where TxnID='" + TransactionId + "'";
            if (_oo.ReturnTag(sqlcnt, "cnt") != "0")
            {
                feefor = "Fee";
            }
            string sqlcnt1 = "select count(TxnID) cnt from AdmissionFormOnline where TxnID='" + TransactionId + "'";
            if (_oo.ReturnTag(sqlcnt1, "cnt") != "0")
            {
                feefor = "AdmissionFee";
            }
            if (feefor== "Fee")
            {
                string sql = "select top(1) SrNo,SessionName,BranchCode from CompositFeeDepositTemp where TxnID='" + TransactionId + "'";
                Srno = conCampus.ReturnTag(sql, "SrNo");
                SessionName = conCampus.ReturnTag(sql, "SessionName");
                BranchCode = conCampus.ReturnTag(sql, "BranchCode");
                InserAPIResponse(Srno, TransactionId);
            }
            else if (feefor == "AdmissionFee")
            {
                string sql = "select top(1) Mobile,SessionName,BranchCode from AdmissionFormOnline where TxnID='" + TransactionId + "'";
                Srno = conCampus.ReturnTag(sql, "Mobile");
                SessionName = conCampus.ReturnTag(sql, "SessionName");
                BranchCode = conCampus.ReturnTag(sql, "BranchCode");
                InserAPIAdmissionResponse(TransactionId);
            }
        }
        else if (ResponseCode == "E00335")
        {
            //lblErrorString.Text = ResponseCode;
            Response.Redirect("FeePaymentFailed.aspx");
        }
        else
        {
            //lblErrorString.Text = ResponseCode;
            Response.Redirect("FeePaymentFailed.aspx");
        }
    }
    public void InserAPIResponse(string Srno, string txnid)
    {
        try
        {
                string MSG = "";
                MakeConnection();
                cmd.CommandText = "[SetAllCompositFeeOnlineProc]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@TxnID", txnid);
                cmd.Parameters.AddWithValue("@SessionNames", SessionName);
                cmd.Parameters.AddWithValue("@BranchCode", BranchCode);
                cmd.Parameters.AddWithValue("@MSG", MSG);
                cmd.Parameters["@MSG"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters["@MSG"].Size = 1000;
                int x=cmd.ExecuteNonQuery();
            lblResponseString.Text = x.ToString();
            MSG = cmd.Parameters["@MSG"].Value.ToString();
            con.Close();
            SendSms(MSG.ToString(), TotalAmount, Srno, "1");
            string qstr = MSG.Replace("/", "__");
            qstr = qstr + ("$" + SessionName.ToString() + "$" + BranchCode.ToString()).ToString();
            Response.Redirect("../2/FeeReceiptAll.aspx?RecieptSrNo=" + qstr);

            
        }
        catch (Exception) { }
    }
    
    public void InserAPIAdmissionResponse(string txnid)
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
                        SendSms(recid, TotalAmount, Srno,"11");
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

    public void SendSms(string recieptNo, string receivedAmount, string registervalue, string templetId)
    {
        try
        {


            var conta = "";
            if (feefor == "Fee")
            { 
                string _sql = "select top(1) FamilyContactNo from StudentFamilyDetails  where SessionName='" + SessionName + "' and BranchCode=" + BranchCode+ " and SrNo='" + registervalue + "' order by id desc";
                conta = conCampus.ReturnTag(_sql, "FamilyContactNo");
            }
            if (feefor == "AdmissionFee")
            {
                conta = Srno;
            }

            SendFeesSms(conta, recieptNo, receivedAmount, templetId);
        }
        catch (Exception)
        {

            throw;
        }
    }

    public void SendFeesSms(string fmobileNo, string recieptNo, string receivedAmount, string templetId)
    {
        try
        {

            var mess = "";
            string _sql = "Select HitValue from SMSActivateDeactivate where BranchCode=" + BranchCode + "";
            if (conCampus.ReturnTag(_sql, "HitValue") == "") return;
            if (conCampus.ReturnTag(_sql, "HitValue") != "true") return;
            var sadpNew = new SMSAdapterNew();
            if (feefor == "Fee")
            {
                _sql = "Select top(1) FirstName as StudentName  from StudentGenaralDetail  where SessionName='" + SessionName + "' and BranchCode=" + BranchCode + " and SrNo='" + Srno + "' order by id desc";
                mess = "INR " + receivedAmount + " received fee for " + conCampus.ReturnTag(_sql, "StudentName") + " ( " + Srno + " ). Receipt No. " + recieptNo + "";
            }
            else if (feefor == "AdmissionFee")
            {
                _sql = "Select top(1) StudentName+' '+LastName as StudentName  from AdmissionFormCollection  where SessionName='" + SessionName + "' and BranchCode=" + BranchCode + " and Srno='" + Srno + "' order by id desc";
                mess = "INR " + receivedAmount + " received admission form fee for " + conCampus.ReturnTag(_sql, "StudentName") + " ( " + Srno + " ). Receipt No. " + recieptNo + "";
            }
            

            _sql = "Select CollegeShortNa  from CollegeMaster where BranchCode=" + BranchCode+ "";
            conCampus.ReturnTag(_sql, "CollegeShortNa");

            if (fmobileNo == "") return;
            sadpNew.Send(mess, fmobileNo, templetId);
        }
        catch
        {
            // ignored
        }
    }


}