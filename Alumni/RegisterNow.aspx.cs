using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.IO;
using System.Web.UI.WebControls;
using c4SmsNew;

public partial class RegisterNow : System.Web.UI.Page
{
    SqlConnection con;
    Campus oo = new Campus();
    string sql = "";//   

    protected void Page_Load(object sender, EventArgs e)
    {
        con = new SqlConnection();
        con = BAL.objBal.dbGet_connection();
        if (!IsPostBack)
        {
            oo.AddDateMonthYearDropDown(DDYear, DDMonth, DDDate);
            oo.FindCurrentDateandSetinDropDown(DDYear, DDMonth, DDDate);
            var OtpSend = "Yes"; var Verified = "Yes"; var ContactNo = "";
            try
            {
                OtpSend = Request.Cookies["OtpSend"].Value;
                Verified = Request.Cookies["Verified"].Value;
                ContactNo = Request.Cookies["ContactNo"].Value;
            }
            catch (Exception)
            {
                Response.Cookies["OtpSend"].Value = "";
                Response.Cookies["Verified"].Value = "";
            }
            if (OtpSend == "Yes" && Verified != "Yes")
            {
                divVerify.Visible = true;
                divForm.Visible = false;
                divBtnVarify.Visible = true;
                divOtp.Visible = true;
                divBtnSend.Visible = false;
                try
                {
                    txtContactNo1.Text = ContactNo;
                }
                catch (Exception)
                {
                    divVerify.Visible = true;
                    divForm.Visible = false;
                    divBtnVarify.Visible = false;
                    divOtp.Visible = false;
                    divBtnSend.Visible = true;
                }


            }
            if (OtpSend == "Yes" && Verified == "Yes")
            {
                divVerify.Visible = false;
                divForm.Visible = true;
                txtContactNo.Text = ContactNo;
                
                if (ContactNo == "")
                {
                    divVerify.Visible = true;
                    divForm.Visible = false;
                    divBtnVarify.Visible = false;
                    divOtp.Visible = false;
                    divBtnSend.Visible = true;
                }
            }
            if (OtpSend == "" && Verified == "")
            {
                divVerify.Visible = true;
                divForm.Visible = false;
                divBtnVarify.Visible = false;
                divOtp.Visible = false;
                divBtnSend.Visible = true;
            }

            sql = "select BranchId, BranchName from BranchTab";
            BAL.objBal.FillDropDown_withValue(sql, drpBranch, "BranchName", "BranchId");
            drpBranch.Items.Insert(0, new ListItem("<--Select-->", ""));

            sql = " with yearlist as ";
            sql = sql + " ( ";
            sql = sql + " select 1950 as year ";
            sql = sql + " union all ";
            sql = sql + " select yl.year + 1 as year ";
            sql = sql + " from yearlist yl ";
            sql = sql + " where yl.year + 1 <= YEAR(GetDate()) ";
            sql = sql + " ) ";

            sql = sql + "  select year from yearlist order by year desc; ";
            BAL.objBal.FillDropDown_withValue(sql, drpLastYearAttended, "year", "year");
            drpLastYearAttended.Items.Insert(0, new ListItem("<--Select-->", ""));
            BAL.objBal.FillDropDown_withValue(sql, drpYearOfPostGraduation, "year", "year");
            drpYearOfPostGraduation.Items.Insert(0, new ListItem("<--Select-->", ""));
            BAL.objBal.FillDropDown_withValue(sql, drpYearofOthers, "year", "year");
            drpYearofOthers.Items.Insert(0, new ListItem("<--Select-->", ""));
            BAL.objBal.FillDropDown_withValue(sql, drpYearOfGraduation, "year", "year");
            drpYearOfGraduation.Items.Insert(0, new ListItem("<--Select-->", ""));
            sql = "Select CountryName,Id from CountryMaster";
            BAL.objBal.FillDropDown_withValue(sql, drpCountry, "CountryName", "id");
            drpCountry.Items.Insert(0, new ListItem("<--Select-->", ""));
            drpState.Items.Insert(0, new ListItem("<--Select-->", ""));
            drpCity.Items.Insert(0, new ListItem("<--Select-->", ""));
            using (var objBll = new BLL())
            {
                try
                {
                    objBll.loadDefaultvalue("Country", drpCountry);
                    sql = "Select StateName,Id from StateMaster where CountryId='" + drpCountry.SelectedValue + "'";
                    BAL.objBal.FillDropDown_withValue(sql, drpState, "StateName", "id");
                    if (drpState.Items.Count == 0)
                    {
                        drpState.Items.Clear();
                        drpState.Items.Insert(0, new ListItem("<--Select-->", ""));
                        drpState.Items.Insert(1, new ListItem("Other", "0"));

                        drpCity.Items.Clear();
                        drpCity.Items.Insert(0, new ListItem("<--Select-->", ""));
                        drpCity.Items.Insert(1, new ListItem("Other", "0"));
                    }
                    else
                    {
                        drpState.Items.Insert(0, new ListItem("<--Select-->", ""));
                    }
                    objBll.loadDefaultvalue("State", drpState);
                    sql = "Select CityName,id from CityMaster where StateId='" + drpState.SelectedValue + "'";
                    BAL.objBal.FillDropDown_withValue(sql, drpCity, "CityName", "id");
                    if (drpCity.Items.Count == 0)
                    {
                        drpCity.Items.Clear();
                        drpCity.Items.Insert(0, new ListItem("<--Select-->", ""));
                        drpCity.Items.Insert(1, new ListItem("Other", "0"));
                    }
                    else
                    {
                        drpCity.Items.Insert(0, new ListItem("<--Select-->", ""));
                    }
                    objBll.loadDefaultvalue("City", drpCity);
                }
                catch
                {
                    // ignored
                }
            }
            LoadData(ContactNo);
        }
    }
    protected void DDYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(DDYear, DDMonth, DDDate);
    }
    protected void DDMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(DDYear, DDMonth, DDDate);

    }
    protected void DDDate_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    protected void drpCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        sql = "Select StateName,Id from StateMaster where CountryId='" + drpCountry.SelectedValue + "'";
        BAL.objBal.FillDropDown_withValue(sql, drpState, "StateName", "id");
        if (drpState.Items.Count == 0)
        {
            drpState.Items.Clear();
            drpState.Items.Insert(0, new ListItem("<--Select-->", ""));
            drpState.Items.Insert(1, new ListItem("Other", "0"));

            drpCity.Items.Clear();
            drpCity.Items.Insert(0, new ListItem("<--Select-->", ""));
            drpCity.Items.Insert(1, new ListItem("Other", "0"));
        }
        else
        {
            drpState.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
    }

    protected void drpState_SelectedIndexChanged(object sender, EventArgs e)
    {
        sql = "Select CityName,id from CityMaster where StateId='" + drpState.SelectedValue + "'";
        BAL.objBal.FillDropDown_withValue(sql, drpCity, "CityName", "id");
        if (drpCity.Items.Count == 0)
        {
            drpCity.Items.Clear();
            drpCity.Items.Insert(0, new ListItem("<--Select-->", ""));
            drpCity.Items.Insert(1, new ListItem("Other", "0"));
        }
        else
        {
            drpCity.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
    }

    protected void BtnSend_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(txtContactNo1.Text.Trim()))
        {
            var otp = Campus.GenerateRandomNo(4);
            SendFeesSms(txtContactNo1.Text.Trim(), otp);

            Response.Cookies["Otp"].Value = otp;
            Response.Cookies["Otp"].Expires = DateTime.Now.AddMinutes(30);
            Response.Cookies["OtpSend"].Value = "Yes";
            Response.Cookies["OtpSend"].Expires = DateTime.Now.AddMinutes(30);
            Response.Cookies["ContactNo"].Value = txtContactNo1.Text.Trim();
            Response.Cookies["ContactNo"].Expires = DateTime.Now.AddMinutes(30);
            divVerify.Visible = true;
            divForm.Visible = false;
            divBtnVarify.Visible = true;
            divBtnSend.Visible = false;
            divOtp.Visible = true;
        }
    }

    protected void BtnClear_Click(object sender, EventArgs e)
    {
        Response.Cookies["ContactNo"].Value = "";
        Response.Cookies["Otp"].Value = "";
        Response.Cookies["OtpSend"].Value = "";
        Response.Cookies["Verified"].Value = "";
        divVerify.Visible = true;
        divForm.Visible = false;
        divOtp.Visible = false;
        divBtnVarify.Visible = false;
        divBtnSend.Visible = true;
    }

    protected void BtnVarify_Click(object sender, EventArgs e)
    {

        if (txtOtp.Text.Trim() == Request.Cookies["Otp"].Value.ToString())
        {
            Response.Cookies["ContactNo"].Value = txtContactNo1.Text.Trim();
            Response.Cookies["ContactNo"].Expires = DateTime.Now.AddMinutes(30);
            txtContactNo.Text = txtContactNo1.Text.Trim();
            divVerify.Visible = false;
            divForm.Visible = true;
            Response.Cookies["Otp"].Value = "";
            Response.Cookies["Verified"].Value = "Yes";
        }
        else
        {
            alert_danger.Style.Add("display", "block");
            lblError.Text = "Invalid OTP!";
            divVerify.Visible = true;
            divForm.Visible = false;
            divOtp.Visible = true;
            divBtnVarify.Visible = true;
            divBtnSend.Visible = false;
        }
    }
    protected void BtnRegisterNow_Click(object sender, EventArgs e)
    {
        sql = "select id from AlumniRegistration where AadhaarNo = '" + txtAadhaarNo.Text.Trim() + "'";
        if (oo.Duplicate(sql))
        {
            Label2AAdhar.Text = "Duplicate Aadhaar Number!";
            return;
        }

        using (var cmd = new SqlCommand())
        {
            var filePathDoc = "";
            var fileNameDoc = "";

            var base64StdDoc = hdnDoc.Value;
            if (base64StdDoc != string.Empty)
            {
                filePathDoc = @"Doc/";
                fileNameDoc = DateTime.Now.ToString("ddMMMyyy") + "Doc_" + txtContactNo.Text.Trim() + "."+ hdnDocExt.Value;

                using (FileStream fs = new FileStream(Server.MapPath((filePathDoc + fileNameDoc)), FileMode.Create))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs))
                    {
                        var data = Convert.FromBase64String(base64StdDoc);
                        bw.Write(data);
                        bw.Close();
                    }
                }
            }
            var filePathPhoto = "";
            var fileNamePhoto = "";

            var base64StdPhoto = hdnPhoto.Value;
            if (base64StdPhoto != string.Empty)
            {
                filePathPhoto = @"Photo/";
                fileNamePhoto = DateTime.Now.ToString("ddMMMyyy") + "Photo_" + txtContactNo.Text.Trim() + "." + hdnPhotoExt.Value;

                using (FileStream fs = new FileStream(Server.MapPath((filePathPhoto + fileNamePhoto)), FileMode.Create))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs))
                    {
                        var data = Convert.FromBase64String(base64StdPhoto);
                        bw.Write(data);
                        bw.Close();
                    }
                }
            }
            string day = "";
            if (int.Parse(DDDate.SelectedItem.Text) < 10)
            {
                day = "0" + DDDate.SelectedItem.Text;
            }
            else
            {
                day = DDDate.SelectedItem.Text;
            }
            string DateOfBirth = day + "-" + DDMonth.SelectedItem.Text + "-" + DDYear.SelectedItem.Text;
            cmd.CommandText = "Sp_AlumniRegistration";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@Fname", txtFname.Text);
            cmd.Parameters.AddWithValue("@Mname", txtMname.Text);
            cmd.Parameters.AddWithValue("@Lname", txtLname.Text);
            cmd.Parameters.AddWithValue("@DOB", DateOfBirth);
            cmd.Parameters.AddWithValue("@Gender", drpGender.SelectedValue);
            cmd.Parameters.AddWithValue("@LastAttendedClass", txtLastClassAttended.Text);
            cmd.Parameters.AddWithValue("@LastAttendedYear", drpLastYearAttended.SelectedValue);
            cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
            cmd.Parameters.AddWithValue("@ContactNo", txtContactNo.Text);
            cmd.Parameters.AddWithValue("@Branch", drpBranch.SelectedValue);
            cmd.Parameters.AddWithValue("@AadhaarNo", txtAadhaarNo.Text);
            cmd.Parameters.AddWithValue("@Graduation", txtGraduation.Text);
            cmd.Parameters.AddWithValue("@GraduationYear", drpYearOfGraduation.SelectedValue);
            cmd.Parameters.AddWithValue("@PostGraduation", txtPostGraduation.Text);
            cmd.Parameters.AddWithValue("@PostGraduationYear", drpYearOfPostGraduation.SelectedValue);
            cmd.Parameters.AddWithValue("@Other", txtOthers.Text);
            cmd.Parameters.AddWithValue("@OtherYear", drpYearofOthers.SelectedValue);
            cmd.Parameters.AddWithValue("@CurrentOccupation", txtCurrentOccupation.Text);
            cmd.Parameters.AddWithValue("@MaritalStatus", drpMaritalStatus.SelectedValue);
            cmd.Parameters.AddWithValue("@CurrentAddress", txtAddress.Text);
            cmd.Parameters.AddWithValue("@City", drpCity.SelectedValue);
            cmd.Parameters.AddWithValue("@State", drpState.SelectedValue);
            cmd.Parameters.AddWithValue("@Country", drpCountry.SelectedValue);
            if (hdnDoc.Value != "")
            {
                cmd.Parameters.AddWithValue("@DocumentProof", filePathDoc + fileNameDoc);
            }
            if (hdnPhoto.Value != "")
            {
                cmd.Parameters.AddWithValue("@RecentPhoto", filePathPhoto + fileNamePhoto);
            }
            cmd.Parameters.AddWithValue("@BranchCode", drpBranch.SelectedValue);
            cmd.Parameters.AddWithValue("@Action", "Insert");

            try
            {
                sql = "select 1 from AlumniRegistration where ContactNo = '"+ txtContactNo.Text.Trim() + "'";
                con.Open();
                int x = cmd.ExecuteNonQuery();
                con.Close();
                
                if (oo.Duplicate(sql))
                {
                    alert_success.Style.Add("display", "none");
                    alert_warning.Style.Add("display", "block");
                    lblWarning.Text = "Your request for update details submitted successfully.";
                }
                else
                {
                    alert_success.Style.Add("display", "block");
                    alert_warning.Style.Add("display", "none");
                    lbllog.Text = "Your request for alumni registration submitted successfully.";
                }
                cmd.Parameters.Clear();
                clear();
            }
            catch (SqlException EX)
            {
                con.Close();
            }
            catch (Exception)
            {
                con.Close();
            }
        }
    }

    public void SendFeesSms(string fmobileNo, string otp)
    {
        try
        {
            var sadpNew = new SMSAdapterNew();
            var mess = "Your OTP for Alumni Registration is " + otp+ " ";
            if (fmobileNo == "") return;
            sadpNew.Send(mess, fmobileNo, "37");
        }
        catch
        {
            // ignored
        }
    }

    protected void txtContactNo1_TextChanged(object sender, EventArgs e)
    {
        LoadData(txtContactNo1.Text.Trim());
    }

    protected void LoadData(string txtContactNo1)
    {
        sql = "select id,* from AlumniRegistration where ContactNo='" + txtContactNo1 + "' and Status<>'Pending'";
        if (oo.Duplicate(sql))
        {
            Response.Redirect("Default.aspx");
        }

        sql = "select id,* from AlumniRegistration where ContactNo='" + txtContactNo1 + "' and Status='Pending'";
        if (oo.Duplicate(sql))
        {
            divVerify.Visible = false;
            divForm.Visible = true;
            txtFname.Text = oo.ReturnTag(sql, "Fname");
            txtMname.Text = oo.ReturnTag(sql, "mname");
            txtLname.Text = oo.ReturnTag(sql, "lname");
            string[] str = oo.ReturnTag(sql, "dob").Split('-');
            DDDate.SelectedValue = int.Parse(str[0].ToString()).ToString();
            DDMonth.SelectedValue= str[1].ToString();
            DDYear.SelectedValue= str[2].ToString();
            drpGender.SelectedValue = oo.ReturnTag(sql, "gender");
            txtLastClassAttended.Text = oo.ReturnTag(sql, "LastAttendedClass");
            drpLastYearAttended.SelectedValue = oo.ReturnTag(sql, "LastAttendedYear");
            txtEmail.Text = oo.ReturnTag(sql, "Email");
            txtContactNo.Text = oo.ReturnTag(sql, "ContactNo");
            drpBranch.SelectedValue = oo.ReturnTag(sql, "Branch");
            txtAadhaarNo.Text = oo.ReturnTag(sql, "AadhaarNo");
            txtGraduation.Text = oo.ReturnTag(sql, "Graduation");
            drpYearOfGraduation.SelectedValue = oo.ReturnTag(sql, "GraduationYear");
            txtPostGraduation.Text = oo.ReturnTag(sql, "PostGraduation");
            drpYearOfPostGraduation.SelectedValue = oo.ReturnTag(sql, "PostGraduationYear");
            txtOthers.Text = oo.ReturnTag(sql, "Other");
            drpYearofOthers.SelectedValue = oo.ReturnTag(sql, "OtherYear");
            txtCurrentOccupation.Text = oo.ReturnTag(sql, "CurrentOccupation");
            drpMaritalStatus.SelectedValue = oo.ReturnTag(sql, "MaritalStatus");
            txtAddress.Text = oo.ReturnTag(sql, "CurrentAddress");
            drpCountry.SelectedValue = oo.ReturnTag(sql, "Country");
            string sql1 = "Select StateName,Id from StateMaster where CountryId='" + drpCountry.SelectedValue + "'";
            drpState.Items.Clear();
            BAL.objBal.FillDropDown_withValue(sql1, drpState, "StateName", "id");
            drpState.Items.Insert(0, new ListItem("<--Select-->", ""));
            if (!oo.Duplicate(sql1))
            {
                drpState.Items.Insert(1, new ListItem("Other", "0"));
                drpState.SelectedValue = "0";
            }
            else
            {
                drpState.SelectedValue = oo.ReturnTag(sql, "state"); ;
            }
            string sql2 = "Select CityName,Id from CityMaster where StateId='" + drpState.SelectedValue + "'";
            drpCity.Items.Clear();
            BAL.objBal.FillDropDown_withValue(sql2, drpCity, "CityName", "id");
            drpCity.Items.Insert(0, new ListItem("<--Select-->", ""));
            if (!oo.Duplicate(sql1))
            {
                drpCity.Items.Insert(1, new ListItem("Other", "0"));
                drpCity.SelectedValue = "0";
            }
            else
            {
                drpCity.SelectedValue = oo.ReturnTag(sql, "city"); ;
            }
            drpBranch.SelectedValue = oo.ReturnTag(sql, "BranchCode");
        }
    }
    protected void clear()
    {
        txtFname.Text = "";
        txtMname.Text = "";
        txtLname.Text = "";
        drpGender.SelectedIndex = 0;
        txtLastClassAttended.Text = "";
        drpLastYearAttended.SelectedIndex = 0;
        txtEmail.Text = "";
        txtContactNo.Text = "";
        drpBranch.SelectedIndex = 0;
        txtAadhaarNo.Text = "";
        txtGraduation.Text = "";
        drpYearOfGraduation.SelectedIndex = 0;
        txtPostGraduation.Text = "";
        drpYearOfPostGraduation.SelectedIndex = 0;
        txtOthers.Text = "";
        drpYearofOthers.SelectedIndex = 0;
        txtCurrentOccupation.Text = "";
        drpMaritalStatus.SelectedIndex = 0;
        txtAddress.Text = "";
        drpCountry.SelectedIndex = 0;
        drpState.Items.Clear();
        drpState.Items.Insert(0, new ListItem("<--Select-->", ""));
        drpCity.Items.Clear();
        drpCity.Items.Insert(0, new ListItem("<--Select-->", ""));
    }

}