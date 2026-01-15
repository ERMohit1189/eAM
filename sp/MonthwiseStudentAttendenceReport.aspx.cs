using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class sp_MonthwiseStudentAttendenceReport : System.Web.UI.Page
{
    protected void Page_InIt(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file
        if (!IsPostBack)
        {
            fillmonth();
            loadgrid();
            countTotalPresentAbsent();
        }
    }
    public void fillmonth()
    {
        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@QueryFor", "S"));
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));

        DataSet ds = new DataSet();
        ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("GetMonthNamebwTwoSession_Proc", param);
        DataTable dt = new DataTable();
        dt = ds.Tables[0];

        Accordion1.DataSource = dt.DefaultView;
        Accordion1.DataBind();

    }

    private void loadgrid()
    {
        for (int i = 0; i < Accordion1.Panes.Count; i++)
        {
            GridView grd = (GridView)Accordion1.Panes[i].FindControl("GridView1");
            Label lblMonth = (Label)Accordion1.Panes[i].FindControl("lblMonth");
            Label lblYear = (Label)Accordion1.Panes[i].FindControl("lblYear");

            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
            param.Add(new SqlParameter("@MonthName", lblMonth.Text.Trim()));
            param.Add(new SqlParameter("@Year", lblYear.Text.Trim()));
            param.Add(new SqlParameter("@Srno", Session["Srno"].ToString()));

            DataSet ds = new DataSet();
            ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("GetStudentMonthlyAtt_Proc", param);

            grd.DataSource = ds;
            grd.DataBind();
        }

        Accordion1.SelectedIndex = Accordion1.Panes.Count - 1;
    }

    private void countTotalPresentAbsent()
    {
        for (int i = 0; i < Accordion1.Panes.Count; i++)
        {
            GridView grd = (GridView)Accordion1.Panes[i].FindControl("GridView1");
            int present = 0;
            int absent = 0;
            for (int j = 0; j < grd.Rows.Count; j++)
            {
                Label lblAttendance = (Label)grd.Rows[j].FindControl("lblAttendance");
                if (lblAttendance.Text == "Absent")
                {
                    absent += 1;
                }
                else if (lblAttendance.Text == "X")
                {
                    absent += 0;
                }
                else
                {
                    present += 1;
                }
                Label lblTotalPrs = (Label)grd.FooterRow.FindControl("lblTotalPrs");
                Label lblTotalAb = (Label)grd.FooterRow.FindControl("lblTotalAb");

                lblTotalPrs.Text = present.ToString();
                lblTotalAb.Text = absent.ToString();
            }
        }
    }
}