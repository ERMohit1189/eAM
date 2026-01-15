using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;
using System.Globalization;


public partial class OtherFeeCollection : Page
{
    SqlConnection con = new SqlConnection();
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
        if (Session["SessionName"].ToString() == "2019-2020" || Session["SessionName"].ToString() == "2020-2021" || Session["SessionName"].ToString() == "2021-2022" || Session["SessionName"].ToString() == "2022-2023" || Session["SessionName"].ToString() == "2023-2024")
        {

        }
        else
        {
            Response.Redirect("OtherFeesCollection.aspx");
        }
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file
        BLL.BLLInstance.LoadHeader("Report", header); 

        if (!IsPostBack)
        {
            oo.AddDateMonthYearDropDown(DDYear, DDMonth, DDDate);
            oo.FindCurrentDateandSetinDropDown(DDYear, DDMonth, DDDate);

            oo.AddDateMonthYearDropDown(DDYearTo, DDMonthTo, DDDateTo);
            oo.FindCurrentDateandSetinDropDown(DDYearTo, DDMonthTo, DDDateTo);


            Panel1.Visible = false;
            abc.Visible = false;

            loadUser();
            string sql = "Select BranchId, BranchName from Branchtab";
            var dt = oo.Fetchdata(sql);
            ddlBranch.DataSource = dt;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "BranchId";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlBranch.SelectedValue = Session["BranchCode"].ToString();

            string sqls = "select SessionName from SessionMaster where BranchCode=" + ddlBranch.SelectedValue + "";
            var dt2 = oo.Fetchdata(sqls);
            drpSession.DataSource = dt2;
            drpSession.DataTextField = "SessionName";
            drpSession.DataValueField = "SessionName";
            drpSession.DataBind();
            drpSession.Items.Insert(0, new ListItem("<--All-->", ""));
            drpSession.SelectedValue=Session["SessionName"].ToString();
            if (Session["LoginType"].ToString() == "Admin")
            {
                divBranch.Visible = false;
                divSession.Visible = true;
            }
            else
            {
                divBranch.Visible = true;
                divSession.Visible = true;
            }
        }

    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadUser();
        if (ddlBranch.SelectedIndex == 0)
        {
            drpSession.Items.Clear();
            drpSession.Items.Insert(0, new ListItem("<--Select Session-->", ""));
            return;
        }
        string sql = "select SessionName from SessionMaster where BranchCode=" + ddlBranch.SelectedValue + "";
        var dt2 = oo.Fetchdata(sql);
        drpSession.DataSource = dt2;
        drpSession.DataTextField = "SessionName";
        drpSession.DataValueField = "SessionName";
        drpSession.DataBind();
        drpSession.Items.Insert(0, new ListItem("<--Select Session-->", ""));
        drpSession.SelectedIndex = (drpSession.Items.Count - 1);
        if (Session["LoginType"].ToString() == "Admin")
        {
            drpSession.SelectedValue = Session["SessionName"].ToString();
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

    public void Grid_fill()
    {
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
        int i;
        
        fromDate = DDYear.SelectedItem.ToString() + " " + DDMonth.SelectedItem.ToString() + " " + DDDate.SelectedItem.ToString();
        ToDate = DDYearTo.SelectedItem.ToString() + " " + DDMonthTo.SelectedItem.ToString() + " " + DDDateTo.SelectedItem.ToString();

        lbltitel.Text = "Other Fee Report from " + DateTime.Parse(fromDate).ToString("dd MMM yyyy") + " to " + DateTime.Parse(ToDate).ToString("dd MMM yyyy");

        string SessionName2 = "";

        SessionName2 = drpSession.SelectedIndex != 0 ? drpSession.SelectedValue : "";
        sql = "select asr.SrNo, OtherFeeDeposit.Id, OtherFeeDeposit.Receipt_no, format(OtherFeeDeposit.DepositDate,'dd-MMM-yyyy') as FeeDepositeDate, OtherFeeDeposit.Mode, asr.FatherName, ";
        sql= sql+ " asr.CombineClassName ClassName,OtherFeeDeposit.Srno,Name as StudentName, case when OtherFeeDeposit.PaymentSatus='Cancelled' then 'Cancelled' else OtherFeeDeposit.PaymentSatus end as Status,  ";
        sql = sql + " AmtForPay, case when OtherFeeDeposit.PaymentSatus='Paid' then isnull(BounceCharges, 0) else 0 end BounceCharges, Discount, case when OtherFeeDeposit.PaymentSatus='Cancelled' then 0 else PaidAmt end PaidAmt, NextDeuAmt from OtherFeeDeposit "; 
        sql = sql+ " inner join AllStudentRecord_UDF('"+ SessionName2+"', " + Session["BranchCode"]+") asr on asr.SrNo=OtherFeeDeposit.SrNo";
        sql = sql + " where Convert(date,OtherFeeDeposit.DepositDate)  between '" + fromDate + "' and '" + ToDate + "' ";
        if (drpSession.SelectedIndex!=0)
        {
            sql = sql + " and OtherFeeDeposit.SessionName='" + SessionName2 + "' ";
        }
        string sts = "";
        if (DdlpaymentMode.SelectedIndex != 0)
        {
            sts = sts + "Mode : " + DdlpaymentMode.SelectedValue;
            sql = sql + " and OtherFeeDeposit.Mode='" + DdlpaymentMode.SelectedValue + "' ";
        }
        if (ddlStatus.SelectedIndex != 0)
        {
            sql = sql + " and OtherFeeDeposit.PaymentSatus='" + ddlStatus.SelectedValue + "' ";
            if (DdlpaymentMode.SelectedIndex != 0)
            {
                sts = sts + " | Status : " + ddlStatus.SelectedValue;
            }
            else
            {
                sts = sts + "Status : " + ddlStatus.SelectedValue;
            }
        }
        if (DropDownList1.SelectedIndex != 0)
        {
            sql = sql + " and OtherFeeDeposit.LoginName='" + DropDownList1.SelectedValue + "' ";
            sts = sts + " by " + DropDownList1.SelectedValue;
        }
        sql = sql + " order By convert(date, OtherFeeDeposit.DepositDate), OtherFeeDeposit.Receipt_no asc";
        lbloptions.Text = sts;

        GridView2.DataSource = oo.GridFill(sql);
        GridView2.DataBind();
        if (GridView2.Rows.Count>0)
        {
            abc.Visible = true;
            double  Paid = 0;
            for (i = 0; i <= GridView2.Rows.Count - 1; i++)
            {
                Label lbl_Status = (Label)GridView2.Rows[i].FindControl("lbl_Status");
                Label lblPaidAmt = (Label)GridView2.Rows[i].FindControl("PaidAmt");
                if (lbl_Status.Text.Trim() == "Paid")
                {
                    Paid = Paid + double.Parse(lblPaidAmt.Text == "" ? "0" : lblPaidAmt.Text);
                }
            }
            Label PaidFoot = (Label)GridView2.FooterRow.FindControl("PaidFoot");
            PaidFoot.Text = Paid.ToString("0.00");
        }
        else
        {
            abc.Visible = false;
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, No Record(s) found!", "A");       
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        Grid_fill();
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
        if (GridView2.Rows.Count > 0)
        {
            oo.ExportToWord(Response, "OtherFeeCollection.doc", gdv);
        }
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        if (GridView2.Rows.Count > 0)
        {
            oo.ExportToExcel("OtherFeeCollection.xls", GridView2);
        }
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {

    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        if (GridView2.Rows.Count > 0)
        {
            GridView2.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }
    protected void LinkButton6_Click(object sender, EventArgs e)
    {
        LinkButton link = (LinkButton)sender;
        Label34.Text = link.Text;
        sql = "select Distinct Receipt_no ,convert(Varchar(11),OtherFeeDeposit.DepositDate,106) as FeeDepositeDate,OtherFeeDeposit.Srno,StudentGenaralDetail.FirstName+' '+StudentGenaralDetail.MiddleName+' '+StudentGenaralDetail.LastName as StudentName from OtherFeeDeposit inner join StudentGenaralDetail on StudentGenaralDetail.SrNo=OtherFeeDeposit.Srno where OtherFeeDeposit.Receipt_no='" + Label34.Text + "' Group By OtherFeeDeposit.Receipt_no,OtherFeeDeposit.Srno,OtherFeeDeposit.DepositDate,StudentGenaralDetail.FirstName,StudentGenaralDetail.MiddleName,StudentGenaralDetail.LastName ";
        Label35.Text = oo.ReturnTag(sql, "StudentName");
        Label36.Text = oo.ReturnTag(sql, "FeeDepositeDate");
        sql = "select ROW_NUMBER() over(order By OtherFeeDeposit.Id) as Id,Amount,OtherFeeHeadMaster.HeadName from OtherFeeDeposit inner join OtherFeeHeadMaster on OtherFeeHeadMaster.Id=OtherFeeDeposit.HeadId where OtherFeeDeposit.Receipt_no='" + link.Text + "' and OtherFeeDeposit.SessionName='" + drpSession.SelectedItem.Text + "' and OtherFeeHeadMaster.SessionName='" + drpSession.SelectedItem.Text + "'";
        GridView3.DataSource = oo.GridFill(sql);
        GridView3.DataBind();
        Panel2.Visible = true;
        Button5_ModalPopupExtender.Show();
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        Session["OtherReciept"] = Label34.Text;
        Response.Redirect("OtherFeeReciept_duplicate.aspx?print=1");
    }
   
}