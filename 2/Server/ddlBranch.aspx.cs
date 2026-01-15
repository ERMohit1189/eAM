using System;
using System.Data;

public partial class ddlBranch : System.Web.UI.Page
{
    private DataTable _dt = new DataTable();
    private DataSet _ds = new DataSet();
    private readonly Campus _oo = new Campus();
    private string _sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string drpclass = Request.Form["classId"].ToString();

        Response.Write("<select id = 'drpBranch' class='form-control-blue'>");
        Response.Write("<option value=''><--Select--></option>");
        _sql = "Select BranchName,Id from BranchMaster Where ClassId='" + drpclass.ToString() + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "'";
        _dt = DAL.DALInstance.GetValueInTable(_sql);

        if (_dt != null && _dt.Rows.Count > 0)
        {
            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                Response.Write("<option value='" + _dt.Rows[i]["id"].ToString() + "'>" + _dt.Rows[i]["BranchName"].ToString() + "</option>");
            }
        }
        Response.Write("</select>");
    }
}