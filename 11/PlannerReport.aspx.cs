using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;

public partial class sp_PlannerReport : System.Web.UI.Page
{
    string sql = "";
    Campus oo = new Campus();
    protected void Page_PreInIt(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (Session["Logintype"] == null) { Response.Redirect("~/default.aspx"); }
        if (Session["Logintype"].ToString() == "Admin")
        {
            this.MasterPageFile = "~/Master/admin_root-manager.master";
        }
        else if (Session["Logintype"].ToString() == "Staff")
        {
            this.MasterPageFile = "~/Staff/staff_root-manager.master";
        }
        else if (Session["Logintype"].ToString() == "Guardian")
        {
            this.MasterPageFile = "~/Sp/sp_root-manager.master";
        }
        else if (Session["Logintype"].ToString() == "Student")
        {
            this.MasterPageFile = "~/13/stuRootManager.master";
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Campus camp = new Campus(); camp.LoadLoader(loader);
        BLL.BLLInstance.LoadHeader("Report", header);
        if (!IsPostBack)
        {
            sql = "Select FromDate,toDate from SessionMaster where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            DateTime date1 = Convert.ToDateTime(BAL.objBal.ReturnTag(sql, "FromDate"));
            DateTime date2 = Convert.ToDateTime(BAL.objBal.ReturnTag(sql, "toDate"));
            oo.AddDateMonthYearDropDown(FromYY, FromMM, FromDD);
            oo.AddDateMonthYearDropDown(ToYY, ToMM, ToDD);
            FromDD.SelectedValue = int.Parse(date1.ToString("dd")).ToString();
            FromMM.SelectedValue = date1.ToString("MMM").ToString();
            FromYY.SelectedValue = date1.ToString("yyyy").ToString();
            ToDD.SelectedValue = int.Parse(date2.ToString("dd")).ToString();
            ToMM.SelectedValue = date2.ToString("MMM").ToString();
            ToYY.SelectedValue = date2.ToString("yyyy").ToString();
            if (Session["Logintype"].ToString() == "Guardian")
            {
                loadGrid();
            }
            else
            {
                Load_Grid();
            }
        }
    }
    protected void FromYY_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(FromYY, FromMM, FromDD);
    }
    protected void FromMM_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(FromYY, FromMM, FromDD);
    }
    protected void ToYY_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(ToYY, ToMM, ToDD);
    }
    protected void ToMM_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(ToYY, ToMM, ToDD);
    }
    public void loadGrid()
    {
        List<SqlParameter> param = new List<SqlParameter>();

        param.Add(new SqlParameter("@srno", Session["Srno"].ToString()));    
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));

        Grd.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("GetPlannerMasterReport_Proc", param);
        Grd.DataBind();        
    }

    private void Load_Grid()
    {
        List<SqlParameter> param = new List<SqlParameter>();
        string fromdate = FromYY.SelectedItem.Text + " " + FromMM.SelectedItem.Text + " " + FromDD.SelectedItem.Text;
        string todate = ToYY.SelectedItem.Text + " " + ToMM.SelectedItem.Text + " " + ToDD.SelectedItem.Text;
        param.Add(new SqlParameter("@start", fromdate.Trim()));
        param.Add(new SqlParameter("@end", todate.Trim()));
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));

        Grd.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("GetPlannerReportForAdmin_Proc", param);
        Grd.DataBind();

        if (Grd.Rows.Count > 0)
        {
            mainDiv.Visible = true;
            divbtns.Visible = true;
        }
        else
        {
            mainDiv.Visible = false;
            divbtns.Visible = false;
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
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
        if (Grd.Rows.Count > 0)
        {
            Grd.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }
    protected void lnkShow_Click(object sender, EventArgs e)
    {
        Load_Grid();
    }
    public void loadReport()
    {
        if (rbList.SelectedIndex == 0)
        {
            Response.Redirect("~/Planner.aspx");
        }
        else
        {
            Response.Redirect("~/Common/PlannerReport.aspx");
        }
    }
    protected void rbList_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadReport();
    }
}
