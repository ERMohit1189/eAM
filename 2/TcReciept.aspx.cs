using System;
using System.Data.SqlClient;

public partial class admin_TcReciept : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";
    string TCRecieptNo = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["TCRecieptNo"] == null)
        {
            Response.Redirect("TCCollection.aspx");
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
            TCRecieptNo = Request.QueryString["TCRecieptNo"].Replace("__", "/");
            DisplayStudentCopy();
            if (Session["IsDuplicate"].ToString() == "Yes")
            {
                recdeiptType.Text = "DUPLICATE";
                recdeiptType0.Text = "DUPLICATE";
            }
            else
            {
                recdeiptType.Text = "ORIGINAL";
                recdeiptType0.Text = "ORIGINAL";
            }

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
        sql = "SELECT TOP (1) tc.srno,RecieptNo,convert(nvarchar, AdmissionFromDate, 106) as AdmissionFromDate,Status,CheckDDNo,ChqBounceAmount,  case when Status='Paid' then BounceCharges else 0 end BounceCharges,MOP,LoginName,StudentName,Concession,ReceivedAmount,";
        sql += " tc.FatherName,ClassName,LoginName,tc.BranchCode,FORMAT(tc.RecordDate,'dd MMM yyyy hh:mm:ss tt') RecordDate,Sex,tc.FatherContactNo,Amount,";
        sql += " CASE WHEN asr.IsDisplay = 1 THEN asr.BranchName ELSE '' END BranchName,asr.SectionName, format(AdmissionFromDate, 'dd MMM yyyy') AdmissionFromDates, format(TCIssueDate, 'dd MMM yyyy') TCIssueDate";
        sql += " ,tc.tctype,CancelledBy,FORMAT(CancelledDate,'dd MMM yyyy hh:mm:ss tt') CancelledDate FROM dbo.TCCollection tc";
        sql += " INNER JOIN dbo.AllStudentRecord_UDF((select top(1) SessionName from StudentOfficialDetails where SrNo=(select srno from TCCollection where RecieptNo = '" + TCRecieptNo + "') order by id desc)," + Session["BranchCode"].ToString() + ") asr ON asr.SrNo=tc.srno AND ISNULL(asr.Promotion,'')<>'Cancelled'";
        sql += " WHERE tc.RecieptNo = '" + TCRecieptNo + "' ORDER BY RIGHT(asr.SessionName,4) DESC";

        lblAdmissionFees.Text = lblAdmissionFees0.Text = "Transfer Certificate Fee (" + oo.ReturnTag(sql, "tctype") + " Copy)";
        lblFooterDate0.Text = lblFooterDate.Text = oo.ReturnTag(sql, "RecordDate");
        lblUserName0.Text = lblUserName.Text = oo.ReturnTag(sql, "LoginName");

        if (oo.ReturnTag(sql, "CancelledDate").ToString() != "")
        {
            grdfeeDetailscancelleddiv1.Visible = true;
            grdfeeDetailscancelleddiv.Visible = true;
            Label133.Text = Label21.Text = oo.ReturnTag(sql, "CancelledBy");
            Label155.Text = Label27.Text = oo.ReturnTag(sql, "CancelledDate");
        }

        Label1.Text = oo.ReturnTag(sql, "RecieptNo");
        lblSrno.Text = oo.ReturnTag(sql, "srno");
        lblSrno0.Text = oo.ReturnTag(sql, "Srno");

        lblRecieptNo.Text = oo.ReturnTag(sql, "RecieptNo");
        lblRecieptNo0.Text = oo.ReturnTag(sql, "RecieptNo");

        lblStudentName.Text = oo.ReturnTag(sql, "StudentName");
        lblStudentName0.Text = oo.ReturnTag(sql, "StudentName");

        lblRecieptDate.Text = oo.ReturnTag(sql, "AdmissionFromDates");
        lblRecieptDate0.Text = oo.ReturnTag(sql, "AdmissionFromDates");

        lblFatherName.Text = oo.ReturnTag(sql, "FatherName");
        lblFatherName0.Text = oo.ReturnTag(sql, "FatherName");

        lblClass.Text = oo.ReturnTag(sql, "ClassName");
        lblClass0.Text = oo.ReturnTag(sql, "ClassName");

        lblSection.Text = oo.ReturnTag(sql, "SectionName");
        lblSection0.Text = oo.ReturnTag(sql, "SectionName");

        lblBranch.Text = oo.ReturnTag(sql, "BranchName");
        lblBranch0.Text = oo.ReturnTag(sql, "BranchName");


        //lblAdmissionFees.Text = lblAdmissionFees.Text + " of " + oo.ReturnTag(sql, "Class") + " " + oo.ReturnTag(sql, "BranchName");
        //lblAdmissionFees0.Text = lblAdmissionFees0.Text + " of " + oo.ReturnTag(sql, "Class") + " " + oo.ReturnTag(sql, "BranchName");


        lblMode.Text = oo.ReturnTag(sql, "MOP") + (oo.ReturnTag(sql, "CheckDDNo") != "" ? " (" + oo.ReturnTag(sql, "CheckDDNo") + ")" : "");
        lblMode0.Text = oo.ReturnTag(sql, "MOP") + (oo.ReturnTag(sql, "CheckDDNo") != "" ? " (" + oo.ReturnTag(sql, "CheckDDNo") + ")" : "");

        lblStatus.Text = oo.ReturnTag(sql, "Status");
        lblStatus0.Text = oo.ReturnTag(sql, "Status");

        lblAmount.Text = simbol + oo.ReturnTag(sql, "Amount");
        lblAmount0.Text = simbol + oo.ReturnTag(sql, "Amount");

        double cbamount = 0;
        double.TryParse(oo.ReturnTag(sql, "BounceCharges"), out cbamount);
        if (cbamount > 0 && oo.ReturnTag(sql, "Status") == "Paid")
        {
            chqBFee.Visible = true;
            chqBFee0.Visible = true;
        }
        else
        {
            chqBFee.Visible = false;
            chqBFee0.Visible = false;
        }
        lblchkBouncefee.Text = simbol + (oo.ReturnTag(sql, "BounceCharges") == "" ? "0" : oo.ReturnTag(sql, "BounceCharges"));
        lblchkBouncefee0.Text = simbol + (oo.ReturnTag(sql, "BounceCharges") == "" ? "0" : oo.ReturnTag(sql, "BounceCharges"));

        double Concession = 0;
        double.TryParse(oo.ReturnTag(sql, "Concession"), out Concession);
        if (Concession > 0)
        {
            chqConcession.Visible = true;
            chqConcession0.Visible = true;
        }
        else
        {
            chqConcession.Visible = false;
            chqConcession0.Visible = false;
        }
        lblConcession.Text = simbol + oo.ReturnTag(sql, "Concession");
        lblConcession1.Text = simbol + oo.ReturnTag(sql, "Concession");

        lblReceivedAmount.Text = simbol + oo.ReturnTag(sql, "ReceivedAmount");
        lblReceivedAmount1.Text = simbol + oo.ReturnTag(sql, "ReceivedAmount");

        long num = 0;
        try
        {
            decimal amt = Convert.ToDecimal(oo.ReturnTag(sql, "ReceivedAmount"));
            lblAmountinWords.Text = oo.NumberToString(amt);
            lblAmountinWords0.Text = oo.NumberToString(amt);

        }
        catch (Exception) { }


    }


    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        // LaserPrint();
        BLL obj = new BLL();
        obj.LoadHeader("Receipt", header1);
        obj.LoadHeader("Receipt", header2);
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }
}