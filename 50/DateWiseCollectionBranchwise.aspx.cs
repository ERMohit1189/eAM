using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DateWiseCollectionBranchwise : Page
{
    private SqlConnection conn;
    private readonly Campus oo;
    private string sql, SQL1 = String.Empty;
    private int mo;
    public DateWiseCollectionBranchwise()
    {
        conn = new SqlConnection();
        oo = new Campus();
        mo = 0;
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Logintype"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (Session["Logintype"].ToString() == "SupperAdmin")
        {
            MasterPageFile = "~/50/sadminRootManager.master";
        }
        conn = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            sql = "Select BranchId, BranchName from Branchtab";
            var dt = oo.Fetchdata(sql);
            ddlBranch.DataSource = dt;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "BranchId";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
            oo.AddDateMonthYearDropDown(DDYear, DDMonth, DDDate);
            oo.FindCurrentDateandSetinDropDown(DDYear, DDMonth, DDDate);


        }
    }
    protected void DDYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(DDYear, DDMonth, DDDate);
    }
    protected void DDMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(DDYear, DDMonth, DDDate);

    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        oo.ExportTolandscapeWord(Response, "TuitionFeeReport", gdv1);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        oo.ExportDivToExcelWithFormatting(Response, "TuitionFeeReport.xls", gdv1, Server.MapPath("~/Admin/css/style.css"));
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        oo.ExporttolandscapePdf(Response, "TuitionFeeReport", abc);
    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        string date = DDDate.SelectedItem.Text + "-" + DDMonth.SelectedItem.Text + "-" + DDYear.SelectedItem.Text;
        List<SqlParameter> param = new List<SqlParameter>();
        heading.Text = "Daily Collection Report of " + date;
        param.Add(new SqlParameter("@Date", date));
        if (ddlBranch.SelectedIndex != 0)
        {
            param.Add(new SqlParameter("@BranchCode", ddlBranch.SelectedValue));
            lblRegister.Text = "Branch Name : " + ddlBranch.SelectedItem.Text;
        }
        if (DdlpaymentMode.SelectedIndex != 0)
        {
            param.Add(new SqlParameter("@Mode", DdlpaymentMode.SelectedValue));
        }
        if (drpStatus.SelectedIndex != 0)
        {
            param.Add(new SqlParameter("@Status", drpStatus.SelectedValue));
        }
        DataTable dt;
        DataSet ds;
        ds = new DLL().Sp_SelectRecord_usingExecuteDataset("sp_DatewiseCollectionBranchwise", param);
        if (ds != null)
        {
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                sql = "Select Organization, Address from tbl_SocietyOrTrust";
                lblOrganization.Text = oo.ReturnTag(sql, "Organization");
                lblAddress.Text = oo.ReturnTag(sql, "Address");
                divExport.Visible = true;
                dt = ds.Tables[0];
                abc.Visible = true;
                GrdDisplay.DataSource = dt;
                GrdDisplay.DataBind();
                double amt = 0;
                for (int i = 0; i < GrdDisplay.Rows.Count; i++)
                {
                    Label amount = (Label)GrdDisplay.Rows[i].FindControl("Amount");
                    amt = amt + double.Parse(amount.Text);
                }
                Label FooterTotal = (Label)GrdDisplay.FooterRow.FindControl("FooterTotal");
                FooterTotal.Text = amt.ToString("0.00");
            }
            else
            {
                divExport.Visible = false;
                abc.Visible = false;
            }
        }
    }
    public override void Dispose()
    {
        conn.Dispose();
        oo.Dispose();
    }
}