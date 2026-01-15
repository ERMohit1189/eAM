using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;

public partial class SuperAdmin_EmailIdPanelSetting : Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = string.Empty;

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

            try
            {
                CheckValueADDDeleteUpdate();
            }
            catch (Exception) { }


            sql = "Select Id,Email,Password,SessionName,BranchCode,LoginName,RecordDate from EmailPanelSetting where Id=1";
            txtEmail.Text = oo.ReturnTag(sql, "Email");
            txtPassword.Text = oo.ReturnTag(sql, "Password");


        }

    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        //(@Email nvarchar(50),@Password nvarchar(502),@SessionName nvarchar(50),@BranchCode int,@LoginName nvarchar(50))

        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "EmailPanelSettingProc";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;

        cmd.Parameters.AddWithValue("@Email ", txtEmail.Text.ToString());

        cmd.Parameters.AddWithValue("@Password ", txtPassword.Text.ToString());

        cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());//Session["Login"].ToString());
        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
        try
        {

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            //oo.MessageBox("Updated successfully.", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "A");       



            sql = "Select Id,Email,Password,SessionName,BranchCode,LoginName,RecordDate from EmailPanelSetting where Id=1";
            txtEmail.Text = oo.ReturnTag(sql, "Email");
            txtPassword.Text = oo.ReturnTag(sql, "Password");


        }
        catch (Exception) { }

    }



    public void PermissionGrant(int add1, LinkButton Ladd)
    {


        if (add1 == 1)
        {
            Ladd.Enabled = true;
        }
        else
        {
            Ladd.Enabled = false;
        }



    }
    public void CheckValueADDDeleteUpdate()
    {
        sql = " select LoginId,LoginName,Pass,SessionId,BranchId,LT.LoginTypeName,ltb.add1 as add1,ltb.delete1 as delete1,ltb.update1 as update1 from LoginTab LTb";
        sql = sql + " left join LoginType Lt on LTb.LoginTypeId=Lt.LoginTypeId where LT.LoginTypeName='" + Session["Logintype"] + "' and LTb.LoginName='" + Session["LoginName"] + "'";
        int a;
        a = Convert.ToInt32(oo.ReturnTag(sql, "add1"));


        PermissionGrant(a, (LinkButton)LinkButton1);
    }
}