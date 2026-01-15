using System;

public partial class admin_usercontrol_widgets_Wid1 : System.Web.UI.UserControl
{
    string sql = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((string)Session["LoginName"] == "" || Session["LoginName"] ==null)
        {
            Response.Redirect("~/Alumni/default.aspx");
        }
        else
        {        
            if (!IsPostBack)
            {
                sql = "Select top 10 convert(nvarchar,FromDate,106)as noticeDate,Title as Noticeheading,Description as noticemessage,";
                sql = sql + " CONVERT(nvarchar,ToDate,106)as ToDate,('| by: '+LoginName) LoginName from AlumniNews ";
                sql = sql + " where todate>=getdate() and BranchCode=" + Session["BranchCode"] + " order by isnull(id, 0) desc";
                GridView1.DataSource = BAL.objBal.GridFill(sql);
                GridView1.DataBind();
            }
        }
    }
}