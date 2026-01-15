using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_CollegeMaster : Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }

        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            try
            {
                CheckValueADDDeleteUpdate();
            }
            catch (Exception) { }
            txtCollegeShortNa.Text = "";
            sql = "select ShortCode from BranchTab where BranchId=" + Session["BranchCode"] + "";
            if (oo.Duplicate(sql))
            {
                txtCollegeShortNa.Text = oo.ReturnTag(sql, "ShortCode");
            }
            oo.AddDateMonthYearDropDown(drpYear, drpMonth, DrpDate);
            oo.FindCurrentDateandSetinDropDown(drpYear, drpMonth, DrpDate);

            sql = "select ltrim(rtrim(CountryName)) as CountryName, id from CountryMaster ";
            oo.FillDropDown_withValue(sql, DrpCountry, "CountryName", "id");

            string sss = "Select defaultvalue from DefaultSelectedValue where BranchCode=" + Session["BranchCode"] + " and defaultvalueof='Country'";
            if (oo.ReturnTag(sss, "defaultvalue") == "")
            {
                DrpCountry.SelectedValue = "1";
            }
            else
            {
                DrpCountry.SelectedValue = oo.ReturnTag(sss, "defaultvalue");
            }
            sql = "select ltrim(rtrim(StateName)) as StateName, id from StateMaster where CountryId=" + DrpCountry.SelectedValue + "";
            oo.FillDropDown_withValue(sql, DrpState, "StateName", "id");
            using (var objBll = new BLL())
            {
                try
                {
                    objBll.loadDefaultvalue("State", DrpState);
                }
                catch
                {
                    // ignored
                }
            }

            sql = "select ltrim(rtrim(CityName)) as CityName, id from CityMaster  where stateid=" + DrpState.SelectedValue + "";
            oo.FillDropDown_withValue(sql, DrpCity, "CityName", "id");
            using (var objBll = new BLL())
            {
                try
                {
                    objBll.loadDefaultvalue("City", DrpCity);
                }
                catch
                {
                    // ignored
                }
            }


            DisplayInformation();
        }

    }


    private void loadCountry()
    {
        sql = "Select CountryName,id from countryMaster";
        BAL.objBal.FillDropDown_withValue(sql, DrpCountry, "CountryName", "id");
    }

    private void loadState()
    {

        sql = "Select StateName,Id from StateMaster where CountryId='" + DrpCountry.SelectedValue.ToString() + "'";
        BAL.objBal.FillDropDown_withValue(sql, DrpState, "StateName", "id");
        if (DrpState.Items.Count == 0)
        {
            DrpState.Items.Add(new ListItem("Other", "0"));
        }
    }

    private void loadCity()
    {
        sql = "Select CityName,id from CityMaster where StateId='" + DrpState.SelectedValue.ToString() + "'";
        BAL.objBal.FillDropDown_withValue(sql, DrpCity, "CityName", "id");
        if (DrpCity.Items.Count == 0)
        {
            DrpCity.Items.Add(new ListItem("Other", "0"));
        }
    }



    public void DisplayInformation()
    {

        sql = "select PrincipalName,CollegeName,CollegeShortNa,CollegeAdd1,CollegeAdd2,CountryName,StateId,CityId,Phone,Affiliatedto,Slogan, AdminEmail, AdminContact, PrincpalSign, ClassTeacherSign, ";
        sql = sql + " ContactPerson,AffiliationNo,SchoolNo,UpTTno,WebSite,Email,CologeLogoPath,CollegeEstablishDate ,";
        sql = sql + " DATEPART(year,CollegeEstablishDate) yy,left(CONVERT(nvarchar,CollegeEstablishDate,106),3) as mm, DATEPART(dd,CollegeEstablishDate) dd, ";
        sql = sql + " isWebHook,FeeReceiptRemark1,FeeReceiptRemark2,BranchName from CollegeMaster where BranchCode=" + Session["BranchCode"] + "";
        if (oo.Duplicate(sql))
        {
            TextBox3.Text = oo.ReturnTag(sql, "BranchName");
            txtCollegeName.Text = oo.ReturnTag(sql, "collegeName");
            txtCollegeShortNa.Text = oo.ReturnTag(sql, "CollegeShortNa");
            txtAdd1.Text = oo.ReturnTag(sql, "CollegeAdd1");
            TextBox1.Text = oo.ReturnTag(sql, "FeeReceiptRemark1");
            TextBox2.Text = oo.ReturnTag(sql, "FeeReceiptRemark2");
            txtAdd2.Text = oo.ReturnTag(sql, "CollegeAdd2");
            txtprincipal.Text = oo.ReturnTag(sql, "PrincipalName");

            txtPhone.Text = oo.ReturnTag(sql, "Phone");
            txtAffiliationNo.Text = oo.ReturnTag(sql, "AffiliationNo");
            txtSchoolNo.Text = oo.ReturnTag(sql, "SchoolNo");
            txtUPTTNo.Text = oo.ReturnTag(sql, "UpTTno");
            txtWebSite.Text = oo.ReturnTag(sql, "WebSite");
            txtEmail.Text = oo.ReturnTag(sql, "Email");
            DrpDate.Text = oo.ReturnTag(sql, "dd");
            drpMonth.Text = oo.ReturnTag(sql, "mm");
            drpYear.Text = oo.ReturnTag(sql, "yy");
            txtAdminEmail.Text = oo.ReturnTag(sql, "AdminEmail");
            txtAdminContact.Text = oo.ReturnTag(sql, "AdminContact");
            txtAffilatedto.Text = oo.ReturnTag(sql, "Affiliatedto");
            txtSlogan.Text = oo.ReturnTag(sql, "Slogan");
            rdoWebhook.SelectedValue = oo.ReturnTag(sql, "isWebHook");
            if (oo.ReturnTag(sql, "CologeLogoPath").ToString() != "")
            {
                Image1.Visible = true;
                Image1.ImageUrl = oo.ReturnTag(sql, "CologeLogoPath") + "?print=" + DateTime.Now;
            }
            if (oo.ReturnTag(sql, "ClassTeacherSign").ToString() == "")
            {
                Image3.Visible = false;
            }
            else
            {
                Image3.Visible = true;
                Image3.ImageUrl = oo.ReturnTag(sql, "ClassTeacherSign") + "?print=" + DateTime.Now;
            }
            if (oo.ReturnTag(sql, "PrincpalSign").ToString() == "")
            {
                Image2.Visible = false;
            }
            else
            {
                Image2.Visible = true;
                Image2.ImageUrl = oo.ReturnTag(sql, "PrincpalSign") + "?print=" + DateTime.Now;
            }
            DrpCountry.SelectedValue = oo.ReturnTag(sql, "CountryName");

            DrpState.SelectedValue = oo.ReturnTag(sql, "stateId");
            string sql1 = sql;
            loadCity();
            DrpCity.SelectedValue = oo.ReturnTag(sql1, "cityId");


        }

    }


    protected void Button1_Click(object sender, EventArgs e)
    {

    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        string qry = string.Empty;
        sql = "select * from CollegeMaster where CollegeShortNa=" + txtCollegeShortNa.Text.Trim() + "";
        if (oo.Duplicate(sql))
        {
            oo.MessageBox("Short Name already exists!", this.Page);
        }
        if (DrpState.SelectedItem.Text == "<--Select-->" || DrpCity.SelectedItem.Text == "<--Select-->")
        {
            oo.MessageBox("Please <--Select--> Condition!", this.Page);
        }
        else
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "CollegeMastProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@CollegeName", txtCollegeName.Text.Trim());
            cmd.Parameters.AddWithValue("@CollegeShortNa", txtCollegeShortNa.Text.Trim());
            cmd.Parameters.AddWithValue("@CollegeAdd1", txtAdd1.Text.Trim());
            cmd.Parameters.AddWithValue("@CollegeAdd2", txtAdd2.Text.Trim());
            cmd.Parameters.AddWithValue("@StateId", DrpState.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@CityId", DrpCity.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@phon", txtPhone.Text.Trim());
            cmd.Parameters.AddWithValue("@PrincipalName", txtprincipal.Text.Trim());
            cmd.Parameters.AddWithValue("@contacPer", " ");
            cmd.Parameters.AddWithValue("@AffiliationNo", txtAffiliationNo.Text.ToString());
            cmd.Parameters.AddWithValue("@SchoolNo", txtSchoolNo.Text.Trim());
            cmd.Parameters.AddWithValue("@UpTno", txtUPTTNo.Text.Trim());
            cmd.Parameters.AddWithValue("@WebSite", txtWebSite.Text.Trim());
            cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
            cmd.Parameters.AddWithValue("@CountryName", DrpCountry.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@CollegeEstablishDate", drpYear.SelectedItem.ToString() + "/" + drpMonth.SelectedItem.ToString() + "/" + DrpDate.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@Affiliatedto", txtAffilatedto.Text.Trim());
            cmd.Parameters.AddWithValue("@Slogan", txtSlogan.Text.Trim());
            cmd.Parameters.AddWithValue("@FeeReceiptRemark1", TextBox1.Text.Trim());
            cmd.Parameters.AddWithValue("@FeeReceiptRemark2", TextBox2.Text.Trim());
            cmd.Parameters.AddWithValue("@isWebHook", rdoWebhook.SelectedValue);
            cmd.Parameters.AddWithValue("@AdminEmail", txtAdminEmail.Text.Trim());
            cmd.Parameters.AddWithValue("@AdminContact", txtAdminContact.Text.Trim());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"]);
            cmd.Parameters.AddWithValue("@BranchName", TextBox3.Text);
            //------Logo
            string base64std = hflogo.Value;
            string logopath = "";
            string fileName = "";

            if (base64std != string.Empty)
            {
                string sessionName = Session["SessionName"].ToString();
                logopath = @"~/Uploads/CollegeLogo/";
                fileName = "logo_" + Session["BranchCode"].ToString() + ".jpg";

                using (FileStream fs = new FileStream(Server.MapPath((logopath + fileName)), FileMode.Create))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs))
                    {
                        byte[] data = Convert.FromBase64String(base64std);
                        bw.Write(data);
                        bw.Close();
                    }
                }

            }

            logopath = logopath + fileName;
            cmd.Parameters.AddWithValue("@CologeLogoPath", logopath);

            //------PrincpalSign
            string base64PrincpalSign = hdnPrincpalSign.Value;
            string PrincpalSignpath = "";
            string PrincpalSignfileName = "";

            if (base64PrincpalSign != string.Empty)
            {
                string sessionName = Session["SessionName"].ToString();
                PrincpalSignpath = @"~/Uploads/CollegeLogo/";
                PrincpalSignfileName = "PrincpalSign_" + Session["BranchCode"].ToString() + ".jpg";

                using (FileStream fs = new FileStream(Server.MapPath((PrincpalSignpath + PrincpalSignfileName)), FileMode.Create))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs))
                    {
                        byte[] data = Convert.FromBase64String(base64PrincpalSign);
                        bw.Write(data);
                        bw.Close();
                    }
                }

            }

            PrincpalSignpath = PrincpalSignpath + PrincpalSignfileName;
            cmd.Parameters.AddWithValue("@PrincpalSign", PrincpalSignpath);

            //------ClassTeacherSign
            string base64ClassTeacherSign = hdnClassTeacherSign.Value;
            string ClassTeacherSignpath = "";
            string ClassTeacherSignfileName = "";

            if (base64ClassTeacherSign != string.Empty)
            {
                string sessionName = Session["SessionName"].ToString();
                ClassTeacherSignpath = @"~/Uploads/CollegeLogo/";
                ClassTeacherSignfileName = "ClassTeacherSign_" + Session["BranchCode"].ToString() + ".jpg";

                using (FileStream fs = new FileStream(Server.MapPath((ClassTeacherSignpath + ClassTeacherSignfileName)), FileMode.Create))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs))
                    {
                        byte[] data = Convert.FromBase64String(base64ClassTeacherSign);
                        bw.Write(data);
                        bw.Close();
                    }
                }

            }

            ClassTeacherSignpath = ClassTeacherSignpath + ClassTeacherSignfileName;
            cmd.Parameters.AddWithValue("@ClassTeacherSign", ClassTeacherSignpath);
            sql = "select count(*)cnt from CollegeMaster where BranchCode=" + Session["BranchCode"] + "";
            if (oo.ReturnTag(sql, "cnt") == "0")
            {
                cmd.Parameters.AddWithValue("@Action", "insert");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Action", "update");
            }

            con.Open();
            cmd.ExecuteNonQuery();
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "S");
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this.Page, GetType(), "reload", "window.location.reload(true);", true);
        }

    }
    protected void DrpState_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadCity();
    }
    protected void drpYear_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        drpYear.Visible = true;
        drpMonth.Visible = true;
        DrpDate.Visible = true;

    }



    public void PermissionGrant(int update1, LinkButton LUpdate)
    {

        if (update1 == 1)
        {
            LUpdate.Enabled = true;
        }
        else
        {
            LUpdate.Enabled = false;
        }


    }
    public void CheckValueADDDeleteUpdate()
    {
        sql = " select LoginId,LoginName,Pass,SessionId,BranchId,LT.LoginTypeName,ltb.add1 as add1,ltb.delete1 as delete1,ltb.update1 as update1 from LoginTab LTb";
        sql = sql + " left join LoginType Lt on LTb.LoginTypeId=Lt.LoginTypeId where LT.LoginTypeName='" + Session["Logintype"] + "' and LTb.LoginName='" + Session["LoginName"] + "' and LTb.BranchId=" + Session["BranchCode"] + "";
#pragma warning disable 168
        int a, u, d;
#pragma warning restore 168

        u = Convert.ToInt32(oo.ReturnTag(sql, "update1"));


        PermissionGrant(u, (LinkButton)LinkButton4);
    }


    protected void DrpCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadState();
        loadCity();
    }

}
