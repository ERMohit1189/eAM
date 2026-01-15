using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Globalization;

public partial class admin_TransportFeeDepositeReport : Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

        if (!IsPostBack)
        {
            abc.Visible = false;
            sql = "Select FeeGroupName from FeeGroupMaster where SessionName='" + Session["SessionName"] + "'";
            oo.FillDropDownWithOutSelect(sql, drpFeegroup, "FeeGroupName");
            loadinsttalment();
            Image1.ImageUrl = "DisplayImage.ashx?UserLoginID=" + 1;
            sql = "Select CollegeName from CollegeMaster where CollegeId=" + 1;
            lblCollegeName.Text = oo.ReturnTag(sql, "CollegeName");
            loadclass();
            LinkButton1.Focus();
            
        }
    }
   
    public void transportfeeother()
    {
        if (Grd.Rows.Count > 0)
        {
            for (int i = 0; i < Grd.Rows.Count; i++)
            {
                Label lblClass = (Label)Grd.Rows[i].FindControl("lblClass");
                Label lblnoofMonth = (Label)Grd.Rows[i].FindControl("lblnoofMonth");

                //sql = " Select sod.SrNo,sod.WayAmount from StudentOfficialDetails sod inner join FeeDeposite fd on sod.SrNo=fd.SrNo";
                //sql = sql + " where sod.SessionName='" + Session["SessionName"].ToString() + "' and fd.SessionName='" + Session["SessionName"].ToString() + "'";
                //sql = sql + " and Class='" + lblClass.Text + "' and FeeMonth='" + drpInsttalment.SelectedItem.ToString() + "' and Card='" + drpFeegroup.SelectedItem.ToString() + "' and fd.BusConvience<>'0.00' and TransportRequired='No' and Cancel is null";
                sql = " Select Sum(Amount) as BusConvience from StudentVehicleAllotment sva ";
                sql = sql + " inner join FeeDeposite fd on sva.SrNo=fd.SrNo ";
                sql = sql + " inner join StudentOfficialDetails sod on sod.SrNo=fd.SrNo ";
                sql = sql + " where fd.Status='Paid' and fd.Class='" + lblClass.Text + "' and fd.SessionName='" + Session["SessionName"].ToString() + "' and sod.SessionName='" + Session["SessionName"].ToString() + "' and sva.SessionName='" + Session["SessionName"].ToString() + "'";
                sql = sql + " and fd.FeeMonth like '" + drpInsttalment.SelectedItem.ToString() + "' and sod.Card='" + drpFeegroup.SelectedItem.ToString() + "' and fd.BusConvience<>'0.00' and TransportRequired='No' and fd.Cancel is null";
                sql = sql + " and sva.Insttalment='" + drpInsttalment.SelectedItem.ToString() + "' and MonthStatus='1'";
                //SqlConnection con = new SqlConnection();
                //con = oo.dbGet_connection();
                //con.Open();
                //SqlDataAdapter da = new SqlDataAdapter(sql, con);
                //DataTable dt = new DataTable();
                //da.Fill(dt);
                double wayamount = 0;
                //if (dt.Rows.Count > 0)
                //{
                string waymonth;
                //    for (int j = 0; j < dt.Rows.Count; j++)
                //    {
                //        string srno = dt.Rows[j][0].ToString();
                //        sql = "select  InstallmentMonth from VehicleAllotmentTime where SrNo='" + srno + "'";
                //        if (oo.ReturnTag(sql, "InstallmentMonth") != "")
                //        {
                waymonth = oo.ReturnTag(sql, "BusConvience");
                //            for (int k = 0; k < waymonth.Length; k++)
                //            {
                //                if (waymonth[k] == drpInsttalment.SelectedValue.ToString())
                //                {
                //                    if (dt.Rows[j][1].ToString() != "")
                //                    {
                double amount = 0;
                if (!string.IsNullOrEmpty(waymonth))
                {
                    amount = Convert.ToDouble(waymonth);
                }
                wayamount = wayamount + (amount * Convert.ToDouble(lblnoofMonth.Text));
                //                    }
                //                }
                //            }
                //        }
                //    }


                //}
                //con.Close();
                Label lblAmount = (Label)Grd.Rows[i].FindControl("lblAmount");
                double GrossAmount = Convert.ToDouble(lblAmount.Text) + wayamount;
                lblAmount.Text = GrossAmount.ToString(".00");

            }
        }
    }

    public void loadclass()
    {
        sql = "Select ClassName,CidOrder from ClassMaster where SessionName='" + Session["SessionName"] + "' order by Cidorder";
        oo.FillDropDownWithOutSelect(sql, drpClass, "ClassName");
        drpClass.Items.Insert(0, "Select All");
    }

    public void loadinsttalment()
    {
        try
        {
            sql = "Select MonthName from MonthMaster where CardType='" + drpFeegroup.SelectedItem.ToString() + "' and SessionName='" + Session["SessionName"] + "' Order by MonthId ";
            oo.FillDropDownWithOutSelect(sql, drpInsttalment, "MonthName");
        }
        catch
        {
        }
    }

    //public void loadgrid()
    //{
    //    sql = " Select Distinct sod.SrNo as srno,(sgd.FirstName+' '+sgd.MiddleName+' '+sgd.LastName) as Name,cm.ClassName as Class,cm.CIDOrder,fm.NoOfMonths ";
    //    sql = sql + " from StudentOfficialDetails sod inner join ClassMaster cm on sod.AdmissionForClassId=cm.id ";
    //    sql = sql + " inner join FeeAllotedForClassWise fafcw on (sod.Medium=fafcw.Medium) and (sod.TypeOFAdmision=fafcw.AdmissionType) and (cm.ClassName=fafcw.Class) ";
    //    sql = sql + " inner join MonthMaster mm on mm.MonthName=fafcw.Month ";
    //    sql = sql + " inner join FeeMaster fm on (fafcw.Medium=fm.medium)  and (fafcw.FeeName=fm.FeeName) ";
    //    sql = sql + " inner join StudentGenaralDetail sgd on sod.SrNo=sgd.SrNo";
    //    sql = sql + " where sod.SessionName='" + Session["SessionName"] + "'";
    //    sql = sql + " and fafcw.SessionName='" + Session["SessionName"] + "'";
    //    sql = sql + " and sgd.SessionName='" + Session["SessionName"] + "'";
    //    sql = sql + " and mm.SessionName='" + Session["SessionName"] + "'";
    //    sql = sql + " and cm.SessionName='" + Session["SessionName"] + "'";
    //    sql = sql + " and fm.SessionName='" + Session["SessionName"] + "'";
    //    sql = sql + " and fafcw.Month='" + drpInsttalment.SelectedItem.ToString() + "' and sod.Card='" + drpFeegroup.SelectedItem.ToString() + "'";
    //    sql = sql + " and fafcw.CardType='" + drpFeegroup.SelectedItem.ToString() + "' and mm.CardType='" + drpFeegroup.SelectedItem.ToString() + "'";        
    //    if (drpClass.SelectedIndex == 0)
    //    {
    //        sql = sql + " and TransportRequired='Yes' and sod.Withdrwal is null Order by cm.CIDOrder ";
    //    }      
    //    else
    //    {
    //        sql = sql + " and TransportRequired='Yes' and cm.ClassName='"+drpClass.SelectedItem.ToString()+"' and sod.Withdrwal is null Order by cm.CIDOrder ";
    //    }
    //    Grd.DataSource = oo.GridFill(sql);
    //    Grd.DataBind();
    //    if (Grd.Rows.Count > 0)
    //    {
    //        abc.Visible = true;
    //    }
    //    else
    //    {
    //        abc.Visible = false;
    //    }
    //    Label3.Text = "Transport Report Till " + drpInsttalment.SelectedValue.ToString() + " Installment";

    //    if(Grd.Rows.Count>0)
    //    {
    //        for (int i=0; i < Grd.Rows.Count; i++)
    //        {

    //        }
    //    }
    //}

    public void loadgrid1()
    {
        sql = "Select asu.Srno,asu.ClassName,asu.CIDOrder from AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "'," + Session["BranchCode"].ToString() + ") asu";   
        sql = sql + " inner join FeeDeposite fd on asu.SrNo=fd.SrNo ";
        sql = sql + " where fd.Status='Paid' and fd.SessionName='" + Session["SessionName"] + "' and (fd.FeeMonth='Yearly' or fd.FeeMode<>'Yearly') ";
        sql = sql + " and Card='" + drpFeegroup.SelectedItem.ToString() + "' and TransportRequired='Yes' and Withdrwal is null";
      
        if (drpClass.SelectedIndex == 0)
        {
            sql = sql + " Union ";
            sql = sql + " (Select fd.srno,cm.ClassName,cm.CIDOrder from FeeDeposite fd inner join ClassMaster cm on fd.Class=cm.ClassName";
            sql = sql + " where fd.Status='Paid' and FeeMonth='" + drpInsttalment.SelectedItem.ToString() + "' and BusConvience<>'0.00' and fd.SessionName='" + Session["SessionName"] + "' and cm.SessionName='" + Session["SessionName"] + "') order by CIDOrder";
        }
        else
        {
            sql = sql + " and ClassName='" + drpClass.SelectedItem.ToString() + "'";
            sql = sql + " Union ";
            sql = sql + " (Select fd.srno,cm.ClassName,cm.CIDOrder from FeeDeposite fd inner join ClassMaster cm on fd.Class=cm.ClassName";
            sql = sql + " where fd.Status='Paid' and FeeMonth='" + drpInsttalment.SelectedItem.ToString() + "' and Class='" + drpClass.SelectedItem.ToString() + "' and BusConvience<>'0.00' and fd.SessionName='" + Session["SessionName"] + "' and cm.SessionName='" + Session["SessionName"] + "') order by CIDOrder";
        }

        Grd.DataSource = oo.GridFill(sql);
        Grd.DataBind();
        if (Grd.Rows.Count > 0)
        {
            abc.Visible = true;
        }
        else
        {
            abc.Visible = false;
        }
        Label3.Text = "Transport Report Till " + drpInsttalment.SelectedValue.ToString() + " Installment";
        //Campus camp = new Campus(); camp.msgbox(Label3, msgbox, "Transport Report Till " + drpInsttalment.SelectedValue.ToString() + " Installment", "S");       


        if (Grd.Rows.Count > 0)
        {
            for(int i=0;i<Grd.Rows.Count;i++)
            {
                Label lblsrno = (Label)Grd.Rows[i].FindControl("lblsrno");
                Label lblName = (Label)Grd.Rows[i].FindControl("lblName");
                Label lblClass = (Label)Grd.Rows[i].FindControl("lblClass");
                sql = " Select Name,ClassName as Class from AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "'," + Session["BranchCode"].ToString() + ")";
                sql = sql + " where Card='" + drpFeegroup.SelectedItem.ToString() + "' and SrNo='" + lblsrno.Text + "'";
                lblName.Text = oo.ReturnTag(sql, "Name");
                lblClass.Text = oo.ReturnTag(sql, "Class");
            }
            
        }
          
    }

    public void withtransport()
    {
        if (Grd.Rows.Count > 0)
        {
            for (int i = 0; i < Grd.Rows.Count; i++)
            {
                double wayamounts = 0;
                for (int j = 0; j <= drpInsttalment.SelectedIndex; j++)
                {
                    Label lblClass = (Label)Grd.Rows[i].FindControl("lblClass");
                    Label lblnoofMonth = (Label)Grd.Rows[i].FindControl("lblnoofMonth");
                    Label lblsrno = (Label)Grd.Rows[i].FindControl("lblsrno");
                    sql = " select Distinct fm.NoOfmonths as NoOfmonths from FeeAllotedForClassWise fa ";
                    sql = sql + " left join feemaster  fm on fa.Medium=fm.medium  and fa.FeeName=fm.FeeName  ";
                    sql = sql + " where fa.Class='" + lblClass.Text + "' and  fm.SessionName='" + Session["SessionName"].ToString() + "'";
                    sql = sql + " and fa.Month='" + drpInsttalment.Items[j].Text + "' and fa.SessionName='" + Session["SessionName"].ToString() + "'";
                    sql = sql + " and fa.CardType='" + drpFeegroup.SelectedItem.ToString() + "'";
                    double noofmonth = 1;
                    if(oo.ReturnTag(sql, "NoOfmonths")!="")
                    {
                    noofmonth = Convert.ToDouble(oo.ReturnTag(sql, "NoOfmonths"));
                    }
                    //sql = "Select sod.WayAmount as WayAmount from StudentOfficialDetails sod ";
                    //sql = sql + " inner join ClassMaster cm on sod.AdmissionForClassId=cm.Id where sod.SessionName='" + Session["SessionName"].ToString() + "' ";
                    //sql = sql + " and cm.ClassName='" + lblClass.Text + "' and cm.SessionName='" + Session["SessionName"].ToString() + "' ";
                    //sql = sql + " and sod.Card='" + drpFeegroup.SelectedValue.ToString() + "' ";
                    //sql = sql + " and sod.Withdrwal is null and sod.SrNo='" + lblsrno.Text + "'";
                    sql = "Select Sum(Amount) as Amount from StudentVehicleAllotment where SessionName='" + Session["SessionName"].ToString() + "' and MonthStatus='1' and SrNo='" + lblsrno.Text + "' and Insttalment='" + drpInsttalment.Items[j].Text + "'";
                    string wayamount = oo.ReturnTag(sql, "Amount");
                    //string[] waymonth;
                    //sql = "select  InstallmentMonth from VehicleAllotmentTime where SrNo='" + lblsrno.Text + "'";
                    //if (oo.ReturnTag(sql, "InstallmentMonth") != "")
                    //{
                    //    waymonth = oo.ReturnTag(sql, "InstallmentMonth").Split(' ');
                    //    for (int k = 0; k < waymonth.Length; k++)
                    //    {
                    //        if (waymonth[k] == drpInsttalment.Items[j].Text)
                    //        {
                    //            if (wayamount != "")
                    //            {
                    double amount = 0;
                    if (!string.IsNullOrEmpty(wayamount))
                    {
                        amount = Convert.ToDouble(wayamount);
                    }
                    wayamounts = wayamounts + (amount * Convert.ToDouble(noofmonth));
                    //            }
                    //        }
                    //    }
                    }
                    Label lblAmount = (Label)Grd.Rows[i].FindControl("lblAmount");
                    double GrossAmount = wayamounts;
                    if (wayamounts == 0)
                    {
                        lblAmount.Text = "0.00";
                    }
                    else
                    {
                        lblAmount.Text = GrossAmount.ToString(".00");
                    }



                //}

            }
        }
    }

    public void transportfeeyearly()
    {
        if (Grd.Rows.Count > 0)
        {
            for (int i = 0; i < Grd.Rows.Count; i++)
            {
                Label lblClass = (Label)Grd.Rows[i].FindControl("lblClass");
                Label lblnoofMonth = (Label)Grd.Rows[i].FindControl("lblnoofMonth");
                //sql = " Select fd.SrNo,sod.WayAmount from StudentOfficialDetails sod inner join FeeDeposite fd on sod.SrNo=fd.SrNo";
                //sql = sql + " where sod.SessionName='" + Session["SessionName"].ToString() + "' and fd.SessionName='" + Session["SessionName"].ToString() + "'";
                //sql = sql + " and Class='" + lblClass.Text + "' and FeeMonth='Yearly' and Card='" + drpFeegroup.SelectedItem.ToString() + "' and TransportRequired='Yes' and Cancel is null";
                sql = " Select Sum(Amount) as BusConvience from StudentVehicleAllotment sva ";
                sql = sql + " inner join FeeDeposite fd on sva.SrNo=fd.SrNo ";
                sql = sql + " inner join StudentOfficialDetails sod on sod.SrNo=fd.SrNo ";
                sql = sql + " where fd.Status='Paid' and fd.Class='" + lblClass.Text + "' and fd.SessionName='" + Session["SessionName"].ToString() + "' and sod.SessionName='" + Session["SessionName"].ToString() + "' and sva.SessionName='" + Session["SessionName"].ToString() + "'";
                sql = sql + " and fd.FeeMonth='Yearly' and sod.Card='" + drpFeegroup.SelectedItem.ToString() + "' and fd.BusConvience<>'0.00' and TransportRequired='Yes' and fd.Cancel is null";
                sql = sql + " and MonthStatus='1'";
                //SqlConnection con = new SqlConnection();
                //con = oo.dbGet_connection();
                //con.Open();
                //SqlDataAdapter da = new SqlDataAdapter(sql, con);
                //DataTable dt = new DataTable();
                //da.Fill(dt);
                double wayamount = 0;
                //if (dt.Rows.Count > 0)
                //{
                string waymonth;
                //    for (int j = 0; j < dt.Rows.Count; j++)
                //    {
                //        string srno = dt.Rows[j][0].ToString();
                //        sql = "select  InstallmentMonth from VehicleAllotmentTime where SrNo='" + srno + "'";
                //        if (oo.ReturnTag(sql, "InstallmentMonth") != "")
                //        {
                waymonth = oo.ReturnTag(sql, "BusConvience");
                //            for (int k = 0; k < waymonth.Length; k++)
                //            {
                //                if (waymonth[k] == drpInsttalment.SelectedValue.ToString())
                //                {
                //                    if (dt.Rows[j][1].ToString() != "")
                //                    {
                double amount = 0;
                if (!string.IsNullOrEmpty(waymonth))
                {
                    amount = Convert.ToDouble(waymonth);
                }
                wayamount = wayamount + (amount * Convert.ToDouble(lblnoofMonth.Text));
                //                    }
                //                }
                //            }
                //        }
                //    }


                //}
                con.Close();
                Label lblDepositeAmount = (Label)Grd.Rows[i].FindControl("lblDepositeAmount");
                double GrossAmount = Convert.ToDouble(lblDepositeAmount.Text) + wayamount;
                lblDepositeAmount.Text = GrossAmount.ToString(".00");

            }
        }
    }

    public void paidamount()
    {
        if (Grd.Rows.Count > 0)
        {
            for (int i = 0; i < Grd.Rows.Count; i++)
            {
                Label lblClass = (Label)Grd.Rows[i].FindControl("lblClass");
                Label lblsrno = (Label)Grd.Rows[i].FindControl("lblsrno");
                double busconvience = 0;
                for (int j = 0; j <= drpInsttalment.SelectedIndex; j++)
                {
                    sql = " Select Sum(Amount) as BusConvience from StudentVehicleAllotment sva ";
                    sql = sql + " inner join FeeDeposite fd on sva.SrNo=fd.SrNo ";
                    sql = sql + " inner join StudentOfficialDetails sod on sod.SrNo=fd.SrNo ";
                    sql = sql + " where fd.Status='Paid' and fd.Class='" + lblClass.Text + "' and fd.SessionName='" + Session["SessionName"].ToString() + "' and sod.SessionName='" + Session["SessionName"].ToString() + "' and sva.SessionName='" + Session["SessionName"].ToString() + "'";
                    sql = sql + " and (fd.FeeMonth like '%" + drpInsttalment.Items[j].Text + "%' or fd.FeeMonth='Yearly') and sod.Card='" + drpFeegroup.SelectedItem.ToString() + "' and fd.SrNo='" + lblsrno.Text + "' and fd.BusConvience<>'0.00' and fd.Cancel is null";
                    sql = sql + " and sva.Insttalment='" + drpInsttalment.Items[j].Text + "' and MonthStatus='1'";
                    if(oo.ReturnTag(sql,"BusConvience")!="")
                    {
                        busconvience = busconvience + Convert.ToDouble(oo.ReturnTag(sql, "BusConvience"));
                    }
                }
                Label lbldeposite = (Label)Grd.Rows[i].FindControl("lbldeposite");
                if (busconvience == 0)
                {
                    lbldeposite.Text = "0.00";
                }
                else
                {
                    lbldeposite.Text = busconvience.ToString(".00");
                }
               
               
            }
        }
       
    }

    protected void drpFeegroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadinsttalment();
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        loadgrid1();
        withtransport();
        paidamount();
        balancedAmount();
        counttotalamount();
    }

    public void balancedAmount()
    {
        if (Grd.Rows.Count > 0)
        {
            for (int i = 0; i < Grd.Rows.Count; i++)
            {
                Label lblAmount = (Label)Grd.Rows[i].FindControl("lblAmount");
                Label lblDeposite = (Label)Grd.Rows[i].FindControl("lblDeposite");
                Label lblBalance = (Label)Grd.Rows[i].FindControl("lblBalance");
                double TotalAmount = 0;
                if (lblAmount.Text != "")
                {
                    TotalAmount = Convert.ToDouble(lblAmount.Text);
                }
                double DepositeAmount = 0;
                if (lblDeposite.Text != "")
                {
                    DepositeAmount = Convert.ToDouble(lblDeposite.Text);
                }
                double NetBalance = TotalAmount - DepositeAmount;
                if (NetBalance != 0)
                {
                    lblBalance.Text = NetBalance.ToString(".00");
                }
                else
                {
                    lblBalance.Text = "0.00";
                }
            }
        }
    }

    public void counttotalamount()
    {
        if (Grd.Rows.Count > 0)
        {
            double TotalDueAmount = 0.00;
            double TotalDepositeAmount = 0.00;
            double TotalBalanceAmount = 0.00;
            for (int i = 0; i < Grd.Rows.Count; i++)
            {
                Label lblAmount = (Label)Grd.Rows[i].FindControl("lblAmount");
                TotalDueAmount = TotalDueAmount + Convert.ToDouble(lblAmount.Text);
                Label lblDepositeAmount = (Label)Grd.Rows[i].FindControl("lblDeposite");
                double totaldeposite = 0;
                if (lblDepositeAmount.Text != "")
                {
                    totaldeposite = Convert.ToDouble(lblDepositeAmount.Text);
                }
                TotalDepositeAmount = TotalDepositeAmount + totaldeposite;
                Label lblBalanceAmount = (Label)Grd.Rows[i].FindControl("lblBalance");
                double totalbalance = 0;
                if (lblBalanceAmount.Text != "")
                {
                    totalbalance = Convert.ToDouble(lblBalanceAmount.Text);
                }
                TotalBalanceAmount = TotalBalanceAmount + totalbalance;
            }

            Label lblTotalAmount = (Label)Grd.FooterRow.FindControl("lblTotalAmount");
            lblTotalAmount.Text = "Total Due Amount (Rs.) : " + TotalDueAmount.ToString("N", new CultureInfo("en-In"));
            Label lblTotalDepositeAmount = (Label)Grd.FooterRow.FindControl("lblTotalDepositeAmount");
            lblTotalDepositeAmount.Text = "Total Deposit Amount (Rs.) : " + TotalDepositeAmount.ToString("N", new CultureInfo("en-In"));
            Label lblTotalBalanceAmount = (Label)Grd.FooterRow.FindControl("lblTotalBalanceAmount");
            lblTotalBalanceAmount.Text = "Total Balance Amount (Rs.) : " + TotalBalanceAmount.ToString("N", new CultureInfo("en-In"));
        }
    }

    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        if (Grd.Rows.Count > 0)
        {
            oo.ExportToWord(Response, "TransportInstallmentWise", divExport);
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        if (Grd.Rows.Count > 0)
        {
            oo.ExportToExcel("TransportInstallmentWise.xls", Grd);
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
}