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
        if (Session["LoginName"] == null)
        {
            Response.Redirect("default.aspx");
        }
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            string sql = "Select BranchId, BranchName from Branchtab";
            var dt = oo.Fetchdata(sql);
            ddlBranch.DataSource = dt;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "BranchId";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
            Grd_Fill();
            populateMenuItem();
            TreeView1.CollapseAll();
            TreeView1.Attributes.Add("onclick", "OnTreeClick(event)");
        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        Grd_Fill();
    }
    public void Grd_Fill()
    {
        sql = "select LoginName,LoginId from LoginTab where LoginTypeId='" + DropDownList2.SelectedValue.ToString() + "' and (IsActive is NULL or IsActive=1)";
        sql = sql + " and BranchId=" + ddlBranch.SelectedValue + " order by CASE WHEN ISNUMERIC(Right(LoginName,3))=1 THEN Right(LoginName,3) ELSE 0 END";
        oo.FillDropDown_withValue(sql, DropDownList1, "LoginName", "LoginId");
        DropDownList1.Items.Insert(0, "<--Select-->");
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

        sql = "SELECT MenuID,ParentID,text FROM MenueAM where isnull(permission, 0)=1 and " + DropDownList2.SelectedItem.Text.ToString() + "=1 and Status=1 order by MenuOrder asc";
       
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

        sql = "SELECT MenuID,ParentID,text FROM MenueAM where " + DropDownList2.SelectedItem.Text.ToString() + "=1 and ParentID="+id+ " and Status=1 order by MenuOrder asc";

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
            DataView views = new DataView(menuData2);
            views.RowFilter = "ParentID=" + parentMenuItem.Value;
            foreach (DataRowView row in views)
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
                    sql = "Select Count(*) as Counter_Value  From menu_permission where Login_Id='" + DropDownList1.SelectedItem.Value + "' and BranchCode=" + ddlBranch.SelectedValue + " and Permission_Value='Yes' and Menu_Id='" + TreeView1.Nodes[i].Value + "'";
                    if (oo.ReturnTag(sql, "Counter_Value") == "1")
                    {
                        TreeView1.Nodes[i].Checked = true;                        
                    }
                    for (int j = 0; j < TreeView1.Nodes[i].ChildNodes.Count; j++)
                    {
                        sql = "Select Count(*) as Counter_Value  From menu_permission where Login_Id='" + DropDownList1.SelectedItem.Value + "' and BranchCode=" + ddlBranch.SelectedValue + " and Permission_Value='Yes' and Menu_Id='" + TreeView1.Nodes[i].ChildNodes[j].Value + "'";
                        if (oo.ReturnTag(sql, "Counter_Value") == "1")
                        {
                            TreeView1.Nodes[i].ChildNodes[j].Checked = true;
                        }
                        for (int k = 0; k < TreeView1.Nodes[i].ChildNodes[j].ChildNodes.Count; k++)
                        {
                            sql = "Select Count(*) as Counter_Value  From menu_permission where Login_Id='" + DropDownList1.SelectedItem.Value + "' and BranchCode=" + ddlBranch.SelectedValue + " and Permission_Value='Yes' and Menu_Id='" + TreeView1.Nodes[i].ChildNodes[j].ChildNodes[k].Value + "'";
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
            if (DropDownList1.SelectedItem.Text != "<--Select-->")
            {
                if (TreeView1.Nodes.Count > 0)
                {
                    sql = "Select Count(*) as Counter From menu_permission Where Login_Id='" + DropDownList1.SelectedValue + "' and BranchCode=" + ddlBranch.SelectedValue + "";
                    if (oo.ReturnTag(sql, "Counter") == "0")
                    {
                        con.Open();
                        for (int i = 0; i < TreeView1.Nodes.Count; i++)
                        {
                            SqlCommand Permission_Header = new SqlCommand("menu_permission_proc", con);
                            Permission_Header.CommandType = CommandType.StoredProcedure;
                            Permission_Header.Parameters.AddWithValue("@Login_Id", DropDownList1.SelectedValue);
                            Permission_Header.Parameters.AddWithValue("@Menu_Id", TreeView1.Nodes[i].Value);
                            Permission_Header.Parameters.AddWithValue("@BranchCode", ddlBranch.SelectedValue.ToString());
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
                            Permission_Header.Parameters.AddWithValue("@LoginTypeId", DropDownList2.SelectedValue.ToString());

                            Permission_Header.ExecuteNonQuery();
                            for (int j = 0; j < TreeView1.Nodes[i].ChildNodes.Count; j++)
                            {
                                SqlCommand Permission_Sub = new SqlCommand("menu_permission_proc", con);
                                Permission_Sub.CommandType = CommandType.StoredProcedure;
                                Permission_Sub.Parameters.AddWithValue("@Login_Id", DropDownList1.SelectedValue);
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
                                Permission_Sub.Parameters.AddWithValue("@LoginTypeId", DropDownList2.SelectedValue.ToString());
                            Permission_Sub.Parameters.AddWithValue("@BranchCode", ddlBranch.SelectedValue.ToString());
                            Permission_Sub.ExecuteNonQuery();
                                for (int k = 0; k < TreeView1.Nodes[i].ChildNodes[j].ChildNodes.Count; k++)
                                {
                                    SqlCommand Permission_Store = new SqlCommand("menu_permission_proc", con);
                                    Permission_Store.CommandType = CommandType.StoredProcedure;
                                    Permission_Store.Parameters.AddWithValue("@Login_Id", DropDownList1.SelectedValue);
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
                                    Permission_Store.Parameters.AddWithValue("@LoginTypeId", DropDownList2.SelectedValue.ToString());
                                Permission_Store.Parameters.AddWithValue("@BranchCode", ddlBranch.SelectedValue.ToString());
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
                            sql = "Select Id From menu_permission where Login_Id='" + DropDownList1.SelectedValue + "' and Menu_Id='" + TreeView1.Nodes[i].Value + "'";
                            if (oo.ReturnTag(sql, "Id") != "")
                            {
                                SqlCommand Permission_Header = new SqlCommand("menu_permission_proc", con);
                                Permission_Header.CommandType = CommandType.StoredProcedure;
                                Permission_Header.Parameters.AddWithValue("@Login_Id", DropDownList1.SelectedValue);
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
                            Permission_Header.Parameters.AddWithValue("@BranchCode", ddlBranch.SelectedValue.ToString());
                            con.Open();
                                Permission_Header.ExecuteNonQuery();
                                con.Close();
                            }
                            else
                            {
                                con.Open();
                                SqlCommand Permission_Header = new SqlCommand("menu_permission_proc", con);
                                Permission_Header.CommandType = CommandType.StoredProcedure;
                                Permission_Header.Parameters.AddWithValue("@Login_Id", DropDownList1.SelectedValue);
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
                                Permission_Header.Parameters.AddWithValue("@LoginTypeId", DropDownList2.SelectedValue.ToString());
                            Permission_Header.Parameters.AddWithValue("@BranchCode", ddlBranch.SelectedValue.ToString());
                            Permission_Header.ExecuteNonQuery();
                                con.Close();
                            }

                            for (int j = 0; j < TreeView1.Nodes[i].ChildNodes.Count; j++)
                            {
                                sql = "Select Id From menu_permission where Login_Id='" + DropDownList1.SelectedValue + "' and Menu_Id='" + TreeView1.Nodes[i].ChildNodes[j].Value + "'";
                                if (oo.ReturnTag(sql, "Id") != "")
                                {
                                    SqlCommand Permission_Sub = new SqlCommand("menu_permission_proc", con);
                                    Permission_Sub.CommandType = CommandType.StoredProcedure;
                                    Permission_Sub.Parameters.AddWithValue("@Login_Id", DropDownList1.SelectedValue);
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
                                Permission_Sub.Parameters.AddWithValue("@BranchCode", ddlBranch.SelectedValue.ToString());
                                con.Open();
                                    Permission_Sub.ExecuteNonQuery();
                                    con.Close();
                                }
                                else
                                {
                                    con.Open();
                                    SqlCommand Permission_Sub = new SqlCommand("menu_permission_proc", con);
                                    Permission_Sub.CommandType = CommandType.StoredProcedure;
                                    Permission_Sub.Parameters.AddWithValue("@Login_Id", DropDownList1.SelectedValue);
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
                                    Permission_Sub.Parameters.AddWithValue("@LoginTypeId", DropDownList2.SelectedValue.ToString());
                                Permission_Sub.Parameters.AddWithValue("@BranchCode", ddlBranch.SelectedValue.ToString());
                                Permission_Sub.ExecuteNonQuery();
                                    con.Close();
                                }
                                for (int k = 0; k < TreeView1.Nodes[i].ChildNodes[j].ChildNodes.Count; k++)
                                {
                                    sql = "Select Id From menu_permission where Login_Id='" + DropDownList1.SelectedValue + "' and Menu_Id='" + TreeView1.Nodes[i].ChildNodes[j].ChildNodes[k].Value + "'";
                                    if (oo.ReturnTag(sql, "Id") != "")
                                    {
                                        SqlCommand Permission_Store = new SqlCommand("menu_permission_proc", con);
                                        Permission_Store.CommandType = CommandType.StoredProcedure;
                                        Permission_Store.Parameters.AddWithValue("@Login_Id", DropDownList1.SelectedValue);
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
                                    Permission_Store.Parameters.AddWithValue("@BranchCode", ddlBranch.SelectedValue.ToString());
                                    con.Open();
                                        Permission_Store.ExecuteNonQuery();
                                        con.Close();
                                    }
                                    else
                                    {
                                        con.Open();
                                        SqlCommand Permission_Store = new SqlCommand("menu_permission_proc", con);
                                        Permission_Store.CommandType = CommandType.StoredProcedure;
                                        Permission_Store.Parameters.AddWithValue("@Login_Id", DropDownList1.SelectedValue);
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
                                        Permission_Store.Parameters.AddWithValue("@LoginTypeId", DropDownList2.SelectedValue.ToString());
                                    Permission_Store.Parameters.AddWithValue("@BranchCode", ddlBranch.SelectedValue.ToString());
                                    Permission_Store.ExecuteNonQuery();
                                        con.Close();
                                    }
                                }
                            }
                        }
                        //oo.MessageBox("Menu Updated Successfully", this.Page);
                        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "S"); 
                    }
                }
            }
            else
            {
                oo.MessageBox("Please Select  User Name", this.Page);
            }
        }

    protected void DropDownList1_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        loadUserName();
        CheckBoxCheck_clear();
        CheckBoxCheck();
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        lblUser.Text = "";

        sql = "select LoginName,LoginId from LoginTab where LoginTypeId='" + DropDownList2.SelectedValue.ToString() + "' and BranchId=" + ddlBranch.SelectedValue + " and (IsActive is NULL or IsActive=1) ";
        oo.FillDropDown_withValue(sql, DropDownList1, "LoginName", "LoginId");
        DropDownList1.Items.Insert(0, "<--Select-->");
        TreeView1.Nodes.Clear();
        populateMenuItem();
    }

    private void loadUserName()
    {
        string username="";
        if (DropDownList2.SelectedValue.ToString() == "3")
        {
            sql = "Select (EG.EFirstName+case when EG.EMiddleName=' ' then ' ' else ' '+EG.EMiddleName+' ' End+EG.ELastName) EmpName from EmpployeeOfficialDetails EO";
            sql = sql + " Inner join EmpGeneralDetail EG on EG.EmpId=EO.EmpId";
            sql = sql + " Where EO.Ecode=(select LoginName from LoginTab where LoginId='" + DropDownList1.SelectedValue.ToString() + "') and Branchid=" + ddlBranch.SelectedValue + "";
            sql = sql + "  and EG.BranchCode=" + ddlBranch.SelectedValue + " and EO.BranchCode=" + ddlBranch.SelectedValue + " and EO.Password=(select Pass from LoginTab where LoginId='" + DropDownList1.SelectedValue.ToString() + "' and Branchid=" + ddlBranch.SelectedValue + ") Order by EO.SrNo";
            username = oo.ReturnTag(sql, "EmpName").Trim();           
        }
        else
        {
            sql = "Select Name from NewAdminInformation";
            sql = sql + " Where UserId=(select LoginName from LoginTab where LoginId='" + DropDownList1.SelectedValue.ToString() + "' and Branchid=" + ddlBranch.SelectedValue + ")";
            sql = sql + " and BranchCode=" + ddlBranch.SelectedValue + " and Password=(select Pass from LoginTab where LoginId='" + DropDownList1.SelectedValue.ToString() + "' and Branchid=" + ddlBranch.SelectedValue + ")";
            username = oo.ReturnTag(sql, "Name").Trim();
        }

        if (username == string.Empty)
        {
            lblUser.Text = DropDownList1.SelectedItem.Text;
        }
        else
        {
            lblUser.Text = username;
        }
    }


}