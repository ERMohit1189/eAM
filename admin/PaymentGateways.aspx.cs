using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
public partial class PaymentGateways : Page
{
    string sql = "";
    private SqlConnection _con;
    private Campus oo;
    protected void Page_Load(object sender, EventArgs e)
    {
        _con = new SqlConnection();
        oo = new Campus();
        _con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            GetPaymentGateway();
        }
    }
    public void GetPaymentGateway()
    {
       string sql = "select count(*)cnt from TblPaymentGateway where BranchCode="+Session["BranchCode"]+" and GateWayName='"+ddlGatewayname.SelectedValue+ "' and GateWayFor='" + ddlGatewayFor.SelectedValue + "'";
       string sql1 = "select * from TblPaymentGateway where BranchCode=" + Session["BranchCode"]+" and GateWayName='"+ddlGatewayname.SelectedValue+ "' and GateWayFor='" + ddlGatewayFor.SelectedValue + "'";
        if (oo.ReturnTag(sql, "cnt")!="0")
        {
            lnkSubmit.Text = "Update";
            if (ddlGatewayname.SelectedValue == "PayUMoney")
            {
                
                txtSU.Text = oo.ReturnTag(sql1,"SuccessURL");
                txtFU.Text = oo.ReturnTag(sql1,"FailurURL");
                txtSP.Text = oo.ReturnTag(sql1,"ServiceProvider");
                txtPI.Text = oo.ReturnTag(sql1,"ProductInfo");
                txtMK.Text = oo.ReturnTag(sql1,"MarchentKEY");
                txtMS.Text = oo.ReturnTag(sql1,"Marchentsalt");
                txtBU.Text = oo.ReturnTag(sql1,"BaseURL");
                txtHS.Text = oo.ReturnTag(sql1, "HashSequence");
                txtAuthHeader.Text = oo.ReturnTag(sql1, "authorizationHeader");
                
            }
            if (ddlGatewayname.SelectedValue == "Eazypay")
            {
                txtGatwayURL.Text = oo.ReturnTag(sql1, "ReturnURL");
                txtMerchantID.Text = oo.ReturnTag(sql1, "MerchantID");
                txtAESKey.Text = oo.ReturnTag(sql1, "AESKey");
            }
            if (ddlGatewayname.SelectedValue == "PayStack")
            {
                txtPUnique.Text = oo.ReturnTag(sql1, "Pseudouniquereference");
                txtLSK.Text = oo.ReturnTag(sql1, "LiveSecretKey");
                txtLPK.Text = oo.ReturnTag(sql1, "LivePublicKey");
                txtEmail.Text = oo.ReturnTag(sql1, "Email");
                txtbaseUrl.Text = oo.ReturnTag(sql1, "BaseURL");
            }
            paymentCharges.Text= oo.ReturnTag(sql1, "paymentCharges");
            
            imgLogo.Visible = true;
            imgLogo.ImageUrl = "~/uploads/CollegeLogo/ss.jpeg"; 
            imgLogo.ImageUrl = "~/uploads/CollegeLogo/" + oo.ReturnTag(sql1, "Logo")+"?s="+DateTime.Now.ToString("ddMMMyyyyhhmmsstt");
            if (oo.ReturnTag(sql1, "Isactive") == "" || oo.ReturnTag(sql1, "Isactive") == "False")
            {
                rbActivate.SelectedIndex = 1;
            }
            else
            {
                rbActivate.SelectedIndex = 0;
            }
        }
        else
        {
            lnkSubmit.Text = "Submit";
            txtSU.Text = "";
            txtFU.Text = "";
            txtSP.Text = "";
            txtPI.Text = "";
            txtMK.Text = "";
            txtMS.Text = "";
            txtBU.Text = "";
            txtHS.Text = "";
            txtGatwayURL.Text = "";
            txtMerchantID.Text = "";
            txtAESKey.Text = "";
            txtPUnique.Text = "";
            txtLSK.Text = "";
            txtLPK.Text = "";
            txtEmail.Text = "";
            txtbaseUrl.Text = "";
            rbActivate.SelectedIndex = 0;
            imgLogo.Visible = false;
        }

        //txtPUnique.Text = "1000000000";
        //txtLSK.Text = "sk_test_777b695a8d4693b7105b7bd896c08a7b9a8f23dd";
        //txtLPK.Text = "pk_test_7daaa6985c14039c9a22a9a67cb15aa53e68de7c";
        //txtEmail.Text = "customer@email.com";
        //txtbaseUrl.Text = "https://js.paystack.co/v1/inline.js";
    }
    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        SetPaymentGateway();
    }

    public void SetPaymentGateway()
    {
        string msgs = ""; string Logonames = "";
        if (ddlGatewayname.SelectedValue == "PayUMoney")
        {
            if (txtSP.Text == "" || txtSU.Text == "" || txtFU.Text == "" || txtSP.Text == "" || txtPI.Text == "" || txtMK.Text == "" || txtMS.Text == "" || txtBU.Text == "" || txtHS.Text == "" || txtAuthHeader.Text == "")
            {
                msgs = "empty";
            }
            Logonames = Session["BranchCode"].ToString()+"PayuMonylogo";
        }
        if (ddlGatewayname.SelectedValue == "Eazypay")
        {
            if (txtGatwayURL.Text == "" || txtMerchantID.Text == "" || txtAESKey.Text == "")
            {
                msgs = "empty";
            }
            Logonames = Session["BranchCode"].ToString()+"EassyPaylogo";
        }
        if (ddlGatewayname.SelectedValue == "PayStack")
        {
            if (txtPUnique.Text == "" || txtLSK.Text == "" || txtLPK.Text == "" || txtEmail.Text == "" || txtbaseUrl.Text == "")
            {
                msgs = "empty";
            }
            Logonames = Session["BranchCode"].ToString()+"PayStacklogo";
        }
        string Logoname = "";
        if (msgs != "")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('All fields are required!')", true);
            Logoname = "";
        }
        if (imgLogo.ImageUrl == "" && fpUploadPhoto.HasFile == false)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Upload Gateway Logo!')", true);
            Logoname = "";
        }
        else if (imgLogo.ImageUrl != "" && fpUploadPhoto.HasFile == false)
        {
            Logoname = Logonames;
        }
        else if (fpUploadPhoto.HasFile == true)
        {
            string ext = Path.GetExtension(fpUploadPhoto.FileName);
            if (ext.ToLower() == ".jpg" || ext.ToLower() == ".jpeg" || ext.ToLower() == ".png")
            {
                string extension = Path.GetExtension(fpUploadPhoto.PostedFile.FileName);
                var filePath = Server.MapPath("~/uploads/CollegeLogo/" + Logonames + extension);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                var fileUploadPath = Server.MapPath("~/uploads/CollegeLogo/");

                fpUploadPhoto.SaveAs(fileUploadPath + Logonames + extension);
                Logonames = Logonames + extension;
            }
            else
            {
                Logonames = "";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Only jpeg, png image format allowed!')", true);
            }
        }
        else
        {
            Logonames = "";
        }
        if (Logonames != "" && msgs == "")
        {

            string msg = "", QueryFor = "";
            QueryFor = lnkSubmit.Text.ToLower() == "submit" ? "I" : "U";
            if (ddlGatewayname.SelectedValue == "PayUMoney")
            {
                var param = new List<SqlParameter>
                {
                    new SqlParameter("@QueryFor", QueryFor),
                    new SqlParameter("@SuccessURL",txtSU.Text),
                    new SqlParameter("@FailurURL",txtFU.Text),
                    new SqlParameter("@ServiceProvider",txtSP.Text),
                    new SqlParameter("@ProductInfo",txtPI.Text),
                    new SqlParameter("@MarchentKEY",txtMK.Text),
                    new SqlParameter("@Marchentsalt",txtMS.Text),
                    new SqlParameter("@BaseURL",txtBU.Text),
                    new SqlParameter("@HashSequence",txtHS.Text),
                    new SqlParameter("@Isactive",rbActivate.SelectedValue),
                    new SqlParameter("@Logo", (fpUploadPhoto.HasFile == true && Logonames!=""? Logonames:"na")),
                    new SqlParameter("@SessionName",Session["SessionName"].ToString()),
                    new SqlParameter("@LoginName",Session["LoginName"].ToString()),
                    new SqlParameter("@BranchCode",Session["BranchCode"].ToString()),
                    new SqlParameter("@Gatewayname",ddlGatewayname.SelectedValue),
                    new SqlParameter("@GatewayFor",ddlGatewayFor.SelectedValue),
                    new SqlParameter("@paymentCharges",paymentCharges.Text),
                    new SqlParameter("@authorizationHeader",txtAuthHeader.Text)

                };
                //---
                SqlParameter para = new SqlParameter("@Msg", "");
                para.Direction = ParameterDirection.Output;
                param.Add(para);
                msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("USP_TblPayMentGateway", param);
            }
            if (ddlGatewayname.SelectedValue == "Eazypay")
            {
                var param = new List<SqlParameter>
                {
                    new SqlParameter("@QueryFor", QueryFor),
                    new SqlParameter("@GatwayURL",txtGatwayURL.Text),
                    new SqlParameter("@MerchantID",txtMerchantID.Text),
                    new SqlParameter("@AESKey",txtAESKey.Text),
                    new SqlParameter("@Isactive",rbActivate.SelectedValue),
                     new SqlParameter("@Logo", (fpUploadPhoto.HasFile == true && Logonames!=""? Logonames:"na")),
                    new SqlParameter("@SessionName",Session["SessionName"].ToString()),
                    new SqlParameter("@LoginName",Session["LoginName"].ToString()),
                    new SqlParameter("@BranchCode",Session["BranchCode"].ToString()),
                    new SqlParameter("@Gatewayname",ddlGatewayname.SelectedValue),
                    new SqlParameter("@GatewayFor",ddlGatewayFor.SelectedValue),
                    new SqlParameter("@paymentCharges",paymentCharges.Text)

                };
                SqlParameter para = new SqlParameter("@Msg", "");
                para.Direction = ParameterDirection.Output;
                param.Add(para);
                msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("USP_TblPayMentGateway", param);
            }
            if (ddlGatewayname.SelectedValue == "PayStack")
            {
                var param = new List<SqlParameter>
                {
                    new SqlParameter("@QueryFor", QueryFor),
                    new SqlParameter("@PseudoUniqueReference",txtPUnique.Text),
                    new SqlParameter("@LiveSecretKey",txtLSK.Text),
                    new SqlParameter("@LivePublicKey",txtLPK.Text),
                    new SqlParameter("@BaseURL",txtbaseUrl.Text),
                    new SqlParameter("@Email",txtEmail.Text),
                    new SqlParameter("@Isactive",rbActivate.SelectedValue),
                     new SqlParameter("@Logo", (fpUploadPhoto.HasFile == true && Logonames!=""? Logonames:"na")),
                    new SqlParameter("@SessionName",Session["SessionName"].ToString()),
                    new SqlParameter("@LoginName",Session["LoginName"].ToString()),
                    new SqlParameter("@BranchCode",Session["BranchCode"].ToString()),
                    new SqlParameter("@Gatewayname",ddlGatewayname.SelectedValue),
                    new SqlParameter("@GatewayFor",ddlGatewayFor.SelectedValue),
                    new SqlParameter("@paymentCharges",paymentCharges.Text)
                };
                SqlParameter para = new SqlParameter("@Msg", "");
                para.Direction = ParameterDirection.Output;
                param.Add(para);
                msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("USP_TblPayMentGateway", param);
            }

            if (msg == "S")
            {
                GetPaymentGateway();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Submitted successfully.')", true);
            }
            if (msg == "U")
            {
                GetPaymentGateway();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Updated successfully.')", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Sorry, records(s) not submitted.')", true);
            }
        }
    }



    protected void ddlGatewayname_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlGatewayname.SelectedValue== "PayUMoney")
        {
            divPayumoney.Visible = true;
            divEazypay.Visible = false;
            divPayStack.Visible = false;
            GetPaymentGateway();
        }
        if (ddlGatewayname.SelectedValue == "Eazypay")
        {
            divPayumoney.Visible = false;
            divEazypay.Visible = true;
            divPayStack.Visible = false;
            GetPaymentGateway();
        }
        if (ddlGatewayname.SelectedValue == "PayStack")
        {
            divPayumoney.Visible = false;
            divEazypay.Visible = false;
            divPayStack.Visible = true;
            GetPaymentGateway();
        }
    }

    protected void ddlGatewayFor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlGatewayname.SelectedValue == "PayUMoney")
        {
            divPayumoney.Visible = true;
            divEazypay.Visible = false;
            divPayStack.Visible = false;
            GetPaymentGateway();
        }
        if (ddlGatewayname.SelectedValue == "Eazypay")
        {
            divPayumoney.Visible = false;
            divEazypay.Visible = true;
            divPayStack.Visible = false;
            GetPaymentGateway();
        }
        if (ddlGatewayname.SelectedValue == "PayStack")
        {
            divPayumoney.Visible = false;
            divEazypay.Visible = false;
            divPayStack.Visible = true;
            GetPaymentGateway();
        }
    }
}