using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;
using System.Globalization;

public partial class admin_miscellaneousFeeReciept : Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["OtherReciept1"] == null)
        {
            Response.Redirect("AdmissionForm.aspx");
        }
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        if (!IsPostBack)
        {
            BLL obj = new BLL();
            obj.LoadHeader("Receipt", header1);
            obj.LoadHeader("Receipt", header2);
            DisplayStudentCopy();
           
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
        Label1.Text = Session["OtherReciept1"].ToString();
        //sql = "select Row_Number() over (order by Other_fee_collection_1.Id Asc) as SNo,Convert(nvarchar(25),Other_fee_collection_1.FeeDepositeDate,106) FeeDepositeDate,Convert(nvarchar(25),Other_fee_collection_1.FeeDepositeDate,100) FeeDepositeDatewithtime,Other_fee_collection_1.LoginName,StudentOfficialDetails.Srno,Convert(Varchar(11),Other_fee_collection_1.FeeDepositeDate,106) as FeeDepositeDate,Other_fee_collection_1.Amount,Other_fee_collection_1.Concession,Other_fee_collection_1.ReceivedAmount,Other_fee_collection_head_1.HeadName,Other_fee_collection_1.Receipt_no,Other_fee_collection_1.Srno,StudentGenaralDetail.FirstName+' '+StudentGenaralDetail.MiddleName+' '+StudentGenaralDetail.LastName as StudentName,StudentFamilyDetails.FatherName,convert(nvarchar,Other_fee_collection_1.FeeDepositeDate,106) as RecieptDate,ClassMaster.ClassName,SectionMaster.SectionName,StudentGenaralDetail.Gender from Other_fee_collection_1 inner join StudentGenaralDetail on StudentGenaralDetail.SrNo=Other_fee_collection_1.Srno inner join StudentFamilyDetails on StudentFamilyDetails.SrNo=Other_fee_collection_1.Srno inner join StudentOfficialDetails on StudentOfficialDetails.SrNo=Other_fee_collection_1.Srno inner join ClassMaster on ClassMaster.Id=StudentOfficialDetails.AdmissionForClassId inner join SectionMaster on SectionMaster.Id=StudentOfficialDetails.SectionId inner join Other_fee_collection_head_1 on Other_fee_collection_head_1.Id=Other_fee_collection_1.HeadId where StudentGenaralDetail.SessionName='" + Session["SessionName"].ToString() + "' and StudentFamilyDetails.SessionName='" + Session["SessionName"].ToString() + "' and StudentOfficialDetails.SessionName='" + Session["SessionName"].ToString() + "' and ClassMaster.SessionName='" + Session["SessionName"].ToString() + "' and SectionMaster.SessionName='" + Session["SessionName"].ToString() + "' and Other_fee_collection_1.SessionName='" + Session["SessionName"].ToString() + "' and Other_fee_collection_1.Receipt_no='" + Label1.Text + "'";
        //sql = " SELECT TOP (1) Id, ofc.srno, RecieptNo, StudentName, ofc.FatherName, Class,FORMAT(ofc.RecordDate,'dd MMM yyyy hh:mm:ss tt') RecordDate, LoginName, BranchCode, convert(nvarchar, RecordDate, 106) as RecordDate, convert(nvarchar, ISNULL(CCissuedate, RecordDate), 106) as CCissuedate, Sex, ofc.FatherContactNo, ReceivedAmount, Concession, Amount,";
        //sql = sql + " CASE WHEN asr.IsDisplay = 1 THEN asr.BranchName ELSE '' END BranchName, asr.SectionName";
        //sql = sql + " FROM dbo.Other_fee_collection_1 ofc";
        //sql = sql + " INNER JOIN dbo.AllStudentRecord_UDF('', 1) asr ON asr.SrNo = ofc.srno AND ISNULL(asr.Promotion, '') <> 'Cancelled'";
        //sql = sql + " WHERE ofc.RecieptNo = '" + Session["CCRecieptNo"].ToString() + "' ORDER BY RIGHT(asr.SessionName,4) DESC";

        sql = "select Row_Number() over(order by Other_fee_collection_1.Id Asc) as SNo, asr.Srno, Convert(nvarchar(25), Other_fee_collection_1.FeeDepositeDate, 106) FeeDepositeDate,";
        sql = sql + " Format(Other_fee_collection_1.FeeDepositeDate+CONVERT(TIME,Other_fee_collection_1.RecordDate), 'dd MMM yyyy hh:mm:ss tt') FeeDepositeDatewithtime, Other_fee_collection_1.LoginName, Other_fee_collection_1.Statas, ";
        sql = sql + " Convert(Varchar(11), Other_fee_collection_1.FeeDepositeDate, 106) as FeeDepositeDate, Other_fee_collection_1.Amount, Other_fee_collection_1.Concession,";
        sql = sql + " Other_fee_collection_1.ReceivedAmount, Other_fee_collection_head_1.HeadName,";
        sql = sql + " Other_fee_collection_1.Receipt_no, Other_fee_collection_1.Srno, asr.FirstName + ' ' + asr.MiddleName + ' ' + asr.LastName as StudentName, asr.FatherName, ";
        sql = sql + " convert(nvarchar, Other_fee_collection_1.FeeDepositeDate, 106) as RecieptDate, asr.ClassName, asr.SectionName, asr.Gender,CASE WHEN asr.IsDisplay = 1 THEN asr.BranchName ELSE '' END BranchName";
        sql = sql + " FROM Other_fee_collection_1";

        sql = sql + " INNER join dbo.AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "', " + Session["BranchCode"].ToString() + ") asr on asr.SrNo = Other_fee_collection_1.Srno";
        sql = sql + " INNER join Other_fee_collection_head_1 on Other_fee_collection_head_1.Id = Other_fee_collection_1.HeadId";

        sql = sql + " WHERE Other_fee_collection_1.SessionName = '" + Session["SessionName"].ToString() + "'";
        sql = sql + " AND Other_fee_collection_1.Receipt_no = '" + Label1.Text + "'";

        lblFooterDate0.Text = lblFooterDate.Text = oo.ReturnTag(sql, "FeeDepositeDatewithtime");
        lblUserName0.Text = lblUserName.Text = oo.ReturnTag(sql, "LoginName");

        lblRecieptNo0.Text = lblRecieptNo.Text = oo.ReturnTag(sql, "Receipt_no");
        lblStudentName0.Text = lblStudentName.Text = oo.ReturnTag(sql, "StudentName");
        lblFatherName0.Text = lblFatherName.Text = oo.ReturnTag(sql, "FatherName");
        lblRecieptDate0.Text = lblRecieptDate.Text = oo.ReturnTag(sql, "FeeDepositeDate");
        lblClass.Text = oo.ReturnTag(sql, "ClassName");
        lblClass0.Text = oo.ReturnTag(sql, "ClassName");

        lblSection.Text = oo.ReturnTag(sql, "SectionName");
        lblSection0.Text = oo.ReturnTag(sql, "SectionName");

        lblBranch.Text = oo.ReturnTag(sql, "BranchName");
        lblBranch0.Text = oo.ReturnTag(sql, "BranchName");
        lblSrno.Text = lblSrno0.Text = oo.ReturnTag(sql, "Srno");
        lblGender.Text = lblGender0.Text = oo.ReturnTag(sql, "Gender");
        GridView1.DataSource = oo.GridFill(sql);
        GridView1.DataBind();
        GridView2.DataSource = oo.GridFill(sql);
        GridView2.DataBind();
        Double total = 0;
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            Label lbl_Received = (Label)GridView1.Rows[i].FindControl("lbl_Received");
            total = total + Convert.ToDouble(lbl_Received.Text);
        }
        lblTotalAmt.Text = lblTotalAmt0.Text = simbol + total.ToString(CultureInfo.InvariantCulture);
        long aa = 0;
        aa = Convert.ToInt64(total);
        lblAmountinWords0.Text = lblAmountinWords.Text = oo.NumberToString(aa);
        FeeHeadName();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        BLL obj = new BLL();
        obj.LoadHeader("Receipt", header1);
        obj.LoadHeader("Receipt", header2);
        PrintHelper_New.ctrl = xyz;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }

    private void FeeHeadName()
    {
        sql = "Select HeadName from Other_fee_collection_1 ofc1 ";
        sql = sql + " inner join Other_fee_Collection_head_1 ofch1 on ofch1.Id=ofc1.HeadId";
        sql = sql + " where Receipt_no='" + Session["OtherReciept1"].ToString() + "'";

        Repeater1.DataSource = oo.GridFill(sql);
        Repeater1.DataBind();

        Repeater2.DataSource = oo.GridFill(sql);
        Repeater2.DataBind();
    }
}