using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Globalization;

public partial class StudentReceiptGenerateOnline : Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        if ((string)Session["RecieptNoSession"] == "" || Session["RecieptNoSession"] == null)
        {
            if ((string)Session["FD"] == "TFD")
            {
                Response.Redirect("TransportFeeDeposit.aspx");
            }
            else if ((string)Session["FD"] == "FD")
            {
                Response.Redirect("fee_deposit_New.aspx");
            }
        }
        BLL.BLLInstance.LoadReceiptHeader(header1);
        Campus camp = new Campus(); camp.LoadLoader(loader);
        Label1.Text = Session["RecieptNoSession"].ToString();
        //ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        //scriptManager.RegisterPostBackControl(this.btnExport);
        if (!IsPostBack)
        {
            try
            {
                BLL obj = new BLL();
       
                LinkButton1.Focus();
                LaserPrint();
            }
            catch (Exception) { }
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = abc;
       ScriptManager.RegisterClientScriptBlock(this.Page,GetType(), "onclick", "var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}",true);
    }

    public void LaserPrint()
    {
        sql = "Select compatibility_level from sys.databases where name='eamdb'";
        string level = BAL.objBal.ReturnTag(sql, "compatibility_level");
        
        sql = "Select  fd.SrNo,asr.Name StudentName,Fd.MOP,asr.Medium,asr.FatherName,";
        sql = sql + "  convert(nvarchar,fd.FeeDepositeDate,106) as FeeDepositeDate,asr.TypeOFAdmision,";
        
        if (level == "110")
        {
            sql = sql + "  FORMAT(fd.RecordDate,'MMM dd yyyy hh:mm:ss tt', 'en-us') RecordDate,";
        }
        else
        {
            sql = sql + "  Convert(nvarchar,fd.RecordDate,100) RecordDate,";
        }
        sql = sql + "  fd.LoginName,asr.BranchId,asr.Classid,asr.MODForFeeDeposit,fd.AmountInWords,";
        sql = sql + "  fd.FeeMonth as FeeMonth,fd.Class as Class,fd.section as section,RecievedAmount,RemainingAmount,CurrentAmount,";
        sql = sql + "  Case when fd.Cancel is not null then 'CANCELLED' Else (Case when fd.Status='Pending' then 'PENDING' Else 'PAID' END) END Cancel,";
        sql = sql + "  Case When IsDisplay=1 then BranchName Else '' End BranchName from FeeDeposite fd";
        sql = sql + "  Inner join AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "'," + Session["BranchCode"].ToString() + ") asr on asr.SrNo=fd.SrNo";
        sql = sql + "  where fd.RecieptSrNo='" + Label1.Text + "' and fd.SessionName='" + Session["SessionName"].ToString() + "'";

        lblStatus.Text = oo.ReturnTag(sql, "Cancel").Trim();


        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@SrNo", oo.ReturnTag(sql, "SrNo")));
        param.Add(new SqlParameter("@RecieptSrNo", Label1.Text));
        param.Add(new SqlParameter("@Class", oo.ReturnTag(sql, "Class")));
        param.Add(new SqlParameter("@Classid", oo.ReturnTag(sql, "Classid")));
        param.Add(new SqlParameter("@Month", oo.ReturnTag(sql, "FeeMonth")));
        param.Add(new SqlParameter("@CardType", Session["CardType"].ToString()));
        param.Add(new SqlParameter("@AdmissionType", oo.ReturnTag(sql, "TypeOFAdmision")));
        param.Add(new SqlParameter("@Medium", oo.ReturnTag(sql, "Medium")));
        param.Add(new SqlParameter("@MOD", oo.ReturnTag(sql, "MODForFeeDeposit")));
        param.Add(new SqlParameter("@Branchid", oo.ReturnTag(sql, "BranchId")));
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));

        DataSet ds = new DataSet();
        ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("GetStudentFeeDataProc", param);

        GridView1.DataSource = ds;
        GridView1.DataBind();

        Label Label24 = (Label)GridView1.HeaderRow.FindControl("Label24");
        Label24.Text = oo.ReturnTag(sql, "SrNo");

        try
        {
            Label lblFooterDate = (Label)GridView1.FooterRow.FindControl("lblFooterDate");
            Label lblUserName = (Label)GridView1.FooterRow.FindControl("lblUserName");

        }
        catch (Exception) { }

        try
        {

            Label Label34 = (Label)GridView1.HeaderRow.FindControl("Label34");
            Label34.Text = oo.ReturnTag(sql, "FeeDepositeDate");

        }

        catch (Exception) { }
        try
        {
            Label Label9 = (Label)GridView1.HeaderRow.FindControl("Label9");
            Label9.Text = oo.ReturnTag(sql, "Class");

            Label lblBranch = (Label)GridView1.HeaderRow.FindControl("lblBranch");
            lblBranch.Text = oo.ReturnTag(sql, "BranchName");
        }
        catch (Exception) { }

        try
        {
            Label Label19 = (Label)GridView1.HeaderRow.FindControl("Label19");
            Label19.Text = oo.ReturnTag(sql, "section");
        }
        catch (Exception) { }

        try
        {
            Label Label11 = (Label)GridView1.HeaderRow.FindControl("Label11");
            Label11.Text = oo.ReturnTag(sql, "FeeMonth");
        }
        catch (Exception) { }


        try
        {
            Label Label8 = (Label)GridView1.HeaderRow.FindControl("Label8");
            Label8.Text = oo.ReturnTag(sql, "StudentName");
        }
        catch (Exception) { }


        try
        {

            Label Label23 = (Label)GridView1.HeaderRow.FindControl("Label23");
            Label23.Text = oo.ReturnTag(sql, "FatherName");

        }
        catch (Exception) { }

        try
        {

            Label Label7 = (Label)GridView1.HeaderRow.FindControl("Label7");
            Label7.Text = Label1.Text;
        }
        catch (Exception) { }

        try
        {


            Label Label27 = (Label)GridView1.FooterRow.FindControl("Label27");
            Label27.Text = oo.ReturnTag(sql, "AmountInWords");

        }
        catch (Exception) { }
        try
        {

            Label Label29 = (Label)GridView1.HeaderRow.FindControl("Label29");
            Label29.Text = oo.ReturnTag(sql, "MOP");

        }
        catch (Exception) { }
        double value = 0;
        string Namt;
        try
        {
            Namt = Session["NextDueAmt"].ToString();
            value = double.TryParse(Namt, out value) ? value : 0;
            Label lblNexDueAmt = (Label)GridView1.FooterRow.FindControl("lblNexDueAmt");
            lblNexDueAmt.Text = value.ToString("N", new CultureInfo("en-In"));
        }
        catch (Exception) { }


        try
        {

            Label lblCurrentAmount1 = (Label)GridView1.FooterRow.FindControl("Label14");
            value = double.TryParse(oo.ReturnTag(sql, "RecievedAmount"), out value) ? value : 0;
            lblCurrentAmount1.Text = value.ToString("N", new CultureInfo("en-In"));

        }
        catch (Exception) { }

        try
        {

            Label lblRecievedAmount1 = (Label)GridView1.FooterRow.FindControl("Label18");
            value = double.TryParse(oo.ReturnTag(sql, "CurrentAmount"), out value) ? value : 0;
            lblRecievedAmount1.Text = value.ToString("N", new CultureInfo("en-In"));
        }
        catch (Exception) { }

        try
        {

            Label lblRemainingAmount1 = (Label)GridView1.FooterRow.FindControl("Label30");
            value = double.TryParse(oo.ReturnTag(sql, "RemainingAmount"), out value) ? value : 0;
            lblRemainingAmount1.Text = value.ToString("N", new CultureInfo("en-In"));
        }
        catch (Exception) { }

         try
        {
            Label lblFooterDate = (Label)GridView1.FooterRow.FindControl("lblFooterDate");
            Label lblUserName = (Label)GridView1.FooterRow.FindControl("lblUserName");

            lblFooterDate.Text = oo.ReturnTag(sql, "RecordDate");
            lblUserName.Text = oo.ReturnTag(sql, "LoginName");

        }
        catch (Exception) { }

         try
         {

             Label Label34 = (Label)GridView1.HeaderRow.FindControl("Label34");
             Label34.Text = oo.ReturnTag(sql, "FeeDepositeDate");

         }
         catch
         {
         }

    }

    protected void LinkButton4_Click(object sender, EventArgs e)
    {
       
            if (Session["FD"].ToString() == "FD")
            {
                Response.Redirect("fee_deposit.aspx");
            }
            else if (Session["FD"].ToString() == "TFD")
            {
                Response.Redirect("TransportFeeDeposit.aspx");
                
            }
            else
            {
                Response.Redirect("FeeDepositBalance.aspx");

            }

        
       
    }

    protected void ExportToPdfAsImage(object sender, EventArgs e)
    {

        string base64 = Request.Form[hfImageData.UniqueID].Split(',')[1];
        BAL.objBal.ExportToPdfAsImage(base64,"studentReceipt",Response);

   
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        return;
    }
}

