using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

public partial class admin_ClassTeacherList : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        Campus camp = new Campus(); camp.LoadLoader(loader);
        BLL.BLLInstance.LoadHeader("Report",header);
        if (!IsPostBack)
        {
            loadGrid();
        }
    }

    private void loadGrid()
    {
        DataSet ds = new DataSet();

        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@QueryFor", "S"));
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));

        ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("ClassTeacherMasterProc", param);

        if (ds.Tables.Count > 0)
        {
            Grd1.DataSource = ds;
            Grd1.DataBind();
        }
    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        BAL.objBal.ExportToWord(Response, "ClassTeacherList", abc);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        BAL.objBal.ExportDivToExcelWithFormatting(Response,"ClassTeacherList", abc);
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        BAL.objBal.ExporttolandscapePdf(Response, "ClassTeacherList", abc);
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