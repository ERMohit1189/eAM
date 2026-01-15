using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class sp_userControl_stfeeataGlance : System.Web.UI.UserControl
{
    SqlConnection con;
    string sql = "";
    SqlDataAdapter da;
    DataTable dt;
    SqlCommand cmd;

    protected void Page_Load(object sender, EventArgs e)
    {
        con = new SqlConnection();
        con = BAL.objBal.dbGet_connection();
        try
        {
            DuesforSingleSrNo();
        }
        catch
        {
        }

        try
        {
            LastFeePaid();
        }
        catch
        {
        }
    }


    public void DuesforSingleSrNo()
    {
        string Current_MonthName = "";
        string sql = "";
        string sql1 = "";

        sql = "Select ClassId,SrNo,ClassName,SectionName,Medium,TypeOFAdmision,Card,BranchId,Cardid from";
        sql = sql + " AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "'," + Session["BranchCode"].ToString() + ") where SrNo='" + Session["Srno"].ToString() + "'";

        string classId = BAL.objBal.ReturnTag(sql, "classId");
        string branchId = BAL.objBal.ReturnTag(sql, "BranchId");
        string cardId = BAL.objBal.ReturnTag(sql, "Cardid");

        Label std_srno = new Label();
        std_srno.Text = BAL.objBal.ReturnTag(sql, "srno");
        Label Class = new Label();
        Class.Text = BAL.objBal.ReturnTag(sql, "ClassName");
        Label Section = new Label();
        Section.Text = BAL.objBal.ReturnTag(sql, "SectionName");
        Label Medium = new Label();
        Medium.Text = BAL.objBal.ReturnTag(sql, "Medium");
        Label StdType = new Label();
        StdType.Text = BAL.objBal.ReturnTag(sql, "TypeOFAdmision");
        Label lblCardType = new Label();
        lblCardType.Text = BAL.objBal.ReturnTag(sql, "Card");

        Label lblTotal = new Label();
        Label lblArrear = new Label();
        Label lblConvence = new Label();
        Label lbltotalValue = new Label();

        sql = "Select ArrearAmt from ArrierMast where srno='" + std_srno.Text + "' and BranchCode = " + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "'";
        if (BAL.objBal.ReturnTag(sql, "ArrearAmt") != "")
        {
            double arrear = Convert.ToDouble(BAL.objBal.ReturnTag(sql, "ArrearAmt"));
            lblArrear.Text = (Math.Round(arrear, 0)).ToString();
        }
        else
        {
            lblArrear.Text = "0";
        }

        con.Open();

        sql = "select ROW_NUMBER() over(order By MonthId) as Sr,MonthName,DateName(MM,DueDate) DueMonth from MonthMaster mm";
        sql = sql + " inner join FeeGroupMaster fgm on fgm.FeeGroupName='" + lblCardType.Text + "' and mm.SessionName=fgm.SessionName";
        sql = sql + " where fgm.FeeGroupName='" + lblCardType.Text + "' and fgm.BranchCode = " + Session["BranchCode"] + " and mm.BranchCode = " + Session["BranchCode"] + " and mm.SessionName='" + Session["SessionName"].ToString() + "' and Datepart(MM,DueDate) is not null";
        sql = sql + " and ClassId='" + classId + "'";

        DataTable dt_monthNames = new DataTable();
        dt_monthNames.Columns.Add("SR");
        dt_monthNames.Columns.Add("MonthName");
        dt_monthNames.Columns.Add("DueMonth");

        SqlCommand dt_monthNames_fill = new SqlCommand(sql, con);
        SqlDataReader dt_monthNames_fill_dr = dt_monthNames_fill.ExecuteReader();
        while (dt_monthNames_fill_dr.Read())
        {
            dt_monthNames.Rows.Add(dt_monthNames_fill_dr["Sr"].ToString(), dt_monthNames_fill_dr["MonthName"].ToString(), dt_monthNames_fill_dr["DueMonth"].ToString());

        }
        dt_monthNames_fill_dr.Close();

        con.Close();
        con.Open();

        sql = "select top 1 FeeMonth from FeeDeposite where SrNo='" + std_srno.Text + "' and BranchCode = " + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "'";
        sql = sql + " and Cancel is NULL and FeeMonth not like '(T)%' order By Id Desc";

        int fullpaid = 0;
        if (BAL.objBal.ReturnTag(sql, "FeeMonth") != "Yearly")
        {
            string Last_fee_deposited_monthName = BAL.objBal.ReturnTag(sql, "FeeMonth");
            string currentmonth = "";
            currentmonth = "Select Top 1 MonthId,MonthName from MonthMaster where ClassId='" + classId + "' and BranchCode = " + Session["BranchCode"] + "";
            currentmonth = currentmonth + " and Datepart(MM,DueDate)<=Datepart(MM,GETDATE()) and Datepart(YYYY,DueDate)=Datepart(YYYY,GETDATE())";
            currentmonth = currentmonth + " Order by Monthid Desc";

            Current_MonthName = BAL.objBal.ReturnTag(currentmonth, "MonthName");

            int Last_fee_deposited_monthName_number = 0;
            int Current_MonthName_number = 0;
            for (int j = 0; j < dt_monthNames.Rows.Count; j++)
            {
                if (Last_fee_deposited_monthName.ToUpper() == dt_monthNames.Rows[j]["MonthName"].ToString().ToUpper())
                {
                    Last_fee_deposited_monthName_number = Convert.ToInt32(dt_monthNames.Rows[j]["SR"].ToString());
                }

                if (Current_MonthName.ToUpper() == dt_monthNames.Rows[j]["DueMonth"].ToString().ToUpper())
                {
                    Current_MonthName_number = Convert.ToInt32(dt_monthNames.Rows[j]["SR"].ToString());

                }
            }

            int startingfrom = 0;
         
            if (Current_MonthName_number <= Last_fee_deposited_monthName_number)
            {
                if (dt_monthNames.Rows.Count > Last_fee_deposited_monthName_number)
                {
                    startingfrom = Last_fee_deposited_monthName_number;
                    Current_MonthName_number = Last_fee_deposited_monthName_number + 1;
                }
                else
                {
                    fullpaid = 1;
                }
            }

            if (Current_MonthName_number > Last_fee_deposited_monthName_number)
            {
                if (Last_fee_deposited_monthName == "")
                {

                    for (int k = startingfrom; k <= Current_MonthName_number - 1; k++)
                    {
                        sql1 = "select SUM(FeePayment) as Sum from FeeAllotedForClassWise where SessionName='" + Session["SessionName"].ToString() + "'";
                        sql1 = sql1 + " and Month='" + dt_monthNames.Rows[k]["MonthName"].ToString() + "' and Class='" + classId + "' and CardType='" + lblCardType.Text + "'";
                        sql1 = sql1 + " and AdmissionType='" + StdType.Text + "' and BranchCode = " + Session["BranchCode"] + " and Medium='" + Medium.Text + "' and Branchid='" + branchId + "'";


                        sql = "Select discountValue from DiscountMaster where SrNo='" + std_srno.Text + "' and BranchCode = " + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "'";
                        string[] discount = BAL.objBal.ReturnTag(sql, "discountValue").Split(' ');
                        string sql2 = "";

                        sql2 = "Select ForMonth NoOfmonths from  MonthMaster where MonthName='" + dt_monthNames.Rows[k]["MonthName"].ToString() + "' and BranchCode = " + Session["BranchCode"] + "";
                        sql2 = sql2 + " and ClassId='" + classId + "' and SessionName='" + Session["SessionName"].ToString() + "'";


                        double amount;
                        if (discount.Length > Current_MonthName_number)
                        {
                            try
                            {
                                double.TryParse(lblTotal.Text, out amount);
                                lblTotal.Text = (amount + (Convert.ToDouble(BAL.objBal.ReturnTag(sql1, "Sum")) - (Convert.ToDouble(discount[k])))).ToString();
                            }
                            catch
                            {
                            }

                        }
                        else
                        {
                            double.TryParse(lblTotal.Text, out amount);
                            lblTotal.Text = (amount + Convert.ToDouble(BAL.objBal.ReturnTag(sql1, "Sum"))).ToString();
                        }

                        sql = "select TransportRequired from StudentOfficialDetails where SrNo='" + std_srno.Text + "' and BranchCode = " + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "'";
                        if (BAL.objBal.ReturnTag(sql, "TransportRequired") == "Yes")
                        {
                            sql1 = "Select SUM(Amount) Transportamount from StudentVehicleAllotment where SrNo='" + std_srno.Text + "' ";
                            sql1 = sql1 + " and Insttalment='" + dt_monthNames.Rows[k - 1]["MonthName"].ToString() + "' and BranchCode = " + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "' and MonthStatus='1'";

                            double noofmonth = Convert.ToDouble(BAL.objBal.ReturnTag(sql2, "NoOfmonths"));
                            double.TryParse(lblConvence.Text, out amount);
                            lblConvence.Text = (amount + (Convert.ToDouble(BAL.objBal.ReturnTag(sql1, "Transportamount")) * noofmonth)).ToString();
                        }
                    }

                }
                else
                {
                    for (int k = Last_fee_deposited_monthName_number + 1; k <= Current_MonthName_number; k++)
                    {

                        sql1 = "select SUM(FeePayment) as Sum from FeeAllotedForClassWise where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode = " + Session["BranchCode"] + "";
                        sql1 = sql1 + " and Month='" + dt_monthNames.Rows[k - 1]["MonthName"].ToString() + "' and BranchCode = " + Session["BranchCode"] + " and Class='" + classId + "' and CardType='" + lblCardType.Text + "'";
                        sql1 = sql1 + " and AdmissionType='" + StdType.Text + "' and Medium='" + Medium.Text + "' and Branchid='" + branchId + "'";

                        sql = "Select discountValue from DiscountMaster where SrNo='" + std_srno.Text + "' and BranchCode = " + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "'";
                        string[] discount = BAL.objBal.ReturnTag(sql, "discountValue").Split(' ');
                        string sql2 = "";

                        sql2 = "Select ForMonth NoOfmonths from  MonthMaster where MonthName='" + dt_monthNames.Rows[k - 1]["MonthName"].ToString() + "' and BranchCode = " + Session["BranchCode"] + "";
                        sql2 = sql2 + " and ClassId='" + classId + "' and SessionName='" + Session["SessionName"].ToString() + "'";

                        double amount;
                        if (discount.Length > Current_MonthName_number)
                        {
                            try
                            {
                                double.TryParse(lblTotal.Text, out amount);
                                lblTotal.Text = (amount + (Convert.ToDouble(BAL.objBal.ReturnTag(sql1, "Sum")) - (Convert.ToDouble(discount[k - 1])))).ToString();
                            }
                            catch
                            {
                            }

                        }
                        else
                        {
                            if (BAL.objBal.ReturnTag(sql1, "Sum") != "")
                            {
                                double.TryParse(lblTotal.Text, out amount);
                                lblTotal.Text = (amount + Convert.ToDouble(BAL.objBal.ReturnTag(sql1, "Sum"))).ToString();
                            }
                        }
                        sql = "select TransportRequired from StudentOfficialDetails where SrNo='" + std_srno.Text + "' and BranchCode = " + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "'";
                        if (BAL.objBal.ReturnTag(sql, "TransportRequired") == "Yes")
                        {
                            sql1 = "Select SUM(Amount) Transportamount from StudentVehicleAllotment where SrNo='" + std_srno.Text + "' ";
                            sql1 = sql1 + " and Insttalment='" + dt_monthNames.Rows[k - 1]["MonthName"].ToString() + "' and BranchCode = " + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "' and MonthStatus='1'";

                            double noofmonth = Convert.ToDouble(BAL.objBal.ReturnTag(sql2, "NoOfmonths"));
                            double.TryParse(lblConvence.Text, out amount);
                            lblConvence.Text = (amount + (Convert.ToDouble(BAL.objBal.ReturnTag(sql1, "Transportamount")) * noofmonth)).ToString();
                        }
                    }
                }
            }
        }
        con.Close();
        double totalamount;
        double convenceamount;
        double.TryParse(lblTotal.Text, out totalamount); double.TryParse(lblConvence.Text, out convenceamount);
        if (fullpaid==0)
        {
            lblFeeDue.Text = (totalamount + convenceamount).ToString();
            CalculateFine((totalamount + convenceamount), Current_MonthName, classId, lblCardType.Text, cardId, std_srno.Text);
        }
        else
        {
            lblFeeDue.Text = "NILL";
        }
    }

    public void CalculateFine(double totalamount, string feemonth, string classid, string cardtype, string cardId, string srno)
    {
        sql = "Select Count(*) as Count from SessionMaster where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode = " + Session["BranchCode"] + " and ToDate< Convert(nvarchar(11),GETDATE(),106)";
        int count = 0;
        count = Convert.ToInt16(BAL.objBal.ReturnTag(sql, "Count"));

        double totalAmount = 0;
        Label lblSrno = new Label();
        lblSrno.Text = srno;
        if (count <= 0)
        {
            totalAmount = totalAmount + CalculateInSameSession(lblSrno.Text, feemonth, classid, cardtype, cardId);
        }
        else
        {
            totalAmount = totalAmount + CalculateInnextSession(lblSrno.Text, feemonth);
        }
        Label lblFine = new Label();
        lblFine.Text = totalAmount.ToString();

        double fineamount;
        double.TryParse(lblFine.Text, out fineamount);
        lblFeeDue.Text = (totalamount + fineamount).ToString("N", new CultureInfo("en-In")) + " /-"; 
    }

    public double CalculateInSameSession(string lblSrno, string feemonth, string classid, string cardtype, string cardId)
    {
        double totalAmount = 0;
        try
        {
            sql = "Select *from FeeDeposite fd where fd.Cancel is null and fd.SessionName='" + Session["SessionName"].ToString() + "' and BranchCode = " + Session["BranchCode"] + " and fd.SrNo='" + lblSrno + "' and ";
            sql = sql + " (FeeMonth='" + feemonth + "'  or FeeMonth='Yearly')";

            if (BAL.objBal.Duplicate(sql) == false)
            {
                int a = 0, b = 0;

                double fineValue = 0;
                sql = "select DAY(getdate()) as DayValue";
                int dv = 0;
                dv = Convert.ToInt32(BAL.objBal.ReturnTag(sql, "DayValue"));

                sql = "Select DATENAME(MONTH,GETDATE()) as MonthName";
                string selectedmonth1 = BAL.objBal.ReturnTag(sql, "MonthName");

                sql = "Select MonthId from MonthMaster where ISNULL(dueMonth,DAteName(MOnth,DueDate))='" + selectedmonth1 + "' and  (ClassId='" + classid + "' or ClassId is null)";
                sql = sql + " and (CardType='" + cardId + "' or CardType='" + cardtype + "') and BranchCode = " + Session["BranchCode"] + "  and SessionName='" + Session["SessionName"].ToString() + "'";
                int id3 = Convert.ToInt32(BAL.objBal.ReturnTag(sql, "MonthId"));



                sql = "Select Top 1 mm.MonthId from FeeDeposite fd inner join MonthMaster mm on mm.MonthName=fd.FeeMonth";
                sql = sql + " and (ClassId='" + classid + "' or ClassId is null)  and (CardType='" + cardId + "'";
                sql = sql + " or CardType='" + cardtype + "') and fd.SessionName='" + Session["SessionName"].ToString() + "'";
                sql = sql + " and mm.SessionName='" + Session["SessionName"].ToString() + "' and mm.BranchCode = " + Session["BranchCode"] + " and SrNo='" + lblSrno + "' order by MonthId desc";
                int id1 = 0;

                if (BAL.objBal.ReturnTag(sql, "MonthId") != "")
                {
                    id1 = Convert.ToInt32(BAL.objBal.ReturnTag(sql, "MonthId"));
                }
                else
                {
                    sql = "Select Top 1 MonthId from MonthMaster where SessionName='" + Session["SessionName"].ToString() + "'";
                    sql = sql + " and (ClassId='" + classid + "' or ClassId is null) and BranchCode = " + Session["BranchCode"] + "  and (CardType='" + cardId + "' or CardType='" + cardtype + "')";
                    if (!string.IsNullOrEmpty(BAL.objBal.ReturnTag(sql, "MonthId")))
                    {
                        id1 = Convert.ToInt32(BAL.objBal.ReturnTag(sql, "MonthId"));
                    }
                }
                string selectedmonth = feemonth;


                sql = "Select MonthId from MonthMaster where ISNULL(dueMonth,DAteName(MOnth,DueDate))='" + selectedmonth + "' and SessionName='" + Session["SessionName"].ToString() + "'";
                sql = sql + " and (ClassId='" + classid + "' or ClassId is null) and BranchCode = " + Session["BranchCode"] + "  and (CardType='" + cardId + "' or CardType='" + cardtype + "')";
                int id2 = Convert.ToInt32(BAL.objBal.ReturnTag(sql, "MonthId"));

                sql = "Select *from FeeDeposite fd inner join MonthMaster mm on mm.MonthName=fd.FeeMonth";
                sql = sql + " and (ClassId='" + classid + "' or ClassId is null)  and fd.BranchCode = " + Session["BranchCode"] + " and (CardType='" + cardId + "' or CardType='" + cardtype + "')";
                sql = sql + " and fd.SessionName='" + Session["SessionName"].ToString() + "' and mm.SessionName='" + Session["SessionName"].ToString() + "'";
                sql = sql + " and SrNo='" + lblSrno + "' order by MonthId desc";

                if (BAL.objBal.Duplicate(sql))
                {
                    sql = "Select MonthName from MonthMaster where MonthId>'" + id1 + "' and MonthId<='" + id2 + "' and (ClassId='" + classid + "' or ClassId is null)";
                    sql = sql + " and (CardType='" + cardId + "' or CardType='" + cardtype + "') and fd.BranchCode = " + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "'";
                }
                else
                {
                    sql = "Select MonthName from MonthMaster where MonthId>='" + id1 + "' and MonthId<='" + id2 + "' and (ClassId='" + classid + "' or ClassId is null)";
                    sql = sql + " and (CardType='" + cardId + "' or CardType='" + cardtype + "') and fd.BranchCode = " + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "'";
                }

                da = new SqlDataAdapter(sql, con);
                dt = new DataTable();
                da.Fill(dt);

                for (int j = 0; j < dt.Rows.Count; j++)
                {

                    sql = "Select MonthId from MonthMaster where ISNULL(dueMonth,DAteName(MOnth,DueDate))='" + dt.Rows[j][0].ToString() + "'";
                    sql = sql + " and (ClassId='" + classid + "' or ClassId is null)  and (CardType='" + cardId + "' or CardType='" + cardtype + "')";
                    sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode = " + Session["BranchCode"] + "";

                    int id4 = Convert.ToInt32(BAL.objBal.ReturnTag(sql, "MonthId"));

                    sql = "Select MonthName from MonthMaster where MonthId>='" + id4 + "' and MonthId<='" + id3 + "' and (ClassId='" + classid + "' or ClassId is null)";
                    sql = sql + " and (CardType='" + cardId + "' or CardType='" + cardtype + "') and BranchCode = " + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "'";
                    da = new SqlDataAdapter(sql, con);
                    DataTable dt1 = new DataTable();
                    da.Fill(dt1);
                    for (int j1 = 0; j1 < dt1.Rows.Count; j1++)
                    {
                        sql = "Select MonthId from MonthMaster where ISNULL(dueMonth,DAteName(MOnth,DueDate))='" + dt1.Rows[j1][0].ToString() + "'";
                        sql = sql + " and (ClassId='" + classid + "' or ClassId is null) and BranchCode = " + Session["BranchCode"] + "  and (CardType='" + cardId + "' or CardType='" + cardtype + "')";
                        sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "'";
                        int id5 = Convert.ToInt32(BAL.objBal.ReturnTag(sql, "MonthId"));

                        if (id5 < id3)
                        {
                            sql = "Select Distinct MonthAmount from RangeBasisFineMaster where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode = " + Session["BranchCode"] + "";
                            if (!string.IsNullOrEmpty(BAL.objBal.ReturnTag(sql, "MonthAmount")))
                            {
                                double fineamount = Convert.ToDouble(BAL.objBal.ReturnTag(sql, "MonthAmount"));
                                totalAmount = totalAmount + fineamount;
                            }
                        }

                        if (id5 == id3)
                        {
                            sql = "select * from RangeBasisFineMaster ";
                            sql = sql + " where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
                            cmd = new SqlCommand();
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
                            catch (SqlException ee)
                            {
                                con.Close();
                            }
                            totalAmount = totalAmount + fineValue;
                        }

                    }
                }
            }
        }
        catch
        {
        }
        return totalAmount;
    }

    public double CalculateInnextSession(string lblSrno, string feemonth)
    {
        double fineValue = 0;
        double totalAmount = 0;

        sql = "Select *from FeeDeposite fd where fd.Cancel is null and fd.SessionName='" + Session["SessionName"].ToString() + "' and fd.SrNo='" + lblSrno + "' and ";
        sql = sql + " (FeeMonth='" + feemonth + "'  or FeeMonth='Yearly')";

        if (BAL.objBal.Duplicate(sql) == false)
        {
            sql = "Select MonthName from StudentOfficialDetails sd";
            sql = sql + " inner join MonthMaster mm on mm.ClassId=sd.AdmissionForClassId and mm.SessionName=sd.SessionName";
            sql = sql + " where sd.SrNo='" + lblSrno + "' and mm.BranchCode = " + Session["BranchCode"] + " and sd.BranchCode = " + Session["BranchCode"] + " and sd.SessionName='" + Session["SessionName"].ToString() + "'";
            sql = sql + " Except";
            sql = sql + " Select FeeMonth from StudentOfficialDetails sd";
            sql = sql + " inner join FeeDeposite fd on fd.SrNo=sd.SrNo and fd.SessionName=sd.SessionName";
            sql = sql + " inner join MonthMaster mm on mm.ClassId=sd.AdmissionForClassId and mm.MonthName=fd.FeeMonth and mm.SessionName=sd.SessionName";
            sql = sql + " where sd.SrNo='" + lblSrno + "' and sd.BranchCode = " + Session["BranchCode"] + " and fd.BranchCode = " + Session["BranchCode"] + " and mm.BranchCode = " + Session["BranchCode"] + " and sd.SessionName='" + Session["SessionName"].ToString() + "'";


            da = new SqlDataAdapter(sql, con);
            DataTable dt2 = new DataTable();
            da.Fill(dt2);
            for (int j = 0; j < (dt2.Rows.Count + CalculateInnextSessionMonth(lblSrno)); j++)
            {
                for (int k = j; k < (dt2.Rows.Count + CalculateInnextSessionMonth(lblSrno)); k++)
                {
                    sql = "Select Distinct MonthAmount from RangeBasisFineMaster where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode = " + Session["BranchCode"] + "";
                    double fineamount = double.TryParse(BAL.objBal.ReturnTag(sql, "MonthAmount"), out fineamount) ? fineamount : 0;
                    totalAmount = totalAmount + fineamount;
                }
            }

            totalAmount = totalAmount + fineValue;

        }


        return totalAmount;
    }

    public int CalculateInnextSessionMonth(string lblSrno)
    {
        sql = "Select AdmissionForClassId,card from StudentOfficialDetails where SrNo='" + lblSrno + "' and BranchCode = " + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "'";
        string classid = BAL.objBal.ReturnTag(sql, "AdmissionForClassId");
        string card = BAL.objBal.ReturnTag(sql, "card");

        sql = "select DATENAME(mm,getdate()) as Month";
        string month = BAL.objBal.ReturnTag(sql, "Month");

        sql = "select MonthName,MonthId from MonthMaster where (ClassId='" + classid + "' or ClassId is null) and BranchCode = " + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "'and MonthId<=";
        sql = sql + " (select MonthId from MonthMaster mm inner join FeeGroupMaster fgm on fgm.id=mm.CardType and fgm.SessionName=mm.SessionName and fgm.BranchCode = " + Session["BranchCode"] + " and mm.BranchCode = " + Session["BranchCode"] + "";
        sql = sql + " where (ClassId='" + classid + "' or ClassId is null) and ISNULL(DueMonth,DATENAME(mm,DueDate))='" + month + "' and mm.SessionName='" + Session["SessionName"].ToString() + "'";
        sql = sql + " and fgm.FeeGroupName='" + card + "')";
        DataSet ds = new DataSet();
        ds = BAL.objBal.GridFill(sql);
        DataTable dt = new DataTable();
        dt = ds.Tables[0];
        return dt.Rows.Count;
    }

    public void LastFeePaid()
    {
        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@Srno", Session["Srno"].ToString()));
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));

        DataSet ds = new DataSet();
        ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("StudentFeeAtaGlance_Proc", param);

        if (ds != null)
        {
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                lblLastPaid.Text = dt.Rows[0][0].ToString();
                double amount;
                double.TryParse(dt.Rows[0][1].ToString(),out amount);
                lblAmount.Text = amount.ToString("N", new CultureInfo("en-In"));
                lblMOP.Text = dt.Rows[0][2].ToString();
                lblStatus.Text = dt.Rows[0][3].ToString();
                lblLastPaidDay.Text = dt.Rows[0][4].ToString();
            }
        }
        if (lblAmount.Text == "")
        {
            divlastfeepaid.Visible = false;
        }
        else
        {
            divlastfeepaid.Visible = true;
        }
        //StudentFeeAtaGlance_Proc
    }
}