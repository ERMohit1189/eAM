using System;

public partial class admin_usercontrol_widgets_Wid1 : System.Web.UI.UserControl
{
    string sql = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Logintype"]==null)
        {
            Response.Redirect("default.aspx");
        }
        else
        {        
            if (!IsPostBack)
            {
                if (Session["Logintype"].ToString().Trim() == "Guardian")
                {
                    sql = "Select top 5 convert(nvarchar,FromDate,106)as noticeDate,Title as Noticeheading,Description as noticemessage,";
                    sql = sql + " CONVERT(nvarchar,ToDate,106)as ToDate from NewsDescription ";
                    sql = sql + " where todate+1>=getdate()";
                    sql = sql + " and Guardian='Show' and sessionName='" + Session["SessionName"] + "' and BranchCode = " + Session["BranchCode"] + " order by isnull(newsid, 0) desc";
                    //sql = sql + " union ";
                    //sql = sql + " Select top 5 convert(nvarchar,Fromdate,106)as NoticeDate,Title as NoticeHeading,Description as NoticeMessage,";
                    //sql = sql + " CONVERT(nvarchar,Todate,106)as ToDate from Events ";
                    //sql = sql + " where todate>=getdate() and BranchCode = " + Session["BranchCode"] + " order by NoticeDate Asc ";

                    GridView1.DataSource = BAL.objBal.GridFill(sql);
                    GridView1.DataBind();
                }
                if (Session["Logintype"].ToString().Trim() == "Student")
                {
                    sql = "Select top 5 convert(nvarchar,FromDate,106)as noticeDate,Title as Noticeheading,Description as noticemessage,";
                    sql = sql + " CONVERT(nvarchar,ToDate,106)as ToDate from NewsDescription ";
                    sql = sql + " where todate+1>=getdate()";
                    sql = sql + " and Student='Show' and sessionName='" + Session["SessionName"] + "' and BranchCode = " + Session["BranchCode"] + " order by isnull(newsid, 0) desc";
                    //sql = sql + " union ";
                    //sql = sql + " Select top 5 convert(nvarchar,Fromdate,106)as NoticeDate,Title as NoticeHeading,Description as NoticeMessage,";
                    //sql = sql + " CONVERT(nvarchar,Todate,106)as ToDate from Events ";
                    //sql = sql + " where todate>=getdate() and BranchCode = " + Session["BranchCode"] + " order by NoticeDate Asc ";

                    GridView1.DataSource = BAL.objBal.GridFill(sql);
                    GridView1.DataBind();
                }
            }
        }
    }
}