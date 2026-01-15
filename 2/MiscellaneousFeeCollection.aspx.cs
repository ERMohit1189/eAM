using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;
using System.Globalization;


public partial class admin_MiscellaneousFeeCollection : Page
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
            oo.AddDateMonthYearDropDown(DDYear, DDMonth, DDDate);
            oo.FindCurrentDateandSetinDropDown(DDYear, DDMonth, DDDate);

            oo.AddDateMonthYearDropDown(DDYearTo, DDMonthTo, DDDateTo);
            oo.FindCurrentDateandSetinDropDown(DDYearTo, DDMonthTo, DDDateTo);


            lblDate.Visible = false;
            lblDate1.Visible = false;
            Panel1.Visible = false;

            lblTitle.Text = "From";
            lblTitle.Visible = false;

            lblTitle1.Text = "to";
            lblTitle1.Visible = false;
            
            Label1.Visible = false;
            abc.Visible = false;
        }

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
        double sum = 0;
        fromDate = DDYear.SelectedItem.ToString() + " " + DDMonth.SelectedItem.ToString() + " " + DDDate.SelectedItem.ToString();
        ToDate = DDYearTo.SelectedItem.ToString() + " " + DDMonthTo.SelectedItem.ToString() + " " + DDDateTo.SelectedItem.ToString();


        sql = "select Distinct other_fee_collection_1.Receipt_no,";
        sql = sql + "convert(Varchar(11),other_fee_collection_1.FeeDepositeDate,101) as FeeDepositeDate,sf.FatherName,cm.ClassName,";
        sql = sql + "other_fee_collection_1.Srno,StudentGenaralDetail.FirstName+' '+StudentGenaralDetail.MiddleName+' '+StudentGenaralDetail.LastName as StudentName,case when Other_fee_collection_1.Statas is null then 'Paid' else Other_fee_collection_1.Statas end as Status from other_fee_collection_1 "; 
        sql = sql+" inner join StudentGenaralDetail on StudentGenaralDetail.SrNo=other_fee_collection_1.Srno ";
        sql = sql + " inner join StudentFamilyDetails sf on sf.SrNo=other_fee_collection_1.Srno ";
        sql = sql + " inner join StudentOfficialDetails so on so.SrNo=other_fee_collection_1.Srno ";
        sql = sql + " inner join ClassMaster cm on cm.Id=so.AdmissionForClassId ";
        sql = sql + " where other_fee_collection_1.SessionName='" + Session["SessionName"].ToString() + "' and ";
        sql = sql + " StudentGenaralDetail.SessionName='" + Session["SessionName"].ToString() + "' and  ";
        sql = sql + " so.SessionName='" + Session["SessionName"].ToString() + "' and cm.SessionName='" + Session["SessionName"].ToString() + "' and sf.SessionName='" + Session["SessionName"].ToString() + "' and other_fee_collection_1.FeeDepositeDate  between '" + fromDate + "' and '" + ToDate + "' order By FeeDepositeDate Asc";
        GridView2.DataSource = oo.GridFill(sql);
        GridView2.DataBind();
        for (i = 0; i < GridView2.Rows.Count; i++)
        {
            Label lbl_sr = (Label)GridView2.Rows[i].FindControl("lbl_sr");
            Label lbl_date = (Label)GridView2.Rows[i].FindControl("lbl_date");
            Label lbl_total = (Label)GridView2.Rows[i].FindControl("lbl_total");
            LinkButton LinkButton6 = (LinkButton)GridView2.Rows[i].FindControl("LinkButton6");
            sql = "select SUM(other_fee_collection_1.ReceivedAmount) as TotalPaid from other_fee_collection_1 where Receipt_no='" + LinkButton6.Text + "' and sessionName='" + Session["SessionName"] + "'";
            lbl_sr.Text = (i + 1).ToString();
            lbl_total.Text = oo.ReturnTag(sql, "TotalPaid");
            DateTime date = DateTime.ParseExact(lbl_date.Text, "MM/dd/yyyy", new CultureInfo("en-US"));
            lbl_date.Text = date.ToString("dd MMM yyyy");
        }

        for (i = 0; i <= GridView2.Rows.Count - 1; i++)
        {
            Label lblSta = (Label)GridView2.Rows[i].FindControl("lbl_Status");
            Label lblAmt = (Label)GridView2.Rows[i].FindControl("lbl_total");


            if (lblSta.Text.Trim() == "Paid")
            {
                try
                {
                    sum = sum + Convert.ToDouble(lblAmt.Text);
                }
                catch (Exception) { }
            }


        }
        if (GridView2.Rows.Count > 0)
        {
            lblDate.Text = DDDate.SelectedItem.ToString() + " " + DDMonth.SelectedItem.ToString() + " " + DDYear.SelectedItem.ToString();
            lblDate1.Text = DDDateTo.SelectedItem.ToString() + " " + DDMonthTo.SelectedItem.ToString() + " " + DDYearTo.SelectedItem.ToString();

            lblDate.Visible = true;
            lblDate1.Visible = true;
            Panel1.Visible = true;
            lblTitle.Visible = true;
            lblTitle1.Visible = true;
            Label lblAmountTotal = (Label)GridView2.FooterRow.FindControl("lblAmountTotal");
            lblAmountTotal.Text = sum.ToString(CultureInfo.InvariantCulture);
            
            Label1.Visible = true;
            abc.Visible = true;
           
        }
        else
        {
            abc.Visible = false;
            //oo.MessageBox("Sorry, No Record(s) found!", this.Page);
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
            oo.ExportToWord(Response, "OtherFeeCollection1.doc", gdv);
        }
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        if (GridView2.Rows.Count > 0)
        {
            oo.ExportToExcel("OtherFeeCollection1.xls", GridView2);
        }
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {

    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }
    protected void LinkButton6_Click(object sender, EventArgs e)
    {
        LinkButton link = (LinkButton)sender;
        Label34.Text = link.Text;
        sql = "select Distinct Receipt_no ,convert(Varchar(11),other_fee_collection_1.FeeDepositeDate,106) as FeeDepositeDate,other_fee_collection_1.Srno,StudentGenaralDetail.FirstName+' '+StudentGenaralDetail.MiddleName+' '+StudentGenaralDetail.LastName as StudentName from other_fee_collection_1 inner join StudentGenaralDetail on StudentGenaralDetail.SrNo=other_fee_collection_1.Srno where other_fee_collection_1.Receipt_no='" + Label34.Text + "' Group By other_fee_collection_1.Receipt_no,other_fee_collection_1.Srno,other_fee_collection_1.FeeDepositeDate,StudentGenaralDetail.FirstName,StudentGenaralDetail.MiddleName,StudentGenaralDetail.LastName ";
        Label35.Text = oo.ReturnTag(sql, "StudentName");
        Label36.Text = oo.ReturnTag(sql, "FeeDepositeDate");
        sql = "select ROW_NUMBER() over(order By other_fee_collection_1.Id) as Id,Amount,other_fee_collection_1_head.HeadName from other_fee_collection_1 inner join other_fee_collection_1_head on other_fee_collection_1_head.Id=other_fee_collection_1.HeadId where other_fee_collection_1.Receipt_no='" + link.Text + "' and other_fee_collection_1.SessionName='" + Session["SessionName"].ToString() + "' and other_fee_collection_1_head.SessionName='" + Session["SessionName"].ToString() + "'";
        GridView3.DataSource = oo.GridFill(sql);
        GridView3.DataBind();
        Button5_ModalPopupExtender.Show();
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        Session["OtherReciept1"] = Label34.Text;
        Response.Redirect("MiscellaneousFeesReciept_duplicate.aspx?print=1");
    }
}