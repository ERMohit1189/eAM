using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_PlannerEntry : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        con = oo.dbGet_connection();

        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {

            try
            {
                CheckValueADDDeleteUpdate();
            }
            catch (Exception) { }

            oo.AddDateMonthYearDropDown(DrpYear, DrpMonth, DrpDate);
            FindCurrentDateandSetinDropDown();
            oo.AddDateMonthYearDropDown(DrpYear1, DrpMonth1, DrpDate1);
            FindCurrentDateandSetinDropDown1();

            oo.AddDateMonthYearDropDown(drpYYPanelFrom, drpMMPanelFrom, drpDDPanelFrom);

            oo.AddDateMonthYearDropDown(drpYYTo, DrpMMToPanel, DrpDDToPanel);

            //BLL.BLLInstance.loadCourse(drpToClass, Session["SessionName"].ToString());
            //drpToClass.Items.Insert(1, new ListItem("All", "0"));
            //BLL.BLLInstance.loadClassUseingCourse(drpClass,drpToClass.SelectedValue.ToString(), Session["SessionName"].ToString());
            loadBranch();
            loadClass();
            loadPlannerType();
            loadFromHH();
            loadToHH();
            loadFromMM();
            loadToMM();
            loadGrid();
            txtPlannerName.Focus();
        }
    }

    public void loadFromHH()
    {
        for (int i = 1; i < 13; i++)
        {
            ListItem li = new ListItem();
            if (i.ToString().Length > 1)
            {
                li.Text = i.ToString();
                li.Value = i.ToString();
                drpFromHH.Items.Add(li);
            }
            else
            {
                li.Text = "0" + i.ToString();
                li.Value = "0" + i.ToString();
                drpFromHH.Items.Add(li);
            }

        }

        drpFromHH.SelectedValue = "12".ToString();
    }

    public void loadToHH()
    {
        for (int i = 1; i < 13; i++)
        {
            ListItem li = new ListItem();
            if (i.ToString().Length > 1)
            {
                li.Text = i.ToString();
                li.Value = i.ToString();
                drpToHH.Items.Add(li);
            }
            else
            {
                li.Text = "0" + i.ToString();
                li.Value = "0" + i.ToString();
                drpToHH.Items.Add(li);
            }

        }
        drpToHH.SelectedValue = "11".ToString();
    }

    public void loadFromMM()
    {
        for (int i = 0; i < 60; i++)
        {
            ListItem li = new ListItem();
            if (i.ToString().Length > 1)
            {
                li.Text = i.ToString();
                li.Value = i.ToString();
                drpFromMM.Items.Add(li);
            }
            else
            {
                li.Text = "0" + i.ToString();
                li.Value = "0" + i.ToString();
                drpFromMM.Items.Add(li);
            }

        }
        drpFromMM.SelectedValue = "00";
    }

    public void loadToMM()
    {
        for (int i = 0; i < 60; i++)
        {
            ListItem li = new ListItem();
            if (i.ToString().Length > 1)
            {
                li.Text = i.ToString();
                li.Value = i.ToString();
                drpToMM.Items.Add(li);
            }
            else
            {
                li.Text = "0" + i.ToString();
                li.Value = "0" + i.ToString();
                drpToMM.Items.Add(li);
            }

        }
        drpToMM.SelectedValue = "59";
    }

    public void loadPlannerType()
    {
        List<SqlParameter> param = new List<SqlParameter>();

        param.Add(new SqlParameter("@Action", "S"));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"]));

        drpPlannerType.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("PlannerTypeProc", param);
        drpPlannerType.DataTextField = "PlannerType";
        drpPlannerType.DataValueField = "Id";
        drpPlannerType.DataBind();

    }

    public void loadBranch()
    {
         BLL.BLLInstance.loadAllBranch(ddlBranch);
    }

    public void loadClass()
    {
        BLL.BLLInstance.loadClass(CheckBoxList2, Session["SessionName"].ToString());
        ListItem item = CheckBoxList2.Items.FindByValue("-1");
        if (item != null)
        {
            CheckBoxList2.Items.Remove(item);
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {

        string msg = "";
        string checkboxvalue = ddlBranch.SelectedValue.ToString();

        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@PlannerFor", DropDownList1.SelectedValue.ToString().Trim()));
        param.Add(new SqlParameter("@PlannerType", drpPlannerType.SelectedValue.ToString()));
        param.Add(new SqlParameter("@PlannerName", txtPlannerName.Text.Trim()));
        string classid = "";
        if (DropDownList1.SelectedValue != "Staff")
        {
            foreach (ListItem item in CheckBoxList2.Items)
            {
                if (item.Selected)
                {
                    if (item.Value.ToString()!="0")
                    {
                        classid = classid + "," + item.Value.ToString();
                    }
                   
                }
            }
        }
        param.Add(new SqlParameter("@Classids", classid.ToString()));
        if (DropDownList1.SelectedValue.ToString().Trim() == "Student")
        {
            param.Add(new SqlParameter("@StCountAtt", "1"));
        }
        if (DropDownList1.SelectedValue.ToString().Trim() == "Staff")
        {
            param.Add(new SqlParameter("@StCountAtt", "0"));
            param.Add(new SqlParameter("@StaffCountAtt", "1"));
        }
        if (DropDownList1.SelectedValue.ToString().Trim() == "Both")
        {
            param.Add(new SqlParameter("@StCountAtt", "1"));
            param.Add(new SqlParameter("@StaffCountAtt", "1"));
        }
        String FromDate = DrpYear.SelectedItem.ToString() + "/" + DrpMonth.SelectedItem.ToString() + "/" + DrpDate.SelectedItem.ToString();
        string fromtime = drpFromHH.Text.Trim() + ":" + drpFromMM.Text.Trim() + " " + drpFromTimeStamp.SelectedItem.Text;
        param.Add(new SqlParameter("@FromDate", FromDate + " " + fromtime));
        String ToDate = DrpYear1.SelectedItem.ToString() + "/" + DrpMonth1.SelectedItem.ToString() + "/" + DrpDate1.SelectedItem.ToString();
        string totime = drpToHH.Text.Trim() + ":" + drpToMM.Text.Trim() + " " + drpToTimeStamp.SelectedItem.Text;
        param.Add(new SqlParameter("@ToDate", ToDate + " " + totime));
       // param.Add(new SqlParameter("@Description", txtDescription.Text.Trim()));
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
        param.Add(new SqlParameter("@LoginName", Session["LoginName"].ToString()));
        param.Add(new SqlParameter("@Applyallchanges", checkboxvalue.ToString()));
       // param.Add(new SqlParameter("@BranchCOdeNew", ddlBranch.SelectedValue));
        SqlParameter para = new SqlParameter("@Msg", "");
        para.Direction = ParameterDirection.Output;
        para.Size = 0x100;

        param.Add(para);

        msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("PlannerMasterProcNew", param);

        try
        {

            if (msg.Trim() == "S")
            {
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");
                txtPlannerName.Text = "";
                UncheckAllCheckboxes();
               // txtDescription.Text = "";
                txtPlannerName.Focus();
               // chkbranchs.Checked = true;

                loadGrid();
            }
            else if (msg == "DU")
            {
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, this record already exists!", "A");
            }
            else
            {
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Record, Not submitted!", "W");
            }

        }
        catch (Exception) { }

        //}
    }
    protected void UncheckAllCheckboxes()
    {
        foreach (ListItem item in CheckBoxList2.Items)
        {
            item.Selected = false;
        }
    }
    public void loadGrid()
    {
        List<SqlParameter> param = new List<SqlParameter>();

        param.Add(new SqlParameter("@QueryFor", "S"));
       // param.Add(new SqlParameter("@PlannerFor", rbPlannerFor.SelectedItem.Text.Trim()));
        //param.Add(new SqlParameter("@PlannerCategory", rbPlannerCategory.SelectedItem.Text.Trim()));
        param.Add(new SqlParameter("@PlannerType", drpPlannerType.SelectedValue.ToString()));
        param.Add(new SqlParameter("@SessionName", Session["SessionName"]));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"]));

        param.Add(new SqlParameter("@FromDate", DBNull.Value));
        param.Add(new SqlParameter("@ToDate", DBNull.Value));

        Grd.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("PlannerMasterProcNew", param);
        Grd.DataBind();
    }

    protected void DrpYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(DrpYear, DrpMonth, DrpDate);
    }
    protected void DrpMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(DrpYear, DrpMonth, DrpDate);
    }
    protected void DrpYear1_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(DrpYear1, DrpMonth1, DrpDate1);
    }
    protected void DrpMonth1_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(DrpYear1, DrpMonth1, DrpDate1);
    }
    public void FindCurrentDateandSetinDropDown()
    {
        string dd = "", mm = "", yy = "";


        dd = oo.ReturnTag("Select day(getdate()) as DateDD", "DateDD");
        mm = oo.ReturnTag("Select Month(getdate())as MonthMM", "MonthMM");
        yy = oo.ReturnTag("Select Year(getdate()) as YearYY ", "YearYY");

        DrpYear.Text = yy;
        if (mm == "1")
        {
            DrpMonth.Text = "Jan";
        }
        else if (mm == "2")
        {
            DrpMonth.Text = "Feb";
        }
        else if (mm == "3")
        {
            DrpMonth.Text = "Mar";
        }
        else if (mm == "4")
        {
            DrpMonth.Text = "Apr";
        }
        else if (mm == "5")
        {
            DrpMonth.Text = "May";
        }
        else if (mm == "6")
        {
            DrpMonth.Text = "Jun";

        }
        else if (mm == "7")
        {
            DrpMonth.Text = "Jul";
        }
        else if (mm == "8")
        {
            DrpMonth.Text = "Aug";
        }
        else if (mm == "9")
        {
            DrpMonth.Text = "Sep";
        }
        else if (mm == "10")
        {
            DrpMonth.Text = "Oct";
        }
        else if (mm == "11")
        {
            DrpMonth.Text = "Nov";
        }
        else if (mm == "12")
        {
            DrpMonth.Text = "Dec";
        }


        DrpDate.Text = dd;
    }
    public void FindCurrentDateandSetinDropDown1()
    {
        string dd = "", mm = "", yy = "";


        dd = oo.ReturnTag("Select day(getdate()) as DateDD", "DateDD");
        mm = oo.ReturnTag("Select Month(getdate())as MonthMM", "MonthMM");
        yy = oo.ReturnTag("Select Year(getdate()) as YearYY ", "YearYY");

        DrpYear1.Text = yy;
        if (mm == "1")
        {
            DrpMonth1.Text = "Jan";
        }
        else if (mm == "2")
        {
            DrpMonth1.Text = "Feb";
        }
        else if (mm == "3")
        {
            DrpMonth1.Text = "Mar";
        }
        else if (mm == "4")
        {
            DrpMonth1.Text = "Apr";
        }
        else if (mm == "5")
        {
            DrpMonth1.Text = "May";
        }
        else if (mm == "6")
        {
            DrpMonth1.Text = "Jun";

        }
        else if (mm == "7")
        {
            DrpMonth1.Text = "Jul";
        }
        else if (mm == "8")
        {
            DrpMonth1.Text = "Aug";
        }
        else if (mm == "9")
        {
            DrpMonth1.Text = "Sep";
        }
        else if (mm == "10")
        {
            DrpMonth1.Text = "Oct";
        }
        else if (mm == "11")
        {
            DrpMonth1.Text = "Nov";
        }
        else if (mm == "12")
        {
            DrpMonth1.Text = "Dec";
        }


        DrpDate1.Text = dd;
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        string msg = "";

        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@QueryFor", "U"));
      //  param.Add(new SqlParameter("@PlannerFor", rbPlannerFor.SelectedItem.Text.Trim()));
        //param.Add(new SqlParameter("@PlannerCategory", rbPlannerCategory.SelectedItem.Text.Trim()));
        param.Add(new SqlParameter("@PlannerType", drpPlannerType.SelectedValue.ToString()));
        param.Add(new SqlParameter("@PlannerName", txtPlannerPanel.Text.Trim()));
     //   param.Add(new SqlParameter("@CountAtt", drpStudentCount.SelectedValue.ToString()));
        param.Add(new SqlParameter("@SessionName", Session["SessionName"]));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"]));
        String Date = drpYYPanelFrom.SelectedItem.ToString() + "/" + drpMMPanelFrom.SelectedItem.ToString() + "/" + drpDDPanelFrom.SelectedItem.ToString();
        param.Add(new SqlParameter("@FromDate", Date));
        String Date1 = drpYYTo.SelectedItem.ToString() + "/" + DrpMMToPanel.SelectedItem.ToString() + "/" + DrpDDToPanel.SelectedItem.ToString();
        param.Add(new SqlParameter("@ToDate", Date1));
        param.Add(new SqlParameter("@Remark", txtRemarkPanel.Text.Trim()));
        param.Add(new SqlParameter("@Id", lblID.Text.Trim()));

        SqlParameter para = new SqlParameter("@Msg", "");
        para.Direction = ParameterDirection.Output;
        para.Size = 0x100;

        param.Add(para);

        msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("PlannerMasterProc", param);

        try
        {

            if (msg.Trim() == "S")
            {
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "S");

                oo.ClearControls(this.Page);

                txtPlannerName.Focus();

                FindCurrentDateandSetinDropDown();
                FindCurrentDateandSetinDropDown1();

                loadGrid();
            }
            else if (msg == "DU")
            {
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, this record already exists!", "A");
            }
            else
            {
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Record, Not updated!", "W");
            }

        }
        catch (Exception) { }

    }
    protected void Button4_Click(object sender, EventArgs e)
    {

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string msg = "";

        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@QueryFor", "D"));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"]));
        param.Add(new SqlParameter("@Id", lblvalue.Text.Trim()));

        SqlParameter para = new SqlParameter("@Msg", "");
        para.Direction = ParameterDirection.Output;
        para.Size = 0x100;

        param.Add(para);

        msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("PlannerMasterProcNew", param);

        try
        {

            if (msg.Trim() == "S")
            {
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Deleted successfully.", "S");

                oo.ClearControls(this.Page);

                txtPlannerName.Focus();

                FindCurrentDateandSetinDropDown();
                FindCurrentDateandSetinDropDown1();

                loadGrid();
            }
            else
            {
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Record, Not deleted!", "W");
            }

        }
        catch (Exception) { }

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

        sql = "Select ROW_NUMBER() OVER (ORDER BY Id ASC) AS SrNo, Id,PlannerName,convert(nvarchar,FromDate,106) as FromDate,convert(nvarchar,ToDate,106) as ToDate,  ";
        sql = sql + " left(convert(nvarchar,FromDate,106),2) as DD,Right(left(convert(nvarchar,FromDate,106),6),3) as MM , RIGHT(convert(nvarchar,FromDate,106),4) as YY,";
        sql = sql + " left(convert(nvarchar,ToDate,106),2) as DDT,Right(left(convert(nvarchar,ToDate,106),6),3) as MMT , RIGHT(convert(nvarchar,ToDate,106),4) as YYT,Remark ";
        sql = sql + " from PlannerMaster where Id=" + ss;
        sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

        txtPlannerPanel.Text = oo.ReturnTag(sql, "PlannerName");
        txtRemarkPanel.Text = oo.ReturnTag(sql, "Remark");



        drpYYPanelFrom.Text = oo.ReturnTag(sql, "YY");
        drpMMPanelFrom.Text = oo.ReturnTag(sql, "MM");
        string t = "";
        if (oo.ReturnTag(sql, "DD").Substring(0, 1) == "0")
        {
            t = oo.ReturnTag(sql, "DD").Substring(1, 1);
        }
        else
        {
            t = oo.ReturnTag(sql, "DD");
        }

        drpDDPanelFrom.Text = t;

        drpYYTo.Text = oo.ReturnTag(sql, "YYT");
        DrpMMToPanel.Text = oo.ReturnTag(sql, "MMT");

        if (oo.ReturnTag(sql, "DDT").Substring(0, 1) == "0")
        {
            t = oo.ReturnTag(sql, "DDT").Substring(1, 1);
        }
        else
        {
            t = oo.ReturnTag(sql, "DDT");
        }

        DrpDDToPanel.Text = t;

        Panel1_ModalPopupExtender.Show();
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        Button8.Focus();
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label37");
        string ss = lblId.Text;
        lblvalue.Text = ss.ToString();
        Panel2_ModalPopupExtender.Show();
    }
    protected void drpYYPanelFrom_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(drpYYPanelFrom, drpMMPanelFrom, drpDDPanelFrom);
    }
    protected void drpMMPanelFrom_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(drpYYPanelFrom, drpMMPanelFrom, drpDDPanelFrom);
    }
    protected void drpDDPanelFrom_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void drpYYTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(drpYYTo, DrpMMToPanel, DrpDDToPanel);
    }
    protected void DrpMMToPanel_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(drpYYTo, DrpMMToPanel, DrpDDToPanel);
    }
    protected void DrpDate1_SelectedIndexChanged(object sender, EventArgs e)
    {

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

        PermissionGrant(a, d, u, (LinkButton)lnkSubmit, btnDelete, Button3);
    }

    protected void drpPlannerType_SelectedIndexChanged(object sender, EventArgs e)
    {
      //  loadGrid();
    }


    protected void LinkButton3_Click1(object sender, EventArgs e)
    {
        //Button8.Focus();
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label37");
        string ss = lblId.Text;
        lblvalue.Text = ss.ToString();
        Panel2_ModalPopupExtender.Show();
    }
}