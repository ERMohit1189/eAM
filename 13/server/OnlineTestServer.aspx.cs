using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;

public partial class OnlineTestServer : System.Web.UI.Page
{
    private SqlConnection _con=new SqlConnection();
    private readonly Campus _oo=new Campus();
    private string _sql, _sql1=String.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        int sectionCount = 0; string SigmentName = "";
        string ExamID = Request.Form["ExamID"].ToString().Trim();

        string _sql1 = "select filepath, fileType from OT_ExamMaster where id=" + ExamID + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        string fileType = _oo.ReturnTag(_sql1, "fileType");
        string classs = "";
        if (fileType == "Manual")
        {
            classs = "col-sm-3";
        }
        else
        {
            classs = "col-sm-6";
        }

        _sql = "select count(*) cnt from OT_SigmentMaster where ExamId=" + ExamID + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        sectionCount = int.Parse(_oo.ReturnTag(_sql, "cnt"));

        _sql = "select OSM.id SectionId, * from (select case when EAR.QuestionId is null then '' else EAR.ChooseOption end QuesStatus, AM.SectionId, AM.id, AM.SubjectId, AM.PaperId, AM.ExamId, NoOfQuestions, Questions, AM.MaxMarks, Option1, Option2, Option3, Option4, Option5, Option6, AM.RightOption, ChooseOption, UploadFiles ";
        _sql = _sql + " from OT_AnswerMaster AM";
        _sql = _sql + " left join OT_ExamAnswerResult EAR on EAR.QuestionId=Am.id";
        _sql = _sql + " where AM.ExamId=" + ExamID + " and EAR.SrNO='" + Session["LoginName"] + "'  and ear.SessionName='" + Session["SessionName"] + "' and ear.BranchCode=" + Session["BranchCode"] + "  and am.SessionName='" + Session["SessionName"] + "' and am.BranchCode=" + Session["BranchCode"] + "";
        _sql = _sql + " union all";
        _sql = _sql + " select '' QuesStatus,  SectionId, id, SubjectId, PaperId, ExamId, NoOfQuestions,";
        _sql = _sql + " Questions, MaxMarks, Option1, Option2, Option3, Option4, Option5, Option6, RightOption, '' ChooseOption, '' UploadFiles  from OT_AnswerMaster ";
        _sql = _sql + " where id not in(select QuestionId from OT_ExamAnswerResult where ExamId=" + ExamID + " and SrNO='" + Session["LoginName"] + "') and ExamId=" + ExamID + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + ")t1 ";
        _sql = _sql + " left join OT_SigmentMaster OSM on OSM.Id=t1.SectionId and osm.SessionName='" + Session["SessionName"] + "' and osm.BranchCode=" + Session["BranchCode"] + " order by t1.SectionId, t1.id asc";
        var ds=_oo.GridFill(_sql);
        if (ds != null)
        {
            int qusNo = 1;
            Response.Write("<div style='max-height: 700px; width: 100%; overflow: auto;' id='ansDiv'>");
            for (int i=0; i <ds.Tables[0].Rows.Count; i++)
            {
                qusNo = qusNo + 1;
                if (sectionCount >= 1)
                {
                    if (SigmentName != ds.Tables[0].Rows[i]["SigmentName"].ToString())
                    {
                        qusNo = 1;
                        SigmentName = ds.Tables[0].Rows[i]["SigmentName"].ToString();
                        Response.Write("<div class='col-sm-12' style='" + (i == 0 ? "margin-top: 10px;" : "margin-top: 50px;") + " text-align: center; border: 1px solid #000 !important; padding: 3px; background: #717276;'>");
                        Response.Write("<span style='font-weight:bold; color:#fff !important; font-size: 17px;'>" + ds.Tables[0].Rows[i]["SigmentName"].ToString() + " ("+ ds.Tables[0].Rows[i]["QuestionType"].ToString() +")</span>");
                        Response.Write("</div>");
                        if (ds.Tables[0].Rows[i]["QuestionType"].ToString() != "MCQs")
                        {
                            Response.Write("<div class='col-sm-12'><br>");
                            Response.Write("<span class='txt-bold txt-middle-l text-primary hide'>Note:- </span><span class='txt-bold txt-middle-l text-danger blink hide'>Either upload documents or type the answers.</span>");
                            Response.Write("<span class='txt-bold txt-middle-l text-primary hide'>Note:- </span><span class='txt-bold txt-middle-l text-danger blink'>If you've to upload morw than 10 files, please merge all the files into a single pdf file then upload into eAM&#174;</span>");
                            Response.Write("<div class='col-sm-12 no-padding'><select style='max-width:350px;' id='" + ds.Tables[0].Rows[i]["SectionId"].ToString() + "' onchange='AnswerType(this);' disabled='disabled'><option value='TypeAnswer'>Type Answer</option><option value='UploadFile' selected='selected'>Upload Answers</option></select></div>");
                            Response.Write("<div class='col-sm-12 no-padding' id='divUpload" + ds.Tables[0].Rows[i]["SectionId"].ToString() + "'>");
                            Response.Write("<div class='col-sm-3 no-padding'><br>");
                            Response.Write("<input type='file'  multiple='5' class='form-control' id='upload_doc" + ds.Tables[0].Rows[i]["SectionId"].ToString() + "' accept='.pdf,.png,.jpg,.jpeg' /><br><span style='Color:red;' class='hide' id='spnError" + ds.Tables[0].Rows[i]["SectionId"].ToString() + "'>You can only upload a maximum of 10 files!</span><span style='Color:green;' class='hide' id='spnSuccess" + ds.Tables[0].Rows[i]["SectionId"].ToString() + "'>Successfully Uploaded.</span>");
                            Response.Write("</div>");
                            Response.Write("<div class='col-sm-3 no-padding'><br>");
                            Response.Write("&nbsp;<button id='btn-submit" + ds.Tables[0].Rows[i]["SectionId"].ToString() + "' onclick='uploadAns(this);' SectionId=" + ds.Tables[0].Rows[i]["SectionId"].ToString() + " ExamID=" + ds.Tables[0].Rows[i]["ExamID"].ToString() + " type='button' class='button form-control-blue' style='height:32px;'>Upload</button>");
                            Response.Write("</div>");
                            Response.Write("</div>");
                            Response.Write("</div>");
                            if (ds.Tables[0].Rows[i]["QuestionType"].ToString() != "MCQs" && ds.Tables[0].Rows[i]["UploadFiles"].ToString() != "")
                            {
                                string files = ds.Tables[0].Rows[i]["UploadFiles"].ToString();
                                string[] uerls = files.Split(new string[] { "##" }, StringSplitOptions.None);
                                Response.Write("<div class='col-sm-12' style='border:1px solid Red;'><br>");
                                Response.Write("<div class='col-sm-12' style='font-weight:bold; color:Red;'>Uploaded Answer File(s):</div>");
                                for (int j = 0; j < uerls.Length; j++)
                                {
                                    Response.Write("<div class='col-sm-3'>");
                                    string[] ss = uerls[j].Split(new string[] { "." }, StringSplitOptions.None);
                                    Response.Write("<a href='../uploads/UploadTestDocs/" + uerls[j] + "' download='" + ss[0] + "' style='text-decoration:underline; color:#428bca;'><i class='fa fa-download'></i> File " + (j + 1) + "</a>");
                                    Response.Write("</div>");
                                }
                                Response.Write("</div>");
                            }
                        }
                    }
                }
                if (ds.Tables[0].Rows[i]["QuestionType"].ToString() == "MCQs")
                {
                    Response.Write("<div class='col-sm-12' style='margin-top: 20px;'>");

                    Response.Write("<div class='col-sm-12' style='padding-left: 5px; background: #f4f5f9;'>");
                    Response.Write("<span style='font-weight:bold; font-size: 15px;'>" + qusNo + ") " + ds.Tables[0].Rows[i]["Questions"].ToString() +" ("+ ds.Tables[0].Rows[i]["MaxMarks"].ToString() + " Marks)</span>");
                    Response.Write("</div>");

                    Response.Write("<div class='col-sm-12'>");
                    if (ds.Tables[0].Rows[i]["Option1"].ToString() != "")
                    {
                        Response.Write("<div class='"+ classs + "'>");
                        if (ds.Tables[0].Rows[i]["QuesStatus"].ToString() == ds.Tables[0].Rows[i]["Option1"].ToString())
                        {
                            Response.Write("<input type='checkbox' class='form-checkbox chk' Qtypr='s'  name='rdoOption" + i + "' value='" + ds.Tables[0].Rows[i]["Option1"].ToString() + "' valueID='" + ds.Tables[0].Rows[i]["id"].ToString() + "' checked='checked'>&nbsp;<label for='rdoOption" + i + "'>" + ds.Tables[0].Rows[i]["Option1"].ToString() + "</label>");
                        }
                        else
                        {
                            Response.Write("<input type='checkbox' class='form-checkbox chk' Qtypr='s'  name='rdoOption" + i + "' value='" + ds.Tables[0].Rows[i]["Option1"].ToString() + "' valueID='" + ds.Tables[0].Rows[i]["id"].ToString() + "'>&nbsp;<label for='rdoOption" + i + "'>" + ds.Tables[0].Rows[i]["Option1"].ToString() + "</label>");
                        }
                        Response.Write("</div>");
                    }
                    if (ds.Tables[0].Rows[i]["Option2"].ToString() != "")
                    {
                        Response.Write("<div class='" + classs + "'>");
                        if (ds.Tables[0].Rows[i]["QuesStatus"].ToString() == ds.Tables[0].Rows[i]["Option2"].ToString())
                        {
                            Response.Write("<input type='checkbox' class='form-checkbox chk' Qtypr='s' name='rdoOption" + i + "' value='" + ds.Tables[0].Rows[i]["Option2"].ToString() + "' valueID='" + ds.Tables[0].Rows[i]["id"].ToString() + "' checked='checked'>&nbsp;<label for='rdoOption" + i + "'>" + ds.Tables[0].Rows[i]["Option2"].ToString() + "</label>");
                        }
                        else
                        {
                            Response.Write("<input type='checkbox' class='form-checkbox chk' Qtypr='s' name='rdoOption" + i + "' value='" + ds.Tables[0].Rows[i]["Option2"].ToString() + "' valueID='" + ds.Tables[0].Rows[i]["id"].ToString() + "'>&nbsp;<label for='rdoOption" + i + "'>" + ds.Tables[0].Rows[i]["Option2"].ToString() + "</label>");
                        }
                        Response.Write("</div>");
                    }
                   
                    if (ds.Tables[0].Rows[i]["Option3"].ToString() != "")
                    {
                        Response.Write("<div class='" + classs + "'>");
                        if (ds.Tables[0].Rows[i]["QuesStatus"].ToString() == ds.Tables[0].Rows[i]["Option3"].ToString())
                        {
                            Response.Write("<input type='checkbox' class='form-checkbox chk' Qtypr='s' name='rdoOption" + i + "' value='" + ds.Tables[0].Rows[i]["Option3"].ToString() + "' valueID='" + ds.Tables[0].Rows[i]["id"].ToString() + "' checked='checked'>&nbsp;<label for='rdoOption" + i + "'>" + ds.Tables[0].Rows[i]["Option3"].ToString() + "</label>");
                        }
                        else
                        {
                            Response.Write("<input type='checkbox' class='form-checkbox chk' Qtypr='s' name='rdoOption" + i + "' value='" + ds.Tables[0].Rows[i]["Option3"].ToString() + "' valueID='" + ds.Tables[0].Rows[i]["id"].ToString() + "'>&nbsp;<label for='rdoOption" + i + "'>" + ds.Tables[0].Rows[i]["Option3"].ToString() + "</label>");
                        }
                        Response.Write("</div>");
                    }
                    if (ds.Tables[0].Rows[i]["Option4"].ToString() != "")
                    {
                        Response.Write("<div class='" + classs + "'>");
                        if (ds.Tables[0].Rows[i]["QuesStatus"].ToString() == ds.Tables[0].Rows[i]["Option4"].ToString())
                        {
                            Response.Write("<input type='checkbox' class='form-checkbox chk' Qtypr='s' name='rdoOption" + i + "' value='" + ds.Tables[0].Rows[i]["Option4"].ToString() + "' valueID='" + ds.Tables[0].Rows[i]["id"].ToString() + "' checked='checked'>&nbsp;<label for='rdoOption" + i + "'>" + ds.Tables[0].Rows[i]["Option4"].ToString() + "</label>");
                        }
                        else
                        {
                            Response.Write("<input type='checkbox' class='form-checkbox chk' Qtypr='s' name='rdoOption" + i + "' value='" + ds.Tables[0].Rows[i]["Option4"].ToString() + "' valueID='" + ds.Tables[0].Rows[i]["id"].ToString() + "'>&nbsp;<label for='rdoOption" + i + "'>" + ds.Tables[0].Rows[i]["Option4"].ToString() + "</label>");
                        }
                        Response.Write("</div>");
                    }
                    
                    if (ds.Tables[0].Rows[i]["Option5"].ToString() != "")
                    {
                        Response.Write("<div class='" + classs + "'>");
                        if (ds.Tables[0].Rows[i]["QuesStatus"].ToString() == ds.Tables[0].Rows[i]["Option5"].ToString())
                        {
                            Response.Write("<input type='checkbox' class='form-checkbox chk' Qtypr='s' name='rdoOption" + i + "' value='" + ds.Tables[0].Rows[i]["Option5"].ToString() + "' valueID='" + ds.Tables[0].Rows[i]["id"].ToString() + "' checked='checked'>&nbsp;<label for='rdoOption" + i + "'>" + ds.Tables[0].Rows[i]["Option5"].ToString() + "</label>");
                        }
                        else
                        {
                            Response.Write("<input type='checkbox' class='form-checkbox chk' Qtypr='s' name='rdoOption" + i + "' value='" + ds.Tables[0].Rows[i]["Option5"].ToString() + "' valueID='" + ds.Tables[0].Rows[i]["id"].ToString() + "'>&nbsp;<label for='rdoOption" + i + "'>" + ds.Tables[0].Rows[i]["Option5"].ToString() + "</label>");
                        }
                        Response.Write("</div>");
                    }
                    if (ds.Tables[0].Rows[i]["Option6"].ToString() != "")
                    {
                        Response.Write("<div class='" + classs + "'></div>");
                    }
                    Response.Write("</div>");


                    Response.Write("</div>");
                }
                else
                {
                    
                    Response.Write("<div class='col-sm-12' style='margin-top: 20px;'>");
                    
                    Response.Write("<div class='col-sm-12' style='padding-left: 5px; background: #f4f5f9;'>");
                    Response.Write("<span style='font-weight:bold; font-size: 15px;'>" + qusNo + ") " + ds.Tables[0].Rows[i]["Questions"].ToString() + " (" + ds.Tables[0].Rows[i]["MaxMarks"].ToString() + " Marks)</span>");
                    Response.Write("</div>");

                    Response.Write("<div class='col-sm-12'><br>");
                    Response.Write("<textarea placeholder='Write answer..' onblur='textarea(this);' valueID='" + ds.Tables[0].Rows[i]["id"].ToString() + "' class='" + ds.Tables[0].Rows[i]["sectionid"].ToString() + " hide'>" + ds.Tables[0].Rows[i]["ChooseOption"].ToString() + "</textarea>");
                    Response.Write("</div>");


                    Response.Write("</div>");
                }

            }
            Response.Write("</div>");

        }
    }
}