using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;

public partial class item_Sub_category : System.Web.UI.Page
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
            sql = "Select CategoryName,id from ItemCategoryMaster where BranchCode = " + Session["BranchCode"] + "";
            oo.FillDropDown_withValue(sql, drpcategory, "CategoryName", "id");
            oo.fillSelectvalue(drpcategory, "<--Select-->");
            oo.FillDropDown_withValue(sql, drpcategory0, "CategoryName", "id");
            oo.fillSelectvalue(drpcategory0, "<--Select-->");
            loadData();
            string SubCode = "";
            try
            {
                sql = "select MAX(Id)+1 AS Srno from ItemSubCategoryMaster where BranchCode = " + Session["BranchCode"] + " ";
                SubCode = "Cat" + oo.ReturnTag(sql, "Srno");
            }
            catch (Exception) { SubCode = "Cat" + "1"; }
            txtsubcategorycode.Text = SubCode;
        }
    }
    protected void loadData()
    {
        sql = "Select  ROW_NUMBER() OVER (ORDER BY iscm.id ASC) AS SrNo,iscm.id,icm.CategoryName,SubCategoryName,SubCategoryCode,iscm.Remark from ItemSubCategoryMaster iscm";
        sql = sql + " inner join ItemCategoryMaster icm on icm.Id=iscm.CategoryName  Where iscm.BranchCode = " + Session["BranchCode"] + "";
        GridView1.DataSource = oo.GridFill(sql);
        GridView1.DataBind();
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            Label id = (Label)GridView1.Rows[i].FindControl("Label36");
            LinkButton LinkButton3 = (LinkButton)GridView1.Rows[i].FindControl("LinkButton3");
            sql = "SELECT SubCategory FROM LibraryItemEntry WHERE SubCategory='" + id.Text.Trim() + "' AND BranchCode=" + Session["BranchCode"] + "";
            if (oo.Duplicate(sql))
            {
                LinkButton3.Text = "<i class='fa fa-lock'></i>";
                LinkButton3.Enabled = false;
            }
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        sql = "select SubCategoryName,SubCategoryCode from ItemSubCategoryMaster where SubCategoryName='" + txtsubcategory.Text.Trim() + "' and BranchCode = " + Session["BranchCode"] + ""; //or SubCategoryCode='" + txtsubcategorycode.Text.ToString() + "'";
        sql = sql + " and CategoryName='" + drpcategory.SelectedValue.ToString() + "'";
        if (oo.Duplicate(sql))
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, Duplicate Entry!", "A");       
        }
        else if (drpcategory.SelectedItem.ToString() == "<--Select-->")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please Select Condition", "A");       
        }
        else
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "ItemSubCategoryMasterProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@CategoryName", drpcategory.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@SubCategoryName", txtsubcategory.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@SubCategoryCode", txtsubcategorycode.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@Remark", txtremark.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");       
                oo.ClearControls(this.Page);
                loadData();
                string SubCode = "";
                try
                {
                    sql = "select MAX(Id)+1 AS Srno from ItemSubCategoryMaster ";
                    SubCode = "Cat" + oo.ReturnTag(sql, "Srno");
                }
                catch (Exception) { SubCode = "Cat" + "1"; }
                txtsubcategorycode.Text = SubCode;
            }
            catch (Exception) { }
        }
    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        if (drpcategory0.SelectedItem.ToString() == "<--Select-->")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please Select Condition", "A");       
        }
        else
        {
            sql = "select SubCategoryName,SubCategoryCode from ItemSubCategoryMaster where SubCategoryName='" + txtsubcategory0.Text.Trim() + "'"; //or SubCategoryCode='" + txtsubcategorycode.Text.ToString() + "'";
            sql = sql + " and CategoryName='" + drpcategory0.SelectedValue.ToString() + "' and Id<>'"+lblID.Text+ "' and BranchCode = " + Session["BranchCode"] + "";

            if (oo.Duplicate(sql))
            {
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Duplicate Entry!", "A");       
            }
            else
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ItemSubCategoryMasterUpdateProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", lblID.Text);
                cmd.Parameters.AddWithValue("@CategoryName", drpcategory0.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@SubCategoryName", txtsubcategory0.Text.Trim().ToString());
                cmd.Parameters.AddWithValue("@SubCategoryCode", txtsubcategorycode0.Text.Trim().ToString());
                cmd.Parameters.AddWithValue("@Remark", txtremark0.Text.Trim().ToString());

                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
                cmd.Connection = con;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "S");
                    loadData();
                }
                catch (SqlException) { }
            }
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        sql = "Delete from ItemSubCategoryMaster where id=" + lblvalue.Text+ " and BranchCode = " + Session["BranchCode"] + "";

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
            loadData();
        }
        catch (SqlException) { }
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label36");
        string ss = lblId.Text;
        lblID.Text = ss;
        sql = "Select  ROW_NUMBER() OVER (ORDER BY id ASC) AS SrNo,id, CategoryName,SubCategoryName,SubCategoryCode,Remark from ItemSubCategoryMaster Where BranchCode = " + Session["BranchCode"] + " ";
        sql = sql + " and Id=" + ss;
        drpcategory0.SelectedValue = oo.ReturnTag(sql, "CategoryName");
        txtsubcategory0.Text = oo.ReturnTag(sql, "SubCategoryName");
        txtsubcategorycode0.Text = oo.ReturnTag(sql, "SubCategoryCode");
        txtremark0.Text = oo.ReturnTag(sql, "Remark");
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
    protected void LinkButton5_Click(object sender, EventArgs e)
    {
    }
    protected void Button8_Click(object sender, EventArgs e)
    {
    }
    protected void drpcategory_SelectedIndexChanged(object sender, EventArgs e)
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
