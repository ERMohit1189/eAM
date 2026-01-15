using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class InvoiceEntry : System.Web.UI.Page
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
            SetInitialRow();
        }
    }
    protected void rblInvoiceType_SelectedIndexChanged(object sender, EventArgs e)
    {
        Reset();
        if (rblInvoiceType.SelectedValue == "POWise")
        {
            divPo.Visible = true;
            divVendor.Visible = false;
        }
        else
        {
            divPo.Visible = false;
            divVendor.Visible = true;
        }
    }

    protected void LoadArticle(DropDownList drpArticleCategory, DropDownList drpArticle)
    {
        _sql = "Select ID,Name, HSNCode from InvArticleEntry where  BranchCode=" + Session["BranchCode"] + " and Caregory='" + drpArticleCategory.SelectedValue + "'";
        _oo.FillDropDown_withValue(_sql, drpArticle, "Name", "ID");
        drpArticle.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        var lnk = (DropDownList)sender;
        var ddlCategory = (DropDownList)lnk.NamingContainer.FindControl("ddlCategory");
        var ddlArticle = (DropDownList)lnk.NamingContainer.FindControl("ddlArticle");
        var txtNextDueOn = (TextBox)lnk.NamingContainer.FindControl("txtNextDueOn");
        LoadArticle(ddlCategory, ddlArticle);
        txtNextDueOn.Text = "";
        if (ddlCategory.SelectedValue == "Product")
        {
             txtNextDueOn.Enabled = false;
        }
        else
        {
            txtNextDueOn.Enabled = true;
        }

    }
    protected void ddlArticle_SelectedIndexChanged(object sender, EventArgs e)
    {
        var lnk = (DropDownList)sender;
        var ddlCategory = (DropDownList)lnk.NamingContainer.FindControl("ddlCategory");
        var ddlArticle = (DropDownList)lnk.NamingContainer.FindControl("ddlArticle");
        var txtHSNCode = (TextBox)lnk.NamingContainer.FindControl("txtHSNCode");
        var txtQty = (TextBox)lnk.NamingContainer.FindControl("txtQty");
        var txtNextDueOn = (TextBox)lnk.NamingContainer.FindControl("txtNextDueOn");
        _sql = "Select HSNCode, Caregory from InvArticleEntry where  BranchCode=" + Session["BranchCode"] + " and Caregory='" + ddlCategory.SelectedValue + "' and id=" + ddlArticle.SelectedValue + "";
        txtHSNCode.Text = _oo.ReturnTag(_sql, "HSNCode");
        if (_oo.ReturnTag(_sql, "Caregory") == "Product")
        {
            txtQty.Enabled = true;
            txtQty.Text = "";
        }
        else
        {
            txtQty.Enabled = false;
            txtQty.Text = "1";
        }

    }
    private void SetInitialRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("Column1", typeof(string)));
        dt.Columns.Add(new DataColumn("Column2", typeof(string)));
        dt.Columns.Add(new DataColumn("Column3", typeof(string)));
        dt.Columns.Add(new DataColumn("Column4", typeof(string)));
        dt.Columns.Add(new DataColumn("Column5", typeof(string)));
        dt.Columns.Add(new DataColumn("Column6", typeof(string)));
        dt.Columns.Add(new DataColumn("Column7", typeof(string)));
        dt.Columns.Add(new DataColumn("Column8", typeof(string)));
        dt.Columns.Add(new DataColumn("Column81", typeof(string)));
        dt.Columns.Add(new DataColumn("Column9", typeof(string)));
        dt.Columns.Add(new DataColumn("Column91", typeof(string)));
        dr = dt.NewRow();
        dr["RowNumber"] = 1;
        dr["Column1"] = string.Empty;
        dr["Column2"] = string.Empty;
        dr["Column3"] = string.Empty;
        dr["Column4"] = string.Empty;
        dr["Column5"] = string.Empty;
        dr["Column6"] = string.Empty;
        dr["Column7"] = string.Empty;
        dr["Column8"] = string.Empty;
        dr["Column81"] = string.Empty;
        dr["Column9"] = string.Empty;
        dr["Column91"] = string.Empty;
        dt.Rows.Add(dr);
        ViewState["CurrentTable"] = dt;
        Gridview1.DataSource = dt;
        Gridview1.DataBind();
        if (Gridview1.Rows.Count == 1)
        {
            LinkButton ButtonRemove = (LinkButton)Gridview1.Rows[0].FindControl("ButtonRemove");
            ButtonRemove.Visible = false;
        }
        DropDownList ddlCategory = (DropDownList)Gridview1.Rows[0].FindControl("ddlCategory");
        DropDownList ddlArticle = (DropDownList)Gridview1.Rows[0].FindControl("ddlArticle");
        TextBox txtNextDueOn = (TextBox)Gridview1.Rows[0].FindControl("txtNextDueOn");
        TextBox txtTax = (TextBox)Gridview1.Rows[0].FindControl("txtTax");
        if (ddlTaxType.SelectedIndex == 0)
        {
            Label lblTax1 = (Label)Gridview1.Rows[0].FindControl("lblTax1");
            if (ddlTaxType.SelectedIndex == 0)
            {
                txtTax.Text = "0.00";
                lblTax1.Text = "0.00";
                txtTax.Enabled = false;
            }
            else
            {
                txtTax.Enabled = true;
            }
            txtTax.Text = "0.00";
            txtTax.Enabled = false;
        }
        ddlArticle.Items.Insert(0, new ListItem("<--Select-->", ""));
        Gridview2.DataSource = null;
        Gridview2.DataBind();
        Gridview2.Visible = false;
        Gridview1.Visible = true;
        //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "datetime();", true);
        txtNextDueOn.Text = "";
    }

    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        AddNewRowToGrid();
    }
    private void AddNewRowToGrid()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    //extract the TextBox values  
                    DropDownList ddlCategory = (DropDownList)Gridview1.Rows[rowIndex].FindControl("ddlCategory");
                    DropDownList ddlArticle = (DropDownList)Gridview1.Rows[rowIndex].FindControl("ddlArticle");
                    TextBox txtHSNCode = (TextBox)Gridview1.Rows[rowIndex].FindControl("txtHSNCode");
                    TextBox txtDescription = (TextBox)Gridview1.Rows[rowIndex].FindControl("txtDescription");
                    TextBox txtQty = (TextBox)Gridview1.Rows[rowIndex].FindControl("txtQty");
                    TextBox txtRate = (TextBox)Gridview1.Rows[rowIndex].FindControl("txtRate");
                    Label lblAmount = (Label)Gridview1.Rows[rowIndex].FindControl("lblAmount");
                    TextBox txtTax = (TextBox)Gridview1.Rows[rowIndex].FindControl("txtTax");
                    Label lblTax1 = (Label)Gridview1.Rows[rowIndex].FindControl("lblTax1");
                    Label lblTotal = (Label)Gridview1.Rows[rowIndex].FindControl("lblTotal");
                    TextBox txtNextDueOn = (TextBox)Gridview1.Rows[rowIndex].FindControl("txtNextDueOn");
                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;
                    dtCurrentTable.Rows[i - 1]["Column1"] = ddlCategory.SelectedValue;
                    dtCurrentTable.Rows[i - 1]["Column2"] = ddlArticle.SelectedValue;
                    dtCurrentTable.Rows[i - 1]["Column3"] = txtHSNCode.Text;
                    dtCurrentTable.Rows[i - 1]["Column4"] = txtDescription.Text;
                    dtCurrentTable.Rows[i - 1]["Column5"] = txtQty.Text;
                    dtCurrentTable.Rows[i - 1]["Column6"] = txtRate.Text;
                    dtCurrentTable.Rows[i - 1]["Column7"] = lblAmount.Text;
                    dtCurrentTable.Rows[i - 1]["Column8"] = txtTax.Text;
                    dtCurrentTable.Rows[i - 1]["Column81"] = lblTax1.Text;
                    dtCurrentTable.Rows[i - 1]["Column9"] = lblTotal.Text;
                    dtCurrentTable.Rows[i - 1]["Column91"] = txtNextDueOn.Text;
                    rowIndex++;
                }
                dtCurrentTable.Rows.Add(drCurrentRow);
                ViewState["CurrentTable"] = dtCurrentTable;
                Gridview1.DataSource = dtCurrentTable;
                Gridview1.DataBind();
                for (int i = 0; i < Gridview1.Rows.Count; i++)
                {
                    TextBox txtTax = (TextBox)Gridview1.Rows[i].FindControl("txtTax");
                   
                    if (ddlTaxType.SelectedIndex == 0)
                    {
                        txtTax.Text = "0.00";
                        txtTax.Enabled = false;
                    }
                    else
                    {
                        txtTax.Enabled = true;
                    }
                    
                }
            }
            if (dtCurrentTable.Rows.Count == 1)
            {
                LinkButton ButtonRemove = (LinkButton)Gridview1.Rows[0].FindControl("ButtonRemove");
                ButtonRemove.Visible = false;
            }

        }
        else
        {
            Response.Write("ViewState is null");
        }
        //Set Previous Data on Postbacks  
        SetPreviousData();
        calculation();
    }

    private void SetPreviousData()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DropDownList ddlCategory = (DropDownList)Gridview1.Rows[rowIndex].FindControl("ddlCategory");
                    DropDownList ddlArticle = (DropDownList)Gridview1.Rows[rowIndex].FindControl("ddlArticle");

                    TextBox txtHSNCode = (TextBox)Gridview1.Rows[rowIndex].FindControl("txtHSNCode");
                    TextBox txtDescription = (TextBox)Gridview1.Rows[rowIndex].FindControl("txtDescription");
                    TextBox txtQty = (TextBox)Gridview1.Rows[rowIndex].FindControl("txtQty");
                    TextBox txtRate = (TextBox)Gridview1.Rows[rowIndex].FindControl("txtRate");
                    Label lblAmount = (Label)Gridview1.Rows[rowIndex].FindControl("lblAmount");
                    TextBox txtTax = (TextBox)Gridview1.Rows[rowIndex].FindControl("txtTax");
                    Label lblTax1 = (Label)Gridview1.Rows[rowIndex].FindControl("lblTax1");
                    Label lblTotal = (Label)Gridview1.Rows[rowIndex].FindControl("lblTotal");
                    TextBox txtNextDueOn = (TextBox)Gridview1.Rows[rowIndex].FindControl("txtNextDueOn");

                    ddlCategory.SelectedValue = dt.Rows[i]["Column1"].ToString();
                    LoadArticle(ddlCategory, ddlArticle);
                    ddlArticle.SelectedValue = dt.Rows[i]["Column2"].ToString();
                    txtHSNCode.Text = dt.Rows[i]["Column3"].ToString();
                    txtDescription.Text = dt.Rows[i]["Column4"].ToString();
                    txtQty.Text = dt.Rows[i]["Column5"].ToString();
                    txtRate.Text = dt.Rows[i]["Column6"].ToString();
                    lblAmount.Text = dt.Rows[i]["Column7"].ToString();
                    txtTax.Text = dt.Rows[i]["Column8"].ToString();
                    lblTax1.Text = dt.Rows[i]["Column81"].ToString();
                    lblTotal.Text = dt.Rows[i]["Column9"].ToString();
                    txtNextDueOn.Text = dt.Rows[i]["Column91"].ToString();
                    rowIndex++;
                    if (ddlTaxType.SelectedIndex == 0)
                    {
                        txtTax.Text = "0.00";
                        txtTax.Enabled = false;
                    }
                    else
                    {
                        txtTax.Enabled = true;
                    }
                    if (ddlCategory.SelectedValue == "Product")
                    {
                        txtNextDueOn.Text = "";
                        txtNextDueOn.Enabled = false;
                    }
                }
            }
        }
    }

    protected void loadDetails(string PONo, string QtnNo)
    {
        Gridview1.DataSource = null;
        Gridview1.DataBind();
        Gridview1.Visible = false;
        Gridview2.Visible = true;
        Gridview2.DataSource = null;
        Gridview2.DataBind();
        sql = "select po.*, Caregory, am.Name itemName, Caregory, am.id itemid  from invPurchaeOrder po ";
        sql = sql + "inner join InvArticleEntry am on am.ID=po.ItemId and am.BranchCode=po.BranchCode ";
        sql = sql + "where PONo='" + PONo + "' and QtnNo='" + QtnNo + "' and po.BranchCode=" + Session["BranchCode"] + "";
        var dt = _oo.Fetchdata(sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            pnlcontrols.Visible = true;
            Gridview2.DataSource = dt;
            Gridview2.DataBind();
            double qty = 0; double Amount = 0; double Tax = 0; double Total = 0;
            for (int i = 0; i < Gridview2.Rows.Count; i++)
            {
                Label lblQty = (Label)Gridview2.Rows[i].FindControl("Qty");
                Label lblAmount = (Label)Gridview2.Rows[i].FindControl("Amount");
                Label lblTax = (Label)Gridview2.Rows[i].FindControl("Tax");
                Label lblTotal = (Label)Gridview2.Rows[i].FindControl("Total");
                Label itemCatId = (Label)Gridview2.Rows[i].FindControl("itemCatId");
                TextBox txtNextDueOn = (TextBox)Gridview2.Rows[i].FindControl("txtNextDueOn");
                if (itemCatId.Text == "Product")
                {
                    txtNextDueOn.Text = "";
                    txtNextDueOn.Visible = false;
                }
                qty = qty + double.Parse(lblQty.Text == "" ? "0" : lblQty.Text);
                Amount = Amount + double.Parse(lblAmount.Text == "" ? "0" : lblAmount.Text);
                Tax = Tax + double.Parse(lblTax.Text == "" ? "0" : lblTax.Text);
                Total = Total + double.Parse(lblTotal.Text == "" ? "0" : lblTotal.Text);


            }
            Label lblSQty = (Label)Gridview2.FooterRow.FindControl("lblSQty");
            Label lblSAmount = (Label)Gridview2.FooterRow.FindControl("lblSAmount");
            Label lblSTax = (Label)Gridview2.FooterRow.FindControl("lblSTax");
            Label lblsTotal = (Label)Gridview2.FooterRow.FindControl("lblsTotal");
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
            }
            if (_oo.ReturnTag(_sql, "TaxType") == "IGST")
            {
                lblIGST.Text = _oo.ReturnTag(_sql, "IGST");
                trCGST.Visible = false;
                trSGST.Visible = false;
                trIGST.Visible = true;
                trUGST.Visible = false;
                trNa.Visible = false;
            }
            if (_oo.ReturnTag(_sql, "TaxType") == "UGST")
            {
                lblUGST.Text = _oo.ReturnTag(_sql, "UGST");
                trCGST.Visible = false;
                trSGST.Visible = false;
                trIGST.Visible = false;
                trUGST.Visible = true;
                trNa.Visible = false;
            }
            if (_oo.ReturnTag(_sql, "TaxType") == "NA")
            {
                lblUGST.Text = _oo.ReturnTag(_sql, "UGST");
                trCGST.Visible = false;
                trSGST.Visible = false;
                trIGST.Visible = false;
                trUGST.Visible = false;
                trNa.Visible = true;
            }
            lblGrandTotal.Text = _oo.ReturnTag(_sql, "GrandTotal");
            txtPODescription.Text = _oo.ReturnTag(_sql, "PODescription");
            txtTerms.Text = _oo.ReturnTag(_sql, "Terms");
            txtSubject.Text = _oo.ReturnTag(_sql, "subject");
            txtInvoiceDate.Text = _oo.ReturnTag(_sql, "InvoiceDate");
        }
        else
        {
            pnlcontrols.Visible = false;
        }

    }
    protected void lbtnSearchBy_Click(object sender, EventArgs e)
    {
        if (rblInvoiceType.SelectedValue == "POWise")
        {
            txtPONos.Text = txtPONos.Text.Trim().ToUpper();
            string hdnPONO = hdnPO.Value;
            string ss = "select PONo from invInvoiceEntry where PONo='" + hdnPONO.Trim() + "' and BranchCode=" + Session["BranchCode"] + "";
            if (_oo.Duplicate(ss))
            {
                Campus camp = new Campus(); camp.msgbox(Page, dvSearch, "Invoice already submitted for this PO!", "A");
                Reset();
                return;
            }
            string ss2 = "select PONo from invPurchaeOrder where PONo='" + hdnPONO.Trim() + "' and BranchCode=" + Session["BranchCode"] + "";
            if (!_oo.Duplicate(ss2))
            {
                Campus camp = new Campus(); camp.msgbox(Page, dvSearch, "Invalid PO No.!", "A");
                Reset();
                return;
            }
            if (txtPONos.Text != string.Empty)
            {

                sql = sql + "select qt.*, qt.Id ids, '" + txtPONos.Text.Trim() + "' PONo, format(qt.QtnDate, 'dd-MMM-yyyy') QtnEnterDate, OrganizationName+' ('+VendorCode+')' VendorName from InvQuotation qt ";
                sql = sql + " inner join AccVendor v on v.id=qt.VendorId and v.BranchCode=qt.BranchCode  and qt.Status<>'Pending'";
                sql = sql + " where qt.BranchCode=" + Session["BranchCode"] + " ";
                sql = sql + " and qt.QtnNo in (select QtnNo from invPurchaeOrder where BranchCode=" + Session["BranchCode"] + " and Pono='" + hdnPONO.Trim() + "') ";
                var dt = _oo.Fetchdata(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    txtSubject.Enabled = false;
                    txtInvoiceDate.Enabled = false;
                    txtPODescription.Enabled = false;
                    ddlTaxType.Enabled = false;
                    txtTerms.Enabled = false;
                    gvBankBranchList.DataSource = dt;
                    gvBankBranchList.DataBind();
                    pnlcontrols.Visible = true;
                    string QtnNo = _oo.ReturnTag(sql, "QtnNo");
                    loadDetails(hdnPONO.Trim(), QtnNo);
                    gvBankBranchList2.DataSource = null;
                    gvBankBranchList2.DataBind();
                    gvBankBranchList2.Visible = false;
                    pnlcontrols.Visible = true;
                }
                else
                {
                    Campus camp = new Campus(); camp.msgbox(Page, dvSearch, "Invalid PO No.!", "A");

                    Reset();
                }
            }
            else
            {
                Campus camp = new Campus(); camp.msgbox(Page, dvSearch, "Invalid PO No.!", "A");
                Reset();
            }
        }
        else
        {
            txtSearchBy.Text = txtSearchBy.Text.Trim().ToUpper();
            string vendorId = hfVendorId.Value;
            sql = sql + "select *, Id ids, OrganizationName+' ('+VendorCode+')' VendorName from  AccVendor";
            sql = sql + " where BranchCode=" + Session["BranchCode"] + " ";
            sql = sql + " and id='" + vendorId.Trim() + "' ";
            var dt = _oo.Fetchdata(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                txtSubject.Enabled = true;
                txtInvoiceDate.Enabled = true;
                txtPODescription.Enabled = true;
                ddlTaxType.Enabled = true;
                txtTerms.Enabled = true;
                gvBankBranchList.DataSource = null;
                gvBankBranchList.DataBind();
                gvBankBranchList.Visible = false;
                gvBankBranchList2.DataSource = dt;
                gvBankBranchList2.DataBind();
                gvBankBranchList2.Visible = true;
                pnlcontrols.Visible = true;
                SetInitialRow();
            }
            else
            {
                Campus camp = new Campus(); camp.msgbox(Page, dvSearch, "Invalid Vendor.!", "A");
                Reset();
            }
        }
    }

    protected void txtSearchBy_TextChanged(object sender, EventArgs e)
    {
        txtSearchBy.Text = txtSearchBy.Text.Trim().ToUpper();
        string vendorId = hfVendorId.Value;
        if (vendorId != "")
        {
            sql = sql + "select *, Id ids, OrganizationName+' ('+VendorCode+')' VendorName from  AccVendor";
            sql = sql + " where BranchCode=" + Session["BranchCode"] + " ";
            sql = sql + " and id='" + vendorId.Trim() + "' ";
            var dt = _oo.Fetchdata(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                txtSubject.Enabled = true;
                txtInvoiceDate.Enabled = true;
                txtPODescription.Enabled = true;
                ddlTaxType.Enabled = true;
                txtTerms.Enabled = true;
                gvBankBranchList.DataSource = null;
                gvBankBranchList.DataBind();
                gvBankBranchList.Visible = false;
                gvBankBranchList2.DataSource = dt;
                gvBankBranchList2.DataBind();
                gvBankBranchList2.Visible = true;
                pnlcontrols.Visible = true;
                SetInitialRow();
            }
            else
            {
                Campus camp = new Campus(); camp.msgbox(Page, dvSearch, "Invalid Vendor.!", "A");
                Reset();
            }
        }
    }
    protected void txtPONos_TextChanged(object sender, EventArgs e)
    {
        txtPONos.Text = txtPONos.Text.Trim().ToUpper();
        string hdnPONO = hdnPO.Value;
        string ss = "select PONo from invInvoiceEntry where PONo='" + hdnPONO.Trim() + "' and BranchCode=" + Session["BranchCode"] + "";
        if (_oo.Duplicate(ss))
        {
            Campus camp = new Campus(); camp.msgbox(Page, dvSearch, "Invoice already submitted for this PO!", "A");
            Reset();
            return;
        }
        string ss2 = "select PONo from invPurchaeOrder where PONo='" + hdnPONO.Trim() + "' and BranchCode=" + Session["BranchCode"] + "";
        if (!_oo.Duplicate(ss2))
        {
            Campus camp = new Campus(); camp.msgbox(Page, dvSearch, "Invalid PO No.!", "A");
            Reset();
            return;
        }
        if (txtPONos.Text != string.Empty)
        {

            sql = sql + "select qt.*, qt.Id ids, '" + txtPONos.Text.Trim() + "' PONo, format(qt.QtnDate, 'dd-MMM-yyyy') QtnEnterDate, OrganizationName+' ('+VendorCode+')' VendorName from InvQuotation qt ";
            sql = sql + " inner join AccVendor v on v.id=qt.VendorId and v.BranchCode=qt.BranchCode  and qt.Status<>'Pending'";
            sql = sql + " where qt.BranchCode=" + Session["BranchCode"] + " ";
            sql = sql + " and qt.QtnNo in (select QtnNo from invPurchaeOrder where BranchCode=" + Session["BranchCode"] + " and Pono='" + hdnPONO.Trim() + "') ";
            var dt = _oo.Fetchdata(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                txtSubject.Enabled = false;
                txtInvoiceDate.Enabled = false;
                txtPODescription.Enabled = false;
                ddlTaxType.Enabled = false;
                txtTerms.Enabled = false;
                gvBankBranchList.DataSource = dt;
                gvBankBranchList.DataBind();
                pnlcontrols.Visible = true;
                string QtnNo = _oo.ReturnTag(sql, "QtnNo");
                loadDetails(hdnPONO.Trim(), QtnNo);
                gvBankBranchList2.DataSource = null;
                gvBankBranchList2.DataBind();
                gvBankBranchList2.Visible = false;
                pnlcontrols.Visible = true;
            }
            else
            {
                Campus camp = new Campus(); camp.msgbox(Page, dvSearch, "Invalid PO No.!", "A");

                Reset();
            }
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(Page, dvSearch, "Invalid PO No.!", "A");
            Reset();
        }
    }
    protected void ButtonRemove_Click(object sender, EventArgs e)
    {
        var lnk = (LinkButton)sender;
        var lblid = (Label)lnk.NamingContainer.FindControl("txtIndex");
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            dtCurrentTable.Rows.Remove(dtCurrentTable.Rows[int.Parse(lblid.Text)]);
            DataRow drCurrentRow = null;
            int idx = 0;
            for (int i = 0; i < Gridview1.Rows.Count; i++)
            {

                DropDownList ddlCategory = (DropDownList)Gridview1.Rows[rowIndex].FindControl("ddlCategory");
                DropDownList ddlArticle = (DropDownList)Gridview1.Rows[rowIndex].FindControl("ddlArticle");
                TextBox txtHSNCode = (TextBox)Gridview1.Rows[rowIndex].FindControl("txtHSNCode");
                TextBox txtDescription = (TextBox)Gridview1.Rows[rowIndex].FindControl("txtDescription");
                TextBox txtQty = (TextBox)Gridview1.Rows[rowIndex].FindControl("txtQty");
                TextBox txtRate = (TextBox)Gridview1.Rows[rowIndex].FindControl("txtRate");
                Label lblAmount = (Label)Gridview1.Rows[rowIndex].FindControl("lblAmount");
                TextBox txtTax = (TextBox)Gridview1.Rows[rowIndex].FindControl("txtTax");
                Label lblTax1 = (Label)Gridview1.Rows[rowIndex].FindControl("lblTax1");
                Label lblTotal = (Label)Gridview1.Rows[rowIndex].FindControl("lblTotal");
                TextBox txtNextDueOn = (TextBox)Gridview1.Rows[rowIndex].FindControl("txtNextDueOn");
                if (int.Parse(lblid.Text) != i)
                {
                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;
                    dtCurrentTable.Rows[idx]["Column1"] = ddlCategory.SelectedValue;
                    dtCurrentTable.Rows[idx]["Column2"] = ddlArticle.SelectedValue;
                    dtCurrentTable.Rows[idx]["Column3"] = txtHSNCode.Text;
                    dtCurrentTable.Rows[idx]["Column4"] = txtDescription.Text;
                    dtCurrentTable.Rows[idx]["Column5"] = txtQty.Text;
                    dtCurrentTable.Rows[idx]["Column6"] = txtRate.Text;
                    dtCurrentTable.Rows[idx]["Column7"] = lblAmount.Text;
                    dtCurrentTable.Rows[idx]["Column8"] = txtTax.Text;
                    dtCurrentTable.Rows[idx]["Column81"] = lblTax1.Text;
                    dtCurrentTable.Rows[idx]["Column9"] = lblTotal.Text;
                    dtCurrentTable.Rows[idx]["Column91"] = txtNextDueOn.Text;
                    rowIndex++;
                    idx++;
                }

            }

            Gridview1.DataSource = dtCurrentTable;
            Gridview1.DataBind();
            ViewState["CurrentTable"] = dtCurrentTable;
            if (dtCurrentTable.Rows.Count == 1)
            {
                LinkButton ButtonRemove = (LinkButton)Gridview1.Rows[0].FindControl("ButtonRemove");
                ButtonRemove.Visible = false;
            }
        }
        SetPreviousData();
        calculation();
    }

    protected void calculation()
    {
        double qty = 0; double Amount = 0; double Tax = 0; double Total = 0;
        for (int i = 0; i < Gridview1.Rows.Count; i++)
        {
            TextBox txtQty = (TextBox)Gridview1.Rows[i].FindControl("txtQty");
            Label lblAmount = (Label)Gridview1.Rows[i].FindControl("lblAmount");
            TextBox txtTax = (TextBox)Gridview1.Rows[i].FindControl("txtTax");
            Label lblTax1 = (Label)Gridview1.Rows[i].FindControl("lblTax1");
            Label lblTotal = (Label)Gridview1.Rows[i].FindControl("lblTotal");

            qty = qty + double.Parse(txtQty.Text == "" ? "0" : txtQty.Text);
            Amount = Amount + double.Parse(lblAmount.Text == "" ? "0" : lblAmount.Text);
            Tax = Tax + double.Parse(lblTax1.Text == "" ? "0" : lblTax1.Text);
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

        lblGTotal.Text = double.Parse(lblSAmount.Text == "" ? "0" : lblSAmount.Text).ToString();
        double tax = 0;
        tax = double.Parse(lblSTax.Text == "" ? "0" : lblSTax.Text);
        if (ddlTaxType.SelectedValue == "GST")
        {
            trCGST.Visible = true;
            trSGST.Visible = true;
            trIGST.Visible = false;
            trUGST.Visible = false;
            lblCGST.Text = (tax / 2).ToString("0.00");
            lblSGST.Text = (tax / 2).ToString("0.00");
            lblIGST.Text = "0.00";
            lblUGST.Text = "0.00";
            trNa.Visible = false;
            lblNa.Text = "0.00";
        }

        if (ddlTaxType.SelectedValue == "IGST")
        {
            trCGST.Visible = false;
            trSGST.Visible = false;
            trIGST.Visible = true;
            trUGST.Visible = false;
            lblIGST.Text = (tax).ToString("0.00");
            lblCGST.Text = "0.00";
            lblSGST.Text = "0.00";
            lblUGST.Text = "0.00";
            trNa.Visible = false;
            lblNa.Text = "0.00";
        }
        if (ddlTaxType.SelectedValue == "UGST")
        {
            trCGST.Visible = false;
            trSGST.Visible = false;
            trIGST.Visible = false;
            trUGST.Visible = true;
            lblUGST.Text = (tax).ToString("0.00");
            lblCGST.Text = "0.00";
            lblSGST.Text = "0.00";
            lblIGST.Text = "0.00";
            trNa.Visible = false;
            lblNa.Text = "0.00";
        }
        if (ddlTaxType.SelectedValue == "NA")
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
        lblGrandTotal.Text = ((tax) + double.Parse(lblGTotal.Text == "" ? "0" : lblGTotal.Text)).ToString("0.00");
    }

    protected void txtQty_TextChanged(object sender, EventArgs e)
    {
        var lnk = (TextBox)sender;
        var txtQty = (TextBox)lnk.NamingContainer.FindControl("txtQty");
        var txtRate = (TextBox)lnk.NamingContainer.FindControl("txtRate");
        var lblAmount = (Label)lnk.NamingContainer.FindControl("lblAmount");
        var txtTax = (TextBox)lnk.NamingContainer.FindControl("txtTax");
        var lblTax1 = (Label)lnk.NamingContainer.FindControl("lblTax1");
        var lblTotal = (Label)lnk.NamingContainer.FindControl("lblTotal");
        if (double.Parse(txtQty.Text == "" ? "0" : txtQty.Text) == 0 || double.Parse(txtRate.Text == "" ? "0" : txtRate.Text) == 0)
        {
            lblAmount.Text = "";
            lblTotal.Text = "";
        }
        else
        {
            lblAmount.Text = (double.Parse(txtQty.Text == "" ? "0" : txtQty.Text) * double.Parse(txtRate.Text == "" ? "0" : txtRate.Text)).ToString();
            txtTax.Text = "";
            lblTax1.Text = "";
            lblTotal.Text = lblAmount.Text;
        }
        calculation();
        txtRate.Focus();
    }

    protected void txtRate_TextChanged(object sender, EventArgs e)
    {
        var lnk = (TextBox)sender;
        var txtQty = (TextBox)lnk.NamingContainer.FindControl("txtQty");
        var txtRate = (TextBox)lnk.NamingContainer.FindControl("txtRate");
        var lblAmount = (Label)lnk.NamingContainer.FindControl("lblAmount");
        var txtTax = (TextBox)lnk.NamingContainer.FindControl("txtTax");
        var lblTax1 = (Label)lnk.NamingContainer.FindControl("lblTax1");
        var lblTotal = (Label)lnk.NamingContainer.FindControl("lblTotal");
        if (double.Parse(txtQty.Text == "" ? "0" : txtQty.Text) == 0 || double.Parse(txtRate.Text == "" ? "0" : txtRate.Text) == 0)
        {
            lblAmount.Text = "";
            lblTotal.Text = "";
        }
        else
        {
            lblAmount.Text = (double.Parse(txtQty.Text == "" ? "0" : txtQty.Text) * double.Parse(txtRate.Text == "" ? "0" : txtRate.Text)).ToString("0.00");
            txtTax.Text = "";
            lblTax1.Text = "";
            lblTotal.Text = lblAmount.Text;
        }
        calculation();
        txtTax.Focus();
    }

    protected void txtTax_TextChanged(object sender, EventArgs e)
    {
        var lnk = (TextBox)sender;
        var txtQty = (TextBox)lnk.NamingContainer.FindControl("txtQty");
        var txtRate = (TextBox)lnk.NamingContainer.FindControl("txtRate");
        var lblAmount = (Label)lnk.NamingContainer.FindControl("lblAmount");
        var txtTax = (TextBox)lnk.NamingContainer.FindControl("txtTax");
        var lblTax1 = (Label)lnk.NamingContainer.FindControl("lblTax1");
        var lblTotal = (Label)lnk.NamingContainer.FindControl("lblTotal");
        if (double.Parse(txtTax.Text == "" ? "0" : txtTax.Text) > 40)
        {
            txtTax.Text = "";
        }
        if (double.Parse(lblAmount.Text == "" ? "0" : lblAmount.Text) == 0)
        {
            lblAmount.Text = "";
            lblTotal.Text = "";
        }
        else
        {
            double tax = (double.Parse(txtTax.Text == "" ? "0" : txtTax.Text) * double.Parse(lblAmount.Text == "" ? "0" : lblAmount.Text)) / 100;
            lblTax1.Text = tax.ToString("0.00");
            lblTotal.Text = (double.Parse(lblAmount.Text == "" ? "0" : lblAmount.Text) + tax).ToString("0.00");
        }
        calculation();
    }

    protected void ddlTaxType_SelectedIndexChanged(object sender, EventArgs e)
    {
        double tax = 0;
        Label lblSTax = (Label)Gridview1.FooterRow.FindControl("lblSTax");
        Label lblSAmount = (Label)Gridview1.FooterRow.FindControl("lblSAmount");
        double sTotal = 0;
        for (int i = 0; i < Gridview1.Rows.Count; i++)
        {
            TextBox txtTax = (TextBox)Gridview1.Rows[i].FindControl("txtTax");
            Label lblTax1 = (Label)Gridview1.Rows[i].FindControl("lblTax1");
            Label lblTotal = (Label)Gridview1.Rows[i].FindControl("lblTotal");
            Label lblAmount = (Label)Gridview1.Rows[i].FindControl("lblAmount");
            if (ddlTaxType.SelectedIndex == 0)
            {
                txtTax.Text = "0.00";
                lblTax1.Text = "0.00";
                txtTax.Enabled = false;
            }
            else
            {
                txtTax.Text = "0.00";
                lblTax1.Text = "0.00";
                txtTax.Enabled = true;
            }
            lblTotal.Text = lblAmount.Text;
            sTotal = sTotal + double.Parse(lblTotal.Text == "" ? "0" : lblTotal.Text);
        }
        Label lblsTotal = (Label)Gridview1.FooterRow.FindControl("lblsTotal");
        lblsTotal.Text = sTotal.ToString("0.00");
        lblSTax.Text = "0.00";
        lblGTotal.Text = double.Parse(lblSAmount.Text == "" ? "0" : lblSAmount.Text).ToString("0.00");
        tax = double.Parse(lblSTax.Text == "" ? "0" : lblSTax.Text);
        if (ddlTaxType.SelectedValue == "GST")
        {
            trCGST.Visible = true;
            trSGST.Visible = true;
            trIGST.Visible = false;
            trUGST.Visible = false;
            lblCGST.Text = (tax / 2).ToString("0.00");
            lblSGST.Text = (tax / 2).ToString("0.00");
            lblIGST.Text = "0.00";
            lblUGST.Text = "0.00";
            trNa.Visible = false;
            lblNa.Text = "0.00";
        }

        if (ddlTaxType.SelectedValue == "IGST")
        {
            trCGST.Visible = false;
            trSGST.Visible = false;
            trIGST.Visible = true;
            trUGST.Visible = false;
            lblIGST.Text = (tax).ToString("0.00");
            lblCGST.Text = "0.00";
            lblSGST.Text = "0.00";
            lblUGST.Text = "0.00";
            trNa.Visible = false;
            lblNa.Text = "0.00";
        }
        if (ddlTaxType.SelectedValue == "UGST")
        {
            trCGST.Visible = false;
            trSGST.Visible = false;
            trIGST.Visible = false;
            trUGST.Visible = true;
            lblUGST.Text = (tax).ToString("0.00");
            lblCGST.Text = "0.00";
            lblSGST.Text = "0.00";
            lblIGST.Text = "0.00";
            trNa.Visible = false;
            lblNa.Text = "0.00";
        }
        if (ddlTaxType.SelectedValue == "NA")
        {
            trCGST.Visible = false;
            trSGST.Visible = false;
            trIGST.Visible = false;
            trUGST.Visible = false;
            trNa.Visible = true;
            lblUGST.Text = "0.00";
            lblCGST.Text = "0.00";
            lblSGST.Text = "0.00";
            lblIGST.Text = "0.00";
            lblNa.Text = "0.00";
        }
        lblGrandTotal.Text = ((tax) + double.Parse(lblGTotal.Text == "" ? "0" : lblGTotal.Text)).ToString("0.00");
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (rblInvoiceType.SelectedValue == "POWise")
        {
            var PONo = txtPONos.Text; int sts = 0;
            if (txtPONos.Text == string.Empty || PONo == String.Empty)
            {
                Campus camp = new Campus(); camp.msgbox(Page, dvSearch, "Invalid Quotation No.!", "A");
                Reset();
                return;
            }
        }
        if (rblInvoiceType.SelectedValue != "POWise")
        {
            var venId = hfVendorId.Value; int sts = 0;
            if (txtSearchBy.Text == string.Empty || venId == String.Empty)
            {
                Campus camp = new Campus(); camp.msgbox(Page, dvSearch, "Invalid QVendor!", "A");
                Reset();
                return;
            }
        }
        using (SqlCommand cmd = new SqlCommand())
        {
            var fileName = string.Empty;
            var filePath = "";

            var base64Std = hidFile.Value;
            if (base64Std != string.Empty)
            {
                filePath = @"~/Uploads/VendorDoc/";
                if (!Directory.Exists(Server.MapPath(filePath)))
                {
                    Directory.CreateDirectory(Server.MapPath(filePath));
                }
                var rn = new Random();
                fileName = "INV_" + rn.Next(123456789, 987654321) + DateTime.Now.ToString("ddmmmyyyyhhmmsstt") + "_" + hidFileExt.Value.ToString();
                filePath = string.Format("~/Uploads/VendorDoc/{0}", fileName);

                using (FileStream fs = new FileStream(Server.MapPath(filePath), FileMode.Create))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs))
                    {
                        var data = Convert.FromBase64String(base64Std);
                        bw.Write(data);
                        bw.Close();
                    }
                }
                hidFile.Value = "";
                hidFileExt.Value = "";
            }

            string InvoiceNo = "";

            string sqls = "select 'INV'+convert(varchar(5),count(*)+1) InvoiceNo from(select distinct InvoiceNo from invInvoiceEntry where branchCode=" + Session["BranchCode"] + ")t1";
            InvoiceNo = _oo.ReturnTag(sqls, "InvoiceNo");
            if (InvoiceNo == "" || InvoiceNo == "0")
            {
                Campus camp = new Campus(); camp.msgbox(Page, dvSearch, "PO No. not genrated!", "A");
            }
            else
            {
                var PONo = hdnPO.Value.Trim();
                int sts = 0;
                for (int i = 0; i < (rblInvoiceType.SelectedValue == "POWise" ? Gridview2.Rows.Count : Gridview1.Rows.Count); i++)
                {
                    cmd.CommandText = "invInvoiceEntryProc";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = _con;
                    cmd.Parameters.AddWithValue("@InvoiceNo", InvoiceNo);

                    if (rblInvoiceType.SelectedValue == "POWise")
                    {
                        var lblVendorId = (Label)gvBankBranchList.Rows[0].FindControl("lblVendorId");
                        var ddlCategory = (Label)Gridview2.Rows[i].FindControl("itemCatId");
                        var ddlArticle = (Label)Gridview2.Rows[i].FindControl("itemId");
                        var txtHSNCode = (Label)Gridview2.Rows[i].FindControl("HSNCode");
                        var txtDescription = (Label)Gridview2.Rows[i].FindControl("Description");
                        var txtQty = (Label)Gridview2.Rows[i].FindControl("Qty");
                        var txtRate = (Label)Gridview2.Rows[i].FindControl("Rate");
                        var lblAmount = (Label)Gridview2.Rows[i].FindControl("Amount");
                        var txtTaxPercent = (Label)Gridview2.Rows[i].FindControl("TaxPercent");
                        var lblTax1 = (Label)Gridview2.Rows[i].FindControl("Tax");
                        var lblTotal = (Label)Gridview2.Rows[i].FindControl("Total");
                        var txtNextDueOn = (TextBox)Gridview2.Rows[i].FindControl("txtNextDueOn");
                        cmd.Parameters.AddWithValue("@PONo", PONo);
                        cmd.Parameters.AddWithValue("@VendorId", lblVendorId.Text.Trim());
                        cmd.Parameters.AddWithValue("@Category", ddlCategory.Text);
                        cmd.Parameters.AddWithValue("@ItemId", ddlArticle.Text.Trim());
                        cmd.Parameters.AddWithValue("@HSNCode", txtHSNCode.Text.Trim());
                        cmd.Parameters.AddWithValue("@Description", txtDescription.Text.Trim());
                        cmd.Parameters.AddWithValue("@Qty", txtQty.Text.Trim());
                        cmd.Parameters.AddWithValue("@Rate", txtRate.Text.Trim());
                        cmd.Parameters.AddWithValue("@Amount", lblAmount.Text.Trim());
                        cmd.Parameters.AddWithValue("@TaxPercent", (txtTaxPercent.Text.Trim() == "" ? "0.00" : txtTaxPercent.Text.Trim()));
                        cmd.Parameters.AddWithValue("@Tax", (lblTax1.Text.Trim() == "" ? "0.00" : lblTax1.Text.Trim()));
                        cmd.Parameters.AddWithValue("@Total", lblTotal.Text.Trim()==""? "0.00" : lblTotal.Text.Trim());
                        if (ddlCategory.Text == "Product")
                        {
                            cmd.Parameters.AddWithValue("@NextDueOn", txtNextDueOn.Text.Trim());
                        }
                    }
                    else
                    {
                        var lblVendorId = (Label)gvBankBranchList2.Rows[0].FindControl("lblVendorId");
                        var ddlCategory = (DropDownList)Gridview1.Rows[i].FindControl("ddlCategory");
                        var ddlArticle = (DropDownList)Gridview1.Rows[i].FindControl("ddlArticle");
                        var txtHSNCode = (TextBox)Gridview1.Rows[i].FindControl("txtHSNCode");
                        var txtDescription = (TextBox)Gridview1.Rows[i].FindControl("txtDescription");
                        var txtQty = (TextBox)Gridview1.Rows[i].FindControl("txtQty");
                        var txtRate = (TextBox)Gridview1.Rows[i].FindControl("txtRate");
                        var lblAmount = (Label)Gridview1.Rows[i].FindControl("lblAmount");
                        var txtTaxPercent = (TextBox)Gridview1.Rows[i].FindControl("txtTax");
                        var lblTax1 = (Label)Gridview1.Rows[i].FindControl("lblTax1");
                        var lblTotal = (Label)Gridview1.Rows[i].FindControl("lblTotal");
                        var txtNextDueOn = (TextBox)Gridview1.Rows[i].FindControl("txtNextDueOn");
                        cmd.Parameters.AddWithValue("@VendorId", lblVendorId.Text.Trim());
                        cmd.Parameters.AddWithValue("@Category", ddlCategory.SelectedValue);
                        cmd.Parameters.AddWithValue("@ItemId", ddlArticle.SelectedValue.Trim());
                        cmd.Parameters.AddWithValue("@HSNCode", txtHSNCode.Text.Trim());
                        cmd.Parameters.AddWithValue("@Description", txtDescription.Text.Trim());
                        cmd.Parameters.AddWithValue("@Qty", txtQty.Text.Trim());
                        cmd.Parameters.AddWithValue("@Rate", txtRate.Text.Trim());
                        cmd.Parameters.AddWithValue("@Amount", lblAmount.Text.Trim());
                        cmd.Parameters.AddWithValue("@TaxPercent", (txtTaxPercent.Text.Trim() == "" ? "0.00" : txtTaxPercent.Text.Trim()));
                        cmd.Parameters.AddWithValue("@Tax", (lblTax1.Text.Trim() == "" ? "0.00" : lblTax1.Text.Trim()));
                        cmd.Parameters.AddWithValue("@Total", lblTotal.Text.Trim() == "" ? "0.00" : lblTotal.Text.Trim());
                        if (ddlCategory.SelectedValue == "Product")
                        {
                            cmd.Parameters.AddWithValue("@NextDueOn", txtNextDueOn.Text.Trim());
                        }
                    }

                    cmd.Parameters.AddWithValue("@TotalAmount", lblGTotal.Text.Trim());
                    if (ddlTaxType.SelectedValue == "GST")
                    {
                        cmd.Parameters.AddWithValue("@CGST", lblCGST.Text.Trim());
                        cmd.Parameters.AddWithValue("@SGST", lblSGST.Text.Trim());
                    }
                    if (ddlTaxType.SelectedValue == "IGST")
                    {
                        cmd.Parameters.AddWithValue("@IGST", lblIGST.Text.Trim());
                    }
                    if (ddlTaxType.SelectedValue == "UGST")
                    {
                        cmd.Parameters.AddWithValue("@UGST", lblUGST.Text.Trim());
                    }
                    cmd.Parameters.AddWithValue("@GrandTotal", lblGrandTotal.Text.Trim());
                    cmd.Parameters.AddWithValue("@TaxType", ddlTaxType.SelectedValue.Trim());
                    cmd.Parameters.AddWithValue("@Subject", txtSubject.Text.Trim());
                    cmd.Parameters.AddWithValue("@InvoiceDate", txtInvoiceDate.Text.Trim());
                    cmd.Parameters.AddWithValue("@PODescription", txtPODescription.Text.Trim());
                    cmd.Parameters.AddWithValue("@Terms", txtTerms.Text.Trim());
                    cmd.Parameters.AddWithValue("@FileName", fileName.Trim());
                    cmd.Parameters.AddWithValue("@FilePath", filePath.Trim());
                    cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                    cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                    cmd.Parameters.AddWithValue("@Action", "insert");
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
                    Campus camp = new Campus(); camp.msgbox(Page, dvSearch, "Submitted successfully.", "S");
                    Reset();
                }
            }
        }
    }

    private void Reset()
    {
        hfVendorId.Value = string.Empty;
        txtSearchBy.Text = string.Empty;
        txtPONos.Text = string.Empty;
        txtSubject.Text = string.Empty;
        ddlTaxType.SelectedIndex = 0;
        txtPODescription.Text = string.Empty;
        txtTerms.Text = string.Empty;
        lblCGST.Text = "0.00";
        lblSGST.Text = "0.00";
        lblIGST.Text = "0.00";
        lblUGST.Text = "0.00";
        lblNa.Text = "0.00";
        lblGTotal.Text = "0.00";
        lblGrandTotal.Text = "0.00";
        pnlcontrols.Visible = false;
        gvBankBranchList.DataSource = null;
        gvBankBranchList.DataBind();
        SetInitialRow();
    }






}