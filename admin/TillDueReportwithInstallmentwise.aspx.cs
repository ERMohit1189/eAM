using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

public partial class admin_TillDueReportwithInstallmentwise : Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file
        BLL.BLLInstance.LoadHeader("Report", header1);
        if (!IsPostBack)
        {      
            abc.Visible = false;
            sql = "Select FeeGroupName from FeeGroupMaster where SessionName='" + Session["SessionName"] + "'";
            oo.FillDropDownWithOutSelect(sql, drpFeegroup, "FeeGroupName");
            loadinsttalment();

            loadClass();
            loadInsttalment();
        }

    }

    public void loadClass()
    {
        BLL.BLLInstance.loadClass(drpClass,Session["SessionName"].ToString());
    }

    public void loadInsttalment()
    {
        BLL.BLLInstance.loadInsttalment(drpInsttalment, drpClass.SelectedValue.ToString(), Session["SessionName"].ToString());
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

    protected void drpFeegroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadinsttalment();
    }

    protected void btnshow_Click(object sender, EventArgs e)
    {
        loadclass();
        payamount();
        depositarrieramountfromarrier();
        undepositarrieramount();
        if (chkTransport.Checked)
        {
            withtransport();
            transportfeeyearly();
            transportfeeother();
            //deposittransportfee();
        }
        if (chkDiscount.Checked)
        {
            countdiscount();
            countconcession();
        }
        if (chkLatefee.Checked)
        {
            latefee();
        }
        //checkwithdrawl();
        fillblank();
        counttotalamount();
    }

    public void countconcession()
    {
        for (int i = 0; i < Grd.Rows.Count; i++)
        {
            Label lblClass = (Label)Grd.Rows[i].FindControl("lblClass");
            Label lblConcessionAmount = (Label)Grd.Rows[i].FindControl("lblConcessionAmount");
            double concession = 0;
            for (int j = 0; j <= drpInsttalment.SelectedIndex; j++)
            {
                sql = " Select Sum(fd.Cocession) as Concession from FeeDeposite fd";
                sql = sql + " inner join StudentOfficialDetails sod on fd.SrNo=sod.SrNo";
                sql = sql + " where fd.Status='Paid' and BalanceMode='Null' and sod.SessionName='" + Session["SessionName"].ToString() + "' and";
                sql = sql + " fd.SessionName='" + Session["SessionName"].ToString() + "'";
                sql = sql + " and fd.Class='" + lblClass.Text + "' and (fd.FeeMonth='" + drpInsttalment.Items[j].Text + "' or fd.FeeMonth='" + "(T) " + drpInsttalment.Items[j].Text + "')  and sod.Card='" + drpFeegroup.SelectedItem.ToString() + "' and fd.Cancel is null and (SOd.Promotion is null or SOd.Promotion<>'Cancelled')";

                if (oo.ReturnTag(sql, "Concession") != "")
                {
                    concession = concession + Convert.ToDouble(oo.ReturnTag(sql, "Concession"));
                }
            }
            if (concession == 0)
            {
                lblConcessionAmount.Text = "0.00";
            }
            else
            {
                lblConcessionAmount.Text = concession.ToString(".00");
            }
        }

    }

    public void checkwithdrawl()
    {
        if (Grd.Rows.Count > 0)
        {
            for (int i = 0; i < Grd.Rows.Count; i++)
            {
                Label lblClass = (Label)Grd.Rows[i].FindControl("lblClass");
                Label lblAmount = (Label)Grd.Rows[i].FindControl("lblAmount");
                Label lblDiscountAmount = (Label)Grd.Rows[i].FindControl("lblDiscountAmount");
                Label lblArreirAmount = (Label)Grd.Rows[i].FindControl("lblArreirAmount");
                Label lblLateFeeAmount = (Label)Grd.Rows[i].FindControl("lblLateFeeAmount");
                Label lblTransportAmount = (Label)Grd.Rows[i].FindControl("lblTransportAmount");


                sql = "Select sod.srno from StudentOfficialDetails sod ";
                sql = sql + " inner join ClassMaster cm on sod.AdmissionForClassId=cm.id  ";
                sql = sql + " where sod.SessionName='" + Session["SessionName"].ToString() + "' ";
                sql = sql + " and cm.SessionName='" + Session["SessionName"].ToString() + "' ";
                sql = sql + " and sod.Card='" + drpFeegroup.SelectedItem.ToString() + "' ";
                sql = sql + " and cm.ClassName='" + lblClass.Text + "' ";
                sql = sql + " and sod.Withdrwal='W' and (SOd.Promotion is null or SOd.Promotion<>'Cancelled') group by cm.CIDOrder,sod.srno Order by cm.CIDOrder";
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    double TotalFeeAmount = 0;
                    double RecievedAmount = 0;
                    double BusConvience = 0;
                    double DiscountAmount = 0;
                    double ArrierAmt = 0;

                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        for (int j1 = 0; j1 < drpInsttalment.SelectedIndex; j1++)
                        {
                            sql = "Select TotalFeeAmount,Cocession,RecievedAmount,LateFeeAmount,BusConvience,FeeMonth,";
                            sql = sql + "Case DiscountAmount when '' then '0' else DiscountAmount End as DiscountAmount ,ArrierAmt from FeeDeposite ";
                            sql = sql + " where SessionName='" + Session["SessionName"].ToString() + "' and Class='" + lblClass.Text + "' and FeeMonth='" + drpInsttalment.Items[j1].Text + "'";
                            sql = sql + " and SrNo='" + dt.Rows[j][0].ToString() + "' and Cancel is null and FeeMonth<>'Yearly'";

                            if (oo.ReturnTag(sql, "TotalFeeAmount") != "")
                            {
                                TotalFeeAmount = TotalFeeAmount + Convert.ToDouble(oo.ReturnTag(sql, "TotalFeeAmount"));
                            }

                            if (chkDiscount.Checked)
                            {
                                if (oo.ReturnTag(sql, "DiscountAmount") != "")
                                {
                                    DiscountAmount = DiscountAmount + Convert.ToDouble(oo.ReturnTag(sql, "DiscountAmount"));
                                }
                            }
                            if (oo.ReturnTag(sql, "RecievedAmount") != "")
                            {
                                RecievedAmount = RecievedAmount + Convert.ToDouble(oo.ReturnTag(sql, "RecievedAmount"));
                            }

                            if (chkTransport.Checked)
                            {
                                if (oo.ReturnTag(sql, "BusConvience") != "")
                                {
                                    BusConvience = BusConvience + Convert.ToDouble(oo.ReturnTag(sql, "BusConvience"));
                                }
                                string sql1;
                                sql1 = "Select BusConvience from FeeDeposite ";
                                sql1 = sql1 + " where SessionName='" + Session["SessionName"].ToString() + "' and Class='" + lblClass.Text + "' and FeeMonth='" + "(T) " + drpInsttalment.Items[j1].Text + "'";
                                sql1 = sql1 + " and SrNo='" + dt.Rows[j][0].ToString() + "' and Cancel is null and FeeMonth<>'Yearly'";
                                if (oo.ReturnTag(sql1, "BusConvience") != "")
                                {
                                    BusConvience = BusConvience + Convert.ToDouble(oo.ReturnTag(sql1, "BusConvience"));
                                }
                            }
                            if (j1 == 0)
                            {
                                if (oo.ReturnTag(sql, "ArrierAmt") != "")
                                {
                                    ArrierAmt = ArrierAmt + Convert.ToDouble(oo.ReturnTag(sql, "ArrierAmt"));
                                }
                            }
                        }
                    }
                    if (lblArreirAmount.Text != "")
                    {
                        if (ArrierAmt != 0)
                        {
                            lblArreirAmount.Text = (Convert.ToDouble(lblArreirAmount.Text) + ArrierAmt).ToString(".00");
                        }
                    }
                    else
                    {
                        if (ArrierAmt != 0)
                        {
                            lblArreirAmount.Text = ArrierAmt.ToString("0.00");
                        }
                        else
                        {
                            lblArreirAmount.Text = "0.00";
                        }

                    }

                    if (lblDiscountAmount.Text != "")
                    {
                        lblDiscountAmount.Text = (Convert.ToDouble(lblDiscountAmount.Text) + DiscountAmount).ToString(".00");
                    }
                    else
                    {
                        if (DiscountAmount != 0)
                        {
                            lblDiscountAmount.Text = DiscountAmount.ToString("0.00");
                        }
                        else
                        {
                            lblDiscountAmount.Text = "0.00";
                        }

                    }
                    if (lblTransportAmount.Text != "")
                    {
                        if (BusConvience != 0)
                        {
                            lblTransportAmount.Text = (Convert.ToDouble(lblTransportAmount.Text) + BusConvience).ToString(".00");
                        }
                    }
                    else
                    {
                        if (BusConvience != 0)
                        {
                            lblTransportAmount.Text = BusConvience.ToString("0.00");
                        }
                        else
                        {
                            lblTransportAmount.Text = "0.00";
                        }
                    }
                    if (lblAmount.Text != "")
                    {
                        lblAmount.Text = (Convert.ToDouble(lblAmount.Text) + TotalFeeAmount).ToString(".00");
                    }
                    else
                    {
                        if (TotalFeeAmount != 0)
                        {
                            lblAmount.Text = TotalFeeAmount.ToString("0.00");
                        }
                        else
                        {
                            lblAmount.Text = "0.00";
                        }

                    }

                }
            }
        }
    }
    
    protected void fillblank()
    {
        for (int i = 0; i < Grd.Rows.Count; i++)
        {
            Label lblDepositeAmount = (Label)Grd.Rows[i].FindControl("lblDepositeAmount");
            if (lblDepositeAmount.Text == "")
            {
                lblDepositeAmount.Text = "0.00";
            }
        }
    }

    public void counttotalamount()
    {
        try
        {
            if (Grd.Rows.Count > 0)
            {
                double TotalTransportAmount = 0;
                double TotalDueAmount = 0;
                double TotalDepositeAmount = 0;
                double TotalBalanceAmount = 0;
                double TotalDisAmount = 0;
                double TotalArrAmount = 0;
                double TotalConAmount = 0;
                double TotalLatefeeAmount = 0;
                double GrossDueAmount = 0;
                double ActualDueAmount = 0;
                double ActualDipositAmount = 0;
                int TotalStrength = 0;

                for (int i = 0; i < Grd.Rows.Count; i++)
                {
                    Label lblTransportAmount = (Label)Grd.Rows[i].FindControl("lblTransportAmount");
                    if (lblTransportAmount.Text != "")
                    {
                        TotalTransportAmount = TotalTransportAmount + Convert.ToDouble(lblTransportAmount.Text);
                    }
                    Label lblStrength = (Label)Grd.Rows[i].FindControl("lblStrength");
                    if (lblStrength.Text != "")
                    {
                        TotalStrength = TotalStrength + Convert.ToInt16(lblStrength.Text);
                    }
                    Label lblLateFeeAmount = (Label)Grd.Rows[i].FindControl("lblLateFeeAmount");
                    if (lblLateFeeAmount.Text != "")
                    {
                        TotalLatefeeAmount = TotalLatefeeAmount + Convert.ToDouble(lblLateFeeAmount.Text);
                    }
                    Label lblAmount = (Label)Grd.Rows[i].FindControl("lblAmount");
                    if (lblAmount.Text != "")
                    {
                        TotalDueAmount = TotalDueAmount + Convert.ToDouble(lblAmount.Text);
                    }
                    Label lblDepositeAmount = (Label)Grd.Rows[i].FindControl("lblDepositeAmount");
                    if (lblDepositeAmount.Text != "")
                    {
                        TotalDepositeAmount = TotalDepositeAmount + Convert.ToDouble(lblDepositeAmount.Text);
                    }
                    Label lblDiscountAmount = (Label)Grd.Rows[i].FindControl("lblDiscountAmount");
                    if (lblDiscountAmount.Text != "")
                    {
                        TotalDisAmount = TotalDisAmount + Convert.ToDouble(lblDiscountAmount.Text);
                    }
                    Label lblConcessionAmount = (Label)Grd.Rows[i].FindControl("lblConcessionAmount");
                    if (lblConcessionAmount.Text != "")
                    {
                        TotalConAmount = TotalConAmount + Convert.ToDouble(lblConcessionAmount.Text);
                    }
                    Label lblArreirAmount = (Label)Grd.Rows[i].FindControl("lblArreirAmount");
                    if (lblArreirAmount.Text != "")
                    {
                        TotalArrAmount = TotalArrAmount + Convert.ToDouble(lblArreirAmount.Text);
                    }
                    Label lblTotalDueAmount = (Label)Grd.Rows[i].FindControl("lblTotalDueAmount");
                    lblTotalDueAmount.Text = (Convert.ToDouble(lblAmount.Text) + Convert.ToDouble(lblArreirAmount.Text) + Convert.ToDouble(lblTransportAmount.Text) + Convert.ToDouble(lblLateFeeAmount.Text)).ToString(".00");
                    if (lblTotalDueAmount.Text != "")
                    {
                        GrossDueAmount = GrossDueAmount + Convert.ToDouble(lblTotalDueAmount.Text);
                    }
                    Label lblActualDueAmount = (Label)Grd.Rows[i].FindControl("lblActualDueAmount");
                    lblActualDueAmount.Text = (Convert.ToDouble(lblTotalDueAmount.Text) - (Convert.ToDouble(lblDiscountAmount.Text) + Convert.ToDouble(lblConcessionAmount.Text))).ToString(".00");
                    if (lblActualDueAmount.Text != "")
                    {
                        ActualDueAmount = ActualDueAmount + Convert.ToDouble(lblActualDueAmount.Text);
                    }
                    Label lblActualDepositAmount = (Label)Grd.Rows[i].FindControl("lblActualDepositAmount");
                    lblActualDepositAmount.Text = (Convert.ToDouble(lblDepositeAmount.Text) + Convert.ToDouble(lblLateFeeAmount.Text)).ToString(".00");
                    if (lblActualDepositAmount.Text != "")
                    {
                        ActualDipositAmount = ActualDipositAmount + Convert.ToDouble(lblActualDepositAmount.Text);
                    }
                    Label lblBalanceAmount = (Label)Grd.Rows[i].FindControl("lblBalanceAmount");
                    double amt = Convert.ToDouble(lblActualDueAmount.Text) - Convert.ToDouble(lblDepositeAmount.Text);
                    if (amt != 0)
                    {
                        lblBalanceAmount.Text = amt.ToString(".00");
                    }
                    else
                    {
                        lblBalanceAmount.Text = "0.00";
                    }
                    if (lblBalanceAmount.Text != "")
                    {
                        TotalBalanceAmount = TotalBalanceAmount + Convert.ToDouble(lblBalanceAmount.Text);
                    }

                }

                Label lblTotalStrength = (Label)Grd.FooterRow.FindControl("lblTotalStrength");
                lblTotalStrength.Text = "Total Strength : " + TotalStrength.ToString();
                Label lblTotalAmount = (Label)Grd.FooterRow.FindControl("lblTotalAmount");
                lblTotalAmount.Text = "Due Amount (Rs.) : " + TotalDueAmount.ToString("N", new CultureInfo("en-In"));
                Label lblTotalTransportAmount = (Label)Grd.FooterRow.FindControl("lblTotalTransportAmount");
                lblTotalTransportAmount.Text = "Due Amount (Rs.) : " + TotalTransportAmount.ToString();
                Label lblTotalDepositeAmount = (Label)Grd.FooterRow.FindControl("lblTotalDepositeAmount");
                lblTotalDepositeAmount.Text = "Deposit Amount (Rs.) : " + TotalDepositeAmount.ToString("N", new CultureInfo("en-In"));
                Label lblGrossDueAmount = (Label)Grd.FooterRow.FindControl("lblGrossDueAmount");
                lblGrossDueAmount.Text = "Total Due Amount (Rs.) : " + GrossDueAmount.ToString("N", new CultureInfo("en-In"));
                Label lblTotalDiscountAmount = (Label)Grd.FooterRow.FindControl("lblTotalDiscountAmount");
                lblTotalDiscountAmount.Text = "Discount Amount (Rs.) : " + TotalDisAmount.ToString("N", new CultureInfo("en-In"));
                Label lblTotalConcessionAmount = (Label)Grd.FooterRow.FindControl("lblTotalConcessionAmount");
                lblTotalConcessionAmount.Text = "Concession Amount (Rs.) : " + TotalConAmount.ToString("N", new CultureInfo("en-In"));
                Label lblTotalLateFeeAmount = (Label)Grd.FooterRow.FindControl("lblTotalLateFeeAmount");
                lblTotalLateFeeAmount.Text = "Late Fee (Rs.) : " + TotalLatefeeAmount.ToString("N", new CultureInfo("en-In"));
                Label lblTotalArreirAmount = (Label)Grd.FooterRow.FindControl("lblTotalArreirAmount");
                lblTotalArreirAmount.Text = "Arreir Amount (Rs.) : " + TotalArrAmount.ToString("N", new CultureInfo("en-In"));
                Label lblTotalActualDueAmount = (Label)Grd.FooterRow.FindControl("lblTotalActualDueAmount");
                lblTotalActualDueAmount.Text = "Actual Due Amount (Rs.) : " + ActualDueAmount.ToString("N", new CultureInfo("en-In"));
                Label lblTotalActualDepositAmount = (Label)Grd.FooterRow.FindControl("lblTotalActualDepositAmount");
                lblTotalActualDepositAmount.Text = "Actual Deposit Amount (Rs.) : " + ActualDipositAmount.ToString("N", new CultureInfo("en-In"));
                Label lblTotalBalanceAmount = (Label)Grd.FooterRow.FindControl("lblTotalBalanceAmount");
                lblTotalBalanceAmount.Text = "Total Balance Amount (Rs.) : " + TotalBalanceAmount.ToString("N", new CultureInfo("en-In"));
            }
        }
        catch
        {
        }
    }

    public void payamount()
    {
        if (Grd.Rows.Count > 0)
        {
            for (int i = 0; i < Grd.Rows.Count; i++)
            {
                Label lblClass = (Label)Grd.Rows[i].FindControl("lblClass");
                Label lblDepositeAmount = (Label)Grd.Rows[i].FindControl("lblDepositeAmount");
                double amount = 0;
                for (int j = 0; j <= drpInsttalment.SelectedIndex; j++)
                {
                    sql = "  select sum(fd.RecievedAmount) as RecievedAmount from ClassMaster cm";
                    sql = sql + " left join FeeDeposite fd on cm.ClassName=fd.Class inner join StudentOfficialDetails sod on fd.SrNo=sod.SrNo";
                    sql = sql + " where fd.Status='Paid' and sod.Card='" + drpFeegroup.SelectedItem.ToString() + "' and sod.SessionName='" + Session["SessionName"].ToString() + "'";
                    sql = sql + " and fd.Cancel is null and cm.className='" + lblClass.Text + "' ";
                    sql = sql + " and (fd.FeeMonth='" + drpInsttalment.Items[j].Text + "' or fd.FeeMonth='" + "(T) " + drpInsttalment.Items[j].Text + "')";
                    sql = sql + " and fd.SessionName='" + Session["SessionName"].ToString() + "' and fd.Cancel is null";
                    sql = sql + " and cm.SessionName='" + Session["SessionName"].ToString() + "' and (SOd.Promotion is null or SOd.Promotion<>'Cancelled')";

                    if (oo.ReturnTag(sql, "RecievedAmount") != "")
                    {
                        amount = amount+ Convert.ToDouble(oo.ReturnTag(sql, "RecievedAmount"));
                    }                   
                }
              
                lblDepositeAmount.Text = amount.ToString(".00");
            }

            if (Grd.Rows.Count > 0)
            {
                for (int i = 0; i < Grd.Rows.Count; i++)
                {
                    Label lblClass = (Label)Grd.Rows[i].FindControl("lblClass");
                  
                    for (int j1 = 1; j1 <= drpInsttalment.SelectedIndex; j1++)
                    {
                        sql = "Select Sum(fafcw.FeePayment) FeePayment,fd.SrNo from FeeDeposite fd  ";
                        sql = sql + " inner join ClassMaster cm on cm.ClassName=fd.Class and cm.SessionName=fd.SessionName ";
                        sql = sql + " inner join StudentOfficialDetails sod on sod.SrNo=fd.SrNo and sod.SessionName=fd.SessionName ";
                        sql = sql + " inner join FeeAllotedForClassWise fafcw on (sod.Medium=fafcw.Medium)  ";
                        sql = sql + " and (sod.TypeOFAdmision=fafcw.AdmissionType)   and (sod.Card=fafcw.CardType)   and (Case When ISNUMERIC(fafcw.Class)=1  ";
                        sql = sql + " THEN Convert(Nvarchar(11),cm.Id) ELSE cm.ClassName END)=fafcw.Class and fafcw.SessionName=sod.SessionName ";
                        sql = sql + " where fd.SessionName='" + Session["SessionName"].ToString() + "' and fd.Class='" + lblClass.Text + "' and FeeMonth='Yearly' ";
                        sql = sql + " and Month='" + drpInsttalment.Items[j1].Text + "' group by fd.SrNo";
                       

                        SqlConnection con = new SqlConnection();
                        con = oo.dbGet_connection();
                        con.Open();
                        SqlDataAdapter da = new SqlDataAdapter(sql, con);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            string sql1 = "";
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                sql1 = "Select DiscountValue from DiscountMaster where srno='" + dt.Rows[j][1].ToString() + "'";
                                sql1 = sql1 + " and SessionName='" + Session["SessionName"].ToString() + "'";
                                
                                string[] discount;
                                double FeePayment = 0;
                                discount = oo.ReturnTag(sql1, "DiscountValue").Split(' ');
                                Label lblDepositeAmount = (Label)Grd.Rows[i].FindControl("lblDepositeAmount");
                                Label lblAmount = (Label)Grd.Rows[i].FindControl("lblAmount");
                                FeePayment = Convert.ToDouble(dt.Rows[j][0].ToString());
                               
                                if (discount.Length >= j1)
                                {
                                    try
                                    {
                                        if (discount[j1] != "")
                                        {                                                                                        
                                            Label lblDiscountAmount = (Label)Grd.Rows[i].FindControl("lblDiscountAmount");
                                            
                                            double GrossDisAmount = 0;
                                            //FeePayment = FeePayment - Convert.ToDouble(discount[j1-1]);
                                            if (lblDiscountAmount.Text != "")
                                            {
                                                GrossDisAmount = Convert.ToDouble(lblDiscountAmount.Text) + Convert.ToDouble(discount[j1 - 1]);
                                            }
                                            else
                                            {
                                                GrossDisAmount = Convert.ToDouble(discount[j1 - 1]);
                                            }
                                            if (GrossDisAmount != 0)
                                            {
                                                lblDiscountAmount.Text = GrossDisAmount.ToString(".00");
                                            }
                                            else
                                            {
                                                lblDiscountAmount.Text = "0.00";
                                            }

                                        }
                                    }
                                    catch
                                    {
                                    }
                                }
                                double GrossAmount = 0;
                                if (lblDepositeAmount.Text != "")
                                {
                                    GrossAmount = Convert.ToDouble(lblDepositeAmount.Text) + FeePayment;
                                }
                                if (GrossAmount != 0)
                                {
                                    lblDepositeAmount.Text = GrossAmount.ToString(".00");
                                    lblAmount.Text = (Convert.ToDouble(lblAmount.Text) + FeePayment).ToString(".00");
                                }
                                else
                                {
                                    lblDepositeAmount.Text = "0.00";
                                }
                               
                            }

                        }
                        con.Close();
                    }
               
                }

            }

        }


    }

    public void loadclass()
    {
        sql = "Select Count(SrNo) Strength,ClassName,ClassId from AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "'," + Session["BranchCode"].ToString() + ")";
        sql = sql + " where ClassId='" + drpClass.SelectedValue.ToString() + "' and Withdrwal is null and (Promotion is null or Promotion<>'Cancelled')";
        sql = sql + " Group by ClassName,ClassId";

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
        if (Grd.Rows.Count > 0)
        {
            for (int i = 0; i < Grd.Rows.Count; i++)
            {
                double dueamount = 0;
                for (int j = 1; j <= drpInsttalment.SelectedIndex; j++)
                {
                    Label lblClass = (Label)Grd.Rows[i].FindControl("lblClass");

                    sql = "Select Sum(fafcw.FeePayment) FeePayment from";
                    sql = sql + " AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "'," + Session["BranchCode"].ToString() + ") sod ";
                    sql = sql + " inner join ClassMaster cm on cm.id=sod.ClassId and cm.SessionName=sod.SessionName";
                    sql = sql + " inner join FeeAllotedForClassWise fafcw on (sod.Medium=fafcw.Medium) and (sod.TypeOFAdmision=fafcw.AdmissionType)  ";
                    sql = sql + " and (sod.Card=fafcw.CardType)  ";
                    sql = sql + " and (Case When ISNUMERIC(fafcw.Class)=1 THEN Convert(Nvarchar(11),cm.Id) ELSE cm.ClassName END)=fafcw.Class";
                    sql = sql + " and fafcw.SessionName=sod.SessionName";
                    sql = sql + " where sod.SessionName='" + Session["SessionName"].ToString() + "' and cm.ClassName='" + lblClass.Text + "' and Month='" + drpInsttalment.Items[j].Text + "'";
                    sql = sql + " and Withdrwal is null";

                    if (oo.ReturnTag(sql, "FeePayment") != "")
                    {
                        dueamount = dueamount + Convert.ToDouble(oo.ReturnTag(sql, "FeePayment"));
                    }
                }
                Label lblAmount = (Label)Grd.Rows[i].FindControl("lblAmount");
                lblAmount.Text = dueamount.ToString(".00");
            }

        }

        Label3.Text = "Due Report Till " + drpInsttalment.SelectedValue.ToString() + " Installment";

        if (Grd.Rows.Count > 0)
        {
            for (int i = 0; i < Grd.Rows.Count; i++)
            {

                Label lblClass = (Label)Grd.Rows[i].FindControl("lblClass");
                Label lblArreirAmount = (Label)Grd.Rows[i].FindControl("lblArreirAmount");

                string className = lblClass.Text;
                double arrieramount = 0;

                sql = "Select Sum(fd.ArrierAmt) as ArrierAmt from FeeDeposite fd inner join StudentOfficialDetails sod on fd.SrNo=sod.SrNo where fd.Status='Paid' and fd.Cancel is null and fd.Class='" + className + "' and fd.SessionName='" + Session["SessionName"].ToString() + "' and sod.SessionName='" + Session["SessionName"].ToString() + "' and sod.Card='" + drpFeegroup.SelectedItem.ToString() + "' and (SO.Promotion is null or SO.Promotion<>'Cancelled')";
                sql = sql + "and fd.FeeMonth='" + drpInsttalment.Items[0].Text + "'";

                if (oo.ReturnTag(sql, "ArrierAmt") != "")
                {
                    arrieramount = arrieramount + Convert.ToDouble(oo.ReturnTag(sql, "ArrierAmt"));
                }


                if (arrieramount != 0)
                {
                    lblArreirAmount.Text = arrieramount.ToString(".00");
                }
                else
                {
                    lblArreirAmount.Text = "0.00";
                }

            }
            //ifClassChengedofStudent();

        }
    }

    public void depositarrieramountfromarrier()
    {
        if (Grd.Rows.Count > 0)
        {
            for (int i = 0; i < Grd.Rows.Count; i++)
            {

                Label lblClass = (Label)Grd.Rows[i].FindControl("lblClass");
                Label lblArreirAmount = (Label)Grd.Rows[i].FindControl("lblArreirAmount");
                Label lblDepositeAmount = (Label)Grd.Rows[i].FindControl("lblDepositeAmount");

                sql = "Select Sum(fd.ArrierAmt) as ArrierAmt from FeeDeposite fd inner join StudentOfficialDetails sod on fd.SrNo=sod.SrNo where fd.Status='Paid' and fd.Cancel is null and fd.Class='" + lblClass.Text + "' and fd.SessionName='" + Session["SessionName"].ToString() + "' and sod.SessionName='" + Session["SessionName"].ToString() + "' and sod.Card='" + drpFeegroup.SelectedItem.ToString() + "'";
                sql = sql + " and fd.FeeMonth='Arrier' and (SOd.Promotion is null or SOd.Promotion<>'Cancelled')";

                double ArreirAmount = 0;
                if(!string.IsNullOrEmpty(oo.ReturnTag(sql,"ArrierAmt")))
                {
                    ArreirAmount=Convert.ToDouble(oo.ReturnTag(sql,"ArrierAmt"));
                }
                if (lblArreirAmount.Text != "")
                {
                    lblArreirAmount.Text = (Convert.ToDouble(lblArreirAmount.Text) + ArreirAmount).ToString();
                }
                else
                {
                    lblArreirAmount.Text = ArreirAmount.ToString();
                }
                if (lblDepositeAmount.Text != "")
                {
                    lblDepositeAmount.Text = (Convert.ToDouble(lblDepositeAmount.Text) + ArreirAmount).ToString();
                }
                else
                {
                    lblDepositeAmount.Text = ArreirAmount.ToString();
                }

            }
        }

    }

    public void undepositarrieramount()
    {
        if (Grd.Rows.Count > 0)
        {

            for (int i = 0; i < Grd.Rows.Count; i++)
            {

                Label lblClass = (Label)Grd.Rows[i].FindControl("lblClass");
                Label lblArreirAmount = (Label)Grd.Rows[i].FindControl("lblArreirAmount");

                sql = "Select Sum(TransportAmount+DiscountAmount+LateFee+LastArrierAmount+FeeAmount) as ArrierAmt from ArrierMast am";
                sql = sql + " inner join StudentOfficialDetails sod  on am.Srno=sod.SrNo";
                sql = sql + " inner join ClassMaster cm on cm.Id=sod.AdmissionForClassId";
                sql = sql + " where cm.SessionName='" + Session["SessionName"].ToString() + "' and am.SessionName='" + Session["SessionName"].ToString() + "'";
                sql = sql + " and sod.SessionName='" + Session["SessionName"].ToString() + "' and sod.Card='" + drpFeegroup.SelectedItem.ToString() + "'";
                sql = sql + " and cm.ClassName='" + lblClass.Text + "' and sod.Withdrwal is null and (SOd.Promotion is null or SOd.Promotion<>'Cancelled')";

                double ArreirAmount = 0;
                if (!string.IsNullOrEmpty(oo.ReturnTag(sql, "ArrierAmt")))
                {
                    ArreirAmount = Convert.ToDouble(oo.ReturnTag(sql, "ArrierAmt"));
                }
                if (lblArreirAmount.Text != "")
                {
                    ArreirAmount = Convert.ToDouble(lblArreirAmount.Text) + ArreirAmount;
                }

                if (ArreirAmount == 0)
                {
                    lblArreirAmount.Text = "0.00";
                }
                else
                {
                    lblArreirAmount.Text = ArreirAmount.ToString(".00");
                }
            }
        }
    }

    public void ifClassChengedofStudent()
    {
        if (Grd.Rows.Count > 0)
        {
            for (int i = 0; i < Grd.Rows.Count; i++)
            {
                Label lblClass = (Label)Grd.Rows[i].FindControl("lblClass");
                Label lblAmount = (Label)Grd.Rows[i].FindControl("lblAmount");

                sql = "Select SUM(RecievedAmount) as Amount,cm.ClassName,SUM(fd.Cocession) as Cocession,SUM(Convert(int,fd.DiscountAmount))as DiscountAmount,SUM(LateFeeAmount) as LateFeeAmount from FeeDeposite fd";
                sql = sql + " inner join StudentOfficialDetails so on so.SrNo=fd.srno";
                sql = sql + " inner join ClassMaster cm on cm.Id=so.AdmissionForClassId";
                sql = sql + " where cm.SessionName='" + Session["SessionName"].ToString() + "' and so.SessionName='" + Session["SessionName"].ToString() + "' and fd.SessionName='" + Session["SessionName"].ToString() + "'";
                sql = sql + " and fd.Class='" + lblClass.Text + "' and cm.ClassName<>'" + lblClass.Text + "' and fd.Cancel is null and (SO.Promotion is null or SO.Promotion<>'Cancelled') group by ClassName";
                if (oo.ReturnTag(sql, "Amount") != "")
                {
                    double amount = (Convert.ToDouble(oo.ReturnTag(sql, "Amount")) + Convert.ToDouble(oo.ReturnTag(sql, "LateFeeAmount"))) - (Convert.ToDouble(oo.ReturnTag(sql, "Cocession")) + Convert.ToDouble(oo.ReturnTag(sql, "DiscountAmount")));
                    double currentamount = Convert.ToDouble(lblAmount.Text);
                    lblAmount.Text = (currentamount + amount).ToString(".00");
                    minusamount(oo.ReturnTag(sql, "ClassName"), amount);
                }
            }
        }
    }

    public void minusamount(string classname, double amount)
    {
        if (Grd.Rows.Count > 0)
        {
            for (int i = 0; i < Grd.Rows.Count; i++)
            {
                Label lblClass = (Label)Grd.Rows[i].FindControl("lblClass");
                Label lblAmount = (Label)Grd.Rows[i].FindControl("lblAmount");
                if (lblClass.Text == classname)
                {
                    lblAmount.Text = (Convert.ToDouble(lblAmount.Text) - amount).ToString(".00");
                }
            }
        }
    }

    public void countdiscount()
    {
        if (Grd.Rows.Count > 0)
        {
            for (int i = 0; i < Grd.Rows.Count; i++)
            {
                Label lblClass = (Label)Grd.Rows[i].FindControl("lblClass");
                Label lblDiscountAmount = (Label)Grd.Rows[i].FindControl("lblDiscountAmount");
                sql = "select DiscountValue  from DiscountMaster dm ";
                sql = sql + " left join StudentOfficialDetails sd on dm.SrNo=sd.SrNo ";
                sql = sql + " left join ClassMaster cm on sd.AdmissionForClassId=cm.Id ";
                sql = sql + " where cm.SessionName='" + Session["SessionName"].ToString() + "'";
                sql = sql + " and sd.SessionName='" + Session["SessionName"].ToString() + "'";
                sql = sql + " and dm.SessionName='" + Session["SessionName"].ToString() + "'";
                sql = sql + " and cm.ClassName='" + lblClass.Text + "' and sd.Card='" + drpFeegroup.SelectedValue.ToString() + "' and Withdrwal is null and (Sd.Promotion is null or Sd.Promotion<>'Cancelled')";
                SqlConnection con = new SqlConnection();
                con = oo.dbGet_connection();
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                double discvalue = 0;
                if (dt.Rows.Count > 0)
                {
                    string[] discount;
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        discount = dt.Rows[j][0].ToString().Split(' ');
                        if (discount.Length == 1)
                        {
                            if (discount[0] != "")
                            {
                                for (int j1 = 0; j1 < drpInsttalment.SelectedIndex; j1++)
                                {
                                    discvalue = discvalue + Convert.ToDouble(discount[j1]);                                    
                                }
                            }
                        }
                        else
                        {
                            
                            if (discount[drpInsttalment.SelectedIndex] != "")
                            {
                                for (int j1 = 0; j1 < drpInsttalment.SelectedIndex; j1++)
                                {
                                    discvalue = discvalue + (Convert.ToDouble(discount[j1]));
                                }
                            }
                        }

                    }
                }
                con.Close();
                if (lblDiscountAmount.Text != string.Empty)
                {
                    discvalue = discvalue + (Convert.ToDouble(lblDiscountAmount.Text));
                }
                if (discvalue == 0)
                {
                    lblDiscountAmount.Text = "0.00";
                }
                else
                {
                    lblDiscountAmount.Text = discvalue.ToString(".00");
                }
               
            }
        }
    }

    public void latefee()
    {
        if (Grd.Rows.Count > 0)
        {
          
            for (int i = 0; i < Grd.Rows.Count; i++)
            {

                Label lblClass = (Label)Grd.Rows[i].FindControl("lblClass");
                Label lblLateFeeAmount = (Label)Grd.Rows[i].FindControl("lblLateFeeAmount");
                string className = lblClass.Text;
                double latefee = 0.00;
                for (int j = 1; j <= drpInsttalment.SelectedIndex; j++)
                {
                    sql = " Select Sum(fd.LateFeeAmount) as LateFee from FeeDeposite fd ";
                    sql = sql + " inner join StudentOfficialDetails sod on fd.SrNo=sod.SrNo";
                    sql = sql + " where fd.Status='Paid' and fd.Cancel is null and fd.Class='" + className + "' and fd.SessionName='" + Session["SessionName"].ToString() + "'";
                    sql = sql + " and BalanceMode='Null' and (fd.FeeMonth='" + drpInsttalment.Items[j].Text + "' or fd.FeeMonth='" + "(T) " + drpInsttalment.Items[j].Text + "') and sod.SessionName='" + Session["SessionName"].ToString() + "' and sod.Card='" + drpFeegroup.SelectedItem.ToString() + "' and (SOd.Promotion is null or SOd.Promotion<>'Cancelled')";

                    if (oo.ReturnTag(sql, "LateFee") != "")
                    {
                        latefee = latefee + Convert.ToDouble(oo.ReturnTag(sql, "LateFee"));
                    }
                   
                }

                sql = " Select Sum(fd.LateFeeAmount) as LateFee from FeeDeposite fd ";
                sql = sql + " inner join StudentOfficialDetails sod on fd.SrNo=sod.SrNo";
                sql = sql + " where fd.Status='Paid' and fd.Cancel is null and fd.Class='" + className + "' and fd.SessionName='" + Session["SessionName"].ToString() + "'";
                sql = sql + " and BalanceMode='Null' and (fd.FeeMonth='Yearly') and sod.SessionName='" + Session["SessionName"].ToString() + "'";
                sql = sql + " and sod.Card='" + drpFeegroup.SelectedItem.ToString() + "' and (SOd.Promotion is null or SOd.Promotion<>'Cancelled')";

                if (oo.ReturnTag(sql, "LateFee") != "")
                {
                    latefee = latefee + Convert.ToDouble(oo.ReturnTag(sql, "LateFee"));
                }

                if (latefee == 0)
                {
                    lblLateFeeAmount.Text = "0.00";
                }
                else
                {
                    lblLateFeeAmount.Text = latefee.ToString(".00");
                }
                
            }
        }
        CalculateFine();
    }

    public void CalculateFine()
    {
        if (Grd.Rows.Count > 0)
        {
            for (int i = 0; i < Grd.Rows.Count; i++)
            {
                double totalAmount = 0;
                Label lblClass = (Label)Grd.Rows[i].FindControl("lblClass");
                
                sql = "select so.srno  as srno from StudentOfficialDetails SO ";
                sql = sql + " left join ClassMaster CM on SO.AdmissionForClassId=CM.Id where CM.ClassName='" + lblClass.Text + "'  and ";
                sql = sql + " so.SessionName='" + Session["SessionName"].ToString() + "' and cm.SessionName='" + Session["SessionName"].ToString() + "'";
                sql = sql + " and so.BranchCode=" + Session["BranchCode"].ToString() + " and SO.Withdrwal is null and (SO.Promotion is null or SO.Promotion<>'Cancelled')";
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                DataTable dt2 = new DataTable();
                da.Fill(dt2);
                if (dt2.Rows.Count > 0)
                {

                    for (int k = 0; k < dt2.Rows.Count; k++)
                    {
                        Label lblSrno = new Label();
                        lblSrno.Text = dt2.Rows[k][0].ToString();
                        sql = "Select *from FeeDeposite fd where fd.SessionName='" + Session["SessionName"].ToString() + "' and SrNo='" + lblSrno.Text + "' and (FeeMonth='" + drpInsttalment.SelectedItem.ToString() + "' or FeeMonth='Yearly')";
                        if (oo.Duplicate(sql) == false)
                        {
                            int a = 0, b = 0;
                            double fineValue = 0;

                            sql = "select DAY(getdate()) as DayValue";
                            int dv = 0;
                            dv = Convert.ToInt32(oo.ReturnTag(sql, "DayValue"));

                            sql = "Select DATENAME(MONTH,GETDATE()) as MonthName";
                            string selectedmonth1 = oo.ReturnTag(sql, "MonthName");

                            sql = "Select MonthId from MonthMaster where DueMonth='" + selectedmonth1 + "' and SessionName='" + Session["SessionName"].ToString() + "'";
                            int id3 = Convert.ToInt32(oo.ReturnTag(sql, "MonthId"));

                            sql = "Select Top 1 mm.MonthId from FeeDeposite fd inner join MonthMaster mm on mm.MonthName=fd.FeeMonth";
                            sql = sql + " and fd.SessionName='" + Session["SessionName"].ToString() + "' and mm.SessionName='" + Session["SessionName"].ToString() + "' and SrNo='" + lblSrno.Text + "' order by MonthId desc";
                            int id1 = 0;

                            if (oo.ReturnTag(sql, "MonthId") != "")
                            {
                                id1 = Convert.ToInt32(oo.ReturnTag(sql, "MonthId"));
                            }
                            else
                            {
                                sql = "Select Top 1 MonthId from MonthMaster where SessionName='" + Session["SessionName"].ToString() + "'";
                                if (!string.IsNullOrEmpty(oo.ReturnTag(sql, "MonthId")))
                                {
                                    id1 = Convert.ToInt32(oo.ReturnTag(sql, "MonthId"));
                                }
                            }
                            string selectedmonth;

                            selectedmonth = drpInsttalment.SelectedItem.ToString();


                            sql = "Select MonthId from MonthMaster where DueMonth='" + selectedmonth + "' and SessionName='" + Session["SessionName"].ToString() + "'";
                            int id2 = Convert.ToInt32(oo.ReturnTag(sql, "MonthId"));

                            sql = "Select *from FeeDeposite fd inner join MonthMaster mm on mm.MonthName=fd.FeeMonth";
                            sql = sql + " and fd.SessionName='" + Session["SessionName"].ToString() + "' and mm.SessionName='" + Session["SessionName"].ToString() + "' and SrNo='" + lblSrno.Text + "' order by MonthId desc";
                            if (oo.Duplicate(sql))
                            {
                                sql = "Select MonthName from MonthMaster where MonthId>'" + id1 + "' and MonthId<='" + id2 + "' and SessionName='" + Session["SessionName"].ToString() + "'";
                            }
                            else
                            {
                                sql = "Select MonthName from MonthMaster where MonthId>='" + id1 + "' and MonthId<='" + id2 + "' and SessionName='" + Session["SessionName"].ToString() + "'";
                            }
                            DataTable dt = new DataTable();
                            da = new SqlDataAdapter(sql, con);
                            dt = new DataTable();
                            da.Fill(dt);

                            for (int j = 0; j < dt.Rows.Count; j++)
                            {

                                sql = "Select MonthId from MonthMaster where DueMonth='" + dt.Rows[j][0].ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "'";
                                int id4 = Convert.ToInt32(oo.ReturnTag(sql, "MonthId"));
                                sql = "Select MonthName from MonthMaster where MonthId>='" + id4 + "' and MonthId<='" + id3 + "' and SessionName='" + Session["SessionName"].ToString() + "'";
                                da = new SqlDataAdapter(sql, con);
                                DataTable dt1 = new DataTable();
                                da.Fill(dt1);
                                for (int j1 = 0; j1 < dt1.Rows.Count; j1++)
                                {
                                    sql = "Select MonthId from MonthMaster where DueMonth='" + dt1.Rows[j1][0].ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "'";
                                    int id5 = Convert.ToInt32(oo.ReturnTag(sql, "MonthId"));

                                    if (id5 < id3)
                                    {
                                        sql = "Select Distinct MonthAmount from RangeBasisFineMaster where SessionName='" + Session["SessionName"].ToString() + "'";
                                        if (!string.IsNullOrEmpty(oo.ReturnTag(sql, "MonthAmount")))
                                        {
                                            double fineamount = Convert.ToDouble(oo.ReturnTag(sql, "MonthAmount"));
                                            totalAmount = totalAmount + fineamount;
                                        }
                                    }

                                    if (id5 == id3)
                                    {
                                        sql = "select * from RangeBasisFineMaster ";
                                        sql = sql + " where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
                                        SqlCommand cmd = new SqlCommand();
                                        try
                                        {
                                            cmd.CommandText = sql;
                                            SqlDataReader dr;
                                            cmd.Connection = con;
                                            con.Open();
                                            dr = cmd.ExecuteReader();

                                            while (dr.Read())
                                            {
                                                a = Convert.ToInt32(dr["FromDate"].ToString());
                                                b = Convert.ToInt32(dr["ToDate"].ToString());
                                                if (a <= dv && dv <= b)
                                                {
                                                    if (!string.IsNullOrEmpty(dr["AmountPerday"].ToString()))
                                                    {
                                                        fineValue = Convert.ToDouble(dr["AmountPerday"].ToString());
                                                    }
                                                    break;
                                                }

                                            }
                                            con.Close();
                                        }
                                        catch (SqlException)
                                        {
                                            con.Close();
                                        }
                                        totalAmount = totalAmount + fineValue;
                                    }

                                }
                            }
                        }
                    }
                }
                Label lblLateFeeAmount = (Label)Grd.Rows[i].FindControl("lblLateFeeAmount");

                if (lblLateFeeAmount.Text != "")
                {
                    lblLateFeeAmount.Text = (Convert.ToDouble(lblLateFeeAmount.Text) + totalAmount).ToString();
                }
                else
                {
                    if (totalAmount != 0)
                    {
                        lblLateFeeAmount.Text = totalAmount.ToString(".00");
                    }
                    else
                    {
                        lblLateFeeAmount.Text = "0.00";
                    }
                }
            }
        }
    }

    public void withtransport()
    {
        if (Grd.Rows.Count > 0)
        {
            SqlConnection con = new SqlConnection();
            con = oo.dbGet_connection();
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
           
            for (int i = 0; i < Grd.Rows.Count; i++)
            {
                Label lblClass = (Label)Grd.Rows[i].FindControl("lblClass");
                double wayamount = 0;
                string sql2 = "";
                for (int j = 1; j < drpInsttalment.SelectedIndex; j++)
                {
                    if (Session["SessionName"].ToString() != "2015-2016" && Session["SessionName"].ToString() != "2016-2017")
                    {
                        sql2 = " select Distinct fm.NoOfmonths as NoOfmonths from FeeAllotedForClassWise fa ";
                        sql2 = sql2 + " left join feemaster  fm on fa.Medium=fm.medium  and fa.FeeName=fm.FeeName  ";
                        sql2 = sql2 + " where fa.Class='" + lblClass.Text + "' and  fm.SessionName='" + Session["SessionName"].ToString() + "'";
                        sql2 = sql2 + "and fa.Month='" + drpInsttalment.Items[j].Text + "' and fa.SessionName='" + Session["SessionName"].ToString() + "'";
                        sql2 = sql2 + "and fa.CardType='" + drpFeegroup.SelectedItem.ToString() + "'";
                    }
                    else
                    {
                        sql2 = "Select ForMonth NoOfmonths from MonthMaster where MonthId='" + drpInsttalment.Items[j].Value + "'";
                    }
                    double noofmonth =Convert.ToDouble(oo.ReturnTag(sql2, "NoOfmonths"));

                    sql = "Select Sum(Amount) WayAmount from StudentVehicleAllotment sva";
                    sql = sql + " inner join AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "',1) asr on asr.SrNo=sva.SrNo";
                    sql = sql + " inner join ClassMaster cm on cm.Id=asr.ClassId and cm.SessionName=sva.SessionName";
                    sql = sql + " where sva.SessionName='" + Session["SessionName"].ToString() + "' and Insttalment='July' and cm.ClassName='" + lblClass.Text + "' and sva.MonthStatus=1";

                    double amount = Convert.ToDouble(oo.ReturnTag(sql, "WayAmount"));
                    wayamount = wayamount + (amount * noofmonth);
                    //sql = sql + " group by sva.SrNo";

                    //if (Session["SessionName"].ToString() != "2014-2015")
                    //{
                    //    sql = "select sod.srNo,sod.WayAmount from StudentOfficialDetails sod ";
                    //    sql = sql + " inner join ClassMaster cm on sod.AdmissionForClassId=cm.Id where sod.SessionName='" + Session["SessionName"].ToString() + "' ";
                    //    sql = sql + " and cm.ClassName='" + lblClass.Text + "' and sod.TransportRequired='Yes' and cm.SessionName='" + Session["SessionName"].ToString() + "' ";
                    //    sql = sql + " and sod.Card='" + drpFeegroup.SelectedValue.ToString() + "' ";
                    //    sql = sql + " and sod.Withdrwal is null and (SOd.Promotion is null or SOd.Promotion<>'Cancelled')";
                    //}
                    //else
                    //{
                    //    sql = "Select sva.SrNo,Sum(Amount) WayAmount from StudentVehicleAllotment sva";
                    //    sql = sql + " inner join AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "',1) asr on asr.SrNo=sva.SrNo";
                    //    sql = sql + " inner join ClassMaster cm on cm.Id=asr.ClassId and cm.SessionName=sva.SessionName";
                    //    sql = sql + " where sva.SessionName='" + Session["SessionName"].ToString() + "' and Insttalment='July' and cm.ClassName='" + lblClass.Text + "' and sva.MonthStatus=1";
                    //    sql = sql + " group by sva.SrNo";
                    //}

                    //DataTable dt = new DataTable();
                    //da = new SqlDataAdapter(sql, con);
                    //da.Fill(dt);
                    
                    //if (dt.Rows.Count > 0)
                    //{
                    //    //string[] waymonth;
                    //    for (int l = 0; l < dt.Rows.Count; l++)
                    //    {
                    //        string srno = dt.Rows[l][0].ToString();
                    //        sql = "Select Sum(Amount) as Amount from StudentVehicleAllotment where SessionName='" + Session["SessionName"].ToString() + "' and MonthStatus='1' and srno='" + srno + "' and Insttalment='" + drpInsttalment.Items[j].Text + "'";
                    //        if (oo.ReturnTag(sql, "Amount") != "")
                    //        {
                    //            double amount = Convert.ToDouble(oo.ReturnTag(sql, "Amount"));
                    //            wayamount = wayamount + (amount * noofmonth);
                    //        }
                    //    }
                    //}
                    //con.Close();
                }

                Label lblTransportAmount = (Label)Grd.Rows[i].FindControl("lblTransportAmount");
                if (lblTransportAmount.Text != "")
                {
                    lblTransportAmount.Text = (Convert.ToDouble(lblTransportAmount.Text) + wayamount).ToString(".00");
                }
                else
                {
                    if (wayamount != 0)
                    {
                        lblTransportAmount.Text = wayamount.ToString(".00");
                    }
                    else
                    {
                        lblTransportAmount.Text = "0.00";
                    }   
                }
                
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
                double wayamount = 0;
                string sql2 = "";
                for (int k1 = 1; k1 < drpInsttalment.SelectedIndex; k1++)
                {
                    //sql2 = " select Distinct fm.NoOfmonths as NoOfmonths from FeeAllotedForClassWise fa ";
                    //sql2 = sql2 + " left join feemaster  fm on fa.Medium=fm.medium  and fa.FeeName=fm.FeeName  ";
                    //sql2 = sql2 + " where fa.Class='" + lblClass.Text + "' and  fm.SessionName='" + Session["SessionName"].ToString() + "'";
                    //sql2 = sql2 + "and fa.Month='" + drpInsttalment.Items[k1].Text + "' and fa.SessionName='" + Session["SessionName"].ToString() + "'";
                    //sql2 = sql2 + "and fa.CardType='" + drpFeegroup.SelectedItem.ToString() + "'";

                    //double noofmonth = Convert.ToDouble(oo.ReturnTag(sql2, "NoOfmonths"));

                    if (Session["SessionName"].ToString() != "2015-2016" && Session["SessionName"].ToString() != "2016-2017")
                    {
                        sql2 = " select Distinct fm.NoOfmonths as NoOfmonths from FeeAllotedForClassWise fa ";
                        sql2 = sql2 + " left join feemaster  fm on fa.Medium=fm.medium  and fa.FeeName=fm.FeeName  ";
                        sql2 = sql2 + " where fa.Class='" + lblClass.Text + "' and  fm.SessionName='" + Session["SessionName"].ToString() + "'";
                        sql2 = sql2 + "and fa.Month='" + drpInsttalment.Items[k1].Text + "' and fa.SessionName='" + Session["SessionName"].ToString() + "'";
                        sql2 = sql2 + "and fa.CardType='" + drpFeegroup.SelectedItem.ToString() + "'";
                    }
                    else
                    {
                        sql2 = "Select ForMonth NoOfmonths from MonthMaster where MonthId='" + drpInsttalment.Items[k1].Value + "'";
                    }
                    double noofmonth = Convert.ToDouble(oo.ReturnTag(sql2, "NoOfmonths"));

                    sql = " Select fd.SrNo,sod.WayAmount from StudentOfficialDetails sod inner join FeeDeposite fd on sod.SrNo=fd.SrNo";
                    sql = sql + " and sod.StEnRCode=sod.StEnRCode where fd.Status='Paid' and sod.SessionName='" + Session["SessionName"].ToString() + "' and fd.SessionName='" + Session["SessionName"].ToString() + "'";
                    sql = sql + " and Class='" + lblClass.Text + "' and FeeMonth='Yearly' and Card='" + drpFeegroup.SelectedItem.ToString() + "' and TransportRequired='Yes' and fd.BusConvience<>'0.00' and Cancel is null and (SOd.Promotion is null or SOd.Promotion<>'Cancelled')";

                    SqlConnection con = new SqlConnection();
                    con = oo.dbGet_connection();
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(sql, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        //string[] waymonth;
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            string srno = dt.Rows[j][0].ToString();
                            sql = "Select Sum(Amount) as Amount from StudentVehicleAllotment where SessionName='" + Session["SessionName"].ToString() + "' and MonthStatus='1' and srno='" + srno + "' and Insttalment='" + drpInsttalment.SelectedValue.ToString() + "'";
                            if (oo.ReturnTag(sql, "Amount") != "")
                            {
                                double amount = Convert.ToDouble(oo.ReturnTag(sql, "Amount"));
                                wayamount = wayamount + (amount * noofmonth);
                            }
                        }
                    }
                    con.Close();
                }
               
                Label lblDepositeAmount = (Label)Grd.Rows[i].FindControl("lblDepositeAmount");
                double GrossAmount1 = Convert.ToDouble(lblDepositeAmount.Text) + wayamount;
                lblDepositeAmount.Text = GrossAmount1.ToString(".00");
            }
        }
    }

    public void transportfeeother()
    {
        if (Grd.Rows.Count > 0)
        {
            for (int i = 0; i < Grd.Rows.Count; i++)
            {
                Label lblClass = (Label)Grd.Rows[i].FindControl("lblClass");
                double wayamount = 0;
                string sql2 = "";
                for (int k1 = 1; k1 < drpInsttalment.SelectedIndex; k1++)
                {
                    //sql2 = " select Distinct fm.NoOfmonths as NoOfmonths from FeeAllotedForClassWise fa ";
                    //sql2 = sql2 + " left join feemaster  fm on fa.Medium=fm.medium  and fa.FeeName=fm.FeeName  ";
                    //sql2 = sql2 + " where fa.Class='" + lblClass.Text + "' and  fm.SessionName='" + Session["SessionName"].ToString() + "'";
                    //sql2 = sql2 + "and fa.Month='" + drpInsttalment.Items[k1].Text + "' and fa.SessionName='" + Session["SessionName"].ToString() + "'";
                    //sql2 = sql2 + "and fa.CardType='" + drpFeegroup.SelectedItem.ToString() + "'";

                    //double noofmonth = Convert.ToDouble(oo.ReturnTag(sql2, "NoOfmonths"));

                    if (Session["SessionName"].ToString() != "2015-2016" && Session["SessionName"].ToString() != "2016-2017")
                    {
                        sql2 = " select Distinct fm.NoOfmonths as NoOfmonths from FeeAllotedForClassWise fa ";
                        sql2 = sql2 + " left join feemaster  fm on fa.Medium=fm.medium  and fa.FeeName=fm.FeeName  ";
                        sql2 = sql2 + " where fa.Class='" + lblClass.Text + "' and  fm.SessionName='" + Session["SessionName"].ToString() + "'";
                        sql2 = sql2 + "and fa.Month='" + drpInsttalment.Items[k1].Text + "' and fa.SessionName='" + Session["SessionName"].ToString() + "'";
                        sql2 = sql2 + "and fa.CardType='" + drpFeegroup.SelectedItem.ToString() + "'";
                    }
                    else
                    {
                        sql2 = "Select ForMonth NoOfmonths from MonthMaster where MonthId='" + drpInsttalment.Items[k1].Value + "'";
                    }
                    double noofmonth = Convert.ToDouble(oo.ReturnTag(sql2, "NoOfmonths"));
                    sql = " Select sod.SrNo,sod.WayAmount from StudentOfficialDetails sod inner join FeeDeposite fd on sod.SrNo=fd.SrNo";
                    sql = sql + " and sod.srno=sod.srno where fd.Status='Paid' and sod.SessionName='" + Session["SessionName"].ToString() + "' and fd.SessionName='" + Session["SessionName"].ToString() + "'";
                    sql = sql + " and Class='" + lblClass.Text + "' and FeeMonth='" + drpInsttalment.Items[k1].Text + "' and Card='" + drpFeegroup.SelectedItem.ToString() + "' and fd.BusConvience<>'0.00' and TransportRequired='No' and Cancel is null and (SOd.Promotion is null or SOd.Promotion<>'Cancelled')";

                    SqlConnection con = new SqlConnection();
                    con = oo.dbGet_connection();
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(sql, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        //string[] waymonth;
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            string srno = dt.Rows[j][0].ToString();
                            sql = "Select Sum(Amount) as Amount from StudentVehicleAllotment where SessionName='" + Session["SessionName"].ToString() + "' and MonthStatus='1' and srno='" + srno + "' and Insttalment='" + drpInsttalment.SelectedValue.ToString() + "'";
                            if (oo.ReturnTag(sql, "Amount") != "")
                            {
                                double amount = Convert.ToDouble(oo.ReturnTag(sql, "Amount"));
                                wayamount = wayamount + (amount * noofmonth);
                            }
                        }
                    }
                    con.Close();
                }
                Label lblTransportAmount = (Label)Grd.Rows[i].FindControl("lblTransportAmount");
                if (lblTransportAmount.Text != "")
                {
                    double GrossAmount = Convert.ToDouble(lblTransportAmount.Text) + wayamount;
                    if (GrossAmount != 0)
                    {
                        lblTransportAmount.Text = GrossAmount.ToString(".00");
                    }
                }
                else
                {
                    lblTransportAmount.Text = "0.00";
                }

            }
        }
    }

    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        if (Grd.Rows.Count > 0)
        {
            oo.ExportToWord(Response, "DueReportInstallmentWise", divExport);
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
            oo.ExportToExcel("DueReportInstallmentWise.xls", Grd);
        }
    }

    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");

    }
    protected void drpClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadInsttalment();
    }
}