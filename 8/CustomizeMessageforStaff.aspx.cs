using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;
using c4SmsNew;
using System.Net.NetworkInformation;

public partial class admin_CustomizeMessageforStaff : Page
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
            var isInternetConnectionAvailable = NetworkInterface.GetIsNetworkAvailable();
            if (isInternetConnectionAvailable == false)
            {
                //oo.MessageBoxforUpdatePanel("Internet connections are not available", this.Page);
                //Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Internet connections are not available.", "A");      
                ScriptManager.RegisterStartupScript(Page, GetType(), "alert", "alertmsg()", true);
            }
            sql = "select EmpDepName from EmpDepMaster where BranchCode="+ Session["BranchCode"] + "";
            oo.FillDropDown(sql, DrpDepartment, "EmpDepName");
            table3.Visible = false;
        }
    }
    protected void ChkAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkAll=(CheckBox)sender;
        if (chkAll.Checked == false)
        {
            foreach (GridViewRow gvr in Grd1.Rows)
            {
                CheckBox chk = (CheckBox)gvr.FindControl("chk");
                chk.Checked = false;
            }
        }
        else
        {
            foreach (GridViewRow gvr in Grd1.Rows)
            {
                CheckBox chk = (CheckBox)gvr.FindControl("chk");
                chk.Checked = true;
            }
        }
    }
    protected void DrpDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        sql = "select Row_Number() over (order by Eo.SrNo Asc) as SNo,Eo.DepartmentName,EO.Ecode,EO.EmpId,EO.RegistrationDate,EG.EMobileNo,EG.EFirstName,EG.EMiddleName,EG.ELastName from EmpployeeOfficialDetails EO ";
        sql = sql + "  inner join EmpGeneralDetail EG on EO.Ecode=EG.Ecode where Eo.DepartmentName='" + DrpDepartment.SelectedItem.ToString() + "'";
        sql = sql + " and eo.withdrwal is null and eo.BranchCode=" + Session["BranchCode"].ToString() + " and eg.BranchCode=" + Session["BranchCode"] + "";

        Grd1.DataSource = oo.GridFill(sql);
        Grd1.DataBind();
        if (Grd1.Rows.Count > 0)
        {
            table3.Visible = true;
        }
        else
        {
            table3.Visible = false;
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
         sql = "Select HitValue from SMSActivateDeactivate where BranchCode=" + Session["BranchCode"] + "";
         if (oo.ReturnTag(sql, "HitValue") != "")
         {
             if (oo.ReturnTag(sql, "HitValue") == "true")
             {
                 if (Grd1.Rows.Count > 0)
                 {
                     string str = "";
                     foreach (GridViewRow gvr in Grd1.Rows)
                     {
                         Label Label19 = (Label)gvr.FindControl("Label19");
                         CheckBox Chk = (CheckBox)gvr.FindControl("Chk");
                         if (Chk.Checked)
                         {
                             if (str == "")
                             {
                                 str = Label19.Text;
                             }
                             else
                             {
                                 str = str + "," + Label19.Text;
                             }
                         }
                     }
                     bool jj = NetworkInterface.GetIsNetworkAvailable();
                     if (jj == false)
                     {
                         //oo.MessageBoxforUpdatePanel("Internet connections are not available", LinkButton1);
                         Campus camp = new Campus(); camp.msgbox(this.Page, msgbox1, "Internet connections are not available", "A");

                     }
                     else
                     {
                         sendsinglemesg(str);
                     }
                 }
             }
             else
             {
                 //oo.MessageBoxforUpdatePanel("Permission not Granted!", LinkButton1);
                 Campus camp = new Campus(); camp.msgbox(this.Page, msgbox1, "Permission not Granted!", "A");

             }
         }
         else
         {
             //oo.MessageBoxforUpdatePanel("Permission not Granted!", LinkButton1);
             Campus camp = new Campus(); camp.msgbox(this.Page, msgbox1, "Permission not Granted!", "A");

         }
    }

    public void sendsinglemesg(string mobileno)
    {
        SendFeesSms(mobileno);       
        //Campus camp = new Campus(); camp.msgbox(this.Page, msgbox1, "Submitted successfully", "S");      
       
    }

    public void SendFeesSms(string FmobileNo)
    {
        string sms_response = "";
        SMSAdapterNew sadpNew = new SMSAdapterNew();
        string mess = "";
        mess = txtMessage.Text.Trim();
        sms_response = sadpNew.Send(mess, FmobileNo, "");

        string noHTML = System.Text.RegularExpressions.Regex.Replace(sms_response, @"<[^>]+>|&nbsp;", "").Trim();

        string noHTMLNormalised = System.Text.RegularExpressions.Regex.Replace(noHTML, @"\s{2,}", " ");

        long value = 0;

        noHTMLNormalised = noHTMLNormalised.Replace("S.", "");

        noHTMLNormalised = noHTMLNormalised.Replace("Job Id:", "");

        noHTMLNormalised = noHTMLNormalised.Replace(" ", "");

        noHTMLNormalised = noHTMLNormalised.Split(',').Length > 2 ? noHTMLNormalised.Split(',')[2].ToString() : noHTMLNormalised;

        bool flag = long.TryParse(noHTMLNormalised.Trim(), out value);

   
        if (flag)
        {
            //msg = "Message Sent Successfully!";
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox1, "Message Sent Successfully!", "S");
            txtMessage.Text = "";
        }
        else
        {
            //msg = noHTMLNormalised;
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox1, noHTMLNormalised, "W");
        }       
    }

}