using System;
using System.Data;
using System.Web.UI;

public partial class SuperAdmin_SetDefaultValue : Page
{
    BAL.Set_DefaultSelectedValue objBal = new BAL.Set_DefaultSelectedValue();
    private string sql = "";
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)

    {
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        if (!IsPostBack)
        {
            loadRecord();
            drpDefaultvalue.Visible = false;
            txtDefaultvalue.Visible = false;
        }
    }
    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        save();
    }

    private void save()
    {
        string msg;
        try
        {
            objBal.defaultvalueof = drpDefaultvalueof.SelectedValue.ToString();
            if (drpDefaultvalueof.SelectedItem.ToString() == "Nationality" || drpDefaultvalueof.SelectedItem.ToString() == "Mother Tongue" || drpDefaultvalueof.SelectedItem.ToString() == "Home Town" || drpDefaultvalueof.SelectedItem.ToString() == "Caste")
            {
                objBal.defaultvalue = txtDefaultvalue.Text.Trim();
            }
            else
            {
                objBal.defaultvalue = drpDefaultvalue.SelectedValue;
            }
            objBal.SessionName = Session["SessionName"].ToString();
            objBal.LoginName = Session["LoginName"].ToString();
            objBal.BranchCode = Convert.ToInt16(Session["BranchCode"].ToString());

            msg = DAL.objDal.Set_DefaultValue(objBal);
            if (msg != string.Empty)
            {
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, objBal.MessageType(msg, new Control(), objBal), "S");
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message;
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, objBal.MessageType(msg, new Control(), objBal), "W");
        }
      

        //BAL.objBal.MessageBoxforUpdatePanel(objBal.MessageType(msg, new Control(), objBal), lnkSubmit);

    }

    private void loadRecord()
    {
        string msg;
        try
        {
            objBal.defaultvalueof = drpDefaultvalueof.SelectedValue.ToString();
            objBal.BranchCode = int.Parse(Session["BranchCode"].ToString());
            if (drpDefaultvalueof.SelectedItem.ToString() == "Nationality" || drpDefaultvalueof.SelectedItem.ToString() == "Mother Tongue" || drpDefaultvalueof.SelectedItem.ToString() == "Home Town" || drpDefaultvalueof.SelectedItem.ToString() == "Caste")
            {
                txtDefaultvalue.Text = (string)DAL.objDal.Get_SetDefaultValue(objBal).Item2;
            }
            else
            {
                drpDefaultvalue.SelectedValue = (string)DAL.objDal.Get_SetDefaultValue(objBal).Item2;
            }
            msg = (string)DAL.objDal.Get_SetDefaultValue(objBal).Item1;
            if(msg!=string.Empty)
            {
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, objBal.MessageType(msg, new Control(), objBal), "S");
            }
       
        }
        catch (Exception ex)
        {
            msg = ex.Message;
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, objBal.MessageType(msg, new Control(), objBal), "W");
            //BAL.objBal.MessageBoxforUpdatePanel(objBal.MessageType(msg, new Control(), objBal), this.Page);
        }
    }
    protected void drpDefaultvalueof_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpDefaultvalueof.SelectedIndex == 0)
        {
            divVal.Visible = false;
            divbtn.Visible = false;
        }
        else
        {
            divVal.Visible = true;
            divbtn.Visible = true;
        }
        if (drpDefaultvalueof.SelectedValue == "Country")
        {
            showdrp();
            loadCountry();
            loadRecord();
        }
        else if (drpDefaultvalueof.SelectedValue == "State")
        {
            showdrp();
            loadState();
            loadRecord();
        }
        else if (drpDefaultvalueof.SelectedValue == "City")
        {
            showdrp();
            loadCity();
            loadRecord();
        }
        else if (drpDefaultvalueof.SelectedValue == "Blood Group")
        {
            showdrp();
            loadBloodGroup();
            loadRecord();
        }
        else if (drpDefaultvalueof.SelectedValue == "Board")
        {
            showdrp();
            loadBoard();
            loadRecord();
        }
        else if (drpDefaultvalueof.SelectedValue == "Medium")
        {
            showdrp();
            loadMedium();
            loadRecord();
        }
        else if (drpDefaultvalueof.SelectedValue == "Nationality")
        {
            showtxt();
            loadRecord();
        }
        else if (drpDefaultvalueof.SelectedValue == "MotherTongue")
        {
            showtxt();
            loadRecord();
        }
        else if (drpDefaultvalueof.SelectedValue == "HomeTown")
        {
            showtxt();
            loadRecord();
        }
        else if (drpDefaultvalueof.SelectedValue == "TypeofAdmission")
        {
            showdrp();
            loadTypeofAdd();
            loadRecord();
        }
        else if (drpDefaultvalueof.SelectedValue == "Religion")
        {
            showdrp();
            loadReligion();
            loadRecord();
        }
        else if (drpDefaultvalueof.SelectedValue == "Category")
        {
            showdrp();
            loadCaste();
            loadRecord();
        }
        else if (drpDefaultvalueof.SelectedValue == "Caste")
        {
            showtxt();
            loadRecord();
        }
        else if (drpDefaultvalueof.SelectedValue == "ReligionCategory")
        {
            showdrp();
            loadReligion();
            loadRecord();
        }
        else if (drpDefaultvalueof.SelectedValue == "Occupation")
        {
            showdrp();
            loadOccupation();
            loadRecord();
        }
        else if (drpDefaultvalueof.SelectedValue == "FeeGroup")
        {
            showdrp();
            loadFeeGroup();
        }
        else if (drpDefaultvalueof.SelectedValue == "House")
        {
            showdrp();
            loadHouse();
            loadRecord();
        }
        else if (drpDefaultvalueof.SelectedValue == "Title")
        {
            showdrp();
            loadMrMissTitle();
            loadRecord();
        }
        else if (drpDefaultvalueof.SelectedValue == "MaritalStatus")
        {
            showdrp();
            loadMaritalStatus();
            loadRecord();
        }
        else if (drpDefaultvalueof.SelectedValue == "ModeofDeposit")
        {
            showdrp();
            loadModeofDeposit();
            loadRecord();
        }
        else if (drpDefaultvalueof.SelectedValue == "ModeofPayment")
        {
            showdrp();
            loadModeofPayment();
            loadRecord();
        }
        else if (drpDefaultvalueof.SelectedValue == "ModeofEducation")
        {
            showdrp();
            ModeofEducation();
            loadRecord();
        }
        else if (drpDefaultvalueof.SelectedValue == "SemesterType")
        {
            showdrp();
            SemesterType();
            loadRecord();
        }

    }

    private void showdrp()
    {
        drpDefaultvalue.Visible = true;
        txtDefaultvalue.Visible = false;
    }

    private void showtxt()
    {
        txtDefaultvalue.Visible = true;
        drpDefaultvalue.Visible = false;
    }

    private void loadCountry()
    {
        sql = "Select CountryName,id from countryMaster";
        BAL.objBal.FillDropDown_withValue(sql, drpDefaultvalue, "CountryName", "id");
    }

    private void loadState()
    {
        sql = @"Select StateName,Id from StateMaster where 
                CountryId=(Select defaultvalue from(Select defaultvalue from DefaultSelectedValue where defaultvalueof='Country' and BranchCode="+ Session["BranchCode"] + ") as T1)";
        BAL.objBal.FillDropDown_withValue(sql, drpDefaultvalue, "StateName", "id");
    }

    private void loadCity()
    {
        sql = @"Select CityName,Id from CityMaster where 
                StateId=(Select defaultvalue from(Select defaultvalue from DefaultSelectedValue where defaultvalueof='State' and BranchCode=" + Session["BranchCode"] + ") as T1)";
        BAL.objBal.FillDropDown_withValue(sql, drpDefaultvalue, "CityName", "id");
    }

    private void loadBloodGroup()
    {
        sql = "Select BloodGroupName,BloodGroupId from BloodGroupMaster";
        BAL.objBal.FillDropDown_withValue(sql, drpDefaultvalue, "BloodGroupName", "BloodGroupId");
    }

    private void loadBoard()
    {
        sql = "Select BoardName,id from BoardMaster where  BranchCode=" + Session["BranchCode"] + "";
        BAL.objBal.FillDropDown_withValue(sql, drpDefaultvalue, "BoardName", "id");
    }

    private void loadMedium()
    {
        sql = "Select Medium,id from MediumMaster where  BranchCode=" + Session["BranchCode"] + "";
        BAL.objBal.FillDropDown_withValue(sql, drpDefaultvalue, "Medium", "id");
    }

    private void loadTypeofAdd()
    {
        dt = new DataTable();
        dt.Columns.Add("TypeofAdd");
        dt.Rows.Add("New");
        dt.Rows.Add("Old");
        drpDefaultvalue.DataSource = dt;
        drpDefaultvalue.DataTextField = "TypeofAdd";
        drpDefaultvalue.DataValueField = "TypeofAdd";
        drpDefaultvalue.DataBind();
    }

    private void loadReligion()
    {
        sql = "select ReligionName,ReligionId from ReligionMaster";
        BAL.objBal.FillDropDown_withValue(sql, drpDefaultvalue, "ReligionName", "ReligionId");
    }

    private void loadCaste()
    {
        sql = "select CasteName,CasteId from CasteMaster";
        BAL.objBal.FillDropDown_withValue(sql, drpDefaultvalue, "CasteName", "CasteId");
    }

    private void loadOccupation()
    {
        sql = "Select DesignationName from GuardianDesMaster where DesignationName not like 'House%'";
        BAL.objBal.FillDropDownWithOutSelect(sql, drpDefaultvalue , "DesignationName");
    }

    private void loadFeeGroup()
    {
        sql = "Select FeeGroupName from FeeGroupMaster ";
        sql = sql + " where  BranchCode=" + Session["BranchCode"].ToString() + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"] + "";
        BAL.objBal.FillDropDownWithOutSelect(sql, drpDefaultvalue, "FeeGroupName");
    }

    private void loadHouse()
    {
        sql = "select HouseName from HouseMaster where  BranchCode=" + Session["BranchCode"] + "";
        BAL.objBal.FillDropDownWithOutSelect(sql, drpDefaultvalue, "HouseName");
    }

    private void loadMrMissTitle()
    {
        dt = new DataTable();
        dt.Columns.Add("MrMiss");

        dt.Rows.Add("Mr.");
        dt.Rows.Add("Mrs.");
        dt.Rows.Add("Miss");

        drpDefaultvalue.DataSource = dt;
        drpDefaultvalue.DataTextField = "MrMiss";
        drpDefaultvalue.DataValueField = "MrMiss";
        drpDefaultvalue.DataBind();
    }

    private void loadMaritalStatus()
    {
        dt = new DataTable();
        dt.Columns.Add("MaritalStatus");
        dt.Rows.Add("Single");
        dt.Rows.Add("Married");
        dt.Rows.Add("Separated");
        dt.Rows.Add("Divorced");
        dt.Rows.Add("Widowed"); 
        drpDefaultvalue.DataSource = dt;
        drpDefaultvalue.DataTextField = "MaritalStatus";
        drpDefaultvalue.DataValueField = "MaritalStatus";
        drpDefaultvalue.DataBind();
    }

    private void loadModeofPayment()
    {
        dt = new DataTable();
        dt.Columns.Add("MOP");

        dt.Rows.Add("Cash");
        dt.Rows.Add("Cheque");
        dt.Rows.Add("DD");
        dt.Rows.Add("Online");
     
        drpDefaultvalue.DataSource = dt;
        drpDefaultvalue.DataTextField = "MOP";
        drpDefaultvalue.DataValueField = "MOP";
        drpDefaultvalue.DataBind();
    }

    private void loadModeofDeposit()
    {
        dt = new DataTable();
        dt.Columns.Add("MOD");
        dt.Columns.Add("value");

        DataRow dr = dt.NewRow();
        dr["MOD"] = "Installment";
        dr["value"] = "I";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["MOD"] = "Quarterly";
        dr["value"] = "Q";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["MOD"] = "Semester";
        dr["value"] = "S";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["MOD"] = "Annual";
        dr["value"] = "A";
        dt.Rows.Add(dr);

        drpDefaultvalue.DataSource = dt;
        drpDefaultvalue.DataTextField = "MOD";
        drpDefaultvalue.DataValueField = "value";
        drpDefaultvalue.DataBind();
    }
    private void ModeofEducation()
    {
        dt = new DataTable();
        dt.Columns.Add("ModeofEducation");

        dt.Rows.Add("Annual");
        dt.Rows.Add("Semester");
        dt.Rows.Add("Trimester");

        drpDefaultvalue.DataSource = dt;
        drpDefaultvalue.DataTextField = "ModeofEducation";
        drpDefaultvalue.DataValueField = "ModeofEducation";
        drpDefaultvalue.DataBind();
    }
    private void SemesterType()
    {
        dt = new DataTable();
        dt.Columns.Add("SemesterType");

        dt.Rows.Add("N/A");
        dt.Rows.Add("Even");
        dt.Rows.Add("Odd");

        drpDefaultvalue.DataSource = dt;
        drpDefaultvalue.DataTextField = "SemesterType";
        drpDefaultvalue.DataValueField = "SemesterType";
        drpDefaultvalue.DataBind();
    }

}