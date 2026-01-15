using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _7
{
    public partial class AdminQuotationApproval : Page
    {
        string sql = "", _sql = "", Sql = "";
        Campus _oo = new Campus();
        private SqlConnection _con;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            _con = new SqlConnection();
            _con = _oo.dbGet_connection();
            Campus camp = new Campus(); camp.LoadLoader(loader);
            if (!IsPostBack)
            {
                try
                {
                    txtQtnFromDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                    txtQtnToDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                    GetQtnList();
                }
                catch (Exception ex)
                {
                }
            }
        }
        protected void txtSearchBy_TextChanged(object sender, EventArgs e)
        {
            var studentId = hfVendorId.Value;
            if (txtSearchBy.Text != string.Empty && studentId != String.Empty)
            {
                GetQtnList();
            }
            else
            {
                Campus camp = new Campus(); camp.msgbox(this.Page, dvSearch, BLL.BLLInstance.FetchMSG("Enter Vendor ID !"), "A");
                txtSearchBy.Focus();
            }
        }

        protected void lbtnSearchBy_Click(object sender, EventArgs e)
        {
          GetQtnList();
        }

        private void GetQtnList()
        {
            var studentId = hfVendorId.Value;
            sql = sql + "select qt.*, qt.Id ids, format(qt.QtnDate, 'dd-MMM-yyyy') QtnEnterDate, OrganizationName+' ('+VendorCode+')' VendorName from InvQuotation qt ";
            sql = sql + " inner join AccVendor v on v.id=qt.VendorId and v.BranchCode=qt.BranchCode and convert(date, qt.QtnDate) between '"+ txtQtnFromDate.Text.Trim() + "' and '"+ txtQtnToDate.Text.Trim() + "'";
            sql = sql + " where qt.BranchCode=" + Session["BranchCode"] + " and qt.Status='Pending'";
            if (studentId != "")
            {
                sql = sql + " and qt.Vendorid=" + studentId + " ";
            }
            if (txtQtnNo.Text.Trim() != "")
            {
                sql = sql + " and qt.QtnNo='" + txtQtnNo.Text.Trim() + "' ";
            }
            var dt = _oo.Fetchdata(sql);
            gvBankBranchList.DataSource = dt;
            gvBankBranchList.DataBind();
            for (int i = 0; i < gvBankBranchList.Rows.Count; i++)
            {
                Label Status = (Label)gvBankBranchList.Rows[i].FindControl("Status");
                LinkButton lbtnEdit = (LinkButton)gvBankBranchList.Rows[i].FindControl("lbtnEdit");
                if (Status.Text != "Pending")
                {
                    lbtnEdit.Text = "<i class='fa fa-lock'></i>";
                    lbtnEdit.Enabled = false;
                }
            }

        }

        protected void lbtnEdit_Click(object sender, EventArgs e)
        {
            LinkButton chk = (LinkButton)sender;
            Label lblId = (Label)chk.NamingContainer.FindControl("Label36");
            string ss = lblId.Text;
            lblID.Text = ss;
            Panel1_ModalPopupExtender.Show();
        }
        protected void Button3_Click(object sender, EventArgs e)
        {

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "InvQuotationProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = _con;
                cmd.Parameters.AddWithValue("@Id", lblID.Text.Trim());
                cmd.Parameters.AddWithValue("@Status", ddlStatus.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@RemarkApprove", txtRemark0.Text.Trim());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@ApproveBy", Session["LoginName"].ToString());
                cmd.Parameters.AddWithValue("@Action", "approveOrReject");
                try
                {
                    _con.Open();
                    cmd.ExecuteNonQuery();
                    _con.Close();
                    Campus camp = new Campus(); camp.msgbox(Page, dvSearch, "Updated successfully.", "S");
                    txtRemark0.Text = "";
                    ddlStatus.SelectedIndex = 0;
                    GetQtnList();
                    _sql = "";
                }
                catch (Exception ex)
                {
                }
            }
        }

        public override void Dispose()
        {
            _oo.Dispose();
        }
    }
}