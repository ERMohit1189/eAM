using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;

public partial class admin_ClassTeacherMaster : System.Web.UI.Page
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
            loadGrid();
            loadmedium();
            loadclass();
            BAL.objBal.fillSelectvalue(drpBranch, "<--Select-->");
            BAL.objBal.fillSelectvalue(drpSection, "<--Select-->");
        }
    }
    private void loadBranch()
    {
        sql = "Select BranchName,Id from BranchMaster Where ClassId='" + drpclass.SelectedValue.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        BAL.objBal.FillDropDown_withValue(sql, drpBranch, "BranchName", "Id");
        BAL.objBal.fillSelectvalue(drpBranch, "<--Select-->");
    }
    public void loadclass()
    {
        sql = "Select Id,ClassName,CidOrder from ClassMaster Where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " Order by CIDOrder";
        BAL.objBal.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");
        BAL.objBal.fillSelectvalue(drpclass, "<--Select-->");
    }

    public void loadsection()
    {
        sql = "Select SectionName,Id from SectionMaster";
        sql = sql + " where ClassNameId='" + drpclass.SelectedValue.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        BAL.objBal.FillDropDown_withValue(sql, drpSection, "SectionName","Id");
        BAL.objBal.fillSelectvalue(drpSection, "<--Select-->");
    }
    public void loadmedium()
    {
        sql = "Select Medium,Id from MediumMaster";
        sql = sql + " Where  BranchCode=" + Session["BranchCode"].ToString() + "";
        BAL.objBal.FillDropDown_withValue(sql, drpmedium, "Medium", "Id");
        BAL.objBal.fillSelectvalue(drpmedium, "<--Select-->");
    }

    protected void drpclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadsection();
        loadBranch();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string msg = "";
        if (Grd.Rows.Count > 0)
        {
            Label lblEmpId = (Label)Grd.Rows[0].FindControl("lblEmpId");
            Label lblEmpCode = (Label)Grd.Rows[0].FindControl("lblEcode");

            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@QueryFor", "I"));
            param.Add(new SqlParameter("@Empid", lblEmpId.Text.Trim()));
            param.Add(new SqlParameter("@EmpCode", lblEmpCode.Text.Trim()));
            param.Add(new SqlParameter("@MediumId", drpmedium.SelectedValue));
            param.Add(new SqlParameter("@ClassId", drpclass.SelectedValue));
            param.Add(new SqlParameter("@SectionId", drpSection.SelectedValue));
            param.Add(new SqlParameter("@BranchId", drpBranch.SelectedValue));
            param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
            param.Add(new SqlParameter("@LoginName", Session["LoginName"].ToString()));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
            SqlParameter para = new SqlParameter("@Msg", "");
            para.Direction = ParameterDirection.Output;
            para.Size = 0x100;
            param.Add(para);

            msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("ClassTeacherMasterProc", param);

            if (msg == "S")
            {
                loadGrid();
                //BAL.objBal.MessageBoxforUpdatePanel("Submitted Successfully.", btnSubmit);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted Successfully.", "S");       

            }
            else
            {
                BAL.objBal.MessageBoxforUpdatePanel(msg, btnSubmit);
            }
        }
       
    }


    private void loadGrid()
    {
        DataSet ds = new DataSet();

        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@QueryFor", "S"));
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));

        ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("ClassTeacherMasterProc", param);

        if (ds.Tables.Count > 0)
        {
            Grd1.DataSource = ds;
            Grd1.DataBind();
        }
    }

    protected void lnkShow_Click(object sender, EventArgs e)
    {
        displayEmpInfo();
    }
    protected void txtHeaderEmpId_TextChanged(object sender, EventArgs e)
    {
        displayEmpInfo();
    }
    public void displayEmpInfo()
    {
        var empId = Request.Form[hfEmployeeId.UniqueID];
        if (empId == string.Empty)
        {
            empId = txtHeaderEmpId.Text.Trim();
        }
        sql = "Select eod.EmpId EmpId,eod.Ecode Ecode,egd.EFirstName+' '+egd.EMiddleName+' '+egd.ELastName as EmpName,egd.EFatherName FatherName,eod.Designation Designation from EmpployeeOfficialDetails eod ";
        sql = sql + " inner join EmpGeneralDetail egd on eod.Ecode=egd.Ecode and eod.EmpId=egd.EmpId where eod.Withdrwal is null ";
        sql = sql + " and eod.ECode='" + empId.Trim() + "'";
        Grd.DataSource = BAL.objBal.GridFill(sql);
        Grd.DataBind();
        if (Grd.Rows.Count > 0)
        {
            div1.Visible = true;
            tb1.Visible = true;
        }
        else
        {
            //BAL.objBal.MessageBoxforUpdatePanel("Sorry, No Record found!", lnkShow);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, No Record found!", "A");       

            div1.Visible = false;
            tb1.Visible = false;
        }
    }
    protected void linkDelete_Click(object sender, EventArgs e)
    {
        Button8.Focus();
        LinkButton chk = (LinkButton)sender;
        Label lblId3 = (Label)chk.NamingContainer.FindControl("Label37");
        string ss = lblId3.Text;

        lblvalue.Text = lblId3.Text;

        Panel2_ModalPopupExtender.Show();

    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
        string msg = "";

        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@QueryFor", "D"));
        param.Add(new SqlParameter("@Id", lblvalue.Text.Trim()));
        SqlParameter para = new SqlParameter("@Msg", "");
        para.Direction = ParameterDirection.Output;
        para.Size = 0x100;
        param.Add(para);

        msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("ClassTeacherMasterProc", param);

        if (msg == "D")
        {
            loadGrid();
            //BAL.objBal.MessageBoxforUpdatePanel("Record, Deleted Successfully.", btnSubmit);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Record, Deleted Successfully.", "S");       

        }
        else
        {
            //BAL.objBal.MessageBoxforUpdatePanel("Sorry, Please contact your provider!", btnSubmit);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, Please contact your provider!", "A");       

        }

    }

    protected void drpBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadsection();
    }
}