using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.IO;

public partial class ResultAdmin : Page
{
    private SqlConnection _con;
    private readonly Campus _oo;
    private string _sql, _sql1 = String.Empty;
    string ExamID = "";
    public ResultAdmin()
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
        if (string.IsNullOrEmpty(Session["SRNo"] as string))
        {
            Response.Redirect("~/default.aspx");
        }
        _con = _oo.dbGet_connection();
        _oo.LoadLoader(loader);

        if (!IsPostBack)
        {
            ExamID = Request.QueryString["p"].ToString();

            try
            {
                if (Request.QueryString.Keys.Count > 0)
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "update OT_ExamAllotment set Status=1, Duration=0 where ExamID=" + ExamID + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and SRNo='" + Session["SRNo"].ToString() + "'";
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
                        }
                    }
                }
                _sql = "select name, srno from AllStudentRecord_UDF('" + Session["SessionName"] + "', " + Session["BranchCode"] + ") where SrNo='" + Session["SRNo"].ToString() + "'";
                StuName.Text = _oo.ReturnTag(_sql, "name");
                rollNo.Text = _oo.ReturnTag(_sql, "srno");

                _sql = "select status from OT_ExamAllotment where SRNo = '" + rollNo.Text + "' and examid=" + ExamID + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and status=1";
                if (_oo.Duplicate(_sql))
                {
                    
                }
                else
                {
                    Response.Redirect("ResultAdmin.aspx");
                }
            }
            catch (Exception)
            {

            }


            hdnExamID.Value = ExamID;
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
            DataTable dt = new DataTable();
            try
            {
                _sql = "select id, SigmentName, QuestionType, DeductionOn from OT_SigmentMaster where ExamId = " + ExamID + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                dt = _oo.Fetchdata(_sql);
            }
            catch (Exception)
            {

            }

            string result = ""; double totalMaxmarks = 0; double totalObtmarks = 0; int totalNoOfQus = 0; int totalNoofQusAttempt = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["QuestionType"].ToString() == "MCQs")
                {
                    _sql = "select top 1 MaxMarks from OT_AnswerMaster where ExamId=" + ExamID + " and SectionId=" + dt.Rows[i]["id"].ToString() + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                    double singleQusMaxMarks = double.Parse(_oo.ReturnTag(_sql, "MaxMarks"));
                    _sql = "select sum(MaxMarks) MaxMarks from OT_AnswerMaster where ExamId=" + ExamID + " and SectionId=" + dt.Rows[i]["id"].ToString() + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                    var MaxMarks = _oo.ReturnTag(_sql, "MaxMarks");
                    _sql = "select isnull(sum(ObtaindMarks), 0) ObtaindMarks from OT_ExamAnswerResult where ExamId = " + ExamID + " and SectionId=" + dt.Rows[i]["id"].ToString() + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and SrNO='" + Session["SRNo"].ToString() + "'";
                    var ObtaindMarks = _oo.ReturnTag(_sql, "ObtaindMarks");

                    _sql = "select count(*) cnt from OT_AnswerMaster where ExamId=" + ExamID + " and SectionId=" + dt.Rows[i]["id"].ToString() + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                    var total = _oo.ReturnTag(_sql, "cnt");

                    _sql = "select count(*) cnt from OT_ExamAnswerResult where ExamId = " + ExamID + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and SrNO='" + Session["SRNo"].ToString() + "' and QuestionId in (select id from OT_AnswerMaster where ExamId = " + ExamID + "  and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and SectionId = " + dt.Rows[i]["id"].ToString() + ")";
                    var attempted = _oo.ReturnTag(_sql, "cnt");

                    _sql = "select count(*) cnt from OT_AnswerMaster ans inner join OT_ExamAnswerResult rans on rans.ExamId = ans.ExamId and ans.id = rans.QuestionId  and  ans.SessionName='" + Session["SessionName"] + "' and ans.BranchCode=" + Session["BranchCode"] + " and  rans.SessionName='" + Session["SessionName"] + "' and rans.BranchCode=" + Session["BranchCode"] + "";
                    _sql = _sql + " where((case when LEFT(ans.RightOption, 1) = right(ans.RightOption, 1) then ans.RightOption else (LEFT(ans.RightOption, 1) + '' + Right(ans.RightOption, 1)) end) = rans.ChooseOption";
                    _sql = _sql + " or(case when LEFT(ans.RightOption, 1) = right(ans.RightOption, 1) then ans.RightOption else (right(ans.RightOption, 1) + '' + left(ans.RightOption, 1)) end) = rans.ChooseOption) ";
                    _sql = _sql + " and ans.ExamId = " + ExamID + " and  rans.SessionName='" + Session["SessionName"] + "' and rans.BranchCode=" + Session["BranchCode"] + " and SrNO = '" + Session["SRNo"].ToString() + "' and QuestionId in (select id from OT_AnswerMaster where ExamId = " + ExamID + " and SectionId = " + dt.Rows[i]["id"].ToString() + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + ")";
                    var right = _oo.ReturnTag(_sql, "cnt");

                    _sql = "select count(*) cnt from OT_AnswerMaster ans inner join OT_ExamAnswerResult rans on rans.ExamId=ans.ExamId and ans.id=rans.QuestionId  and  ans.SessionName='" + Session["SessionName"] + "' and ans.BranchCode=" + Session["BranchCode"] + " and  rans.SessionName='" + Session["SessionName"] + "' and rans.BranchCode=" + Session["BranchCode"] + "";
                    _sql = _sql + " where ((case when LEFT(ans.RightOption, 1)=right(ans.RightOption, 1) then ans.RightOption else (LEFT(ans.RightOption, 1)+''+Right(ans.RightOption, 1)) end) <>rans.ChooseOption  ";
                    _sql = _sql + "and(case when LEFT(ans.RightOption, 1) = right(ans.RightOption, 1) then ans.RightOption else (right(ans.RightOption, 1) + '' + left(ans.RightOption, 1)) end) <> rans.ChooseOption)  ";
                    _sql = _sql + "and ans.ExamId=" + ExamID + " and  rans.SessionName='" + Session["SessionName"] + "' and rans.BranchCode=" + Session["BranchCode"] + " and SrNO='" + Session["SRNo"].ToString() + "' and QuestionId in (select id from OT_AnswerMaster where ExamId = " + ExamID + " and SectionId = " + dt.Rows[i]["id"].ToString() + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + ") ";
                    var wrong = _oo.ReturnTag(_sql, "cnt");
                    result = result + "<h2 class='breadcrumb1'>Segment : " + dt.Rows[i]["SigmentName"].ToString() + " (MCQs)</h2>";
                    result = result + "<table class='table table-bordered'><tbody>";
                    result = result + "<tr>";
                    result = result + "<td style='width:15%; padding: 5px !important;'><b> No. of Questions </b></td>";
                    result = result + "<td style='width:10%; padding: 5px !important;'><span class='label label-primary' >" + total + "</span></td>";
                    result = result + "<td style='width:15%; padding: 5px !important;'><b> Attempted Questions </b></td>";
                    result = result + "<td style='width:10%; padding: 5px !important;'><span class='label label-primary' >" + attempted + "</span></td>";

                    result = result + "<td style='width:15%; padding: 5px !important;'><b> Incorrect Answers </b></td>";
                    result = result + "<td style='width:10%; padding: 5px !important;'><span class='label label-primary' >" + wrong + "</span></td>";
                    result = result + "<td style='width:15%; padding: 5px !important;'><b> Correct Answers </b></td>";
                    result = result + "<td style='width:10%; padding: 5px !important;'><span class='label label-primary' >" + right + "</span></td>";
                    result = result + "</tr>";

                    result = result + "<tr>";
                    result = result + "<td style='width:15%; padding: 5px !important;'><b> Maximum Marks </b></td>";
                    result = result + "<td style='width:10%; padding: 5px !important;'><span class='label label-warning'>" + double.Parse(MaxMarks).ToString("0.0") + "</span></td>";


                    result = result + "<td style='width:15%; padding: 5px !important;'><b> Obtained Marks </b></td>";
                    result = result + "<td style='width:10%; padding: 5px !important;'><span class='label label-warning' >" + double.Parse(ObtaindMarks).ToString("0.0") + "</span></td>";
                    double dd = (double.Parse(dt.Rows[i]["DeductionOn"].ToString() == "" ? "0" : dt.Rows[i]["DeductionOn"].ToString()) * 100);
                    string mm = (double.Parse(dt.Rows[i]["DeductionOn"].ToString() == "" ? "0" : dt.Rows[i]["DeductionOn"].ToString()) > 0 ? ("1/" + (100 / dd).ToString()) : "0");
                    if (mm == "0")
                    {
                        mm = "";
                    }
                    result = result + "<td style='width:20%; padding: 5px !important;'><b> Negative Marks " + mm + "</b></td>";
                    double NegativeMarking = (double.Parse(dt.Rows[i]["DeductionOn"].ToString() == "" ? "0" : dt.Rows[i]["DeductionOn"].ToString()) * double.Parse(wrong)) * singleQusMaxMarks;
                    result = result + "<td style='width:10%; padding: 5px !important;'><span class='label label-danger' >" + NegativeMarking.ToString("0.00") + "</span></td>";

                    result = result + "<td style='width:15%; padding: 5px !important;'><b> Total Obtained Marks </b></td>";
                    result = result + "<td style='width:10%; padding: 5px !important;'><span class='label label-success' >" + (double.Parse(ObtaindMarks) - NegativeMarking).ToString("0.00") + "</span></td>";

                    result = result + "</tr>";
                    result = result + " </tbody></table>";
                    totalMaxmarks = totalMaxmarks + double.Parse(MaxMarks);
                    totalObtmarks = totalObtmarks + (double.Parse(ObtaindMarks) - NegativeMarking);
                    totalNoOfQus = totalNoOfQus + int.Parse(total);
                    totalNoofQusAttempt = totalNoofQusAttempt + int.Parse(attempted);
                }
                else
                {
                    _sql = "select sum(MaxMarks) MaxMarks from OT_AnswerMaster where ExamId=" + ExamID + " and SectionId=" + dt.Rows[i]["id"].ToString() + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                    var MaxMarks = _oo.ReturnTag(_sql, "MaxMarks");
                    _sql = "select isnull(sum(ObtaindMarks), 0) ObtaindMarks from OT_ExamAnswerResult where ExamId = " + ExamID + " and SectionId=" + dt.Rows[i]["id"].ToString() + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and SrNO='" + Session["SRNo"].ToString() + "'";
                    var ObtaindMarks = _oo.ReturnTag(_sql, "ObtaindMarks");

                    _sql = "select count(*) cnt from OT_AnswerMaster where ExamId=" + ExamID + " and SectionId=" + dt.Rows[i]["id"].ToString() + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                    var total = _oo.ReturnTag(_sql, "cnt");

                    result = result + "<h2 class='breadcrumb1'>Segment : " + dt.Rows[i]["SigmentName"].ToString() + " (Descriptive)</h2>";
                    result = result + "<table class='table table-bordered'><tbody>";
                    result = result + "<tr>";
                    result = result + "<td style='width:15%; padding: 5px !important;'><b> No. of Questions </b></td>";
                    result = result + "<td style='width:10%; padding: 5px !important;'><span class='label label-warning'>" + total + "</span></td>";
                    result = result + "<td style='width:15%; padding: 5px !important;'><b> Total Maximum Marks </b></td>";
                    result = result + "<td style='width:10%; padding: 5px !important;'><span class='label label-warning'>" + double.Parse(MaxMarks).ToString("0.0") + "</span></td>";


                    result = result + "<td style='width:15%; padding: 5px !important;'><b> Obtained Marks </b></td>";
                    result = result + "<td style='width:10%; padding: 5px !important;'><span class='label label-success'>" + double.Parse(ObtaindMarks).ToString("0.0") + "</span></td>";

                    result = result + "</tr>";
                    result = result + " </tbody></table>";
                    totalMaxmarks = totalMaxmarks + double.Parse(MaxMarks);
                    totalObtmarks = totalObtmarks + double.Parse(ObtaindMarks);
                    totalNoOfQus = totalNoOfQus + int.Parse(total);
                }
            }
            summry.InnerHtml = result.ToString();

            totalQues.Text = totalNoOfQus.ToString("0");
            lblMaxmarks.Text = totalMaxmarks.ToString("0");
            lblobtmarks.Text = totalObtmarks.ToString("0.00");
        }
    }
    public override void Dispose()
    {
        _con.Dispose();
        _oo.Dispose();
    }
    
}