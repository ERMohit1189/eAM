using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _10
{
    public partial class IssueProduct : System.Web.UI.Page
    {
        private SqlConnection _con;
        private readonly Campus _oo;
        private DataSet _ds;
        private string _sql = String.Empty;
        public IssueProduct()
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
                GetArticleName();
            }
        }
        protected void lnkShow_Click(object sender, EventArgs e)
        {
            displayEmpInfo();
        }
        protected void txtHeaderEmpId_TextChanged(object sender, EventArgs e)
        {
            displayEmpInfo();
        }
        public override void VerifyRenderingInServerForm(Control control)
        {

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
                GetArticleEntryList();
            }
            else
            {
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, No Record found!", "A");
                divTools.Visible = false;
            }
        }
        private void GetArticleName()
        {
            _sql = "Select ID,Name from InvArticleEntry where BranchCode=" + Session["BranchCode"] + " and Caregory='Product'";
            _oo.FillDropDown_withValue(_sql, ddlProduct, "Name", "ID");
            ddlProduct.Items.Insert(0, new ListItem("<--Select-->", ""));
        }

        private void GetArticleEntryList()
        {

            var empId = Request.Form[hfEmployeeId.UniqueID];
            if (empId == string.Empty || txtStaff.Text.Trim() == "")
            {
                empId = "";
                txtStaff.Text = "";
            }
            abc.Visible = true;
            divExport.Visible = true;
            try
            {
                string ss = "Select ae.*, um.Name ItemName, emp.Name, format(ae.IssueDate, 'dd-MMM-yyyy') IssueDates, ";
                ss = ss + " format(ae.RecordedDate, 'dd-MMM-yyyy hh:mm:ss tt') RecordedDates, isnull(ae.ReturnQty,0) ReturnQtys from invIssueProduct ae ";
                ss = ss + " left join InvArticleEntry um on um.ID = ae.itemid and um.BranchCode = ae.BranchCode ";
                ss = ss + " left join GetAllStaffRecords_UDF(" + Session["BranchCode"] + ") emp on emp.Ecode = ae.EmpCode and emp.BranchCode = ae.BranchCode ";
                ss = ss + " where ae.BranchCode = " + Session["BranchCode"] + "";
                if (empId != "" && empId != null)
                {
                    ss = ss + " and ae.EmpCode = '" + empId + "' ";
                }
                ss = ss + " order by ae.ID asc";

                var dt = _oo.Fetchdata(ss);
                if (dt.Rows.Count > 0)
                {
                    pnlcontrols.Visible = true;
                    divlistshow.Visible = true;
                    Repeater1.DataSource = dt;
                    Repeater1.DataBind();
                    heading.Text = "Issue- Return List";
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
                    pnlcontrols.Visible = false;
                    Repeater1.DataSource = null;
                    Repeater1.DataBind();
                }
               // hfEmployeeId.Value = "";
               // txtStaff.Text = "";
            }
            catch (Exception ex)
            {
                // ignored
            }
        }
        protected void txtQty_TextChanged(object sender, EventArgs e)
        {
            
            if (ddlProduct.SelectedIndex==0)
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Select product!", "A");
                txtQty.Text = "";
                return;
            }
            _sql = "Select isnull(qty, 0) qty from InvArticleEntry where id='" + ddlProduct.SelectedValue + "' and  BranchCode=" + Session["BranchCode"] + "";
            int qty = int.Parse(_oo.ReturnTag(_sql, "qty"));
            if (qty < int.Parse(txtQty.Text == "" ? "0" : txtQty.Text.Trim()))
            {
                txtQty.Text = "";
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Invalid Quantity!", "A");
            }
        }
        protected void txtEditQty_TextChanged(object sender, EventArgs e)
        {
            _sql = "Select isnull(qty, 0) qty from InvArticleEntry where id='" + lblItemIdsss.Text.Trim() + "' and  BranchCode=" + Session["BranchCode"] + "";
            int qty = int.Parse(_oo.ReturnTag(_sql, "qty"));
            if (qty < int.Parse(txtEditQty.Text == "" ? "0" : txtEditQty.Text.Trim()))
            {
                txtEditQty.Text = "";
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Invalid Quantity!", "A");
            }
            Panel1_ModalPopupExtender.Show();
        }
        protected void lnkSubmit_OnClick(object sender, EventArgs e)
        {
            var empId = Request.Form[hfEmployeeId.UniqueID];
            if (empId == string.Empty)
            {
                empId = txtStaff.Text.Trim();
                return;
            }
            try
            {
                _sql = "Select isnull(qty, 0) qty from InvArticleEntry where id='" + ddlProduct.SelectedValue + "' and  BranchCode=" + Session["BranchCode"] + "";
                int qty =int.Parse(_oo.ReturnTag(_sql, "qty"));
                if (qty< int.Parse(txtQty.Text==""?"0": txtQty.Text.Trim()))
                {
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Invalid Quantity!", "A");
                }
                else
                {
                    var param = new List<SqlParameter>
                    {
                        new SqlParameter("@EmpCode", empId),
                        new SqlParameter("@itemid", ddlProduct.SelectedValue),
                        new SqlParameter("@Qty", txtQty.Text.Trim()),
                        new SqlParameter("@IssueDate", txtIssueDate.Text.Trim()),
                        new SqlParameter("@Remark", txtremark.Text.Trim()),
                        new SqlParameter("@LoginName", Session["LoginName"].ToString()),
                        new SqlParameter("@BranchCode", Session["BranchCode"].ToString()),
                        new SqlParameter("@QueryFor", "I")
                    };
                    var para = new SqlParameter("@Msg", "")
                    {
                        Direction = ParameterDirection.Output,
                        Size = 0x100
                    };
                    param.Add(para);

                    var msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("invIssueProductProc", param);

                    if (msg == "S")
                    {
                        Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
                        divTools.Visible = false;
                        ddlProduct.SelectedIndex = 0;
                        txtQty.Text = "";
                        txtremark.Text = "";
                    }
                    else
                    {
                        Campus camp = new Campus(); camp.msgbox(Page, msgbox, msg, "A");
                    }
                    _oo.ClearControls(Page);
                    GetArticleEntryList();
                }
            }
            catch (Exception)
            {
            }
        }
        
        protected void LinkButton1_OnClick(object sender, EventArgs e)
        {
            try
            {
                var lnk = (LinkButton)sender;
                var id = (Label)lnk.NamingContainer.FindControl("lblid");
                var lblItemIds = (Label)lnk.NamingContainer.FindControl("lblItemIds");
                var lblqty = (Label)lnk.NamingContainer.FindControl("lblqty");
                var lblRemark = (Label)lnk.NamingContainer.FindControl("lblRemark");

                lblItemIdsss.Text = lblItemIds.Text.Trim();
                txtEditQty.Text = lblqty.Text.Trim();
                txtEditRemark.Text = lblRemark.Text.Trim();
                lblID.Text = id.Text.Trim();
                Panel1_ModalPopupExtender.Show();
            }
            catch (Exception)
            {
            }
        }

        protected void lnkDelete_OnClick(object sender, EventArgs e)
        {
            var lnk = (LinkButton)sender;
            var lblid = (Label)lnk.NamingContainer.FindControl("lblid");
            var lblItemIds = (Label)lnk.NamingContainer.FindControl("lblItemIds");
            var lblqty = (Label)lnk.NamingContainer.FindControl("lblqty");
            lblvalue.Text = lblid.Text;
            lblProdId.Text = lblItemIds.Text;
            txtLblQty.Text = lblqty.Text.Trim();
            Panel2_ModalPopupExtender.Show();
        }
        protected void btnupdate_OnClick(object sender, EventArgs e)
        {
            try
            {
                var param = new List<SqlParameter>
                    {
                        new SqlParameter("@id", lblID.Text),
                        new SqlParameter("@itemid", lblItemIdsss.Text.Trim()),
                        new SqlParameter("@Remark", txtEditRemark.Text),
                        new SqlParameter("@LoginName", Session["LoginName"].ToString()),
                        new SqlParameter("@BranchCode", Session["BranchCode"].ToString()),
                        new SqlParameter("@QueryFor", "U")
                    };
                var para = new SqlParameter("@Msg", "")
                {
                    Direction = ParameterDirection.Output,
                    Size = 0x100
                };
                param.Add(para);

                var msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("invIssueProductProc", param);

                if (msg == "S")
                {
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Updated successfully.", "S");
                }
                else
                {
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, msg, "W");
                }
                GetArticleEntryList();
            }
            catch (Exception)
            {
            }
        }

        protected void btnDelete_OnClick(object sender, EventArgs e)
        {
            var param = new List<SqlParameter>
            {
                new SqlParameter("@id", lblvalue.Text.Trim()),
                new SqlParameter("@itemid", lblProdId.Text.Trim()),
                new SqlParameter("@Qty", txtLblQty.Text.Trim()),
                new SqlParameter("@BranchCode", Session["BranchCode"]),
                new SqlParameter("@QueryFor", "D")
            };
            var para = new SqlParameter("@Msg", "")
            {
                Direction = ParameterDirection.Output,
                Size = 0x100
            };
            param.Add(para);

            var msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("invIssueProductProc", param);

            if (msg == "S")
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Deleted successfully.", "S");
            }
            GetArticleEntryList();
        }
        public override void Dispose()
        {
            _con.Dispose();
            _oo.Dispose();
        }
        protected void ImageButton1_Click(object sender, EventArgs e)
        {
            _oo.ExportTolandscapeWord(Response, "IssueProductList", gdv1);
        }
        protected void ImageButton2_Click(object sender, EventArgs e)
        {
            _oo.ExportDivToExcelWithFormatting(Response, "IssueProductList.xls", gdv1, Server.MapPath("~/Admin/css/style.css"));
        }
        protected void ImageButton3_Click(object sender, EventArgs e)
        {
            _oo.ExporttolandscapePdf(Response, "IssueProductList", abc);
        }
        protected void ImageButton4_Click(object sender, EventArgs e)
        {
            PrintHelper_New.ctrl = abc;
            ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
        }
    }
}