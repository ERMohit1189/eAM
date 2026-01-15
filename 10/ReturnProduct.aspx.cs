using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _10
{
    public partial class ReturnProduct : System.Web.UI.Page
    {
        private SqlConnection _con;
        private readonly Campus _oo;
        private DataSet _ds;
        private string _sql = String.Empty;
        public ReturnProduct()
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
                txtRerurnEditDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {

        }
        private void GetArticleName(string EmpCode)
        {
            _sql = "Select ID,Name from InvArticleEntry where BranchCode=" + Session["BranchCode"] + " and Caregory='Product'";
            _sql = _sql+ " and id in (select itemid from invIssueProduct where EmpCode='"+ EmpCode + "' and BranchCode=" + Session["BranchCode"] + ")";
            _oo.FillDropDown_withValue(_sql, ddlProduct, "Name", "ID");
            ddlProduct.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
        protected void lnkShow_Click(object sender, EventArgs e)
        {
            displayEmpInfo();
        }
        protected void txtHeaderEmpId_TextChanged(object sender, EventArgs e)
        {
            displayEmpInfo();
        }
        public void displayEmpInfo()
        {
            var empId = Request.Form[hfEmployeeId.UniqueID];
            if (empId == string.Empty)
            {
                empId = txtStaff.Text.Trim();
            }
            _sql = "Select eod.EmpId EmpId,eod.Ecode Ecode,egd.EFirstName+' '+egd.EMiddleName+' '+egd.ELastName as EmpName,egd.EFatherName FatherName,eod.Designation Designation from EmpployeeOfficialDetails eod ";
            _sql = _sql + " inner join EmpGeneralDetail egd on eod.Ecode=egd.Ecode and eod.EmpId=egd.EmpId where eod.Withdrwal is null ";
            _sql = _sql + " and eod.ECode='" + empId.Trim() + "' and eod.BranchCode=" + Session["BranchCode"].ToString() + " and egd.BranchCode=" + Session["BranchCode"].ToString() + "";
            Grd.DataSource = _oo.GridFill(_sql);
            Grd.DataBind();
            if (Grd.Rows.Count > 0)
            {
                divTools.Visible = true;
                GetArticleName(empId);
                GetArticleEntryList();
            }
            else
            {
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, No Record found!", "A");
                divTools.Visible = false;
            }
        }
        protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetArticleEntryList();
        }
        private void GetArticleEntryList()
        {
            var empId = Request.Form[hfEmployeeId.UniqueID];
            if (empId == string.Empty)
            {
                empId = txtStaff.Text.Trim();
            }
            abc.Visible = true;
            divExport.Visible = true;
            pnlcontrols.Visible = true;
            try
            {
                string ss = "Select ae.*, um.Name ItemName, emp.Name, format(ae.IssueDate, 'dd-MMM-yyyy') IssueDates, ";
                ss = ss + " format(ae.RecordedDate, 'dd-MMM-yyyy hh:mm:ss tt') RecordedDates, isnull(ae.ReturnQty,0) ReturnQtys from invIssueProduct ae ";
                ss = ss + " left join InvArticleEntry um on um.ID = ae.itemid and um.BranchCode = ae.BranchCode ";
                ss = ss + " left join GetAllStaffRecords_UDF("+ Session["BranchCode"] + ") emp on emp.Ecode = ae.EmpCode and emp.BranchCode = ae.BranchCode ";
                //ss = ss + " left join (select Issueid, itemid, EmpCode,sum(isnull(qty,0))Qty, BranchCode from invReturnProduct group by Issueid, itemid, EmpCode, BranchCode)T1 on T1.Issueid = ae.Id and T1.BranchCode = ae.BranchCode ";
                ss = ss + " where ae.BranchCode = " + Session["BranchCode"] + " and isnull(ae.ReturnQty,0)<>ae.Qty";
                if (ddlProduct.SelectedIndex!=0)
                {
                    ss = ss + " and ae.ItemId = " + ddlProduct.SelectedValue + " ";
                }
                if (empId!="" && empId!=null)
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
                    heading.Text = "Pending Return Product List";
                    lblRegister.Text = "Date : " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
                }
                else
                {
                    abc.Visible = false;
                    divExport.Visible = false;
                    divlistshow.Visible = false;
                    pnlcontrols.Visible = false;
                    Repeater1.DataSource = null;
                    Repeater1.DataBind();
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, No products issued!", "A");
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }
       
        protected void txtEditQty_TextChanged(object sender, EventArgs e)
        {
            int qty = int.Parse(txtReturnableQty.Text);
            if (qty < int.Parse(txtEditQty.Text == "" ? "0" : txtEditQty.Text.Trim())|| int.Parse(txtEditQty.Text == "" ? "0" : txtEditQty.Text.Trim())==0)
            {
                txtEditQty.Text = txtReturnableQty.Text;
                Campus camp = new Campus(); camp.msgbox(Page, Div2, "Invalid Quantity!", "A");
            }
            Panel1_ModalPopupExtender.Show();
        }
        
        protected void LinkButton1_OnClick(object sender, EventArgs e)
        {
            try
            {
                var lnk = (LinkButton)sender;
                var id = (Label)lnk.NamingContainer.FindControl("lblid");
                var lblItemIds = (Label)lnk.NamingContainer.FindControl("lblItemIds");
                var lblqty = (Label)lnk.NamingContainer.FindControl("lblqty");
                var lblReturnQty = (Label)lnk.NamingContainer.FindControl("lblReturnQty");
                var lblRemark = (Label)lnk.NamingContainer.FindControl("lblRemark");
                var EmpCode = (Label)lnk.NamingContainer.FindControl("Label1");
                int ReturnableQty = int.Parse(lblqty.Text) - int.Parse(lblReturnQty.Text);

                lblItemIdsss.Text = lblItemIds.Text.Trim();
                txtEditQty.Text = ReturnableQty.ToString();
                txtReturnableQty.Text = ReturnableQty.ToString();
                lblID.Text = id.Text.Trim();
                lblEmpCode.Text = EmpCode.Text;
                Panel1_ModalPopupExtender.Show();
            }
            catch (Exception)
            {
            }
        }
        protected void btnupdate_OnClick(object sender, EventArgs e)
        {
            int qty = int.Parse(txtReturnableQty.Text);
            if (qty < int.Parse(txtEditQty.Text == "" ? "0" : txtEditQty.Text.Trim()) || int.Parse(txtEditQty.Text == "" ? "0" : txtEditQty.Text.Trim()) == 0)
            {
                txtEditQty.Text = "";
                Campus camp = new Campus(); camp.msgbox(Page, Div2, "Invalid Quantity!", "A");
                Panel1_ModalPopupExtender.Show();
            }
            else
            {
                try
                {
                    var param = new List<SqlParameter>
                    {
                        new SqlParameter("@Issueid", lblID.Text),
                        new SqlParameter("@itemid", lblItemIdsss.Text.Trim()),
                        new SqlParameter("@EmpCode", lblEmpCode.Text.Trim()),
                        new SqlParameter("@ReturDate", txtRerurnEditDate.Text.Trim()),
                        new SqlParameter("@Qty", txtEditQty.Text.Trim()),
                        new SqlParameter("@Remark", txtEditRemark.Text),
                        new SqlParameter("@LoginName", Session["LoginName"].ToString()),
                        new SqlParameter("@BranchCode", Session["BranchCode"].ToString()),
                        new SqlParameter("@QueryFor", "Return")
                    };
                    var para = new SqlParameter("@Msg", "")
                    {
                        Direction = ParameterDirection.Output,
                        Size = 0x100
                    };
                    param.Add(para);

                    var msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("invReturnProductProc", param);

                    if (msg == "S")
                    {
                        Panel1_ModalPopupExtender.Hide();
                        Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
                        GetArticleEntryList();
                    }
                    else
                    {
                        Campus camp = new Campus(); camp.msgbox(Page, msgbox, msg, "W");
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        public override void Dispose()
        {
            _con.Dispose();
            _oo.Dispose();
        }
        protected void ImageButton1_Click(object sender, EventArgs e)
        {
            _oo.ExportTolandscapeWord(Response, "PendingReturnProductList", gdv1);
        }
        protected void ImageButton2_Click(object sender, EventArgs e)
        {
            _oo.ExportDivToExcelWithFormatting(Response, "PendingReturnProductList.xls", gdv1, Server.MapPath("~/Admin/css/style.css"));
        }
        protected void ImageButton3_Click(object sender, EventArgs e)
        {
            _oo.ExporttolandscapePdf(Response, "PendingReturnProductList", abc);
        }
        protected void ImageButton4_Click(object sender, EventArgs e)
        {
            PrintHelper_New.ctrl = abc;
            ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
        }



        
    }
}