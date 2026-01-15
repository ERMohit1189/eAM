using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using c4SmsNew;
using System.Net.NetworkInformation;

public partial class SuperAdmin_Customized_Message : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }

        con = oo.dbGet_connection();

        if (!IsPostBack)
        {
            bool bb = NetworkInterface.GetIsNetworkAvailable();

            if (bb == false)
            {
                //oo.MessageBoxforUpdatePanel("Internet connections are not available", this.Page);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Internet connections are not available", "A");

            }
            loadDesi();
        }
    }

    private void loadDesi()
    {

        sql = "Select EmpDesName,EmpDesId from EmpDesMaster where  BranchCode=" + Session["BranchCode"].ToString() + "";
        BAL.objBal.FillDropDown_withValue(sql, drpdes, "EmpDesName", "EmpDesId");
        drpdes.Items.Insert(0, new ListItem("<--Select Designation-->", "<--Select Designation-->"));
    }      
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        if (Repeater1.Items.Count > 0)
        {
            sql = "Select HitValue from SMSActivateDeactivate where BranchCode=" + Session["BranchCode"] + "";
            if (oo.ReturnTag(sql, "HitValue") != "")
            {
                if (oo.ReturnTag(sql, "HitValue") == "true")
                {
                    //sql = "Select SmsSent From SmsEmailMaster where Id='22' ";
                    //if (oo.ReturnTag(sql, "SmsSent").Trim() == "true")
                    //{
                        string contact = "";
                        foreach (RepeaterItem item in Repeater1.Items)
                        {
                            CheckBox Chk = (CheckBox)item.FindControl("Chk");
                            Label FmobileNo = (Label)item.FindControl("lblContactNo");
                            if (Chk.Checked)
                            {                               
                                if (contact == "")
                                {
                                    if (FmobileNo.Text.Length == 10)
                                    {
                                    contact = FmobileNo.Text;
                                    }
                                }
                                else
                                {
                                    if (FmobileNo.Text.Length == 10)
                                    {
                                        contact = contact +","+ FmobileNo.Text;
                                    }
                                }
                            }
                        }
                        if (contact != "")
                        {
                            bool bb = NetworkInterface.GetIsNetworkAvailable();

                            if (bb == false)
                            {
                                //oo.MessageBoxforUpdatePanel("Internet connections are not available", LinkButton1);
                                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Internet connections are not available", "A");

                            }
                            else
                            {
                                string str = SendFeesSms(contact);
                                //oo.MessageBoxforUpdatePanel("Message sent successfully", LinkButton1);
                                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Message sent successfully", "S");

                            }
                        }

                    //}
                    //else
                    //{
                    //    oo.MessageBoxforUpdatePanel("Permission not grantted", LinkButton1);
                    //}

                }
                else
                {
                    //oo.MessageBoxforUpdatePanel("Permission not grantted", LinkButton1);
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Permission not grantted", "A");

                }
            }
            else
            {
                //oo.MessageBoxforUpdatePanel("Permission not grantted", LinkButton1);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Permission not grantted", "A");

            }
        }
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {

    }

    public string SendFeesSms(string FmobileNo)
    {
        string sms_response = "";

        SMSAdapterNew sadpNew = new SMSAdapterNew();
        string mess = "";
        mess = txtMessage.Text.Trim();
        sms_response = sadpNew.Send(mess, FmobileNo, "");
        return sms_response;
#pragma warning disable 162
        txtMessage.Text = "";
#pragma warning restore 162
    }

    protected void ChkAll_CheckedChanged(object sender, EventArgs e)
    {
        Control HeaderTemplate = Repeater1.Controls[0].Controls[0];
        CheckBox chkall = HeaderTemplate.FindControl("ChkAll") as CheckBox;
        if (chkall.Checked)
        {
            foreach (RepeaterItem item in Repeater1.Items)
            {
                CheckBox chk = (CheckBox)item.FindControl("Chk");
                chk.Checked = true;
            }
        }
        else
        {
            foreach (RepeaterItem item in Repeater1.Items)
            {
                CheckBox chk = (CheckBox)item.FindControl("Chk");
                chk.Checked = false;
            }
        }
    }
    protected void drpdes_SelectedIndexChanged(object sender, EventArgs e)
    {
        display();
    }

    public void display()
    {
        sql = "Select Distinct EmpName,EmpFather,EmpGender,EmpContactNo,edm.EmpDesName ";
        sql = sql + " from Employmentform  ef";
        sql = sql + " Inner join EmpDesMaster edm on edm.EmpDesId=EmpDesignation";
        sql = sql + " where ef.SessionName='" + Session["SessionName"].ToString() + "' and  edm.BranchCode=" + Session["BranchCode"].ToString() + " and ef.BranchCode=" + Session["BranchCode"].ToString() + " and IsCancel='false' and EmpDesignation='"+drpdes.SelectedValue.ToString()+"'";

        Repeater1.DataSource = oo.GridFill(sql);
        Repeater1.DataBind();
    }
}