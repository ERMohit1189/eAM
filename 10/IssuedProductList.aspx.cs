using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _10
{
    public partial class IssuedProductList : System.Web.UI.Page
    {
        private SqlConnection _con;
        private readonly Campus _oo;
        private DataSet _ds;
        private string _sql = String.Empty;
        public IssuedProductList()
        {
            _con = new SqlConnection();
            _oo = new Campus();
            _ds = new DataSet();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["LoginName"] == "")
            {
                Response.Redirect("default.aspx");
            }
            _con = _oo.dbGet_connection();
            Campus camp = new Campus(); camp.LoadLoader(loader);
            BLL.BLLInstance.LoadHeader("Report", header);
            if (!IsPostBack)
            {
                GetArticleEntryList();
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {

        }
        
        protected void lnkView_Click(object sender, EventArgs e)
        {
            GetArticleEntryList();
        }
        private void GetArticleEntryList()
        {
            var empId = Request.Form[hfEmployeeId.UniqueID];
            if (empId == string.Empty || txtStaff.Text.Trim()=="")
            {
                empId = "";
                txtStaff.Text = "";
            }
            abc.Visible = true;
            divExport.Visible = true;
            try
            {
                string ss = "Select ae.*, um.Name ItemName, emp.Name, format(ae.IssueDate, 'dd-MMM-yyyy') IssueDates, um.ProductType, ";
                ss = ss + " format(ae.RecordedDate, 'dd-MMM-yyyy hh:mm:ss tt') RecordedDates, isnull(ae.ReturnQty,0) ReturnQtys from invIssueProduct ae ";
                ss = ss + " left join InvArticleEntry um on um.ID = ae.itemid and um.BranchCode = ae.BranchCode ";
                ss = ss + " left join GetAllStaffRecords_UDF("+ Session["BranchCode"] + ") emp on emp.Ecode = ae.EmpCode and emp.BranchCode = ae.BranchCode ";
                ss = ss + " where ae.BranchCode = " + Session["BranchCode"] + "";
                if (ddlProductType.SelectedIndex!=0)
                {
                    ss = ss + " and um.ProductType = '" + ddlProductType.SelectedValue + "' ";
                }
                if (empId != "" && empId != null)
                {
                    ss = ss + " and ae.EmpCode = '" + empId + "' ";
                }
                ss = ss + " order by ae.ID asc";

                var dt = _oo.Fetchdata(ss);
                if (dt.Rows.Count > 0)
                {
                    divlistshow.Visible = true;
                    Repeater1.DataSource = dt;
                    Repeater1.DataBind();
                    heading.Text = "Issue- Return Product List";
                    lblRegister.Text = "Date : " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
                    for (int i = 0; i < Repeater1.Items.Count; i++)
                    {
                        var lblIssueid = (Label)Repeater1.Items[i].FindControl("lblid");
                        var RepeaterHistory = (Repeater)Repeater1.Items[i].FindControl("RepeaterHistory");
                        string ss1 = "Select ae.*, um.Name ItemName, format(ae.RecordedDate, 'dd-MMM-yyyy') ReturnDates, ";
                        ss1 = ss1 + " format(ae.RecordedDate, 'dd-MMM-yyyy hh:mm:ss tt') RecordedDates, isnull(ae.Qty,0) Qtys from invReturnProduct ae ";
                        ss1 = ss1 + " left join InvArticleEntry um on um.ID = ae.itemid and um.BranchCode = ae.BranchCode ";
                        ss1 = ss1 + " where ae.BranchCode = " + Session["BranchCode"] + " and ae.IssueId=" + lblIssueid.Text + "";
                        var dts = _oo.Fetchdata(ss1);
                        if (dts.Rows.Count > 0)
                        {
                            RepeaterHistory.DataSource = dts;
                            RepeaterHistory.DataBind();
                        }
                        else
                        {
                        }
                    }
                }
                else
                {
                    abc.Visible = false;
                    divExport.Visible = false;
                    divlistshow.Visible = false;
                    Repeater1.DataSource = null;
                    Repeater1.DataBind();
                }
                hfEmployeeId.Value = "";
                txtStaff.Text = "";
            }
            catch (Exception ex)
            {
                // ignored
            }
        }
       
        public override void Dispose()
        {
            _con.Dispose();
            _oo.Dispose();
        }
        protected void ImageButton1_Click(object sender, EventArgs e)
        {
            _oo.ExportTolandscapeWord(Response, "Issue-ReturnProductList", gdv1);
        }
        protected void ImageButton2_Click(object sender, EventArgs e)
        {
            _oo.ExportDivToExcelWithFormatting(Response, "Issue-ReturnProductList.xls", gdv1, Server.MapPath("~/Admin/css/style.css"));
        }
        protected void ImageButton3_Click(object sender, EventArgs e)
        {
            _oo.ExporttolandscapePdf(Response, "Issue-ReturnProductList", abc);
        }
        protected void ImageButton4_Click(object sender, EventArgs e)
        {
            PrintHelper_New.ctrl = abc;
            ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
        }

        
    }
}