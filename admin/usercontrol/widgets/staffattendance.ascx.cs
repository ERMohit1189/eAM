using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;

public partial class admin_usercontrol_widgets_Wid3 : System.Web.UI.UserControl
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
                loadTotalemployeeAttendence();
            }
        }
    }

    private void loadTotalemployeeAttendence()
    {
        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@Msg", ""));
        //param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));

        ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("EmployeeAttendenceDetailsProc2", param);
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];

                if (dt.Rows.Count > 0)
                {
                    lblTotalAttendence.Text = dt.Rows[0]["totalAttendence"].ToString();
                    lblEmpTotalPresent.Text = dt.Rows[0]["totalPresent"].ToString();
                    lblEmpTotalLate.Text = dt.Rows[0]["totalLate"].ToString();
                    lblEmpTotalAbsent.Text = dt.Rows[0]["totalAbsent"].ToString();
                    lblEmpHD.Text = dt.Rows[0]["totalHD"].ToString();
                    lblEmpSL.Text = dt.Rows[0]["TotalSL"].ToString();
                    lblNotFilled.Text = dt.Rows[0]["totalNotFilled"].ToString();

                    double[] yValues = { Convert.ToInt16(lblEmpTotalPresent.Text), Convert.ToInt16(lblEmpTotalLate.Text), Convert.ToInt16(lblEmpTotalAbsent.Text), Convert.ToInt16(lblNotFilled.Text), Convert.ToInt16(lblEmpHD.Text), Convert.ToInt16(lblEmpSL.Text) };
                    string[] xValues = { "P", "LT", "A", "NM", "HD", "SL" };
                    Chart2.Series["Default"].Points.DataBindXY(xValues, yValues);

                    Chart2.Series["Default"].Points[0].Color = ColorTranslator.FromHtml("#1FAE66");
                    Chart2.Series["Default"].Points[1].Color = ColorTranslator.FromHtml("#F89C2C");
                    Chart2.Series["Default"].Points[2].Color = ColorTranslator.FromHtml("#E91E63");
                    Chart2.Series["Default"].Points[3].Color = ColorTranslator.FromHtml("#2196f3");
                    Chart2.Series["Default"].Points[4].Color = ColorTranslator.FromHtml("#673ab7");
                    Chart2.Series["Default"].Points[5].Color = ColorTranslator.FromHtml("#000");

                    Chart2.Series["Default"].ChartType = SeriesChartType.Pie;

                    Chart2.Series["Default"]["PieLabelStyle"] = "Disabled";

                    Chart2.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;

                    Chart2.Legends[0].Enabled = true;
                }

            }
        }
        
    }

}