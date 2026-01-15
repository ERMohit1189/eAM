using System;
using System.Web.UI;
using System.Data.SqlClient;
using System.Data;
using c4SmsNew;
using System.Net.NetworkInformation;
using System.IO;

public partial class admin_GenralMessage : Page
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
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            
            loadGroup();
            Div1.InnerHtml = "";
            table1.Visible = true;
            txtNo.Focus();
            table2.Visible = false;
            table3.Visible = true;
            LinkButton1.Visible = true;
            LinkButton2.Visible = false;
            var isInternetConnectionAvailable = NetworkInterface.GetIsNetworkAvailable();
            if (isInternetConnectionAvailable == false)
            {
                table3.Visible = false;
                Div1.InnerHtml="Internet connections are not available.";
            }
        }
    }

    public void loadGroup()
    {
        sql = "Select GroupName From ContactGroupMaster where  BranchCode=" + Session["BranchCode"] + "";
        oo.FillDropDown(sql, drpGrpName, "GroupName");
    }

    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedIndex == 0)
        {
            table1.Visible = true;
            txtNo.Focus();
            table2.Visible = false;
            table3.Visible = true;
            LinkButton1.Visible = true;
            LinkButton2.Visible = false;
        }
        else
        {
            table1.Visible = false;
            drpGrpName.Focus();
            table2.Visible = true;
            table3.Visible = true;
            LinkButton1.Visible = false;
            LinkButton2.Visible = true;
        }
    }

    protected void txtMessage_TextChanged(object sender, EventArgs e)
    {
        Label8.Text = txtMessage.Text.Length.ToString();
        LinkButton1.Focus();
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedIndex == 0)
        {
            sendsinglemesg();
        }
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedIndex == 1)
        {
            sql = "Select ContactNo from AcadmicContacts where GroupName='" + drpGrpName.SelectedItem.ToString() + "' and BranchCode=" + Session["BranchCode"] + "";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                string contactno = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (contactno == "")
                    {
                        contactno = dt.Rows[i]["ContactNo"].ToString();
                    }
                    else
                    {
                        contactno = contactno+','+ dt.Rows[i]["ContactNo"].ToString();
                    }

                    bool jj = NetworkInterface.GetIsNetworkAvailable();
                    if (jj == false)
                    {
                        //oo.MessageBoxforUpdatePanel("Internet connections are not available", LinkButton2);
                        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox1, "Internet connections are not available.", "S"); 
                    }
                    else
                    {
                        string str = SendFeesSms(contactno);
                        
                        string noHTML = System.Text.RegularExpressions.Regex.Replace(str, @"<[^>]+>|&nbsp;", "").Trim();

                        string noHTMLNormalised = System.Text.RegularExpressions.Regex.Replace(noHTML, @"\s{2,}", " ");

                        double value = 0;

                        noHTMLNormalised = noHTMLNormalised.Replace("S.", "");

                        noHTMLNormalised = noHTMLNormalised.Replace("Job Id:", "");

                        //noHTMLNormalised = noHTMLNormalised.Replace(" ", "");

                        noHTMLNormalised = noHTMLNormalised.Replace(",", "");

                        bool flag = double.TryParse(noHTMLNormalised.Trim(), out value);

                        if (flag)
                        {
                            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox1, "SMS sent successfully.", "S"); 
                        }
                        else
                        {
                            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox1, noHTMLNormalised, "W");   
                        }                                 
                    }
                }
            }
        }
    }


    public void sendsinglemesg()
    {
        string[] mobileno = txtNo.Text.Split('\n');
        string mobilenos = string.Join(",", mobileno);

        string str = SendFeesSms(mobilenos);

        string noHTML = System.Text.RegularExpressions.Regex.Replace(str, @"<[^>]+>|&nbsp;", "").Trim();

        string noHTMLNormalised = System.Text.RegularExpressions.Regex.Replace(noHTML, @"\s{2,}", " ");

        long value = 0;

        noHTMLNormalised = noHTMLNormalised.Replace("S.", "");

        noHTMLNormalised = noHTMLNormalised.Replace("Job Id:", "");

        //noHTMLNormalised = noHTMLNormalised.Replace(" ", "");

        noHTMLNormalised = noHTMLNormalised.Split(',').Length > 2 ? noHTMLNormalised.Split(',')[2].ToString() : noHTMLNormalised;

        noHTMLNormalised = noHTMLNormalised.Replace(",", "");


        bool flag = long.TryParse(noHTMLNormalised.Trim(), out value);

        if (flag)
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox1, "SMS sent successfully.", "S");
            txtMessage.Text = "";
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox1, noHTMLNormalised, "W");
        }

    }

    public string SendFeesSms(string FmobileNo)
    {
        string sms_response = "";
        sql = "Select HitValue from SMSActivateDeactivate where BranchCode=" + Session["BranchCode"] + "";
        if (oo.ReturnTag(sql, "HitValue") != "")
        {
            if (oo.ReturnTag(sql, "HitValue") == "true")
            {
                SMSAdapterNew sadpNew = new SMSAdapterNew();
                string mess = "";
                mess = txtMessage.Text.Trim();
                sms_response = sadpNew.Send(mess, FmobileNo, "");
            }
            else
            {
                sms_response = "SMS Panel is not active!";
            }
        }
        return sms_response.ToString();
    }

    protected void lnkShow_Click(object sender, EventArgs e)
    {
        showExcel();
    }


    private void showExcel()
    {
        if (fu1.HasFile)
        {
            string getExtention = Path.GetExtension(fu1.FileName.ToString());

            string filePath = Server.MapPath("~/uploads/UploadExcel/mobilenolist") + getExtention;

            fu1.SaveAs(filePath);

           var dt2= BLL.BLLInstance.ReadExcel(filePath, getExtention, lnkShow);

            string mobilenos = "";

            for (int i=0;i<dt2.Rows.Count;i++)
            {
                if(mobilenos==string.Empty)
                {
                    mobilenos = mobilenos + dt2.Rows[i][0].ToString();
                }
                else
                {
                    mobilenos = mobilenos + Environment.NewLine + dt2.Rows[i][0].ToString();
                }
            }

            txtNo.Text = mobilenos;
        }
    }
}