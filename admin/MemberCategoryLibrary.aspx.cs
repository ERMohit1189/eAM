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
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
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
   

            sql = "Select SubCategoryName from ItemSubCategoryMaster"; //where CategoryName='"Books"'";
            oo.FillDropDown(sql, drpCatagoryName, "SubCategoryName");
            GridDisplay();
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        if (drpCatagoryName.SelectedItem.ToString() == "<--Select-->")
        {
            oo.MessageBox("Please Select Condition", this.Page);
        }
        else
        {
            sql = "Select CategoryName from MemberCategoryLibrary where CategoryName='" + drpCatagoryName.SelectedItem.ToString() + "'";
            if (oo.Duplicate(sql))
            {
                oo.MessageBox("Already exist", this.Page);
            }
            else
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "MemberCategoryLibraryProc";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CategoryName", drpCatagoryName.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@CautionMoneyLibrary", txtcatMonLibrary.Text.ToString());
                cmd.Parameters.AddWithValue("@CautionMoneyBookBank", txtcautionBookBank.Text.ToString());
                cmd.Parameters.AddWithValue("@MonthlychargeLibrary", txtMonthlyChargesLibrary.Text.ToString());
                cmd.Parameters.AddWithValue("@FineLibraryPerDay", txtFineLibraryPerDay.Text.ToString());
                cmd.Parameters.AddWithValue("@FineBookBankPerDay", txtFineBookBankPerday.Text.ToString());
                cmd.Parameters.AddWithValue("@MaxItemLibrary", txtMaximumitemLibrary.Text.ToString());
                cmd.Parameters.AddWithValue("@MaxItemBookBank", txtMaxItemBookBank.Text.ToString());
                cmd.Parameters.AddWithValue("@DaysReturnLibrary", txtdaysRetLibrary.Text.ToString());
                cmd.Parameters.AddWithValue("@DaysReturnBookBank", txtDaysRetBookBank.Text.ToString());
                cmd.Parameters.AddWithValue("@MembershipValidityinMonth", txtMembershipValidityMonth.Text.ToString());
                cmd.Parameters.AddWithValue("@Remark", txtRemark.Text.ToString());


                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());//Session["Login"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
                cmd.Connection = con;
                try
                {

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    oo.MessageBox("Submitted successfully.", this.Page);
                    oo.ClearControls(this.Page);
                    GridDisplay();
                }
                catch (Exception) { }
            }

        }
    }

    public void GridDisplay()
    {
        sql = "Select Row_Number() over (order by Id Asc) as SrNo,Id  ,CategoryName  ,CautionMoneyLibrary  ,CautionMoneyBookBank  ,MonthlychargeLibrary  ,FineLibraryPerDay ,";
        sql=sql+"   FineBookBankPerDay  ,MaxItemLibrary  ,MaxItemBookBank  ,DaysReturnLibrary ,DaysReturnBookBank  ,MembershipValidityinMonth  ,";
        sql = sql + "    Remark ,LoginName  ,SessionName  ,BranchCode  ,RecordDate from MemberCategoryLibrary";
        sql = sql + "  where  SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        sql = sql + "   and LoginName='" + Session["LoginName"].ToString() + "'";
        GridView1.DataSource = oo.GridFill(sql);
        GridView1.DataBind();



    }











    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        string ss = chk.Text;
        lblID.Text = ss;
        sql = "Select Row_Number() over (order by Id Asc) as SrNo,Id  ,CategoryName  ,CautionMoneyLibrary  ,CautionMoneyBookBank  ,MonthlychargeLibrary  ,FineLibraryPerDay ,";
        sql=sql+"   FineBookBankPerDay  ,MaxItemLibrary  ,MaxItemBookBank  ,DaysReturnLibrary ,DaysReturnBookBank  ,MembershipValidityinMonth  ,";
        sql = sql + "    Remark ,LoginName  ,SessionName  ,BranchCode  ,RecordDate from MemberCategoryLibrary where Id=" + ss;
        LblCatagoryName.Text = oo.ReturnTag(sql, "CategoryName");
        txtcatMonLibrary0.Text = oo.ReturnTag(sql, "CautionMoneyLibrary");
        txtcautionBookBank0.Text = oo.ReturnTag(sql, "CautionMoneyBookBank");
        txtMonthlyChargesLibrary0.Text = oo.ReturnTag(sql, "MonthlychargeLibrary");
        txtFineLibraryPerDay0.Text = oo.ReturnTag(sql, "FineLibraryPerDay");
        txtFineBookBankPerday0.Text = oo.ReturnTag(sql, "FineBookBankPerDay");
        txtMaximumitemLibrary0.Text = oo.ReturnTag(sql, "MaxItemLibrary");
        txtMaxItemBookBank0.Text = oo.ReturnTag(sql, "MaxItemBookBank");
        txtdaysRetLibrary0.Text = oo.ReturnTag(sql, "DaysReturnLibrary");
        txtDaysRetBookBank0.Text = oo.ReturnTag(sql, "DaysReturnBookBank");
        txtMembershipValidityMonth0.Text = oo.ReturnTag(sql, "MembershipValidityinMonth");

        txtRemark0.Text = oo.ReturnTag(sql, "Remark");
      
        Panel1_ModalPopupExtender.Show();


    }
protected void  Button3_Click(object sender, EventArgs e)
{
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "MemberCategoryLibraryUpdateProc";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", lblID.Text);
            cmd.Parameters.AddWithValue("@CategoryName", LblCatagoryName.Text.ToString());
            cmd.Parameters.AddWithValue("@CautionMoneyLibrary", txtcatMonLibrary0.Text.ToString());
            cmd.Parameters.AddWithValue("@CautionMoneyBookBank", txtcautionBookBank0.Text.ToString());
            cmd.Parameters.AddWithValue("@MonthlychargeLibrary", txtMonthlyChargesLibrary0.Text.ToString());
            cmd.Parameters.AddWithValue("@FineLibraryPerDay", txtFineLibraryPerDay0.Text.ToString());
            cmd.Parameters.AddWithValue("@FineBookBankPerDay", txtFineBookBankPerday0.Text.ToString());
            cmd.Parameters.AddWithValue("@MaxItemLibrary", txtMaximumitemLibrary0.Text.ToString());
            cmd.Parameters.AddWithValue("@MaxItemBookBank", txtMaxItemBookBank0.Text.ToString());
            cmd.Parameters.AddWithValue("@DaysReturnLibrary", txtdaysRetLibrary0.Text.ToString());
            cmd.Parameters.AddWithValue("@DaysReturnBookBank", txtDaysRetBookBank0.Text.ToString());
            cmd.Parameters.AddWithValue("@MembershipValidityinMonth", txtMembershipValidityMonth0.Text.ToString());
            cmd.Parameters.AddWithValue("@Remark", txtRemark0.Text.ToString());


            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());//Session["Login"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
            cmd.Connection = con;
            try
            {

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                oo.MessageBox("Updated successfully.", this.Page);
                oo.ClearControls(this.Page);
                GridDisplay();
            }
            catch (Exception) { }
        }

protected void Button4_Click(object sender, EventArgs e)
{

}
protected void btnDelete_Click(object sender, EventArgs e)
{
    sql = "Delete from MemberCategoryLibrary where Id=" + lblvalue.Text;

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
        GridDisplay();
    }
    catch (SqlException) { }
}
protected void Button8_Click(object sender, EventArgs e)
{

}
protected void LinkButton3_Click(object sender, EventArgs e)
{
    LinkButton chk = (LinkButton)sender;
    string ss = chk.Text;
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



