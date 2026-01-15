using System;
using System.Collections.Generic;
using System.Data.SqlClient;

public partial class sp_Library : System.Web.UI.Page
{
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
       loadGrid();
    }

    private void loadGrid()
    {
        List<SqlParameter> param = new List<SqlParameter>();

        param.Add(new SqlParameter("@Srno", Session["LoginName"].ToString()));

        grdDocList.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_LibraryIssuedItemStudent", param);
        grdDocList.DataBind();
    }

}