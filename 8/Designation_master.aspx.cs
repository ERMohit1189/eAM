using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;

public partial class Designation_master : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        Campus camp = new Campus(); camp.LoadLoader(loader); 

        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        con = oo.dbGet_connection();
        if (!IsPostBack)
        {
            try
            {
                CheckValueADDDeleteUpdate();
            }
            catch (Exception) { }
            loaddate();

        }
    }
    protected void loaddate()
    {
        sql = "Select  ROW_NUMBER() OVER (ORDER BY DesId ASC) AS SrNo,DesId, DesName from DesMaster";
        sql = sql + " where BranchCode=" + Session["BranchCode"].ToString() + "";
        Grd.DataSource = oo.GridFill(sql);
        Grd.DataBind();
        for (int i = 0; i < Grd.Rows.Count; i++)
        {
            Label EmpDesName = (Label)Grd.Rows[i].FindControl("Label2");
            Label EmpDesId = (Label)Grd.Rows[i].FindControl("Label36");
            LinkButton lnkEdit = (LinkButton)Grd.Rows[i].FindControl("LinkButton2");
            LinkButton lnkDelete = (LinkButton)Grd.Rows[i].FindControl("LinkButton3");
            sql = "select Designation from EmpployeeOfficialDetails where isnull(DesNameNew, '')='" + EmpDesName.Text.Trim() + "' and BranchCode=" + Session["BranchCode"] + "";
            if (oo.Duplicate(sql))
            {
                lnkEdit.Text = "<i class='fa fa-lock'></i>";
                lnkDelete.Text = "<i class='fa fa-lock'></i>";
                lnkEdit.Enabled = false;
                lnkDelete.Enabled = false;
            }
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        sql = "select DesName from DesMaster where DesName='" + txtdesi.Text + "'";
        sql = sql + " and BranchCode=" + Session["BranchCode"].ToString() + "";

        if (oo.Duplicate(sql))
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, Duplicate Entry!", "A");       

        }
        else
        {

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "DesMasterProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@DesName", txtdesi.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
            cmd.Parameters.AddWithValue("@Action", "insert");

            try
            {

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");       
                oo.ClearControls(this.Page);
                loaddate();

            }
            catch (Exception ex) { }
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        sql = "Delete from DesMaster where DesId=" + lblvalue.Text.Trim() + "";
        sql = sql + " and BranchCode=" + Session["BranchCode"].ToString() + "";
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = sql;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            loaddate();
        }
        catch (SqlException) { }
    }
    protected void Button8_Click(object sender, EventArgs e)
    {

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        sql = "select DesName from DesMaster where DesName='" + txtDepartmentNamePanel.Text.Trim() + "'";
        sql = sql + " and BranchCode=" + Session["BranchCode"].ToString() + " and DesId<>"+ lblID.Text.Trim() + "";

        if (oo.Duplicate(sql))
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, Duplicate Entry!", "A");

        }
        else
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "DesMasterProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DesId", lblID.Text.Trim());
            cmd.Parameters.AddWithValue("@DesName", txtDepartmentNamePanel.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@Action", "update");

            cmd.Connection = con;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "S");
                loaddate();
            }
            catch (SqlException) { }
        }

    }
    protected void Button4_Click(object sender, EventArgs e)
    {

    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {

        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label36");
        string ss = lblId.Text;
        lblID.Text = ss;

        sql = "Select  ROW_NUMBER() OVER (ORDER BY DesId ASC) AS SrNo,DesId, DesName from DesMaster";
        sql = sql + " where DesId=" + ss + " and BranchCode=" + Session["BranchCode"].ToString() + "";

        txtDepartmentNamePanel.Text = oo.ReturnTag(sql, "DesName");

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


    public void PermissionGrant(int add1, int delete1, int update1, LinkButton Ladd, Button Ldelete, Button LUpdate)
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

        PermissionGrant(a, d, u, (LinkButton)LinkButton1, btnDelete, Button3);
    }
}