using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;

public partial class publisher_category : System.Web.UI.Page
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

        //con.ConnectionString = ConfigurationManager.ConnectionStrings["Menu"].ConnectionString;
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
        sql = "Select  ROW_NUMBER() OVER (ORDER BY id ASC) AS SrNo,id,CategoryCode, CategoryName,Remark from PublisherCategoryMaster where  BranchCode=" + Session["BranchCode"] + " ";
        GridView1.DataSource = oo.GridFill(sql);
        GridView1.DataBind();
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            Label PublisherCategory = (Label)GridView1.Rows[i].FindControl("Label2");
            LinkButton LinkButton3 = (LinkButton)GridView1.Rows[i].FindControl("LinkButton3");
            sql = "SELECT PublisherCategory FROM PublisherInfoEntry where PublisherCategory='" + PublisherCategory.Text.Trim() + "' AND BranchCode=" + Session["BranchCode"] + "";
            if (oo.Duplicate(sql))
            {
                LinkButton3.Text = "<i class='fa fa-lock'></i>";
                LinkButton3.Enabled = false;
            }
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
           sql = "select CategoryName from PublisherCategoryMaster where CategoryName='" + TextBox1.Text.Trim() + "' and  BranchCode=" + Session["BranchCode"] + "";

        if (oo.Duplicate(sql))
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, Duplicate Entry!", "A");       
        }
        else
        {
            string PubCat = "";
            string ii = "";
            try
            {
                sql = "select max(Id)+1 as ID from PublisherCategoryMaster where  BranchCode=" + Session["BranchCode"] + "";
                ii = oo.ReturnTag(sql, "Id");
            }
            catch (Exception) { ii = "1"; }
            PubCat = "Pubcat"+ii;


            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "PublisherCategoryMasterProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@CategoryCode", PubCat);
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
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully", "S");       

                oo.ClearControls(this.Page);
                loadData();

            }
            catch (Exception ex) { }
        }
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label36");
        string ss = lblId.Text;
        lblID.Text = ss;
        sql = "Select  ROW_NUMBER() OVER (ORDER BY id ASC) AS SrNo,id,CategoryCode, CategoryName,Remark from PublisherCategoryMaster where  BranchCode=" + Session["BranchCode"] + " ";
        sql = sql + " and Id=" + ss;
        //  sql = sql + " and  SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        TextBox3.Text = oo.ReturnTag(sql, "CategoryName");
        TextBox4.Text = oo.ReturnTag(sql, "Remark");
        Panel1_ModalPopupExtender.Show();
    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        sql = "select CategoryName from PublisherCategoryMaster where CategoryName='" + TextBox3.Text.Trim() + "' AND ID<>"+ lblID.Text.Trim() + " and  BranchCode=" + Session["BranchCode"] + "";
        if (oo.Duplicate(sql))
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, Duplicate Entry!", "A");
            return;
        }
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "PublisherCategoryMasterUpdateProc";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@id", lblID.Text.Trim());
        cmd.Parameters.AddWithValue("@CategoryName", TextBox3.Text.Trim().ToString());
        cmd.Parameters.AddWithValue("@Remark", TextBox4.Text.Trim().ToString());
        cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());//Session["Login"].ToString());
        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
        cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
        cmd.Connection = con;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            //oo.MessageBox("Updated successfully.", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully", "S");
            loadData();
        }
        catch (SqlException) { }
    }
    protected void  btnDelete_Click(object sender, EventArgs e)
{
    sql = "Delete from PublisherCategoryMaster where id=" + lblvalue.Text.Trim() + " and  BranchCode=" + Session["BranchCode"] + "";

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
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Deleted successfully", "S");
            loadData();
        }
        catch (SqlException) { }
}
    protected void Button8_Click(object sender, EventArgs e)
    {

    }
    protected void LinkButton5_Click(object sender, EventArgs e)
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
    