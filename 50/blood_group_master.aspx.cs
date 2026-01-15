using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;

public partial class admin_blood_group_master : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {


            txtbloodGroup.Focus();

            //con.ConnectionString = ConfigurationManager.ConnectionStrings["Menu"].ConnectionString;
            con = oo.dbGet_connection();
            if (!IsPostBack)
            {

                //try
                //{
                //    CheckValueADDDeleteUpdate();
                //}
                //catch (Exception) { }



                load();

            }
        }
        catch
        { }
    

        
    }
    protected void load()
    {
        sql = "Select  ROW_NUMBER() OVER (ORDER BY BloodGroupId ASC) AS SrNo,BloodGroupId,BloodGroupName,BloodGroupRemark from BloodGroupMaster";
        Grd.DataSource = oo.GridFill(sql);
        Grd.DataBind();
        for (int i = 0; i < Grd.Rows.Count; i++)
        {
            Label BloodGroup = (Label)Grd.Rows[i].FindControl("Label2");
            sql = "select defaultvalue from DefaultSelectedValue where defaultvalueof='Blood Group' and defaultvalue='" + BloodGroup.Text.Trim() + "'";
            if (oo.Duplicate(sql))
            {
                LinkButton LinkButton2 = (LinkButton)Grd.Rows[i].FindControl("LinkButton2");
                LinkButton LinkButton3 = (LinkButton)Grd.Rows[i].FindControl("LinkButton3");
                LinkButton2.Text = "<i class='fa fa-lock'></i>";
                LinkButton3.Text = "<i class='fa fa-lock'></i>";
                LinkButton2.Enabled = false;
                LinkButton3.Enabled = false;
            }
            sql = "select BloodGroup from StudentGenaralDetail where BloodGroup='" + BloodGroup.Text.Trim() + "'";
            if (oo.Duplicate(sql))
            {
                LinkButton LinkButton2 = (LinkButton)Grd.Rows[i].FindControl("LinkButton2");
                LinkButton LinkButton3 = (LinkButton)Grd.Rows[i].FindControl("LinkButton3");
                LinkButton2.Text = "<i class='fa fa-lock'></i>";
                LinkButton3.Text = "<i class='fa fa-lock'></i>";
                LinkButton2.Enabled = false;
                LinkButton3.Enabled = false;
            }
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
       if (oo.Duplicate("select BloodGroupName from BloodGroupMaster where BloodGroupName='" + txtbloodGroup.Text + "'"))
        {

            //oo.MessageBox("Duplicate Blood Group!", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Duplicate Blood Group!", "A");
        }        
      
            
        else
       {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "BloodGroupMasterProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BloodGroupNa", txtbloodGroup.Text.Trim());
            cmd.Parameters.AddWithValue("@BloodGroupRema", txtRemark.Text.Trim());
            cmd.Connection = con;

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                //oo.MessageBox("Submitted successfully.", this.Page);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");
                oo.ClearControls(this.Page);
                load();
            }
            catch (SqlException ee) { oo.MessageBox(ee.Message.ToString(), this.Page); }
        }
    }
  
    protected void Button3_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "BloodGroupMasterUpdateProc";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@id", lblID.Text);
        cmd.Parameters.AddWithValue("@BloodGroupRemark",txtRemarkPanel.Text);
        cmd.Parameters.AddWithValue("@BloodGroupName", txtPanelBloodGroup.Text);      
        cmd.Connection = con;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            //oo.MessageBox("Updated successfully.", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "S");
            load();
        }
        catch (SqlException) { }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        sql = "Delete from BloodGroupMaster where BloodGroupId=" + lblvalue.Text;

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
    protected void Button8_Click(object sender, EventArgs e)
    {

    }
    protected void Button4_Click(object sender, EventArgs e)
    {

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


    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label36");
        string ss = lblId.Text;
        lblID.Text = ss;

        // sql = "Select  distinct ROW_NUMBER() OVER (ORDER BY p.ProductId ASC) AS  [ProductId] ,po.id as ID, pc.ProductCategoryName as[ProductCategoryName], p.ProductName as [ProductName], Pm.ProductTypeName as [ProductTypeName],PO.ProductModelName as [ProductModelName] from Productcategorymaster pc left join ProductName p on p.ProductId=pc.ProductId  left join ProductTypeMaster PM on p.ProductId=PM.ProductId left join ProductModelMaster PO on p.ProductId=PO.ProductId ";
        sql = "Select  ROW_NUMBER() OVER (ORDER BY BloodGroupId ASC) AS SrNo,BloodGroupId,BloodGroupName,BloodGroupRemark from BloodGroupMaster where BloodGroupId=" + ss;

        txtPanelBloodGroup.Text = oo.ReturnTag(sql, "BloodGroupName");
        txtRemarkPanel.Text = oo.ReturnTag(sql, "BloodGroupRemark");


        Panel1_ModalPopupExtender.Show();
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