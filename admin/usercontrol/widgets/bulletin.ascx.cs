using System;

public partial class admin_usercontrol_widgets_Wid1 : System.Web.UI.UserControl
{
    string sql = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((string)Session["Logintype"] == "" || Session["Logintype"]==null)
        {
            Response.Redirect("default.aspx");
        }
        else
        {        
            if (!IsPostBack)
            {
                if (Session["Logintype"].ToString().Trim() == "Admin")
                {
                    sql = "Select top 5 convert(nvarchar,FromDate,106)as noticeDate,Title as Noticeheading,Description as noticemessage,";
                    sql = sql + " CONVERT(nvarchar,ToDate,106)as ToDate,('| by: '+LoginName) LoginName from NewsDescription ";
                    sql = sql + " where todate+1>=getdate()  ";
                    sql = sql + " and sessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and Admin='Show' order by isnull(newsid, 0) desc";
                    //sql = sql + " union ";
                    //sql = sql + " Select top 5 convert(nvarchar,Fromdate,106)as NoticeDate,Title as NoticeHeading,Description as NoticeMessage,";
                    //sql = sql + " CONVERT(nvarchar,Todate,106)as ToDate,('| by: '+LoginName) LoginName from Events ";
                    //sql = sql + " where todate>=getdate() and BranchCode=" + Session["BranchCode"] + "  and sessionName='" + Session["SessionName"] + "' order by NoticeDate dec ";

                    GridView1.DataSource = BAL.objBal.GridFill(sql);
                    GridView1.DataBind();
                }
                else if (Session["Logintype"].ToString().Trim() == "Staff")
                {
                    sql = "Select top 5 convert(nvarchar,FromDate,106)as noticeDate,Title as Noticeheading,Description as noticemessage,";
                    sql = sql + " CONVERT(nvarchar,ToDate,106)as ToDate,'' LoginName from NewsDescription ";
                    sql = sql + " where todate+1>=getdate()  ";
                    sql = sql + " and sessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and Staff='Show' order by isnull(newsid, 0) desc";
                    //sql = sql + " union ";
                    //sql = sql + " Select top 5 convert(nvarchar,Fromdate,106)as NoticeDate,Title as NoticeHeading,Description as NoticeMessage,";
                    //sql = sql + " CONVERT(nvarchar,Todate,106)as ToDate,'' LoginName from Events ";
                    //sql = sql + " where todate>=getdate() and BranchCode=" + Session["BranchCode"] + "  and sessionName='" + Session["SessionName"] + "' order by NoticeDate Asc ";


                    GridView1.DataSource = BAL.objBal.GridFill(sql);
                    GridView1.DataBind();
                }
                else if (Session["Logintype"].ToString().Trim() == "Guardian")
                {
                    sql = "Select top 5 convert(nvarchar,FromDate,106)as noticeDate,Title as Noticeheading,Description as noticemessage,";
                    sql = sql + " CONVERT(nvarchar,ToDate,106)as ToDate,'' LoginName from NewsDescription ";
                    sql = sql + " where todate+1>=getdate() and BranchCode=" + Session["BranchCode"] + " and sessionName='" + Session["SessionName"] + "'";
                    sql = sql + " and Guardian='Show' order by isnull(newsid, 0) desc";
                    //sql = sql + " union ";
                    //sql = sql + " Select top 5 convert(nvarchar,Fromdate,106)as NoticeDate,Title as NoticeHeading,Description as NoticeMessage,";
                    //sql = sql + " CONVERT(nvarchar,Todate,106)as ToDate,'' LoginName from Events ";
                    //sql = sql + " where todate>=getdate() and BranchCode=" + Session["BranchCode"] + " and sessionName='" + Session["SessionName"] + "' order by NoticeDate Asc ";

                    GridView1.DataSource = BAL.objBal.GridFill(sql);
                    GridView1.DataBind();
                }
            }
        }
    }
}