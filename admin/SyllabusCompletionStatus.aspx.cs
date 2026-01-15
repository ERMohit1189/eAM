using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_SyllabusCompletionStatus : System.Web.UI.Page
{
    string _sql = "";
    private SqlConnection _con;
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
        _con = new SqlConnection();
        _con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);
        con = oo.dbGet_connection();
        if (!IsPostBack)
        {
            string sql = "Select id, ClassName from ClassMaster";
            sql += "  where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            sql += "  order by Id";
            oo.FillDropDown_withValue(sql, DrpClass, "ClassName", "id");
            oo.FillDropDown_withValue(sql, ddlClassEdit, "ClassName", "id");
            ddlClassEdit.Items.Insert(0, new ListItem("<--Select-->", ""));
            DrpClass.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlSubject.Items.Insert(0, new ListItem("<--Select-->", ""));
            drpBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlStreamEdit.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlSubjectEdit.Items.Insert(0, new ListItem("<--Select-->", ""));
            DropDownList1.Items.Insert(0, new ListItem("<--Select-->", ""));
            oo.AddDateMonthYearDropDown(DrpSaal, DrpMahina, DrpDin);
            oo.FindCurrentDateandSetinDropDown(DrpSaal, DrpMahina, DrpDin);

            oo.AddDateMonthYearDropDown(DropDownList2, DropDownList3, DropDownList4);
            oo.FindCurrentDateandSetinDropDown(DropDownList2, DropDownList3, DropDownList4);


            oo.AddDateMonthYearDropDown(DropDownList5, DropDownList6, DropDownList7);
            oo.FindCurrentDateandSetinDropDown(DropDownList5, DropDownList6, DropDownList7);

            oo.AddDateMonthYearDropDown(DropDownList8, DropDownList9, DropDownList10);
            oo.FindCurrentDateandSetinDropDown(DropDownList8, DropDownList9, DropDownList10);

            string _sql = "Select Top 1 ISNULL(MAX(ID), 0) AS MaxID  from SyllabusMaster_tb cm";
            _sql += " where cm.SessionName='" + Session["SessionName"] + "' and cm.BranchCode=" + Session["BranchCode"] + "  ";
            int maxID = Convert.ToInt32(oo.ReturnTag(_sql, "MaxID")) + 1;
           // TextBox1.Text = maxID.ToString();
        }
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("lblLessonID");

        Label lblclassId = (Label)chk.NamingContainer.FindControl("lblClassID");
        Label lblBeanchId = (Label)chk.NamingContainer.FindControl("lblStreamID");
        Label LabelSubject = (Label)chk.NamingContainer.FindControl("lblSubjectID");
        Label lblLessonName = (Label)chk.NamingContainer.FindControl("lblLessonName");
        Label lblLessondiscription = (Label)chk.NamingContainer.FindControl("lblLessondiscription");
        Label lblDisplayofSequence = (Label)chk.NamingContainer.FindControl("lblDisplayofSequence");
        Label LabelApplicableFor = (Label)chk.NamingContainer.FindControl("lblNumberofDays");
        LoadEditStream(Convert.ToInt32(lblclassId.Text));
        LoadEditSUbject(Convert.ToInt32(lblclassId.Text), Convert.ToInt32(lblBeanchId.Text));
        lblIDEdit.Text = lblId.Text;
        ddlClassEdit.SelectedValue = lblclassId.Text;

        ddlStreamEdit.SelectedValue = lblBeanchId.Text;
        ddlSubjectEdit.SelectedValue = LabelSubject.Text;
        txtlessonEdit.Text = lblLessonName.Text;
       // txtdiscriptionedit.Text = lblLessondiscription.Text;
        //txtEditDisplayOrder.Text = lblDisplayofSequence.Text;
        TextBox3.Text = LabelApplicableFor.Text;
        Panel1_ModalPopupExtender.Show();
    }
    private void LoadEditStream(int classId)
    {
        string sqls = "select id, BranchName from BranchMaster ";
        sqls = sqls + "  where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and ClassId=" + classId + "";
        sqls = sqls + "  order by Id";
        oo.FillDropDown_withValue(sqls, ddlStreamEdit, "BranchName", "id");
        ddlStreamEdit.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    private void LoadEditSUbject(int classId, int StreamID)
    {
        string sql = "select sm.*, sm.id sid, cm.ClassName, bm.BranchName, case when isnull(isPractical,0)=1 then 'Yes' else 'No' end isPracticals, case when isnull(isCompulsoryForBest5,0)=1 then 'Yes' else 'No' end isCompulsoryForBest from TTSubjectMaster sm ";
        sql = sql + " inner join ClassMaster cm on cm.id=sm.Classid and cm.BranchCode=sm.BranchCode and cm.SessionName=sm.SessionName ";
        sql = sql + " inner join BranchMaster bm on bm.id=sm.BranchId and bm.BranchCode=sm.BranchCode and bm.SessionName=sm.SessionName ";
        sql = sql + " inner join MediumMaster mm on mm.medium=sm.medium and mm.BranchCode=sm.BranchCode  ";
        sql = sql + " where sm.BranchCode=" + Session["BranchCode"] + " and sm.SessionName='" + Session["SessionName"] + "' and sm.Classid=" + classId + " and sm.BranchId=" + StreamID + "";

        sql = sql + " order by cm.id, sm.DisplayOrder asc";
        oo.FillDropDown_withValue(sql, ddlSubjectEdit, "SubjectName", "sid");
        ddlSubjectEdit.Items.Insert(0, new ListItem("<--Select-->", ""));


    }
    protected void LoadReport()
    {
        try
        {
            if (Session["SessionName"] == null || Session["BranchCode"] == null)
            {
                Campus camp = new Campus();
                camp.msgbox(Page, msgbox, "Session Expired. Please login again.", "E");
                return;
            }

            List<SqlParameter> param = new List<SqlParameter>
        {
            new SqlParameter("@ClassId", string.IsNullOrEmpty(DrpClass.SelectedValue) ? DBNull.Value : (object)Convert.ToInt32(DrpClass.SelectedValue)),
            new SqlParameter("@StreamID", string.IsNullOrEmpty(drpBranch.SelectedValue) ? DBNull.Value : (object)Convert.ToInt32(drpBranch.SelectedValue)),
            new SqlParameter("@SubjectID", string.IsNullOrEmpty(ddlSubject.SelectedValue) ? DBNull.Value : (object)Convert.ToInt32(ddlSubject.SelectedValue)), // If needed, replace with actual value
            new SqlParameter("@LessonID", string.IsNullOrEmpty(DropDownList1.SelectedValue) ? DBNull.Value : (object)Convert.ToInt32(DropDownList1.SelectedValue)), // If needed, replace with actual value
            new SqlParameter("@SessionName", Session["SessionName"].ToString()),
            new SqlParameter("@BranchCode", Convert.ToInt32(Session["BranchCode"])),
            new SqlParameter("@Action", "Select")
        };
            DataSet ds = new DLL().Sp_SelectRecord_usingExecuteDataset("SyllabusCompletionProc", param);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    Grd.DataSource = dt;
                    Grd.DataBind();
                }
                else
                {
                    Grd.DataSource = null;
                    Grd.DataBind();
                }
            }
            else
            {
                Grd.DataSource = null;
                Grd.DataBind();
            }
        }
        catch (Exception ex)
        {
            Campus camp = new Campus();
            camp.msgbox(Page, msgbox, "Error: " + ex.Message, "E");
        }
    }

    protected void DrpClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        string sqls = "select id, BranchName from BranchMaster ";
        sqls = sqls + "  where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and ClassId=" + DrpClass.SelectedValue + "";
        sqls = sqls + "  order by Id";
        oo.FillDropDown_withValue(sqls, drpBranch, "BranchName", "id");
        drpBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        int flg = 0;
        string message = "";
        SqlCommand cmd = new SqlCommand
        {
            CommandText = "SyllabusCompletionProc",
            CommandType = CommandType.StoredProcedure,
            Connection = con
        };
        cmd.Parameters.AddWithValue("@ClassId", Convert.ToInt32(DrpClass.SelectedValue));
        cmd.Parameters.AddWithValue("@StreamID", Convert.ToInt32(drpBranch.SelectedValue));
        cmd.Parameters.AddWithValue("@SubjectID", Convert.ToInt32(ddlSubject.SelectedValue));
        cmd.Parameters.AddWithValue("@LessonID", Convert.ToInt32(DropDownList1.SelectedValue));
        cmd.Parameters.AddWithValue("@ActualCompletiondate", DrpSaal.SelectedItem.Text.ToString() + "/" + DrpMahina.SelectedItem.Text.ToString() + "/" + DrpDin.SelectedItem.Text.ToString());
        cmd.Parameters.AddWithValue("@Completiondate", DropDownList2.SelectedItem.Text.ToString() + "/" + DropDownList3.SelectedItem.Text.ToString() + "/" + DropDownList4.SelectedItem.Text.ToString());
        cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
        cmd.Parameters.AddWithValue("@BranchCode", Convert.ToInt32(Session["BranchCode"]));
        cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
        cmd.Parameters.AddWithValue("@Action", "Insert");
        try
        {
            con.Open();
            object result = cmd.ExecuteScalar();
            con.Close();
            if (result != null)
            {
                message = result.ToString();
                flg = message == "Saved Successfully" ? 1 : 0;
            }

        }
        catch (Exception ex)
        {
            con.Close();
            message = "Error: " + ex.Message;
        }
        if (flg > 0)
        {
            LoadReport();
            Campus camp = new Campus();
            camp.msgbox(Page, msgbox, message, "S");
        }
        else
        {
            Campus camp = new Campus();
            camp.msgbox(Page, msgbox, message, "E");
        }


    }

    protected void drpResultAll_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void txtPromotedToClassHeader_TextChanged(object sender, EventArgs e)
    {

    }

    protected void drpBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        string sql = "select sm.*, sm.id sid, cm.ClassName, bm.BranchName, case when isnull(isPractical,0)=1 then 'Yes' else 'No' end isPracticals, case when isnull(isCompulsoryForBest5,0)=1 then 'Yes' else 'No' end isCompulsoryForBest from TTSubjectMaster sm ";
        sql = sql + " inner join ClassMaster cm on cm.id=sm.Classid and cm.BranchCode=sm.BranchCode and cm.SessionName=sm.SessionName ";
        sql = sql + " inner join BranchMaster bm on bm.id=sm.BranchId and bm.BranchCode=sm.BranchCode and bm.SessionName=sm.SessionName ";
        sql = sql + " inner join MediumMaster mm on mm.medium=sm.medium and mm.BranchCode=sm.BranchCode  ";
        sql = sql + " where sm.BranchCode=" + Session["BranchCode"] + " and sm.SessionName='" + Session["SessionName"] + "' and sm.Classid=" + DrpClass.SelectedValue + " and sm.BranchId=" + drpBranch.SelectedValue + "";

        sql = sql + " order by cm.id, sm.DisplayOrder asc";
        oo.FillDropDown_withValue(sql, ddlSubject, "SubjectName", "sid");
        ddlSubject.Items.Insert(0, new ListItem("<--Select-->", ""));
    }

    protected void LinkButton3_Click(object sender, EventArgs e)
    {

        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("lblLessonID");
        // Label lblclassId = (Label)chk.NamingContainer.FindControl("lblclassId");
        // lblClassids.Text = lblclassId.Text;
        lblValue.Text = lblId.Text;
        mpeDelete.Show();
    }

    protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        string sqls = "select ID, LessonName from SyllabusMaster_tb ";
        sqls = sqls + "  where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and ClassID=" + DrpClass.SelectedValue + " and StreamID=" + drpBranch.SelectedValue + " and SubjectID=" + ddlSubject.SelectedValue + "";
        sqls = sqls + "  order by Id";
        oo.FillDropDown_withValue(sqls, DropDownList1, "LessonName", "ID");
        DropDownList1.Items.Insert(0, new ListItem("<--Select-->", ""));
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        int flg = 0;
        string message = "";
        SqlCommand cmd = new SqlCommand
        {
            CommandText = "SyllabusCompletionProc",
            CommandType = CommandType.StoredProcedure,
            Connection = con
        };
        cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(lblIDEdit.Text));
        cmd.Parameters.AddWithValue("@ClassId", Convert.ToInt32(ddlClassEdit.SelectedValue));
        cmd.Parameters.AddWithValue("@StreamID", Convert.ToInt32(ddlStreamEdit.SelectedValue));
        cmd.Parameters.AddWithValue("@SubjectID", Convert.ToInt32(ddlSubjectEdit.SelectedValue));
        cmd.Parameters.AddWithValue("@LessonID", Convert.ToInt32(lblValue.Text));
        cmd.Parameters.AddWithValue("@ActualCompletiondate", DropDownList5.SelectedItem.Text.ToString() + "/" + DropDownList6.SelectedItem.Text.ToString() + "/" + DropDownList7.SelectedItem.Text.ToString());
        cmd.Parameters.AddWithValue("@Completiondate", DropDownList8.SelectedItem.Text.ToString() + "/" + DropDownList9.SelectedItem.Text.ToString() + "/" + DropDownList10.SelectedItem.Text.ToString());
        cmd.Parameters.AddWithValue("@NumberofDays", Convert.ToDecimal(TextBox3.Text.Trim()));
        cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
        cmd.Parameters.AddWithValue("@BranchCode", Convert.ToInt32(Session["BranchCode"]));
        cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
        cmd.Parameters.AddWithValue("@Action", "Update");
        try
        {
            con.Open();
            object result = cmd.ExecuteScalar();
            con.Close();
            if (result != null)
            {
                message = result.ToString();
                flg = message == "Update Successfully" ? 1 : 0;
            }

        }
        catch (Exception ex)
        {
            con.Close();
            message = "Error: " + ex.Message;
        }
        if (flg > 0)
        {
            LoadReport();
            Campus camp = new Campus();
            camp.msgbox(Page, msgbox, message, "S");
        }
        else
        {
            Campus camp = new Campus();
            camp.msgbox(Page, msgbox, message, "E");
        }
    }

    protected void btnNo_Click(object sender, EventArgs e)
    {

    }

    protected void btnYes_Click(object sender, EventArgs e)
    {
        _sql = "Delete from SyllabusMaster_tb where ID=" + lblValue.Text + "  and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "'";

        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = _sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = _con;
            try
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Deleted successfully.", "S");
                LoadReport();

            }
            catch (Exception ex)
            {
            }
        }
    }

    protected void ddlClassEdit_SelectedIndexChanged(object sender, EventArgs e)
    {
        string sqls = "select id, BranchName from BranchMaster ";
        sqls = sqls + "  where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and ClassId=" + ddlClassEdit.SelectedValue + "";
        sqls = sqls + "  order by Id";
        oo.FillDropDown_withValue(sqls, ddlStreamEdit, "BranchName", "id");
        ddlStreamEdit.Items.Insert(0, new ListItem("<--Select-->", ""));
    }

    protected void ddlStreamEdit_SelectedIndexChanged(object sender, EventArgs e)
    {
        string sql = "select sm.*, sm.id sid, cm.ClassName, bm.BranchName, case when isnull(isPractical,0)=1 then 'Yes' else 'No' end isPracticals, case when isnull(isCompulsoryForBest5,0)=1 then 'Yes' else 'No' end isCompulsoryForBest from TTSubjectMaster sm ";
        sql = sql + " inner join ClassMaster cm on cm.id=sm.Classid and cm.BranchCode=sm.BranchCode and cm.SessionName=sm.SessionName ";
        sql = sql + " inner join BranchMaster bm on bm.id=sm.BranchId and bm.BranchCode=sm.BranchCode and bm.SessionName=sm.SessionName ";
        sql = sql + " inner join MediumMaster mm on mm.medium=sm.medium and mm.BranchCode=sm.BranchCode  ";
        sql = sql + " where sm.BranchCode=" + Session["BranchCode"] + " and sm.SessionName='" + Session["SessionName"] + "' and sm.Classid=" + ddlClassEdit.SelectedValue + " and sm.BranchId=" + ddlStreamEdit.SelectedValue + "";

        sql = sql + " order by cm.id, sm.DisplayOrder asc";
        oo.FillDropDown_withValue(sql, ddlSubjectEdit, "SubjectName", "sid");
        ddlSubjectEdit.Items.Insert(0, new ListItem("<--Select-->", ""));
    }

    protected void DrpSaal_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(DrpSaal, DrpMahina, DrpDin);
    }

    protected void DrpMahina_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(DrpSaal, DrpMahina, DrpDin);
    }

    protected void DrpDin_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadReport();
        string sqls = "select *,DATEADD(DAY, Cast(NumberofDays as int), Created_date) AS NewDate,YEAR(DATEADD(DAY, Cast(NumberofDays as int), Created_date)) AS YearPart,FORMAT(DATEADD(DAY, Cast(NumberofDays as int), Created_date), 'MMM') AS MonthAbbr,DAY(DATEADD(DAY, Cast(NumberofDays as int), Created_date)) AS DayPart from SyllabusMaster_tb ";
        sqls = sqls + "  where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and ClassID=" + DrpClass.SelectedValue + " and StreamID=" + drpBranch.SelectedValue + " and SubjectID=" + ddlSubject.SelectedValue + "";
        sqls = sqls + "  order by ID";
        var dt = oo.Fetchdata(sqls);
        if (dt.Rows.Count > 0)
        {
            DrpSaal.SelectedValue = dt.Rows[0]["YearPart"].ToString();
            DrpMahina.SelectedValue = dt.Rows[0]["MonthAbbr"].ToString();
            DrpDin.SelectedValue = dt.Rows[0]["DayPart"].ToString();
        }
        else
        {
            DrpSaal.SelectedIndex = 0;
            DrpMahina.SelectedIndex = 0;
            DrpDin.SelectedIndex = 0;
        }
    }

    protected void DropDownList5_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void DropDownList6_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void DropDownList7_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void DropDownList8_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void DropDownList9_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void DropDownList10_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}