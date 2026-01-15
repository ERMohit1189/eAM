using System;
using System.Web.UI;

using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
public partial class BulkUpdate : Page
{
    readonly Campus _oo = new Campus();
    SqlConnection con = new SqlConnection();
    string sql = ""; string _sql = "";
    protected void Page_PreInIt(object sender, EventArgs e)
    {
        if (Session["Logintype"] == null) { Response.Redirect("~/default.aspx"); }
        if (Session["Logintype"].ToString() == "Admin")
        {
            this.MasterPageFile = "~/Master/admin_root-manager.master";
        }
        else if (Session["Logintype"].ToString() != "Staff")
        {
            this.MasterPageFile = "~/Staff/staff_root-manager.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        con = _oo.dbGet_connection();
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            loadclass();
            loadsection();
            LoadBloodGroup(drpBloodGroupALL);
            LoadDefaultHouse(drpHouseALL);
            BAL.objBal.fillSelectvalue(drpBranch, "<--Select-->", "-1");
        }
    }
    public void loadclass()
    {
        BLL.BLLInstance.loadClass(drpclass, Session["SessionName"].ToString());
    }
    public void loadsection()
    {
        sql = "Select SectionName,Id from SectionMaster where ClassNameId='" + drpclass.SelectedValue.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        BAL.objBal.FillDropDown_withValue(sql, drpsection, "SectionName", "Id");
        drpsection.Items.Insert(0, "<--Select-->");
    }
    private void LoadBloodGroup(DropDownList drpBloodGroup)
    {
        string _sql = "Select BloodGroupName,BloodGroupId from BloodGroupMaster";
        BAL.objBal.FillDropDown_withValue(_sql, drpBloodGroup, "BloodGroupName", "BloodGroupId");
        drpBloodGroup.Items.Insert(0, new ListItem("<--Select-->", ""));
        using (var objBll = new BLL())
        {
            try
            {
                objBll.loadDefaultvalue("Blood Group", drpBloodGroup);
            }
            catch
            {
                // ignored
            }
        }
    }
    private void LoadDefaultHouse(DropDownList drpHouse)
    {
        _sql = "select HouseName from HouseMaster where BranchCode=" + Session["BranchCode"] + "";
        BAL.objBal.FillDropDown_withValue(_sql, drpHouse, "HouseName", "HouseName");
        drpHouse.Items.Insert(0, new ListItem("<--Select-->", ""));
        using (var objBll = new BLL())
        {
            try
            {
                objBll.loadDefaultvalue("HouseName", drpHouse);
            }
            catch
            {
                // ignored
            }
        }
    }
    protected void drpclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadsection();
    }
    protected void drpsection_SelectedIndexChanged(object sender, EventArgs e)
    {
        BLL.BLLInstance.loadBranch(drpBranch, Session["SessionName"].ToString(), drpclass.SelectedValue.ToString());
    }
    public void loadStudents()
    {
        string BranchId = "";
        if (drpBranch.SelectedIndex != 0)
        {
            BranchId = drpBranch.SelectedValue;
        }
        sql = "select name,srno,CombineClassName,Pen udisePen,BloodGroup booldGroup,MobileNumber studentMobile,FatherEmail studentEmail,APAARID apaarID,HouseName houseName from AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "', " + Session["BranchCode"].ToString() + ") where ISNULL(Withdrwal,'A')=IIF('" + drpStatus.SelectedValue+ "'='All',ISNULL(Withdrwal,'A'),'" + drpStatus.SelectedValue+"')";
        sql +=  " and ClassId=" + drpclass.SelectedValue.ToString() + " and SectionID='" + drpsection.SelectedValue + "' and BranchId=case when '" + BranchId + "'='' then BranchId else '" + BranchId + "' end and SessionName='" + Session["SessionName"].ToString() + "' order by name";
        rptStudents.DataSource = BAL.objBal.GridFill(sql);
        rptStudents.DataBind();
        for (int i = 0; i < rptStudents.Items.Count; i++)
        {
            HiddenField booldGroup = (HiddenField)rptStudents.Items[i].FindControl("hdBloodGroup");
            DropDownList drpBloodGroup = (DropDownList)rptStudents.Items[i].FindControl("drpBloodGroup");
            drpBloodGroup.SelectedValue = booldGroup.Value.Trim();

            HiddenField house = (HiddenField)rptStudents.Items[i].FindControl("hdHouse");
            DropDownList drpHouse = (DropDownList)rptStudents.Items[i].FindControl("drpHouse");
            drpHouse.SelectedValue = house.Value.Trim();

        }
        if (rptStudents.Items.Count > 0)
        {
            divExport.Visible = true;
            LinkUpdate.Visible = true;
        }
        else
        {
            divExport.Visible = false;
            LinkUpdate.Visible = false;
        }
    }

    protected void LinkView_Click(object sender, EventArgs e)
    {
        loadStudents();
    }

    protected void LinkUpdate_Click(object sender, EventArgs e)
    {
        bool flag1 = false; bool flag2 = true; bool flag = false;
        if (rptStudents.Items.Count > 0)
        {

            for (int i = 0; i < rptStudents.Items.Count; i++)
            {
                CheckBox chk = (CheckBox)rptStudents.Items[i].FindControl("chk");
                if (chk.Checked)
                {
                    flag = true;
                }
            }
            if (flag)
                for (int i = 0; i < rptStudents.Items.Count; i++)
                {
                    CheckBox chk = (CheckBox)rptStudents.Items[i].FindControl("chk");
                    if (chk.Checked)
                    {
                        Label LblSrno = (Label)rptStudents.Items[i].FindControl("LblSrno");
                        TextBox txtUDISEPEN = (TextBox)rptStudents.Items[i].FindControl("txtUDISEPEN");
                        TextBox txtAPAARID = (TextBox)rptStudents.Items[i].FindControl("txtAPAARID");
                        DropDownList drpBloodGroup = (DropDownList)rptStudents.Items[i].FindControl("drpBloodGroup");
                        TextBox txtStudentMobile = (TextBox)rptStudents.Items[i].FindControl("txtStudentMobile");
                        TextBox txtStudentEmail = (TextBox)rptStudents.Items[i].FindControl("txtStudentEmail");
                        DropDownList drpHouse = (DropDownList)rptStudents.Items[i].FindControl("drpHouse");

                        SqlCommand cmd = new SqlCommand();

                        cmd.CommandText = "BulkUpdate";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@SrNo", LblSrno.Text.Trim());
                        cmd.Parameters.AddWithValue("@UDISEPEN", txtUDISEPEN.Text);
                        cmd.Parameters.AddWithValue("@APAARID", txtAPAARID.Text);
                        cmd.Parameters.AddWithValue("@BloodGroup", drpBloodGroup.SelectedValue);
                        cmd.Parameters.AddWithValue("@StudentMobile", txtStudentMobile.Text);
                        cmd.Parameters.AddWithValue("@StudentEmail", txtStudentEmail.Text);
                        cmd.Parameters.AddWithValue("@House", drpHouse.Text);
                        cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                        cmd.Parameters.AddWithValue("@Action", "update");
                        cmd.Connection = con;
                        try
                        {
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                            flag1 = true;
                        }
                        catch (SqlException)
                        {
                            flag2 = true;
                        }
                    }
                }

        }

        if (flag)
        {

            if (flag1 == true && flag2 == true)
            {
                loadStudents();
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Updated successfully.", "S");
            }
            else
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Record not updated!", "W");

            }

        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Please Select at least one student!", "W");

        }
    }

    protected void rptStudents_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DropDownList drpBloodGroup = (e.Item.FindControl("drpBloodGroup") as DropDownList);
            LoadBloodGroup(drpBloodGroup);
            //HiddenField booldGroup = (e.Item.FindControl("hdBloodGroup") as HiddenField);
            //drpBloodGroup.SelectedValue = booldGroup.Value.Trim();

            DropDownList drpHouse = (e.Item.FindControl("drpHouse") as DropDownList);
            LoadDefaultHouse(drpHouse);
            //HiddenField hdHouse = (e.Item.FindControl("hdHouse") as HiddenField);
            //drpHouse.SelectedValue = hdHouse.Value.Trim();
        }
    }

    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        foreach(RepeaterItem li in rptStudents.Items)
        {
            CheckBox chk = (CheckBox)li.FindControl("chk");
            chk.Checked = chkAll.Checked;
        }
    }

    protected void drpBloodGroupALL_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (RepeaterItem li in rptStudents.Items)
        {
            DropDownList drpBloodGroup = (DropDownList)li.FindControl("drpBloodGroup");
            drpBloodGroup.SelectedValue = drpBloodGroupALL.SelectedValue;
        }
    }

    protected void drpHouseALL_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (RepeaterItem li in rptStudents.Items)
        {
            DropDownList drpHouse = (DropDownList)li.FindControl("drpHouse");
            drpHouse.SelectedValue = drpHouseALL.SelectedValue;
        }
    }

    protected void drpStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadStudents();
    }
}