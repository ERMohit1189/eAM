using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;
using System.Data.SqlClient;

public partial class admin_usercontrol_widgets_Wid2 : System.Web.UI.UserControl
{
    DataSet ds = new DataSet();
    DataTable dt = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        else
        {
            if (!IsPostBack)
            {
                LoadTotalstudentAttendence();
            }
        }
    }

    private void LoadTotalstudentAttendence()
    {
        if (Session["Logintype"].ToString() == "Staff")
        {
            var param = new List<SqlParameter>
            {
                new SqlParameter("@SessionName", Session["SessionName"].ToString()),
                new SqlParameter("@BranchCode", Session["BranchCode"].ToString()),
                new SqlParameter("@Empcode", Session["LoginName"].ToString())
            };
            ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_StudentAttendenceForStaffPortal", param);
        }
        else
        {
            var param = new List<SqlParameter>
            {
                new SqlParameter("@SessionName", Session["SessionName"].ToString()),
                new SqlParameter("@BranchCode", Session["BranchCode"].ToString())
            };
            ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("StudentAttendenceDetailsProc", param);
        }
        
        if (ds.Tables.Count <= 0) return;
        dt = ds.Tables[0];
        if (dt.Rows.Count <= 0) return;
        lblStdTotalAttendence.Text = dt.Rows[0][0].ToString();
        lblStdTotalPresent.Text = dt.Rows[0][1].ToString();
        lblStdTotalAbsent.Text = dt.Rows[0][2].ToString();
        lblStdTotalLate.Text = dt.Rows[0][3].ToString();
        lblNotFilled.Text = dt.Rows[0][4].ToString();
        lblStdOther.Text = dt.Rows[0][5].ToString();

        double[] yValues = { Convert.ToInt16(lblStdTotalPresent.Text), Convert.ToInt16(lblStdTotalLate.Text), Convert.ToInt16(lblStdTotalAbsent.Text), Convert.ToInt16(lblNotFilled.Text), Convert.ToInt16(lblStdOther.Text) };
        string[] xValues = { "P", "LT", "A", "NM", "O" };
        Chart1.Series["Default"].Points.DataBindXY(xValues, yValues);

        Chart1.Series["Default"].Points[0].Color = ColorTranslator.FromHtml("#1FAE66");
        Chart1.Series["Default"].Points[1].Color = ColorTranslator.FromHtml("#F89C2C");
        Chart1.Series["Default"].Points[2].Color = ColorTranslator.FromHtml("#E91E63");
        Chart1.Series["Default"].Points[3].Color = ColorTranslator.FromHtml("#DA4448");
        Chart1.Series["Default"].Points[4].Color = ColorTranslator.FromHtml("#23709E");
                

        Chart1.Series["Default"].ChartType = SeriesChartType.Pie;

        Chart1.Series["Default"]["PieLabelStyle"] = "Disabled";

        Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;

        Chart1.Legends[0].Enabled = true;
    }
}