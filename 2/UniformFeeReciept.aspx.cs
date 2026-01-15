using System;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UniformFeeReciept : Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["UniformReciept"] == null)
        if (Request.QueryString["UniformReciept"] == null)
        {
            Response.Redirect("UniformFeeDeposit.aspx");
        }
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        if (!IsPostBack)
        {
            DisplayStudentCopy();
            BLL obj = new BLL();
            lblDuplicateStudent.Text = "(ORIGINAL)";
            lblDuplicateSCHOOL.Text = "(ORIGINAL)";
            obj.LoadHeader("Receipt", header1);
            obj.LoadHeader("Receipt", header2);
        }
    }


    public void DisplayStudentCopy()
    {
        string simbol = "";
        string simbolsql = "select top(1) CurrencySymbols from setting";
        if (oo.ReturnTag(simbolsql, "CurrencySymbols") != "")
        {
            simbol = "&#" + oo.ReturnTag(simbolsql, "CurrencySymbols") + ";&nbsp;";
        }
        //Label1.Text = Session["UniformReciept"].ToString();
        Label1.Text = Request.QueryString["UniformReciept"].Replace("__", "/");

        sql = "select srno from uniformFeeDeposit where Receipt_no = '" + Label1.Text + "'";

        string srno = oo.ReturnTag(sql, "srno");

        //sql = "select Row_Number() over (order by Other_fee_collection.Id Asc) as SNo,Other_fee_collection.Statas,Convert(Varchar(11),Other_fee_collection.FeeDepositeDate,106) as FeeDepositeDate,Other_fee_collection.Amount,Other_fee_collection.Concession,Other_fee_collection.ReceivedAmount,StudentOfficialDetails.Srno,Other_fee_Collection_head.HeadName,Other_fee_collection.Receipt_no,Other_fee_collection.Srno,StudentGenaralDetail.FirstName+' '+StudentGenaralDetail.MiddleName+' '+StudentGenaralDetail.LastName as StudentName,StudentFamilyDetails.FatherName,convert(nvarchar,Other_fee_collection.FeeDepositeDate,106) as RecieptDate,ClassMaster.ClassName,SectionMaster.SectionName,StudentGenaralDetail.Gender from Other_fee_collection inner join StudentGenaralDetail on StudentGenaralDetail.SrNo=Other_fee_collection.Srno inner join StudentFamilyDetails on StudentFamilyDetails.SrNo=Other_fee_collection.Srno inner join StudentOfficialDetails on StudentOfficialDetails.SrNo=Other_fee_collection.Srno inner join ClassMaster on ClassMaster.Id=StudentOfficialDetails.AdmissionForClassId inner join SectionMaster on SectionMaster.Id=StudentOfficialDetails.SectionId inner join Other_fee_Collection_head on Other_fee_Collection_head.Id=Other_fee_collection.HeadId where StudentGenaralDetail.SessionName='" + Session["SessionName"].ToString() + "' and StudentFamilyDetails.SessionName='" + Session["SessionName"].ToString() + "' and StudentOfficialDetails.SessionName='" + Session["SessionName"].ToString() + "' and ClassMaster.SessionName='" + Session["SessionName"].ToString() + "' and SectionMaster.SessionName='" + Session["SessionName"].ToString() + "' and Other_fee_collection.SessionName='" + Session["SessionName"].ToString() + "' and Other_fee_collection.Receipt_no='" + Label1.Text + "'";
        sql = "Select sum(AmtForPay) Amount,sum(discount) Concession,sum(PaidAmt) ReceivedAmount,  NextDeuAmt,ofc.LoginName,FORMAT(DepositDate,'dd MMM yyyy') as RecieptDate,";
        sql += " Name as StudentName,asr.Srno,Receipt_no,FatherName,ClassName,SectionName,Gender, ofc.classid as classids,'OTHER FEE' HeadName,PaymentSatus,asr.ISDisplay,asr.BranchName,Mode,PaymentSatus Status";
        sql += " from AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "'," + Session["BranchCode"].ToString() + ") asr";
        sql += " inner join UniformFeeDeposit ofc on ofc.Srno=asr.SrNo and ofc.SessionName=asr.SessionName";
        sql += " where ofc.srno='" + srno + "' and ofc.Receipt_no='" + Label1.Text + "'";
        sql += " Group by Receipt_no,Name,asr.Srno,FatherName,ClassName,SectionName,Gender,PaymentSatus,ISDisplay,BranchName,LoginName,DepositDate, NextDeuAmt, ofc.classid, Mode,PaymentSatus";

        //if (oo.ReturnTag(sql, "Statas") == "")
        //{
        //	Label2.Visible = false;
        //	Label3.Visible = false;
        //}
        //else
        //{
        //	Label2.Visible = true;
        //	Label3.Visible = true;
        //}

        hdnClsssId.Value = oo.ReturnTag(sql, "classids");


        lblRecieptNo0.Text = lblRecieptNo.Text = oo.ReturnTag(sql, "Receipt_no");
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

        lblMode.Text = lblMode0.Text = oo.ReturnTag(sql, "Mode");
        lblStatus.Text = lblStatus0.Text = oo.ReturnTag(sql, "Status");

        GridView1.DataSource = oo.GridFill(sql);
        GridView1.DataBind();
        GridView10.DataSource = oo.GridFill(sql);
        GridView10.DataBind();

        sql = "Select Top 1 Format(DepositDate,'dd MMM yyyy hh:mm:ss tt') DepositDate, PaidAmt, LoginName, CancelledBy, CancelledDate  from uniformFeeDeposit where Receipt_no='" + Label1.Text + "'";
        lblFooterDate0.Text = lblFooterDate.Text = oo.ReturnTag(sql, "DepositDate");
        //lblFooterDate0.Text = lblFooterDate.Text = DateTime.Parse(oo.ReturnTag(sql, "DepositDate")).ToString("dd MMM yyyy hh:mm:ss tt");
        lblUserName0.Text = lblUserName.Text = oo.ReturnTag(sql, "LoginName");

        decimal aa = 0;
        aa = decimal.Parse(oo.ReturnTag(sql, "PaidAmt"));
        lblTotalAmt.Text = lblTotalAmt0.Text = simbol + double.Parse(oo.ReturnTag(sql, "PaidAmt")).ToString(CultureInfo.InvariantCulture);

        lblAmountinWords0.Text = lblAmountinWords.Text = oo.NumberToString(Convert.ToInt64(aa));


        string remarkSql = "Select FeeReceiptRemark1, FeeReceiptRemark2 from CollegeMaster where BranchCode='" + Session["BranchCode"] + "'";
        string remark1 = oo.ReturnTag(remarkSql, "FeeReceiptRemark1");
        string remark2 = oo.ReturnTag(remarkSql, "FeeReceiptRemark2");

        lblRemark1.Text = remark1;
        lblRemark2.Text = remark2;

        lblRemark1_1.Text = remark1;
        lblRemark2_1.Text = remark2;
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
        string simbol = "";
        string simbolsql = "select top(1) CurrencySymbols from setting";
        if (oo.ReturnTag(simbolsql, "CurrencySymbols") != "")
        {
            simbol = "&#" + oo.ReturnTag(simbolsql, "CurrencySymbols") + ";&nbsp;";
        }
        sql = "Select remark from UniformFeeHeadMaster where ClassId = " + hdnClsssId.Value + "";
        sql += " and SessionName = '" + Session["SessionName"].ToString() + "' and Gender = (case when '" + lblGender.Text + "'='FEMALE' then 2 else case when '" + lblGender.Text + "'='Male' then 1 else 3 end end)";

        lblremark.Text = lblremark0.Text = oo.ReturnTag(sql, "remark");

        sql = "Select HeadName, Qty, Amount, (Qty*Amount) Total from UniformHistory where Receipt_no = '" + Request.QueryString["UniformReciept"].Replace("__", "/") + "' and SessionName = '" + Session["SessionName"].ToString() + "'";

        GridView3.DataSource = oo.GridFill(sql);
        GridView3.DataBind();

        GridView30.DataSource = oo.GridFill(sql);
        GridView30.DataBind();
        double total = 0;
        for (int i = 0; i < GridView3.Rows.Count; i++)
        {
            Label lblAmt = (Label)GridView3.Rows[i].FindControl("lbl_Total");
            total = total + double.Parse(lblAmt.Text);
        }
        lblHeadTotal.Text = lblHeadTotal0.Text = simbol + total.ToString("0.00");
    }
}