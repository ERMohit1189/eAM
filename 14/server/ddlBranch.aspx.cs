using System;
using System.Data;

public partial class ddlBranch : System.Web.UI.Page
{
    private DataTable _dt = new DataTable();
    private readonly Campus _oo = new Campus();
    private string _perType = "";
    private string _sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string drpclass = Request.Form["classId"].ToString();
        Response.Write("<select id ='drpBranch' class='form-control-blue validatedrp'>");
        Response.Write("<option value=''><--Select--></option>");
        if (Session["Logintype"].ToString() == "Admin")
        {
            _sql = "Select Distinct BranchName,Id from  BranchMaster ";
            _sql = _sql + " where SessionName='" + Session["SessionName"] + "' and BranchCode = " + Session["BranchCode"] + " and classid=" + drpclass + "";
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
            _sql = "Select Distinct t1.BranchName, t1.Id  from ICSEClassTeacherAllotment ctm";
            _sql = _sql + " inner join BranchMaster t1 on t1.ClassId=ctm.ClassId and t1.id=ctm.BranchId  and t1.SessionName=ctm.SessionName and t1.BranchCode=ctm.BranchCode";
            _sql = _sql + " where Ecode='" + Session["LoginName"].ToString() + "'";
            _sql = _sql + " and ctm.SessionName='" + Session["SessionName"].ToString() + "' and ctm.BranchCode = " + Session["BranchCode"] + "  and ctm.classid=" + drpclass + "";
            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                Response.Write("<option value='" + _dt.Rows[i]["id"].ToString() + "'>" + _dt.Rows[i]["BranchName"].ToString() + "</option>");
            }
        }

        Response.Write("</select>");
    }
}