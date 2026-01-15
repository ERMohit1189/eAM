using System;
using System.Data;
public partial class ddlClass_MarkEntry : System.Web.UI.Page
{
    private DataTable _dt = new DataTable();
    private readonly Campus _oo = new Campus();
    
    private string _sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Write("<select id ='drpclass' class='form-control-blue'>");
        Response.Write("<option value=''><--Select--></option>");
        if (Session["Logintype"].ToString() == "Admin")
        {
            _sql = "Select Distinct ClassName,cm.Id as Id,CIDOrder from ClassMaster cm ";
            _sql +=  " where cm.SessionName='" + Session["SessionName"] + "' order by cidorder asc ";
            _dt = DAL.DALInstance.GetValueInTable(_sql);
            
            if (_dt != null && _dt.Rows.Count > 0)
            {
                for (int i = 0; i < _dt.Rows.Count; i++)
                {
                    Response.Write("<option value='" + _dt.Rows[i]["id"].ToString() + "'>" + _dt.Rows[i]["ClassName"].ToString() + "</option>");
                }
            }
        }
        else
        {
            _sql = "select distinct * from (";
            _sql +=  "Select Distinct ClassName,cm.Id,CIDOrder from ClassTeacherMaster T1 ";
            _sql = _sql+ "inner join ClassMaster cm on cm.Id = T1.ClassId and cm.SessionName = t1.SessionName and T1.SessionName = '" + Session["SessionName"] + "' ";
            _sql = _sql+ "and cm.SessionName = T1.SessionName and cm.SessionName = '" + Session["SessionName"] + "' and EmpCode = '" + Session["LoginName"].ToString() + "' ";
            _sql = _sql+ "union all ";
            _sql = _sql+ "Select Distinct ClassName, cm.Id,CIDOrder from SubjectClassTeacherMaster T1 ";
            _sql = _sql+ "inner join ClassMaster cm on cm.Id = T1.ClassId and cm.SessionName = t1.SessionName and T1.SessionName = '" + Session["SessionName"] + "' ";
            _sql = _sql+ "and cm.SessionName = T1.SessionName and cm.SessionName = '" + Session["SessionName"] + "' and ECode = '" + Session["LoginName"].ToString() + "' ";
            _sql +=  " where cm.SessionName='" + Session["SessionName"] + "')tMain order by cidorder asc ";
            _dt = DAL.DALInstance.GetValueInTable(_sql);

            if (_dt != null && _dt.Rows.Count > 0)
            {
                for (int i = 0; i < _dt.Rows.Count; i++)
                {
                    Response.Write("<option value='" + _dt.Rows[i]["id"].ToString() + "'>" + _dt.Rows[i]["ClassName"].ToString() + "</option>");
                }
            }
        }
        Response.Write("</select>");
    }
}