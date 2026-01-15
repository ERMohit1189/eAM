using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;

public partial class admin_employee_category : System.Web.UI.Page
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

        if (!IsPostBack)
        {
            try
            {
                CheckValueADDDeleteUpdate();
            }
            catch (Exception) { }
                       
            
            
            
            sql = "Select  ROW_NUMBER() OVER (ORDER BY EmployeeCategoryId ASC) AS SrNo,EmployeeCategoryId, EmployeeCategoryName,EmployeeCategoryRemark from EmployeeCategoryMaster";
        sql = sql +  " where BranchCode=" + Session["BranchCode"].ToString() + "";
            Grd.DataSource = oo.GridFill(sql);
            Grd.DataBind();

        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {

        sql = "select * from EmployeeCategoryMaster where EmployeeCategoryName='" + txtCategoryName.Text.Trim() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        if (oo.Duplicate(sql))
        {
            //oo.MessageBox("Duplicate Entry!", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, Duplicate Entry!", "A");       

        }
        else
        {

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EmployeeCategoryMastProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@EmployeeCategoryName", txtCategoryName.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@EmployeeCategoryRemark", txtRemark.Text.Trim().ToString());

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
                sql = "Select  ROW_NUMBER() OVER (ORDER BY EmployeeCategoryId ASC) AS SrNo,EmployeeCategoryId, EmployeeCategoryName,EmployeeCategoryRemark from EmployeeCategoryMaster";
               sql = sql + " where BranchCode=" + Session["BranchCode"].ToString() + "";
                Grd.DataSource = oo.GridFill(sql);
                Grd.DataBind();

            }
            catch (Exception) { }
        }
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label36");
        string ss = lblId.Text;
        lblID.Text = ss;

        //sql = "Select  distinct ROW_NUMBER() OVER (ORDER BY p.ProductId ASC) AS  [ProductId] ,po.id as ID, pc.ProductCategoryName as[ProductCategoryName], p.ProductName as [ProductName], Pm.ProductTypeName as [ProductTypeName],PO.ProductModelName as [ProductModelName] from Productcategorymaster pc left join ProductName p on p.ProductId=pc.ProductId  left join ProductTypeMaster PM on p.ProductId=PM.ProductId left join ProductModelMaster PO on p.ProductId=PO.ProductId ";        
        sql = "Select  ROW_NUMBER() OVER (ORDER BY EmployeeCategoryId ASC) AS SrNo,EmployeeCategoryId, EmployeeCategoryName,EmployeeCategoryRemark from EmployeeCategoryMaster";
        sql = sql + " where EmployeeCategoryId=" + ss+ " and BranchCode=" + Session["BranchCode"].ToString() + "";

        txtCategoryNamePanel.Text = oo.ReturnTag(sql, "EmployeeCategoryName");    
        txtRemarkPanel.Text = oo.ReturnTag(sql, "EmployeeCategoryRemark");

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
    protected void Button3_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "EmployeeCategoryMasterProc";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@EmployeeCategoryId", lblID.Text.Trim());
        cmd.Parameters.AddWithValue("@EmployeeCategoryName", txtCategoryNamePanel.Text.Trim().ToString());
        cmd.Parameters.AddWithValue("@EmployeeCategoryRemark", txtRemarkPanel.Text.Trim().ToString());
       
        cmd.Connection = con;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            //oo.MessageBox("Updated successfully.", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "S");       

            sql = "Select  ROW_NUMBER() OVER (ORDER BY EmployeeCategoryId ASC) AS SrNo,EmployeeCategoryId, EmployeeCategoryName,EmployeeCategoryRemark from EmployeeCategoryMaster";
          sql = sql +"  where BranchCode=" + Session["BranchCode"].ToString() + "";
            Grd.DataSource = oo.GridFill(sql);
            Grd.DataBind();
        }
        catch (SqlException) { }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        sql = "Delete from EmployeeCategoryMaster where EmployeeCategoryId=" + lblvalue.Text.Trim() + "";
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
            //oo.MessageBox("Deleted successfully.", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Deleted successfully.", "S");       

            sql = "Select  ROW_NUMBER() OVER (ORDER BY EmployeeCategoryId ASC) AS SrNo,EmployeeCategoryId, EmployeeCategoryName,EmployeeCategoryRemark from EmployeeCategoryMaster";
           sql = sql + "  where  BranchCode=" + Session["BranchCode"].ToString() + "";
            Grd.DataSource = oo.GridFill(sql);
            Grd.DataBind();
        }
        catch (SqlException) { }

    }
    protected void Button4_Click(object sender, EventArgs e)
    {

    }
    protected void Button8_Click(object sender, EventArgs e)
    {

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