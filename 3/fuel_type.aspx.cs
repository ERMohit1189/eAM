using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;


public partial class admin_fuel_type : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
           if ((string)Session["LoginName"] == "" || (string)Session["BranchCode"] == "" || (string)Session["SessionName"] == "" || Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
           con = oo.dbGet_connection();
           Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

    
        if (!IsPostBack)
        {

            load();
        }
    }
    protected void load()
    {
        sql = "Select  ROW_NUMBER() OVER (ORDER BY id ASC) AS SrNo,Id,FuelType,Remark,RecordDate from FuelMaster";
        sql = sql + " where  BranchCode=" + Session["BranchCode"].ToString() + "";
        GridView1.DataSource = oo.GridFill(sql);
        GridView1.DataBind();
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            Label Label2 = (Label)GridView1.Rows[i].FindControl("Label2");
            sql = "Select  FuelType from VehicleDetails where FuelType='" + Label2.Text.Trim() + "' and BranchCode=" + Session["BranchCode"] + "";
            if (oo.Duplicate(sql))
            {
                LinkButton LinkButton2 = (LinkButton)GridView1.Rows[i].FindControl("LinkButton2");
                LinkButton LinkButton3 = (LinkButton)GridView1.Rows[i].FindControl("LinkButton3");
                LinkButton2.Text = "<i class='fa fa-lock'></i>";
                LinkButton3.Text = "<i class='fa fa-lock'></i>";
                LinkButton2.Enabled = false;
                LinkButton3.Enabled = false;
            }
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
         sql = "select FuelType from FuelMaster where FuelType='" + TextBox1.Text.Trim() + "'";
         sql = sql + " and BranchCode=" + Session["BranchCode"].ToString() + "";
         if (oo.Duplicate(sql))
         {
             //oo.MessageBox("Duplicate Entry!", this.Page);
             Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Duplicate Entry!", "A");       

         }
         else
         {

             SqlCommand cmd = new SqlCommand();
             cmd.CommandText = "[FuelMasterProce]";
             cmd.CommandType = CommandType.StoredProcedure;
             cmd.Connection = con;

             cmd.Parameters.AddWithValue("@FuelType", TextBox1.Text.Trim().ToString());
             cmd.Parameters.AddWithValue("@Remark", TextBox2.Text.Trim().ToString());
             cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
             cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
             cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());

             try
             {
                 con.Open();
                 cmd.ExecuteNonQuery();
                 con.Close();
                 //oo.MessageBox("Submitted successfully.", this.Page);
                 Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");
                load();
                 oo.ClearControls(this.Page);
                 

             }
             catch (Exception) { }
         }


    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {

        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "[FuelMasterUpdateProce]";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        cmd.Parameters.AddWithValue("@id", lblID.Text);
        cmd.Parameters.AddWithValue("@FuelType", TextBox3.Text.Trim().ToString());
        cmd.Parameters.AddWithValue("@Remark", TextBox4.Text.Trim().ToString());

        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            //oo.MessageBox("Updated successfully.", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "S");       

            oo.ClearControls(this.Page);
            load();

        }
        catch (Exception) { }



    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label36");
        string ss = lblId.Text;
        lblID.Text = ss;
        sql = "Select  ROW_NUMBER() OVER (ORDER BY id ASC) AS SrNo,id,FuelType,Remark,RecordDate from FuelMaster";
        sql = sql + " where Id=" + ss;
        sql = sql + " and BranchCode=" + Session["BranchCode"].ToString() + "";
        TextBox3.Text = oo.ReturnTag(sql, "FuelType");
        TextBox4.Text = oo.ReturnTag(sql, "Remark");
        Panel1_ModalPopupExtender.Show();


    }
    protected void  btnDelete_Click(object sender, EventArgs e)
{
    sql = "Delete from FuelMaster where id=" + lblvalue.Text;

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
        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Deleted successfully.", "S");

            load();
        }
    catch (SqlException) { }
}
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        Button8.Focus();
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label37");
        string ss = lblId.Text;
        lblvalue.Text = ss.ToString();
        Panel3_ModalPopupExtender.Show();
    }
    protected void Button8_Click(object sender, EventArgs e)
    {

    }
}
