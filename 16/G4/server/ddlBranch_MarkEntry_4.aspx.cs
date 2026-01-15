using System;
using System.Data;

public partial class common_G4_ddlBranch_MarkEntry_4 : System.Web.UI.Page
{
    private DataTable _dt = new DataTable();
    private readonly Campus _oo = new Campus();
    private string _perType = "";
    private string _sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string drpclass = Request.Form["classId"].ToString();

        Response.Write("<select id ='drpbranch' class='form-control-blue'>");
        Response.Write("<option value=''><--Select--></option>");
        
        if (Session["Logintype"].ToString() == "Admin")
        {
            _sql = "Select BranchName,id from BranchMaster where ClassId='" + drpclass.ToString() + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "'";
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
            _sql = " Select Distinct sm.BranchName,sm.id from ICSESubjectTeacherAllotment sctm";
            _sql +=  " inner join BranchMaster sm on sm.ClassId=sctm.ClassId and sm.Id=sctm.BranchId and sm.SessionName=sctm.SessionName and sm.BranchCode=sctm.BranchCode ";
            _sql +=  " where sctm.BranchCode=" + Session["BranchCode"].ToString() + " and sctm.SessionName = '" + Session["SessionName"] + "' and sctm.BranchCode = " + Session["BranchCode"] + " and sctm.ClassId='" + drpclass.ToString() + "' ";
            _sql +=  " and ECode='" + Session["LoginName"].ToString() + "' ";

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