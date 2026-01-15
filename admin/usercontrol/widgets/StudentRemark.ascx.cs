using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;

// ReSharper disable once IdentifierTypo
namespace admin.usercontrol.widgets
{
    // ReSharper disable once IdentifierTypo
    public partial class AdminUsercontrolWidgetsWid2 : System.Web.UI.UserControl
    {
        DataSet _ds;
        DataTable _dt;

        // ReSharper disable once IdentifierTypo
        public AdminUsercontrolWidgetsWid2()
        {
            _ds = new DataSet();
            _dt = new DataTable();
        }

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
                }
                LoadTotalstudentRemark(Session["id"].ToString());
            }
        }

        // ReSharper disable once IdentifierTypo
        private void LoadTotalstudentRemark(string id)
        {
            var param = new List<SqlParameter>
            {
                new SqlParameter("@QueryFor", 'C'),
                new SqlParameter("@SrNo", id),
                new SqlParameter("@SessionName", Session["SessionName"].ToString()),
                new SqlParameter("@BranchCode", Session["BranchCode"].ToString())
            };
            _ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_StudentRemarkDetails", param);
            if (_ds.Tables.Count <= 0) return;
            _dt = _ds.Tables[0];
            if (_dt.Rows.Count > 0)
            {
                var v = _dt.Rows[0][0].ToString();
                lblStdTotalPresent.Text = v != "" ? _dt.Rows[0][0].ToString() : "0";
                var vv = _dt.Rows[0][1].ToString();
                lblStdTotalAbsent.Text = vv != "" ? _dt.Rows[0][1].ToString() : "0";

                double[] yValues = { Convert.ToInt16(lblStdTotalPresent.Text), Convert.ToInt16(lblStdTotalAbsent.Text) };
                string[] xValues = { "Positive", "Negative" };
                Chart1.Series["Default"].Points.DataBindXY(xValues, yValues);

                Chart1.Series["Default"].Points[0].Color = ColorTranslator.FromHtml("#1FAE66");
                Chart1.Series["Default"].Points[1].Color = ColorTranslator.FromHtml("#da4448");

                Chart1.Series["Default"].ChartType = SeriesChartType.Pie;

                Chart1.Series["Default"]["PieLabelStyle"] = "Disabled";

                Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;

                Chart1.Legends[0].Enabled = true;
            }
            else
            {
                Session["id"] = string.Empty;
                lblStdTotalPresent.Text = "0";
                lblStdTotalAbsent.Text = "0";
            }
        }

        public override void Dispose()
        {
            _ds.Dispose();
            _dt.Dispose();
        }
    }
}