using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;

public partial class admin_TotalDuesRegister : Page
{
    SqlConnection con = new SqlConnection();
    SqlConnection con1 = new SqlConnection();
    Campus oo = new Campus();
    string sql = string.Empty;

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        con = oo.dbGet_connection();
        con1.ConnectionString = ConfigurationManager.ConnectionStrings["Menu"].ConnectionString;
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        if (!IsPostBack)
        {
            //sql = "select SessionName from SessionMaster where SessionName='" + Session["SessionName"].ToString() + "'";
            //oo.FillDropDown(sql, DropDownList1, "SessionName");


        }

    }

    

    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        sql = "select ROW_NUMBER() OVER (ORDER BY Id ASC) AS SrNo,Id, className from ClassMaster  where className!= '<--Select-->'  and SessionName='" + Session["SessionName"].ToString() + "'order by CIDOrder ";
        grdDisplayRepo.DataSource = oo.GridFill(sql);
        grdDisplayRepo.DataBind();
        int i;

        for (i = 0; i <= grdDisplayRepo.Rows.Count - 1; i++)
        {
            Label lblClass = (Label)grdDisplayRepo.Rows[i].FindControl("Label2");

            Label lblTotInstallmentApr = (Label)grdDisplayRepo.Rows[i].FindControl("lblTotInstallmentApr");
            Label lblTotInstallmentMay = (Label)grdDisplayRepo.Rows[i].FindControl("lblTotInstallmentMay");
            Label lblTotInstallmentJul = (Label)grdDisplayRepo.Rows[i].FindControl("lblTotInstallmentJul");
            Label lblTotInstallmentAug = (Label)grdDisplayRepo.Rows[i].FindControl("lblTotInstallmentAug");
            Label lblTotInstallmentSep = (Label)grdDisplayRepo.Rows[i].FindControl("lblTotInstallmentSep");
            Label lblTotInstallmentOct = (Label)grdDisplayRepo.Rows[i].FindControl("lblTotInstallmentOct");
            Label lblTotInstallmentNov = (Label)grdDisplayRepo.Rows[i].FindControl("lblTotInstallmentNov");
            Label lblTotInstallmentDec = (Label)grdDisplayRepo.Rows[i].FindControl("lblTotInstallmentDec");
            Label lblTotInstallmentJan = (Label)grdDisplayRepo.Rows[i].FindControl("lblTotInstallmentJan");
            Label lblTotInstallmentFeb = (Label)grdDisplayRepo.Rows[i].FindControl("lblTotInstallmentFeb");



            lblTotInstallmentApr.Text = ClassWiseAmountForParticularMonth(lblClass.Text, "Apr").ToString();
            lblTotInstallmentMay.Text = ClassWiseAmountForParticularMonth(lblClass.Text, "May").ToString();
            lblTotInstallmentJul.Text = ClassWiseAmountForParticularMonth(lblClass.Text, "Jul").ToString();
            lblTotInstallmentAug.Text = ClassWiseAmountForParticularMonth(lblClass.Text, "Aug").ToString();
            lblTotInstallmentSep.Text = ClassWiseAmountForParticularMonth(lblClass.Text, "Sep").ToString();
            lblTotInstallmentOct.Text = ClassWiseAmountForParticularMonth(lblClass.Text, "Oct").ToString();
            lblTotInstallmentNov.Text = ClassWiseAmountForParticularMonth(lblClass.Text, "Nov").ToString();
            lblTotInstallmentDec.Text = ClassWiseAmountForParticularMonth(lblClass.Text, "Dec").ToString();
            lblTotInstallmentJan.Text = ClassWiseAmountForParticularMonth(lblClass.Text, "Jan").ToString();
            lblTotInstallmentFeb.Text = ClassWiseAmountForParticularMonth(lblClass.Text, "Feb").ToString();



        }



        //Total Deposit 

        for (i = 0; i <= grdDisplayRepo.Rows.Count - 1; i++)
        {
            Label lblClass = (Label)grdDisplayRepo.Rows[i].FindControl("Label2");

            Label lblApr = (Label)grdDisplayRepo.Rows[i].FindControl("lblApr");
            Label lblMay = (Label)grdDisplayRepo.Rows[i].FindControl("lblMay");
            Label lblJul = (Label)grdDisplayRepo.Rows[i].FindControl("lblJul");
            Label lblAug = (Label)grdDisplayRepo.Rows[i].FindControl("lblAug");
            Label lblSep = (Label)grdDisplayRepo.Rows[i].FindControl("lblSep");
            Label lblOct = (Label)grdDisplayRepo.Rows[i].FindControl("lblOct");
            Label lblNov = (Label)grdDisplayRepo.Rows[i].FindControl("lblNov");
            Label lblDec = (Label)grdDisplayRepo.Rows[i].FindControl("lblDec");
            Label lblJan = (Label)grdDisplayRepo.Rows[i].FindControl("lblJan");
            Label lblFeb = (Label)grdDisplayRepo.Rows[i].FindControl("lblFeb");



            lblApr.Text = TotalDepositClassWiseSelectedMonth(lblClass.Text, "Apr").ToString();
            lblMay.Text = TotalDepositClassWiseSelectedMonth(lblClass.Text, "May").ToString();
            lblJul.Text = TotalDepositClassWiseSelectedMonth(lblClass.Text, "Jul").ToString();
            lblAug.Text = TotalDepositClassWiseSelectedMonth(lblClass.Text, "Aug").ToString();
            lblSep.Text = TotalDepositClassWiseSelectedMonth(lblClass.Text, "Sep").ToString();
            lblOct.Text = TotalDepositClassWiseSelectedMonth(lblClass.Text, "Oct").ToString();
            lblNov.Text = TotalDepositClassWiseSelectedMonth(lblClass.Text, "Nov").ToString();
            lblDec.Text = TotalDepositClassWiseSelectedMonth(lblClass.Text, "Dec").ToString();
            lblJan.Text = TotalDepositClassWiseSelectedMonth(lblClass.Text, "Jan").ToString();
            lblFeb.Text = TotalDepositClassWiseSelectedMonth(lblClass.Text, "Feb").ToString();



        }
        try
        {
           
            FindDuesAndBalance();
            FooterGridCalculationForTotalDues();
            FindAllTotalDuesofFooterRows();
        }
        catch (Exception) { }
    }


  


    

    

    
   
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        oo.ExportToWord(Response, "TotalCollectionAccordingtoClassWise.doc", gdv);
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        oo.ExportToExcel("TotalCollectionAccordingtoClassWise.xls", grdDisplayRepo);
    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }

    public string Balance(Label lblTotalDues, Label lblDeposit)
    {

        double bal = 0;
        try
        {
            bal = Convert.ToDouble(lblTotalDues.Text) - Convert.ToDouble(lblDeposit.Text);
        }
        catch (Exception) { bal = 0; }
        return bal.ToString();

    }
    public double ClassWiseAmountForParticularMonth(string className, string monthName)
    {
        string med = string.Empty, typeOfAdd = string.Empty;
        string crd = string.Empty, clna = string.Empty;
        string srno = string.Empty;
        string Fmo = string.Empty;
#pragma warning disable 219
        int j = 0;
#pragma warning restore 219
        string sql3 = string.Empty;
        double damt = 0;
        int mId = 0;
        double conc = 0;
        double sum = 0, tutionFees = 0;
        string sql5 = string.Empty;
        string WidthMonth = string.Empty;
#pragma warning disable 219
        int PPP = 0;
#pragma warning restore 219
        int dy = 0, yy1 = 0, yy = 0, yr = 0;
        yy = Convert.ToInt32(Session["SessionName"].ToString().Substring(0, 4));//2012
        yy1 = Convert.ToInt32(Session["SessionName"].ToString().Substring(5, 4));//2013
        if (monthName == "Jan")
        {
            dy = 31;
            yr = yy1;
        }
        if (monthName == "Feb")
        {

            if (yy1 % 4 == 0)
            {
                dy = 29;
            }
            else
            {
                dy = 28;
            }
            yr = yy1;
        }
        if (monthName == "Mar")
        {
            dy = 31;
            yr = yy;
        }
        if (monthName == "Apr")
        {
            dy = 30;
            yr = yy;
        }

        if (monthName == "May")
        {
            dy = 31;
            yr = yy;
        }
        if (monthName == "Jun")
        {
            dy = 30;
            yr = yy;
        }
        if (monthName == "Jul")
        {
            dy = 31;
            yr = yy;
        }
        if (monthName == "Aug")
        {
            dy = 31;
            yr = yy;
        }
        if (monthName == "Sep")
        {
            dy = 30;
            yr = yy;
        }
        if (monthName == "Oct")
        {
            dy = 31;
            yr = yy;
        }
        if (monthName == "Nov")
        {
            dy = 30;
            yr = yy;
        }
        if (monthName == "Dec")
        {
            dy = 31;
            yr = yy;
        }

        sql = " select distinct so.srno as srno,sg.FirstName+' '+sg.MiddleName+' '+sg.LastName as Sname,cm.ClassName as ClassName , sm.SectionName  ";
        sql = sql + ", so.TypeOFAdmision as TypeOFAdmision, so.Medium as Medium,so.card as card,convert(nvarchar,sw.WithdrawalDate,106) as WithdrawalDate,left(DATENAME(m,sw.WithdrawalDate),3) as WMonth from StudentGenaralDetail sg ";
        sql = sql + " left join StudentOfficialDetails so on sg.SrNo=so.SrNo  ";
        sql = sql + " left join ClassMaster cm on cm.Id=so.AdmissionForClassId   ";
        sql = sql + "  left join StudentWithdrawal sw on sw.SrNo=sg.srno ";
        sql = sql + " left join SectionMaster sm on sm.Id=so.SectionId   ";
        sql = sql + " where so.SessionName='" + Session["SessionName"].ToString() + "' and so.BranchCode=" + Session["BranchCode"].ToString() + "  and so.Withdrwal is null ";
        sql = sql + "  and cm.ClassName='" + className + "'";
        sql = sql + "  and cm.SessionName='" + Session["SessionName"].ToString() + "'";
        sql = sql + "  and sm.SessionName='" + Session["SessionName"].ToString() + "'";
        sql = sql + " and sg.SessionName='" + Session["SessionName"].ToString() + "'";
        sql = sql + "  and  so.DateOfAdmiission<='" + dy + "/" + monthName + "/" + yr + "'";
        SqlCommand cmd1 = new SqlCommand();
        SqlCommand cmd2 = new SqlCommand();

        cmd1.CommandText = sql;
        try
        {
            SqlDataReader dr1;
            cmd1.Connection = con1;
            con1.Open();
            dr1 = cmd1.ExecuteReader();

            while (dr1.Read())
            {
                srno = dr1["srno"].ToString();
                clna = dr1["ClassName"].ToString(); //oo.ReturnTag(sql, "ClassName");
                typeOfAdd = dr1["TypeOFAdmision"].ToString(); //oo.ReturnTag(sql, "TypeOFAdmision");
                med = dr1["Medium"].ToString();//oo.ReturnTag(sql, "Medium");
                crd = dr1["card"].ToString();//oo.ReturnTag(sql, "card");

                sql3 = "Select MonthId,GMonth,YMonth from gymon where month='" + monthName + "'";
                mId = Convert.ToInt32(oo.ReturnTag(sql3, "MonthId"));
                WidthMonth = dr1["WMonth"].ToString();

                if (WidthMonth == monthName)
                {
                    PPP = 1;
                }
                else if (YearlyFeesDeposit(srno) == true)
                {
                    PPP = 2;
                }
                else
                {



                    if (crd.ToUpper() == "YELLOW")
                    {
                        Fmo = oo.ReturnTag(sql3, "YMonth");
                    }
                    if (crd.ToUpper() == "GREEN")
                    {
                        Fmo = oo.ReturnTag(sql3, "GMonth");
                    }


                    sql = " select distinct Id,fa.Month, fa.FeeParticular,fa.Class,fa.FeeType,fa.FeePayment,FM.Medium,Fa.CardType ,fm.NoOfmonths as NoOfmonths from FeeAllotedForClassWise fa ";
                    sql = sql + " left join feemaster  fm on fa.Medium=fm.medium  and fa.FeeName=fm.FeeName  ";
                    sql = sql + " where fa.Class='" + clna + "'   and fa.Month='" + Fmo + "' and fa.SessionName='" + Session["SessionName"].ToString() + "'  and fa.BranchCode=" + Session["BranchCode"].ToString() + "";
                    sql = sql + "and fa.CardType='" + crd + "' and   fa.AdmissionType='" + typeOfAdd.Trim() + "'  and fa.Medium='" + med + "'";
                    sql = sql + "  and fm.SessionName='" + Session["SessionName"].ToString() + "'";
                    sql = sql + "  and fa.SessionName='" + Session["SessionName"].ToString() + "'";


                    try
                    {
                        cmd2.CommandText = sql;
                        SqlDataReader dr;
                        cmd2.Connection = con;
                        con.Open();
                        dr = cmd2.ExecuteReader();
                        while (dr.Read())
                        {
                            try
                            {
                                sum = sum + Convert.ToDouble(dr["FeePayment"].ToString());
                            }
                            catch (Exception) { }

                            if (dr["FeeType"].ToString().Substring(0, 3).ToString().ToUpper() == "TUT" || dr["FeeType"].ToString().Substring(0, 3).ToString().ToUpper() == "TUI" || dr["FeeType"].ToString().Substring(0, 3).ToString().ToUpper() == "MON")
                            {
                                try
                                {
                                    tutionFees = Convert.ToDouble(dr["FeePayment"].ToString());
                                }
                                catch (Exception) { }
                            }
                        }
                        con.Close();
                    }
                    catch (Exception) { }


                    int noMonths = 0;
                    try
                    {
                        noMonths = Convert.ToInt32(oo.ReturnTag(sql, "NoOfMonths"));
                    }
                    catch (Exception) { noMonths = 0; }

                    //Find the Discount
                    damt = FindDiscount(srno, Fmo, noMonths, tutionFees);
                    //Find the Discount


                    //Find the ConcessionAmt
                    conc = FindConcession(srno, Fmo);
                    //Find the ConcessionAmt

                    sum = sum - conc - damt;



                }//Else Withdraw Date

            }//While Close
            con1.Close();
        }


        catch (Exception) { con1.Close(); }
        return sum;
    }




    public double FindDiscount(string srno, string FeeMonth, int noOfMonths, double tutionFees)
    {
#pragma warning disable 168
        int j;
#pragma warning restore 168
        double totAmt = 0;
        double discAmt = 0;
        string ss = "select * from FeeDeposite where srno='" + srno + "'  and FeeMonth='" + FeeMonth + "'";
        sql = "select DiscountValue,DiscountType from DiscountMaster where srno='" + srno + "'   and RecordDate <='" + oo.ReturnTag(ss, "FeeDepositeDate") + "'";
        sql = sql + "  and SessionName='" + Session["SessionName"].ToString() + "'";
        try
        {
            if (oo.ReturnTag(sql, "DiscountType") == "Amount")
            {
                //for (j = 1; j <= noOfMonths; j++)
                //{
                totAmt = totAmt + Convert.ToDouble(oo.ReturnTag(sql, "DiscountValue")); 
                //}
                discAmt = totAmt;
            }
            if (oo.ReturnTag(sql, "DiscountType") == "Percentage")
            {


                discAmt = discAmt + (tutionFees) * Convert.ToDouble(oo.ReturnTag(sql, "DiscountValue")) / 100;

            }
        }
        catch (Exception) { discAmt = 0; }
        return discAmt;
    }


    public double FindConcession(string srno, string FeeMonth)
    {
        double concession = 0;
        string ss = "select * from FeeDeposite where srno='" + srno + "'  and FeeMonth='" + FeeMonth + "'";
        ss = ss + " and   SessionName='" + Session["SessionName"].ToString() + "'";
        try
        {
            concession = Convert.ToDouble(oo.ReturnTag(ss, "cocession"));
        }
        catch (Exception) { concession = 0; }
        return concession;
    }
    public double TotalDepositClassWiseSelectedMonth(string className, string monthName)
    {
        double totDeposit;
        string FmoY = string.Empty, FmoG = string.Empty;
        string sql3 = "";
#pragma warning disable 219
        int i = 0;
#pragma warning restore 219
        int dy = 0;
        int yy = 0, yy1 = 0, yr = 0;
        yy = Convert.ToInt32(Session["SessionName"].ToString().Substring(0, 4));//2012
        yy1 = Convert.ToInt32(Session["SessionName"].ToString().Substring(5, 4));//2012


        if (monthName == "Jan")
        {
            dy = 31;
            yr = yy1;
        }
        if (monthName == "Feb")
        {

            if (yy1 % 4 == 0)
            {
                dy = 29;
            }
            else
            {
                dy = 28;
            }
            yr = yy1;
        }
        if (monthName == "Mar")
        {
            dy = 31;
            yr = yy;
        }
        if (monthName == "Apr")
        {
            dy = 30;
            yr = yy;
        }

        if (monthName == "May")
        {
            dy = 31;
            yr = yy;
        }
        if (monthName == "Jun")
        {
            dy = 30;
            yr = yy;
        }
        if (monthName == "Jul")
        {
            dy = 31;
            yr = yy;
        }
        if (monthName == "Aug")
        {
            dy = 31;
            yr = yy;
        }
        if (monthName == "Sep")
        {
            dy = 30;
            yr = yy;
        }
        if (monthName == "Oct")
        {
            dy = 31;
            yr = yy;
        }
        if (monthName == "Nov")
        {
            dy = 30;
            yr = yy;
        }
        if (monthName == "Dec")
        {
            dy = 31;
            yr = yy;
        }




        totDeposit = 0;
        string month;
        sql3 = "select YMonth,GMonth,month from GYMon where  Month='" + monthName + "'";

        month = oo.ReturnTag(sql3, "month");

        if (month == "Apr")
        {
            month = "Mar";
            sql = " select SUM(RecievedAmount) as DepositAmt from FeeDeposite fd";
            sql = sql + "  left join StudentOfficialDetails so on fd.SrNo=so.SrNo ";
            sql = sql + " where  fd.Class='" + className + "'  and  fd.FeeDepositeDate between '1/" + month + "/" + yr + "'  and '" + dy + "/" + monthName + "/" + yr + "'   and cancel is null";
            sql = sql + "  and fd.SessionName='" + Session["SessionName"].ToString() + "'";
            sql = sql + "  and so.SessionName='" + Session["SessionName"].ToString() + "'";
        }
        else
        {
            sql = " select SUM(RecievedAmount) as DepositAmt from FeeDeposite fd";
            sql = sql + "  left join StudentOfficialDetails so on fd.SrNo=so.SrNo ";
            sql = sql + " where  fd.Class='" + className + "'  and  fd.FeeDepositeDate between '1/" + month + "/" + yr + "'  and '" + dy + "/" + month + "/" + yr + "'   and cancel is null";
            sql = sql + "  and fd.SessionName='" + Session["SessionName"].ToString() + "'";
            sql = sql + "  and so.SessionName='" + Session["SessionName"].ToString() + "'";
        }


       
       
        try
        {
            totDeposit = totDeposit + Convert.ToDouble(oo.ReturnTag(sql, "DepositAmt"));
        }
        catch (Exception) { totDeposit = 0; }
      
        return totDeposit;
    }

    public void FindDuesAndBalance()
    {

        int i;
        for (i = 0; i <= grdDisplayRepo.Rows.Count - 1; i++)
        {
            Label lblClass = (Label)grdDisplayRepo.Rows[i].FindControl("Label2");

            Label lblTotInstallmentApr = (Label)grdDisplayRepo.Rows[i].FindControl("lblTotInstallmentApr");
            Label lblTotInstallmentMay = (Label)grdDisplayRepo.Rows[i].FindControl("lblTotInstallmentMay");
            Label lblTotInstallmentJul = (Label)grdDisplayRepo.Rows[i].FindControl("lblTotInstallmentJul");
            Label lblTotInstallmentAug = (Label)grdDisplayRepo.Rows[i].FindControl("lblTotInstallmentAug");
            Label lblTotInstallmentSep = (Label)grdDisplayRepo.Rows[i].FindControl("lblTotInstallmentSep");
            Label lblTotInstallmentOct = (Label)grdDisplayRepo.Rows[i].FindControl("lblTotInstallmentOct");
            Label lblTotInstallmentNov = (Label)grdDisplayRepo.Rows[i].FindControl("lblTotInstallmentNov");
            Label lblTotInstallmentDec = (Label)grdDisplayRepo.Rows[i].FindControl("lblTotInstallmentDec");
            Label lblTotInstallmentJan = (Label)grdDisplayRepo.Rows[i].FindControl("lblTotInstallmentJan");
            Label lblTotInstallmentFeb = (Label)grdDisplayRepo.Rows[i].FindControl("lblTotInstallmentFeb");

            Label lblApr = (Label)grdDisplayRepo.Rows[i].FindControl("lblApr");
            Label lblMay = (Label)grdDisplayRepo.Rows[i].FindControl("lblMay");
            Label lblJul = (Label)grdDisplayRepo.Rows[i].FindControl("lblJul");
            Label lblAug = (Label)grdDisplayRepo.Rows[i].FindControl("lblAug");
            Label lblSep = (Label)grdDisplayRepo.Rows[i].FindControl("lblSep");
            Label lblOct = (Label)grdDisplayRepo.Rows[i].FindControl("lblOct");
            Label lblNov = (Label)grdDisplayRepo.Rows[i].FindControl("lblNov");
            Label lblDec = (Label)grdDisplayRepo.Rows[i].FindControl("lblDec");
            Label lblJan = (Label)grdDisplayRepo.Rows[i].FindControl("lblJan");
            Label lblFeb = (Label)grdDisplayRepo.Rows[i].FindControl("lblFeb");


            Label lblTotDuesApr = (Label)grdDisplayRepo.Rows[i].FindControl("lblTotDuesApr");
            Label lblTotDuesMay = (Label)grdDisplayRepo.Rows[i].FindControl("lblTotDuesMay");
            Label lblTotDuesJul = (Label)grdDisplayRepo.Rows[i].FindControl("lblTotDuesJul");
            Label lblTotDuesAug = (Label)grdDisplayRepo.Rows[i].FindControl("lblTotDuesAug");
            Label lblTotDuesSep = (Label)grdDisplayRepo.Rows[i].FindControl("lblTotDuesSep");
            Label lblTotDuesOct = (Label)grdDisplayRepo.Rows[i].FindControl("lblTotDuesOct");
            Label lblTotDuesNov = (Label)grdDisplayRepo.Rows[i].FindControl("lblTotDuesNov");
            Label lblTotDuesDec = (Label)grdDisplayRepo.Rows[i].FindControl("lblTotDuesDec");
            Label lblTotDuesJan = (Label)grdDisplayRepo.Rows[i].FindControl("lblTotDuesJan");
            Label lblTotDuesFeb = (Label)grdDisplayRepo.Rows[i].FindControl("lblTotDuesFeb");



            Label lblRBalApr = (Label)grdDisplayRepo.Rows[i].FindControl("lblRBalApr");
            Label lblRBalMay = (Label)grdDisplayRepo.Rows[i].FindControl("lblRBalMay");
            Label lblRBalJul = (Label)grdDisplayRepo.Rows[i].FindControl("lblRBalJul");
            Label lblRBalAug = (Label)grdDisplayRepo.Rows[i].FindControl("lblRBalAug");
            Label lblRBalSep = (Label)grdDisplayRepo.Rows[i].FindControl("lblRBalSep");
            Label lblRBalOct = (Label)grdDisplayRepo.Rows[i].FindControl("lblRBalOct");
            Label lblRBalNov = (Label)grdDisplayRepo.Rows[i].FindControl("lblRBalNov");
            Label lblRBalDec = (Label)grdDisplayRepo.Rows[i].FindControl("lblRBalDec");
            Label lblRBalJan = (Label)grdDisplayRepo.Rows[i].FindControl("lblRBalJan");
            Label lblRBalFeb = (Label)grdDisplayRepo.Rows[i].FindControl("lblRBalFeb");



            double totalDuesAmt = 0;
            double currAmt = 0, depoAmt = 0;


            //For April========================
            try
            {
                currAmt = Convert.ToDouble(lblTotInstallmentApr.Text);
            }
            catch (Exception) { currAmt = 0; }
            try
            {
                depoAmt = Convert.ToDouble(lblApr.Text);
            }
            catch (Exception) { depoAmt = 0; }

            if (currAmt > depoAmt)
            {
                totalDuesAmt = currAmt;

            }
            else
            {
                totalDuesAmt = depoAmt;
            }


            lblTotDuesApr.Text = totalDuesAmt.ToString();
            lblRBalApr.Text = Balance(lblTotDuesApr, lblApr);

            //April End========================



            //For May Total Dues Balance Start 

            try
            {
                currAmt = Convert.ToDouble(lblTotInstallmentMay.Text);
            }
            catch (Exception) { currAmt = 0; }
            try
            {
                depoAmt = Convert.ToDouble(lblMay.Text);
            }
            catch (Exception) { depoAmt = 0; }

            if (currAmt > depoAmt)
            {
                totalDuesAmt = currAmt;
                try
                {
                    totalDuesAmt = totalDuesAmt + Convert.ToDouble(lblRBalApr.Text);
                }
                catch (Exception) { }
                lblTotDuesMay.Text = totalDuesAmt.ToString();
                lblRBalMay.Text = Balance(lblTotDuesMay, lblMay);
            }
            else
            {
                totalDuesAmt = depoAmt;
                totalDuesAmt = totalDuesAmt + Convert.ToDouble(lblRBalApr.Text);
                lblTotDuesMay.Text = totalDuesAmt.ToString();
                lblRBalMay.Text = Balance(lblTotDuesMay, lblMay);
            }




            //For May Total Dues Balance End==================================



            // For Jul  Total Dues Balance Start===============================
            try
            {
                depoAmt = Convert.ToDouble(lblJul.Text);
            }
            catch (Exception) { depoAmt = 0; }
            try
            {
                currAmt = Convert.ToDouble(lblTotInstallmentJul.Text);
            }
            catch (Exception) { currAmt = 0; }

            if (currAmt > depoAmt)
            {
                totalDuesAmt = currAmt;
                try
                {
                    totalDuesAmt = totalDuesAmt + Convert.ToDouble(lblRBalMay.Text);
                }
                catch (Exception) { }
                lblTotDuesJul.Text = totalDuesAmt.ToString();
                lblRBalJul.Text = Balance(lblTotDuesJul, lblJul);
            }
            else
            {
                totalDuesAmt = depoAmt;
                totalDuesAmt = totalDuesAmt + Convert.ToDouble(lblRBalMay.Text);
                lblTotDuesJul.Text = totalDuesAmt.ToString();
                lblRBalJul.Text = Balance(lblTotDuesJul, lblJul);
            }

            // For Jul  Total Dues Balance End=================================



            // for Aug TotalDues Balance Start==================================

            try
            {
                depoAmt = Convert.ToDouble(lblAug.Text);

            }
            catch (Exception) { depoAmt = 0; }
            try
            {
                currAmt = Convert.ToDouble(lblTotInstallmentAug.Text);
            }
            catch (Exception) { currAmt = 0; }

            if (currAmt > depoAmt)
            {
                totalDuesAmt = currAmt;
                totalDuesAmt = totalDuesAmt + Convert.ToDouble(lblRBalJul.Text);
                lblTotDuesAug.Text = totalDuesAmt.ToString();
                lblRBalAug.Text = Balance(lblTotDuesAug, lblAug);
            }

            else
            {
                totalDuesAmt = depoAmt;
                totalDuesAmt = totalDuesAmt + Convert.ToDouble(lblRBalJul.Text);
                lblTotDuesAug.Text = totalDuesAmt.ToString();
                lblRBalAug.Text = Balance(lblTotDuesAug, lblAug);
            }



            // for Aug TotalDues Balance End==================================






            //for Sep Totaldues Balance Start===================================
            try
            {
                depoAmt = Convert.ToDouble(lblSep.Text);

            }
            catch (Exception) { depoAmt = 0; }
            try
            {
                currAmt = Convert.ToDouble(lblTotInstallmentSep.Text);
            }
            catch (Exception) { currAmt = 0; }

            if (currAmt > depoAmt)
            {
                totalDuesAmt = currAmt;
                totalDuesAmt = totalDuesAmt + Convert.ToDouble(lblRBalAug.Text);
                lblTotDuesSep.Text = totalDuesAmt.ToString();
                lblRBalSep.Text = Balance(lblTotDuesSep, lblSep);
            }

            else
            {
                totalDuesAmt = depoAmt;
                totalDuesAmt = totalDuesAmt + Convert.ToDouble(lblRBalAug.Text);
                lblTotDuesSep.Text = totalDuesAmt.ToString();
                lblRBalSep.Text = Balance(lblTotDuesSep, lblSep);
            }



            //for Sep Totaldues Balance Start===================================



            // For Oct TotalDues Balance Start==================================

            try
            {
                depoAmt = Convert.ToDouble(lblOct.Text);

            }
            catch (Exception) { depoAmt = 0; }
            try
            {
                currAmt = Convert.ToDouble(lblTotInstallmentOct.Text);
            }
            catch (Exception) { currAmt = 0; }

            if (currAmt > depoAmt)
            {
                totalDuesAmt = currAmt;
                totalDuesAmt = totalDuesAmt + Convert.ToDouble(lblRBalSep.Text);
                lblTotDuesOct.Text = totalDuesAmt.ToString();
                lblRBalOct.Text = Balance(lblTotDuesOct, lblOct);
            }

            else
            {
                totalDuesAmt = depoAmt;
                totalDuesAmt = totalDuesAmt + Convert.ToDouble(lblRBalSep.Text);
                lblTotDuesOct.Text = totalDuesAmt.ToString();
                lblRBalOct.Text = Balance(lblTotDuesSep, lblSep);
            }


            //For Oct TotalDues Balance End=====================================





            // For Nov TotalDues Balance Start==================================

            try
            {
                depoAmt = Convert.ToDouble(lblNov.Text);

            }
            catch (Exception) { depoAmt = 0; }
            try
            {
                currAmt = Convert.ToDouble(lblTotInstallmentNov.Text);
            }
            catch (Exception) { currAmt = 0; }

            if (currAmt > depoAmt)
            {
                totalDuesAmt = currAmt;
                totalDuesAmt = totalDuesAmt + Convert.ToDouble(lblRBalOct.Text);
                lblTotDuesNov.Text = totalDuesAmt.ToString();
                lblRBalNov.Text = Balance(lblTotDuesNov, lblNov);
            }

            else
            {
                totalDuesAmt = depoAmt;
                totalDuesAmt = totalDuesAmt + Convert.ToDouble(lblRBalOct.Text);
                lblTotDuesNov.Text = totalDuesAmt.ToString();
                lblRBalNov.Text = Balance(lblTotDuesNov, lblNov);
            }
            // For Nov TotalDues Balance End====================================



            //For Dec TotalDues Balance Start======================================

            try
            {
                depoAmt = Convert.ToDouble(lblDec.Text);

            }
            catch (Exception) { depoAmt = 0; }
            try
            {
                currAmt = Convert.ToDouble(lblTotInstallmentDec.Text);
            }
            catch (Exception) { currAmt = 0; }

            if (currAmt > depoAmt)
            {
                totalDuesAmt = currAmt;
                totalDuesAmt = totalDuesAmt + Convert.ToDouble(lblRBalNov.Text);
                lblTotDuesDec.Text = totalDuesAmt.ToString();
                lblRBalDec.Text = Balance(lblTotDuesDec, lblDec);
            }

            else
            {
                totalDuesAmt = depoAmt;
                totalDuesAmt = totalDuesAmt + Convert.ToDouble(lblRBalNov.Text);
                lblTotDuesDec.Text = totalDuesAmt.ToString();
                lblRBalDec.Text = Balance(lblTotDuesDec, lblDec);
            }
            //For Dec TotalDues Balance End=========================================

            //For Jan TotalDues Balance Start========================================
            try
            {
                depoAmt = Convert.ToDouble(lblJan.Text);

            }
            catch (Exception) { depoAmt = 0; }
            try
            {
                currAmt = Convert.ToDouble(lblTotInstallmentJan.Text);
            }
            catch (Exception) { currAmt = 0; }

            if (currAmt > depoAmt)
            {
                totalDuesAmt = currAmt;
                totalDuesAmt = totalDuesAmt + Convert.ToDouble(lblRBalDec.Text);
                lblTotDuesJan.Text = totalDuesAmt.ToString();
                lblRBalJan.Text = Balance(lblTotDuesJan, lblJan);
            }

            else
            {
                totalDuesAmt = depoAmt;
                totalDuesAmt = totalDuesAmt + Convert.ToDouble(lblRBalDec.Text);
                lblTotDuesJan.Text = totalDuesAmt.ToString();
                lblRBalJan.Text = Balance(lblTotDuesJan, lblJan);
            }

            //For Jan TotalDues Balance End==========================================



            //For Feb TotalDues Balance Start========================================
            try
            {
                depoAmt = Convert.ToDouble(lblFeb.Text);

            }
            catch (Exception) { depoAmt = 0; }
            try
            {
                currAmt = Convert.ToDouble(lblTotInstallmentFeb.Text);
            }
            catch (Exception) { currAmt = 0; }

            if (currAmt > depoAmt)
            {
                totalDuesAmt = currAmt;
                totalDuesAmt = totalDuesAmt + Convert.ToDouble(lblRBalJan.Text);
                lblTotDuesFeb.Text = totalDuesAmt.ToString();
                lblRBalFeb.Text = Balance(lblTotDuesFeb, lblFeb);
            }

            else
            {
                totalDuesAmt = depoAmt;
                totalDuesAmt = totalDuesAmt + Convert.ToDouble(lblRBalJan.Text);
                lblTotDuesFeb.Text = totalDuesAmt.ToString();
                lblRBalFeb.Text = Balance(lblTotDuesFeb, lblFeb);
            }

            //For Feb TotalDues Balance End==========================================








        }//For i for Grid






    }

    public bool YearlyFeesDeposit(string srno)
    {
        
        sql = "select YearlyFeeDepositYesNo from FeeDeposite where srno='" + srno + "'";
        sql = sql + "  and SessionName='" + Session["SessionName"].ToString() + "'";

        if (oo.ReturnTag(sql, "YearlyFeeDepositYesNo").Trim() == "Y")
        {
            return true;
        }
        else
        {
            return false;
        }

    }


 



    public void FooterGridCalculationForTotalDues()
    {
        int i;
        double AprSum = 0,MaySum=0,JulSum=0,AugSum=0,SepSum=0,OctSum=0,NovSum=0,DecSum=0,JanSum=0,FebSum=0;
        for (i = 0; i <= grdDisplayRepo.Rows.Count - 1; i++)
        {
            Label lblTotDuesApr = (Label)grdDisplayRepo.Rows[i].FindControl("lblTotDuesApr");
            Label lblTotDuesMay = (Label)grdDisplayRepo.Rows[i].FindControl("lblTotDuesMay");
            Label lblTotDuesJul = (Label)grdDisplayRepo.Rows[i].FindControl("lblTotDuesJul");
            Label lblTotDuesAug = (Label)grdDisplayRepo.Rows[i].FindControl("lblTotDuesAug");
            Label lblTotDuesSep = (Label)grdDisplayRepo.Rows[i].FindControl("lblTotDuesSep");
            Label lblTotDuesOct = (Label)grdDisplayRepo.Rows[i].FindControl("lblTotDuesOct");
            Label lblTotDuesNov = (Label)grdDisplayRepo.Rows[i].FindControl("lblTotDuesNov");
            Label lblTotDuesDec = (Label)grdDisplayRepo.Rows[i].FindControl("lblTotDuesDec");
            Label lblTotDuesJan = (Label)grdDisplayRepo.Rows[i].FindControl("lblTotDuesJan");
            Label lblTotDuesFeb = (Label)grdDisplayRepo.Rows[i].FindControl("lblTotDuesFeb");
            try
            {
                AprSum = AprSum + Convert.ToDouble(lblTotDuesApr.Text);
            }
            catch (Exception) { }

            try
            {
                MaySum = MaySum + Convert.ToDouble(lblTotDuesMay.Text);
            }
            catch (Exception) { }

            try
            {
                JulSum = JulSum + Convert.ToDouble(lblTotDuesJul.Text);
            }
            catch (Exception) { }

            try
            {
                AugSum = AugSum + Convert.ToDouble(lblTotDuesAug.Text);
            }
            catch (Exception) { }

            try
            {
                SepSum = SepSum + Convert.ToDouble(lblTotDuesSep.Text);
            }
            catch (Exception) { }

            try
            {
                OctSum = OctSum + Convert.ToDouble(lblTotDuesOct.Text);
            }
            catch (Exception) { }

            try
            {
                NovSum = NovSum + Convert.ToDouble(lblTotDuesNov.Text);
            }
            catch (Exception) { }

            try
            {
                DecSum = DecSum + Convert.ToDouble(lblTotDuesDec.Text);
            }
            catch (Exception) { }

            try
            {
                JanSum = JanSum + Convert.ToDouble(lblTotDuesJan.Text);
            }
            catch (Exception) { }

            try
            {
                FebSum = FebSum + Convert.ToDouble(lblTotDuesFeb.Text);
            }
            catch (Exception) { }
        }

        try
        {
            Label lblGrandTDuesApr = (Label)grdDisplayRepo.FooterRow.FindControl("lblGrandTDuesApr");
            Label lblGrandTDuesMay = (Label)grdDisplayRepo.FooterRow.FindControl("lblGrandTDuesMay");
            Label lblGrandTDuesJul = (Label)grdDisplayRepo.FooterRow.FindControl("lblGrandTDuesJul");
            Label lblGrandTDuesAug = (Label)grdDisplayRepo.FooterRow.FindControl("lblGrandTDuesAug");
            Label lblGrandTDuesSep = (Label)grdDisplayRepo.FooterRow.FindControl("lblGrandTDuesSep");
            Label lblGrandTDuesOct = (Label)grdDisplayRepo.FooterRow.FindControl("lblGrandTDuesOct");
            Label lblGrandTDuesNov = (Label)grdDisplayRepo.FooterRow.FindControl("lblGrandTDuesNov");
            Label lblGrandTDuesDec = (Label)grdDisplayRepo.FooterRow.FindControl("lblGrandTDuesDec");
            Label lblGrandTDuesJan = (Label)grdDisplayRepo.FooterRow.FindControl("lblGrandTDuesJan");
            Label lblGrandTDuesFeb = (Label)grdDisplayRepo.FooterRow.FindControl("lblGrandTDuesFeb");


            lblGrandTDuesApr.Text = AprSum.ToString();
            lblGrandTDuesMay.Text = MaySum.ToString();
            lblGrandTDuesJul.Text = JulSum.ToString();
            lblGrandTDuesAug.Text = AugSum.ToString();
            lblGrandTDuesSep.Text = SepSum.ToString();
            lblGrandTDuesOct.Text = OctSum.ToString();
            lblGrandTDuesNov.Text = NovSum.ToString();
            lblGrandTDuesDec.Text = DecSum.ToString();
            lblGrandTDuesJan.Text = JanSum.ToString();
            lblGrandTDuesFeb.Text = FebSum.ToString();
        }
        catch (Exception) { oo.MessageBox("No Record(s) Found", this.Page); }
    }

  
    public void FindAllTotalDuesofFooterRows()
    {
        double sum1=0;
        try
        {
            Label lblGrandTDuesApr = (Label)grdDisplayRepo.FooterRow.FindControl("lblGrandTDuesApr");
            Label lblGrandTDuesMay = (Label)grdDisplayRepo.FooterRow.FindControl("lblGrandTDuesMay");
            Label lblGrandTDuesJul = (Label)grdDisplayRepo.FooterRow.FindControl("lblGrandTDuesJul");
            Label lblGrandTDuesAug = (Label)grdDisplayRepo.FooterRow.FindControl("lblGrandTDuesAug");
            Label lblGrandTDuesSep = (Label)grdDisplayRepo.FooterRow.FindControl("lblGrandTDuesSep");
            Label lblGrandTDuesOct = (Label)grdDisplayRepo.FooterRow.FindControl("lblGrandTDuesOct");
            Label lblGrandTDuesNov = (Label)grdDisplayRepo.FooterRow.FindControl("lblGrandTDuesNov");
            Label lblGrandTDuesDec = (Label)grdDisplayRepo.FooterRow.FindControl("lblGrandTDuesDec");
            Label lblGrandTDuesJan = (Label)grdDisplayRepo.FooterRow.FindControl("lblGrandTDuesJan");
            Label lblGrandTDuesFeb = (Label)grdDisplayRepo.FooterRow.FindControl("lblGrandTDuesFeb");
            Label lblArrearTotAmt = (Label)grdDisplayRepo.FooterRow.FindControl("lblArrearTotAmt");
            
            sum1=sum1+Convert.ToDouble(lblGrandTDuesApr.Text);
            sum1=sum1+Convert.ToDouble(lblGrandTDuesApr.Text );
            sum1=sum1+Convert.ToDouble( lblGrandTDuesMay.Text); 
            sum1=sum1+Convert.ToDouble(lblGrandTDuesJul.Text ); 
            sum1=sum1+Convert.ToDouble(lblGrandTDuesAug.Text );
            sum1=sum1+Convert.ToDouble(lblGrandTDuesSep.Text ); 
            sum1=sum1+Convert.ToDouble(lblGrandTDuesOct.Text );
            sum1=sum1+Convert.ToDouble(lblGrandTDuesNov.Text );
            sum1=sum1+Convert.ToDouble(lblGrandTDuesDec.Text );
            sum1=sum1+Convert.ToDouble(lblGrandTDuesJan.Text );
            sum1 = sum1 + Convert.ToDouble(lblGrandTDuesFeb.Text);

            lblTDues.Text = "All Total Dues Amount :"+sum1.ToString();
        }
        catch (Exception) { oo.MessageBox("No Record(s) Found", this.Page); }
    }
}
