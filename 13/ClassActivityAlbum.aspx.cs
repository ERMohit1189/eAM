using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class sp_ClassActivity : System.Web.UI.Page
{
    string sql = "";
    protected void Page_InIt(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            sql = "Select ClassId,SectionID,BranchId from AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "'," + Session["BranchCode"].ToString() + ")";
            sql = sql + " where SrNo='" + Session["LoginName"].ToString() + "'";
            string classId = "", sectionId = "", branchId = "";
            classId = BAL.objBal.ReturnTag(sql, "ClassId");
            sectionId = BAL.objBal.ReturnTag(sql, "SectionID");
            branchId = BAL.objBal.ReturnTag(sql, "BranchId");
            loadGrid("-1", classId, sectionId, branchId);
        }
    }

    private DataSet select(string id, string classid, string sectionid, string branchid)
    {
        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@QueryFor", "S"));
        param.Add(new SqlParameter("@Id", id));
        param.Add(new SqlParameter("@ClassId", classid));
        param.Add(new SqlParameter("@SectionId", sectionid));
        param.Add(new SqlParameter("@BranchId", branchid));
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));

        DataSet ds = new DataSet();

        rpt1.DataSource = ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("ClassWiseActivityAlbum_Proc", param);
        rpt1.DataBind();

        if (rpt1.Items.Count == 0)
        {
            hr1.Visible = false;
        }
        else
        {
            hr1.Visible = true;
        }

        return ds;
    }
    // ReSharper disable once UnusedMethodReturnValue.Local
    private DataSet loadGrid(string id, string classid, string sectionid, string branchid)
    {
        return select(id, classid, sectionid, branchid);
    }
    protected void lnktitle_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        Label lblId=(Label)lnk.NamingContainer.FindControl("lblId");
        Response.Redirect("ClassActivity.aspx?CWAAID=" + lblId.Text);
    }
    protected void lnkImage_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        Label lblId = (Label)lnk.NamingContainer.FindControl("lblId");
        Response.Redirect("ClassActivity.aspx?CWAAID=" + lblId.Text);
    }
}