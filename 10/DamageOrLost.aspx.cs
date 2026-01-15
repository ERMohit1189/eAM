using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _10
{
    public partial class DamageOrLost : System.Web.UI.Page
    {
        private SqlConnection _con;
        private readonly Campus _oo;
        private DataSet _ds;
        private string _sql = String.Empty;
        public DamageOrLost()
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
                GetArticleEntryList();
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {

        }
        private void GetArticleName()
        {
            _sql = "Select ID,Name from InvArticleEntry where BranchCode=" + Session["BranchCode"] + " and Caregory='Product'";
            _oo.FillDropDown_withValue(_sql, ddlProduct, "Name", "ID");
            ddlProduct.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
        
        private void GetArticleEntryList()
        {
            abc.Visible = true;
            divExport.Visible = true;
            try
            {
                var param = new List<SqlParameter> { new SqlParameter("@QueryFor", "S"), new SqlParameter("@BranchCode", Session["BranchCode"]) };
                _ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("invDamageOrLostProc", param);
                if (_ds.Tables[0].Rows.Count > 0)
                {
                    divlistshow.Visible = true;
                    Repeater1.DataSource = _ds;
                    Repeater1.DataBind();
                    heading.Text = "Damage Product List";
                    lblRegister.Text = "Date : " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
                }
                else
                {
                    abc.Visible = false;
                    divExport.Visible = false;
                    divlistshow.Visible = false;
                    Repeater1.DataSource = null;
                    Repeater1.DataBind();
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }
        protected void txtQty_TextChanged(object sender, EventArgs e)
        {
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
                        new SqlParameter("@type", rdoType.SelectedValue.Trim()),
                        new SqlParameter("@itemid", ddlProduct.SelectedValue),
                        new SqlParameter("@Qty", txtQty.Text.Trim()),
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

                    var msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("invDamageOrLostProc", param);

                    if (msg == "S")
                    {
                        Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
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
                        new SqlParameter("@id", lblID.Text.Trim()),
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

                var msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("invDamageOrLostProc", param);

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

            var msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("invDamageOrLostProc", param);

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
            _oo.ExportTolandscapeWord(Response, "DamageProductList", gdv1);
        }
        protected void ImageButton2_Click(object sender, EventArgs e)
        {
            _oo.ExportDivToExcelWithFormatting(Response, "DamageProductList.xls", gdv1, Server.MapPath("~/Admin/css/style.css"));
        }
        protected void ImageButton3_Click(object sender, EventArgs e)
        {
            _oo.ExporttolandscapePdf(Response, "DamageProductList", abc);
        }
        protected void ImageButton4_Click(object sender, EventArgs e)
        {
            PrintHelper_New.ctrl = abc;
            ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
        }
    }
}