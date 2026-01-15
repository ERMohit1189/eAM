using System;
using System.Data;

public partial class ddlSubject_MarkEntry_5 : System.Web.UI.Page
{
    private DataTable _dt = new DataTable();
    private readonly Campus _oo = new Campus();
    private string _sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string drpclass = Request.Form["classId"].ToString();
        string drpsectionName = Request.Form["drpsectionName"].ToString();
        string drpsectionId = Request.Form["drpsectionId"].ToString();


        Response.Write("<select id ='drpSubject' class='form-control-blue'>");
        Response.Write("<option value=''><--Select--></option>");

        if (Session["Logintype"].ToString() == "Admin")
        {
            _sql = "Select SubjectName,Id from TTSubjectMaster where ClassId='" + drpclass.ToString() + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "' and ApplicableFor in ('Exam','Both')";
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
            _sql = "Select  Distinct sm.SubjectName,sm.Id from ICSESubjectTeacherAllotment sctm";
            _sql +=  " inner join TTSubjectMaster sm on sm.Classid=sctm.Classid and sm.branchid=sctm.branchid and sm.Id=sctm.Subjectid and sm.SessionName=sctm.SessionName and sm.BranchCode=sctm.BranchCode";
            _sql +=  " where Ecode='" + Session["LoginName"].ToString() + "' and sctm.BranchCode=" + Session["BranchCode"] + " and sctm.SessionName='" + Session["SessionName"].ToString() + "' and sctm.ClassId='" + drpclass.ToString() + "'  ";
            _sql +=  " and sctm.SectionId='" + drpsectionId.ToString() + "' ";
            _sql +=  "  and ApplicableFor in ('Exam','Both') ";
            _dt = DAL.DALInstance.GetValueInTable(_sql);
            if (_dt != null && _dt.Rows.Count > 0)
            {
                for (int i = 0; i < _dt.Rows.Count; i++)
                {
                    Response.Write("<option value='" + _dt.Rows[i]["id"].ToString() + "'>" + _dt.Rows[i]["SubjectName"].ToString() + "</option>");
                }
            }
        }
        Response.Write("</select>");
    }

}