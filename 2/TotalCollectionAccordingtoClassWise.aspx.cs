using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;

public partial class admin_Total_CollectionAccordingtoClassWise : Page
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

        BLL.BLLInstance.LoadHeader("Report", header);
        if (!IsPostBack)
        {
            oo.AddDateMonthYearDropDown(FromYY, FromMM, FromDD);
            oo.AddDateMonthYearDropDown(ToYY, ToMM, ToDD);
            Label1.Visible = false;

        
         
            oo.FindCurrentDateandSetinDropDown(FromYY, FromMM, FromDD);
            oo.FindCurrentDateandSetinDropDown(ToYY, ToMM, ToDD);
            lblTitle.Text = "from ";
            lblTitle1.Text = "to ";
            ImageButton1.Visible = false;
            ImageButton2.Visible = false;
            ImageButton3.Visible = false;
            ImageButton4.Visible = false;

            abc.Visible = false;
            loadUser();
            loadClass();
            drpSection.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
            drpBranch.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));

        }
    }

    protected void loadClass()
    {
        sql = "Select ClassName,Id from ClassMaster";
        sql = sql + "  where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        sql = sql + " order by CIDOrder";
        oo.FillDropDown_withValue(sql, drpClass, "ClassName", "Id");
        drpClass.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
    }

    protected void loadUser()
    {
        sql = "Select UserId From NewAdminInformation";
        oo.FillDropDownWithOutSelect(sql, DropDownList1, "UserId");
        DropDownList1.Items.Insert(0, "All");
    }

    protected void drpClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadBranch();
        loadSection();
    }

    protected void loadBranch()
    {
        sql = "Select BranchName,Id from BranchMaster where ClassId=" + drpClass.SelectedValue.ToString();
        sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

        oo.FillDropDown_withValue(sql, drpBranch, "BranchName", "Id");

        drpBranch.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));

    }

    protected void loadSection()
    {
        sql = "Select SectionName,id from SectionMaster where ClassNameId=" + drpClass.SelectedValue.ToString();
        sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

        oo.FillDropDown_withValue(sql, drpSection, "SectionName","id");
      
            drpSection.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
        
    }

    protected void LinkButton5_Click(object sender, EventArgs e)
    {
        string todate = "", fromdate = "";
        //todate = ToYY.SelectedItem.ToString() + "/" + ToMM.SelectedItem.ToString() + "/" + ToDD.SelectedItem.ToString();
        //fromdate = FromYY.SelectedItem.ToString() + "/" + FromMM.SelectedItem.ToString() + "/" + FromDD.SelectedItem.ToString();
        todate = ToDD.SelectedItem.ToString() + " " + ToMM.SelectedItem.ToString() + " " + ToYY.SelectedItem.ToString();
        fromdate = FromDD.SelectedItem.ToString() + " " + FromMM.SelectedItem.ToString() + " " + FromYY.SelectedItem.ToString();
        lblDate.Text = fromdate;
        lblDate1.Text = todate;
           

        sql = "Select ClassName as Class from ClassMaster where Id!=0 ";
        sql = sql + "  and  SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        if (drpClass.SelectedIndex != 0)
        {
            sql = sql + " and id='" + drpClass.SelectedValue.ToString() + "'";
        }
        sql = sql + " order by CIDOrder";

        GridView1.DataSource = oo.GridFill(sql);
        GridView1.DataBind();
        GridView1.Visible = true;
        ImageButton1.Visible = true;
        ImageButton2.Visible = true;
        ImageButton3.Visible = true;
        ImageButton4.Visible = true;

        for (int a = 0; a < GridView1.Rows.Count; a++)//department Wise
        {
            Label l = (Label)GridView1.Rows[a].FindControl("lblclass");
            GridView GridView2 = (GridView)GridView1.Rows[a].FindControl("GridView2");


            sql = "select ROW_NUMBER() OVER (ORDER BY fd.Id ASC) AS SNo,fd.Id as Id, fd.SrNo as SrNo ,fd.SessionName,Upper(sg.FirstName) as FirstName,Upper(sg.MiddleName) as MiddleName,SOD.Medium as Medium ,Upper(sg.LastName) as LastName,SF.FatherName as FatherName,convert(nvarchar,fd.FeeDepositeDate,106) as FeeDepositeDate,sod.TypeOFAdmision as TypeOfAdmission ,";
            sql = sql + "  fd.LateFeeAmount as LateFeeAmount,fd.RemainingAmount as RemainingAmount ,fd.MOP,fd.Cocession as Cocession ,fd.RecieptSrNo as RecieptSrNo ,fd.RecievedAmount as RecievedAmount,fd.AmountInWords,";
            sql = sql + "  Case When (Select Count(*) from FeeReceiptWithInstallment where ReceiptNo=fd.RecieptSrNo)=0 Then UPPER(fd.FeeMonth) Else ";
            sql = sql + "  Case When (Select Count(*) from FeeReceiptWithInstallment where ReceiptNo = fd.RecieptSrNo)= 1 Then";
            sql = sql + "  (Select UPPER(InstallmentName) from FeeReceiptWithInstallment where ReceiptNo = fd.RecieptSrNo) Else '' End End FeeMonth,";
            sql = sql + "  fd.Class as Class,fd.section as section,(fd.RecievedAmount-fd.BusConvience) ActualAmount,fd.BusConvience BusConvience from FeeDeposite Fd ";
            sql = sql + "  left join StudentGenaralDetail sg on Fd.StEnRCode=sg.StEnRCode";
            sql = sql + "  left join StudentFamilyDetails SF on Fd.StEnRCode=SF.StEnRCode";
            sql = sql + "  left join StudentOfficialDetails SOD on Fd.StEnRCode=SOD.StEnRCode";
            sql = sql + "  where fd.Status='Paid' and fd.FeeDepositeDate between '" + fromdate + "'   and   '" + todate + "'  ";
            sql = sql + "  and sg.BranchCode=" + Session["BranchCode"].ToString() + " and fd.Class='" + l.Text + "' and fd.Cancel is null";
            if (RadioButtonList2.SelectedIndex != 0)
            {
                sql = sql + " and fd.MoP='" + RadioButtonList2.SelectedItem.ToString() + "'";
            }
            if (DropDownList1.SelectedIndex != 0)
            {
                sql = sql + " and fd.LoginName='" + DropDownList1.SelectedItem.ToString() + "'";
            }
           
            GridView2.DataSource = oo.GridFill(sql);
            GridView2.DataBind();
            if (GridView1.Rows.Count > 0)
            {
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            if (GridView2.Rows.Count > 0)
            {
                GridView2.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            GridView2.Visible = true;
            ImageButton1.Visible = true;
            ImageButton2.Visible = true;
            ImageButton3.Visible = true;
            ImageButton4.Visible = true;
            Label10.Text = " by User " + DropDownList1.SelectedItem.Text;
            if (drpSection.SelectedIndex != 0)
            {
                l.Text = l.Text + " (" + drpSection.SelectedItem.Text + ")";
            }
            if (GridView2.Rows.Count > 0)
            {
                string newInstallment = "";
                for (int i = 0; i < GridView2.Rows.Count; i++)
                {
                    Label lblReceptNo = (Label)GridView2.Rows[i].FindControl("lblReceptNo");
                    Label lblinstal = (Label)GridView2.Rows[i].FindControl("FeeMonth");
                    if (lblinstal.Text.Trim() == "")
                    {
                        string sqls = "Select Count(*) counts from FeeReceiptWithInstallment where ReceiptNo = '" + lblReceptNo.Text.Trim() + "'";
                        int counts = int.Parse(BAL.objBal.ReturnTag(sqls, "counts"));
                        if (counts > 1)
                        {
                            string sql1 = "Select UPPER(InstallmentName) InstallmentName from FeeReceiptWithInstallment where ReceiptNo = '" + lblReceptNo.Text.Trim() + "'";
                            var ds = oo.GridFill(sql1);
                            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                            {
                                if (j == 0)
                                {
                                    newInstallment = newInstallment + ds.Tables[0].Rows[j]["InstallmentName"].ToString() + " - ";
                                }
                                if ((j + 1) == ds.Tables[0].Rows.Count)
                                {
                                    newInstallment = newInstallment + ds.Tables[0].Rows[j]["InstallmentName"].ToString();
                                }
                            }
                        }
                    }
                    sql = "Select Case When(Select Count(*) from FeeReceiptWithInstallment where ReceiptNo = '" + lblReceptNo.Text.Trim() + "') = 1 Then";
                    sql = sql + " (Select UPPER(InstallmentName) from FeeReceiptWithInstallment where ReceiptNo = '" + lblReceptNo.Text.Trim() + "') Else '" + newInstallment + "' End FeeMonth";
                    lblinstal.Text = BAL.objBal.ReturnTag(sql, "FeeMonth");
                    sql = ""; newInstallment = "";
                }
            }


        }
        
        
        
        for (int a = 0; a < GridView1.Rows.Count; a++)//department Wise
        {
            Label l = (Label)GridView1.Rows[a].FindControl("lblclass");
            Label lblNotfound = (Label)GridView1.Rows[a].FindControl("lblNotfound");

            try
            {
                GridView G2 = (GridView)GridView1.Rows[a].FindControl("GridView2");
                if (G2.Rows.Count > 0)
                {
                    Label lb = (Label)G2.FooterRow.FindControl("Label5");

                    sql = "Select sum(fd.RecievedAmount) as RecievedAmount from FeeDeposite Fd ";
                    sql = sql + "   left join StudentGenaralDetail sg on Fd.StEnRCode=sg.StEnRCode";
                    sql = sql + "  left join StudentFamilyDetails SF on Fd.StEnRCode=SF.StEnRCode";
                    sql = sql + "  left join StudentOfficialDetails SOD on Fd.StEnRCode=SOD.StEnRCode";
                    sql = sql + "  where fd.Status='Paid' and fd.FeeDepositeDate between '" + fromdate + "'   and   '" + todate + "'  ";
                    sql = sql + "  and fd.Cancel is null";
                    if (RadioButtonList2.SelectedIndex != 0)
                    {
                        sql = sql + " and fd.MoP='" + RadioButtonList2.SelectedItem.ToString() + "'";
                    }
                    if (DropDownList1.SelectedIndex != 0)
                    {
                        sql = sql + " and fd.LoginName='" + DropDownList1.SelectedItem.ToString() + "'";
                    }
                    if (drpClass.SelectedIndex != 0)
                    {
                        sql = sql + " and fd.Class='" + l.Text + "'";
                    }
                    //if (drpSection.SelectedIndex != 0)
                    //{
                    //    sql = sql + " and SOD.SectionId='" + drpSection.SelectedValue.ToString() + "'";
                    //}
                    //if (drpBranch.SelectedIndex != 0)
                    //{
                    //    sql = sql + " and (SOD.Branch='" + drpBranch.SelectedValue.ToString() + "' or SOD.Branch is null)";
                    //}
                    lb.Text = oo.ReturnTag(sql, "RecievedAmount");
                }
                else
                {
                    GridView1.Rows[a].Visible = false;
                    lblNotfound.Text = "Sorry, No Record(s) found!";
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, No Record(s) found!", "S");
                }
            }
            catch (Exception) 
            {
                //GridView1.Rows[a].Visible = false;
                //lblNotfound.Text = "Sorry, No Record(s) found!"; 
            }          
        }


        sql = "Select sum(fd.RecievedAmount) as RecievedAmount from FeeDeposite Fd ";
        sql = sql + "   left join StudentGenaralDetail sg on Fd.StEnRCode=sg.StEnRCode";
        sql = sql + "  left join StudentFamilyDetails SF on Fd.StEnRCode=SF.StEnRCode";
        sql = sql + "  left join StudentOfficialDetails SOD on Fd.StEnRCode=SOD.StEnRCode";
        sql = sql + "  where fd.Status='Paid' and fd.FeeDepositeDate between '" + fromdate + "'   and   '" + todate + "'  ";
        sql = sql + " and sg.BranchCode=" + Session["BranchCode"].ToString() + " and fd.Cancel is null";
        if (RadioButtonList2.SelectedIndex != 0)
        {
            sql = sql + " and fd.MoP='" + RadioButtonList2.SelectedItem.ToString() + "'";
        }
        if (DropDownList1.SelectedIndex != 0)
        {
            sql = sql + " and fd.LoginName='" + DropDownList1.SelectedItem.ToString() + "'";
        }
        if (drpClass.SelectedIndex != 0)
        {
            sql = sql + " and fd.Class='" + drpClass.SelectedItem.Text.ToString() + "'";
        }
        //if (drpSection.SelectedIndex != 0)
        //{
        //    sql = sql + " and SOD.SectionId='" + drpSection.SelectedValue.ToString() + "'";
        //}
        //if (drpBranch.SelectedIndex != 0)
        //{
        //    sql = sql + " and (SOD.Branch='" + drpBranch.SelectedValue.ToString() + "' or SOD.Branch is null)";
        //}
        try
        {
            Label lbh = (Label)GridView1.FooterRow.FindControl("Label11");
            lbh.Text = oo.ReturnTag(sql, "RecievedAmount");

            
            Label1.Visible = true;
            abc.Visible = true;

        }
        catch (Exception) { }
       

    }
    protected void FromYY_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(FromYY, FromMM, FromDD);
    }
    protected void ToYY_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(ToYY, ToMM, ToDD);
    }
    protected void ToMM_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(ToYY, ToMM, ToDD);
    }
    protected void ToDD_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void FromMM_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(FromYY, FromMM, FromDD);
    }
    protected void FromDD_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        if (GridView1.Rows.Count > 0)
        {
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        for (int a = 0; a < GridView1.Rows.Count; a++)//department Wise
        {
            GridView GridView2 = (GridView)GridView1.Rows[a].FindControl("GridView2");
            if (GridView2.Rows.Count > 0)
            {
                GridView2.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        oo.ExportToWord(Response, "TotalCollectionAccordingtoClassWise.doc", gdv);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        oo.ExportToExcel("TotalCollectionAccordingtoClassWise.xls", GridView1);
    }
    protected void RadioButtonList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        abc.Visible = false;
        GridView1.DataSource = null;
        GridView1.DataBind();
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {

    }

  
}