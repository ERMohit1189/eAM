using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class ddlSrNo : System.Web.UI.Page
{
    private DataTable _dt = new DataTable();
    private readonly Campus _oo = new Campus();
    private string _sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string drpclass = Request.Form["classId"].ToString();
        string drpsection = Request.Form["SectionId"].ToString();
        string BranchId = Request.Form["BranchId"].ToString();
        string drpStatus = Request.Form["Status"].ToString();
        Response.Write("<select id = 'drpsrno' class='form-control-blue'>");
        Response.Write("<option value=''><--Select--></option>");
        _sql = "Select Name+' - '+SrNo NAME,SrNo from AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "', " + Session["BranchCode"] + ") where ClassId=" + drpclass + " and BranchId=" + BranchId + " "; 
        _sql += "and SectionID=(Case when '" + drpsection + "'='' then SectionID else '" + drpsection + "' end) and SessionName = '" + Session["SessionName"].ToString() + "' ";
        _sql += "and isnull(Withdrwal,'') = (case when '" + drpStatus + "' = 'B' or '" + drpStatus + "' = '' then isnull(Withdrwal,'') else case when '" + drpStatus + "' = 'A' then '' else 'W' end end) ";
        _sql += "and isnull(blocked,'') = (case when '" + drpStatus + "'='W' or '" + drpStatus + "'='' then isnull(blocked,'') else case when '" + drpStatus + "'='A' then '' else 'yes' end end) order by name asc";
        _dt = DAL.DALInstance.GetValueInTable(_sql);
        if (_dt != null && _dt.Rows.Count > 0)
        {

            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                Response.Write("<option value='" + _dt.Rows[i]["SrNo"].ToString() + "'>" + _dt.Rows[i]["NAME"].ToString() + "</option>");
            }
        }
        Response.Write("</select>");
    }

}