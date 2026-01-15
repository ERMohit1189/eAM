using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;

public partial class state_master : Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        TextBox1.Focus();

  Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

        
        con = oo.dbGet_connection();
        //con.ConnectionString = ConfigurationManager.ConnectionStrings["Menu"].ConnectionString;
        if (!IsPostBack)
        {
            try
            {
                CheckValueADDDeleteUpdate();
            }
            catch (Exception) { }
            
            sql = "Select CountryName, id from CountryMaster order by CountryName asc";
            oo.FillDropDown_withValue(sql, DropDownList1, "CountryName", "id");
            oo.FillDropDown_withValue(sql, ddlCountry0, "CountryName", "id");
            string sqls = "select top(1) defaultvalue from DefaultSelectedValue where defaultvalueof='Country'";
            if (oo.Duplicate(sqls))
            {
                DropDownList1.SelectedValue = oo.ReturnTag(sqls, "defaultvalue");
            }
            
            sql = "Select  ROW_NUMBER() OVER (ORDER BY SM.Id ASC) AS SrNo,SM.Id, CM.CountryName,SM.StateName,SM.Remark from StateMaster SM";
            sql = sql + "  left join CountryMaster CM on SM.CountryId=CM.Id where SM.CountryId='" + DropDownList1.SelectedValue + "'";
            Grd.DataSource = oo.GridFill(sql);
            Grd.DataBind();
        }
       // Grd.FooterRow.Visible = false;

    }
    public void fillgrid()
    {
        sql = "Select  ROW_NUMBER() OVER (ORDER BY SM.Id ASC) AS SrNo,SM.Id, CM.CountryName,SM.StateName,SM.Remark from StateMaster SM";
        sql = sql + "  left join CountryMaster CM on SM.CountryId=CM.Id where SM.CountryId='" + DropDownList1.SelectedValue + "'";
        Grd.DataSource = oo.GridFill(sql);
        Grd.DataBind();
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        sql=" select s.stateName as statename from StateMaster ";
        sql = sql + " where stateName='" + TextBox1.Text + "' and Countryid='" + DropDownList1.SelectedValue + "'";
        if (oo.Duplicate(sql))
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Duplicate Entry!", "A");
        }
        else if (DropDownList1.SelectedItem.ToString() == "<--Select-->")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Choose  the Select Option", "A");

        }
        else
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "StateMasterProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            string DD = DropDownList1.SelectedValue;

            cmd.Parameters.AddWithValue("@CountryId", DD);
            cmd.Parameters.AddWithValue("@StateName", TextBox1.Text.ToString());
            cmd.Parameters.AddWithValue("@Remark", TextBox2.Text.ToString());
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());//Session["Login"].ToString());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
            try
            {

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");
                oo.ClearControls(this.Page);
                sql = "Select  ROW_NUMBER() OVER (ORDER BY SM.Id ASC) AS SrNo,SM.Id, CM.CountryName,SM.StateName,SM.Remark from StateMaster SM";
                sql = sql + "  left join CountryMaster CM on SM.CountryId=CM.Id where SM.CountryId='" + DropDownList1.SelectedValue + "'";
                Grd.DataSource = oo.GridFill(sql);
                Grd.DataBind();
                TextBox1.Text = "";
                TextBox2.Text = "";
            }
            catch (Exception) { }
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        sql = " select s.stateName as statename from StateMaster ";
        sql = sql + " where stateName='" + txtStatePanel.Text + "' and Countryid='" + ddlCountry0.SelectedValue + "' and id<>"+ lblID.Text + "";
        if (oo.Duplicate(sql))
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, Div1, "Duplicate Entry!", "A");
            Panel1_ModalPopupExtender.Show();
        }
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "StateMasterUpdateProc";        
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Id", lblID.Text);
        cmd.Parameters.AddWithValue("@StateName", txtStatePanel.Text);
        cmd.Parameters.AddWithValue("@Remark", txtRemarkPanel.Text);

        cmd.Connection = con;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "S");
            fillgrid();
        }
        catch (SqlException) { }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        sql = "Delete from StateMaster where Id=" + lblvalue.Text;

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
            fillgrid();

        }
        catch (SqlException) {
           
        }

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

        sql = "Select  Id, countryId,StateName as StateName,Remark as Remark from StateMaster";
        sql = sql + " where sm.Id=" + ss;

        ddlCountry0.SelectedValue = oo.ReturnTag(sql, "countryId");
        txtStatePanel.Text = oo.ReturnTag(sql, "StateName");

        txtRemarkPanel.Text = oo.ReturnTag(sql, "Remark");

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
        //fillgrid();
    }


    public void PermissionGrant(int add1, int delete1, int update1, LinkButton Ladd, Button Ldelete, Button LUpdate)
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

        PermissionGrant(a, d, u, (LinkButton)LinkButton1, btnDelete, Button3);
    }



    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        sql = "Select  ROW_NUMBER() OVER (ORDER BY SM.Id ASC) AS SrNo,SM.Id, CM.CountryName,SM.StateName,SM.Remark from StateMaster SM";
        sql = sql + "  left join CountryMaster CM on SM.CountryId=CM.Id where SM.CountryId='" + DropDownList1.SelectedValue+"'";
        Grd.DataSource = oo.GridFill(sql);
        Grd.DataBind();

    }
}