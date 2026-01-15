using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

// ReSharper disable once CheckNamespace
public partial class MonthwisePunchReport : Page
{
    Campus oo = new Campus();
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file
        BLL.BLLInstance.LoadHeader("Report", header);
        if (!IsPostBack)
        {
            sql = "select DesId, DesName from DesMaster where BranchCode=" + Session["BranchCode"] + "";
            oo.FillDropDown_withValue(sql, drpDesignation, "DesName", "DesName");
            drpDesignation.Items.Insert(0, new ListItem("<--Select-->", ""));

            sql = "SELECT YEAR(getdate()) curYear";
            int back5Year = int.Parse(oo.ReturnTag(sql, "curYear")) - 4;
            int CuYear = int.Parse(oo.ReturnTag(sql, "curYear"));
            for (int i = back5Year; i <= CuYear; i++)
            {
                ddlYear.Items.Add(i.ToString());
            }
            ddlYear.SelectedValue = CuYear.ToString();
            sql = "SELECT format(GETDATE(), 'MMM')curMonth";
            ddlMonth.SelectedValue = oo.ReturnTag(sql, "curMonth");

            GetMonthwisePunch();
        }
        if (grdMonthlyPunch.Rows.Count > 0)
        {
            grdMonthlyPunch.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        lblTitle.Text = ddlMonth.SelectedValue + " " + ddlYear.SelectedValue + " Attendance Report";
    }

