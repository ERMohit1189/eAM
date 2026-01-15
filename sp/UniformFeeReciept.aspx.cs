using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;
using System.Globalization;

public partial class UniformFeeReciept : Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UniformReciept"] == null)
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
        Label1.Text = Session["UniformReciept"].ToString();


        sql = "select srno from uniformFeeDeposit where Receipt_no = '" + Label1.Text + "'";

        string srno = oo.ReturnTag(sql, "srno");

        //sql = "select Row_Number() over (order by Other_fee_collection.Id Asc) as SNo,Other_fee_collection.Statas,Convert(Varchar(11),Other_fee_collection.FeeDepositeDate,106) as FeeDepositeDate,Other_fee_collection.Amount,Other_fee_collection.Concession,Other_fee_collection.ReceivedAmount,StudentOfficialDetails.Srno,Other_fee_Collection_head.HeadName,Other_fee_collection.Receipt_no,Other_fee_collection.Srno,StudentGenaralDetail.FirstName+' '+StudentGenaralDetail.MiddleName+' '+StudentGenaralDetail.LastName as StudentName,StudentFamilyDetails.FatherName,convert(nvarchar,Other_fee_collection.FeeDepositeDate,106) as RecieptDate,ClassMaster.ClassName,SectionMaster.SectionName,StudentGenaralDetail.Gender from Other_fee_collection inner join StudentGenaralDetail on StudentGenaralDetail.SrNo=Other_fee_collection.Srno inner join StudentFamilyDetails on StudentFamilyDetails.SrNo=Other_fee_collection.Srno inner join StudentOfficialDetails on StudentOfficialDetails.SrNo=Other_fee_collection.Srno inner join ClassMaster on ClassMaster.Id=StudentOfficialDetails.AdmissionForClassId inner join SectionMaster on SectionMaster.Id=StudentOfficialDetails.SectionId inner join Other_fee_Collection_head on Other_fee_Collection_head.Id=Other_fee_collection.HeadId where StudentGenaralDetail.SessionName='" + Session["SessionName"].ToString() + "' and StudentFamilyDetails.SessionName='" + Session["SessionName"].ToString() + "' and StudentOfficialDetails.SessionName='" + Session["SessionName"].ToString() + "' and ClassMaster.SessionName='" + Session["SessionName"].ToString() + "' and SectionMaster.SessionName='" + Session["SessionName"].ToString() + "' and Other_fee_collection.SessionName='" + Session["SessionName"].ToString() + "' and Other_fee_collection.Receipt_no='" + Label1.Text + "'";
        sql = "Select sum(AmtForPay) Amount,sum(discount) Concession,sum(PaidAmt) ReceivedAmount,  NextDeuAmt,ofc.LoginName,FORMAT(DepositDate,'dd MMM yyyy') as RecieptDate,";
        sql = sql + " Name as StudentName,asr.Srno,Receipt_no,FatherName,ClassName,SectionName,Gender, ofc.classid as classids,'OTHER FEE' HeadName,PaymentSatus,asr.ISDisplay,asr.BranchName";
        sql = sql + " from AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "'," + Session["BranchCode"].ToString() + ") asr";
        sql = sql + " inner join UniformFeeDeposit ofc on ofc.Srno=asr.SrNo and ofc.SessionName=asr.SessionName";
        sql = sql + " where ofc.srno='" + srno + "' and ofc.Receipt_no='"+ Label1.Text + "' and ofc.BranchCode = " + Session["BranchCode"] + "";
        sql = sql + " Group by Receipt_no,Name,asr.Srno,FatherName,ClassName,SectionName,Gender,PaymentSatus,ISDisplay,BranchName,LoginName,DepositDate, NextDeuAmt, ofc.classid";

        if (oo.ReturnTag(sql, "Statas") == "")
        {
            Label2.Visible = false;
        }
        else
        {
            Label2.Visible = true;
        }

        hdnClsssId.Value= oo.ReturnTag(sql, "classids");
        

        lblRecieptNo.Text = oo.ReturnTag(sql, "Receipt_no");
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

        sql = "Select Top 1 DepositDate, PaidAmt, LoginName  from uniformFeeDeposit where Receipt_no='" + Label1.Text + "' and BranchCode = " + Session["BranchCode"] + " and BranchCode = " + Session["BranchCode"] + "";
        lblFooterDate.Text = DateTime.Parse(oo.ReturnTag(sql, "DepositDate")).ToString("dd MMM yyyy hh:mm tt");
        lblUserName.Text = oo.ReturnTag(sql, "LoginName");
        string aa = "0";
        aa=double.Parse(oo.ReturnTag(sql, "PaidAmt")).ToString(CultureInfo.InvariantCulture);
        lblTotalAmt.Text =  double.Parse(oo.ReturnTag(sql, "PaidAmt")).ToString(CultureInfo.InvariantCulture);
        
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
        sql = "Select remark from UniformFeeHeadMaster where ClassId = " + hdnClsssId.Value + "";
        sql = sql+" and SessionName = '" + Session["SessionName"].ToString() + "' and BranchCode = " + Session["BranchCode"] + " and Gender = (case when '" + lblGender.Text + "'='FEMALE' then 2 else case when '" + lblGender.Text + "'='Male' then 1 else 3 end end)";

        lblremark.Text= oo.ReturnTag(sql, "remark");

        sql = "Select HeadName, Qty, Amount, (Qty*Amount) Total from UniformHistory where Receipt_no = '" + Session["UniformReciept"] + "' and BranchCode = " + Session["BranchCode"] + " and SessionName = '" + Session["SessionName"].ToString() + "'";

        GridView3.DataSource = oo.GridFill(sql);
        GridView3.DataBind();

        double total = 0;
        for (int i = 0; i < GridView3.Rows.Count; i++)
        {
            Label lblAmt = (Label)GridView3.Rows[i].FindControl("lbl_Total");
            total = total + double.Parse(lblAmt.Text);
        }
        lblHeadTotal.Text = total.ToString("0.00");
    }
}