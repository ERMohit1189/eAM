using c4SmsNew;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MonthlyReport : Page
{
    Campus oo = new Campus();
    protected void Page_PreInIt(object sender, EventArgs e)
    {
        if (Session["Logintype"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (Session["Logintype"].ToString() == "SuperAdmin")
        {
            MasterPageFile = "~/50/sadminRootManager.master";
        }

    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        Campus camp = new Campus(); camp.LoadLoader(loader);
        BLL.BLLInstance.LoadHeader("Report", header);
        if (!IsPostBack)
        {
            string sql = "Select BranchId, BranchName from Branchtab";
            var dt = oo.Fetchdata(sql);
            ddlBranch.DataSource = dt;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "BranchId";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
            if (Session["LoginType"].ToString() == "Admin")
            {
                divBranch.Visible = false;
                divSession.Visible = false;
                ddlBranch.SelectedValue = Session["BranchCode"].ToString();

                string sqls = "select SessionName from SessionMaster where BranchCode=" + ddlBranch.SelectedValue + "";
                var dt2 = oo.Fetchdata(sqls);
                DrpSessionName.DataSource = dt2;
                DrpSessionName.DataTextField = "SessionName";
                DrpSessionName.DataValueField = "SessionName";
                DrpSessionName.DataBind();
                DrpSessionName.Items.Insert(0, new ListItem("<--Select Session-->", ""));
                DrpSessionName.SelectedIndex = (DrpSessionName.Items.Count - 1);
                if (Session["LoginType"].ToString() == "Admin")
                {
                    ddlMonths.Items.Clear();
                    DrpSessionName.SelectedValue = Session["SessionName"].ToString();
                    string[] ss = DrpSessionName.SelectedValue.ToString().Split(new string[] { "-" }, StringSplitOptions.None);
                    string Sans1 = ss[0];
                    string Sans2 = ss[1];
                    ddlMonths.Items.Insert(0, new ListItem("Apr-" + Sans1, "Apr-" + Sans1));
                    ddlMonths.Items.Insert(1, new ListItem("May-" + Sans1, "May-" + Sans1));
                    ddlMonths.Items.Insert(2, new ListItem("Jun-" + Sans1, "Jun-" + Sans1));
                    ddlMonths.Items.Insert(3, new ListItem("Jul-" + Sans1, "Jul-" + Sans1));
                    ddlMonths.Items.Insert(4, new ListItem("Aug-" + Sans1, "Aug-" + Sans1));
                    ddlMonths.Items.Insert(5, new ListItem("Sep-" + Sans1, "Sep-" + Sans1));
                    ddlMonths.Items.Insert(6, new ListItem("Oct-" + Sans1, "Oct-" + Sans1));
                    ddlMonths.Items.Insert(7, new ListItem("Nov-" + Sans1, "Nov-" + Sans1));
                    ddlMonths.Items.Insert(8, new ListItem("Dec-" + Sans1, "Dec-" + Sans1));
                    ddlMonths.Items.Insert(9, new ListItem("Jan-" + Sans2, "Jan-" + Sans2));
                    ddlMonths.Items.Insert(10, new ListItem("Feb-" + Sans2, "Feb-" + Sans2));
                    ddlMonths.Items.Insert(11, new ListItem("Mar-" + Sans2, "Mar-" + Sans2));
                }
                
            }

            DrpSessionName.Items.Insert(0, new ListItem("<--Select-->", ""));

            sql = "Select id, ClassName from ClassMaster";
            sql = sql + "  where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            sql = sql + "  order by Id";

            
        }
        
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        if (ddlBranch.SelectedIndex==0)
        {
            DrpSessionName.Items.Clear();
            DrpSessionName.Items.Insert(0, new ListItem("<--Select Session-->", ""));
            return;
        }
        string sql = "select SessionName from SessionMaster where BranchCode=" + ddlBranch.SelectedValue + "";
        var dt2 = oo.Fetchdata(sql);
        DrpSessionName.DataSource = dt2;
        DrpSessionName.DataTextField = "SessionName";
        DrpSessionName.DataValueField = "SessionName";
        DrpSessionName.DataBind();
        DrpSessionName.Items.Insert(0, new ListItem("<--Select Session-->", ""));
        DrpSessionName.SelectedIndex = (DrpSessionName.Items.Count - 1);
        if (Session["LoginType"].ToString() == "Admin")
        {
            ddlMonths.Items.Clear();
            DrpSessionName.SelectedValue = Session["SessionName"].ToString();
            string[] ss = DrpSessionName.SelectedValue.ToString().Split(new string[] { "-" }, StringSplitOptions.None);
            string Sans1 = ss[0];
            string Sans2 = ss[1];
            ddlMonths.Items.Insert(0, new ListItem("Apr-" + Sans1, "Apr-" + Sans1));
            ddlMonths.Items.Insert(1, new ListItem("May-" + Sans1, "May-" + Sans1));
            ddlMonths.Items.Insert(2, new ListItem("Jun-" + Sans1, "Jun-" + Sans1));
            ddlMonths.Items.Insert(3, new ListItem("Jul-" + Sans1, "Jul-" + Sans1));
            ddlMonths.Items.Insert(4, new ListItem("Aug-" + Sans1, "Aug-" + Sans1));
            ddlMonths.Items.Insert(5, new ListItem("Sep-" + Sans1, "Sep-" + Sans1));
            ddlMonths.Items.Insert(6, new ListItem("Oct-" + Sans1, "Oct-" + Sans1));
            ddlMonths.Items.Insert(7, new ListItem("Nov-" + Sans1, "Nov-" + Sans1));
            ddlMonths.Items.Insert(8, new ListItem("Dec-" + Sans1, "Dec-" + Sans1));
            ddlMonths.Items.Insert(9, new ListItem("Jan-" + Sans2, "Jan-" + Sans2));
            ddlMonths.Items.Insert(10, new ListItem("Feb-" + Sans2, "Feb-" + Sans2));
            ddlMonths.Items.Insert(11, new ListItem("Mar-" + Sans2, "Mar-" + Sans2));
        }
    }
    
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        LoadReport();
    }
    protected void LoadReport()
    {
        //ddlMonths.Items.Clear();
        //string[] ss = DrpSessionName.SelectedValue.ToString().Split(new string[] { "-" }, StringSplitOptions.None);
        //string Sans1 = ss[0];
        //string Sans2 = ss[1];
        //ddlMonths.Items.Insert(0, new ListItem("Jan-" + Sans2, "Jan-" + Sans2));
        //ddlMonths.Items.Insert(1, new ListItem("Feb-" + Sans2, "Feb-" + Sans2));
        //ddlMonths.Items.Insert(2, new ListItem("Mar-" + Sans2, "Mar-" + Sans2));
        //ddlMonths.Items.Insert(3, new ListItem("Apr-" + Sans1, "Apr-" + Sans1));
        //ddlMonths.Items.Insert(4, new ListItem("May-" + Sans1, "May-" + Sans1));
        //ddlMonths.Items.Insert(5, new ListItem("Jun-" + Sans1, "Jun-" + Sans1));
        //ddlMonths.Items.Insert(6, new ListItem("Jul-" + Sans1, "Jul-" + Sans1));
        //ddlMonths.Items.Insert(7, new ListItem("Aug-" + Sans1, "Aug-" + Sans1));
        //ddlMonths.Items.Insert(8, new ListItem("Sep-" + Sans1, "Sep-" + Sans1));
        //ddlMonths.Items.Insert(9, new ListItem("Oct-" + Sans1, "Oct-" + Sans1));
        //ddlMonths.Items.Insert(10, new ListItem("Nov-" + Sans1, "Nov-" + Sans1));
        //ddlMonths.Items.Insert(11, new ListItem("Dec-" + Sans1, "Dec-" + Sans1));

        string sql = "select id, classname from classmaster where SessionName='" + Session["SessionName"] + "' and branchcode=" + Session["BranchCode"] + "";
        var dt = oo.Fetchdata(sql);
        if (dt.Rows.Count>0)
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();
            Panel2.Visible = true;
            abc.Visible = true;
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                string classid = dt.Rows[j]["Id"].ToString();
                double gtotl = 0;
                string date = "01-" + ddlMonths.SelectedValue;
                string sq = "select day(eomonth('" + date + "')) as NoOfDays";
                int NoOfDays = int.Parse(oo.ReturnTag(sq, "NoOfDays"));
                for (int i = 1; i <= NoOfDays; i++)
                {
                    string depositDate = "0" + i + "-" + ddlMonths.SelectedItem.Text;
                    List<SqlParameter> param = new List<SqlParameter>();
                    param.Add(new SqlParameter("@DepositDate", depositDate));
                    if (DdlpaymentMode.SelectedIndex != 0)
                    {
                        param.Add(new SqlParameter("@PaymentMode", DdlpaymentMode.SelectedValue));
                    }
                    //if (drpStatus.SelectedIndex != 0)
                    //{
                        param.Add(new SqlParameter("@Status", drpStatus.SelectedValue));
                    //}
                    param.Add(new SqlParameter("@ClassId", classid));
                    param.Add(new SqlParameter("@BranchCode", ddlBranch.SelectedValue));
                    param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
                    DataSet ds = new DLL().Sp_SelectRecord_usingExecuteDataset("sp_MonthlyReport", param);
                    GridView1.Rows[j].Cells[(i + 1)].Text = (ds.Tables[0].Rows[0]["Amount"].ToString() == "" ? "0" : ds.Tables[0].Rows[0]["Amount"].ToString());
                    gtotl = gtotl + double.Parse(ds.Tables[0].Rows[0]["Amount"].ToString() == "" ? "0" : ds.Tables[0].Rows[0]["Amount"].ToString());
                }
                Label lblGtotal = (Label)GridView1.Rows[j].FindControl("lblGtotal");
                lblGtotal.Text = gtotl.ToString("0.00");
            }
            double tot = 0;
            for (int i = 2; i < GridView1.Columns.Count-1; i++)
            {
                double f = 0;
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    f+=double.Parse(GridView1.Rows[j].Cells[(i)].Text==""?"0": GridView1.Rows[j].Cells[(i)].Text);
                    if (i==2)
                    {
                      Label total=(Label)GridView1.Rows[j].FindControl("lblGtotal");
                      tot = tot + double.Parse(total.Text==""?"0": total.Text);
                   }
                }
                GridView1.FooterRow.Cells[(i)].Text = f.ToString("0.00");
            }
            Label ftotal = (Label)GridView1.FooterRow.FindControl("ftotal");
            ftotal.Text= tot.ToString("0.00");
            
            heading.Text = "Monthly Fee Report (" + ddlMonths.SelectedValue + ")";
            string sqs = "select format(getdate(), 'dd-MMM-yyyy hh:mm:ss tt') getdate";
            lblRegister.Text = "Date : " + oo.ReturnTag(sqs, "getdate") + (DdlpaymentMode.SelectedIndex == 0 ? "" : " | Mode : " + DdlpaymentMode.SelectedItem.Text) + (drpStatus.SelectedIndex == 0 ? "" : " | Status : " + drpStatus.SelectedItem.Text);
        }
    }
    
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        LoadReport();
        oo.ExportTolandscapeWord(Response, "MonthlyReport", gdv1);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        LoadReport();
        oo.ExportDivToExcelWithFormatting(Response, "MonthlyReport.xls", gdv1, Server.MapPath("~/Admin/css/style.css"));
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        LoadReport();
        oo.ExporttolandscapePdf(Response, "MonthlyReport", abc);
    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        LoadReport();
        ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }
    
}