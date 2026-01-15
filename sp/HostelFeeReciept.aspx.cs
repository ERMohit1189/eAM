using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;
using System.Globalization;

public partial class HostelFeeReciept : Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["HostelReceiptNo"] == null)
        {
            Response.Redirect("AdmissionForm.aspx");
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
        }
    }


    public void DisplayStudentCopy()
    {
        Label1.Text = Session["HostelReceiptNo"].ToString();


        sql = "select SrNoOrEmpid as srno from HostelFeeDeposit where ReceiptNo = '" + Label1.Text + "'";

        string srno = oo.ReturnTag(sql, "srno");

        sql = "Select Amount, Total,Exemption,Paid, NextDue, fine,ofc.LoginName,FORMAT(DepositDate, 'dd MMM yyyy') as RecieptDate,";
        sql = sql + " Name as StudentName, asr.Srno,ReceiptNo,FatherName,ClassName,SectionName,Gender, asr.classid as classids, 'HOSTEL FEE' AS HeadName, ofc.Status, asr.ISDisplay,asr.BranchName";
        sql = sql + " from AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "'," + Session["BranchCode"].ToString() + ") asr";
        sql = sql + " inner join HostelFeeDeposit ofc on ofc.SrNoOrEmpid=asr.SrNo and ofc.SessionName=asr.SessionName ";
        sql = sql + " where ofc.SrNoOrEmpid='" + srno + "' and ofc.ReceiptNo='" + Label1.Text + "' ";

        if (oo.ReturnTag(sql, "Statas") == "")
        {
            Label2.Visible = false;
        }
        else
        {
            Label2.Visible = true;
        }

        hdnClsssId.Value = oo.ReturnTag(sql, "classids");


        lblRecieptNo.Text = oo.ReturnTag(sql, "ReceiptNo");
        lblStudentName.Text = oo.ReturnTag(sql, "StudentName");
        lblFatherName.Text = oo.ReturnTag(sql, "FatherName");
        lblRecieptDate.Text = oo.ReturnTag(sql, "RecieptDate");
        lblClass.Text = oo.ReturnTag(sql, "ClassName");
        if (oo.ReturnTag(sql, "ISDisplay").ToUpper() == "TRUE")
        {
            lblBranch.Text = oo.ReturnTag(sql, "BranchName");
        }
        lblSection.Text = oo.ReturnTag(sql, "SectionName");
        lblGender.Text = oo.ReturnTag(sql, "Gender");
        lblSrno.Text = oo.ReturnTag(sql, "Srno");
        GridView1.DataSource = oo.GridFill(sql);
        GridView1.DataBind();

        sql = "Select Top 1 DepositDate, Paid, LoginName  from HostelFeeDeposit where ReceiptNo='" + Label1.Text + "' and BranchCode = " + Session["BranchCode"] + "";
        lblFooterDate.Text = DateTime.Parse(oo.ReturnTag(sql, "DepositDate")).ToString("dd MMM yyyy hh:mm tt");
        lblUserName.Text = oo.ReturnTag(sql, "LoginName");
        string aa = "0";
        aa = double.Parse(oo.ReturnTag(sql, "Paid")).ToString(CultureInfo.InvariantCulture);
        lblTotalAmt.Text =  double.Parse(oo.ReturnTag(sql, "Paid")).ToString(CultureInfo.InvariantCulture);

        lblAmountinWords.Text = oo.NumberToString(Convert.ToInt64(aa));
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
        PrintHelper_New.ctrl = xyz;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }

    private void FeeHeadName()
    {
        sql = "select 'HOSTEL FEE' AS HeadName, Amount, '' AS remark from  HostelFeeDeposit where ReceiptNo='" + Session["HostelReceiptNo"].ToString() + "' and BranchCode = " + Session["BranchCode"] + "";
        sql = sql + " and SessionName = '" + Session["SessionName"].ToString() + "'";

        lblremark.Text = oo.ReturnTag(sql, "remark");

        GridView3.DataSource = oo.GridFill(sql);
        GridView3.DataBind();

        double total = 0;
        for (int i = 0; i < GridView3.Rows.Count; i++)
        {
            Label lblAmt = (Label)GridView3.Rows[i].FindControl("lbl_amount");
            total = total + double.Parse(lblAmt.Text);
        }
        lblHeadTotal.Text = total.ToString("0.00");
    }
}