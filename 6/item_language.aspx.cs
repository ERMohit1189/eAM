using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;

public partial class item_language : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            try
            {
                loadData();
                CheckValueADDDeleteUpdate();
            }
            catch (Exception) { }
        }
    }
    protected void loadData()
    {
        sql = "Select  ROW_NUMBER() OVER (ORDER BY id ASC) AS SrNo,id,CategoryCode, CategoryName,Remark from ItemLanguageMaster Where BranchCode = " + Session["BranchCode"] + " ";
        GridView1.DataSource = oo.GridFill(sql);
        GridView1.DataBind();
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            Label id = (Label)GridView1.Rows[i].FindControl("Label36");
            LinkButton LinkButton3 = (LinkButton)GridView1.Rows[i].FindControl("LinkButton3");
            sql = "SELECT Language FROM LibraryItemEntry WHERE Language='" + id.Text.Trim() + "' AND BranchCode=" + Session["BranchCode"] + "";
            if (oo.Duplicate(sql))
            {
                LinkButton3.Text = "<i class='fa fa-lock'></i>";
                LinkButton3.Enabled = false;
            }
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        sql = "select CategoryName from ItemLanguageMaster where CategoryName='" + TextBox1.Text.Trim() + "' and BranchCode = " + Session["BranchCode"] + "";
        if (oo.Duplicate(sql))
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, Duplicate Entry!", "A");
        }
        else
        {
            string Lang = "";
            string ii = "";
            try
            {
                sql = "select max(Id)+1 as ID from ItemLanguageMaster";
                ii = oo.ReturnTag(sql, "Id");
            }
            catch (Exception) { ii = "1"; }
            Lang = "Lang" + ii;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "ItemLanguageMasterProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@CategoryCode", Lang);
            cmd.Parameters.AddWithValue("@CategoryName", TextBox1.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@Remark", TextBox2.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");
                oo.ClearControls(this.Page);
                loadData();
            }
            catch (Exception) { }
        }

    }


    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        sql = "select CategoryName from ItemLanguageMaster where CategoryName='" + TextBox3.Text.Trim() + "' and id<>"+lblID.Text.Trim() + " and BranchCode = " + Session["BranchCode"] + "";
        if (oo.Duplicate(sql))
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, Duplicate Entry!", "A");
            return;
        }
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "ItemLanguageMasterUpdateProc";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@id", lblID.Text.Trim());
        cmd.Parameters.AddWithValue("@CategoryName", TextBox3.Text.Trim().ToString());
        cmd.Parameters.AddWithValue("@Remark", TextBox4.Text.Trim().ToString());
        cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
        cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
        cmd.Connection = con;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "S");
            loadData();
        }
        catch (SqlException) { }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        sql = "Delete from ItemLanguageMaster where id=" + lblvalue.Text.Trim() + " and BranchCode = " + Session["BranchCode"] + "";

        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = sql;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Deleted successfully.", "S");
            loadData();
        }
        catch (SqlException) { }
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label36");
        string ss = lblId.Text;
        lblID.Text = ss;
        Label CategoryName = (Label)chk.NamingContainer.FindControl("CategoryName");
        Label Remark = (Label)chk.NamingContainer.FindControl("Remark");

        TextBox3.Text = CategoryName.Text;
        TextBox4.Text = Remark.Text;
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
    protected void LinkButton5_Click(object sender, EventArgs e)
    {
    }
    protected void Button8_Click(object sender, EventArgs e)
    {
    }

    public void PermissionGrant(int add1, int delete1, int update1, LinkButton Ladd, Button Ldelete, LinkButton LUpdate)
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
        sql = sql + " left join LoginType Lt on LTb.LoginTypeId=Lt.LoginTypeId where LT.LoginTypeName='" + Session["Logintype"] + "' and LTb.LoginName='" + Session["LoginName"] + "' and LTb.BranchId = " + Session["BranchCode"] + "";
        int a, u, d;
        a = Convert.ToInt32(oo.ReturnTag(sql, "add1"));
        u = Convert.ToInt32(oo.ReturnTag(sql, "update1"));
        d = Convert.ToInt32(oo.ReturnTag(sql, "delete1"));
        PermissionGrant(a, d, u, (LinkButton)LinkButton1, btnDelete, LinkButton4);
    }
}
