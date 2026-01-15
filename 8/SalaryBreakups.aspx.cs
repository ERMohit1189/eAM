using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class SalaryBreakups : Page
{
    public string MSG = "", SQL = "";
    public static int H01ID = 0;
    public DataTable dt = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        //scriptManager.RegisterPostBackControl(this.g);

        Campus camp = new Campus(); camp.LoadLoader(loader);
        BLL.BLLInstance.LoadHeader("Report", header);
        if (!IsPostBack)
        {
            GetDepartment();
            GetDesignation();
            GetEmp();
        }
    }

    private void GetDesignation()
    {
        dt = new DataTable();
        dt = DAL.DALInstance.GetValueInTable("SELECT *FROM EmpDesMaster where  BranchCode=" + Session["BranchCode"].ToString() + "");

        ddlDesignation.Items.Clear();
        if (dt != null && dt.Rows.Count > 0)
        {
            BLL.FillDropDown(ddlDesignation, dt, "EmpDesName", "EmpDesID", 'S');
        }
    }

    private void GetDepartment()
    {
        dt = new DataTable();
        dt = DAL.DALInstance.GetValueInTable("SELECT *FROM EmpDepMaster where  BranchCode=" + Session["BranchCode"].ToString() + "");

        ddlDepartment.Items.Clear();
        if (dt != null && dt.Rows.Count > 0)
        {
            BLL.FillDropDown(ddlDepartment, dt, "EmpDepName", "EmpDepID", 'S');
        }
    }

    public void GetEmp()
    {
        SQL = "Select eod.EmpId,egd.EFirstName+' '+egd.EMiddleName+' '+egd.ELastName Name";
        SQL = SQL + " from EmpployeeOfficialDetails eod inner";
        SQL = SQL + " join EmpGeneralDetail egd on egd.EmpId = eod.EmpId";
        SQL = SQL + " where Designation = '" + ddlDesignation.SelectedItem.Text + "' and egd.BranchCode=" + Session["BranchCode"].ToString() + " and eod.BranchCode=" + Session["BranchCode"].ToString() + " and DepartmentName= '" + ddlDepartment.SelectedItem.Text + "' and eod.Withdrwal is null";
        SQL = SQL + " order by EFirstName";

        dt = new DataTable();
        dt = DAL.DALInstance.GetValueInTable(SQL);
        if (dt != null && dt.Rows.Count > 0)
        {
            BLL.FillDropDown(ddlEmpID, dt, "Name", "EmpId", 'A');
        }
    }

    //private void GetVendorType()
    //{
    //    BAL.clsVendorType obj = new BAL.clsVendorType();

    //    obj.M04ID = (Int32)Default.All;
    //    obj.IsActive = (Int32)Default.Yes;
    //    obj.VendorType = string.Empty;

    //    dt = null;
    //    dt = new DAL().GetVendorType(obj);

    //    if (dt != null && dt.Rows.Count > 0)
    //    {
    //        BLL.FillDropDown(ddlVendorType, dt, "VendorType", "M04ID", 'S');
    //    }
    //}

    private void GetSalaryBreakups()
    {
        SQL = "Select eod.EmpID,egd.EFirstName+' '+egd.EMiddleName+' '+egd.ELastName EmpName";
        SQL = SQL + " from EmpployeeOfficialDetails eod inner";
        SQL = SQL + " join EmpGeneralDetail egd on egd.EmpId = eod.EmpId";
        SQL = SQL + " where Designation = '" + ddlDesignation.SelectedItem.Text + "' and DepartmentName= '" + ddlDepartment.SelectedItem.Text + "' and egd.BranchCode=" + Session["BranchCode"].ToString() + " and eod.BranchCode=" + Session["BranchCode"].ToString() + "";
        SQL = SQL + " and '" + ddlEmpID.SelectedValue + "' = CASE WHEN '" + ddlEmpID.SelectedValue + "' = '-1' THEN '" + ddlEmpID.SelectedValue + "' ELSE eod.EmpID END and eod.Withdrwal is null";
        SQL = SQL + " order by EFirstName";

        DataTable dt = new DataTable();
        dt = DAL.DALInstance.GetValueInTable(SQL);

        if (dt != null && dt.Rows.Count > 0)
        {
            repSalaryBreakups.DataSource = dt;
            repSalaryBreakups.DataBind();
        }
        else
        {
            repSalaryBreakups.DataSource = null;
            repSalaryBreakups.DataBind();
        }
    }

  
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetSalaryBreakups();
    }


    protected void repSalaryBreakups_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        monthlysalary = 0;
        yearlysalary = 0;
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            GridView gv = e.Item.FindControl("gvComponents") as GridView;
            Label lbl = e.Item.FindControl("lblEmpID") as Label;

            DataSet ds = new DataSet();
            ds = DAL.DALInstance.GetSalaryBreakups(lbl.Text.Trim(), Session["SessionName"].ToString());

            if (ds != null && ds.Tables.Count > 0)
            {
                gv.DataSource = ds.Tables[0];
                gv.DataBind();
            }
            else
            {
                gv.DataSource = null;
                gv.DataBind();
            }
        }
    }

    decimal monthlysalary;
    decimal yearlysalary;
    protected void gvComponents_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblComponentType = (Label)e.Row.FindControl("lblComponentType");
            if (lblComponentType.Text == "E")
            {
                lblComponentType.Text = "Total";
                lblComponentType.Style.Add("font-weight", "bold");
            }
            if (lblComponentType.Text != "Total")
            {
                decimal value = 0;
                Label lblMonthly = (Label)e.Row.FindControl("lblMonthly");
                decimal.TryParse(lblMonthly.Text.Trim(), out value);
                monthlysalary = monthlysalary + value;

                Label lblAnnual = (Label)e.Row.FindControl("lblAnnual");
                decimal.TryParse(lblAnnual.Text.Trim(), out value);
                yearlysalary = yearlysalary + value;
            }
        }
        if(e.Row.RowType== DataControlRowType.Footer)
        {
            Label lblTotalMonthly = (Label)e.Row.FindControl("lblTotalMonthly");
            Label lblTotalAnnual = (Label)e.Row.FindControl("lblTotalAnnual");

            lblTotalMonthly.Text = monthlysalary.ToString(CultureInfo.InvariantCulture);
            lblTotalAnnual.Text = yearlysalary.ToString(CultureInfo.InvariantCulture);
        }
    }

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetEmp();
    }

    protected void ddlDesignation_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetEmp();
    }
}