using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class HolidayMaster : Page
{
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    Campus oo = new Campus();
    string sql = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file
        if (!IsPostBack)
        {
            loadYear();
            selectedMonth();
            loadGrid();
        }
    }

    public void selectedMonth()
    {
        sql = "Select format(getdate(),'yyyy') yearvalue";
        drpYear.SelectedValue = BAL.objBal.ReturnTag(sql, "yearvalue");
        sql = "Select format(getdate(),'MMM') monthvalue";
        drpMonth.SelectedValue = BAL.objBal.ReturnTag(sql, "monthvalue");
    }
    protected void loadYear()
    {
        sql = "Select years from(Select DatePart(YEAR,Getdate())-1 years Union Select DatePart(YEAR,Getdate()) years Union Select DatePart(YEAR,Getdate()) years)T1 order by years desc";
        BAL.objBal.FillDropDown_withValue(sql, drpYear, "years", "years");
    }
    protected void lnkview_Click(object sender, EventArgs e)
    {
        loadGrid();
    }
    public void loadGrid()
    {
        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@MonthName", drpMonth.SelectedValue.ToString().Trim()));
        param.Add(new SqlParameter("@year", drpYear.SelectedValue.ToString().Trim()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
        param.Add(new SqlParameter("@Action", "select"));

        rptEmp.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("SalaryMonthlyHolidayProc", param);
        rptEmp.DataBind();
        if (rptEmp.Items.Count > 0)
        {
            divExport.Visible = true;
            for (int i = 0; i < rptEmp.Items.Count; i++)
            {
                CheckBox chk = (CheckBox)rptEmp.Items[i].FindControl("chk");
                Label IsHoliday = (Label)rptEmp.Items[i].FindControl("IsHoliday");
                if (IsHoliday.Text == "True")
                {
                    chk.Checked = true;
                }
            }
        }
    }



    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        con.Close();
        int sts = 0;
        for (int i = 0; i < rptEmp.Items.Count; i++)
        {
            Label MonthDate = (Label)rptEmp.Items[i].FindControl("MonthDate");
            Label MonthDay = (Label)rptEmp.Items[i].FindControl("MonthDay");
            TextBox HolidayName = (TextBox)rptEmp.Items[i].FindControl("HolidayName");
            CheckBox chk = (CheckBox)rptEmp.Items[i].FindControl("chk");
            cmd = new SqlCommand();
            cmd.CommandText = "SalaryMonthlyHolidayProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@MonthName", drpMonth.SelectedValue);
            cmd.Parameters.AddWithValue("@MonthDate", MonthDate.Text.Trim());
            cmd.Parameters.AddWithValue("@MonthDay", MonthDay.Text.Trim());
            cmd.Parameters.AddWithValue("@year", drpYear.SelectedValue.ToString().Trim());
            cmd.Parameters.AddWithValue("@HolidayName", HolidayName.Text.Trim());
            cmd.Parameters.AddWithValue("@IsHoliday", (chk.Checked ? "1" : "0"));
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@Action", "insert");
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                con.Close();
                sts = sts + 1;
            }
            catch (SqlException ex)
            {
            }
        }
        if (sts > 0)
        {
            loadGrid();
            Campus camp = new Campus(); camp.msgbox(Page, divmsg1, "Submitted successfully.", "S");
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(Page, divmsg1, "Some technical problem, please contact to admin!", "A");
        }
    }

    protected void drpMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadGrid();
    }

    protected void drpYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadGrid();
    }
    protected void rptEmp_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            HtmlTableRow row = (HtmlTableRow)e.Item.FindControl("row");
            Label IsHoliday = (Label)e.Item.FindControl("IsHoliday");
            HiddenField IsPrevInsertedRecord = (HiddenField)e.Item.FindControl("IsPrevInsertedRecord");
            if (IsHoliday.Text == "True" && IsPrevInsertedRecord.Value == "True")
            {
                for (int i = 0; i < 3; i++)
                {
                    row.Cells[i].Attributes.Add("style", "background-color: #008140 !important; color: #fff !important");
                }
            }
        }
    }

    protected void chk_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)sender;
        RepeaterItem repeaterItem = (RepeaterItem)chk.NamingContainer;
        TextBox HolidayName = (TextBox)repeaterItem.FindControl("HolidayName");
        if (!chk.Checked)
        {
            HolidayName.Text = "";
        }
    }
}