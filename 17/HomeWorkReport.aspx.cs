using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
public partial class _11_HomeWorkReport : System.Web.UI.Page
{
    private DataSet _ds;
    public _11_HomeWorkReport()
    {
        _ds = new DataSet();
    }
    protected void Page_PreInIt(object sender, EventArgs e)
    {
        if (Session["Logintype"] == null) { Response.Redirect("~/default.aspx"); }
        if ((string)Session["Logintype"] == "Admin")
        {
            MasterPageFile = "~/Master/admin_root-manager.master";
        }
        else if ((string)Session["Logintype"] == "Staff")
        {
            MasterPageFile = "~/Staff/staff_root-manager.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtFromDate.Text = BAL.objBal.CurrentDate("yyyy MMM dd");
            txtToDate.Text = BAL.objBal.CurrentDate("yyyy MMM dd");
            LoadClass(drpClass);
            LoadGrid("-1");
        }
    }
    private void LoadClass(DropDownList drpclass)
    {
        if (Session["logintype"].ToString() == "Admin")
        {
            BLL.BLLInstance.loadClass(drpclass, Session["SessionName"].ToString());
        }
        else
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
            param.Add(new SqlParameter("@EmpCode", Session["LoginName"].ToString()));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));

            drpclass.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("GetClassTeacherClassName_Proc", param);
            drpclass.DataTextField = "ClassName";
            drpclass.DataValueField = "Id";
            drpclass.DataBind();
            drpclass.Items.Insert(0, new ListItem("<--Select-->", "-1"));
        }
    }
    private void LoadGrid(string id)
    {
        List<SqlParameter> param = new List<SqlParameter>();

        param.Add(new SqlParameter("@Id", id));
        param.Add(new SqlParameter("@ClassId", drpClass.SelectedValue));
        param.Add(new SqlParameter("@BranchId", "-1"));
        param.Add(new SqlParameter("@SectionId", ""));
        param.Add(new SqlParameter("@FromDate", txtFromDate.Text.Trim()));
        param.Add(new SqlParameter("@ToDate", txtToDate.Text.Trim()));
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
        param.Add(new SqlParameter("@QueryFor", "R"));

        _ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_Student_HomeWork", param);
        if (_ds.Tables[0].Rows.Count > 0)
        {
            grdDocList.DataSource = _ds;
            grdDocList.DataBind();
        }
        else
        {
            grdDocList.DataSource = null;
            grdDocList.DataBind();
        }
    }
    protected void lnkSubmit_OnClick(object sender, EventArgs e)
    {
        LoadGrid("-1");
    }
    public override void Dispose()
    {
        _ds.Dispose();
    }
    
}