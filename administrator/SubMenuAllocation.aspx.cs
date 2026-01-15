using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;


public partial class MainMenuAllocation : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    string sql = "";
    bool flag = false;
    Campus oo = new Campus();
    protected void Page_Load(object sender, EventArgs e)
    {
        con = oo.dbGet_connection();
        if (!IsPostBack)
        {
            sql = "select text+(Case When Admin=1 then '- Admin' When Staff=1 then '- Staff' Else 'Not Access' End),MenuId from MenueAM where ParentId is NULL order by MenuID";
            oo.FillDropDown_withValue_withSelect(sql, DrpMainMenu, "text", "MenuId");
        }

    }
    public void DataBindMenu()
    {
        sql = "Select ROW_NUMBER() OVER (ORDER BY T1.MenuID ASC) AS SrNo,T1.Parentid,T1.MenuId,T1.Text,T2.Text ParentMenu,SubMenu,";
        sql = sql + " SubMenuDesc,Mid,Case When T1.Admin=1 then 'Admin' When T1.Staff=1 then 'Staff' Else 'Not Access' End MenuFor";
        sql = sql + " from (select ROW_NUMBER() OVER (ORDER BY MenuID ASC) AS SrNo,Parentid,MenuId,Text ,Text as SubMenu,";
        sql = sql + " Description as SubMenuDesc,MenuId as Mid,Admin,Staff from MenueAM Where Parentid='" + DrpMainMenu.SelectedValue.ToString() + "') T1";
        sql = sql + " Inner Join MenueAM T2 on t2.MenuID=T1.ParentID";
        sql = sql + " order by T1.MenuID";

        GridView1.DataSource = oo.GridFill(sql);
        GridView1.DataBind();
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            Label lblSubMenuID = (Label)GridView1.Rows[i].FindControl("Label4");
            CheckBox chk = (CheckBox)GridView1.Rows[i].FindControl("CheckBox1");

            string allotmentfor = DrpMainMenu.SelectedItem.Text.Split('-')[1].Trim();
            if (chk.Checked == true)
            {
              
                if (allotmentfor == "Admin")
                {
                    UpdateAdmin(lblSubMenuID.Text);
                    flag = true;
                }
                if (allotmentfor == "Staff")
                {
                    UpdateStaff(lblSubMenuID.Text);
                    flag = true;
                }

            }

            else
            {
                if (allotmentfor == "Admin")
                {
                    UpdateAdminZero(lblSubMenuID.Text);
                    flag = true;
                }
                if (allotmentfor == "Staff")
                {
                    UpdateStaffZero(lblSubMenuID.Text);
                    flag = true;
                }
            }
        }
    
         if (flag)
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully", "S");
            DataBindMenu();
        }

    }

    public void UpdateAdmin(string Mid)
    {
        flag = false;
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "update MenueAM set admin=1 where  MenuId="+Mid;
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

    public void UpdateStaff(string Mid)
    {
        flag = false;
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "update MenueAM set Staff=1 where  MenuId=" + Mid;
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
   
    public void UpdateAdminZero(string Mid)
    {
        flag = false;
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "update MenueAM set admin=0 where  MenuId="+Mid;
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

    public void UpdateStaffZero(string Mid)
    {
        flag = false;
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "update MenueAM set Staff=0 where  MenuId="+Mid;
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

    
    protected void DrpSubMenu_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
    protected void DrpMainMenu_SelectedIndexChanged(object sender, EventArgs e)
    {
      
        DataBindMenu();
     
       
    }
    protected void LinkButton6_Click(object sender, EventArgs e)
    {
       
    }
    protected void DrpAllotement_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
