using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class common_G7_ddlSrNo_7 : System.Web.UI.Page
{
    private DataTable _dt = new DataTable();
    private DataSet _ds = new DataSet();
    private readonly Campus _oo = new Campus();
    private string _sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string drpclass = Request.Form["classId"].ToString();
        string drpsection = Request.Form["SectionId"].ToString();
        string drpStatus = Request.Form["Status"].ToString();
        string BranchId = Request.Form["BranchId"].ToString();
        string StreamId = Request.Form["StreamId"].ToString();
        Response.Write("<select id = 'drpsrno' class='form-control-blue'>");
        Response.Write("<option value=''><--Select--></option>");
        List<SqlParameter> param = new List<SqlParameter>();
        _sql = @"Select Name+' - '+SrNo NAME,SrNo from AllStudentRecord_UDF(@SessionName,@BranchCode) where 
                @Classid=Classid and @Sectionid=CASE WHEN @Sectionid='' THEN @Sectionid ELSE Sectionid END  and isnull(Withdrwal,'') = case when isnull(@Withdrwal,'')='B' or isnull(@Withdrwal,'')='' then isnull(Withdrwal,'') else case when isnull(@Withdrwal,'')='A' then '' else 'W' end end
           and @BranchId=CASE WHEN @BranchId='' THEN @BranchId ELSE BranchId END and @StreamId=CASE WHEN @StreamId='' THEN @StreamId ELSE StreamId END and isnull(blocked,'') = case when isnull(@Withdrwal,'')='W' or isnull(@Withdrwal,'')='' then isnull(blocked,'') else case when isnull(@Withdrwal,'')='A' then '' else 'yes' end end  ORDER BY NAME";

        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
        param.Add(new SqlParameter("@Classid", drpclass.ToString()));
        param.Add(new SqlParameter("@Sectionid", drpsection.ToString()));
        param.Add(new SqlParameter("@BranchId", BranchId.ToString()));
        param.Add(new SqlParameter("@StreamId", StreamId.ToString()));
        if (drpStatus == "")
        {
            param.Add(new SqlParameter("@Withdrwal", null));
        }
        else
        {
            param.Add(new SqlParameter("@Withdrwal", drpStatus.ToString()));
        }

        _ds = DLL.objDll.SelectRecord_usingExecuteDataset(_sql, param);
        if (_ds.Tables[0] != null && _ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
            {
                Response.Write("<option value='" + _ds.Tables[0].Rows[i]["SrNo"].ToString() + "'>" + _ds.Tables[0].Rows[i]["NAME"].ToString() + "</option>");
            }
        }
        Response.Write("</select>");
    }

}