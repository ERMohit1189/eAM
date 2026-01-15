using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_LibIssuedItemList : Page
{
    List<SqlParameter> param = new List<SqlParameter>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        Campus camp = new Campus(); camp.LoadLoader(loader);
        BLL.BLLInstance.LoadHeader("Report", header);
        if (!IsPostBack)
        {
            loadGrid();
        }
    }
    
    public void loadGrid()
    {
        if (ddlIssuedItemReport.SelectedItem.Value == "0")
        {
            lblheadername.Text = "List of " + ddlIssuedItemReport.SelectedItem.Text;
        }
        param = new List<SqlParameter>();
        param.Add(new SqlParameter("@SessionName", "2020-2021"));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
        param.Add(new SqlParameter("@BookIssueFor", rdotype.SelectedValue));
        param.Add(new SqlParameter("@isreturn",  ddlIssuedItemReport.SelectedItem.Value));
      
        grd1.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("IssueReturnDate_proc", param);
        grd1.DataBind();
    }

    protected void lnkShow_Click(object sender, EventArgs e)
    {      
        loadGrid();      
    }

    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        BAL.objBal.ExportToWord(Response, "LibIssuedItemList", divExport);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        BAL.objBal.ExportDivToExcelWithFormatting(Response, "LibIssuedItemList", divExport);
    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = divExport;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
     
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        BAL.objBal.ExporttoPdf(Response, "LibIssuedItemList", divExport);
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        if (ddlIssuedItemReport.SelectedItem.Value != "")
        {
            if (ddlIssuedItemReport.SelectedItem.Value == "0")
            {
                lblheadername.Text = "List of " + ddlIssuedItemReport.SelectedItem.Text;
            }
            else if (ddlIssuedItemReport.SelectedItem.Value == "1")
            {
                lblheadername.Text = "List of " + ddlIssuedItemReport.SelectedItem.Text;
            }
            else if (ddlIssuedItemReport.SelectedItem.Value == "-1")
            {
                lblheadername.Text = "List of Return and Non Return Item";
            }


            param = new List<SqlParameter>();
            param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
            param.Add(new SqlParameter("@isreturn", ddlIssuedItemReport.SelectedItem.Value));
            param.Add(new SqlParameter("@BookIssueFor", rdotype.SelectedValue));
            grd1.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("IssueReturnDate_proc", param);
            grd1.DataBind();
        }
        else 
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please Select Library Item Type", "W"); 
        }
    }
    protected void grd1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if(e.Row.RowType== DataControlRowType.Header)
        {
            if (ddlIssuedItemReport.SelectedIndex == 0)
            {
                e.Row.Cells[9].Text = "Expected Return Date";
            }
        }

    }
}