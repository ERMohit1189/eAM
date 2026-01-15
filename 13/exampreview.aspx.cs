using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.IO;

public partial class exampreview : Page
{
    private SqlConnection _con;
    private readonly Campus _oo;
    private string _sql, _sql1 = String.Empty;
    string ExamID = "";
    string DescriptiveExists = "";
    public exampreview()
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
        if (Session["Logintype"].ToString().ToLower() == "admin")
        {
            Response.Redirect("~/default.aspx");
        }
        _con = _oo.dbGet_connection();
        _oo.LoadLoader(loader);
        if (!IsPostBack)
        {
            ExamID = Request.QueryString["p"].ToString();

            string ss = "select QuestionType from OT_SigmentMaster where QuestionType='Descriptive' and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and ExamId=" + ExamID + "";

            DescriptiveExists = (_oo.Duplicate(ss)==true ? "True" : "False").ToString();
            hdnDescriptiveExists.Value = DescriptiveExists.ToString();
            _sql = "select name, srno from AllStudentRecord_UDF('"+ Session["SessionName"] + "', " + Session["BranchCode"] + ") where SrNo='" + Session["LoginName"].ToString()+"'";
            StuName.Text = _oo.ReturnTag(_sql, "name");
            rollNo.Text = _oo.ReturnTag(_sql, "srno");

            hdnExamID.Value = ExamID;
            string sq = "select ExamId from OT_ExamAnswerResult where ExamId=" + ExamID + " and isnull(AnswerStatus, '')='' and QuestionType='Descriptive' and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and ExamId=" + ExamID + "";
            if (_oo.Duplicate(sq))
            {
                hdnIsExamTested.Value = "False";
            }
            else
            {
                hdnIsExamTested.Value = "True";
            }

            _sql = "select ExamName, ResultStting from OT_ExamMaster where GETDATE() between ExamStart and ExamEnd and id=" + ExamID + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            string ResultStting = _oo.ReturnTag(_sql, "ResultStting");
            hdnResultStting.Value = ResultStting;
            if (!_oo.Duplicate(_sql))
            {
                _sql = "select Status from OT_ExamAllotment where SrNo='" + Session["LoginName"].ToString() + "' and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and ExamId=" + ExamID + "";
                if (_oo.ReturnTag(_sql, "Status").ToString() == "True" && ResultStting == "True")
                {
                    Response.Redirect("result.aspx?p=" + ExamID);
                }
                else
                {
                    Response.Redirect("studashboard.aspx?ExamID=" + ExamID);
                }
            }

            _sql = "select filepath, fileType from OT_ExamMaster where id=" + ExamID + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            string fileType = _oo.ReturnTag(_sql, "fileType");
            string filepath = _oo.ReturnTag(_sql, "filepath");
            if (filepath != "")
            {
                if (fileType == "FileUpload")
                {
                    string data = filepath.Replace("../", "~/");
                    string[] str = data.Split(new string[] { "." }, StringSplitOptions.None);
                    if (str[1].ToLower() == "pdf")
                    {
                        string strs = "<iframe id='fraDisabled' src='" + filepath + "#&embedded=true&toolbar=0&navpanes=0&view=FitH' style='width:100%; height:700px;'></iframe>";
                        ltEmbed.InnerHtml = strs;
                        ltEmbed.Visible = true;
                        imgExam.Visible = false;
                    }
                    else
                    {
                        ltEmbed.Visible = false;
                        imgExam.Visible = true;
                        imgExam.ImageUrl = data;
                    }
                }
                
                else
                {
                    string strs = "<iframe id='fraDisabled' src='" + filepath + "' style='width:100%; height:700px;'></iframe>";
                    strs = strs + "<div style='width: 80px; height: 80px; position: absolute; opacity: 0; right: 0px; top: 0px; '></div>";
                    ltEmbed.InnerHtml = strs;
                    ltEmbed.Visible = true;
                    imgExam.Visible = false;
                }

            }
            else if (fileType == "Manual")
            {
                divPdf.Visible = false;
                tblAppend.Attributes.Add("class", "col-sm-12");
            }
            else
            {
                ltEmbed.Visible = false;
                imgExam.Visible = false;
            }
            
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        _sql = "select ExamID from OT_ExamAnswerResult where SrNO='" + Session["LoginName"].ToString() + "' and ExamID=" + Request.QueryString["p"].ToString() + "  and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        if (_oo.Duplicate(_sql))
        {
            Panel2_ModalPopupExtender.Show();
        }
        else
        {
            _oo.msgbox(Page, msgbox, "Plaese answer the questions!", "A");
        }
    }
    protected void btnFinalSubmit_Click(object sender, EventArgs e)
    {
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "update OT_ExamAllotment set Status=1,  Duration=0 where ExamID=" + Request.QueryString["p"].ToString() + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and SRNo='" + Session["LoginName"].ToString() + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = _con;
            try
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
                _sql = "select ExamName, ResultStting from OT_ExamMaster where GETDATE() between ExamStart and ExamEnd and id=" + Request.QueryString["p"].ToString() + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                string ResultStting = _oo.ReturnTag(_sql, "ResultStting");
                if (_oo.Duplicate(_sql))
                {
                    _sql = "select Status from OT_ExamAllotment where SrNo='" + Session["LoginName"].ToString() + "' and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and ExamId=" + Request.QueryString["p"].ToString() + "";
                    
                    if (ResultStting == "True" && hdnDescriptiveExists.Value == "False" && hdnIsExamTested.Value=="True")
                    {
                        Response.Redirect("result.aspx?p=" + Request.QueryString["p"].ToString());
                    }
                    else
                    {
                        Response.Redirect("studashboard.aspx?ExamID=" + Request.QueryString["p"].ToString());
                    }
                }
                else
                {
                    Response.Redirect("studashboard.aspx?ExamID=" + Request.QueryString["p"].ToString());
                }
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
    public override void Dispose()
    {
        _con.Dispose();
        _oo.Dispose();
    }


    
}