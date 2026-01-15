using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class PurchaseOrderPrint : System.Web.UI.Page
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
        BLL.BLLInstance.LoadHeader("Report", header);
        if (!Page.IsPostBack)
        {
            if (Request.QueryString.Keys.Count > 0)
            {
                if (Request.QueryString["QTN"] != null && Request.QueryString["PO"] != null)
                {
                    string PONo = Request.QueryString["PO"].ToString();
                    string QtnNo = Request.QueryString["QTN"].ToString();
                    loadDetails(PONo, QtnNo);
                }
            }
        }
    }

   
    protected void loadDetails(string PONo, string QtnNo)
    {
        Gridview1.DataSource = null;
        Gridview1.DataBind();
        sql = "select po.*, Caregory, am.Name itemName  from invPurchaeOrder po ";
        sql = sql+ "inner join InvArticleEntry am on am.ID=po.ItemId and am.BranchCode=po.BranchCode ";
        sql = sql + "where PONo='"+ PONo + "' and QtnNo='"+ QtnNo + "' and po.BranchCode=" + Session["BranchCode"] + "";
        var dt = _oo.Fetchdata(sql);
        if (dt!=null && dt.Rows.Count>0)
        {
            pnlcontrols.Visible = true;
            divBtn.Visible = true;
            Gridview1.DataSource = dt;
            Gridview1.DataBind();
            double qty = 0; double Amount = 0; double Tax = 0; double Total = 0;
            for (int i = 0; i < Gridview1.Rows.Count; i++)
            {
                Label lblQty = (Label)Gridview1.Rows[i].FindControl("Qty");
                Label lblAmount = (Label)Gridview1.Rows[i].FindControl("Amount");
                Label lblTax = (Label)Gridview1.Rows[i].FindControl("Tax");
                Label lblTotal = (Label)Gridview1.Rows[i].FindControl("Total");

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

            _sql = "select top(1) * from invPurchaeOrder where PONo='" + PONo + "' and QtnNo='" + QtnNo + "' and BranchCode=" + Session["BranchCode"] + "";
            lblGTotal.Text = _oo.ReturnTag(_sql, "TotalAmount");
            if (_oo.ReturnTag(_sql, "TaxType") == "GST")
            {
                lblCGST.Text = _oo.ReturnTag(_sql, "CGST");
                lblSGST.Text = _oo.ReturnTag(_sql, "SGST");
                trCGST.Visible = true;
                trSGST.Visible = true;
                trIGST.Visible = false;
                trUGST.Visible = false;
                trNa.Visible = false;
                lblNa.Text = "0.00";
            }
            if (_oo.ReturnTag(_sql, "TaxType") == "IGST")
            {
                lblIGST.Text = _oo.ReturnTag(_sql, "IGST");
                trCGST.Visible = false;
                trSGST.Visible = false;
                trIGST.Visible = true;
                trUGST.Visible = false;
                trNa.Visible = false;
                lblNa.Text = "0.00";
            }
            if (_oo.ReturnTag(_sql, "TaxType") == "UGST")
            {
                lblUGST.Text = _oo.ReturnTag(_sql, "UGST");
                trCGST.Visible = false;
                trSGST.Visible = false;
                trIGST.Visible = false;
                trUGST.Visible = true;
                trNa.Visible = false;
                lblNa.Text = "0.00";
            }
            if (_oo.ReturnTag(_sql, "TaxType") == "NA")
            {
                trCGST.Visible = false;
                trSGST.Visible = false;
                trIGST.Visible = false;
                trUGST.Visible = false;
                lblUGST.Text = "0.00";
                lblCGST.Text = "0.00";
                lblSGST.Text = "0.00";
                lblIGST.Text = "0.00";
                trNa.Visible = true;
                lblNa.Text = "0.00";
            }
            lblGrandTotal.Text = _oo.ReturnTag(_sql, "GrandTotal");
            txtPODescription.Text = _oo.ReturnTag(_sql, "PODescription");
            txtTerms.Text = _oo.ReturnTag(_sql, "Terms");
            lblSubject.Text = _oo.ReturnTag(_sql, "subject");
            lblDate.Text = DateTime.Parse(_oo.ReturnTag(_sql, "OrderDate")).ToString("dd-MMM-yyyy");
        }
        else
        {
            pnlcontrols.Visible = false;
            divBtn.Visible = false;
        }

    }

    protected void txtPONos_TextChanged(object sender, EventArgs e)
    {
        txtPONo.Text = txtPONo.Text.Trim().ToUpper();
        var PONo = hdnPO.Value.Trim();
        string ss = "select top(1) QtnNo, * from invPurchaeOrder where PONo='" + PONo + "' and BranchCode=" + Session["BranchCode"] + "";
        if (!_oo.Duplicate(ss))
        {
            txtPONo.Text = "";
            hdnPO.Value = "";
            Campus camp = new Campus(); camp.msgbox(Page, dvSearch, "Invalid PO No.!", "A");
            return;
        }
        if (txtPONo.Text != string.Empty && PONo != String.Empty)
        {
            string QtnNo = _oo.ReturnTag(ss, "QtnNo");
            sql = sql + "select qt.*, '" + PONo + "' PONo, qt.Id ids, format(qt.QtnDate, 'dd-MMM-yyyy') QtnEnterDate, OrganizationName+' ('+VendorCode+')' VendorName from InvQuotation qt ";
            sql = sql + " inner join AccVendor v on v.id=qt.VendorId and v.BranchCode=qt.BranchCode  and qt.Status<>'Pending'";
            sql = sql + " where qt.BranchCode=" + Session["BranchCode"] + " ";
            sql = sql + " and qt.QtnNo='" + QtnNo.Trim() + "' ";
            var dt = _oo.Fetchdata(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                gvBankBranchList.DataSource = dt;
                gvBankBranchList.DataBind();
                pnlcontrols.Visible = true;
                loadDetails(PONo, QtnNo);
                lblPONo.Text = PONo.ToString();
                string pp = "select * from AccVendor where id=" + _oo.ReturnTag(sql, "VendorId") + " and BranchCode=" + Session["BranchCode"] + "";
                lblvendorCode.Text = _oo.ReturnTag(pp, "VendorCode");
                lblOragnization.Text = _oo.ReturnTag(pp, "OrganizationName");
                lblAddress.Text = _oo.ReturnTag(pp, "Address");
                lblPin.Text = _oo.ReturnTag(pp, "PIN");
                lblMobile.Text = _oo.ReturnTag(pp, "MobileNo");
                lblPhone.Text = _oo.ReturnTag(pp, "PhoneNo");
                RegNo.Text = _oo.ReturnTag(pp, "RegistrationNo");
                Pan.Text = _oo.ReturnTag(pp, "PAN");
                string pp1 = "select * from citymaster where id=" + _oo.ReturnTag(pp, "DistrictID") + "";
                lblCity.Text = _oo.ReturnTag(pp1, "cityname");
                string pp2 = "select * from statemaster where id=" + _oo.ReturnTag(pp, "StateID") + "";
                lblState.Text = _oo.ReturnTag(pp2, "statename");
                lblSubject.Text = _oo.ReturnTag(ss, "Subject");

            }
            else
            {
                Campus camp = new Campus(); camp.msgbox(Page, dvSearch, "Invalid PO No.!", "A");
            }
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(Page, dvSearch, "Invalid PO No.!", "A");
        }
    }
    protected void lbtnSearchBy_Click(object sender, EventArgs e)
    {
        txtPONo.Text = txtPONo.Text.Trim().ToUpper();
        var PONo = hdnPO.Value.Trim();
        string ss = "select top(1) QtnNo, * from invPurchaeOrder where PONo='" + PONo + "' and BranchCode=" + Session["BranchCode"] + "";
        if (!_oo.Duplicate(ss))
        {
            txtPONo.Text = "";
            hdnPO.Value="";
            Campus camp = new Campus(); camp.msgbox(Page, dvSearch, "Invalid PO No.!", "A");
            return;
        }
        if (txtPONo.Text != string.Empty && PONo != String.Empty)
        {
            string QtnNo = _oo.ReturnTag(ss, "QtnNo");
            sql = sql + "select qt.*, '" + PONo + "' PONo, qt.Id ids, format(qt.QtnDate, 'dd-MMM-yyyy') QtnEnterDate, OrganizationName+' ('+VendorCode+')' VendorName from InvQuotation qt ";
            sql = sql + " inner join AccVendor v on v.id=qt.VendorId and v.BranchCode=qt.BranchCode  and qt.Status<>'Pending'";
            sql = sql + " where qt.BranchCode=" + Session["BranchCode"] + " ";
            sql = sql + " and qt.QtnNo='" + QtnNo.Trim() + "' ";
            var dt = _oo.Fetchdata(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                gvBankBranchList.DataSource = dt;
                gvBankBranchList.DataBind();
                pnlcontrols.Visible = true;
                loadDetails(PONo, QtnNo);
                lblPONo.Text = PONo.ToString();
                string pp = "select * from AccVendor where id="+_oo.ReturnTag(sql, "VendorId") +" and BranchCode=" + Session["BranchCode"] + "";
                lblvendorCode.Text = _oo.ReturnTag(pp, "VendorCode");
                lblOragnization.Text = _oo.ReturnTag(pp, "OrganizationName");
                lblAddress.Text = _oo.ReturnTag(pp, "Address");
                lblPin.Text = _oo.ReturnTag(pp, "PIN");
                lblMobile.Text = _oo.ReturnTag(pp, "MobileNo");
                lblPhone.Text = _oo.ReturnTag(pp, "PhoneNo");
                RegNo.Text = _oo.ReturnTag(pp, "RegistrationNo");
                Pan.Text = _oo.ReturnTag(pp, "PAN");
                string pp1 = "select * from citymaster where id=" + _oo.ReturnTag(pp, "DistrictID") + "";
                lblCity.Text = _oo.ReturnTag(pp1, "cityname");
                string pp2 = "select * from statemaster where id=" + _oo.ReturnTag(pp, "StateID") + "";
                lblState.Text = _oo.ReturnTag(pp2, "statename");
                lblSubject.Text = _oo.ReturnTag(ss, "Subject");

            }
            else
            {
                Campus camp = new Campus(); camp.msgbox(Page, dvSearch, "Invalid PO No.!", "A");
            }
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(Page, dvSearch, "Invalid PO No.!", "A");
        }
    }
    
    protected void btnprint_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = pnlcontrols;
        ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }
}