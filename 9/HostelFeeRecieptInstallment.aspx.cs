using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;
using System.Globalization;

public partial class HostelFeeRecieptInstallment : Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["HostelReceiptNo"] == null)
        {
            Response.Redirect("HostelFeePayment.aspx");
        }
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        if (!IsPostBack)
        {
            DisplayStudentCopy();
            BLL obj = new BLL();
            obj.LoadHeader("Receipt", header1);
            obj.LoadHeader("Receipt", header2);
        }
    }


    public void DisplayStudentCopy()
    {
        Label1.Text = Session["HostelReceiptNo"].ToString();


        sql = "select SrNoOrEmpid as srno from HostelFeeDeposit where ReceiptNo = '" + Label1.Text + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

        string srno = oo.ReturnTag(sql, "srno");

        sql = "Select mm.MonthName as InstallmentName, Amount, Total,Exemption,Paid, NextDue, fine,ofc.LoginName,FORMAT(DepositDate, 'dd MMM yyyy') as RecieptDate,";
        sql = sql + " Name as StudentName, asr.Srno,ReceiptNo,FatherName,ClassName,SectionName,Gender, asr.classid as classids, 'HOSTEL FEE' AS HeadName, ofc.Status, asr.ISDisplay,asr.BranchName";
        sql = sql + " from AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "'," + Session["BranchCode"].ToString() + ") asr";
        sql = sql + " inner join HostelFeeDeposit ofc on ofc.SrNoOrEmpid=asr.SrNo and ofc.SessionName=asr.SessionName inner join MonthMaster mm on mm.MonthId=ofc.InstallmentId and ofc.SessionName=mm.SessionName";
        sql = sql + " where ofc.SrNoOrEmpid='" + srno + "' and ofc.ReceiptNo='" + Label1.Text + "' and ofcBranchCode=" + Session["BranchCode"].ToString() + " and  ofc.status<>'Canceled' ";

        if (oo.ReturnTag(sql, "Statas") == "")
        {
            Label2.Visible = false;
            Label3.Visible = false;
        }
        else
        {
            Label2.Visible = true;
            Label3.Visible = true;
        }

        hdnClsssId.Value = oo.ReturnTag(sql, "classids");


        lblRecieptNo0.Text = lblRecieptNo.Text = oo.ReturnTag(sql, "ReceiptNo");
        lblStudentName0.Text = lblStudentName.Text = oo.ReturnTag(sql, "StudentName");
        lblFatherName0.Text = lblFatherName.Text = oo.ReturnTag(sql, "FatherName");
        lblRecieptDate0.Text = lblRecieptDate.Text = oo.ReturnTag(sql, "RecieptDate");
        lblClass0.Text = lblClass.Text = oo.ReturnTag(sql, "ClassName");
        if (oo.ReturnTag(sql, "ISDisplay").ToUpper() == "TRUE")
        {
            lblBranch0.Text = lblBranch.Text = oo.ReturnTag(sql, "BranchName");
        }
        lblSection0.Text = lblSection.Text = oo.ReturnTag(sql, "SectionName");
        lblGender.Text = lblGender0.Text = oo.ReturnTag(sql, "Gender");
        lblSrno.Text = lblSrno0.Text = oo.ReturnTag(sql, "Srno");
        GridView1.DataSource = oo.GridFill(sql);
        GridView1.DataBind();
        GridView10.DataSource = oo.GridFill(sql);
        GridView10.DataBind();

        sql = "Select Top 1 Recordeddate, Paid, LoginName  from HostelFeeDeposit where ReceiptNo='" + Label1.Text + "' and status<>'Canceled' and BranchCode=" + Session["BranchCode"].ToString() + "";
        lblFooterDate0.Text = lblFooterDate.Text = DateTime.Parse(oo.ReturnTag(sql, "Recordeddate")).ToString("dd MMM yyyy hh:mm:ss tt");
        lblUserName0.Text = lblUserName.Text = oo.ReturnTag(sql, "LoginName");
        string paid = "0"; string balance = "0";

        sql = "Select sum(Paid) Paid, SUM(NextDue) balance  from HostelFeeDeposit where ReceiptNo='" + Label1.Text + "' and status<>'Canceled' and BranchCode=" + Session["BranchCode"].ToString() + "";
        paid = double.Parse(oo.ReturnTag(sql, "Paid")).ToString(CultureInfo.InvariantCulture);
        lblTotalAmt.Text = lblTotalAmt0.Text = double.Parse(oo.ReturnTag(sql, "Paid")).ToString(CultureInfo.InvariantCulture);
        lblAmountinWords.Text = lblAmountinWords0.Text = "(" + oo.NumberToString(Convert.ToInt64(paid)).Trim() + ")";

        balance = double.Parse(oo.ReturnTag(sql, "balance")).ToString(CultureInfo.InvariantCulture);
        lblBalanceAmt.Text = lblBalanceAmt0.Text = double.Parse(oo.ReturnTag(sql, "balance")).ToString(CultureInfo.InvariantCulture);
        lblBalanceAmtinWord.Text = lblBalanceAmtinWord0.Text = "("+oo.NumberToString(Convert.ToInt64(balance)).Trim() + ")";

        FeeHeadName();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        // LaserPrint();
        BLL obj = new BLL();
        obj.LoadHeader("Receipt", header1);
        obj.LoadHeader("Receipt", header2);
        PrintHelper_New.ctrl = xyz;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }

    private void FeeHeadName()
    {
        sql = "select mm.MonthName AS HeadName, Amount, '' AS remark from  HostelFeeDeposit hfd inner join MonthMaster mm on mm.MonthId=hfd.InstallmentId and hfd.SessionName=mm.SessionName where ReceiptNo='" + Session["HostelReceiptNo"].ToString() + "' and hfd.BranchCode=" + Session["BranchCode"].ToString() + " and mm.BranchCode=" + Session["BranchCode"].ToString() + " and hfd.status<>'Canceled'";
        sql = sql + " and  hfd.SessionName = '" + Session["SessionName"].ToString() + "'";

        lblremark.Text = lblremark0.Text = oo.ReturnTag(sql, "remark");
    }
}