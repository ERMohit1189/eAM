using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI;

public partial class SegmentMaster : Page
{
    private SqlConnection _con;
    private readonly Campus _oo;
    private string _sql;
    public SegmentMaster()
    {
        _con = new SqlConnection();
        _oo = new Campus();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Session["LoginName"] as string))
        {
            Response.Redirect("~/default.aspx");
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
            ddlSubject.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlPaper.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlExam.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlTerm.Items.Insert(0, new ListItem("<--Select-->", ""));
            txtSection.Text = "";
        }
    }
    protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        _sql = "select Id, Subject from OT_SubjectMaster where classid=" + ddlClass.SelectedValue + " AND SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        _oo.FillDropDown_withValue(_sql, ddlSubject, "Subject", "Id");
        ddlSubject.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    private void loadClass()
    {
        _sql = "select Id, ClassName from ClassMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " order by CIDOrder";
        _oo.FillDropDown_withValue(_sql, ddlClass, "ClassName", "Id");
        ddlClass.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        Grd.DataSource = null;
        Grd.DataBind();
        txtSection.Text = "";
        _sql = "select Id, Paper from OT_PaperMaster where SubjectId=" + ddlSubject.SelectedValue + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        _oo.FillDropDown_withValue(_sql, ddlPaper, "Paper", "Id");
        ddlPaper.Items.Insert(0, new ListItem("<--Select-->", ""));
        
    }
    protected void ddlPaper_SelectedIndexChanged(object sender, EventArgs e)
    {
        Grd.DataSource = null;
        Grd.DataBind();
        txtSection.Text = "";
        _sql = "select Id, TermName from OT_TermMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " order by id asc";
        _oo.FillDropDown_withValue(_sql, ddlTerm, "TermName", "Id");
        ddlTerm.Items.Insert(0, new ListItem("<--Select-->", ""));

    }
    protected void ddlTerm_SelectedIndexChanged(object sender, EventArgs e)
    {

        _sql = "select Id, ExamName from OT_ExamMaster where SubjectId=" + ddlSubject.SelectedValue + " and PaperId=" + ddlPaper.SelectedValue + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and TermId=" + ddlTerm.SelectedValue + "";
        _oo.FillDropDown_withValue(_sql, ddlExam, "ExamName", "Id");
        ddlExam.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    protected void ddlExam_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadData();
    }
    private void LoadData()
    {
        Grd.DataSource = null;
        Grd.DataBind();
        _sql = "select os.id, cm.classname, pm.id as paperid, sm.id as subjectid, ExamId, sm.Subject, pm.Paper, ExamName, SigmentName, QuestionType, AllowMinusMarking,case when AllowMinusMarking=1 then convert(varchar, DeductionOn) else 'Not Applied' end DeductionOn, NoOfQuestions from OT_SigmentMaster os inner join OT_ExamMaster oe on oe.id = os.ExamId inner join classMaster cm on cm.id=os.classid inner join OT_SubjectMaster sm on sm.id = oe.SubjectId inner join OT_PaperMaster pm on pm.id = oe.PaperId ";
        _sql = _sql + " where ExamId=" + ddlExam.SelectedValue + " and  os.SessionName='" + Session["SessionName"] + "' and os.BranchCode=" + Session["BranchCode"] + "  and  oe.SessionName='" + Session["SessionName"] + "' and oe.BranchCode=" + Session["BranchCode"] + " and  pm.SessionName='" + Session["SessionName"] + "' and pm.BranchCode=" + Session["BranchCode"] + "  and  cm.SessionName='" + Session["SessionName"] + "' and cm.BranchCode=" + Session["BranchCode"] + " and  sm.SessionName='" + Session["SessionName"] + "' and sm.BranchCode=" + Session["BranchCode"] + " and pm.id=" + ddlPaper.SelectedValue + " and sm.id=" + ddlSubject.SelectedValue + "  order by ExamId asc, PaperId asc";
        Grd.DataSource = _oo.GridFill(_sql);
        Grd.DataBind();
        for (int i = 0; i < Grd.Rows.Count; i++)
        {
            Label deleteid = (Label)Grd.Rows[i].FindControl("deleteid");
            LinkButton btnEdit = (LinkButton)Grd.Rows[i].FindControl("btnEdit");
            LinkButton delete = (LinkButton)Grd.Rows[i].FindControl("btnDelete");
            string _sql2 = "select count(*) cnt from OT_ExamAnswerResult where ExamId=" + ddlExam.SelectedValue + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            if (_oo.ReturnTag(_sql2, "cnt") != "0")
            {
                btnEdit.Enabled = false;
                btnEdit.Text = "<i class='fa fa-lock'></i>";
                delete.Enabled = false;
                delete.Text = "<i class='fa fa-lock'></i>";
            }
            else
            {
                _sql = "select count(*) cnt from OT_AnswerMaster where SectionId=" + deleteid.Text + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                if (_oo.ReturnTag(_sql, "cnt") != "0")
                {
                    btnEdit.Enabled = false;
                    btnEdit.Text = "<i class='fa fa-lock'></i>";
                    delete.Enabled = false;
                    delete.Text = "<i class='fa fa-lock'></i>";
                }
            }
        }
    }
    
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        Button8.Focus();
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("deleteid");
        var ss = lblId.Text;
        lblvalue.Text = ss;
        Panel2_ModalPopupExtender.Show();
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("editid");
        string ss = lblId.Text;
        lblID.Text = ss;
        _sql = "select * from OT_SigmentMaster where id="+ ss + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        txtSectionPanel.Text = _oo.ReturnTag(_sql, "SigmentName");
        hdnSigName.Value = _oo.ReturnTag(_sql, "SigmentName");
        ddlQuestionTypePanel.SelectedValue = _oo.ReturnTag(_sql, "QuestionType");
        txtNoofQuestionsPanel.Text = _oo.ReturnTag(_sql, "NoOfQuestions");
        if (_oo.ReturnTag(_sql, "AllowMinusMarking") == "" || _oo.ReturnTag(_sql, "AllowMinusMarking") == "False" || _oo.ReturnTag(_sql, "AllowMinusMarking") == "0")
        {
            chkAllowMinusMarkingPanel.Checked = false;
            divWrongQuesPanel.Visible = false;
            ddlDeductionPanel.SelectedIndex = 0;
        }
        else
        {
            chkAllowMinusMarkingPanel.Checked = true;
            divWrongQuesPanel.Visible = true;
            ddlDeductionPanel.SelectedValue = _oo.ReturnTag(_sql, "DeductionOn");

        }
        if (_oo.ReturnTag(_sql, "QuestionType") != "MCQs")
        {
            divMinusMarkingPanel.Visible = false;
        }
        else
        {
            divMinusMarkingPanel.Visible = true;
        }
        Panel1_ModalPopupExtender.Show();
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        _sql = "select T1.SigmentName from (select SigmentName from OT_SigmentMaster where SigmentName<>'" + hdnSigName.Value + "' and ExamId=" + ddlExam.SelectedValue + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + ")T1 where T1.SigmentName='" + txtSectionPanel.Text.Trim() + "'";
        if (_oo.Duplicate(_sql))
        {
                _oo.msgbox(Page, msgbox, "Duplicate entry!", "A");
        }
        else
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "OT_SigmentMasterProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", lblID.Text);
                cmd.Parameters.AddWithValue("@SigmentName", txtSectionPanel.Text.Trim());
                cmd.Parameters.AddWithValue("@QuestionType", ddlQuestionType.SelectedValue);
                if (ddlQuestionTypePanel.SelectedValue != "Descriptive")
                {
                    if (chkAllowMinusMarkingPanel.Checked)
                    {
                        cmd.Parameters.AddWithValue("@AllowMinusMarking", (chkAllowMinusMarkingPanel.Checked ? "1" : "0"));
                        cmd.Parameters.AddWithValue("@DeductionOn", ddlDeductionPanel.SelectedValue);
                    }
                }
                cmd.Parameters.AddWithValue("@NoOfQuestions", txtNoofQuestionsPanel.Text.Trim());
                cmd.Parameters.AddWithValue("@Loginname", Session["LoginName"].ToString());
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@Action", "update");
                cmd.Connection = _con;
                try
                {
                    _con.Open();
                    cmd.ExecuteNonQuery();
                    _con.Close();
                    _oo.msgbox(Page, msgbox, "Updated successfully.", "S");
                    LoadData();
                    Panel1_ModalPopupExtender.Hide();
                }
                catch (Exception)
                {
                }
            }
        }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
    }
    protected void btnDeleteYes_Click(object sender, EventArgs e)
    {
        _sql = "Delete from OT_SigmentMaster where Id=" + lblvalue.Text+ " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";

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
                _oo.msgbox(Page, msgbox, "Deleted successfully.", "S");
                LoadData();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    protected void Button8_Click(object sender, EventArgs e)
    {
    }
    protected void LinkSubmit_Click(object sender, EventArgs e)
    {

        _sql = "select count(*) cnt from OT_ExamAnswerResult where ExamId=" + ddlExam.SelectedValue + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        if (_oo.ReturnTag(_sql, "cnt") != "0")
        {
            _oo.msgbox(Page, msgbox, "This Test has been attempted, new segment could not be created!", "A");
        }
        else
        {
            _sql = "select SigmentName from OT_SigmentMaster where SigmentName='" + txtSection.Text.Trim() + "' and ExamId=" + ddlExam.SelectedValue + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            if (_oo.Duplicate(_sql))
            {
                _oo.msgbox(Page, msgbox, "Duplicate entry!", "A");
            }
            else
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "OT_SigmentMasterProc";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = _con;
                    cmd.Parameters.AddWithValue("@ClassId", ddlClass.SelectedValue);
                    cmd.Parameters.AddWithValue("@SubjectId", ddlSubject.SelectedValue);
                    cmd.Parameters.AddWithValue("@PaperId", ddlPaper.SelectedValue);
                    cmd.Parameters.AddWithValue("@TermId", ddlTerm.SelectedValue);
                    cmd.Parameters.AddWithValue("@ExamId", ddlExam.SelectedValue);
                    cmd.Parameters.AddWithValue("@SigmentName", txtSection.Text.Trim());
                    cmd.Parameters.AddWithValue("@QuestionType", ddlQuestionType.SelectedValue);
                    if (ddlQuestionType.SelectedValue != "Descriptive")
                    {
                        if (chkMinusMarking.Checked)
                        {
                            cmd.Parameters.AddWithValue("@AllowMinusMarking", (chkMinusMarking.Checked ? "1" : "0"));
                            cmd.Parameters.AddWithValue("@DeductionOn", ddlWrongQues.SelectedValue);
                        }
                    }
                    cmd.Parameters.AddWithValue("@NoOfQuestions", txtNoofQuestions.Text.Trim());
                    cmd.Parameters.AddWithValue("@Loginname", Session["LoginName"].ToString());
                    cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                    cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                    cmd.Parameters.AddWithValue("@Action", "insert");
                    try
                    {
                        _con.Open();
                        cmd.ExecuteNonQuery();
                        _con.Close();
                        _oo.msgbox(Page, msgbox, "Submitted successfully.", "S");
                        divWrongQues.Visible = false;
                        txtNoofQuestions.Text = "";
                        txtSection.Text = "";
                        LoadData();
                    }
                    catch (Exception ex)
                    {
                        // ignored
                    }
                }
            }
        }
    }
    protected void ddlQuestionType_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkMinusMarking.Checked = false;
        divWrongQues.Visible = false;
        if (ddlQuestionType.SelectedIndex == 0)
        {
            divMinusMarking.Visible = true;
        }
        else
        {
            divMinusMarking.Visible = false;
        }
    }
    protected void chkMinusMarking_CheckedChanged(object sender, EventArgs e)
    {
        if (chkMinusMarking.Checked)
        {
            divWrongQues.Visible = true;
        }
        else
        {
            divWrongQues.Visible = false;
        }
    }
    protected void ddlQuestionTypePanel_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkAllowMinusMarkingPanel.Checked = false;
        divWrongQuesPanel.Visible = false;
        if (ddlQuestionTypePanel.SelectedIndex == 0)
        {
            divMinusMarkingPanel.Visible = true;
        }
        else
        {
            divMinusMarkingPanel.Visible = false;
        }
        Panel1_ModalPopupExtender.Show();
    }
    protected void chkAllowMinusMarkingPanel_CheckedChanged(object sender, EventArgs e)
    {
        if (chkAllowMinusMarkingPanel.Checked)
        {
            divWrongQuesPanel.Visible = true;
        }
        else
        {
            divWrongQuesPanel.Visible = false;
        }
        Panel1_ModalPopupExtender.Show();
    }
    public override void Dispose()
    {
        _con.Dispose();
        _oo.Dispose();
    }
}