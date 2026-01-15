using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;

public partial class admin_MonthWisePerDayFeeCollectionReport : Page
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
        //con.ConnectionString = ConfigurationManager.ConnectionStrings["Menu"].ConnectionString;
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

        if (!IsPostBack)
        {
            Panel1.Visible = false;
           // LinkButton1.Visible = false;
            ImageButton3.Visible = false;
            ImageButton4.Visible = false;
            ImageButton5.Visible = false;
            ImageButton6.Visible = false;

            Image1.ImageUrl = "DisplayImage.ashx?UserLoginID=" + 1;
            sql = "Select CollegeName,CollegeAdd2,CityId from CollegeMaster where CollegeId=" + 1;
            lblCollegeName.Text = oo.ReturnTag(sql, "CollegeName");
            lblbranchwithcity.Text = oo.ReturnTag(sql, "CollegeAdd2") + ' ' + oo.ReturnTag(sql, "CityId");
            
            abc.Visible = false;
        }

    }

    public void GridDisplay()
    {
        int i = 0;
        sql = "select ROW_NUMBER() OVER (ORDER BY Id ASC) AS SrNo,Id, className from ClassMaster  where id!=0 ";
        sql=sql+" and sessionName='"+Session["SessionName"].ToString()+"'";
        sql=sql+"  order by CIDOrder";
        GridView1.DataSource = oo.GridFill(sql);
        GridView1.DataBind();
        abc.Visible = true;
        Label5.Text = DropDownList1.SelectedItem.ToString();
        Panel1.Visible = true;
        ImageButton3.Visible = true;
        ImageButton4.Visible = true;
        ImageButton5.Visible = true;
        ImageButton6.Visible = true;

        for (i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            Label Label2 = (Label)GridView1.Rows[i].FindControl("Label2");

            FeesDepositSum(Label2.Text.Trim(),i);




        }
        GrandTotal();

        try
        {
            Perday1Sum();
            Perday2Sum();
            Perday3Sum();
            Perday4Sum();
            Perday5Sum();
            Perday6Sum();
            Perday7Sum();
            Perday8Sum();
            Perday9Sum();
            Perday10Sum();
            Perday11Sum();
            Perday12Sum();
            Perday13Sum();
            Perday14Sum();
            Perday15Sum();
            Perday16Sum();
            Perday17Sum();
            Perday18Sum();
            Perday19Sum();
            Perday20Sum();
            Perday21Sum();
            Perday22Sum();
            Perday23Sum();
            Perday24Sum();
            Perday25Sum();
            Perday26Sum();
            Perday27Sum();
            Perday28Sum();
            Perday29Sum();
            Perday30Sum();
            Perday31Sum();



        }
        catch (Exception) { }

        //Perday Sum Close
    }

    public void FeesDepositSum(string ClassName,int x)
    {
        
        Label lbl1 = (Label)GridView1.Rows[x].FindControl("lbl1");
        Label lbl2 = (Label)GridView1.Rows[x].FindControl("lbl2");
        Label lbl3 = (Label)GridView1.Rows[x].FindControl("lbl3");
        Label lbl4 = (Label)GridView1.Rows[x].FindControl("lbl4");
        Label lbl5 = (Label)GridView1.Rows[x].FindControl("lbl5");
        Label lbl6 = (Label)GridView1.Rows[x].FindControl("lbl6");
        Label lbl7 = (Label)GridView1.Rows[x].FindControl("lbl7");
        Label lbl8 = (Label)GridView1.Rows[x].FindControl("lbl8");
        Label lbl9 = (Label)GridView1.Rows[x].FindControl("lbl9");
        Label lbl10 = (Label)GridView1.Rows[x].FindControl("lbl10");
        Label lbl11 = (Label)GridView1.Rows[x].FindControl("lbl11");
        Label lbl12 = (Label)GridView1.Rows[x].FindControl("lbl12");
        Label lbl13 = (Label)GridView1.Rows[x].FindControl("lbl13");
        Label lbl14 = (Label)GridView1.Rows[x].FindControl("lbl14");
        Label lbl15 = (Label)GridView1.Rows[x].FindControl("lbl15");
        Label lbl16 = (Label)GridView1.Rows[x].FindControl("lbl16");
        Label lbl17 = (Label)GridView1.Rows[x].FindControl("lbl17");
        Label lbl18 = (Label)GridView1.Rows[x].FindControl("lbl18");
        Label lbl19 = (Label)GridView1.Rows[x].FindControl("lbl19");
        Label lbl20 = (Label)GridView1.Rows[x].FindControl("lbl20");
        Label lbl21 = (Label)GridView1.Rows[x].FindControl("lbl21");
        Label lbl22 = (Label)GridView1.Rows[x].FindControl("lbl22");
        Label lbl23 = (Label)GridView1.Rows[x].FindControl("lbl23");
        Label lbl24 = (Label)GridView1.Rows[x].FindControl("lbl24");
        Label lbl25 = (Label)GridView1.Rows[x].FindControl("lbl25");
        Label lbl26 = (Label)GridView1.Rows[x].FindControl("lbl26");
        Label lbl27 = (Label)GridView1.Rows[x].FindControl("lbl27");
        Label lbl28 = (Label)GridView1.Rows[x].FindControl("lbl28");
        Label lbl29 = (Label)GridView1.Rows[x].FindControl("lbl29");
        Label lbl30 = (Label)GridView1.Rows[x].FindControl("lbl30");
        Label lbl31 = (Label)GridView1.Rows[x].FindControl("lbl31");
        Label lblTotalAmt = (Label)GridView1.Rows[x].FindControl("lblTotalAmt");


        lbl1.Text = PerDayDepositFees(1, DropDownList1.SelectedValue.ToString(), ClassName);
        lbl2.Text = PerDayDepositFees(2, DropDownList1.SelectedValue.ToString(), ClassName);
        lbl3.Text = PerDayDepositFees(3, DropDownList1.SelectedValue.ToString(), ClassName);
        lbl4.Text = PerDayDepositFees(4, DropDownList1.SelectedValue.ToString(), ClassName);
        lbl5.Text = PerDayDepositFees(5, DropDownList1.SelectedValue.ToString(), ClassName);
        lbl6.Text = PerDayDepositFees(6, DropDownList1.SelectedValue.ToString(), ClassName);
        lbl7.Text = PerDayDepositFees(7, DropDownList1.SelectedValue.ToString(), ClassName);
        lbl8.Text = PerDayDepositFees(8, DropDownList1.SelectedValue.ToString(), ClassName);
        lbl9.Text = PerDayDepositFees(9, DropDownList1.SelectedValue.ToString(), ClassName);
        lbl10.Text = PerDayDepositFees(10, DropDownList1.SelectedValue.ToString(), ClassName);
        lbl11.Text = PerDayDepositFees(11, DropDownList1.SelectedValue.ToString(), ClassName);
        lbl12.Text = PerDayDepositFees(12, DropDownList1.SelectedValue.ToString(), ClassName);
        lbl13.Text = PerDayDepositFees(13, DropDownList1.SelectedValue.ToString(), ClassName);
        lbl14.Text = PerDayDepositFees(14, DropDownList1.SelectedValue.ToString(), ClassName);
        lbl15.Text = PerDayDepositFees(15, DropDownList1.SelectedValue.ToString(), ClassName);
        lbl16.Text = PerDayDepositFees(16, DropDownList1.SelectedValue.ToString(), ClassName);
        lbl17.Text = PerDayDepositFees(17, DropDownList1.SelectedValue.ToString(), ClassName);
        lbl18.Text = PerDayDepositFees(18, DropDownList1.SelectedValue.ToString(), ClassName);
        lbl19.Text = PerDayDepositFees(19, DropDownList1.SelectedValue.ToString(), ClassName);
        lbl20.Text = PerDayDepositFees(20, DropDownList1.SelectedValue.ToString(), ClassName);
        lbl21.Text = PerDayDepositFees(21, DropDownList1.SelectedValue.ToString(), ClassName);
        lbl22.Text = PerDayDepositFees(22, DropDownList1.SelectedValue.ToString(), ClassName);
        lbl23.Text = PerDayDepositFees(23, DropDownList1.SelectedValue.ToString(), ClassName);
        lbl24.Text = PerDayDepositFees(24, DropDownList1.SelectedValue.ToString(), ClassName);
        lbl25.Text = PerDayDepositFees(25, DropDownList1.SelectedValue.ToString(), ClassName);
        lbl26.Text = PerDayDepositFees(26, DropDownList1.SelectedValue.ToString(), ClassName);
        lbl27.Text = PerDayDepositFees(27, DropDownList1.SelectedValue.ToString(), ClassName);
        lbl28.Text = PerDayDepositFees(28, DropDownList1.SelectedValue.ToString(), ClassName);
        lbl29.Text = PerDayDepositFees(29, DropDownList1.SelectedValue.ToString(), ClassName);
        lbl30.Text = PerDayDepositFees(30, DropDownList1.SelectedValue.ToString(), ClassName);
        lbl31.Text = PerDayDepositFees(31, DropDownList1.SelectedValue.ToString(), ClassName);

        double sum=0;

        try
        {
            sum=sum+Convert.ToDouble(lbl1.Text);
        }
        catch(Exception){}

        try
        {
            sum = sum + Convert.ToDouble(lbl2.Text);
        }
        catch (Exception) { }

        try
        {
            sum = sum + Convert.ToDouble(lbl3.Text);
        }
        catch (Exception) { }

        try
        {
            sum = sum + Convert.ToDouble(lbl4.Text);
        }
        catch (Exception) { }

        try
        {
            sum = sum + Convert.ToDouble(lbl5.Text);
        }
        catch (Exception) { }

        try
        {
            sum = sum + Convert.ToDouble(lbl6.Text);
        }
        catch (Exception) { }
        try
        {
            sum = sum + Convert.ToDouble(lbl7.Text);
        }
        catch (Exception) { }
        try
        {
            sum = sum + Convert.ToDouble(lbl8.Text);
        }
        catch (Exception) { }
        try
        {
            sum = sum + Convert.ToDouble(lbl9.Text);
        }
        catch (Exception) { }
        try
        {
            sum = sum + Convert.ToDouble(lbl10.Text);
        }
        catch (Exception) { }
        try
        {
            sum = sum + Convert.ToDouble(lbl11.Text);
        }
        catch (Exception) { }
        try
        {
            sum = sum + Convert.ToDouble(lbl12.Text);
        }
        catch (Exception) { }
        try
        {
            sum = sum + Convert.ToDouble(lbl13.Text);
        }
        catch (Exception) { }
        try
        {
            sum = sum + Convert.ToDouble(lbl14.Text);
        }
        catch (Exception) { }
        try
        {
            sum = sum + Convert.ToDouble(lbl15.Text);
        }
        catch (Exception) { }
        try
        {
            sum = sum + Convert.ToDouble(lbl16.Text);
        }
        catch (Exception) { }
        try
        {
            sum = sum + Convert.ToDouble(lbl17.Text);
        }
        catch (Exception) { }
        try
        {
            sum = sum + Convert.ToDouble(lbl18.Text);
        }
        catch (Exception) { }
        try
        {
            sum = sum + Convert.ToDouble(lbl19.Text);
        }
        catch (Exception) { }

        try
        {
            sum = sum + Convert.ToDouble(lbl20.Text);
        }
        catch (Exception) { }

        try
        {
            sum = sum + Convert.ToDouble(lbl21.Text);
        }
        catch (Exception) { }
        try
        {
            sum = sum + Convert.ToDouble(lbl22.Text);
        }
        catch (Exception) { }
        try
        {
            sum = sum + Convert.ToDouble(lbl23.Text);
        }
        catch (Exception) { }
        try
        {
            sum = sum + Convert.ToDouble(lbl24.Text);
        }
        catch (Exception) { }
        try
        {
            sum = sum + Convert.ToDouble(lbl25.Text);
        }
        catch (Exception) { }
        try
        {
            sum = sum + Convert.ToDouble(lbl26.Text);
        }
        catch (Exception) { }
        try
        {
            sum = sum + Convert.ToDouble(lbl27.Text);
        }
        catch (Exception) { }
        try
        {
            sum = sum + Convert.ToDouble(lbl28.Text);
        }
        catch (Exception) { }
        try
        {
            sum = sum + Convert.ToDouble(lbl29.Text);
        }
        catch (Exception) { }
        try
        {
            sum = sum + Convert.ToDouble(lbl30.Text);
        }
        catch (Exception) { }

        try
        {
            sum = sum + Convert.ToDouble(lbl31.Text);
        }
        catch (Exception) { }
        lblTotalAmt.Text = sum.ToString();


        }


    public string   PerDayDepositFees(int day,string month,string ClassName)
    {
        
        string dd = "";

        sql = "  select sum(RecievedAmount-BusConvience) as BusConvience from FeeDeposite where Status='Paid' and DAY(FeeDepositeDate)=" + day + " and Month(FeeDepositeDate)=" + month + " and Class='" + ClassName.Trim() + "' and SessionName='" + Session["SessionName"] + "' and cancel is null";
        try
        {
            dd = oo.ReturnTag(sql, "BusConvience");
        }
        catch (Exception) { dd = ""; }
           
        return  dd;    
}



    public void GrandTotal()
    {
        int i;
        double GrandSum = 0;

        if (GridView1.Rows.Count >= 0)
        {
            for (i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                Label lblTotalAmt = (Label)GridView1.Rows[i].FindControl("lblTotalAmt");
                GrandSum = GrandSum + Convert.ToDouble(lblTotalAmt.Text);
            }

            Label lblGrand = (Label)GridView1.FooterRow.FindControl("lblGrandTotal");
            lblGrand.Text = GrandSum.ToString();
        }
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {        
        GridDisplay();
        aspacted_Amount();
        Total_Aspacted();
    }

    public void Total_Aspacted()
    {
     
        double Total = 0;      
        double totalBal=0;

        for (int c = 0; c < GridView1.Rows.Count; c++)
        {
            Label lblAspacted = (Label)GridView1.Rows[c].FindControl("lblAspacted");
            Label lblTotalBalance = (Label)GridView1.Rows[c].FindControl("lblTotalBalance");
            Label lblTotalAmt = (Label)GridView1.Rows[c].FindControl("lblTotalAmt");
            Total = Total + Convert.ToDouble(lblAspacted.Text);
            if (Convert.ToDouble(lblTotalAmt.Text) < Convert.ToDouble(lblAspacted.Text))
            {
                lblTotalBalance.Text = (Convert.ToDouble(lblAspacted.Text) - Convert.ToDouble(lblTotalAmt.Text)).ToString();
            }
            else
            {
                lblTotalBalance.Text = "0";
            }
            totalBal = totalBal + Convert.ToDouble(lblTotalBalance.Text);  
        }

        Label lblGrandAspacted = (Label)GridView1.FooterRow.FindControl("lblGrandAspacted");
        lblGrandAspacted.Text = Total.ToString();
        Label lblGrandBalance = (Label)GridView1.FooterRow.FindControl("lblGrandBalance");
        lblGrandBalance.Text = totalBal.ToString();
    }

    string sql1 = "";

    //public void aspacted_amount()
    //{
    //    con.Open();
    //    for (int j = 0; j < 1; j++)
    //    {
    //        double class_amount_total = 0;
    //        Label lblAspacted = (Label)GridView1.Rows[j].FindControl("lblAspacted");
    //        DataTable Student_details = new DataTable();
    //        Student_details.Columns.Add("Sr");
    //        Student_details.Columns.Add("Srno");
    //        Student_details.Columns.Add("ClassName");
    //        Student_details.Columns.Add("Name");
    //        Student_details.Columns.Add("Card");
    //        Student_details.Columns.Add("Medium");
    //        Student_details.Columns.Add("TypeOFAdmision");
    //        Label Label2 = (Label)GridView1.Rows[j].FindControl("Label2");
    //        sql = "select Row_Number() Over(Order By StudentOfficialDetails.SrNo) as Sr,StudentOfficialDetails.SrNo,ClassMaster.ClassName,StudentGenaralDetail.FirstName+StudentGenaralDetail.MiddleName+StudentGenaralDetail.LastName as Name,StudentOfficialDetails.Medium,StudentOfficialDetails.TypeOFAdmision,StudentOfficialDetails.Card from StudentOfficialDetails inner join StudentGenaralDetail on StudentGenaralDetail.SrNo=StudentOfficialDetails.SrNo inner join ClassMaster on ClassMaster.Id=StudentOfficialDetails.AdmissionForClassId where StudentOfficialDetails.SessionName='" + Session["SessionName"].ToString() + "' and ClassMaster.ClassName='" + Label2.Text + "' and ClassMaster.SessionName='" + Session["SessionName"].ToString() + "' and StudentGenaralDetail.SessionName='" + Session["SessionName"].ToString() + "'";
    //        SqlCommand student_class_list = new SqlCommand(sql, con);
    //        SqlDataReader student_class_list_dr = student_class_list.ExecuteReader();
    //        while (student_class_list_dr.Read())
    //        {
    //            Student_details.Rows.Add(student_class_list_dr["Sr"].ToString(), student_class_list_dr["SrNo"].ToString(), student_class_list_dr["ClassName"].ToString(), student_class_list_dr["Name"].ToString(), student_class_list_dr["Card"].ToString(), student_class_list_dr["Medium"].ToString(), student_class_list_dr["TypeOFAdmision"].ToString());
    //        }
    //        student_class_list_dr.Close();
    //        // Grid To Display Student Details
    //        GridView2.DataSource = Student_details;
    //        GridView2.DataBind();

    //        //for (int k = 0; k < Student_details.Rows.Count; k++)
    //        for (int k = 0; k <1; k++)
    //        {
    //            //sql = "select * from FeeAllotedForClassWise where Class='" + Student_details.Rows[k]["ClassName"].ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and CardType='" + Student_details.Rows[k]["Card"].ToString() + "' and AdmissionType='" + Student_details.Rows[k]["TypeOFAdmision"].ToString() + "' and Medium='" + Student_details.Rows[k]["Medium"].ToString() + "' order by Id";

    //            sql = "select MonthName from MonthMaster where CardType='" + Student_details.Rows[k]["Card"].ToString() + "'";
    //            sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
    //            sql = sql + " or Monthid=0  ";
    //            sql = sql + " order by MonthId";
    //            GridView4.DataSource = oo.GridFill(sql);
    //            GridView4.DataBind();

    //            sql = "select Month,FeePayment from FeeAllotedForClassWise where Class='" + Student_details.Rows[k]["ClassName"].ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and CardType='" + Student_details.Rows[k]["Card"].ToString() + "' and AdmissionType='" + Student_details.Rows[k]["TypeOFAdmision"].ToString() + "' and Medium='" + Student_details.Rows[k]["Medium"].ToString() + "' and Month like '%" + DropDownList1.SelectedItem.Text + "%' ";

                
    //        }

    //    }
    //    con.Close();
    //}





    public void aspacted_Amount()
    {

        //try
        //{
            con.Open();
            for (int j = 0; j < GridView1.Rows.Count; j++)
            {
                double class_amount_total = 0;
                Label lblAspacted = (Label)GridView1.Rows[j].FindControl("lblAspacted");
                DataTable Student_details = new DataTable();
                Student_details.Columns.Add("Sr");
                Student_details.Columns.Add("Srno");
                Student_details.Columns.Add("ClassName");
                Student_details.Columns.Add("Name");
                Student_details.Columns.Add("Card");
                Student_details.Columns.Add("Medium");
                Student_details.Columns.Add("TypeOFAdmision");
                Label Label2 = (Label)GridView1.Rows[j].FindControl("Label2");
                sql = "select Row_Number() Over(Order By StudentOfficialDetails.SrNo) as Sr,StudentOfficialDetails.SrNo,ClassMaster.ClassName,StudentGenaralDetail.FirstName+StudentGenaralDetail.MiddleName+StudentGenaralDetail.LastName as Name,StudentOfficialDetails.Medium,StudentOfficialDetails.TypeOFAdmision,StudentOfficialDetails.Card from StudentOfficialDetails inner join StudentGenaralDetail on StudentGenaralDetail.SrNo=StudentOfficialDetails.SrNo inner join ClassMaster on ClassMaster.Id=StudentOfficialDetails.AdmissionForClassId where StudentOfficialDetails.SessionName='" + Session["SessionName"].ToString() + "' and ClassMaster.ClassName='" + Label2.Text + "' and ClassMaster.SessionName='" + Session["SessionName"].ToString() + "' and StudentGenaralDetail.SessionName='" + Session["SessionName"].ToString() + "'";
                SqlCommand student_class_list = new SqlCommand(sql, con);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlDataReader student_class_list_dr = student_class_list.ExecuteReader();
                while (student_class_list_dr.Read())
                {
                    Student_details.Rows.Add(student_class_list_dr["Sr"].ToString(), student_class_list_dr["SrNo"].ToString(), student_class_list_dr["ClassName"].ToString(), student_class_list_dr["Name"].ToString(), student_class_list_dr["Card"].ToString(), student_class_list_dr["Medium"].ToString(), student_class_list_dr["TypeOFAdmision"].ToString());
                }
                student_class_list_dr.Close();
                // Grid To Display Student Details
                GridView2.DataSource = Student_details;
                GridView2.DataBind();
                //Select The Months
                //for (int k = 0; k < Student_details.Rows.Count; k++)
                for (int k = 0; k < Student_details.Rows.Count; k++)
                {

                    DataTable Due_monthes = new DataTable();
                    Due_monthes.Columns.Add("SrNO");
                    Due_monthes.Columns.Add("MonthNames");
                    Due_monthes.Columns.Add("DueMonthesName");
                    sql1 = "select ROW_NUMBER() Over(Order By MonthId) as Sr ,MonthName,DueMonth from MonthMaster where SessionName='" + Session["SessionName"].ToString() + "' and CardType='" + Student_details.Rows[k]["Card"].ToString() + "' Order By MonthId ";
                    SqlCommand DueMonth_cmd = new SqlCommand(sql1, con);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    SqlDataReader DueMonth_cmd_dr = DueMonth_cmd.ExecuteReader();
                    while (DueMonth_cmd_dr.Read())
                    {
                        Due_monthes.Rows.Add(DueMonth_cmd_dr["Sr"].ToString(), DueMonth_cmd_dr["MonthName"].ToString(), DueMonth_cmd_dr["DueMonth"].ToString());
                    }
                    DueMonth_cmd_dr.Close();
                    GridView3.DataSource = Due_monthes;
                    GridView3.DataBind();
                    string CurrentMonthName = DropDownList1.SelectedItem.Text;
                    int CurrentMonthValue = 0;
                    int PastMonthValue = 0;
                    sql1 = "select top 1 FeeMonth from FeeDeposite where Status='Paid' and SrNo='" + Student_details.Rows[k]["Srno"].ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "'  order by Id Desc";
                    string Past_monthFeesPaid = oo.ReturnTag(sql1, "FeeMonth").ToString();
                    if (Past_monthFeesPaid != "Yearly")
                    {
                        for (int a = 0; a < Due_monthes.Rows.Count; a++)
                        {
                            if (Due_monthes.Rows[a]["DueMonthesName"].ToString() == CurrentMonthName)
                            {
                                CurrentMonthValue = Convert.ToInt32(Due_monthes.Rows[a]["SrNO"].ToString());
                            }
                            if (Due_monthes.Rows[a]["MonthNames"].ToString() == Past_monthFeesPaid)
                            {
                                PastMonthValue = Convert.ToInt32(Due_monthes.Rows[a]["SrNO"].ToString());
                            }
                        }

                        //oo.MessageBox(CurrentMonthValue.ToString() + "//" + PastMonthValue.ToString(), this.Page);
                        if (PastMonthValue < CurrentMonthValue)
                        {
                            for (int b = PastMonthValue; b < CurrentMonthValue; b++)
                            {
                                sql1 = "select Month,Sum(FeePayment) as Sums from FeeAllotedForClassWise where Class='" + Student_details.Rows[k]["ClassName"].ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and CardType='" + Student_details.Rows[k]["Card"].ToString() + "' and AdmissionType='" + Student_details.Rows[k]["TypeOFAdmision"].ToString() + "' and Medium='" + Student_details.Rows[k]["Medium"].ToString() + "' and Month='" + Due_monthes.Rows[b]["MonthNames"].ToString() + "' Group By Month";
                                class_amount_total = class_amount_total + Convert.ToDouble(oo.ReturnTag(sql1, "Sums").ToString());
                            }
                        }
                        else
                        {
                            DataTable Monthes_Values_add = new DataTable();
                            Monthes_Values_add.Columns.Add("Id");
                            Monthes_Values_add.Columns.Add("FeeMonth");
                            Monthes_Values_add.Columns.Add("TotalFeeAmount");
                            if (con.State == ConnectionState.Closed)
                            {
                                con.Open();
                            }
                            sql = "select ROW_NUMBER() Over(Order By Id) as Id,FeeMonth,TotalFeeAmount from FeeDeposite where Status='Paid' and MONTH(FeeDepositeDate)='" + DropDownList1.SelectedValue.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and Class='" + Student_details.Rows[k]["ClassName"].ToString() + "' and cancel is null and SrNo='" + Student_details.Rows[k]["Srno"].ToString() + "' Order By Id";
                            SqlCommand Monthes_Values_add_cmd = new SqlCommand(sql, con);
                            SqlDataReader Monthes_Values_add_cmd_dr = Monthes_Values_add_cmd.ExecuteReader();
                            while (Monthes_Values_add_cmd_dr.Read())
                            {
                                Monthes_Values_add.Rows.Add(Monthes_Values_add_cmd_dr[0].ToString(), Monthes_Values_add_cmd_dr[1].ToString(), Monthes_Values_add_cmd_dr[2].ToString());
                            }
                            Monthes_Values_add_cmd_dr.Close();
                            //sql = "select  FeeMonth,TotalFeeAmount from FeeDeposite where MONTH(FeeDepositeDate)='" + DropDownList1.SelectedValue.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and Class='" + Student_details.Rows[k]["Class"].ToString() + "' and cancel is null and SrNo='" + Student_details.Rows[k]["Srno"].ToString() + "' and FeeMonth!='" + Due_monthes.Rows[CurrentMonthValue]["MonthNames"].ToString() + "' order By Id";
                            int monthnamesValue_add = 0;
                            for (int c = 0; c < Monthes_Values_add.Rows.Count; c++)
                            {
                                
                                
                                //oo.MessageBox(Monthes_Values_add.Rows[c]["FeeMonth"].ToString(), this.Page);
                                try
                                {
                                    if (Monthes_Values_add.Rows[c]["FeeMonth"].ToString() == Due_monthes.Rows[CurrentMonthValue]["MonthNames"].ToString())
                                    {
                                        monthnamesValue_add = c;
                                    }
                                }
                                catch{}
                            }

                            double Sum_student_monthes = 0;
                            for (int d = monthnamesValue_add; d < Monthes_Values_add.Rows.Count; d++)
                            {
                                Sum_student_monthes = Sum_student_monthes + Convert.ToDouble(Monthes_Values_add.Rows[d]["TotalFeeAmount"].ToString());
                            }

                            class_amount_total = class_amount_total + Sum_student_monthes;
                        }
                    }


                }

                lblAspacted.Text = class_amount_total.ToString();

            }
            con.Close();
        //}
        //catch { }
    }



    public void Perday1Sum()
    {
        int i;
        double sum = 0;
        for (i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            Label lbl1 = (Label)GridView1.Rows[i].FindControl("lbl1");
            try
            {
                sum = sum + Convert.ToDouble(lbl1.Text.Trim());
            }
            catch (Exception) { }
        }
        Label lbl1stCollection = (Label)GridView1.FooterRow.FindControl("lbl1stCollection");
        lbl1stCollection.Text = sum.ToString();
    }

    public void Perday2Sum()
    {
        int i;
        double sum = 0;
        for (i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            Label lbl2 = (Label)GridView1.Rows[i].FindControl("lbl2");
            try
            {
                sum = sum + Convert.ToDouble(lbl2.Text.Trim());
            }
            catch (Exception) { }
        }
        Label lbl2ndCollection = (Label)GridView1.FooterRow.FindControl("lbl2ndCollection");
        lbl2ndCollection.Text = sum.ToString();
    }




    public void Perday3Sum()
    {
        int i;
        double sum = 0;
        for (i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            Label lbl3 = (Label)GridView1.Rows[i].FindControl("lbl3");
            try
            {
                sum = sum + Convert.ToDouble(lbl3.Text.Trim());
            }
            catch (Exception) { }
        }
        Label lbl3rdCollection = (Label)GridView1.FooterRow.FindControl("lbl3rdCollection");
        lbl3rdCollection.Text = sum.ToString();
    }

    public void Perday4Sum()
    {
        int i;
        double sum = 0;
        for (i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            Label lbl4 = (Label)GridView1.Rows[i].FindControl("lbl4");
            try
            {
                sum = sum + Convert.ToDouble(lbl4.Text.Trim());
            }
            catch (Exception) { }
        }
        Label lbl4thCollection = (Label)GridView1.FooterRow.FindControl("lbl4thCollection");
        lbl4thCollection.Text = sum.ToString();
    }
    public void Perday5Sum()
    {
        int i;
        double sum = 0;
        for (i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            Label lbl5 = (Label)GridView1.Rows[i].FindControl("lbl5");
            try
            {
                sum = sum + Convert.ToDouble(lbl5.Text.Trim());
            }
            catch (Exception) { }
        }
        Label lbl5thCollection = (Label)GridView1.FooterRow.FindControl("lbl5thCollection");
        lbl5thCollection.Text = sum.ToString();
    }
    public void Perday6Sum()
    {
        int i;
        double sum = 0;
        for (i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            Label lbl6 = (Label)GridView1.Rows[i].FindControl("lbl6");
            try
            {
                sum = sum + Convert.ToDouble(lbl6.Text.Trim());
            }
            catch (Exception) { }
        }
        Label lbl6thCollection = (Label)GridView1.FooterRow.FindControl("lbl6thCollection");
        lbl6thCollection.Text = sum.ToString();
    }
    public void Perday7Sum()
    {
        int i;
        double sum = 0;
        for (i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            Label lbl7 = (Label)GridView1.Rows[i].FindControl("lbl7");
            try
            {
                sum = sum + Convert.ToDouble(lbl7.Text.Trim());
            }
            catch (Exception) { }
        }
        Label lbl7thCollection = (Label)GridView1.FooterRow.FindControl("lbl7thCollection");
        lbl7thCollection.Text = sum.ToString();
    }
    public void Perday8Sum()
    {
        int i;
        double sum = 0;
        for (i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            Label lbl8 = (Label)GridView1.Rows[i].FindControl("lbl8");
            try
            {
                sum = sum + Convert.ToDouble(lbl8.Text.Trim());
            }
            catch (Exception) { }
        }
        Label lbl8thCollection = (Label)GridView1.FooterRow.FindControl("lbl8thCollection");
        lbl8thCollection.Text = sum.ToString();
    }

    public void Perday9Sum()
    {
        int i;
        double sum = 0;
        for (i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            Label lbl9 = (Label)GridView1.Rows[i].FindControl("lbl9");
            try
            {
                sum = sum + Convert.ToDouble(lbl9.Text.Trim());
            }
            catch (Exception) { }
        }
        Label lbl9thCollection = (Label)GridView1.FooterRow.FindControl("lbl9thCollection");
        lbl9thCollection.Text = sum.ToString();
    }

    public void Perday10Sum()
    {
        int i;
        double sum = 0;
        for (i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            Label lbl10 = (Label)GridView1.Rows[i].FindControl("lbl10");
            try
            {
                sum = sum + Convert.ToDouble(lbl10.Text.Trim());
            }
            catch (Exception) { }
        }
        Label lbl10thCollection = (Label)GridView1.FooterRow.FindControl("lbl10thCollection");
        lbl10thCollection.Text = sum.ToString();
    }

    public void Perday11Sum()
    {
        int i;
        double sum = 0;
        for (i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            Label lbl11 = (Label)GridView1.Rows[i].FindControl("lbl11");
            try
            {
                sum = sum + Convert.ToDouble(lbl11.Text.Trim());
            }
            catch (Exception) { }
        }
        Label lbl11thCollection = (Label)GridView1.FooterRow.FindControl("lbl11thCollection");
        lbl11thCollection.Text = sum.ToString();
    }

    public void Perday12Sum()
    {
        int i;
        double sum = 0;
        for (i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            Label lbl12 = (Label)GridView1.Rows[i].FindControl("lbl12");
            try
            {
                sum = sum + Convert.ToDouble(lbl12.Text.Trim());
            }
            catch (Exception) { }
        }
        Label lbl12thCollection = (Label)GridView1.FooterRow.FindControl("lbl12thCollection");
        lbl12thCollection.Text = sum.ToString();
    }

    public void Perday13Sum()
    {
        int i;
        double sum = 0;
        for (i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            Label lbl13 = (Label)GridView1.Rows[i].FindControl("lbl13");
            try
            {
                sum = sum + Convert.ToDouble(lbl13.Text.Trim());
            }
            catch (Exception) { }
        }
        Label lbl13thCollection = (Label)GridView1.FooterRow.FindControl("lbl13thCollection");
        lbl13thCollection.Text = sum.ToString();
    }

    public void Perday14Sum()
    {
        int i;
        double sum = 0;
        for (i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            Label lbl14 = (Label)GridView1.Rows[i].FindControl("lbl14");
            try
            {
                sum = sum + Convert.ToDouble(lbl14.Text.Trim());
            }
            catch (Exception) { }
        }
        Label lbl14thCollection = (Label)GridView1.FooterRow.FindControl("lbl14thCollection");
        lbl14thCollection.Text = sum.ToString();
    }

    public void Perday15Sum()
    {
        int i;
        double sum = 0;
        for (i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            Label lbl15 = (Label)GridView1.Rows[i].FindControl("lbl15");
            try
            {
                sum = sum + Convert.ToDouble(lbl15.Text.Trim());
            }
            catch (Exception) { }
        }
        Label lbl15thCollection = (Label)GridView1.FooterRow.FindControl("lbl15thCollection");
        lbl15thCollection.Text = sum.ToString();
    }

    public void Perday16Sum()
    {
        int i;
        double sum = 0;
        for (i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            Label lbl16 = (Label)GridView1.Rows[i].FindControl("lbl16");
            try
            {
                sum = sum + Convert.ToDouble(lbl16.Text.Trim());
            }
            catch (Exception) { }
        }
        Label lbl16thCollection = (Label)GridView1.FooterRow.FindControl("lbl16thCollection");
        lbl16thCollection.Text = sum.ToString();
    }
    public void Perday17Sum()
    {
        int i;
        double sum = 0;
        for (i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            Label lbl17 = (Label)GridView1.Rows[i].FindControl("lbl17");
            try
            {
                sum = sum + Convert.ToDouble(lbl17.Text.Trim());
            }
            catch (Exception) { }
        }
        Label lbl17thCollection = (Label)GridView1.FooterRow.FindControl("lbl17thCollection");
        lbl17thCollection.Text = sum.ToString();
    }
    public void Perday18Sum()
    {
        int i;
        double sum = 0;
        for (i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            Label lbl18 = (Label)GridView1.Rows[i].FindControl("lbl18");
            try
            {
                sum = sum + Convert.ToDouble(lbl18.Text.Trim());
            }
            catch (Exception) { }
        }
        Label lbl18thCollection = (Label)GridView1.FooterRow.FindControl("lbl18thCollection");
        lbl18thCollection.Text = sum.ToString();
    }
    public void Perday19Sum()
    {
        int i;
        double sum = 0;
        for (i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            Label lbl19 = (Label)GridView1.Rows[i].FindControl("lbl19");
            try
            {
                sum = sum + Convert.ToDouble(lbl19.Text.Trim());
            }
            catch (Exception) { }
        }
        Label lbl19thCollection = (Label)GridView1.FooterRow.FindControl("lbl19thCollection");
        lbl19thCollection.Text = sum.ToString();
    }
    public void Perday20Sum()
    {
        int i;
        double sum = 0;
        for (i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            Label lbl20 = (Label)GridView1.Rows[i].FindControl("lbl20");
            try
            {
                sum = sum + Convert.ToDouble(lbl20.Text.Trim());
            }
            catch (Exception) { }
        }
        Label lbl20thCollection = (Label)GridView1.FooterRow.FindControl("lbl20thCollection");
        lbl20thCollection.Text = sum.ToString();
    }
    public void Perday21Sum()
    {
        int i;
        double sum = 0;
        for (i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            Label lbl21 = (Label)GridView1.Rows[i].FindControl("lbl21");
            try
            {
                sum = sum + Convert.ToDouble(lbl21.Text.Trim());
            }
            catch (Exception) { }
        }
        Label lbl21thCollection = (Label)GridView1.FooterRow.FindControl("lbl21thCollection");
        lbl21thCollection.Text = sum.ToString();
    }
    public void Perday22Sum()
    {
        int i;
        double sum = 0;
        for (i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            Label lbl22 = (Label)GridView1.Rows[i].FindControl("lbl22");
            try
            {
                sum = sum + Convert.ToDouble(lbl22.Text.Trim());
            }
            catch (Exception) { }
        }
        Label lbl22thCollection = (Label)GridView1.FooterRow.FindControl("lbl22thCollection");
        lbl22thCollection.Text = sum.ToString();
    }
    public void Perday23Sum()
    {
        int i;
        double sum = 0;
        for (i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            Label lbl23 = (Label)GridView1.Rows[i].FindControl("lbl23");
            try
            {
                sum = sum + Convert.ToDouble(lbl23.Text.Trim());
            }
            catch (Exception) { }
        }
        Label lbl23thCollection = (Label)GridView1.FooterRow.FindControl("lbl23thCollection");
        lbl23thCollection.Text = sum.ToString();
    }
    public void Perday24Sum()
    {
        int i;
        double sum = 0;
        for (i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            Label lbl24 = (Label)GridView1.Rows[i].FindControl("lbl24");
            try
            {
                sum = sum + Convert.ToDouble(lbl24.Text.Trim());
            }
            catch (Exception) { }
        }
        Label lbl24thCollection = (Label)GridView1.FooterRow.FindControl("lbl24thCollection");
        lbl24thCollection.Text = sum.ToString();
    }
    public void Perday25Sum()
    {
        int i;
        double sum = 0;
        for (i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            Label lbl25 = (Label)GridView1.Rows[i].FindControl("lbl25");
            try
            {
                sum = sum + Convert.ToDouble(lbl25.Text.Trim());
            }
            catch (Exception) { }
        }
        Label lbl25thCollection = (Label)GridView1.FooterRow.FindControl("lbl25thCollection");
        lbl25thCollection.Text = sum.ToString();
    }
    public void Perday26Sum()
    {
        int i;
        double sum = 0;
        for (i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            Label lbl26 = (Label)GridView1.Rows[i].FindControl("lbl26");
            try
            {
                sum = sum + Convert.ToDouble(lbl26.Text.Trim());
            }
            catch (Exception) { }
        }
        Label lbl26thCollection = (Label)GridView1.FooterRow.FindControl("lbl26thCollection");
        lbl26thCollection.Text = sum.ToString();
    }
    public void Perday27Sum()
    {
        int i;
        double sum = 0;
        for (i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            Label lbl27 = (Label)GridView1.Rows[i].FindControl("lbl27");
            try
            {
                sum = sum + Convert.ToDouble(lbl27.Text.Trim());
            }
            catch (Exception) { }
        }
        Label lbl27thCollection = (Label)GridView1.FooterRow.FindControl("lbl27thCollection");
        lbl27thCollection.Text = sum.ToString();
    }

    public void Perday28Sum()
    {
        int i;
        double sum = 0;
        for (i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            Label lbl28 = (Label)GridView1.Rows[i].FindControl("lbl28");
            try
            {
                sum = sum + Convert.ToDouble(lbl28.Text.Trim());
            }
            catch (Exception) { }
        }
        Label lbl28thCollection = (Label)GridView1.FooterRow.FindControl("lbl28thCollection");
        lbl28thCollection.Text = sum.ToString();
    }
    public void Perday29Sum()
    {
        int i;
        double sum = 0;
        for (i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            Label lbl29 = (Label)GridView1.Rows[i].FindControl("lbl29");
            try
            {
                sum = sum + Convert.ToDouble(lbl29.Text.Trim());
            }
            catch (Exception) { }
        }
        Label lbl29thCollection = (Label)GridView1.FooterRow.FindControl("lbl29thCollection");
        lbl29thCollection.Text = sum.ToString();
    }
    public void Perday30Sum()
    {
        int i;
        double sum = 0;
        for (i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            Label lbl30 = (Label)GridView1.Rows[i].FindControl("lbl30");
            try
            {
                sum = sum + Convert.ToDouble(lbl30.Text.Trim());
            }
            catch (Exception) { }
        }
        Label lbl30thCollection = (Label)GridView1.FooterRow.FindControl("lbl30thCollection");
        lbl30thCollection.Text = sum.ToString();
    }
    public void Perday31Sum()
    {
        int i;
        double sum = 0;
        for (i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            Label lbl31 = (Label)GridView1.Rows[i].FindControl("lbl31");
            try
            {
                sum = sum + Convert.ToDouble(lbl31.Text.Trim());
            }
            catch (Exception) { }
        }
        Label lbl31thCollection = (Label)GridView1.FooterRow.FindControl("lbl31thCollection");
        lbl31thCollection.Text = sum.ToString();
    }




    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        GridDisplay();
    }

    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }
    protected void ImageButton5_Click(object sender, EventArgs e)
    {
        oo.ExportToWord(Response, "MonthWisePerDayFeeCollectionReportWithoutConveyance.doc", gdv);
    }
    protected void ImageButton6_Click(object sender, EventArgs e)
    {
        oo.ExportToExcel("MonthWisePerDayFeeCollectionReportWithoutConveyance.xls", GridView1);
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {

    }
}