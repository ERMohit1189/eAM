using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CancelInvoice : System.Web.UI.Page
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
        if (!Page.IsPostBack)
        {
        }
    }
    
    protected void loadDetails(string invoiceNo)
    {
        Gridview1.DataSource = null;
        Gridview1.DataBind();
        sql = "select po.*, Caregory, am.Name itemName, Caregory, am.id itemid  from invInvoiceEntry po ";
        sql = sql + "inner join InvArticleEntry am on am.ID=po.ItemId and am.BranchCode=po.BranchCode ";
        sql = sql + "where InvoiceNo='" + invoiceNo + "' and po.BranchCode=" + Session["BranchCode"] + "";
        var dt = _oo.Fetchdata(sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            pnlcontrols.Visible = true;
            Gridview1.DataSource = dt;
            Gridview1.DataBind();
            double qty = 0; double Amount = 0; double Tax = 0; double Total = 0;
            for (int i = 0; i < Gridview1.Rows.Count; i++)
            {
                Label lblQty = (Label)Gridview1.Rows[i].FindControl("lblQty");
                Label lblAmount = (Label)Gridview1.Rows[i].FindControl("Amount");
                Label lblTax = (Label)Gridview1.Rows[i].FindControl("Tax");
                Label lblTotal = (Label)Gridview1.Rows[i].FindControl("Total");
                Label itemCatId = (Label)Gridview1.Rows[i].FindControl("itemCatId");
                qty = qty + double.Parse(lblQty.Text == "" ? "0" : lblQty.Text);
                Amount = Amount + double.Parse(lblAmount.Text == "" ? "0" : lblAmount.Text);
                Tax = Tax + double.Parse(lblTax.Text == "" ? "0" : lblTax.Text);
                Total = Total + double.Parse(lblTotal.Text == "" ? "0" : lblTotal.Text);

                
            }
            Label lblSQty = (Label)Gridview1.FooterRow.FindControl("lblSQty");
            Label lblSAmount = (Label)Gridview1.FooterRow.FindControl("lblSAmount");
            Label lblSTax = (Label)Gridview1.FooterRow.FindControl("lblSTax");
            Label lblsTotal = (Label)Gridview1.FooterRow.FindControl("lblsTotal");
            lblSQty.Text = qty.ToString("0.00");
            lblSAmount.Text = Amount.ToString("0.00");
            lblSTax.Text = Tax.ToString("0.00");
            lblsTotal.Text = Total.ToString("0.00");
        }
        else
        {
            pnlcontrols.Visible = false;
        }

    }
    protected void lbtnSearchBy_Click(object sender, EventArgs e)
    {
        string ssp = "select id from invInvoiceEntry where BranchCode=" + Session["BranchCode"] + " and invoiceNo='" + txtInvoiceNo.Text.Trim() + "' and status='Pending'";
        if (_oo.Duplicate(ssp))
        {
            string ss = "select top(1) FilePath, pono from invInvoiceEntry where BranchCode=" + Session["BranchCode"] + " and invoiceNo='" + txtInvoiceNo.Text.Trim() + "' and status='Pending'";

            string filePath = _oo.ReturnTag(ss, "FilePath");
            if (_oo.ReturnTag(ss, "pono") != "")
            {
                sql = "select qt.*, qt.Id ids, '" + txtInvoiceNo.Text.Trim() + "' InvoiceNo, '" + filePath + "' filePaths, format(qt.QtnDate, 'dd-MMM-yyyy') QtnEnterDate, OrganizationName+' ('+VendorCode+')' VendorName from InvQuotation qt ";
                sql = sql + " inner join AccVendor v on v.id=qt.VendorId and v.BranchCode=qt.BranchCode  and qt.Status<>'Pending'";
                sql = sql + " where qt.BranchCode=" + Session["BranchCode"] + " ";
                sql = sql + " and qt.QtnNo in (select QtnNo from invPurchaeOrder where BranchCode=" + Session["BranchCode"] + " and Pono=(select top(1) PONO from invInvoiceEntry where BranchCode=" + Session["BranchCode"] + " and invoiceNo='" + txtInvoiceNo.Text.Trim() + "')) ";
                var dt = _oo.Fetchdata(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    gvBankBranchList.DataSource = dt;
                    gvBankBranchList.DataBind();
                    GridView2.DataSource = null;
                    GridView2.DataBind();
                    GridView2.Visible = false;
                    loadDetails(txtInvoiceNo.Text.Trim());
                    pnlcontrols.Visible = true;
                }
                else
                {
                    pnlcontrols.Visible = false;
                    Campus camp = new Campus(); camp.msgbox(Page, dvSearch, "Invalid Invoice No.!", "A");
                }
            }
            else
            {
                sql = "select '" + txtInvoiceNo.Text.Trim() + "' InvoiceNo, inv.VendorId, v.MobileNo, format(inv.RecordedDate, 'dd-MMM-yyyy hh:mm:ss tt') QtnEnterDaten, inv.LoginName, OrganizationName+' ('+VendorCode+')' VendorName  from AccVendor v  ";
                sql = sql + " inner join (select top(1) VendorId, BranchCode, Status, RecordedDate, LoginName from invInvoiceEntry where BranchCode=" + Session["BranchCode"] + " and invoiceNo='" + txtInvoiceNo.Text.Trim() + "' and Status='Pending') inv ";
                sql = sql + " on v.id=inv.VendorId and v.BranchCode=inv.BranchCode  and inv.Status='Pending'  ";
                sql = sql + " where v.BranchCode=" + Session["BranchCode"] + " ";
                var dt = _oo.Fetchdata(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    GridView2.DataSource = dt;
                    GridView2.DataBind();
                    gvBankBranchList.DataSource = null;
                    gvBankBranchList.DataBind();
                    gvBankBranchList.Visible = false;
                    loadDetails(txtInvoiceNo.Text.Trim());
                    pnlcontrols.Visible = true;
                }
                else
                {
                    pnlcontrols.Visible = false;
                    Campus camp = new Campus(); camp.msgbox(Page, dvSearch, "Invalid Invoice No.!", "A");
                }
            }
            
        }
        else
        {
            pnlcontrols.Visible = false;
            Campus camp = new Campus(); camp.msgbox(Page, dvSearch, "Invalid Invoice No.!", "A");
        }
    }



    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int ChkQty = 0;
        for (int i = 0; i < Gridview1.Rows.Count; i++)
        {
            var ddlArticle = (Label)Gridview1.Rows[i].FindControl("itemId");
            var lblQty = (Label)Gridview1.Rows[i].FindControl("lblQty");
            string dd = "select isnull((CASE WHEN Caregory='Services' THEN 1 ELSE Qty END), 0) Qty from InvArticleEntry where branchCode=" + Session["BranchCode"] + " and id=" + ddlArticle.Text.Trim() + "";
            double qts1 = double.Parse(_oo.ReturnTag(dd, "Qty"));
            double qts = double.Parse(lblQty.Text.Trim());
            if (qts1 < qts)
            {
                ChkQty = ChkQty + 1;
            }

        }
        if (ChkQty > 0)
        {
            Campus camp = new Campus(); camp.msgbox(Page, dvSearch, "Invoice can not be cancelled because product quantity is less then required quantity!", "A");
            return;
        }

        using (SqlCommand cmd = new SqlCommand())
        {
            int sts = 0;
            for (int i = 0; i < Gridview1.Rows.Count; i++)
            {
                cmd.CommandText = "invInvoiceEntryProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = _con;
                cmd.Parameters.AddWithValue("@InvoiceNo", txtInvoiceNo.Text.Trim());
                var ddlCategory = (Label)Gridview1.Rows[i].FindControl("itemCatName");
                var ddlArticle = (Label)Gridview1.Rows[i].FindControl("itemId");
                var txtQty = (Label)Gridview1.Rows[i].FindControl("lblQty");
                cmd.Parameters.AddWithValue("@ItemId", ddlArticle.Text.Trim());
                cmd.Parameters.AddWithValue("@Qty", txtQty.Text.Trim());
                cmd.Parameters.AddWithValue("@Category", ddlCategory.Text);
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                cmd.Parameters.AddWithValue("@Action", "Cancel");
                try
                {
                    _con.Open();
                    cmd.ExecuteNonQuery();
                    _con.Close();
                    cmd.Parameters.Clear();
                    sts = sts + 1;
                }
                catch (Exception ex)
                {
                }
            }
            if (sts > 0)
            {
                Campus camp = new Campus(); camp.msgbox(Page, dvSearch, "Cancelled successfully.", "S");
                Gridview1.DataSource = null;
                Gridview1.DataBind();
                pnlcontrols.Visible = false;
            }
        }
    }
}