using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;
using System.Globalization;


public partial class BalanceListfoOtherFee : Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";
    int mo = 0;
    
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
            Response.Redirect("OtherReportBalanceList.aspx");
        }
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file
        BLL.BLLInstance.LoadHeader("Report", header);
        Date.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        if (!IsPostBack)
        {
            LoadClass();
            abc.Visible = false;
        }
       
    }

   
    protected void LoadClass()
    {
        sql = "select ClassName,Id from ClassMaster where SessionName='" + Session["SessionName"].ToString() + "' order By CIDOrder";
        oo.FillDropDown_withValue(sql, drpClass, "ClassName", "Id");
        drpClass.Items.Insert(0, new ListItem("Select ALL", "Select ALL"));
    }
    public void Grid_fill()
    {
        


        int i; int sr=0;
        double sum = 0;

        int classId = 0;
        if (drpClass.SelectedValue == "Select ALL")
        {
            classId = 0;
            Class.Text = "All Classes";
        }
        else
        {
            classId=int.Parse(drpClass.SelectedValue);
            Class.Text = drpClass.SelectedItem.Text;
        }
           
        //int classId = 1;

        sql = "select * from AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "', " + Session["BranchCode"].ToString() + ") where SessionName='" + Session["SessionName"].ToString() + "' and  ClassId= case when " + classId + "=0 then ClassId else " + classId + " end and withdrwal is null and ISNULL(Promotion,'')<>'Cancelled' order by classid, name asc";
        GridView2.DataSource = oo.GridFill(sql);
        GridView2.DataBind();
        if (GridView2.Rows.Count>0)
        {
            divExportBTN.Visible = true;
        }
        else
        {
            divExportBTN.Visible = false;
        }
        for (i = 0; i < GridView2.Rows.Count; i++)
        {
            Label lbl_sr = (Label)GridView2.Rows[i].FindControl("lbl_sr");
            Label lbl_ClassId = (Label)GridView2.Rows[i].FindControl("lbl_ClassId");

            Label lbl_srno = (Label)GridView2.Rows[i].FindControl("lbl_srno");
            Label lbl_date = (Label)GridView2.Rows[i].FindControl("lbl_date");
            Label lbl_total = (Label)GridView2.Rows[i].FindControl("lbl_total");
            Label lbl_gender = (Label)GridView2.Rows[i].FindControl("lbl_gender");
            LinkButton LinkButton6 = (LinkButton)GridView2.Rows[i].FindControl("LinkButton6");
            int gengerId = 0;
            if (lbl_gender.Text.ToLower()=="male"){ gengerId = 1;}
            if (lbl_gender.Text.ToLower()=="female"){ gengerId = 2; }
            if (lbl_gender.Text.ToLower()== "transgender") { gengerId = 3; }
            sql = "select sum(Amount) PayableAmount from OtherFeeHeadMaster where SessionName='" + Session["SessionName"].ToString() + "' and ClassId=" + lbl_ClassId.Text + " and gender=" + gengerId + "";
            string PayableAmount = oo.ReturnTag(sql, "PayableAmount");

            sql = "select SUM(OtherFeeDeposit.PaidAmt) as TotalPaid, SUM(OtherFeeDeposit.Discount) Discount from OtherFeeDeposit where srno='" + lbl_srno.Text + "' and sessionName='" + Session["SessionName"].ToString() + "'";
            
            string PaidAmount = oo.ReturnTag(sql, "TotalPaid");
            string Discount = oo.ReturnTag(sql, "Discount");
            string totalAmt = "0"; 
            if (PaidAmount=="" || PaidAmount=="0")
            {
                PaidAmount = "0";
            }
            if (Discount == "" || Discount == "0")
            {
                Discount = "0";
            }
            if (PayableAmount == "" || PayableAmount == "0")
            {
                lbl_total.Text = "0";
            }
            else if (PayableAmount !="")
            {
                totalAmt = (double.Parse(PayableAmount)- (double.Parse(PaidAmount)+ double.Parse(Discount))).ToString("0");
            }
            lbl_total.Text = totalAmt;
            if (totalAmt == "0")
            {
                GridView2.Rows[i].Visible = false;
            }
            else
            {
                sr = sr + 1;
                lbl_sr.Text = sr.ToString();
                sum = sum + double.Parse(totalAmt);
            }
        }
        
        if (GridView2.Rows.Count > 0)
        {
            Label lblAmountTotal = (Label)GridView2.FooterRow.FindControl("lblAmountTotal");
            lblAmountTotal.Text = sum.ToString(CultureInfo.InvariantCulture);
            
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
        Grid_fill();
    }

    
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        if (GridView2.Rows.Count > 0)
        {
            oo.ExportToWord(Response, "BalanceListOfOtherFee.doc", gdv);
        }
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        if (GridView2.Rows.Count > 0)
        {
            oo.ExportToExcel("BalanceListOfOtherFee.xls", GridView2);
        }
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        if (GridView2.Rows.Count > 0)
        {
            oo.ExportToExcel("BalanceListOfOtherFee.pdf", GridView2);
        }
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
    
}