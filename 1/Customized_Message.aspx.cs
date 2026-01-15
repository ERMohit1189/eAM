using System;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using System.Web.UI;
using System.Web.UI.WebControls;
using c4SmsNew;

namespace _1
{
    public partial class SuperAdminCustomizedMessage : Page
    {
        private SqlConnection _con;
        private readonly Campus _oo;
        private string _sql = String.Empty;
        public SuperAdminCustomizedMessage()
        {
            _con = new SqlConnection();
            _oo = new Campus();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
            {
                Response.Redirect("default.aspx");
            }

            _con = _oo.dbGet_connection();
            Campus camp = new Campus(); camp.LoadLoader(loader);
            if (!IsPostBack)
            {
                var isInternetConnectionAvailable = NetworkInterface.GetIsNetworkAvailable();
                if (!isInternetConnectionAvailable)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "alert", "alertmsg()", true);
                }

                table1.Visible = false;
                table2.Visible = false;
                table3.Visible = false;
                LoadCourse();
                LoadClass();
                QuickMessage();
                BLL.BLLInstance.loadSection(drpSection, Session["SessionName"].ToString(), drpClassCourse.SelectedValue);
                BLL.BLLInstance.loadBranch(drpBranch, Session["SessionName"].ToString(), drpClassCourse.SelectedValue);

