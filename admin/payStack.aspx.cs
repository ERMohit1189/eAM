using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
namespace admin
{
    public partial class payStack : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Campus camp = new Campus(); camp.LoadLoader(loader);
            if(!IsPostBack)
            {
                GetPaymentGateway();
            }
            //Page.Form.Attributes.Add("enctype", "multipart/form-data");
        }

        protected void lnkSubmit_Click(object sender, EventArgs e)
        {
            SetPaymentGateway();
        }

        public void SetPaymentGateway()
        {
            string msgs = "";
            if (txtSP.Text == "" || txtSU.Text == "" || txtFU.Text == "" || txtPUnique.Text == "" || txtLSK.Text == "" || txtLPK.Text == "" || txtEmail.Text == "" || txtBU.Text == "")
            {
                msgs = "empty";
            }
            string Logoname = "";
            if (msgs != "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('All fields are required!')", true);
                Logoname = "";
            }
            else if(imgLogo.ImageUrl == "" && fpUploadPhoto.HasFile == false)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Upload Getway Logo!')", true);
                Logoname = "";
            }
            else if (imgLogo.ImageUrl != "" && fpUploadPhoto.HasFile == false)
            {
                Logoname = "PayStacklogo.jpg";
            }
            
            else if (fpUploadPhoto.HasFile == true && msgs == "")
            {
                string ext = Path.GetExtension(fpUploadPhoto.FileName);
                if (ext == ".jpg")
                {
                    var filePath = Server.MapPath("~/uploads/CollegeLogo/getWaylogo.jpg");
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                    var fileUploadPath = Server.MapPath("~/uploads/CollegeLogo/");
                    string extension = Path.GetExtension(fpUploadPhoto.PostedFile.FileName);
                    fpUploadPhoto.SaveAs(fileUploadPath + "PayStacklogo" + extension);
                    Logoname = "PayStacklogo" + extension;
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
                        new SqlParameter("@SuccessURL",txtSU.Text),
                        new SqlParameter("@FailureUrl",txtFU.Text),
                        new SqlParameter("@ServiceProvider",txtSP.Text),
                        new SqlParameter("@PseudoUniqueReference",txtPUnique.Text),
                        new SqlParameter("@LiveSecretKey",txtLSK.Text),
                        new SqlParameter("@LivePublicKey",txtLPK.Text),
                        new SqlParameter("@BaseURL",txtBU.Text),
                        new SqlParameter("@Email",txtEmail.Text),
                        new SqlParameter("@Logo",Logoname),
                        new SqlParameter("@Isactive",rbActivate.SelectedValue),
                    };

                SqlParameter para = new SqlParameter("@Msg", "");
                para.Direction = ParameterDirection.Output;

                param.Add(para);

                var msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("USP_PayStackGatewaySetting", param);
                if (msg == "S")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Submitted successfully.')", true);
                    //Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
                    GetPaymentGateway();
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
            };

            DataSet ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_PayStackGatewaySetting", param);

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lnkSubmit.Text = "Update";
                        txtSP.Text = ds.Tables[0].Rows[0]["ServiceProvider"].ToString();
                        txtSU.Text = ds.Tables[0].Rows[0]["SuccessURL"].ToString();
                        txtFU.Text = ds.Tables[0].Rows[0]["FailureUrl"].ToString();
                        txtPUnique.Text = ds.Tables[0].Rows[0]["PseudoUniqueReference"].ToString();
                        txtLSK.Text = ds.Tables[0].Rows[0]["LiveSecretKey"].ToString();
                        txtLPK.Text = ds.Tables[0].Rows[0]["LivePublicKey"].ToString();
                        txtEmail.Text = ds.Tables[0].Rows[0]["Email"].ToString();
                        txtBU.Text = ds.Tables[0].Rows[0]["BaseURL"].ToString();
                        imgLogo.ImageUrl = ""; imgLogo.ImageUrl = string.Empty;
                        imgLogo.ImageUrl = "~/uploads/CollegeLogo/"+ds.Tables[0].Rows[0]["Logo"].ToString();
                        rbActivate.SelectedValue = ds.Tables[0].Rows[0]["Isactive"].ToString().ToLower() == "true" ? "1" : "0";
                    }
                    else
                    {
                        lnkSubmit.Text = "Submit";
                        //txtSP.Text = "";
                        //txtSU.Text = "";
                        //txtFU.Text = "";
                        //txtPUnique.Text = "";
                        //txtLSK.Text = "";
                        //txtLPK.Text = "";
                        //txtEmail.Text = "";
                        //txtBU.Text = "";
                        //imgLogo.ImageUrl = "";
                    }
                }
                else
                {
                    txtSP.Text = "";
                    txtSU.Text = "";
                    txtFU.Text = "";
                    txtPUnique.Text = "";
                    txtLSK.Text = "";
                    txtLPK.Text = "";
                    txtEmail.Text = "";
                    txtBU.Text = "";
                    imgLogo.ImageUrl = "";
                }
            }
            else
            {
                txtSP.Text = "";
                txtSU.Text = "";
                txtFU.Text = "";
                txtPUnique.Text = "";
                txtLSK.Text = "";
                txtLPK.Text = "";
                txtEmail.Text = "";
                txtBU.Text = "";
                imgLogo.ImageUrl = "";
            }
        }
    }
}
