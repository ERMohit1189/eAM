using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;

public partial class SuperAdmin_CollegeBranchMaster : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LoginName"] == null)
        {
            Response.Redirect("default.aspx");
        }
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

        if (!IsPostBack)
        {
            try
            {
                CheckValueADDDeleteUpdate();
            }
            catch (Exception) { }
            Load();
        }
    }
    protected void Load()
    {
        sql = "Select  ROW_NUMBER() OVER (ORDER BY BranchId ASC) AS SrNo,BranchId,BranchName, ShortCode,BranchRemark from BranchTab";
        Grd.DataSource = oo.GridFill(sql);
        Grd.DataBind();
        for (int i = 0; i < Grd.Rows.Count; i++)
        {
            LinkButton lnkDelete = (LinkButton)Grd.Rows[i].FindControl("LinkButton3");
            Label id = (Label)Grd.Rows[i].FindControl("Label37");
            string sql1 = "select BranchCode from SessionMaster where BranchCode=" + id.Text + "";
            if (oo.Duplicate(sql1))
            {
                lnkDelete.Text = "<i class='fa fa-lock'></i>";
                lnkDelete.Enabled = false;
            }
        }
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId2 = (Label)chk.NamingContainer.FindControl("Label36");
        string ss = lblId2.Text;
        lblID.Text = ss;
        sql = "Select  ROW_NUMBER() OVER (ORDER BY BranchId ASC) AS SrNo,BranchId,BranchName, ShortCode,BranchRemark from BranchTab";
        sql = sql + "  where BranchId=" + ss;
        txtPanelCategory.Text = oo.ReturnTag(sql, "BranchName");
        hdnPanelCategory.Value = oo.ReturnTag(sql, "BranchName");
        txtRemarkPanel.Text = oo.ReturnTag(sql, "BranchRemark");
        txtShortcodePanel.Text = oo.ReturnTag(sql, "ShortCode");
        hdnShortcodePanel.Value = oo.ReturnTag(sql, "ShortCode");
        Panel1_ModalPopupExtender.Show();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        sql = "Delete from BranchTab where BranchId=" + lblvalue.Text;

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
            Load();
        }
        catch (SqlException) { }
    }
    protected void Button8_Click(object sender, EventArgs e)
    {
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
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
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        if (oo.Duplicate("Select  BranchName, ShortCode,BranchRemark from BranchTab where BranchName='" + txtCaste.Text.Trim() + "'"))
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Duplicate Branch Name!", "A");
        }
        else if (oo.Duplicate("Select  BranchName, ShortCode,BranchRemark from BranchTab where ShortCode='" + txtShortcode.Text.Trim() + "'"))
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Duplicate Branch Code!", "A");
        }
        else if (txtCaste.Text == "")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please Enter Branch Name!", "A");
        }
        else if (txtShortcode.Text == "")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please Enter Branch Code!", "A");
        }
        else
        {
            cmd.CommandText = "BranchTabProce";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BrNa", txtCaste.Text.Trim());
            cmd.Parameters.AddWithValue("@BrRemark", txtRemark.Text.Trim());
            cmd.Parameters.AddWithValue("@Shortcode", txtShortcode.Text.Trim());
            cmd.Connection = con;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                txtCaste.Text = "";
                txtShortcode.Text = "";
                txtRemark.Text = "";
                con.Close();
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");
                Load();
            }
            catch (SqlException ee) { oo.MessageBox(ee.Message.ToString(), this.Page); }
        }
    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        if (oo.Duplicate("Select  BranchName, ShortCode,BranchRemark from BranchTab where BranchName='" + txtPanelCategory.Text.Trim() + "' and BranchId<>'" + lblID.Text.Trim() + "'"))
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Duplicate Branch Name!", "A");
            Panel1_ModalPopupExtender.Show();
        }
        if (oo.Duplicate("Select  BranchName, ShortCode,BranchRemark from BranchTab where  ShortCode='" + txtShortcodePanel.Text.Trim() + "' and BranchId<>'" + lblID.Text.Trim() + "'"))
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Duplicate Branch Code!", "A");
            Panel1_ModalPopupExtender.Show();
        }
        else if (txtPanelCategory.Text == "")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please Enter Branch Name!", "A");
            Panel1_ModalPopupExtender.Show();
        }
        else if (txtShortcodePanel.Text == "")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please Enter Branch Code!", "A");
            Panel1_ModalPopupExtender.Show();
        }
        else
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[BranchTabUpdateProce]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BrId", lblID.Text);
            cmd.Parameters.AddWithValue("@BrNa", txtPanelCategory.Text);
            cmd.Parameters.AddWithValue("@BrRemark", txtRemarkPanel.Text);
            cmd.Parameters.AddWithValue("@Shortcode", txtShortcodePanel.Text);
            cmd.Connection = con;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "S");
                Load();
            }
            catch (SqlException) { }
        }
    }
    protected void LinkButton5_Click(object sender, EventArgs e)
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
        sql = sql + " left join LoginType Lt on LTb.LoginTypeId=Lt.LoginTypeId where LT.LoginTypeName='" + Session["Logintype"] + "' and LTb.LoginName='" + Session["LoginName"] + "'";
        int a, u, d;
        a = Convert.ToInt32(oo.ReturnTag(sql, "add1"));
        u = Convert.ToInt32(oo.ReturnTag(sql, "update1"));
        d = Convert.ToInt32(oo.ReturnTag(sql, "delete1"));
        PermissionGrant(a, d, u, (LinkButton)LinkButton1, btnDelete, LinkButton4);
    }
}