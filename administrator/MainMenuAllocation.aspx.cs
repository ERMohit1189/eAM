using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;


public partial class MainMenuAllocation : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    string sql = "";
#pragma warning disable 414
    bool flag;
#pragma warning restore 414
    Campus oo = new Campus();

    public MainMenuAllocation()
    {
        flag = false;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        con = oo.dbGet_connection();
        if (!IsPostBack)
        {
            loadMenu();
        }

    }

    public void loadMenu()
    {
        sql = "select Menuid,ROW_NUMBER() OVER (ORDER BY MenuID ASC) AS SrNo ,text,";
        sql = sql + " Case When Admin=1 then 'Admin' When Staff=1 then 'Staff' Else 'Not Access' End MenuFor from MenueAM where ParentId is NULL and " + DrpAllotement.SelectedItem.Text + " in (0,1)";
        GridView1.DataSource = oo.GridFill(sql);
        GridView1.DataBind();
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        bool flag =false;
        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            Label lblid = (Label)GridView1.Rows[i].FindControl("lblid");
            CheckBox chk = (CheckBox)GridView1.Rows[i].FindControl("CheckBox1");

            if (chk.Checked == true)
            {
                if (DrpAllotement.SelectedItem.ToString() == "Admin")
                {
                    UpdateAdmin(lblid.Text);
                    flag = true;
                }
                if (DrpAllotement.SelectedItem.ToString() == "Staff")
                {
                    UpdateStaff(lblid.Text);
                    flag = true;
                }

            }

            else
            {
                if (DrpAllotement.SelectedItem.ToString() == "Admin")
                {
                    UpdateAdminZero(lblid.Text);
                    flag = true;
                }
                if (DrpAllotement.SelectedItem.ToString() == "Staff")
                {
                    UpdateStaffZero(lblid.Text);
                    flag = true;
                }
            }
        }

        if (flag)
        {
            //oo.MessageBox("Updated successfully",this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "S");
            loadMenu();
        }

    }


    public void UpdateAdmin(string MenuId)
    {
        flag = false;
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "update MenueAM set admin=1 where Menuid='" + MenuId + "'";
        cmd.CommandType = CommandType.Text;
        cmd.Connection=con;

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                flag = true;
            }
            catch (SqlException) { con.Close(); }
    
    }
   
    public void UpdateStaff(string MenuId)
    {
        flag = false;
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "update MenueAM set staff=1 where Menuid='" + MenuId + "'";
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;

        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            flag = true;
        }
        catch (SqlException) { con.Close(); }

    }

    public void UpdateAdminZero(string MenuId)
    {
        flag = false;
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "update MenueAM set admin=0 where Menuid='" + MenuId + "'";
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;

        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            flag = true;
        }
        catch (SqlException) { con.Close(); }

    }
    public void UpdateStaffZero(string MenuId)
    {
        flag = false;
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "update MenueAM set staff=0 where Menuid='" + MenuId + "'";
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;

        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            flag = true;
        }
        catch (SqlException) { con.Close(); }

    }
    
    protected void DrpAllotement_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadMenu();
    }
}
