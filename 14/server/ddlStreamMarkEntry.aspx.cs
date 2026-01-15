using System;
using System.Data;

public partial class ddlStreamMarkEntry : System.Web.UI.Page
{
    private DataTable _dt = new DataTable();
    private readonly Campus _oo = new Campus();
    private string _perType = "";
    private string _sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string classId = Request.Form["classId"].ToString();
        string BranchId = Request.Form["BranchId"].ToString();
        Response.Write("<select id ='drpStream' class='form-control-blue'>");
        Response.Write("<option value=''><--Select--></option>");
        if (Session["Logintype"].ToString() == "Admin")
        {
            _sql = "Select Distinct Stream,Id from  StreamMaster ";
            _sql = _sql + " where SessionName='" + Session["SessionName"] + "' and BranchCode = " + Session["BranchCode"] + " and classid=" + classId + " and Branchid=" + BranchId + "";
            _dt = DAL.DALInstance.GetValueInTable(_sql);

            if (_dt != null && _dt.Rows.Count > 0)
            {
                for (int i = 0; i < _dt.Rows.Count; i++)
                {
                    Response.Write("<option value='" + _dt.Rows[i]["id"].ToString() + "'>" + _dt.Rows[i]["Stream"].ToString() + "</option>");
                }
            }
        }
        else
        {
            _sql = "Select Distinct Stream,Id from  StreamMaster ";
            _sql = _sql + " where SessionName='" + Session["SessionName"] + "' and BranchCode = " + Session["BranchCode"] + " and classid=" + classId + " and Branchid=" + BranchId + "";
            _dt = DAL.DALInstance.GetValueInTable(_sql);

            if (_dt != null && _dt.Rows.Count > 0)
            {
                for (int i = 0; i < _dt.Rows.Count; i++)
                {
                    Response.Write("<option value='" + _dt.Rows[i]["id"].ToString() + "'>" + _dt.Rows[i]["Stream"].ToString() + "</option>");
                }
            }
        }

        Response.Write("</select>");
    }
}