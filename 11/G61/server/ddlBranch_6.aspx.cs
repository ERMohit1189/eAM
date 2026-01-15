using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class common_G6_ddlBranch_6 : System.Web.UI.Page
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
        if (Session["Logintype"].ToString() == "Admin" || Session["Logintype"].ToString() == "Guardian")
        {
            _sql = "Select BranchName,Id from BranchMaster Where ClassId='" + drpclass.ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and SessionName='" + Session["SessionName"].ToString() + "'";
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
            _sql = "Select Distinct t1.BranchName, t1.Id  from ClassTeacherMaster ctm";
            _sql +=  " inner join BranchMaster t1 on t1.ClassId=ctm.ClassId and t1.id=ctm.BranchId  and t1.SessionName=ctm.SessionName and t1.BranchCode=ctm.BranchCode";
            _sql +=  " where EmpCode='" + Session["LoginName"].ToString() + "'";
            _sql +=  " and ctm.SessionName='" + Session["SessionName"].ToString() + "' and ctm.BranchCode = " + Session["BranchCode"] + "  and ctm.classid=" + drpclass + "";
            _dt = DAL.DALInstance.GetValueInTable(_sql);
            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                Response.Write("<option value='" + _dt.Rows[i]["id"].ToString() + "'>" + _dt.Rows[i]["BranchName"].ToString() + "</option>");
            }
        }
        Response.Write("</select>");
    }
}