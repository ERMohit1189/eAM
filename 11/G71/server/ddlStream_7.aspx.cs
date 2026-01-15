using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class common_G7_ddlStream_7 : System.Web.UI.Page
{
    private DataTable _dt = new DataTable();
    private DataSet _ds = new DataSet();
    private readonly Campus _oo = new Campus();
    private string _sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string drpclass = Request.Form["classId"].ToString();
        string BranchId = Request.Form["Branchid"].ToString();

        Response.Write("<select id = 'drpStream' class='form-control-blue'>");
        Response.Write("<option value=''><--Select--></option>");
        _sql = "Select Stream,id from StreamMaster where ClassId='" + drpclass.ToString() + "' and BranchCode=" + Session["BranchCode"] + " and BranchId='" + BranchId.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "'  order by id";
        _dt = DAL.DALInstance.GetValueInTable(_sql);

        if (_dt != null && _dt.Rows.Count > 0)
        {
            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                Response.Write("<option value='" + _dt.Rows[i]["id"].ToString() + "'>" + _dt.Rows[i]["Stream"].ToString() + "</option>");
            }
        }
        Response.Write("</select>");
    }

}