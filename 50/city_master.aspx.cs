using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System;


public partial class admin_city_master : Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);

        if (!IsPostBack)
        {
            sql = "Select CountryName, id from CountryMaster order by CountryName asc";
            oo.FillDropDown_withValue(sql, DrpCountry, "CountryName", "id");
            string sqls = "select top(1) defaultvalue from DefaultSelectedValue where defaultvalueof='Country'";
            if (oo.Duplicate(sqls))
            {
                DrpCountry.SelectedValue = oo.ReturnTag(sqls, "defaultvalue");
            }


            sql = "Select StateName, id from stateMaster where countryId="+ DrpCountry.SelectedValue + " order by StateName asc";
            oo.FillDropDown_withValue(sql, DrpState, "StateName", "id");
            oo.FillDropDown_withValue(sql, DrpStatePanel, "StateName", "id");
            sqls = "select top(1) defaultvalue from DefaultSelectedValue where defaultvalueof='State'";
            if (oo.Duplicate(sqls))
            {
                DrpState.SelectedValue = oo.ReturnTag(sqls, "defaultvalue");
            }
            LoadData();
        }
    }
    protected void LoadData()
    {
        sql = "select ROW_NUMBER() OVER (ORDER BY CM.Id ASC) AS SrNo,CM.Id as  ID, CU.CountryName,SM.StateName,CM.CityName from CityMaster CM";
        sql = sql + "   left join StateMaster SM on CM.StateId=SM.Id";
        sql = sql + "    left join CountryMaster CU on CM.CountryId=CU.Id";
        sql = sql + " where cm.countryId="+ DrpCountry.SelectedValue + " and  cm.stateid=" + DrpState.SelectedValue + "";
        Grd.DataSource = oo.GridFill(sql);
        Grd.DataBind();
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        sql=" select cityName from CityMaster ";
        sql = sql + " where Stateid='" + DrpState.SelectedValue.ToString() + "' and CountryId='" + DrpCountry.SelectedValue.ToString() + "'  and cityName='"+txtCity.Text+"'";
        if (oo.Duplicate(sql))
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Duplicate Entry!", "A");
            oo.MessageBox("Duplicate Entry!", this.Page);
        }
        else
        {

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "CityMasterProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@CountryId", DrpCountry.SelectedValue);
            cmd.Parameters.AddWithValue("@StateId", DrpState.SelectedValue);
            cmd.Parameters.AddWithValue("@CityName", txtCity.Text.ToString());
            cmd.Parameters.AddWithValue("@Remark", txtremark.Text.ToString());
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully,", "S");
                txtCity.Focus();
                LoadData();
                txtCity.Text = "";
                txtremark.Text = "";
            }
            catch (Exception) { }
        }
    }

    protected void DrpCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        sql = "Select StateName, id from stateMaster where countryId=" + DrpCountry.SelectedValue + " order by StateName asc";
        oo.FillDropDown_withValue(sql, DrpState, "StateName", "id");
        LoadData();
    }
    protected void DrpState_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadData();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        sql = " select cityName from CityMaster ";
        sql = sql + " where Stateid='" + DrpStatePanel.SelectedValue.ToString() + "' and cityName='" + txtCityPanel.Text + "' and id<>" + lblID.Text + "";
        if (oo.Duplicate(sql))
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, Div1, "Duplicate Entry!", "A");
            Panel1_ModalPopupExtender.Show();
        }
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "CityMasterProcUpdate";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@id", lblID.Text);
        cmd.Parameters.AddWithValue("@stateid", DrpStatePanel.SelectedValue);
        cmd.Parameters.AddWithValue("@CityName", txtCityPanel.Text);
        cmd.Parameters.AddWithValue("@Remark", txtRemarkPanel.Text);
        cmd.Connection = con;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully,", "S");
            LoadData();
        }
        catch (SqlException) { }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        sql = "Delete from CityMaster where Id=" + lblvalue.Text;

        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = sql;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Duplicate Entry!", "A");
            LoadData();
        }
        catch (SqlException) { }
    }
    protected void Button8_Click(object sender, EventArgs e)
    {

    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label36");
        string ss = lblId.Text;
        lblID.Text = ss;
        try
        {
            sql = "select countryid, stateid, CityName,remark from CityMaster";
            sql = sql + " where id=" + ss;
            DrpStatePanel.SelectedValue = oo.ReturnTag(sql, "stateid");
            txtCityPanel.Text = oo.ReturnTag(sql, "CityName");
            txtRemarkPanel.Text = oo.ReturnTag(sql, "Remark");
            Panel1_ModalPopupExtender.Show();
        }
        catch (Exception) { }
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
    protected void DrpStatePanel_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    
}