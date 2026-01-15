using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;

public partial class SubMenu : System.Web.UI.Page
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
            sql = "select text+(Case When Admin=1 then '- Admin' When Staff=1 then '- Staff' Else 'Not Access' End),MenuId from MenueAM where ParentId is NULL order by MenuID";
            oo.FillDropDown_withValue_withSelect(sql, DrpMainMenu, "text","MenuId");
            DataGridBind();
        }
    }

    public void DataGridBind()
    {
        sql = "select ROW_NUMBER() OVER (ORDER BY MenuID ASC) AS SrNo,MenuId,Text ,Text as SubMenu,Description as SubMenuDesc,MenuId as Mid from MenueAM";
        sql = sql + " Where Parentid='" + DrpMainMenu.SelectedValue.ToString() + "' order by MenuID";

        GridView1.DataSource = oo.GridFill(sql);
        GridView1.DataBind();
    }
    protected void DrpMainMenu_SelectedIndexChanged(object sender, EventArgs e)
    {

        DataGridBind();   


    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        string MainMenuId = "";


        sql = "select Description from MenueAM where Description='" + txtMenuDesc.Text + "'";
        if (oo.Duplicate(sql))
        {
            //oo.MessageBox("Duplicate Description Name",this.Page);
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
            cmd.Parameters.AddWithValue("@url", txtUrl.Text);

            sql = "Select MenuId from MenueAM where text='" + DrpMainMenu.SelectedItem.ToString() + "'";
            MainMenuId = oo.ReturnTag(sql, "MenuId");

            cmd.Parameters.AddWithValue("@ParrentId", MainMenuId);

            //@SubMenuName,@SubmenuDesc,@ParrentId
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                oo.ClearControls(this.Page);
                //oo.MessageBox("Successfully Submitted", this.Page);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");

                DataGridBind();
            }
            catch (SqlException) { }
        }
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
    protected void LinkButton2_Click(object sender, EventArgs e)
    {

        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label36");
        string ss = lblId.Text;
        lblID.Text = ss;
        try
        {
            // sql = "Select  distinct ROW_NUMBER() OVER (ORDER BY p.ProductId ASC) AS  [ProductId] ,po.id as ID, pc.ProductCategoryName as[ProductCategoryName], p.ProductName as [ProductName], Pm.ProductTypeName as [ProductTypeName],PO.ProductModelName as [ProductModelName] from Productcategorymaster pc left join ProductName p on p.ProductId=pc.ProductId  left join ProductTypeMaster PM on p.ProductId=PM.ProductId left join ProductModelMaster PO on p.ProductId=PO.ProductId ";        
            sql = " select ROW_NUMBER() OVER (ORDER BY mt1.MenuID ASC) AS SrNo,mt.MenuId,mt.Text ,mt1.Text as SubMenu,mt1.Description as SubMenuDesc,mt1.MenuId as Mid,mt1.url as URL from MenueAM mt     ";
            sql = sql + "  left join MenueAM mt1 on mt.MenuID=mt1.ParentID     ";
            sql = sql + "  where mt1.MenuId=" + ss;
            txtSubMenuPanel.Text = oo.ReturnTag(sql, "SubMenu");
            txtSubMenuDescPanel.Text = oo.ReturnTag(sql, "SubMenuDesc");
            txtUrlPanel.Text = oo.ReturnTag(sql, "url");
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
            //oo.MessageBox("Deleted successfully.", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Deleted successfully.", "S");

            DataGridBind();
        }
        catch (SqlException) { }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {  
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "SubMenuUpdateProc";
        cmd.CommandType = CommandType.StoredProcedure;       
        cmd.Parameters.AddWithValue("@SubMenuName", txtSubMenuPanel.Text);
        cmd.Parameters.AddWithValue("@SubmenuDesc", txtSubMenuDescPanel.Text);
        cmd.Parameters.AddWithValue("@ParrentId", DrpMainMenu.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@MenuId", lblID.Text);
        cmd.Parameters.AddWithValue("@url",txtUrlPanel.Text);
        cmd.Connection = con;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            //oo.MessageBox("Updated successfully.", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "S");

            DataGridBind();
            oo.ClearControls(this.Page);
        }
        catch (SqlException) { }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {

    }
    protected void Button8_Click(object sender, EventArgs e)
    {

    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}