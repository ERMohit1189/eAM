using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;
using System.Drawing;
using System.Drawing.Printing;
using Zen.Barcode;
using System.Collections.Generic;
using System.Globalization;

public partial class StudentReceiptGenerate : Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql,sql1 = string.Empty;
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
        if (Session["RecieptNoSession"] == null)
        {
            if (Session["FD"].ToString() == "TFD")
            {
                Response.Redirect("TransportFeeDeposit.aspx");
            }
            else if (Session["FD"].ToString() == "FD")
            {
                Response.Redirect("fee_deposit_New.aspx");
            }
        }
        Label1.Text = Session["RecieptNoSession"].ToString();
        if (!IsPostBack)
        {
            try
            {
                BLL obj = new BLL();
                BLL.BLLInstance.LoadReceiptHeader(header1);
                BLL.BLLInstance.LoadReceiptHeader(header2);
                LinkButton1.Focus();
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
        }
        catch (Exception) { }

        try
        {
            Website = oo.ReturnTag(sql, "WebSite");
        }
        catch (Exception) { }


        try
        {
            CityId = oo.ReturnTag(sql, "CityId");
        }
        catch (Exception) { }

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

            sw.WriteLine("Father's Name :" + Label23.Text.Trim() + "                       Mode : " + Label29.Text.Trim());
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
        sql = "Select compatibility_level from sys.databases where name='eamdb'";
        string level = BAL.objBal.ReturnTag(sql, "compatibility_level");
        
        sql = "Select  fd.SrNo,asr.Name StudentName,Fd.MOP,asr.Medium,asr.FatherName,";
        sql = sql + "  convert(nvarchar,fd.FeeDepositeDate,106) as FeeDepositeDate,asr.TypeOFAdmision,";
        
        if (level == "110")
        {
            sql = sql + "  FORMAT(fd.RecordDate,'MMM dd yyyy hh:mm:ss tt', 'en-us') RecordDate,";
        }
        else
        {
            sql = sql + "  Convert(nvarchar,fd.RecordDate,100) RecordDate,";
        }
        //sql = sql + "  fd.LoginName,asr.BranchId,asr.Classid,asr.MODForFeeDeposit,fd.AmountInWords,";
        //sql = sql + "  fd.FeeMonth as FeeMonth,fd.Class as Class,fd.section as section,RecievedAmount,RemainingAmount,CurrentAmount,fd.Cancel,Case When IsDisplay=1 then BranchName Else '' End BranchName from FeeDeposite fd";
        //sql = sql + "  Inner join AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "'," + Session["BranchCode"].ToString() + ") asr on asr.SrNo=fd.SrNo";
        //sql = sql + "  where fd.RecieptSrNo='" + Label1.Text + "' and fd.SessionName='" + Session["SessionName"].ToString() + "'";

        //if (oo.ReturnTag(sql, "Cancel").Trim() == "Y")
        //{
        //    Label26.Text = "CANCELLED";
        //    Label6.Text = "CANCELLED";
        //}

        sql = sql + "  fd.LoginName,asr.BranchId,asr.Classid,asr.MODForFeeDeposit,fd.AmountInWords,";
        sql = sql + "  fd.FeeMonth as FeeMonth,fd.Class as Class,fd.section as section,RecievedAmount,RemainingAmount,CurrentAmount,";
        sql = sql + "  Case when fd.Cancel is not null then 'CANCELLED' when fd.Status='Pending' then 'PENDING' Else 'PAID' END Cancel,";
        sql = sql + "  Case When IsDisplay=1 then BranchName Else '' End BranchName from FeeDeposite fd";
        sql = sql + "  Inner join AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "'," + Session["BranchCode"].ToString() + ") asr on asr.SrNo=fd.SrNo";
        sql = sql + "  where fd.RecieptSrNo='" + Label1.Text + "' and fd.SessionName='" + Session["SessionName"].ToString() + "'";

        lblStatus.Text = oo.ReturnTag(sql, "Cancel").Trim();
        lblStatus1.Text = oo.ReturnTag(sql, "Cancel").Trim();


        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@SrNo", oo.ReturnTag(sql, "SrNo")));
        param.Add(new SqlParameter("@RecieptSrNo", Label1.Text));
        param.Add(new SqlParameter("@Class", oo.ReturnTag(sql, "Class")));
        param.Add(new SqlParameter("@Classid", oo.ReturnTag(sql, "Classid")));
        param.Add(new SqlParameter("@Month", oo.ReturnTag(sql, "FeeMonth")));
        param.Add(new SqlParameter("@CardType", Session["CardType"].ToString()));
        param.Add(new SqlParameter("@AdmissionType", oo.ReturnTag(sql, "TypeOFAdmision")));
        param.Add(new SqlParameter("@Medium", oo.ReturnTag(sql, "Medium")));
        param.Add(new SqlParameter("@MOD", oo.ReturnTag(sql, "MODForFeeDeposit")));
        param.Add(new SqlParameter("@Branchid", oo.ReturnTag(sql, "BranchId")));
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));

        DataSet ds = new DataSet();
        ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("GetStudentFeeDataProc", param);

        GridView1.DataSource = ds;
        GridView1.DataBind();
        GridView2.DataSource = ds;
        GridView2.DataBind();

        try
        {
            Label lblFooterDate = (Label)GridView1.FooterRow.FindControl("lblFooterDate");
            Label lblUserName = (Label)GridView1.FooterRow.FindControl("lblUserName");

            Label lblFooterDate1 = (Label)GridView2.FooterRow.FindControl("lblFooterDate");
            Label lblUserName1 = (Label)GridView2.FooterRow.FindControl("lblUserName");

            lblFooterDate1.Text = lblFooterDate.Text = oo.ReturnTag(sql, "RecordDate");
            lblUserName1.Text = lblUserName.Text = oo.ReturnTag(sql, "LoginName");

        }
        catch (Exception) { }

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

            Label lblBranch = (Label)GridView1.HeaderRow.FindControl("lblBranch");
            lblBranch.Text = oo.ReturnTag(sql, "BranchName");

            Label Label90 = (Label)GridView2.HeaderRow.FindControl("Label90");
            Label90.Text = oo.ReturnTag(sql, "Class");

            Label lblBranch2 = (Label)GridView2.HeaderRow.FindControl("lblBranch");
            lblBranch2.Text = oo.ReturnTag(sql, "BranchName");
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


            BarcodeSymbology symbology = BarcodeSymbology.Code39NC;

            string text = Label24.Text;

            string scaleText = Label24.Text;
            int scale;
            if (!int.TryParse(scaleText, out scale))
            {
                if (symbology == BarcodeSymbology.CodeQr)
                {
                    scale = 3;
                }
                else
                {
                    scale = 1;
                }
            }
            else if (scale < 1)
            {
                scale = 1;
            }

            //if (!string.IsNullOrEmpty(text) && symbology != BarcodeSymbology.Unknown)
            //{
            //    lblBarCode.BarcodeEncoding = symbology;
            //    lblBarCode.Scale = scale;
            //    lblBarCode.Text = text;
            //}

            Label Label240 = (Label)GridView2.HeaderRow.FindControl("Label240");
            Label240.Text = oo.ReturnTag(sql, "SrNo");

            text = Label240.Text;
            scaleText = Label240.Text;

            if (!int.TryParse(scaleText, out scale))
            {
                if (symbology == BarcodeSymbology.CodeQr)
                {
                    scale = 3;
                }
                else
                {
                    scale = 1;
                }
            }
            else if (scale < 1)
            {
                scale = 1;
            }
            //if (!string.IsNullOrEmpty(text) && symbology != BarcodeSymbology.Unknown)
            //{
            //    lblBarCodesch.BarcodeEncoding = symbology;
            //    lblBarCodesch.Scale = scale;
            //    lblBarCodesch.Text = text;
            //}
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
            Label8.Text = oo.ReturnTag(sql, "StudentName");

            Label Label80 = (Label)GridView2.HeaderRow.FindControl("Label80");
            Label80.Text = oo.ReturnTag(sql, "StudentName");
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

            Label Label7 = (Label)GridView1.HeaderRow.FindControl("Label7");
            Label7.Text = Label1.Text;

            Label Label70 = (Label)GridView2.HeaderRow.FindControl("Label70");
            Label70.Text = Label1.Text;
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
            Label29.Text = oo.ReturnTag(sql, "MOP");

            Label Label290 = (Label)GridView2.HeaderRow.FindControl("Label290");
            Label290.Text = oo.ReturnTag(sql, "MOP");

        }
        catch (Exception) { }
        double value = 0;
        string Namt;
        try
        {
            Namt = Session["NextDueAmt"].ToString();
            value = double.TryParse(Namt, out value) ? value : 0;
            Label lblNexDueAmt = (Label)GridView1.FooterRow.FindControl("lblNexDueAmt");
            Label lblNexDueAmt1 = (Label)GridView2.FooterRow.FindControl("lblNexDueAmt");
            lblNexDueAmt1.Text = lblNexDueAmt.Text = value.ToString("N", new CultureInfo("en-In"));
        }
        catch (Exception) { }


        try
        {

            Label lblCurrentAmount1 = (Label)GridView1.FooterRow.FindControl("Label14");
            Label lblCurrentAmount2 = (Label)GridView2.FooterRow.FindControl("Label14");
            value = double.TryParse(oo.ReturnTag(sql, "RecievedAmount"), out value) ? value : 0;
            lblCurrentAmount2.Text = lblCurrentAmount1.Text = value.ToString("N", new CultureInfo("en-In"));

        }
        catch (Exception) { }

        try
        {

            Label lblRecievedAmount1 = (Label)GridView1.FooterRow.FindControl("Label18");
            Label lblRecievedAmount2 = (Label)GridView2.FooterRow.FindControl("Label180");
            value = double.TryParse(oo.ReturnTag(sql, "CurrentAmount"), out value) ? value : 0;
            lblRecievedAmount2.Text = lblRecievedAmount1.Text = value.ToString("N", new CultureInfo("en-In"));
        }
        catch (Exception) { }

        try
        {

            Label lblRemainingAmount1 = (Label)GridView1.FooterRow.FindControl("Label30");
            Label lblRemainingAmount2 = (Label)GridView2.FooterRow.FindControl("Label30");
            value = double.TryParse(oo.ReturnTag(sql, "RemainingAmount"), out value) ? value : 0;
            lblRemainingAmount1.Text = lblRemainingAmount2.Text = value.ToString("N", new CultureInfo("en-In"));
        }
        catch (Exception) { }

    }

    protected void LinkButton4_Click(object sender, EventArgs e)
    {
       
            if (Session["FD"].ToString() == "FD")
            {
                Response.Redirect("fee_deposit.aspx");
            }
            else if (Session["FD"].ToString() == "TFD")
            {
                Response.Redirect("TransportFeeDeposit.aspx");
                
            }
            else
            {
                Response.Redirect("FeeDepositBalance.aspx");

            }

        
       
    }

}

