using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using c4SmsNew;
using System.Net.NetworkInformation;

public partial class SuperAdmin_StaffLoginDetails : Page
{
    Campus oo = new Campus();
    protected void Page_Load(object sender, EventArgs e)
    {
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            string sql = "Select BranchId, BranchName from Branchtab";
            var dt = oo.Fetchdata(sql);
            ddlBranch.DataSource = dt;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "BranchId";
            ddlBranch.DataBind();
            Session();
            //userPasswordDetails("-1");
        }
    }
    protected void Session()
    {
        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@Queryfor", "S"));
        param.Add(new SqlParameter("@BranchCode", ddlBranch.SelectedValue));
        DataSet ds = new DataSet();
        ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("Get_GenralInfo", param);
        DrpSessionName.DataSource = ds.Tables[1];
        DrpSessionName.DataTextField = "SessionName";
        DrpSessionName.DataValueField = "SessionName";
        DrpSessionName.DataBind();
        DrpSessionName.SelectedIndex = (DrpSessionName.Items.Count - 1);
        BLL.BLLInstance.loadClass2(DrpClass, DrpSessionName.SelectedValue, ddlBranch.SelectedValue);
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session();
        BLL.BLLInstance.loadClass2(DrpClass, DrpSessionName.SelectedValue.ToString(), ddlBranch.SelectedValue);
    }
    public void userPasswordDetails(string classid)
    {
        List<SqlParameter> param = new List<SqlParameter>();

        param.Add(new SqlParameter("@Id", "-1"));
        param.Add(new SqlParameter("@ClassId", classid));
        param.Add(new SqlParameter("@SessionName", DrpSessionName.SelectedValue.ToString()));
        param.Add(new SqlParameter("@BranchCode", ddlBranch.SelectedValue));

        GridView1.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("StudentLoginandPassword_Proc", param);
        GridView1.DataBind();

        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            TextBox txtPassword = (TextBox)GridView1.Rows[i].FindControl("txtPassword");
            txtPassword.Attributes["type"] = "password";
        }

    }
    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        Label lblEdit = (Label)lnk.NamingContainer.FindControl("lblEdit");
        lblID.Text = lblEdit.Text.Trim();

        List<SqlParameter> param = new List<SqlParameter>();

        param.Add(new SqlParameter("@Id", lblID.Text.Trim()));
        param.Add(new SqlParameter("@SessionName", DrpSessionName.SelectedValue.ToString()));
        param.Add(new SqlParameter("@BranchCode", ddlBranch.SelectedValue));

        DataSet ds = new DataSet();

        ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("StudentLoginandPassword_Proc", param);
        if (ds != null)
        {
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                lbluserNamePanel.Text = dt.Rows[0]["UserName"].ToString();
                txtPasswordPanel.Text = dt.Rows[0]["Password"].ToString();
                drpactivePanel.SelectedIndex = dt.Rows[0]["IsBlocked"].ToString() == "YES" ? 1 : 0;
            }
        }

        Panel1_ModalPopupExtender.Show();
    }
    protected void lnkUpdate_Click(object sender, EventArgs e)
    {
        string Msg = "";


        List<SqlParameter> param = new List<SqlParameter>();

        param.Add(new SqlParameter("@QueryFor", "U"));
        param.Add(new SqlParameter("@Id", lblID.Text.Trim()));
        param.Add(new SqlParameter("@Password", txtPasswordPanel.Text.Trim()));
        param.Add(new SqlParameter("@IsActive", drpactivePanel.SelectedValue == "0" ? true : false));
        param.Add(new SqlParameter("@SessionName", DrpSessionName.SelectedValue.ToString()));
        param.Add(new SqlParameter("@LoginName", "SupperAdmin"));
        param.Add(new SqlParameter("@BranchCode", ddlBranch.SelectedValue));

        SqlParameter para = new SqlParameter("@Msg", "");
        para.Direction = ParameterDirection.Output;
        para.Size = 0x100;

        param.Add(para);

        Msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("GerrateStudentLoginandPassword_Proc", param);

        if (Msg == "S")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "S");
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, Record not updated!", "W");
        }

        userPasswordDetails(DrpClass.SelectedValue);
    }
    protected void DrpClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        userPasswordDetails(DrpClass.SelectedValue);
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        userPasswordDetails(DrpClass.SelectedValue);
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataBind();
    }
    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkall = (CheckBox)GridView1.HeaderRow.FindControl("ChkAll");
        if (chkall.Checked)
        {
            foreach (GridViewRow gvr in GridView1.Rows)
            {
                CheckBox chk = (CheckBox)gvr.FindControl("Chk");
                chk.Checked = true;
            }
        }
        else
        {
            foreach (GridViewRow gvr in GridView1.Rows)
            {
                CheckBox chk = (CheckBox)gvr.FindControl("Chk");
                chk.Checked = false;
            }
        }
    }

    string sql = "";
    protected void lnkSend_Click(object sender, EventArgs e)
    {
        try
        {
            bool chkInternetabail = NetworkInterface.GetIsNetworkAvailable();
            if (chkInternetabail == false)
            {
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Internet connections are not available.", "A");
            }
            else
            {
                sql = "Select HitValue from SMSActivateDeactivate where BranchCode=" + ddlBranch.SelectedValue + "";
                if (BAL.objBal.ReturnTag(sql, "HitValue") != "")
                {
                    if (BAL.objBal.ReturnTag(sql, "HitValue") == "true")
                    {
                        for (int i = 0; i < GridView1.Rows.Count; i++)
                        {
                            Label lblSrno = (Label)GridView1.Rows[i].FindControl("lblUsername");
                            CheckBox chk = (CheckBox)GridView1.Rows[i].FindControl("chk");
                            if (chk.Checked)
                            {
                                sql = "select MobileNumber from StudentGenaralDetail  where SrNo='" + lblSrno.Text.Trim() + "'  and BranchCode=" + ddlBranch.SelectedValue + " and SessionName='" + DrpSessionName.SelectedValue.ToString() + "'";
                                string MobileNumber = "";
                                MobileNumber = BAL.objBal.ReturnTag(sql, "MobileNumber");

                                SendFeeSms(MobileNumber, lblSrno.Text.Trim());
                                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Message sent successfully", "S");
                            }
                        }
                    }
                    else
                    {
                        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Permission not grantted. Please, Check your system setting!", "A");
                    }
                }
                else
                {
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Permission not grantted. Please, Check your system setting!", "A");
                }
            }
        }
        catch
        {
        }
    }

    public void SendFeeSms(string FamilyConNo, string srno)
    {
        if (FamilyConNo != string.Empty)
        {
            SendFeesSmsStudent(FamilyConNo, srno);
        }
    }

    public void SendFeesSmsStudent(string FmobileNo, string srno)
    {

        SMSAdapterNew sadpNew = new SMSAdapterNew();
        string mess = "";
        string collegeTitle = "";

        sql = "Select FirstName as StudentName   from StudentGenaralDetail";
        sql = sql + "    where  Srno='" + srno + "' and BranchCode=" + ddlBranch.SelectedValue + "";

        string StudentName = BAL.objBal.ReturnTag(sql, "StudentName");

        sql = "Select CollegeShortNa from CollegeMaster  where BranchCode=" + ddlBranch.SelectedValue + "";
        string CollegeShortNa = BAL.objBal.ReturnTag(sql, "CollegeShortNa");

        sql = "Select UserName,Password StudentPassword from StudentLoginandPassword where Srno='" + srno + "' and BranchCode=" + ddlBranch.SelectedValue + "";

        string UserName = BAL.objBal.ReturnTag(sql, "UserName");
        string StudentPassword = BAL.objBal.ReturnTag(sql, "StudentPassword");

        mess = "Congrats! " + StudentName + ", you've registered successfully with " + CollegeShortNa + ". Your Userid: " + UserName + " and Password: " + StudentPassword + "";

        string sms_response = "";

        sql = "Select CollegeShortNa  from CollegeMaster where BranchCode=" + ddlBranch.SelectedValue + "";
        collegeTitle = BAL.objBal.ReturnTag(sql, "CollegeShortNa");
        if (FmobileNo != "")
        {
            sms_response = sadpNew.Send(mess, FmobileNo, "");
        }
    }

    protected void DrpSessionName_SelectedIndexChanged(object sender, EventArgs e)
    {
        BLL.BLLInstance.loadClass2(DrpClass, DrpSessionName.SelectedValue.ToString(), ddlBranch.SelectedValue);
    }
}