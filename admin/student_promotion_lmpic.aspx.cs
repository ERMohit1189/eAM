using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;

public partial class admin_student_promotion_lmpic : Page
{
    SqlConnection con = new SqlConnection();
    SqlConnection con1 = new SqlConnection();
    Campus oo = new Campus(); 
    string sql = string.Empty;
    string wherecondition = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        drpSession.Focus();

        string CCode = string.Empty;
       
        if ( Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        con = oo.dbGet_connection();
        con1 = oo.dbGet_connection();
      
        string Sess = string.Empty;
        int yy1 = 0, yy2 = 0 ;
        yy1 = Convert.ToInt32(Session["SessionName"].ToString().Substring(0, 4)); 
        yy1 = yy1 - 1;
        yy2 = Convert.ToInt32(Session["SessionName"].ToString().Substring(5, 4));
        yy2 = yy2 - 1;
        Sess = yy1.ToString() + "-" + yy2.ToString();
        Campus camp = new Campus(); camp.LoadLoader(loader);
            if (!IsPostBack)
            {
                lblRedIndicate.Visible = false;
                try
                {
                    CheckValueADDDeleteUpdate();
                }
                catch (Exception) { }

                sql = "Select SessionName from SessionMaster";
                sql = sql + " where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
                oo.FillDropDownWithOutSelect(sql, drpSessionNew, "SessionName");
                sql = "Select SessionName from SessionMaster";

                sql = sql + " where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
                string sessValue = "", ff = "";
                sessValue = oo.ReturnTag(sql, "SessionName");
                int s1, s2;
                s1 = Convert.ToInt32(sessValue.Substring(0, 4));
                s2 = Convert.ToInt32(sessValue.Substring(5, 4));
                s1 = s1 + 1;
                s2 = s2 + 1;
                ff = s1.ToString() + "-" + s2.ToString();

                drpSessionNew.SelectedItem.Text = ff.ToString();

               


                sql = "Select SessionName from SessionMaster";
                sql = sql + " where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
                oo.FillDropDownWithOutSelect(sql, drpSession, "SessionName");

                loadFeeGroup();
                loadClass();
                loadBranch();
                loadSection();
            
                Panel2.Visible = false;

                LinkSubmit.Enabled = false;


            }

    
    }
    string sql2 = "";

    private void loadFeeGroup()
    {
        sql = "Select FeeGroupName,Id from FeeGroupMaster where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        oo.FillDropDown_withValue_withSelect(sql, DrpFeeGroup, "FeeGroupName", "Id");
    }

    private void loadClass()
    {
        sql = "Select Id,ClassName from ClassMaster";
        sql = sql + " where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and CIDOrder !=0 ";
        oo.FillDropDown_withValue(sql, DrpClass , "ClassName", "Id");
        DrpClass.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
    }

    private void loadBranch()
    {
        sql = "Select BranchName,Id from BranchMaster";
        sql = sql + " where (ClassId='" + DrpClass.SelectedValue.ToString() + "' or ClassId is NULL) and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

        oo.FillDropDown_withValue(sql, DropBranch, "BranchName", "Id");
        DropBranch.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
    }

    private void loadSection()
    {
        sql = "select SectionName from SectionMaster where ClassNameId='" + DrpClass.SelectedValue.ToString() + "'";
        sql = sql + "  and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        oo.FillDropDown(sql, drpSection, "SectionName");
    }

