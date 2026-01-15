using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;

public partial class admin_country_master : Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        //con.ConnectionString = ConfigurationManager.ConnectionStrings["Menu"].ConnectionString;
        con = oo.dbGet_connection();
        if (!IsPostBack)
        {
            try
            {
                CheckValueADDDeleteUpdate();
            }
            catch (Exception) { }
            sql = "Select  ROW_NUMBER() OVER (ORDER BY Id ASC) AS SrNo,Id, CountryName,Remark from CountryMaster";
            Grd.DataSource = oo.GridFill(sql);
            Grd.DataBind();

        }
        Grd.FooterRow.Visible = false;
        TextBox1.Focus();
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        //@CountryName nvarchar(100),@Remark nvarchar(500),@BranchCode nvarchar(50),@LoginName nvarchar(50),@SessionName nvarchar(50))

        sql = "select CountryName from CountryMaster  where CountryName='" + TextBox1.Text + "'";
        if (oo.Duplicate(sql))
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Duplicate Entry!", "A");
        }
        else
        {

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "CountryMasterProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@CountryName", TextBox1.Text.ToString());
            cmd.Parameters.AddWithValue("@Remark", TextBox2.Text.ToString());
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");
                oo.ClearControls(this.Page);
                sql = "Select  ROW_NUMBER() OVER (ORDER BY Id ASC) AS SrNo,Id, CountryName,Remark from CountryMaster";
                Grd.DataSource = oo.GridFill(sql);
                Grd.DataBind();
                TextBox1.Text = "";
                TextBox2.Text = "";

            }
            catch (Exception) { }
        }

        TextBox1.Focus();
    }


    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label36");
        string ss = lblId.Text;
        lblID.Text = ss;

        sql = "Select  ROW_NUMBER() OVER (ORDER BY Id ASC) AS SrNo,Id, CountryName,Remark from CountryMaster";
        sql = sql + " where id=" + ss;


        txtRemarkPanel.Text = oo.ReturnTag(sql, "Remark");
        txtCountryPanel.Text = oo.ReturnTag(sql, "CountryName");
    

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

    protected void Button3_Click(object sender, EventArgs e)
    {
        sql = "select CountryName from CountryMaster  where CountryName='" + txtCountryPanel.Text + "' and id<>"+ lblID.Text + "";
        if (oo.Duplicate(sql))
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, Div1, "Duplicate Entry!", "A");
            Panel1_ModalPopupExtender.Show();
        }
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "CountryMasterUpdateProc";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@id", lblID.Text);
        cmd.Parameters.AddWithValue("@CountryName", txtCountryPanel.Text.ToString());
        cmd.Parameters.AddWithValue("@Remark", txtRemarkPanel.Text.ToString());
      
        cmd.Connection = con;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            //oo.MessageBox("Updated successfully.", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "S");
            sql = "Select  ROW_NUMBER() OVER (ORDER BY Id ASC) AS SrNo,Id, CountryName,Remark from CountryMaster";
            Grd.DataSource = oo.GridFill(sql);
            Grd.DataBind();
        }
        catch (SqlException) { }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        sql = "Delete from CountryMaster where Id=" + lblvalue.Text;

        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = sql;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            //oo.MessageBox("Deleted successfully.", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Deleted successfully.", "S");
            sql = "Select  ROW_NUMBER() OVER (ORDER BY Id ASC) AS SrNo,Id, CountryName,Remark from CountryMaster";
            Grd.DataSource = oo.GridFill(sql);
            Grd.DataBind();
        }
        catch (SqlException) { }
    }
    protected void Button8_Click(object sender, EventArgs e)
    {

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

    protected void Grd_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
}
    
