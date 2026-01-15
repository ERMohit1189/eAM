using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _1
{
    public partial class DownloadedPunchReport : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file
            BLL.BLLInstance.LoadHeader("Report", header);
            if (!IsPostBack)
            {
                txtFromDate.Text = BAL.objBal.CurrentDate("yyyy MMM dd");
                txtToDate.Text = txtFromDate.Text;             
                GetDownloadedPunch();              
            }
            if (grdDownloadedPunch.Rows.Count > 0)
            {
                grdDownloadedPunch.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            lblTitle.Text = "Punch Report " + BAL.objBal.CurrentDate();
        }

        public void GetDownloadedPunch()
        {
            var fromdate = txtFromDate.Text;
            //var toate = txtToDate.Text;

            var param = new List<SqlParameter>
            {
                (new SqlParameter("@FromDate", fromdate)),
                (new SqlParameter("@ToDate", fromdate)),
                (new SqlParameter("@BranchCode", Session["BranchCode"]))
            };

            grdDownloadedPunch.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_GetDownloadedPunch", param);
            grdDownloadedPunch.DataBind();
            lblTitle.Text = "Punch Report " + fromdate;
        }

        protected void lnkSubmit_OnClick(object sender, EventArgs e)
        {
            GetDownloadedPunch();
        }

        protected void lnkexcel_OnClick(object sender, EventArgs e)
        {
            BAL.objBal.ExportDivToExcel(Response, "PunchReport" + BAL.objBal.CurrentDate()+".xls", divExport);
        }

        protected void lnkword_OnClick(object sender, EventArgs e)
        {
            BAL.objBal.ExportTolandscapeWord(Response, "PunchReport" + BAL.objBal.CurrentDate(), divExport);
        }

        protected void lnkpdf_OnClick(object sender, EventArgs e)
        {
            BAL.objBal.ExporttolandscapePdf(Response, "PunchReport" + BAL.objBal.CurrentDate(), divExport);
        }

        protected void lnkprint_OnClick(object sender, EventArgs e)
        {
            PrintHelper_New.ctrl = divExport;
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "onclick", "var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}", true);
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void grdDownloadedPunch_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
                e.Row.TableSection = TableRowSection.TableHeader;
        }
    }
}