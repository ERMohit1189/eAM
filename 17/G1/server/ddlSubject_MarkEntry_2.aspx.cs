using System;
using System.Data;

public partial class ddlSubject_MarkEntry_2 : System.Web.UI.Page
{
    private DataTable _dt = new DataTable();
    private readonly Campus _oo = new Campus();
    private string _sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        string drpclass = Request.Form["classId"].ToString();
        string sectionId = Request.Form["sectionId"].ToString();
        Response.Write("<select id ='drpSubject' class='form-control-blue validatedrp'>");
        Response.Write("<option value=''><--Select--></option>");
        if (Session["Logintype"].ToString() == "Admin")
        {
            _sql = "Select SubjectName,Id from TTSubjectMaster where ClassId=" + drpclass.ToString() + "";
            _sql +=  " and  BranchCode = " + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and ApplicableFor<>'TimeTable'";
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
            _sql = "Select  Distinct SubjectName,sm.Id from ICSESubjectTeacherAllotment sctm";
            _sql +=  " inner join TTSubjectMaster sm on sm.Id=sctm.Subjectid and sm.ClassId=sctm.ClassId and sm.BranchId=sctm.BranchId and sm.SessionName=sctm.SessionName  and sm.BranchCode=sctm.BranchCode";
            _sql +=  " where Ecode='" + Session["LoginName"].ToString() + "'  and ApplicableFor<>'TimeTable' and sctm.ClassId=" + drpclass.ToString() + " and sctm.sectionid=" + sectionId + " ";
            _sql +=  " and  sctm.BranchCode = " + Session["BranchCode"] + " and sctm.SessionName='" + Session["SessionName"].ToString() + "'";
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
