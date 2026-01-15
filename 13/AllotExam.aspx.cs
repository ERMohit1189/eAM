using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.IO;

public partial class AllotExam : Page
{
    private SqlConnection _con;
    private readonly Campus _oo;
    private string _sql, _sql1 = String.Empty;
    public AllotExam()
    {
        _con = new SqlConnection();
        _oo = new Campus();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Session["LoginName"] as string))
        {
            Response.Redirect("default.aspx");
        }
        if (Session["Logintype"].ToString().ToLower() != "admin")
        {
            Response.Redirect("~/default.aspx");
        }
        _con = _oo.dbGet_connection();
        _oo.LoadLoader(loader); 

        if (!IsPostBack)
        {
            loadClass();
            ddlMedium.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlStream.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlSection.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlgroup.Items.Insert(0, new ListItem("<--Select-->", ""));

            ddlSubject.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlPaper.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlExam.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
    }
    private void loadClass()
    {
        _sql = "select Id, ClassName from ClassMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " order by CIDOrder";
        _oo.FillDropDown_withValue(_sql, ddlClass, "ClassName", "Id");
        ddlClass.Items.Insert(0, new ListItem("<--Select-->", ""));
    }

    private void loadStream()
    {
        _sql = "select Id, BranchName from BranchMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and Classid="+ ddlClass.SelectedValue + "";
        _oo.FillDropDown_withValue(_sql, ddlStream, "BranchName", "Id");
        ddlStream.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    private void loadSection()
    {
        _sql = "select Id, SectionName from SectionMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and ClassNameId=" + ddlClass.SelectedValue + "";
        _oo.FillDropDown_withValue(_sql, ddlSection, "SectionName", "Id");
        ddlSection.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    private void loadGroup()
    {
        _sql = "select Id, Stream from StreamMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and Classid=" + ddlClass.SelectedValue + "  and Branchid=" + ddlStream.SelectedValue + "";
        _oo.FillDropDown_withValue(_sql, ddlgroup, "Stream", "Id");
        ddlgroup.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        _sql = "select Id, Medium from MediumMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        _oo.FillDropDown_withValue(_sql, ddlMedium, "Medium", "Id");
        ddlMedium.Items.Insert(0, new ListItem("<--Select-->", ""));

        _sql = "select Id, Subject from OT_SubjectMaster where classId="+ ddlClass.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        _oo.FillDropDown_withValue(_sql, ddlSubject, "Subject", "Id");
        ddlSubject.Items.Insert(0, new ListItem("<--Select-->", ""));
        
    }
    protected void ddlMedium_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadSection();
    }
    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadStream();
    }
    protected void ddlStream_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadGroup();
    }
    protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        _sql = "select Id, Paper from OT_PaperMaster where SubjectId=" + ddlSubject.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        _oo.FillDropDown_withValue(_sql, ddlPaper, "Paper", "Id");
        ddlPaper.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    protected void ddlPaper_SelectedIndexChanged(object sender, EventArgs e)
    {
        _sql = "select Id, TermName from OT_TermMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " order by id asc";
        _oo.FillDropDown_withValue(_sql, ddlTerm, "TermName", "Id");
        ddlTerm.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    protected void ddlTerm_SelectedIndexChanged(object sender, EventArgs e)
    {
        _sql = "select Id, ExamName from OT_ExamMaster where SubjectId=" + ddlSubject.SelectedValue + " and PaperId=" + ddlPaper.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and TermId=" + ddlTerm.SelectedValue + "";
        _oo.FillDropDown_withValue(_sql, ddlExam, "ExamName", "Id");
        ddlExam.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        Grd.DataSource = null;
        Grd.DataBind();
        _sql = "select Id from OT_SigmentMaster where ExamId=" + ddlExam.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        string _sql2 = "select Id from OT_AnswerMaster where ExamId=" + ddlExam.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        if (!_oo.Duplicate(_sql))
        {
            _oo.msgbox(Page, msgbox, "Please create segment for the test.", "A");
            return;
        }
        if (!_oo.Duplicate(_sql2))
        {
            _oo.msgbox(Page, msgbox, "Please generate answers for the test.", "A");
            return;
        }
        else
        {
            LoadData();

        }
    }
    protected void LoadData()
    {
        Grd.DataSource =null;
        Grd.DataBind();
        _sql = "Select SRNo, Name, FatherContactNo MobileNo, FatherName, DOB DateofBirth, CombineClassName  from AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "','" + Session["BranchCode"].ToString() + "') where Withdrwal is null and isnull(blocked, '')='' ";
        _sql = _sql + "  and SRNo not in (select SRNo from OT_ExamAllotment where SubjectId=" + ddlSubject.SelectedValue + " and PaperId=" + ddlPaper.SelectedValue + " and ExamID=" + ddlExam.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + ") and classid=" + ddlClass.SelectedValue+"";
        if (ddlStream.SelectedValue!="")
        {
            _sql = _sql + " and BranchId=" + ddlStream.SelectedValue + "";
        }
        if (ddlSection.SelectedValue != "")
        {
            _sql = _sql + " and SectionID=" + ddlSection.SelectedValue + "";
        }
        if (ddlgroup.SelectedValue != "")
        {
            _sql = _sql + " and isnull(StreamId, '')=" + ddlgroup.SelectedValue + "";
        }
        Grd.DataSource = _oo.GridFill(_sql);
        Grd.DataBind();
        if (Grd.Rows.Count > 0)
        {
            btnSubmit.Visible = true;
        }
        else
        {
            btnSubmit.Visible = false;
            _oo.msgbox(Page, msgbox, "Record(s) not found!", "A");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
       
        for (int i = 0; i < Grd.Rows.Count; i++)
        {
            Label SRNo = (Label)Grd.Rows[i].FindControl("SRNo");
            
            CheckBox chk = (CheckBox)Grd.Rows[i].FindControl("chk");
            if (chk.Checked)
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "OT_ExamAllotmentProc";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = _con;
                    cmd.Parameters.AddWithValue("@SRNo", SRNo.Text.Trim());
                    cmd.Parameters.AddWithValue("@Classid", ddlClass.SelectedValue);
                    cmd.Parameters.AddWithValue("@SubjectId", ddlSubject.SelectedValue);
                    cmd.Parameters.AddWithValue("@PaperId", ddlPaper.SelectedValue);
                    cmd.Parameters.AddWithValue("@ExamID", ddlExam.SelectedValue);
                    cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                    cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                    cmd.Parameters.AddWithValue("@Loginname", Session["LoginName"].ToString());
                    cmd.Parameters.AddWithValue("@Action", "insert");
                    try
                    {
                        _con.Open();
                        cmd.ExecuteNonQuery();
                        _con.Close();
                        cmd.Parameters.Clear();
                    }
                    catch (Exception ex)
                    {
                        _oo.msgbox(Page, msgbox, ex.Message, "A");
                        return;
                    }
                }
            }
        }
        _oo.msgbox(Page, msgbox, "Submitted successfully.", "S");
        LoadData();
    }
    public override void Dispose()
    {
        _con.Dispose();
        _oo.Dispose();
    }

    

    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkAll = (CheckBox)Grd.HeaderRow.FindControl("chkAll");
        if (chkAll.Checked)
        {
            for (int i = 0; i < Grd.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)Grd.Rows[i].FindControl("chk");
                chk.Checked = true;
            }
        }
        else
        {
            for (int i = 0; i < Grd.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)Grd.Rows[i].FindControl("chk");
                chk.Checked = false;
            }
        }
    }

    protected void chk_CheckedChanged(object sender, EventArgs e)
    {
        int cnt = 0;
        CheckBox chkAll = (CheckBox)Grd.HeaderRow.FindControl("chkAll");
        for (int i = 0; i < Grd.Rows.Count; i++)
        {
            CheckBox chk = (CheckBox)Grd.Rows[i].FindControl("chk");
            if (chk.Checked)
            {
                cnt = cnt + 1;
            }
        }
        if (cnt == Grd.Rows.Count)
        {
            chkAll.Checked = true;
        }
        else
        {
            chkAll.Checked = false;
        }
    }
    

    protected void ExamValidFromH_TextChanged(object sender, EventArgs e)
    {
        TextBox ExamValidFromH = (TextBox)Grd.HeaderRow.FindControl("ExamValidFromH");
        for (int i = 0; i < Grd.Rows.Count; i++)
        {
            TextBox ExamValidFrom = (TextBox)Grd.Rows[i].FindControl("ExamValidFrom");
            ExamValidFrom.Text = ExamValidFromH.Text;
        }
    }

    protected void ExamValidToH_TextChanged(object sender, EventArgs e)
    {
        TextBox ExamValidToH = (TextBox)Grd.HeaderRow.FindControl("ExamValidToH");
        for (int i = 0; i < Grd.Rows.Count; i++)
        {
            TextBox ExamValidTo = (TextBox)Grd.Rows[i].FindControl("ExamValidTo");
            ExamValidTo.Text = ExamValidToH.Text;
        }
    }

    protected void ddlFromHourFromH_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlFromHourFromH = (DropDownList)Grd.HeaderRow.FindControl("ddlFromHourFromH");
        for (int i = 0; i < Grd.Rows.Count; i++)
        {
            DropDownList ddlFromHourFrom = (DropDownList)Grd.Rows[i].FindControl("ddlFromHourFrom");
            ddlFromHourFrom.SelectedItem.Text = ddlFromHourFromH.SelectedItem.Text;
        }

    }

    protected void ddlFromMinuteFromH_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlFromMinuteFromH = (DropDownList)Grd.HeaderRow.FindControl("ddlFromMinuteFromH");
        for (int i = 0; i < Grd.Rows.Count; i++)
        {
            DropDownList ddlFromMinuteFrom = (DropDownList)Grd.Rows[i].FindControl("ddlFromMinuteFrom");
            ddlFromMinuteFrom.SelectedItem.Text = ddlFromMinuteFromH.SelectedItem.Text;
        }
    }
    protected void ddlFromTypeFromH_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlFromTypeFromH = (DropDownList)Grd.HeaderRow.FindControl("ddlFromTypeFromH");
        for (int i = 0; i < Grd.Rows.Count; i++)
        {
            DropDownList ddlFromTypeFrom = (DropDownList)Grd.Rows[i].FindControl("ddlFromTypeFrom");
            ddlFromTypeFrom.SelectedItem.Text = ddlFromTypeFromH.SelectedItem.Text;
        }
    }


    protected void ddlFromHourToH_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlFromHourToH = (DropDownList)Grd.HeaderRow.FindControl("ddlFromHourToH");
        for (int i = 0; i < Grd.Rows.Count; i++)
        {
            DropDownList ddlFromHourTo = (DropDownList)Grd.Rows[i].FindControl("ddlFromHourTo");
            ddlFromHourTo.SelectedItem.Text = ddlFromHourToH.SelectedItem.Text;
        }

    }
    protected void ddlFromMinuteToH_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlFromMinuteToH = (DropDownList)Grd.HeaderRow.FindControl("ddlFromMinuteToH");
        for (int i = 0; i < Grd.Rows.Count; i++)
        {
            DropDownList ddlFromMinuteTo = (DropDownList)Grd.Rows[i].FindControl("ddlFromMinuteTo");
            ddlFromMinuteTo.SelectedItem.Text = ddlFromMinuteToH.SelectedItem.Text;
        }
    }

    protected void ddlFromTypeToH_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlFromTypeToH = (DropDownList)Grd.HeaderRow.FindControl("ddlFromTypeToH");
        for (int i = 0; i < Grd.Rows.Count; i++)
        {
            DropDownList ddlFromTypeTo = (DropDownList)Grd.Rows[i].FindControl("ddlFromTypeTo");
            ddlFromTypeTo.SelectedItem.Text = ddlFromTypeToH.SelectedItem.Text;
        }
    }

    
}