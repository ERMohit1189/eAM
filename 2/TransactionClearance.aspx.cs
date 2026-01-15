using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using c4SmsNew;
using System.Data;
using System.Threading;
using System.Web.UI.HtmlControls;

public partial class TransactionClearance : Page
{
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    Campus oo = new Campus();
    string sql = "";
    int mo = 0;
    BAL.DepositedUnclearedChequeorDD bal = new BAL.DepositedUnclearedChequeorDD();
    DAL dal = new DAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

        if (!IsPostBack)
        {
            txtFromDate.Text = BAL.objBal.CurrentMonthFirstDate().ToString("dd-MMM-yyyy");
            txtToDate.Text = BAL.objBal.CurrentDateinDatetime().ToString("dd-MMM-yyyy");

            loadGrid();
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        loadGrid();
    }
    protected void loadGrid()
    {
        rptStudent.DataSource = null;
        rptStudent.DataBind();
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = "TransactionClearanceProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fromDate", txtFromDate.Text.Trim());
                cmd.Parameters.AddWithValue("@toDate", txtToDate.Text.Trim());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString().Trim());
                cmd.Parameters.AddWithValue("@Action", "GetSrNO");
                SqlDataAdapter das = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                das.Fill(dt);
                cmd.Parameters.Clear();
                if (dt.Rows.Count > 0)
                {
                    trChkAll.Visible = true;
                    rptStudent.DataSource = dt;
                    rptStudent.DataBind();
                    for (int i = 0; i < rptStudent.Items.Count; i++)
                    {
                        Repeater RepeaterFee = (Repeater)rptStudent.Items[i].FindControl("RepeaterFee");
                        Label lblChequeNo = (Label)rptStudent.Items[i].FindControl("lblChequeNo");
                        Label lblDate = (Label)rptStudent.Items[i].FindControl("lblDate");
                        Label lblBankName = (Label)rptStudent.Items[i].FindControl("lblBankName");

                        cmd.Connection = conn;
                        cmd.CommandText = "TransactionClearanceProc";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ChequeNo", lblChequeNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@ChequeDate", lblDate.Text.Trim());
                        cmd.Parameters.AddWithValue("@BankName", lblBankName.Text.Trim());
                        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString().Trim());
                        cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString().Trim());
                        cmd.Parameters.AddWithValue("@Action", "GetDetails");

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dtFee = new DataTable();
                        das.Fill(dtFee);
                        cmd.Parameters.Clear();
                        RepeaterFee.DataSource = null;
                        RepeaterFee.DataBind();
                        if (dtFee.Rows.Count > 0)
                        {
                            LnkSubmit.Visible = true;
                            RepeaterFee.DataSource = dtFee;
                            RepeaterFee.DataBind();
                            double total = 0;
                            for (int j = 0; j < RepeaterFee.Items.Count; j++)
                            {
                                Label lblPaidAmount = (Label)RepeaterFee.Items[j].FindControl("lblPaidAmount");
                                total = total + double.Parse((lblPaidAmount.Text == "" ? "0" : lblPaidAmount.Text));
                            }
                            var lblTotal = (Label)RepeaterFee.Controls[RepeaterFee.Controls.Count - 1].FindControl("lblTotal");
                            lblTotal.Text = total.ToString("0.00");
                        }
                        else
                        {
                            LnkSubmit.Visible = false;
                        }
                    }
                }
                else
                {
                    trChkAll.Visible = false;
                    LnkSubmit.Visible = false;
                }
            }
        }
    }
    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        if (chkAll.Checked)
        {
            for (int i = 0; i < rptStudent.Items.Count; i++)
            {
                CheckBox chk = (CheckBox)rptStudent.Items[i].FindControl("chk");
                chk.Checked = true;
            }
        }
        else
        {
            for (int i = 0; i < rptStudent.Items.Count; i++)
            {
                CheckBox chk = (CheckBox)rptStudent.Items[i].FindControl("chk");
                chk.Checked = false;
            }
        }
    }
    protected void LnkSubmit_Click(object sender, EventArgs e)
    {
        saveBounce();
        
    }
    protected void saveBounce()
    {
        bool chks = false;
        for (int i = 0; i < rptStudent.Items.Count; i++)
        {
            CheckBox chk = (CheckBox)rptStudent.Items[i].FindControl("chk");
            if (chk.Checked)
            {
                chks = true;
            }
        }
        if (!chks)
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Please select at-least one transaction to clear.", "A");
        }
        else
        {
            for (int i = 0; i < rptStudent.Items.Count; i++)
            {
                CheckBox chk = (CheckBox)rptStudent.Items[i].FindControl("chk");
                DropDownList ddlStatus = (DropDownList)rptStudent.Items[i].FindControl("ddlStatus");
                if (chk.Checked == true && ddlStatus.SelectedValue != "Pending")
                {
                    Label lblChequeNo = (Label)rptStudent.Items[i].FindControl("lblChequeNo");

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "TransactionClearanceSaveProc";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;

                    cmd.Parameters.AddWithValue("@ChequeNo", lblChequeNo.Text.Trim());
                    cmd.Parameters.AddWithValue("@Status", (ddlStatus.SelectedValue.Trim() == "Bounced" ? "Cancelled" : ddlStatus.SelectedValue.Trim()));
                    cmd.Parameters.AddWithValue("@ChequeStatus", (ddlStatus.SelectedValue.Trim() == "Paid" ? "" : ddlStatus.SelectedValue.Trim()));
                    cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
                    cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                    cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                    cmd.Parameters.Add("@ReturnMobile", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        string FmobileNo = cmd.Parameters["@ReturnMobile"].Value.ToString();
                        con.Close();
                        loadGrid();
                        Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully", "S");
                        if (FmobileNo != "")
                        {
                            SendFeesSms(FmobileNo.ToString(), lblChequeNo.Text);
                        }
                    }
                    catch (Exception ex) { }
                }
            }

        }
    }
    public void SendFeesSms(string FmobileNo, string Checkno)
    {
        SMSAdapterNew sadpNew = new SMSAdapterNew();
        string mess = "";  
        mess = "Paid fee against Check No.-" + Checkno + " has been cancelled due to dishonour of your cheque.";

        string sms_response = "";

        if (FmobileNo != "")
        {
            sql = "Select SmsSent From SmsEmailMaster where Id='1' ";
            if (oo.ReturnTag(sql, "SmsSent").Trim() == "true")
            {
                sms_response = sadpNew.Send(mess, FmobileNo, "1");
            }
        }
    }
    
}