using c4SmsNew;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;
using System.Web.UI;
namespace _1
{
    public partial class EnquiryVsAdmission : System.Web.UI.Page
    {
        private SqlConnection _con;
        private readonly Campus _oo;
        private string _sql, _ss = string.Empty;
        public EnquiryVsAdmission()
        {
            _con = new SqlConnection();
            _oo = new Campus();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
            {
                Response.Redirect("default.aspx");
            }
            _con = _oo.dbGet_connection();
            Campus camp = new Campus(); camp.LoadLoader(loader);
            BLL.BLLInstance.LoadHeader("Report", header);
            if (!IsPostBack)
            {
                txtFromdate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                txtTodate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                if (Request.QueryString.AllKeys.Length > 0 && Request.QueryString["Type"] != null)
                {
                    if (Request.QueryString["Type"].ToString() == "1")
                    {
                        loadData();
                    }
                }
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {

        }
        protected void btnView_Click(object sender, EventArgs e)
        {
            loadData();
        }
        protected void loadData()
        {
            string DateFrom = "", DateTo = "";
            try
            {
                DateFrom = DateTime.Parse(txtFromdate.Text.Trim()).ToString("dd-MMM-yyyy");
            }
            catch (Exception)
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgView, "Invalid From date!", "A");
                divDetails.Visible = false;
                return;
            }
            try
            {
                DateTo = DateTime.Parse(txtTodate.Text.Trim()).ToString("dd-MMM-yyyy");
            }
            catch (Exception)
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgView, "Invalid To date!", "A");
                divDetails.Visible = false;
                return;
            }
            var days = 0;
            try
            {
                string ss = "SELECT DATEDIFF(dd,'" + DateFrom + "', '" + DateTo + "') AS days";
                days = int.Parse(_oo.ReturnTag(ss, "days"));
            }
            catch (Exception)
            {
            }
            if (days<0)
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgView, "Invalid from or to date!", "A");
                divDetails.Visible = false;
                return;
            }
            if (days>370)
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgView, "Please select date range between 370 days!", "A");
                divDetails.Visible = false;
                Grd.DataSource = null;
                Grd.DataBind();
                return;
            }
            else
            {
                string _sql2 = "declare @table as table(AllEnquery int, PendingEnquery int, FormIssuedEnquery int, RegisteredEnquery int) ";
                _sql2 = _sql2 + " insert into @table select ";
                _sql2 = _sql2 + " (select count(*)  from AdmissionEnquiry where BranchCode=" + Session["BranchCode"].ToString() + " and convert(date, date) between '" + DateFrom + "' and '" + DateTo + "'), ";
                _sql2 = _sql2 + " (select count(*)  from AdmissionEnquiry where BranchCode=" + Session["BranchCode"].ToString() + " and convert(date, date) between '" + DateFrom + "' and '" + DateTo + "' and Status='Pending'), ";
                _sql2 = _sql2 + " (select count(*)  from AdmissionEnquiry where BranchCode=" + Session["BranchCode"].ToString() + " and convert(date, date) between '" + DateFrom + "' and '" + DateTo + "' and Status='Form Issued'), ";
                _sql2 = _sql2 + " (select count(*)  from AdmissionEnquiry where BranchCode=" + Session["BranchCode"].ToString() + " and convert(date, date) between '" + DateFrom + "' and '" + DateTo + "' and Status='Registered') ";
                _sql2 = _sql2 + " select * from @table ";
                divDetails.Visible = true;
                Grd.DataSource = _oo.Fetchdata(_sql2);
                Grd.DataBind();
                if (Grd.Rows.Count>0)
                {
                    lblRegister.Text = "(" + DateFrom + " to " + DateTo + ")";
                    divDetails.Visible = true;
                    var value1 =(Label)Grd.Rows[0].FindControl("Enquiry");
                    var value2 =(Label)Grd.Rows[0].FindControl("Pending");
                    var value3 =(Label)Grd.Rows[0].FindControl("FormIssued");
                    var value4 =(Label)Grd.Rows[0].FindControl("Registered");
                    string Values = value1.Text + "," + value2.Text + "," + value3.Text + "," + value4.Text;
                    loadChart(Values);
                }
                else
                {
                    divDetails.Visible = false;
                }
            }
        }
        protected void loadChart(string Values)
        {
            double[] yValues = Array.ConvertAll(Values.Split(','), double.Parse);
            string[] xValues = { "E", "P", "F", "R"};
            Chart1.Series["Default"].Points.DataBindXY(xValues, yValues);
            //Chart1.Titles["Title1"].Text = ("Enquiry Vs. Admission Ratio").ToUpper();
            Chart1.Series["Default"].ChartType = SeriesChartType.Pie;
            Chart1.Series["Default"]["PieLabelStyle"] = "Disabled";
            Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;
            Chart1.Legends[0].Enabled = false;
            Chart1.Series["Default"].Points[0].Color = ColorTranslator.FromHtml("#020096");
            Chart1.Series["Default"].Points[1].Color = ColorTranslator.FromHtml("#F89C2C");
            Chart1.Series["Default"].Points[2].Color = ColorTranslator.FromHtml("#DA4448");
            Chart1.Series["Default"].Points[3].Color = ColorTranslator.FromHtml("#1FAE66");

            Chart1.Series["Default"].ChartType = SeriesChartType.Pie;

            Chart1.Series["Default"]["PieLabelStyle"] = "Disabled";

            Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;

            Chart1.Legends[0].Enabled = true;
            lblEnquiry.Text = yValues[0].ToString("0");
            lblPending.Text = yValues[1].ToString("0");
            lblFormIssued.Text = yValues[2].ToString("0");
            lblRegistered.Text = yValues[3].ToString("0");
        }
        protected void ImageButton1_Click(object sender, EventArgs e)
        {
            _oo.ExportTolandscapeWord(Response, "TuitionFeeReport", gdv1);
        }
        protected void ImageButton2_Click(object sender, EventArgs e)
        {
            _oo.ExportDivToExcelWithFormatting(Response, "TuitionFeeReport.xls", gdv1, Server.MapPath("~/Admin/css/style.css"));
        }
        protected void ImageButton3_Click(object sender, EventArgs e)
        {
            _oo.ExporttolandscapePdf(Response, "TuitionFeeReport", abc);
        }
        protected void ImageButton4_Click(object sender, EventArgs e)
        {
            PrintHelper_New.ctrl = abc;
            ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
        }
        public override void Dispose()
        {
            _con.Dispose();
            _oo.Dispose();
        }
    }
}