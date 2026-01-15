using System;

public partial class News : System.Web.UI.Page
{
    string sql = "";

    protected void Page_PreInIt(object sender, EventArgs e)
    {
        if (Session["Logintype"] == null) { Response.Redirect("~/default.aspx"); }
        if (Session["Logintype"].ToString() == "Admin")
        {
            this.MasterPageFile = "~/Master/admin_root-manager.master";
        }
        else if (Session["Logintype"].ToString() == "Staff")
        {
            this.MasterPageFile = "~/Staff/staff_root-manager.master";
        }
        else if (Session["Logintype"].ToString() == "Guardian")
        {
            this.MasterPageFile = "~/Sp/sp_root-manager.master";
        }
        else if (Session["Logintype"].ToString() == "Student")
        {
            this.MasterPageFile = "~/13/stuRootManager.master";
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Campus camp = new Campus(); camp.LoadLoader(loader); 

        if (!IsPostBack)
        {
            if (Session["Logintype"].ToString().Trim() == "Admin")
            {
                sql = "select * from (Select convert(nvarchar,FromDate,106)as noticeDate,Title as Noticeheading,Description as noticemessage,";
                sql +=  " CONVERT(nvarchar,ToDate,106)as ToDate,('| by: '+LoginName) LoginName, convert(date,FromDate) orderdate from NewsDescription ";
                sql +=  " where BranchCode=" + Session["BranchCode"].ToString() + "";
                sql +=  " and Admin='Show'";
                sql +=  " union ";
                sql +=  " Select convert(nvarchar,Fromdate,106)as NoticeDate,Title as NoticeHeading,Description as NoticeMessage,";
                sql +=  " CONVERT(nvarchar,Todate,106)as ToDate,('| by: '+LoginName) LoginName, convert(date,Fromdate) orderdate from Events ";
                sql +=  " where BranchCode=" + Session["BranchCode"].ToString() + ")T1 order by T1.orderdate desc ";

                GridView1.DataSource = BAL.objBal.GridFill(sql);
                GridView1.DataBind();
            }

            else if (Session["Logintype"].ToString().Trim() == "Staff")
            {
                sql = "select * from (Select convert(nvarchar,FromDate,106)as noticeDate,Title as Noticeheading,Description as noticemessage,";
                sql +=  " CONVERT(nvarchar,ToDate,106)as ToDate,'' LoginName, convert(date,FromDate) orderdate from NewsDescription ";
                sql +=  " where BranchCode=" + Session["BranchCode"].ToString() + "";
                sql +=  " and Staff='Show'";
                sql +=  " union ";
                sql +=  " Select convert(nvarchar,Fromdate,106)as NoticeDate,Title as NoticeHeading,Description as NoticeMessage,";
                sql +=  " CONVERT(nvarchar,Todate,106)as ToDate,'' LoginName, convert(date,Fromdate) orderdate from Events ";
                sql +=  " where BranchCode=" + Session["BranchCode"].ToString() + ")T1 order by T1.orderdate desc ";

                GridView1.DataSource = BAL.objBal.GridFill(sql);
                GridView1.DataBind();
            }

            else if (Session["Logintype"].ToString().Trim() == "Guardian")
            {
                sql = "select * from (Select convert(nvarchar,FromDate,106)as noticeDate,Title as Noticeheading,Description as noticemessage,";
                sql +=  " CONVERT(nvarchar,ToDate,106)as ToDate,'' LoginName, convert(date,FromDate) orderdate from NewsDescription ";
                sql +=  " where BranchCode=" + Session["BranchCode"].ToString() + "";
                sql +=  " and Guardian='Show'";
                sql +=  " union ";
                sql +=  " Select convert(nvarchar,Fromdate,106)as NoticeDate,Title as NoticeHeading,Description as NoticeMessage,";
                sql +=  " CONVERT(nvarchar,Todate,106)as ToDate,'' LoginName, convert(date,Fromdate) orderdate from Events ";
                sql +=  " where BranchCode=" + Session["BranchCode"].ToString() + ")T1 order by T1.orderdate desc ";

                GridView1.DataSource = BAL.objBal.GridFill(sql);
                GridView1.DataBind();
            }
            else if (Session["Logintype"].ToString().Trim() == "Student")
            {
                sql = "select * from (Select convert(nvarchar,FromDate,106)as noticeDate,Title as Noticeheading,Description as noticemessage,";
                sql +=  " CONVERT(nvarchar,ToDate,106)as ToDate,'' LoginName, convert(date,FromDate) orderdate from NewsDescription ";
                sql +=  " where BranchCode=" + Session["BranchCode"].ToString() + "";
                sql +=  " and Student='Show'";
                sql +=  " union ";
                sql +=  " Select convert(nvarchar,Fromdate,106)as NoticeDate,Title as NoticeHeading,Description as NoticeMessage,";
                sql +=  " CONVERT(nvarchar,Todate,106)as ToDate,'' LoginName, convert(date,Fromdate) orderdate from Events ";
                sql +=  " where BranchCode=" + Session["BranchCode"].ToString() + ")T1 order by T1.orderdate desc ";

                GridView1.DataSource = BAL.objBal.GridFill(sql);
                GridView1.DataBind();
            }

        }
    }
}
