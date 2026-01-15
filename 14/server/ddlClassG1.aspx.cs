using System;
using System.Data;

public partial class ddlClassG1 : System.Web.UI.Page
{
    private DataTable _dt = new DataTable();
    private readonly Campus _oo = new Campus();
    private string _perType = "";
    private string _sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Write("<select id ='drpClass' class='form-control-blue validatedrp'>");
        Response.Write("<option value=''><--Select--></option>");
        if (Session["Logintype"].ToString() == "Admin")
        {
            _sql = "Select Id,ClassName,CidOrder from ClassMaster Where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            _sql = _sql + " and id in(select classid from ICSEClassGroupMaster where GroupName='G1' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + ")  Order by CIDOrder";
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
            _sql = "Select Distinct ClassName,cm.Id,CIDOrder from ICSEClassTeacherAllotment ctm";
            _sql = _sql + " inner join ClassMaster cm on cm.Id=ctm.ClassId and cm.SessionName=ctm.SessionName and cm.BranchCode=ctm.BranchCode";
            _sql = _sql + " where Ecode='" + Session["LoginName"].ToString() + "' ";
            _sql = _sql + " and ctm.SessionName='" + Session["SessionName"].ToString() + "' and ctm.BranchCode = " + Session["BranchCode"] + " ";
            _sql = _sql + " and cm.id in(select classid from ICSEClassGroupMaster where GroupName='G1' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + ")";
            _sql = _sql + " order by CIDOrder asc ";
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