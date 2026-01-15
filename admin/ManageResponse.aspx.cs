using c4SmsNew;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Web.UI;

public partial class admin_ManageResponse : Page
{
    private string txtno;
    private string sql;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            if (Request.QueryString["txnid"] != null)
            {
                txtno = Request.QueryString["txnid"];

                param.Add(new SqlParameter("@txtno", txtno));
                param.Add(new SqlParameter("@Srno", Session["SetSrNo"].ToString()));
                DataSet ds = new DataSet();

                ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("getStatus_Proc", param);
                if (ds != null)
                {
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        Session["RecieptSrNo"] = dt.Rows[0]["RecieptSrNo"].ToString();
                        Session["PreviousBalanceAmount"] = dt.Rows[0]["ArrierAmt"].ToString();
                        Session["FD"] = "FD";
                        Session["RCancel"] = dt.Rows[0]["Cancel"].ToString();
                        string recievedAmount = dt.Rows[0]["RecievedAmount"].ToString();
                        string srno = dt.Rows[0]["srno"].ToString();
                        string insttalment = dt.Rows[0]["FeeMonth"].ToString();
                        if (dt.Rows[0]["Cancel"].ToString().Trim() == "" && dt.Rows[0]["status"].ToString().ToLower() == "paid")
                        {
                            sql = "select top(1) FamilyContactNo from StudentFamilyDetails  where srno='" + srno + "'  and SessionName='" + Session["SessionName"].ToString() + "'";
                            string Conta = "";
                            Conta = BAL.objBal.ReturnTag(sql, "FamilyContactNo");
                            try
                            {
                                SendFeesSms(Conta, Session["RecieptSrNo"].ToString(), srno, recievedAmount, insttalment);
                            }
                            catch
                            {
                            }
                        }
                        Session["SetSrNo"] = null;
                    }
                }
                Response.Redirect("../2/FeeReceiptGenerate.aspx?print=1");
            }
        }
    }


    public void SendFeesSms(string FmobileNo, string RecieptNo, string srno, string recievedAmount, string insttalment)
    {
        sql = "Select HitValue from SMSActivateDeactivate where BranchCode=" + Session["BranchCode"] + "";
        if (BAL.objBal.ReturnTag(sql, "HitValue") != "")
        {
            if (BAL.objBal.ReturnTag(sql, "HitValue") == "true")
            {
                SMSAdapterNew sadpNew = new SMSAdapterNew();
                string mess = "";
                string collegeTitle = "";

                sql = "Select FirstName as StudentName   from StudentGenaralDetail  where SessionName='" + Session["SessionName"].ToString() + "'";
                sql = sql + "    and  srno='" + srno + "' and BranchCode=" + Session["BranchCode"] + "";

                mess = "INR " + recievedAmount + " received towards Installment " + insttalment + " for " + BAL.objBal.ReturnTag(sql, "StudentName") + " ( " + srno + " ). Receipt No. " + RecieptNo + "";
                string sms_response = "";

                sql = "Select CollegeShortNa  from CollegeMaster where BranchCode=" + Session["BranchCode"] + "";
                collegeTitle = BAL.objBal.ReturnTag(sql, "CollegeShortNa");

                if (FmobileNo != "")
                {
                    sql = "Select SmsSent From SmsEmailMaster where Id='1' ";
                    if (BAL.objBal.ReturnTag(sql, "SmsSent").Trim() == "true")
                    {
                        sms_response = sadpNew.Send(mess, FmobileNo, "");
                    }
                }
            }
        }
    }
}