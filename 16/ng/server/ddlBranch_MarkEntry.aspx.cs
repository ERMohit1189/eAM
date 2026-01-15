using System;
using System.Data;

public partial class ddlBranch_MarkEntry : System.Web.UI.Page
{
    private DataTable _dt = new DataTable();
    private DataSet _ds = new DataSet();
    private readonly Campus _oo = new Campus();
    private string _sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string classId = Request.Form["classId"].ToString();
        Response.Write("<select id = 'drpBranch' class='form-control-blue'>");
        Response.Write("<option value=''><--Select--></option>");
        if (Session["Logintype"].ToString() == "Admin")
        {
            _sql = "Select BranchName,Id from BranchMaster Where ClassId=" + classId.ToString() + " and SessionName='" + Session["SessionName"].ToString() + "'";
            _dt = DAL.DALInstance.GetValueInTable(_sql);

            if (_dt != null && _dt.Rows.Count > 0)
            {
                for (int i = 0; i < _dt.Rows.Count; i++)
                {
                    Response.Write("<option value='" + _dt.Rows[i]["id"].ToString() + "'>" + _dt.Rows[i]["BranchName"].ToString() + "</option>");
                }
            }
        }
        else
        {
            _sql = "select distinct * from (";
            _sql +=  "Select Distinct BranchName,sm.Id from ClassTeacherMaster T1 ";
            _sql =_sql+ "inner join BranchMaster sm on sm.Id = T1.BranchId and sm.SessionName = t1.SessionName and T1.SessionName = '" + Session["SessionName"].ToString() + "' ";
            _sql =_sql+ "and sm.SessionName = T1.SessionName and sm.SessionName = '" + Session["SessionName"].ToString() + "' and EmpCode = '" + Session["LoginName"].ToString() + "' and t1.ClassId = " + classId.ToString() + " ";
            _sql =_sql+ "union all ";
            _sql =_sql+ "Select Distinct sm.BranchName,sm.Id from SubjectClassTeacherMaster T1 ";
            _sql =_sql+ "inner join BranchMaster sm on sm.id = T1.BranchId and sm.SessionName = t1.SessionName and T1.SessionName = '" + Session["SessionName"].ToString() + "' and sm.ClassId=" + classId.ToString() + " ";
            _sql +=  "and sm.SessionName = T1.SessionName and sm.SessionName = '" + Session["SessionName"].ToString() + "' and t1.Ecode = '" + Session["LoginName"].ToString() + "' and t1.ClassId = " + classId.ToString() + ")tMain Order by tMain.Id asc ";
            _dt = DAL.DALInstance.GetValueInTable(_sql);

            if (_dt != null && _dt.Rows.Count > 0)
            {
                for (int i = 0; i < _dt.Rows.Count; i++)
                {
                    Response.Write("<option value='" + _dt.Rows[i]["id"].ToString() + "'>" + _dt.Rows[i]["BranchName"].ToString() + "</option>");
                }
            }
        }
        Response.Write("</select>");
    }
}