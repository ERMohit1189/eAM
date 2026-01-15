using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;

public partial class admin_AllStudentReceiptMonthDate : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = string.Empty;

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
        if (!IsPostBack)
        {
            Image1.Visible = false;
            Label1.Visible = false;
            Image1.ImageUrl = "DisplayImage.ashx?UserLoginID=" + 1;
            sql = "Select CollegeName,CollegeAdd2,CityId from CollegeMaster where CollegeId=" + 1+ " and BranchCode=" + Session["BranchCode"].ToString() + "";
            lblCollegeName.Text = oo.ReturnTag(sql, "CollegeName");
            lblbranchwithcity.Text = oo.ReturnTag(sql, "CollegeAdd2") + ' ' + oo.ReturnTag(sql, "CityId");
            sql = "Select FeeGroupName from FeeGroupMaster";
            oo.FillDropDown(sql, DrpGroup, "FeeGroupName");
            
            oo.AddDateMonthYearDropDown(FromYY, FromMM, FromDD);
            oo.AddDateMonthYearDropDown(ToYY, ToMM, ToDD);


            oo.FindCurrentDateandSetinDropDown(FromYY, FromMM, FromDD);
            oo.FindCurrentDateandSetinDropDown(ToYY, ToMM, ToDD);
            sql = "select MonthName from MonthMaster where CardType='" + DrpGroup.SelectedItem.ToString() + "'";
            sql = sql + " and sessionName='"+Session["SessionName"].ToString()+"' and BranchCode=" + Session["BranchCode"].ToString() + "";
            sql = sql + " or Monthid=0  ";
            sql = sql + " order by MonthId";

            oo.FillDropDownWithOutSelect(sql, DropDownMonth, "MonthName");
            lblDate.Visible = false;
            lblDate1.Visible = false;
            Panel1.Visible = false;

            lblTitle.Text = "( From Date :";
            lblTitle.Visible = false;

            lblTitle1.Text = "To Date :";
            lblTitle1.Visible = false;
            Panel2.Visible = false;
            Label32.Text = "";

            LinkButton5.Visible = true;

            if (RadioButtonList1.Items[0].Selected == true)
            {
                Panel1.Visible = true;
            }
            abc.Visible = false;
            //loadUser();
            loadclass();
        }

    }

    //protected void loadUser()
    //{
    //    sql = "Select UserId From NewAdminInformation";
    //    oo.FillDropDownWithOutSelect(sql, DropDownList1, "UserId");
    //    DropDownList1.Items.Insert(0, "All");
    //}
    public void loadclass()
    {
        sql = "Select Distinct ClassName,cm.Id as Id,CIDOrder from ClassTeacherMaster ctm";
        sql = sql + " inner join ClassMaster cm on cm.Id=ctm.ClassId and cm.SessionName=ctm.SessionName";
        sql = sql + " where ctm.SessionName='" + Session["SessionName"].ToString() + "' and EmpCode='" + Session["LoginName"].ToString() + "' and ctm.BranchCode=" + Session["BranchCode"].ToString() + "";
        sql = sql + " and ctm.BranchCode=" + Session["BranchCode"].ToString() + " and IsClassTeacher=1 Order by CIDOrder";

        oo.FillDropDown_withValue(sql, DropDownList1, "ClassName", "Id");
        DropDownList1.Items.Insert(0, "All");
    }

    protected void FromYY_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(FromYY, FromMM, FromDD);
    }
    protected void FromMM_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(FromYY, FromMM, FromDD);
    }
    protected void FromDD_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ToYY_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(ToYY, ToMM, ToDD);
    }
    protected void ToMM_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(ToYY, ToMM, ToDD);
    }
    protected void DropDownMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(RadioButtonList1.SelectedItem.ToString()=="Installment Wise")
        {
             Panel2.Visible=true;
             Panel1.Visible=false;
             GridView1.Visible = false;
             abc.Visible = false;
             LinkButton5.Visible = false;
             sql = "select MonthName from MonthMaster where CardType='" + DrpGroup.SelectedItem.ToString() + "'";
             sql = sql + " and SessionName='"+Session["SessionName"].ToString() +"' and BranchCode=" + Session["BranchCode"].ToString() + "";
             sql = sql + " or Monthid=0  ";
             sql = sql + " order by MonthId";

             oo.FillDropDownWithOutSelect(sql, DropDownMonth, "MonthName");
             drpFilter.Visible = false;
        }
        else
        {   Panel1.Visible=true;
            Panel2.Visible=false;
            LinkButton5.Visible = true;
            GridView1.Visible = false;
            abc.Visible = false;
            drpFilter.Visible = true;
    }

        
}
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        //sql = "select ROW_NUMBER() OVER (ORDER BY Id ASC) AS SrNo,Id, StEnRCode,SrNo,convert(nvarchar,FeeDepositeDate,106) as FeeDepositeDate,RecieptSrNo,FeeMonth,RecievedAmount,RemainingAmount,Class,Section from FeeDeposite  where " + DrpEnter.SelectedValue.ToString() + "=" + "'" + TxtEnter.Text.Trim() + "'";
        //sql = sql + "  and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";


      
    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
         string ss = "";

         lblcancel.Text = "";
        //  string aa = GridView1.SelectedRow.RowIndex.ToString();




        LinkButton chk = (LinkButton)sender;
        ss = chk.ToolTip.ToString();

        //sql="select StEnRCode,SrNo,convert(nvarchar,FeeDepositeDate,106) as FeeDepositeDate,RecieptSrNo,";
        //sql = sql + "   FeeMonth,TotalFeeAmount,Cocession,RecievedAmount,CurrentAmount,RemainingAmount,Remark,Class,Section from FeeDeposite  where SessionName='2011-2012' and " + DrpEnter.SelectedValue.ToString() + "=" + "'" + TxtEnter.Text.Trim() + "' and Id =" + ss;

        sql = "select id,StEnRCode,SrNo,convert(nvarchar,FeeDepositeDate,106) as FeeDepositeDate,RecieptSrNo,Cancel,BusConvience,  FeeMonth,TotalFeeAmount,";
        sql = sql + "   Cocession,RecievedAmount,CurrentAmount,LateFeeAmount,RemainingAmount,Remark,Class,Section,DiscountName,DiscountAmount from FeeDeposite  where  RecieptSrNo='" + chk.Text + "'";
        sql = sql + "  and BranchCode=" + Session["BranchCode"].ToString() + "";


        lblTotalFee.Text = oo.ReturnTag(sql, "TotalFeeAmount");
        lblConcession.Text = oo.ReturnTag(sql, "Cocession");
        // lblTotalAmount.Text = oo.ReturnTag(sql, "CurrentAmount");
        lblPaidAmount.Text = oo.ReturnTag(sql, "RecievedAmount");
        lblBalace.Text = oo.ReturnTag(sql, "RemainingAmount");
        lblRemark.Text = oo.ReturnTag(sql, "Remark");
        //  Label25.Text = oo.ReturnTag(sql, "RecievedAmount");
        lblLate.Text = oo.ReturnTag(sql, "LateFeeAmount");

        Label33.Text = oo.ReturnTag(sql, "BusConvience");

        if (oo.ReturnTag(sql, "BusConvience") == "")
        {
            Label33.Text = "0.00";
        }
        else
        {
            Label33.Text = oo.ReturnTag(sql, "BusConvience");
        }





        lblID.Text = chk.Text;
        if (oo.ReturnTag(sql, "Cancel").Trim() == "Y")
        {
            lblcancel.Text = "CANCEL";
        }
        else
        {
            lblcancel.Text = "";
        }


        try
        {
            lblDiscountValue.Text = oo.ReturnTag(sql, "DiscountAmount");
            lblDiscountName.Text = oo.ReturnTag(sql, "DiscountName");
            lblDiscountName.Visible = true ; lblDiscountValue.Visible = true ;
        }
        catch (Exception) { lblDiscountName.Visible = false; lblDiscountValue.Visible = false; }
        int k = Convert.ToInt32(ss) - 1;

        sql = "   select top 1 id,StEnRCode,SrNo,convert(nvarchar,FeeDepositeDate,106) as FeeDepositeDate,RecieptSrNo,Cancel,   FeeMonth,TotalFeeAmount,   Cocession,RecievedAmount,CurrentAmount,RemainingAmount,Remark,Class,Section,DiscountName,DiscountAmount from FeeDeposite   ";
        sql = sql + "  where  FeeMonth='" + DropDownMonth.SelectedItem.ToString() + "'   and id<=" + k.ToString();
        sql = sql + "  and BranchCode=" + Session["BranchCode"].ToString() + "";
        sql = sql + "  order by RecieptSrNo  desc ";

        try
        {
            Label25.Text = oo.ReturnTag(sql, "RemainingAmount");
            double a = Convert.ToDouble(Label25.Text);
        }
        catch (Exception) { Label25.Text = "0"; con.Close(); }

        


        Panel4_ModalPopupExtender.Show();



    }
    protected void  Button3_Click(object sender, EventArgs e)
    {
     
      Session["RecieptNoSession"] = lblID.Text;

      Response.Redirect("StudentReciptGenrate_duplicate.aspx?print=1");
    }
    protected void  Button1_Click(object sender, EventArgs e)
    {

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void GridView1_PageIndexChanged(object sender, EventArgs e)
    {

    }
    protected void LinkButton5_Click(object sender, EventArgs e)
    {
        string todate = "", fromdate = "";
        todate = ToYY.SelectedItem.ToString() + "/" + ToMM.SelectedItem.ToString() + "/" + ToDD.SelectedItem.ToString();
        fromdate = FromYY.SelectedItem.ToString() + "/" + FromMM.SelectedItem.ToString() + "/" + FromDD.SelectedItem.ToString();

        lblDate.Text = fromdate;
        lblDate1.Text = todate;
        lblDate.Visible = true;
        lblDate1.Visible = true;
        Panel1.Visible = true;
        lblTitle.Visible = true;
        lblTitle1.Visible = true;

        if (drpFilter.SelectedIndex == 0)
        {

            sql = "select ROW_NUMBER() OVER (ORDER BY fd.Id ASC) AS SNo,fd.Id as Id, fd.SrNo as SrNo ,SOD.Card as Card,  sg.FirstName as FirstName,sg.MiddleName as MiddleName,SOD.Medium as Medium ,sg.LastName as LastName,SF.FatherName as FatherName,convert(nvarchar,fd.FeeDepositeDate,106) as FeeDepositeDate,sod.TypeOFAdmision as TypeOfAdmission ,";
            sql = sql + "  fd.LateFeeAmount as LateFeeAmount,fd.MOP,fd.RemainingAmount as RemainingAmount ,fd.Cocession as Cocession ,fd.RecieptSrNo as RecieptSrNo ,fd.RecievedAmount as RecievedAmount,fd.AmountInWords,fd.FeeMonth as FeeMonth,fd.Class as Class,fd.section as section from FeeDeposite Fd ";
            sql = sql + "  left join StudentGenaralDetail sg on Fd.StEnRCode=sg.StEnRCode";
            sql = sql + "  left join StudentFamilyDetails SF on Fd.StEnRCode=SF.StEnRCode";
            sql = sql + "  left join StudentOfficialDetails SOD on Fd.StEnRCode=SOD.StEnRCode";
            sql = sql + "  where fd.Status='Paid' and fd.FeeDepositeDate between '" + fromdate + "' and   '" + todate + "' and Cancel is null  and sf.BranchCode=" + Session["BranchCode"].ToString() + " and sg.BranchCode=" + Session["BranchCode"].ToString() + " and sod.BranchCode=" + Session["BranchCode"].ToString() + " and fd.BranchCode=" + Session["BranchCode"].ToString() + "    ";
            if (RadioButtonList2.SelectedIndex != 0)
            {
                sql = sql + " and fd.MoP='" + RadioButtonList2.SelectedItem.ToString() + "'";
            }
            if (DropDownList1.SelectedIndex != 0)
            {
                sql = sql + " and fd.Class='" + DropDownList1.SelectedItem.ToString() + "'  order by FirstName Asc";
            }
            else
            {
                sql = sql + " and (fd.Class='" + DropDownList1.Items[1].ToString() + "'";
                string str = "";
                for (int i = 2; i < DropDownList1.Items.Count; i++)
                {
                    if (str == "")
                    {
                        str = "or fd.class='" + DropDownList1.Items[i].Text + "'";
                    }
                    else
                    {
                        str = str + " or fd.class='" + DropDownList1.Items[i].Text + "'"; 
                    }
                }
                str = str + ")  order by FirstName Asc";
                sql = sql + str;

            }
        }
        else if (drpFilter.SelectedIndex == 1)
        {
            sql = "select ROW_NUMBER() OVER (ORDER BY fd.Id ASC) AS SNo,fd.Id as Id, fd.SrNo as SrNo ,SOD.Card as Card,  sg.FirstName as FirstName,sg.MiddleName as MiddleName,SOD.Medium as Medium ,sg.LastName as LastName,SF.FatherName as FatherName,convert(nvarchar,fd.FeeDepositeDate,106) as FeeDepositeDate,sod.TypeOFAdmision as TypeOfAdmission ,";
            sql = sql + "   fd.LateFeeAmount as LateFeeAmount,fd.MOP,fd.RemainingAmount as RemainingAmount ,fd.Cocession as Cocession ,fd.RecieptSrNo as RecieptSrNo ,fd.RecievedAmount as RecievedAmount,fd.AmountInWords,fd.FeeMonth as FeeMonth,fd.Class as Class,fd.section as section from FeeDeposite Fd ";
            sql = sql + "   left join StudentGenaralDetail sg on Fd.StEnRCode=sg.StEnRCode";
            sql = sql + "  left join StudentFamilyDetails SF on Fd.StEnRCode=SF.StEnRCode";
            sql = sql + "  left join StudentOfficialDetails SOD on Fd.StEnRCode=SOD.StEnRCode";
            sql = sql + "  where fd.Status='Paid' and fd.FeeDepositeDate between '" + fromdate + "'   and   '" + todate + "' and fd.FeeMonth<>'Yearly' and Cancel is null  and sf.BranchCode=" + Session["BranchCode"].ToString() + " and sg.BranchCode=" + Session["BranchCode"].ToString() + " and sod.BranchCode=" + Session["BranchCode"].ToString() + " and fd.BranchCode=" + Session["BranchCode"].ToString() + "    ";
            if (RadioButtonList2.SelectedIndex != 0)
            {
                sql = sql + " and fd.MoP='" + RadioButtonList2.SelectedItem.ToString() + "'";
            }
            if (DropDownList1.SelectedIndex != 0)
            {
                sql = sql + " and fd.Class='" + DropDownList1.SelectedItem.ToString() + "'  order by FirstName Asc";
            }
            else
            {
                sql = sql + " and (fd.Class='" + DropDownList1.Items[1].ToString() + "'";
                string str = "";
                for (int i = 2; i < DropDownList1.Items.Count; i++)
                {
                    if (str == "")
                    {
                        str = "or fd.class='" + DropDownList1.Items[i].Text + "'";
                    }
                    else
                    {
                        str = str + " or fd.class='" + DropDownList1.Items[i].Text + "'"; 
                    }
                }
                str = str + ")  order by FirstName Asc";
                sql = sql + str;

            }
        }
        else
        {
            sql = "select ROW_NUMBER() OVER (ORDER BY fd.Id ASC) AS SNo,fd.Id as Id, fd.SrNo as SrNo ,SOD.Card as Card,  sg.FirstName as FirstName,sg.MiddleName as MiddleName,SOD.Medium as Medium ,sg.LastName as LastName,SF.FatherName as FatherName,convert(nvarchar,fd.FeeDepositeDate,106) as FeeDepositeDate,sod.TypeOFAdmision as TypeOfAdmission ,";
            sql = sql + "   fd.LateFeeAmount as LateFeeAmount,fd.MOP,fd.RemainingAmount as RemainingAmount ,fd.Cocession as Cocession ,fd.RecieptSrNo as RecieptSrNo ,fd.RecievedAmount as RecievedAmount,fd.AmountInWords,fd.FeeMonth as FeeMonth,fd.Class as Class,fd.section as section from FeeDeposite Fd ";
            sql = sql + "   left join StudentGenaralDetail sg on Fd.StEnRCode=sg.StEnRCode";
            sql = sql + "  left join StudentFamilyDetails SF on Fd.StEnRCode=SF.StEnRCode";
            sql = sql + "  left join StudentOfficialDetails SOD on Fd.StEnRCode=SOD.StEnRCode";
            sql = sql + "  where fd.Status='Paid' and fd.FeeDepositeDate between '" + fromdate + "'   and   '" + todate + "' and fd.FeeMonth='Yearly' and Cancel is null   and sf.BranchCode=" + Session["BranchCode"].ToString() + " and sg.BranchCode=" + Session["BranchCode"].ToString() + " and sod.BranchCode=" + Session["BranchCode"].ToString() + " and fd.BranchCode=" + Session["BranchCode"].ToString() + "    ";
            if (RadioButtonList2.SelectedIndex != 0)
            {
                sql = sql + " and fd.MoP='" + RadioButtonList2.SelectedItem.ToString() + "'";
            }
            if (DropDownList1.SelectedIndex != 0)
            {
                sql = sql + " and fd.Class='" + DropDownList1.SelectedItem.ToString() + "'  order by FirstName Asc";
            }
            else
            {
                sql = sql + " and (fd.Class='" + DropDownList1.Items[1].ToString() + "'";
                string str = "";
                for (int i = 2; i < DropDownList1.Items.Count; i++)
                {
                    if (str == "")
                    {
                        str = "or fd.class='" + DropDownList1.Items[i].Text + "'";
                    }
                    else
                    {
                        str = str + " or fd.class='" + DropDownList1.Items[i].Text + "'"; 
                    }
                }
                str = str + ")  order by FirstName Asc";
                sql = sql + str;

            }
        }

            Session["CardType"] = oo.ReturnTag(sql, "Card");

            GridView1.DataSource = oo.GridFill(sql);
            GridView1.DataBind();
            GridView1.Visible = true;

            try
            {
                if (drpFilter.SelectedIndex == 0)
                {
                    sql = "  select SUM(RecievedAmount)as TotAmt from FeeDeposite  ";
                    sql = sql + "  where Status='Paid' and FeeDepositeDate between '" + fromdate + "'   and   '" + todate + "'  ";
                    sql = sql + "  and BranchCode=" + Session["BranchCode"].ToString() + " and Cancel is null";
                    if (RadioButtonList2.SelectedIndex != 0)
                    {
                        sql = sql + " and MoP='" + RadioButtonList2.SelectedItem.ToString() + "'";
                    }
                    if (DropDownList1.SelectedIndex != 0)
                    {
                        sql = sql + " and Class='" + DropDownList1.SelectedItem.ToString() + "'";
                    }
                    else
                    {
                        sql = sql + " and (Class='" + DropDownList1.Items[1].ToString() + "'";
                        string str = "";
                        for (int i = 2; i < DropDownList1.Items.Count; i++)
                        {
                            if (str == "")
                            {
                                str = "or class='" + DropDownList1.Items[i].Text + "'";
                            }
                            else
                            {
                                str = str + " or class='" + DropDownList1.Items[i].Text + "'"; 
                            }
                        }
                        str = str + ")";
                        sql = sql + str;

                    }
                    Label lbh = (Label)GridView1.FooterRow.FindControl("Label31");
                    lbh.Text = oo.ReturnTag(sql, "TotAmt");
                }
                else if (drpFilter.SelectedIndex == 1)
                {
                    sql = "  select SUM(RecievedAmount)as TotAmt from FeeDeposite  ";
                    sql = sql + "  where Status='Paid' and FeeDepositeDate between '" + fromdate + "'   and   '" + todate + "' and FeeMonth<> 'Yearly'   ";
                    sql = sql + "  and BranchCode=" + Session["BranchCode"].ToString() + " and Cancel is null";
                    if (RadioButtonList2.SelectedIndex != 0)
                    {
                        sql = sql + " and MoP='" + RadioButtonList2.SelectedItem.ToString() + "'";
                    }
                    if (DropDownList1.SelectedIndex != 0)
                    {
                        sql = sql + " and Class='" + DropDownList1.SelectedItem.ToString() + "'";
                    }
                    else
                    {
                        sql = sql + " and (Class='" + DropDownList1.Items[1].ToString() + "'";
                        string str = "";
                        for (int i = 2; i < DropDownList1.Items.Count; i++)
                        {
                            if (str == "")
                            {
                                str = "or class='" + DropDownList1.Items[i].Text + "'";
                            }
                            else
                            {
                                str = str + " or class='" + DropDownList1.Items[i].Text + "'"; 
                            }
                        }
                        str = str + ")";
                        sql = sql + str;

                    }
                    Label lbh = (Label)GridView1.FooterRow.FindControl("Label31");
                    lbh.Text = oo.ReturnTag(sql, "TotAmt");
                }
                else
                {
                    sql = "  select SUM(RecievedAmount)as TotAmt from FeeDeposite  ";
                    sql = sql + "  where Status='Paid' and FeeDepositeDate between '" + fromdate + "'   and   '" + todate + "' and FeeMonth='Yearly'   ";
                    sql = sql + "  and BranchCode=" + Session["BranchCode"].ToString() + " and Cancel is null";
                    if (RadioButtonList2.SelectedIndex != 0)
                    {
                        sql = sql + " and MoP='" + RadioButtonList2.SelectedItem.ToString() + "'";
                    }
                    if (DropDownList1.SelectedIndex != 0)
                    {
                        sql = sql + " and Class='" + DropDownList1.SelectedItem.ToString() + "'";
                    }
                    else
                    {
                        sql = sql + " and (Class='" + DropDownList1.Items[1].ToString() + "'";
                        string str = "";
                        for (int i = 2; i < DropDownList1.Items.Count; i++)
                        {
                            if (str == "")
                            {
                                str = "or class='" + DropDownList1.Items[i].Text + "'";
                            }
                            else
                            {
                                str = str + " or class='" + DropDownList1.Items[i].Text + "'"; 
                            }
                        }
                        str = str + ")";
                        sql = sql + str;

                    }
                    Label lbh = (Label)GridView1.FooterRow.FindControl("Label31");
                    lbh.Text = oo.ReturnTag(sql, "TotAmt");
                }
            }
            catch (Exception) { }
         if (GridView1.Rows.Count == 0)
        {
            oo.MessageBox("Sorry, No Record(s) found!", this.Page);

            Label32.Text = "Sorry, No Record(s) found!";
            abc.Visible = false;
            Label2.Text = "";
        }
        else
         {
             Label32.Text = "";
             abc.Visible = true;
             lblDate1.Visible = true;
             lblDate.Visible = true;
             lblTitle.Visible = true;
             lblTitle1.Visible = true;
             Label3.Visible = true;
             Image1.Visible = true;
             Label1.Visible = true;
             Label2.Text = " of class " + DropDownList1.SelectedItem.Text;
            
        }
    }
    protected void DrpGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        sql = "select MonthName from MonthMaster where CardType='" + DrpGroup.SelectedItem.ToString() + "'";
        sql = sql + " and sessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " or monthid=0";
        sql = sql + "  order by MonthId";
        oo.FillDropDownWithOutSelect(sql, DropDownMonth, "MonthName");
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        oo.ExportToWord(Response, "AllStudentReceiptMonthDate.doc", gdv1);
    }
  
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        oo.ExportToExcel("AllStudentReceiptMonthDate.xls", GridView1);
    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }
    protected void LinkButton6_Click(object sender, EventArgs e)
    {

        sql = "select ROW_NUMBER() OVER (ORDER BY fd.Id ASC) AS SNo,fd.Id as Id, fd.SrNo as SrNo ,SOD.Card as Card,sg.FirstName as FirstName,sg.MiddleName as MiddleName,SOD.Medium as Medium ,sg.LastName as LastName,SF.FatherName as FatherName,convert(nvarchar,fd.FeeDepositeDate,106) as FeeDepositeDate,sod.TypeOFAdmision as TypeOfAdmission ,";
        sql = sql + "   fd.LateFeeAmount as LateFeeAmount,fd.MOP,fd.RemainingAmount as RemainingAmount ,fd.Cocession as Cocession ,fd.RecieptSrNo as RecieptSrNo ,fd.RecievedAmount as RecievedAmount,fd.AmountInWords,fd.FeeMonth as FeeMonth,fd.Class as Class,fd.section as section from FeeDeposite Fd ";
        sql = sql + "   left join StudentGenaralDetail sg on Fd.StEnRCode=sg.StEnRCode";
        sql = sql + "  left join StudentFamilyDetails SF on Fd.StEnRCode=SF.StEnRCode";
        sql = sql + "  left join StudentOfficialDetails SOD on Fd.StEnRCode=SOD.StEnRCode and Fd.SrNo=SOD.SrNo";
        sql = sql + "  where fd.Status='Paid' and sod.Card='" + DrpGroup.SelectedItem.ToString() + "' and fd.FeeMonth='" + DropDownMonth.SelectedItem.ToString() + "' and Cancel is null";
        sql = sql + "  and sg.SessionName='" + Session["SessionName"].ToString() + "' and sg.BranchCode=" + Session["BranchCode"].ToString() + " ";
        sql = sql + "  and sf.SessionName='" + Session["SessionName"].ToString() + "'  and sod.SessionName='" + Session["SessionName"].ToString() + "' and sf.BranchCode=" + Session["BranchCode"].ToString() + " and sg.BranchCode=" + Session["BranchCode"].ToString() + " and sod.BranchCode=" + Session["BranchCode"].ToString() + " and fd.BranchCode=" + Session["BranchCode"].ToString() + "   ";
        if (RadioButtonList2.SelectedIndex != 0)
        {
            sql = sql + " and fd.MoP='" + RadioButtonList2.SelectedItem.ToString() + "'";
        }
        if (DropDownList1.SelectedIndex != 0)
        {
            sql = sql + " and fd.Class='" + DropDownList1.SelectedItem.ToString() + "'  order by FirstName Asc";
        }
        else
        {
            sql = sql + " and (fd.Class='" + DropDownList1.Items[1].ToString() + "'";
            string str = "";
            for (int i = 2; i < DropDownList1.Items.Count; i++)
            {
                if (str == "")
                {
                    str = "or fd.class='" + DropDownList1.Items[i].Text + "'";
                }
                else
                {
                    str = str + " or fd.class='" + DropDownList1.Items[i].Text + "'"; 
                }
            }
            str = str + ")  order by FirstName Asc";
            sql = sql + str;

        }
        Session["CardType"] = oo.ReturnTag(sql, "Card");

        GridView1.DataSource = oo.GridFill(sql);
        GridView1.DataBind();
        GridView1.Visible = true;

        sql = "  select SUM(fd.RecievedAmount) as TotAmt from FeeDeposite fd";
        sql = sql + "  inner join StudentOfficialDetails sod on fd.SrNo=sod.SrNo and fd.StEnRCode=sod.StEnRCode";
        sql = sql + "  where fd.Status='Paid' and sod.Card='" + DrpGroup.SelectedItem.ToString() + "'";
        sql = sql + "  and fd.FeeMonth='" + DropDownMonth.SelectedItem.ToString() + "'";
        sql = sql + "  and fd.BranchCode=" + Session["BranchCode"].ToString() + " and fd.Cancel is null";
        if (RadioButtonList2.SelectedIndex != 0)
        {
            sql = sql + " and fd.MoP='" + RadioButtonList2.SelectedItem.ToString() + "'";
        }
        if (DropDownList1.SelectedIndex != 0)
        {
            sql = sql + " and fd.Class='" + DropDownList1.SelectedItem.ToString() + "'  order by FirstName Asc";
        }
        else
        {
            sql = sql + " and (fd.Class='" + DropDownList1.Items[1].ToString() + "'";
            string str = "";
            for (int i = 2; i < DropDownList1.Items.Count; i++)
            {
                if (str == "")
                {
                    str = "or fd.class='" + DropDownList1.Items[i].Text + "'";
                }
                else
                {
                    str = str + " or fd.class='" + DropDownList1.Items[i].Text + "'"; 
                }
            }
            str = str + ")  order by FirstName Asc";
            sql = sql + str;

        }
        try
        {
            Label lbh = (Label)GridView1.FooterRow.FindControl("Label31");
            lbh.Text = oo.ReturnTag(sql, "TotAmt");
        }
        catch (Exception) { }
        if (GridView1.Rows.Count == 0)
        {
            oo.MessageBox("Sorry, No Record(s) found!", this.Page);
            Label32.Text = "Sorry, No Record(s) found!";
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
            Label3.Visible = false;
            Image1.Visible = true;
            Label1.Visible = true;
            Label2.Text = " of Class " + DropDownList1.SelectedItem.Text;
        }
    

           
    }


    protected void RadioButtonList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedItem.ToString() == "Installment Wise")
        {
            Panel2.Visible = true;
            Panel1.Visible = false;
            GridView1.DataSource = null;
            GridView1.DataBind();
            abc.Visible = false;
            LinkButton5.Visible = false;
            sql = "select MonthName from MonthMaster where CardType='" + DrpGroup.SelectedItem.ToString() + "'";
            sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            sql = sql + " or Monthid=0  ";
            sql = sql + " order by MonthId";

            oo.FillDropDownWithOutSelect(sql, DropDownMonth, "MonthName");
            drpFilter.Visible = false;
        }
        else
        {
            Panel1.Visible = true;
            Panel2.Visible = false;
            LinkButton5.Visible = true;
            abc.Visible = false;
            drpFilter.Visible = true;
        }
    }
}