using System;
using System.Data;

public partial class common_G2_ddlClass_MarkEntry_2 : System.Web.UI.Page
{
    private DataTable _dt = new DataTable();
    private readonly Campus _oo = new Campus();
    private string _perType = "";
    private string _sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Write("<select id ='drpclass' class='form-control-blue'>");
        Response.Write("<option value=''><--Select--></option>");
        if (Session["Logintype"].ToString() == "Admin")
        {
            _sql = "Select Distinct ClassName,cm.Id as Id,CIDOrder from ClassMaster cm ";
            _sql +=  " inner join dt_ClassGroupMaster t1 on t1.ClassId=cm.Id";
            _sql +=  " where cm.SessionName='" + Session["SessionName"] + "' and cm.BranchCode=" + Session["BranchCode"] + " and t1.SessionName='" + Session["SessionName"] + "' and t1.BranchCode=" + Session["BranchCode"] + " and GroupId='G1' Order by CIDOrder";
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
            _sql = "Select Distinct ClassName,cm.Id from ICSESubjectTeacherAllotment T1";
            _sql += " inner join ClassMaster cm on cm.Id = T1.ClassId and cm.SessionName = t1.SessionName and cm.BranchCode = t1.BranchCode ";
            _sql += " inner join dt_ClassGroupMaster T2 on T2.ClassId = T1.ClassId and cm.SessionName = T1.SessionName and T1.BranchCode=t2.BranchCode ";
            _sql += " where T1.BranchCode=" + Session["BranchCode"] + " and t2.SessionName='" + Session["SessionName"] + "' and GroupId = 'G1' and ECode = '" + Session["LoginName"].ToString() + "'";
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