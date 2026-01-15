using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Master_Examterms_ng : Page
{
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    Campus oo = new Campus();
    string sql = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((string)Session["LoginName"] == "" || (string)Session["BranchCode"] == "" || (string)Session["SessionName"] == "" || Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

        if (!IsPostBack)
        {
            sql = "Select Id,ClassName from ClassMaster";
            sql = sql + " where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            sql = sql + "  order by CIDOrder";
            
            oo.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");
            drpclass.Items.Insert(0, new ListItem("<--Select-->", ""));
            oo.FillDropDown_withValue(sql, drpclass0, "ClassName", "Id");
            drpclass0.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
    }
    public void DisplayGrid()
    {
        if (ddlExamPattern.SelectedValue.ToString() != "" || drpmedium.SelectedValue.ToString() != "<--Select-->")
        {
            sql = "select isnull(master_subjects.isOptional, '0') isOptionals, * from master_subjects ";
            sql += "inner join master_ExamPattern on master_ExamPattern.id = master_subjects.exampatternId and master_subjects.Branchcode=master_ExamPattern.Branchcode ";
            sql += "inner join ClassMaster on ClassMaster.Id = master_subjects.classid  and master_subjects.Branchcode=ClassMaster.Branchcode ";
            sql += "left join BranchMaster on BranchMaster.Id = master_subjects.branchid  and master_subjects.Branchcode=BranchMaster.Branchcode ";
            sql += "where master_subjects.exampatternId = " + ddlExamPattern.SelectedValue.ToString() + " and master_subjects.classid = " + drpclass.SelectedValue.ToString() + " ";
            sql += "and master_subjects.SessionName = '" + Session["SessionName"].ToString() + "' and master_subjects.Branchcode=" + Session["Branchcode"] +" and isnull(master_subjects.Medium, '') = case when ('" + drpmedium.SelectedValue + "'='<--Select-->' or '" + drpmedium.SelectedValue + "'='') then isnull(master_subjects.Medium, '') else '" + drpmedium.SelectedValue + "' end ";

            Grd.DataSource = oo.GridFill(sql);
            Grd.DataBind();
        }
        else { Grd.DataSource = null; Grd.DataBind(); }
    }
   
    protected void lnkSave_Click(object sender, EventArgs e)
    {
        sql = "select * from master_subjects ";
        sql += "inner join master_ExamPattern on master_ExamPattern.id = master_subjects.exampatternId  and master_subjects.Branchcode=master_ExamPattern.Branchcode ";
        sql += "inner join ClassMaster on ClassMaster.Id = master_subjects.classid  and master_subjects.Branchcode=ClassMaster.Branchcode ";
        sql += "where master_subjects.exampatternId = " + ddlExamPattern.SelectedValue.ToString() + " and master_subjects.classid = " + drpclass.SelectedValue.ToString() + " ";
        sql += "and master_subjects.SessionName = '" + Session["SessionName"].ToString() + "' and master_subjects.Branchcode=" + Session["Branchcode"] + " and master_subjects.subjectName = '" + txtSubject.Text.Trim() + "' and master_subjects.Medium = '" + drpmedium.SelectedValue + "'";

        if (oo.Duplicate(sql))
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, Duplicate Entry!", "A");
        }
        else if (txtSubject.Text == "" || ddlExamPattern.SelectedValue=="" || drpmedium.SelectedValue== "<--Select-->")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, Enter and select all fields Of subject", "A");
        }
        else
        {
            save();
            oo.ClearControls(this.Page);
            DisplayGrid();
        }
    }
    public void save()
    {
        cmd = new SqlCommand();
        cmd.CommandText = "usp_master_subjects";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        cmd.Parameters.AddWithValue("@Queryfor", "save");
        cmd.Parameters.AddWithValue("@exampatternId",ddlExamPattern.SelectedValue);
        cmd.Parameters.AddWithValue("@classid", drpclass.SelectedValue);
        cmd.Parameters.AddWithValue("@subjectName", txtSubject.Text.Trim());
        cmd.Parameters.AddWithValue("@isVisible", ddlIsVisible.SelectedValue);
        cmd.Parameters.AddWithValue("@isOptional", ddlIsoptional.SelectedValue);
        cmd.Parameters.AddWithValue("@Medium", drpmedium.SelectedValue);
        cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
        cmd.Parameters.AddWithValue("@Branchcode", Session["Branchcode"].ToString());
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");
        }
        catch
        {
        }
    }
    protected void LinkEdit_Click(object sender, EventArgs e)
    {
        sql = "select * from master_ExamPattern where Branchcode=" + Session["Branchcode"].ToString() + " ";
        oo.FillDropDown_withValue(sql, ddlExamPattern0, "pattern", "Id");
        ddlExamPattern0.Items.Insert(0, new ListItem("<--Select-->", ""));

        //sql = "Select BranchName,Id from BranchMaster Where ClassId='" + drpclass0.SelectedValue.ToString() + "' and sessionName='" + Session["SessionName"].ToString() + "'";
        //oo.FillDropDown_withValue(sql, drpBranch0, "BranchName", "Id");
        //BAL.objBal.fillSelectvalue(drpBranch0, "<--Select-->");

        sql = "Select Medium from MediumMaster";
        sql = sql + " where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        oo.FillDropDown(sql, drpmedium0, "Medium");

        GridViewRow gvr = (GridViewRow)(sender as Control).Parent.Parent;
        Label LblIdForEdit = (Label)gvr.FindControl("LblIdForEdit");

        sql = "select * from master_subjects where id=" + LblIdForEdit.Text.Trim() + " and Branchcode=" + Session["Branchcode"].ToString() + " ";
        cmd = new SqlCommand();
        cmd.CommandText = sql;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            LblForUpdate.Text = LblIdForEdit.Text;
            drpclass0.SelectedValue = dt.Rows[0]["classid"].ToString();
            ddlExamPattern0.SelectedValue = dt.Rows[0]["exampatternId"].ToString();
            txtSubject0.Text = dt.Rows[0]["subjectName"].ToString();
            drpmedium0.SelectedValue= (dt.Rows[0]["Medium"].ToString()==""? "<--Select-->" : dt.Rows[0]["Medium"].ToString());
            ddlIsVisible0.Text = dt.Rows[0]["isVisible"].ToString();
            ddlIsoptional0.Text = dt.Rows[0]["isOptional"].ToString();
        }
        Panel1_ModalPopupExtender.Show();
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (txtSubject0.Text == "")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, Enter Subject", "A");
        }
        else if (drpmedium0.SelectedValue == "<--Select-->")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, Select Medium", "A");
        }
        else
        {
            cmd = new SqlCommand();
            cmd.CommandText = "usp_master_subjects";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@Queryfor", "update");
            cmd.Parameters.AddWithValue("@id", LblForUpdate.Text.Trim());
            cmd.Parameters.AddWithValue("@subjectName", txtSubject0.Text.Trim());
            cmd.Parameters.AddWithValue("@Medium", drpmedium0.SelectedValue);
            cmd.Parameters.AddWithValue("@isVisible", ddlIsVisible0.SelectedValue);
            cmd.Parameters.AddWithValue("@isOptional", ddlIsoptional0.SelectedValue);
            cmd.Parameters.AddWithValue("@Branchcode", Session["Branchcode"].ToString());
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                DisplayGrid();
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");
            }
            catch
            {
            }
        }
    }
    protected void LinkDelete_Click(object sender, EventArgs e)
    {
        GridViewRow gvr = (GridViewRow)(sender as Control).Parent.Parent;
        Label LblIdForDelete = (Label)gvr.FindControl("LblIdForDelete");
        lblvalue.Text = LblIdForDelete.Text;
        Panel2_ModalPopupExtender.Show();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        sql = "Delete from master_subjects where Id=" + lblvalue.Text.Trim() + " and Branchcode=" + Session["Branchcode"].ToString() + "";

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
            DisplayGrid();
        }
        catch (SqlException) { }
    }
    

    protected void drpclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        
       
        ddlExamPattern.Items.Clear();
        drpmedium.Items.Clear();
        txtSubject.Text = "";
        if (drpclass.SelectedValue != "")
        {
            sql = "select * from master_ExamPattern  where Branchcode=" + Session["Branchcode"].ToString() + "";
            oo.FillDropDown_withValue(sql, ddlExamPattern, "pattern", "Id");
            ddlExamPattern.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
    }

    protected void drpclass0_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlExamPattern0.Items.Clear();
        drpmedium0.Items.Clear();
        txtSubject0.Text = "";
        if (drpclass0.SelectedValue != "")
        {
            sql = "select * from master_ExamPattern  where Branchcode=" + Session["Branchcode"].ToString() + "";
            oo.FillDropDown_withValue(sql, ddlExamPattern0, "pattern", "Id");
            ddlExamPattern.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
    }
    protected void ddlExamPattern_SelectedIndexChanged(object sender, EventArgs e)
    {
        drpmedium.Items.Clear();
        txtSubject.Text = "";
        sql = "Select Medium from MediumMaster";
        sql = sql + " where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        oo.FillDropDown(sql, drpmedium, "Medium");
        DisplayGrid();
    }
   

    protected void drpBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        drpmedium.Items.Clear();
        txtSubject.Text = "";
        sql = "Select Medium from MediumMaster";
        sql = sql + " where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        oo.FillDropDown(sql, drpmedium, "Medium");
    }

    protected void drpmedium_SelectedIndexChanged(object sender, EventArgs e)
    {
        DisplayGrid();
    }
}