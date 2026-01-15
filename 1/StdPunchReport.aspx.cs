using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _1
{
    public partial class StdPunchReport : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file
            BLL.BLLInstance.LoadHeader("Report", header);
            if (!IsPostBack)
            {
                txtFromDate.Text = BAL.objBal.CurrentDate("yyyy MMM dd");           
                
                BLL.BLLInstance.loadClass(drpClass, Session["SessionName"].ToString());
                BLL.BLLInstance.loadBranch(drpBranch, Session["SessionName"].ToString(), drpClass.SelectedValue);
                BLL.BLLInstance.loadSection(drpSection, Session["SessionName"].ToString(), drpClass.SelectedValue);

                GetStdPunchdata(drpClass.SelectedValue, drpBranch.SelectedValue, drpSection.SelectedValue, 
                    txtFromDate.Text,drpAttendance.SelectedValue, Session["SessionName"].ToString());

            }
            if (grdStdPunchdata.Rows.Count > 0)
            {
                grdStdPunchdata.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            
        }

        public void GetStdPunchdata(string classId, string branchId, string sectionId, string fromDate,string attendance, string sessionName)
        {
            var param = new List<SqlParameter>
            {
                (new SqlParameter("@ClassId", classId)),
                (new SqlParameter("@BranchId", branchId)),
                (new SqlParameter("@SectionId", sectionId)),
                (new SqlParameter("@Date", fromDate)),
                (new SqlParameter("@Attendance", attendance)),
                (new SqlParameter("@SessionName", sessionName)),
                (new SqlParameter("@BranchCode", Session["BranchCode"]))
            };

            grdStdPunchdata.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_Get_StdDailyPunchReport", param);
            grdStdPunchdata.DataBind();

            lblTitle.Text = "Punch Report " + Convert.ToDateTime(fromDate).ToString("dd MMM yyyy");
        }

        protected void lnkSubmit_OnClick(object sender, EventArgs e)
        {
            GetStdPunchdata(drpClass.SelectedValue, drpBranch.SelectedValue, drpSection.SelectedValue,
            txtFromDate.Text, drpAttendance.SelectedValue, Session["SessionName"].ToString());
        }

        protected void lnkexcel_OnClick(object sender, EventArgs e)
        {
            BAL.objBal.ExportDivToExcel(Response, "Std_DailyPunchReport" + BAL.objBal.CurrentDate()+".xls", divExport);
        }

        protected void lnkword_OnClick(object sender, EventArgs e)
        {
            BAL.objBal.ExportTolandscapeWord(Response, "Std_DailyPunchReport" + BAL.objBal.CurrentDate(), divExport);
        }

        protected void lnkpdf_OnClick(object sender, EventArgs e)
        {
            BAL.objBal.ExporttolandscapePdf(Response, "Std_DailyPunchReport" + BAL.objBal.CurrentDate(), divExport);
        }

        protected void lnkprint_OnClick(object sender, EventArgs e)
        {
            PrintHelper_New.ctrl = divExport;
            ScriptManager.RegisterClientScriptBlock(Page, GetType(), "onclick", "var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}", true);
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void grdStdPunchdata_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
                e.Row.TableSection = TableRowSection.TableHeader;
        }

        protected void drpClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            BLL.BLLInstance.loadBranch(drpBranch, Session["SessionName"].ToString(), drpClass.SelectedValue);
            BLL.BLLInstance.loadSection(drpSection, Session["SessionName"].ToString(), drpClass.SelectedValue);

            GetStdPunchdata(drpClass.SelectedValue, drpBranch.SelectedValue, drpSection.SelectedValue,
    txtFromDate.Text, drpAttendance.SelectedValue, Session["SessionName"].ToString());
        }

        protected void drpBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetStdPunchdata(drpClass.SelectedValue, drpBranch.SelectedValue, drpSection.SelectedValue,
        txtFromDate.Text, drpAttendance.SelectedValue, Session["SessionName"].ToString());
        }

        protected void drpSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetStdPunchdata(drpClass.SelectedValue, drpBranch.SelectedValue, drpSection.SelectedValue,
    txtFromDate.Text, drpAttendance.SelectedValue, Session["SessionName"].ToString());
        }

        protected void drpAttendance_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetStdPunchdata(drpClass.SelectedValue, drpBranch.SelectedValue, drpSection.SelectedValue,
     txtFromDate.Text, drpAttendance.SelectedValue, Session["SessionName"].ToString());
        }
    }
}