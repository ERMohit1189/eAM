using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vendor : Page
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
            }
        }
    }

    private void GetVendorType()
    {
        ddlVendorType.Items.Clear();
        _sql = "select * from AccVendorType where branchcode=" + Session["BranchCode"] + "";
        _oo.FillDropDown_withValue(_sql, ddlVendorType, "VendorType", "id");
        ddlVendorType.Items.Insert(0, new ListItem("<--Select-->", ">"));
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
    }

    protected void lbtnInsert_Click(object sender, EventArgs e)
    {

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

                fileName = "ERGDocument_" + rn.Next(123456789, 987654321) + DateTime.Now.ToString("ddmmmyyyyhhmmsstt") + "_" + hidFileExt.Value.ToString();
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
            cmd.Parameters.AddWithValue("@IsWhatsApp", cbIsWhatsApp.Checked);
            cmd.Parameters.AddWithValue("@Website", txtWebsite.Text.Trim());
            cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
            cmd.Parameters.AddWithValue("@CountryID", ddlCountry.SelectedValue.Trim());
            cmd.Parameters.AddWithValue("@MailID", txtMailID.Text.Trim());
            cmd.Parameters.AddWithValue("@StateID", ddlState.SelectedValue.Trim());
            cmd.Parameters.AddWithValue("@DistrictID", ddlDistrict.SelectedValue.Trim());
            cmd.Parameters.AddWithValue("@PIN", txtPIN.Text.Trim());
            cmd.Parameters.AddWithValue("@DocumentName", txtDocumentName.Text.Trim());
            cmd.Parameters.AddWithValue("@FileName", fileName.Trim());
            cmd.Parameters.AddWithValue("@FilePath", filePath.Trim());
            cmd.Parameters.AddWithValue("@BankID", ddlBankName.SelectedValue.Trim());
            cmd.Parameters.AddWithValue("@BankBranchID", ddlBankBranch.SelectedValue.Trim());
            cmd.Parameters.AddWithValue("@AccountNo", txtAccountNo.Text.Trim());
            cmd.Parameters.AddWithValue("@AccountType", rblAccountType.SelectedItem.Text.Trim());
            cmd.Parameters.AddWithValue("@Remark", txtRemark.Text.Trim());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
            cmd.Parameters.AddWithValue("@IsActive", Convert.ToInt32(rblIsActive.SelectedValue));
            cmd.Parameters.AddWithValue("@Action", "insert");
            try
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
                Reset();
                GetCountry();
                GetState();
                GetDistrict();
            }
            catch (Exception ex)
            {
            }
        }
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
                _sql = "select * from AccBankBranchMaster where bankid=" + ddlBankName.SelectedValue + " and id=" + ddlBankBranch.SelectedValue + "";
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
    
    public override void Dispose()
    {
        _oo.Dispose();
        _oo.Dispose();
    }
}
