using System;
using System.Data;

public partial class ddlSrNo : System.Web.UI.Page
{
    private DataTable _dt = new DataTable();
    private DataSet _ds = new DataSet();
    private readonly Campus _oo = new Campus();
    private string _sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string classId = Request.Form["classId"].ToString();
        string SectionId = Request.Form["SectionId"].ToString();
        string BranchId = Request.Form["BranchId"].ToString();

        Response.Write("<select id ='drpsrno' class='form-control-blue'>");
        Response.Write("<option value=''><--Select--></option>");

        if (Session["Logintype"].ToString() == "Admin")
        {
            _sql = "Select Name+' - '+SrNo NAME,SrNo from AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "',1) where ClassId=" + classId.ToString() + " and SessionName='" + Session["SessionName"].ToString() + "'";
            _sql +=  " and SectionID=" + SectionId.ToString() + " and  BranchId=" + BranchId.ToString() + " order by Name asc ";
            _dt = DAL.DALInstance.GetValueInTable(_sql);

            if (_dt != null && _dt.Rows.Count > 0)
            {
                for (int i = 0; i < _dt.Rows.Count; i++)
                {
                    Response.Write("<option value='" + _dt.Rows[i]["SrNo"].ToString() + "'>" + _dt.Rows[i]["NAME"].ToString() + "</option>");
                }
            }
        }
        else
        {
            _sql = "Select Name+' - '+SrNo NAME,SrNo from AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "',1) where ClassId=" + classId.ToString() + " and SessionName='" + Session["SessionName"].ToString() + "'";
            _sql +=  " and SectionID=" + SectionId.ToString() + " and  BranchId=" + BranchId.ToString() + " order by Name asc ";
            _dt = DAL.DALInstance.GetValueInTable(_sql);

            if (_dt != null && _dt.Rows.Count > 0)
            {
                for (int i = 0; i < _dt.Rows.Count; i++)
                {
                    Response.Write("<option value='" + _dt.Rows[i]["SrNo"].ToString() + "'>" + _dt.Rows[i]["NAME"].ToString() + "</option>");
                }
            }
        }

        Response.Write("</select>");

    }

}