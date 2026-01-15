using System;
using System.Data;

public partial class ddlMedium_MarkEntry_5 : System.Web.UI.Page
{
    private DataTable _dt = new DataTable();
    private readonly Campus _oo = new Campus();
    private string _perType = "";
    private string _sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Write("<select id ='drpMedium' class='form-control-blue'>");
        Response.Write("<option value=''><--Select--></option>");
        _sql = "Select Distinct Medium from MediumMaster  where  BranchCode=" + Session["BranchCode"] + "";
        _dt = DAL.DALInstance.GetValueInTable(_sql);

        if (_dt != null && _dt.Rows.Count > 0)
        {
            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                Response.Write("<option value='" + _dt.Rows[i]["Medium"].ToString() + "'>" + _dt.Rows[i]["Medium"].ToString() + "</option>");
            }
        }
        Response.Write("</select>");
    }
}