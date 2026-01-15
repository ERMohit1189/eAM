using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;

public partial class publisher_entry : System.Web.UI.Page
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
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);

        if (!IsPostBack)
        {
            try
            {
                CheckValueADDDeleteUpdate();
            }
            catch (Exception) { }
            sql = "select ltrim(rtrim(CountryName)) as CountryName, id from CountryMaster ";
            oo.FillDropDown_withValue(sql, Drpcountry, "CountryName", "id");
            sql = "Select defaultvalue from DefaultSelectedValue where defaultvalueof='Country'";
            Drpcountry.SelectedValue = oo.ReturnTag(sql, "defaultvalue");

            sql = "select ltrim(rtrim(StateName)) as StateName, id from StateMaster where CountryId=" + Drpcountry.SelectedValue + "";
            oo.FillDropDown_withValue(sql, drpstate, "StateName", "id");
            sql = "Select defaultvalue from DefaultSelectedValue where defaultvalueof='State'";
            drpstate.SelectedValue = oo.ReturnTag(sql, "defaultvalue");

            sql = "select ltrim(rtrim(CityName)) as CityName, id from CityMaster  where stateid=" + drpstate.SelectedValue + "";
            oo.FillDropDown_withValue(sql, drpCity, "CityName", "id");
            sql = "Select defaultvalue from DefaultSelectedValue where defaultvalueof='City'";
            drpCity.SelectedValue = oo.ReturnTag(sql, "defaultvalue");


            sql = "select ltrim(rtrim(CountryName)) as CountryName, id from CountryMaster ";
            oo.FillDropDown_withValue(sql, Drpcountry0, "CountryName", "id");

            sql = "select ltrim(rtrim(StateName)) as StateName, id from StateMaster where CountryId=" + Drpcountry0.SelectedValue + "";
            oo.FillDropDown_withValue(sql, drpstate0, "StateName", "id");

            sql = "select ltrim(rtrim(CityName)) as CityName, id from CityMaster  where stateid=" + drpstate0.SelectedValue + "";
            oo.FillDropDown_withValue(sql, drpCity0, "CityName", "id");

            sql = "Select CategoryName from PublisherCategoryMaster where  BranchCode=" + Session["BranchCode"] + "";
            oo.FillDropDown(sql, drppublisherCategory, "CategoryName");
            loadData();
        }
    }
    protected void loadData()
    {
        sql = " Select  ROW_NUMBER() OVER (ORDER BY p.id ASC) AS SrNo,p.id as id,pm.CategoryName as CategoryName, p.PublisherCategory as PublisherCategory,p.PublisherName as PublisherName,p.Address as Address,p.Country  as  Country,p.State  as State,p.City  as City ,p.Zip  as Zip,p.Phone1  as Phone1,p.Phone2  as Phone2,p.Mobile  as Mobile,p.Fax  as Fax,p.Email as Email,p.Website  as Website,p.Remark as Remark,p.SessionName as SessionName,p.BranchCode  as BranchCode,p.LoginName as LoginName,p.RecordDate  as RecordDate  from PublisherInfoEntry p  ";
        sql = sql + " left join PublisherCategoryMaster pm on p.PublisherCategory=pm.CategoryCode  and p.BranchCode=pm.BranchCode  where p.BranchCode=" + Session["BranchCode"] + "";
        GridView1.DataSource = oo.GridFill(sql);
        GridView1.DataBind();
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            Label id = (Label)GridView1.Rows[i].FindControl("Label36");
            LinkButton LinkButton3 = (LinkButton)GridView1.Rows[i].FindControl("LinkButton3");
            sql = "SELECT Publisher FROM LibraryItemEntry WHERE Publisher='" + id.Text + "' AND BranchCode=" + Session["BranchCode"] + "";
            if (oo.Duplicate(sql))
            {
                LinkButton3.Text = "<i class='fa fa-lock'></i>";
                LinkButton3.Enabled = false;
            }
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        sql = "Select PublisherName from PublisherInfoEntry where PublisherName='" + txtName.Text.Trim().ToString() + "' and  BranchCode=" + Session["BranchCode"] + "";
        if (oo.Duplicate(sql))
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, Duplicate Entry!", "A");
        }
        else if (drppublisherCategory.SelectedItem.ToString() == "<--Select-->" || Drpcountry.SelectedItem.ToString() == "<--Select-->" || drpCity.SelectedItem.ToString() == "<--Select-->" || drpstate.SelectedItem.ToString() == "<--Select-->")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please Select Condition", "A");
        }
        else
        {
            string PubCode = "";
            try
            {
                sql = "select MAX(Id)+1 AS Srno from PublisherInfoEntry ";
                PubCode = "Pub" + oo.ReturnTag(sql, "Srno");
            }
            catch (Exception) { PubCode = "Pub" + "1"; }
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "PublisherInfoEntryProce";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            string pubCate = "";
            sql = "Select CategoryCode from PublisherCategoryMaster where CategoryName='" + drppublisherCategory.SelectedItem.ToString() + "' and  BranchCode=" + Session["BranchCode"] + "";
            pubCate = oo.ReturnTag(sql, "CategoryCode");
            cmd.Parameters.AddWithValue("@PublisherCategory", pubCate);
            cmd.Parameters.AddWithValue("@PubCode", PubCode);
            cmd.Parameters.AddWithValue("@PublisherName", txtName.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@Country", Drpcountry.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@State", drpstate.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@City", drpCity.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@Zip", txtzip.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@Phone1", txtphone1.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@Phone2", txtphoneno2.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@Mobile", txtmobile.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@Fax", txtFax.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@Website", txtwebsite.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@Remark", txtRemark.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");
                oo.ClearControls(this.Page);
                loadData();
            }
            catch (Exception) { }
        }
    }
    protected void Drpcountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        sql = "select ltrim(rtrim(StateName)) as StateName, id from StateMaster where CountryId=" + Drpcountry.SelectedValue + "";
        oo.FillDropDown_withValue(sql, drpstate, "StateName", "id");
        sql = "select ltrim(rtrim(CityName)) as CityName, id from CityMaster  where stateid=" + drpstate.SelectedValue + "";
        oo.FillDropDown_withValue(sql, drpCity, "CityName", "id");
    }
    protected void drpstate_SelectedIndexChanged(object sender, EventArgs e)
    {
        sql = "select ltrim(rtrim(CityName)) as CityName, id from CityMaster  where stateid=" + drpstate.SelectedValue + "";
        oo.FillDropDown_withValue(sql, drpCity, "CityName", "id");
    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        sql = "Select PublisherName from PublisherInfoEntry where PublisherName='" + txtName0.Text.Trim().ToString() + "' and id<>"+ lblID.Text.Trim() + " and  BranchCode=" + Session["BranchCode"] + "";
        if (oo.Duplicate(sql))
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, Duplicate Entry!", "A");
            return;
        }
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "PublisherInfoEntryUpdateProce";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        cmd.Parameters.AddWithValue("@id", lblID.Text.Trim());
        cmd.Parameters.AddWithValue("@PublisherCategory", Label8.Text.Trim().ToString());
        cmd.Parameters.AddWithValue("@PublisherName", txtName0.Text.Trim().ToString());
        cmd.Parameters.AddWithValue("@Address", txtAddress0.Text.Trim().ToString());
        cmd.Parameters.AddWithValue("@Country", Drpcountry0.SelectedItem.ToString());
        cmd.Parameters.AddWithValue("@State", drpstate0.SelectedItem.ToString());
        cmd.Parameters.AddWithValue("@City", drpCity0.SelectedItem.ToString());
        cmd.Parameters.AddWithValue("@Zip", txtzip0.Text.Trim().ToString());
        cmd.Parameters.AddWithValue("@Phone1", txtphone2.Text.Trim().ToString());
        cmd.Parameters.AddWithValue("@Phone2", txtphoneno3.Text.Trim().ToString());
        cmd.Parameters.AddWithValue("@Mobile", txtmobile0.Text.Trim().ToString());
        cmd.Parameters.AddWithValue("@Fax", txtFax0.Text.Trim().ToString());
        cmd.Parameters.AddWithValue("@Email", txtEmail0.Text.Trim().ToString());
        cmd.Parameters.AddWithValue("@Website", txtwebsite0.Text.Trim().ToString());
        cmd.Parameters.AddWithValue("@Remark", txtRemark0.Text.Trim().ToString());
        cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
        cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
        try
        {
            con.Open();
            //cmd.ExecuteNonQuery();
            con.Close();
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "S");
            oo.ClearControls(this.Page);
            loadData();
        }
        catch (Exception) { }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        sql = "Delete from PublisherInfoEntry where id=" + lblvalue.Text.Trim() + " and  BranchCode=" + Session["BranchCode"] + "";
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = sql;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Deleted successfully.", "S");
            loadData();
        }
        catch (SqlException) { }
    }
    protected void LinkButton5_Click(object sender, EventArgs e)
    {
    }
    protected void Button8_Click(object sender, EventArgs e)
    {
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        sql = "select ltrim(rtrim(CountryName)) as CountryName, id from CountryMaster ";
        oo.FillDropDown_withValue(sql, Drpcountry0, "CountryName", "id");
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label36");
        string ss = lblId.Text;
        lblID.Text = ss;
        sql = "Select  ROW_NUMBER() OVER (ORDER BY p.id ASC) AS SrNo,p.id, p.PublisherCategory,pm.CategoryName as CategoryName,p.PublisherName,p.Address,p.Country,p.State as ss,p.City,p.Zip,p.Phone1,p.Phone2,p.Mobile,p.Fax,p.Email,p.Website,p.Remark,";
        sql = sql + "     p.SessionName,p.BranchCode,p.LoginName,p.RecordDate from PublisherInfoEntry p";
        sql = sql + " left join PublisherCategoryMaster pm on p.PublisherCategory=pm.CategoryCode  and  pm.BranchCode=" + Session["BranchCode"] + "  and  p.BranchCode=" + Session["BranchCode"] + "";
        sql = sql + "    where p.Id=" + ss + " and  p.BranchCode=" + Session["BranchCode"] + "";
        Label8.Text = oo.ReturnTag(sql, "CategoryName");
        txtName0.Text = oo.ReturnTag(sql, "PublisherName");
        txtAddress0.Text = oo.ReturnTag(sql, "Address");
        string sts = oo.ReturnTag(sql, "ss");
        try
        {
            Drpcountry0.SelectedItem.Text = oo.ReturnTag(sql, "Country");
        }
        catch (Exception)
        {
            Drpcountry0.SelectedValue = oo.ReturnTag(sql, "Country");
        }
        string sql1 = "select ltrim(rtrim(StateName)) as StateName, id from StateMaster where CountryId=(select id from CountryMaster where ltrim(rtrim(CountryName))='" + Drpcountry0.SelectedItem.Text + "')";
        oo.FillDropDown_withValue(sql1, drpstate0, "StateName", "id");


        try
        {
            drpstate0.SelectedItem.Text = sts;
        }
        catch (Exception)
        {
            drpstate0.SelectedValue = oo.ReturnTag(sql, "State");
        }
        string sql2 = "select ltrim(rtrim(CityName)) as CityName, id from CityMaster  where stateid=(select id from StateMaster where ltrim(rtrim(StateName))='" + drpstate0.SelectedItem.Text + "')";
        oo.FillDropDown_withValue(sql2, drpCity0, "CityName", "id");
        int s = 0;
        int.TryParse(oo.ReturnTag(sql, "City"), out s);
        if (s > 0)
        {
            drpCity0.SelectedValue = oo.ReturnTag(sql, "City");
        }
        else
        {
            drpCity0.SelectedItem.Text = oo.ReturnTag(sql, "City");
        }
        txtzip0.Text = oo.ReturnTag(sql, "Zip");
        txtphone2.Text = oo.ReturnTag(sql, "Phone1");
        txtphoneno3.Text = oo.ReturnTag(sql, "Phone2");
        txtmobile0.Text = oo.ReturnTag(sql, "Mobile");
        txtFax0.Text = oo.ReturnTag(sql, "Fax");
        txtEmail0.Text = oo.ReturnTag(sql, "Email");
        txtwebsite0.Text = oo.ReturnTag(sql, "Website");
        txtRemark0.Text = oo.ReturnTag(sql, "Remark");



        Panel1_ModalPopupExtender.Show();
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        Button8.Focus();
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label37");
        string ss = lblId.Text;
        lblvalue.Text = ss.ToString();
        Panel2_ModalPopupExtender.Show();
    }
    protected void Drpcountry0_SelectedIndexChanged(object sender, EventArgs e)
    {
        sql = "select ltrim(rtrim(StateName)) as StateName, id from StateMaster where CountryId=" + Drpcountry0.SelectedValue + "";
        oo.FillDropDown_withValue(sql, drpstate0, "StateName", "id");
        sql = "select ltrim(rtrim(CityName)) as CityName, id from CityMaster  where stateid=" + drpstate0.SelectedValue + "";
        oo.FillDropDown_withValue(sql, drpCity0, "CityName", "id");
        Panel1_ModalPopupExtender.Show();
    }
    protected void drpstate0_SelectedIndexChanged(object sender, EventArgs e)
    {
        sql = "select ltrim(rtrim(CityName)) as CityName, id from CityMaster  where stateid=" + drpstate0.SelectedValue + "";
        oo.FillDropDown_withValue(sql, drpCity0, "CityName", "id");
        Panel1_ModalPopupExtender.Show();
    }
    protected void drpCity0_SelectedIndexChanged(object sender, EventArgs e)
    {
        Panel1_ModalPopupExtender.Show();
    }
    protected void LinkButton2_Click1(object sender, EventArgs e)
    {

    }


    public void PermissionGrant(int add1, int delete1, int update1, LinkButton Ladd, Button Ldelete, LinkButton LUpdate)
    {


        if (add1 == 1)
        {
            Ladd.Enabled = true;
        }
        else
        {
            Ladd.Enabled = false;
        }


        if (delete1 == 1)
        {
            Ldelete.Enabled = true;
        }
        else
        {
            Ldelete.Enabled = false;
        }

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
        sql = sql + " left join LoginType Lt on LTb.LoginTypeId=Lt.LoginTypeId where LT.LoginTypeName='" + Session["Logintype"] + "' and LTb.LoginName='" + Session["LoginName"] + "'";
        int a, u, d;
        a = Convert.ToInt32(oo.ReturnTag(sql, "add1"));
        u = Convert.ToInt32(oo.ReturnTag(sql, "update1"));
        d = Convert.ToInt32(oo.ReturnTag(sql, "delete1"));

        PermissionGrant(a, d, u, (LinkButton)LinkButton1, btnDelete, LinkButton4);
    }




}
