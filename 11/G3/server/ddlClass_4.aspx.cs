using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _11_G3_server_ddlClass_4 : System.Web.UI.Page
{
    private DataTable _dt = new DataTable();
    private readonly Campus _oo = new Campus();
    private string _perType = "";
    private string _sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Write("<select id = 'drpclass' class='form-control-blue'>");
        Response.Write("<option value=''><--Select--></option>");
        if (Session["Logintype"].ToString() == "Admin" || Session["Logintype"].ToString() == "Guardian")
        {
            _sql = "Select Distinct ClassName,cm.Id as Id,CIDOrder from ClassMaster cm ";
            _sql += " inner join dt_ClassGroupMaster t1 on t1.ClassId=cm.Id";
            _sql += " where cm.SessionName='" + Session["SessionName"] + "' and cm.BranchCode=" + Session["BranchCode"] + "  and T1.BranchCode=" + Session["BranchCode"] + " and t1.SessionName='" + Session["SessionName"] + "' and GroupId='G4' Order by CIDOrder";
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
            _sql = "Select Distinct ClassName,cm.Id,CIDOrder from ClassTeacherMaster ctm";
            _sql += " inner join ClassMaster cm on cm.Id=ctm.ClassId and cm.SessionName=ctm.SessionName and cm.BranchCode=ctm.BranchCode";
            _sql += " where EmpCode='" + Session["LoginName"].ToString() + "' ";
            _sql += " and ctm.SessionName='" + Session["SessionName"].ToString() + "' and ctm.BranchCode = " + Session["BranchCode"] + " ";
            _sql += " and cm.id in(select ClassId from dt_ClassGroupMaster where GroupId='G4' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + ")";
            _sql += " order by CIDOrder asc ";

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