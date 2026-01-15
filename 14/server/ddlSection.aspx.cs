using System;
using System.Data;

public partial class ddlSection : System.Web.UI.Page
{
    private DataTable _dt = new DataTable();
    private readonly Campus _oo = new Campus();
    private string _perType = "";
    private string _sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string drpclass = Request.Form["classId"].ToString();

        Response.Write("<select id ='drpSection' class='form-control-blue validatedrp'>");
        Response.Write("<option value=''><--Select--></option>");


        if (Session["Logintype"].ToString() == "Admin")
        {
            _sql = "Select SectionName,id from SectionMaster where ClassNameId='" + drpclass.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and  BranchCode = " + Session["BranchCode"] + "";
            _dt = DAL.DALInstance.GetValueInTable(_sql);

            if (_dt != null && _dt.Rows.Count > 0)
            {
                for (int i = 0; i < _dt.Rows.Count; i++)
                {
                    Response.Write("<option value='" + _dt.Rows[i]["id"].ToString() + "'>" + _dt.Rows[i]["SectionName"].ToString() + "</option>");
                }
            }
        }
        else
        {
            _sql = " Select Distinct sm.SectionName,sm.id from ICSEClassTeacherAllotment sctm";
            _sql = _sql + " inner join SectionMaster sm on sm.ClassNameId=sctm.ClassId and sm.Id=sctm.SectionId and sm.SessionName=sctm.SessionName and sm.BranchCode=sctm.BranchCode ";
            _sql = _sql + " where sctm.BranchCode=" + Session["BranchCode"].ToString() + " and sctm.BranchCode = " + Session["BranchCode"] + " and ClassId='" + drpclass.ToString() + "' ";
            _sql = _sql + " and Ecode='" + Session["LoginName"].ToString() + "' ";

            _dt = DAL.DALInstance.GetValueInTable(_sql);
            if (_dt != null && _dt.Rows.Count > 0)
            {
                for (int i = 0; i < _dt.Rows.Count; i++)
                {
                    Response.Write("<option value='" + _dt.Rows[i]["id"].ToString() + "'>" + _dt.Rows[i]["SectionName"].ToString() + "</option>");
                }
            }
        }
        Response.Write("</select>");
    }
}