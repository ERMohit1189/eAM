using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
namespace admin
{
    public partial class Eazypayicici : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Campus camp = new Campus(); camp.LoadLoader(loader);
            if(!IsPostBack)
            {
                GetPaymentGateway();
            }
        }

        protected void lnkSubmit_Click(object sender, EventArgs e)
        {
            if (lnkSubmit.Text == "Submit")
            {
                SetPaymentGateway();
            }
            else if (lnkSubmit.Text == "Update")
            {
                SetPaymentGatewayUpdate();
            }
        }

        public void SetPaymentGateway()
        {
            string msgs = "";
            if (txtGatwayURL.Text == "" || txtMerchantID.Text == "" || txtAESKey.Text == "")
            {
                msgs = "empty";
            }
            string Logoname = "";
            if (msgs != "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('All fields are required!')", true);
                Logoname = "";
            }
            if (imgLogo.ImageUrl == "" && fpUploadPhoto.HasFile == false)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Upload Getway Logo!')", true);
                Logoname = "";
            }
            else if (imgLogo.ImageUrl != "" && fpUploadPhoto.HasFile == false)
            {
                Logoname = "EasyPaylogo.jpg";
            }
            else if (fpUploadPhoto.HasFile == true)
            {
                string ext = Path.GetExtension(fpUploadPhoto.FileName);
                if (ext == ".jpg" || ext == ".jpeg")
                {
                    var filePath = Server.MapPath("~/uploads/CollegeLogo/EasyPaylogo.jpg");
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                    var fileUploadPath = Server.MapPath("~/uploads/CollegeLogo/");
                    string extension = Path.GetExtension(fpUploadPhoto.PostedFile.FileName);
                    fpUploadPhoto.SaveAs(fileUploadPath + "EasyPaylogo" + extension);
                    Logoname = "EasyPaylogo" + extension;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Only jpeg image allowed!')", true);
                    Logoname = "";
                }
            }
            if (Logoname != "" && msgs == "")
            {
                var param = new List<SqlParameter>
                {
                    new SqlParameter("@QueryFor", "I"),
                    new SqlParameter("@GatwayURL",txtGatwayURL.Text),
                    new SqlParameter("@MerchantID",txtMerchantID.Text),
                    new SqlParameter("@AESKey",txtAESKey.Text),
                    new SqlParameter("@Isactive",rbActivate.SelectedValue),
                    new SqlParameter("@Logo",Logoname),
                    new SqlParameter("@SessionName",Session["SessionName"].ToString()),
                    new SqlParameter("@LoginName",Session["LoginName"].ToString()),
                    new SqlParameter("@BranchCode",Session["BranchCode"].ToString())
                };

                SqlParameter para = new SqlParameter("@Msg", "");
                para.Direction = ParameterDirection.Output;

                param.Add(para);

                var msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("USP_EasyPayGatewaySetting", param);

                if (msg == "S")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Submitted successfully.')", true);
                    //Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Sorry, records(s) not submitted.')", true);
                    //Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sorry, records(s) not submitted.", "A");
                }
            }
        }
        public void SetPaymentGatewayUpdate()
        {
            string msgs = "";
            if (txtGatwayURL.Text == "" || txtMerchantID.Text == "" || txtAESKey.Text == "")
            {
                msgs = "empty";
            }
            string Logoname = "";
            if (msgs != "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('All fields are required!')", true);
                Logoname = "";
            }
            if (imgLogo.ImageUrl == "" && fpUploadPhoto.HasFile == false)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Upload Getway Logo!')", true);
                Logoname = "";
            }
            else if (imgLogo.ImageUrl != "" && fpUploadPhoto.HasFile == false)
            {
                Logoname = "EasyPaylogo.jpg";
            }
            else if (fpUploadPhoto.HasFile == true)
            {
                string ext = Path.GetExtension(fpUploadPhoto.FileName);
                if (ext == ".jpg")
                {
                    var filePath = Server.MapPath("~/uploads/CollegeLogo/EasyPaylogo.jpg");
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                    var fileUploadPath = Server.MapPath("~/uploads/CollegeLogo/");
                    string extension = Path.GetExtension(fpUploadPhoto.PostedFile.FileName);
                    fpUploadPhoto.SaveAs(fileUploadPath + "EasyPaylogo" + extension);
                    Logoname = "EasyPaylogo" + extension;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Only jpeg image allowed!')", true);
                    Logoname = "";
                }
            }
            if (Logoname != "" && msgs == "")
            {
                var param = new List<SqlParameter>
                {
                    new SqlParameter("@QueryFor", "U"),
                    new SqlParameter("@GatwayURL",txtGatwayURL.Text),
                    new SqlParameter("@MerchantID",txtMerchantID.Text),
                    new SqlParameter("@AESKey",txtAESKey.Text),
                    new SqlParameter("@Isactive",rbActivate.SelectedValue),
                    new SqlParameter("@Logo",Logoname),
                    new SqlParameter("@SessionName",Session["SessionName"].ToString()),
                    new SqlParameter("@LoginName",Session["LoginName"].ToString()),
                    new SqlParameter("@BranchCode",Session["BranchCode"].ToString())
                };

                SqlParameter para = new SqlParameter("@Msg", "");
                para.Direction = ParameterDirection.Output;

                param.Add(para);

                var msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("USP_EasyPayGatewaySetting", param);

                if (msg == "S")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Updated successfully.')", true);
                    //Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Updated successfully.", "S");
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Sorry, records(s) not submitted.')", true);
                    //Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sorry, records(s) not submitted.", "A");
                }
            }
        }
        public void GetPaymentGateway()
        {
            var param = new List<SqlParameter>
            {
                (new SqlParameter("@QueryFor","S")),
                (new SqlParameter("@BranchCode", Session["BranchCode"].ToString()))
            };

            SqlParameter para = new SqlParameter("@Msg", "");
            para.Direction = ParameterDirection.Output;

            param.Add(para);

            DataSet ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_EasyPayGatewaySetting", param);

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lnkSubmit.Text = "Update";
                        txtGatwayURL.Text = ds.Tables[0].Rows[0]["GatwayURL"].ToString();
                        txtMerchantID.Text = ds.Tables[0].Rows[0]["MerchantID"].ToString();
                        txtAESKey.Text = ds.Tables[0].Rows[0]["AESKey"].ToString();
                        imgLogo.ImageUrl = ""; imgLogo.ImageUrl = string.Empty;
                        imgLogo.ImageUrl = "../uploads/CollegeLogo/" + ds.Tables[0].Rows[0]["Logo"].ToString();
                        rbActivate.SelectedValue = ds.Tables[0].Rows[0]["Isactive"].ToString() == string.Empty ? "1" : ds.Tables[0].Rows[0]["Isactive"].ToString();
                    }
                    else
                    {
                        lnkSubmit.Text = "Submit";
                        txtGatwayURL.Text = "";
                        txtMerchantID.Text = "";
                        txtAESKey.Text = "";
                    }
                }
                else
                {
                    txtGatwayURL.Text = "";
                    txtMerchantID.Text = "";
                    txtAESKey.Text = "";
                }
            }
            else
            {
                txtGatwayURL.Text = "";
                txtMerchantID.Text = "";
                txtAESKey.Text = "";
            }
        }
    }
}
