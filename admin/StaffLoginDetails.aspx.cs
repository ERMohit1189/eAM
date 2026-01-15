using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;

public partial class SuperAdmin_StaffLoginDetails : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        con = oo.dbGet_connection();

        
            if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
            {
                Response.Redirect("default.aspx");
            }
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            loadEmpOfficialDetails();
            Display();
        
        }
        try
        {

            GridView1.FooterRow.Visible = false;
        }
        catch (Exception) { }


    }

    private void loadEmpOfficialDetails()
    {
        sql = "Select EmpDepName,EmpDepId from EmpDepMaster where BranchCode="+ Session["BranchCode"] + "";
        BAL.objBal.FillDropDown_withValue(sql, drpDepartment, "EmpDepName", "EmpDepId");
        drpDepartment.Items.Insert(0, new ListItem("<--Select-->", "-1"));

        BAL.objBal.FillDropDown_withValue(sql, ddlDepartment2, "EmpDepName", "EmpDepId");
        ddlDepartment2.Items.Insert(0, new ListItem("<--Select-->", ""));


        sql = "Select DesId, DesName from DesMaster where BranchCode=" + Session["BranchCode"].ToString() + "";
        BAL.objBal.FillDropDown_withValue(sql, drpDesignation, "DesName", "DesId");
        drpDesignation.Items.Insert(0, new ListItem("<--Select-->", "-1"));

        BAL.objBal.FillDropDown_withValue(sql, ddlDesignation2, "DesName", "DesId");
        ddlDesignation2.Items.Insert(0, new ListItem("<--Select-->", ""));
        
    }


    public void Display()
    {

        sql = " select Row_Number() over (order by Eo.SrNo Asc) as SNo,CASE WHEN ISNULL(lt.IsActive,1)=1 THEN 'Active' ELSE 'Inactive' End Status,";
        sql = sql + "  eo.srno, Eo.DepartmentName,eo.DesNameNew as Designation,EO.Ecode,";
        sql = sql + "  EO.EmpId,convert(nvarchar,EO.RegistrationDate,106) as RegistrationDate,  EG.EFirstName,EG.EMiddleName, ";
        sql = sql + "  EG.ELastName,EG.EFatherName,EG.EMobileNo,EG.EPreCityId as EPreCityId,EG.EEmail as EEmail,";
        sql = sql + "  EO.DepartmentName as DepartmentName ,EO.password as password ";
        sql = sql + "  from EmpployeeOfficialDetails EO ";
        sql = sql + "  left join EmpGeneralDetail EG on EO.Ecode=EG.Ecode and EG.BranchCode=" + Session["BranchCode"] + "";
        sql = sql + "  left JOIN dbo.LoginTab lt ON lt.LoginName=Eo.Ecode and lt.BranchId=" + Session["BranchCode"] + "";
        sql = sql + "  where eo.BranchCode=" + Session["BranchCode"].ToString() + " and Eo.Withdrwal is null";
        if(drpDepartment.SelectedIndex != 0)
        {
            sql = sql + "  and DepartmentName = '" + drpDepartment.SelectedItem.Text + "'";
        }
        if(drpDesignation.SelectedIndex != 0)
        {
            sql = sql + "  and DesNameNew = '" + drpDesignation.SelectedItem.Text + "'";
        }
        sql = sql + "  order by CASE WHEN ISNUMERIC(Right(EO.Ecode,3))=1 THEN Right(EO.Ecode,3) ELSE 0 END";

        GridView1.DataSource = oo.GridFill(sql);
        GridView1.DataBind();

        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            HtmlInputText txtPassword = (HtmlInputText)GridView1.Rows[i].FindControl("txtPassword");
            txtPassword.Attributes["type"] = "password";
        }

       
    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        sql = "Update EmpployeeOfficialDetails set Password='" + txtPassword0.Text.ToString().Trim() + "' where Ecode='" + lblID.Text.ToString() + "' and BranchCode=" + Session["BranchCode"] + "";
        oo.ProcedureDatabase(sql);
        bool status=true;
        if (RadioButtonList1.SelectedIndex == 0)
        {
            status = true;
        }
        else if (RadioButtonList1.SelectedIndex == 1)
        {
            status = false;
        }
        sql = "Select *from LoginTab where LoginName='" + lblID.Text.ToString() + "' and BranchId=" + Session["BranchCode"] + "";
        if (oo.Duplicate(sql))
        {
            sql = "Update LoginTab set Pass='" + txtPassword0.Text.ToString().Trim() + "',IsActive='" + status + "' where LoginName='" + lblID.Text.ToString() + "' and BranchId=" + Session["BranchCode"] + "";
            oo.ProcedureDatabase(sql);
        }
        else
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@LoginName", lblID.Text.ToString()));
            param.Add(new SqlParameter("@Pass", txtPassword0.Text.ToString().Trim()));
            param.Add(new SqlParameter("@LoginTypeId", "3"));
            param.Add(new SqlParameter("@IsActive", status));
            param.Add(new SqlParameter("@BranchId", Session["BranchCode"].ToString()));

            DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("set_Login", param);
        }
        Display();
        Campus camp = new Campus(); camp.msgbox(this.Page, divmsg, "Updated successfully.", "S");
        //oo.MessageBox("Updated successfully.", this.Page);



    }

    protected void LinkButton7_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label36");
        string ss = lblId.Text;
      
        sql = " select  Row_Number() over (order by Eo.SrNo Asc) as SNo, eo.srno, Eo.DepartmentName,eo.Designation as Designation,EO.Ecode,  ";
        sql = sql + " EO.EmpId,convert(nvarchar,EO.RegistrationDate,106) as RegistrationDate,  EG.EFirstName,EG.EMiddleName, ";
        sql = sql + " EG.ELastName,EG.EFatherName,EG.EMobileNo,EG.EPreCityId as EPreCityId,EG.EEmail as EEmail,EO.DepartmentName as DepartmentName ,EO.password as password ";
        sql = sql + " from EmpployeeOfficialDetails EO ";
        sql = sql + "  left join EmpGeneralDetail EG on EO.Ecode=EG.Ecode and EG.BranchCode=" + Session["BranchCode"] + "";
        sql = sql + "  where eo.BranchCode=" + Session["BranchCode"].ToString() + "";
        sql = sql + " and EO.Withdrwal is null and eo.srno=" + ss;

        txtUserId0.Text = oo.ReturnTag(sql, "Ecode");

        txtPassword0.Text = oo.ReturnTag(sql, "password");
        lblID.Text = oo.ReturnTag(sql, "Ecode");

        sql = "Select IsActive from Logintab where LoginName='" + txtUserId0.Text + "' and BranchId=" + Session["BranchCode"] + "";
        bool status;
        if (oo.ReturnTag(sql, "IsActive")=="" || Convert.ToBoolean(oo.ReturnTag(sql, "IsActive"))==true)
        {
            status = true;
        }
        else
        {
            status = false;
        }
        if (status == true)
        {
            RadioButtonList1.SelectedValue = "1";
        }
        else
        {
            RadioButtonList1.SelectedValue = "0";
        }


        Panel1_ModalPopupExtender.Show();

    }
    protected void LinkButton6_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        string ss = chk.Text;
        lblvalue.Text = ss.ToString();
        sql = " select  Row_Number() over (order by Eo.SrNo Asc) as SNo, eo.srno, Eo.DepartmentName,eo.Designation as Designation,EO.Ecode,  ";
        sql = sql + " EO.EmpId,convert(nvarchar,EO.RegistrationDate,106) as RegistrationDate,  EG.EFirstName,EG.EMiddleName, ";
        sql = sql + " EG.ELastName,EG.EFatherName,EG.EMobileNo,EG.EPreCityId as EPreCityId,EG.EEmail as EEmail,EO.DepartmentName as DepartmentName ,EO.password as password ";
        sql = sql + " from EmpployeeOfficialDetails EO ";
        sql = sql + "  left join EmpGeneralDetail EG on EO.Ecode=EG.Ecode and EG.BranchCode=" + Session["BranchCode"] + "";
        sql = sql + "  and eo.BranchCode=" + Session["BranchCode"].ToString() + "";
        sql = sql + " where EO.Withdrwal is null and eo.srno=" + ss;

        lblvalue.Text = oo.ReturnTag(sql, "Ecode");
        Panel2_ModalPopupExtender.Show();     
    }

    protected void lnkYes_Click(object sender, EventArgs e)
    { 
     
        sql = "Delete from EmpployeeOfficialDetails where Ecode='" + lblvalue.Text+ "' and BranchCode=" + Session["BranchCode"] + "";
        oo.ProcedureDatabase(sql);

        sql = "Delete from EmpGeneralDetail where Ecode='" + lblvalue.Text+ "' and BranchCode=" + Session["BranchCode"] + "";
        oo.ProcedureDatabase(sql);

        sql = "Delete from EmpEmployeeDetails where Ecode='" + lblvalue.Text+ "' and BranchCode=" + Session["BranchCode"] + "";
        oo.ProcedureDatabase(sql);

        sql = "Delete from EmpPreviousEmployment where Ecode='" + lblvalue.Text+ "' and BranchCode=" + Session["BranchCode"] + "";
        oo.ProcedureDatabase(sql);

        sql = "Delete from EmpDocuments where Ecode='" + lblvalue.Text + "' and BranchCode=" + Session["BranchCode"] + "";
        oo.ProcedureDatabase(sql);
     
        sql = "Delete from LoginTab where LoginName='" + lblvalue.Text + "' and LoginTypeId=3 and BranchId=" + Session["BranchCode"] + "";
        oo.ProcedureDatabase(sql);

        sql = "Delete from EmployeeAttendance where Ecode='" + lblvalue.Text + "' and BranchCode=" + Session["BranchCode"] + "";
        oo.ProcedureDatabase(sql);

        oo.MessageBox("Deleted successfully.", this.Page);
        Display();

    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        Display();
    }
    protected void lnkView_Click(object sender, EventArgs e)
    {
        Display();
    }

    protected void LinkButtonUpdate_Click(object sender, EventArgs e)
    {
        sql = "select Ecode, Designation from EmpployeeOfficialDetails where ltrim(rtrim(DepartmentName))='" + ddlDepartment2.SelectedItem.Text.Trim() + "' and ltrim(rtrim(Designation))='" + ddlDesignation2.SelectedItem.Text.Trim() + "' and BranchCode=" + Session["BranchCode"] + "";
        if (oo.Duplicate(sql))
        {
            sql = "Update LoginTab set IsActive='" + ddlStatus.SelectedValue + "' where LoginName in (select Ecode from EmpployeeOfficialDetails where ltrim(rtrim(DepartmentName))='" + ddlDepartment2.SelectedItem.Text.Trim() + "' and ltrim(rtrim(Designation))='" + ddlDesignation2.SelectedItem.Text.Trim() + "' and BranchCode=" + Session["BranchCode"] + ")  and BranchId=" + Session["BranchCode"] + "";
            oo.ProcedureDatabase(sql);
            Display();
            Campus camp = new Campus(); camp.msgbox(this.Page, div1, "Updated successfully.", "S");
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, div1, "Records not found!", "A");
        }
        
    }
}