using System;
using System.Data;

public partial class ddlActivity_MarkEntry_2 : System.Web.UI.Page
{
    private DataTable _dt = new DataTable();
    private readonly Campus _oo = new Campus();
    private string _sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string drpclass = Request.Form["classId"].ToString();
        string drpSubjectId = Request.Form["SubjectId"].ToString();
        string drpPaperId = Request.Form["PaperId"].ToString();

        Response.Write("<select id ='drpActivity' class='form-control-blue validatedrp'>");
        Response.Write("<option value=''><--Select--></option>");

        _sql = "Select ActivityName,Id from TTActivityMaster where ClassId=" + drpclass.ToString() + " and SubjectId=" + drpSubjectId.ToString() + "  and PaperId=" + drpPaperId.ToString() + " ";
        _sql +=  " and  BranchCode = " + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "'";
        _dt = DAL.DALInstance.GetValueInTable(_sql);
        if (_dt != null && _dt.Rows.Count > 0)
        {
            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                Response.Write("<option value='" + _dt.Rows[i]["id"].ToString() + "'>" + _dt.Rows[i]["ActivityName"].ToString() + "</option>");
            }
        }
        Response.Write("</select>");
    }
}