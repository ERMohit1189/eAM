using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class admin_VehicleRouteMaster : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    SqlCommand cmd ;
    Campus oo = new Campus();
    string sql = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

        if (!IsPostBack)
        {
            loadGrid();
        }
    }
    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        if (txtRoute.Text != String.Empty)
        {
            sql = "Select *from VehicleRouteMaster where RouteName='" + txtRoute.Text.Trim() + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            if (oo.Duplicate(sql) == false)
            {
                cmd = new SqlCommand();
                cmd.CommandText = "VehicleRouteMasterProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Type", "Insert");
                cmd.Parameters.AddWithValue("@Id", "");
                cmd.Parameters.AddWithValue("@Route", txtRoute.Text.Trim());
                cmd.Parameters.AddWithValue("@Remark", txtRemark.Text.Trim());
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                loadGrid();
                oo.ClearControls(table1);
                //oo.MessageBoxforUpdatePanel("Submitted successfully.", lnkSubmit);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");       

            }
            else
            {
                //oo.MessageBoxforUpdatePanel("Duplicate Entry!", lnkSubmit);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Duplicate Entry!", "A");       

            }
        }
        else
        {
            //oo.MessageBoxforUpdatePanel("Plaese enter Route Name!", lnkSubmit);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Plaese enter Route Name!", "A");       

        }
    }
    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblID1 = (Label)chk.NamingContainer.FindControl("Label36");
        lblID.Text = lblID1.Text;

        sql = "Select RouteName,Remark from VehicleRouteMaster where Id='" + lblID.Text.Trim() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        txtroute0.Text = oo.ReturnTag(sql, "RouteName");
        txtremark0.Text = oo.ReturnTag(sql, "Remark");

        Panel1_ModalPopupExtender.Show();

    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        

        if (txtroute0.Text != String.Empty)
        {
          
            sql = "Select RouteName from VehicleRouteMaster where RouteName='" + txtroute0.Text.Trim() + "' and Id<>'" + lblID.Text.Trim() + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            if (oo.Duplicate(sql) == false)
            {
                cmd = new SqlCommand();
                cmd.CommandText = "VehicleRouteMasterProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Type", "Update");
                cmd.Parameters.AddWithValue("@Id", lblID.Text.Trim());
                cmd.Parameters.AddWithValue("@Route", txtroute0.Text.Trim());   
                cmd.Parameters.AddWithValue("@Remark", txtremark0.Text.Trim());
                cmd.Parameters.AddWithValue("@SessionName", "");
                cmd.Parameters.AddWithValue("@LoginName", "");
                cmd.Parameters.AddWithValue("@BranchCode", "");
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                loadGrid();
                //oo.MessageBoxforUpdatePanel("Updated successfully.", LinkButton4);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "S");       

            }
            else
            {
                //oo.MessageBoxforUpdatePanel("Duplicate Entry!", lnkSubmit);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Duplicate Entry!", "A");       

            }
        }
        else
        {
            //oo.MessageBoxforUpdatePanel("Plaese enter Route Name!", lnkSubmit);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Plaese enter Route Name!", "A");       

        }
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        Button8.Focus();
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label37");
        string ss = lblId.Text;
        lblvalue.Text = lblId.Text;
        Panel2_ModalPopupExtender.Show();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "VehicleRouteMasterProc";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        cmd.Parameters.AddWithValue("@Type", "Delete");
        cmd.Parameters.AddWithValue("@Id", lblvalue.Text.Trim());
        cmd.Parameters.AddWithValue("@Route", "");
        cmd.Parameters.AddWithValue("@Remark", "");
        cmd.Parameters.AddWithValue("@SessionName", "");
        cmd.Parameters.AddWithValue("@LoginName", "");
        cmd.Parameters.AddWithValue("@BranchCode", "");
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        loadGrid();
        //oo.MessageBoxforUpdatePanel("Deleted successfully.", btnDelete);
        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Deleted successfully.", "S");       

    }
    private void loadGrid()
    {
        sql = "Select Id,RouteName as Route,Remark from VehicleRouteMaster where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        GridView1.DataSource = oo.GridFill(sql);
        GridView1.DataBind();
        
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            Label Label36 = (Label)GridView1.Rows[i].FindControl("Label36");
            sql = "Select  VehicleNoid from VehiclePickupLocationMaster where RouteId='" + Label36.Text.Trim() + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "'";
            if (oo.Duplicate(sql))
            {
                LinkButton LinkButton2 = (LinkButton)GridView1.Rows[i].FindControl("lnkEdit");
                LinkButton LinkButton3 = (LinkButton)GridView1.Rows[i].FindControl("lnkDelete");
                LinkButton2.Text = "<i class='fa fa-lock'></i>";
                LinkButton3.Text = "<i class='fa fa-lock'></i>";
                LinkButton2.Enabled = false;
                LinkButton3.Enabled = false;
            }
            sql = "Select  VehicleNoid from VehicleDropLocationMaster where RouteId='" + Label36.Text.Trim() + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "'";
            if (oo.Duplicate(sql))
            {
                LinkButton LinkButton2 = (LinkButton)GridView1.Rows[i].FindControl("lnkEdit");
                LinkButton LinkButton3 = (LinkButton)GridView1.Rows[i].FindControl("lnkDelete");
                LinkButton2.Text = "<i class='fa fa-lock'></i>";
                LinkButton3.Text = "<i class='fa fa-lock'></i>";
                LinkButton2.Enabled = false;
                LinkButton3.Enabled = false;
            }
        }
    }
}