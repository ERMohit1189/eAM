using System;
using System.Data.SqlClient;
using System.Web.UI;

namespace _1
{
    public partial class AdminApplicationForWithdrawal : Page
    {
        private SqlConnection con;
        private readonly Campus oo;
        private string sql;
        string id = "";
        public AdminApplicationForWithdrawal()
        {
            con = new SqlConnection();
            oo = new Campus();
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            con = oo.dbGet_connection();
            BLL.BLLInstance.LoadHeader("Certificate", header1);
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            PrintHelper_New.ctrl = abc;
            ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
        }
        protected void lnkWord_Click(object sender, EventArgs e)
        {
            BAL.objBal.ExportToWord(Response, "ApplicationforWithdrawal", abc);
        }
        protected void lnkPdf_Click(object sender, EventArgs e)
        {
            BAL.objBal.ExporttoPdf(Response, "ApplicationforWithdrawal", abc);
        }
        protected void lnkExcel_Click(object sender, EventArgs e)
        {
            BAL.objBal.ExportDivToExcel(Response, "ApplicationforWithdrawal.xls", abc);
        }
    }
}