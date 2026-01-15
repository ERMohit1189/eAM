using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;

public partial class admin_Hostel_category : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader); 

        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }

        //con.ConnectionString = ConfigurationManager.ConnectionStrings["Menu"].ConnectionString;

        if (!IsPostBack)
        {

            try
            {
               // CheckValueADDDeleteUpdate();
            }
            catch (Exception) { }

            sql = "Select  ROW_NUMBER() OVER (ORDER BY id ASC) AS SrNo,id, CategoryName,Remark from HostelCategoryMaster ";
            sql = sql + " where BranchCode=" + Session["BranchCode"].ToString() + "";
            GridView1.DataSource = oo.GridFill(sql);
            GridView1.DataBind();

        }

    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        sql = "select CategoryName from HostelCategoryMaster where CategoryName='" + TextBox1.Text.Trim() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
       // @CategoryCode nvarchar(50),@CategoryName nvarchar(50),@Remark nvarchar(200),@SessionName nvarchar(50),@BranchCode int,@LoginName nvarchar(50))

        if (oo.Duplicate(sql))
        {
            //oo.MessageBox("Duplicate Entry!", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, Duplicate Entry!", "A");       

        }
        else
        {

            //Pubcat2
            string PubCat = "";
            string ii = "";
            try
            {
                sql = "select max(Id)+1 as ID from HostelCategoryMaster";
                ii = oo.ReturnTag(sql, "Id");
            }
            catch (Exception) { ii = "1"; }
            PubCat = "Pubcat"+ii;


            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "HostelCategoryMasterProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
          
            cmd.Parameters.AddWithValue("@CategoryName", TextBox1.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@Remark", TextBox2.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());//Session["Login"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
            try
            {

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                //oo.MessageBox("Submited successfully", this.Page);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully", "S");       

                oo.ClearControls(this.Page);
                sql = "Select  ROW_NUMBER() OVER (ORDER BY id ASC) AS SrNo,id, CategoryName,Remark from HostelCategoryMaster ";
               sql = sql + " where BranchCode=" + Session["BranchCode"].ToString() + "";
                GridView1.DataSource = oo.GridFill(sql);
                GridView1.DataBind();

            }
            catch (Exception) { }
        }
    }

    protected void  LinkButton4_Click(object sender, EventArgs e)
{
         SqlCommand cmd = new SqlCommand();
         cmd.CommandText = "HostelCategoryMasterUpdateProc";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@id", lblID.Text);
    
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

            sql = "Select  ROW_NUMBER() OVER (ORDER BY id ASC) AS SrNo,id, CategoryName,Remark from HostelCategoryMaster ";
              sql = sql + " where BranchCode=" + Session["BranchCode"].ToString() + "";
                GridView1.DataSource = oo.GridFill(sql);
                GridView1.DataBind();
        }
        catch (SqlException) { }
    }
protected void  btnDelete_Click(object sender, EventArgs e)
{
    sql = "Delete from HostelCategoryMaster where id=" + lblvalue.Text;

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

            sql = "Select  ROW_NUMBER() OVER (ORDER BY id ASC) AS SrNo,id,CategoryName,Remark from HostelCategoryMaster ";
            sql = sql + " where BranchCode=" + Session["BranchCode"].ToString() + "";
            GridView1.DataSource = oo.GridFill(sql);
            GridView1.DataBind();
        }
        catch (SqlException) { }
}
protected void LinkButton2_Click(object sender, EventArgs e)
{
    LinkButton chk = (LinkButton)sender;
    Label lblId = (Label)chk.NamingContainer.FindControl("Label36");
    string ss = lblId.Text;
    lblID.Text = ss;
    sql = "Select  ROW_NUMBER() OVER (ORDER BY id ASC) AS SrNo,id, CategoryName,Remark from HostelCategoryMaster ";
    sql = sql + " where Id=" + ss  + " and BranchCode=" + Session["BranchCode"].ToString() + "";
    TextBox3.Text = oo.ReturnTag(sql, "CategoryName");
    TextBox4.Text = oo.ReturnTag(sql, "Remark");
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
}
