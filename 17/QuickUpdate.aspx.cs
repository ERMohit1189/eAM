using System;
using System.Collections.Generic;
using System.Web.UI;

using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using c4SmsNew;
using System.Threading;
public partial class QuickUpdate : Page
{
    readonly Campus _oo = new Campus();
    SqlConnection con = new SqlConnection();
    string sql = ""; string _sql = "";
    protected void Page_PreInIt(object sender, EventArgs e)
    {
        if (Session["Logintype"] == null) { Response.Redirect("~/default.aspx"); }
        if (Session["Logintype"].ToString() == "Admin")
        {
            this.MasterPageFile = "~/Master/admin_root-manager.master";
        }
        else if (Session["Logintype"].ToString() != "Staff")
        {
            this.MasterPageFile = "~/Staff/staff_root-manager.master";
        }
    }
    public void LoadK12()
    {
        _sql = "Select * from setting";
        thIdentity.InnerText = _oo.ReturnTag(_sql, "IsAadhaar").ToString();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        con = _oo.dbGet_connection();
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            loadclass();
            loadsection();
            BAL.objBal.fillSelectvalue(drpBranch, "<--Select-->", "-1");
        }
    }
    public void loadclass()
    {
        BLL.BLLInstance.loadClass(drpclass, Session["SessionName"].ToString());
    }
    public void loadsection()
    {
        sql = "Select SectionName,Id from SectionMaster where ClassNameId='" + drpclass.SelectedValue.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        BAL.objBal.FillDropDown_withValue(sql, drpsection, "SectionName", "Id");
        drpsection.Items.Insert(0, "<--Select-->");
    }
    protected void drpclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadsection();
    }
    protected void drpsection_SelectedIndexChanged(object sender, EventArgs e)
    {
        BLL.BLLInstance.loadBranch(drpBranch, Session["SessionName"].ToString(), drpclass.SelectedValue.ToString());
    }
    private void LoaddefaultCountry(DropDownList drp)
    {
        _sql = "Select CountryName,Id from CountryMaster";
        BAL.objBal.FillDropDown_withValue(_sql, drp, "CountryName", "id");
        using (var objBll = new BLL())
        {
            try
            {
                objBll.loadDefaultvalue("Country", drp);
            }
            catch
            {
                // ignored
            }
        }
    }
    private void LoaddefaultState(DropDownList drp, DropDownList drpValue)
    {
        drp.Items.Clear();
        _sql = "Select count(*) cnt from StateMaster where countryId='" + drpValue.SelectedValue + "'";
        if (_oo.ReturnTag(_sql, "cnt") == "0")
        {
            drp.Items.Add(new ListItem("Other", "0"));
        }
        else
        {
            _sql = "Select StateName,Id from StateMaster";
            BAL.objBal.FillDropDown_withValue(_sql, drp, "StateName", "id");
            using (var objBll = new BLL())
            {
                try
                {
                    objBll.loadDefaultvalue("State", drp);
                }
                catch
                {
                    // ignored
                }
            }
        }
    }
    private void LoaddefaultCity(DropDownList drp, DropDownList drpValue)
    {
        drp.Items.Clear();
        _sql = "Select count(*) cnt from CityMaster where StateId='" + drpValue.SelectedValue + "'";
        if (_oo.ReturnTag(_sql, "cnt") == "0")
        {
            drp.Items.Add(new ListItem("Other", "0"));
        }
        else
        {
            _sql = "Select CityName,id from CityMaster where StateId='" + drpValue.SelectedValue + "'";
            BAL.objBal.FillDropDown_withValue(_sql, drp, "CityName", "id");
            using (var objBll = new BLL())
            {
                try
                {
                    objBll.loadDefaultvalue("City", drp);
                }
                catch
                {
                    // ignored
                }
            }
        }
    }
    public void loadStudents()
    {
        string BranchId = "";
        if (drpBranch.SelectedIndex != 0)
        {
            BranchId = drpBranch.SelectedValue;
        }
        sql = "select * from AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "', " + Session["BranchCode"].ToString() + ") ";
        sql +=  " where ClassId=" + drpclass.SelectedValue.ToString() + " and SectionID='" + drpsection.SelectedValue + "' and BranchId=case when '" + BranchId + "'='' then BranchId else '" + BranchId + "' end and SessionName='" + Session["SessionName"].ToString() + "' order by name";
        rptStudents.DataSource = BAL.objBal.GridFill(sql);
        rptStudents.DataBind();
        if (rptStudents.Items.Count > 0)
        {
            LoadK12();
            divExport.Visible = true;
            LinkUpdate.Visible = true;
            for (int i = 0; i < rptStudents.Items.Count; i++)
            {
                Label LblSrno = (Label)rptStudents.Items[i].FindControl("LblSrno");

                Label CountryId = (Label)rptStudents.Items[i].FindControl("CountryId");
                DropDownList drpCountry = (DropDownList)rptStudents.Items[i].FindControl("drpCountry");
                LoaddefaultCountry(drpCountry);
                drpCountry.SelectedValue = CountryId.Text.Trim();

                Label stateId = (Label)rptStudents.Items[i].FindControl("stateId");
                DropDownList drpState = (DropDownList)rptStudents.Items[i].FindControl("drpState");
                LoaddefaultState(drpState, drpCountry);
                drpState.SelectedValue = stateId.Text.Trim();

                Label cityId = (Label)rptStudents.Items[i].FindControl("cityId");
                DropDownList drpCity = (DropDownList)rptStudents.Items[i].FindControl("drpCity");
                LoaddefaultCity(drpCity, drpState);
                drpCity.SelectedValue = cityId.Text.Trim();
            }
        }
        else
        {
            divExport.Visible = false;
            LinkUpdate.Visible = false;
        }
    }

    protected void LinkView_Click(object sender, EventArgs e)
    {
        loadStudents();
    }

    protected void LinkUpdate_Click(object sender, EventArgs e)
    {
        bool valid = true;
        for (int i = 0; i < rptStudents.Items.Count; i++)
        {
            TextBox txtDob = (TextBox)rptStudents.Items[i].FindControl("txtDob");
            TextBox txtFatherName = (TextBox)rptStudents.Items[i].FindControl("txtFatherName");
            TextBox txtMotherName = (TextBox)rptStudents.Items[i].FindControl("txtMotherName");
            TextBox txtFatherContactNo = (TextBox)rptStudents.Items[i].FindControl("txtFatherContactNo");
            TextBox txtStLocalAddress = (TextBox)rptStudents.Items[i].FindControl("txtStLocalAddress");
            if (txtDob.Text == "" || txtFatherName.Text == "" || txtMotherName.Text == "" || txtFatherContactNo.Text == "" || txtStLocalAddress.Text == "")
            {
                valid = false;
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Please enter all required fields!", "W");
            }
        }
        if (valid)
        {
            bool flag1 = false; bool flag2 = true;
            if (rptStudents.Items.Count > 0)
            {
                for (int i = 0; i < rptStudents.Items.Count; i++)
                {
                    Label LblSrno = (Label)rptStudents.Items[i].FindControl("LblSrno");
                    TextBox txtDob = (TextBox)rptStudents.Items[i].FindControl("txtDob");
                    DropDownList drpCountry = (DropDownList)rptStudents.Items[i].FindControl("drpCountry");
                    DropDownList drpState = (DropDownList)rptStudents.Items[i].FindControl("drpState");
                    DropDownList drpCity = (DropDownList)rptStudents.Items[i].FindControl("drpCity");
                    TextBox txtFatherName = (TextBox)rptStudents.Items[i].FindControl("txtFatherName");
                    TextBox txtMotherName = (TextBox)rptStudents.Items[i].FindControl("txtMotherName");
                    TextBox txtFatherContactNo = (TextBox)rptStudents.Items[i].FindControl("txtFatherContactNo");
                    TextBox txtStLocalAddress = (TextBox)rptStudents.Items[i].FindControl("txtStLocalAddress");
                    TextBox txtAadharNo = (TextBox)rptStudents.Items[i].FindControl("txtAadharNo");

                    SqlCommand cmd = new SqlCommand();

                    cmd.CommandText = "QuickUpdate";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@SrNo", LblSrno.Text.Trim());
                    cmd.Parameters.AddWithValue("@DOB", txtDob.Text.Trim());
                    cmd.Parameters.AddWithValue("@StLocalCountryId", drpCountry.SelectedValue);
                    cmd.Parameters.AddWithValue("@StLocalStateId", drpState.SelectedValue);
                    cmd.Parameters.AddWithValue("@StLocalCityId", drpCity.SelectedValue);
                    cmd.Parameters.AddWithValue("@FatherName", txtFatherName.Text);
                    cmd.Parameters.AddWithValue("@MotherName", txtMotherName.Text);
                    cmd.Parameters.AddWithValue("@AadharNo", txtAadharNo.Text);
                    cmd.Parameters.AddWithValue("@FamilyContactNo", txtFatherContactNo.Text);
                    cmd.Parameters.AddWithValue("@StLocalAddress", txtStLocalAddress.Text);
                    cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                    cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                    cmd.Parameters.AddWithValue("@Action", "update");
                    cmd.Connection = con;
                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        flag1 = true;
                    }
                    catch (SqlException)
                    {
                        flag2 = true;
                    }
                }
            }

            if (flag1 == true && flag2 == true)
            {
                loadStudents();
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Updated successfully.", "S");
            }
            else
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Record not updated!", "W");
            }
        }
    }

    protected void drpCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList drp = (DropDownList)sender;
        DropDownList drpCountry = (DropDownList)drp.FindControl("drpCountry");
        DropDownList drpState = (DropDownList)drp.FindControl("drpState");
        sql = "select id, StateName from StateMaster where CountryId="+ drpCountry.SelectedValue+"";
        _oo.FillDropDown_withValue(sql, drpState, "StateName", "id");
    }

    protected void drpState_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList drp = (DropDownList)sender;
        DropDownList drpState = (DropDownList)drp.FindControl("drpState");
        DropDownList drpCity = (DropDownList)drp.FindControl("drpCity");
        sql = "select id, CityName from CityMaster where StateId=" + drpState.SelectedValue + "";
        _oo.FillDropDown_withValue(sql, drpCity, "CityName", "id");
    }
}