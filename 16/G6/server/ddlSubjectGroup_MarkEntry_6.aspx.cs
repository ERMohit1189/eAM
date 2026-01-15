using System;
using System.Data;

public partial class common_G6_ddlSubjectGroup_MarkEntry_6 : System.Web.UI.Page
{
    private DataTable _dt = new DataTable();
    private readonly Campus _oo = new Campus();
    private string _sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string drpclass = Request.Form["classId"].ToString();
        string drpsectionId = Request.Form["drpsectionId"].ToString();
        string drpBranch = Request.Form["drpBranch"].ToString();
        string drpsectionName = Request.Form["drpsectionName"].ToString();
        string streamName = Request.Form["streamName"].ToString();



        Response.Write("<select id ='drpSubjectGroup' class='form-control-blue'>");
        Response.Write("<option value=''><--Select--></option>");

        _sql = "Select Count(*) Count from ClassTeacherMaster ctm";
        _sql +=  " inner join dt_ClassGroupMaster t1 on t1.ClassId=ctm.ClassId";
        _sql +=  " where EmpCode='" + Session["LoginName"].ToString() + "' and IsClassTeacher=1";
        _sql +=  " and ctm.SessionName='" + Session["SessionName"].ToString() + "' and BranchId='" + drpBranch.ToString() + "' ";
        _sql +=  " and Sectionid='" + drpsectionId.ToString() + "' and t1.BranchCode=" + Session["BranchCode"].ToString() + " and ctm.BranchCode=" + Session["BranchCode"].ToString() + " and t1.GroupId='G6'";
        Session["count"] = BAL.objBal.ReturnTag(_sql, "Count");

        if (Session["Logintype"].ToString() == "Admin" || Convert.ToInt16(Session["count"].ToString()) > 0)
        {
            _sql = "Select SubjectGroup,Id from SubjectGroupMaster where ClassId='" + drpclass.ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and SectionName='" + drpsectionName.ToString() + "' and Branchid='" + drpBranch.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and IsForOnlyExam=1";
            _dt = DAL.DALInstance.GetValueInTable(_sql);
            if (_dt != null && _dt.Rows.Count > 0)
            {
                for (int i = 0; i < _dt.Rows.Count; i++)
                {
                    if (streamName.ToUpper()=="PCM")
                    {
                        if (_dt.Rows[i]["SubjectGroup"].ToString().ToUpper()!= "BIOLOGY")
                        {
                            Response.Write("<option value='" + _dt.Rows[i]["id"].ToString() + "'>" + _dt.Rows[i]["SubjectGroup"].ToString() + "</option>");
                        }
                    }
                    else if (streamName.ToUpper() == "PCB")
                    {
                        if (_dt.Rows[i]["SubjectGroup"].ToString().ToUpper() != "MATHEMATICS")
                        {
                            Response.Write("<option value='" + _dt.Rows[i]["id"].ToString() + "'>" + _dt.Rows[i]["SubjectGroup"].ToString() + "</option>");
                        }
                    }
                    else if (streamName.ToUpper() == "PCMB")
                    {
                        Response.Write("<option value='" + _dt.Rows[i]["id"].ToString() + "'>" + _dt.Rows[i]["SubjectGroup"].ToString() + "</option>");
                    }
                    else
                    {
                        Response.Write("<option value='" + _dt.Rows[i]["id"].ToString() + "'>" + _dt.Rows[i]["SubjectGroup"].ToString() + "</option>");
                    }
                }
            }
        }
        else
        {
            _sql = "Select Distinct sgm.SubjectGroup,sgm.Id from SubjectClassTeacherMaster sctm";
            _sql +=  " inner join SubjectMaster sm on sm.Id=sctm.Subjectid and sm.SessionName=sctm.SessionName";
            _sql +=  " Left join S02_SubjectPaperMaster spm on spm.S02ID=sm.PaperID and spm.SessionName=sctm.SessionName";
            _sql +=  " Inner join SubjectGroupMaster sgm on sgm.Id=spm.SubjectGroupID and sgm.SessionName=sctm.SessionName";
            _sql +=  " where Ecode='" + Session["LoginName"].ToString() + "' and sgm.ClassId='" + drpclass.ToString() + "'";
            _sql +=  " and sgm.SectionName='" + drpsectionName.ToString() + "' and sgm.BranchCode=" + Session["BranchCode"].ToString() + " and spm.BranchCode=" + Session["BranchCode"].ToString() + " and sm.BranchCode=" + Session["BranchCode"].ToString() + " and sctm.BranchCode=" + Session["BranchCode"].ToString() + " and sctm.SessionName='" + Session["SessionName"].ToString() + "'";
            _sql +=  " and (IsForExam is null or IsForExam ='1')  and (sgm.Branchid='" + drpBranch.ToString() + "' or sgm.Branchid is null)";
            _dt = DAL.DALInstance.GetValueInTable(_sql);
            if (_dt != null && _dt.Rows.Count > 0)
            {
                for (int i = 0; i < _dt.Rows.Count; i++)
                {
                    Response.Write("<option value='" + _dt.Rows[i]["id"].ToString() + "'>" + _dt.Rows[i]["SubjectGroup"].ToString() + "</option>");
                }
            }
        }
        Response.Write("</select>");
    }

}