    private void GetMonthwisePunch()
    {
        grdMonthlyPunch.DataSource = null;
        grdMonthlyPunch.DataBind();
        GridConsolidated.DataSource = null;
        GridConsolidated.DataBind();


        if (rdoType.SelectedIndex == 0)
        {
            sql = "exec USP_MonthwisePunchReport @Month=@Month,@Year=@Year,@BranchCode=@BranchCode,@Type=@Type,@Designation=@Designation,@EmpId=@EmpId";
            var cmd = new SqlCommand()
            {
                Connection = BAL.objBal.dbGet_connection(),
                CommandText = sql,
                CommandType = CommandType.Text
            };
            cmd.Parameters.Add(new SqlParameter("@Month", ddlMonth.SelectedValue));
            cmd.Parameters.Add(new SqlParameter("@Year", ddlYear.SelectedValue));
            cmd.Parameters.Add(new SqlParameter("@BranchCode", Session["BranchCode"]));
            cmd.Parameters.Add(new SqlParameter("@Type", rdoType.SelectedValue));
            if (drpDesignation.SelectedIndex != 0)
            {
                cmd.Parameters.Add(new SqlParameter("@Designation", drpDesignation.SelectedValue));

            }
            else
            {
                cmd.Parameters.Add(new SqlParameter("@Designation", "0"));
            }
            cmd.Parameters.Add(new SqlParameter("@EmpId", ""));
            var da = new SqlDataAdapter { SelectCommand = cmd };
            var dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                divExport1.Visible = true;
                grdMonthlyPunch.DataSource = dt;
                grdMonthlyPunch.DataBind();
            }
        }
        else
        {

            sql = "select EmpId, Name, DesNameNew from GetAllStaffRecords_UDF(" + Session["BranchCode"] + ") where ISNULL(Withdrwal,'')='' ";
            if (drpDesignation.SelectedIndex != 0)
            {
                sql += " and DesNameNew='" + drpDesignation.SelectedValue + "' ";
            }
            sql += " order by name asc ";
            var dt = oo.Fetchdata(sql);
            if (dt.Rows.Count > 0)
            {
                divExport1.Visible = true;
                GridConsolidated.DataSource = dt;
                GridConsolidated.DataBind();
                for (int i = 0; i < GridConsolidated.Rows.Count; i++)
                {
                    string sql1 = "";
                    sql1 = "exec USP_MonthwisePunchReport @Month=@Month,@Year=@Year,@BranchCode=@BranchCode,@Type=@Type,@Designation=@Designation,@EmpId=@EmpId";
                    var cmd = new SqlCommand()
                    {
                        Connection = BAL.objBal.dbGet_connection(),
                        CommandText = sql1,
                        CommandType = CommandType.Text
                    };
                    cmd.Parameters.Add(new SqlParameter("@Month", ddlMonth.SelectedValue));
                    cmd.Parameters.Add(new SqlParameter("@Year", ddlYear.SelectedValue));
                    cmd.Parameters.Add(new SqlParameter("@BranchCode", Session["BranchCode"]));
                    cmd.Parameters.Add(new SqlParameter("@Type", rdoType.SelectedValue));
                    if (drpDesignation.SelectedIndex != 0)
                    {
                        cmd.Parameters.Add(new SqlParameter("@Designation", drpDesignation.SelectedValue));
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@Designation", "0"));
                    }
                    cmd.Parameters.Add(new SqlParameter("@EmpId", dt.Rows[i]["EmpId"].ToString()));
                    var da2 = new SqlDataAdapter { SelectCommand = cmd };
                    var dt2 = new DataTable();
                    da2.Fill(dt2);
                    if (dt2.Rows.Count > 0)
                    {
                        Label TotalDaysOfMonth = (Label)GridConsolidated.Rows[i].FindControl("TotalDaysOfMonth");
                        TotalDaysOfMonth.Text = dt2.Rows[0]["TotalDaysOfMonth"].ToString();
                        Label Present = (Label)GridConsolidated.Rows[i].FindControl("Present");
                        Present.Text = dt2.Rows[0]["PCount"].ToString();
                        Label HalfDay = (Label)GridConsolidated.Rows[i].FindControl("HalfDay");
                        HalfDay.Text = dt2.Rows[0]["HDCount"].ToString();
                        Label ShortLeave = (Label)GridConsolidated.Rows[i].FindControl("ShortLeave");
                        ShortLeave.Text = dt2.Rows[0]["SLCount"].ToString();
                        Label Late = (Label)GridConsolidated.Rows[i].FindControl("Late");
                        Late.Text = dt2.Rows[0]["LTCount"].ToString();
                        Label Absent = (Label)GridConsolidated.Rows[i].FindControl("Absent");
                        Absent.Text = dt2.Rows[0]["ACount"].ToString();
                        Label Holiday = (Label)GridConsolidated.Rows[i].FindControl("Holiday");
                        Holiday.Text = dt2.Rows[0]["Holiday"].ToString();
                        Label Sandwitch = (Label)GridConsolidated.Rows[i].FindControl("Sandwitch");
                        Sandwitch.Text = dt2.Rows[0]["SWLCount"].ToString();
                        Label AchievDays = (Label)GridConsolidated.Rows[i].FindControl("AchievDays");
                        AchievDays.Text = dt2.Rows[0]["AchievDays"].ToString();
                        Label Leaves = (Label)GridConsolidated.Rows[i].FindControl("Leaves");
                        Leaves.Text = dt2.Rows[0]["Leaves"].ToString();
                        Label PaidDays = (Label)GridConsolidated.Rows[i].FindControl("PaidDays");
                        PaidDays.Text = dt2.Rows[0]["PaidDays"].ToString();
                    }
                }
            }
        }

    }
    protected void drpDesignation_SelectedIndexChanged(object sender, EventArgs e)
    {
        grdMonthlyPunch.DataSource = null;
        grdMonthlyPunch.DataBind();
        GridConsolidated.DataSource = null;
        GridConsolidated.DataBind();
        divExport1.Visible = false;
    }
    protected void lnkexcel_OnClick(object sender, EventArgs e)
    {
        BAL.objBal.ExportDivToExcel(Response, "MonthlyAttendanceReport" + BAL.objBal.CurrentDate() + ".xls", divExport);
    }

    protected void lnkword_OnClick(object sender, EventArgs e)
    {
        BAL.objBal.ExportTolandscapeWord(Response, "MonthlyAttendanceReport" + BAL.objBal.CurrentDate(), divExport);
    }

    protected void lnkpdf_OnClick(object sender, EventArgs e)
    {
        BAL.objBal.ExporttolandscapePdf(Response, "MonthlyAttendanceReport" + BAL.objBal.CurrentDate(), divExport);
    }

    protected void lnkprint_OnClick(object sender, EventArgs e)
    {

        PrintHelper_New.ctrl = divExport;
        ScriptManager.RegisterClientScriptBlock(Page, GetType(), "onclick", "var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}", true);
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected void lnkSubmit_OnClick(object sender, EventArgs e)
    {
        GetMonthwisePunch();
    }
    protected void grdMonthlyPunch_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            for (var i = 2; i < e.Row.Cells.Count; i++)
            {
                if (e.Row.Cells[i].Text.Trim() == "&nbsp;")
                {
                    e.Row.Cells[i].BackColor = Color.FromArgb(203, 203, 63);
                }
                else
                {
                    e.Row.Cells[i].BackColor = ColorTranslator.FromHtml("#00c300");
                    e.Row.Cells[i].ForeColor = Color.Black;
                }
            }
        }
    }
}