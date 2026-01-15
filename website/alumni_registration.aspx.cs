using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;



public partial class website_alumni_registration : System.Web.UI.Page
{ string sql = "";
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    protected void Page_Load(object sender, EventArgs e)
    {
        con = oo.dbGet_connection();
        if (!IsPostBack)
        {
            loadCountry();
            loadState();
            loadCity();
            LoadYearDropDownList();
        }

    }

    public void LoadYearDropDownList()
    {
        rrs.FillYearDropDown(drpYear);
        drpYear.Items.Insert(0, new ListItem("<--Select Batch-->", "<--Select Batch-->"));
    }

    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        string PhotoPath = "~/uploads/docs/other/";
        if (FileUpload1.HasFile)
        {
            FileUpload1.SaveAs(Server.MapPath(@PhotoPath + FileUpload1.FileName));
            PhotoPath = PhotoPath + FileUpload1.FileName.ToString();
        }
        else
        {
            PhotoPath = PhotoPath + "dummy.png";
        }



        //cmd.Parameters.AddWithValue("@Profession", txtProfession.Text.Trim());
        string txtcmp = String.Empty;
        if (txtCmp.Text == "")
        {
           // cmd.Parameters.AddWithValue("@Cmpy", "N/A");
            txtcmp = "N/A";
        }
        else
        {
           // cmd.Parameters.AddWithValue("@Cmpy", txtCmp.Text);
            txtcmp = txtCmp.Text;
        }

        string txtdesi = String.Empty;
        if (txtDesi.Text == "")
        {
           // cmd.Parameters.AddWithValue("@Desig", "N/A");
            txtdesi = "N/A";
        }
        else
        {
          //  cmd.Parameters.AddWithValue("@Desig", txtDesi.Text);
            txtdesi = txtDesi.Text;
        }


      //  cmd.Parameters.AddWithValue("@Photo", PhotoPath);

        string txtremark = String.Empty;
        if (txtRemark.Text == "")
        {
           // cmd.Parameters.AddWithValue("@Remark", "N/A");
            txtremark = "N/A";
        }
        else
        {
           // cmd.Parameters.AddWithValue("@Remark", txtRemark.Text);
            txtremark = txtRemark.Text;
        }
        //con.Open();
        //cmd.ExecuteNonQuery();
        //con.Close();

        string msgpass = BLL.obj_bll1.set_alumniregistration(txtName.Text.Replace("'", "").Trim(), "Kanpur Road", txtFName.Text.Replace("'", "").Trim(), drpClass.SelectedItem.ToString(), drpYear.SelectedItem.ToString(), txtAddress.Text.Replace("'", "").Trim(), txtContact.Text.Replace("'", "").Trim(), txtEmail.Text.Replace("'", "").Trim(), drpCountry.SelectedItem.ToString(), drpState.SelectedItem.ToString(), drpCity.SelectedItem.ToString(), txtProfession.Text.Replace("'", "").Trim(), txtcmp.ToString().Replace("'", "").Trim(), txtdesi.ToString().Replace("'", "").Trim(), PhotoPath.ToString().Replace("'", "").Trim(), txtremark.ToString().Replace("'", "").Trim());

        if (msgpass == "S")
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Submitted successfully.')", true);
            oo.MessageBox("Submitted Successfully", this.Page);
            oo.ClearControls(abc);
            //txtBranch.Text = "Kanpur Road";
        }
    }

    public void loadCountry()
    {
        sql = "Select CountryName from CountryMaster order by CountryName Asc";
        oo.FillDropDown(sql, drpCountry, "CountryName");
        drpCountry.Items.RemoveAt(0);
        drpCountry.Items.Insert(0, new ListItem("<--Select Country-->", "<--Select Country-->"));
        drpCountry.SelectedValue = "India";
    }

    protected void drpCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadState();
    }

    public void loadState()
    {
        sql = "Select Id from CountryMaster where CountryName='" + drpCountry.SelectedItem.ToString() + "'";
        string id = oo.ReturnTag(sql, "Id");
        sql = "Select StateName from StateMaster where CountryId='" + id + "' order by StateName Asc";
        oo.FillDropDown(sql, drpState, "StateName");
        if (drpState.Items.Count == 1)
        {
            drpState.Items.Insert(1, "Other");
        }
        else
        {
            drpState.Items.Add("Other");
        }
        drpState.Items.RemoveAt(0);
        drpState.Items.Insert(0, new ListItem("<--Select State-->", "<--Select State-->"));
    }

    public void loadCity()
    {
        sql = "Select Id from CountryMaster where CountryName='" + drpCountry.SelectedItem.ToString() + "'";
        string countryid = oo.ReturnTag(sql, "Id");
        sql = "Select Id from StateMaster where StateName='" + drpState.SelectedItem.ToString() + "'";
        string stateid = oo.ReturnTag(sql, "Id");
        sql = "Select CityName from CityMaster where CountryId='" + countryid + "' and StateId='" + stateid + "' order by CityName Asc";
        oo.FillDropDown(sql, drpCity, "CityName");
        if (drpCity.Items.Count == 1)
        {
            drpCity.Items.Insert(1, new ListItem("Other", "Other"));
        }
        else
        {
            drpCity.Items.Insert(drpCity.Items.Count, new ListItem("Other", "Other"));
        }
        drpCity.Items.RemoveAt(0);
        drpCity.Items.Insert(0, new ListItem("<--Select City-->", "<--Select City-->"));
    }

    protected void drpState_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadCity();
    }
    protected void drpCountry_SelectedIndexChanged1(object sender, EventArgs e)
    {
        loadState();
    }
    protected void drpState_SelectedIndexChanged1(object sender, EventArgs e)
    {
        loadCity();
    }

}