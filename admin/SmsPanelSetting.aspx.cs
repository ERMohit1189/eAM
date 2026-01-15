using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;

public partial class SuperAdmin_SmsPanelSetting : Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql, ss = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Session["LoginName"].ToString()) || string.IsNullOrEmpty(Session["BranchCode"].ToString()) || string.IsNullOrEmpty(Session["SessionName"].ToString()))
        {
            Response.Redirect("default.aspx");
        }
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

        if (!IsPostBack)
        {
            try
            {
                //CheckValueADDDeleteUpdate();
                loadData();
            }
            catch (Exception) { }
        }
    }

    public void loadData()
    {
        sql = "Select Id,PanelUrl,UserId,Password,SenderIdPass,Priority,Smstype,SmsPanel from SmsPanelSetting where BranchCode=" + Session["BranchCode"] + "";
        txtPanelurl.Text = oo.ReturnTag(sql, "PanelUrl");
        TxtPassword.Text = oo.ReturnTag(sql, "Password");
        TxtUserId.Text = oo.ReturnTag(sql, "UserId");
        TxtSenderId.Text = oo.ReturnTag(sql, "SenderIdPass");
        TxtPriority.Text = oo.ReturnTag(sql, "Priority");
        TxtSmstype.Text = oo.ReturnTag(sql, "Smstype");
        for (int i = 0; i < RadioButtonList1.Items.Count; i++)
        {
            if (RadioButtonList1.Items[i].Text.ToUpper() == oo.ReturnTag(sql, "SmsPanel").ToUpper())
            {
                RadioButtonList1.SelectedValue = i.ToString() == "0" ? "1" : i.ToString();
            }
        }
        sql = "Select HitValue  from  SMSActivateDeactivate where BranchCode=" + Session["BranchCode"] + "";
        ss = oo.ReturnTag(sql, "HitValue");

        if (ss == "true")
        {
            RadioButton1.Checked = true;
            RadioButton2.Checked = false;
        }
        else
        {
            RadioButton2.Checked = true;
            RadioButton1.Checked = false;
        }

    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        if (RadioButton1.Checked)
        {
            ss = "true";
        }
        else
        {
            ss = "false";
        }

        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "SmsPanelSettingProc";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;

        cmd.Parameters.AddWithValue("@PanelUrl ", txtPanelurl.Text.ToString());
        cmd.Parameters.AddWithValue("@UserId", TxtUserId.Text.ToString());
        cmd.Parameters.AddWithValue("@Password ", TxtPassword.Text.ToString());
        cmd.Parameters.AddWithValue("@SenderIdPass ", TxtSenderId.Text.ToString());
        cmd.Parameters.AddWithValue("@ClientIdPass ", "N/A");
        cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
        cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
        cmd.Parameters.AddWithValue("@Priority", TxtPriority.Text.Trim());
        cmd.Parameters.AddWithValue("@Smstype", TxtSmstype.Text.ToString());
        cmd.Parameters.AddWithValue("@SmsPanel", RadioButtonList1.SelectedItem.ToString());
        cmd.Parameters.AddWithValue("@HitValue", ss);
        try
        {

            con.Open();
            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            con.Close();
            if (RadioButton1.Checked)
            {
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Activated successfully.", "S");
            }
            else
            {
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Deactivated successfully.", "S");
            }
        }
        catch (Exception) { }
    }
    public void PermissionGrant(int add1, LinkButton Ladd)
    {


        if (add1 == 1)
        {
            Ladd.Enabled = true;
        }
        else
        {
            Ladd.Enabled = false;
        }



    }
    public void CheckValueADDDeleteUpdate()
    {
        sql = " select LoginId,LoginName,Pass,SessionId,BranchId,LT.LoginTypeName,ltb.add1 as add1,ltb.delete1 as delete1,ltb.update1 as update1 from LoginTab LTb";
        sql = sql + " left join LoginType Lt on LTb.LoginTypeId=Lt.LoginTypeId where LT.LoginTypeName='" + Session["Logintype"] + "' and LTb.LoginName='" + Session["LoginName"] + "' and LTb.BranchId=" + Session["BranchCode"] + "";
        int a = 0;
        if (!string.IsNullOrEmpty(oo.ReturnTag(sql, "add1")))
        {
            a = Convert.ToInt32(oo.ReturnTag(sql, "add1"));
        }

        PermissionGrant(a, (LinkButton)LinkButton1);
    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedIndex == 0)
        {
            panel12.Visible = true;
            panel3.Visible = false;
            panel4.Visible = false;
        }
        else if (RadioButtonList1.SelectedIndex == 1)
        {
            panel12.Visible = false;
            panel3.Visible = true;
            panel4.Visible = false;
        }
        else
        {
            panel12.Visible = false;
            panel3.Visible = false;
            panel4.Visible = true;
        }
    }
}