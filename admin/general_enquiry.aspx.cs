using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;


public partial class admin_general_enquiry : Page
{ 
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql ,ss = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        txtsubject.Focus();
        
        if ( Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        con = oo.dbGet_connection();
        //con.ConnectionString = ConfigurationManager.ConnectionStrings["Menu"].ConnectionString;
        Campus camp = new Campus(); camp.LoadLoader(loader);

        if ( Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }


        if (!IsPostBack)            
        {

            try
            {
                CheckValueADDDeleteUpdate();
            }
            catch (Exception) { }


            loadCountry();
            loadState();
            loadCity();
            

            oo.AddDateMonthYearDropDown(drpEnyear, drpenmonth, drpendate);
            oo.AddDateMonthYearDropDown(drpYYPanel, drpMMPanel, drpDDPanel);
            FindCurrentDateandSetinDropDown();


            sql = "select ROW_NUMBER() OVER (ORDER BY ad.Id desc) AS SrNo,ad.Id,ad.EnquiryNo,convert(nvarchar,Ad.Date,106) as Date,Cm.CountryName,CS.CityName,Sm.StateName, Ad.Subject ,Ad.Name ,Ad.ContactNo ,Ad.MobileNo ,Ad.EMail ,";
            sql = sql + "  Ad.Address  from GeneralEnquiry AD";
            sql = sql + " left join CountryMaster CM on AD.CountryId=CM.Id";
            sql = sql + " left join StateMaster SM on AD.StateId=SM.Id";
            sql = sql + " left join CityMaster CS on AD.CityId=CS.Id ";
            sql=sql+"  where ad.SessionName='"+Session["SessionName"].ToString()+"' and ad.BranchCode='"+Session["BranchCode"].ToString()+"'";
            sql=sql+"    order by ad.Id desc";

            Grd.DataSource = oo.GridFill(sql);
            Grd.DataBind();



        }
        try
        {

            Grd.FooterRow.Visible = false;
        }
        catch (Exception) { }
    }

    private void loadCountry()
    {
        sql = "Select CountryName,id from countryMaster";
        BAL.objBal.FillDropDown_withValue(sql, drCountry, "CountryName", "id");
        BAL.objBal.FillDropDown_withValue(sql, drpCountryPanel, "CountryName", "id");
        try
        {
        BLL objBll = new BLL();
        objBll.loadDefaultvalue("Country", drCountry);
        }
        catch
        {
        }
    }

    private void loadState()
    {
        sql = "Select StateName,Id from StateMaster where CountryId='" + drCountry.SelectedValue.ToString() + "'";
        BAL.objBal.FillDropDown_withValue(sql, drstate, "StateName", "id");
        BAL.objBal.FillDropDown_withValue(sql, drpStatePanel, "StateName", "id");
        try
        {
        BLL objBll = new BLL();
        objBll.loadDefaultvalue("State", drstate);
        }
        catch
        {
        }
    }

    private void loadCity()
    {
        sql = "Select CityName,id from CityMaster where StateId='" + drstate.SelectedValue.ToString() + "'";
        BAL.objBal.FillDropDown_withValue(sql, drcity, "CityName", "id");
        BAL.objBal.FillDropDown_withValue(sql, drpCityPanel, "CityName", "id");
        try
        {
        BLL objBll = new BLL();
        objBll.loadDefaultvalue("City", drcity);
        }
        catch
        {
        }
    }

    protected void drCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        string CCode = "";
        sql = "select Id from CountryMaster where CountryName='" + drCountry.SelectedItem.ToString() + "'";

        CCode = oo.ReturnTag(sql, "id");
        sql = "Select StateName from StateMaster where Countryid=" + CCode;