                Displaydata();
            }
        }
        public void QuickMessage()
        {
            _sql = "select id, Title, SMS from tbl_QuickSMSTemplets where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            _oo.FillDropDown_withValue(_sql, drpQuickMessage, "Title", "Id");
            drpQuickMessage.Items.Insert(0, "<--Not Select-->");
        }
        public void LoadCourse()
        {
            _sql = "select id, CourseName from CourseMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " Order by id ";
            _oo.FillDropDown_withValue(_sql, drpCourse, "CourseName", "Id");
            drpCourse.Items.Insert(0, "All");
        }
        public void LoadClass()
        {
            _sql = "Select ClassName,Id from ClassMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " Order by CIDOrder ";
            _oo.FillDropDown_withValue(_sql, drpClassCourse, "ClassName","Id");
            drpClassCourse.Items.Insert(0, "<-- Select -->");
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            var studentId = Request.Form[hfStudentId.UniqueID];
            if (string.IsNullOrEmpty(studentId))
            {
                studentId = txtSearch.Text.Trim();
            }
            LoadSrGrid(LinkButton2, studentId);
        }
        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            var studentId = Request.Form[hfStudentId.UniqueID];
            if (string.IsNullOrEmpty(studentId))
            {
                studentId = txtSearch.Text.Trim();
            }
            LoadSrGrid(LinkButton2, studentId);
        }

        public void LoadSrGrid(Control ctrl, string studentId)
        {
            _sql = "Select *from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") where Withdrwal is null and Promotion is null";
            _sql = _sql + " and srno='" + studentId.Trim() + "'";

            Grd.DataSource = _oo.GridFill(_sql);
            Grd.DataBind();

            if (Grd.Rows.Count == 0)
            {
                //oo.MessageBoxforUpdatePanel("Sorry, No Record found!", ctrl);
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sorry, No Record found!", "A");   
            }
        }



        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Displaydata();
        }

        public void Displaydata()
        {
            if (RadioButtonList1.Items[1].Selected)
            {
                table1.Visible = false;
                table2.Visible = true;
                Grd.DataSource = null;
                Grd.DataBind();
                table3.Visible = true;
                txtSearch.Text = "";
                drpClassCourse.Focus();
                if (drpClassCourse.SelectedIndex!=0)
                {
                    LoadGrid(RadioButtonList1);
                }
            }
            else
            {
                table1.Visible = true;
                table2.Visible = false;
                table3.Visible = true;
                Grd.DataSource = null;
                Grd.DataBind();
                txtSearch.Text = "";
                txtSearch.Focus();
            }
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            if (Grd.Rows.Count > 0)
            {
                _sql = "Select HitValue from SMSActivateDeactivate where BranchCode=" + Session["BranchCode"] + "";
                if (_oo.ReturnTag(_sql, "HitValue") != "")
                {
                    if (_oo.ReturnTag(_sql, "HitValue") == "true")
                    {
                        string sql = "Select SmsSent From SmsEmailMaster where Id='22'";
                        if (_oo.ReturnTag(sql, "SmsSent").Trim() == "true")
                        {
                            string contact = "";
                            foreach (GridViewRow gvr in Grd.Rows)
                            {
                                CheckBox chk = (CheckBox)gvr.FindControl("Chk");
                                Label fmobileNo = (Label)gvr.FindControl("Label30");
                                if (chk.Checked)
                                {
                                    if (contact == "")
                                    {
                                        if (fmobileNo.Text.Length == 10)
                                        {
                                            contact = fmobileNo.Text;
                                        }
                                    }
                                    else
                                    {
                                        if (fmobileNo.Text.Length == 10)
                                        {
                                            contact = contact + "," + fmobileNo.Text;
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
                                        Campus camp = new Campus(); camp.msgbox(Page, msg1, "Message Sent Successfully!", "S");
                                    }
                                    else
                                    {
                                        Campus camp = new Campus(); camp.msgbox(Page, msg1, noHtmlNormalised, "W");
                                    }
                                }
                            }
                        }

                    }
                    else
                    {
                        Campus camp = new Campus(); camp.msgbox(Page, msg1, "SMS Panel is not active!", "A");
                    }
                }
                else
                {
                    Campus camp = new Campus(); camp.msgbox(Page, msg1, "SMS Panel is not active!", "A");
                }
            }
        }
        protected void LinkButton3_Click(object sender, EventArgs e)
        {

        }

        public string SendFeesSms(string fmobileNo)
        {
            string smsResponse;

            SMSAdapterNew sadpNew = new SMSAdapterNew();
            string mess;
            mess = txtMessage.Text.Trim();
            smsResponse = sadpNew.Send(mess, fmobileNo,"");
            txtMessage.Text = "";
            return smsResponse;
        }

        protected void ChkAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkall = (CheckBox)Grd.HeaderRow.FindControl("ChkAll");
            if (chkall.Checked)
            {
                foreach (GridViewRow gvr in Grd.Rows)
                {
                    CheckBox chk = (CheckBox)gvr.FindControl("Chk");
                    chk.Checked = true;
                }
            }
            else
            {
                foreach (GridViewRow gvr in Grd.Rows)
                {
                    CheckBox chk = (CheckBox)gvr.FindControl("Chk");
                    chk.Checked = false;
                }
            }
        }
        protected void drpClassCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpClassCourse.SelectedIndex != 0)
            {
                BLL.BLLInstance.loadSection(drpSection, Session["SessionName"].ToString(), drpClassCourse.SelectedValue);
                BLL.BLLInstance.loadBranch(drpBranch, Session["SessionName"].ToString(), drpClassCourse.SelectedValue);
                LoadGrid(drpClassCourse);
            }
            else
            {
                drpSection.Items.Clear();
                drpBranch.Items.Clear();
                Grd.DataSource = null;
                Grd.DataBind();
            }
            
        }

        public void LoadGrid(Control ctrl)
        {
            if (drpCourse.SelectedIndex == 0 && drpClassCourse.SelectedIndex == 0)
            {
                _sql = "Select *from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") where Withdrwal is null and Promotion is null order by CIDOrder, Name";
            }
            if (drpCourse.SelectedIndex != 0 && drpClassCourse.SelectedIndex == 0)
            {
                _sql = "Select *from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") where Withdrwal is null and Promotion is null and CourseId='" + drpCourse.SelectedValue+ "' order by CIDOrder, Name";

            }
            if ((drpCourse.SelectedIndex == 0 || drpCourse.SelectedIndex != 0) && drpClassCourse.SelectedIndex != 0)
            {
                _sql = "Select *from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") where Withdrwal is null and Promotion is null ";
                _sql = _sql + " and ClassName='" + drpClassCourse.SelectedItem + "'";
                if (drpSection.SelectedValue != "-1")
                {
                    _sql = _sql + " and SectionName='" + drpSection.SelectedItem + "'";
                }
                if (drpBranch.SelectedValue != "-1")
                {
                    _sql = _sql + " and BranchName='" + drpBranch.SelectedItem + "'";
                }
                _sql = _sql + "  order by CIDOrder,Name";
            }
            

            Grd.DataSource = _oo.GridFill(_sql);
            Grd.DataBind();

            if (Grd.Rows.Count == 0)
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sorry, No Record found!", "A"); 
            }
        }
        protected void drpSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGrid(drpSection);
        }
        protected void drpBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGrid(drpBranch);
        }

        public override void Dispose()
        {
            _con.Dispose();
            _oo.Dispose();
        }

        protected void drpCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpCourse.SelectedIndex == 0)
            {
                _sql = "Select ClassName,Id from ClassMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " Order by CIDOrder ";
                _oo.FillDropDown_withValue(_sql, drpClassCourse, "ClassName", "Id");
                drpClassCourse.Items.Insert(0, "All");
            }
            else
            {
                _sql = "Select ClassName,Id from ClassMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and Course='" + drpCourse.SelectedValue+ "' Order by CIDOrder ";
                _oo.FillDropDown_withValue(_sql, drpClassCourse, "ClassName", "Id");
                drpClassCourse.Items.Insert(0, "All");
            }
            LoadGrid(drpCourse);
        }

        protected void drpQuickMessage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpQuickMessage.SelectedIndex == 0)
            {
                txtMessage.Text = "";
            }
            else
            {
                _sql = "select id, Title, SMS from tbl_QuickSMSTemplets where id=" + drpQuickMessage.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                txtMessage.Text = _oo.ReturnTag(_sql, "SMS");
            }
        }
    }
}