using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _2_Server_ddlFeeMedium : System.Web.UI.Page
{
    private DataTable _dt = new DataTable();
    private readonly Campus _oo = new Campus();
    private string _perType = "";
    private string _sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Write("<select id='ddlFeeType' class='form-control-blue'>");
        Response.Write("<option value='0'><--Select--></option>");
        if (Session["Logintype"].ToString() == "Admin")
        {
            _sql = "Select Distinct Medium from FeeAllotedForClassWise";
            _dt = DAL.DALInstance.GetValueInTable(_sql);

            if (_dt != null && _dt.Rows.Count > 0)
            {

                for (int i = 0; i < _dt.Rows.Count; i++)
                {
                    Response.Write("<option value='" + _dt.Rows[i]["Medium"].ToString() + "'>" + _dt.Rows[i]["Medium"].ToString() + "</option>");
                }
            }
        }
        Response.Write("</select>");
    }
}