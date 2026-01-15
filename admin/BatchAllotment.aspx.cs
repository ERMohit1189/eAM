using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class admin_BatchAllotment : System.Web.UI.Page
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

            string sql1 = "Select ID, BatchName from BatchMst_tb";
            sql1 += "  where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            sql1 += "  order by Id";
            oo.FillDropDown_withValue(sql1, DropDownList1, "BatchName", "ID");
            DropDownList1.Items.Insert(0, new ListItem("<--Select-->", ""));

            oo.AddDateMonthYearDropDown(DDYear, DDMonth, DDDate);
            oo.FindCurrentDateandSetinDropDown(DDYear, DDMonth, DDDate);

            oo.AddDateMonthYearDropDown(DDYearTo, DDMonthTo, DDDateTo);
            oo.FindCurrentDateandSetinDropDown(DDYearTo, DDMonthTo, DDDateTo);

            string gtDate = "select top 1 case when convert(int, format(DateOfAdmiission,'dd'))<10 then convert(int, format(DateOfAdmiission,'dd')) else format(DateOfAdmiission,'dd') end dd, format(DateOfAdmiission, 'MMM')MMM, format(DateOfAdmiission, 'yyyy')yyyy from StudentOfficialDetails where SessionName = '" + Session["SessionName"] + "' and BranchCode = '" + Session["BranchCode"] + "' order by convert(date, DateOfAdmiission) asc";
            DDDate.SelectedValue = oo.ReturnTag(gtDate, "dd").ToString();
            DDMonth.SelectedValue = oo.ReturnTag(gtDate, "MMM").ToString();
            DDYear.SelectedValue = oo.ReturnTag(gtDate, "yyyy").ToString();
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
        txtdiscriptionedit.Text = lblLessondiscription.Text;
        txtEditDisplayOrder.Text = lblDisplayofSequence.Text;
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
            var fromDate = DDYear.SelectedItem + " " + DDMonth.SelectedItem + " " + DDDate.SelectedItem;
            var toDate = DDYearTo.SelectedItem + " " + DDMonthTo.SelectedItem + " " + DDDateTo.SelectedItem;
            List<SqlParameter> param = new List<SqlParameter>
            {
            new SqlParameter("@BatchID", string.IsNullOrEmpty(DropDownList1.SelectedValue) ? DBNull.Value : (object)Convert.ToInt32(DropDownList1.SelectedValue)),
            new SqlParameter("@ClassId", string.IsNullOrEmpty(DrpClass.SelectedValue) ? DBNull.Value : (object)Convert.ToInt32(DrpClass.SelectedValue)),
            new SqlParameter("@StreamID", string.IsNullOrEmpty(drpBranch.SelectedValue) ? DBNull.Value : (object)Convert.ToInt32(drpBranch.SelectedValue)),
            new SqlParameter("@SectionID", string.IsNullOrEmpty(ddlSubject.SelectedValue) ? DBNull.Value : (object)Convert.ToInt32(ddlSubject.SelectedValue)), // If needed, replace with actual value
            new SqlParameter("@DateofAllotment", txtlesson.Text.ToString()), // If needed, replace with actual value
            new SqlParameter("@SessionName", Session["SessionName"].ToString()),
            new SqlParameter("@BranchCode", Convert.ToInt32(Session["BranchCode"])),
            new SqlParameter("@fromDate", fromDate),
            new SqlParameter("@toDate", toDate),
            new SqlParameter("@Action", "Select")
            };
            DataSet ds = new DLL().Sp_SelectRecord_usingExecuteDataset("STudentBatchAllotmentProc", param);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    Grd.DataSource = dt;
                    Grd.DataBind();
                    SubmitdivMain.Visible = true;
                }
                else
                {
                    Grd.DataSource = null;
                    Grd.DataBind();
                    SubmitdivMain.Visible = false;
                }
            }
            else
            {
                Grd.DataSource = null;
                Grd.DataBind();
                SubmitdivMain.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Campus camp = new Campus();
            camp.msgbox(Page, msgbox, "Error: " + ex.Message, "E");
            SubmitdivMain.Visible = false;
        }
    }

    protected void DrpClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        string sqls = "select id, BranchName from BranchMaster ";
        sqls = sqls + "  where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and ClassId=" + DrpClass.SelectedValue + "";
        sqls = sqls + "  order by Id";
        oo.FillDropDown_withValue(sqls, drpBranch, "BranchName", "id");
        drpBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
        string sqlss = "select Id,SectionName from SectionMaster ";
        sqlss = sqlss + "  where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and ClassNameId=" + DrpClass.SelectedValue + "";
        sqlss = sqlss + "  order by Id";
        oo.FillDropDown_withValue(sqlss, ddlSubject, "SectionName", "Id");
        ddlSubject.Items.Insert(0, new ListItem("<--Select-->", ""));
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        //int flg = 0;
        //string message = "";
        //SqlCommand cmd = new SqlCommand
        //{
        //    CommandText = "STudentBatchAllotmentProc",
        //    CommandType = CommandType.StoredProcedure,
        //    Connection = con
        //};
        //cmd.Parameters.AddWithValue("@BatchID", Convert.ToInt32(DropDownList1.SelectedValue));
        //cmd.Parameters.AddWithValue("@ClassId", Convert.ToInt32(DrpClass.SelectedValue));
        //cmd.Parameters.AddWithValue("@StreamID", Convert.ToInt32(drpBranch.SelectedValue));
        //cmd.Parameters.AddWithValue("@SectionID", Convert.ToInt32(ddlSubject.SelectedValue));
        //cmd.Parameters.AddWithValue("@DateofAllotment", txtlesson.Text.Trim());
        //cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
        //cmd.Parameters.AddWithValue("@BranchCode", Convert.ToInt32(Session["BranchCode"]));
        //cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
        //cmd.Parameters.AddWithValue("@Action", "Insert");
        //try
        //{
        //    con.Open();
        //    object result = cmd.ExecuteScalar();
        //    con.Close();
        //    if (result != null)
        //    {
        //        message = result.ToString();
        //        flg = message == "Saved Successfully" ? 1 : 0;
        //    }

        //}
        //catch (Exception ex)
        //{
        //    con.Close();
        //    message = "Error: " + ex.Message;
        //}
        //if (flg > 0)
        //{
        //    LoadReport();
        //    Campus camp = new Campus();
        //    camp.msgbox(Page, msgbox, message, "S");
        //}
        //else
        //{
        //    Campus camp = new Campus();
        //    camp.msgbox(Page, msgbox, message, "E");
        //}


        int savedCount = 0;
        string message = "";

        foreach (GridViewRow row in Grd.Rows)
        {
            CheckBox chkRow = row.FindControl("chkRow") as CheckBox;
            Label lblAdmission = row.FindControl("Label6") as Label; // This contains SrNo or recordID

            if (chkRow != null && chkRow.Checked && lblAdmission != null)
            {
                SqlCommand cmd = new SqlCommand
                {
                    CommandText = "STudentBatchAllotmentProc",
                    CommandType = CommandType.StoredProcedure,
                    Connection = con
                };

                cmd.Parameters.AddWithValue("@SrNo", lblAdmission.Text);
                cmd.Parameters.AddWithValue("@BatchID", Convert.ToInt32(DropDownList1.SelectedValue));
                cmd.Parameters.AddWithValue("@DateofAllotment", txtlesson.Text.Trim());
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Convert.ToInt32(Session["BranchCode"]));
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                cmd.Parameters.AddWithValue("@Action", "Insert");

                try
                {
                    con.Open();
                    object result = cmd.ExecuteScalar();
                    con.Close();

                    if (result != null && result.ToString() == "Saved Successfully")
                    {
                        savedCount++;
                    }
                }
                catch (Exception ex)
                {
                    con.Close();
                    message = "Error: " + ex.Message;
                    break; // stop loop on error
                }
            }
        }

        if (savedCount > 0)
        {
            LoadReport(); // refresh data
            Campus camp = new Campus();
            camp.msgbox(Page, msgbox, savedCount + " record(s) saved successfully.", "S");
        }
        else
        {
            Campus camp = new Campus();
            camp.msgbox(Page, msgbox, string.IsNullOrEmpty(message) ? "No record saved." : message, "E");
        }

    }

    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("lblLessonID");
        lblValue.Text = lblId.Text;
        mpeDelete.Show();
    }

    protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadReport();
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        int flg = 0;
        string message = "";
        SqlCommand cmd = new SqlCommand
        {
            CommandText = "SyllabusMasterProc",
            CommandType = CommandType.StoredProcedure,
            Connection = con
        };
        cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(lblIDEdit.Text));
        cmd.Parameters.AddWithValue("@ClassId", Convert.ToInt32(ddlClassEdit.SelectedValue));
        cmd.Parameters.AddWithValue("@StreamID", Convert.ToInt32(ddlStreamEdit.SelectedValue));
        cmd.Parameters.AddWithValue("@SubjectID", Convert.ToInt32(ddlSubjectEdit.SelectedValue));
        cmd.Parameters.AddWithValue("@LessonName", txtlessonEdit.Text.Trim());
        cmd.Parameters.AddWithValue("@Discription", txtdiscriptionedit.Text.Trim());
        cmd.Parameters.AddWithValue("@DisplayofSequence", Convert.ToInt32(txtEditDisplayOrder.Text.Trim()));
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
        _sql = "Delete from STudentBatchAllotment_tb where ID=" + lblValue.Text + "  and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "'";

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


    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadReport();
    }

    protected void ddlSubject_SelectedIndexChanged1(object sender, EventArgs e)
    {

    }
    protected void DDYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(DDYear, DDMonth, DDDate);
    }
    protected void DDMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(DDYear, DDMonth, DDDate);
    }
    protected void DDDate_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    //protected void DDYear2_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    oo.YearDropDown(DDYear2, DDMonth2, DDDate2);
    //}
    //protected void DDMonth2_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    oo.MonthDropDown(DDYear2, DDMonth2, DDDate2);
    //}
    //protected void DDDate2_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //}

    protected void LinkButton1_Click1(object sender, EventArgs e)
    {
        LoadReport();
    }
}