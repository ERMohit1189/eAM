using System;
using System.Data;

public partial class ddlPaper_MarkEntry_3_medium : System.Web.UI.Page
{
    private DataTable _dt = new DataTable();
    private readonly Campus _oo = new Campus();
    private string _sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string drpclass = Request.Form["classId"].ToString();
        string drpSubjectId = Request.Form["SubjectId"].ToString();
        string Medium = Request.Form["Medium"].ToString();

        Response.Write("<select id ='drpPaper' class='form-control-blue validatedrp'>");
        Response.Write("<option value=''><--Select--></option>");

        if (Session["Logintype"].ToString() == "Admin")
        {
            _sql = "Select PaperName,Id from TTPaperMaster where ClassId=" + drpclass.ToString() + " and SubjectId=" + drpSubjectId.ToString() + " ";
            _sql +=  " and  BranchCode = " + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and medium='" + Medium + "'";
            _dt = DAL.DALInstance.GetValueInTable(_sql);
            if (_dt != null && _dt.Rows.Count > 0)
            {
                for (int i = 0; i < _dt.Rows.Count; i++)
                {
                    Response.Write("<option value='" + _dt.Rows[i]["id"].ToString() + "'>" + _dt.Rows[i]["PaperName"].ToString() + "</option>");
                }
            }
        }
        else
        {
            _sql = "Select  Distinct PaperName,pm.Id from ICSESubjectTeacherAllotment sctm";
            _sql +=  " inner join TTPaperMaster pm on pm.Id=sctm.PaperId and pm.Subjectid=sctm.Subjectid and pm.ClassId=sctm.ClassId and pm.BranchId=sctm.BranchId and pm.SessionName=sctm.SessionName  and pm.BranchCode=sctm.BranchCode";
            _sql +=  " where Ecode='" + Session["LoginName"].ToString() + "' and sctm.ClassId=" + drpclass.ToString() + " and sctm.SubjectId=" + drpSubjectId.ToString() + " ";
            _sql +=  " and  sctm.BranchCode = " + Session["BranchCode"] + " and sctm.SessionName='" + Session["SessionName"].ToString() + "' and pm.medium='" + Medium + "'";
            _dt = DAL.DALInstance.GetValueInTable(_sql);
            if (_dt != null && _dt.Rows.Count > 0)
            {
                for (int i = 0; i < _dt.Rows.Count; i++)
                {
                    Response.Write("<option value='" + _dt.Rows[i]["id"].ToString() + "'>" + _dt.Rows[i]["PaperName"].ToString() + "</option>");
                }
            }
        }
        Response.Write("</select>");
    }

}