        oo.FillDropDown(sql, drstate, "StateName");
    }
    protected void drstate_SelectedIndexChanged(object sender, EventArgs e)
    {


        string CC = "";
        sql = "select Id from StateMaster where StateName='" + drstate.SelectedItem.ToString() + "'";
        CC = oo.ReturnTag(sql, "id");
        sql = "Select CityName from CityMaster where StateId=" + CC;
        oo.FillDropDown(sql, drcity, "CityName");

    }
    protected void drcity_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void drpenmonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(drpEnyear, drpenmonth, drpendate);
    }
    protected void drpEnyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(drpEnyear, drpenmonth, drpendate);
    }
    public void FindCurrentDateandSetinDropDown()
    {
        string dd = "", mm = "", yy = "";


        dd = oo.ReturnTag("Select day(getdate()) as DateDD", "DateDD");
        mm = oo.ReturnTag("Select Month(getdate())as MonthMM", "MonthMM");
        yy = oo.ReturnTag("Select Year(getdate()) as YearYY ", "YearYY");

        drpEnyear.Text = yy;
        if (mm == "1")
        {
            drpenmonth.Text = "Jan";
        }
        else if (mm == "2")
        {
            drpenmonth.Text = "Feb";
        }
        else if (mm == "3")
        {
            drpenmonth.Text = "Mar";
        }
        else if (mm == "4")
        {
            drpenmonth.Text = "Apr";
        }
        else if (mm == "5")
        {
            drpenmonth.Text = "May";
        }
        else if (mm == "6")
        {
            drpenmonth.Text = "Jun";

        }
        else if (mm == "7")
        {
            drpenmonth.Text = "Jul";
        }
        else if (mm == "8")
        {
            drpenmonth.Text = "Aug";
        }
        else if (mm == "9")
        {
            drpenmonth.Text = "Sep";
        }
        else if (mm == "10")
        {
            drpenmonth.Text = "Oct";
        }
        else if (mm == "11")
        {
            drpenmonth.Text = "Nov";
        }
        else if (mm == "12")
        {
            drpenmonth.Text = "Dec";
        }


        drpendate.Text = dd;
    }
    public string IDGeneration(string x)
    {
        string xx = "";
        if (x.Length == 1)
        {
            xx = "00000" + x;

        }
        else if (x.Length == 2)
        {
            xx = "0000" + x;
        }
        else if (x.Length == 3)
        {
            xx = "000" + x;

        }
        else if (x.Length == 4)
        {
            xx = "00" + x;
        }
        else if (x.Length == 5)
        {
            xx = xx + "0" + x;
        }
        else
        {
            xx = x;
        }
        return "GE/"+Session["SessionName"].ToString()+"/"+xx;
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {


        sql = "select Max(Id)+1 as Id  from GeneralEnquiry ";

        ss = oo.ReturnTag(sql, "Id");
        if (ss == "")
        {
            ss = IDGeneration("1");

        }
        else
        {
            ss = IDGeneration(ss);
        }

        
        
        
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "GeneralEnquiryProc";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;

        String fromDate = "";
        fromDate = drpEnyear.SelectedItem.ToString() + "/" + drpenmonth.SelectedItem.ToString() + "/" + drpendate.SelectedItem.ToString();
        cmd.Parameters.AddWithValue("@Date", fromDate);

        cmd.Parameters.AddWithValue("@Subject", txtsubject.Text.ToString());
        cmd.Parameters.AddWithValue("@Name", txtnamead.Text.ToString());
        cmd.Parameters.AddWithValue("@ContactNo", txtcontAdm.Text.ToString());
        cmd.Parameters.AddWithValue("@MobileNo", txtmobAdmiss.Text.ToString());
        cmd.Parameters.AddWithValue("@EMail", txtemaAdmiss.Text.ToString());
        cmd.Parameters.AddWithValue("@Address", txtAddressAdmiss.Text.ToString());
        cmd.Parameters.AddWithValue("@OrgName", txtorg.Text.ToString());

      //  string ss = "";
        string DD2 = "";
        sql = "Select Id from CountryMaster where CountryName='" + drCountry.SelectedItem.ToString() + "'";
        DD2 = oo.ReturnTag(sql, "Id");
        cmd.Parameters.AddWithValue("@CountryId", DD2);
        string DD1 = "";
        sql = "Select Id from StateMaster where StateName='" + drstate.SelectedItem.ToString() + "'";
        DD1 = oo.ReturnTag(sql, "Id");
        cmd.Parameters.AddWithValue("@StateId", DD1);
        string SD = "";
        sql = "Select Id from CityMaster where CityName='" + drcity.SelectedItem.ToString() + "'";
        SD = oo.ReturnTag(sql, "Id");
        cmd.Parameters.AddWithValue("@CityId", SD);



        cmd.Parameters.AddWithValue("@Remark", txtreferenceaden.Text.ToString());
      
        cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());//Session["Login"].ToString());
        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
        cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
        cmd.Parameters.AddWithValue("@EnquiryNo", ss);
        try
        {

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully. General Enquiry No. is : " + ss, "S"); 
            oo.ClearControls(this.Page);


            sql = "select ROW_NUMBER() OVER (ORDER BY ad.Id desc) AS SrNo,ad.Id as Id,ad.EnquiryNo,convert(nvarchar,Ad.Date,106) as Date,Cm.CountryName,CS.CityName,Sm.StateName,";
            sql = sql + " Ad.Subject ,Ad.Name ,Ad.OrgName as Organization,Ad.ContactNo ,Ad.MobileNo ,Ad.EMail ,";
            sql = sql + " Ad.Address  from GeneralEnquiry AD";
            sql = sql + " left join CountryMaster CM on AD.CountryId=CM.Id";
            sql = sql + " left join StateMaster SM on AD.StateId=SM.Id";
            sql = sql + " left join CityMaster CS on AD.CityId=CS.Id ";
            sql = sql + " where ad.SessionName='" + Session["SessionName"].ToString() + "' and ad.BranchCode=" + Session["BranchCode"].ToString() + "";
            sql = sql + " order by ad.Id desc";
            Grd.DataSource = oo.GridFill(sql);
            Grd.DataBind();

        }
        catch (SqlException) { }






    }
    protected void drpCountryPanel_SelectedIndexChanged(object sender, EventArgs e)
    {
        string CCode = "";
        sql = "select Id from CountryMaster where CountryName='" + drpCountryPanel.SelectedItem.ToString() + "'";

        CCode = oo.ReturnTag(sql, "id");
        sql = "Select StateName from StateMaster where Countryid=" + CCode;

        oo.FillDropDown(sql, drpStatePanel, "StateName");
    }
    protected void drpStatePanel_SelectedIndexChanged(object sender, EventArgs e)
    {
        string CC = "";
        sql = "select Id from StateMaster where StateName='" + drpStatePanel.SelectedItem.ToString() + "'";
        CC = oo.ReturnTag(sql, "id");
        sql = "Select CityName from CityMaster where StateId=" + CC;
        oo.FillDropDown(sql, drpCityPanel, "CityName");
    }
    protected void drpYYPanel_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(drpYYPanel, drpMMPanel, drpDDPanel);
    }
    protected void drpMMPanel_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(drpYYPanel, drpMMPanel, drpDDPanel);
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        if (txtSubjectPanel.Enabled == true)
        {
            String fromDate = "";

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "GeneralEnquiryUpdateProc";
            cmd.CommandType = CommandType.StoredProcedure;

            fromDate = drpYYPanel.SelectedItem.ToString() + "/" + drpMMPanel.SelectedItem.ToString() + "/" + drpDDPanel.SelectedItem.ToString();

            cmd.Parameters.AddWithValue("@id", lblID.Text);
            cmd.Parameters.AddWithValue("@Date", fromDate);
            cmd.Parameters.AddWithValue("@Subject", txtSubjectPanel.Text.ToString());
            cmd.Parameters.AddWithValue("@Name", txtName.Text.ToString());
            cmd.Parameters.AddWithValue("@ContactNo", txtContactNoPanel.Text.ToString());
            cmd.Parameters.AddWithValue("@MobileNo", txtMobilePanel.Text.ToString());
            cmd.Parameters.AddWithValue("@EMail", txtEmailPanel.Text.ToString());
            cmd.Parameters.AddWithValue("@Address", txtAddPanel.Text.ToString());
            cmd.Parameters.AddWithValue("@OrgName", txtOrg1.Text.ToString());

            string DD2 = "";
            sql = "Select Id from CountryMaster where CountryName='" + drpCountryPanel.SelectedItem.ToString() + "'";
            DD2 = oo.ReturnTag(sql, "Id");
            cmd.Parameters.AddWithValue("@CountryId", DD2);
            string DD1 = "";
            sql = "Select Id from StateMaster where StateName='" + drpStatePanel.SelectedItem.ToString() + "'";
            DD1 = oo.ReturnTag(sql, "Id");
            cmd.Parameters.AddWithValue("@StateId", DD1);
            string SD = "";
            sql = "Select Id from CityMaster where CityName='" + drpCityPanel.SelectedItem.ToString() + "'";
            SD = oo.ReturnTag(sql, "Id");
            cmd.Parameters.AddWithValue("@CityId", SD);
            cmd.Parameters.AddWithValue("@Remark", txtRemarkPanel.Text.ToString());
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());//Session["Login"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);

            cmd.Connection = con;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
          
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "S"); 
                sql = "select ROW_NUMBER() OVER (ORDER BY ad.Id desc) AS SrNo,ad.Id as Id,ad.EnquiryNo,convert(nvarchar,Ad.Date,106) as Date,Cm.CountryName,CS.CityName,Sm.StateName,";
                sql = sql + " Ad.Subject ,Ad.Name ,Ad.ContactNo ,Ad.MobileNo ,Ad.EMail ,";
                sql = sql + " Ad.Address  from GeneralEnquiry AD";
                sql = sql + " left join CountryMaster CM on AD.CountryId=CM.Id";
                sql = sql + " left join StateMaster SM on AD.StateId=SM.Id";
                sql = sql + " left join CityMaster CS on AD.CityId=CS.Id ";
                sql = sql + " where ad.SessionName='" + Session["SessionName"].ToString() + "' and ad.BranchCode=" + Session["BranchCode"].ToString() + "";
                sql = sql + " order by ad.Id desc";
                Grd.DataSource = oo.GridFill(sql);
                Grd.DataBind();
            }
            catch (SqlException) { }
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, unable to update!", "W"); 
        }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
     
            sql = "Delete from GeneralEnquiry where Id=" + lblvalue.Text;
            sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                //oo.MessageBox("Deleted successfully.", this.Page);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Deleted successfully.", "S"); 
                sql = "select ROW_NUMBER() OVER (ORDER BY ad.Id desc) AS SrNo,ad.Id as Id,ad.EnquiryNo,convert(nvarchar,Ad.Date,106) as Date,Cm.CountryName,CS.CityName,Sm.StateName, Ad.Subject ,Ad.Name ,Ad.ContactNo ,Ad.MobileNo ,Ad.EMail ,";
                sql = sql + "  Ad.Address  from GeneralEnquiry AD";
                sql = sql + " left join CountryMaster CM on AD.CountryId=CM.Id";
                sql = sql + " left join StateMaster SM on AD.StateId=SM.Id";
                sql = sql + " left join CityMaster CS on AD.CityId=CS.Id ";
                sql = sql + " where ad.SessionName='" + Session["SessionName"].ToString() + "' and ad.BranchCode=" + Session["BranchCode"].ToString() + "";
                sql = sql + " order by ad.Id desc";

                Grd.DataSource = oo.GridFill(sql);
                Grd.DataBind();
            }
            catch (SqlException) { }
           
        //}
        //else
        //{
        //    oo.MessageBox("Sorry, unable to delete!", this.Page);
        //}


        
    }
    protected void Button8_Click(object sender, EventArgs e)
    {

    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label36");
        string ss = lblId.Text;
        lblID.Text = ss;
        
        sql = "select ROW_NUMBER() OVER (ORDER BY ad.Id desc) AS SrNo,ad.Id as Id,Cm.CountryName as CountryName ,CS.CityName as CityName, Sm.StateName as StateName, Ad.Subject as Subject,Ad.Name as Name ,Ad.OrgName as Organization, Ad.ContactNo  as ContactNo,Ad.MobileNo as MobileNo,Ad.EMail  as EMail,ad.Address as Address,ad.remark as Remark,";
        sql = sql + " left(convert(nvarchar,Date,106),2) as DD,Right(left(convert(nvarchar,Date,106),6),3) as MM , RIGHT(convert(nvarchar,Date,106),4) as YY,";
        sql = sql + "  Ad.Address,ad.EnquiryNo as EnquiryNo  from GeneralEnquiry AD ";
        sql = sql + " left join CountryMaster CM on AD.CountryId=CM.Id ";
        sql = sql + " left join StateMaster SM on AD.StateId=SM.Id ";
        sql = sql + " left join CityMaster CS on AD.CityId=CS.Id  ";
        sql = sql + " where ad.Id=" + ss;
        sql = sql + " and ad.SessionName='" + Session["SessionName"].ToString() + "' and ad.BranchCode=" + Session["BranchCode"].ToString() + "";

        drpYYPanel.Text = oo.ReturnTag(sql, "YY");
        drpMMPanel.Text = oo.ReturnTag(sql, "MM");
        string t = "";
        if (oo.ReturnTag(sql, "DD").Substring(0, 1) == "0")
        {
            t = oo.ReturnTag(sql, "DD").Substring(1, 1);
        }
        else
        {
            t = oo.ReturnTag(sql, "DD");
        }

        drpDDPanel.Text = t;

        txtSubjectPanel.Text = oo.ReturnTag(sql, "Subject");
        txtName.Text = oo.ReturnTag(sql,"Name");
        txtContactNoPanel.Text = oo.ReturnTag(sql, "ContactNo");
        txtEmailPanel.Text = oo.ReturnTag(sql, "EMail");
        txtAddPanel.Text = oo.ReturnTag(sql, "Address");
        drpCountryPanel.SelectedValue = drpCountryPanel.Items.FindByText(oo.ReturnTag(sql, "CountryName")).Value;
        drpStatePanel.SelectedValue = drpStatePanel.Items.FindByText(oo.ReturnTag(sql, "StateName")).Value;
        try
        {
            drpCityPanel.SelectedValue = drpCityPanel.Items.FindByText(oo.ReturnTag(sql, "CityName")).Value;
        }
        catch
        {
            drpCityPanel.SelectedIndex = 0;
        }
        txtMobilePanel.Text = oo.ReturnTag(sql, "MobileNo");
        txtRemarkPanel.Text = oo.ReturnTag(sql, "Remark");
        txtOrg1.Text = oo.ReturnTag(sql, "Organization");
        Label9.Text = oo.ReturnTag(sql, "EnquiryNo");
        if ((t == System.DateTime.Today.ToString("dd") || "0"+t == System.DateTime.Today.ToString("dd")) && oo.ReturnTag(sql, "MM") == 
            System.DateTime.Today.ToString("MMM") && oo.ReturnTag(sql, "YY") == System.DateTime.Today.ToString("yyyy"))
        {
            txtSubjectPanel.Enabled = true;
            txtName.Enabled = true;
            txtContactNoPanel.Enabled = true;
            txtEmailPanel.Enabled = true;
            txtAddPanel.Enabled = true;
            drpCountryPanel.Enabled = true;
            drpStatePanel.Enabled = true;
            drpCityPanel.Enabled = true;
            txtMobilePanel.Enabled = true;
            txtRemarkPanel.Enabled = true;
            txtOrg1.Enabled = true;
            Label9.Enabled = true;
            drpYYPanel.Enabled = true;
            drpMMPanel.Enabled = true;
            drpDDPanel.Enabled = true;

          
        }
        else
        {
            txtSubjectPanel.Enabled = false;
            txtName.Enabled = false;
            txtContactNoPanel.Enabled = false;
            txtEmailPanel.Enabled = false;
            txtAddPanel.Enabled = false;
            drpCountryPanel.Enabled = false;
            drpStatePanel.Enabled = false;
            drpCityPanel.Enabled = false;
            txtMobilePanel.Enabled = false;
            txtRemarkPanel.Enabled = false;
            txtOrg1.Enabled = false;
            Label9.Enabled = false;
            drpYYPanel.Enabled = false;
            drpMMPanel.Enabled = false;
            drpDDPanel.Enabled = false;
        }

        Panel1_ModalPopupExtender.Show();
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        string ss = "";
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label37");
        Button8.Focus();
        ss = lblId.Text;
        sql = "select ROW_NUMBER() OVER (ORDER BY ad.Id desc) AS SrNo,ad.Id as Id,Cm.CountryName as CountryName ,CS.CityName as CityName, Sm.StateName as StateName, Ad.Subject as Subject,Ad.Name as Name ,Ad.OrgName as Organization, Ad.ContactNo  as ContactNo,Ad.MobileNo as MobileNo,Ad.EMail  as EMail,ad.Address as Address,ad.remark as Remark,";
        sql = sql + " left(convert(nvarchar,Date,106),2) as DD,Right(left(convert(nvarchar,Date,106),6),3) as MM , RIGHT(convert(nvarchar,Date,106),4) as YY,";
        sql = sql + "  Ad.Address,ad.EnquiryNo as EnquiryNo  from GeneralEnquiry AD ";
        sql = sql + " left join CountryMaster CM on AD.CountryId=CM.Id ";
        sql = sql + " left join StateMaster SM on AD.StateId=SM.Id ";
        sql = sql + " left join CityMaster CS on AD.CityId=CS.Id  ";
        sql = sql + " where ad.Id=" + ss;
        sql = sql + " and ad.SessionName='" + Session["SessionName"].ToString() + "' and ad.BranchCode=" + Session["BranchCode"].ToString() + "";

        string t = "";
        if (oo.ReturnTag(sql, "DD").Substring(0, 1) == "0")
        {
            t = oo.ReturnTag(sql, "DD").Substring(1, 1);
        }
        else
        {
            t = oo.ReturnTag(sql, "DD");
        }


        if (t == System.DateTime.Today.ToString("dd") && oo.ReturnTag(sql, "MM") == System.DateTime.Today.ToString("MMM") && oo.ReturnTag(sql, "YY") == System.DateTime.Today.ToString("yyyy"))
        {
            lblvalue.Text = ss.ToString();
            Panel2_ModalPopupExtender.Show();
        }
        else
        {
            //oo.MessageBox("Sorry, unable to delete!", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, unable to delete (Note:you can update/delete only today(s) Enquiry No.)!", "A"); 
        }
      
    }

    public void PermissionGrant(int add1, int delete1, int update1, LinkButton Ladd, Button Ldelete, Button LUpdate)
    {


        if (add1 == 1)
        {
            Ladd.Enabled = true;
        }
        else
        {
            Ladd.Enabled = false;
        }


        if (delete1 == 1)
        {
            Ldelete.Enabled = true;
        }
        else
        {
            Ldelete.Enabled = false;
        }

        if (update1 == 1)
        {
            LUpdate.Enabled = true;
        }
        else
        {
            LUpdate.Enabled = false;
        }


    }
    public void CheckValueADDDeleteUpdate()
    {
        sql = " select LoginId,LoginName,Pass,SessionId,BranchId,LT.LoginTypeName,ltb.add1 as add1,ltb.delete1 as delete1,ltb.update1 as update1 from LoginTab LTb";
        sql = sql + " left join LoginType Lt on LTb.LoginTypeId=Lt.LoginTypeId where LT.LoginTypeName='" + Session["Logintype"] + "' and LTb.LoginName='" + Session["LoginName"] + "'";
        int a, u, d;
        a = Convert.ToInt32(oo.ReturnTag(sql, "add1"));
        u = Convert.ToInt32(oo.ReturnTag(sql, "update1"));
        d = Convert.ToInt32(oo.ReturnTag(sql, "delete1"));

        PermissionGrant(a, d, u, (LinkButton)LinkButton1, btnDelete, Button3);
    }
}