using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;
using System.Globalization;


public partial class issue_return_book_bank_item_master : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        //con.ConnectionString = ConfigurationManager.ConnectionStrings["Menu"].ConnectionString;
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader); 

        if (!IsPostBack)
        {

            try
            {
                CheckValueADDDeleteUpdate();
            }
            catch (Exception) { }
           
            
            
            
            Panel1.Visible = false;
            sql = "Select CategoryName from ItemCategoryMaster where BranchCode = " + Session["BranchCode"] + "";
           // oo.FillDropDown(sql, drpcategory, "CategoryName");

            sql = "select distinct SubCategoryName from ItemSubCategoryMaster ";
           // oo.FillDropDown(sql, drpSubCate, "SubCategoryName");

            oo.AddDateMonthYearDropDown(DrpIsseYY, DrpIsseMM, DrpIsseDD);
            oo.AddDateMonthYearDropDown(DrpAcRetnYY, DrpAcRetnMM, DrpAcRetnDD);
            oo.AddDateMonthYearDropDown(DrpRetYY, DrpRetMM, DrpRetDD);
            oo.FindCurrentDateandSetinDropDown(DrpIsseYY, DrpIsseMM, DrpIsseDD);
            oo.FindCurrentDateandSetinDropDown(DrpAcRetnYY, DrpAcRetnMM, DrpAcRetnDD);
            oo.FindCurrentDateandSetinDropDown(DrpRetYY, DrpRetMM, DrpRetDD);
            sql = "Select BookStatus from BookStatusMaster ";
            oo.FillDropDown(sql, drpBookStatus, "BookStatus");
            Panel3.Visible = false;

        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {





     sql = "Select AccessionNo from LibraryItemEntry where AccessionNo='" + txtAxxessionNo.Text.ToString() + "' and DeleteBookYesno is null or DeleteBookYesno='No' and BranchCode = " + Session["BranchCode"] + "";
    if (oo.Duplicate(sql))
    {
        Panel1.Visible = true;
        Label1.Text = "";
        sql = "Select Title,SubjectTopic,Author1,Author2,Category,SubCategory from LibraryItemEntry where AccessionNo='" + txtAxxessionNo.Text.ToString() + "' and BranchCode = " + Session["BranchCode"] + "";
        txtTitle.Text = oo.ReturnTag(sql, "Title");
        txtSubject.Text = oo.ReturnTag(sql, "SubjectTopic");
        txtAuthor1.Text = oo.ReturnTag(sql, "Author1");
        txtAuthor2.Text = oo.ReturnTag(sql, "Author2");
        drpcategory.Text = oo.ReturnTag(sql, "Category");
        drpSubCate.Text = oo.ReturnTag(sql, "SubCategory");
        txtTitle.ReadOnly = true;
        txtSubject.ReadOnly = true;
        txtAuthor1.ReadOnly = true;
        txtAuthor2.ReadOnly = true;
        drpcategory.ReadOnly = true;
        drpSubCate.ReadOnly = true;
        Status.ReadOnly = true;
           


       
        string sql1 = "";
        sql1 = "Select Status from IssueReturnLibraryItem where AccessionNo='" + txtAxxessionNo.Text.ToString() + "' and BranchCode = " + Session["BranchCode"] + "";
        if (oo.ReturnTag(sql1, "Status") == "Return" ||oo.ReturnTag(sql1, "Status") =="")
        {
            Status.Text = "Issue";


            Panel1.Visible = true;
            Panel2.Visible = false;
          //  oo.UnReadOnlyControls(this.Page);
            sql = "Select BookStatus from BookStatusMaster  where BranchCode = " + Session["BranchCode"] + "";
            oo.FillDropDown(sql, drpBookStatus, "BookStatus");
            oo.FindCurrentDateandSetinDropDown(DrpIsseYY, DrpIsseMM, DrpIsseDD);
            oo.FindCurrentDateandSetinDropDown(DrpAcRetnYY, DrpAcRetnMM, DrpAcRetnDD);
            oo.FindCurrentDateandSetinDropDown(DrpRetYY, DrpRetMM, DrpRetDD);
            FindReturnDate();
        }
      
        else 
        {
            Label1.Text = "Item(s) already issued!";
            Status.Text = "Return";
            DisplayItemIssue();
            drpcategory.Enabled = false;
            drpSubCate.Enabled = false;
            Panel1.Visible = true;
            Panel2.Visible = true;
          

        }
    }
    else
        {
        //oo.MessageBox("Sorry, No Record(s) found!", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, No Record(s) found!", "A");       

        Label1.Text = "Sorry, No Record(s) found!";
        Panel1.Visible=false;

    }

}
    public void DisplayItemIssue()
    {
        sql = "Select AccessionNo,Title,Subject,Author1,Author2,Status,MemberId,Category,Name,Address,PhoneNo,Email,";
        sql = sql + "  left(convert(nvarchar,IssueDate,106),2) as DD,Right(left(convert(nvarchar,IssueDate,106),6),3) as MM , RIGHT(convert(nvarchar,IssueDate,106),4) as YY,";
        sql = sql + "    left(convert(nvarchar,ReturnDate,106),2) as DD1,Right(left(convert(nvarchar,ReturnDate,106),6),3) as MM1 , RIGHT(convert(nvarchar,ReturnDate,106),4) as YY1,";
        sql = sql + "   left(convert(nvarchar,ActualReturnDate,106),2) as DD2,Right(left(convert(nvarchar,ActualReturnDate,106),6),3) as MM2 , RIGHT(convert(nvarchar,ActualReturnDate,106),4) as YY2,";
        sql = sql + "    BookStatus,RenewalCost,FinePayable,FinePaid,Balance,Remark,SubCategory from IssueReturnLibraryItem  where AccessionNo='" + txtAxxessionNo.Text.ToString() + "'";
        sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

      //  txtAddress.Text = oo.ReturnTag(sql, "Address");
        txtAuthor1.Text = oo.ReturnTag(sql, "Author1");
        txtAuthor2.Text = oo.ReturnTag(sql, "Author2");
        txtBalance.Text = oo.ReturnTag(sql, "Balance");
        drpcategory.Text = oo.ReturnTag(sql, "Category");
        drpSubCate.Text = oo.ReturnTag(sql, "SubCategory").Trim();
        //txtEmail.Text = oo.ReturnTag(sql, "Email");
        txtFinePaid.Text = oo.ReturnTag(sql, "FinePaid");
        txtFinePayable.Text = oo.ReturnTag(sql, "FinePayable");
        //txtMemberId.Text = oo.ReturnTag(sql, "MemberId");
        //txtName.Text = oo.ReturnTag(sql, "Name");
        //txtPhoneNo.Text = oo.ReturnTag(sql, "PhoneNo");
        txtRemark.Text = oo.ReturnTag(sql, "Remark");
        txtRenewalCost.Text = oo.ReturnTag(sql, "RenewalCost");
        txtSubject.Text = oo.ReturnTag(sql, "Subject");
        txtTitle.Text = oo.ReturnTag(sql, "Title");
        DrpIsseDD.Text = oo.ReadDD(oo.ReturnTag(sql, "DD"));
        DrpIsseMM.Text = oo.ReturnTag(sql, "MM");

        DrpIsseYY.Text = oo.ReturnTag(sql, "YY");
        DrpAcRetnDD.Text = oo.ReadDD(oo.ReturnTag(sql, "DD2"));
        DrpAcRetnMM.Text = oo.ReturnTag(sql, "MM2");
        DrpAcRetnYY.Text = oo.ReturnTag(sql, "YY2");
        DrpRetDD.Text = oo.ReadDD(oo.ReturnTag(sql, "DD1"));
        DrpRetMM.Text = oo.ReturnTag(sql, "MM1");
        DrpRetYY.Text = oo.ReturnTag(sql, "YY1");
        try
        {
            drpBookStatus.Text = oo.ReturnTag(sql, "BookStatus");
        }

        catch (Exception) { }

        //sql = "select distinct PhotoPath from StudentDocuments  where srno='" + txtMemberId.Text + "'  and sessionName='" + Session["SessionName"] + "'";
        //Image1.ImageUrl = oo.ReturnTag(sql, "PhotoPath");



        //txtAddress.ReadOnly = true;
        txtAuthor1.ReadOnly = true;
        txtAuthor2.ReadOnly = true;
        txtBalance.ReadOnly = true;
        //txtCategory.ReadOnly = true;
       // txtEmail.ReadOnly = true;
        // txtFinePaid.ReadOnly = true;
        txtFinePayable.ReadOnly = true;
        //txtMemberId.ReadOnly = true;
        //txtName.ReadOnly = true;
        //txtPhoneNo.ReadOnly = true;
        txtRemark.ReadOnly = true;
        txtRenewalCost.ReadOnly = true;
        txtSubject.ReadOnly = true;
        txtTitle.ReadOnly = true;
        //DrpIsseDD.Text = oo.ReturnTag(sql, "DD");
        //DrpIsseMM.Text = oo.ReturnTag(sql, "MM");
        //DrpIsseYY.Text = oo.ReturnTag(sql, "YY");
        //DrpAcRetnDD.Text = oo.ReturnTag(sql, "DD1");
        //DrpAcRetnMM.Text = oo.ReturnTag(sql, "MM1");
        //DrpAcRetnYY.Text = oo.ReturnTag(sql, "YY1");



        sql = "Select typeofPerson from MemberEntryLibrary where MemberCode='" + TxtEnter.Text + "' and BranchCode = " + Session["BranchCode"] + "";
        if (oo.ReturnTag(sql, "typeofperson").Trim() == "Faculty")
        {
            FindReturnDateFaculty();
        }
        else
        {
            FindReturnDate();
        }
        }

    public void FindReturnDateFaculty()
    {

        string sql1 = "";


        //Find the Total No's of Days Book Return to Library 
        //Start Logic
        //Find the Current Date
        string sql2 = "select CONVERT(NVARCHAR,Getdate(),106) as TodayDate ";
        string TDate = oo.ReturnTag(sql2, "TodayDate");
        string sql3 = "";
        try
        {
            sql1 = "select FineForLibrary,MaximumItemsLibrary,DaysReturn,MembershipValidity,Remark  from MemberCategoryFaculty where BranchCode = " + Session["BranchCode"] + "";



            Session["FinePerDay"] = oo.ReturnTag(sql1, "FineForLibrary");
            sql3 = "select CONVERT(NVARCHAR, convert(smalldatetime,'" + TDate + "')+" + oo.ReturnTag(sql1, "DaysReturn") + " ,106) as ReturnDate";
            string dt = "";
            string DD = "", mm = "", yy = "";
            dt = oo.ReturnTag(sql3, "ReturnDate");
            DD = oo.ReadDD(dt.Substring(0, 2));
            mm = dt.Substring(3, 3);
            yy = dt.Substring(6, 5);


            DrpRetYY.Text = yy.Trim();
            DrpRetMM.Text = mm.Trim();
            DrpRetDD.Text = DD.Trim();

            DrpAcRetnYY.Text = yy.Trim();
            DrpAcRetnMM.Text = mm.Trim();
            DrpAcRetnDD.Text = DD.Trim();



        }
        catch (Exception) { }
        //End Logic

    }
 protected void txtAxxessionNo_TextChanged(object sender, EventArgs e)
    {
        //sql = "Select AccessionNo from LibraryItemEntry where AccessionNo='" + txtAxxessionNo.Text.ToString() + "'";
        //if (oo.Duplicate(sql))
        //{
        //    Label1.Text = "Item Already Issued";
        //}
        //else
        //{
        //    Label1.Text = "";
        //}
    }
 
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
#pragma warning disable 219
        string card = "";
#pragma warning restore 219
        sql = "select top 1 sg.FirstName+' '+sg.MiddleName+' '+sg.LastName as StudentName,so.Medium as Medium, SFD.FatherName as FatherName ,SC.SectionName  as SectionName, sg.StLocalAddress as StLocalAddress,me.SrNo as SrNo,me.StEnRCode as StEnRCode,sg.MobileNumber  as MobileNumber,sg.Email as Email,cm.ClassName  as ClassName ,sd.PhotoPath  as PhotoPath";
        sql = sql + "  from StudentOfficialDetails so  ";
        sql = sql + " left join StudentGenaralDetail sg on so.SrNo=sg.SrNo  ";
        sql = sql + " left join ClassMaster cm on so.AdmissionForClassId=cm.Id ";
        sql = sql + "   left join SectionMaster SC on so.SectionId=SC.Id";
        sql = sql + " left join StudentDocuments sd on so.SrNo=sd.SrNo  ";
        sql = sql + " left join StudentFamilyDetails SFD on so.SrNo=SFD.SrNo ";
        sql = sql + " left join MemberEntryLibrary me on so.srno=me.srno";
        sql = sql + "    where me.MemberCode='" + TxtEnter.Text.ToString() + "'";
        sql = sql + " and sg.SessionName='" + Session["SessionName"].ToString() + "' and sg.BranchCode=" + Session["BranchCode"].ToString() + "";
        sql = sql + "  and so.Promotion is null";

        Grd.DataSource = oo.GridFill(sql);
        Grd.DataBind();
        Panel3.Visible = true;
        if (Grd.Rows.Count == 0)
        {
            //oo.MessageBox("Sorry, No Record(s) found!", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, No Record(s) found!", "A");       

            Panel1.Visible = false; 
            Panel3.Visible = false;

        
        }
    }
    protected void DrpEnter_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void TxtEnter_TextChanged(object sender, EventArgs e)
    {

    }
    protected void TextBox2_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtTitle_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtAuthor1_TextChanged(object sender, EventArgs e)
    {

    }
    protected void Status_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtAuthor2_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtSubject_TextChanged(object sender, EventArgs e)
    {

    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        if (drpBookStatus.SelectedItem.ToString() == "<--Select-->" && Panel1.Visible == true)
        {
            //oo.MessageBox("Please Select Condition", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please Select Condition", "A");       

        }
        else
        {

            sql = "select top 1 sg.FirstName+' '+sg.MiddleName+' '+sg.LastName as StudentName,so.Medium as Medium,me.MemberCode as MemberCode, SFD.FatherName as FatherName ,";
            sql = sql + "   SC.SectionName  as SectionName, sg.StLocalAddress as StLocalAddress,me.SrNo as SrNo,me.StEnRCode as StEnRCode,sg.MobileNumber  as MobileNumber,sg.Email as Email,";
            sql = sql + "  cm.ClassName  as ClassName ,sd.PhotoPath  as PhotoPath,sg.StPerAddress as StPerAddress";
            sql = sql + "  from StudentOfficialDetails so  ";
            sql = sql + " left join StudentGenaralDetail sg on so.SrNo=sg.SrNo  ";
            sql = sql + " left join ClassMaster cm on so.AdmissionForClassId=cm.Id ";
            sql = sql + "   left join SectionMaster SC on so.SectionId=SC.Id";
            sql = sql + " left join StudentDocuments sd on so.SrNo=sd.SrNo  ";
            sql = sql + " left join StudentFamilyDetails SFD on so.SrNo=SFD.SrNo ";
            sql = sql + " left join MemberEntryLibrary me on so.srno=me.srno";
            sql = sql + "    where me.MemberCode='" + TxtEnter.Text.ToString() + "'";
            sql = sql + " and sg.SessionName='" + Session["SessionName"].ToString() + "' and sg.BranchCode=" + Session["BranchCode"].ToString() + "";
            sql = sql + "  and so.Promotion is null";

            string Member = "";
            Member = oo.ReturnTag(sql, "MemberCode");
            string Name = "";
            Name = oo.ReturnTag(sql, "StudentName");
            string address = "";
            address = oo.ReturnTag(sql, "StPerAddress");
            string phoneno = "";
            phoneno = oo.ReturnTag(sql, "MobileNumber");
            string Email = "";
            Email = oo.ReturnTag(sql, "Email");





            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "IssueReturnLibraryItemProce";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@AccessionNo", txtAxxessionNo.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@Title", txtTitle.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@Subject", txtSubject.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@Author1", txtAuthor1.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@Author2", txtAuthor2.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@Status", Status.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@MemberId", Member.Trim());
            cmd.Parameters.AddWithValue("@Category", drpcategory.Text.Trim().ToString());

            cmd.Parameters.AddWithValue("@Name", Name.Trim());
            cmd.Parameters.AddWithValue("@Address", address.Trim());
            cmd.Parameters.AddWithValue("@PhoneNo", phoneno.Trim());
            cmd.Parameters.AddWithValue("@Email", Email.Trim());
            string IssDate = "";
            IssDate = DrpIsseYY.SelectedItem.ToString() + "/" + DrpIsseMM.SelectedItem.ToString() + "/" + DrpIsseDD.SelectedItem.ToString();

            cmd.Parameters.AddWithValue("@IssueDate", IssDate);
            string ReturDate = "";
            ReturDate = DrpRetYY.SelectedItem.ToString() + "/" + DrpRetMM.SelectedItem.ToString() + "/" + DrpRetDD.SelectedItem.ToString();
            cmd.Parameters.AddWithValue("@ReturnDate", ReturDate);

            string ActualDate = "";
            ActualDate = DrpAcRetnYY.SelectedItem.ToString() + "/" + DrpAcRetnMM.SelectedItem.ToString() + "/" + DrpAcRetnDD.SelectedItem.ToString();
            cmd.Parameters.AddWithValue("@ActualReturnDate", ActualDate);

            if (Status.Text == "Issue")
            {
                cmd.Parameters.AddWithValue("@BookStatus", "");
                cmd.Parameters.AddWithValue("@RenewalCost", "0.00");
                cmd.Parameters.AddWithValue("@FinePayable", "0.00");
                cmd.Parameters.AddWithValue("@FinePaid", "0.00");
                cmd.Parameters.AddWithValue("@Balance", "0.00");
            }
            else
            {

                cmd.Parameters.AddWithValue("@BookStatus", drpBookStatus.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@RenewalCost", txtRenewalCost.Text.Trim().ToString());
                cmd.Parameters.AddWithValue("@FinePayable", txtFinePayable.Text.Trim().ToString());
                cmd.Parameters.AddWithValue("@FinePaid", txtFinePaid.Text.Trim().ToString());
                cmd.Parameters.AddWithValue("@Balance", txtBalance.Text.Trim().ToString());
            }
            cmd.Parameters.AddWithValue("@Remark", txtRemark.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());//Session["Login"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
            cmd.Parameters.AddWithValue("@SubCategory", drpSubCate.Text.Trim().ToString());
            cmd.Connection = con;
            try
            {

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                //oo.MessageBox("Submitted successfully.", this.Page);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");       

                oo.ClearControls(this.Page);


                Label1.Text = "";

            }
            catch (Exception) { }
        }
    }
    public void FindReturnDate()
    {

        //sql = "select top 1 sg.FirstName+' '+sg.MiddleName+' '+sg.LastName as StudentName,sg.StLocalAddress as StLocalAddress,sg.MobileNumber  as MobileNumber,sg.Email as Email,cm.ClassName  as ClassName ,sd.PhotoPath  as PhotoPath";
        //sql = sql + "  from StudentOfficialDetails so  ";
        //sql = sql + " left join StudentGenaralDetail sg on so.SrNo=sg.SrNo  ";
        //sql = sql + " left join ClassMaster cm on so.AdmissionForClassId=cm.Id ";
        //sql = sql + " left join StudentDocuments sd on so.SrNo=sd.SrNo  ";
        //sql = sql + " left join MemberEntryLibrary me on so.srno=me.srno";
        //sql = sql + "    where me.MemberCode='" + txtMemberId.Text.ToString() + "'";
        //sql = sql + " and sg.SessionName='" + Session["SessionName"].ToString() + "' and sg.BranchCode=" + Session["BranchCode"].ToString() + "";
        //sql = sql + "  and so.Promotion is null";




        string sql1 = "";
        //Find the Total No's of Days Book Return to Library 
        //Start Logic
        //Find the Current Date6
        string sql2 = "select CONVERT(NVARCHAR,Getdate(),106) as TodayDate ";
        string TDate = oo.ReturnTag(sql2, "TodayDate");
        string sql3 = "";
        try
        {
            sql1 = "select CategoryName  ,CautionMoneyLibrary  ,   CautionMoneyBookBank  ,MonthlychargeLibrary  ,FineLibraryPerDay ,   FineBookBankPerDay  ,MaxItemLibrary  ,";
            sql1 = sql1 + " MaxItemBookBank  ,   DaysReturnLibrary ,DaysReturnBookBank  ,MembershipValidityinMonth  ,Remark ,DaysReturnLibrary  from MemberCategoryLibrary ";
            sql1 = sql1 + " where CategoryName='" + drpSubCate.Text.ToString() + "' and BranchCode = " + Session["BranchCode"] + "";

            Session["FineBookBankPerDay"] = oo.ReturnTag(sql1, "FineBookBankPerDay");
            sql3 = "select CONVERT(NVARCHAR, convert(smalldatetime,'" + TDate + "')+" + oo.ReturnTag(sql1, "DaysReturnBookBank") + " ,106) as ReturnDate";
            string dt = "";
            string DD = "", mm = "", yy = "";
            dt = oo.ReturnTag(sql3, "ReturnDate");
            DD = oo.ReadDD(dt.Substring(0, 2));
            mm = dt.Substring(3, 3);
            yy = dt.Substring(6, 5);


            DrpRetYY.Text = yy.Trim();
            DrpRetMM.Text = mm.Trim();
            DrpRetDD.Text = DD.Trim();

            DrpAcRetnYY.Text = yy.Trim();
            DrpAcRetnMM.Text = mm.Trim();
            DrpAcRetnDD.Text = DD.Trim();



        }
        catch (Exception) { }
        //End Logic

    }
   
   
    protected void DrpIsseYY_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(DrpIsseYY, DrpIsseMM, DrpIsseDD);
    }
    protected void DrpIsseMM_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(DrpIsseYY,DrpIsseMM,DrpIsseDD);
    }
    protected void DrpIsseDD_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void DrpAcRetnYY_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(DrpAcRetnYY, DrpAcRetnMM, DrpAcRetnDD);
        Finecalculatuion();
    }
    protected void DrpAcRetnMM_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(DrpAcRetnYY, DrpAcRetnMM, DrpAcRetnDD);
        Finecalculatuion();
        totalfine();
    }
    protected void DrpAcRetnDD_SelectedIndexChanged(object sender, EventArgs e)
    {
        Finecalculatuion();
        totalfine();
    }
    protected void DrpRetYY_SelectedIndexChanged(object sender, EventArgs e)
    {

        oo.YearDropDown(DrpRetYY, DrpRetMM, DrpRetDD);
    }
    protected void DrpRetMM_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(DrpRetYY, DrpRetMM, DrpRetDD);
    }
    protected void DrpRetDD_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void drpSubCate_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void drpBookStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        sql = "Select RenewalCost from BookStatusMaster where BookStatus='" + drpBookStatus.SelectedItem.ToString() + "' and BranchCode = " + Session["BranchCode"] + "";
        txtRenewalCost.Text = oo.ReturnTag(sql, "RenewalCost");
        txtFinePayable.Text = "0";
        txtBalance.Text = "0";
        Finecalculatuion();
        totalfine();
    }
    protected void txtRemark_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtRenewalCost_TextChanged(object sender, EventArgs e)
    {
        Finecalculatuion();
        totalfine();
    }
    protected void txtBalance_TextChanged(object sender, EventArgs e)
    {
        Finecalculatuion();
        totalfine();
    }
    protected void txtFinePayable_TextChanged(object sender, EventArgs e)
    {
        Finecalculatuion();
        totalfine();
    }
    protected void txtFinePaid_TextChanged(object sender, EventArgs e)
    {
    double dd = 0;
    dd = Convert.ToDouble(txtFinePayable.Text) + Convert.ToDouble(txtRenewalCost.Text);
    dd = dd - Convert.ToDouble(txtFinePaid.Text);


    txtBalance.Text = dd.ToString(CultureInfo.InvariantCulture);
  //  totalfine();
}
   
    public void Finecalculatuion()
    {
        try
        {
            DateTime Returndate = new DateTime(Convert.ToInt32(DrpRetYY.Text.ToString()), Convert.ToInt32(findMonthValue(DrpRetMM.Text.ToString())), Convert.ToInt32(DrpRetDD.Text.ToString()));

            DateTime ActualReturnDate = new DateTime(Convert.ToInt32(DrpAcRetnYY.Text.ToString()), Convert.ToInt32(findMonthValue(DrpAcRetnMM.Text.ToString())), Convert.ToInt32(DrpAcRetnDD.SelectedItem.ToString()));


            double days = 0;
            double amt = 0;
            TimeSpan ts = ActualReturnDate.Subtract(Returndate);
            days = ts.Days;
            if (Convert.ToInt32(days) > 0)
            {
                amt = days * Convert.ToDouble(Session["FineBookBankPerDay"].ToString());
            }
            else
            {
                amt = 0;
            }
            txtFinePaid.Text = txtFinePayable.Text = amt.ToString(CultureInfo.InvariantCulture);
        }
        catch (Exception) { txtFinePayable.Text = "0"; }

    }
    public int findMonthValue(string ss)
    {
        int kk = 0;
        if (ss == "Jan")
        {
            kk = 1;
        }
        if (ss == "Feb")
        {
            kk = 2;
        }

        if (ss == "Mar")
        {
            kk = 3;
        }

        if (ss == "Apr")
        {
            kk = 4;
        }
        if (ss == "May")
        {
            kk = 5;
        }

        if (ss == "Jun")
        {
            kk = 6;
        }
        if (ss == "Jul")
        {
            kk = 7;
        }

        if (ss == "Aug")
        {
            kk = 8;
        }

        if (ss == "Sep")
        {
            kk = 9;
        }
        if (ss == "Oct")
        {
            kk = 10;
        }
        if (ss == "Nov")

        {
            kk = 11;
        }
        if (ss == "Dec")
        { 
            kk = 12;
        } 

        return kk;

    }
    public void totalfine()
    {
#pragma warning disable 168
        double a, b, c, d;
#pragma warning restore 168
        try
        {
            a = Convert.ToDouble(txtRenewalCost.Text);

        }
        catch (Exception) { a = 0; }

        try
        {
            c = Convert.ToDouble(txtFinePayable.Text);
        }
        catch (Exception) { c = 0; }
        d = a + c;
        txtFinePaid.Text = d.ToString(CultureInfo.InvariantCulture);




    }
    protected void drpcategory_TextChanged(object sender, EventArgs e)
    {

    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        


                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "TempLibraryBookBankProc";
                cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@AccessionNo", txtAxxessionNo.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@Title", txtTitle.Text.Trim().ToString());
            //cmd.Parameters.AddWithValue("@Subject", txtSubject.Text.ToString());
            cmd.Parameters.AddWithValue("@Author1", txtAuthor1.Text.Trim().ToString());
         
            cmd.Parameters.AddWithValue("@Category", drpcategory.Text.Trim().ToString());

            string IssDate = "";
            IssDate = DrpIsseYY.SelectedItem.ToString() + "/" + DrpIsseMM.SelectedItem.ToString() + "/" + DrpIsseDD.SelectedItem.ToString();

            cmd.Parameters.AddWithValue("@IssueDate", IssDate);
          

            string ActualDate = "";
            ActualDate = DrpAcRetnYY.SelectedItem.ToString() + "/" + DrpAcRetnMM.SelectedItem.ToString() + "/" + DrpAcRetnDD.SelectedItem.ToString();
            cmd.Parameters.AddWithValue("@ActualReturnDate", ActualDate.Trim());
            cmd.Parameters.AddWithValue("@SubCategory", drpSubCate.Text.Trim().ToString());
            cmd.Connection = con;
            try
            {

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                sql = "Select   Row_Number() over (order by Id Asc) as SrNo,  Id,AccessionNo , Title ,Author ,Category ,SubCategory ,IssueDate ,ReturnDate from TempLibraryBookBank where AccessionNo='" + txtAxxessionNo.Text.ToString() + "' and BranchCode = " + Session["BranchCode"] + "";
                GridView1.DataSource = oo.GridFill(sql);
                GridView1.DataBind();
            }
            catch (Exception) { }
    }



    public void PermissionGrant(int add1, LinkButton Ladd,LinkButton Ladd2)
    {


        if (add1 == 1)
        {
            Ladd.Enabled = true;
            Ladd2.Enabled = true;
        }
        else
        {
            Ladd.Enabled = false;
            Ladd2.Enabled = false;
        }

    }
    public void CheckValueADDDeleteUpdate()
    {
        sql = " select LoginId,LoginName,Pass,SessionId,BranchId,LT.LoginTypeName,ltb.add1 as add1,ltb.delete1 as delete1,ltb.update1 as update1 from LoginTab LTb";
        sql = sql + " left join LoginType Lt on LTb.LoginTypeId=Lt.LoginTypeId where LT.LoginTypeName='" + Session["Logintype"] + "' and LTb.LoginName='" + Session["LoginName"] + "' and LTb.BranchId = " + Session["BranchCode"] + "";
#pragma warning disable 168
        int a, u, d;
#pragma warning restore 168
        a = Convert.ToInt32(oo.ReturnTag(sql, "add1"));


        PermissionGrant(a, (LinkButton)LinkButton4, LinkButton3);
    }



}