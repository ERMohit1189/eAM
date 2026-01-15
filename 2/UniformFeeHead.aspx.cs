using System;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class UniformFeeHead : System.Web.UI.Page
{
    Campus oo = new Campus();
    SqlConnection con = new SqlConnection();
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file
       
        if (!IsPostBack)
        {
            loadclass();
        }
    }

    public void loadclass()
    {
        sql = "Select ClassName,Id from ClassMaster Where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " order by id asc";
        oo.FillDropDown_withValue(sql, drpFromClass, "ClassName", "Id");
        oo.FillDropDown_withValue(sql, drpToClass, "ClassName", "Id");
    }
}