using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;
using System.IO;

public partial class Contact_Us : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sql = "Select BranchId, BranchName from Branchtab";
            var dt = oo.Fetchdata(sql);
            ddlBranch.DataSource = dt;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "BranchId";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, new ListItem("<--Select Branch-->", "0"));
            load();
        }
    }

    protected void load()
    {
        sql = "select CollegeName, Phone, Email,  (CollegeAdd1 + ' ' + (case when CollegeAdd2 <> '' then CollegeAdd2 + ', ' else '' end) + (case when CityName<> '' then CityName+', ' else '' end)+StateName) Address1 from CollegeMaster inner join CityMaster on CityMaster.Id = CollegeMaster.CityId inner join StateMaster on StateMaster.Id = CollegeMaster.StateId where  CollegeMaster.BranchCode=" + ddlBranch.SelectedValue + "";
        if (oo.Duplicate(sql))
        {
            divDetails.Visible = true;
            instituteName.Text = oo.ReturnTag(sql, "collegeName");
            address.Text = oo.ReturnTag(sql, "Address1");
            email.Text = oo.ReturnTag(sql, "Email");
            Phone.Text = oo.ReturnTag(sql, "Phone");
        }
        else
        {
            divDetails.Visible = false;
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        load();
    }
}