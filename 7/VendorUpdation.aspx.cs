using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VendorUpdation : Page
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
                txtDOR.Text = DateTime.Now.ToString("dd-MMM-yyyy");


                GetVendorType();
                GetOrganizationType();
                GetCountry();
                GetState();
                GetDistrict();
                GetBank();
                GetBankBranch();
            }
            catch (Exception ex)
            {
                //throw new Exception("some reason to rethrow", ex);
            }
        }
    }

    private void GetVendorType()
    {
        ddlVendorType.Items.Clear();
        _sql = "select * from AccVendorType where branchcode=" + Session["BranchCode"] + "";
        _oo.FillDropDown_withValue(_sql, ddlVendorType, "VendorType", "id");
        ddlVendorType.Items.Insert(0, new ListItem("<--Select-->", "0"));
    }

    private void GetOrganizationType()
    {
        ddlOrganisationType.Items.Clear();
        _sql = "select * from AccOrganizationType where branchcode=" + Session["BranchCode"] + "";
        _oo.FillDropDown_withValue(_sql, ddlOrganisationType, "OrganizationType", "id");
        ddlOrganisationType.Items.Insert(0, new ListItem("<--Select-->", "0"));
    }

    private void GetBank()
    {
        ddlBankName.Items.Clear();
        _sql = "select * from AccBankMaster";
        _oo.FillDropDown_withValue(_sql, ddlBankName, "BankName", "id");
        ddlBankName.Items.Insert(0, new ListItem("<--Select-->", "0"));
    }

    private void GetBankBranch()
    {
        ddlBankBranch.Items.Clear();
        _sql = "select * from AccBankBranchMaster where bankid=" + ddlBankName.SelectedValue + "";
        _oo.FillDropDown_withValue(_sql, ddlBankBranch, "BankBranchName", "id");
        ddlBankBranch.Items.Insert(0, new ListItem("<--Select-->", "0"));

    }

    private void GetCountry()
    {
        ddlCountry.Items.Clear();
        _sql = "select * from countrymaster";
        _oo.FillDropDown_withValue(_sql, ddlCountry, "CountryName", "id");
        ddlCountry.Items.Insert(0, new ListItem("<--Select-->", "0"));
        BLL.BLLInstance.loadDefaultvalue("Country", ddlCountry);
    }

    private void GetState()
    {
        ddlState.Items.Clear();
        _sql = "select * from statemaster where countryid=" + ddlCountry.SelectedValue + "";
        _oo.FillDropDown_withValue(_sql, ddlState, "StateName", "id");
        ddlState.Items.Insert(0, new ListItem("<--Select-->", "0"));
        BLL.BLLInstance.loadDefaultvalue("State", ddlState);
    }

    private void GetDistrict()
    {
        ddlDistrict.Items.Clear();
        _sql = "select * from citymaster where stateid=" + ddlState.SelectedValue + "";
        _oo.FillDropDown_withValue(_sql, ddlDistrict, "CityName", "id");
        ddlDistrict.Items.Insert(0, new ListItem("<--Select-->", "0"));
        BLL.BLLInstance.loadDefaultvalue("City", ddlDistrict);

    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDistrict();
        ddlState.Focus();
    }
    protected void ddlBankName_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBankBranch();
        ddlBankName.Focus();
    }
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetState();
        ddlCountry.Focus();
    }


    protected void ddlBankBranch_SelectedIndexChanged1(object sender, EventArgs e)
    {
        txtIFSC.Text = "";
        txtBankAddress.Text = "";
        txtBankPIN.Text = "";
        txtAccountNo.Text = "";

        if (ddlBankName.SelectedIndex > 0)
        {

            if (ddlBankBranch.SelectedIndex > 0)
            {
                _sql = "select * from AccBankBranchMaster where branchcode=" + Session["BranchCode"] + " and bankid=" + ddlBankName.SelectedValue + " and id=" + ddlBankBranch.SelectedValue + "";
                dt = null;
                dt = _oo.Fetchdata(_sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    txtIFSC.Text = dt.Rows[0]["IFSC"].ToString();
                    txtBankAddress.Text = dt.Rows[0]["Address"].ToString();
                    txtBankPIN.Text = dt.Rows[0]["PIN"].ToString();
                }
            }
        }
    }

    protected void txtOrganizationName_TextChanged(object sender, EventArgs e)
    {
        txtDisplayName.Text = string.Empty;

        if (!string.IsNullOrEmpty(txtOrganizationName.Text.Trim()))
        {
            txtDisplayName.Text = txtOrganizationName.Text.Trim();
        }
    }




    protected void txtSearchBy_TextChanged(object sender, EventArgs e)
    {
        var studentId = hfVendorId.Value;
        if (txtSearchBy.Text != string.Empty && studentId != String.Empty)
        {

            GetVendor();
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, dvSearch, BLL.BLLInstance.FetchMSG("Enter Vendor ID !"), "A");
            txtSearchBy.Focus();
        }
    }
    protected void lbtnSearchBy_Click(object sender, EventArgs e)
    {
        var studentId = hfVendorId.Value;
        if (txtSearchBy.Text != string.Empty && studentId != String.Empty)
        {


            GetVendor();
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, dvSearch, BLL.BLLInstance.FetchMSG("Enter Vendor ID !"), "A");
            txtSearchBy.Focus();
        }
    }
    private void GetVendor()
    {
        DataTable Dt1 = null;
        var studentId = hfVendorId.Value;
        sql = " SELECT Vend.*, VenderTypeId, OrganizationTypeId, VendDoc.DocumentName, VendDoc.FileName, ";
        sql = sql + " CASE WHEN VendDoc.FilePath = '~/Uploads/VendorDoc/' THEN '' ELSE VendDoc.FilePath END FilePath, VendDoc.Remark RemarkVendDoc, AccountNo, ";
        sql = sql + " AccountType, Vend.BranchCode, Vend.Address, VendBank.bankid, BankBranchId, VendBank.Remark RemarkVendBank, BM.BankName, BbM.BankBranchName, BbM.Address AddressBbM, ";
        sql = sql + " BbM.IFSC, BbM.PIN BankPIN, CONVERT(VARCHAR(50), Vend.DOR, 106) Date, C.CityName ";
        sql = sql + " FROM AccVendor Vend ";
        sql = sql + " LEFT JOIN AccVendorDocument VendDoc ON VendDoc.VendorId = Vend.ID and VendDoc.BranchCode = Vend.BranchCode ";
        sql = sql + " LEFT JOIN AccVendorBank VendBank ON VendBank.VendorId = Vend.ID and VendBank.BranchCode = Vend.BranchCode ";
        sql = sql + " LEFT JOIN AccBankMaster BM ON BM.id = VendBank.BankId ";
        sql = sql + " LEFT JOIN AccBankBranchMaster BbM ON BbM.id = VendBank.BankBranchId ";
        sql = sql + " JOIN CityMaster C ON C.Id = Vend.DistrictID and C.BranchCode = Vend.BranchCode ";
        sql = sql + " WHERE Vend.IsActive = 1 and Vend.BranchCode ="+Session["BranchCode"] +" and Vend.id ="+ studentId + " ";

        Dt1 = _oo.Fetchdata(sql);

        if (Dt1 != null && Dt1.Rows.Count > 0)
        {
            txtVendorID.Text = studentId.Trim();
            txtAccountNo.Text = Dt1.Rows[0]["AccountNo"].ToString();
            txtAddress.Text = Dt1.Rows[0]["Address"].ToString();
            txtBankAddress.Text = Dt1.Rows[0]["AddressBbM"].ToString();
            txtBankPIN.Text = Dt1.Rows[0]["BankPIN"].ToString();
            txtContactPerson.Text = Dt1.Rows[0]["ContactPerson"].ToString();
            txtDisplayName.Text = Dt1.Rows[0]["DisplayName"].ToString();
            txtDOR.Text = Convert.ToDateTime(Dt1.Rows[0]["Date"].ToString()).ToString("MMM dd yyyy");
            txtIFSC.Text = Dt1.Rows[0]["IFSC"].ToString();
            txtMailID.Text = Dt1.Rows[0]["MailID"].ToString();
            txtMobileNo.Text = Dt1.Rows[0]["MobileNo"].ToString();
            cbIsWhatsApp.Checked = Dt1.Rows[0]["IsWhatsApp"].ToString() == "True" ? true : false;
            txtOrganizationName.Text = Dt1.Rows[0]["OrganizationName"].ToString();
            txtOwnerName.Text = Dt1.Rows[0]["OwnerName"].ToString();
            txtPAN.Text = Dt1.Rows[0]["PAN"].ToString();
            txtPhoneNo.Text = Dt1.Rows[0]["PhoneNo"].ToString();
            txtPIN.Text = Dt1.Rows[0]["PIN"].ToString();
            txtRegistrationNo.Text = Dt1.Rows[0]["RegistrationNo"].ToString();
            txtRemark.Text = Dt1.Rows[0]["Remark"].ToString();
            txtServiceTaxNo.Text = Dt1.Rows[0]["ServiceTaxNo"].ToString();
            txtTAN.Text = Dt1.Rows[0]["TAN"].ToString();
            txtTIN.Text = Dt1.Rows[0]["TIN"].ToString();
            txtWebsite.Text = Dt1.Rows[0]["Website"].ToString();
            txtVendorID.Text = Dt1.Rows[0]["ID"].ToString();
            txtDocumentName.Text = Dt1.Rows[0]["DocumentName"].ToString();

            var banknameddlvar = Dt1.Rows[0]["bankid"].ToString().Trim();
            if (banknameddlvar != "0")
            {
                GetBank();
                ddlBankName.SelectedValue = banknameddlvar;
            }
            var vardankddl = Dt1.Rows[0]["BankBranchId"].ToString().Trim();
            if (vardankddl != "0")
            {
                GetBankBranch();
                ddlBankBranch.SelectedValue = vardankddl;
            }
            var ddlcountry = Dt1.Rows[0]["CountryID"].ToString().Trim();
            if (ddlcountry != "0")
            {
                ddlCountry.SelectedValue = ddlcountry;
            }
            var ddlstate = Dt1.Rows[0]["StateID"].ToString().Trim();
            if (ddlstate != "0")
            {
                ddlState.SelectedValue = ddlstate;
            }
            var ddldistt = Dt1.Rows[0]["DistrictID"].ToString().Trim();
            if (ddldistt != "0")
            {
                GetDistrict();
                ddlDistrict.SelectedValue = ddldistt;
            }
            var ddlvendor = Dt1.Rows[0]["VenderTypeId"].ToString().Trim();
            if (ddlvendor != "0")
            {
                ddlVendorType.SelectedValue = ddlvendor;
            }
            var ddlorg = Dt1.Rows[0]["OrganizationTypeId"].ToString().Trim();
            if (ddlorg != "0")
            {
                ddlOrganisationType.SelectedValue = ddlorg;
            }
            var rdbac = Dt1.Rows[0]["AccountType"].ToString().Trim();
            if (rdbac != "")
            {
                rblAccountType.SelectedValue = rdbac;
            }
            rblIsActive.SelectedValue = Dt1.Rows[0]["IsActive"].ToString() == "True" ? "1" : "0";
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Invalid Account!", "A");
        }
    }


    protected void lbtnUpdate_Click(object sender, EventArgs e)
    {
        if (txtVendorID.Text=="")
        {
            Campus camp = new Campus(); camp.msgbox(Page, dvSearch, "Invalid Vendor Name!", "A");

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

                fileName = "REGDocument_" + rn.Next(123456789, 987654321) + DateTime.Now.ToString("ddmmmyyyyhhmmsstt") + "_" + hidFileExt.Value.ToString();
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
            cmd.CommandText = "AccVendorProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = _con;
            cmd.Parameters.AddWithValue("@ID", txtVendorID.Text);
            cmd.Parameters.AddWithValue("@VendorTypeID", ddlVendorType.SelectedValue);
            cmd.Parameters.AddWithValue("@OrganizationTypeID", ddlOrganisationType.SelectedValue);
            cmd.Parameters.AddWithValue("@RegistrationNo", txtRegistrationNo.Text.Trim());
            cmd.Parameters.AddWithValue("@OrganizationName", txtOrganizationName.Text.Trim());
            cmd.Parameters.AddWithValue("@OwnerName", txtOwnerName.Text.Trim());
            cmd.Parameters.AddWithValue("@DisplayName", txtDisplayName.Text.Trim());
            cmd.Parameters.AddWithValue("@DOR", txtDOR.Text.Trim());
            cmd.Parameters.AddWithValue("@PAN", txtPAN.Text.Trim());
            cmd.Parameters.AddWithValue("@TAN", txtTAN.Text.Trim());
            cmd.Parameters.AddWithValue("@TIN", txtTIN.Text.Trim());
            cmd.Parameters.AddWithValue("@ServiceTaxNo", txtServiceTaxNo.Text.Trim());
            cmd.Parameters.AddWithValue("@ContactPerson", txtContactPerson.Text.Trim());
            cmd.Parameters.AddWithValue("@PhoneNo", txtPhoneNo.Text.Trim());
            cmd.Parameters.AddWithValue("@MobileNo", txtMobileNo.Text.Trim());
            cmd.Parameters.AddWithValue("@IsWhatsApp", cbIsWhatsApp.Checked? true : false);
            cmd.Parameters.AddWithValue("@Website", txtWebsite.Text.Trim());
            cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
            cmd.Parameters.AddWithValue("@CountryID", ddlCountry.SelectedValue.Trim());
            cmd.Parameters.AddWithValue("@MailID", txtMailID.Text.Trim());
            cmd.Parameters.AddWithValue("@StateID", ddlState.SelectedValue.Trim());
            cmd.Parameters.AddWithValue("@DistrictID", ddlDistrict.SelectedValue.Trim());
            cmd.Parameters.AddWithValue("@PIN", txtPIN.Text.Trim());
            cmd.Parameters.AddWithValue("@DocumentName", txtDocumentName.Text.Trim());
            if (fileName!="" && fileName!= string.Empty)
            {
                cmd.Parameters.AddWithValue("@FileName", fileName.Trim());
            }
            if (filePath != "" && filePath != string.Empty)
            {
                cmd.Parameters.AddWithValue("@FilePath", filePath.Trim());
            }
            cmd.Parameters.AddWithValue("@BankID", ddlBankName.SelectedValue.Trim());
            cmd.Parameters.AddWithValue("@BankBranchID", ddlBankBranch.SelectedValue.Trim());
            cmd.Parameters.AddWithValue("@AccountNo", txtAccountNo.Text.Trim());
            cmd.Parameters.AddWithValue("@AccountType", rblAccountType.SelectedItem.Text.Trim());
            cmd.Parameters.AddWithValue("@Remark", txtRemark.Text.Trim());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
            cmd.Parameters.AddWithValue("@IsActive", Convert.ToInt32(rblIsActive.SelectedValue));
            cmd.Parameters.AddWithValue("@Action", "update");
            try
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Updated successfully.", "S");
                Reset();
            }
            catch (Exception ex)
            {
            }
        }
    }


    private void Reset()
    {
        txtAccountNo.Text = string.Empty;
        txtAddress.Text = string.Empty;
        txtBankAddress.Text = string.Empty;
        txtBankPIN.Text = string.Empty;
        txtContactPerson.Text = string.Empty;
        txtDisplayName.Text = string.Empty;
        txtDOR.Text = string.Empty;
        txtIFSC.Text = string.Empty;
        txtMailID.Text = string.Empty;
        txtMobileNo.Text = string.Empty;
        txtOrganizationName.Text = string.Empty;
        txtOwnerName.Text = string.Empty;
        txtPAN.Text = string.Empty;
        txtPhoneNo.Text = string.Empty;
        txtPIN.Text = string.Empty;
        txtRegistrationNo.Text = string.Empty;
        txtRemark.Text = string.Empty;
        txtServiceTaxNo.Text = string.Empty;
        txtTAN.Text = string.Empty;
        txtTIN.Text = string.Empty;
        txtWebsite.Text = string.Empty;
        txtDocumentName.Text = string.Empty;

        ddlBankBranch.SelectedIndex = 0;
        ddlBankName.SelectedIndex = 0;
        ddlDistrict.SelectedIndex = 0;
        ddlState.SelectedIndex = 0;
        ddlVendorType.SelectedIndex = 0;
        ddlOrganisationType.SelectedIndex = 0;
        hfVendorId.Value = "";
        txtSearchBy.Text = "";
        txtVendorID.Text = "";
    }
    public override void Dispose()
    {
        _oo.Dispose();
        _oo.Dispose();
    }
}
