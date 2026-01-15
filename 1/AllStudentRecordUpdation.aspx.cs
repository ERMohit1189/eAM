using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _1_AllStudentRecordUpdation : System.Web.UI.Page
{
    private readonly Campus _oo;
    SqlConnection con = new SqlConnection();
    string sql = "";
    public _1_AllStudentRecordUpdation()
    {
        _oo = new Campus();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if(!IsPostBack)
        {
            loadClass(drpclass);
            _oo.fillSelectvalue(drpBranch, "<--Select-->");
            _oo.fillSelectvalue(drpBranch, "<--Select-->");
            //  BLL.BLLInstance.loadClass(drpclass, Session["SessionName"].ToString());
            //  BLL.BLLInstance.loadSection(drpsection, Session["SessionName"].ToString(), drpclass.SelectedValue.ToString());
            // BLL.BLLInstance.loadBranch(drpBranch, Session["SessionName"].ToString(), drpclass.SelectedValue.ToString());
        }
    }
    public void LoadMachineNo(DropDownList drpMachinNo)
    {
        string sql = "select MachineNo from PunchMachineConfigurationStudent where BranchCode=" + Session["BranchCode"] + "";
        _oo.FillDropDown_withValue(sql, drpMachinNo, "MachineNo", "MachineNo");
        drpMachinNo.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    public void GetStudentDeatils()
    {
        List<SqlParameter> param = new List<SqlParameter>();
        if (drpclass.SelectedIndex!=0)
        {
            param.Add(new SqlParameter("@Classid", drpclass.SelectedValue.ToString()));
        }
        if (drpBranch.SelectedIndex != 0)
        {
            param.Add(new SqlParameter("@Branchid", drpBranch.SelectedValue.ToString()));
        }
        if (drpsection.SelectedIndex != 0)
        {
            param.Add(new SqlParameter("@SectionId", drpsection.SelectedValue.ToString()));
        }
        if (drpGender.SelectedIndex != 0)
        {
            param.Add(new SqlParameter("@Gender", drpGender.SelectedValue.ToString()));
        }
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
        param.Add(new SqlParameter("@OrerBY", rdoOrder.SelectedValue));
        param.Add(new SqlParameter("@Status", drpStatus.SelectedValue));
        var ds = new DLL().Sp_SelectRecord_usingExecuteDataset("Proc_GetStudentPersonalDetails", param);
        grdStdDeatils.DataSource = ds;
        grdStdDeatils.DataBind();
        if (grdStdDeatils.Rows.Count > 0)
        {
            DropDownList ddlMachineNoH = (DropDownList)grdStdDeatils.HeaderRow.FindControl("ddlMachineNoH");
            LoadMachineNo(ddlMachineNoH);
            lnkSubmit.Visible = true;
            for (int i = 0; i < grdStdDeatils.Rows.Count; i++)
            {
                DropDownList ddlMachineNo = (DropDownList)grdStdDeatils.Rows[i].FindControl("ddlMachineNo");
                LoadMachineNo(ddlMachineNo);
                try
                {
                    ddlMachineNo.SelectedValue = ds.Tables[0].Rows[i]["MachineNo"].ToString();
                }
                catch (Exception)
                {
                    ddlMachineNo.SelectedValue = "";
                }
                
            }
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "TabFunction();", true);
        }
        else
        {
            lnkSubmit.Visible = false;
        }
    }

    protected void drpclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadSection(drpsection, drpclass);
       
       // BLL.BLLInstance.loadSection(drpsection, Session["SessionName"].ToString(), drpclass.SelectedValue.ToString());
        BLL.BLLInstance.loadBranch(drpBranch, Session["SessionName"].ToString(),drpclass.SelectedValue.ToString());
        grdStdDeatils.DataSource = null;
        grdStdDeatils.DataBind();
    }
    private void loadSection(DropDownList drpsection, DropDownList drpclass)
    {
        if (Session["Logintype"].ToString() == "Admin")
        {
            BLL.BLLInstance.loadSection(drpsection, Session["SessionName"].ToString(), drpclass.SelectedValue.ToString());
        }
        else
        {
            sql = "Select distinct  SectionName,sm.Id from ClassTeacherMaster T1";
            sql += " inner join SectionMaster sm on sm.Id=T1.SectionId and sm.SessionName=t1.SessionName";
            sql += " where EmpCode='" + Session["LoginName"].ToString() + "' and IsClassTeacher=1 and T1.SessionName='" + Session["SessionName"] + "'";
            sql += " and t1.Classid=" + drpclass.SelectedValue.ToString() + " and sm.BranchCode=" + Session["BranchCode"] + " and T1.BranchCode=" + Session["BranchCode"] + "";
            BAL.objBal.FillDropDown_withValue(sql, drpsection, "SectionName", "Id");
            drpsection.Items.Insert(0, "<--Select-->");
        }
    }
    protected void lnkShow_Click(object sender, EventArgs e)
    {
        GetStudentDeatils();
    }

    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        int cnt = 0;
        foreach (GridViewRow gvr in grdStdDeatils.Rows)
        {
            Label lblSrno = (Label)gvr.FindControl("lblSrno");
            TextBox txtInstituteRollNo = (TextBox)gvr.FindControl("txtInstituteRollNo");
            TextBox txtCardNo = (TextBox)gvr.FindControl("txtCardNo");
            DropDownList ddlMachineNo = (DropDownList)gvr.FindControl("ddlMachineNo");

            int InstituteRlNo = 0;
            int.TryParse(txtInstituteRollNo.Text.Trim(), out InstituteRlNo);
            int StudenMachinId = 0;
            int.TryParse(txtCardNo.Text.Trim(), out StudenMachinId);
            int MachinNos = 0;
            int.TryParse(ddlMachineNo.SelectedValue, out MachinNos);
            if (InstituteRlNo!=0 || StudenMachinId!=0 || MachinNos!=0)
            {
                var msg = UpdateStudentDeatils(lblSrno.Text.Trim(), InstituteRlNo.ToString(), StudenMachinId.ToString(), MachinNos.ToString());
                if (msg != "S")
                {
                    txtCardNo.CssClass = "RedBorder";
                    cnt = cnt + 1;
                }
            }
        }
        
        if (cnt >0)
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgdiv, "Some Student(s) Machin Id are duplicate.", "A");
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgdiv, "Submitted successfully.", "S");
            GetStudentDeatils();
        }
        
    }
    public string UpdateStudentDeatils(string srno,string instituterollno, string cardno, string machinNo)
    {
        var param = new List<SqlParameter>
        {
            new SqlParameter("@SessionName",Session["SessionName"].ToString()),
            new SqlParameter("@BranchCode",Session["BranchCode"].ToString()),
            new SqlParameter("@srno",srno),
            new SqlParameter("@InstituteRollNo",instituterollno),
            new SqlParameter("@CardNo",cardno),
            new SqlParameter("@MachinNo",machinNo)
        };
        SqlParameter para = new SqlParameter("@Msg", "");
        para.Direction = ParameterDirection.Output;
        para.Size = 0x100;
        param.Add(para);
        return DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("Proc_UpdateStudentSessionalRecord", param);
        
    }

    protected void ddlMachineNoH_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlMachineNoH = (DropDownList)grdStdDeatils.HeaderRow.FindControl("ddlMachineNoH");
        for (int i = 0; i < grdStdDeatils.Rows.Count; i++)
        {
            DropDownList ddlMachineNo = (DropDownList)grdStdDeatils.Rows[i].FindControl("ddlMachineNo");
            ddlMachineNo.SelectedValue = ddlMachineNoH.SelectedValue;
        }
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "TabFunction();", true);
    }
    private void loadClass(DropDownList drpclass)
    {
        if (Session["logintype"].ToString() == "Admin")
        {
            BLL.BLLInstance.loadClass(drpclass, Session["SessionName"].ToString());
        }
        else
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
            param.Add(new SqlParameter("@EmpCode", Session["LoginName"].ToString()));

            drpclass.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("GetClassTeacherClassName_Proc", param);
            drpclass.DataTextField = "ClassName";
            drpclass.DataValueField = "Id";
            drpclass.DataBind();
            drpclass.Items.Insert(0, new ListItem("<--Select-->", "0"));
        }
    }

    protected void ddlInstituteRollNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        int count = 1;
        DropDownList ddlInstituteRollNo = (DropDownList)grdStdDeatils.HeaderRow.FindControl("ddlInstituteRollNo");
        if (ddlInstituteRollNo.SelectedValue== "0")
        {
            for (int i = 0; i < grdStdDeatils.Rows.Count; i++)
            {
                TextBox txtInstituteRollNo = (TextBox)grdStdDeatils.Rows[i].FindControl("txtInstituteRollNo");
                txtInstituteRollNo.Text = string.Empty;
            }
        }
        else
        {
            for (int i = 0; i < grdStdDeatils.Rows.Count; i++)
            {
                // DropDownList ddlMachineNo = (DropDownList)grdStdDeatils.Rows[i].FindControl("ddlInstituteRollNo");
                TextBox txtInstituteRollNo = (TextBox)grdStdDeatils.Rows[i].FindControl("txtInstituteRollNo");
                txtInstituteRollNo.Text = count.ToString();
                count++;
            }
        }
       
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "TabFunction();", true);
    }
}