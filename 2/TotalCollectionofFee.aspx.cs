using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;

public partial class admin_TotalCollectionofFee : Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";

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

        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file
        BLL.BLLInstance.LoadHeader("Report",header); 

        if (!IsPostBack)
        {
            ImageButton1.Visible = false;
            ImageButton2.Visible = false;
            ImageButton3.Visible = false;
            ImageButton4.Visible = false;

            lbltitel.Visible = false;
            
            sql = "Select FeeGroupName from FeeGroupMaster";
            oo.FillDropDown(sql, DrpGroup, "FeeGroupName");
            
            oo.AddDateMonthYearDropDown(FromYY, FromMM, FromDD);
            oo.AddDateMonthYearDropDown(ToYY, ToMM, ToDD);


            oo.FindCurrentDateandSetinDropDown(FromYY, FromMM, FromDD);
            oo.FindCurrentDateandSetinDropDown(ToYY, ToMM, ToDD);
            sql = "select MonthName from MonthMaster where CardType='" + DrpGroup.SelectedItem.ToString() + "'";
            sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            sql = sql + " or Monthid=0  ";
            sql = sql + " order by MonthId";

            oo.FillDropDownWithOutSelect(sql, DropDownMonth, "MonthName");
            Panel2.Visible = false;
            Label32.Text = "";
            LinkButton5.Visible = true;
            lblTitle.Text = "from ";
            lblTitle1.Text = "to ";
            if (RadioButtonList1.Items[0].Selected == true)
            {
                Panel1.Visible = true;
            }
            abc.Visible = false;
        }
    }
    protected void LinkButton5_Click(object sender, EventArgs e)
    {
        string todate = "", fromdate = "";

        //todate=ToYY.SelectedItem.ToString()+"/"+ToMM.SelectedItem.ToString()+"/"+ToDD.SelectedItem.ToString();
        //fromdate=FromYY.SelectedItem.ToString()+"/"+FromMM.SelectedItem.ToString()+"/"+FromDD.SelectedItem.ToString();

        todate = ToDD.SelectedItem.ToString() + " " + ToMM.SelectedItem.ToString() + " " + ToYY.SelectedItem.ToString();
        fromdate = FromDD.SelectedItem.ToString() + " " + FromMM.SelectedItem.ToString() + " " + FromYY.SelectedItem.ToString();

        lblDate.Text = fromdate;
        lblDate1.Text = todate;
        lblDate.Visible = true;
        lblDate1.Visible = true;
        Panel1.Visible = true;
        lblTitle.Visible = true;
        lblTitle1.Visible = true;

        sql = "  select max(cm.ClassName) as Class,sum(fd.RecievedAmount) as RecievedAmount,CIDOrder,fd.SessionName from ClassMaster cm";
        sql = sql + "   left join FeeDeposite fd on cm.ClassName=fd.Class and fd.SessionName=cm.SessionName";
        sql = sql + "  where fd.Status='Paid' and fd.Cancel is null ";
        sql = sql + "  and fd.FeeDepositeDate between '" + fromdate + "'   and   '" + todate + "'  ";
        sql = sql + "  and fd.BranchCode=" + Session["BranchCode"].ToString() + " and fd.Cancel is null";
        //sql = sql + "  and cm.sessionName='" + Session["SessionName"].ToString() + "'";
        if (RadioButtonList2.SelectedIndex != 0)
        {
            sql = sql + " and fd.MoP='" + RadioButtonList2.SelectedItem.ToString() + "'";
        }
        sql = sql + "  group by cm.ClassName,Cidorder,fd.SessionName Order by cm.CIDOrder Asc";

        GridView1.DataSource = oo.GridFill(sql);
        GridView1.DataBind();

        Label32.Text = "";

        sql = "  Select SUM(RecievedAmount)as TotAmt from FeeDeposite ";
        sql = sql + "  where Status='Paid' and FeeDepositeDate between '" + fromdate + "'   and   '" + todate + "'  ";
        sql = sql + "  and BranchCode=" + Session["BranchCode"].ToString() + " and Cancel is null";
        if (RadioButtonList2.SelectedIndex != 0)
        {
            sql = sql + " and MoP='" + RadioButtonList2.SelectedItem.ToString() + "'";
        }
        try
        {
            Label lbh = (Label)GridView1.FooterRow.FindControl("Label26");
            lbh.Text = oo.ReturnTag(sql, "TotAmt");
        }
        catch (Exception) { }
        if (GridView1.Rows.Count == 0)
        {
            //Label32.Text = "Sorry, No Record(s) found!";
            Campus camp = new Campus(); camp.msgbox(Label32, msgbox, "Sorry, No Record(s) found!", "A");
        }
        else
        {
            Label32.Text = "";
        }

        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            Label Label24 = (Label)GridView1.Rows[i].FindControl("Label24");
            Label24.Text = (i + 1).ToString();
        }


        if (GridView1.Rows.Count > 0)
        {
            ImageButton1.Visible = true;
            ImageButton2.Visible = true;
            ImageButton3.Visible = true;
            ImageButton4.Visible = true;
            GridView1.Visible = true;
            lbltitel.Visible = true;
            //Label3.Visible = true;
            abc.Visible = true;
        }

        else
        {
            ImageButton1.Visible = false;
            ImageButton2.Visible = false;
            ImageButton3.Visible = false;
            ImageButton4.Visible = false;
            GridView1.Visible = false;
            abc.Visible = false;
            //Label33.Text = "Sorry, No Record(s) found!";
            Campus camp = new Campus(); camp.msgbox(Label33, msgbox, "Sorry, No Record(s) found!", "A");
        }
    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        if (RadioButtonList1.SelectedItem.ToString() == "Installment Wise")
        {
            Panel2.Visible = true;
            ImageButton1.Visible = false;
            ImageButton2.Visible = false;
            ImageButton3.Visible = false;
            ImageButton4.Visible = false;
            abc.Visible = false;
            Panel1.Visible = false;
            GridView1.Visible = false;
            LinkButton5.Visible = false;
            sql = "select MonthName from MonthMaster  where CardType='" + DrpGroup.SelectedItem.ToString() + "'";
            sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            sql = sql + " or Monthid=0  ";
            sql = sql + " order by MonthId";

            oo.FillDropDownWithOutSelect(sql, DropDownMonth, "MonthName");
            Label32.Text = "";
            Label33.Text = "";
        }
        else
        {
            Panel1.Visible = true;
            ImageButton1.Visible = false;
            ImageButton2.Visible = false;
            ImageButton3.Visible = false;
            ImageButton4.Visible = false;
            abc.Visible = false;
            Panel2.Visible = false;
            LinkButton5.Visible = true;
            GridView1.Visible = false;
            Label33.Text = "";
            Label32.Text = "";
        }

        
    }
    protected void FromYY_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(FromYY, FromMM, FromDD);
    }
    protected void FromMM_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(FromYY, FromMM, FromDD);
    }
    protected void ToYY_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(ToYY, ToMM, ToDD);
    }
    protected void ToMM_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(ToYY, ToMM, ToDD);
    }
    protected void FromDD_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ToDD_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void DropDownMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
    
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_PageIndexChanged(object sender, EventArgs e)
    {

    }
    protected void DrpGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        sql = "select MonthName from MonthMaster where CardType='" + DrpGroup.SelectedItem.ToString() + "'";
        sql = sql + "  and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " or monthid=0";
        sql = sql + "  order by MonthId";
        oo.FillDropDownWithOutSelect(sql, DropDownMonth, "MonthName");
      //  GridView1.Visible = false;
    }
    protected void LinkButton6_Click(object sender, EventArgs e)
    {
        sql = "  select max(cm.ClassName) as Class,sum(fd.RecievedAmount) as RecievedAmount,cm.CIDOrder from ClassMaster cm";
        sql = sql + "   left join FeeDeposite fd on cm.ClassName=fd.Class inner join StudentOfficialDetails sod on fd.SrNo=sod.SrNo and fd.StEnRCode=sod.StEnRCode";
        sql = sql + "  where fd.Status='Paid' and sod.Card='" + DrpGroup.SelectedItem.ToString() + "' and sod.SessionName='" + Session["SessionName"].ToString() + "'";
        sql = sql + "  and (fd.FeeMonth='" + DropDownMonth.SelectedItem.ToString() + "' or fd.FeeMonth='" + "(T) " + DropDownMonth.SelectedItem.ToString() + "')";
        sql = sql + "  and fd.SessionName='" + Session["SessionName"].ToString() + "' and fd.BranchCode=" + Session["BranchCode"].ToString() + " and fd.Cancel is null";
        sql = sql + " and cm.SessionName='" + Session["SessionName"].ToString() + "'";
        if (RadioButtonList2.SelectedIndex != 0)
        {
            sql = sql + " and fd.MoP='" + RadioButtonList2.SelectedItem.ToString() + "'";
        }
        sql = sql + "  group by cm.CIDOrder Order by cm.CIDOrder Asc";
       
        GridView1.DataSource = oo.GridFill(sql);
        GridView1.DataBind();
        GridView1.Visible = true;
        ImageButton1.Visible = true;
        ImageButton2.Visible = true;
        ImageButton3.Visible = true;
        ImageButton4.Visible = true;

        sql = "  select SUM(fd.RecievedAmount) as TotAmt from FeeDeposite fd";
        sql = sql + "  inner join StudentOfficialDetails sod on fd.SrNo=sod.SrNo and fd.StEnRCode=sod.StEnRCode";
        sql = sql + "  where fd.Status='Paid' and sod.Card='" + DrpGroup.SelectedItem.ToString() + "' and sod.SessionName='" + Session["SessionName"].ToString() + "'";
        sql = sql + "  and fd.FeeMonth='" + DropDownMonth.SelectedItem.ToString() + "'";
        sql = sql + "  and fd.SessionName='" + Session["SessionName"].ToString() + "' and fd.BranchCode=" + Session["BranchCode"].ToString() + " and fd.Cancel is null";
        if (RadioButtonList2.SelectedIndex != 0)
        {
            sql = sql + " and fd.MoP='" + RadioButtonList2.SelectedItem.ToString() + "'";
        }
        try
        {
            Label lbh = (Label)GridView1.FooterRow.FindControl("Label26");
            lbh.Text = oo.ReturnTag(sql, "TotAmt");
        }
        catch (Exception) { }

        if (GridView1.Rows.Count == 0)
        {
            Label32.Text = "Sorry, No Record(s) found!";


            ImageButton1.Visible = false;
            ImageButton2.Visible = false;
            ImageButton3.Visible = false;
            ImageButton4.Visible = false;
            abc.Visible = false;
           



        }
        else
        {
            Label32.Text = "";
            abc.Visible = true;
            lblDate1.Visible = false;
            lblDate.Visible = false;
            lblTitle.Visible = false;
            lblTitle1.Visible = false;
            //Label3.Visible = false;
            lbltitel.Visible = true;

        }
        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            Label Label24 = (Label)GridView1.Rows[i].FindControl("Label24");
            Label24.Text = (i + 1).ToString();
        }
    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        oo.ExportToWord(Response, "TotalCollectionofFee.doc", gdv);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        oo.ExportToExcel("TotalCollectionofFee.xls", GridView1);
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {

    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        if (GridView1.Rows.Count > 0)
        {
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }
    protected void RadioButtonList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        abc.Visible = false;
        GridView1.DataSource = null;
        GridView1.DataBind();
    }
}