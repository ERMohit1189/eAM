using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;

public partial class SuperAdmin_SmsPanelSetting : Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql , ss = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
       
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

        if (!IsPostBack)
        {
            try
            {
                //CheckValueADDDeleteUpdate();
                loadData();
                sql = "Select BranchId, BranchName from Branchtab";
                var dt = oo.Fetchdata(sql);
                ddlBranch.DataSource = dt;
                ddlBranch.DataTextField = "BranchName";
                ddlBranch.DataValueField = "BranchId";
                ddlBranch.DataBind();
                Session();
            }
            catch (Exception) { }
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
    }
    public void loadData()
    {
        sql = "Select * from SmsPanelSetting";

        if (ddlBranch.SelectedIndex==0 && oo.Duplicate(sql))
        {
            txtPanelurl.Enabled = false;
            TxtPassword.Enabled = false;
            TxtUserId.Enabled = false;
            TxtSenderId.Enabled = false;
            TxtPriority.Enabled = false;
            TxtSmstype.Enabled = false;
            RadioButton1.Checked = false;
            RadioButton2.Checked = false;
            RadioButton1.Enabled = false;
            RadioButton2.Enabled = false;
            RadioButtonList1.Enabled = false;
        }
        if (ddlBranch.SelectedIndex == 0 && !oo.Duplicate(sql))
        {
            txtPanelurl.Enabled = true;
            TxtPassword.Enabled = true;
            TxtUserId.Enabled = true;
            TxtSenderId.Enabled = true;
            TxtPriority.Enabled = true;
            TxtSmstype.Enabled = true;
            RadioButton1.Checked = false;
            RadioButton2.Checked = true;
            RadioButton1.Enabled = true;
            RadioButton2.Enabled = true;
            RadioButtonList1.Enabled = true;
        }
        if (ddlBranch.SelectedIndex != 0)
        {
            sql = "Select Id,PanelUrl,UserId,Password,SenderIdPass,Priority,Smstype,SmsPanel from SmsPanelSetting where BranchCode=" + ddlBranch.SelectedValue + "";
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
            sql = "Select HitValue  from  SMSActivateDeactivate where BranchCode=" + ddlBranch.SelectedValue + "";
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
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedIndex != 0)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SmsPanelSettingProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@PanelUrl ", txtPanelurl.Text.ToString());
            cmd.Parameters.AddWithValue("@UserId", TxtUserId.Text.ToString());
            cmd.Parameters.AddWithValue("@Password ", TxtPassword.Text.ToString());
            cmd.Parameters.AddWithValue("@SenderIdPass ", TxtSenderId.Text.ToString());
            cmd.Parameters.AddWithValue("@ClientIdPass ", "N/A");
            cmd.Parameters.AddWithValue("@LoginName", "SupperAdmin");
            cmd.Parameters.AddWithValue("@BranchCode", ddlBranch.SelectedValue);
            cmd.Parameters.AddWithValue("@SessionName", DrpSessionName.SelectedValue);
            cmd.Parameters.AddWithValue("@Priority", TxtPriority.Text.Trim());
            cmd.Parameters.AddWithValue("@Smstype", TxtSmstype.Text.ToString());
            cmd.Parameters.AddWithValue("@SmsPanel", RadioButtonList1.SelectedItem.ToString());
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                cmd.Parameters.Clear();
            }
            catch (Exception) { }
            string ss = "";
            string msg = "";
            cmd = new SqlCommand();
            cmd.CommandText = "SMSActivateDeactivateProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BranchCode", ddlBranch.SelectedValue);
            cmd.Connection = con;
            if (RadioButton1.Checked)
            {
                ss = "true";
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Activated successfully.", "S");
            }
            else
            {
                ss = "false";
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Deactivated successfully.", "S");
            }
            cmd.Parameters.AddWithValue("@HitValue", ss);

            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                con.Close();
                cmd.Parameters.Clear();
                loadData();
                oo.MessageBox(msg, this.Page);
            }
            catch (SqlException) { }
        }
        else
        {
            for (int i = 1; i < ddlBranch.Items.Count; i++)
            {
                ddlBranch.SelectedIndex = i;
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SmsPanelSettingProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                cmd.Parameters.AddWithValue("@PanelUrl ", txtPanelurl.Text.ToString());
                cmd.Parameters.AddWithValue("@UserId", TxtUserId.Text.ToString());
                cmd.Parameters.AddWithValue("@Password ", TxtPassword.Text.ToString());
                cmd.Parameters.AddWithValue("@SenderIdPass ", TxtSenderId.Text.ToString());
                cmd.Parameters.AddWithValue("@ClientIdPass ", "N/A");
                cmd.Parameters.AddWithValue("@LoginName", "SupperAdmin");
                cmd.Parameters.AddWithValue("@BranchCode", ddlBranch.SelectedValue);
                cmd.Parameters.AddWithValue("@SessionName", DrpSessionName.SelectedValue);
                cmd.Parameters.AddWithValue("@Priority", TxtPriority.Text.Trim());
                cmd.Parameters.AddWithValue("@Smstype", TxtSmstype.Text.ToString());
                cmd.Parameters.AddWithValue("@SmsPanel", RadioButtonList1.SelectedItem.ToString());
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    cmd.Parameters.Clear();
                }
                catch (Exception) { }

                string ss = "";
                cmd = new SqlCommand();
                cmd.CommandText = "SMSActivateDeactivateProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BranchCode", ddlBranch.SelectedValue);
                cmd.Connection = con;
                if (RadioButton1.Checked)
                {
                    ss = "true";
                }
                else
                {
                    ss = "false";
                }
                cmd.Parameters.AddWithValue("@HitValue", ss);
                try
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.ExecuteNonQuery();
                    con.Close();
                    cmd.Parameters.Clear();
                }
                catch (SqlException) { }
            }
            ddlBranch.SelectedIndex = 0;
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");
        }
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
        sql = sql + " left join LoginType Lt on LTb.LoginTypeId=Lt.LoginTypeId where LT.LoginTypeName='SupperAdmin' and LTb.LoginName='SupperAdmin' and LTb.BranchId='" + ddlBranch.SelectedValue + "'";
        int a=0;
        if (!string.IsNullOrEmpty(oo.ReturnTag(sql, "add1")))
        {
            a = Convert.ToInt32(oo.ReturnTag(sql, "add1"));
        }
        //PermissionGrant(a, (LinkButton)LinkButton1);
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

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session();
        loadData();
    }
}