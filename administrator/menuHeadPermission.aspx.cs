using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class menuHeadPermission : Page
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
            TreeView1.Nodes.Clear();
            populateMenuItem();
            TreeView1.CollapseAll();
            TreeView1.Attributes.Add("onclick", "OnTreeClick(event)");
        }
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        TreeView1.Nodes.Clear();
        populateMenuItem();
    }
    private void populateMenuItem()
    {
        TreeView1.Nodes.Clear();
        try
        {
            DataTable menuData = GetMenuData();
            AddTopMenuItems(menuData);
        }
        catch (Exception) { }
    }

    private DataTable GetMenuData()
    {
        string sql = "";
        sql = "SELECT MenuID,ParentID,text FROM MenueAM where " + DropDownList2.SelectedItem.Text.ToString() + "=1 and ParentID is null and isnull(Url, '')='' order by MenuID asc";
        con.Open();
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        try
        {
            da.Fill(dt);
            con.Close();
        }
        catch (Exception) { }
        return dt;
    }
    private void AddTopMenuItems(DataTable menuData)
    {
        try
        {
            DataView view = new DataView(menuData);
            string sql = string.Empty;

            view.RowFilter = "ParentId is null";

            foreach (DataRowView row in view)
            {
                TreeNode newMenuItem = new TreeNode(row["text"].ToString(), row["MenuID"].ToString());
                TreeView1.Nodes.Add(newMenuItem);
            }
            for (int i = 0; i < TreeView1.Nodes.Count; i++)
            {
                sql = "select case when isnull(Permission,0)=0 then 'false' else 'true' end Permissions from Menueam where ParentID is null and " + DropDownList2.SelectedItem.Text.ToString() + "=1 and isnull(Url, '')='' and menuid=" + TreeView1.Nodes[i].Value + "";
                if (oo.ReturnTag(sql, "Permissions").ToString().ToLower() == "true")
                {
                    TreeView1.Nodes[i].Checked = true;
                }
                else
                {
                    TreeView1.Nodes[i].Checked = false;
                }
            }
        }
        catch
        { }
    }

    private void AddChildMenuItems(DataTable menuData, TreeNode parentMenuItem)
    {
        try
        {
            DataView view = new DataView(menuData);
            view.RowFilter = "ParentID=" + parentMenuItem.Value;
            foreach (DataRowView row in view)
            {
                TreeNode newMenuItem = new TreeNode(row["text"].ToString(), row["MenuID"].ToString());
                parentMenuItem.ChildNodes.Add(newMenuItem);
                AddChildMenuItems(menuData, newMenuItem);
            }
        }
        catch
        { }
    }
    
    protected void Button1_Click(object sender, System.EventArgs e)
    {
        con.Open();
        for (int i = 0; i < TreeView1.Nodes.Count; i++)
        {
            if (TreeView1.Nodes[i].Checked)
            {
                sql = "update Menueam set Permission=1 where ParentID is null and " + DropDownList2.SelectedItem.Text.ToString() + "=1 and isnull(Url, '')='' and menuid="+ TreeView1.Nodes[i].Value + "";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
            else
            {
                sql = "update Menueam set Permission=0 where ParentID is null and " + DropDownList2.SelectedItem.Text.ToString() + "=1 and isnull(Url, '')='' and menuid=" + TreeView1.Nodes[i].Value + "";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
        }
        con.Close();
        populateMenuItem();
        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted Successfully.", "S");
    }
}