using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using c4SmsNew;

namespace _11
{
    public partial class CommonConversation : Page
    {
        SqlConnection _con;
        readonly Campus _oo;
        private string _sql = "";
        private string _filePath = "";
        // ReSharper disable once IdentifierTypo
        readonly DataTable _dtconv;
        bool _ct,_md, _director, _manager, _principal, _admin;
        private string _msginsert, _faemail, _msgsentto, _msgsentby = string.Empty;

        public CommonConversation()
        {
            _con = new SqlConnection();
            _oo = new Campus();
            _dtconv = new DataTable();
        }

        protected void Page_PreInIt(object sender, EventArgs e)
        {
            if (Session["Logintype"] == null) { Response.Redirect("~/default.aspx"); }
            // ReSharper disable once PossibleNullReferenceException
            if (Session["Logintype"].ToString() == "Admin")
            {
                MasterPageFile = "~/Master/admin_root-manager.master";
            }
            else if (Session["Logintype"].ToString() == "Staff")
            {
                MasterPageFile = "~/Staff/staff_root-manager.master";
            }
            else if (Session["Logintype"].ToString() == "Guardian")
            {
                MasterPageFile = "~/sp/sp_root-manager.master";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Campus camp = new Campus(); camp.LoadLoader(loader);
            _con = _oo.dbGet_connection();
            if (!IsPostBack)
            {
                LoadClass(drpClass);
                BAL.objBal.fillSelectvalue(drpSection, "<--Select-->", "-1");
                BAL.objBal.fillSelectvalue(drpBranch, "<--Select-->", "-1");

                if (Session["Logintype"].ToString() == "Guardian")
                {
                    divDropdown.Visible = true;
                    forGuardianShop.Visible = true;
                    divshowclass.Visible = false;
                    divshowbranch.Visible = false;
                    divshowsection.Visible = false;
                    divteacherlist.Visible = false;
                    divconlist.Attributes.Add("class", "col-sm-12 ");
                }
                else
                {
                    divDropdown.Visible = true;
                    forGuardianShop.Visible = false;
                    divshowclass.Visible = true;
                    divshowbranch.Visible = true;
                    divshowsection.Visible = true;
                    divteacherlist.Visible = true;
                    divconlist.Attributes.Add("class", "col-md-8 col-sm-6 ");
                }

                if (Session["Logintype"].ToString() == "Guardian")
                {
                    GetTeacherProfile();
                }
            }
        }

        private void LoadClass(DropDownList drpclass)
        {
            if (Session["logintype"].ToString() == "Admin")
            {
                BLL.BLLInstance.loadClass(drpClass, Session["SessionName"].ToString());
            }
            else
            {
                var param = new List<SqlParameter>();
                param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
                param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
                param.Add(new SqlParameter("@EmpCode", Session["LoginName"].ToString()));

                drpclass.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("GetClassTeacherClassName_Proc", param);
                drpclass.DataTextField = "ClassName";
                drpclass.DataValueField = "Id";
                drpclass.DataBind();
                drpclass.Items.Insert(0, new ListItem("<--Select-->", "0"));     

            }
        }
        private void LoadSection(DropDownList drpsection, DropDownList drpclass)
        {
            if (Session["Logintype"].ToString() == "Admin")
            {
                BLL.BLLInstance.loadSection(drpsection, Session["SessionName"].ToString(), drpclass.SelectedValue);
            }
            else
            {
                _sql = "Select SectionName,sm.Id from ClassTeacherMaster T1";
                _sql +=  " inner join SectionMaster sm on sm.Id=T1.SectionId and sm.SessionName=t1.SessionName";
                _sql +=  " where EmpCode='" + Session["LoginName"] + "' and IsClassTeacher=1 and T1.SessionName='" + Session["SessionName"] + "'";
                _sql +=  " and t1.Classid=" + drpclass.SelectedValue + "";
                BAL.objBal.FillDropDown_withValue(_sql, drpsection, "SectionName", "Id");
            }
        }
        private void LoadBranch(DropDownList drpbranch, DropDownList drpclass)
        {
            if (Session["Logintype"].ToString() == "Admin")
            {
                BLL.BLLInstance.loadBranch(drpbranch, Session["SessionName"].ToString(), drpclass.SelectedValue);
            }
            else
            {
                _sql = "Select BranchName,bm.Id from ClassTeacherMaster T1";
                _sql +=  "   inner join BranchMaster bm on bm.Id=T1.BranchId and bm.SessionName=t1.SessionName";
                _sql +=  "   where EmpCode='" + Session["LoginName"] + "' and IsClassTeacher=1 and";
                _sql +=  "   T1.SessionName='" + Session["SessionName"] + "' and T1.Classid='" + drpclass.SelectedValue + "'";
                BAL.objBal.FillDropDown_withValue(_sql, drpbranch, "BranchName", "Id");
            }
        }


        private void GetTeacherProfile()
        {
            DataTable dt = null;

            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Srno", Session["Srno"].ToString()));
            param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));

