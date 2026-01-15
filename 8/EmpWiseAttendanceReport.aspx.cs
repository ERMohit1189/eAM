using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _8_EmpWiseAttendanceReport : System.Web.UI.Page
{
    Campus oo = new Campus();
    public SqlConnection con;
    public SqlCommand cmd = new SqlCommand();
    public SqlDataAdapter ad = new SqlDataAdapter();
    string sql = "";
    public void MakeConnection()
    {
        con = new SqlConnection();
        try
        {
            cmd = new SqlCommand();
            con = oo.dbGet_connection();
            con.Open();
        }
        catch { }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        if (!IsPostBack)
        {
            divExport.Visible = false;
            lblMsg.Visible = false;
            //GetYearList();
            GetMonthList();
            ddlMonth.SelectedValue=DateTime.Now.ToString("MMM");
            if (ddlMonth.SelectedValue != "") { GetList(); GetEmpName(); }
        }
    }

    //public void GetYearList()
    //{
    //    sql = "Select Distinct SessionId,SessionName from SessionMaster where BranchCode=" + Session["BranchCode"].ToString() + " Order By SessionName";
    //    oo.FillDropDown_withValue(sql, ddlYear, "SessionName", "SessionName");
    //    ddlYear.Items.Insert(0, new ListItem("Select", ""));
    //}
    public void GetMonthList()
    {
        ddlMonth.Items.Insert(0, new ListItem("Select", ""));
        ddlMonth.Items.Insert(1, new ListItem("April", "Apr"));
        ddlMonth.Items.Insert(2, new ListItem("May", "May"));
        ddlMonth.Items.Insert(3, new ListItem("June", "Jun"));
        ddlMonth.Items.Insert(4, new ListItem("July", "Jul"));
        ddlMonth.Items.Insert(5, new ListItem("August", "Aug"));
        ddlMonth.Items.Insert(6, new ListItem("September", "Sep"));
        ddlMonth.Items.Insert(7, new ListItem("October", "Oct"));
        ddlMonth.Items.Insert(8, new ListItem("November", "Nov"));
        ddlMonth.Items.Insert(9, new ListItem("December", "Dec"));
        ddlMonth.Items.Insert(10, new ListItem("January", "Jan"));
        ddlMonth.Items.Insert(11, new ListItem("February", "Feb"));
        ddlMonth.Items.Insert(12, new ListItem("March", "Mar"));
    }
    protected void lnkView_OnClick(object sender, EventArgs e)
    {
        GetList();
        GetEmpName();
    }
    public void GetList()
    {
        if (ddlMonth.SelectedValue == "") return;
        var param = new List<SqlParameter>
        {
            new SqlParameter("@Year", Session["SessionName"].ToString()),
            new SqlParameter("@Month", ddlMonth.SelectedValue),
            new SqlParameter("@EmpId", Session["LoginName"]),
            new SqlParameter("@BranchCode", Session["BranchCode"].ToString())
        };

        rptPunch.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_GetStaffAttendence", param);
        rptPunch.DataBind();
        divExport.Visible = true;
        lblMsg.Visible = true;
    }

    protected void rptPunch_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem) return;
        var lblSavedAttendence = (Label)e.Item.FindControl("lblSavedAttendence");
        var attText = lblSavedAttendence.Text.Trim();
        lblSavedAttendence.ForeColor = attText == "A" ? Color.FromArgb(228, 39, 9) : (attText == "LT" || attText == "LC" ? Color.FromArgb(255, 191, 0) : (attText == "HD" ? Color.FromArgb(128, 0, 128) : Color.FromArgb(12, 173, 39)));
       
    }

    public void GetEmpName()
    {
        sql = "Select EmpName From  dbo.GetSingleStaffRecords_UDF(" + Session["BranchCode"].ToString() + ", '" + Session["LoginName"] + "')";
        var data = oo.Fetchdata(sql);
        if (data.Rows.Count > 0)
        {
            var month = ddlMonth.SelectedValue.ToString();
            var year = Session["SessionName"].ToString();
            if ( month == "Jan" || month == "Feb" || month == "Mar") 
            { year = year.Substring(year.Length - 4); }
            else { year = year.Substring(0, 4); }
            lblMsg.Text = "Attendance Report of " + data.Rows[0]["EmpName"].ToString() + " for " + ddlMonth.SelectedItem + " " + year  + ".";
            lblMsg.ForeColor = Color.Red;
        }
    }
}