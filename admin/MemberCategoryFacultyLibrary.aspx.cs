using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;

public partial class admin_MemberCategoryLibrary : Page
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
        if (!IsPostBack)
        {

            try
            {
                CheckValueADDDeleteUpdate();
            }
            catch (Exception) { }
            
            
            
            Display();

        }
    }

    public void Display()
    {

        sql = "Select FineForLibrary,MaximumItemsLibrary,DaysReturn,MembershipValidity,Remark from MemberCategoryFaculty where Id=1";

        txtFineLibraryPerDay.Text = oo.ReturnTag(sql, "FineForLibrary");
        txtMaximumitemLibrary.Text = oo.ReturnTag(sql, "MaximumItemsLibrary");
        txtdaysRetLibrary.Text = oo.ReturnTag(sql, "DaysReturn");
        txtMembershipValidityMonth.Text = oo.ReturnTag(sql, "MembershipValidity");
        txtRemark.Text = oo.ReturnTag(sql, "Remark");

    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        //        .[MemberCategoryFacultyProce](@FineForLibrary numeric(18,2),@MaximumItemsLibrary nvarchar(50),@DaysReturn nvarchar(50),
        //@MembershipValidity nvarchar(50),@Remark nvarchar(500),@SessionName nvarchar(50),@LoginName nvarchar(50),@BranchCode int)

        //as

        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "MemberCategoryFacultyProce";
        cmd.CommandType = CommandType.StoredProcedure;


        cmd.Parameters.AddWithValue("@FineForLibrary", txtFineLibraryPerDay.Text.ToString());
        cmd.Parameters.AddWithValue("@MaximumItemsLibrary", txtMaximumitemLibrary.Text.ToString());
        cmd.Parameters.AddWithValue("@DaysReturn", txtdaysRetLibrary.Text.ToString());
        cmd.Parameters.AddWithValue("@MembershipValidity", txtMembershipValidityMonth.Text.ToString());
        cmd.Parameters.AddWithValue("@Remark", txtRemark.Text.ToString());
        cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
        cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
        cmd.Connection = con;
        try
        {

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            oo.MessageBox("Submitted successfully.", this.Page);

            Display();




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
#pragma warning disable 168
        int a, u, d;
#pragma warning restore 168
        a = Convert.ToInt32(oo.ReturnTag(sql, "add1"));
       

        PermissionGrant(a, (LinkButton)LinkButton1);
    }
}


