using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class SuperAdmin_menu_accessability : Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        con = oo.dbGet_connection();
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            sql = "select (Name+' ('+Ecode+')') LoginName, LoginId, EmpId from GetAllStaffRecords_UDF(" + Session["BranchCode"] + ") asr inner join LoginTab lt on lt.LoginName=asr.Ecode and asr.BranchCode=lt.BranchId where lt.LoginTypeId='" + ddltype.SelectedValue.ToString() + "' and lt.BranchId=" + Session["BranchCode"] + " and (lt.IsActive is NULL or lt.IsActive=1) ";
            oo.FillDropDown_withValue(sql, ddlUser, "LoginName", "LoginId");
            ddlUser.Items.Insert(0, "<--Select-->");
            TreeView1.Nodes.Clear();
            populateMenuItem();
            TreeView1.CollapseAll();
            TreeView1.Attributes.Add("onclick", "OnTreeClick(event)");
            if (ddltype.SelectedIndex == 0)
            {
                div2.Visible = false;
                div3.Visible = false;
                div4.Visible = false;
            }
        }
    }

    public void Grd_Fill()
    {
        sql = "select LoginName,LoginId from LoginTab where LoginTypeId='" + ddltype.SelectedValue.ToString() + "' and (IsActive is NULL or IsActive=1)";
        sql = sql + " and BranchId=" + Session["BranchCode"] + " order by CASE WHEN ISNUMERIC(Right(LoginName,3))=1 THEN Right(LoginName,3) ELSE 0 END";
        oo.FillDropDown_withValue(sql, ddlUser, "LoginName", "LoginId");
        ddlUser.Items.Insert(0, "<--Select-->");
    }

    private void populateMenuItem()
    {
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

        sql = "SELECT MenuID,ParentID,text FROM MenueAM where   isnull(permission, 0)=1 and " + ddltype.SelectedItem.Text.ToString() + "=1  and Status=1 order by MenuOrder asc";


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

    private DataTable GetMenuData2(string id)
    {
        string sql = "";

        sql = "SELECT MenuID,ParentID,text FROM MenueAM where " + ddltype.SelectedItem.Text.ToString() + "=1 and ParentID=" + id + " and Status=1 order by MenuOrder asc";


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
                DataTable menuData2 = GetMenuData2(newMenuItem.Value);
                AddChildMenuItems(menuData2, newMenuItem);
            }
        }
        catch
        { }

    }



    private void AddChildMenuItems(DataTable menuData2, TreeNode parentMenuItem)
    {
        try
        {
            DataView view = new DataView(menuData2);
            view.RowFilter = "ParentID=" + parentMenuItem.Value;
            foreach (DataRowView row in view)
            {
                TreeNode newMenuItem = new TreeNode(row["text"].ToString(), row["MenuID"].ToString());
                parentMenuItem.ChildNodes.Add(newMenuItem);

                DataView view2 = new DataView(GetMenuData2(row["MenuID"].ToString()));
                view2.RowFilter = "ParentID=" + row["MenuID"].ToString();
                foreach (DataRowView rows in view2)
                {
                    TreeNode newMenuItem2 = new TreeNode(rows["text"].ToString(), rows["MenuID"].ToString());
                    newMenuItem.ChildNodes.Add(newMenuItem2);
                }

                AddChildMenuItems(menuData2, newMenuItem);

            }
        }
        catch
        { }
    }


    public void CheckBoxCheck_clear()
    {
        if (TreeView1.Nodes.Count > 0)
        {

            for (int i = 0; i < TreeView1.Nodes.Count; i++)
            {

                TreeView1.Nodes[i].Checked = false;

                for (int j = 0; j < TreeView1.Nodes[i].ChildNodes.Count; j++)
                {

                    TreeView1.Nodes[i].ChildNodes[j].Checked = false;

                    for (int k = 0; k < TreeView1.Nodes[i].ChildNodes[j].ChildNodes.Count; k++)
                    {

                        TreeView1.Nodes[i].ChildNodes[j].ChildNodes[k].Checked = false;

                    }
                }
            }
        }
    }


    public void CheckBoxCheck()
    {
        if (TreeView1.Nodes.Count > 0)
        {

            for (int i = 0; i < TreeView1.Nodes.Count; i++)
            {
                sql = "Select Count(*) as Counter_Value  From menu_permission where Login_Id='" + ddlUser.SelectedItem.Value + "' and BranchCode=" + Session["BranchCode"] + " and Permission_Value='Yes' and Menu_Id='" + TreeView1.Nodes[i].Value + "'";
                if (oo.ReturnTag(sql, "Counter_Value") == "1")
                {
                    TreeView1.Nodes[i].Checked = true;
                }
                for (int j = 0; j < TreeView1.Nodes[i].ChildNodes.Count; j++)
                {
                    sql = "Select Count(*) as Counter_Value  From menu_permission where Login_Id='" + ddlUser.SelectedItem.Value + "' and BranchCode=" + Session["BranchCode"] + " and Permission_Value='Yes' and Menu_Id='" + TreeView1.Nodes[i].ChildNodes[j].Value + "'";
                    if (oo.ReturnTag(sql, "Counter_Value") == "1")
                    {
                        TreeView1.Nodes[i].ChildNodes[j].Checked = true;
                    }
                    for (int k = 0; k < TreeView1.Nodes[i].ChildNodes[j].ChildNodes.Count; k++)
                    {
                        sql = "Select Count(*) as Counter_Value  From menu_permission where Login_Id='" + ddlUser.SelectedItem.Value + "' and BranchCode=" + Session["BranchCode"] + " and Permission_Value='Yes' and Menu_Id='" + TreeView1.Nodes[i].ChildNodes[j].ChildNodes[k].Value + "'";
                        if (oo.ReturnTag(sql, "Counter_Value") == "1")
                        {
                            TreeView1.Nodes[i].ChildNodes[j].ChildNodes[k].Checked = true;
                        }
                    }
                }
            }


        }
    }


    protected void Button1_Click(object sender, System.EventArgs e)
    {
        //try
        //{      
        if (ddlUser.SelectedItem.Text != "<--Select-->")
        {
            if (TreeView1.Nodes.Count > 0)
            {
                sql = "Select Count(*) as Counter From menu_permission Where Login_Id='" + ddlUser.SelectedValue + "' and BranchCode=" + Session["BranchCode"] + "";
                if (oo.ReturnTag(sql, "Counter") == "0")
                {
                    con.Open();
                    for (int i = 0; i < TreeView1.Nodes.Count; i++)
                    {
                        SqlCommand Permission_Header = new SqlCommand("menu_permission_proc", con);
                        Permission_Header.CommandType = CommandType.StoredProcedure;
                        Permission_Header.Parameters.AddWithValue("@Login_Id", ddlUser.SelectedValue);
                        Permission_Header.Parameters.AddWithValue("@Menu_Id", TreeView1.Nodes[i].Value);
                        Permission_Header.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                        if (TreeView1.Nodes[i].Checked == true)
                        {
                            Permission_Header.Parameters.AddWithValue("@Permission_Value", "Yes");
                        }
                        else
                        {
                            Permission_Header.Parameters.AddWithValue("@Permission_Value", "No");
                        }
                        Permission_Header.Parameters.AddWithValue("@task", "Insert");
                        Permission_Header.Parameters.AddWithValue("@Id", "0");
                        Permission_Header.Parameters.AddWithValue("@LoginTypeId", ddltype.SelectedValue.ToString());

                        Permission_Header.ExecuteNonQuery();
                        for (int j = 0; j < TreeView1.Nodes[i].ChildNodes.Count; j++)
                        {
                            SqlCommand Permission_Sub = new SqlCommand("menu_permission_proc", con);
                            Permission_Sub.CommandType = CommandType.StoredProcedure;
                            Permission_Sub.Parameters.AddWithValue("@Login_Id", ddlUser.SelectedValue);
                            Permission_Sub.Parameters.AddWithValue("@Menu_Id", TreeView1.Nodes[i].ChildNodes[j].Value);
                            if (TreeView1.Nodes[i].ChildNodes[j].Checked == true)
                            {
                                Permission_Sub.Parameters.AddWithValue("@Permission_Value", "Yes");
                            }
                            else
                            {
                                Permission_Sub.Parameters.AddWithValue("@Permission_Value", "No");
                            }
                            Permission_Sub.Parameters.AddWithValue("@task", "Insert");
                            Permission_Sub.Parameters.AddWithValue("@Id", "0");
                            Permission_Sub.Parameters.AddWithValue("@LoginTypeId", ddltype.SelectedValue.ToString());
                            Permission_Sub.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                            Permission_Sub.ExecuteNonQuery();
                            for (int k = 0; k < TreeView1.Nodes[i].ChildNodes[j].ChildNodes.Count; k++)
                            {
                                SqlCommand Permission_Store = new SqlCommand("menu_permission_proc", con);
                                Permission_Store.CommandType = CommandType.StoredProcedure;
                                Permission_Store.Parameters.AddWithValue("@Login_Id", ddlUser.SelectedValue);
                                Permission_Store.Parameters.AddWithValue("@Menu_Id", TreeView1.Nodes[i].ChildNodes[j].ChildNodes[k].Value);
                                if (TreeView1.Nodes[i].ChildNodes[j].ChildNodes[k].Checked == true)
                                {
                                    Permission_Store.Parameters.AddWithValue("@Permission_Value", "Yes");
                                }
                                else
                                {
                                    Permission_Store.Parameters.AddWithValue("@Permission_Value", "No");
                                }
                                Permission_Store.Parameters.AddWithValue("@task", "Insert");
                                Permission_Store.Parameters.AddWithValue("@Id", "0");
                                Permission_Store.Parameters.AddWithValue("@LoginTypeId", ddltype.SelectedValue.ToString());
                                Permission_Store.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                                Permission_Store.ExecuteNonQuery();
                            }
                        }
                    }
                    con.Close();
                    //oo.MessageBox("Menu Submitted Successfully", this.Page);
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");
                }
                else
                {
                    for (int i = 0; i < TreeView1.Nodes.Count; i++)
                    {
                        sql = "Select Id From menu_permission where Login_Id='" + ddlUser.SelectedValue + "' and Menu_Id='" + TreeView1.Nodes[i].Value + "'";
                        if (oo.ReturnTag(sql, "Id") != "")
                        {
                            SqlCommand Permission_Header = new SqlCommand("menu_permission_proc", con);
                            Permission_Header.CommandType = CommandType.StoredProcedure;
                            Permission_Header.Parameters.AddWithValue("@Login_Id", ddlUser.SelectedValue);
                            Permission_Header.Parameters.AddWithValue("@Menu_Id", TreeView1.Nodes[i].Value);
                            if (TreeView1.Nodes[i].Checked == true)
                            {
                                Permission_Header.Parameters.AddWithValue("@Permission_Value", "Yes");
                            }
                            else
                            {
                                Permission_Header.Parameters.AddWithValue("@Permission_Value", "No");
                            }
                            Permission_Header.Parameters.AddWithValue("@task", "Update");
                            Permission_Header.Parameters.AddWithValue("@Id", oo.ReturnTag(sql, "Id"));
                            Permission_Header.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                            con.Open();
                            Permission_Header.ExecuteNonQuery();
                            con.Close();
                        }
                        else
                        {
                            con.Open();
                            SqlCommand Permission_Header = new SqlCommand("menu_permission_proc", con);
                            Permission_Header.CommandType = CommandType.StoredProcedure;
                            Permission_Header.Parameters.AddWithValue("@Login_Id", ddlUser.SelectedValue);
                            Permission_Header.Parameters.AddWithValue("@Menu_Id", TreeView1.Nodes[i].Value);
                            if (TreeView1.Nodes[i].Checked == true)
                            {
                                Permission_Header.Parameters.AddWithValue("@Permission_Value", "Yes");
                            }
                            else
                            {
                                Permission_Header.Parameters.AddWithValue("@Permission_Value", "No");
                            }
                            Permission_Header.Parameters.AddWithValue("@task", "Insert");
                            Permission_Header.Parameters.AddWithValue("@Id", "0");
                            Permission_Header.Parameters.AddWithValue("@LoginTypeId", ddltype.SelectedValue.ToString());
                            Permission_Header.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                            Permission_Header.ExecuteNonQuery();
                            con.Close();
                        }

                        for (int j = 0; j < TreeView1.Nodes[i].ChildNodes.Count; j++)
                        {
                            sql = "Select Id From menu_permission where Login_Id='" + ddlUser.SelectedValue + "' and Menu_Id='" + TreeView1.Nodes[i].ChildNodes[j].Value + "'";
                            if (oo.ReturnTag(sql, "Id") != "")
                            {
                                SqlCommand Permission_Sub = new SqlCommand("menu_permission_proc", con);
                                Permission_Sub.CommandType = CommandType.StoredProcedure;
                                Permission_Sub.Parameters.AddWithValue("@Login_Id", ddlUser.SelectedValue);
                                Permission_Sub.Parameters.AddWithValue("@Menu_Id", TreeView1.Nodes[i].ChildNodes[j].Value);
                                if (TreeView1.Nodes[i].ChildNodes[j].Checked == true)
                                {
                                    Permission_Sub.Parameters.AddWithValue("@Permission_Value", "Yes");
                                }
                                else
                                {
                                    Permission_Sub.Parameters.AddWithValue("@Permission_Value", "No");
                                }
                                Permission_Sub.Parameters.AddWithValue("@task", "Update");
                                Permission_Sub.Parameters.AddWithValue("@Id", oo.ReturnTag(sql, "Id"));
                                Permission_Sub.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                                con.Open();
                                Permission_Sub.ExecuteNonQuery();
                                con.Close();
                            }
                            else
                            {
                                con.Open();
                                SqlCommand Permission_Sub = new SqlCommand("menu_permission_proc", con);
                                Permission_Sub.CommandType = CommandType.StoredProcedure;
                                Permission_Sub.Parameters.AddWithValue("@Login_Id", ddlUser.SelectedValue);
                                Permission_Sub.Parameters.AddWithValue("@Menu_Id", TreeView1.Nodes[i].ChildNodes[j].Value);
                                if (TreeView1.Nodes[i].ChildNodes[j].Checked == true)
                                {
                                    Permission_Sub.Parameters.AddWithValue("@Permission_Value", "Yes");
                                }
                                else
                                {
                                    Permission_Sub.Parameters.AddWithValue("@Permission_Value", "No");
                                }
                                Permission_Sub.Parameters.AddWithValue("@task", "Insert");
                                Permission_Sub.Parameters.AddWithValue("@Id", "0");
                                Permission_Sub.Parameters.AddWithValue("@LoginTypeId", ddltype.SelectedValue.ToString());
                                Permission_Sub.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                                Permission_Sub.ExecuteNonQuery();
                                con.Close();
                            }
                            for (int k = 0; k < TreeView1.Nodes[i].ChildNodes[j].ChildNodes.Count; k++)
                            {
                                sql = "Select Id From menu_permission where Login_Id='" + ddlUser.SelectedValue + "' and Menu_Id='" + TreeView1.Nodes[i].ChildNodes[j].ChildNodes[k].Value + "'";
                                if (oo.ReturnTag(sql, "Id") != "")
                                {
                                    SqlCommand Permission_Store = new SqlCommand("menu_permission_proc", con);
                                    Permission_Store.CommandType = CommandType.StoredProcedure;
                                    Permission_Store.Parameters.AddWithValue("@Login_Id", ddlUser.SelectedValue);
                                    Permission_Store.Parameters.AddWithValue("@Menu_Id", TreeView1.Nodes[i].ChildNodes[j].ChildNodes[k].Value);
                                    if (TreeView1.Nodes[i].ChildNodes[j].ChildNodes[k].Checked == true)
                                    {
                                        Permission_Store.Parameters.AddWithValue("@Permission_Value", "Yes");
                                    }
                                    else
                                    {
                                        Permission_Store.Parameters.AddWithValue("@Permission_Value", "No");
                                    }
                                    Permission_Store.Parameters.AddWithValue("@task", "Update");
                                    Permission_Store.Parameters.AddWithValue("@Id", oo.ReturnTag(sql, "Id"));
                                    Permission_Store.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                                    con.Open();
                                    Permission_Store.ExecuteNonQuery();
                                    con.Close();
                                }
                                else
                                {
                                    con.Open();
                                    SqlCommand Permission_Store = new SqlCommand("menu_permission_proc", con);
                                    Permission_Store.CommandType = CommandType.StoredProcedure;
                                    Permission_Store.Parameters.AddWithValue("@Login_Id", ddlUser.SelectedValue);
                                    Permission_Store.Parameters.AddWithValue("@Menu_Id", TreeView1.Nodes[i].ChildNodes[j].ChildNodes[k].Value);
                                    if (TreeView1.Nodes[i].ChildNodes[j].ChildNodes[k].Checked == true)
                                    {
                                        Permission_Store.Parameters.AddWithValue("@Permission_Value", "Yes");
                                    }
                                    else
                                    {
                                        Permission_Store.Parameters.AddWithValue("@Permission_Value", "No");
                                    }
                                    Permission_Store.Parameters.AddWithValue("@task", "Insert");
                                    Permission_Store.Parameters.AddWithValue("@Id", "0");
                                    Permission_Store.Parameters.AddWithValue("@LoginTypeId", ddltype.SelectedValue.ToString());
                                    Permission_Store.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                                    Permission_Store.ExecuteNonQuery();
                                    con.Close();
                                }
                            }
                        }
                    }
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "S");
                }
            }
        }
        else
        {
            oo.MessageBox("Please Select  User Name", this.Page);
        }
    }
    protected void ddltype_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        sql = "select LoginName,LoginId from LoginTab where LoginTypeId='" + ddltype.SelectedValue.ToString() + "' and BranchId=" + Session["BranchCode"] + " and (IsActive is NULL or IsActive=1) ";
        oo.FillDropDown_withValue(sql, ddlUser, "LoginName", "LoginId");
        ddlUser.Items.Insert(0, "<--Select-->");
        TreeView1.Nodes.Clear();
        populateMenuItem();
        if (ddltype.SelectedIndex==0)
        {
            div2.Visible = false;
            div3.Visible = false;
            div4.Visible = false;
        }
    }
    protected void ddlUser_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        loadUserName();
        CheckBoxCheck_clear();
        CheckBoxCheck();
        if (ddlUser.SelectedIndex == 0)
        {
            div2.Visible = false;
            div3.Visible = false;
            div4.Visible = false;
        }
        else
        {
            div2.Visible = true;
            div3.Visible = true;
            div4.Visible = true;
        }
    }


    private void loadUserName()
    {
        string username = "";
        sql = "Select (EFirstName+case when EMiddleName=' ' then ' ' else ' '+EMiddleName+' ' End+ELastName) EmpName  from EmpGeneralDetail Where Ecode = '" + ddlUser.SelectedItem.Text.ToString() + "' and BranchCode=1 Order by SrNo desc";
        username = oo.ReturnTag(sql, "EmpName").Trim();
    }
}