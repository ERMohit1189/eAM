using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _7
{
    public partial class AdminLedgerReport : Page
    {
        public string Msg = "", Sql = "";
        public static int H01Id = 0;
        private DataTable _dt;
        private decimal _cr = 0;
        private decimal _dr = 0;
        public decimal FinalOpeningAmount = 0;
        public decimal FinalClosingAmount = 0;
        public decimal OpeningAmount = 0;
        string sql = "", _sql = "";
        Campus _oo = new Campus();
        private SqlConnection _con;
        public AdminLedgerReport()
        {
            _con = new SqlConnection();
            _con = _oo.dbGet_connection();
            _dt = new DataTable();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Campus camp = new Campus(); camp.LoadLoader(loader);
            BLL.BLLInstance.LoadHeader("Report", header);
            if (!IsPostBack)
            {
                try
                {
                    txtFromDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                    txtToDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                    GetSummary();
                    //GetLedgerReport();
                }
                catch (Exception)
                {
                }
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GetSummary();
            //GetLedgerReport();
        }

        /* Hari Om Thapa, Date : 15-03-2021 */
        private void GetSummary()
        {
            var sql = "GetSummaryTwoDates";
            var cmd = new SqlCommand()
            {
                Connection = BAL.objBal.dbGet_connection(),
                CommandText = sql,
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@dateFrom", txtFromDate.Text));
            cmd.Parameters.Add(new SqlParameter("@dateTo", txtToDate.Text));
            cmd.Parameters.Add(new SqlParameter("@branchCode", Convert.ToInt32(Session["BranchCode"].ToString())));

            var da = new SqlDataAdapter { SelectCommand = cmd };
            var dt = new DataTable();
            da.Fill(dt);
            gvLedger.DataSource = dt;
            gvLedger.DataBind();
            if (dt.Rows.Count > 1)
            {
                Panel2.Visible = true;
                divExport.Visible = true;
            }
            else {
                Panel2.Visible = false;
                divExport.Visible = false;
            }
            
        }

        private void GetLedgerReport()
        {
            string sql = "select format(convert(date, db.date), 'dd-MMM-yyyy') date, ";
            sql = sql + " isnull((select top(1) Balance from AccDayBook where branchcode=1 and PaymentMode='Cash' and convert(date, Date)<convert(date, db.Date) order by id desc),0)Opening, ";
            sql = sql + " sum(isnull(db.Income,0))Income, sum(isnull(db.Expens,0))Expens, ";
            sql = sql + " isnull((select top(1) Balance from AccDayBook where branchcode=1 and PaymentMode='Cash' and convert(date, Date)=convert(date, db.Date) order by id desc),0)Closing ";
            sql = sql + " from AccDayBook db where convert(date, date) between  convert(date, '" + txtFromDate.Text + "') and  convert(date, '" + txtToDate.Text + "') ";
            if (ddlPaymentMode.SelectedIndex!=0)
            {
                sql = sql + " and db.PaymentMode='" + ddlPaymentMode.SelectedValue + "' ";
            }
            sql = sql + " group by convert(date, db.date), db.PaymentMode order by convert(date, date) asc  ";
            var dt=_oo.Fetchdata(sql);
            if (dt.Rows.Count > 0)
            {
                heading.Text = "Ledger Report (From " + txtFromDate.Text + " to " + txtToDate.Text + ")";
                lblRegister.Text = " Print by : " + Session["LoginName"].ToString();

                Panel2.Visible = true;
                divExport.Visible = true;
                gvLedger.DataSource = dt;
                gvLedger.DataBind();
                double Income = 0, Expens = 0;
                double TotalIncomes = 0, TotalExpenss = 0;
                for (int i = 0; i < gvLedger.Rows.Count; i++)
                {
                    Label lblIncome = (Label)gvLedger.Rows[i].FindControl("lblIncome");
                    Label lblExpens = (Label)gvLedger.Rows[i].FindControl("lblExpens");
                    double.TryParse(lblIncome.Text, out Income);
                    double.TryParse(lblExpens.Text, out Expens);
                    TotalIncomes = TotalIncomes + Income;
                    TotalExpenss = TotalExpenss + Expens;
                }
                Label totalIncome = (Label)gvLedger.FooterRow.FindControl("totalIncome");
                Label totalExpens = (Label)gvLedger.FooterRow.FindControl("totalExpens");
                totalIncome.Text = TotalIncomes.ToString("0.00");
                totalExpens.Text = TotalExpenss.ToString("0.00");
            }
            else
            {
                Panel2.Visible = false;
                divExport.Visible = false;
            }
        }
       
        protected void ImageButton1_Click(object sender, EventArgs e)
        {
            BAL.objBal.ExportToWord(Response, "LedgerReport", divExport);
        }
        protected void ImageButton2_Click(object sender, EventArgs e)
        {
            BAL.objBal.ExportDivToExcelWithFormatting(Response, "LedgerReport", divExport);
        }
        protected void ImageButton3_Click(object sender, EventArgs e)
        {
            BAL.objBal.ExporttoPdf(Response, "LedgerReport", divExport);
        }
        protected void ImageButton4_Click(object sender, EventArgs e)
        {
            PrintHelper_New.ctrl = divExport;
            ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

    }
}