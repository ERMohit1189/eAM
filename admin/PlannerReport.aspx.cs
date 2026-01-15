using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class admin_PlannerReport : Page
{
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        BLL.BLLInstance.LoadHeader("Report", header);
        Campus camp = new Campus(); camp.LoadLoader(loader);

        if (!IsPostBack)
        {
            sql = "Select FromDate,toDate from SessionMaster where SessionName='"+Session["SessionName"].ToString()+"'";
            DateTime date1 = Convert.ToDateTime(BAL.objBal.ReturnTag(sql, "FromDate"));
            string fromdate = date1.ToString("dd-MMM-yyyy");
            txtFromDate.Text = fromdate;

            DateTime date2 = Convert.ToDateTime(BAL.objBal.ReturnTag(sql, "toDate"));
            string todate = date2.ToString("dd-MMM-yyyy");
            txtToDate.Text = todate;

            Load_Grid();
        }
    }

    private void Load_Grid()
    {     
        List<SqlParameter> param = new List<SqlParameter>();

        param.Add(new SqlParameter("@start", txtFromDate.Text.Trim()));
        param.Add(new SqlParameter("@end", txtToDate.Text.Trim()));

        Repeater1.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("GetSeprateEventProc", param);
        Repeater1.DataBind();

        if (Repeater1.Items.Count > 0)
        {
            mainDiv.Visible = true;

            Label lblheader = (Label)Repeater1.Controls[0].Controls[0].FindControl("lblHeader");
            lblheader.Text = "PLANNER" + " (" + " From: " + txtFromDate.Text.Trim() + " To: " + txtToDate.Text.Trim() + ")";            
        }
        else
        {
            mainDiv.Visible = false;
        }
    }

    protected void lnkShow_Click(object sender, EventArgs e)
    {
        Load_Grid();
    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        BAL.objBal.ExportTolandscapeWord(Response, "PlannerReport", mainDiv);       
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        BAL.objBal.ExportDivToExcelWithFormatting(Response, "PlannerReport", mainDiv);        
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        BAL.objBal.ExporttolandscapePdf(Response, "PlannerReport", mainDiv);
    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = mainDiv;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }

}