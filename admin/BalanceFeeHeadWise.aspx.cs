using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;

public partial class admin_BalanceFeeRemainder : Page
{
    string sql = "";
    Campus oo = new Campus();
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    SqlDataAdapter da;
    DataTable dt;

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }

        if (!IsPostBack)
        {
            abc.Visible = false;
            loadfeeGroup();

            BLL.BLLInstance.loadCourse(drpCourse, Session["SessionName"].ToString());
            loadClass();
            oo.fillSelectvalue(drpBranch, "<--Select-->");
            oo.fillSelectvalue(drpStream, "<--Select-->", "-1");
            loadGridMonth();
            loadSection();
        }              
    }
    public void loadClass()
    {
        if (drpCourse.SelectedIndex == 0)
        {
            sql = "Select ClassName,Id from ClassMaster where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"] + " Order by CidOrder";
            oo.FillDropDown_withValue(sql, drpClass, "ClassName", "Id");
            drpClass.Items.Insert(0, "<--Select-->");
        }
        else
        {
            sql = "Select ClassName,Id from ClassMaster where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"] + " and Course='" + drpCourse.SelectedValue.ToString() + "' Order by CidOrder";
            oo.FillDropDown_withValue(sql, drpClass, "ClassName", "Id");
            drpClass.Items.Insert(0, "<--Select-->");
        }
    }
    public void loadBranch()
    {
        sql = "select BranchName,Id from BranchMaster";
        sql = sql + " where ClassId='" + drpClass.SelectedValue.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        oo.FillDropDown_withValue(sql, drpBranch, "BranchName", "Id");
        oo.fillSelectvalue(drpBranch, "<--Select-->");
    }
    public void loadStream()
    {
        BLL.BLLInstance.loadStream(drpStream,drpClass.SelectedValue.ToString(),drpBranch.SelectedValue.ToString(), Session["SessionName"].ToString());
    }
    public void loadSection()
    {
        sql = "Select SectionName,Id from SectionMaster where ClassNameId='" + drpClass.SelectedValue.ToString() + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "'";
        oo.FillDropDown_withValue(sql, drpSection, "SectionName", "Id");
        drpSection.Items.Insert(0, "<--Select-->");
    }
    public void loadfeeGroup()
    {
        sql = "Select FeeGroupName,Id from FeeGroupMaster where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"] + "";
        oo.FillDropDown_withValue(sql, drpFeeGroup, "FeeGroupName", "Id");
    }
    public void loadGridMonth()
    {
        sql = "select MonthName,mm.MonthId from MonthMaster mm";
        sql = sql + " inner join FeeGroupMaster fgm on (Case when ISNUMERIC(CardType)=1 THEN fgm.Id Else fgm.FeeGroupName End)=mm.CardType and fgm.SessionName=mm.SessionName";
        sql = sql + " inner join ClassMaster cm on cm.Id=mm.ClassId and cm.SessionName=mm.SessionName";
        sql = sql + " where fgm.FeeGroupName='" + drpFeeGroup.SelectedItem.ToString() + "' and cm.ClassName='" + drpClass.SelectedItem.Text.ToString() + "' and (MOD='I' or MOD is NULL)  and mm.SessionName='" + Session["SessionName"].ToString() + "' ";
        sql = sql + " and cm.BranchCode=" + Session["BranchCode"] + " and fgm.BranchCode=" + Session["BranchCode"] + " and mm.BranchCode=" + Session["BranchCode"].ToString() + " or monthid=0 order by MonthId";
        
        oo.FillDropDown_withValue(sql, drpInstallment, "MonthName", "MonthId");
        drpInstallment.Items.Insert(0, "<--Select-->");
    }
    protected void ChkAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkall = (CheckBox)GridView1.HeaderRow.FindControl("ChkAll");
        if (chkall.Checked)
        {
            foreach (GridViewRow gvr in GridView1.Rows)
            {
                CheckBox chk = (CheckBox)gvr.FindControl("Chk");
                chk.Checked = true;
            }
        }
        else
        {
            foreach (GridViewRow gvr in GridView1.Rows)
            {
                CheckBox chk = (CheckBox)gvr.FindControl("Chk");
                chk.Checked = false;
            }
        }
    } 
    public void Value()
    {
        sql = "select Row_Number() over (order by SG.Id Asc) as SNo,SG.Id, SC.SectionName,SO.Card,SO.Medium as Medium,SO.TypeOFAdmision,CM.ClassName,convert(nvarchar,So.DateOfAdmiission,106) as DateOfAdmiission ,SO.SectionId,Sf.FatherName,SF.MotherName,SF.FamilyContactNo,SG.FirstName,SG.MiddleName,SG.LastName,sg.StEnRCode as StEnRCode,sg.srno  as srno,case  when so.TransportRequired='Yes' then 'Yes' else 'No' end as TransportRequired,so.wayamount as wayamount from StudentGenaralDetail SG ";
        sql = sql + "   left join StudentFamilyDetails SF on SG.Srno=SF.Srno";
        sql = sql + "   left join StudentOfficialDetails SO on SG.Srno=SO.Srno";
        sql = sql + "   left join ClassMaster CM on SO.AdmissionForClassId=CM.Id";
        sql = sql + "   left join SectionMaster SC on SO.SectionId=SC.Id";
        sql = sql + "    where CM.ClassName='" + drpClass.SelectedItem.ToString() + "'   and SC.SectionName='" + drpSection.SelectedItem.ToString() + "'";
        sql = sql + " and  sg.SessionName='" + Session["SessionName"].ToString() + "' and ";
        sql = sql + " so.SessionName='" + Session["SessionName"].ToString() + "' and sf.SessionName='" + Session["SessionName"].ToString() + "' and cm.SessionName='" + Session["SessionName"].ToString() + "'";
        sql = sql + " and SC.SessionName='" + Session["SessionName"].ToString() + "'  and";
        sql = sql + " and sc.BranchCode=" + Session["BranchCode"] + " and sf.BranchCode=" + Session["BranchCode"] + " and so.BranchCode=" + Session["BranchCode"] + " and cm.BranchCode=" + Session["BranchCode"] + " sg.BranchCode=" + Session["BranchCode"].ToString() + "";
        sql = sql + " and SO.Withdrwal is null";
        GridView1.DataSource = oo.GridFill(sql);
        GridView1.DataBind();

        for (int i = 0; i < GridView1.Rows.Count; i++)
        {

            Label std_srno = (Label)GridView1.Rows[i].FindControl("lblsrno");
            Label Class = (Label)GridView1.Rows[i].FindControl("Class");
            Label Section = (Label)GridView1.Rows[i].FindControl("Section");
            Label Medium = (Label)GridView1.Rows[i].FindControl("Medium");
            Label StdType = (Label)GridView1.Rows[i].FindControl("StdType");
            Label lblCardType = (Label)GridView1.Rows[i].FindControl("lblCardType");
            Label lblTotal = (Label)GridView1.Rows[i].FindControl("lblTotal");
            Label lblConvence = (Label)GridView1.Rows[i].FindControl("lblConvence");
            Label lbltotalValue = (Label)GridView1.Rows[i].FindControl("lbltotalValue");
            DataTable dt_Month = new DataTable();
            dt_Month.Columns.Add("MonthName");
            sql = "select DISTINCT (Month) as Month  from FeeAllotedForClassWise where Class='" + Class.Text + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "' and CardType='" + lblCardType.Text + "' and AdmissionType='" + StdType.Text + "' and Medium='" + Medium.Text + "'";
            SqlCommand MonthNames = new SqlCommand(sql, con);
            con.Open();
            SqlDataReader Month_Names_dr = MonthNames.ExecuteReader();
            while (Month_Names_dr.Read())
            {
                dt_Month.Rows.Add(Month_Names_dr["Month"].ToString());
            }
            Month_Names_dr.Close();
            DataTable dt_StudentFeeSubmit = new DataTable();
            dt_StudentFeeSubmit.Columns.Add("SubmittedMonths");
            sql = "select FeeMonth from FeeDeposite where SrNo='" + std_srno.Text + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "' ";
            SqlCommand SubmittedMonthsName = new SqlCommand(sql, con);
            SqlDataReader SubmittedMonthsName_dr = SubmittedMonthsName.ExecuteReader();
            while (SubmittedMonthsName_dr.Read())
            {
                dt_StudentFeeSubmit.Rows.Add(SubmittedMonthsName_dr["FeeMonth"].ToString());
            }
            SubmittedMonthsName_dr.Close();
            con.Close();

            double TotalFees = 0;
            sql = "select * from FeeDeposite where SrNo='" + std_srno.Text + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "'";
            DataTable MonthNamesFeNotSubmitted = new DataTable();
            MonthNamesFeNotSubmitted.Columns.Add("MonthsNames");
            if (oo.ReturnTag(sql, "FeeMonth").ToString() != "Yearly" && oo.ReturnTag(sql, "FeeMonth").ToString() != "New")
            {

                for (int j = 0; j < dt_Month.Rows.Count; j++)
                {
                    int MonthNameMathcing = 0;
                    if (dt_StudentFeeSubmit.Rows.Count != 0)
                    {
                        for (int k = 0; k < dt_StudentFeeSubmit.Rows.Count; k++)
                        {

                            if (dt_Month.Rows[j]["MonthName"].ToString() == dt_StudentFeeSubmit.Rows[k]["SubmittedMonths"].ToString())
                            {
                                MonthNameMathcing++;
                            }
                        }

                        if (MonthNameMathcing == 0)
                        {
                            MonthNamesFeNotSubmitted.Rows.Add(dt_Month.Rows[j]["MonthName"].ToString());
                        }
                    }

                    else
                    {

                        MonthNamesFeNotSubmitted.Rows.Add(dt_Month.Rows[j]["MonthName"].ToString());
                    }
                }
            }


            Double transportDue = 0;

            for (int l = 0; l < MonthNamesFeNotSubmitted.Rows.Count; l++)
            {
                sql = "select SUM(FeePayment) as Total from FeeAllotedForClassWise where Class='" + Class.Text + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "' and CardType='" + lblCardType.Text + "' and AdmissionType='" + StdType.Text + "' and Medium='" + Medium.Text + "' and Month='" + MonthNamesFeNotSubmitted.Rows[l]["MonthsNames"].ToString() + "'  ";
                TotalFees = TotalFees + Convert.ToDouble(oo.ReturnTag(sql, "Total").ToString());

            }

            for (int l = 0; l < MonthNamesFeNotSubmitted.Rows.Count; l++)
            {


                char[] c = (MonthNamesFeNotSubmitted.Rows[l][0].ToString()).ToCharArray();
                int count = 0;
                foreach (char ch in c)
                {
                    if (ch == '+')
                    {
                        count++;
                    }
                }

                if (count == 0)
                {
                    sql1 = "select Count(*) as Counter from StudentOfficialDetails where SrNo='" + std_srno.Text + "' and BranchCode=" + Session["BranchCode"] + " and TransportRequired='Yes' and SessionName='" + Session["SessionName"].ToString() + "'";

                    if (oo.ReturnTag(sql1, "Counter").ToString() != "0")
                    {
                        sql1 = "select WayAmount from StudentOfficialDetails where SrNo='" + std_srno.Text + "' and BranchCode=" + Session["BranchCode"] + " and TransportRequired='Yes' and SessionName='" + Session["SessionName"].ToString() + "'";
                        transportDue = transportDue + ((Convert.ToDouble(oo.ReturnTag(sql1, "WayAmount").ToString()) * (count + 1)));
                    }

                }
                else
                {
                    sql1 = "select Count(*) as Counter from StudentOfficialDetails where SrNo='" + std_srno.Text + "' and BranchCode=" + Session["BranchCode"] + " and TransportRequired='Yes' and SessionName='" + Session["SessionName"].ToString() + "'";
                    if (oo.ReturnTag(sql1, "Counter").ToString() != "0")
                    {
                        sql1 = "select WayAmount from StudentOfficialDetails where SrNo='" + std_srno.Text + "' and BranchCode=" + Session["BranchCode"] + " and TransportRequired='Yes' and SessionName='" + Session["SessionName"].ToString() + "'";
                        transportDue = transportDue + ((Convert.ToDouble(oo.ReturnTag(sql1, "WayAmount").ToString()) * (count + 1)));
                    }
                }


            }

            lblConvence.Text = transportDue.ToString();
            sql = "select count(*) as Count from DiscountMaster where SrNo='" + std_srno.Text + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "'";

            if (Convert.ToInt32(oo.ReturnTag(sql, "Count").ToString()) != 0)
            {
                sql = "select DiscountType,DiscountValue from DiscountMaster where SrNo='" + std_srno.Text + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "'";
                if (oo.ReturnTag(sql, "DiscountType").ToString() == "Amount")
                {
                    for (int l = 0; l < MonthNamesFeNotSubmitted.Rows.Count; l++)
                    {
                        char[] c = (MonthNamesFeNotSubmitted.Rows[l][0].ToString()).ToCharArray();
                        int count = 0;
                        foreach (char ch in c)
                        {
                            if (ch == '+')
                            {
                                count++;
                            }
                        }

                        if (count == 0)
                        {

                            TotalFees = TotalFees - Convert.ToDouble(oo.ReturnTag(sql, "DiscountValue").ToString());
                            sql1 = "select Count(*) as Counter from StudentOfficialDetails where SrNo='" + std_srno.Text + "' and TransportRequired='Yes' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "'";
                            if (oo.ReturnTag(sql1, "Counter").ToString() != "0")
                            {
                                sql1 = "select WayAmount from StudentOfficialDetails where SrNo='" + std_srno.Text + "' and TransportRequired='Yes' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "'";
                                transportDue = transportDue + Convert.ToDouble(oo.ReturnTag(sql1, "WayAmount").ToString());
                            }

                        }
                        else
                        {
                            TotalFees = TotalFees - (Convert.ToDouble(oo.ReturnTag(sql, "DiscountValue").ToString()) * (count + 1));
                            sql1 = "select Count(*) as Counter from StudentOfficialDetails where SrNo='" + std_srno.Text + "' and TransportRequired='Yes' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "'";
                            if (oo.ReturnTag(sql1, "Counter").ToString() != "0")
                            {
                                sql1 = "select WayAmount from StudentOfficialDetails where SrNo='" + std_srno.Text + "' and TransportRequired='Yes' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "'";
                                transportDue = transportDue + Convert.ToDouble(oo.ReturnTag(sql1, "WayAmount").ToString());
                            }
                        }
                    }
                }
            }

            TotalFees = TotalFees + undepositarrieramount(std_srno.Text, "Arrear");
            lblTotal.Text = TotalFees.ToString();



        }
    }

    public double undepositarrieramount(string lblSrno, string installment)
    {
        double ArreirAmount = 0;

        sql = "Select ArrearAmt from ArrierMast am";
        sql = sql + " inner join StudentOfficialDetails sod  on am.Srno=sod.SrNo";
        sql = sql + " where am.SessionName='" + Session["SessionName"].ToString() + "'";
        sql = sql + " and sod.SessionName='" + Session["SessionName"].ToString() + "'";
        sql = sql + " and sodBranchCode=" + Session["BranchCode"] + " and am.BranchCode=" + Session["BranchCode"] + " and sod.Withdrwal is null and am.srno='" + lblSrno + "'";

        if (!string.IsNullOrEmpty(oo.ReturnTag(sql, "ArrearAmt")))
        {
            ArreirAmount = Convert.ToDouble(oo.ReturnTag(sql, "ArrearAmt"));
        }

        if (ArreirAmount==0)
        {
            sql = "Select Top 1(PayableAmount - PaidAmount) ArrearBalance from FeeReceiptWithInstallment where Srno = '" + lblSrno + "' and BranchCode=" + Session["BranchCode"] + "";
            sql = sql + "and InstallmentName = 'Arrear' and IsCancel = 0 order by id desc";
            if (!string.IsNullOrEmpty(oo.ReturnTag(sql, "ArrearBalance")))
            {
                ArreirAmount = ArreirAmount + Convert.ToDouble(oo.ReturnTag(sql, "ArrearBalance"));
            }
        }

        sql = "Select Concession from Concession_Permission where SrNo='" + lblSrno + "' and BranchCode=" + Session["BranchCode"] + " and TableId=2 and Reset<>'YES'";
        string arrcon = "";
        arrcon = oo.ReturnTag(sql, "Concession");

        double arrearcon = 0;

        double.TryParse(arrcon, out arrearcon);

        if (ArreirAmount == arrearcon)
        {
            ArreirAmount = 0;
        }
        else
        {
            List<SqlParameter> param = new List<SqlParameter>();

            param.Add(new SqlParameter("@registervalue", lblSrno));
            param.Add(new SqlParameter("@installment", installment));
            param.Add(new SqlParameter("@sessionName", Session["SessionName"].ToString()));

            DataSet ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_GetFineforArrear", param);

            if (ds != null)
            {
                string latefine = "";
                latefine = ds.Tables[0].Rows[0][1].ToString();

                double value1;

                double.TryParse(latefine, out value1);

                ArreirAmount = ArreirAmount + value1;
            }
        }




        return ArreirAmount;
    }
    string sql1 = "";

    static bool isAnualDeposit = false;
    private void Dues_display()
    {
        isAnualDeposit = false;
        List<SqlParameter> para = new List<SqlParameter>();
        para.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        para.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
        para.Add(new SqlParameter("@AdmissionForClassId", drpClass.SelectedValue.ToString()));
        para.Add(new SqlParameter("@BranchId", drpBranch.SelectedValue.ToString()));
        para.Add(new SqlParameter("@FeeMonth", drpInstallment.SelectedItem.Text.ToString()));
        para.Add(new SqlParameter("@SectionId",drpSection.SelectedValue.ToString()));
        para.Add(new SqlParameter("@SectionName", drpSection.SelectedItem.Text));
        para.Add(new SqlParameter("@StreamId", drpStream.SelectedValue.ToString()));
        para.Add(new SqlParameter("@Card", drpFeeGroup.SelectedItem.Text));

        DataSet ds=new DataSet();
        ds=DLL.objDll.Sp_SelectRecord_usingExecuteDataset("GetStudentRecordWhichNotPaidFee", para);
        if (ds.Tables.Count > 0)
        {
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }

        if (GridView1.Rows.Count > 0)
        {
            Label lblTotalFooter = (Label)GridView1.FooterRow.FindControl("lblTotalFooter");
            Label lblTotalArrear = (Label)GridView1.FooterRow.FindControl("lblTotalArrear");
            Label lblConvenceFooter = (Label)GridView1.FooterRow.FindControl("lblConvenceFooter");
            Label lblTotalFooterFinal = (Label)GridView1.FooterRow.FindControl("lblTotalFooterFinal");


            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                double monthlyfee = 0;

                Label std_srno = (Label)GridView1.Rows[i].FindControl("lblsrno");
                Label Class = (Label)GridView1.Rows[i].FindControl("Class");
                Label Section = (Label)GridView1.Rows[i].FindControl("Section");
                Label Medium = (Label)GridView1.Rows[i].FindControl("Medium");
                Label StdType = (Label)GridView1.Rows[i].FindControl("StdType");
                Label lblCardType = (Label)GridView1.Rows[i].FindControl("lblCardType");
                Label lblTotal = (Label)GridView1.Rows[i].FindControl("lblTotal");
                Label lblConvence = (Label)GridView1.Rows[i].FindControl("lblConvence");
                Label lblArrear = (Label)GridView1.Rows[i].FindControl("lblArrear");
                Label lbltotalValue = (Label)GridView1.Rows[i].FindControl("lbltotalValue");
                Label lblFine = (Label)GridView1.Rows[i].FindControl("lblFine");
                string sql = "Select Promotion,MODForFeeDeposit from StudentOfficialDetails where srno='" + std_srno.Text.Trim() + "' and SessionName='" + Session["SessionName"].ToString() + "'";
                isAnualDeposit = BAL.objBal.ReturnTag(sql1, "MODForFeeDeposit") == "A" ? true : false;
                decimal discountamount = 0;
                for (int j = 1; j <= drpInstallment.SelectedIndex; j++)
                {
                    discountamount = 0;
                    sql = "Select srno from FeeDeposite where (FeeMonth='" + drpInstallment.Items[j].Text + "' or IsMopAnnual=1) and SrNo='" + std_srno.Text + "'";
                    sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and Cancel is null";
                    sql = sql + " Union";
                    sql = sql + " Select srno from FeeReceiptWithInstallment where InstallmentName = '" + drpInstallment.Items[j].Text + "' and SrNo = '" + std_srno.Text + "'";
                    sql = sql + " and SessionName = '" + Session["SessionName"].ToString() + "' and IsCancel = 0";
                    if (oo.Duplicate(sql) == false)
                    {
                        sql = "select SUM(FeePayment) as Sum from FeeAllotedForClassWise fc inner join classmaster cm on cm.id=fc.Class and cm.SessionName=fc.SessionName";
                        sql = sql + " where fc.SessionName='" + Session["SessionName"].ToString() + "' and Month='" + drpInstallment.Items[j].Text + "' and cm.ClassName='" + Class.Text + "'";
                        sql = sql + " and CardType='" + lblCardType.Text + "' and AdmissionType='" + StdType.Text + "' and Medium='" + Medium.Text + "' and";
                        sql = sql + "(fc.Branchid='" + drpBranch.SelectedValue.ToString() + "' or fc.Branchid is null)  ";
                        string monthfee = oo.ReturnTag(sql, "Sum");
                        monthlyfee = 0;

                        lblTotal.Text = ((double.TryParse(lblTotal.Text, out monthlyfee) ? monthlyfee : 0) + (double.TryParse(monthfee, out monthlyfee) ? monthlyfee : 0)).ToString();


                        if (j == 1)
                        {
                            //sql = "Select ArrearAmt from ArrierMast where srno='" + std_srno.Text + "' and SessionName='" + Session["SessionName"].ToString() + "'";
                            //string arrearfee = oo.ReturnTag(sql, "ArrearAmt");
                            //double arrearamount = 0;
                            //lblArrear.Text = (double.TryParse(arrearfee, out arrearamount) ? arrearamount : 0).ToString();

                            lblArrear.Text = undepositarrieramount(std_srno.Text, "Arrear").ToString();
                        }


                        List<decimal> discountvalues = new List<decimal>();
                        discountvalues = Get_AutometicDiscountAmount(std_srno.Text, drpInstallment.Items[j].Text);
                        discountamount = discountvalues[4];
                        decimal calculatemonthlyfee = decimal.TryParse(lblTotal.Text, out calculatemonthlyfee) ? calculatemonthlyfee : 0;

                        lblTotal.Text = (calculatemonthlyfee + discountamount).ToString();

                    }

                    monthlyfee = 0;
                    lblTotal.Text = ((double.TryParse(lblTotal.Text, out monthlyfee) ? monthlyfee : 0) + balanceAmount(std_srno.Text, drpInstallment.Items[j].Text)).ToString();

                    double value = 0;
                    lblConvence.Text = ((double.TryParse(lblConvence.Text, out value) ? value : 0) + (double.TryParse(transportfee(std_srno.Text, drpInstallment.Items[j].Text.ToString(), drpInstallment.Items[j].Value.ToString()), out value) ? value : 0)).ToString();

                    double totalfee;
                    double convencefee;
                    double.TryParse(lblTotal.Text, out totalfee);
                    double.TryParse(lblConvence.Text, out convencefee);
                    if (totalfee + convencefee != 0)
                    {
                        if(isFineExempted(std_srno.Text.Trim(), drpInstallment.Items[j].Text)==false)
                        {
                            DataSet dsforfine = getFine(std_srno.Text.Trim(), drpInstallment.Items[j].Text, Session["SessionName"].ToString());
                            double fineamount;
                            double.TryParse(dsforfine.Tables[0].Rows[0][1].ToString(), out fineamount);
                            double totalFineAmount;
                            double.TryParse(lblFine.Text, out totalFineAmount);
                            lblFine.Text = (totalFineAmount + fineamount).ToString();
                        }
                    }
                }

            }

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                Label lblSrno = (Label)GridView1.Rows[i].FindControl("lblsrno");
                Label lblChequeBounceFine = (Label)GridView1.Rows[i].FindControl("lblChequeBounceFine");
                var chequeBounceFineAmount = BLL.BLLInstance.loadChequeBounceFineAmount(lblSrno.Text.Trim());
                if (chequeBounceFineAmount != null)
                {
                    if (chequeBounceFineAmount.Tables.Count > 0)
                    {
                        if (chequeBounceFineAmount.Tables[0].Rows.Count > 0)
                        {
                            lblChequeBounceFine.Text = chequeBounceFineAmount.Tables[0].Rows[0]["BouncedCharge"].ToString() == string.Empty ? "0" : chequeBounceFineAmount.Tables[0].Rows[0]["BouncedCharge"].ToString();
                        }
                        else
                        {
                            lblChequeBounceFine.Text = "0";
                        }
                    }
                    else
                    {
                        lblChequeBounceFine.Text = "0";
                    }
                }
                else
                {
                    lblChequeBounceFine.Text = "0";
                }
            }

            calculateFooterFee();                     
            abc.Visible = true;
        }
        else
        {

            abc.Visible = false;
            //oo.MessageBox("No Record Found!", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "No Record Found!", "A");       

        }
    }
    public bool isFineExempted(string srno, string installment)
    {
        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
        param.Add(new SqlParameter("@Srno", srno));
        param.Add(new SqlParameter("@Installment", installment));

        return Convert.ToBoolean(DLL.objDll.Sp_SelectRecord_usingExecuteScalar("USP_IsFineExempted", param));
    }


    protected DataSet getFine(string registervalue, string installment, string sessionName)
    {
        //decimal lateFine = 0;
        List<SqlParameter> param = new List<SqlParameter>();

        param.Add(new SqlParameter("@registervalue", registervalue));
        param.Add(new SqlParameter("@installment", installment));
        param.Add(new SqlParameter("@sessionName", sessionName));


        DataSet ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_GetFine", param);

        return ds;
    }
    public List<decimal> Get_AutometicDiscountAmount(string registervalue, string installmentName)
    {
        List<decimal> discamount = new List<decimal>();

        decimal discountamount1 = 0, discountamount2 = 0, discountamount3 = 0, discountamount4 = 0;
        decimal totaldiscount = 0;
        string autoDisc = "0";
        string manualDisc = "0";
        string fconcDisc = "0";
        string cconcDisc = "0";
        DataSet ds = new DataSet();

        List<SqlParameter> param = new List<SqlParameter>();

        param.Add(new SqlParameter("@RegisterValue", registervalue));
        param.Add(new SqlParameter("@InstallmentName", installmentName));
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
        param.Add(new SqlParameter("@isAnnualDeposit", isAnualDeposit == true ? "A" : "I"));


        ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_GetAutometicDiscountAmount", param);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                autoDisc = ds.Tables[0].Rows[0]["AutoDisc"].ToString();
                manualDisc = ds.Tables[0].Rows[0]["ManualDisc"].ToString();
                fconcDisc = ds.Tables[0].Rows[0]["FConc"].ToString();
                cconcDisc = ds.Tables[0].Rows[0]["CConc"].ToString();
            }
        }
        decimal.TryParse(autoDisc, out discountamount1);
        decimal.TryParse(manualDisc, out discountamount2);
        decimal.TryParse(fconcDisc, out discountamount3);
        decimal.TryParse(cconcDisc, out discountamount4);

        totaldiscount = discountamount1 + discountamount2 + discountamount3 + discountamount4;

        discamount.Add(discountamount1);
        discamount.Add(discountamount2);
        discamount.Add(discountamount3);
        discamount.Add(discountamount4);
        discamount.Add(totaldiscount);


        return discamount;
    }

    public double balanceAmount(string srno, string smonth)
    {
        double sum = 0;
        sql = "select Max(Id) as Id from FeeDeposite where Status='Paid' and SrNo='" + srno + "' and cancel is null and SessionName='" + Session["SessionName"].ToString() + "'";
        string maxid = oo.ReturnTag(sql, "Id");

        sql = "select RemainingAmount from FeeDeposite where Status='Paid' and Id='" + maxid + "' and FeeMonth='" + smonth + "' and SessionName='" + Session["SessionName"].ToString() + "'";


        if (oo.ReturnTag(sql, "RemainingAmount") != "")
        {
            sum = Convert.ToDouble(oo.ReturnTag(sql, "RemainingAmount"));
        }


 
        sql = "Select MAx(Id) id from FeeReceiptWithInstallment where SrNo = '" + srno + "' and InstallmentName = '" + smonth + "' and SessionName = '" + Session["SessionName"].ToString() + "' and IsCancel = 0";
        maxid = oo.ReturnTag(sql, "Id");
      
        sql = "Select (PayableAmount-PaidAmount) RemainingAmount from FeeReceiptWithInstallment where Id='" + maxid + "' and SrNo='" + srno + "' and InstallmentName='" + smonth + "' and SessionName='" + Session["SessionName"].ToString() + "' and IsCancel=0";

        if (oo.ReturnTag(sql, "RemainingAmount") != "")
        {
            sum = Convert.ToDouble(oo.ReturnTag(sql, "RemainingAmount"));
        }

        return sum;
    }

    private void calculateFooterFee()
    {
        Label lblTotalFooter = (Label)GridView1.FooterRow.FindControl("lblTotalFooter");
        Label lblTotalArrear = (Label)GridView1.FooterRow.FindControl("lblTotalArrear");
        Label lblConvenceFooter = (Label)GridView1.FooterRow.FindControl("lblConvenceFooter");
        Label lblTotalChequeBounceFine = (Label)GridView1.FooterRow.FindControl("lblTotalChequeBounceFine");
        Label lblTotalFooterFinal = (Label)GridView1.FooterRow.FindControl("lblTotalFooterFinal");
        Label lblTotalFine = (Label)GridView1.FooterRow.FindControl("lbltotalFine");

        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            Label lblTotal = (Label)GridView1.Rows[i].FindControl("lblTotal");
            Label lblConvence = (Label)GridView1.Rows[i].FindControl("lblConvence");
            Label lblArrear = (Label)GridView1.Rows[i].FindControl("lblArrear");
            Label lblChequeBounceFine = (Label)GridView1.Rows[i].FindControl("lblChequeBounceFine");
            Label lbltotalValue = (Label)GridView1.Rows[i].FindControl("lbltotalValue");
            Label lblFine = (Label)GridView1.Rows[i].FindControl("lblFine");

            double calculaterowwisefee;
            lbltotalValue.Text = ((double.TryParse(lblTotal.Text, out calculaterowwisefee) ? calculaterowwisefee : 0)
                                 +(double.TryParse(lblArrear.Text, out calculaterowwisefee) ? calculaterowwisefee : 0)
                                 +(double.TryParse(lblConvence.Text, out calculaterowwisefee) ? calculaterowwisefee : 0)
                                 + (double.TryParse(lblFine.Text, out calculaterowwisefee) ? calculaterowwisefee : 0)
                                 + (double.TryParse(lblChequeBounceFine.Text, out calculaterowwisefee) ? calculaterowwisefee : 0)).ToString();


            if (lbltotalValue.Text == "0")
            {
                GridView1.Rows[i].Visible = false;
            }

            double totalmonthlyfee = 0;
            lblTotalFooter.Text = ((double.TryParse(lblTotalFooter.Text, out totalmonthlyfee) ? totalmonthlyfee : 0) + (double.TryParse(lblTotal.Text, out totalmonthlyfee) ? totalmonthlyfee : 0)).ToString();
            
            double totalarrierfee = 0;
            lblTotalArrear.Text = ((double.TryParse(lblTotalArrear.Text, out totalarrierfee) ? totalarrierfee : 0) + (double.TryParse(lblArrear.Text, out totalarrierfee) ? totalarrierfee : 0)).ToString();
            
            double totalconvencefee = 0;
            lblConvenceFooter.Text = ((double.TryParse(lblConvenceFooter.Text, out totalconvencefee) ? totalconvencefee : 0) + (double.TryParse(lblConvence.Text, out totalconvencefee) ? totalconvencefee : 0)).ToString();

            double totalChequeBounceFine;
            lblTotalChequeBounceFine.Text = ((double.TryParse(lblTotalChequeBounceFine.Text, out totalChequeBounceFine) ? totalChequeBounceFine : 0) + (double.TryParse(lblChequeBounceFine.Text, out totalChequeBounceFine) ? totalChequeBounceFine : 0)).ToString();

            double totalcalculaterowwisefee;
            lblTotalFooterFinal.Text = ((double.TryParse(lblTotalFooterFinal.Text, out totalcalculaterowwisefee) ? totalcalculaterowwisefee : 0) + (double.TryParse(lbltotalValue.Text, out totalcalculaterowwisefee) ? totalcalculaterowwisefee : 0)).ToString();

            double totalfinefee = 0;
            lblTotalFine.Text = ((double.TryParse(lblTotalFine.Text, out totalfinefee) ? totalfinefee : 0) + (double.TryParse(lblFine.Text, out totalfinefee) ? totalfinefee : 0)).ToString();

        }
    }
    private string transportfee(string srno, string insttalment, string insttalmentid)
    {
        double conv = 0;
        sql = "select TransportRequired from StudentOfficialDetails ";
        sql = sql + "  where  SrNo='" + srno + "' and SessionName='" + Session["SessionName"].ToString() + "'";
        string transport = oo.ReturnTag(sql, "TransportRequired");
        if (transport.ToUpper() == "YES")
        {
            sql = "Select * from StudentVehicleAllotment where SrNo='" + srno + "' and SessionName='" + Session["SessionName"].ToString() + "' and Insttalment='" + insttalment + "' and MonthStatus=1";
            if (oo.Duplicate(sql))
            {
                sql = "Select srno from FeeDeposite where (FeeMonth='" + insttalment + "' or FeeMonth='" + "(T) " + insttalment + "' or FeeMonth='Yearly') and BusConvience<>'0.00' and SrNo='" + srno + "' and SessionName='" + Session["SessionName"].ToString() + "' and Cancel is null";
                sql = sql + " Union";
                sql = sql + " Select srno from FeeReceiptWithInstallment where InstallmentName = '" + insttalment + "' and ConveyanceAmount> 0 and IsCancel = 0 and SrNo='" + srno + "' and SessionName='" + Session["SessionName"].ToString() + "'";

                if (oo.Duplicate(sql) == false)
                {
                    sql = "Select Sum(Amount) as Amount from StudentVehicleAllotment where SessionName='" + Session["SessionName"].ToString() + "' and MonthStatus='1' and SrNo='" + srno + "' and Insttalment='" + insttalment + "'";
                    double convamount = double.TryParse(oo.ReturnTag(sql, "Amount"), out convamount) ? convamount : 0;
                    sql = "Select ForMonth from MonthMaster where MonthId='" + insttalmentid + "'";
                    int noofmonth = int.TryParse(oo.ReturnTag(sql, "ForMonth"), out noofmonth) ? noofmonth : 0;
                    if (noofmonth > 0)
                    {
                        conv = conv + (convamount * noofmonth);
                    }
                    else
                    {
                        conv = conv + convamount;
                    }
                }
            }
        }

        return conv.ToString();
    }
    public void CalculateFine()
    {
        sql = "Select Count(*) as Count from SessionMaster where SessionName='" + Session["SessionName"].ToString() + "' and ToDate< Convert(nvarchar(11),GETDATE(),106)";
        int count = 0;
        count = Convert.ToInt16(oo.ReturnTag(sql, "Count"));
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            double totalAmount = 0;
            Label lblSrno = (Label)GridView1.Rows[i].FindControl("lblsrno");
            if (count <= 0)
            {
                totalAmount = totalAmount + CalculateInSameSession(lblSrno.Text);
            }
            else
            {
                totalAmount = totalAmount + CalculateInnextSession(lblSrno.Text);
            }
            Label lblFine = (Label)GridView1.Rows[i].FindControl("lblFine");
            lblFine.Text = totalAmount.ToString();
        }
        countfineamount();
    }
    public double CalculateInSameSession(string lblSrno)
    {
        double totalAmount = 0;
        try
        {


            sql = "Select *from FeeDeposite fd where fd.Cancel is null and fd.SessionName='" + Session["SessionName"].ToString() + "' and fd.SrNo='" + lblSrno + "' and ";
            sql = sql + " (FeeMonth='" + drpInstallment.SelectedItem.ToString() + "'  or FeeMonth='Yearly')";

            if (oo.Duplicate(sql) == false)
            {
                int a = 0, b = 0;

                double fineValue = 0;
                sql = "select DAY(getdate()) as DayValue";
                int dv = 0;
                dv = Convert.ToInt32(oo.ReturnTag(sql, "DayValue"));

                sql = "Select DATENAME(MONTH,GETDATE()) as MonthName";
                string selectedmonth1 = oo.ReturnTag(sql, "MonthName");

                sql = "Select MonthId from MonthMaster where ISNULL(dueMonth,DAteName(MOnth,DueDate))='" + selectedmonth1 + "' and  (ClassId='" + drpClass.SelectedValue.ToString() + "' or ClassId is null)  and (CardType='" + drpFeeGroup.SelectedValue.ToString() + "' or CardType='" + drpFeeGroup.SelectedItem.Text.ToString() + "')  and SessionName='" + Session["SessionName"].ToString() + "'";
                int id3 = Convert.ToInt32(oo.ReturnTag(sql, "MonthId"));



                sql = "Select Top 1 mm.MonthId from FeeDeposite fd inner join MonthMaster mm on mm.MonthName=fd.FeeMonth";
                sql = sql + " and (ClassId='" + drpClass.SelectedValue.ToString() + "' or ClassId is null)  and (CardType='" + drpFeeGroup.SelectedValue.ToString() + "' or CardType='" + drpFeeGroup.SelectedItem.Text.ToString() + "') and fd.SessionName='" + Session["SessionName"].ToString() + "' and mm.SessionName='" + Session["SessionName"].ToString() + "' and SrNo='" + lblSrno + "' and cancel is null order by MonthId desc";
                int id1 = 0;

                if (oo.ReturnTag(sql, "MonthId") != "")
                {
                    id1 = Convert.ToInt32(oo.ReturnTag(sql, "MonthId"));
                }
                else
                {
                    sql = "Select Top 1 MonthId from MonthMaster where SessionName='" + Session["SessionName"].ToString() + "' and (ClassId='" + drpClass.SelectedValue.ToString() + "' or ClassId is null)  and (CardType='" + drpFeeGroup.SelectedValue.ToString() + "' or CardType='" + drpFeeGroup.SelectedItem.Text.ToString() + "')";
                    if (!string.IsNullOrEmpty(oo.ReturnTag(sql, "MonthId")))
                    {
                        id1 = Convert.ToInt32(oo.ReturnTag(sql, "MonthId"));
                    }
                }
                string selectedmonth;
                selectedmonth = drpInstallment.SelectedItem.ToString();

                sql = "Select MonthId from MonthMaster where ISNULL(dueMonth,DAteName(MOnth,DueDate))='" + selectedmonth + "' and SessionName='" + Session["SessionName"].ToString() + "' and (ClassId='" + drpClass.SelectedValue.ToString() + "' or ClassId is null)  and (CardType='" + drpFeeGroup.SelectedValue.ToString() + "' or CardType='" + drpFeeGroup.SelectedItem.Text.ToString() + "')";
                int id2 = Convert.ToInt32(oo.ReturnTag(sql, "MonthId"));

                sql = "Select *from FeeDeposite fd inner join MonthMaster mm on mm.MonthName=fd.FeeMonth";
                sql = sql + " and (ClassId='" + drpClass.SelectedValue.ToString() + "' or ClassId is null)  and (CardType='" + drpFeeGroup.SelectedValue.ToString() + "' or CardType='" + drpFeeGroup.SelectedItem.Text.ToString() + "') and fd.SessionName='" + Session["SessionName"].ToString() + "' and mm.SessionName='" + Session["SessionName"].ToString() + "' and SrNo='" + lblSrno + "' and cancel is null order by MonthId desc";
                if (oo.Duplicate(sql))
                {
                    sql = "Select MonthName from MonthMaster where MonthId>'" + id1 + "' and MonthId<='" + id2 + "' and (ClassId='" + drpClass.SelectedValue.ToString() + "' or ClassId is null)  and (CardType='" + drpFeeGroup.SelectedValue.ToString() + "' or CardType='" + drpFeeGroup.SelectedItem.Text.ToString() + "') and SessionName='" + Session["SessionName"].ToString() + "'";
                }
                else
                {
                    sql = "Select MonthName from MonthMaster where MonthId>='" + id1 + "' and MonthId<='" + id2 + "' and (ClassId='" + drpClass.SelectedValue.ToString() + "' or ClassId is null)  and (CardType='" + drpFeeGroup.SelectedValue.ToString() + "' or CardType='" + drpFeeGroup.SelectedItem.Text.ToString() + "') and SessionName='" + Session["SessionName"].ToString() + "'";
                }
                da = new SqlDataAdapter(sql, con);
                dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {

                        sql = "Select MonthId from MonthMaster where ISNULL(dueMonth,DAteName(MOnth,DueDate))='" + dt.Rows[j][0].ToString() + "' and (ClassId='" + drpClass.SelectedValue.ToString() + "' or ClassId is null)  and (CardType='" + drpFeeGroup.SelectedValue.ToString() + "' or CardType='" + drpFeeGroup.SelectedItem.Text.ToString() + "') and SessionName='" + Session["SessionName"].ToString() + "'";
                        int id4 = Convert.ToInt32(oo.ReturnTag(sql, "MonthId"));
                        sql = "Select MonthName from MonthMaster where MonthId>='" + id4 + "' and MonthId<='" + id3 + "' and (ClassId='" + drpClass.SelectedValue.ToString() + "' or ClassId is null)  and (CardType='" + drpFeeGroup.SelectedValue.ToString() + "' or CardType='" + drpFeeGroup.SelectedItem.Text.ToString() + "') and SessionName='" + Session["SessionName"].ToString() + "'";
                        da = new SqlDataAdapter(sql, con);
                        DataTable dt1 = new DataTable();
                        da.Fill(dt1);
                        for (int j1 = 0; j1 < dt1.Rows.Count; j1++)
                        {
                            sql = "Select MonthId from MonthMaster where ISNULL(dueMonth,DAteName(MOnth,DueDate))='" + dt1.Rows[j1][0].ToString() + "' and (ClassId='" + drpClass.SelectedValue.ToString() + "' or ClassId is null)  and (CardType='" + drpFeeGroup.SelectedValue.ToString() + "' or CardType='" + drpFeeGroup.SelectedItem.Text.ToString() + "') and SessionName='" + Session["SessionName"].ToString() + "'";
                            int id5 = Convert.ToInt32(oo.ReturnTag(sql, "MonthId"));

                            if (id5 < id3)
                            {
                                sql = "Select Distinct MonthAmount,AmountType from RangeBasisFineMaster where SessionName='" + Session["SessionName"].ToString() + "'";
                                if (!string.IsNullOrEmpty(oo.ReturnTag(sql, "MonthAmount")))
                                {
                                    double fineamount = Convert.ToDouble(oo.ReturnTag(sql, "MonthAmount"));
                                    if (oo.ReturnTag(sql, "AmountType") == "Completion Month")
                                    {
                                        totalAmount = fineamount;
                                    }
                                    else
                                    {
                                        totalAmount = totalAmount + fineamount;
                                    }
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
                                catch (SqlException)
                                {
                                    con.Close();
                                }
                                totalAmount = totalAmount + fineValue;
                            }

                        }
                    }

                }
                else
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
                    catch (SqlException)
                    {
                        con.Close();
                    }
                    totalAmount = totalAmount + fineValue;
                }
            }
        }
        catch
        {
        }                                                                       
        return totalAmount;
    }
    public double CalculateInnextSession(string lblSrno)
    {
        double fineValue = 0;
        double totalAmount = 0;

        sql = "Select *from FeeDeposite fd where fd.Cancel is null and fd.SessionName='" + Session["SessionName"].ToString() + "' and fd.SrNo='" + lblSrno + "' and ";
        sql = sql + " (FeeMonth='" + drpInstallment.SelectedItem.ToString() + "'  or FeeMonth='Yearly')";
        if (oo.Duplicate(sql) == false)
        {
            sql = "Select MonthName from StudentOfficialDetails sd";
            sql = sql + " inner join MonthMaster mm on mm.ClassId=sd.AdmissionForClassId and mm.SessionName=sd.SessionName";
            sql = sql + " where sd.SrNo='" + lblSrno + "' and sd.SessionName='" + Session["SessionName"].ToString() + "'";
            sql = sql + " Except";
            sql = sql + " Select FeeMonth from StudentOfficialDetails sd";
            sql = sql + " inner join FeeDeposite fd on fd.SrNo=sd.SrNo and fd.SessionName=sd.SessionName";
            sql = sql + " inner join MonthMaster mm on mm.ClassId=sd.AdmissionForClassId and mm.MonthName=fd.FeeMonth and mm.SessionName=sd.SessionName";
            sql = sql + " where sd.SrNo='" + lblSrno + "' and sd.SessionName='" + Session["SessionName"].ToString() + "'";


            da = new SqlDataAdapter(sql, con);
            DataTable dt2 = new DataTable();
            da.Fill(dt2);
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                for (int j = 0; j < dt2.Rows.Count ; j++)
                {
                    for (int k = j; k < dt2.Rows.Count ; k++)
                    {
                        sql = "Select Distinct MonthAmount,AmountType from RangeBasisFineMaster where SessionName='" + Session["SessionName"].ToString() + "'";
                        double fineamount = double.TryParse(oo.ReturnTag(sql, "MonthAmount"), out fineamount) ? fineamount : 0;
                        if (oo.ReturnTag(sql, "AmountType") == "Completion Month")
                        {
                            totalAmount = fineamount;
                        }
                        else
                        {
                            totalAmount = totalAmount + fineamount;
                        }
                    }
                }
            }

            totalAmount = totalAmount + fineValue;

        }


        return totalAmount;
    }
    public void countfineamount()
    {
        if (GridView1.Rows.Count > 0)
        {
            Label lblTotalFine = (Label)GridView1.FooterRow.FindControl("lblTotalFine");
            Label lblTotalFooter = (Label)GridView1.FooterRow.FindControl("lblTotalFooter");
            Label lblTotalArrear = (Label)GridView1.FooterRow.FindControl("lblTotalArrear");
            Label lblConvenceFooter = (Label)GridView1.FooterRow.FindControl("lblConvenceFooter");
            Label lblTotalChequeBounceFine = (Label)GridView1.FooterRow.FindControl("lblTotalChequeBounceFine");
            Label lblTotalFooterFinal = (Label)GridView1.FooterRow.FindControl("lblTotalFooterFinal");
            double sss = 0;
            for (int a = 0; a < GridView1.Rows.Count; a++)
            {
                double ss = 0;
                Label lblFine = (Label)GridView1.Rows[a].FindControl("lblFine");
                Label lbltotalValue = (Label)GridView1.Rows[a].FindControl("lbltotalValue");
                lbltotalValue.Text = ((double.TryParse(lblFine.Text, out ss) ? ss : 0) + (double.TryParse(lbltotalValue.Text, out ss) ? ss : 0)).ToString();
                lblTotalFine.Text = ((double.TryParse(lblFine.Text, out ss) ? ss : 0) + (double.TryParse(lblTotalFine.Text, out ss) ? ss : 0)).ToString();
            }

            lblTotalFooterFinal.Text = ((double.TryParse(lblTotalFooter.Text, out sss) ? sss : 0) + (double.TryParse(lblTotalArrear.Text, out sss) ? sss : 0) + (double.TryParse(lblConvenceFooter.Text, out sss) ? sss : 0) + (double.TryParse(lblTotalFine.Text, out sss) ? sss : 0) + (double.TryParse(lblTotalChequeBounceFine.Text, out sss) ? sss : 0)).ToString();
        }
    }  
    public void AddbalanceAmount()
    {
        if (GridView1.Rows.Count > 0)
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                Label std_srno = (Label)GridView1.Rows[i].FindControl("lblsrno");
                Label lblTotal = (Label)GridView1.Rows[i].FindControl("lblTotal");
            }
        }
    }
    protected void drpCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadClass();
    }
    protected void drpClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadBranch();
        loadStream();
        loadSection();
        loadGridMonth();
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        try
        {
            Dues_display();
        }
        catch (Exception ex)
        {
            oo.MessageBoxforUpdatePanel(ex.Message, LinkButton2);
        }
    }
    protected void drpFeeGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadGridMonth();
    }
    protected void drpBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadStream();
    }

    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        oo.ExportToWord(Response, "BalanceFeeHeadWise.doc", gdv);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        oo.ExportDivToExcel(Response, "BalanceFeeHeadWise.xls", gdv);
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        oo.ExporttolandscapePdf(Response, "BalanceFeeHeadWise.xls", gdv);
    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }
}