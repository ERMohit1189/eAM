using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;

public partial class MenuMaster : System.Web.UI.Page
{
    Campus oo = new Campus();
    SqlConnection con = new SqlConnection();
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            DataBindTable();
        }
    }

    public void DataBindTable()
    {
        sql = " select ROW_NUMBER() OVER (ORDER BY MenuID ASC) AS SrNo,MenuID,Text,Description,Url,";
        sql = sql + " Case When Admin=1 then 'Admin' When Staff=1 then 'Staff' Else 'Not Access' End MenuFor  from MenueAM where ParentId is null and " + drpMenufor.SelectedItem.Text + "=1";
        GridView1.DataSource = oo.GridFill(sql);
        GridView1.DataBind();
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        sql = "select  MenuID from MenueAM where MenueAM where Text='" + txtMainMenu.Text + "'";
        if (oo.Duplicate(sql))
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, mssgbox, "Duplicate Entry!", "A");
        }
        else
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "MainMenuProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Text", txtMainMenu.Text);
            cmd.Parameters.AddWithValue("@Desc", txtDescription.Text);
            cmd.Parameters.AddWithValue("@Admin", drpMenufor.SelectedIndex == 0 ? 1 : 0);
            cmd.Parameters.AddWithValue("@Staff", drpMenufor.SelectedIndex == 1 ? 1 : 0);

            cmd.Connection = con;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Campus camp = new Campus(); camp.msgbox(this.Page, mssgbox, "Submitted successfully.", "S");
                DataBindTable();
            }
            catch (Exception) { }

        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "MainMenuUpdateProc";
        cmd.CommandType = CommandType.StoredProcedure;      
        cmd.Parameters.AddWithValue("@MenuId", lblID.Text);
        cmd.Parameters.AddWithValue("@Text", txtMainMenuPanel.Text);
        cmd.Parameters.AddWithValue("@Desc", txtMainMenuDescPanel.Text);
        //cmd.Parameters.AddWithValue("@Admin", drpMenuforPanel.SelectedIndex == 0 ? 1 : 0);
        //cmd.Parameters.AddWithValue("@Staff", drpMenuforPanel.SelectedIndex == 1 ? 1 : 0);
    
        cmd.Connection = con;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Campus camp = new Campus(); camp.msgbox(this.Page, mssgbox, "Updated successfully.", "S");
            DataBindTable();
            oo.ClearControls(this.Page);
        }
        catch (SqlException) { }
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label37");
        string ss = lblId.Text;

        lblvalue.Text = ss.ToString();
        Panel2_ModalPopupExtender.Show();
        Button8.Focus();
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label36");
        string ss = lblId.Text;
        lblID.Text = ss;

        try
        {
            sql = " select ROW_NUMBER() OVER (ORDER BY MenuID ASC) AS SrNo,MenuID,Text,Description,Url,";
            sql = sql + " Case When Admin=1 then 'Admin' When Staff=1 then 'Staff' End MenuFor  from MenueAM where ParentId is null and "+drpMenufor.SelectedItem.Text+"=1 and MenuId=" + ss;

            txtMainMenuPanel.Text = oo.ReturnTag(sql, "Text");
            txtMainMenuDescPanel.Text = oo.ReturnTag(sql, "Description");

            drpMenuforPanel.SelectedIndex = oo.ReturnTag(sql, "MenuFor") == "Admin" ? 0 : 1;
        
          Panel1_ModalPopupExtender.Show();
        }
        catch (Exception) { }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        sql = "Delete from MenueAM where menuId=" + lblvalue.Text;

        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = sql;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Campus camp = new Campus(); camp.msgbox(this.Page, mssgbox, "Deleted successfully.", "S");
            DataBindTable();
        }
        catch (SqlException) { }
    }
    protected void drpMenufor_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataBindTable();
    }
}