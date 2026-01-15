using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;

public partial class admin_Promotion_StatusMaster : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
         if ( Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        con = oo.dbGet_connection();
        if (!IsPostBack)
        {

            //try
            //{
            //    CheckValueADDDeleteUpdate();
            //}
            //catch (Exception) { }

            sql = "Select  ROW_NUMBER() OVER (ORDER BY id ASC) AS SrNo,id, Status from PromotionStatusMaster ";
            //sql = sql + " where  SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            GridView1.DataSource = oo.GridFill(sql);
            GridView1.DataBind();

        }

    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
         sql = "select Status from PromotionStatusMaster where Status='" + txtStatus.Text + "'";
       // sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        if (oo.Duplicate(sql))
        {
            oo.MessageBox("Duplicate Enntry", this.Page);
        }
        else
        {

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "PromotionStatusMasterProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@Status", txtStatus.Text.ToString());
            cmd.Parameters.AddWithValue("@Remark", txtRemark.Text.ToString());

            
            try
            {

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                oo.MessageBox("Submitted successfully.", this.Page);
                oo.ClearControls(this.Page);
                sql = "Select  ROW_NUMBER() OVER (ORDER BY id ASC) AS SrNo,id, Status from PromotionStatusMaster ";
               // sql = sql + " where  SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
                GridView1.DataSource = oo.GridFill(sql);
                GridView1.DataBind();

            }
            catch (Exception) { }     




        }
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        string ss = chk.Text;
        lblvalue.Text = ss.ToString();
        Panel2_ModalPopupExtender.Show();
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        string ss = chk.Text;
        lblID.Text = ss;
        sql = "Select  ROW_NUMBER() OVER (ORDER BY id ASC) AS SrNo,id, Status,Remark from PromotionStatusMaster ";
        sql = sql + " where Id=" + ss;
        //  sql = sql + " and  SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        txtStatusPanel.Text = oo.ReturnTag(sql, "Status");
        txtRemarkPanel.Text = oo.ReturnTag(sql, "Remark");
        Panel1_ModalPopupExtender.Show();
    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "PromotionStatusMasterUpdateProc";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@id", lblID.Text);
        cmd.Parameters.AddWithValue("@Status", txtStatusPanel.Text.ToString());
        cmd.Parameters.AddWithValue("@Remark", txtRemarkPanel.Text.ToString());
        cmd.Connection = con;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            oo.MessageBox("Updated successfully.", this.Page);
            sql = "Select  ROW_NUMBER() OVER (ORDER BY id ASC) AS SrNo,id, Status,Remark from PromotionStatusMaster ";
           // sql = sql + " where  SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            GridView1.DataSource = oo.GridFill(sql);
            GridView1.DataBind();
        }
        catch (SqlException) { }
    }

    protected void  btnDelete_Click(object sender, EventArgs e)
   {
      sql = "Delete from PromotionStatusMaster where id=" + lblvalue.Text;

        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = sql;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;

        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            oo.MessageBox("Deleted successfully.", this.Page);
            sql = "Select  ROW_NUMBER() OVER (ORDER BY id ASC) AS SrNo,id,Status,Remark from PromotionStatusMaster";
            GridView1.DataSource = oo.GridFill(sql);
            GridView1.DataBind();
        }
        catch (SqlException) { }
    }

    protected void LinkButton5_Click(object sender, EventArgs e)
    {

    }
    
    protected void Button8_Click(object sender, EventArgs e)
    {

    }
}
