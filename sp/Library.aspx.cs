using System;
using System.Collections.Generic;
using System.Data.SqlClient;

public partial class sp_Library : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        Campus camp = new Campus(); camp.LoadLoader(loader);
       loadGrid();
    }

    private void loadGrid()
    {
        List<SqlParameter> param = new List<SqlParameter>();

        param.Add(new SqlParameter("@Srno", Session["Srno"].ToString()));

        grdDocList.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_LibraryIssuedItemStudent", param);
        grdDocList.DataBind();
    }

}