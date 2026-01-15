using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_LIBBOOKFINE : Page
{
    List<SqlParameter> param = new List<SqlParameter>();
    string msg = string.Empty;
    Campus oo = new Campus();
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
            string sql = "Select BranchId, BranchName from Branchtab";
            var dt = oo.Fetchdata(sql);
            ddlBranch.DataSource = dt;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "BranchId";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlBranch.SelectedValue = Session["BranchCode"].ToString();

            string sqls = "select SessionName from SessionMaster where BranchCode=" + ddlBranch.SelectedValue + "";
            var dt2 = oo.Fetchdata(sqls);
            DrpSessionName.DataSource = dt2;
            DrpSessionName.DataTextField = "SessionName";
            DrpSessionName.DataValueField = "SessionName";
            DrpSessionName.DataBind();
            DrpSessionName.Items.Insert(0, new ListItem("<--All-->", ""));
            if (Session["LoginType"].ToString() == "Admin")
            {
                divBranch.Visible = false;
                divSession.Visible = true;
            }
            else
            {
                divBranch.Visible = true;
                divSession.Visible = true;
            }
            getCurrentSessionFromtodate();
            loadGrid();
        }
    }

    public void getCurrentSessionFromtodate()
    {
        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@Query", "S"));
        param.Add(new SqlParameter("@BranchCode", ddlBranch.SelectedValue));
        DataSet ds = new DataSet();
        ds=DLL.objDll.SelectRecord_usingExecuteDataset("USP_GetCurrentSessionFromtodate", param);

        if (ds != null)
        {
            if(ds.Tables.Count>0)
            {
                if(ds.Tables[0].Rows.Count>0)
                {
                    string fromdate = Convert.ToDateTime(ds.Tables[0].Rows[0][0].ToString()).ToString("dd-MMM-yyyy");
                    txtFrom.Text = fromdate;
                    string todate = Convert.ToDateTime(ds.Tables[0].Rows[0][1].ToString()).ToString("dd-MMM-yyyy");
                    txtTo.Text = todate;
                }
            }
        }
    }

    public void loadGrid()
    {
        param = new List<SqlParameter>();
        if (DrpSessionName.SelectedIndex!=0)
        {
            param.Add(new SqlParameter("@SessionName", DrpSessionName.SelectedValue));
        }
        param.Add(new SqlParameter("@BranchCode", ddlBranch.SelectedValue));
        param.Add(new SqlParameter("@FROMDATE", txtFrom.Text.Trim()));
        param.Add(new SqlParameter("@TODATE", txtTo.Text.Trim()));
        grd1.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("GET_LIBBOOKFINEPROC", param);
        grd1.DataBind();
        if (grd1.Rows.Count > 0)
        {
            DivPrints.Visible = true;
            divExport.Visible = true;
        }
        else
        {
            DivPrints.Visible = false;
            divExport.Visible = false;
        }
    }


    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedIndex == 0)
        {
            DrpSessionName.Items.Clear();
            DrpSessionName.Items.Insert(0, new ListItem("<--Select Session-->", ""));
            return;
        }
        string sql = "select SessionName from SessionMaster where BranchCode=" + ddlBranch.SelectedValue + "";
        var dt2 = oo.Fetchdata(sql);
        DrpSessionName.DataSource = dt2;
        DrpSessionName.DataTextField = "SessionName";
        DrpSessionName.DataValueField = "SessionName";
        DrpSessionName.DataBind();
        DrpSessionName.Items.Insert(0, new ListItem("<--Select Session-->", ""));
        DrpSessionName.SelectedIndex = (DrpSessionName.Items.Count - 1);
        if (Session["LoginType"].ToString() == "Admin")
        {
            DrpSessionName.SelectedValue = Session["SessionName"].ToString();
        }

    }
    protected void lnkShow_Click(object sender, EventArgs e)
    {
       
            loadGrid();
       
    }

    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        BAL.objBal.ExportToWord(Response, "LIBBOOKFINE", divExport);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        BAL.objBal.ExportDivToExcelWithFormatting(Response, "LIBBOOKFINE", divExport);
    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = divExport;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");

    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        BAL.objBal.ExporttoPdf(Response, "LIBBOOKFINE", divExport);
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    double totalamount = 0;
    protected void grd1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblAmount = (Label)e.Row.FindControl("lblAmount");
            double amount = 0;
            double.TryParse(lblAmount.Text.Trim(), out amount);
            totalamount = totalamount + amount;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblTotalAmount = (Label)e.Row.FindControl("lblTotalAmount");
            lblTotalAmount.Text = totalamount.ToString(CultureInfo.InvariantCulture);
        }
    }
}