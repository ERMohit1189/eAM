using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;

public partial class admin_book_status_master : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
         if ( Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        //con.ConnectionString = ConfigurationManager.ConnectionStrings["Menu"].ConnectionString;
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader); 

        if (!IsPostBack)
        {

            try
            {
                CheckValueADDDeleteUpdate();
            }
            catch (Exception) { }
            
            
            
              sql = "Select  ROW_NUMBER() OVER (ORDER BY id ASC) AS SrNo,id, BookStatus,RenewalCost,Remark from BookStatusMaster ";
                // sql = sql + " where  SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
                GridView1.DataSource = oo.GridFill(sql);
                GridView1.DataBind();
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        //(@BookStatus nvarchar(50) ,@RenewalCost nvarchar(50),@Remark nvarchar(200),@SessionName nvarchar(50),
        //@BranchCode int,@LoginName nvarchar(50))
        sql = "select BookStatus from BookStatusMaster where BookStatus='" + txtBookStatus.Text.Trim().ToString() + "'";
        // @CategoryCode nvarchar(50),@CategoryName nvarchar(50),@Remark nvarchar(200),@SessionName nvarchar(50),@BranchCode int,@LoginName nvarchar(50))

        if (oo.Duplicate(sql))
        {
            //oo.MessageBox("Duplicate Entry!", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, Duplicate Entry!", "A");       

        }
        else
        {

            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "BookStatusMasterPROC";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@BookStatus", txtBookStatus.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@RenewalCost", txtRenewalCost.Text.Trim().ToString());

            cmd.Parameters.AddWithValue("@Remark", txtRemark.Text.Trim().ToString());

            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());//Session["Login"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
            try
            {

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                //oo.MessageBox("Submitted successfully.", this.Page);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");       

                oo.ClearControls(this.Page);
                sql = "Select  ROW_NUMBER() OVER (ORDER BY id ASC) AS SrNo,id, BookStatus,RenewalCost,Remark from BookStatusMaster ";
                // sql = sql + " where  SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
                GridView1.DataSource = oo.GridFill(sql);
                GridView1.DataBind();

            }
            catch (Exception ex) { }
        }
    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        sql = "select BookStatus from BookStatusMaster where BookStatus='" + txtBookStatus0.Text.Trim().ToString() + "' and id<>'"+ lblID.Text.Trim() + "' ";
        if (oo.Duplicate(sql))
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, Duplicate Entry!", "A");
        }
        else
        {
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "BookStatusMasterUpdateProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@id", lblID.Text.Trim());
            cmd.Parameters.AddWithValue("@BookStatus", txtBookStatus0.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@RenewalCost", txtRenewalCost0.Text.Trim().ToString());

            cmd.Parameters.AddWithValue("@Remark", txtRemark0.Text.Trim().ToString());


            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());//Session["Login"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
            try
            {

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "S");

                oo.ClearControls(this.Page);
                sql = "Select  ROW_NUMBER() OVER (ORDER BY id ASC) AS SrNo,id, BookStatus,RenewalCost,Remark from BookStatusMaster ";
                GridView1.DataSource = oo.GridFill(sql);
                GridView1.DataBind();
            }
            catch (Exception) { }
        }
    }
    protected void  btnDelete_Click(object sender, EventArgs e)
{
         sql = "Delete from BookStatusMaster where id=" + lblvalue.Text.Trim();

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

            sql = "Select  ROW_NUMBER() OVER (ORDER BY id ASC) AS SrNo,id, BookStatus,RenewalCost,Remark from BookStatusMaster ";
                // sql = sql + " where  SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
                GridView1.DataSource = oo.GridFill(sql);
                GridView1.DataBind();

        }
        catch (SqlException) { }
}
protected void  LinkButton6_Click(object sender, EventArgs e)
{
    LinkButton chk = (LinkButton)sender;
    Label lblId = (Label)chk.NamingContainer.FindControl("Label36");
    string ss = lblId.Text;
        lblID.Text = ss;
       sql = "Select  ROW_NUMBER() OVER (ORDER BY id ASC) AS SrNo,id, BookStatus,RenewalCost,Remark from BookStatusMaster ";
        sql = sql + " where Id=" + ss;
        //  sql = sql + " and  SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        txtBookStatus0.Text = oo.ReturnTag(sql, "BookStatus");
        txtRenewalCost0.Text = oo.ReturnTag(sql, "RenewalCost");
        txtRemark0.Text = oo.ReturnTag(sql, "Remark");
        Panel1_ModalPopupExtender.Show();
}
protected void  LinkButton7_Click(object sender, EventArgs e)
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
    sql = sql + " left join LoginType Lt on LTb.LoginTypeId=Lt.LoginTypeId where LT.LoginTypeName='" + Session["Logintype"] + "' and LTb.LoginName='" + Session["LoginName"] + "'";
    int a, u, d;
    a = Convert.ToInt32(oo.ReturnTag(sql, "add1"));
    u = Convert.ToInt32(oo.ReturnTag(sql, "update1"));
    d = Convert.ToInt32(oo.ReturnTag(sql, "delete1"));

    PermissionGrant(a, d, u, (LinkButton)LinkButton1, btnDelete, LinkButton4);
}


}

