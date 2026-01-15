using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sp
{
    public partial class AdminUploadHomeWorkText : Page
    {
        private DataSet _ds;
        private string _sql = String.Empty;
        public AdminUploadHomeWorkText()
        {
            _ds = new DataSet();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Campus camp = new Campus(); camp.LoadLoader(loader);
            BLL.BLLInstance.LoadHeader("Report", header);
            if (!IsPostBack)
            {
                _sql = "Select FromDate,toDate from SessionMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                DateTime date1 = Convert.ToDateTime(BAL.objBal.ReturnTag(_sql, "FromDate"));
                string fromdate = date1.ToString("dd-MMM-yyyy");
                txtFromDate.Text = fromdate;

                DateTime date2 = Convert.ToDateTime(BAL.objBal.ReturnTag(_sql, "toDate"));
                string todate = date2.ToString("dd-MMM-yyyy");
                txtToDate.Text = todate;
                LoadGrid("-1");
            }
        }
        private void LoadGrid(string id)
        {
            List<SqlParameter> param = new List<SqlParameter>();

            param.Add(new SqlParameter("@Id", id));
            param.Add(new SqlParameter("@Srno", Session["Srno"].ToString()));
            param.Add(new SqlParameter("@ClassId", ""));
            param.Add(new SqlParameter("@BranchId", ""));
            param.Add(new SqlParameter("@SectionId", ""));
            param.Add(new SqlParameter("@FromDate", txtFromDate.Text.Trim()));
            param.Add(new SqlParameter("@ToDate", txtToDate.Text.Trim()));
            param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
            param.Add(new SqlParameter("@QueryFor", "G"));

            _ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_Student_HomeWork", param);
            if (_ds.Tables[0].Rows.Count > 0)
            {
                grdDocList.DataSource = _ds;
                grdDocList.DataBind();
            }
            else
            {
                grdDocList.DataSource = null;
                grdDocList.DataBind();
            }
        }

        protected void lnkSubmit_OnClick(object sender, EventArgs e)
        {
            LoadGrid("-1");
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        protected void ImageButton1_Click(object sender, EventArgs e)
        {
            BAL.objBal.ExportTolandscapeWord(Response, "PlannerReport", mainDiv);
        }
        protected void ImageButton2_Click(object sender, EventArgs e)
        {
            BAL.objBal.ExportDivToExcelWithFormatting(Response, "PlannerReport", mainDiv);
        }
        protected void ImageButton3_Click(object sender, EventArgs e)
        {
            BAL.objBal.ExporttolandscapePdf(Response, "PlannerReport", mainDiv);
        }
        protected void ImageButton4_Click(object sender, EventArgs e)
        {
            PrintHelper_New.ctrl = mainDiv;
            if (grdDocList.Rows.Count > 0)
            {
                grdDocList.HeaderRow.TableSection = TableRowSection.TableHeader;
            }

            ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
        }
        public override void Dispose()
        {
            _ds.Dispose();
        }
    }
}