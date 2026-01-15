using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;

public partial class sp_userControl_attendanceataGlance : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                StudentAttendanceataGlance();
            }
            catch
            {
            }
        }
        else
        {
            div11.Attributes.Add("class", "col-sm-12");
            StudentAttendanceataGlance();
        }
    }
    private void StudentAttendanceataGlance()
    {
        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@Srno", Session["Srno"].ToString()));
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));

        DataSet ds = new DataSet();
        ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("GetStudentAttendance_Proc", param);

        if (ds != null)
        {
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                lblAbsent.Text = dt.Rows[0][0].ToString();
                if (dt.Rows[0][8] != DBNull.Value)
                {
                    viewInTime.Visible = true;
                    viewOutTime.Visible = true;
                    viewLine.Visible = true;
                    lblInTime.Text = dt.Rows[0][8].ToString();
                }

                if (dt.Rows[0][9] != DBNull.Value)
                    lblOutTime.Text = dt.Rows[0][9].ToString();

                if (lblAbsent.Text.ToUpper() == "Present".ToUpper() || lblAbsent.Text.ToUpper() == "Late".ToUpper() || lblAbsent.Text.ToUpper() == "Late Commers".ToUpper())
                {
                    todayAtt.Attributes.Add("class", "col-sm-6  tab-green");
                }
                else if (lblAbsent.Text.ToUpper() == "Not Mark".ToUpper() || lblAbsent.Text.ToUpper() == "Absent".ToUpper() || lblAbsent.Text.ToUpper() == "Leave".ToUpper() || lblAbsent.Text.ToUpper() == "Medical".ToUpper() || lblAbsent.Text == "Medical leave".ToUpper())
                {
                    todayAtt.Attributes.Add("class", "col-sm-6  tab-red");
                }
                else
                {
                    todayAtt.Attributes.Add("class", "col-sm-6  tab-yellow");
                }

                lbllastAbsentDate.Text = dt.Rows[0][1].ToString();
                if (lbllastAbsentDate.Text == "NEVER")
                {
                    divlastAb.Attributes.Add("class", "col-sm-6  tab-green");
                }
                lblAttper.Text = dt.Rows[0][2].ToString() + "%";

                int attper = 0;
                int.TryParse(dt.Rows[0][2].ToString(), out attper);

                if (attper < 60)
                {
                    divoverAllAtt.Attributes.Add("class", "col-sm-12  tab-red");
                }
                else
                {
                    divoverAllAtt.Attributes.Add("class", "col-sm-12  tab-green");
                }

                lblTodaydate.Text = (dt.Rows[0][3].ToString() == "" ? "" : "(" + dt.Rows[0][3].ToString() + ")");
                if (dt.Rows[0][4].ToString() == "Unknown" || dt.Rows[0][4].ToString() == string.Empty)
                {
                    divreason.Visible = false;
                }
                else
                {
                    divreason.Visible = true;
                }
                lblReason.Text = (dt.Rows[0][4].ToString() == "" ? "" : "Reason:&nbsp;" + dt.Rows[0][4].ToString());
                lblStdTotalPresent.Text = dt.Rows[0]["Attendence"].ToString();
                lblStdTotaldays.Text = dt.Rows[0]["totaldays"].ToString();

                lblLastDayName.Text = (dt.Rows[0][5].ToString() == "" ? "" : "(" + dt.Rows[0][5].ToString() + ")");
                double[] yValues = { Convert.ToInt16(dt.Rows[0][2].ToString()), (100 - Convert.ToInt16(dt.Rows[0][2].ToString())) };
                string[] xValues = { "P", "A" };
                Chart1.Series["Default"].Points.DataBindXY(xValues, yValues);

                Chart1.Series["Default"].Points[0].Color = ColorTranslator.FromHtml("#1FAE66");
                Chart1.Series["Default"].Points[1].Color = ColorTranslator.FromHtml("#da4448");

                Chart1.Series["Default"].ChartType = SeriesChartType.Pie;

                Chart1.Series["Default"]["PieLabelStyle"] = "Disabled";

                Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;

                Chart1.Legends[0].Enabled = true;
            }
        }
    }
}