using c4SmsNew;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class forgot_password : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            sql = "select BrandName from BrandTab where isnull(BrandName, '')!=''";
            if (oo.Duplicate(sql))
            {
                divCompany.Visible = true;
                Label1.Text = oo.ReturnTag(sql, "BrandName");

            }
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        sql = "select Status, Password from AlumniRegistration where ContactNo='" + txtContactno.Text.Trim() + "'";
        if (oo.ReturnTag(sql, "Status")=="Pending")
        {
            alert_Pending.Style.Add("display", "block");
            alert_Inactive.Style.Add("display", "none");
            alert_Rejected.Style.Add("display", "none");
            alert_danger.Style.Add("display", "none");
            alert_success.Style.Add("display", "none");
        }
        else if (oo.ReturnTag(sql, "Status") == "Inactive")
        {
            alert_Pending.Style.Add("display", "none");
            alert_Inactive.Style.Add("display", "block");
            alert_Rejected.Style.Add("display", "none");
            alert_danger.Style.Add("display", "none");
            alert_success.Style.Add("display", "none");
        }
        else if (oo.ReturnTag(sql, "Status") == "Rejected")
        {
            alert_Pending.Style.Add("display", "none");
            alert_Inactive.Style.Add("display", "none");
            alert_Rejected.Style.Add("display", "block");
            alert_danger.Style.Add("display", "none");
            alert_success.Style.Add("display", "none");
        }
        else 
        {
            if (oo.ReturnTag(sql, "Password") != "")
            {
                alert_Pending.Style.Add("display", "none");
                alert_Inactive.Style.Add("display", "none");
                alert_Rejected.Style.Add("display", "none");
                alert_danger.Style.Add("display", "none");
                alert_success.Style.Add("display", "block");

                SendFeesSms(txtContactno.Text.Trim(), oo.ReturnTag(sql, "Password"));
            }
            else
            {
                alert_Pending.Style.Add("display", "none");
                alert_Inactive.Style.Add("display", "none");
                alert_Rejected.Style.Add("display", "none");
                alert_danger.Style.Add("display", "block");
                alert_success.Style.Add("display", "none");
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "GenerateCaptcha();", true);
            }
        }
    }
    
    public void SendFeesSms(string fmobileNo,string pass)
    {
        try
        {
            var sadpNew = new SMSAdapterNew();
            var mess = "Your Password for Alumni Portal is "+ pass + " ";
            if (fmobileNo == "") return;
            sadpNew.Send(mess, fmobileNo, "38");
        }
        catch
        {
            // ignored
        }
    }

}