using System;
using System.Data;

public partial class common_G6_ddlSubject_MarkEntry_6  : System.Web.UI.Page
{
    private DataTable _dt = new DataTable();
    private readonly Campus _oo = new Campus();
    private string _sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string drpclass = Request.Form["classId"].ToString();
        string drpSubjectGroup = Request.Form["drpSubjectGroup"].ToString();
        string drpsectionName = Request.Form["drpsectionName"].ToString();
        string drpBranch = Request.Form["drpBranch"].ToString();

        Response.Write("<select id='ddlSubject' class='form-control-blue'>");
        Response.Write("<option value=''><--Select--></option>");

        if (Session["Logintype"].ToString() == "Admin")
        {
            _sql = "Select SubjectName,sm.Id from SubjectMaster sm";
            _sql +=  " inner join S02_SubjectPaperMaster spm on spm.S02ID=sm.PaperID and spm.SessionName=sm.SessionName";
            _sql +=  " inner join SubjectGroupMaster sgm on sgm.Id=spm.SubjectGroupID and sgm.SessionName=sm.SessionName";
            _sql +=  " where sgm.Id='" + drpSubjectGroup.ToString() + "' and spm.BranchCode=" + Session["BranchCode"].ToString() + " and sgm.BranchCode=" + Session["BranchCode"].ToString() + " and sm.BranchCode=" + Session["BranchCode"].ToString() + " and sm.SessionName='" + Session["SessionName"].ToString() + "' and (IsForExam=1 or IsForExam is null)  order by sm.id";
            _dt = DAL.DALInstance.GetValueInTable(_sql);
            if (_dt != null && _dt.Rows.Count > 0)
            {
                for (int i = 0; i < _dt.Rows.Count; i++)
                {
                    Response.Write("<option value='" + _dt.Rows[i]["id"].ToString() + "'>" + _dt.Rows[i]["SubjectName"].ToString() + "</option>");
                }
            }
        }
        else
        {
           
            _sql = "Select sm.SubjectName,sctm.Subjectid as Id from SubjectClassTeacherMaster sctm";
            _sql +=  " inner join SubjectMaster sm on sm.Id=sctm.Subjectid";
            _sql +=  " Left join S02_SubjectPaperMaster spm on spm.S02ID=sm.PaperID";
            _sql +=  " where Ecode='" + Session["LoginName"].ToString() + "' and sctm.ClassId='" + drpclass.ToString() + "' ";
            _sql +=  " and (sctm.Branchid='" + drpBranch.ToString() + "' and spm.BranchCode=" + Session["BranchCode"].ToString() + " and sm.BranchCode=" + Session["BranchCode"].ToString() + " and sctm.BranchCode=" + Session["BranchCode"].ToString() + " or sctm.Branchid is null) and sctm.SectionName='" + drpsectionName.ToString() + "' and sctm.SessionName='" + Session["SessionName"].ToString() + "'";
            _sql +=  " and (IsForExam is null or IsForExam ='1')  and spm.SubjectGroupID='" + drpSubjectGroup.ToString() + "'";
            _dt = DAL.DALInstance.GetValueInTable(_sql);
            if (_dt != null && _dt.Rows.Count > 0)
            {
                for (int i = 0; i < _dt.Rows.Count; i++)
                {
                    Response.Write("<option value='" + _dt.Rows[i]["id"].ToString() + "'>" + _dt.Rows[i]["SubjectName"].ToString() + "</option>");
                }
            }
            Response.Write("</select>");
        }
    }
}