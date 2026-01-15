using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;

public partial class sp_Circular : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Campus camp = new Campus(); camp.LoadLoader(loader);
        //loadGrid();
    }
    //private void loadGrid()
    //{
    //    //List<SqlParameter> param = new List<SqlParameter>();

    //    //param.Add(new SqlParameter("@Srno", Session["Srno"].ToString()));
    //    //param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
    //    //param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));

    //    //grdDocList.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("Std_AssignmentReport_proc", param);
    //    //grdDocList.DataBind();

    //    //for (int i = 0; i < grdDocList.Rows.Count; i++)
    //    //{
    //    //    Label lblDocPath = (Label)grdDocList.Rows[i].FindControl("lblDocPath");
    //    //    HyperLink lnkPath = (HyperLink)grdDocList.Rows[i].FindControl("lnkPath");

    //    //    string path = lblDocPath.Text.Replace("~/", "../");
    //    //    lnkPath.NavigateUrl = path;
    //    }
    //}
    //protected void lnkPath_Click(object sender, EventArgs e)
    //{
    //    LinkButton lnk = (LinkButton)sender;
    //    Label lblDocPath = (Label)lnk.NamingContainer.FindControl("lblDocPath");
    //    if (File.Exists(Server.MapPath(lblDocPath.Text)))
    //    {
    //        Response.ContentType = ContentType;
    //        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(lblDocPath.Text));
    //        // ReSharper disable once AssignNullToNotNullAttribute
    //        Response.WriteFile(lblDocPath.Text);
    //        Response.End();
    //    }
    //    else
    //    {
    //        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, File is missing!", "A");
    //    }
    //}
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
}