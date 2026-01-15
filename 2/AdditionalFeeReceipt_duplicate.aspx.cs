using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdditionalFeeReceipt_duplicate : Page
{
	SqlConnection con = new SqlConnection();
	Campus oo = new Campus();
	string sql = "";
	protected void Page_Load(object sender, EventArgs e)
	{
		if (Request.QueryString["OtherReciept1"] == null)
		{
			Response.Redirect("AdditionalFeesDeposit.aspx");
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
            lblDuplicateStudent.Text = "(DUPLICATE)";
            lblDuplicateSCHOOL.Text = "(DUPLICATE)";
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
		//Label1.Text = Session["OtherReciept1"].ToString();
		Label1.Text = Request.QueryString["OtherReciept1"].Replace("__", "/");

		sql = "select  top(1) asr.Srno, Convert(nvarchar(25), Other_fee_collection_1.FeeDepositeDate, 106) FeeDepositeDate, BounceCharges, ";
		sql += " Format(Other_fee_collection_1.RecordDate, 'dd MMM yyyy hh:mm:ss tt') FeeDepositeDatewithtime, Other_fee_collection_1.LoginName, Other_fee_collection_1.Statas, ";
		sql += " Convert(Varchar(11), Other_fee_collection_1.FeeDepositeDate, 106) as FeeDepositeDate, Other_fee_collection_1.Amount, Other_fee_collection_1.Concession,";
		sql += " Other_fee_collection_1.ReceivedAmount, Other_fee_collection_head_1.HeadName,";
		sql += " Other_fee_collection_1.Receipt_no, Other_fee_collection_1.Srno, asr.FirstName + ' ' + asr.MiddleName + ' ' + asr.LastName as StudentName, asr.FatherName, ";
		sql += " convert(nvarchar, Other_fee_collection_1.FeeDepositeDate, 106) as RecieptDate, asr.ClassName, asr.SectionName, asr.Gender,CASE WHEN asr.IsDisplay = 1 THEN asr.BranchName ELSE '' END BranchName, MOP,Status,CancelledBy,FORMAT(CancelledDate,'dd MMM yyyy hh:mm:ss tt') CancelledDate ";
		sql += " FROM Other_fee_collection_1";
		sql += " INNER join dbo.AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "', " + Session["BranchCode"].ToString() + ") asr on asr.SrNo = Other_fee_collection_1.Srno";
		sql += " INNER join Other_fee_collection_head_1 on Other_fee_collection_head_1.Id = Other_fee_collection_1.HeadId";
		sql += " WHERE Other_fee_collection_1.SessionName = '" + Session["SessionName"].ToString() + "'";
		sql += " AND Other_fee_collection_1.Receipt_no = '" + Label1.Text + "' order by Other_fee_collection_1.id asc";



		string sql2 = "select Row_Number() over(order by Other_fee_collection_1.Id Asc) as SNo, asr.Srno, Convert(nvarchar(25), Other_fee_collection_1.FeeDepositeDate, 106) FeeDepositeDate,";
		sql2 = sql2 + " Format(Other_fee_collection_1.RecordDate, 'dd MMM yyyy hh:mm:ss tt') FeeDepositeDatewithtime, Other_fee_collection_1.LoginName, Other_fee_collection_1.Statas, ";
		sql2 = sql2 + " Convert(Varchar(11), Other_fee_collection_1.FeeDepositeDate, 106) as FeeDepositeDate, Other_fee_collection_1.Amount, Other_fee_collection_1.Concession,";
		sql2 = sql2 + " Other_fee_collection_1.ReceivedAmount, Other_fee_collection_head_1.HeadName,";
		sql2 = sql2 + " Other_fee_collection_1.Receipt_no, Other_fee_collection_1.Srno, asr.FirstName + ' ' + asr.MiddleName + ' ' + asr.LastName as StudentName, asr.FatherName, ";
		sql2 = sql2 + " convert(nvarchar, Other_fee_collection_1.FeeDepositeDate, 106) as RecieptDate, asr.ClassName, asr.SectionName, asr.Gender,CASE WHEN asr.IsDisplay = 1 THEN asr.BranchName ELSE '' END BranchName";
		sql2 = sql2 + " FROM Other_fee_collection_1";
		sql2 = sql2 + " INNER join dbo.AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "', " + Session["BranchCode"].ToString() + ") asr on asr.SrNo = Other_fee_collection_1.Srno";
		sql2 = sql2 + " INNER join Other_fee_collection_head_1 on Other_fee_collection_head_1.Id = Other_fee_collection_1.HeadId";
		sql2 = sql2 + " WHERE Other_fee_collection_1.SessionName = '" + Session["SessionName"].ToString() + "'";
		sql2 = sql2 + " AND Other_fee_collection_1.Receipt_no = '" + Label1.Text + "'";

		lblFooterDate0.Text = lblFooterDate.Text = oo.ReturnTag(sql, "FeeDepositeDatewithtime");
		lblUserName0.Text = lblUserName.Text = oo.ReturnTag(sql, "LoginName");

		lblRecieptNo0.Text = lblRecieptNo.Text = oo.ReturnTag(sql, "Receipt_no");
		lblStudentName0.Text = lblStudentName.Text = oo.ReturnTag(sql, "StudentName");
		lblFatherName0.Text = lblFatherName.Text = oo.ReturnTag(sql, "FatherName");
		lblRecieptDate0.Text = lblRecieptDate.Text = oo.ReturnTag(sql, "FeeDepositeDate");
		lblMode.Text = lblMode0.Text = oo.ReturnTag(sql, "MOP");
		lblStatus.Text = lblStatus0.Text = oo.ReturnTag(sql, "Status");
		lblClass.Text = oo.ReturnTag(sql, "ClassName");
		lblClass0.Text = oo.ReturnTag(sql, "ClassName");

		lblSection.Text = oo.ReturnTag(sql, "SectionName");
		lblSection0.Text = oo.ReturnTag(sql, "SectionName");

		lblBranch.Text = oo.ReturnTag(sql, "BranchName");
		lblBranch0.Text = oo.ReturnTag(sql, "BranchName");
		lblSrno.Text = lblSrno0.Text = oo.ReturnTag(sql, "Srno");
		lblGender.Text = lblGender0.Text = oo.ReturnTag(sql, "Gender");
		GridView1.DataSource = oo.GridFill(sql2);
		GridView1.DataBind();
		GridView2.DataSource = oo.GridFill(sql2);
		GridView2.DataBind();
		double BounceCharges = 0;
		if (oo.ReturnTag(sql, "BounceCharges") != "")
		{
			BounceCharges = double.Parse(oo.ReturnTag(sql, "BounceCharges"));

		}
		Double total = 0;
		for (int i = 0; i < GridView1.Rows.Count; i++)
		{
			Label lbl_Received = (Label)GridView1.Rows[i].FindControl("lbl_Received");
			total = total + Convert.ToDouble(lbl_Received.Text);
		}
		if (BounceCharges > 0)
		{
			total = total + BounceCharges;
			lblbounceCharge.Text = lblbounceCharge0.Text = simbol + BounceCharges.ToString("0.00");
			trbounce.Visible = true;
			trbounce0.Visible = true;
		}
		else
		{
			trbounce.Visible = false;
			trbounce0.Visible = false;
		}
		lblTotalAmt.Text = lblTotalAmt0.Text = simbol + total.ToString("0.00");
		decimal aa = 0;
		aa = Convert.ToDecimal(total);
		lblAmountinWords0.Text = lblAmountinWords.Text = oo.NumberToString(aa);

        if (lblStatus.Text == "Cancelled")
        {
            Label2.Text = oo.ReturnTag(sql, "CancelledBy");
            Label34.Text = oo.ReturnTag(sql, "CancelledBy");

            Label22.Text = oo.ReturnTag(sql, "CancelledDate");
            Label39.Text = oo.ReturnTag(sql, "CancelledDate");
            if (Label22.Text != "")
            {
                grdfeeDetailscancelleddiv.Visible = true;
                grdfeeDetailscancelleddiv1.Visible = true;
            }
        }

        string remarkSql = "Select FeeReceiptRemark1, FeeReceiptRemark2 from CollegeMaster where BranchCode='" + Session["BranchCode"] + "'";
        string remark1 = oo.ReturnTag(remarkSql, "FeeReceiptRemark1");
        string remark2 = oo.ReturnTag(remarkSql, "FeeReceiptRemark2");

        lblRemark1.Text = remark1;
        lblRemark2.Text = remark2;

        lblRemark1_1.Text = remark1;
        lblRemark2_1.Text = remark2;
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
}