            DataSet ds;
            ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("GetClassTeacherProfile_Proc", param);

            if (ds != null)
            {
                DataView dv = new DataView(ds.Tables[0]);
                dt = dv.ToTable(true, "CTNAME", "CTContactNo", "CTEmail", "ClassTeacherCode");

                dt.Columns["CTNAME"].ColumnName = "Name";
                dt.Columns["CTContactNo"].ColumnName = "ContactNo";
                dt.Columns["CTEmail"].ColumnName = "Email";
                dt.Columns["ClassTeacherCode"].ColumnName = "Ids";

                Session["Ids"] = dt.Rows[0]["Ids"].ToString();
            }

            rpt2.DataSource = dt;
            rpt2.DataBind();
        }

        protected void rblist1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Session["Logintype"].ToString() == "Guardian")
            {
                if (rblist1.SelectedIndex == 0)
                {
                    div1.Visible = true;
                    div2.Visible = false;
                    divshowclass.Visible = false;
                    divshowbranch.Visible = false;
                    divshowsection.Visible = false;
                    divteacherlist.Visible = false;
                    divconlist.Attributes.Add("class", "col-sm-12 ");
                }
                else
                {
                    div1.Visible = false;
                    div2.Visible = true;
                    divshowclass.Visible = false;
                    divshowbranch.Visible = false;
                    divshowsection.Visible = false;
                    divteacherlist.Visible = false;
                    divconlist.Attributes.Add("class", "col-sm-12 ");

                    LoadMyConversation();
                }
            }
            else
            {
                if (rblist1.SelectedIndex == 0)
                {
                    div1.Visible = true;
                    div2.Visible = false;
                    divshowclass.Visible = true;
                    divshowbranch.Visible = true;
                    divshowsection.Visible = true;
                    divteacherlist.Visible = true;
                    divconlist.Attributes.Add("class", "col-md-8 col-sm-6 ");
                }
                else
                {
                    div1.Visible = false;
                    div2.Visible = true;
                    divshowclass.Visible = false;
                    divshowbranch.Visible = false;
                    divshowsection.Visible = false;
                    divteacherlist.Visible = false;
                    divconlist.Attributes.Add("class", "col-sm-12 ");

                    LoadMyConversation();
                }
            }
        
            ScriptManager.RegisterStartupScript(rblist1, GetType(), "summernote", "txtAreaHtml()", true);
        }
        protected void lnkcatta_Click(object sender, EventArgs e)
        {
            hidFile.Value = string.Empty;
            hidFileExt.Value = string.Empty;
        }
        protected void lnkSubmit_Click(object sender, EventArgs e)
        {
            string subject = txtSubject.Text.Trim();
            string msg = txtDescription.Text.Trim();

            if (Session["Logintype"].ToString() == "Guardian")
            {
                if (CheckBoxList1.SelectedIndex != -1)
                {
                    if (chkAll2.Checked && txtSubject.Text.Trim() != string.Empty)
                    {
                        _ct = true; _md = true; _director = true; _manager = true; _principal = true; _admin = true;
                    }
                    else
                    {
                        if (CheckBoxList1.SelectedItem.Selected && txtSubject.Text.Trim() != string.Empty)
                        {
                            // ReSharper disable once RedundantBoolCompare
                            _ct = CheckBoxList1.Items.FindByValue("CT").Selected == true;
                            // ReSharper disable once RedundantBoolCompare
                            _md = CheckBoxList1.Items.FindByValue("MD").Selected == true;
                            // ReSharper disable once RedundantBoolCompare
                            _director = CheckBoxList1.Items.FindByValue("Director").Selected == true;
                            // ReSharper disable once RedundantBoolCompare
                            _manager = CheckBoxList1.Items.FindByValue("Manager").Selected == true;
                            // ReSharper disable once RedundantBoolCompare
                            _principal = CheckBoxList1.Items.FindByValue("Principal").Selected == true;
                            // ReSharper disable once RedundantBoolCompare
                            _admin = CheckBoxList1.Items.FindByValue("Admin").Selected == true;
                        }
                    }
                    // ReSharper disable once RedundantBoolCompare
                    if (_ct == true)
                    {
                        foreach (RepeaterItem ri in rpt2.Items)
                        {
                            CheckBox chk = (CheckBox)ri.FindControl("chk");
                            Label lblsrno = (Label)ri.FindControl("lblSrno");
                            Label lblFamilyEmail = (Label)ri.FindControl("lblFamilyEmail");
                            if (chk.Checked)
                            {
                                _msgsentto = lblsrno.Text.Trim();
                                _msgsentby = Session["LoginName"].ToString();
                                _faemail = lblFamilyEmail.Text.Trim();
                            }
                        }
                    }
                    else
                    {
                        _msgsentto = "";
                        _msgsentby = Session["LoginName"].ToString();
                        _faemail = "";
                    }
                    string base64Std1 = hidFile.Value;
                    if (base64Std1 != string.Empty)
                    {
                        // ReSharper disable once RedundantAssignment
                        string fileName = "";
                        string fileExtention = hidFileExt.Value;

                        _filePath = @"../Uploads/Attachment/";
                        string datetime = DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss");
                        fileName = String.Format("{0}", datetime + "_Send" + fileExtention);
                        _filePath = _filePath + fileName;
                        // ReSharper disable once AssignNullToNotNullAttribute
                        using (FileStream fs = new FileStream(Server.MapPath(_filePath), FileMode.Create))
                        {
                            using (BinaryWriter bw = new BinaryWriter(fs))
                            {
                                byte[] data = Convert.FromBase64String(base64Std1);
                                bw.Write(data);
                                bw.Close();
                            }
                        }
                    }

                    _msginsert = Insert(_msgsentby, _msgsentto, _faemail, txtSubject.Text.Trim(), txtDescription.Text.Trim(), _filePath, Session["LoginTypeId"].ToString(), _ct, _md, _director, _manager, _principal, _admin);

                    if (_msginsert == "S")
                    {
                        Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sent", "S");
                    }
                    else
                    {
                        Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sorry", "W");
                    }

                    txtSubject.Text = String.Empty;
                    txtDescription.Text= String.Empty;
                    txtMobileno.Text = String.Empty;
                    hidFile.Value = String.Empty;
                    hidFileExt.Value = String.Empty;
                }
                else
                {
                    //chkAll2.Focus();
                    chkAll2.Attributes.Add("onLoad", "setFocus()");
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Please, Select For ", "W");
                }
            }
            else
            {
                if (txtSubject.Text.Trim() != string.Empty)
                {
                    bool flag = false;
                    for (int i = 0; i < rpt2.Items.Count; i++)
                    {
                        CheckBox chk = (CheckBox)rpt2.Items[i].FindControl("chk");
                        if (chk.Checked)
                        {
                            flag = true;
                        }
                    }

                    if (flag)
                    {
                        Thread thread = new Thread(() => { EmailSending(subject, msg, txtToEmail.Text.Trim()); });
                        Thread thread1 = new Thread(() => { SendFeesSms(subject, txtMobileno.Text.Trim()); });

                        thread.IsBackground = true;
                        thread1.IsBackground = true;

                        thread.Start();
                        thread1.Start();

                        // ReSharper disable once RedundantNameQualifier
                        System.Threading.Thread.Sleep(1000);
                        var builder = new StringBuilder();
                        builder.Append(_filePath);

                        foreach (RepeaterItem ri in rpt2.Items)
                        {
                            CheckBox chk = (CheckBox)ri.FindControl("chk");
                            Label lblsrno = (Label)ri.FindControl("lblSrno");
                            Label lblFamilyEmail = (Label)ri.FindControl("lblFamilyEmail");
                            if (chk.Checked)
                            {
                                _msgsentto = lblsrno.Text.Trim();

                                _msgsentby = Session["LoginName"].ToString();

                                //DataTable dttt = new DataTable();
                                //dttt.Columns.Add("ChekName");
                                //List<string> selectedValues = CheckBoxList1.Items.Cast<ListItem>().Where(li => li.Selected).Select(li => li.Value).ToList();
                                //for (int c = 0; c < selectedValues.Count; c++)
                                //{
                                //    dttt.Rows.Add(selectedValues[c]);
                                //}
                                var base64Std1 = hidFile.Value;
                                if (base64Std1 != string.Empty)
                                {
                                    // ReSharper disable once RedundantAssignment
                                    var fileName = "";
                                    var fileExtention = hidFileExt.Value;

                                    _filePath = @"../Uploads/Attachment/";
                                    var datetime = DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss");
                                    // ReSharper disable once BuiltInTypeReferenceStyle
                                    // ReSharper disable once UseStringInterpolation
                                    fileName = String.Format("{0}", datetime + "_Send" + fileExtention);
                                    builder.Append(fileName);
                                    // ReSharper disable once AssignNullToNotNullAttribute
                                    using (FileStream fs = new FileStream(Server.MapPath(_filePath), FileMode.Create))
                                    {
                                        using (BinaryWriter bw = new BinaryWriter(fs))
                                        {
                                            byte[] data = Convert.FromBase64String(base64Std1);
                                            bw.Write(data);
                                            bw.Close();
                                        }
                                    }
                                }

                                _msginsert = Insert(_msgsentby, _msgsentto, lblFamilyEmail.Text.Trim(), txtSubject.Text.Trim(), txtDescription.Text.Trim(), _filePath, Session["LoginTypeId"].ToString(), _ct, _md, _director, _manager, _principal, _admin);

                                if (_msginsert != "S")
                                {
                                    lblsrno.ForeColor = System.Drawing.ColorTranslator.FromHtml("#da4448");
                                }
                                else
                                {
                                    lblsrno.ForeColor = System.Drawing.ColorTranslator.FromHtml("#333");
                                }
                            }
                        }
                        _filePath = builder.ToString();

                        if (_msginsert == "S")
                        {
                            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sent", "S");
                        }
                        else
                        {
                            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sorry", "W");
                        }

                        txtSubject.Text = string.Empty;
                        txtMobileno.Text = string.Empty;
                        hidFile.Value = string.Empty;
                        hidFileExt.Value = string.Empty;
                    }
                    else
                    {
                        Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Please, Select at least one S.R. No.", "A");
                    }
                }
                else
                {
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Please, Add Subject", "A");
                    ScriptManager.RegisterClientScriptBlock(lnkSubmit, GetType(), "summernote", "setFocus()", true);
                }
            }
       
            ScriptManager.RegisterStartupScript(lnkSubmit, GetType(), "summernote", "txtAreaHtml()", true);

        }

        protected string Insert(string sentby, string sentTo, string email, string emailsubject, string emaildiscription, string attechment, string msgsentby,bool ct1,bool md1, bool director1, bool manager1, bool principal1, bool admin1)
        {
            // ReSharper disable once RedundantAssignment
            var msg = "";
            var param = new List<SqlParameter>();

            param.Add(new SqlParameter("@Sentby", sentby));
            param.Add(new SqlParameter("@SentTo", sentTo));
            param.Add(new SqlParameter("@SenttoEmail", email));
            param.Add(new SqlParameter("@Subject", emailsubject));
            param.Add(new SqlParameter("@Discription", emaildiscription));
            param.Add(new SqlParameter("@AttechmentPath", attechment));
            param.Add(new SqlParameter("@Msgsentby", msgsentby));
            param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
            param.Add(new SqlParameter("@LoginName", Session["LoginName"].ToString()));

            param.Add(new SqlParameter("@CT", ct1));
            param.Add(new SqlParameter("@MD", md1));
            param.Add(new SqlParameter("@Director", director1));
            param.Add(new SqlParameter("@Manager", manager1));
            param.Add(new SqlParameter("@Principal", principal1));
            param.Add(new SqlParameter("@Admin", admin1));

            var para = new SqlParameter("@Msg", "")
            {
                Direction = ParameterDirection.Output,
                Size = 0x100
            };

            param.Add(para);

            msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("TeacherGuardianConversation_Proc", param);


            return msg;
        }
        private DataTable GetStudentsRecordinGrid()
        {
            // ReSharper disable once RedundantAssignment
            DataTable dt = null;

            var param = new List<SqlParameter>();
            param.Add(new SqlParameter("@ClassName", drpClass.SelectedItem.Text));
            param.Add(new SqlParameter("@SectionName", drpSection.SelectedItem.Text));
            param.Add(new SqlParameter("@BranchName", drpBranch.SelectedItem.Text));
            param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));

            var ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("Get_AllStudentRecord", param);

            using (var dv = new DataView(ds.Tables[0]))
            {
                dt = dv.ToTable(true, "S.R.No.", "Name", "Family Email", "Guardian Contact No.");

                dt.Columns["S.R.No."].ColumnName = "ids";
                dt.Columns["Family Email"].ColumnName = "Email";
                dt.Columns["Guardian Contact No."].ColumnName = "ContactNo";
                return dt;
            }
        }
        public bool EmailSending(string subject, string msg, string emaidId)
        {
            var flag = false;

            var smtp = new SmtpClient();
            if (emaidId != string.Empty)
            {

                var mail = new MailMessage();

                emaidId = emaidId.Replace("\n", "");

                mail.To.Add(emaidId);//to ID

                mail.From = new MailAddress("donotreply@eam.co.in");
                mail.Subject = subject;

                mail.BodyEncoding = Encoding.UTF8;
                mail.SubjectEncoding = Encoding.UTF8;

                var htmlView = AlternateView.CreateAlternateViewFromString(msg);
                htmlView.ContentType = new System.Net.Mime.ContentType("text/html");
                mail.AlternateViews.Add(htmlView);

                var base64Std = hidFile.Value;
                if (base64Std != string.Empty)
                {
                    // ReSharper disable once RedundantAssignment
                    var fileName = "";
                    var fileExtention = hidFileExt.Value;

                    _filePath = @"../Uploads/Attachment/";
                    var datetime = DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss");
                    // ReSharper disable once BuiltInTypeReferenceStyle
                    // ReSharper disable once UseStringInterpolation
                    fileName = String.Format("{0}", datetime + fileExtention);
                    _filePath = _filePath + fileName;
                    // ReSharper disable once AssignNullToNotNullAttribute
                    using (FileStream fs = new FileStream(Server.MapPath(_filePath), FileMode.Create))
                    {
                        using (BinaryWriter bw = new BinaryWriter(fs))
                        {
                            byte[] data = Convert.FromBase64String(base64Std);
                            bw.Write(data);
                            bw.Close();
                        }
                    }
                    // ReSharper disable once AssignNullToNotNullAttribute
                    var attech = new Attachment(Server.MapPath(_filePath));
                    mail.Attachments.Add(attech);
                }

                smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
                smtp.Port = 587;
                smtp.Credentials = new System.Net.NetworkCredential("donotreply@eam.co.in", "reNply_33@9D");//from id
                //Or your Smtp Email ID and Password
                smtp.EnableSsl = true;
                try
                {
                    smtp.Send(mail);
                    flag = true;
                }
                catch (Exception)
                {
                    // ignored
                }

                Thread.Yield();
            }

            return flag;
        }
        public string SendFeesSms(string message,string mobileNo)
        {
            var smsResponse = "";

            if (mobileNo != string.Empty)
            {
                mobileNo = mobileNo.Replace("\n", "");
                _sql = "Select HitValue from SMSActivateDeactivate where BranchCode=" + Session["BranchCode"] + "";
                if (BAL.objBal.ReturnTag(_sql, "HitValue") != "")
                {
                    if (BAL.objBal.ReturnTag(_sql, "HitValue") == "true")
                    {
                        var sadpNew = new SMSAdapterNew();
                        smsResponse = sadpNew.Send(message, mobileNo, "");
                    }
                    else
                    {
                        smsResponse = "Go to Sms Setting and Activate Panel!";
                    }
                }

                Thread.Yield();
            }

            return smsResponse;
        }

        protected void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            var chkAll = (CheckBox)sender;
            var flag = chkAll.Checked;
            var emails = "";
            var contactno = "";

            foreach (RepeaterItem ri in rpt2.Items)
            {
                var chk = (CheckBox)ri.FindControl("chk");
                chk.Checked = flag;

                if (flag)
                {
                    var lblFamilyEmail = (Label)ri.FindControl("lblFamilyEmail");
                    var lblFamilyContactNo = (Label)ri.FindControl("lblFamilyContactNo");

                    var isEmail = Regex.IsMatch(lblFamilyEmail.Text.Trim(), @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);


                    if (isEmail)
                    {
                        if (emails == string.Empty)
                        {
                            emails = lblFamilyEmail.Text.Trim();
                        }
                        else
                        {
                            emails = emails + "," + Environment.NewLine + lblFamilyEmail.Text.Trim();
                        }
                    }

                    var isMobileno = Regex.IsMatch(lblFamilyContactNo.Text.Trim(), @"^(?:(?:\+|0{0,2})91(\s*[\-]\s*)?|[0]?)?[789]\d{9}$");

                    if (isMobileno)
                    {
                        if (contactno == string.Empty)
                        {
                            contactno = lblFamilyContactNo.Text.Trim();
                        }
                        else
                        {
                            contactno = contactno + "," + Environment.NewLine + lblFamilyContactNo.Text.Trim();
                        }
                    }
                }
            }

            txtToEmail.Text = emails;

            txtMobileno.Text = contactno;

            ScriptManager.RegisterStartupScript(drpClass, GetType(), "summernote", "txtAreaHtml()", true);
        }
        protected void drpClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(drpClass, GetType(), "summernote", "txtAreaHtml()", true);

            LoadSection(drpSection, drpClass);
            LoadBranch(drpBranch, drpClass);

            // ReSharper disable once RedundantAssignment
            var dt = new DataTable();
            dt = GetStudentsRecordinGrid();

            rpt2.DataSource = dt;
            rpt2.DataBind();
        }
        protected void chk_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chknew = (CheckBox)sender;

            string emails = "";
            string contactno = "";

            foreach (RepeaterItem ri in rpt2.Items)
            {
                CheckBox chk = (CheckBox)ri.FindControl("chk");
                if (chk.Checked)
                {
                    Label lblFamilyEmail = (Label)ri.FindControl("lblFamilyEmail");
                    Label lblFamilyContactNo = (Label)ri.FindControl("lblFamilyContactNo");

                    bool isEmail = Regex.IsMatch(lblFamilyEmail.Text.Trim(), @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);

                    if (isEmail)
                    {
                        if (emails == string.Empty)
                        {
                            emails = lblFamilyEmail.Text.Trim();
                        }
                        else
                        {
                            emails = emails + "," + Environment.NewLine + lblFamilyEmail.Text.Trim();
                        }
                    }

                    bool isMobileno = Regex.IsMatch(lblFamilyContactNo.Text.Trim(), @"^(?:(?:\+|0{0,2})91(\s*[\-]\s*)?|[0]?)?[789]\d{9}$");

                    if (isMobileno)
                    {
                        if (contactno == string.Empty)
                        {
                            contactno = lblFamilyContactNo.Text.Trim();
                        }
                        else
                        {
                            contactno = contactno + "," + Environment.NewLine + lblFamilyContactNo.Text.Trim();
                        }
                    }
                }
            }

            txtToEmail.Text = emails;

            txtMobileno.Text = contactno;

            ScriptManager.RegisterStartupScript(chknew, GetType(), "summernote", "txtAreaHtml();noteeditableReply();", true);
        }
        protected void drpSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ReSharper disable once RedundantAssignment
            var dt = new DataTable();
            dt = GetStudentsRecordinGrid();

            rpt2.DataSource = dt;
            rpt2.DataBind();

            ScriptManager.RegisterStartupScript(drpClass, GetType(), "summernote", "txtAreaHtml()", true);
        }
        protected void drpBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ReSharper disable once JoinDeclarationAndInitializer
            DataTable dt;
            dt = GetStudentsRecordinGrid();

            rpt2.DataSource = dt;
            rpt2.DataBind();

            ScriptManager.RegisterStartupScript(drpClass, GetType(), "summernote", "txtAreaHtml()", true);
        }

        public void LoadMyConversation()
        {
            var param = new List<SqlParameter>();

            string msgsentby;

            msgsentby = Session["LoginName"].ToString();

            param.Add(new SqlParameter("@Sentby", msgsentby));
            param.Add(new SqlParameter("@isActive", "1"));
            param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
            param.Add(new SqlParameter("@LoginType", Session["LoginType"].ToString()));
            param.Add(new SqlParameter("@LoginName", Session["LoginName"].ToString()));
            rpt1.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("get_TeacherGuardian_Proc", param);
            rpt1.DataBind();

            LoadReplyConversation();
        }
        protected void lnkAttechment_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            Label lblDocPath = (Label)lnk.NamingContainer.FindControl("lblPath");

            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(lblDocPath.Text));
            Response.WriteFile(lblDocPath.Text);
            Response.End();
        }
        protected void lnkReply_Click(object sender, EventArgs e)
        {
            LinkButton lnkReply = (LinkButton)sender;
            Label lblid = (Label)lnkReply.NamingContainer.FindControl("lblid");
            Label lblid1 = (Label)lnkReply.NamingContainer.FindControl("Label2");

            HiddenField hfid = (HiddenField)lnkReply.NamingContainer.FindControl("hfid");
            HiddenField hfid1 = (HiddenField)lnkReply.NamingContainer.FindControl("hfid1");
            HtmlGenericControl divReply = (HtmlGenericControl)lnkReply.NamingContainer.FindControl("divReply");

            hfid.Value=lblid.Text.Trim();
            hfid1.Value = lblid1.Text.Trim();

            if (rblist1.SelectedIndex == 0)
            {
                divReply.Visible = false;
            }
            else
            {
                divReply.Visible = true;
            }

            ScriptManager.RegisterStartupScript(rblist1, GetType(), "summernote", "txtAreaHtml()", true);
        }
        protected void lnkSubmitReply_Click(object sender, EventArgs e)
        {
            LinkButton lnkSubmitReply = (LinkButton)sender;
            HiddenField hidFileReply = (HiddenField)lnkSubmitReply.NamingContainer.FindControl("hidFileReply");
            HiddenField hidFileExtReply = (HiddenField)lnkSubmitReply.NamingContainer.FindControl("hidFileExtReply");
            HiddenField hfid = (HiddenField)lnkSubmitReply.NamingContainer.FindControl("hfid");
            HiddenField hfid1 = (HiddenField)lnkSubmitReply.NamingContainer.FindControl("hfid1");
            TextBox txtReply = (TextBox)lnkSubmitReply.NamingContainer.FindControl("txtReply");
            HtmlGenericControl msgbox1 = (HtmlGenericControl)lnkSubmitReply.NamingContainer.FindControl("msgbox1");

            // ReSharper disable once RedundantAssignment
            var msg = "";

            var base64Std = hidFileReply.Value;
            if (base64Std != string.Empty)
            {
                // ReSharper disable once RedundantAssignment
                var fileName = "";
                var fileExtention = hidFileExtReply.Value;

                _filePath = @"../Uploads/Attachment/";
                string datetime = DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss");
                fileName = String.Format("{0}", datetime + "_Reply" + fileExtention);
                _filePath = _filePath + fileName;
                // ReSharper disable once AssignNullToNotNullAttribute
                using (FileStream fs = new FileStream(Server.MapPath(_filePath), FileMode.Create))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs))
                    {
                        var data = Convert.FromBase64String(base64Std);
                        bw.Write(data);
                        bw.Close();
                    }
                }
            }

            msg = InsertReply(hfid1.Value, hfid.Value, Session["LoginName"].ToString(), txtReply.Text.Trim(), _filePath, Session["LoginTypeId"].ToString());

            if (msg == "S")
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox1, "Sent", "S");
                LoadMyConversation();
            }
            else
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox1, "Sorry", "W");
            }

            ScriptManager.RegisterStartupScript(rblist1, GetType(), "summernote", "txtAreaHtml()", true);
        }

        protected string InsertReply(string subjectId2, string subjectId, string userid, string emaildiscription, string attechment, string msgsentby)
        {
            // ReSharper disable once RedundantAssignment
            var msg = "";

            var param = new List<SqlParameter>();
            param.Add(new SqlParameter("@ConversationID", subjectId2));
            param.Add(new SqlParameter("@SubjectId", subjectId));
            param.Add(new SqlParameter("@UserId", userid));
            param.Add(new SqlParameter("@Discription", emaildiscription));
            param.Add(new SqlParameter("@AttechmentPath", attechment));
            param.Add(new SqlParameter("@Msgsentby", msgsentby));
            param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
            param.Add(new SqlParameter("@LoginName", Session["LoginName"].ToString()));

            var para = new SqlParameter("@Msg", "")
            {
                Direction = ParameterDirection.Output,
                Size = 0x100
            };

            param.Add(para);

            msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("TeacherGuardianConversationReply_Proc", param);

            return msg;
        }
        protected void lnkReplyAttechment_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            Label lblReplyAttechmentPath = (Label)lnk.NamingContainer.FindControl("lblReplyAttechmentPath");

            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(lblReplyAttechmentPath.Text));
            Response.WriteFile(lblReplyAttechmentPath.Text);
            Response.End();
        }

        public void LoadReplyConversation()
        {
            foreach (RepeaterItem ri in rpt1.Items)
            {
                var rptReply = (Repeater)ri.FindControl("rptReply");
                var lblid = (Label)ri.FindControl("lblid");

                var param = new List<SqlParameter>();

                param.Add(new SqlParameter("@Subjectid", lblid.Text.Trim()));
                param.Add(new SqlParameter("@isActive", "1"));

                rptReply.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("get_ConversationReply_Proc", param);
                rptReply.DataBind();
            }
        }
        protected void lnkcattaReply_Click(object sender, EventArgs e)
        {
            var lnkcattaReply = (LinkButton)sender;
            var hidFileReply = (HiddenField)lnkcattaReply.NamingContainer.FindControl("hidFileReply");
            var hidFileExtReply = (HiddenField)lnkcattaReply.NamingContainer.FindControl("hidFileExtReply");

            hidFileReply.Value = string.Empty;
            hidFileExtReply.Value = string.Empty;
        }
   
        protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ReSharper disable once RedundantBoolCompare
            if (CheckBoxList1.Items.FindByValue("CT").Selected == true)
            {
                for (var i = 0; i < rpt2.Items.Count; i++)
                {
                    var chk = (CheckBox)rpt2.Items[i].FindControl("chk");
                    chk.Checked = true;
                }
            }
            else
            {
                for (var i = 0; i < rpt2.Items.Count; i++)
                {
                    var chk = (CheckBox)rpt2.Items[i].FindControl("chk");
                    chk.Checked = false;
                }
            }
        }

        protected void chkAll2_CheckedChanged(object sender, EventArgs e)
        {
            // ReSharper disable once RedundantBoolCompare
            if (CheckBoxList1.Items.FindByValue("CT").Selected == true && chkAll2.Checked)
            {
                for (var i = 0; i < rpt2.Items.Count; i++)
                {
                    var chk = (CheckBox)rpt2.Items[i].FindControl("chk");
                    chk.Checked = true;
                }
            }
            else
            {
                for (var i = 0; i < rpt2.Items.Count; i++)
                {
                    var chk = (CheckBox)rpt2.Items[i].FindControl("chk");
                    chk.Checked = false;
                }
            }
        }

        public override void Dispose()
        {
            _con.Dispose();
            _oo.Dispose();
            _dtconv.Dispose();
        }
    }
}