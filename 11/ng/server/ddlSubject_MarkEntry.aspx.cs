using System;
using System.Data;

public partial class ddlSubject_MarkEntry : System.Web.UI.Page
{
    private DataTable _dt = new DataTable();
    private readonly Campus _oo = new Campus();
    private string _sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string drpclass = Request.Form["classId"].ToString();
        string branchId = Request.Form["branchId"].ToString();

        Response.Write("<select id='ddlSubject' class='form-control-blue'>");
        Response.Write("<option value=''><--Select--></option>");

        if (Session["Logintype"].ToString() == "Admin")
        {
            _sql = "Select SubjectName,Id from master_subjects";
            _sql +=  " where ClassId=" + drpclass.ToString() + " and SessionName='" + Session["SessionName"].ToString() + "' and isVisible=1 order by id";
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
            _sql = "Select distinct sm.SubjectName,sctm.Subjectid as Id from SubjectClassTeacherMaster sctm ";
            _sql +=  "inner join master_subjects sm on sm.Id=sctm.Subjectid ";
            _sql +=  "inner join BranchMaster bm on bm.id=sctm.BranchId ";
            _sql +=  " where sctm.ClassId=" + drpclass.ToString() + " and sctm.BranchId="+ branchId.ToString() + " and Ecode='" + Session["LoginName"].ToString() + "' and sctm.SessionName='" + Session["SessionName"].ToString() + "' and isVisible=1 order by id";
            _dt = DAL.DALInstance.GetValueInTable(_sql);
            if (_dt != null && _dt.Rows.Count > 0)
            {
                for (int i = 0; i < _dt.Rows.Count; i++)
                {
                    Response.Write("<option value='" + _dt.Rows[i]["Id"].ToString() + "'>" + _dt.Rows[i]["SubjectName"].ToString() + "</option>");
                }
            }
        }

        Response.Write("</select>");
    }
}