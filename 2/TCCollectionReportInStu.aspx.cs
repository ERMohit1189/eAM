using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;
using System.Globalization;

public partial class TCCollectionReportInStu : Page
{
    SqlConnection con = new SqlConnection();
    SqlConnection con1 = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";
#pragma warning disable 414
    int mo = 0;
#pragma warning restore 414

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    protected void Page_Load(object sender, EventArgs e)
    {
    
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file
        BLL.BLLInstance.LoadHeader("Report", header);
        if (!IsPostBack)
        {
            //oo.AddDateMonthYearDropDown(DDYear, DDMonth, DDDate);
            //oo.FindCurrentDateandSetinDropDown(DDYear, DDMonth, DDDate);

            //oo.AddDateMonthYearDropDown(DDYearTo, DDMonthTo, DDDateTo);
            //oo.FindCurrentDateandSetinDropDown(DDYearTo, DDMonthTo, DDDateTo);

            oo.AddDateMonthYearDropDown(DDYear, DDMonth, DDDate);
            oo.AddDateMonthYearDropDown(DDYearTo, DDMonthTo, DDDateTo);
            oo.FindCurrentDateandSetinDropDown(DDYear, DDMonth, DDDate);
            oo.FindCurrentDateandSetinDropDown(DDYearTo, DDMonthTo, DDDateTo);

            Panel1.Visible = false;
            lbltitel.Visible = false;
            abc.Visible = false;
            string sql = "Select BranchId, BranchName from Branchtab";
            var dt = oo.Fetchdata(sql);
            ddlBranch.DataSource = dt;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "BranchId";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
            loadUser();
            if (Session["LoginType"].ToString() == "Admin")
            {
                divBranch.Visible = false;
                ddlBranch.SelectedValue = Session["BranchCode"].ToString();
            }
        }
    }
    protected void loadUser()
    {
        string sql = "";
        if (Session["LoginType"].ToString() == "Admin")
        {
            sql = "Select UserId From NewAdminInformation where BranchCode=" + Session["BranchCode"] + "";
        }
        else
        {
            sql = "Select UserId From NewAdminInformation where BranchCode=" + ddlBranch.SelectedValue + "";
        }
        oo.FillDropDownWithOutSelect(sql, DropDownList1, "UserId");
        DropDownList1.Items.Insert(0, new ListItem("All", ""));
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        loadData();
    }
    protected void txtSrNo_TextChanged(object sender, EventArgs e)
    {
        loadData();
    }
    protected void loadData()
    {
        var studentId = Request.Form[hfStudentId.UniqueID];
        if (studentId == string.Empty)
        {
            studentId = txtSrNo.Text.Trim();
        }
        if (txtSrNo.Text == string.Empty)
        {
            studentId = "";
            txtSrNo.Text = "";
            hfStudentId.Value = "";
        }

        if (DDMonth.SelectedItem.ToString() == "Jan")
        {
            mo = 1;
        }
        else if (DDMonth.SelectedItem.ToString() == "Feb")
        {
            mo = 2;
        }
        else if (DDMonth.SelectedItem.ToString() == "Mar")
        {
            mo = 3;
        }
        else if (DDMonth.SelectedItem.ToString() == "Apr")
        {
            mo = 4;
        }
        else if (DDMonth.SelectedItem.ToString() == "May")
        {
            mo = 5;
        }
        else if (DDMonth.SelectedItem.ToString() == "Jun")
        {
            mo = 6;
        }
        else if (DDMonth.SelectedItem.ToString() == "Jul")
        {
            mo = 7;
        }
        else if (DDMonth.SelectedItem.ToString() == "Aug")
        {
            mo = 8;
        }
        else if (DDMonth.SelectedItem.ToString() == "Sep")
        {
            mo = 9;
        }
        else if (DDMonth.SelectedItem.ToString() == "Oct")
        {
            mo = 10;
        }
        else if (DDMonth.SelectedItem.ToString() == "Nov")
        {
            mo = 11;
        }
        else if (DDMonth.SelectedItem.ToString() == "Dec")
        {
            mo = 12;
        }

        string fromDate = "", ToDate = "";

        fromDate = DDYear.SelectedItem.ToString() + " " + DDMonth.SelectedItem.ToString() + " " + DDDate.SelectedItem.ToString();
        ToDate = DDYearTo.SelectedItem.ToString() + " " + DDMonthTo.SelectedItem.ToString() + " " + DDDateTo.SelectedItem.ToString();
        lbltitel.Text = "TC Fee Collection Report from " + DateTime.Parse(fromDate).ToString("dd MMM yyyy") + " to " + DateTime.Parse(ToDate).ToString("dd MMM yyyy");
        sql = " select Row_Number() over (order by Id Asc) as SNo ,id,srno,RecieptNo,format(TCIssueDate,'dd-MMM-yyyy') as TCIssueDate,format(AdmissionFromDate,'dd-MMM-yyyy') as AdmissionFromDate,StudentName,FatherName,Class,LoginName,BranchCode,convert(nvarchar,RecordDate,106) as RecordDate,Sex,FatherContactNo,ReceivedAmount,case when cancel IS NULL then 'No' else 'Yes' end as Cancel, mop, status, tcType, (select CombineClassName from AllStudentRecord_UDF(tc.SessionName,tc.BranchCode) asr where asr.SrNo=tc.srno) CombineClassName  from TCCollection tc";
        sql = sql + " where  Cancel is NULL and  convert(date, RecordDate) between '" + fromDate+"' and '"+ToDate+ "'";
        sql = sql + " and BranchCode=" + ddlBranch.SelectedValue + " ";
        if (studentId!="")
        {
            sql = sql + " and srno='" + studentId + "' ";
        }
        string sts = "";
        if (DdlpaymentMode.SelectedIndex != 0)
        {
            sts= sts+ "Mode : " + DdlpaymentMode.SelectedValue;
            sql = sql + " and mop='" + DdlpaymentMode.SelectedValue + "' ";
        }
        if (drpStatus.SelectedIndex != 0)
        {
            sql = sql + " and Status='" + drpStatus.SelectedValue + "' ";
            if (DdlpaymentMode.SelectedIndex != 0)
            {
                sts = sts + " | Status : " + drpStatus.SelectedValue;
            }
            else
            {
                sts = sts + "Status : " + drpStatus.SelectedValue;
            }
        }
        if (DropDownList1.SelectedIndex != 0)
        {
            sql = sql + " and LoginName='" + DropDownList1.SelectedValue + "' ";
            sts = sts+ " by " + DropDownList1.SelectedValue;
        }
        sql = sql + "  order by id desc";
        lbloptions.Text = sts;

        Grd.DataSource = oo.GridFill(sql);
        Grd.DataBind();

        if (Grd.Rows.Count == 0)
        {
            Panel1.Visible = false;
            abc.Visible = false;
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, No Record(s) found!", "A");
        }
        else
        {
            Panel1.Visible = true;
            abc.Visible = true;

        }
    }
    protected void DDYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(DDYear, DDMonth, DDDate);
    }
    protected void DDMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(DDYear, DDMonth, DDDate);
    }
    protected void DDDate_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
   protected void ImageButton1_Click(object sender, EventArgs e)
    {
        oo.ExportTolandscapeWord(Response, "TCReport", divExport);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        oo.ExportDivToExcelWithFormatting(Response, "TCReport.xls", divExport, Server.MapPath("~/Admin/css/style.css"));
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        oo.ExporttolandscapePdf(Response, "TCReport", abc);
    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }

    
}