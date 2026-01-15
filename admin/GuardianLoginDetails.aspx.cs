using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web.UI.WebControls;

public partial class SuperAdmin_StaffLoginDetails : System.Web.UI.Page
{
    Campus camp = new Campus();
    protected void Page_Load(object sender, EventArgs e)
    {
        camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            BLL.BLLInstance.loadClass(DrpClass, Session["SessionName"].ToString());
            ddlSection.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
            //userPasswordDetails("-1");
        }
    }
    //load details
    public void userPasswordDetails()
    {
        List<SqlParameter> param = new List<SqlParameter>();

        param.Add(new SqlParameter("@Id", "-1"));
        param.Add(new SqlParameter("@ClassId", DrpClass.SelectedValue));
        if (ddlSection.SelectedIndex != 0)
        {
            param.Add(new SqlParameter("@SectionID", ddlSection.SelectedValue));

        }
        else
        {
            param.Add(new SqlParameter("@SectionID", "-1"));
        }
        if (ddlBranch.SelectedIndex != 0)
        {
            param.Add(new SqlParameter("@BranchId", ddlBranch.SelectedValue));

        }
        else
        {
            param.Add(new SqlParameter("@BranchId", "-1"));
        }
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));

        GridView1.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("GuardianLoginandPassword_Proc", param);
        GridView1.DataBind();
        if (GridView1.Rows.Count > 0)
        {
            lnkSend.Visible = true;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                TextBox txtPassword = (TextBox)GridView1.Rows[i].FindControl("txtPassword");
                txtPassword.Attributes["type"] = "password";
            }
        }
        else
        {
            lnkSend.Visible = false;
            GridView1.DataSource = null;
            GridView1.DataBind();
        }

    }
    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        Label lblEdit = (Label)lnk.NamingContainer.FindControl("lblEdit");
        lblID.Text = lblEdit.Text.Trim();

        List<SqlParameter> param = new List<SqlParameter>();

        param.Add(new SqlParameter("@Id", lblID.Text.Trim()));
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));

        DataSet ds = new DataSet();

        ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("GuardianLoginandPassword_Proc", param);
        if (ds != null)
        {
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                lbluserNamePanel.Text = dt.Rows[0]["UserName"].ToString();
                txtPasswordPanel.Text = dt.Rows[0]["Password"].ToString();
                drpactivePanel.SelectedIndex = dt.Rows[0]["IsBlocked"].ToString() == "Inactive" ? 1 : 0;
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
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@LoginName", Session["LoginName"].ToString()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));

        SqlParameter para = new SqlParameter("@Msg", "");
        para.Direction = ParameterDirection.Output;
        para.Size = 0x100;

        param.Add(para);

        Msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("GerrateGuardianLoginandPassword_Proc", param);

        if (Msg == "S")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "S");
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, Record not updated!", "W");
        }

        userPasswordDetails();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        userPasswordDetails();
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataBind();
    }
    protected void DrpClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        BLL.BLLInstance.loadSection(ddlSection, Session["SessionName"].ToString(), DrpClass.SelectedValue);
        BLL.BLLInstance.loadBranch(ddlBranch, Session["SessionName"].ToString(), DrpClass.SelectedValue);
        userPasswordDetails();
    }
    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        userPasswordDetails();
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        userPasswordDetails();
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
                sql = "Select HitValue from SMSActivateDeactivate where BranchCode=" + Session["BranchCode"] + "";
                if (BAL.objBal.ReturnTag(sql, "HitValue") != "")
                {
                    if (BAL.objBal.ReturnTag(sql, "HitValue") == "true")
                    {
                        for (int i = 0; i < GridView1.Rows.Count; i++)
                        {
                            Label lblSrno = (Label)GridView1.Rows[i].FindControl("lblSrno");
                            CheckBox chk = (CheckBox)GridView1.Rows[i].FindControl("chk");
                            if (chk.Checked)
                            {
                                //sql = "select FamilyContactNo from StudentFamilyDetails  where SrNo='" + lblSrno.Text.Trim() + "' and  BranchCode=" + Session["BranchCode"] + "  and SessionName='" + Session["SessionName"].ToString() + "'";
                                //string FamilyContactNo = "";
                                //FamilyContactNo = BAL.objBal.ReturnTag(sql, "FamilyContactNo");
                                ComposeSMSGuardian(lblSrno.Text.Trim());
                                Campus camp = new Campus();
                                camp.msgbox(this.Page, msgbox, "SMS sent successfully.", "S");
                            }
                        }
                    }
                    else
                    {
                        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "SMS Panel is not active!", "A");
                    }
                }
                else
                {
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "SMS Panel is not active!", "A");
                }
            }

        }
        catch
        {
        }
    }

    public void ComposeSMSGuardian(string srno)
    {
        try
        {
            List<SqlParameter> param = new List<SqlParameter>()
            {
                new SqlParameter("@SessionName",Session["SessionName"]),
                new SqlParameter("@SrNo",srno),
                new SqlParameter("@BranchCode",Session["BranchCode"])
            };
            DataSet ds = camp.ReturnDataSet("USP_GuardianLoginCredentialsTemplate", param.ToArray());
            if (ds != null && ds.Tables.Count > 0)
            {
                string msg = SendSms(ds, "14");
            }

        }
        catch
        {

        }

    }

    public string SendSms(DataSet ds, string pageid)
    {
        string msg;
        try
        {
            DataTable data = new DataTable();
            data = ds.Tables[0];

            var fatherContactNo = data.Rows[0]["ContactNo"].ToString();

            DataTable template = new DataTable();
            template = ds.Tables[1];

            msg = template.Rows[0][0].ToString();
            string[] param = template.Rows[0][1].ToString().Split(',');

            string[] daynamicVariables = msg.Split(new char[0]);
            foreach (var para in param)
            {
                string value = data.Rows[0][para].ToString();
                for (int i = 0; i < daynamicVariables.Count(); i++)
                {
                    if (daynamicVariables[i].ToString() == "{{{}}}")
                    {
                        daynamicVariables[i] = value;
                        break;
                    }
                }
            }

            msg = string.Join(" ", daynamicVariables);

            new SMS().SendSms(fatherContactNo, msg, pageid, Session["BranchCode"].ToString());

        }
        catch
        {
            msg = "";
        }
        return msg;
    }

    //public string SendFeesSmsGuardian(string FmobileNo, string srno)
    //{
    //    SMSAdapterNew sadpNew = new SMSAdapterNew();
    //    string mess = "";

    //    sql = "Select UserName,Password GuardianPassword from GuardianLoginandPassword";
    //    sql = sql + " where Srno='" + srno + "' and  BranchCode=" + Session["BranchCode"] + "";

    //    string UserName = BAL.objBal.ReturnTag(sql, "UserName");
    //    string GuardianPassword = BAL.objBal.ReturnTag(sql, "GuardianPassword");

    //    sql = "Select WebSite from CollegeMaster where CollegeId=" + Session["BranchCode"].ToString() + "";
    //    string collegeWebSite = BAL.objBal.ReturnTag(sql, "WebSite");

    //    mess = "Dear Parents,"+Environment.NewLine;
    //    mess = mess + "To catch up with the latest, login to school website " + collegeWebSite + " and click on Parent Portal." + Environment.NewLine;
    //    mess = mess + "Credentials are: " + Environment.NewLine;
    //    mess = mess + "Username: " + UserName + Environment.NewLine;
    //    mess = mess + "Password: " + GuardianPassword + Environment.NewLine;
    //    mess = mess + "Thanks. ";

    //    string sms_response = "";

    //    if (FmobileNo != "")
    //    {
    //        sms_response = sadpNew.Send(mess, FmobileNo, "35");
    //    }

    //    return sms_response;
    //}
}