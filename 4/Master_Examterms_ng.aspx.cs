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
        Campus camp = new Campus(); camp.LoadLoader(loader);  

        if (!IsPostBack)
        {
            loadExamPattern();
            DisplayGrid();
        }
    }

    private void loadExamPattern()
    {
        sql = "select * from master_ExamPattern where BranchCode=" + Session["BranchCode"] + "";
        oo.FillDropDown_withValue(sql, ddlExamPattern, "pattern", "Id");
        ddlExamPattern.Items.Insert(0, new ListItem("<--Select-->", ""));

        sql = "select * from master_ExamPattern where BranchCode=" + Session["BranchCode"] + "";
        oo.FillDropDown_withValue(sql, ddlExamPattern0, "pattern", "Id");
        ddlExamPattern0.Items.Insert(0, new ListItem("<--Select-->", ""));
    }

    public void DisplayGrid()
    {
        if (ddlExamPattern.SelectedValue.ToString() == "")
        {
            sql = "select * from master_examterms ";
            sql += "inner join master_ExamPattern on master_ExamPattern.id = master_examterms.exampatternId ";
            sql += "where master_examterms.SessionName = '" + Session["SessionName"].ToString() + "' and master_examterms.BranchCode=" + Session["BranchCode"] + "";
        }
        else
        {
            sql = "select * from master_examterms ";
            sql += "inner join master_ExamPattern on master_ExamPattern.id = master_examterms.exampatternId ";
            sql += "where master_examterms.exampatternId = " + ddlExamPattern.SelectedValue.ToString() + " ";
            sql += "and master_examterms.SessionName = '" + Session["SessionName"].ToString() + "' and master_examterms.BranchCode=" + Session["BranchCode"] + "";
        }
        Grd.DataSource = oo.GridFill(sql);
        Grd.DataBind();
    }
    
    protected void ddlExamPattern_SelectedIndexChanged(object sender, EventArgs e)
    {
        DisplayGrid();
    }
    protected void ddlNoofterms_SelectedIndexChanged(object sender, EventArgs e)
    {
        row_1.Visible = false;
        row_2.Visible = false;
        row_3.Visible = false;
        row_4.Visible = false;
        if (ddlNoofterms.SelectedValue=="1")
        {
            row_1.Visible = true;
        }
        if (ddlNoofterms.SelectedValue == "2")
        {
            row_1.Visible = true;
            row_2.Visible = true;
        }
        if (ddlNoofterms.SelectedValue == "3")
        {
            row_1.Visible = true;
            row_2.Visible = true;
            row_3.Visible = true;
        }
        if (ddlNoofterms.SelectedValue == "4")
        {
            row_1.Visible = true;
            row_2.Visible = true;
            row_3.Visible = true;
            row_4.Visible = true;
        }
    }

    protected void lnkSave_Click(object sender, EventArgs e)
    {
        if (ddlExamPattern.SelectedValue == "")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, Select Exam Pattern", "A");
        }

        if (ddlNoofterms.SelectedValue=="")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, Select No. Of Terms", "A");
        }

        if (ddlExamPattern.SelectedValue != "")
        {

            if (ddlNoofterms.SelectedValue == "1")
            {
                if (txtTerm1.Text == "" || txtT1StartDate.Text == "" || txtT1EndDate.Text == "")
                {
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, Enter all fields Of Terms", "A");
                }
                else
                {
                    sql = "select * from master_examterms ";
                    sql += "where exampatternId = " + ddlExamPattern.SelectedValue.ToString() + " and BranchCode=" + Session["BranchCode"] + " and term='" + txtTerm1.Text + "' and SessionName='" + Session["SessionName"].ToString() + "' ";
                }
            }
            if (ddlNoofterms.SelectedValue == "2")
            {
                if (txtTerm1.Text == "" || txtT1StartDate.Text == "" || txtT1EndDate.Text == "" || txtTerm2.Text == "" || txtT2StartDate.Text == "" || txtT2EndDate.Text == "")
                {
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, Enter all fields Of Terms", "A");
                }
                else
                {
                    sql = "select * from master_examterms ";
                    sql += "where exampatternId = " + ddlExamPattern.SelectedValue.ToString() + " and BranchCode=" + Session["BranchCode"] + " and term in ('" + txtTerm1.Text + "', '" + txtTerm2.Text + "') and SessionName='" + Session["SessionName"].ToString() + "' ";
                }
            }
            if (ddlNoofterms.SelectedValue == "3")
            {
                if (txtTerm1.Text == "" || txtT1StartDate.Text == "" || txtT1EndDate.Text == "" || txtTerm2.Text == "" || txtT2StartDate.Text == "" || txtT2EndDate.Text == "" || txtTerm3.Text == "" || txtT3StartDate.Text == "" || txtT3EndDate.Text == "")
                {
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, Enter all fields Of Terms", "A");
                }
                else
                {
                    sql = "select * from master_examterms ";
                    sql += "where exampatternId = " + ddlExamPattern.SelectedValue.ToString() + " and BranchCode=" + Session["BranchCode"] + " and term in ('" + txtTerm1.Text + "', '" + txtTerm2.Text + "', '" + txtTerm3.Text + "') and SessionName='" + Session["SessionName"].ToString() + "' ";
                }
            }
            if (ddlNoofterms.SelectedValue == "4")
            {
                if (txtTerm1.Text == "" || txtT1StartDate.Text == "" || txtT1EndDate.Text == "" || txtTerm2.Text == "" || txtT2StartDate.Text == "" || txtT2EndDate.Text == "" || txtTerm3.Text == "" || txtT3StartDate.Text == "" || txtT3EndDate.Text == "")
                {
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, Enter all fields Of Terms", "A");
                }
                else
                {
                    sql = "select * from master_examterms ";
                    sql += "where exampatternId = " + ddlExamPattern.SelectedValue.ToString() + " and BranchCode=" + Session["BranchCode"] + " and term in ('" + txtTerm1.Text + "', '" + txtTerm2.Text + "', '" + txtTerm3.Text + "', , '" + txtTerm4.Text + "') and SessionName='" + Session["SessionName"].ToString() + "' ";
                }
            }
        }
        if (sql!="")
        {
            if (oo.Duplicate(sql))
            {
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, Duplicate Entry!", "A");
            }

            else
            {
                save();
                oo.ClearControls(this.Page);
                DisplayGrid();
            }
        }
        
    }

    public void save()
    {
        int lenth = int.Parse(ddlNoofterms.SelectedValue);
        int counts = 0;
        for (int i = 0; i < lenth; i++)
        {
            cmd = new SqlCommand();
            cmd.CommandText = "usp_master_examterms";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@Queryfor", "save");
            cmd.Parameters.AddWithValue("@exampatternId", ddlExamPattern.SelectedValue.ToString());
            if (i==0)
            {
                cmd.Parameters.AddWithValue("@term", txtTerm1.Text.Trim());
                cmd.Parameters.AddWithValue("@beginDate", txtT1StartDate.Text.Trim());
                cmd.Parameters.AddWithValue("@endDate", txtT1EndDate.Text.Trim());
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                try
                {
                    con.Open();
                    int x1=cmd.ExecuteNonQuery();
                    if (x1>0)
                    {
                        counts= counts+1;
                    }
                    cmd.Parameters.Clear();
                    con.Close();
                }
                catch (Exception ex)
                {
                }
            }
            if (i == 1)
            {
                cmd.Parameters.AddWithValue("@term", txtTerm2.Text.Trim());
                cmd.Parameters.AddWithValue("@beginDate", txtT2StartDate.Text.Trim());
                cmd.Parameters.AddWithValue("@endDate", txtT2EndDate.Text.Trim());
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                try
                {
                    con.Open();
                    int x2 = cmd.ExecuteNonQuery();
                    if (x2 > 0)
                    {
                        counts = counts + 1;
                    }
                    cmd.Parameters.Clear();
                    con.Close();
                }
                catch (Exception ex)
                {
                }
            }
            if (i == 2)
            {
                cmd.Parameters.AddWithValue("@term", txtTerm3.Text.Trim());
                cmd.Parameters.AddWithValue("@beginDate", txtT3StartDate.Text.Trim());
                cmd.Parameters.AddWithValue("@endDate", txtT3EndDate.Text.Trim());
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                try
                {
                    con.Open();
                    int x3 = cmd.ExecuteNonQuery();
                    if (x3 > 0)
                    {
                        counts = counts + 1;
                    }
                    cmd.Parameters.Clear();
                    con.Close();
                }
                catch (Exception ex)
                {
                }
            }
            if (i == 3)
            {
                cmd.Parameters.AddWithValue("@term", txtTerm4.Text.Trim());
                cmd.Parameters.AddWithValue("@beginDate", txtT4StartDate.Text.Trim());
                cmd.Parameters.AddWithValue("@endDate", txtT4EndDate.Text.Trim());
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                try
                {
                    con.Open();
                    int x4 = cmd.ExecuteNonQuery();
                    if (x4 > 0)
                    {
                        counts = counts + 1;
                    }
                    cmd.Parameters.Clear();
                    con.Close();
                }
                catch (Exception ex)
                {
                }
            }
        }
        if (counts > 0)
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");
            DisplayGrid();
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, Somthing went wrong!", "A");
        }
    }

    

    protected void LinkEdit_Click(object sender, EventArgs e)
    {
        GridViewRow gvr = (GridViewRow)(sender as Control).Parent.Parent;
        Label LblIdForEdit = (Label)gvr.FindControl("LblIdForEdit");

        sql = "select * from master_examterms where id="+ LblIdForEdit.Text + " and BranchCode=" + Session["BranchCode"] + "";
        cmd = new SqlCommand();
        cmd.CommandText = sql;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count>0)
        {
            LblIdForUpdate.Text = LblIdForEdit.Text;
            ddlExamPattern0.SelectedValue= dt.Rows[0]["exampatternId"].ToString();
            txtTerm.Text = dt.Rows[0]["term"].ToString();
            txtStartDate.Text = DateTime.Parse(dt.Rows[0]["beginDate"].ToString()).ToString("dd-MMM-yyyy");
            txtEndDate.Text = DateTime.Parse(dt.Rows[0]["endDate"].ToString()).ToString("dd-MMM-yyyy");
        }
        Panel1_ModalPopupExtender.Show();
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (txtTerm.Text == "" || txtStartDate.Text == "" || txtEndDate.Text == "" || ddlExamPattern0.SelectedValue=="")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, Enter all fields Of Terms", "A");
        }
        else
        {
            cmd = new SqlCommand();
            cmd.CommandText = "usp_master_examterms";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@Queryfor", "update");
            cmd.Parameters.AddWithValue("@id", LblIdForUpdate.Text);
            cmd.Parameters.AddWithValue("@exampatternId", ddlExamPattern0.SelectedValue);
            cmd.Parameters.AddWithValue("@term", txtTerm.Text.Trim());
            cmd.Parameters.AddWithValue("@beginDate", txtStartDate.Text.Trim());
            cmd.Parameters.AddWithValue("@endDate", txtEndDate.Text.Trim());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                DisplayGrid();
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "S");
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
        sql = "Delete from master_examterms where id=" + lblvalue.Text + " and BranchCode=" + Session["BranchCode"] + "";

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
}