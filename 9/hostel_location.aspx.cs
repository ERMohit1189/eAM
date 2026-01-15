using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class admin_hostel_location : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    string sql = "";
    Campus oo = new Campus();
    protected void Page_Load(object sender, EventArgs e)
    {
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);

        //con.ConnectionString = ConfigurationManager.ConnectionStrings["Menu"].ConnectionString;
        if (!IsPostBack)
        {
            Display();

        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {

        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "HostelLocationMasterProc";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        cmd.Parameters.AddWithValue("@BuildingLocation", txtBuilding.Text.Trim().ToString());
        cmd.Parameters.AddWithValue("@Warden", txtWarden.Text.Trim().ToString());
        cmd.Parameters.AddWithValue("@Remark", txtRemark.Text.Trim().ToString());
        cmd.Parameters.AddWithValue("@ContactNo", txtWardenContact.Text.Trim().ToString());
        cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
        cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
        cmd.Parameters.AddWithValue("@action", "Insert");

        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            //oo.MessageBox("Submitted successfully.", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");

            oo.ClearControls(this.Page);
            Display();
        }
        catch (Exception ex) { }
    }



    public void Display()
    {
        sql = "Select Row_Number() over (order by Id Asc) as SrNo,Id,BuildingLocation,Warden, ContactNo,Remark from HostelLocationMaster  where BranchCode=" + Session["BranchCode"].ToString() + "";
        GridView1.DataSource = oo.GridFill(sql);
        GridView1.DataBind();
    }

    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label36");
        string ss = lblId.Text;
        lblID.Text = ss;

        // sql = "Select  distinct ROW_NUMBER() OVER (ORDER BY p.ProductId ASC) AS  [ProductId] ,po.id as ID, pc.ProductCategoryName as[ProductCategoryName], p.ProductName as [ProductName], Pm.ProductTypeName as [ProductTypeName],PO.ProductModelName as [ProductModelName] from Productcategorymaster pc left join ProductName p on p.ProductId=pc.ProductId  left join ProductTypeMaster PM on p.ProductId=PM.ProductId left join ProductModelMaster PO on p.ProductId=PO.ProductId ";        
        sql = "Select Row_Number() over (order by Id Asc) as SrNo,BuildingLocation,Warden, ContactNo,Remark from HostelLocationMaster";
        sql = sql + "  where Id=" + ss;
        sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

        txtBuilding0.Text = oo.ReturnTag(sql, "BuildingLocation");
        txtWarden0.Text = oo.ReturnTag(sql, "Warden");
        txtWardenContact0.Text = oo.ReturnTag(sql, "ContactNo");
        txtRemark0.Text = oo.ReturnTag(sql, "Remark");
      

      Panel1_ModalPopupExtender.Show();
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label37");
        string ss = lblId.Text;
        lblvalue.Text = ss.ToString();
        Panel2_ModalPopupExtender.Show();
        Button8.Focus();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        sql = "Delete from HostelLocationMaster where Id=" + lblvalue.Text.Trim();
        sql = sql + " and  SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

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

            sql = "Select Row_Number() over (order by Id Asc) as SrNo,Id,BuildingLocation,Warden, ContactNo,Remark from HostelLocationMaster where BranchCode=" + Session["BranchCode"].ToString() + "";
            GridView1.DataSource = oo.GridFill(sql);
            GridView1.DataBind();
        }
        catch (SqlException) { }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
         SqlCommand cmd = new SqlCommand();
         cmd.CommandText = "HostelLocationMasterProc";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
         cmd.Parameters.AddWithValue("@Id", lblID.Text);
        cmd.Parameters.AddWithValue("@BuildingLocation", txtBuilding0.Text.Trim().ToString());
        cmd.Parameters.AddWithValue("@Warden", txtWarden0.Text.Trim().ToString());
        cmd.Parameters.AddWithValue("@Remark", txtRemark0.Text.Trim().ToString());
        cmd.Parameters.AddWithValue("@ContactNo", txtWardenContact0.Text.Trim().ToString());
        cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());//Session["Login"].ToString());
        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
        cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
        cmd.Parameters.AddWithValue("@action", "Update");



        try
        {

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                //oo.MessageBox("Updated successfully.", this.Page);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "S");       

                Display();
            }
            catch (Exception) { }
        }
    protected void Button8_Click(object sender, EventArgs e)
    {

    }
    protected void Button4_Click(object sender, EventArgs e)
    {

    }
}
