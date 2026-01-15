using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;

public partial class Hostel_master : Page
{
    SqlConnection con = new SqlConnection();
    string sql = "";
    Campus oo = new Campus();
    protected void Page_Load(object sender, EventArgs e)

    {
        con = oo.dbGet_connection();
        //con.ConnectionString = ConfigurationManager.ConnectionStrings["Menu"].ConnectionString;
        if (!IsPostBack)
        {
            sql = "select id, ltrim(rtrim(CountryName)) as CountryName from CountryMaster";
            oo.FillDropDown_withValue(sql, drpcountry, "CountryName", "id");
            sql = "Select defaultvalue from DefaultSelectedValue where defaultvalueof='Country'";
            drpcountry.SelectedValue = oo.ReturnTag(sql, "defaultvalue");

            sql = "select id, ltrim(rtrim(StateName)) as StateName from StateMaster where countryid=" + drpcountry.SelectedValue + " ";
            oo.FillDropDown_withValue(sql, drpsate, "StateName", "id");
            sql = "Select defaultvalue from DefaultSelectedValue where defaultvalueof='State'";
            drpsate.SelectedValue = oo.ReturnTag(sql, "defaultvalue");

            sql = "select id, ltrim(rtrim(CityName)) as CityName from CityMaster where stateid=" + drpsate.SelectedValue + "";
            oo.FillDropDown_withValue(sql, drpcity, "CityName", "id");
            sql = "Select defaultvalue from DefaultSelectedValue where defaultvalueof='City'";
            drpcity.SelectedValue = oo.ReturnTag(sql, "defaultvalue");
            Display();
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "CompanyInformationProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@CompanyName", txtCompanyname.Text.ToString());
        string base64std = hflogo.Value;
        string logopath = "";
        string fileName = "";

        if (base64std != string.Empty)
        {
            string sessionName = Session["SessionName"].ToString();
            logopath = @"~/Uploads/HostelLogo/";
            fileName = "hostellogo.jpg";

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
        cmd.Parameters.AddWithValue("@CompanyLogo", logopath);
        

            cmd.Parameters.AddWithValue("@Adress1", txtaddress1.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@Address2", "");
            cmd.Parameters.AddWithValue("@Country", drpcountry.SelectedItem.ToString());

            cmd.Parameters.AddWithValue("@State", drpsate.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@City", drpcity.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@PinNo", txtpinno.Text.Trim().ToString());

            cmd.Parameters.AddWithValue("@PhoneNo", txtphone.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@MobileNo", txtmobileno.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@Email", txtemail.Text.Trim().ToString());


            cmd.Parameters.AddWithValue("@Website", txtwebsite.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@Remark", txtremark.Text.Trim().ToString());

            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());

            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"]);
            try
            {

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            if (LinkButton1.Text == "Submit")
            {
                oo.msgbox(this.Page, msgbox, "Submitted successfully", "S");
            }
            else
            {
                oo.msgbox(this.Page, msgbox, "Updated successfully", "S");
            }
                
                Display();
            }
            catch (Exception ex) { }
    }
    protected void drpcountry_SelectedIndexChanged(object sender, EventArgs e)
    {

        string CCode = "";
        if (drpcountry.SelectedItem.ToString() == "India")
        {


            sql = "select Id from CountryMaster where CountryName='" + drpcountry.SelectedItem.ToString() + "'";

            CCode = oo.ReturnTag(sql, "id");
            sql = "Select StateName from StateMaster where Countryid=" + CCode;

            oo.FillDropDown(sql, drpsate, "StateName");
        }

        else
        {
            drpsate.Items.Clear();
            drpcity.Items.Clear();
            drpcity.Items.Add("Others");
            drpsate.Items.Add("Others");
        }
    }
    protected void drpsate_SelectedIndexChanged(object sender, EventArgs e)
    {
        string CCode = "";
        sql = "select Id from StateMaster where StateName='" + drpsate.SelectedItem.ToString() + "'";

        CCode = oo.ReturnTag(sql, "id");
        sql = "Select CityName from CityMaster where StateId=" + CCode;

        oo.FillDropDown(sql, drpcity, "CityName");
    }

   
    public void Display()
    {
        sql = "Select Id,CompanyName  ,CompanyLogo  ,Adress1  ,	Address2  ,	Country  ,	State ,	City  ,	PinNo  ,	PhoneNo  ,	MobileNo  ,	Email  ,	Website  ,	Remark  ,	LoginName  ,SessionName,RecordDate from CompanyInformation ";
        sql = sql + " where  BranchCode=" + Session["BranchCode"].ToString() + "";
        txtCompanyname.Text = oo.ReturnTag(sql, "CompanyName");
        txtaddress1.Text = oo.ReturnTag(sql, "Adress1");

        //  txtaddres2.Text = oo.ReturnTag(sql, "Address2");
        drpcountry.Text = oo.ReturnTag(sql, "Country");
        drpsate.Text = oo.ReturnTag(sql, "State");
        drpcity.Text = oo.ReturnTag(sql, "City");
        txtpinno.Text = oo.ReturnTag(sql, "PinNo");
        txtphone.Text = oo.ReturnTag(sql, "PhoneNo");
        txtmobileno.Text = oo.ReturnTag(sql, "MobileNo");
        txtemail.Text = oo.ReturnTag(sql, "Email");
        txtwebsite.Text = oo.ReturnTag(sql, "Website");
        txtremark.Text = oo.ReturnTag(sql, "Remark");
        if (oo.ReturnTag(sql, "CompanyLogo").ToString() != "")
        {
            Image1.ImageUrl = oo.ReturnTag(sql, "CompanyLogo");
        }
        if (oo.Duplicate(sql))
        {
            LinkButton1.Text = "Update";
        }
    }
    
}