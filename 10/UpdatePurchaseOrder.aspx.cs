using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class UpdatePurchaseOrder : System.Web.UI.Page
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
            //SetInitialRow();
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
        LoadArticle(ddlCategory, ddlArticle);
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
    protected void loadDetails(string PONo, string QtnNo)
    {
        DataTable dtv = new DataTable();
        dtv.Rows.Clear();
        Gridview1.DataSource = null;
        Gridview1.DataBind();
        sql = "select po.*, Caregory, am.Name itemName  from invPurchaeOrder po ";
        sql = sql + "inner join InvArticleEntry am on am.ID=po.ItemId and am.BranchCode=po.BranchCode ";
        sql = sql + "where PONo='" + PONo + "' and QtnNo='" + QtnNo + "' and po.BranchCode=" + Session["BranchCode"] + "";
        var dt = _oo.Fetchdata(sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            
            DataRow dr = null;
            dtv.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dtv.Columns.Add(new DataColumn("Column1", typeof(string)));
            dtv.Columns.Add(new DataColumn("Column2", typeof(string)));
            dtv.Columns.Add(new DataColumn("Column3", typeof(string)));
            dtv.Columns.Add(new DataColumn("Column4", typeof(string)));
            dtv.Columns.Add(new DataColumn("Column5", typeof(string)));
            dtv.Columns.Add(new DataColumn("Column6", typeof(string)));
            dtv.Columns.Add(new DataColumn("Column7", typeof(string)));
            dtv.Columns.Add(new DataColumn("Column8", typeof(string)));
            dtv.Columns.Add(new DataColumn("Column81", typeof(string)));
            dtv.Columns.Add(new DataColumn("Column9", typeof(string)));
            
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dr = dtv.NewRow();
                dr["RowNumber"] = 1;
                dr["Column1"] = dt.Rows[i]["Caregory"].ToString();
                dr["Column2"] = dt.Rows[i]["ItemId"].ToString();
                dr["Column3"] = dt.Rows[i]["HSNCode"].ToString();
                dr["Column4"] = dt.Rows[i]["Description"].ToString();
                dr["Column5"] = dt.Rows[i]["Qty"].ToString();
                dr["Column6"] = dt.Rows[i]["Rate"].ToString();
                dr["Column7"] = dt.Rows[i]["Amount"].ToString();
                dr["Column8"] = dt.Rows[i]["TaxPercent"].ToString();
                dr["Column81"] = dt.Rows[i]["Tax"].ToString();
                dr["Column9"] = dt.Rows[i]["Total"].ToString();
                dtv.Rows.Add(dr);
            }
            ViewState["CurrentTable"] = dtv;

            pnlcontrols.Visible = true;
            Gridview1.DataSource = dtv;
            Gridview1.DataBind();
            double qty = 0; double Amount = 0; double Tax = 0; double Total = 0;
            for (int i = 0; i < dtv.Rows.Count; i++)
            {
                DropDownList ddlCategory = (DropDownList)Gridview1.Rows[i].FindControl("ddlCategory");
                ddlCategory.SelectedValue = dtv.Rows[i]["Column1"].ToString();
                DropDownList ddlArticle = (DropDownList)Gridview1.Rows[i].FindControl("ddlArticle");
                LoadArticle(ddlCategory, ddlArticle);
                ddlArticle.SelectedValue = dtv.Rows[i]["Column2"].ToString();
                TextBox txtHSNCode = (TextBox)Gridview1.Rows[i].FindControl("txtHSNCode");
                txtHSNCode.Text = dtv.Rows[i]["Column3"].ToString();
                TextBox txtDescription = (TextBox)Gridview1.Rows[i].FindControl("txtDescription");
                txtDescription.Text = dtv.Rows[i]["Column4"].ToString();
                TextBox txtQty = (TextBox)Gridview1.Rows[i].FindControl("txtQty");
                txtQty.Text= dtv.Rows[i]["Column5"].ToString();
                TextBox txtRate = (TextBox)Gridview1.Rows[i].FindControl("txtRate");
                txtRate.Text = dtv.Rows[i]["Column6"].ToString();
                Label lblAmount = (Label)Gridview1.Rows[i].FindControl("lblAmount");
                lblAmount.Text = dtv.Rows[i]["Column7"].ToString();
                TextBox txtTax = (TextBox)Gridview1.Rows[i].FindControl("txtTax");
                txtTax.Text = dtv.Rows[i]["Column8"].ToString();
                Label lblTax1 = (Label)Gridview1.Rows[i].FindControl("lblTax1");
                lblTax1.Text = dtv.Rows[i]["Column81"].ToString();
                Label lblTotal = (Label)Gridview1.Rows[i].FindControl("lblTotal");
                lblTotal.Text = dtv.Rows[i]["Column9"].ToString();

                qty = qty + double.Parse(txtQty.Text == "" ? "0" : txtQty.Text);
                Amount = Amount + double.Parse(lblAmount.Text == "" ? "0" : lblAmount.Text);
                Tax = Tax + double.Parse(lblTax1.Text == "" ? "0" : lblTax1.Text);
                Total = Total + double.Parse(lblTotal.Text == "" ? "0" : lblTotal.Text);
                LinkButton ButtonRemove = (LinkButton)Gridview1.Rows[i].FindControl("ButtonRemove");
                if (dtv.Rows.Count>1)
                {
                    if (i == (dtv.Rows.Count - 1))
                    {
                        ButtonRemove.Visible = true;
                    }
                    else
                    {
                        ButtonRemove.Visible = false;
                    }
                }
                else
                {
                    ButtonRemove.Visible = false;
                }
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
            ddlTaxType.SelectedValue = _oo.ReturnTag(_sql, "TaxType");
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
            lblGrandTotal.Text = _oo.ReturnTag(_sql, "GrandTotal");
            txtPODescription.Text = _oo.ReturnTag(_sql, "PODescription");
            txtTerms.Text = _oo.ReturnTag(_sql, "Terms");
            txtSubject.Text = _oo.ReturnTag(_sql, "subject");
            txtOrderDate.Text = DateTime.Parse(_oo.ReturnTag(_sql, "OrderDate")).ToString("dd-MMM-yyyy");
        }
        else
        {
            pnlcontrols.Visible = false;
        }

    }
    protected void txtPONo_TextChanged(object sender, EventArgs e)
    {
        var PONo = hfPONo.Value;
        string ss = "select QtnNo from invInvoiceEntry where PONo='"+ PONo + "' and BranchCode=" + Session["BranchCode"] + "";
        if (_oo.Duplicate(ss))
        {
            Reset();
            Campus camp = new Campus(); camp.msgbox(Page, dvSearch, "Invalid PO No.!", "A");
            return;
        }
        if (txtPONo.Text != string.Empty && PONo != String.Empty)
        {
            txtPONo.Text = txtPONo.Text.Trim().ToUpper();
            sql = "select qt.*, qt.Id ids, format(qt.QtnDate, 'dd-MMM-yyyy') QtnEnterDate, OrganizationName+' ('+VendorCode+')' VendorName, qt.QtnNo from InvQuotation qt ";
            sql = sql + " inner join AccVendor v on v.id=qt.VendorId and v.BranchCode=qt.BranchCode  and qt.Status='Approve'";
            sql = sql + " where qt.BranchCode=" + Session["BranchCode"] + " ";
            sql = sql + " and qt.QtnNo in (select QtnNo from invPurchaeOrder where PONo='" + PONo + "' and BranchCode=" + Session["BranchCode"] + ") ";
            var dts = _oo.Fetchdata(sql);
            if (dts != null && dts.Rows.Count > 0)
            {
                gvBankBranchList.DataSource = dts;
                gvBankBranchList.DataBind();
                pnlcontrols.Visible = true;
                //SetInitialRow();

                loadDetails(PONo, _oo.ReturnTag(sql, "QtnNo"));
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
    protected void lbtnSearchBy_Click(object sender, EventArgs e)
    {
        var PONo = hfPONo.Value;
        string ss = "select QtnNo from invInvoiceEntry where PONo='" + PONo + "' and BranchCode=" + Session["BranchCode"] + "";
        if (_oo.Duplicate(ss))
        {
            Reset();
            Campus camp = new Campus(); camp.msgbox(Page, dvSearch, "Invalid PO No.!", "A");
            return;
        }
        if (txtPONo.Text != string.Empty && PONo != String.Empty)
        {
            txtPONo.Text = txtPONo.Text.Trim().ToUpper();
            sql = "select qt.*, qt.Id ids, format(qt.QtnDate, 'dd-MMM-yyyy') QtnEnterDate, OrganizationName+' ('+VendorCode+')' VendorName, qt.QtnNo from InvQuotation qt ";
            sql = sql + " inner join AccVendor v on v.id=qt.VendorId and v.BranchCode=qt.BranchCode  and qt.Status='Approve'";
            sql = sql + " where qt.BranchCode=" + Session["BranchCode"] + " ";
            sql = sql + " and qt.QtnNo in (select QtnNo from invPurchaeOrder where PONo='" + PONo + "' and BranchCode=" + Session["BranchCode"] + ") ";
            var dts = _oo.Fetchdata(sql);
            if (dts != null && dts.Rows.Count > 0)
            {
                gvBankBranchList.DataSource = dts;
                gvBankBranchList.DataBind();
                pnlcontrols.Visible = true;
                //SetInitialRow();
                loadDetails(PONo, _oo.ReturnTag(sql, "QtnNo"));
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
        dt.Rows.Add(dr);
        ViewState["CurrentTable"] = dt;
        //Gridview1.DataSource = dt;
        //Gridview1.DataBind();
        //if (Gridview1.Rows.Count == 1)
        //{
        //    TextBox txtTax = (TextBox)Gridview1.Rows[0].FindControl("txtTax");
        //    Label lblTax1 = (Label)Gridview1.Rows[0].FindControl("lblTax1");
        //    if (ddlTaxType.SelectedIndex == 0)
        //    {
        //        txtTax.Text = "0.00";
        //        lblTax1.Text = "0.00";
        //        txtTax.Enabled = false;
        //    }
        //    else
        //    {
        //        txtTax.Enabled = true;
        //    }
        //    LinkButton ButtonRemove = (LinkButton)Gridview1.Rows[0].FindControl("ButtonRemove");
        //    ButtonRemove.Visible = false;
        //}
        //DropDownList ddlArticle = (DropDownList)Gridview1.Rows[0].FindControl("ddlArticle");
        //ddlArticle.Items.Insert(0, new ListItem("<--Select-->", ""));
    }

    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        AddNewRowToGrid();
    }
    private void AddNewRowToGrid()
    {
        //SetInitialRow();
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 0; i < Gridview1.Rows.Count; i++)
                {
                    //extract the TextBox values  
                    DropDownList ddlCategory = (DropDownList)Gridview1.Rows[i].FindControl("ddlCategory");
                    DropDownList ddlArticle = (DropDownList)Gridview1.Rows[i].FindControl("ddlArticle");
                    TextBox txtHSNCode = (TextBox)Gridview1.Rows[i].FindControl("txtHSNCode");
                    TextBox txtDescription = (TextBox)Gridview1.Rows[i].FindControl("txtDescription");
                    TextBox txtQty = (TextBox)Gridview1.Rows[i].FindControl("txtQty");
                    TextBox txtRate = (TextBox)Gridview1.Rows[i].FindControl("txtRate");
                    Label lblAmount = (Label)Gridview1.Rows[i].FindControl("lblAmount");
                    TextBox txtTax = (TextBox)Gridview1.Rows[i].FindControl("txtTax");
                    Label lblTax1 = (Label)Gridview1.Rows[i].FindControl("lblTax1");
                    Label lblTotal = (Label)Gridview1.Rows[i].FindControl("lblTotal");
                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;
                    dtCurrentTable.Rows[i]["Column1"] = ddlCategory.SelectedValue;
                    dtCurrentTable.Rows[i]["Column2"] = ddlArticle.SelectedValue;
                    dtCurrentTable.Rows[i]["Column3"] = txtHSNCode.Text;
                    dtCurrentTable.Rows[i]["Column4"] = txtDescription.Text;
                    dtCurrentTable.Rows[i]["Column5"] = txtQty.Text;
                    dtCurrentTable.Rows[i]["Column6"] = txtRate.Text;
                    dtCurrentTable.Rows[i]["Column7"] = lblAmount.Text;
                    dtCurrentTable.Rows[i]["Column8"] = txtTax.Text;
                    dtCurrentTable.Rows[i]["Column81"] = lblTax1.Text;
                    dtCurrentTable.Rows[i]["Column9"] = lblTotal.Text;
                    rowIndex++;
                    
                }
                dtCurrentTable.Rows.Add(drCurrentRow);
                ViewState["CurrentTable"] = dtCurrentTable;
                Gridview1.DataSource = dtCurrentTable;
                Gridview1.DataBind();
                for (int i = 0; i < Gridview1.Rows.Count; i++)
                {
                    LinkButton ButtonRemove = (LinkButton)Gridview1.Rows[i].FindControl("ButtonRemove");
                    if (Gridview1.Rows.Count > 1)
                    {
                        if (i == (Gridview1.Rows.Count - 1))
                        {
                            ButtonRemove.Visible = true;
                        }
                        else
                        {
                            ButtonRemove.Visible = false;
                        }
                    }
                    else
                    {
                        ButtonRemove.Visible = false;
                    }
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
                    rowIndex++;
                }
            }
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
            for (int i = 0; i < Gridview1.Rows.Count; i++)
            {
                LinkButton ButtonRemove = (LinkButton)Gridview1.Rows[i].FindControl("ButtonRemove");
                if (Gridview1.Rows.Count > 1)
                {
                    if (i == (Gridview1.Rows.Count - 1))
                    {
                        ButtonRemove.Visible = true;
                    }
                    else
                    {
                        ButtonRemove.Visible = false;
                    }
                }
                else
                {
                    ButtonRemove.Visible = false;
                }
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
            sTotal= sTotal+ double.Parse(lblTotal.Text == "" ? "0" : lblTotal.Text);
        }
        Label lblsTotal = (Label)Gridview1.FooterRow.FindControl("lblsTotal");
        lblsTotal.Text= sTotal.ToString("0.00");
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
        string poDate = "";
        if (txtOrderDate.Text.Trim()!="")
        {
            try
            {
                poDate=DateTime.Parse(txtOrderDate.Text.Trim()).ToString("dd-MMM-yyyy");
            }
            catch (Exception)
            {
                poDate = "";
            }  
        }
        if (poDate=="")
        {
            Campus camp = new Campus(); camp.msgbox(Page, dvSearch, "Invalid purchase order date!", "A");
            return;
        }
        var PONo = hfPONo.Value; int sts = 0;
        if (txtPONo.Text != string.Empty && PONo != String.Empty)
        {
            int x = 0;
            var lblVendorId = (Label)gvBankBranchList.Rows[0].FindControl("lblVendorId");
            string QtnNo = "";
            string sqls = "select distinct QtnNo from invPurchaeOrder where branchCode=" + Session["BranchCode"] + " and pono='" + PONo + "'";
            QtnNo = _oo.ReturnTag(sqls, "QtnNo");
            _sql = "Delete from invPurchaeOrder where PONo='" + PONo + "' and branchCode=" + Session["BranchCode"] + "";

            using (SqlCommand cmds = new SqlCommand())
            {
                cmds.CommandText = _sql;
                cmds.CommandType = CommandType.Text;
                cmds.Connection = _con;
                try
                {
                    _con.Open();
                    x=cmds.ExecuteNonQuery();
                    _con.Close();
                }
                catch (Exception ex)
                {
                }
            }
            if (x > 0)
            {

                using (SqlCommand cmd = new SqlCommand())
                {
                   
                    if (PONo == "" || PONo == "0")
                    {
                        Campus camp = new Campus(); camp.msgbox(Page, dvSearch, "PO No. not genrated!", "A");
                    }
                    else
                    {

                        for (int i = 0; i < Gridview1.Rows.Count; i++)
                        {
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
                            cmd.CommandText = "invPurchaeOrderProc";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Connection = _con;
                            cmd.Parameters.AddWithValue("@QtnNo", QtnNo);
                            cmd.Parameters.AddWithValue("@PONo", PONo);
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
                            cmd.Parameters.AddWithValue("@Total", lblTotal.Text.Trim());
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
                            cmd.Parameters.AddWithValue("@OrderDate", txtOrderDate.Text.Trim());
                            cmd.Parameters.AddWithValue("@PODescription", txtPODescription.Text.Trim());
                            cmd.Parameters.AddWithValue("@Terms", txtTerms.Text.Trim());
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
                            //Response.Write("<script>window.open ('PurchaseOrderPrint.aspx?QTN=" + QtnNo + "&&PO=" + PONo + "','_blank');</script>");
                        }
                    }
                }
            }
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(Page, dvSearch, "Invalid Quotation No.!", "A");
            Reset();
        }
    }

    private void Reset()
    {
        hfPONo.Value = string.Empty;
        txtPONo.Text = string.Empty;
        ddlTaxType.SelectedIndex = 0;
        txtPODescription.Text = string.Empty;
        txtTerms.Text = string.Empty;
        lblCGST.Text = "0.00";
        lblSGST.Text = "0.00";
        lblIGST.Text = "0.00";
        lblUGST.Text = "0.00";
        lblNa.Text = "0.00";
        lblGTotal.Text = "0.00";
        lblGrandTotal.Text= "0.00";
        pnlcontrols.Visible = false;
        gvBankBranchList.DataSource = null;
        gvBankBranchList.DataBind();
        SetInitialRow();
    }
}