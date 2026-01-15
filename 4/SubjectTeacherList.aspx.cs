using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SubjectTeacherList : Page
{
#pragma warning disable 169
    string sql = "";
#pragma warning restore 169
    Campus _oo = new Campus();
    private SqlConnection _con;
    DataTable dt;

    SqlCommand cmd = new SqlCommand();
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((string)Session["LoginName"] == "" || (string)Session["BranchCode"] == "" || (string)Session["SessionName"] == "" || Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        _con = new SqlConnection();
        _con = _oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);
        BLL.BLLInstance.LoadHeader("Report", header);
        if (!IsPostBack)
        {
            loadClass();
            BAL.objBal.fillSelectvalue(drpSection, "<--Select-->");
            BAL.objBal.fillSelectvalue(drpBranch, "<--Select-->");
        }
    }
    protected void drpClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadBranch();
        loadSection();
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        loadGrid();
    }
    protected void loadClass()
    {
        sql = "Select ClassName,Id from ClassMaster";
        sql = sql + "  where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        sql = sql + " order by CIDOrder";
        BAL.objBal.FillDropDown_withValue(sql, drpClass, "ClassName", "Id");
        drpClass.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
    }

    protected void loadBranch()
    {
        sql = "Select BranchName,Id from BranchMaster where ClassId=" + drpClass.SelectedValue.ToString();
        sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

        BAL.objBal.FillDropDown_withValue(sql, drpBranch, "BranchName", "Id");

        drpBranch.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));

    }

    protected void loadSection()
    {
        sql = "Select SectionName,id from SectionMaster where ClassNameId=" + drpClass.SelectedValue.ToString();
        sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

        BAL.objBal.FillDropDown_withValue(sql, drpSection, "SectionName", "id");

        drpSection.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));

    }
    private void loadGrid()
    {
        DataSet ds = new DataSet();
        cmd = new SqlCommand();
        cmd.CommandText = "ICSESubjectTeacherAllotmentProc";
        cmd.Connection = _con;
        cmd.CommandType = CommandType.StoredProcedure;
        if (drpClass.SelectedIndex > 0)
        {
            cmd.Parameters.AddWithValue("@ClassId", drpClass.SelectedValue);
        }
        if (drpBranch.SelectedIndex > 0)
        {
            cmd.Parameters.AddWithValue("@BranchId", drpBranch.SelectedValue);
        }
        if (drpBranch.SelectedIndex > 0)
        {
            cmd.Parameters.AddWithValue("@SectionId", drpSection.SelectedValue);
        }
        cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
        cmd.Parameters.AddWithValue("@QueryFor", "S");
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        if (ds.Tables.Count > 0)
        {
            heading.Text = "List of Subject Teachers";
            pnlcontrols.Visible = true;
            abc.Visible = true;
            Grd1.DataSource = ds;
            Grd1.DataBind();
        }
    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        //BAL.objBal.ExportToWord(Response, "SubjectTeacherList", abc);
        _oo.ExportTolandscapeWord(Response, "SubjectTeacherList", gdv1);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        //BAL.objBal.ExportDivToExcelWithFormatting(Response,"SubjectTeacherList", abc);
        _oo.ExportDivToExcelWithFormatting(Response, "SubjectTeacherList.xls", gdv1, Server.MapPath("~/Admin/css/style.css"));
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        BAL.objBal.ExporttolandscapePdf(Response, "SubjectTeacherList", abc);
        //_oo.ExporttolandscapePdf(Response, "SubjectTeacherList", abc);
    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
}