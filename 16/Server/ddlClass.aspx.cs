using System;
using System.Data;

public partial class ddlClass : System.Web.UI.Page
{
    private DataTable _dt = new DataTable();
    private readonly Campus _oo = new Campus();
    private string _perType = "";
    private string _sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Write("<select id='drpclass' class='form-control-blue'>");
        Response.Write("<option value=''><--Select--></option>");
        if (Session["Logintype"].ToString() == "Admin")
        {
            _sql = "select id, ClassName from ClassMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and ClassName in ('X', 'XII') Order by CIDOrder";
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