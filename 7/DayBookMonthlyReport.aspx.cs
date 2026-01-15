using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;

public partial class DayBookMonthlyReport : System.Web.UI.Page
{
    private string _sql = "";
    Campus _oo = new Campus();
    private SqlConnection _con;
    protected void Page_Load(object sender, EventArgs e)
    {
        _con = new SqlConnection();
        _con = _oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);
        BLL.BLLInstance.LoadHeader("Report", header);
        if (!IsPostBack)
        {
            try
            {
                FillMonth();
            }
            catch (Exception)
            {
            }
        }
    }
    public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
    {
    }
    public void FillMonth()
    {
        _sql = "with months (date)";
        _sql += " AS (SELECT Convert(date, (Select FromDate from SessionMaster where SessionName = '" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "))";
        _sql += " UNION ALL";
        _sql += " SELECT DATEADD(month, 1, date)";
        _sql += " from months";
        _sql += " where date <= Convert(date, (Select ToDate from SessionMaster where SessionName = '" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + ")))";
        _sql += " select Left(Datename(month,date),3)+' '+Convert(Varchar(5),Datepart(YEAR,date)) monthname,Convert(Varchar(5),Datepart(month,date))+'-'+Convert(Varchar(5),Datepart(year,date)) monthvalue from months";
        _oo.FillDropDown_withValue(_sql, drpMonth, "monthname", "monthvalue");
        drpMonth.Items.Insert(0, "<--Select-->");
        var month = DateTime.Now.ToString("M-yyyy");
        drpMonth.SelectedValue = month;
    }
    DataSet ds = new DataSet();
    string[] column;
    private void GetMonthlyDayBookReport()
    {
        ds = new DataSet();
        gvDayBook.Columns.Clear();
        gvDayBook.DataSource = null;
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "DayBookMonthlyReport";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = _con;
            cmd.Parameters.AddWithValue("@Date", "01 " + drpMonth.SelectedItem);
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                _con.Open();
                adapter.Fill(ds);

                column = ds.Tables[1].Rows[0][0].ToString().Split(',');
                foreach (string str in column)
                {
                    BoundField testField = new BoundField
                    {
                        DataField = str
                    };
                    testField.HeaderStyle.CssClass ="v-top";
                    if (str.Split('_').Where((val) => val == "All").ToList().Count() > 0)
                    {
                        testField.HeaderText = str.Split('_')[0].Replace("#", "[").Replace("!", "]") + " (All)";
                    }
                    else if (str.Split('_').Where((val) => val == "Online").ToList().Count() > 0)
                    {
                        testField.HeaderText = str.Split('_')[0].Replace("#","[").Replace("!", "]") + " (Online)";
                    }
                    else
                    {
                        testField.HeaderText = str.Replace("#", "[").Replace("!", "]");
                    }

                    gvDayBook.Columns.Add(testField);
                }

                gvDayBook.DataSource = ds.Tables[0];
                gvDayBook.DataBind();

                gvDayBook.Font.Size = 9;
                gvDayBook.HeaderStyle.Font.Size = 10;
                gvDayBook.FooterStyle.Font.Size = 10;

                heading.Text = "Monthly Day Book Report of " + drpMonth.SelectedItem + "";
                lblRegister.Text = "Print by : " + Session["LoginName"].ToString() + " on " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

                abc.Visible = true;
                divExport.Visible = true;
            }
            catch (Exception ex)
            {
            }
        }
    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        _oo.ExportTolandscapeWord(Response, "DayBookMonthlyReport", gdv1);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        _oo.ExportDivToExcelWithFormatting(Response, "DayBookMonthlyReport.xls", gdv1, Server.MapPath("~/Admin/css/style.css"));
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {

        _oo.ExporttolandscapePdf(Response, "DayBookMonthlyReport", abc);
    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        GetMonthlyDayBookReport();
    }
    protected void gvDayBook_RowDataBound(object sender, GridViewRowEventArgs ge)
    {
        if (ge.Row.RowType == DataControlRowType.Footer)
        {
            ge.Row.Cells[0].Text = "Total";
            ge.Row.Cells[0].Font.Bold = true;
            ge.Row.Cells[0].Font.Size = 11;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                for (int j = 1; j < column.Count(); j++)
                {
                    decimal amount;
                    decimal.TryParse(ge.Row.Cells[j].Text, out amount);

                    decimal rowAmount;
                    decimal.TryParse(ds.Tables[0].Rows[i][j + 1].ToString(), out rowAmount);

                    ge.Row.Cells[j].Text = (amount + rowAmount).ToString();
                    ge.Row.Cells[j].Font.Bold = true;
                    ge.Row.Cells[j].Font.Size = 11;
                }
            }
        }
    }
}