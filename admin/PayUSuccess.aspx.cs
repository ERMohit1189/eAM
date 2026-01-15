using c4SmsNew;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;

public partial class PayUSuccess : System.Web.UI.Page
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
        string amt = Request.QueryString["amt"].ToString();
        txnid = Request.Form["txnid"];
        string sql = "select top(1) SrNo, SessionName, BranchCode  from CompositFeeDepositTemp where TxnID='" + txnid+"'";
        Srno = conCampus.ReturnTag(sql, "SrNo");
        SessionName = conCampus.ReturnTag(sql, "SessionName");
        BranchCode = conCampus.ReturnTag(sql, "BranchCode");
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
            cmd.ExecuteNonQuery();
            MSG = cmd.Parameters["@MSG"].Value.ToString();
            con.Close();
            SendSms(MSG.ToString(), amt, Srno);
            string qstr = MSG.Replace("/", "__");
            qstr = qstr + ("$" + SessionName.ToString() + "$" + BranchCode.ToString()).ToString();
            Response.Redirect("../2/FeeReceiptAll.aspx?RecieptSrNo=" + qstr);
        }
        catch (Exception ex)
        {
            //Response.Redirect("../2/FeeReceiptAll.aspx?RecieptSrNo=" + MSG.ToString());
        }
    }
    public void SendSms(string recieptNo, string receivedAmount, string registervalue)
    {
        try
        {


            var conta = "";
            if (Session["PaymentFor"] != null && Session["PaymentFor"].ToString() == "PaymentForFee")
            {
                string _sql = "select top(1) FamilyContactNo from StudentFamilyDetails  where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and SrNo='" + registervalue + "' order by id desc";
                conta = conCampus.ReturnTag(_sql, "FamilyContactNo");
            }
            if (Session["PaymentFor"] != null && Session["PaymentFor"].ToString() == "PaymentForAdmissionFee")
            {
                conta = Srno;
            }

            SendFeesSms(conta, recieptNo, receivedAmount);
        }
        catch (Exception)
        {
        }
    }

    public void SendFeesSms(string fmobileNo, string recieptNo, string receivedAmount)
    {
        try
        {

            var mess = "";
            string _sql = "Select HitValue from SMSActivateDeactivate where BranchCode=" + Session["BranchCode"] + "";
            if (conCampus.ReturnTag(_sql, "HitValue") == "") return;
            if (conCampus.ReturnTag(_sql, "HitValue") != "true") return;
            var sadpNew = new SMSAdapterNew();
            if (Session["PaymentFor"] != null && Session["PaymentFor"].ToString() == "PaymentForFee")
            {
                _sql = "Select top(1) FirstName as StudentName  from StudentGenaralDetail  where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and SrNo='" + Srno + "' order by id desc";
                mess = "INR " + receivedAmount + " received fee for " + conCampus.ReturnTag(_sql, "StudentName") + " ( " + Srno + " ). Receipt No. " + recieptNo + "";
            }
            else
            {
                _sql = "Select top(1) StudentName+' '+LastName as StudentName  from AdmissionFormCollection  where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and Srno='" + Srno + "' order by id desc";
                mess = "INR " + receivedAmount + " received admission form fee for " + conCampus.ReturnTag(_sql, "StudentName") + " ( " + Srno + " ). Receipt No. " + recieptNo + "";
            }
            _sql = "Select CollegeShortNa  from CollegeMaster where BranchCode=" + Session["BranchCode"] + "";
            conCampus.ReturnTag(_sql, "CollegeShortNa");

            if (fmobileNo == "") return;
            sadpNew.Send(mess, fmobileNo, "1");
        }
        catch
        {
            // ignored
        }
    }
}


   