    public void arrier_add()
    {
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            Label Label13 = (Label)GridView1.Rows[i].FindControl("Label13");
            Label lblBalanceAmt = (Label)GridView1.Rows[i].FindControl("lblBalanceAmt");
            sql2 = "select COUNT(ArrierId) as Count,SUM(ArrearAmt) as Arrier from ArrierMast where Srno='" + Label13.Text + "'";
            if (Convert.ToInt32(oo.ReturnTag(sql2, "Count")) != 0)
            {
                if (Convert.ToDouble(oo.ReturnTag(sql2, "Arrier")) != 0)
                {
                    lblBalanceAmt.Text = (Convert.ToDouble(lblBalanceAmt.Text) + Convert.ToDouble(oo.ReturnTag(sql2, "Arrier"))).ToString();
                }
            }
        }
    }

    public void WithSelect()
    {
        try
        {
            if (DrpClass.SelectedItem.Text != "<--Select-->" && drpSection.SelectedItem.Text != "<--Select-->")
            {
                wherecondition = " where SC.SectionName='" + drpSection.SelectedItem.ToString() + "' and  CM.ClassName='" + DrpClass.SelectedItem.ToString() + "'";
            }

            else if (DrpClass.SelectedItem.Text == "<--Select-->" && drpSection.SelectedItem.Text != "<--Select-->")
            {
               
                oo.MessageBox("Please Select Class Name!", this.Page);
            }
            else if (DrpClass.SelectedItem.Text != "<--Select-->" && drpSection.SelectedItem.Text == "<--Select-->")
            {
              
                wherecondition = " where CM.ClassName='" + DrpClass.SelectedItem.ToString() + "'";

            }
        }
        catch (Exception) { }

    }
       
    protected void DrpClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadBranch();
        loadSection();
    }

    protected void DrpClassNew_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadNewSessionBranch();
        loadNewSessionSection();
    }

    private void loadNewSessionCourse()
    {
        sql = "Select CourseName,Id from CourseMaster where SessionName='" + drpSessionNew.SelectedItem.Text.ToString() + "'";
        oo.FillDropDown_withValue(sql, DropCourseNew, "CourseName", "Id");
        DropCourseNew.Items.Insert(0, new ListItem("<--Select Course-->", "0"));

    }

    private void loadNewSessionBranch()
    {
        sql = "Select BranchName,Id from BranchMaster";
        sql = sql + " where (ClassId='" + DrpClassNew.SelectedValue.ToString() + "' or ClassId is NULL) and SessionName='" + drpSessionNew.SelectedItem.Text.ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

        oo.FillDropDown_withValue(sql, DrpBranchNew, "BranchName", "Id");
        DrpBranchNew.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
    }

    private void loadNewSessionSection()
    {
        sql = "select SectionName,Id from SectionMaster where ClassNameId='" + DrpClassNew.SelectedValue.ToString() + "'";
        sql = sql + "  and SessionName='" + drpSessionNew.SelectedItem.Text.ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        oo.FillDropDown_withValue(sql, drpSectionNew, "SectionName", "Id");
        drpSectionNew.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
    }

    public void StudentPromotionStudentGenaralDetail()
    {
        string StEnRCode = "";
        int i = 0;
        string sql1 = "";
        for (i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            Label LbRegNo = (Label)GridView1.Rows[i].Cells[1].FindControl("Label12");
            CheckBox chkBox = (CheckBox)GridView1.Rows[i].Cells[8].FindControl("CheckBox1");
            Label lblSrNo = (Label)GridView1.Rows[i].Cells[1].FindControl("Label13");
  
            DropDownList drpstatus = (DropDownList)GridView1.Rows[i].FindControl("drpstatus");

            if (drpstatus.Text.ToUpper() == "PENDING" || drpstatus.Text.ToUpper() == "Left")
            {
            }
            else
            {
                    sql1 = "select * from StudentGenaralDetail where SrNo='" + lblSrNo.Text + "'  and SessionName='" + drpSessionNew.SelectedItem.ToString() + "'";
                    if (oo.Duplicate(sql1) == false)
                    {

                        int co = 0;
                        sql = "select max(id)  as id from StudentGenaralDetail";

                        co = Convert.ToInt32(oo.ReturnTag(sql, "id").ToString());
                        co = co + 1;
                        StEnRCode = IDGeneration(co.ToString());


                        sql = "  insert into StudentGenaralDetail(Id,StEnRCode ,SrNo,FirstName,MiddleName,LastName,DOB,Gender,Email,   ";
                        sql = sql + "  MobileNumber,SiblingCategory,SBSrNo,SBStName,SBFathersName,SBClass,SBSection,PhysicallDisabledCategory,PhyStName,  ";
                        sql = sql + "  PhyStDetail,StLocalAddress,StLocalStateId,StLocalCityId,StLocalZip,StPerAddress,StPerStateId,StPerCityId,StPerZip,  ";
                        sql = sql + "  BranchCode ,LoginName ,SessionName,RecordDate,Religion,Nationality,Category,Caste,BloodGroup,HouseName )  ";
                        sql = sql + "  select  " + co + ",'" + StEnRCode + "' ,SrNo,FirstName,MiddleName,LastName,DOB,Gender,Email, ";
                        sql = sql + " MobileNumber,SiblingCategory,SBSrNo,SBStName,SBFathersName,SBClass,SBSection,PhysicallDisabledCategory,PhyStName,  ";
                        sql = sql + "  PhyStDetail,StLocalAddress,StLocalStateId,StLocalCityId,StLocalZip,StPerAddress,StPerStateId,StPerCityId,StPerZip,  ";
                        sql = sql + "  BranchCode ,'" + Session["LoginName"] + "','" + drpSessionNew.SelectedItem.ToString() + "',getdate(),Religion,Nationality,Category,Caste,BloodGroup,HouseName   from StudentGenaralDetail  ";
                        sql = sql + "  where srno='" + lblSrNo.Text + "' and  SessionName='" + Session["SessionName"].ToString() + "'";

                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandText = sql;
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        try
                        {
                            if (chkBox.Checked == true)
                            {

                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                        catch (SqlException) { con.Close(); }
                    }
                }
                          
        }

    }

    public void StudentPromotionStudentFamilyDetails()
    {
        int i = 0;
        string StEnRcode = "";
        for (i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            Label LbRegNo = (Label)GridView1.Rows[i].Cells[1].FindControl("Label12");
            Label lblSrNo = (Label)GridView1.Rows[i].Cells[1].FindControl("Label13");
            CheckBox chkBox = (CheckBox)GridView1.Rows[i].Cells[8].FindControl("CheckBox1");
            DropDownList drpstatus = (DropDownList)GridView1.Rows[i].FindControl("drpstatus");

            if (drpstatus.Text.ToUpper() == "PENDING" || drpstatus.Text.ToUpper() == "Left")
            {
            }
            else
            {
                sql1 = "select * from StudentFamilyDetails where SrNo='" + lblSrNo.Text + "'  and SessionName='" + drpSessionNew.SelectedItem.ToString() + "'";
                 if (oo.Duplicate(sql1) == false)
                 {
                     int co = 0;
                     sql = "select max(id)  as id from StudentFamilyDetails";
                     co = Convert.ToInt32(oo.ReturnTag(sql, "id"));
                     co = co + 1;
                     StEnRcode = IDGeneration(co.ToString());


                     sql = "  insert into StudentFamilyDetails(Id,StEnRCode,SrNo,FatherOccupation,FatherDesignation,FatherQualification,FatherIncomeMonthly,  ";
                     sql = sql + "  FatherOfficeAddress,FatherContactNo,FatherEmail,FamilyIncomeMonthly,FamilyRelationship,FamilyEmail,FamilyGuardianName,FamilyContactNo,";
                     sql = sql + " MotherOccupation,MotherDesignation,MotherQualification,MotherIncomeMonthly,MotherOfficeAddress,MotherContactNo,MotherEmail,";
                     sql = sql + "  BranchCode,LoginName,SessionName,RecordDate,FatherName,MotherName)";
                     sql = sql + "  Select  " + co + ",'" + StEnRcode + "',SrNo,FatherOccupation,FatherDesignation,FatherQualification,FatherIncomeMonthly,";
                     sql = sql + "  FatherOfficeAddress,FatherContactNo,FatherEmail,FamilyIncomeMonthly,FamilyRelationship,FamilyEmail,FamilyGuardianName,FamilyContactNo,";
                     sql = sql + "  MotherOccupation,MotherDesignation,MotherQualification,MotherIncomeMonthly,MotherOfficeAddress,MotherContactNo,MotherEmail,";
                     sql = sql + "  BranchCode ,'" + Session["LoginName"] + "','" + drpSessionNew.SelectedItem.ToString() + "',getdate(), FatherName,MotherName from StudentFamilyDetails  ";
                     sql = sql + "  where Srno='" + lblSrNo.Text + "'  and SessionName='" + Session["SessionName"].ToString() + "'";
                     SqlCommand cmd = new SqlCommand();
                     cmd.CommandText = sql;
                     cmd.CommandType = CommandType.Text;
                     cmd.Connection = con;
                     try
                     {
                         if (chkBox.Checked == true)
                         {
                             con.Open();
                             cmd.ExecuteNonQuery();
                             con.Close();
                         }

                     }
                     catch (SqlException) { con.Close(); }
                 }
            }
        }
    }

    public void StudentPromotionStudentPreviousSchool()
    {
         int i = 0;
         string StEnRCode = "";
         for (i = 0; i <= GridView1.Rows.Count - 1; i++)
         {
             Label LbRegNo = (Label)GridView1.Rows[i].Cells[1].FindControl("Label12");
             Label lblSrNo = (Label)GridView1.Rows[i].Cells[1].FindControl("Label13");
             CheckBox chkBox = (CheckBox)GridView1.Rows[i].Cells[8].FindControl("CheckBox1");
             DropDownList drpstatus = (DropDownList)GridView1.Rows[i].FindControl("drpstatus");

             if (drpstatus.Text.ToUpper() == "PENDING" || drpstatus.Text.ToUpper() == "Left")
             {
             }
             else
             {
                 sql1 = "select * from StudentPreviousSchool where SrNo='" + lblSrNo.Text + "'  and SessionName='" + drpSessionNew.SelectedItem.ToString() + "'";
                  if (oo.Duplicate(sql1) == false)
                  {
                      int co = 0;
                      sql = "select max(id)  as id from StudentPreviousSchool";
                      if (oo.ReturnTag(sql, "id").ToString() != "")
                      {
                          co = Convert.ToInt32(oo.ReturnTag(sql, "id").ToString());
                      }
                      else
                      {
                          co = 0;
                      }

                      co = co + 1;

                      StEnRCode = IDGeneration(co.ToString());

                      sql = "  insert into StudentPreviousSchool(Id,StEnRCode,SrNo,CountryId,StateId,CityId,Board,Medium,Class,FromDate,ToDate,Marks,Percentage,SchoolName,Schooladdress,BranchCode ,LoginName ,SessionName ,RecordDate)";
                      sql = sql + "  Select  " + co + ",'" + StEnRCode + "',SrNo,CountryId,StateId,CityId,Board,Medium,Class,FromDate,ToDate,Marks,Percentage,SchoolName,Schooladdress, ";
                      sql = sql + "  BranchCode ,'" + Session["LoginName"] + "','" + drpSessionNew.SelectedItem.ToString() + "',getdate() from StudentPreviousSchool ";
                      sql = sql + "  where srno='" + lblSrNo.Text + "'  and SessionName='" + Session["SessionName"].ToString() + "'";



                      SqlCommand cmd = new SqlCommand();
                      cmd.CommandText = sql;
                      cmd.CommandType = CommandType.Text;
                      cmd.Connection = con;
                      try
                      {
                          if (chkBox.Checked == true)
                          {

                              con.Open();
                              cmd.ExecuteNonQuery();
                              con.Close();
                          }
                      }
                      catch (SqlException) { con.Close(); }
                  }
             }
         }
    }
    
    public void StudentPromotionStudentOfficialDetails()
    {
        int i = 0;
        string StEnRCode = "";
        string sec = "", classId = "";
        for (i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            Label LbRegNo = (Label)GridView1.Rows[i].Cells[1].FindControl("Label12");
            CheckBox chkBox = (CheckBox)GridView1.Rows[i].Cells[8].FindControl("CheckBox1");
            DropDownList drpstatus = (DropDownList)GridView1.Rows[i].FindControl("drpstatus");
            Label lblSrNo = (Label)GridView1.Rows[i].Cells[1].FindControl("Label13");
            sql = "update StudentOfficialDetails set Promotion='" + drpstatus.SelectedItem.ToString() + "' where srno='" + LbRegNo.Text + "'  and sessionName='" + Session["SessionName"].ToString() + "'";

            SqlCommand cmd1 = new SqlCommand();
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = sql;
            cmd1.Connection = con;

            try
            {

                con.Open();
                cmd1.ExecuteNonQuery();
                con.Close();

            }
            catch (SqlException) { }



            if (drpstatus.Text.ToUpper() == "PENDING" || drpstatus.Text.ToUpper() == "Left")
            {
                
            }
            else
            {
                if (drpstatus.Text == "Failed" || drpstatus.Text.ToUpper() == "FAILED")
                {

                    sql1 = "select * from StudentOfficialDetails where SrNo='" + lblSrNo.Text + "'  and SessionName='" + drpSessionNew.SelectedItem.ToString() + "'";

                    if (oo.Duplicate(sql1) == false)
                    {
                        int co = 0;
                        sql = "select max(id)  as id from StudentOfficialDetails";
                        if (oo.ReturnTag(sql, "id").ToString() != "")
                        {
                            co = Convert.ToInt32(oo.ReturnTag(sql, "id"));
                        }
                        else
                        {
                            co = 0;
                        }
                        co = co + 1;

                        sql = "Select Id from ClassMaster where ClassName='" + DrpClass.SelectedItem.ToString() + "' and SessionName='" + drpSession.SelectedItem.ToString() + "'";

                        classId = oo.ReturnTag(sql, "Id");

                        sql = "select Id from SectionMaster where SectionName='" + drpSectionNew.SelectedItem.ToString() + "' and ClassNameId='" + classId.ToString() + "' and SessionName='" + drpSessionNew.SelectedItem.ToString() + "'";
                        sec = oo.ReturnTag(sql, "Id");

                        StEnRCode = IDGeneration(co.ToString());

                        sql = "insert into StudentOfficialDetails(Id,StEnRCode,SrNo,DateOfAdmiission,AdmissionForClassId,SectionId,GroupNa,FileNo,Reference,Remark,";
                        sql = sql + "  BranchCode ,LoginName ,SessionName,RecordDate,medium ,Board,TypeOFAdmision,card,TransportRequired,HouseName,BoardUniversityRollNo,InstituteRollNo,CardNo,MachineNo,course,branch,ModforFeeDeposit,SMSAcknowledgment,EmailAcknowledgment)";
                        sql = sql + "  Select  '" + co + "','" + StEnRCode + "',SrNo,DateOfAdmiission,'" + DrpClassNew.SelectedValue.ToString() + "','" + drpSectionNew.SelectedValue.ToString() + "',GroupNa,FileNo,Reference,Remark,";
                        sql = sql + "  " + Session["BranchCode"].ToString() + " ,'" + Session["loginName"].ToString() + "','" + drpSessionNew.SelectedItem.Text.ToString() + "',getdate(),'" + DropMediumNew.SelectedItem.Text.ToString() + "' ,'" + DropBoardNew.SelectedItem.Text.ToString() + "','Old','" + DropFeeGroup.SelectedItem.Text.ToString() + "',TransportRequired,'" + DropHouseNew.SelectedItem.Text.Trim() + "',";
                        sql = sql + " BoardUniversityRollNo,InstituteRollNo,CardNo,MachineNo,'" + DropCourseNew.SelectedValue.ToString() + "','" + DrpBranchNew.SelectedValue.ToString() + "',ModforFeeDeposit,SMSAcknowledgment,EmailAcknowledgment from StudentOfficialDetails";
                        sql = sql + "  where srno='" + lblSrNo.Text + "'  and SessionName='" + Session["SessionName"].ToString() + "'";

                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandText = sql;
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        try
                        {
                            if (chkBox.Checked == true)
                            {
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                        catch (SqlException) { con.Close(); }
                    }
                    else
                    {
                        sql = "Update StudentOfficialDetails set Promotion='" + drpstatus.SelectedItem.ToString() + "' where Srno='" + lblSrNo.Text.Trim() + "'  and sessionName='" + Session["SessionName"].ToString() + "'";
                        try
                        {
                            oo.ProcedureDatabase(sql);
                        }
                        catch (SqlException) { }
                        sql = "Select Id from ClassMaster where ClassName='" + DrpClassNew.SelectedItem.ToString() + "'";
                        sql = sql + "  and SessionName='" + drpSessionNew.SelectedItem.ToString() + "'";

                        classId = oo.ReturnTag(sql, "Id");

                        sql = "select Id from SectionMaster where SectionName='" + drpSectionNew.SelectedItem.ToString() + "' and ClassNameId=" + classId;
                        sec = oo.ReturnTag(sql, "Id");
                        sql = "Update StudentOfficialDetails set Promotion=null,AdmissionForClassId='" + classId + "',SectionId='" + sec + "' where Srno='" + lblSrNo.Text.Trim() + "'  and sessionName='" + drpSessionNew.SelectedItem.Text.Trim() + "'";
                        try
                        {
                            oo.ProcedureDatabase(sql);
                        }
                        catch (SqlException) { }
                    }
                }
                else
                {
                    sql1 = "select * from StudentOfficialDetails where SrNo='" + lblSrNo.Text + "'  and SessionName='" + drpSessionNew.SelectedItem.ToString() + "'";

                    if (oo.Duplicate(sql1) == false)
                    {
                        int co = 0;
                        sql = "select max(id)  as id from StudentOfficialDetails";
                        if (oo.ReturnTag(sql, "id").ToString() != "")
                        {
                            co = Convert.ToInt32(oo.ReturnTag(sql, "id"));
                        }
                        else
                        {
                            co = 0;
                        }
                        co = co + 1;

                        sql = "Select Id from ClassMaster where ClassName='" + DrpClassNew.SelectedItem.ToString() + "' and SessionName='" + drpSessionNew.SelectedItem.ToString() + "'";

                        classId = oo.ReturnTag(sql, "Id");

                        sql = "select Id from SectionMaster where SectionName='" + drpSectionNew.SelectedItem.ToString() + "' and ClassNameId='" + classId.ToString() + "' and SessionName='" + drpSessionNew.SelectedItem.ToString() + "'";
                        sec = oo.ReturnTag(sql, "Id");

                        StEnRCode = IDGeneration(co.ToString());

                        sql = "insert into StudentOfficialDetails(Id,StEnRCode,SrNo,DateOfAdmiission,AdmissionForClassId,SectionId,GroupNa,FileNo,Reference,Remark,";
                        sql = sql + "  BranchCode ,LoginName ,SessionName,RecordDate,medium ,Board,TypeOFAdmision,card,TransportRequired,HouseName,BoardUniversityRollNo,InstituteRollNo,CardNo,MachineNo,course,branch,ModforFeeDeposit,SMSAcknowledgment,EmailAcknowledgment)";
                        sql = sql + "  Select  '" + co + "','" + StEnRCode + "',SrNo,DateOfAdmiission,'" + DrpClassNew.SelectedValue.ToString() + "','" + drpSectionNew.SelectedValue.ToString() + "',GroupNa,FileNo,Reference,Remark,";
                        sql = sql + "  " + Session["BranchCode"].ToString() + " ,'" + Session["loginName"].ToString() + "','" + drpSessionNew.SelectedItem.Text.ToString() + "',getdate(),'" + DropMediumNew.SelectedItem.Text.ToString() + "' ,'" + DropBoardNew.SelectedItem.Text.ToString() + "','Old','" + DropFeeGroup.SelectedItem.Text.ToString() + "',TransportRequired,'" + DropHouseNew.SelectedItem.Text.Trim() + "',";
                        sql = sql + " BoardUniversityRollNo,InstituteRollNo,CardNo,MachineNo,'" + DropCourseNew.SelectedValue.ToString() + "','" + DrpBranchNew.SelectedValue.ToString() + "',ModforFeeDeposit,SMSAcknowledgment,EmailAcknowledgment from StudentOfficialDetails";
                        sql = sql + "  where srno='" + lblSrNo.Text + "'  and SessionName='" + Session["SessionName"].ToString() + "'";


                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandText = sql;
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        try
                        {
                            if (chkBox.Checked == true)
                            {
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                        catch (SqlException) { con.Close(); }
                    }
                    else
                    {
                        sql = "Update StudentOfficialDetails set Promotion='" + drpstatus.SelectedItem.ToString() + "' where Srno='" + lblSrNo.Text.Trim() + "'  and sessionName='" + Session["SessionName"].ToString() + "'";
                        try
                        {
                            oo.ProcedureDatabase(sql);
                        }
                        catch (SqlException) { }
                        sql = "Select Id from ClassMaster where ClassName='" + DrpClassNew.SelectedItem.ToString() + "'";
                        sql = sql + "  and SessionName='" + drpSessionNew.SelectedItem.ToString() + "'";

                        classId = oo.ReturnTag(sql, "Id");

                        sql = "select Id from SectionMaster where SectionName='" + drpSectionNew.SelectedItem.ToString() + "' and ClassNameId=" + classId;
                        sec = oo.ReturnTag(sql, "Id");
                        sql = "Update StudentOfficialDetails set Promotion=null,AdmissionForClassId='" + classId + "',SectionId='" + sec + "' where Srno='" + lblSrNo.Text.Trim() + "'  and sessionName='" + drpSessionNew.SelectedItem.Text.Trim() + "'";
                        try
                        {
                            oo.ProcedureDatabase(sql);
                        }
                        catch (SqlException) { }
                    }

                }
            }
        }
}

    public void StudentPromotionStudentStudentDocuments()
    {
        int i = 0;
        string StEnRCode = "";
        for (i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            Label LbRegNo = (Label)GridView1.Rows[i].Cells[1].FindControl("Label12");
            Label lblSrNo = (Label)GridView1.Rows[i].Cells[1].FindControl("Label13");
            CheckBox chkBox = (CheckBox)GridView1.Rows[i].Cells[8].FindControl("CheckBox1");
            DropDownList drpstatus = (DropDownList)GridView1.Rows[i].FindControl("drpstatus");

            if (drpstatus.Text.ToUpper() == "PENDING" || drpstatus.Text.ToUpper() == "Left")
            {
            }
            else
            {
                sql1 = "select * from StudentDocuments where SrNo='" + lblSrNo.Text + "'  and SessionName='" + drpSessionNew.SelectedItem.ToString() + "'";
                if (oo.Duplicate(sql1) == false)
                {
                    int co = 0;
                    sql = "select max(id)  as id from StudentDocuments";
                    if (oo.ReturnTag(sql, "id").ToString() != "")
                    {
                        co = Convert.ToInt32(oo.ReturnTag(sql, "id"));
                    }
                    else
                    {
                        co = 0;
                    }
                    co = co + 1;

                    StEnRCode = IDGeneration(co.ToString());

                    sql = "  insert into StudentDocuments(Id,StEnRCode,SrNo,PhotoPath,TransferCertificatePath,DomicileCertitificatePath,IncomeCertificatePath, ";
                    sql = sql + " BirthCertificatePath,CasteCertificatePath,BranchCode ,LoginName  ,SessionName,RecordDate ,PhotoRemark ,TransferCertificateRemark ,DomicileCertificateRemark,IncomeCertificateRemark,BirthCertificateRemark,CasteCertificateRemark  )";
                    sql = sql + " select " + co + ",'" + StEnRCode + "',SrNo,PhotoPath,TransferCertificatePath,DomicileCertitificatePath,IncomeCertificatePath, ";
                    sql = sql + "   BirthCertificatePath,CasteCertificatePath,BranchCode ,'" + Session["LoginName"] + "'  ,'" + drpSessionNew.SelectedItem.ToString() + "',RecordDate ,PhotoRemark ,TransferCertificateRemark ,DomicileCertificateRemark,IncomeCertificateRemark,BirthCertificateRemark,CasteCertificateRemark  ";
                    sql = sql + "  from StudentDocuments ";
                    sql = sql + "   where srno='" + lblSrNo.Text + "'  and SessionName='" + Session["SessionName"].ToString() + "'";

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = sql;
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    try
                    {
                        if (chkBox.Checked == true)
                        {
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                    catch (SqlException) { con.Close(); }
                }
            }
        }

    }

    protected void LinkSubmit_Click(object sender, EventArgs e)
    {
        string sql1 = "";
        sql = "Select Row_Number() over (order by SG.Id Asc) as SNo ,SO.Promotion,SG.Id, SC.SectionName,CM.ClassName,convert(nvarchar,So.DateOfAdmiission,106) as DateOfAdmiission , ";
        sql = sql + "   SO.SectionId,Sf.FatherName,SF.MotherName,SG.FirstName,SG.MiddleName,SG.LastName,sg.StEnRCode as StEnRCode,sg.srno  as srno,sg.SessionName from StudentGenaralDetail SG ";
        sql = sql + "   left join StudentFamilyDetails SF on SG.srno=SF.srno";
        sql = sql + "   left join StudentOfficialDetails SO on SG.srno=SO.srno";
        sql = sql + "   left join ClassMaster CM on SO.AdmissionForClassId=CM.Id";
        sql = sql + "   left join SectionMaster SC on SO.SectionId=SC.Id";
        sql = sql + "   where  CM.ClassName='"+ DrpClass.SelectedItem.ToString() + "' and SC.SectionName='" + drpSection.SelectedItem.ToString() + "' and sg.sessionName='" + drpSession.SelectedItem.ToString() + "'";
        sql = sql + "   and sf.sessionName='" + drpSession.SelectedItem.ToString() + "'";
        sql=  sql + "   and so.sessionName='" + drpSession.SelectedItem.ToString() + "'";
        sql = sql + "   and so.sessionName='" + drpSession.SelectedItem.ToString() + "'";
        sql = sql + "   and sc.sessionName='" + drpSession.SelectedItem.ToString() + "'";
        sql = sql + "   and cm.sessionName='" + drpSession.SelectedItem.ToString() + "'";
        sql = sql + "   and Promotion is not null and Withdrwal is null";

      
        sql1 = "select * From SessionMAster where SessionName='" + drpSessionNew.SelectedItem.ToString() + "'";
        if (oo.Duplicate(sql1))
        {
          

                    if (DrpClassNew.SelectedItem.ToString() == "<--Select-->" || drpSectionNew.SelectedItem.ToString() == "<--Select-->")
                    {
                        //oo.MessageBoxforUpdatePanel("Please Select Condition!", LinkSubmit);
                        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox1, "Please Select Condition!", "A");

                    }
                    else if (oo.Duplicate(sql))
                    {
                        //oo.MessageBoxforUpdatePanel("Sorry, Already Promoted!", LinkSubmit);                      
                        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox1, "Sorry, Already Promoted!", "A");

                    }
                    else
                    {
                       
                            
                            StudentPromotionStudentGenaralDetail();
                            StudentPromotionStudentFamilyDetails();
                            StudentPromotionStudentPreviousSchool();
                            //StudentPromotionStudentStudentDocuments();
                            StudentPromotionStudentOfficialDetails();
                            //CheckTheBalanceAmountParticularStudent();
                            ArrierTransaction();
                            //oo.MessageBoxforUpdatePanel("Promoted Successfully.", LinkSubmit);
                            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox1, "Promoted Successfully.", "S");

                            LinkSubmit.Enabled = false;
                        
                    }
        }
        else
        {
            //oo.MessageBoxforUpdatePanel("Sorry, Promoted Session Name not found!", LinkSubmit);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox1, "Sorry, Promoted Session Name not found!", "A");


        
        }
    }

    public string IDGeneration(string x)
    {
        string xx = "";
        if (x.Length == 1)
        {
            xx = "eAM-00000" + x;

        }
        else if (x.Length == 2)
        {
            xx = "eAM-0000" + x;
        }
        else if (x.Length == 3)
        {
            xx = "eAM-000" + x;

        }
        else if (x.Length == 4)
        {
            xx = "eAM-00" + x;
        }
        else if (x.Length == 5)
        {
            xx = xx + "eAM-0" + x;
        }
        else
        {
            xx = x;
        }
        return xx;
    }

    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {

    }

    public void PermissionGrant(int add1,LinkButton Ladd)
    {

        if (add1 == 1)
        {
            Ladd.Enabled = true;
        }
        else
        {
            Ladd.Enabled = false;
        }


    }

    public void CheckValueADDDeleteUpdate()
    {
        sql = " select LoginId,LoginName,Pass,SessionId,BranchId,LT.LoginTypeName,ltb.add1 as add1,ltb.delete1 as delete1,ltb.update1 as update1 from LoginTab LTb";
        sql = sql + " left join LoginType Lt on LTb.LoginTypeId=Lt.LoginTypeId where LT.LoginTypeName='" + Session["Logintype"] + "' and LTb.LoginName='" + Session["LoginName"] + "'";
#pragma warning disable 168
        int a, u, d;
#pragma warning restore 168
        a = Convert.ToInt32(oo.ReturnTag(sql, "add1"));
      

        PermissionGrant(a,(LinkButton)LinkSubmit);
    }

    protected void drpstatus_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    public void CheckTheBalanceAmountParticularStudent()
    {
#pragma warning disable 168
        int i,k;
#pragma warning restore 168
#pragma warning disable 219
        double pp = 0,qq=0;
#pragma warning restore 219
        for (i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            pp = 0;
            Label lblSrNo = (Label)GridView1.Rows[i].FindControl("Label13");
            Label lblClass = (Label)GridView1.Rows[i].FindControl("Label18");
            Label lblBalanceAmt = (Label)GridView1.Rows[i].FindControl("lblBalanceAmt");

          
           
            lblBalanceAmt.Text = pp.ToString();

          
            pp = pp +Convert.ToDouble(ClassWiseAmountForParticularMonth(lblClass.Text, "Apr", lblSrNo.Text));
            pp = pp + Convert.ToDouble(ClassWiseAmountForParticularMonth(lblClass.Text, "May", lblSrNo.Text));
            pp = pp + Convert.ToDouble(ClassWiseAmountForParticularMonth(lblClass.Text, "Jul", lblSrNo.Text));
            pp = pp + Convert.ToDouble(ClassWiseAmountForParticularMonth(lblClass.Text, "Aug", lblSrNo.Text));
            pp = pp + Convert.ToDouble(ClassWiseAmountForParticularMonth(lblClass.Text, "Sep", lblSrNo.Text));
            pp = pp + Convert.ToDouble(ClassWiseAmountForParticularMonth(lblClass.Text, "Oct", lblSrNo.Text));
            pp = pp +Convert.ToDouble( ClassWiseAmountForParticularMonth(lblClass.Text, "Nov", lblSrNo.Text));
            pp = pp +Convert.ToDouble( ClassWiseAmountForParticularMonth(lblClass.Text, "Dec", lblSrNo.Text));
            pp = pp +Convert.ToDouble( ClassWiseAmountForParticularMonth(lblClass.Text, "Jan", lblSrNo.Text));
            pp = pp +Convert.ToDouble( ClassWiseAmountForParticularMonth(lblClass.Text, "Feb", lblSrNo.Text));
            sql = "select * from FeeDeposite where Srno='" + lblSrNo.Text + "' and FeeMonth='Yearly' and session='"+Session["SessionName"].ToString()+"'";
            if (oo.Duplicate(sql))
            {
                lblBalanceAmt.Text = "0";
            }
            else
            {
                lblBalanceAmt.Text = "";
                lblBalanceAmt.Text = pp.ToString();
            }



        }


      

     
    }

    public double ConvenceAmount(string ClassName, string MonthNa, string Srno)
    {
        string sql1 = "";
        double amt = 0;
        double AMTValue = 0;
        string card="";
        sql = "select wayAmount from StudentOfficialDetails  where TransportRequired='Yes'  and srno='"+Srno+"'  and SessionName='"+Session["SessionName"].ToString()+"'";
        sql1 = "select card From StudentOfficialDetails  where  srno='" + Srno + "'  and SessionName='" + Session["SessionName"].ToString() + "'";
        if (oo.Duplicate(sql))
           {

            card=oo.ReturnTag(sql1,"Card");
            if (DepositYesORNo(Srno, MonthNa, card) == false)
            {

                try
                {
                    amt = Convert.ToDouble(oo.ReturnTag(sql, "WayAmount"));
                    AMTValue = amt;
                }
                catch (Exception) { AMTValue = 0; }                
            }
        }
        return AMTValue;
       
    }

    public double  BalanceAmount(string srno, string className, string typeOfAdmission, string medium, string crd, string FeesMonth)
    {
        double damt=0;

        

        sql = " select distinct ROW_NUMBER() OVER (ORDER BY fa.Id ASC) AS SrNo,Id,fa.Month, fa.FeeParticular,fa.Class,fa.FeeType,fa.FeePayment,FM.Medium,Fa.CardType ,fm.NoOfmonths as NoOfmonths from FeeAllotedForClassWise fa ";
        sql = sql + " left join feemaster  fm on fa.Medium=fm.medium  and fa.FeeName=fm.FeeName  ";
        sql = sql + " where fa.Class='" + className  + "'   and fa.Month='" + FeesMonth + "' and fa.SessionName='" + Session["SessionName"].ToString() + "'  and fa.BranchCode=" + Session["BranchCode"].ToString() + "";
        sql = sql + "and fa.CardType='" + crd + "' and   fa.AdmissionType='" + typeOfAdmission.Trim() + "'  and fa.Medium='" + medium + "'";


                                    double sum = 0, discAmt = 0, tutionFees = 0;
                                    string sql4 = "select * from FeeDeposite where srno='" + srno + "'  and FeeMonth='" + FeesMonth + "'";
                                    sql4=sql4+ " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

                                    SqlCommand cmd1 = new SqlCommand();
                                    if (oo.Duplicate(sql4) == false)
                                    {//----------->


                                        try
                                        {
                                            cmd1.CommandText = sql;
                                            SqlDataReader dr;
                                            cmd1.Connection = con;
                                            con.Open();
                                            dr = cmd1.ExecuteReader();

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
                                                        tutionFees = tutionFees + Convert.ToDouble(dr["FeePayment"].ToString());
                                                    }
                                                    catch (Exception) { }
                                                }
                                            }
                                            con.Close();
                                        }
                                        catch (SqlException)
                                        {
                                            con.Close();
                                        }
                                    } //--------->

                            double totAmt = 0;
                            int noMonths = 0;
                            int j=0;
                            double lastmonth=0;

                            try
                            {
                                noMonths = Convert.ToInt32(oo.ReturnTag(sql, "NoOfMonths"));
                            }
                            catch (Exception) { }
                            sql = "select DiscountValue,DiscountType from DiscountMaster where srno='" + srno + "'";
                            sql=sql+"  and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
                            if (oo.ReturnTag(sql, "DiscountType") == "Amount")
                            {
                                for (j = 1; j <= noMonths; j++)
                                {
                                    totAmt = totAmt + Convert.ToDouble(oo.ReturnTag(sql, "DiscountValue")); 

                                }
                                discAmt = totAmt;
                            }
                            if (oo.ReturnTag(sql, "DiscountType") == "Percentage")
                            {
                                for (j = 1; j <= noMonths; j++)
                                {

                                    discAmt = discAmt + (tutionFees) * Convert.ToDouble(oo.ReturnTag(sql, "DiscountValue")) / 100;
                                }

                            }
                            try
                            {
                                damt = sum - discAmt;
                            }
                            catch (Exception)
                            {
                                damt = sum;
                            }

                            lastmonth = lastmonth + damt;
                            return lastmonth;
    }

    protected void LinkShow_Click(object sender, EventArgs e)
    {
        string sql2 = "";

        sql = "Select SessionName from SessionMaster";
        sql = sql + " where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        string sessValue = "", ff = "";
        sessValue = oo.ReturnTag(sql, "SessionName");
        int s1, s2;
        s1 = Convert.ToInt32(sessValue.Substring(0, 4));
        s2 = Convert.ToInt32(sessValue.Substring(5, 4));
        s1 = s1 + 1;
        s2 = s2 + 1;
        ff = s1.ToString() + "-" + s2.ToString();


        
        sql2 = "Select *from(Select T1.srno,ISNULL(Promotion,'0') as Promotion from (Select so.srno  as srno from StudentOfficialDetails SO ";
        sql2 = sql2 + "  left join ClassMaster CM on SO.AdmissionForClassId=CM.Id  ";
        sql2 = sql2 + "  left join SectionMaster SC on SO.SectionId=SC.Id  where  CM.ClassName='" + DrpClass.SelectedItem.ToString() + "'";
        sql2 = sql2 + "  and SC.SectionName='" + drpSection.SelectedItem.ToString() + "'";
        sql2 = sql2 + "  and so.SessionName='" + Session["SessionName"].ToString() + "'  and sc.SessionName='" + Session["SessionName"].ToString() + "'";
        sql2 = sql2 + "  and cm.SessionName='" + Session["SessionName"].ToString() + "' and (Promotion is null or Promotion='Pending') and SO.Withdrwal is null) as T1";
        sql2 = sql2 + "  left join StudentOfficialDetails T2 on T2.SrNo=T1.srno and T2.SessionName=(Select SessionName  from SessionMaster where SessionId=(Select * from(Select SessionId";
        sql2 = sql2 + "  from SessionMaster sm where sm.SessionName='" + Session["SessionName"].ToString() + "') as T2)+1)";
        sql2 = sql2 + "  and (t2.Promotion is not null or t2.Promotion='Pending') and Withdrwal is null)";
        sql2 = sql2 + "  as T4 where Promotion='0' or Promotion='Cancelled'";

        if (oo.Duplicate(sql2) == false)
        {
            //oo.MessageBoxforUpdatePanel("Sorry, Students already promoted to next Session!", LinkShow);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, Students already promoted to next Session!", "A");

            GridView1.DataSource = null;
            GridView1.DataBind();
            Panel2.Visible = false;
        }



        else
            if (DrpClass.SelectedItem.Text == "<--Select-->" || drpSection.SelectedItem.Text == "<--Select-->")
            {
                //oo.MessageBoxforUpdatePanel("Please Select Condition!", LinkShow);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please Select Condition!!", "A");

            }
            else
            {
                WithSelect();

                sql = "Select Row_Number() over (order by SG.Id Asc) as SNo ,SG.Id, SC.SectionName,CM.ClassName,convert(nvarchar,So.DateOfAdmiission,106) as DateOfAdmiission , ";
                sql = sql + "   SO.SectionId,SO.Medium,SO.Card,Sf.FatherName,SF.MotherName,SG.FirstName,SG.MiddleName,SG.LastName,sg.StEnRCode as StEnRCode,sg.srno  as srno,sg.SessionName,so.Promotion as Promotion from StudentGenaralDetail SG ";
                sql = sql + "   left join StudentFamilyDetails SF on SG.srno=SF.srno";
                sql = sql + "   left join StudentOfficialDetails SO on SG.srno=SO.srno";
                sql = sql + "   left join ClassMaster CM on SO.AdmissionForClassId=CM.Id";
                sql = sql + "   left join SectionMaster SC on SO.SectionId=SC.Id";
                sql = sql + wherecondition;
                sql = sql + "  and sg.SessionName='" + Session["SessionName"].ToString() + "' and sg.BranchCode=" + Session["BranchCode"].ToString() + " ";
                sql = sql + "  and SO.Withdrwal is null";
                sql = sql + "  and so.SessionName='" + Session["SessionName"].ToString() + "'";
                sql = sql + "  and sf.SessionName='" + Session["SessionName"].ToString() + "'";
                sql = sql + "  and cm.SessionName='" + Session["SessionName"].ToString() + "'";
                sql = sql + "  and sc.SessionName='" + Session["SessionName"].ToString() + "'";
                sql = sql + "  and so.Card='" + DrpFeeGroup.SelectedItem.Text.ToUpper() + "'";
                sql = sql + "  and (Promotion is null or Promotion='Pending') order by FirstName Asc";

                GridView1.DataSource = oo.GridFill(sql);
                GridView1.DataBind();
                int i;
                if (GridView1.Rows.Count == 0)
                {
                    lblRedIndicate.Visible = false;
                    //oo.MessageBoxforUpdatePanel("No Record(s) found!", LinkShow);
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "No Record(s) found!", "A");

                    LinkSubmit.Enabled = false;
                }
                else
                {
                    lblRedIndicate.Visible = true;
                    for (i = 0; i <= GridView1.Rows.Count - 1; i++)
                    {
                        DropDownList drpstatus = (DropDownList)GridView1.Rows[i].FindControl("drpstatus");
                        sql = "Select Status from PromotionStatusMaster ";
                        oo.FillDropDownWithOutSelect(sql, drpstatus, "Status");
                    }



                    LinkSubmit.Enabled = true;
                    Panel2.Visible = true;

                    sql = "Select SessionName from SessionMaster";
                    sql = sql + " where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

                    sessValue = oo.ReturnTag(sql, "SessionName");

                    s1 = Convert.ToInt32(sessValue.Substring(0, 4));
                    s2 = Convert.ToInt32(sessValue.Substring(5, 4));
                    s1 = s1 + 1;
                    s2 = s2 + 1;
                    ff = s1.ToString() + "-" + s2.ToString();
                    drpSessionNew.Items.Clear();
                    drpSessionNew.Items.Add(ff);
                    LinkSubmit.Enabled = true;


                    for (i = 0; i <= GridView1.Rows.Count - 1; i++)
                    {
                        DropDownList drpstatus = (DropDownList)GridView1.Rows[i].FindControl("drpstatus");
                        Label LbRegNo = (Label)GridView1.Rows[i].Cells[1].FindControl("Label12");
                        sql = "Select Promotion from StudentOfficialDetails where srno='" + LbRegNo.Text + "'";
                        try
                        {
                            drpstatus.Text = oo.ReturnTag(sql, "Promotion");
                        }
                        catch (Exception) { }

                    }


                    sql = "Select Count(so.SrNo) counter from StudentGenaralDetail SG ";
                    sql = sql + "   left join StudentFamilyDetails SF on SG.srno=SF.srno";
                    sql = sql + "   left join StudentOfficialDetails SO on SG.srno=SO.srno";
                    sql = sql + "   left join ClassMaster CM on SO.AdmissionForClassId=CM.Id";
                    sql = sql + "   left join SectionMaster SC on SO.SectionId=SC.Id";
                    sql = sql + wherecondition;
                    sql = sql + "   and sg.SessionName='" + Session["SessionName"].ToString() + "' and sg.BranchCode=" + Session["BranchCode"].ToString() + " ";
                    sql = sql + "   and SO.Withdrwal is null";
                    sql = sql + "  and so.SessionName='" + Session["SessionName"].ToString() + "'";
                    sql = sql + "  and sf.SessionName='" + Session["SessionName"].ToString() + "'";
                    sql = sql + " and cm.SessionName='" + Session["SessionName"].ToString() + "'";
                    sql = sql + " and sc.SessionName='" + Session["SessionName"].ToString() + "'";

                    int count1 = int.TryParse(oo.ReturnTag(sql, "counter"), out count1) ? count1 : 0;

                    sql = "Select Count(so.SrNo) counter from StudentOfficialDetails SO  ";
                    sql = sql + "    left join ClassMaster CM on SO.AdmissionForClassId=CM.Id   ";
                    sql = sql + "    left join SectionMaster SC on SO.SectionId=SC.Id where SC.SectionName='"+drpSection.SelectedItem.Text+"' and  CM.ClassName='"+DrpClass.SelectedItem.Text.Trim()+"'  ";
                    sql = sql + "    and so.BranchCode=" + Session["BranchCode"].ToString() + " and SO.Withdrwal is null and Promotion in ('Pass','Failed')";
                    sql = sql + "    and so.SessionName='" + Session["SessionName"].ToString() + "' and cm.SessionName='" + Session["SessionName"].ToString() + "' and sc.SessionName='" + Session["SessionName"].ToString() + "'";

                    int count2 = int.TryParse(oo.ReturnTag(sql, "counter"), out count2) ? count2 : 0;

                    if (count1 == count2)
                    {
                        //oo.MessageBoxforUpdatePanel("This Class is already Promoted!", LinkShow);
                        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "This Class is already Promoted!", "A");

                        Panel2.Visible = false;

                    }

                    for (i = 0; i < GridView1.Rows.Count; i++)
                    {
                        Label Label13 = (Label)GridView1.Rows[i].FindControl("Label13");
                        Label Label18 = (Label)GridView1.Rows[i].FindControl("Label18");
                        Label Label21 = (Label)GridView1.Rows[i].FindControl("Label21");
                        Label Label22 = (Label)GridView1.Rows[i].FindControl("Label22");
                        Label lblBalanceAmt = (Label)GridView1.Rows[i].FindControl("lblBalanceAmt");
                        Label lblArrierAmt = (Label)GridView1.Rows[i].FindControl("lblArrierAmt");
                        Label lblDiscountAmt = (Label)GridView1.Rows[i].FindControl("lblDiscountAmt");
                        Label lblTransportAmt = (Label)GridView1.Rows[i].FindControl("lblTransportAmt");
                        Label lblLateFeeAmt = (Label)GridView1.Rows[i].FindControl("lblLateFeeAmt");
                        Label lblTotalArrier = (Label)GridView1.Rows[i].FindControl("lblTotalArrier");

                        sql = "select TypeOFAdmision,MODForFeeDeposit Mop,card,Medium,AdmissionForClassId  from StudentOfficialDetails where SessionName='" + Session["SessionName"].ToString() + "' and SrNo='" + Label13.Text + "'";
                        string tyofadmission = oo.ReturnTag(sql, "TypeOFAdmision").ToString();
                        string mop = oo.ReturnTag(sql, "mop").ToString();
                        string feegroup = oo.ReturnTag(sql, "card").ToString();
                        string Medium = oo.ReturnTag(sql, "Medium").ToString();
                        string AdmissionForClassId = oo.ReturnTag(sql, "AdmissionForClassId");
                        DataTable FeeMonthDetails = new DataTable();
                        FeeMonthDetails.Columns.Add("Sr");
                        FeeMonthDetails.Columns.Add("TotalPayment");
                        FeeMonthDetails.Columns.Add("Month");
                        string TypeOFAdmision = oo.ReturnTag(sql, "TypeOFAdmision").ToString();
                      
                        sql1 = " select distinct Sum(FeePayment) as FeePayment,Month,mm.MonthId from FeeAllotedForClassWise fa ";
                        sql1 = sql1 + " inner join MonthMaster mm on mm.MonthName=fa.Month and fa.SessionName=mm.SessionName and mm.ClassId='" + AdmissionForClassId + "'";
                        sql1 = sql1 + " left join feemaster  fm on fa.Medium=fm.medium  and (fa.FeeType=fm.FeeName  or fa.FeeName=fm.FeeName )  and fm.SessionName=fa.SessionName ";
                        sql1 = sql1 + " where (fa.Class='" + DrpClass.SelectedItem.Text + "' or fa.Class='" + AdmissionForClassId + "') ";
                        sql1 = sql1 + "and fa.SessionName='" + Session["SessionName"].ToString() + "'  and fa.BranchCode=" + Session["BranchCode"].ToString() + "";
                        sql1 = sql1 + " and fa.CardType='" + feegroup + "' and   fa.AdmissionType='" + TypeOFAdmision + "'";
                        sql1 = sql1 + " and (fa.MOD='I' or fa.MOD is null) and (fa.Branchid='" + DropBranch.SelectedValue.ToString() + "' or fa.Branchid is null) ";
                        sql1 = sql1 + " and (fa.Medium='" + Medium + "' or fa.Medium is null) group by Month,mm.MonthId order by mm.MonthId";


                        con.Open();
                        SqlCommand FeeMonthDetails_cmd = new SqlCommand(sql1, con);

                        SqlDataReader FeeMonthDetails_cmd_dr = FeeMonthDetails_cmd.ExecuteReader();
                        int srNos = 0;
                        while (FeeMonthDetails_cmd_dr.Read())
                        {
                            FeeMonthDetails.Rows.Add(srNos + 1, FeeMonthDetails_cmd_dr["FeePayment"].ToString(), FeeMonthDetails_cmd_dr["Month"].ToString());
                        }
                        FeeMonthDetails_cmd_dr.Close();
                        double totalAmount = 0;

                        sql = "select FeeMonth from FeeDeposite where Cancel is null and SrNo='" + Label13.Text + "' and SessionName='" + Session["SessionName"].ToString() + "'";

                        double discpuntamount = 0;
                        double transportAmount = 0;
                        double ArrierAmount = 0;
                        double LatefeeAmount = 0;
                        if (oo.ReturnTag(sql, "FeeMonth").ToString() != "Yearly")
                        {
                            if (oo.ReturnTag(sql, "FeeMonth").ToString() != "")
                            {
                                if (oo.ReturnTag(sql, "FeeMonth").Substring(0, 3) != "(T)")
                                {
                                    for (int j = 0; j < FeeMonthDetails.Rows.Count; j++)
                                    {

                                        sql = "select  COUNT(*) as Counter from FeeDeposite where SessionName='" + Session["SessionName"].ToString() + "' and SrNo='" + Label13.Text + "' and FeeMonth='" + FeeMonthDetails.Rows[j]["Month"].ToString() + "' and Cancel is Null";
                                        if (Convert.ToInt32(oo.ReturnTag(sql, "Counter").ToString()) < 1)
                                        {
                                            string[] words = FeeMonthDetails.Rows[j]["Month"].ToString().Split(' ');
                                            totalAmount = totalAmount + Convert.ToDouble(FeeMonthDetails.Rows[j]["TotalPayment"].ToString());
                                            sql1 = "select TransportRequired,WayAmount  from StudentOfficialDetails where SessionName='" + Session["SessionName"].ToString() + "' and SrNo='" + Label13.Text + "' and TransportRequired = 'Yes'";
                                            if (oo.ReturnTag(sql1, "TransportRequired") == "Yes")
                                            {

                                                foreach (string word in words)
                                                {
                                                    try
                                                    {
                                                        sql = "Select ForMonth from MonthMaster where MonthName='" + word + "' and SessionName='" + Session["SessionName"].ToString() + "'";
                                                        int noofmonths = int.TryParse(oo.ReturnTag(sql, "ForMonth"), out noofmonths) ? noofmonths : 0;
                                                        if (noofmonths > 0)
                                                        {
                                                            sql = "Select Sum(Amount) as Amount from StudentVehicleAllotment where MonthStatus='1' and SessionName='" + Session["SessionName"].ToString() + "' and SrNo='" + Label13.Text + "' and Insttalment='" + word + "'";
                                                            if (oo.ReturnTag(sql, "Amount") != string.Empty)
                                                            {
                                                                transportAmount = transportAmount + (Convert.ToDouble(oo.ReturnTag(sql, "Amount").ToString()) * noofmonths);

                                                            }
                                                        }
                                                        else
                                                        {
                                                            noofmonth(feegroup, mop, DrpClass.SelectedValue.ToString());
                                                        }

                                                    }
                                                    catch
                                                    {
                                                    }


                                                }
                                            }
                                            sql2 = "select DiscountValue from DiscountMaster where SessionName='" + Session["SessionName"].ToString() + "' and SrNo='" + Label13.Text + "' ";
                                            if (oo.ReturnTag(sql2, "DiscountValue") != "")
                                            {
                                                string[] discountamount = oo.ReturnTag(sql2, "DiscountValue").Split(' ');

                                                if (discountamount.Length > 0)
                                                {
                                                    try
                                                    {
                                                        //discpuntamount = discpuntamount + Convert.ToDouble(discountamount[j]);
                                                        double discountamounts = 0;
                                                        discpuntamount = discpuntamount + (double.TryParse(discountamount[j], out discountamounts) ? discountamounts : 0);
                                                    }
                                                    catch
                                                    { }
                                                }
                                            }
                                            foreach (string word in words)
                                            {
                                                LatefeeAmount = LatefeeAmount + CalculateFine(word, Label13.Text);
                                            }
                                        }

                                    }

                                }
                                else
                                {
                                    totalAmount = 0;
                                    discpuntamount = 0;
                                    transportAmount = 0;
                                    for (int j = 0; j < FeeMonthDetails.Rows.Count; j++)
                                    {

                                        sql1 = "select TransportRequired,WayAmount  from StudentOfficialDetails where SessionName='" + Session["SessionName"].ToString() + "' and SrNo='" + Label13.Text + "' and TransportRequired = 'Yes'";
                                        if (oo.ReturnTag(sql1, "TransportRequired") == "Yes")
                                        {
                                            string[] words = FeeMonthDetails.Rows[j]["Month"].ToString().Split(' ');

                                            foreach (string word in words)
                                            {
                                                try
                                                {
                                                    sql = "Select ForMonth from MonthMaster where MonthName='" + word + "' and SessionName='" + Session["SessionName"].ToString() + "'";
                                                    int noofmonths = int.TryParse(oo.ReturnTag(sql, "ForMonth"), out noofmonths) ? noofmonths : 0;
                                                    //double noofmonths = Convert.ToDouble(oo.ReturnTag(sql, "NoOfmonths"));
                                                    sql = "Select Sum(Amount) as Amount from StudentVehicleAllotment where MonthStatus='1' and SessionName='" + Session["SessionName"].ToString() + "' and SrNo='" + Label13.Text + "' and Insttalment='" + word + "'";
                                                    if (oo.ReturnTag(sql, "Amount") != string.Empty)
                                                    {
                                                        transportAmount = transportAmount + (Convert.ToDouble(oo.ReturnTag(sql, "Amount").ToString()) * noofmonths);

                                                    }

                                                }
                                                catch
                                                {
                                                }

                                            }
                                            foreach (string word in words)
                                            {
                                                LatefeeAmount = LatefeeAmount + CalculateFine(word, Label13.Text);
                                            }

                                        }

                                    }
                                    sql = " Select sum(BusConvience) as BusConvience from FeeDeposite fd inner join StudentOfficialDetails sd on fd.SrNo=sd.SrNo and fd.srno=sd.srno";
                                    sql = sql + " where fd.SessionName='" + Session["SessionName"].ToString() + "' and sd.SessionName='" + Session["SessionName"].ToString() + "'";
                                    sql = sql + " and fd.SrNo='" + Label13.Text + "' and fd.FeeMonth like '(T)%' and Cancel is null and Withdrwal is null";

                                    double BusConvience = 0;
                                    if (oo.ReturnTag(sql, "BusConvience") != "")
                                    {
                                        BusConvience = Convert.ToDouble(oo.ReturnTag(sql, "BusConvience"));
                                    }
                                    transportAmount = transportAmount - BusConvience;

                                }

                                //    string WayAmount = "";
                                //    string[] words = FeeMonthDetails.Rows[j]["Month"].ToString().Split(' ');

                                //    foreach (string word in words)
                                //    {

                                //        sql = " Select *from FeeDeposite fd inner join StudentOfficialDetails sd on fd.SrNo=sd.SrNo and fd.srno=sd.srno";
                                //        sql = sql + " where fd.Status='Paid' and fd.SessionName='" + Session["SessionName"].ToString() + "' and sd.SessionName='" + Session["SessionName"].ToString() + "'";
                                //        sql = sql + " and fd.SrNo='" + Label13.Text + "' and fd.FeeMonth like '%" + word + "%' and Cancel is null and Withdrwal is null";
                                //        if (oo.Duplicate(sql) == false)
                                //        {

                                //            sql = "Select Sum(Amount) as Amount from StudentVehicleAllotment where SessionName='" + Session["SessionName"].ToString() + "' and MonthStatus='1' and srno='" + Label13.Text + "' and Insttalment='" + word + "'";
                                //            WayAmount = oo.ReturnTag(sql, "Amount");
                                //            if (WayAmount != "")
                                //            {                                                                                              
                                //                {
                                //                    sql = "Select ForMonth from MonthMaster where MonthName='" + word + "' and SessionName='" + Session["SessionName"].ToString() + "'";
                                //                    int noofmonth = int.TryParse(oo.ReturnTag(sql, "ForMonth"), out noofmonth) ? noofmonth : 0;
                                //                    if (noofmonth > 0)
                                //                    {
                                //                        transportAmount = transportAmount + (Convert.ToDouble(WayAmount) * noofmonth);
                                //                    }
                                //                    else
                                //                    {
                                //                        transportAmount = transportAmount + (Convert.ToDouble(WayAmount));
                                //                    }
                                //                }
                                //            }

                                //        }

                                //        LatefeeAmount = LatefeeAmount + CalculateFine(word, Label13.Text);
                                //    }
                                //}
                            }
                            else
                            {
                                for (int j = 0; j < FeeMonthDetails.Rows.Count; j++)
                                {

                                    sql = "select  COUNT(*) as Counter from FeeDeposite where SessionName='" + Session["SessionName"].ToString() + "' and SrNo='" + Label13.Text + "' and FeeMonth='" + FeeMonthDetails.Rows[j]["Month"].ToString() + "' and Cancel is Null";
                                    if (Convert.ToInt32(oo.ReturnTag(sql, "Counter").ToString()) != 1)
                                    {
                                        string[] words = FeeMonthDetails.Rows[j]["Month"].ToString().Split(' ');
                                        totalAmount = totalAmount + Convert.ToDouble(FeeMonthDetails.Rows[j]["TotalPayment"].ToString());
                                        sql1 = "select TransportRequired,WayAmount  from StudentOfficialDetails where SessionName='" + Session["SessionName"].ToString() + "' and SrNo='" + Label13.Text + "' and TransportRequired = 'Yes'";
                                        if (oo.ReturnTag(sql1, "TransportRequired") == "Yes")
                                        {
                                            foreach (string word in words)
                                            {
                                                try
                                                {
                                                    sql = "Select ForMonth from MonthMaster where MonthName='" + word + "' and SessionName='" + Session["SessionName"].ToString() + "'";
                                                    int noofmonths = int.TryParse(oo.ReturnTag(sql, "ForMonth"), out noofmonths) ? noofmonths : 0;
                                                    sql = "Select Sum(Amount) as Amount from StudentVehicleAllotment where MonthStatus='1' and SessionName='" + Session["SessionName"].ToString() + "' and SrNo='" + Label13.Text + "' and Insttalment='" + word + "'";
                                                    if (oo.ReturnTag(sql, "Amount") != string.Empty)
                                                    {
                                                        transportAmount = transportAmount + (Convert.ToDouble(oo.ReturnTag(sql, "Amount").ToString()) * noofmonths);

                                                    }

                                                }
                                                catch
                                                {
                                                }
                                            }
                                        }
                                        sql2 = "select DiscountValue from DiscountMaster where SessionName='" + Session["SessionName"].ToString() + "' and SrNo='" + Label13.Text + "' ";
                                        if (oo.ReturnTag(sql2, "DiscountValue") != "")
                                        {
                                            string[] discountamount = oo.ReturnTag(sql2, "DiscountValue").Split(' ');

                                            if (discountamount.Length > 0)
                                            {
                                                try
                                                {
                                                    //discpuntamount = discpuntamount + Convert.ToDouble(discountamount[j]);
                                                    double discountamounts = 0;
                                                    discpuntamount = discpuntamount + (double.TryParse(discountamount[j], out discountamounts) ? discountamounts : 0);
                                                }
                                                catch
                                                { }
                                            }
                                        }
                                        foreach (string word in words)
                                        {
                                            LatefeeAmount = LatefeeAmount + CalculateFine(word, Label13.Text);
                                        }

                                    }

                                }

                            }

                        }
                        else
                        {
                            totalAmount = 0;
                            discpuntamount = 0;
                            ArrierAmount = 0;
                            transportAmount = 0;
                            for (int j = 0; j < FeeMonthDetails.Rows.Count; j++)
                            {

                                sql1 = "select TransportRequired,WayAmount  from StudentOfficialDetails where SessionName='" + Session["SessionName"].ToString() + "' and SrNo='" + Label13.Text + "' and TransportRequired = 'Yes'";
                                if (oo.ReturnTag(sql1, "TransportRequired") == "Yes")
                                {
                                    sql = "Select *from FeeDeposite where Cancel is null and SessionName='" + Session["SessionName"].ToString() + "' and SrNo='" + Label13.Text + "' and BusConvience<>'0.00'";
                                    if (oo.Duplicate(sql) == false)
                                    {
                                        string[] words = FeeMonthDetails.Rows[j]["Month"].ToString().Split(' ');

                                        foreach (string word in words)
                                        {
                                            try
                                            {
                                                sql = "Select ForMonth from MonthMaster where MonthName='" + word + "' and SessionName='" + Session["SessionName"].ToString() + "'";
                                                int noofmonths = int.TryParse(oo.ReturnTag(sql, "ForMonth"), out noofmonths) ? noofmonths : 0;

                                                sql = "Select Sum(Amount) as Amount from StudentVehicleAllotment where MonthStatus='1' and SessionName='" + Session["SessionName"].ToString() + "' and SrNo='" + Label13.Text + "' and Insttalment='" + word + "'";
                                                if (oo.ReturnTag(sql, "Amount") != string.Empty)
                                                {
                                                    transportAmount = transportAmount + (Convert.ToDouble(oo.ReturnTag(sql, "Amount").ToString()) * noofmonths);

                                                }

                                            }
                                            catch
                                            {
                                            }

                                        }


                                        foreach (string word in words)
                                        {
                                            LatefeeAmount = LatefeeAmount + CalculateFine(word, Label13.Text);
                                        }
                                    }
                                }

                            }
                        }

                        sql = "select COUNT(*) as FeeeverSubmit from FeeDeposite where Cancel is null and SrNo='" + Label13.Text + "' and SessionName='" + Session["SessionName"].ToString() + "'";
                        if (Convert.ToInt32(oo.ReturnTag(sql, "FeeeverSubmit").ToString()) == 0)
                        {
                            sql = "select ArrearAmt from ArrierMast where SrNo='" + Label13.Text + "' and SessionName='" + Session["SessionName"].ToString() + "'";
                            if (oo.ReturnTag(sql, "ArrearAmt").ToString() != "")
                            {
                                ArrierAmount = ArrierAmount + Convert.ToDouble(oo.ReturnTag(sql, "ArrearAmt").ToString());
                            }

                        }
                        else
                        {
                            sql = "select Top 1 RemainingAmount from FeeDeposite where SrNo='" + Label13.Text + "' and SessionName='" + Session["SessionName"].ToString() + "' Order By Id Desc";
                            if (oo.ReturnTag(sql, "RemainingAmount") != "")
                            {
                                totalAmount = totalAmount + Convert.ToDouble(oo.ReturnTag(sql, "RemainingAmount").ToString());
                            }
                        }


                        con.Close();
                        
                        lblBalanceAmt.Text = totalAmount.ToString();
                        lblArrierAmt.Text = ArrierAmount.ToString();
                        lblDiscountAmt.Text = discpuntamount.ToString();
                        lblTransportAmt.Text = transportAmount.ToString();
                        lblLateFeeAmt.Text = (LatefeeAmount + calculateNextMonthAmountofNextSession(Label13.Text)).ToString();
                        lblTotalArrier.Text = ((totalAmount + ArrierAmount + transportAmount + changeFineAmount(i)) - discpuntamount).ToString();
                        //lblLateFeeAmt.Text = LatefeeAmount.ToString();
                        //lblTotalArrier.Text = ((totalAmount + ArrierAmount + transportAmount + LatefeeAmount) - discpuntamount).ToString();
                    }
                    loadNewSessionClass();
                    loadNewSessionCourse();
                    loadNewSessionBoard();
                    loadNewSessionMedium();
                    loadNewSessionHouse();
                    loadNewSessionFeeGroup();
                }
            }
    }

    public int changeFineAmount(int i)
    {
        int LatefeeAmount = 0;

        Label lblBalanceAmt = (Label)GridView1.Rows[i].FindControl("lblBalanceAmt");
        Label lblTransportAmt = (Label)GridView1.Rows[i].FindControl("lblTransportAmt");
        Label lblLateFeeAmt = (Label)GridView1.Rows[i].FindControl("lblLateFeeAmt");
        if (lblBalanceAmt.Text == "0" && lblTransportAmt.Text == "0")
        {
            lblLateFeeAmt.Text = "0";
        }
        else
        {
            int value = 0;
            LatefeeAmount = LatefeeAmount + (int.TryParse(lblLateFeeAmt.Text, out value) ? value : 0);
        }


        return LatefeeAmount;
    }

    protected int noofmonth(string feegroup,string mop,string classid)
    {
        int noofmonths = 1;
        //sql = "Select ISNULL(Formonth,1) noofmonths from monthmaster  mm inner join FeeGroup fg on fg.Cary";
        //sql = sql + " where CardType='" + feegroup + "' and MOD='" + mop + "' and classid='" + classid + "'";
        sql = "Select ISNULL(Formonth,1) noofmonths from monthmaster  mm ";
        sql = sql + " inner join FeeGroupMaster fgm on fgm.FeeGroupName='" + feegroup + "' and mm.SessionName=fgm.SessionName";
        sql = sql + " where MOD='" + mop + "' and classid='" + classid + "' and mm.SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        noofmonths = Convert.ToInt16(oo.ReturnTag(sql, "noofmonths"));
        return noofmonths;
    }

    private void loadNewSessionClass()
    {
        sql = "Select Id,ClassName from ClassMaster";
        sql = sql + " where (Course='" + DropCourseNew.SelectedValue.ToString() + "' or Course is NULL) and SessionName='" + drpSessionNew.SelectedItem.Text.ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and CIDOrder !=0 ";
        
        oo.FillDropDown_withValue(sql, DrpClassNew, "ClassName", "Id");
        DrpClassNew.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
    }

    private void loadNewSessionBoard()
    {
        sql = "Select BoardName from BoardMaster";
        BAL.objBal.FillDropDown(sql, DropBoardNew, "BoardName");
       
    }

    private void loadNewSessionMedium()
    {
        sql = "Select Medium from MediumMaster where SessionName='" + drpSessionNew.SelectedItem.Text.ToString() + "'";
        BAL.objBal.FillDropDown(sql, DropMediumNew, "Medium");
       
    }

    private void loadNewSessionHouse()
    {
        sql = "Select HouseName from HouseMaster where SessionName='" + drpSessionNew.SelectedItem.Text.ToString() + "'";
        BAL.objBal.FillDropDown(sql, DropHouseNew, "HouseName");
    }

    private void loadNewSessionFeeGroup()
    {
        sql = "Select FeeGroupName,Id from FeeGroupMaster where SessionName='" + drpSessionNew.SelectedItem.Text.ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        oo.FillDropDown_withValue_withSelect(sql, DropFeeGroup, "FeeGroupName", "Id");
    }
   
    //public double CalculateFine(string month, string lblSrno)
    //{
    //    sql = "Select Count(*) as Count from SessionMaster where SessionName='" + Session["SessionName"].ToString() + "' and ToDate< Convert(nvarchar(11),GETDATE(),106)";
    //    int count = 0;
    //    count = Convert.ToInt16(oo.ReturnTag(sql, "Count"));
    //    if (count <= 0)
    //    {
    //        return CalculateInSameSession(month, lblSrno);
    //    }
    //    else
    //    {
    //        return CalculateInnextSession(month, lblSrno);
    //    }
    //}

    //public double CalculateInSameSession(string month, string lblSrno)
    //{
    //    int a = 0, b = 0;
    //    double fineValue = 0;

    //    sql = "select DAY(getdate()) as DayValue";
    //    int dv = 0;
    //    dv = Convert.ToInt32(oo.ReturnTag(sql, "DayValue"));

    //    sql = "Select DATENAME(MONTH,GETDATE()) as MonthName";
    //    string selectedmonth1 = oo.ReturnTag(sql, "MonthName");

    //    //sql = "Select MonthId from MonthMaster where DueMonth='" + selectedmonth1 + "' and SessionName='" + Session["SessionName"].ToString() + "'";
    //    //int id3 = Convert.ToInt32(oo.ReturnTag(sql, "MonthId"));
    //    sql = "Select AdmissionForClassId,Card from StudentOfficialDetails where SrNo='" + lblSrno + "' and SessionName='" + Session["SessionName"].ToString() + "'";
    //    string classid = oo.ReturnTag(sql, "AdmissionForClassId");
    //    string card = oo.ReturnTag(sql, "card");


    //    sql = "Select MonthId from MonthMaster mm";
    //    sql = sql + " inner join FeeGroupMaster fgm on (Case When ISNUmeric(mm.CardType)=1 Then convert(varchar,fgm.Id) Else fgm.FeeGroupName End)=mm.CardType";
    //    sql = sql + " and fgm.SessionName=mm.SessionName where ISNULL(dueMonth,DAteName(MOnth,DueDate))='" + selectedmonth1 + "' and ";
    //    sql = sql + " (ClassId='" + classid + "' or ClassId is null)  and fgm.FeeGroupName='" + card + "' and mm.SessionName='" + Session["SessionName"].ToString() + "'";

    //    int id3 = Convert.ToInt32(oo.ReturnTag(sql, "MonthId"));

    //    string selectedmonth = month;

    //    //sql = "Select MonthId from MonthMaster where DueMonth='" + selectedmonth + "' and SessionName='" + Session["SessionName"].ToString() + "'";
    //    //int id2 = Convert.ToInt32(oo.ReturnTag(sql, "MonthId"));

    //    sql = "Select MonthId from MonthMaster mm";
    //    sql = sql + " inner join FeeGroupMaster fgm on (Case When ISNUmeric(mm.CardType)=1 Then convert(varchar,fgm.Id) Else fgm.FeeGroupName End)=mm.CardType";
    //    sql = sql + " and fgm.SessionName=mm.SessionName where ISNULL(dueMonth,DAteName(MOnth,DueDate))='" + selectedmonth + "' and ";
    //    sql = sql + " (ClassId='" + classid + "' or ClassId is null)  and fgm.FeeGroupName='" + card + "' and mm.SessionName='" + Session["SessionName"].ToString() + "'";

    //    int id2 = Convert.ToInt32(oo.ReturnTag(sql, "MonthId"));

    //    SqlDataAdapter da;
    //    double totalAmount = 0;

    //    //sql = "Select *from MonthMaster where MonthId>='" + id2 + "' and MonthId<'" + id3 + "' and SessionName='" + Session["SessionName"].ToString() + "'";
    //    sql = "Select Monthid from MonthMaster mm";
    //    sql = sql + " inner join FeeGroupMaster fgm on (Case When ISNUmeric(mm.CardType)=1 Then convert(varchar,fgm.Id) Else fgm.FeeGroupName End)=mm.CardType";
    //    sql = sql + " and fgm.SessionName=mm.SessionName where (ClassId='" + classid + "' or ClassId is null)  and fgm.FeeGroupName='" + card + "' and mm.SessionName='" + Session["SessionName"].ToString() + "'";
    //    sql = sql + " and MonthId>='" + id2 + "' and MonthId<'" + id3 + "'";

    //    da = new SqlDataAdapter(sql, con);
    //    DataTable dt2 = new DataTable();
    //    da.Fill(dt2);
    //    for (int j2 = 0; j2 < dt2.Rows.Count; j2++)
    //    {
    //        sql = "Select Distinct MonthAmount from RangeBasisFineMaster where SessionName='" + Session["SessionName"].ToString() + "'";
    //        double fineamount = Convert.ToDouble(oo.ReturnTag(sql, "MonthAmount"));
    //        totalAmount = totalAmount + fineamount;
    //    }
    //    if (id2 <= id3)
    //    {
    //        sql = "select * from RangeBasisFineMaster ";
    //        sql = sql + " where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
    //        SqlCommand cmd = new SqlCommand();
    //        try
    //        {
    //            cmd.CommandText = sql;
    //            SqlDataReader dr;
    //            cmd.Connection = con;
    //            con.Open();
    //            dr = cmd.ExecuteReader();

    //            fineValue = 0;
    //            while (dr.Read())
    //            {
    //                a = Convert.ToInt32(dr["FromDate"].ToString());
    //                b = Convert.ToInt32(dr["ToDate"].ToString());
    //                if (a <= dv && dv <= b)
    //                {
    //                    fineValue = Convert.ToDouble(dr["AmountPerday"].ToString());
    //                    break;
    //                }

    //            }
    //            con.Close();
    //        }
    //        catch (SqlException ee)
    //        {
    //            con.Close();
    //        }
    //    }
    //    totalAmount = totalAmount + fineValue;

    //    return totalAmount;
    //}

    //public double CalculateInnextSession(string month, string lblSrno)
    //{
    //    double fineValue = 0;

    //    sql = "select DAY(getdate()) as DayValue";
    //    int dv = 0;
    //    dv = Convert.ToInt32(oo.ReturnTag(sql, "DayValue"));
    //    sql = "Select AdmissionForClassId,Card from StudentOfficialDetails where SrNo='" + lblSrno + "' and SessionName='" + Session["SessionName"].ToString() + "'";
    //    string classid = oo.ReturnTag(sql, "AdmissionForClassId");
    //    string card = oo.ReturnTag(sql, "card");


    //    //sql = "Select Top 1 ROW_NUMBER() Over (Order by MonthId desc) as Id,DueMonth from MonthMaster where SessionName='" + Session["SessionName"].ToString() + "'";
    //    sql = "Select Top 1 ROW_NUMBER() Over (Order by MonthId desc) as Id,ISNULL(dueMonth,DAteName(MOnth,DueDate)) DueMonth from MonthMaster mm";
    //    sql = sql + " inner join FeeGroupMaster fgm on (Case When ISNUmeric(mm.CardType)=1 Then convert(varchar,fgm.Id) Else fgm.FeeGroupName End)=mm.CardType";
    //    sql = sql + " and fgm.SessionName=mm.SessionName where (ClassId='" + classid + "' or ClassId is null)  and fgm.FeeGroupName='" + card + "' and mm.SessionName='" + Session["SessionName"].ToString() + "'";
    //    string selectedmonth1 = oo.ReturnTag(sql, "DueMonth");

    //    //sql = "Select MonthId from MonthMaster where DueMonth='" + selectedmonth1 + "' and SessionName='" + Session["SessionName"].ToString() + "'";



    //    sql = "Select MonthId from MonthMaster mm";
    //    sql = sql + " inner join FeeGroupMaster fgm on (Case When ISNUmeric(mm.CardType)=1 Then convert(varchar,fgm.Id) Else fgm.FeeGroupName End)=mm.CardType";
    //    sql = sql + " and fgm.SessionName=mm.SessionName where ISNULL(dueMonth,DAteName(MOnth,DueDate))='" + selectedmonth1 + "' and ";
    //    sql = sql + " (ClassId='" + classid + "' or ClassId is null)  and fgm.FeeGroupName='" + card + "' and mm.SessionName='" + Session["SessionName"].ToString() + "'";

    //    int id3 = Convert.ToInt32(oo.ReturnTag(sql, "MonthId"));

    //    string selectedmonth = month;


    //    sql = "Select MonthId from MonthMaster mm";
    //    sql = sql + " inner join FeeGroupMaster fgm on (Case When ISNUmeric(mm.CardType)=1 Then convert(varchar,fgm.Id) Else fgm.FeeGroupName End)=mm.CardType";
    //    sql = sql + " and fgm.SessionName=mm.SessionName where ISNULL(dueMonth,DAteName(MOnth,DueDate))='" + selectedmonth + "' and ";
    //    sql = sql + " (ClassId='" + classid + "' or ClassId is null)  and fgm.FeeGroupName='" + card + "' and mm.SessionName='" + Session["SessionName"].ToString() + "'";
    //    //sql = "Select MonthId from MonthMaster where DueMonth='" + selectedmonth + "' and SessionName='" + Session["SessionName"].ToString() + "'";
    //    int id2 = Convert.ToInt32(oo.ReturnTag(sql, "MonthId"));

    //    SqlDataAdapter da;
    //    double totalAmount = 0;

    //    //sql = "Select *from MonthMaster where MonthId>='" + id2 + "' and MonthId<='" + id3 + "' and SessionName='" + Session["SessionName"].ToString() + "'";

    //    sql = "Select Monthid from MonthMaster mm";
    //    sql = sql + " inner join FeeGroupMaster fgm on (Case When ISNUmeric(mm.CardType)=1 Then convert(varchar,fgm.Id) Else fgm.FeeGroupName End)=mm.CardType";
    //    sql = sql + " and fgm.SessionName=mm.SessionName where (ClassId='" + classid + "' or ClassId is null)  and fgm.FeeGroupName='" + card + "' and mm.SessionName='" + Session["SessionName"].ToString() + "'";
    //    sql = sql + " and MonthId>='" + id2 + "' and MonthId<='" + id3 + "'";

    //    da = new SqlDataAdapter(sql, con);
    //    DataTable dt2 = new DataTable();
    //    da.Fill(dt2);
    //    for (int j2 = 0; j2 < dt2.Rows.Count; j2++)
    //    {
    //        sql = "Select Distinct MonthAmount from RangeBasisFineMaster where SessionName='" + Session["SessionName"].ToString() + "'";
    //        double fineamount = Convert.ToDouble(oo.ReturnTag(sql, "MonthAmount"));
    //        totalAmount = totalAmount + fineamount;
    //    }

    //    totalAmount = totalAmount + fineValue;

    //    return totalAmount;
    //}


    //public double CalculateInSameSession(string month, string lblSrno)
    //{
    //    int a = 0, b = 0;
    //    double fineValue = 0;

    //    sql = "select DAY(getdate()) as DayValue";
    //    int dv = 0;
    //    dv = Convert.ToInt32(oo.ReturnTag(sql, "DayValue"));

    //    sql = "Select DATENAME(MONTH,GETDATE()) as MonthName";
    //    string selectedmonth1 = oo.ReturnTag(sql, "MonthName");
      
    //    sql = "Select MonthId from MonthMaster where DueMonth='" + selectedmonth1 + "' and SessionName='" + Session["SessionName"].ToString() + "'";
    //    int id3 = Convert.ToInt32(oo.ReturnTag(sql, "MonthId"));

    //    string selectedmonth = month;

    //    sql = "Select MonthId from MonthMaster where DueMonth='" + selectedmonth + "' and SessionName='" + Session["SessionName"].ToString() + "'";
    //    int id2 = Convert.ToInt32(oo.ReturnTag(sql, "MonthId"));

    //    SqlDataAdapter da;
    //    double totalAmount = 0;

    //    sql = "Select *from MonthMaster where MonthId>='" + id2 + "' and MonthId<'" + id3 + "' and SessionName='" + Session["SessionName"].ToString() + "'";
    //    da = new SqlDataAdapter(sql, con);
    //    DataTable dt2 = new DataTable();
    //    da.Fill(dt2);
    //    for (int j2 = 0; j2 < dt2.Rows.Count; j2++)
    //    {
    //        sql = "Select Distinct MonthAmount from RangeBasisFineMaster where SessionName='" + Session["SessionName"].ToString() + "'";
    //        double fineamount = Convert.ToDouble(oo.ReturnTag(sql, "MonthAmount"));
    //        totalAmount = totalAmount + fineamount;
    //    }
    //    if (id2 <= id3)
    //    {
    //        sql = "select * from RangeBasisFineMaster ";
    //        sql = sql + " where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
    //        SqlCommand cmd = new SqlCommand();
    //        try
    //        {
    //            cmd.CommandText = sql;
    //            SqlDataReader dr;
    //            cmd.Connection = con;
    //            con.Open();
    //            dr = cmd.ExecuteReader();

    //            fineValue = 0;
    //            while (dr.Read())
    //            {
    //                a = Convert.ToInt32(dr["FromDate"].ToString());
    //                b = Convert.ToInt32(dr["ToDate"].ToString());
    //                if (a <= dv && dv <= b)
    //                {
    //                    fineValue = Convert.ToDouble(dr["AmountPerday"].ToString());
    //                    break;
    //                }

    //            }
    //            con.Close();
    //        }
    //        catch (SqlException ee)
    //        {
    //            con.Close();
    //        }
    //    }
    //    totalAmount = totalAmount + fineValue;

    //    return totalAmount;
    //}

    //public double CalculateInnextSession(string month, string lblSrno)
    //{
    //    double fineValue = 0;

    //    sql = "select DAY(getdate()) as DayValue";
    //    int dv = 0;
    //    dv = Convert.ToInt32(oo.ReturnTag(sql, "DayValue"));

    //    sql = "Select Top 1 ROW_NUMBER() Over (Order by MonthId desc) as Id,DueMonth from MonthMaster where SessionName='" + Session["SessionName"].ToString() + "'";
    //    string selectedmonth1 = oo.ReturnTag(sql, "DueMonth");
       
    //    sql = "Select MonthId from MonthMaster where DueMonth='" + selectedmonth1 + "' and SessionName='" + Session["SessionName"].ToString() + "'";
    //    int id3 = Convert.ToInt32(oo.ReturnTag(sql, "MonthId"));

    //    string selectedmonth = month;

    //    sql = "Select MonthId from MonthMaster where DueMonth='" + selectedmonth + "' and SessionName='" + Session["SessionName"].ToString() + "'";
    //    int id2 = Convert.ToInt32(oo.ReturnTag(sql, "MonthId"));

    //    SqlDataAdapter da;
    //    double totalAmount = 0;

    //    sql = "Select *from MonthMaster where MonthId>='" + id2 + "' and MonthId<='" + id3 + "' and SessionName='" + Session["SessionName"].ToString() + "'";
    //    da = new SqlDataAdapter(sql, con);
    //    DataTable dt2 = new DataTable();
    //    da.Fill(dt2);
    //    for (int j2 = 0; j2 < dt2.Rows.Count; j2++)
    //    {
    //        sql = "Select Distinct MonthAmount from RangeBasisFineMaster where SessionName='" + Session["SessionName"].ToString() + "'";
    //        double fineamount = Convert.ToDouble(oo.ReturnTag(sql, "MonthAmount"));
    //        totalAmount = totalAmount + fineamount;
    //    }
                
    //    totalAmount = totalAmount + fineValue;

    //    return totalAmount;
    //}

    public double CalculateFine(string month, string lblSrno)
    {
        sql = "Select Count(*) as Count from SessionMaster where SessionName='" + Session["SessionName"].ToString() + "' and ToDate< Convert(nvarchar(11),GETDATE(),106)";
        int count = 0;
        count = Convert.ToInt16(oo.ReturnTag(sql, "Count"));
        if (count <= 0)
        {
            return CalculateInSameSession(month, lblSrno);
        }
        else
        {
            return CalculateInnextSession(month, lblSrno);
        }
    }

    public double CalculateInSameSession(string month, string lblSrno)
    {
        int a = 0, b = 0;
        double fineValue = 0;

        sql = "select DAY(getdate()) as DayValue";
        int dv = 0;
        dv = Convert.ToInt32(oo.ReturnTag(sql, "DayValue"));

        sql = "Select DATENAME(MONTH,GETDATE()) as MonthName";
        string selectedmonth1 = oo.ReturnTag(sql, "MonthName");

        //sql = "Select MonthId from MonthMaster where DueMonth='" + selectedmonth1 + "' and SessionName='" + Session["SessionName"].ToString() + "'";
        //int id3 = Convert.ToInt32(oo.ReturnTag(sql, "MonthId"));
        sql = "Select AdmissionForClassId,Card from StudentOfficialDetails where SrNo='" + lblSrno + "' and SessionName='" + Session["SessionName"].ToString() + "'";
        string classid = oo.ReturnTag(sql, "AdmissionForClassId");
        string card = oo.ReturnTag(sql, "card");


        sql = "Select MonthId from MonthMaster mm";
        sql = sql + " inner join FeeGroupMaster fgm on (Case When ISNUmeric(mm.CardType)=1 Then convert(varchar,fgm.Id) Else fgm.FeeGroupName End)=mm.CardType";
        sql = sql + " and fgm.SessionName=mm.SessionName where ISNULL(dueMonth,DAteName(MOnth,DueDate))='" + selectedmonth1 + "' and ";
        sql = sql + " (ClassId='" + classid + "' or ClassId is null)  and fgm.FeeGroupName='" + card + "' and mm.SessionName='" + Session["SessionName"].ToString() + "'";
        int id3 = 0;
        try
        {
            id3 = Convert.ToInt32(oo.ReturnTag(sql, "MonthId"));
        }
        catch
        { }

        string selectedmonth = month;

        //sql = "Select MonthId from MonthMaster where DueMonth='" + selectedmonth + "' and SessionName='" + Session["SessionName"].ToString() + "'";
        //int id2 = Convert.ToInt32(oo.ReturnTag(sql, "MonthId"));

        sql = "Select MonthId from MonthMaster mm";
        sql = sql + " inner join FeeGroupMaster fgm on (Case When ISNUmeric(mm.CardType)=1 Then convert(varchar,fgm.Id) Else fgm.FeeGroupName End)=mm.CardType";
        sql = sql + " and fgm.SessionName=mm.SessionName where ISNULL(dueMonth,DAteName(MOnth,DueDate))='" + selectedmonth + "' and ";
        sql = sql + " (ClassId='" + classid + "' or ClassId is null)  and fgm.FeeGroupName='" + card + "' and mm.SessionName='" + Session["SessionName"].ToString() + "'";
        int id2 = 0;
        try
        {
        id2 = Convert.ToInt32(oo.ReturnTag(sql, "MonthId"));
        }
        catch
        { }
        SqlDataAdapter da;
        double totalAmount = 0;

        //sql = "Select *from MonthMaster where MonthId>='" + id2 + "' and MonthId<'" + id3 + "' and SessionName='" + Session["SessionName"].ToString() + "'";
        sql = "Select Monthid from MonthMaster mm";
        sql = sql + " inner join FeeGroupMaster fgm on (Case When ISNUmeric(mm.CardType)=1 Then convert(varchar,fgm.Id) Else fgm.FeeGroupName End)=mm.CardType";
        sql = sql + " and fgm.SessionName=mm.SessionName where (ClassId='" + classid + "' or ClassId is null)  and fgm.FeeGroupName='" + card + "' and mm.SessionName='" + Session["SessionName"].ToString() + "'";
        sql = sql + " and MonthId>='" + id2 + "' and MonthId<'" + id3 + "'";

        da = new SqlDataAdapter(sql, con);
        DataTable dt2 = new DataTable();
        da.Fill(dt2);
        for (int j2 = 0; j2 < dt2.Rows.Count; j2++)
        {
            sql = "Select Distinct MonthAmount from RangeBasisFineMaster where SessionName='" + Session["SessionName"].ToString() + "'";
            double fineamount = Convert.ToDouble(oo.ReturnTag(sql, "MonthAmount"));
            totalAmount = totalAmount + fineamount;
        }
        if (id2 <= id3)
        {
            sql = "select * from RangeBasisFineMaster ";
            sql = sql + " where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandText = sql;
                SqlDataReader dr;
                cmd.Connection = con;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                dr = cmd.ExecuteReader();

                fineValue = 0;
                while (dr.Read())
                {
                    a = Convert.ToInt32(dr["FromDate"].ToString());
                    b = Convert.ToInt32(dr["ToDate"].ToString());
                    if (a <= dv && dv <= b)
                    {
                        fineValue = Convert.ToDouble(dr["AmountPerday"].ToString());
                        break;
                    }

                }
                con.Close();
            }
            catch (SqlException)
            {
                con.Close();
            }
        }
        totalAmount = totalAmount + fineValue;

        return totalAmount;
    }

    public double CalculateInnextSession(string month, string lblSrno)
    {
        double fineValue = 0;

        sql = "select DAY(getdate()) as DayValue";
        int dv = 0;
        dv = Convert.ToInt32(oo.ReturnTag(sql, "DayValue"));
        sql = "Select AdmissionForClassId,Card from StudentOfficialDetails where SrNo='" + lblSrno + "' and SessionName='" + Session["SessionName"].ToString() + "'";
        string classid = oo.ReturnTag(sql, "AdmissionForClassId");
        string card = oo.ReturnTag(sql, "card");


        //sql = "Select Top 1 ROW_NUMBER() Over (Order by MonthId desc) as Id,DueMonth from MonthMaster where SessionName='" + Session["SessionName"].ToString() + "'";
        sql = "Select Top 1 ROW_NUMBER() Over (Order by MonthId desc) as Id,ISNULL(dueMonth,DAteName(MOnth,DueDate)) DueMonth from MonthMaster mm";
        sql = sql + " inner join FeeGroupMaster fgm on (Case When ISNUmeric(mm.CardType)=1 Then convert(varchar,fgm.Id) Else fgm.FeeGroupName End)=mm.CardType";
        sql = sql + " and fgm.SessionName=mm.SessionName where (ClassId='" + classid + "' or ClassId is null)  and fgm.FeeGroupName='" + card + "' and mm.SessionName='" + Session["SessionName"].ToString() + "'";
        string selectedmonth1 = oo.ReturnTag(sql, "DueMonth");

        //sql = "Select MonthId from MonthMaster where DueMonth='" + selectedmonth1 + "' and SessionName='" + Session["SessionName"].ToString() + "'";



        sql = "Select MonthId from MonthMaster mm";
        sql = sql + " inner join FeeGroupMaster fgm on (Case When ISNUmeric(mm.CardType)=1 Then convert(varchar,fgm.Id) Else fgm.FeeGroupName End)=mm.CardType";
        sql = sql + " and fgm.SessionName=mm.SessionName where ISNULL(dueMonth,DAteName(MOnth,DueDate))='" + selectedmonth1 + "' and ";
        sql = sql + " (ClassId='" + classid + "' or ClassId is null)  and fgm.FeeGroupName='" + card + "' and mm.SessionName='" + Session["SessionName"].ToString() + "'";
        int id3 = 0;
        try
        {
            id3 = Convert.ToInt32(oo.ReturnTag(sql, "MonthId"));
        }
        catch
        {
 
        }

        string selectedmonth = month;


        sql = "Select MonthId from MonthMaster mm";
        sql = sql + " inner join FeeGroupMaster fgm on (Case When ISNUmeric(mm.CardType)=1 Then convert(varchar,fgm.Id) Else fgm.FeeGroupName End)=mm.CardType";
        sql = sql + " and fgm.SessionName=mm.SessionName where ISNULL(dueMonth,DAteName(MOnth,DueDate))='" + selectedmonth + "' and ";
        sql = sql + " (ClassId='" + classid + "' or ClassId is null)  and fgm.FeeGroupName='" + card + "' and mm.SessionName='" + Session["SessionName"].ToString() + "'";
        //sql = "Select MonthId from MonthMaster where DueMonth='" + selectedmonth + "' and SessionName='" + Session["SessionName"].ToString() + "'";
        int id2 = 0;
        try
        {
            id2 = Convert.ToInt32(oo.ReturnTag(sql, "MonthId"));
        }
        catch
        {

        }
        

        SqlDataAdapter da;
        double totalAmount = 0;

        //sql = "Select *from MonthMaster where MonthId>='" + id2 + "' and MonthId<='" + id3 + "' and SessionName='" + Session["SessionName"].ToString() + "'";

        sql = "Select Monthid from MonthMaster mm";
        sql = sql + " inner join FeeGroupMaster fgm on (Case When ISNUmeric(mm.CardType)=1 Then convert(varchar,fgm.Id) Else fgm.FeeGroupName End)=mm.CardType";
        sql = sql + " and fgm.SessionName=mm.SessionName where (ClassId='" + classid + "' or ClassId is null)  and fgm.FeeGroupName='" + card + "' and mm.SessionName='" + Session["SessionName"].ToString() + "'";
        sql = sql + " and MonthId>='" + id2 + "' and MonthId<='" + id3 + "'";

        da = new SqlDataAdapter(sql, con);
        DataTable dt2 = new DataTable();
        da.Fill(dt2);
        for (int j2 = 0; j2 < (dt2.Rows.Count); j2++)
        {
            sql = "Select Distinct MonthAmount from RangeBasisFineMaster where SessionName='" + Session["SessionName"].ToString() + "'";
            double fineamount = double.TryParse(oo.ReturnTag(sql, "MonthAmount"), out fineamount) ? fineamount : 0;
            totalAmount = totalAmount + fineamount;
        }

        totalAmount = totalAmount + fineValue;

        return totalAmount;
    }

    public int CalculateInnextSessionMonth(string lblSrno)
    {
        sql = "Select AdmissionForClassId,card from StudentOfficialDetails where SrNo='" + lblSrno + "' and SessionName='" + Session["SessionName"].ToString() + "'";
        string classid = oo.ReturnTag(sql, "AdmissionForClassId");
        string card = oo.ReturnTag(sql, "card");

        sql = "select DATENAME(mm,getdate()) as Month";
        string month = oo.ReturnTag(sql, "Month");

        sql = "select MonthName,MonthId from MonthMaster where (ClassId='" + classid + "' or ClassId is null) and SessionName='" + Session["SessionName"].ToString() + "'and MonthId<=";
        sql = sql + " (select MonthId from MonthMaster mm inner join FeeGroupMaster fgm on fgm.id=mm.CardType and fgm.SessionName=mm.SessionName where (ClassId='" + classid + "' or ClassId is null) and ISNULL(DueMonth,DATENAME(mm,DueDate))='" + month + "' and mm.SessionName='" + Session["SessionName"].ToString() + "' and fgm.FeeGroupName='" + card + "')";
        DataSet ds = new DataSet();
        ds = oo.GridFill(sql);
        DataTable dt = new DataTable();
        dt = ds.Tables[0];
        return dt.Rows.Count;
    }

    public double calculateNextMonthAmountofNextSession(string lblSrno)
    {
        double totalAmount = 0;
        sql = "Select Count(*) as Count from SessionMaster where SessionName='" + Session["SessionName"].ToString() + "' and ToDate< Convert(nvarchar(11),GETDATE(),106)";
        int count = 0;
        count = Convert.ToInt16(oo.ReturnTag(sql, "Count"));
        if (count > 0)
        {
            sql = "Select AdmissionForClassId,card from StudentOfficialDetails where SrNo='" + lblSrno + "' and SessionName='" + Session["SessionName"].ToString() + "'";
            string classid = oo.ReturnTag(sql, "AdmissionForClassId");
            string card = oo.ReturnTag(sql, "card");

            sql = "select DATENAME(mm,getdate()) as Month";
            string month = oo.ReturnTag(sql, "Month");

            sql = "select MonthName,MonthId from MonthMaster where (ClassId='" + classid + "' or ClassId is null) and SessionName='" + Session["SessionName"].ToString() + "'and MonthId<=";
            sql = sql + " (select MonthId from MonthMaster mm inner join FeeGroupMaster fgm on fgm.id=mm.CardType and fgm.SessionName=mm.SessionName where (ClassId='" + classid + "' or ClassId is null) and ISNULL(DueMonth,DATENAME(mm,DueDate))='" + month + "' and mm.SessionName='" + Session["SessionName"].ToString() + "' and fgm.FeeGroupName='" + card + "')";
            DataSet ds = new DataSet();
            ds = oo.GridFill(sql);
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            for (int j1 = 0; j1 <= dt.Rows.Count; j1++)
            {
                for (int j2 = 0; j2 < j1; j2++)
                {
                    sql = "Select Distinct MonthAmount from RangeBasisFineMaster where SessionName='" + Session["SessionName"].ToString() + "'";
                    double fineamount = double.TryParse(oo.ReturnTag(sql, "MonthAmount"), out fineamount) ? fineamount : 0;
                    totalAmount = totalAmount + fineamount;
                }
            }
        }

        return totalAmount;
    }

    string sql1 = "";

    public void ArrierTransaction()
    {
        int i;
        for (i = 0; i <= GridView1.Rows.Count - 1; i++)
        {

            Label lblSrNo = (Label)GridView1.Rows[i].FindControl("Label13");
            Label lblStEnRcode = (Label)GridView1.Rows[i].FindControl("Label12");
            Label lblFirstName = (Label)GridView1.Rows[i].FindControl("Label14");
            Label lblSecondName = (Label)GridView1.Rows[i].FindControl("Label15");
            Label lblThirdName = (Label)GridView1.Rows[i].FindControl("Label16");
            Label lblFatherName = (Label)GridView1.Rows[i].FindControl("Label17");
            Label lblClass = (Label)GridView1.Rows[i].FindControl("Label18");
            Label lblFeeGroup = (Label)GridView1.Rows[i].FindControl("Label22");
            Label lblBalanceAmt = (Label)GridView1.Rows[i].FindControl("lblBalanceAmt");
            Label lblArrierAmt = (Label)GridView1.Rows[i].FindControl("lblArrierAmt");
            Label lblDiscountAmt = (Label)GridView1.Rows[i].FindControl("lblDiscountAmt");
            Label lblTransportAmt = (Label)GridView1.Rows[i].FindControl("lblTransportAmt");
            Label lblLateFeeAmt = (Label)GridView1.Rows[i].FindControl("lblLateFeeAmt");
            Label lblTotalArrier = (Label)GridView1.Rows[i].FindControl("lblTotalArrier");
              DropDownList drpstatus = (DropDownList)GridView1.Rows[i].FindControl("drpstatus");

              if (drpstatus.Text.ToUpper() == "PENDING" || drpstatus.Text == "Left")
              {
              }
              else
              {
                  string DT = "";
                  string dd,mm,yy;
                   dd = oo.ReturnTag("Select day(getdate()) as DateDD", "DateDD");
                   mm = oo.ReturnTag("Select Month(getdate())as MonthMM", "MonthMM");
                   yy = oo.ReturnTag("Select Year(getdate()) as YearYY ", "YearYY");

                   DT = yy + "/" + mm + "/" + dd;
                  if (Convert.ToDouble(lblBalanceAmt.Text) > 0)
                  {
                      SqlCommand cmd1 = new SqlCommand();
                      cmd1.CommandText = "ArrierMastProc";
                      cmd1.CommandType = CommandType.StoredProcedure;
                      cmd1.Parameters.AddWithValue("@RegNo", lblStEnRcode.Text);
                      cmd1.Parameters.AddWithValue("@Srno", lblSrNo.Text);
                      cmd1.Parameters.AddWithValue("@ArrearAmt", lblTotalArrier.Text);
                      cmd1.Parameters.AddWithValue("@TransportAmount", lblTransportAmt.Text);
                      cmd1.Parameters.AddWithValue("@DiscountAmount", lblDiscountAmt.Text);
                      cmd1.Parameters.AddWithValue("@LateFee",lblLateFeeAmt.Text);
                      cmd1.Parameters.AddWithValue("@LastArrierAmount",lblArrierAmt.Text);
                      cmd1.Parameters.AddWithValue("@FeeAmount", lblBalanceAmt.Text);

                      cmd1.Parameters.AddWithValue("@Remark", "");                                          
                      sql = "Select SessionName from SessionMaster";
                      sql = sql + " where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
                      string sessValue = "", ff = "";
                      sessValue = oo.ReturnTag(sql, "SessionName");
                      int s1, s2;
                      s1 = Convert.ToInt32(sessValue.Substring(0, 4));
                      s2 = Convert.ToInt32(sessValue.Substring(5, 4));
                      s1 = s1 + 1;
                      s2 = s2 + 1;
                      ff = s1.ToString() + "-" + s2.ToString();
                      cmd1.Parameters.AddWithValue("@SessionName",ff  );
                      cmd1.Parameters.AddWithValue("@ArrierSession", Session["SessionName"].ToString());
                      cmd1.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                      cmd1.Parameters.AddWithValue("@ArrierDate", DT);
                      cmd1.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                      cmd1.Connection = con;
                      try
                      {
                         con.Open();
                         cmd1.ExecuteNonQuery();
                         con.Close();
                      }
                      catch (SqlException) { con.Close(); }
                  }
              }
        }
    }

    //public void StudentPromotionStudentOfficialDetailsUpdate()
    //{
    //    int i = 0;
    //    string StEnRCode = "";
    //    string sec = "", classId = "";
    //    for (i = 0; i <= GridView1.Rows.Count - 1; i++)
    //    {
    //        Label LbRegNo = (Label)GridView1.Rows[i].Cells[1].FindControl("Label12");
    //        Label LbSrNo = (Label)GridView1.Rows[i].Cells[1].FindControl("Label13");           
    //        DropDownList drpstatus = (DropDownList)GridView1.Rows[i].FindControl("drpstatus");
    //        sql = "Select Id from ClassMaster where ClassName='" + DrpClassNew.SelectedItem.ToString() + "'";
    //        sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "'";
    //        classId = oo.ReturnTag(sql, "Id");



    //        sql = "select Id from SectionMaster where SectionName='" + drpSectionNew.SelectedItem.ToString() + "' and ClassNameId=" + classId;
    //        sql = sql + "  and SessionName='" + Session["SessionName"].ToString() + "'";
    //        sec = oo.ReturnTag(sql, "Id");


    //        sql = "update StudentOfficialDetails set Promotion='" + drpstatus.SelectedItem.ToString() + "' where SrNo='" + LbSrNo.Text + "'";
    //        sql=sql+"   and class='"+classId+"'  and SessionName='"+drpSessionNew.SelectedItem.ToString()+"'";
    //        sql = sql + "  and sectionId=" + sec;

    //        SqlCommand cmd1 = new SqlCommand();
    //        cmd1.CommandType = CommandType.Text;
    //        cmd1.CommandText = sql;
    //        cmd1.Connection = con1;

    //        try
    //        {

    //            con1.Open();
    //            cmd1.ExecuteNonQuery();
    //            con1.Close();

    //        }
    //        catch (SqlException) { con1.Close(); }



    //    }
    //}

    protected void drpSessionNew_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void drpSectionNew_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    public double ClassWiseAmountForParticularMonth(string className, string monthName,string SRNO)
    {
        string med , typeOfAdd = string.Empty;
        string crd, clna = string.Empty;
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
        string ss=string.Empty;
        string WidthMonth = string.Empty;
#pragma warning disable 219
        int PPP = 0;
#pragma warning restore 219
        bool Flag = false;


        sql = " select ROW_NUMBER() OVER (ORDER BY so.Id ASC) AS SNo,so.srno as srno,sg.FirstName+' '+sg.MiddleName+' '+sg.LastName as Sname,cm.ClassName as ClassName , sm.SectionName  ";
        sql = sql + ", so.TypeOFAdmision as TypeOFAdmision, so.Medium as Medium,so.card as card,convert(nvarchar,sw.WithdrawalDate,106) as WithdrawalDate,left(DATENAME(m,sw.WithdrawalDate),3) as WMonth from StudentGenaralDetail sg ";
        sql = sql + " left join StudentOfficialDetails so on sg.SrNo=so.SrNo  ";
        sql = sql + " left join ClassMaster cm on cm.Id=so.AdmissionForClassId   ";
        sql = sql + "  left join StudentWithdrawal sw on sw.SrNo=sg.srno ";
        sql = sql + " left join SectionMaster sm on sm.Id=so.SectionId   ";
        sql = sql + " where so.SessionName='" + Session["SessionName"].ToString() + "' and so.BranchCode=" + Session["BranchCode"].ToString() + "  and so.Withdrwal is null ";
        sql = sql + "  and cm.ClassName='" + className + "'  and so.srno='"+SRNO+"'";
        sql = sql + "  and sg.SessionName='" + Session["SessionName"].ToString() + "'";
        sql = sql + "  and cm.SessionName='" + Session["SessionName"].ToString()+"'";
        sql = sql + "  and sm.SessionName='" + Session["SessionName"].ToString() + "'";
        
        SqlCommand cmd1 = new SqlCommand();
        SqlCommand cmd2 = new SqlCommand();

        cmd1.CommandText = sql;
        try
        {         
            srno = oo.ReturnTag(sql, "srno");
           
            clna = oo.ReturnTag(sql, "ClassName"); 
            typeOfAdd = oo.ReturnTag(sql, "TypeOFAdmision"); 
            med = oo.ReturnTag(sql, "Medium"); 
            crd = oo.ReturnTag(sql, "card");

                sql3 = "Select MonthId,GMonth,YMonth from gymon where month='" + monthName + "'";
                mId = Convert.ToInt32(oo.ReturnTag(sql3, "MonthId"));
                WidthMonth = oo.ReturnTag(sql, "WMonth"); 
          
                if (WidthMonth == monthName)
                {
                    PPP = 1;
                }
                else if (YearlyFeesDeposit(srno) == true)
                {
                    PPP = 2;
                }
                else if (DepositYesORNo(srno, monthName,crd) == true)
                {
                    PPP = 55;
                }
                else
                {

                    int noMonths = 0;
                    if (crd.ToUpper() == "YELLOW")
                    {
                        Fmo = oo.ReturnTag(sql3, "YMonth");
                    }
                    if (crd.ToUpper() == "GREEN")
                    {
                        Fmo = oo.ReturnTag(sql3, "GMonth");
                    }

                    sql = " select distinct ROW_NUMBER() OVER (ORDER BY fa.Id ASC) AS SrNo,Id,fa.Month, fa.FeeParticular,fa.Class,fa.FeeType,fa.FeePayment,FM.Medium,Fa.CardType ,fm.NoOfmonths as NoOfmonths from FeeAllotedForClassWise fa ";
                    sql = sql + " left join feemaster  fm on fa.Medium=fm.medium  and fa.FeeName=fm.FeeName  ";
                    sql = sql + " where fa.Class='" + clna + "'   and fa.Month='" + Fmo + "' and fa.SessionName='" + Session["SessionName"].ToString() + "'  and fa.BranchCode=" + Session["BranchCode"].ToString() + "";
                    sql = sql + " and fa.CardType='" + crd + "' and   fa.AdmissionType='" + typeOfAdd.Trim() + "'  and fa.Medium='" + med + "'";
                    sql = sql + " and fm.SessionName='" + Session["SessionName"].ToString() + "'";
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
                                    try
                                    {
                                        noMonths = Convert.ToInt32(dr["NoOfMonths"]);
                                    }
                                    catch (Exception) { noMonths = 0; }

                                }
                                catch (Exception) { }
                            }
                            Flag = true;
                        }
                        con.Close();
                  
                    if (Flag)
                    {
                        damt = FindDiscount(srno, Fmo, noMonths, tutionFees);
                    }
                  
                    if (Flag)
                    {
                        conc = FindConcession(srno, Fmo);
                    }
                   
                    sum = sum - conc - damt;

                    }
                    catch (Exception) { }

                }
                                        
        }

        catch (Exception) { con1.Close(); }
        return sum;
    }

    public bool DepositYesORNo(string srno, string monthNa,string CARD)
    {
        int yy = 0, yy1 = 0, yr = 0, dy = 0;
        yy = Convert.ToInt32(Session["SessionName"].ToString().Substring(0, 4));//2012
        yy1 = Convert.ToInt32(Session["SessionName"].ToString().Substring(5, 4));//2012
        string ss = "",Fmo="";
        ss = "Select MonthId,GMonth,YMonth from gymon where month='" + monthNa + "'";
     

        if (CARD.ToUpper() == "YELLOW")
        {
            Fmo = oo.ReturnTag(ss, "YMonth");
        }
        if (CARD.ToUpper() == "GREEN")
        {
            Fmo = oo.ReturnTag(ss, "GMonth");
        }

        if (monthNa == "Jan")
        {
            dy = 31;
            yr = yy1;
        }
        if (monthNa == "Feb")
        {

            if (yy1 % 4 == 0)
            {
                dy = 28;
            }
            else
            {
                dy = 29;
            }
            yr = yy1;
        }
        if (monthNa == "Mar")
        {
            dy = 31;
            yr = yy;
        }
        if (monthNa == "Apr")
        {
            dy = 30;
            yr = yy;
        }

        if (monthNa == "May")
        {
            dy = 31;
            yr = yy;
        }
        if (monthNa == "Jun")
        {
            dy = 30;
            yr = yy;
        }
        if (monthNa == "Jul")
        {
            dy = 31;
            yr = yy;
        }
        if (monthNa == "Aug")
        {
            dy = 31;
            yr = yy;
        }
        if (monthNa == "Sep")
        {
            dy = 30;
            yr = yy;
        }
        if (monthNa == "Oct")
        {
            dy = 31;
            yr = yy;
        }
        if (monthNa == "Nov")
        {
            dy = 30;
            yr = yy;
        }
        if (monthNa == "Dec")
        {
            dy = 31;
            yr = yy;
        }

        string fromdate = "", todate = "";

        if (monthNa == "Apr")
        {
            fromdate = "1/" + "Mar" + "/" + yr;
        }
        else
        {
            fromdate = "1/" + monthNa + "/" + yr;
        }
   
        todate = dy.ToString() + "/" + monthNa + "/" + yr;

        sql = "select * from FeeDeposite where SrNo='" + srno + "' and Cancel is null " + "  and feeMonth='" + Fmo + "'";
        sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "'";
        if (oo.Duplicate(sql))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public double FindDiscount(string srno, string FeeMonth, int noOfMonths, double tutionFees)
    {
        int j;
        double totAmt = 0;
        double discAmt = 0;
        string ss = "select * from FeeDeposite where srno='" + srno + "'  and FeeMonth='" + FeeMonth + "'";
        ss = ss + "  and SessionName='" + Session["SessionName"].ToString() + "'";
       
        sql = "select DiscountValue,DiscountType from DiscountMaster where srno='" + srno + "'";
        sql = sql + " and  sessionName='" + Session["SessionName"].ToString() + "'";
      

        try
        {
            if (oo.ReturnTag(sql, "DiscountType") == "Amount")
            {
                for (j = 1; j <= noOfMonths; j++)
                {
                totAmt = totAmt + Convert.ToDouble(oo.ReturnTag(sql, "DiscountValue")); 

                }
                discAmt = totAmt;
            }
            if (oo.ReturnTag(sql, "DiscountType") == "Percentage")
            {

                //for (j = 1; j <= noOfMonths; j++)
                //{

                    discAmt = discAmt + (tutionFees) * Convert.ToDouble(oo.ReturnTag(sql, "DiscountValue")) / 100;
                //}

            }
        }
        catch (Exception) { discAmt = 0; }
        return discAmt;
    }

    public double FindConcession(string srno, string FeeMonth)
    {
        double concession = 0;
        string ss = "select * from FeeDeposite where srno='" + srno + "'  and FeeMonth='" + FeeMonth + "' and SessionName='"+Session["SessionName"]+"'";
        try
        {
            concession = Convert.ToDouble(oo.ReturnTag(ss, "cocession"));
        }
        catch (Exception) { concession = 0; }
        return concession;
    }

    public bool YearlyFeesDeposit(string srno)
    {

        sql = "select YearlyFeeDepositYesNo from FeeDeposite where srno='" + srno + "' and SessionName='" + Session["SessionName"].ToString() + "'"; 

        if (oo.ReturnTag(sql, "YearlyFeeDepositYesNo").Trim() == "Y")
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public void BalanceAmount()
    {   
        int i;
        string ss="";
        double sum = 0;
        for (i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
        sum=0;
            Label lblSrNo = (Label)GridView1.Rows[i].FindControl("Label13");
            Label lblBalanceAmt = (Label)GridView1.Rows[i].FindControl("lblBalanceAmt");
           
            ss = " select top 1 RemainingAmount from FeeDeposite where SrNo='" + lblSrNo.Text  + "' and Cancel is null order by id  asc ";
            try
            {
                sum = sum + Convert.ToDouble(oo.ReturnTag(ss, "RemainingAmount"));
            }
            catch (Exception) { sum = 0; }

            sum = sum + Convert.ToDouble(lblBalanceAmt.Text);
            lblBalanceAmt.Text = sum.ToString();
        }
        arrier_add();
    }

    protected void DropBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadClass();
    }
    protected void DropCourseNew_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadNewSessionClass();
    }

}