using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;


public partial class SubMenuHierarchical : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    string sql = "";
    Campus oo = new Campus();
    protected void Page_Load(object sender, EventArgs e)
    {
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            sql = "select text+(Case When Admin=1 then '- Admin' When Staff=1 then '- Staff' Else 'Not Access' End) text,MenuId from MenueAM where ParentId is NULL order by MenuID";
            oo.FillDropDown_withValue_withSelect(sql, DrpMainMenu, "text", "MenuId");

            DataBindGrid();
        }
    }


    public void DataBindMenu()
    {
        sql = "select Text as SubMenu,MenuId from MenueAM Where Parentid='" + DrpMainMenu.SelectedValue.ToString() + "' order by MenuID";
        oo.FillDropDown_withValue_withSelect(sql, DrpSubMenu, "SubMenu", "MenuId");
       
    }

    public void DataBindGrid()
    {

        sql = "Select ROW_NUMBER() OVER (ORDER BY T1.MenuID ASC) AS SrNo,T1.Parentid,T1.MenuId,T1.Text,T2.Text ParentMenu,SubMenu,";
        sql = sql + " SubMenuDesc,Mid from (select ROW_NUMBER() OVER (ORDER BY MenuID ASC) AS SrNo,Parentid,MenuId,Text ,Text as SubMenu,";
        sql = sql + " Description as SubMenuDesc,MenuId as Mid from MenueAM Where Parentid='" + DrpSubMenu.SelectedValue.ToString() + "') T1";
        sql = sql + " Inner Join MenueAM T2 on t2.MenuID=T1.ParentID";
        sql = sql + " order by T1.MenuID";

        GridView1.DataSource = oo.GridFill(sql);
        GridView1.DataBind();

    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
         sql = "select Description from MenueAM where Description='" + txtMenuDesc.Text + "'";
         if (oo.Duplicate(sql))
         {
             Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Duplicate Description Name", "A");
         }
         else
         {
             SqlCommand cmd = new SqlCommand();
             cmd.CommandType = CommandType.StoredProcedure;
             cmd.CommandText = "SubMenuProc";
             cmd.Connection = con;
             cmd.Parameters.AddWithValue("@SubMenuName", txtSubMenu.Text);
             cmd.Parameters.AddWithValue("@SubmenuDesc", txtMenuDesc.Text);
             cmd.Parameters.AddWithValue("@ParrentId", DrpSubMenu.SelectedValue.ToString());
             cmd.Parameters.AddWithValue("@url", txtUrl.Text);

             try
             {
                 con.Open();
                 cmd.ExecuteNonQuery();
                 con.Close();
                 oo.ClearControls(this.Page);
                 Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully", "S");
                 GridData();
             }
             catch (SqlException) { }
         }
    }


    public void GridData()
    
    {
        DataBindGrid();
    }

    protected void DrpSubMenu_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridData();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        string MainMenuId = "";

        sql = "Select MenuId from MenueAM where Description='" + DrpSubMenu.SelectedItem.ToString() + "'";
        MainMenuId = oo.ReturnTag(sql, "MenuId");
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "SubMenuUpdateProc";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@SubMenuName", txtSubMenuPanel.Text);
        cmd.Parameters.AddWithValue("@SubmenuDesc", txtSubMenuDescPanel.Text);
        cmd.Parameters.AddWithValue("@ParrentId", DrpSubMenu.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@MenuId", lblID.Text);
        cmd.Parameters.AddWithValue("@url", txtUrlPanel.Text);
        cmd.Connection = con;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully", "S");

            GridData();
            oo.ClearControls(this.Page);
        }
        catch (SqlException) { }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {

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
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Deleted successfully", "S");

            GridData();
        }
        catch (SqlException) { }

    }
    protected void Button8_Click(object sender, EventArgs e)
    {

    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {  
        string mid = "";
        mid = oo.ReturnTag(sql, "mid");
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label36");
        string ss = lblId.Text;
        lblID.Text = ss;
        try
        {
            sql = "     select ROW_NUMBER() OVER (ORDER BY mt.MenuID ASC) AS SrNo ,Mt.description as SubMenuDescription,mt.MenuId as MID,mt.Text as ParrentMenu, ";
            sql = sql + "   mt.Text as SubMenu ,mt1.MenuId as mid,mt.url as URL from MenueAM mt   ";
            sql = sql + " left join MenueAM mt1 on mt.MenuID=mt1.ParentID     ";
            sql = sql + "   where Mt.MenuId=" + ss;

            txtSubMenuPanel.Text = oo.ReturnTag(sql, "SubMenu");
            txtSubMenuDescPanel.Text = oo.ReturnTag(sql, "SubMenuDescription");
            txtUrlPanel.Text = oo.ReturnTag(sql, "url");
            Panel1_ModalPopupExtender.Show();

        }
        catch (Exception) { }


    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label37");
        string ss = lblId.Text;
        lblvalue.Text = ss.ToString();
        Panel2_ModalPopupExtender.Show();
        Button8.Focus();
    }
    protected void DrpMainMenu_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        DataBindMenu();
        GridData();
    }
}