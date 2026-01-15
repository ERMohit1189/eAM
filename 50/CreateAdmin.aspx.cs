using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System;
using System.Net.Mail;
using c4SmsNew;
using System.IO;
using System.Collections.Generic;
using System.Web.UI;

public partial class CreateAdmin : Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        string sql1 = "";
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);

        if (!IsPostBack)
        {
            sql = "select ltrim(rtrim(CountryName)) as CountryName, id from CountryMaster ";
            oo.FillDropDown_withValue(sql, DrpCountry, "CountryName", "id");
            DrpCountry.SelectedValue = "1";

            sql = "select ltrim(rtrim(StateName)) as StateName, id from StateMaster where CountryId=" + DrpCountry.SelectedValue + "";
            oo.FillDropDown_withValue(sql, DrpPreState, "StateName", "id");
            using (var objBll = new BLL())
            {
                try
                {
                    objBll.loadDefaultvalue("State", DrpPreState);
                }
                catch
                {
                    // ignored
                }
            }

            sql = "select ltrim(rtrim(CityName)) as CityName, id from CityMaster  where stateid=" + DrpPreState.SelectedValue + "";
            oo.FillDropDown_withValue(sql, DrpPreCity, "CityName", "id");
            using (var objBll = new BLL())
            {
                try
                {
                    objBll.loadDefaultvalue("City", DrpPreCity);
                }
                catch
                {
                    // ignored
                }
            }


            sql = "select ltrim(rtrim(CountryName)) as CountryName, id from CountryMaster ";
            oo.FillDropDown_withValue(sql, DrpCountry0, "CountryName", "id");

            sql = "select ltrim(rtrim(StateName)) as StateName, id from StateMaster";
            oo.FillDropDown_withValue(sql, DrpPreState0, "StateName", "id");

            sql = "select ltrim(rtrim(CityName)) as CityName, id from CityMaster";
            oo.FillDropDown_withValue(sql, DrpPreCity0, "CityName", "id");

            sql = "Select BranchId, BranchName from Branchtab";
            var dt = oo.Fetchdata(sql);
            ddlBranch.DataSource = dt;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "BranchId";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, new ListItem("<--Select Branch-->", ""));

            DrpBranchName0.DataSource = dt;
            DrpBranchName0.DataTextField = "BranchName";
            DrpBranchName0.DataValueField = "BranchId";
            DrpBranchName0.DataBind();

            Session();
            sql1 = " select YEAR(GETDATE())+1 as yy ";
            sql1 = oo.ReturnTag(sql1, "yy");
            loadAdminInformation();
        }
    }
    protected void Session()
    {
        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@Queryfor", "S"));
        param.Add(new SqlParameter("@BranchCode", ddlBranch.SelectedValue));
        DataSet ds = new DataSet();
        ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("Get_GenralInfo", param);
        DrpSessionName.DataSource = ds.Tables[1];
        DrpSessionName.DataTextField = "SessionName";
        DrpSessionName.DataValueField = "SessionName";
        DrpSessionName.DataBind();
        sql = "select top(1) SessionName from SessionMaster where BranchCode="+ ddlBranch.SelectedValue+ " and convert(date, GETDATE()) between convert(date, FromDate) and convert(date, ToDate) order by SessionId desc";
        if (oo.Duplicate(sql))
        {
            DrpSessionName.SelectedValue = oo.ReturnTag(sql, "SessionName");
        }
        else
        {
            DrpSessionName.SelectedIndex = (DrpSessionName.Items.Count - 1);
        }
        DrpSessionName.Items.Insert(0, new ListItem("<--Select Session-->", ""));
    }
    public void loadAdminInformation()
    {
        sql = "Select  ROW_NUMBER() OVER (ORDER BY Id desc) AS SrNo,Id,Name,FatherName,ContactNo,Email,Address,State,City,Zip,UserId,Password,SessionName,BranchCode,";
        sql = sql + " Country,Designation,CASE WHEN ISNULL(lt.IsActive,1)=1 THEN 'Active' ELSE 'Inactive' END Status from NewAdminInformation nai INNER JOIN dbo.LoginTab lt ON lt.LoginName=nai.UserId  where id is not null and nai.branchCode=" + ddlBranch.SelectedValue + "";
        GridView1.DataSource = oo.GridFill(sql);
        GridView1.DataBind();

        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            HtmlInputText txtPassword = (HtmlInputText)GridView1.Rows[i].FindControl("txtPassword");
            txtPassword.Attributes["type"] = "password";
            Label Username = (Label)GridView1.Rows[i].FindControl("Label10");
            LinkButton LinkButton6 = (LinkButton)GridView1.Rows[i].FindControl("LinkButton6");
            sql = "select (select count(*) from DefaultSelectedValue where BranchCode=" + ddlBranch.SelectedValue + " and LoginName='" + Username.Text + "')+ ";
            sql = sql+" (select count(*) from StudentOfficialDetails where BranchCode = " + ddlBranch.SelectedValue+" and LoginName = '"+Username.Text+ "') + ";
            sql = sql + " (select count(*) from ClassMaster where BranchCode = " + ddlBranch.SelectedValue+" and LoginName = '"+Username.Text+"') counts";
            if (oo.ReturnTag(sql, "counts")!="0")
            {
                LinkButton6.Text = "<i class='fa fa-lock'></i>";
                LinkButton6.Enabled = false;
            }
        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session();
        loadAdminInformation();
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        sql = "Select LoginName from LoginTab where LoginName='" + txtUserId.Text.ToString().Replace(" ", "") + "' and BranchId=" + ddlBranch.SelectedValue + "";
        if (oo.Duplicate(sql))
        {
            oo.msgbox(Page, msgbox, "Username " + txtUserId.Text + " already exists.", "A");
            txtUserId.Focus();
            txtUserId.Text = "";
            return;
        }
        if (txtPassword.Text.Trim() == "" && txtPassword.Text.Trim() == "")
        {
            oo.msgbox(Page, msgbox, "Please enter Password", "A");
            return;
        }
        if (txtPassword.Text.Trim() != txtPassword.Text.Trim())
        {
            oo.msgbox(Page, msgbox, "Please enter confirm Password", "A");
            return;
        }
        int count = 0;

        string sql1 = "";
        sql1 = "Select Email from NewAdminInformation where Email='" + txtEmail.Text + "'";
        sql = "Select LoginName from LoginTab where LoginName='" + txtUserId.Text.ToString().Replace(" ", "") + "'";
        if (oo.Duplicate(sql))
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Username already exists. Please use a different username.", "A");
        }


        else if (oo.Duplicate(sql1))
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Email-id already exist!", "A");
        }


        else if (ddlBranch.SelectedValue == "" || DrpPreState.SelectedItem.ToString() == "<--Select-->" || DrpPreCity.SelectedItem.ToString() == "<--Select-->")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please <--Select--> Condition", "A");
        }
        else
        {

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "NewAdminCreateProce";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@LoginName", txtUserId.Text.ToString().Replace(" ", ""));
            cmd.Parameters.AddWithValue("@Pass", txtPassword.Text.ToString());
            cmd.Parameters.AddWithValue("@Name", txtName.Text.ToString());
            cmd.Parameters.AddWithValue("@FatherName", txtFatherName.Text.ToString());
            cmd.Parameters.AddWithValue("@ContactNo", txtContactNo.Text.ToString());
            cmd.Parameters.AddWithValue("@Email", txtEmail.Text.ToString());
            cmd.Parameters.AddWithValue("@Address", txtAddress.Text.ToString());
            cmd.Parameters.AddWithValue("@State", DrpPreState.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@City", DrpPreCity.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@Zip", "NULL");
            cmd.Parameters.AddWithValue("@DisplayName", txtDisplayName.Text.Trim());
            cmd.Parameters.AddWithValue("@SessionName", DrpSessionName.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@BranchCode", ddlBranch.SelectedValue);
            cmd.Parameters.AddWithValue("@Country", DrpCountry.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@Designation", drpDesignation.SelectedValue.ToString().Trim());

            cmd.Parameters.AddWithValue("@isSysUser", RadioButtonList1.SelectedValue);
            cmd.Parameters.AddWithValue("@macId", txtMacId.Text.Trim());
            cmd.Parameters.AddWithValue("@motherboardno", txtSerialno.Text.Trim());

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                SendNotificationToSuperAdmin(txtUserId.Text.ToString().Replace(" ", ""), txtPassword.Text);
                try
                {
                    string userid = txtUserId.Text.ToString().Replace(" ", "");
                    string passward = txtPassword.Text;
                    string name = txtName.Text;
                    string contactno = txtContactNo.Text;
                    SendNewUserSms(name, userid, passward, contactno);
                }
                catch (Exception) { }
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");

                string filePath = "";
                string fileName = "";

                string base64std = hdAdminPhoto.Value;
                if (base64std != string.Empty)
                {
                    filePath = @"./Uploads/AdminPhoto/";
                    fileName = txtUserId.Text.ToString().Replace(" ", "") + ".jpg";

                    using (FileStream fs = new FileStream(Server.MapPath("." + filePath + fileName), FileMode.Create))
                    {
                        using (BinaryWriter bw = new BinaryWriter(fs))
                        {
                            byte[] data = Convert.FromBase64String(base64std);
                            bw.Write(data);
                            bw.Close();
                        }
                    }

                    sql = "Update NewAdminInformation Set PhotoPath='" + (filePath + fileName) + "' , PhotoName='" + fileName + "'";
                    sql = sql + " where Userid='" + txtUserId.Text.ToString().Replace(" ", "") + "' and BranchCode=" + ddlBranch.SelectedValue + "";
                    count = BAL.objBal.Insert_Update_Delete1(sql);

                    hdAdminPhoto.Value = null;
                }
                oo.ClearControls(this.Page);
                txtUserId.Text = "";
                txtPassword.Text = "";
                loadAdminInformation();
            }
            catch (SqlException ee)
            {
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, ee.Message.ToString(), "W");
            }
        }

    }
    public void SendNewUserSms(string Name, string UserId, string Passward, string FmobileNo)
    {
        sql = "Select HitValue from SMSActivateDeactivate where BranchCode=" + ddlBranch.SelectedValue + "";
        if (oo.ReturnTag(sql, "HitValue") != "")
        {
            if (oo.ReturnTag(sql, "HitValue") == "true")
            {
                SMSAdapterNew sadpNew = new SMSAdapterNew();
                string mess = "";
                string collegeTitle = "";

                sql = "Select CollegeShortNa from CollegeMaster where BranchCode=" + ddlBranch.SelectedValue + "";
                string CollegeShortNa = oo.ReturnTag(sql, "CollegeShortNa");

                sql = "Select Password from EmpployeeOfficialDetails where";
                sql = sql + " Ecode='" + Application["Ecode"].ToString().Trim() + "' and SessionName='" + DrpSessionName.SelectedValue.ToString() + "' and BranchCode=" + ddlBranch.SelectedValue + "";

                string Ecode = Application["Ecode"].ToString().Trim();
                string EmployeePassword = oo.ReturnTag(sql, "Password");

                mess = "Congrats! " + DrpTitle.SelectedItem.ToString() + " " + Name + ", you've registered successfully with our ERP Application. Your Userid: " + UserId + " and Password: " + Passward + "";

                string sms_response = "";
                sql = "Select CollegeShortNa  from CollegeMaster where BranchCode=" + ddlBranch.SelectedValue + "";
                collegeTitle = oo.ReturnTag(sql, "CollegeShortNa");

                if (FmobileNo != "")
                {

                    sql = "Select SmsSent From SmsEmailMaster where Id='19'";
                    if (oo.ReturnTag(sql, "SmsSent").Trim() == "true")
                    {
                        sms_response = sadpNew.Send(mess, FmobileNo, "");
                    }
                }
            }
        }
    }
    protected void DrpSessionName_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void txtUserId_TextChanged(object sender, EventArgs e)
    {
        sql = "Select LoginName from LoginTab where LoginName='" + txtUserId.Text.ToString().Replace(" ", "") + "' and BranchId=" + ddlBranch.SelectedValue + "";
        if (oo.Duplicate(sql))
        {
            oo.msgbox(Page, msgbox, "That username " + txtUserId.Text + " already exists.", "A");
            txtUserId.Focus();
            txtUserId.Text = "";
        }
    }
    protected void LinkButton7_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label36");
        string ss = lblId.Text;
        lblID.Text = ss;
        sql = "Select ISNULL(DisplayName,(Case When CHARINDEX(' ',Name)=0 then Name Else Left(Name,CHARINDEX(' ',Name)) End)) as DisplayName,ROW_NUMBER() OVER (ORDER BY Id desc) AS ";
        sql = sql + " SrNo,Id,Name,FatherName,ContactNo,Email,Address,State,City,Zip,";
        sql = sql + " UserId,Password,SessionName,BranchCode,Country,PhotoPath,Designation,isSysUser,macId,motherboardno from NewAdminInformation";
        sql = sql + " where Id=" + ss + " and BranchCode=" + ddlBranch.SelectedValue + "";
        txtName0.Text = oo.ReturnTag(sql, "Name");
        txtFatherName0.Text = oo.ReturnTag(sql, "FatherName");
        txtContactNo0.Text = oo.ReturnTag(sql, "ContactNo");

        txtEmail0.Text = oo.ReturnTag(sql, "Email");
        txtAddress0.Text = oo.ReturnTag(sql, "Address");
        try
        {
            DrpCountry0.SelectedItem.Text = oo.ReturnTag(sql, "Country");
        }
        catch (Exception) { }
        try
        {
            DrpPreState0.SelectedItem.Text = oo.ReturnTag(sql, "State");
        }
        catch (Exception) { }
        try
        {
            DrpPreCity0.SelectedItem.Text = oo.ReturnTag(sql, "City");
        }
        catch (Exception) { }

        try
        {
            drpDesignation0.SelectedValue = oo.ReturnTag(sql, "Designation");
        }
        catch (Exception) { }
        try
        {
            RadioButtonList4.SelectedIndex = oo.ReturnTag(sql, "isSysUser").ToString() == "True" ? 1 : 0;
        }
        catch (Exception) { }

        txtMacId0.Text = oo.ReturnTag(sql, "macId");
        txtSerialno0.Text = oo.ReturnTag(sql, "motherboardno");

        if (RadioButtonList4.SelectedIndex == 1)
        {
            divSystem0.Visible = true;
            divSystem01.Visible = true;
        }
        else
        {
            divSystem0.Visible = false;
            divSystem01.Visible = false;
        }

        txtUserId0.Text = oo.ReturnTag(sql, "UserId");
        txtPassword0.Text = oo.ReturnTag(sql, "Password");
        txtDisplayName0.Text = oo.ReturnTag(sql, "DisplayName");

        if (oo.ReturnTag(sql, "PhotoPath") != string.Empty)
        {
            imgAvatars1.ImageUrl = "." + oo.ReturnTag(sql, "PhotoPath").Replace("~", ".");
        }
        else
        {
            imgAvatars1.ImageUrl = "~/img/user-pic/user-pic.jpg";
        }

        sql = "Select IsActive from Logintab where LoginName='" + txtUserId0.Text + "' and BranchId=" + ddlBranch.SelectedValue + "";
        bool status;
        if (oo.ReturnTag(sql, "IsActive") == "" || Convert.ToBoolean(oo.ReturnTag(sql, "IsActive")) == true)
        {
            status = true;
        }
        else
        {
            status = false;
        }
        if (status == true)
        {
            RadioButtonList2.SelectedValue = "1";
        }
        else
        {
            RadioButtonList2.SelectedValue = "0";
        }
        try
        {
            sql = "Select BranchName from BranchTab where BranchId=" + ddlBranch.SelectedValue + "";
            DrpBranchName0.Text = oo.ReturnTag(sql, "BranchName");

        }

        catch (Exception) { }

        Panel1_ModalPopupExtender.Show();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "hidePassword3();", true);
    }
    protected void LinkButton6_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label37");
        string ss = lblId.Text;
        lblvalue.Text = ss.ToString();
        Panel2_ModalPopupExtender.Show();
        lnkNo.Focus();
    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        string Sna = "", Email = "";

        sql = "Select Userid from NewAdminInformation where id='" + lblID.Text + "' and BranchCode=" + ddlBranch.SelectedValue + "";
        string loginname = BAL.objBal.ReturnTag(sql, "Userid");

        int count = 0;
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "NewAdminCreateUpdateProce";
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Connection = con;
        cmd.Parameters.AddWithValue("@id", lblID.Text);
        cmd.Parameters.AddWithValue("@LoginName", txtUserId0.Text.ToString());
        cmd.Parameters.AddWithValue("@Pass", txtPassword0.Text.ToString());
        cmd.Parameters.AddWithValue("@Name", txtName0.Text.ToString());
        cmd.Parameters.AddWithValue("@FatherName", txtFatherName0.Text.ToString());
        cmd.Parameters.AddWithValue("@ContactNo", txtContactNo0.Text.ToString());
        cmd.Parameters.AddWithValue("@Email", txtEmail0.Text.ToString());
        cmd.Parameters.AddWithValue("@Address", txtAddress0.Text.ToString());
        cmd.Parameters.AddWithValue("@State", DrpPreState0.SelectedItem.ToString());
        cmd.Parameters.AddWithValue("@City", DrpPreCity0.SelectedItem.ToString());
        cmd.Parameters.AddWithValue("@Zip", "NULL");
        cmd.Parameters.AddWithValue("@DisplayName", txtDisplayName0.Text.Trim());
        bool status;
        if (RadioButtonList2.SelectedIndex == 0)
        {
            status = true;
        }
        else
        {
            status = false;
        }
        cmd.Parameters.AddWithValue("@IsActive", status);

        cmd.Parameters.AddWithValue("@SessionName", DrpSessionName.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@BranchCode", ddlBranch.SelectedValue);

        cmd.Parameters.AddWithValue("@Designation", drpDesignation0.SelectedValue.ToString().Trim());


        cmd.Parameters.AddWithValue("@isSysUser", RadioButtonList4.SelectedValue);
        cmd.Parameters.AddWithValue("@macId", txtMacId0.Text.Trim());
        cmd.Parameters.AddWithValue("@motherboardno", txtSerialno0.Text.Trim());

        string ss = "";
        ss = ddlBranch.SelectedValue;
        cmd.Parameters.AddWithValue("@Country", DrpCountry0.SelectedItem.ToString());

        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            sql = "select  email,firstName+' '+MiddleName+' '+LastName as Sname from  StudentGenaralDetail  where  StEnRCode='" + lblID.Text.ToString() + "' and BranchCode=" + ddlBranch.SelectedValue + "";
            Sna = oo.ReturnTag(sql, "Sname");
            Email = oo.ReturnTag(sql, "Email");
            if (Email != "")
            {
                ChangePasswordEmail(ddlBranch.SelectedItem.Text, Sna, lblID.Text.Trim(), txtPassword0.Text.ToString().Trim(), Email);
            }

            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "S");

            string filePath = "";
            string fileName = "";

            string base64std = hdAdminPhotoPanel.Value;
            if (base64std != string.Empty)
            {
                filePath = @"./Uploads/AdminPhoto/";
                fileName = txtUserId0.Text.Trim() + ".jpg";

                using (FileStream fs = new FileStream(Server.MapPath("." + filePath + fileName), FileMode.Create))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs))
                    {
                        byte[] data = Convert.FromBase64String(base64std);
                        bw.Write(data);
                        bw.Close();
                    }
                }

                sql = "Update NewAdminInformation Set PhotoPath='" + (filePath + fileName) + "' , PhotoName='" + fileName + "'";
                sql = sql + " where Userid='" + txtUserId0.Text.ToString() + "' and BranchCode=" + ddlBranch.SelectedValue + "";
                count = BAL.objBal.Insert_Update_Delete1(sql);

                hdAdminPhotoPanel.Value = null;
            }

            oo.ClearControls(this.Page);
            loadAdminInformation();
        }
        catch (SqlException ee) { Label9.Text = ee.Message.ToString(); }
    }
    protected void LinkButton5_Click(object sender, EventArgs e)
    {

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {

    }
    protected void Button8_Click(object sender, EventArgs e)
    {

    }
    public void SendNotificationToSuperAdmin(string UID, string Password)
    {
        string Mess = "", collegeTitle = "";

        sql = "Select CollegeShortNa  from CollegeMaster where BranchCode=" + ddlBranch.SelectedValue + "";
        collegeTitle = oo.ReturnTag(sql, "CollegeShortNa");
        string logopath = "";
        sql = "select  CologeLogoPath from  CollegeMaster where BranchCode=" + ddlBranch.SelectedValue + "";
        logopath = oo.ReturnTag(sql, "CologeLogoPath");
        int l = 0;
        l = Convert.ToInt32(logopath.Length.ToString());
        Mess = "  <table > ";
        Mess = Mess + "<tr>";
        Mess = Mess + "<td>" + "<img src='http://www.eam.co.in/images/eammail.png' height='88' width='80' >";
        Mess = Mess + "</td>";
        Mess = Mess + "</tr>";

        Mess = Mess + " <tr>";
        Mess = Mess + " <td>";
        Mess = Mess + "   <hr/></td>";
        Mess = Mess + " </tr>";

        Mess = Mess + " <tr>";
        Mess = Mess + " <td>";
        Mess = Mess + " This message was sent from a notification-only E-mail address.</td>";
        Mess = Mess + "</tr>";




        Mess = Mess + "<tr>";
        Mess = Mess + "   <td>";
        Mess = Mess + " Please do not reply to this message.</td>";
        Mess = Mess + " </tr>";


        Mess = Mess + " <tr>";
        Mess = Mess + " <td >";
        Mess = Mess + "  <hr/></td>";
        Mess = Mess + " </tr>";



        Mess = Mess + " <tr>";
        Mess = Mess + "  <td >";
        Mess = Mess + "Dear ";
        Mess = Mess + "             " + txtName.Text + ",";
        Mess = Mess + "<br/>";
        Mess = Mess + "  Greetings from " + collegeTitle + "!</td>";
        Mess = Mess + " </tr>";


        Mess = Mess + " <tr>";
        Mess = Mess + " <td ><br>";
        Mess = Mess + " </td>";
        Mess = Mess + " </tr>";

        Mess = Mess + " <tr>";
        Mess = Mess + " <td>";
        Mess = Mess + "      Your Login Details are as follows: ";
        Mess = Mess + " </td>";
        Mess = Mess + " </tr>";



        Mess = Mess + " <tr>";
        Mess = Mess + " <td ><hr/>";
        Mess = Mess + " </td>";
        Mess = Mess + " </tr>";

        Mess = Mess + " <tr>";
        Mess = Mess + " <td >";
        Mess = Mess + "User ID  :  " + UID;
        Mess = Mess + " </td>";
        Mess = Mess + " </tr >";

        Mess = Mess + " <tr>";
        Mess = Mess + " <td >";
        Mess = Mess + "Password :  " + Password;
        Mess = Mess + " </td>";
        Mess = Mess + " </tr >";

        Mess = Mess + " <tr>";
        Mess = Mess + " <td >";
        Mess = Mess + " <hr/>";
        Mess = Mess + " </td>";
        Mess = Mess + " </tr>";

        Mess = Mess + " <tr>";
        Mess = Mess + " <td ></td>";

        Mess = Mess + " </tr>";
        Mess = Mess + " <tr>";
        Mess = Mess + " <td>";
        Mess = Mess + "   &nbsp;</td>";
        Mess = Mess + " <td>";
        Mess = Mess + "   &nbsp;</td>";
        Mess = Mess + " </tr>";
        Mess = Mess + " <tr>";
        Mess = Mess + "  <td colspan='2'>";
        Mess = Mess + " <tr>";

        Mess = Mess + " <hr/></td>";
        Mess = Mess + "   </tr>";
        Mess = Mess + "  <tr>";
        Mess = Mess + "     <td colspan='2'>";
        Mess = Mess + "        Warm Regards&nbsp;";
        Mess = Mess + "   </td>";
        Mess = Mess + " </tr>";
        Mess = Mess + " <tr>";
        Mess = Mess + "   <td>";
        string schName = "";
        sql = "Select CollegeName from CollegeMaster where BranchCode=" + ddlBranch.SelectedValue + "";
        schName = oo.ReturnTag(sql, "CollegeName");

        Mess = Mess + "  " + schName + "</td>";
        Mess = Mess + " <td>";
        Mess = Mess + "    &nbsp;</td>";
        Mess = Mess + " </tr>";
        Mess = Mess + " <tr>";
        Mess = Mess + "   <td>";

        string CityName = "";
        sql = "Select Case When ISNUMERIC(CityId)!=1 Then CityId";
        sql = sql + " Else (Select CityName from CityMaster where Id=(Select CityId from CollegeMaster)) End CityName from CollegeMaster where BranchCode=" + ddlBranch.SelectedValue + "";
        CityName = oo.ReturnTag(sql, "CityName");


        Mess = Mess + "   " + CityName + "</td>";
        Mess = Mess + " <td>";
        Mess = Mess + "   &nbsp;</td>";
        Mess = Mess + " </tr>";
        Mess = Mess + " <tr>";
        Mess = Mess + "    <td>";

        string email = "";
        sql = "Select Email from CollegeMaster where BranchCode=" + ddlBranch.SelectedValue + "";

        email = oo.ReturnTag(sql, "Email");
        Mess = Mess + "   " + email + "</td>";
        Mess = Mess + "  <td>";
        Mess = Mess + "    &nbsp;</td>";
        Mess = Mess + " </tr>";
        Mess = Mess + " <tr>";
        Mess = Mess + " <td>";
        Mess = Mess + "  &nbsp;</td>";
        Mess = Mess + " <td>";
        Mess = Mess + "  &nbsp;</td>";
        Mess = Mess + "  </tr>";
        Mess = Mess + " </table>";
        EmailSending(Mess, "" + collegeTitle + " : Admin Panel Login Details", txtEmail.Text);
    }
    public bool EmailSending(string Mess, string subjectParameter, string TOEmailID)
    {
        string ss = "";
        bool send = false;
        MailMessage mail = new MailMessage();
        mail.To.Add(TOEmailID);//to ID

        mail.From = new MailAddress("donotreply@eam.co.in");
        mail.Subject = subjectParameter;
        mail.Body = Mess;
        mail.IsBodyHtml = true;
        SmtpClient smtp = new SmtpClient();
        smtp.Host = "smtp.gmail.com";
        smtp.Port = 587;
        smtp.Credentials = new System.Net.NetworkCredential("donotreply@eam.co.in", "reNply_33@9D");//from id
        smtp.EnableSsl = true;
        try
        {
            smtp.Send(mail);
            send = true;
        }
        catch (Exception) { }
        return send;
    }
    protected void lnkYes_Click(object sender, EventArgs e)
    {
        string lna = "", pas = "";
        sql = "select UserId,Password  from  NewAdminInformation where id=" + lblvalue.Text + " and BranchCode=" + ddlBranch.SelectedValue + "";
        lna = oo.ReturnTag(sql, "UserId");
        pas = oo.ReturnTag(sql, "Password");

        sql = "Delete from NewAdminInformation where id=" + lblvalue.Text + " and BranchCode=" + ddlBranch.SelectedValue + "";
        oo.ProcedureDatabase(sql);
        sql = "Delete from LoginTab where LoginName='" + lna + "'  and  Pass='" + pas + "'  and LoginTypeId=2 and BranchId=" + ddlBranch.SelectedValue + "";

        oo.ProcedureDatabase(sql);

        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Deleted successfully.", "S");
        loadAdminInformation();
    }
    public void SendNotificationToAdminModup(string UID, string Password)
    {
        string Mess = "", collegeTitle = "";

        sql = "Select CollegeShortNa  from CollegeMaster and BranchCode=" + ddlBranch.SelectedValue + "";
        collegeTitle = oo.ReturnTag(sql, "CollegeShortNa");
        string logopath = "";
        sql = "select  CologeLogoPath from  CollegeMaster and BranchCode=" + ddlBranch.SelectedValue + "";
        logopath = oo.ReturnTag(sql, "CologeLogoPath");
        int l = 0;
        l = Convert.ToInt32(logopath.Length.ToString());


        Mess = "  <table > ";
        Mess = Mess + "<tr>";
        Mess = Mess + "<td>" + "<img src='http://www.eam.co.in/images/eammail.png' height='88' width='80' >";
        Mess = Mess + "</tr>";

        Mess = Mess + " <tr>";
        Mess = Mess + "  <td>";
        Mess = Mess + "  &nbsp;</td>";
        Mess = Mess + " <td>";
        Mess = Mess + "    &nbsp;</td>";
        Mess = Mess + " </tr>";
        Mess = Mess + " <tr>";
        Mess = Mess + " <td colspan='2'>";
        Mess = Mess + "   <hr/></td>";
        Mess = Mess + " </tr>";
        Mess = Mess + " <tr>";
        Mess = Mess + " <td>";
        Mess = Mess + " This message was sent from a notification-only E-mail address.</td>";
        Mess = Mess + " <td>";
        Mess = Mess + " &nbsp;</td>";
        Mess = Mess + "</tr>";
        Mess = Mess + "<tr>";
        Mess = Mess + "   <td>";
        Mess = Mess + " Please do not reply to this message.</td>";
        Mess = Mess + "  <td>";
        Mess = Mess + "     &nbsp;</td>";
        Mess = Mess + " </tr>";
        Mess = Mess + " <tr>";
        Mess = Mess + " <td colspan='2'>";
        Mess = Mess + "  <hr/></td>";
        Mess = Mess + " </tr>";
        Mess = Mess + " <tr>";
        Mess = Mess + " <td colspan='2'>";
        //Mess = Mess + "   Dear Sir/Madam,</td>";
        Mess = Mess + " </tr>";
        Mess = Mess + " <tr>";
        Mess = Mess + "  <td colspan='2'>";
        Mess = Mess + "Dear ";

        Mess = Mess + "             " + txtName0.Text + ",";
        Mess = Mess + "<br/>";

        Mess = Mess + "  Greetings from " + collegeTitle + "!</td>";
        Mess = Mess + " </tr>";
        Mess = Mess + " <tr>";
        Mess = Mess + " <td colspan='2'>";


        Mess = Mess + "      Your Login Details are as follows: ";
        Mess = Mess + " <hr/>";

        Mess = Mess + "<Br>";
        Mess = Mess + "User ID  :  " + UID;
        Mess = Mess + "<Br>";
        Mess = Mess + "Password :  " + Password;

        Mess = Mess + " </td>";
        Mess = Mess + " </tr>";
        Mess = Mess + " <tr>";
        Mess = Mess + " <td colspan='2'>";

        Mess = Mess + " </tr>";
        Mess = Mess + " <tr>";
        Mess = Mess + " <td>";
        Mess = Mess + "   &nbsp;</td>";
        Mess = Mess + " <td>";
        Mess = Mess + "   &nbsp;</td>";
        Mess = Mess + " </tr>";
        Mess = Mess + " <tr>";
        Mess = Mess + "  <td colspan='2'>";
        Mess = Mess + " <tr>";

        Mess = Mess + " <hr/></td>";
        Mess = Mess + "   </tr>";
        Mess = Mess + "  <tr>";
        Mess = Mess + "     <td colspan='2'>";
        Mess = Mess + "        Warm Regards,&nbsp;";
        Mess = Mess + "   </td>";
        Mess = Mess + " </tr>";
        Mess = Mess + " <tr>";
        Mess = Mess + "   <td>";
        string schName = "";
        sql = "Select CollegeName from CollegeMaster where BranchCode=" + ddlBranch.SelectedValue + "";
        schName = oo.ReturnTag(sql, "CollegeName");

        Mess = Mess + "  " + schName + "</td>";
        Mess = Mess + " <td>";
        Mess = Mess + "    &nbsp;</td>";
        Mess = Mess + " </tr>";
        Mess = Mess + " <tr>";
        Mess = Mess + "   <td>";

        string CityName = "";
        sql = "Select Case When ISNUMERIC(CityId)!=1 Then CityId";
        sql = sql + " Else (Select CityName from CityMaster where Id=(Select CityId from CollegeMaster)) End CityName from CollegeMaster and BranchCode=" + ddlBranch.SelectedValue + "";
        CityName = oo.ReturnTag(sql, "CityName");


        Mess = Mess + "   " + CityName + "</td>";
        Mess = Mess + " <td>";
        Mess = Mess + "   &nbsp;</td>";
        Mess = Mess + " </tr>";
        Mess = Mess + " <tr>";
        Mess = Mess + "    <td>";

        string email = "";
        sql = "Select Email from CollegeMaster where BranchCode=" + ddlBranch.SelectedValue + "";

        email = oo.ReturnTag(sql, "Email");
        Mess = Mess + "   " + email + "</td>";
        Mess = Mess + "  <td>";
        Mess = Mess + "    &nbsp;</td>";
        Mess = Mess + " </tr>";
        Mess = Mess + " <tr>";
        Mess = Mess + " <td>";
        Mess = Mess + "  &nbsp;</td>";
        Mess = Mess + " <td>";
        Mess = Mess + "  &nbsp;</td>";
        Mess = Mess + "  </tr>";
        Mess = Mess + " </table>";
        EmailSending(Mess, "" + collegeTitle + " : Admin Panel Login Details", txtEmail0.Text);
    }
    protected void DrpPreState_SelectedIndexChanged(object sender, EventArgs e)
    {
        string CCode = "";
        sql = "select Id from StateMaster where StateName='" + DrpPreState.SelectedItem.ToString() + "'";

        CCode = oo.ReturnTag(sql, "id");
        sql = "Select CityName from CityMaster where StateId=" + CCode;

        oo.FillDropDown(sql, DrpPreCity, "CityName");


    }

    protected void DrpPreState0_SelectedIndexChanged(object sender, EventArgs e)
    {
        string CCode = "";
        sql = "select Id from StateMaster where StateName='" + DrpPreState0.SelectedItem.ToString() + "'";

        CCode = oo.ReturnTag(sql, "id");
        sql = "Select CityName from CityMaster where StateId=" + CCode;

        oo.FillDropDown(sql, DrpPreCity0, "CityName");
        Panel1_ModalPopupExtender.Show();
    }

    protected void DrpCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        string CnCode = "";
        sql = "select Id from CountryMaster where CountryName='" + DrpCountry.SelectedItem.ToString() + "'";

        CnCode = oo.ReturnTag(sql, "id");
        sql = "Select StateName from StateMaster where CountryId=" + CnCode;

        oo.FillDropDown(sql, DrpPreState, "StateName");

    }
    protected void DrpCountry0_SelectedIndexChanged(object sender, EventArgs e)
    {
        string CnCode1 = "";
        sql = "select Id from CountryMaster where CountryName='" + DrpCountry0.SelectedItem.ToString() + "'";

        CnCode1 = oo.ReturnTag(sql, "id");
        sql = "Select StateName from StateMaster where CountryId=" + CnCode1;

        oo.FillDropDown(sql, DrpPreState0, "StateName");
        Panel1_ModalPopupExtender.Show();


    }

    public void ChangePasswordEmail(string bran, string Adna, string uid, string pass, string eMailID)
    {
        string bid = "";
        sql = "select branchid from branchtab  where branchName='" + bran + "' and BranchId=" + ddlBranch.SelectedValue + "";
        bid = oo.ReturnTag(sql, "branchid");

        string ss = "";

        string sql2 = "";
        ss = ss + "<table >";

        ss = ss + "<tr>";
        ss = ss + "<td >";
        ss = ss + "<hr />";
        ss = ss + "</td>";
        ss = ss + "</tr>";
        ss = ss + "<tr>";
        ss = ss + "<td >";
        ss = ss + " This message was sent from a notification-only E-mail address.";
        ss = ss + "</td>";
        ss = ss + "</tr>";
        ss = ss + "<tr>";
        ss = ss + "<td>";
        ss = ss + "Please do not reply to this message.";
        ss = ss + "</td>";
        ss = ss + "</tr>";
        ss = ss + "<tr>";
        ss = ss + "<td>";
        ss = ss + "<hr />";
        ss = ss + "</td>";
        ss = ss + "</tr>";
        ss = ss + "<tr>";
        ss = ss + "<td ><font color='maroon'>";
        ss = ss + "Dear  " + Adna + ",</td>";
        // ss=ss+"</td>";
        ss = ss + "</tr>";
        ss = ss + "<tr>";
        ss = ss + "<td ><font color='maroon'>";
        ss = ss + "Your Login Credentials are as follows:";
        ss = ss + "</td>";
        ss = ss + "</tr>";
        ss = ss + "<tr>";
        ss = ss + "<td >";
        ss = ss + "<hr />";
        ss = ss + "</td>";
        ss = ss + "</tr>";

        ss = ss + "<tr>";
        ss = ss + "<td> <font color='maroon'>";
        ss = ss + "User Type:  Admin</font></td>";

        ss = ss + "</tr>";
        ss = ss + "<tr>";
        ss = ss + "<td> <font color='maroon'>";
        ss = ss + "User Id:   " + uid + "</font></td>";

        sql2 = "Select Pass  from LoginTab where LoginName ='" + uid + "' and Branchid='" + bid + "' and logintypeid=2 and BranchId=" + ddlBranch.SelectedValue + "";


        ss = ss + "</tr> ";
        ss = ss + "<tr>";
        ss = ss + "<td><font color='maroon'>";
        ss = ss + "Password:   " + oo.ReturnTag(sql2, "Pass") + "</font></td>";

        ss = ss + "</tr>";

        ss = ss + "<tr>";
        ss = ss + "<td><font color='maroon'>";


        ss = ss + "Branch Name:   " + bran + "</font></td>";

        ss = ss + "</tr>";

        ss = ss + "<tr>";
        ss = ss + "<td>";

        ss = ss + "<hr />";
        ss = ss + "</td>";
        ss = ss + "</tr>";
        ss = ss + "<tr>";
        ss = ss + "<td>";
        ss = ss + " <b>Warm Regards,</b>";
        ss = ss + "</td>";
        ss = ss + "</tr>";
        ss = ss + "<tr>";
        ss = ss + "<td >";
        ss = ss + " <b>Team eAM</b>";
        ss = ss + "</td>";
        ss = ss + "</tr>";
        ss = ss + "<tr>";
        ss = ss + "<td >";
        ss = ss + "&nbsp;";
        ss = ss + "</td>";
        ss = ss + "</tr>";
        ss = ss + "</table>";
        try
        {
            string msg = oo.SendEmailInBackgroundThread(ss, "Change Password", eMailID, "Your Login Details has been sent to your E-mail address.");
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, msg, "S");
            //oo.MessageBoxforUpdatePanel(msg, LinkButton4);

        }
        catch (Exception) { }
    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedIndex == 0)
        {
            divSystem.Visible = false;
            txtMacId.CssClass = "form-control-blue";
            txtSerialno.CssClass = "form-control-blue";
        }
        else
        {
            divSystem.Visible = true;
            txtMacId.CssClass = "form-control-blue validatetxt";
            txtSerialno.CssClass = "form-control-blue validatetxt";
        }
    }

    protected void RadioButtonList4_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList4.SelectedIndex == 0)
        {
            divSystem0.Visible = false;
            txtMacId0.CssClass = "form-control-blue";
            txtSerialno0.CssClass = "form-control-blue";
        }
        else
        {
            divSystem0.Visible = true;
            txtMacId0.CssClass = "form-control-blue validatetxt";
            txtSerialno0.CssClass = "form-control-blue validatetxt";
        }

        Panel1_ModalPopupExtender.Show();
    }

    

}