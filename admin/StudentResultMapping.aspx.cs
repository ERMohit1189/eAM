using c4SmsNew;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StudentResultMapping : Page
{
    Campus oo = new Campus();
    SqlConnection con = new SqlConnection();
    protected void Page_PreInIt(object sender, EventArgs e)
    {
        if (Session["Logintype"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        Campus camp = new Campus(); camp.LoadLoader(loader);
        con = oo.dbGet_connection();
        if (!IsPostBack)
        {
            string sql = "Select id, ClassName from ClassMaster";
            sql +=  "  where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            sql +=  "  order by Id";
            oo.FillDropDown_withValue(sql, DrpClass, "ClassName", "id");
            DrpClass.Items.Insert(0, new ListItem("<--Select-->", ""));
            drpSection.Items.Insert(0, new ListItem("<--Select-->", ""));
            drpBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        
        LoadReport();
    }
    protected void LoadReport()
    {

        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@ClassId", DrpClass.SelectedValue));
        if (drpSection.SelectedIndex != 0)
        {
            param.Add(new SqlParameter("@SectionId", drpSection.SelectedValue));
        }
        if (drpBranch.SelectedIndex!=0)
        {
            param.Add(new SqlParameter("@BranchId", drpBranch.SelectedValue));
        }
        if (ddlgender.SelectedIndex != 0)
        {
            param.Add(new SqlParameter("@Gender", ddlgender.SelectedValue));
        }
        param.Add(new SqlParameter("@Status", ddlStatus.SelectedValue));
        param.Add(new SqlParameter("@SessionName", Session["SessionName"]));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"]));
        param.Add(new SqlParameter("@Action", "Select"));
        DataSet ds = new DLL().Sp_SelectRecord_usingExecuteDataset("StudentResultMappingProc", param);
        if (ds != null)
        {
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 1)
            {
               
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
                if (GridView1.Rows.Count>0)
                {
                    LinkButton1.Visible = true;
                    var drpResultAll = (DropDownList)GridView1.HeaderRow.FindControl("drpResultAll");
                    string sqls = "select id, ResultText from ResultMaster ";
                    sqls = sqls + "  where BranchCode=" + Session["BranchCode"] + "";
                    oo.FillDropDown_withValue(sqls, drpResultAll, "ResultText", "id");
                    drpResultAll.Items.Insert(0, new ListItem("<--Select-->", "0"));
                    for (int i = 0; i < GridView1.Rows.Count; i++)
                    {
                        var drpResult = (DropDownList)GridView1.Rows[i].FindControl("drpResult");
                        oo.FillDropDown_withValue(sqls, drpResult, "ResultText", "id");
                        drpResult.Items.Insert(0, new ListItem("<--Select-->", "0"));

                        var lblSrno = (Label)GridView1.Rows[i].FindControl("lblSrno");
                        string sqls1 = "select ResultId, PromotedTo from StudentResultMapping where srno='" + lblSrno.Text + "' and ";
                        sqls1 = sqls1 + "   SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                        if (oo.Duplicate(sqls1))
                        {
                            drpResult.SelectedValue = oo.ReturnTag(sqls1, "ResultId");
                        }
                    }
                }
            }
            else
            {
                LinkButton1.Visible = false;
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
            }
        }
    }
    
    protected void DrpClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        string  sql = "select id, SectionName from SectionMaster ";
        sql +=  "  where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and ClassNameId="+ DrpClass.SelectedValue + "";
        sql +=  "  order by Id";
        oo.FillDropDown_withValue(sql, drpSection, "SectionName", "id");
        drpSection.Items.Insert(0, new ListItem("<--Select-->", ""));

        string sqls = "select id, BranchName from BranchMaster ";
        sqls = sqls + "  where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and ClassId=" + DrpClass.SelectedValue + "";
        sqls = sqls + "  order by Id";
        oo.FillDropDown_withValue(sqls, drpBranch, "BranchName", "id");
        drpBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        int flg1 = 0;
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            var drpResult = (DropDownList)GridView1.Rows[i].FindControl("drpResult");
            if (drpResult.SelectedIndex == 0)
            {
                flg1 += 1;
            }
        }
        if (flg1>0)
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Please select result in all students.", "A");
            return;
        }
        int flg = 0;
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            var drpResult = (DropDownList)GridView1.Rows[i].FindControl("drpResult");
            var txtPromotedToClass = (TextBox)GridView1.Rows[i].FindControl("txtPromotedToClass");
            if (drpResult.SelectedIndex>0)
            {
                var lblSrno = (Label)GridView1.Rows[i].FindControl("lblSrno");
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "StudentResultMappingProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                cmd.Parameters.AddWithValue("@ResultId", drpResult.SelectedValue);
                cmd.Parameters.AddWithValue("@PromotedTo", txtPromotedToClass.Text.Trim());
                cmd.Parameters.AddWithValue("@SrNo", lblSrno.Text);
                cmd.Parameters.AddWithValue("@ReopenOn", txtDateReopen.Text.Trim()+" "+ txtTime.Text.Trim());
                cmd.Parameters.AddWithValue("@GeneratedOn", txtGeneratedOn.Text.Trim());
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
                cmd.Parameters.AddWithValue("@Action", "Insert");
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    flg += 1;
                }
                catch (Exception) { con.Close(); }
            }
        }
        if (flg > 0)
        {
            LoadReport();
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
        }
        
    }

    protected void drpResultAll_SelectedIndexChanged(object sender, EventArgs e)
    {
        var drpResultAll = (DropDownList)GridView1.HeaderRow.FindControl("drpResultAll");
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            var drpResult = (DropDownList)GridView1.Rows[i].FindControl("drpResult");
            drpResult.SelectedValue = drpResultAll.SelectedValue;
        }
    }

    protected void txtPromotedToClassHeader_TextChanged(object sender, EventArgs e)
    {
        var drpResultAll = (TextBox)GridView1.HeaderRow.FindControl("txtPromotedToClassHeader");
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            var drpResult = (TextBox)GridView1.Rows[i].FindControl("txtPromotedToClass");
            drpResult.Text = drpResultAll.Text;
        }
    }
}