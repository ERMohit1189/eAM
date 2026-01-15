using System;
using System.Data.SqlClient;

public partial class ReportHeadetUserControl : System.Web.UI.UserControl
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        con = oo.dbGet_connection();
        if (!IsPostBack)
        {
            sql = "select CollegeName,CollegeAdd1,CollegeAdd2,Phone,WebSite,Email,CollegeLogo,CityId from CollegeMaster where BranchCode=" + Session["BranchCode"] + "";
            try
            {
                Label1.Text = oo.ReturnTag(sql, "CollegeName");
            }
            catch(Exception){Label1.Visible = false;}
            try
            {
                Label2.Text = oo.ReturnTag(sql, "CollegeAdd1");
            }
            catch (Exception) { Label2.Visible = false; }
            try
            {
                Label8.Text = oo.ReturnTag(sql, "CollegeAdd2");
            }
            catch (Exception) { Label8.Visible = false; }
            try
            {
                Label7.Text = oo.ReturnTag(sql, "Phone");
                if (Label7.Text == "")
                {
                    Lbleph.Visible = false;
                }
                else
                {
                    Lbleph.Visible = true;
                }
                Lbleph.Text = "Ph:";
            }
            catch (Exception) { Label7.Visible = false; Lbleph.Visible = false; }

            try
            {
                Label11.Text = oo.ReturnTag(sql, "WebSite");
                if (Label11.Text == "")
                {
                    lblWebsite.Visible = false;
                }
                else
                {
                    lblWebsite.Visible = true;
                }
            
                lblWebsite.Text = "Website:";
            }
            catch (Exception) { Label11.Visible = false; lblWebsite.Visible = false; }


            try
            {
                Label12.Text = oo.ReturnTag(sql, "CityId");
            }
            catch (Exception) { Label12.Visible = false; }



            //try
            //{
            //    Label4.Text = oo.ReturnTag(sql, "WebSite");
            //}
            //catch (Exception) { Label4.Visible = false; }
            try
            {
                Label9.Text = oo.ReturnTag(sql, "Email");
                if (Label9.Text == "")
                {
                    Lblemail.Visible = false;
                }
                else
                {
                    Lblemail.Visible = true;
                }
              
                Lblemail.Text = "Email:";

            }
            catch (Exception) { Label9.Visible = false; Lblemail.Visible = false; }

            try
            {
                Label13.Text = "U.P.";
            }
            catch (Exception) { Label13.Visible = false; }


            try
            {
                Label14.Text = "India";
            }
            catch (Exception) { Label14.Visible = false; }


          




            try
            {
                Image1.ImageUrl = "~/DisplayImage.ashx?UserLoginID=" + 1;
            }
            catch (Exception) { }






        }
    }
}