using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StudentShiftMapping : Page
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
            sql += "  where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            sql += "  order by Id";
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
        if (drpBranch.SelectedIndex != 0)
        {
            param.Add(new SqlParameter("@BranchId", drpBranch.SelectedValue));
        }
        if (ddlStatus.SelectedIndex != 0)
        {
            param.Add(new SqlParameter("@Gender", ddlStatus.SelectedValue));
        }
        param.Add(new SqlParameter("@SessionName", Session["SessionName"]));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"]));
        param.Add(new SqlParameter("@Action", "Select"));
        DataSet ds = new DLL().Sp_SelectRecord_usingExecuteDataset("sp_StudentShiftMapping", param);
        if (ds != null)
        {
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 1)
            {

                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
                if (GridView1.Rows.Count > 0)
                {
                    LinkButton1.Visible = true;
                    var drpShiftAll = (DropDownList)GridView1.HeaderRow.FindControl("drpShiftAll");
                    string sqls = "select id, ShiftName from StudentShiftMaster ";
                    sqls = sqls + "  where BranchCode=" + Session["BranchCode"] + "";
                    oo.FillDropDown_withValue(sqls, drpShiftAll, "ShiftName", "id");
                    drpShiftAll.Items.Insert(0, new ListItem("<--Select-->", ""));
                    for (int i = 0; i < GridView1.Rows.Count; i++)
                    {
                        var drpShift = (DropDownList)GridView1.Rows[i].FindControl("drpShift");
                        oo.FillDropDown_withValue(sqls, drpShift, "ShiftName", "id");
                        drpShift.Items.Insert(0, new ListItem("<--Select-->", ""));

                        var lblSrno = (Label)GridView1.Rows[i].FindControl("lblSrno");
                        string sqls1 = "select Shiftid from StudentShiftMapping where srno='" + lblSrno.Text + "' and ";
                        sqls1 = sqls1 + "BranchCode=" + Session["BranchCode"] + "";
                        if (oo.Duplicate(sqls1))
                        {
                            drpShift.SelectedValue = oo.ReturnTag(sqls1, "Shiftid");
                        }
                    }
                }
            }
            else
            {
                LinkButton1.Visible = false;
            }
        }
    }

    protected void DrpClass_SelectedIndexChanged(object sender, EventArgs e)
    {

        string sql = "select id, SectionName from SectionMaster ";
        sql += "  where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and ClassNameId=" + DrpClass.SelectedValue + "";
        sql += "  order by Id";
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
            var drpShift = (DropDownList)GridView1.Rows[i].FindControl("drpShift");
            if (drpShift.SelectedIndex == 0)
            {
                flg1 += 1;
            }
        }
        if (flg1 > 0)
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Please allot sift in all students.", "A");
            return;
        }
        int flg = 0;
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            var drpShift = (DropDownList)GridView1.Rows[i].FindControl("drpShift");
            if (drpShift.SelectedIndex > 0)
            {
                var lblSrno = (Label)GridView1.Rows[i].FindControl("lblSrno");
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "sp_StudentShiftMapping";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                cmd.Parameters.AddWithValue("@Shiftid", drpShift.SelectedValue);
                cmd.Parameters.AddWithValue("@SrNo", lblSrno.Text);
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

    protected void drpShiftAll_SelectedIndexChanged(object sender, EventArgs e)
    {
        var drpShiftAll = (DropDownList)GridView1.HeaderRow.FindControl("drpShiftAll");
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            var drpShift = (DropDownList)GridView1.Rows[i].FindControl("drpShift");
            drpShift.SelectedValue = drpShiftAll.SelectedValue;
        }
    }
}