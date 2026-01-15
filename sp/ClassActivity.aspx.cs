using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public partial class sp_ClassActivity : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            string albumId = Request.QueryString["CWAAID"].ToString();

            loadGrid("-1", albumId);
        }
    }

    private DataSet select(string id, string albumId)
    {
        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@QueryFor", "S"));
        param.Add(new SqlParameter("@Id", id));
        param.Add(new SqlParameter("@CWAAID", albumId));
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));

        DataSet ds = new DataSet();

        rpt1.DataSource = ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("ClassWiseActivity_Proc", param);
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
    private DataSet loadGrid(string id, string albumId)
    {
        return select(id, albumId);
    }

}