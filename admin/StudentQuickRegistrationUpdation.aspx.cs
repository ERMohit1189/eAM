using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;

public partial class admin_student_registration_new : Page
{
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            loadCourse();
            loadClass();
            loadBranch();
            loadSection();
            loadMedium();
            loadBoard();
            loadCard();
            loadHouse();
         
        }
    }

    protected void DropCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadClass();
    }
    protected void DropAdmissionClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadBranch();
        loadSection();
    }
    private void loadBoard()
    {
        sql = "Select BoardName,Id from BoardMaster where BranchCode=" + Session["BranchCode"] + "";
        BAL.objBal.FillDropDown_withValue(sql, DrpBoard, "BoardName", "id");
        DrpBoard.Items.Insert(0, new ListItem("<--Select Board-->", "0"));        
    }

    private void loadMedium()
    {
        sql = "Select Medium,id from MediumMaster where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"] + "";
        BAL.objBal.FillDropDown_withValue(sql, drpMedium, "Medium", "id");
        drpMedium.Items.Insert(0, new ListItem("<--Select Medium-->", "0"));
    }

    private void loadCourse()
    {
        sql = "Select CourseName,Id from CourseMaster where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"] + "";
        BAL.objBal.FillDropDown_withValue(sql, DropCourse, "CourseName", "Id");
        DropCourse.Items.Insert(0, new ListItem("<--Select Course-->", "0"));

    }

    private void loadBranch()
    {
        sql = "Select BranchName,Id from BranchMaster where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"] + " and Course='" + DropCourse.SelectedValue.ToString() + "' and ClassId='" + DropAdmissionClass.SelectedValue.ToString() + "'";
        BAL.objBal.FillDropDown_withValue(sql, DropBranch, "BranchName", "Id");
        DropBranch.Items.Insert(0, new ListItem("<--Select Stream-->", "0"));
    }

    private void loadClass()
    {
        sql = "Select Id,ClassName from ClassMaster";
        sql = sql + " where (Course='" + DropCourse.SelectedValue.ToString() + "' or Course is NULL) and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and CIDOrder !=0 ";
        BAL.objBal.FillDropDown_withValue(sql, DropAdmissionClass, "ClassName", "Id");
        DropAdmissionClass.Items.Insert(0, new ListItem("<--Select Class-->", "0"));
    }

    private void loadSection()
    {
        sql = "select SectionName,Id from SectionMaster where ClassNameId='" + DropAdmissionClass.SelectedValue.ToString() + "'";
        sql = sql + "  and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        BAL.objBal.FillDropDown_withValue(sql, DropSection, "SectionName","Id");
        DropSection.Items.Insert(0, new ListItem("<--Select Section-->", "0"));
    }
    private void loadHouse()
    {
        sql = "select HouseName from HouseMaster where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"] + "";
        BAL.objBal.FillDropDownWithOutSelect(sql, Drophouse, "HouseName");
        Drophouse.Items.Insert(0, new ListItem("<--Select House Name-->", "0"));
    }

    private void loadCard()
    {
        sql = "Select FeeGroupName from FeeGroupMaster ";
        sql = sql + " where  BranchCode=" + Session["BranchCode"].ToString() + " and SessionName='" + Session["SessionName"].ToString() + "'";
        BAL.objBal.FillDropDown_withValue(sql, Dropcard, "FeeGroupName","FeeGroupName");
        Dropcard.Items.Insert(0, new ListItem("<--Select Fee Category-->", "0"));
    }
    protected void LinkButton14_Click(object sender, EventArgs e)
    {      
       SaveData();      
    }

    private void SaveData()
    {
        if (txtsrno.Text.Trim() != string.Empty)
        {
            string msg = "";

            List<SqlParameter> param = new List<SqlParameter>();


            param.Add(new SqlParameter("@Srno", txtsrno.Text.Trim().ToString()));
            param.Add(new SqlParameter("@FirstName", txtFirstNa.Text.Trim()));
            param.Add(new SqlParameter("@MiddleName", txtMidNa.Text.Trim()));
            param.Add(new SqlParameter("@LastName", txtlast.Text.Trim()));
            param.Add(new SqlParameter("@DOB", txtStudentDOB.Text.Trim()));
            param.Add(new SqlParameter("@MobileNo", txtMobile.Text.Trim()));
            param.Add(new SqlParameter("@Gender", rbGender.SelectedItem.Text.Trim()));
            param.Add(new SqlParameter("@FatherName", txtFatherName.Text.Trim()));
            param.Add(new SqlParameter("@FamilyContactNo", txtFamilyContactNo.Text.Trim()));
            param.Add(new SqlParameter("@DOA", txtDOA.Text.Trim()));
            param.Add(new SqlParameter("@Courseid", DropCourse.SelectedValue.ToString()));
            param.Add(new SqlParameter("@Classid", DropAdmissionClass.SelectedValue.ToString()));
            param.Add(new SqlParameter("@Branchid", DropBranch.SelectedValue.ToString()));
            param.Add(new SqlParameter("@Sectionid", DropSection.SelectedValue.ToString()));
            param.Add(new SqlParameter("@Medium", drpMedium.SelectedItem.Text.ToString()));
            param.Add(new SqlParameter("@Board", DrpBoard.SelectedItem.Text.Trim()));
            param.Add(new SqlParameter("@TypeOfAdd", DrpTypeofAdmission.SelectedItem.Text.Trim()));
            param.Add(new SqlParameter("@Card", Dropcard.SelectedItem.Text.Trim()));
            param.Add(new SqlParameter("@HouseName", Drophouse.SelectedItem.Text.Trim()));
            if (rbTransport.SelectedIndex == 0)
            {
                param.Add(new SqlParameter("@TransportReq", "Yes"));
                if (drpTransportMOD.SelectedIndex == 0)
                {
                    param.Add(new SqlParameter("@ModForTransport", "I"));
                }
                else
                {
                    param.Add(new SqlParameter("@ModForTransport", drpTransportMOD.SelectedValue.Trim()));
                }
            }
            else
            {
                param.Add(new SqlParameter("@TransportReq", "No"));
                param.Add(new SqlParameter("@ModForTransport", "I"));
            }
            param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString().Trim()));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString().Trim()));
            param.Add(new SqlParameter("@CardNo", txtCardNo.Text.Trim()));
            param.Add(new SqlParameter("@MODForFeeDeposit", drpFeeDepositMOD.SelectedValue.ToString().Trim()));
            SqlParameter para = new SqlParameter("@Msg", "");
            para.Direction = ParameterDirection.Output;
            para.Size = 0x100;
            param.Add(para);
            msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("StudentQuickRegUpdateProc", param);
            if (msg == string.Empty)
            {
                BAL.objBal.MessageBoxforUpdatePanel("Record, Updated successfully.", LinkButton14);
                cleartextBox();
                setDropDownIndex();
                setRbIndex();
            }
            else
            {
                BAL.objBal.MessageBoxforUpdatePanel(msg, LinkButton14);
            }
        }
        else
        {
            BAL.objBal.MessageBoxforUpdatePanel("Sorry, Please Enter S.R. No.", LinkButton14);
        }
    }

    private void cleartextBox()
    {
        txtsrno.Text = "";
        txtFirstNa.Text = "";
        txtMidNa.Text = "";
        txtlast.Text = "";
        txtStudentDOB.Text = "";
        txtMobile.Text = "";
        txtFatherName.Text = "";
        txtFamilyContactNo.Text = "";
        txtCardNo.Text = "";
        string txtIds = "txtStudentDOB,txtDOA";
        BAL.objBal.clearTextBoxbyJavaScript(txtIds, LinkButton14);
    }
    private void setDropDownIndex()
    {
        DropAdmissionClass.SelectedIndex = 0;
        DropSection.SelectedIndex = 0;
        DropBranch.SelectedIndex = 0;
        DropCourse.SelectedIndex = 0;
        drpMedium.SelectedIndex = 0;
        drpTransportMOD.SelectedIndex = 0;
        DrpBoard.SelectedIndex = 0;
        Drophouse.SelectedIndex = 0;
        Dropcard.SelectedIndex = 0;
        DrpTypeofAdmission.SelectedIndex = 1;
    }

    private void setRbIndex()
    {
        rbTransport.SelectedIndex = 1;
        rbGender.SelectedIndex = 0;
    }
    protected void lnkShow_Click(object sender, EventArgs e)
    {

        sql = "select Withdrwal from StudentOfficialDetails where " + DrpEnter.SelectedValue.ToString() + "=" + "'" + TxtEnter.Text.Trim() + "'";
        sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and (Promotion is null or Promotion<>'Cancelled')";
        if (BAL.objBal.ReturnTag(sql, "Withdrwal") != "")
        {
            LinkButton14.Enabled = false;
            BAL.objBal.MessageBox("This Student is already Withdrawn!", this.Page);
        }
        else
        {
            LinkButton14.Enabled = true;
        }
        sql = "select COUNT(*) as Counter from FeeDeposite where SrNo='" + TxtEnter.Text + "' and BranchCode=" + Session["BranchCode"] + " and Cancel is NULL";
        if (Convert.ToInt32(BAL.objBal.ReturnTag(sql, "Counter")) != 0)
        {
            txtsrno.ReadOnly = true;
        }
        else
        {
            txtsrno.ReadOnly = false;
        }
        sql = "select srno,StEnRCode from StudentOfficialDetails where " + DrpEnter.SelectedValue.ToString() + "=" + "'" + TxtEnter.Text.Trim() + "'";
        sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and (Promotion is null or Promotion<>'Cancelled')";
        if (DrpEnter.SelectedValue.ToString() == "<--Select-->")
        {
            BAL.objBal.MessageBox("Please <--Select--> Condition", this.Page);
        }

        else if (BAL.objBal.Duplicate(sql) == false)
        {
            BAL.objBal.MessageBox("Invalid " + DrpEnter.SelectedItem.ToString() + "!", this.Page);
        }

        else
        {
            loadStudentRecord();
        }
    }

    private void loadStudentRecord()
    {
        DateTime dt = new DateTime();
        string sql1 = string.Empty;
        sql = "Select *from AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "'," + Session["BranchCode"].ToString() + ") where " + DrpEnter.SelectedValue.ToString() + "=" + "'" + TxtEnter.Text.Trim() + "'";
        txtsrno.Text = BAL.objBal.ReturnTag(sql, "srno");
        txtFirstNa.Text = BAL.objBal.ReturnTag(sql, "FirstName");
        txtMidNa.Text = BAL.objBal.ReturnTag(sql, "MiddleName");
        txtlast.Text = BAL.objBal.ReturnTag(sql, "LastName");
        txtStudentDOB.Text = DateTime.TryParse(BAL.objBal.ReturnTag(sql, "DOB"), out dt) ? dt.ToString("yyyy MMM dd") : "";
        txtMobile.Text = BAL.objBal.ReturnTag(sql, "MobileNumber");
        txtFatherName.Text = BAL.objBal.ReturnTag(sql, "FatherName");
        txtFamilyContactNo.Text = BAL.objBal.ReturnTag(sql, "FamilyContactNo");
        dt = new DateTime();
        txtDOA.Text = DateTime.TryParse(BAL.objBal.ReturnTag(sql, "DateOfAdmiission"), out dt) ? dt.ToString("yyyy MMM dd") : "";
        string courseid = BAL.objBal.ReturnTag(sql, "CourseId");
        DropCourse.SelectedValue = courseid; 
        string cardno = BAL.objBal.ReturnTag(sql, "cardno");
        txtCardNo.Text = cardno;
        string gender = BAL.objBal.ReturnTag(sql, "Gender1").Trim();
        string classid = BAL.objBal.ReturnTag(sql, "ClassId");
        string branchid = BAL.objBal.ReturnTag(sql, "BranchId");
        string sectionid = BAL.objBal.ReturnTag(sql, "SectionID");
        string medium = BAL.objBal.ReturnTag(sql, "Medium1");
        string board = BAL.objBal.ReturnTag(sql, "Board");
        string typeodadd = BAL.objBal.ReturnTag(sql, "TypeOFAdmision");
        string card = BAL.objBal.ReturnTag(sql, "card1");
        string house = BAL.objBal.ReturnTag(sql, "HouseName1");
        string transportreq = BAL.objBal.ReturnTag(sql, "TransportRequired");
        string modfortransport = BAL.objBal.ReturnTag(sql, "ModForTransport");
        

        loadClass();
        DropAdmissionClass.SelectedValue = classid;
        loadSection();
        DropSection.SelectedValue = sectionid;
        loadBranch();
        DropBranch.SelectedValue = branchid;
        try
        {
            drpMedium.SelectedValue = drpMedium.Items.FindByText(medium).Value;
        }
        catch
        {
        }
        DrpBoard.SelectedValue = DrpBoard.Items.FindByText(board).Value;
        DrpTypeofAdmission.SelectedValue = typeodadd;
        try
        {
            Dropcard.SelectedValue = Dropcard.Items.FindByText(card).Value;
        }
        catch
        {
        }
        try
        {
            
            Drophouse.SelectedValue = Drophouse.Items.FindByText(house).Value;
        }
        catch
        {
        }
        drpTransportMOD.SelectedValue = modfortransport;
        rbTransport.SelectedValue = rbTransport.Items.FindByText(transportreq).Value;
        rbGender.SelectedValue = gender;

        if (rbTransport.SelectedIndex == 0)
        {
            drpTransportMODB.Style.Add("display","block");
        }
        else
        {
            drpTransportMODB.Style.Add("display", "none");
        }

        
    }
    protected void lnkRegistration_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Common/update_student_registration.aspx");
    }
}