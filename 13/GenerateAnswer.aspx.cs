using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.IO;

public partial class GenerateAnswer : Page
{
    private SqlConnection _con;
    private readonly Campus _oo;
    private string _sql, _sql1 = String.Empty;
    public GenerateAnswer()
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

            ddlSection.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlTerm.Items.Insert(0, new ListItem("<--Select-->", ""));

           
            
        }
    }
    protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        _sql = "select Id, Subject from OT_SubjectMaster where classid=" + ddlClass.SelectedValue + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        _oo.FillDropDown_withValue(_sql, ddlSubject, "Subject", "Id");
        ddlSubject.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    private void loadClass()
    {
        divAlterNate.Visible = false;
        _sql = "select Id, ClassName from ClassMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " order by CIDOrder";
        _oo.FillDropDown_withValue(_sql, ddlClass, "ClassName", "Id");
        ddlClass.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        divAlterNate.Visible = false;
        _sql = "select Id, Paper from OT_PaperMaster where SubjectId=" + ddlSubject.SelectedValue + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        _oo.FillDropDown_withValue(_sql, ddlPaper, "Paper", "Id");
        ddlPaper.Items.Insert(0, new ListItem("<--Select-->", ""));
        
        _sql = "select Id, ExamName from OT_ExamMaster where SubjectId=" + ddlSubject.SelectedValue + " and PaperId=" + ddlPaper.SelectedValue + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        _oo.FillDropDown_withValue(_sql, ddlExam, "ExamName", "Id");
        ddlExam.Items.Insert(0, new ListItem("<--Select-->", ""));
        
    }
    protected void ddlPaper_SelectedIndexChanged(object sender, EventArgs e)
    {
        divAlterNate.Visible = false;
        _sql = "select Id, TermName from OT_TermMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " order by id asc";
        _oo.FillDropDown_withValue(_sql, ddlTerm, "TermName", "Id");
        ddlTerm.Items.Insert(0, new ListItem("<--Select-->", ""));

       
        Grd.DataSource = null;
        Grd.DataBind();

    }
    protected void ddlTerm_SelectedIndexChanged(object sender, EventArgs e)
    {
        divAlterNate.Visible = false;
        _sql = "select Id, ExamName from OT_ExamMaster where SubjectId=" + ddlSubject.SelectedValue + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and PaperId=" + ddlPaper.SelectedValue + " and TermId=" + ddlTerm.SelectedValue + "";
        _oo.FillDropDown_withValue(_sql, ddlExam, "ExamName", "Id");
        ddlExam.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    protected void ddlExam_SelectedIndexChanged(object sender, EventArgs e)
    {
        divAlterNate.Visible = false;
        ddlSection.Items.Clear();
        _sql = "select Id, SigmentName from OT_SigmentMaster where ExamId=" + ddlExam.SelectedValue + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        _oo.FillDropDown_withValue(_sql, ddlSection, "SigmentName", "Id");
        ddlSection.Items.Insert(0, new ListItem("<--Select-->", ""));
        Grd.DataSource = null;
        Grd.DataBind();
        btnSubmit.Text = "Submit";
        btnSubmit.Visible = false;
    }
    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        _sql = "select NoOfQuestions, QuestionType, DeductionOn from OT_SigmentMaster where id=" + ddlSection.SelectedValue + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        txtNoOfQues.Text = _oo.ReturnTag(_sql, "NoOfQuestions");
        txtQtype.Text = _oo.ReturnTag(_sql, "QuestionType");
        txtMMarking.Text = _oo.ReturnTag(_sql, "DeductionOn");
        if (txtMMarking.Text.Trim() == "")
        {
            divNegMarking.Visible = false;
        }
        else
        {
            divNegMarking.Visible = true;
        }
        ddlOptiontype.SelectedIndex = 0;
        
        Grd.DataSource = null;
        Grd.DataBind();
        _sql = "select id from OT_AnswerMaster where SectionId=" + ddlSection.SelectedValue + " and examid="+ ddlExam.SelectedValue + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        if (_oo.Duplicate(_sql))
        {
            LoadData();
        }
        else
        {
            LoadBlankData();
        }
        if (ddlSection.SelectedIndex==0)
        {
            
            btnSubmit.Visible = false;
        }
        if (txtQtype.Text!="MCQs")
        {
            divAlterNate.Visible = false;
        }
        
    }
    protected void LoadBlankData()
    {
        if (txtQtype.Text == "MCQs" && txtMMarking.Text.Trim() == "")
        {
            divAlterNate.Visible = true;
        }
        else
        {
            divAlterNate.Visible = false;
        }
        if (ddlSection.SelectedIndex != 0)
        {
            divAlterNate.Visible = true;
        }
        else
        {
            divAlterNate.Visible = false;
        }
        _sql = "select filetype from OT_ExamMaster where id=" + ddlExam.SelectedValue + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        string filetype = _oo.ReturnTag(_sql, "filetype");
        int counts = int.Parse(txtNoOfQues.Text.Trim() == "" ? "0" : txtNoOfQues.Text.Trim());
        if (txtNoOfQues.Text.Trim() == "0")
        {
            _oo.msgbox(Page, msgbox2, "Please enter no of questions!", "A");
            return;
        }
        _sql = "select case when AllowMinusMarking is null or AllowMinusMarking=0 then 'No' else 'Yes' end AllowMinusMarking from OT_SigmentMaster where id=" + ddlSection.SelectedValue + " and examid=" + ddlExam.SelectedValue + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        string AllowMinusMarking = _oo.ReturnTag(_sql, "AllowMinusMarking");

        DataTable dt = new DataTable();
        dt.Columns.Add("Id", typeof(string));
        dt.Columns.Add("Questions", typeof(string));
        dt.Columns.Add("MaxMarks", typeof(string));
        dt.Columns.Add("Option1", typeof(string));
        dt.Columns.Add("Option2", typeof(string));
        dt.Columns.Add("Option3", typeof(string));
        dt.Columns.Add("Option4", typeof(string));
        dt.Columns.Add("Option5", typeof(string));
        dt.Columns.Add("RightOption", typeof(string));
        DataRow dr = null;
        for (int i = 0; i < counts; i++)
        {
            dr = dt.NewRow();
            dr["Id"] = "";
            if (filetype == "Manual")
            {
                dr["Questions"] = "";
            }
            else
            {
                dr["Questions"] = "Question";
            }
            dr["MaxMarks"] = "";
            dr["Option1"] = "A";
            dr["Option2"] = "B";
            dr["Option3"] = "C";
            dr["Option4"] = "D";
            dr["Option5"] = "";
            dr["RightOption"] = "";
            dt.Rows.Add(dr);
        }

        Grd.DataSource = dt;
        Grd.DataBind();
        if (txtQtype.Text== "Descriptive")
        {
            Grd.HeaderRow.Cells[3].Visible = false;
            Grd.HeaderRow.Cells[4].Visible = false;
            Grd.HeaderRow.Cells[5].Visible = false;
            Grd.HeaderRow.Cells[6].Visible = false;
            Grd.HeaderRow.Cells[7].Visible = false;
            Grd.HeaderRow.Cells[8].Visible = false;
        }
        for (int i = 0; i < Grd.Rows.Count; i++)
        {
            
            TextBox txtAnswer = (TextBox)Grd.Rows[i].FindControl("txtAnswer");
            TextBox txtmaxmarks = (TextBox)Grd.Rows[i].FindControl("txtmaxmarks");
            if (filetype == "Manual")
            {
                txtAnswer.Enabled = true;
            }
            else
            {
                txtAnswer.Enabled = false;
            }
            if (AllowMinusMarking == "Yes")
            {
                txtmaxmarks.Enabled = false;
            }
            else
            {
                txtmaxmarks.Enabled = true;
            }
            if (txtQtype.Text == "Descriptive")
            {
                Grd.Rows[i].Cells[3].Visible = false;
                Grd.Rows[i].Cells[4].Visible = false;
                Grd.Rows[i].Cells[5].Visible = false;
                Grd.Rows[i].Cells[6].Visible = false;
                Grd.Rows[i].Cells[7].Visible = false;
                Grd.Rows[i].Cells[8].Visible = false;
            }
        }
        
        btnSubmit.Visible = true;
        btnSubmit.Text = "Submit";
    }
    protected void LoadData()
    {
        if (txtQtype.Text == "MCQs" && txtMMarking.Text.Trim() == "")
        {
            divAlterNate.Visible = true;
        }
        else
        {
            divAlterNate.Visible = false;
        }
        if (ddlSection.SelectedIndex != 0)
        {
            divAlterNate.Visible = true;
        }
        else
        {
            divAlterNate.Visible = false;
        }
        _sql = "select filetype from OT_ExamMaster where id=" + ddlExam.SelectedValue + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        string filetype = _oo.ReturnTag(_sql, "filetype");
        _sql = "select case when AllowMinusMarking is null or AllowMinusMarking=0 then 'No' else 'Yes' end AllowMinusMarking from OT_SigmentMaster where id=" + ddlSection.SelectedValue + " and examid=" + ddlExam.SelectedValue + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        string AllowMinusMarking = _oo.ReturnTag(_sql, "AllowMinusMarking");

        _sql = "select count(*) as cnt from OT_AnswerMaster where SubjectId=" + ddlSubject.SelectedValue + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and PaperId=" + ddlPaper.SelectedValue + " and ExamId=" + ddlExam.SelectedValue + " and sectionid=" + ddlSection.SelectedValue + " ";
        string cnt = _oo.ReturnTag(_sql, "cnt");
        if (cnt != "0")
        {
            
            _sql = "select Id, Questions, Option1, Option2, Option3, Option4, Option5, Option6, MaxMarks, RightOption from OT_AnswerMaster where SubjectId=" + ddlSubject.SelectedValue + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and PaperId=" + ddlPaper.SelectedValue + " and ExamId=" + ddlExam.SelectedValue + " and sectionid=" + ddlSection.SelectedValue + "  order by id asc";
            Grd.DataSource = _oo.GridFill(_sql);
            Grd.DataBind();
            //Grd.HeaderRow.Cells[7].Visible = false;

            _sql = "select count(*) cnt from OT_ExamAnswerResult where ExamId=" + ddlExam.SelectedValue + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            if (txtQtype.Text == "Descriptive")
            {
                Grd.HeaderRow.Cells[3].Visible = false;
                Grd.HeaderRow.Cells[4].Visible = false;
                Grd.HeaderRow.Cells[5].Visible = false;
                Grd.HeaderRow.Cells[6].Visible = false;
                Grd.HeaderRow.Cells[7].Visible = false;
                Grd.HeaderRow.Cells[8].Visible = false;
            }
            for (int i = 0; i < Grd.Rows.Count; i++)
            {

                if (txtQtype.Text == "Descriptive")
                {
                    Grd.Rows[i].Cells[3].Visible = false;
                    Grd.Rows[i].Cells[4].Visible = false;
                    Grd.Rows[i].Cells[5].Visible = false;
                    Grd.Rows[i].Cells[6].Visible = false;
                    Grd.Rows[i].Cells[7].Visible = false;
                    Grd.Rows[i].Cells[8].Visible = false;
                }

                if (_oo.ReturnTag(_sql, "cnt") != "0")
                {
                    //LinkDeleteAll.Enabled = false;
                    //LinkDeleteAll.Text = "<i class='fa fa-lock'></i>";
                    //LinkDelete.Enabled = false;
                    //LinkDelete.Text = "<i class='fa fa-lock'></i>";
                    btnSubmit.Visible = false;
                    btnSubmit.Text = "Update";
                    //chkdelete.Visible = false;
                }
                else
                {
                    btnSubmit.Visible = true;
                    btnSubmit.Text = "Update";
                    //chkdelete.Visible = true;
                }

                TextBox txtAnswer = (TextBox)Grd.Rows[i].FindControl("txtAnswer");
                TextBox txtmaxmarks = (TextBox)Grd.Rows[i].FindControl("txtmaxmarks");
                if (filetype == "Manual")
                {
                    txtAnswer.Enabled = true;
                }
                else
                {
                    txtAnswer.Enabled = false;
                }
                if (AllowMinusMarking == "Yes")
                {
                    txtmaxmarks.Enabled = false;
                }
                else
                {
                    txtmaxmarks.Enabled = true;
                }

            }

        }
        else
        {
            Grd.DataSource = null;
            Grd.DataBind();
            btnSubmit.Text = "Submit";
            btnSubmit.Visible = false;
        }
    }
    protected void LinkDeleteAll_Click(object sender, EventArgs e)
    {
        bool sts = false;
        for (int i = 0; i < Grd.Rows.Count; i++)
        {
            CheckBox chkdelete = (CheckBox)Grd.Rows[i].FindControl("chkdelete");
            if (chkdelete.Checked)
            {
                sts = true;
            }
        }
        if (sts==false)
        {
            _oo.msgbox(Page, msgbox, "Please select atleast one item!", "A");
            return;
        }
        Buttonss.Focus();
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label38");
        var ss = lblId.Text;
        lblvalueall.Text = ss;
        Panel3_ModalPopupExtender.Show();
    }
    protected void Buttonss_Click(object sender, EventArgs e)
    {
    }
    protected void btnDeleteall_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < Grd.Rows.Count; i++)
        {
            CheckBox chkdelete = (CheckBox)Grd.Rows[i].FindControl("chkdelete");
            if (chkdelete.Checked)
            {
                Label id = (Label)Grd.Rows[i].FindControl("Label37");
                _sql = "Delete from OT_AnswerMaster where Id=" + id.Text + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";

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
                        
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
            
        }
        LoadData();
        _oo.msgbox(Page, msgbox, "Deleted successfully.", "S");
    }
    protected void LinkDelete_Click(object sender, EventArgs e)
    {
        Button8.Focus();
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label37");
        var ss = lblId.Text;
        lblvalue.Text = ss;
        Panel2_ModalPopupExtender.Show();
    }
    protected void Button8_Click(object sender, EventArgs e)
    {
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        _sql = "Delete from OT_AnswerMaster where Id=" + lblvalue.Text + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";

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
                LoadData();
                _oo.msgbox(Page, msgbox, "Deleted successfully.", "S");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        msgbox.InnerText = "";
        int NoOfOptions = 0;
        CheckBox chkH1 = (CheckBox)Grd.HeaderRow.FindControl("chkH1");
        CheckBox chkH2 = (CheckBox)Grd.HeaderRow.FindControl("chkH2");
        CheckBox chkH3 = (CheckBox)Grd.HeaderRow.FindControl("chkH3");
        CheckBox chkH4 = (CheckBox)Grd.HeaderRow.FindControl("chkH4");
        CheckBox chkH5 = (CheckBox)Grd.HeaderRow.FindControl("chkH5");
        if (chkH1.Checked)
        {
            NoOfOptions = NoOfOptions + 1;
        }
        if (chkH2.Checked)
        {
            NoOfOptions = NoOfOptions + 1;
        }
        if (chkH3.Checked)
        {
            NoOfOptions = NoOfOptions + 1;
        }
        if (chkH4.Checked)
        {
            NoOfOptions = NoOfOptions + 1;
        }
        if (chkH5.Checked)
        {
            NoOfOptions = NoOfOptions + 1;
        }
        if (NoOfOptions < 2)
        {
            _oo.msgbox(Page, msgbox2, "Please must be check atleast 2 options!", "A");
            return;
        }
        bool result = false;
        if (btnSubmit.Text.ToLower() == "submit")
        {

            for (int i = 0; i < Grd.Rows.Count; i++)
            {
                result = false;

                TextBox Option1 = (TextBox)Grd.Rows[i].FindControl("Option1");
                TextBox Option2 = (TextBox)Grd.Rows[i].FindControl("Option2");
                TextBox Option3 = (TextBox)Grd.Rows[i].FindControl("Option3");
                TextBox Option4 = (TextBox)Grd.Rows[i].FindControl("Option4");
                TextBox Option5 = (TextBox)Grd.Rows[i].FindControl("Option5");
                TextBox txtAnswer = (TextBox)Grd.Rows[i].FindControl("txtAnswer");
                TextBox RightOption = (TextBox)Grd.Rows[i].FindControl("RightOption");
                DropDownList ddlTypofOption = (DropDownList)Grd.Rows[i].FindControl("ddlTypofOption");
                TextBox txtmaxmarks = (TextBox)Grd.Rows[i].FindControl("txtmaxmarks");
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = "OT_AnswerMasterProc";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = _con;
                    cmd.Parameters.AddWithValue("@classId", ddlClass.SelectedValue);
                    cmd.Parameters.AddWithValue("@SubjectId", ddlSubject.SelectedValue);
                    cmd.Parameters.AddWithValue("@PaperId", ddlPaper.SelectedValue);
                    cmd.Parameters.AddWithValue("@ExamId", ddlExam.SelectedValue);
                    cmd.Parameters.AddWithValue("@SectionId", ddlSection.SelectedValue);
                    cmd.Parameters.AddWithValue("@NoOfQuestions", txtNoOfQues.Text.Trim());
                    cmd.Parameters.AddWithValue("@MaxMarks", txtmaxmarks.Text.Trim());

                    cmd.Parameters.AddWithValue("@Questions", txtAnswer.Text);
                    if (txtQtype.Text != "Descriptive")
                    {
                        if (chkH1.Checked)
                        {
                            cmd.Parameters.AddWithValue("@Option1", Option1.Text.ToUpper());
                        }
                        if (chkH2.Checked)
                        {
                            cmd.Parameters.AddWithValue("@Option2", Option2.Text.ToUpper());
                        }
                        if (chkH3.Checked)
                        {
                            cmd.Parameters.AddWithValue("@Option3", Option3.Text.ToUpper());
                        }
                        if (chkH4.Checked)
                        {
                            cmd.Parameters.AddWithValue("@Option4", Option4.Text.ToUpper());
                        }
                        if (chkH5.Checked)
                        {
                            cmd.Parameters.AddWithValue("@Option5", Option5.Text.ToUpper());
                        }
                        cmd.Parameters.AddWithValue("@RightOption", RightOption.Text);
                    }
                    cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                    cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                    cmd.Parameters.AddWithValue("@Loginname", Session["LoginName"].ToString());
                    cmd.Parameters.AddWithValue("@Action", "insert");
                    try
                    {
                        _con.Open();
                        cmd.ExecuteNonQuery();
                        result = true;
                        _con.Close();
                        cmd.Parameters.Clear();
                    }
                    catch (Exception ex)
                    {
                        _oo.msgbox(Page, msgbox2, ex.Message, "A");
                        return;
                    }
                }

            }
            if (result == true)
            {
                _oo.msgbox(Page, msgbox2, "Submitted successfully.", "S");
                LoadData();
            }
            
        }
        else
        {

            for (int i = 0; i < Grd.Rows.Count; i++)
            {
                result = false;
                Label id = (Label)Grd.Rows[i].FindControl("id");
                TextBox Option1 = (TextBox)Grd.Rows[i].FindControl("Option1");
                TextBox Option2 = (TextBox)Grd.Rows[i].FindControl("Option2");
                TextBox Option3 = (TextBox)Grd.Rows[i].FindControl("Option3");
                TextBox Option4 = (TextBox)Grd.Rows[i].FindControl("Option4");
                TextBox Option5 = (TextBox)Grd.Rows[i].FindControl("Option5");
                TextBox txtAnswer = (TextBox)Grd.Rows[i].FindControl("txtAnswer");
                TextBox RightOption = (TextBox)Grd.Rows[i].FindControl("RightOption");
                TextBox txtmaxmarks = (TextBox)Grd.Rows[i].FindControl("txtmaxmarks");
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = "OT_AnswerMasterProc";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = _con;
                    cmd.Parameters.AddWithValue("@id", id.Text);
                    cmd.Parameters.AddWithValue("@classId", ddlClass.SelectedValue);
                    cmd.Parameters.AddWithValue("@SubjectId", ddlSubject.SelectedValue);
                    cmd.Parameters.AddWithValue("@PaperId", ddlPaper.SelectedValue);
                    cmd.Parameters.AddWithValue("@ExamId", ddlExam.SelectedValue);
                    cmd.Parameters.AddWithValue("@SectionId", ddlSection.SelectedValue);
                    cmd.Parameters.AddWithValue("@NoOfQuestions", txtNoOfQues.Text.Trim());
                    cmd.Parameters.AddWithValue("@MaxMarks", txtmaxmarks.Text.Trim());

                    cmd.Parameters.AddWithValue("@Questions", txtAnswer.Text);
                    if (txtQtype.Text != "Descriptive")
                    {
                        if (chkH1.Checked)
                        {
                            cmd.Parameters.AddWithValue("@Option1", Option1.Text);
                        }
                        if (chkH2.Checked)
                        {
                            cmd.Parameters.AddWithValue("@Option2", Option2.Text);
                        }
                        if (chkH3.Checked)
                        {
                            cmd.Parameters.AddWithValue("@Option3", Option3.Text);
                        }
                        if (chkH4.Checked)
                        {
                            cmd.Parameters.AddWithValue("@Option4", Option4.Text);
                        }
                        if (chkH5.Checked)
                        {
                            cmd.Parameters.AddWithValue("@Option5", Option5.Text);
                        }
                        cmd.Parameters.AddWithValue("@RightOption", RightOption.Text);
                    }
                    cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                    cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                    cmd.Parameters.AddWithValue("@Loginname", Session["LoginName"].ToString());
                    cmd.Parameters.AddWithValue("@Action", "update");
                    try
                    {
                        _con.Open();
                        cmd.ExecuteNonQuery();
                        result = true;
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
            if (result == true)
            {
                _oo.msgbox(Page, msgbox2, "Updated successfully.", "S");
                LoadData();
            }
        }
    }
    
    public override void Dispose()
    {
        _con.Dispose();
        _oo.Dispose();
    }





    
}