using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class admin_PeriodGroupMaster : Page
{
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    Campus oo = new Campus();
    string sql = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }

        con = oo.dbGet_connection();
        if (!IsPostBack)
        {
            loadClass();
            display();
        }
    }
    protected void lnkShow_Click(object sender, EventArgs e)
    {
        save();
    }

    public void save()
    {
        foreach (ListItem li in chkClass.Items)
        {
            if (li.Selected)
            {
                cmd = new SqlCommand();
                cmd.CommandText = "ClassGroupMasterProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Type", "Insert");
                cmd.Parameters.AddWithValue("@Id", "");
                cmd.Parameters.AddWithValue("@GroupName", txtGrpName.Text.Trim());
                cmd.Parameters.AddWithValue("@ClassName", li.Text);
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                oo.MessageBoxforUpdatePanel("Submitted successfully!", lnkShow);
               
            }
        }
        display();
    }

    public void display()
    {
        sql = "Select Id,GroupName,ClassName From ClassGroupMaster";
        GridView1.DataSource = oo.GridFill(sql);
        GridView1.DataBind();
    }
    public void enabledisable()
    {
        if (chkClass.Items.Count > 0)
        {

        }
    }

    protected void lnkUpdate_Click(object sender, EventArgs e)
    {
        sql = "Select *From ClassGroupMaster where GroupName='" + txtGrpName1.Text.Trim() + "' and Classname='"+drpClass.SelectedItem.ToString()+"' and Id<>'" + Session["Id"].ToString() + "'";
        if (oo.Duplicate(sql))
        {
            oo.MessageBoxforUpdatePanel("Duplicate record!", lnkUpdate);
        }
        else
        {
            update();
        }
    }

    public void update()
    {
        cmd = new SqlCommand();
        cmd.CommandText = "ClassGroupMasterProc";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        cmd.Parameters.AddWithValue("@Type", "Update");
        cmd.Parameters.AddWithValue("@Id", Session["Id"].ToString());
        cmd.Parameters.AddWithValue("@GroupName", txtGrpName1.Text.Trim());
        cmd.Parameters.AddWithValue("@ClassName", drpClass.SelectedItem.ToString());
        cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
        cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        oo.MessageBoxforUpdatePanel("Updated successfully!", lnkShow);
        display();
    }

    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        Session["Id"] = lnk.Text;
        sql = "Select Id,GroupName,ClassName From ClassGroupMaster where Id='" + Session["Id"].ToString() + "'";
        txtGrpName1.Text = oo.ReturnTag(sql, "GroupName");
        drpClass.SelectedValue = oo.ReturnTag(sql, "ClassName");
        Panel1_ModalPopupExtender.Show();
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        Session["Id"] = lnk.Text;
        Panel2_ModalPopupExtender.Show();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        sql = "delete From ClassGroupMaster where Id='" + Session["Id"].ToString() + "'";
        cmd = new SqlCommand(sql, con);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        oo.MessageBoxforUpdatePanel("Deleted successfully", btnDelete);
        display();
    }

    public void loadClass()
    {
        sql = "Select ClassName from ClassMaster where SessionName='" + Session["SessionName"].ToString() + "'";
        oo.FillCheckBox(sql,chkClass,"ClassName");
        oo.FillDropDownWithOutSelect(sql, drpClass, "ClassName");
        if (chkClass.Items.Count > 0)
        {
            for (int i=0;i<chkClass.Items.Count;i++)
            {
                if (chkClass.Items[i].Text == "<--Select-->")
                {
                    chkClass.Items.RemoveAt(i);
                }
            }
        }
    }

}