using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;
using System.Drawing;
using System.Drawing.Printing;
public partial class StudentReceiptGenerate : Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql, sql1 = string.Empty;
#pragma warning disable 649
    Font verdana10Font;
#pragma warning restore 649
#pragma warning disable 649
    StreamReader reader;
#pragma warning restore 649


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        if ((string)Session["RecieptNoSession"] == "" || Session["RecieptNoSession"] == null)
        {
            Response.Redirect("fee_deposit_old.aspx");
        }


        Label1.Text = Session["RecieptNoSession"].ToString();
        if (!IsPostBack)
        {
            try
            {
                BLL obj = new BLL();
                obj.LoadHeader("Receipt", header1);
                obj.LoadHeader("Receipt", header2);
                LaserPrint();
            }
            catch (Exception) { }
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        // LaserPrint();
        BLL obj = new BLL();
        obj.LoadHeader("Receipt", header1);
        obj.LoadHeader("Receipt", header2);
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }




    public void DotMatrixPrint()
    {

        int i = 0;
        //For Header Control
        //========================================================================================================== Start
        string colegeName = "", CollegAdd1 = "", CollegeAdd2 = "", Phone = "", Website = "", Email = "", CityId = "";
        sql = "select CollegeName,CollegeAdd1,CollegeAdd2,Phone,WebSite,Email,CollegeLogo,CityId from CollegeMaster where BranchCode=" + Session["BranchCode"] + "";
        try
        {
            colegeName = oo.ReturnTag(sql, "CollegeName");
        }
        catch (Exception) { Label1.Visible = false; }
        try
        {
            CollegAdd1 = oo.ReturnTag(sql, "CollegeAdd1");
        }
        catch (Exception) { }
        try
        {
            CollegeAdd2 = oo.ReturnTag(sql, "CollegeAdd2");
        }
        catch (Exception) { }
        try
        {
            Phone = oo.ReturnTag(sql, "Phone");

            // Lbleph.Text = "Ph:";
        }
        catch (Exception) { }

        try
        {
            Website = oo.ReturnTag(sql, "WebSite");

            //lblWebsite.Text = "Website:";
        }
        catch (Exception) { }


        try
        {
            CityId = oo.ReturnTag(sql, "CityId");
        }
        catch (Exception) { }



        //try
        //{
        // Label4.Text = oo.ReturnTag(sql, "WebSite");
        //}
        //catch (Exception) { Label4.Visible = false; }
        try
        {
            Email = oo.ReturnTag(sql, "Email");


        }
        catch (Exception) { }





        FileStream fs = new FileStream("d:\\Reciept\\Reciept.txt", FileMode.Create, FileAccess.Write);
        StreamWriter sw = new StreamWriter(fs);
        sw.WriteLine("------------------------------------------------------------------------------------");
        sw.WriteLine("                                           FEE RECEIPT ");
        sw.WriteLine("------------------------------------------------------------------------------------");
        sw.WriteLine("                        " + colegeName.ToUpper());
        sw.WriteLine("                         " + CollegAdd1 + " " + CollegeAdd2);
        sw.WriteLine("                                     " + CityId + " U.P. India");
        sw.WriteLine("                        Ph:-" + Phone + " " + "Email :" + Email);
        sw.WriteLine("------------------------------------------------------------------------------------");


        try
        {
            Label Label34 = (Label)GridView1.HeaderRow.FindControl("Label34");
            Label Label7 = (Label)GridView1.HeaderRow.FindControl("Label7");

            sw.WriteLine("Receipt No :" + Label7.Text + "                                         " + Label34.Text);
            sw.WriteLine("------------------------------------------------------------------------------------");
            Label Label11 = (Label)GridView1.HeaderRow.FindControl("Label11");
            Label Label24 = (Label)GridView1.HeaderRow.FindControl("Label24");
            sw.WriteLine("S.R.No.:" + Label24.Text + "                                                  " + "                 Installment :" + Label11.Text);
            Label Label8 = (Label)GridView1.HeaderRow.FindControl("Label8");
            Label Label15 = (Label)GridView1.HeaderRow.FindControl("Label15");
            Label Label16 = (Label)GridView1.HeaderRow.FindControl("Label16");
            Label Label9 = (Label)GridView1.HeaderRow.FindControl("Label9");
            Label Label19 = (Label)GridView1.HeaderRow.FindControl("Label19");



            sw.WriteLine("Student's Name :" + Label8.Text + " " + Label15.Text + " " + Label16.Text + "                        Class :" + Label9.Text + "(" + Label19.Text + ")");

            Label Label23 = (Label)GridView1.HeaderRow.FindControl("Label23");

            Label Label29 = (Label)GridView1.HeaderRow.FindControl("Label29");

            sw.WriteLine("Father's Name :" + Label23.Text.Trim() + "                       Medium : " + Label29.Text.Trim());
        }
        catch (Exception) { }



        sw.WriteLine("------------------------------------------------------------------------------------");
        sw.WriteLine("S. No.        Particulars                                                                Amount");
        sw.WriteLine("------------------------------------------------------------------------------------");

        for (i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            Label Label25 = (Label)GridView1.Rows[i].FindControl("Label25");//Srno
            Label Label12 = (Label)GridView1.Rows[i].FindControl("Label12");//Particulars
            Label Label13 = (Label)GridView1.Rows[i].FindControl("Label13");//Amount
            // sw.WriteLine(Label25.Text.Trim() + "              " + Label12.Text.Trim() + "                                   " + Label13.Text.Trim());
            WriteingLines(Label25.Text.Trim(), Label12.Text.Trim(), Label13.Text.Trim(), sw, i + 1);

        }
        sw.WriteLine("------------------------------------------------------------------------------------");


        //Footer


        try
        {

            Label lbllateFeesAmt = (Label)GridView1.FooterRow.FindControl("Label17");
            Label lfTotalAmt = (Label)GridView1.FooterRow.FindControl("lblAmttotal");
            Label Label17 = (Label)GridView1.FooterRow.FindControl("Label17");
            Label Label33 = (Label)GridView1.FooterRow.FindControl("Label33");
            Label Label341 = (Label)GridView1.FooterRow.FindControl("Label341");
            Label Label18 = (Label)GridView1.FooterRow.FindControl("Label18");
            Label Label27 = (Label)GridView1.FooterRow.FindControl("Label27");
            Label Label30 = (Label)GridView1.FooterRow.FindControl("Label30");
            Label lblBusConvence = (Label)GridView1.FooterRow.FindControl("lblBusConvence");
            sw.WriteLine("Late Fee :                                                                                  " + Label17.Text);
            sw.WriteLine("Bus Conveyance :                                                                       " + lblBusConvence.Text);
            sw.WriteLine("Total Amount :                                                                           " + lfTotalAmt.Text + ".00");
            sw.WriteLine("Concession :                                                                             " + Label33.Text);
            sw.WriteLine("Payble Amount :                                                                       " + Label341.Text + ".00");
            sw.WriteLine("Recieved Amount :                                                                   " + Label18.Text);
            sw.WriteLine("------------------------------------------------------------------------------------");
            sw.WriteLine(Label27.Text);
            sw.WriteLine("------------------------------------------------------------------------------------");
            try
            {
                sw.WriteLine("Balance Amount:                                                            " + Label30.Text);
            }
            catch (Exception)
            {
                sw.WriteLine("Balance Amount:                                                                          0.00");
            }
            sw.WriteLine("------------------------------------------------------------------------------------");
            sw.WriteLine(" ");
            sw.WriteLine(" ");
            sw.WriteLine("                                                                           Signature");
            sw.WriteLine(" ");

        }
        catch (Exception) { }

        sw.Flush();

        sw.Close();

        fs.Close();
    }



    public void WriteingLines(string xx, string yy, string zz, StreamWriter sw, int po)
    {

        string[] arr = new string[121];
        int i;
        string ff = "";
        int co = 0;
        for (i = 0; i <= 120; i++)
        {
            arr[i] = " ";
            ff = ff + arr[i];
        }

        for (i = 0; i <= xx.Length - 1; i++)
        {
            arr[co] = xx[i].ToString();
            co++;
        }
        co = 16;

        for (i = 0; i <= yy.Length - 1; i++)
        {
            arr[co] = yy[i].ToString();
            co++;
        }

        co = po + 80;
        for (i = 0; i <= zz.Length - 1; i++)
        {
            arr[co] = zz[i].ToString();
            co++;
        }
        string kk = "";
        for (i = 0; i <= 120; i++)
        {
            kk = kk + arr[i].ToString();
        }
        // po = po + 1;
        string mm = kk;
        sw.WriteLine(kk.Trim());

    }


    //protected void LinkButton2_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        DotMatrixPrint();
    //        Label1.Text = Session["RecieptNoSession"].ToString();

    //    }
    //    catch (IOException) { }
    //    // System.Diagnostics.Process.Start("PRINT", "D:\\Reciept.txt");
    //    string filename = "d:\\Receipt\\Receipt.txt";
    //    try
    //    {
    //        //Create a StreamReader object
    //        reader = new StreamReader(filename);
    //        //Create a Verdana font with size 10
    //        verdana10Font = new Font("Verdana", 10);
    //        //Create a PrintDocument object
    //        PrintDocument pd = new PrintDocument();
    //        //Add PrintPage event handler
    //        pd.PrintPage += new PrintPageEventHandler(this.PrintTextFileHandler);
    //        //Call Print Method
    //        pd.Print();
    //        //Close the reader
    //        if (reader != null)
    //            reader.Close();
    //    }
    //    catch (Exception) { oo.MessageBox("Please check printer!", this.Page); }
    //}


    private void PrintTextFileHandler(object sender, PrintPageEventArgs ppeArgs)
    {

        //Get the Graphics object
        Graphics g = ppeArgs.Graphics;
        float linesPerPage = 0;
        float yPos = 0;
        int count = 0;
        //Read margins from PrintPageEventArgs
        float leftMargin = ppeArgs.MarginBounds.Left;
        float topMargin = ppeArgs.MarginBounds.Top;
        string line = null;
        //Calculate the lines per page on the basis of the height of the page and the height of the font
        linesPerPage = ppeArgs.MarginBounds.Height /
        verdana10Font.GetHeight(g);
        //Now read lines one by one, using StreamReader
        while (count < linesPerPage && ((line = reader.ReadLine()) != null))
        {
            //Calculate the starting position
            yPos = topMargin + (count *
            verdana10Font.GetHeight(g));
            //Draw text
            g.DrawString(line, verdana10Font, Brushes.Black,
            leftMargin, yPos, new StringFormat());
            //Move to next line
            count++;
        }
        //If PrintPageEventArgs has more pages to print
        if (line != null)
        {
            ppeArgs.HasMorePages = true;
        }
        else
        {
            ppeArgs.HasMorePages = false;
        }
    }

    public void LaserPrint()
    {
        double busconvence = 0;
        sql = "select  fd.SrNo as SrNo ,sg.FirstName as FirstName,sg.MiddleName as MiddleName,SOD.Medium as Medium ,sg.LastName as LastName,SF.FatherName as FatherName,convert(nvarchar,fd.FeeDepositeDate,106) as FeeDepositeDate,sod.TypeOFAdmision as TypeOfAdmission ,";
        sql = sql + "   fd.LateFeeAmount as LateFeeAmount,fd.RemainingAmount as RemainingAmount ,fd.Cocession as Cocession ,fd.RecievedAmount as RecievedAmount,fd.AmountInWords,fd.BusConvience as BusConvience,fd.FeeMonth as FeeMonth,fd.Class as Class,fd.section as section,fd.DiscountName as DiscountName,fd.DiscountAmount as DiscountAmount from FeeDeposite Fd ";
        sql = sql + "   left join StudentGenaralDetail sg on Fd.StEnRCode=sg.StEnRCode";
        sql = sql + "  left join StudentFamilyDetails SF on Fd.StEnRCode=SF.StEnRCode";
        sql = sql + "  left join StudentOfficialDetails SOD on Fd.StEnRCode=SOD.StEnRCode";
        sql = sql + "  where fd.RecieptSrNo='" + Label1.Text + "'";
        sql = sql + "  and sg.SessionName='" + Session["SessionName"].ToString() + "' and sg.BranchCode=" + Session["BranchCode"].ToString() + "";
        sql = sql + " and fd.SessionName='" + Session["SessionName"].ToString() + "' and sf.SessionName='" + Session["SessionName"].ToString() + "'  and sod.SessionName='" + Session["SessionName"].ToString() + "'";

        

    
        sql1 = " select ROW_NUMBER() OVER (ORDER BY Id ASC) AS serialno,Id,fa.Month, fa.FeeParticular,fa.Class,fa.FeeType,fa.FeePayment,FM.Medium,Fa.CardType ,fm.NoOfmonths as NoOfmonths from FeeAllotedForClassWise fa ";
        sql1 = sql1 + " left join feemaster  fm on fa.Medium=fm.medium  and fa.FeeName=fm.FeeName  ";
        sql1 = sql1 + " where fa.Class='" + oo.ReturnTag(sql, "Class") + "'   and fa.Month='" + oo.ReturnTag(sql, "FeeMonth") + "' and fa.SessionName='" + Session["SessionName"].ToString() + "'  and fa.BranchCode=" + Session["BranchCode"].ToString() + "";
        sql1 = sql1 + "and fa.CardType='" + Session["CardType"].ToString() + "' and   fa.AdmissionType='" + oo.ReturnTag(sql, "TypeOfAdmission") + "'  and fa.Medium='" + oo.ReturnTag(sql, "Medium") + "'";
        sql1 = sql1 + " and fm.SessionName='" + Session["SessionName"].ToString() + "'";



        GridView1.DataSource = oo.GridFill(sql1);
        GridView1.DataBind();
        GridView2.DataSource = oo.GridFill(sql1);
        GridView2.DataBind();

        try
        {

            Label Label34 = (Label)GridView1.HeaderRow.FindControl("Label34");
            Label34.Text = oo.ReturnTag(sql, "FeeDepositeDate");


            Label Label340 = (Label)GridView2.HeaderRow.FindControl("Label340");
            Label340.Text = oo.ReturnTag(sql, "FeeDepositeDate");

        }

        catch (Exception) { }
        try
        {
            Label Label9 = (Label)GridView1.HeaderRow.FindControl("Label9");
            Label9.Text = oo.ReturnTag(sql, "Class");

            Label Label90 = (Label)GridView2.HeaderRow.FindControl("Label90");
            Label90.Text = oo.ReturnTag(sql, "Class");
        }
        catch (Exception) { }

        try
        {
            Label Label19 = (Label)GridView1.HeaderRow.FindControl("Label19");
            Label19.Text = oo.ReturnTag(sql, "section");

            Label Label190 = (Label)GridView2.HeaderRow.FindControl("Label190");
            Label190.Text = oo.ReturnTag(sql, "section");
        }
        catch (Exception) { }

        try
        {
            Label Label24 = (Label)GridView1.HeaderRow.FindControl("Label24");
            Label24.Text = oo.ReturnTag(sql, "SrNo");


            Label Label240 = (Label)GridView2.HeaderRow.FindControl("Label240");
            Label240.Text = oo.ReturnTag(sql, "SrNo");
        }
        catch (Exception) { }

        try
        {
            Label Label11 = (Label)GridView1.HeaderRow.FindControl("Label11");
            Label11.Text = oo.ReturnTag(sql, "FeeMonth");

            Label Label110 = (Label)GridView2.HeaderRow.FindControl("Label110");
            Label110.Text = oo.ReturnTag(sql, "FeeMonth");
        }
        catch (Exception) { }


        try
        {
            Label Label8 = (Label)GridView1.HeaderRow.FindControl("Label8");
            Label8.Text = oo.ReturnTag(sql, "FirstName");

            Label Label80 = (Label)GridView2.HeaderRow.FindControl("Label80");
            Label80.Text = oo.ReturnTag(sql, "FirstName");
        }
        catch (Exception) { }

        try
        {

            Label Label15 = (Label)GridView1.HeaderRow.FindControl("Label15");
            Label15.Text = oo.ReturnTag(sql, "MiddleName");

            Label Label150 = (Label)GridView2.HeaderRow.FindControl("Label150");
            Label150.Text = oo.ReturnTag(sql, "MiddleName");
        }
        catch (Exception) { }
        try
        {

            Label Label23 = (Label)GridView1.HeaderRow.FindControl("Label23");
            Label23.Text = oo.ReturnTag(sql, "FatherName");

            Label Label230 = (Label)GridView2.HeaderRow.FindControl("Label230");
            Label230.Text = oo.ReturnTag(sql, "FatherName");

        }
        catch (Exception) { }
        try
        {

            Label Label16 = (Label)GridView1.HeaderRow.FindControl("Label16");
            Label16.Text = oo.ReturnTag(sql, "LastName");


            Label Label160 = (Label)GridView2.HeaderRow.FindControl("Label160");
            Label160.Text = oo.ReturnTag(sql, "LastName");

        }
        catch (Exception) { }

        try
        {

            Label Label7 = (Label)GridView1.HeaderRow.FindControl("Label7");
            Label7.Text = Label1.Text;

            Label Label70 = (Label)GridView2.HeaderRow.FindControl("Label70");
            Label70.Text = Label1.Text;
        }
        catch (Exception) { }

        try
        {

            Label Label17 = (Label)GridView1.FooterRow.FindControl("Label17");
            Label17.Text = oo.ReturnTag(sql, "LateFeeAmount");

            Label Label170 = (Label)GridView2.FooterRow.FindControl("Label170");
            Label170.Text = oo.ReturnTag(sql, "LateFeeAmount");
        }
        catch (Exception) { }


        try
        {

            Label lblBusConvence = (Label)GridView1.FooterRow.FindControl("lblBusConvence");
            if (oo.ReturnTag(sql, "BusConvience") == "")
            {
                lblBusConvence.Text = "0.00";
                busconvence = 0;
            }
            else
            {
                lblBusConvence.Text = oo.ReturnTag(sql, "BusConvience");
                busconvence = Convert.ToDouble(lblBusConvence.Text);
            }

            Label lblBusConvence1 = (Label)GridView2.FooterRow.FindControl("lblBusConvence1");
            if (oo.ReturnTag(sql, "BusConvience") == "")
            {
                lblBusConvence1.Text = "0.00";
            }
            else
            {

                lblBusConvence1.Text = oo.ReturnTag(sql, "BusConvience");
            }
        }
        catch (Exception) { }


        try
        {


            Label Label30 = (Label)GridView1.FooterRow.FindControl("Label30");

            if (oo.ReturnTag(sql, "RemainingAmount") == "")
            {
                Label30.Text = "0.00";
            }
            else
            {
                Label30.Text = oo.ReturnTag(sql, "RemainingAmount");
            }


            Label Label300 = (Label)GridView2.FooterRow.FindControl("Label300");

            if (oo.ReturnTag(sql, "RemainingAmount") == "")
            {
                Label300.Text = "0.00";
            }
            else
            {
                Label300.Text = oo.ReturnTag(sql, "RemainingAmount");
            }



            //if (Label30.Text.Trim() == "0.00")
            //{
            //    Label30.Text = Session["LastMonthRemainsAmt"].ToString();
            //}
            //if (Label300.Text.Trim() == "0.00")
            //{
            //    Label300.Text = Session["LastMonthRemainsAmt"].ToString();
            //}


        }
        catch (Exception) { }


        try
        {

            Label Label33 = (Label)GridView1.FooterRow.FindControl("Label33");
            Label33.Text = oo.ReturnTag(sql, "Cocession");


            Label Label330 = (Label)GridView2.FooterRow.FindControl("Label330");
            Label330.Text = oo.ReturnTag(sql, "Cocession");

        }
        catch (Exception) { }

        try
        {
            Label Label18 = (Label)GridView1.FooterRow.FindControl("Label18");
            Label18.Text = oo.ReturnTag(sql, "RecievedAmount");


            Label Label180 = (Label)GridView2.FooterRow.FindControl("Label180");
            Label180.Text = oo.ReturnTag(sql, "RecievedAmount");

        }

        catch (Exception) { }

        try
        {


            Label Label27 = (Label)GridView1.FooterRow.FindControl("Label27");
            Label27.Text = oo.ReturnTag(sql, "AmountInWords");


            Label Label270 = (Label)GridView2.FooterRow.FindControl("Label270");
            Label270.Text = oo.ReturnTag(sql, "AmountInWords");

        }
        catch (Exception) { }
        try
        {

            Label Label29 = (Label)GridView1.HeaderRow.FindControl("Label29");
            Label29.Text = oo.ReturnTag(sql, "Medium");

            Label Label290 = (Label)GridView2.HeaderRow.FindControl("Label290");
            Label290.Text = oo.ReturnTag(sql, "Medium");

        }
        catch (Exception) { }

        //Session["LastMonthRemainsAmt"] = "";
        //Session["CardType"] = "";


        try
        {
            Label lblPreviousBalance = (Label)GridView1.FooterRow.FindControl("lblPreviousBalance");
            Label lblPreviousBalance1 = (Label)GridView2.FooterRow.FindControl("lblPreviousBalance1");
            lblPreviousBalance.Text = Session["LastMonthRemainsAmt"].ToString();
            lblPreviousBalance1.Text = Session["LastMonthRemainsAmt"].ToString();
        }
        catch (Exception) { }




        // GridView1.DataBind();

        //findTotalAmount
        
        int i = 0;
        double ta = 0;
        try
        {
            for (i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                Label ll = (Label)GridView1.Rows[i].FindControl("Label13");
                ta = ta + Convert.ToDouble(ll.Text.Trim());

            }
            try
            {
                Label lbllateFeesAmt = (Label)GridView1.FooterRow.FindControl("Label17");
                ta = ta + Convert.ToDouble(lbllateFeesAmt.Text);
            }
            catch (Exception) { }

        }
        catch (Exception) { }
        //Find Total Amount of Previous Balance First Grid

        try
        {
            Label lblPreviousBalance1 = (Label)GridView1.FooterRow.FindControl("lblPreviousBalance");
            ta = ta + Convert.ToDouble(lblPreviousBalance1.Text);
        }


        catch (Exception) { }


       
        
        
        //End  Total Amount of Previous Balance First Grid





        //ta = ta + Convert.ToDouble(lbllateFeesAmt.Text);
        ta = ta + busconvence;
        try
        {
            Label lfTotalAmt = (Label)GridView1.FooterRow.FindControl("lblAmttotal");
            lfTotalAmt.Text = ta.ToString();
        }
        catch (Exception) { }
        //Total Amount calculation


        ta = 0;
        //findTotalAmount  for gridview2


        for (i = 0; i <= GridView2.Rows.Count - 1; i++)
        {
            Label ll = (Label)GridView2.Rows[i].FindControl("Label130");
            ta = ta + Convert.ToDouble(ll.Text.Trim());

        }

        Label lbllateFeesAmt1 = (Label)GridView2.FooterRow.FindControl("Label170");

        try
        {
            Label lblPreviousBalance2 = (Label)GridView2.FooterRow.FindControl("lblPreviousBalance");
            ta = ta + Convert.ToDouble(lblPreviousBalance2.Text);
        }


        catch (Exception) { }

        try
        {
            ta = ta + Convert.ToDouble(lbllateFeesAmt1.Text);
        }
        catch (Exception) { }

        ta = ta + busconvence;


        Label lfTotalAmt1 = (Label)GridView2.FooterRow.FindControl("lblAmttotal");
        lfTotalAmt1.Text = ta.ToString();

        //Total Amount calculation


        //Payable Amount


        Label Label341 = (Label)GridView1.FooterRow.FindControl("Label341");
        Label concess = (Label)GridView1.FooterRow.FindControl("Label33");

        try
        {
          Label341.Text = (Convert.ToDouble(lfTotalAmt1.Text.ToString()) - Convert.ToDouble(concess.Text.ToString())).ToString();
        }
        catch (Exception) { }




        Label Label342 = (Label)GridView2.FooterRow.FindControl("Label342");
        Label concess1 = (Label)GridView2.FooterRow.FindControl("Label330");

        try
        {
            Label342.Text = (Convert.ToDouble(lfTotalAmt1.Text.ToString()) - Convert.ToDouble(concess1.Text.ToString())).ToString();
        }
        catch (Exception) { }

        //Discount Add 
        try
        {
            Label lblDiscount = (Label)GridView1.FooterRow.FindControl("lblDiscount");
            Label lblDiscountValue = (Label)GridView1.FooterRow.FindControl("lblDiscountValue");
            lblDiscount.Text = oo.ReturnTag(sql, "DiscountName");
            lblDiscountValue.Text = oo.ReturnTag(sql, "DiscountAmount");

        }

        catch (Exception) { }


        try
        {
            Label lblDiscountGrd2 = (Label)GridView2.FooterRow.FindControl("lblDiscountGrd2");
            Label lblDiscountValueGrd2 = (Label)GridView2.FooterRow.FindControl("lblDiscountValueGrd2");
            lblDiscountGrd2.Text = oo.ReturnTag(sql, "DiscountName");
            lblDiscountValueGrd2.Text = oo.ReturnTag(sql, "DiscountAmount");

        }

        catch (Exception) { }
        string Namt = "";
        try
        {
            Namt = Session["NextDueAmt"].ToString();
            Label lblNexDueAmt = (Label)GridView1.FooterRow.FindControl("lblNexDueAmt");
            lblNexDueAmt.Text = "Next Due Amount :" + Namt;
            Session["NextDueAmt"] = "";
        }
        catch (Exception) { }


        ArrierAmt();
        if ((string)Session["RCancel"] == "")
        {
            lblCancel.Visible = false;
            lblCancelStudent.Visible = false;
        }
        else
        {
            lblCancel.Text = "(" + Session["RCancel"].ToString() + ")";
            lblCancelStudent.Text = "(" + Session["RCancel"].ToString() + ")";
        }

      
      
    }


    public void ArrierAmt()
    {
        string amt = "";

        try
        {
            Label Label11 = (Label)GridView1.HeaderRow.FindControl("Label11");
            Label Label110 = (Label)GridView2.HeaderRow.FindControl("Label110");
            Label lblSrno = (Label)GridView1.HeaderRow.FindControl("Label24");
            Label lblPrevbalance = (Label)GridView1.FooterRow.FindControl("lblPrevbalance");
            Label lblPrevBalAmt = (Label)GridView2.FooterRow.FindControl("lblPrevBalAmt");

            Label lblPreviousBalance = (Label)GridView1.FooterRow.FindControl("lblPreviousBalance");//ForBalanceValue
            Label lblPreviousBalanceSecondGrid = (Label)GridView2.FooterRow.FindControl("lblPreviousBalance1");//ForBalanceValue

            

        if (Label11.Text.ToUpper() == "JULY" || Label11.Text.ToUpper() == "APRIL")
        {
            sql = "select  ArrearAmt  from ArrierMast   where srno=" + "'" + lblSrno.Text.Trim() + "'";
            amt = oo.ReturnTag(sql, "ArrearAmt");

            if (amt == "")
            {
                lblPrevbalance.Text = "Balance Amount (Previous) : ";
                lblPrevBalAmt.Text = "Balance Amount (Previous) : ";
            }
            else
            {
                lblPrevbalance.Text = "Balance Amount (Arrear) : ";
                lblPrevBalAmt.Text = "Balance Amount (Arrear) : ";
                lblPreviousBalance.Text=amt;
                lblPreviousBalanceSecondGrid.Text = amt;

            }



            //Balance Amount (Previous) : 
        }
        else
        {
            lblPrevbalance.Text = "Balance Amount (Previous) : ";
            lblPrevBalAmt.Text = "Balance Amount (Previous) : ";
        }

        }
        catch (Exception) { }
    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        //try
        //{
            if (Session["FD"].ToString() == "FD")
            {
                Response.Redirect("fee_deposit_old.aspx");
            }
            else
            {
                Response.Redirect("FeeDepositBalance.aspx");

            }

        //}
        //catch (Exception) { Response.Redirect("default.aspx");}
    }

}

