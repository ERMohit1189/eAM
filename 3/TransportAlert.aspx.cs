using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using c4SmsNew;
using System.Net.NetworkInformation;

public partial class admin_VehicleNoWiseTransportAllotedStudentList : Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        //con.ConnectionString = ConfigurationManager.ConnectionStrings["Menu"].ConnectionString;

        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file
        if (!IsPostBack)
        {
            loadClass();

            DropDownList4.Items.Insert(0, new ListItem("<--Select-->", "-1"));

            sql = "Select VehicleType,id from VehicleMaster where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

            oo.FillDropDown_withValue(sql, DropDownList1, "VehicleType", "id");
            DropDownList1.Items.Insert(0, new ListItem("<--Select-->", "-1"));

            sql = "Select VehicleNo,id from VehicleDetails where VehicleType='" + DropDownList1.SelectedItem.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

            oo.FillDropDown_withValue(sql, DropDownList2, "VehicleNo", "id");
            DropDownList2.Items.Insert(0, new ListItem("<--Select-->", "-1"));

            sql = "Select RouteName,id from VehicleRouteMaster where SessionName='" + Session["SessionName"].ToString() + "'";

            oo.FillDropDown_withValue(sql, drpRoute, "RouteName", "id");
            drpRoute.Items.Insert(0, new ListItem("<--Select-->", "-1"));
        }
    }


    protected void loadClass()
    {
        sql = "Select ClassName,Id from ClassMaster";
        sql = sql + "  where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        sql = sql + " order by CIDOrder";
        oo.FillDropDown_withValue(sql, DropDownList3, "ClassName", "Id");
        DropDownList3.Items.Insert(0, new ListItem("<--Select-->", "-1"));
    }

    protected void loadSection()
    {
        sql = "Select SectionName,id from SectionMaster where ClassNameId=" + DropDownList3.SelectedValue.ToString();
        sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

        oo.FillDropDown_withValue(sql, DropDownList4, "SectionName", "Id");

        DropDownList4.Items.Insert(0, new ListItem("<--Select-->", "-1"));
    }

    private void loadGrid()
    {
        List<SqlParameter> param = new List<SqlParameter>();
        if (DropDownList3.SelectedIndex!=0)
        {
            param.Add(new SqlParameter("@Classid", DropDownList3.SelectedValue.ToString()));
        }
        if (DropDownList4.SelectedIndex != 0)
        {
            param.Add(new SqlParameter("@Sectionid", DropDownList4.SelectedValue.ToString()));
        }
        param.Add(new SqlParameter("@VehicleType", DropDownList1.SelectedIndex == 0 ? "-1" : DropDownList1.SelectedItem.Text.ToString()));
        param.Add(new SqlParameter("@VehicleNo", DropDownList2.SelectedIndex == 0 ? "-1" : DropDownList2.SelectedItem.Text.ToString()));
        param.Add(new SqlParameter("@VehicleRouteid", drpRoute.SelectedValue.ToString()));
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
        DataSet ds = new DataSet();
        ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_RouteWiseStudentAlert", param);

        GridView1.DataSource = ds;
        GridView1.DataBind();

    }

    protected void LinkButton6_Click(object sender, EventArgs e)
    {
        loadGrid();
        if (GridView1.Rows.Count == 0)
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, No Record(s) found!", "A");
        }
    }
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadSection();
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        sql = "Select VehicleNo,id from VehicleDetails where VehicleType='" + DropDownList1.SelectedItem.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

        oo.FillDropDown_withValue(sql, DropDownList2, "VehicleNo", "id");
        DropDownList2.Items.Insert(0, new ListItem("<--Select-->", "-1"));

    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        oo.ExportToWord(Response, "ListofTransportAllotedStudent.doc", gdv);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        oo.ExportToExcel("ListofTransportAllotedStudent.xls", GridView1);
    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }

    protected void lnkSend_Click(object sender, EventArgs e)
    {
        string contact = "";
        if (GridView1.Rows.Count > 0)
        {
            string _sql = "Select HitValue from SMSActivateDeactivate where BranchCode=" + Session["BranchCode"] + "";
            if (BAL.objBal.ReturnTag(_sql, "HitValue") != "")
            {
                if (BAL.objBal.ReturnTag(_sql, "HitValue") == "true")
                {
                    foreach (GridViewRow gvr in GridView1.Rows)
                    {
                        CheckBox chk = (CheckBox)gvr.FindControl("chk");
                        Label lblContact = (Label)gvr.FindControl("lblContact");
                        if (chk.Checked)
                        {
                            if (contact == "")
                            {
                                if (lblContact.Text.Length == 10)
                                {
                                    contact = lblContact.Text;
                                }
                            }
                            else
                            {
                                if (lblContact.Text.Length == 10)
                                {
                                    contact = contact + "," + lblContact.Text;
                                }
                            }
                        }
                    }
                }
                else
                {
                    Campus camp = new Campus(); camp.msgbox(Page, msg1, "SMS Panel has been deactivated!", "A");
                }
            }
        }
        if (contact != "")
        {
            bool bb = NetworkInterface.GetIsNetworkAvailable();

            if (bb == false)
            {
                //oo.MessageBoxforUpdatePanel("Internet connections are not available", LinkButton1);
                Campus camp = new Campus(); camp.msgbox(Page, msg1, "Internet connections are not available", "A");
            }
            else
            {
                string str = SendFeesSms(contact);

                string noHtml = System.Text.RegularExpressions.Regex.Replace(str, @"<[^>]+>|&nbsp;", "").Trim();

                string noHtmlNormalised = System.Text.RegularExpressions.Regex.Replace(noHtml, @"\s{2,}", " ");

                // ReSharper disable once RedundantAssignment
                double value = 0;

                noHtmlNormalised = noHtmlNormalised.Replace("S.", "");

                noHtmlNormalised = noHtmlNormalised.Replace("Job Id:", "");

                noHtmlNormalised = noHtmlNormalised.Replace(" ", "");

                noHtmlNormalised = noHtmlNormalised.Split(',').Length > 2 ? noHtmlNormalised.Split(',')[2] : noHtmlNormalised;

                bool flag = double.TryParse(noHtmlNormalised.Trim(), out value);

                if (flag)
                {
                    Campus camp = new Campus(); camp.msgbox(Page, msg1, "SMS Sent Successfully!", "S");
                }
                else
                {
                    Campus camp = new Campus(); camp.msgbox(Page, msg1, noHtmlNormalised, "W");
                }
            }
        }
    }

    public string SendFeesSms(string fmobileNo)
    {
        string smsResponse;

        SMSAdapterNew sadpNew = new SMSAdapterNew();
        string mess;
        mess = txtMessage.Text.Trim();
        smsResponse = sadpNew.Send(mess, fmobileNo, "");
        txtMessage.Text = "";
        return smsResponse;
    }

    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkAll = (CheckBox)sender;
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            CheckBox chk = (CheckBox)GridView1.Rows[i].FindControl("chk");
            chk.Checked = chkAll.Checked;
        }
    }
}