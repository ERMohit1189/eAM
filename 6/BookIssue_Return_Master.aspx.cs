using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _6
{
    public partial class AdminBookIssueReturnMaster : Page
    {
        private List<SqlParameter> _param = new List<SqlParameter>();
        readonly Campus _oo;
        string _sql = "";

        public AdminBookIssueReturnMaster()
        {
            _oo = new Campus();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
            {
                Response.Redirect("default.aspx");
            }
            Campus camp = new Campus(); camp.LoadLoader(loader);
            if (IsPostBack) return;
            Session["LibRecieptNo"] = "";
            BAL.objBal.AddDateMonthYearDropDown(DDYear, DDMonth, DDDate);
            BAL.objBal.FindCurrentDateandSetinDropDown(DDYear, DDMonth, DDDate);

            txtSearchStudent.Attributes.Add("onkeypress", "button_click(this,'" + lnkview.ClientID + "');");
            txtSearchStudent.Attributes.Add("onblur", "button_click_onblur(this,'" + lnkview.ClientID + "')");

            //txtAccessionNo.Attributes.Add("onkeypress", "button_click(this,'" + lnkaccessionview.ClientID + "');");
            //txtAccessionNo.Attributes.Add("onkeydown", "button_click_ontabpress(this,'" + lnkaccessionview.ClientID + "');");
        }

        protected void DDYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            BAL.objBal.YearDropDown(DDYear, DDMonth, DDDate);
        }

        protected void DDMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            BAL.objBal.MonthDropDown(DDYear, DDMonth, DDDate);
        }

        public void LoadHistory()
        {
            var studentId = "";
            if (rdotype.SelectedValue == "Student")
            {
                studentId = Request.Form[hfStudentId.UniqueID];
                if (studentId == string.Empty)
                {
                    studentId = txtSearchStudent.Text.Trim(); 
                }
            }
            else
            {
                studentId = Request.Form[hfStaffId.UniqueID];
                if (studentId == string.Empty)
                {
                    studentId = txtSearchStaff.Text.Trim();
                }
            }

            _param = new List<SqlParameter>
            {
                new SqlParameter("@SRNO", studentId),
                new SqlParameter("@BookIssueFor", rdotype.SelectedValue),
                new SqlParameter("@SessionName", Session["SessionName"].ToString()),
                new SqlParameter("@BranchCode", Session["BranchCode"].ToString())
            };

            var ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("Get_LIBHISTORY", _param);

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    var dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        grdHistory.DataSource = ds;
                        grdHistory.DataBind();
                        div6.Visible = true;
                    }
                    else
                    {
                        grdHistory.DataSource = null;
                        grdHistory.DataBind();
                        div6.Visible = false;
                    }
                }
                else
                {
                    grdHistory.DataSource = null;
                    grdHistory.DataBind();
                    div6.Visible = false;
                }
            }

            else
            {
                grdHistory.DataSource = null;
                grdHistory.DataBind();
                div6.Visible = false;
            }
        }

        protected void rdotype_SelectedIndexChanged(object sender, EventArgs e)
        {
            GrdEmp.DataSource = null;
            GrdEmp.DataBind();
            Grd.DataSource = null;
            Grd.DataBind();
            grdshow.Visible = false;
            grdshow2.Visible = false;
            div2.Visible = false;
            txtSearchStudent.Text = "";
            hfStudentId.Value = "";
            txtSearchStaff.Text = "";
            hfStaffId.Value = "";
            if (rdotype.SelectedValue == "Student")
            {
                divStudent.Visible = true;
                divStaff.Visible = false;
            }
            else
            {
                divStudent.Visible = false;
                divStaff.Visible = true;
            }
        }

        protected void txtSearchStudent_TextChanged(object sender, EventArgs e)
        {
            var studentId = Request.Form[hfStudentId.UniqueID];
            if (string.IsNullOrEmpty(studentId))
            {
                studentId = txtSearchStudent.Text.Trim();
            }
            BAL.objBal.AddDateMonthYearDropDown(DDYear, DDMonth, DDDate);
            BAL.objBal.FindCurrentDateandSetinDropDown(DDYear, DDMonth, DDDate);
            LoadStudentInfo();
            LoadHistory();
        }

        protected void txtSearchStaff_TextChanged(object sender, EventArgs e)
        {
            
            var StaffId = Request.Form[hfStaffId.UniqueID];
            if (string.IsNullOrEmpty(StaffId))
            {
                StaffId = txtSearchStaff.Text.Trim();
            }
            LoadEmployeeInfo();
            LoadHistory();
        }
        protected void lnkview_Click(object sender, EventArgs e)
        {
            BAL.objBal.AddDateMonthYearDropDown(DDYear, DDMonth, DDDate);
            BAL.objBal.FindCurrentDateandSetinDropDown(DDYear, DDMonth, DDDate);
            if (rdotype.SelectedValue == "Student")
            {
                LoadStudentInfo();
            }
            else
            {
                LoadEmployeeInfo();
            }
            LoadHistory();
        }

        protected void LoadEmployeeInfo()
        {
            GrdEmp.DataSource = null;
            GrdEmp.DataBind();
            string sql = "";
            var StaffId = Request.Form[hfStaffId.UniqueID];
            if (string.IsNullOrEmpty(StaffId))
            {
                StaffId = txtSearchStaff.Text.Trim();
            }
            sql = "Select eod.EmpId EmpId,eod.Ecode Ecode,egd.EFirstName+' '+egd.EMiddleName+' '+egd.ELastName as EmpName,egd.EFatherName FatherName,eod.Designation Designation from EmpployeeOfficialDetails eod ";
            sql = sql + " inner join EmpGeneralDetail egd on eod.Ecode=egd.Ecode and eod.EmpId=egd.EmpId where eod.Withdrwal is null ";
            sql = sql + " and (eod.Ecode='" + StaffId.Trim() + "' or eod.EmpId='" + StaffId.Trim() + "') and eod.BranchCode=" + Session["BranchCode"].ToString() + " and egd.BranchCode=" + Session["BranchCode"].ToString() + "";
            var dt = _oo.Fetchdata(sql);
            if (dt.Rows.Count > 0)
            {
                grdshow.Visible = false;
                grdshow2.Visible = true;
                GrdEmp.DataSource = dt;
                GrdEmp.DataBind();
            }
            Clear();
            if (GrdEmp.Rows.Count > 0)
            {
                div2.Visible = true;
                LoadGrid();
            }
            else
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sorry, No Record(s) found!", "W");
                div2.Visible = true;
                txtSearchStaff.Text = string.Empty;
            }
        }

        public void LoadStudentInfo()
        {
            Grd.DataSource = null;
            Grd.DataBind();
            string studentId = Request.Form[hfStudentId.UniqueID];
            if (studentId == string.Empty)
            {
                studentId = txtSearchStudent.Text.Trim();
            }

            _sql = "select ROW_NUMBER() over(order by SrNo ) as SNo, SrNo,StEnRCode,Name,FatherName, CombineClassName,ClassName,SectionName,Medium,Card,Convert(varchar(11),DateOfAdmiission) as DateOfAdmiission,CourseName,BranchName,FamilyContactNo,PhotoPath from AllStudentRecord_UDF ('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") where Case When Left('" + studentId + "',3)='eAM' Then StEnRCode Else SrNo End ='" + studentId + "' and Withdrwal is null";
            Grd.DataSource = BAL.objBal.GridFill(_sql);
            Grd.DataBind();
            DataSet ds;
            ds = _oo.GridFill(_sql);
            // ReSharper disable once UseNullPropagation
            if (ds != null && Grd.Rows.Count > 0)
            {
                grdshow.Visible = true;
                grdshow2.Visible = false;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    img.ImageUrl = ds.Tables[0].Rows[0]["PhotoPath"].ToString() != string.Empty ? ds.Tables[0].Rows[0]["PhotoPath"] + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture) : "../img/user-pic/user-pic.jpg" + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                    studentImg.NavigateUrl = ds.Tables[0].Rows[0]["PhotoPath"].ToString() != string.Empty ? ds.Tables[0].Rows[0]["PhotoPath"] + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture) : "../img/user-pic/user-pic.jpg" + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                    hylinkmoredetails.NavigateUrl = "../11/StudentRegView.aspx?print=1&id=" + ds.Tables[0].Rows[0]["StEnRCode"];
                }
            }
            Clear();
            if (Grd.Rows.Count > 0)
            {
                div2.Visible = true;
                LoadGrid();
            }
            else
            {
                //BAL.objBal.MessageBoxforUpdatePanel("", lnkview);
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sorry, No Record(s) found!", "W");
                div2.Visible = true;
                txtSearchStudent.Text = string.Empty;
            }
        }

        public void Clear()
        {
            txtAccessionNo.Text = string.Empty;
            grdaccsnogrd.DataSource = null;
            grdaccsnogrd.DataBind();
            grdIssued.DataSource = null;
            grdIssued.DataBind();
            div3.Visible = false;
            div4.Visible = false;
            div5.Visible = false;
            div7.Visible = false;
        }

        private void Loadbookbyacc(GridView grd,string accno)
        {
            var studentId = "";
            if (rdotype.SelectedValue == "Student")
            {
                studentId = Request.Form[hfStudentId.UniqueID];
                if (studentId == string.Empty)
                {
                    studentId = txtSearchStudent.Text.Trim();
                }
            }
            else
            {
                studentId = Request.Form[hfStaffId.UniqueID];
                if (studentId == string.Empty)
                {
                    studentId = txtSearchStaff.Text.Trim();
                }
            }
            _param = new List<SqlParameter>
            {
                new SqlParameter("@srno", studentId),
                new SqlParameter("@rulesFor", rdotype.SelectedItem.Text.Trim()),
                new SqlParameter("@AccessionNo", accno),
                new SqlParameter("@SessionName", Session["SessionName"].ToString()),
                new SqlParameter("@BranchCode", Session["BranchCode"].ToString())
            };

            if (accno != string.Empty)
            {                var ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("GetLibraryItemNotIssued", _param);
                //SelectedDatesCollection 
                //if (ds.Tables[0].Rows[0]["maxbookallowed"].ToString()=="")
                //{

                //}
                grd.DataSource=ds; 
                grd.DataBind();
                // ReSharper disable once UseNullPropagation
                if(ds!=null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        var dt = ds.Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            hfmaxbookallowed.Value = dt.Rows[0]["maxbookallowed"].ToString();
                        }
                    }              
                }
            }
            else
            {
                grd.DataSource = null;
                grd.DataBind();
            }

            foreach (GridViewRow gvr in grdaccsnogrd.Rows)
            {
                var lbltitle = (Label)gvr.FindControl("lbltitle");
                var isissued = (Label)gvr.FindControl("lblisissued");
                var chkbox = (CheckBox)gvr.FindControl("chkbox");
                if (lbltitle.Text == "No Record found!" || isissued.Text == "Yes")
                {
                    chkbox.Visible = false;
                }
            }
        }

        protected void lnkaccessionview_Click(object sender, EventArgs e)
        {
            string notexistsAccessionNo = "";
            if (txtAccessionNo.Text.Trim() != "")
            {
                string existsAccessionNo = ""; 
                string[] data = txtAccessionNo.Text.Trim().Split(new string[] { "," }, StringSplitOptions.None);
                for (int i = 0; i < data.Length; i++)
                {
                    
                    string sql = "select count(1) cnt from BookIssueReturn where  isreturn=0 and AccessionNo='" + data[i].Trim() + "' and Branchcode=" + Session["BranchCode"] + "";
                    if (_oo.ReturnTag(sql, "cnt") != "0")
                    {
                        existsAccessionNo = existsAccessionNo + data[i] + " ";
                    }
                    else
                    {
                        notexistsAccessionNo = notexistsAccessionNo + data[i] + ",";
                    }
                }
                if (existsAccessionNo.Trim() != "")
                {
                    txtAccessionNo.Text = notexistsAccessionNo;
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sorry, Accession No.(s) (" + existsAccessionNo + ") are already issued.", "W");
                    return;
                }
                else
                {

                    GetBookByAcc(grdaccsnogrd, txtAccessionNo.Text.Trim());
                    //Loadfinalrecord();
                    GetBookDetails();
                    if (grdaccsnogrd.Rows.Count == 0)
                    {
                        Campus camp = new Campus(); camp.msgbox(lnkaccessionview, msgbox, "Sorry, No Record(s) found!", "W");
                    }
                    ScriptManager.RegisterClientScriptBlock(Page, GetType(), "SetCursorToTextEndandSepratebyComma", "SetCursorToTextEndandSepratebyComma('" + txtAccessionNo.ClientID + "')", true);
                }
            }
        }
        

   
        protected void chkbox_CheckedChanged(object sender, EventArgs e)
        {      
            if (Convert.ToInt16(hfmaxbookallowed.Value) >=0)
            {
                CheckBox chk = (CheckBox)sender;
                if (chk.Checked)
                {
                    int plusone=0;
                    Label lblaccessinno = (Label)chk.NamingContainer.FindControl("lblaccessinno");
                    string studentId = Request.Form[hfStudentId.UniqueID];
                    if (studentId == string.Empty)
                    {
                        studentId = txtSearchStudent.Text.Trim();
                    }
                    string srno = studentId;
                    string accessionno = lblaccessinno.Text;
                    if (IsReissueOver(srno, accessionno) == true)
                    {
                        foreach (GridViewRow gvr in grdaccsnogrd.Rows)
                        {
                            CheckBox chk1 = (CheckBox)gvr.FindControl("chkbox");
                            if (chk1.Checked)
                            {
                                if (plusone == 0)
                                {
                                    plusone = IsOvermaximumlimtatatime(srno) + 1;
                                }
                                else
                                {
                                    plusone = plusone + 1;
                                }
                            }
                        }
                        if (Convert.ToInt16(hfmaxbookallowed.Value) >= plusone)
                        {
                            Loadfinalrecord();
                        }
                        else
                        {
                            chk.Checked = false;
                            Campus camp = new Campus(); camp.msgbox(chk, msgbox, "Sorry, maximum " + hfmaxbookallowed.Value + " books can issue at a S.R. NO.!", "A");
                        }
                    }
                    else
                    {
                        chk.Checked = false;
                        Campus camp = new Campus(); camp.msgbox(chk, msgbox, "Sorry, Reissue of this book is over at this S.R. NO. for this month!", "A");
                    }
                }
                else
                {
                    Loadfinalrecord();
                }
            }       
        }
        public int IsOvermaximumlimtatatime(string srno)
        {
            _param = new List<SqlParameter> {new SqlParameter("@srno", srno), new SqlParameter("@BranchCode", Session["BranchCode"].ToString()) };
            int limit = Convert.ToInt16(DLL.objDll.Sp_SelectRecord_usingExecuteScalar("Getmaxbookatatime", _param));
            return limit;
        }

        public bool IsReissueOver(string srno, string accessionno)
        {
            bool flag = false;
            _param = new List<SqlParameter>
            {
                new SqlParameter("@srno", srno),
                new SqlParameter("@accessionno", accessionno),
                new SqlParameter("@BranchCode", Session["BranchCode"].ToString()),
                new SqlParameter("@SessionName", Session["SessionName"].ToString())
            };
            var ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("GetmaxbookinamonthProc", _param);
            if (ds.Tables[0].Rows[0]["maxlimitendofabookinamonth"].ToString()== "Yes")
            {
                flag = true;
            }
            return flag;
        }
        public void Loadfinalrecord()
        {
            var str = "";
            foreach (GridViewRow gvr in grdaccsnogrd.Rows)
            {
                var lblaccno = (Label)gvr.FindControl("lblaccessinno");
                var chkbox = (CheckBox)gvr.FindControl("chkbox");
                if (!chkbox.Checked) continue;
                if (str == string.Empty)
                {
                    str = lblaccno.Text;
                }
                else
                {
                    str = str + ',' + lblaccno.Text;
                }
            }
            Loadbookbyacc(grdIssued, str);
            if (grdIssued.Rows.Count > 0)
            {
                lnkIssue.Visible = true;
                div5.Visible = true;
    
            
            }
            else
            {
                div5.Visible = false;

                lnkIssue.Visible = grdReturn.Rows.Count > 0;
            }
        }
        protected void lnkIssue_Click(object sender, EventArgs e)
        {
            InsertRecord();
            UpdateRecord();
            Clear();
            LoadGrid();
            
        }
        public void GenrateFinereceipt()
        {
            if (grdReturn.Rows.Count <= 0) return;
            foreach (GridViewRow gvr in grdReturn.Rows)
            {
                _param = new List<SqlParameter>();
                var lblBiRid = (Label)gvr.FindControl("lblId");
                var lblFine = (Label)gvr.FindControl("lblFine");
                var drpCondition = (DropDownList)gvr.FindControl("drpCondition");
                var txtDamageAmount = (TextBox)gvr.FindControl("txtDamageAmount");
                var chkReturn = (CheckBox)gvr.FindControl("chkReturn");
                if (!chkReturn.Checked) continue;
                if (lblFine.Text == "0" && (drpCondition.SelectedIndex == 0 || txtDamageAmount.Text == "0" ||
                                            txtDamageAmount.Text == string.Empty)) continue;
                _param.Add(new SqlParameter("@Queryfor", "I"));
                _param.Add(new SqlParameter("@BIRid", lblBiRid.Text));
                if (lblFine.Text != "0")
                {
                    _param.Add(new SqlParameter("@FineAmount", lblFine.Text.Trim()));
                }
                if (drpCondition.SelectedIndex != 0)
                {
                    _param.Add(new SqlParameter("@DamaageAmount", txtDamageAmount.Text.Trim()));
                }
                _param.Add(new SqlParameter("@prifix", "LB/" + Session["SessionName"] + "/"));
                _param.Add(new SqlParameter("@tableName", "ReturnBookFine"));
                _param.Add(gvr.RowIndex == 0
                    ? new SqlParameter("@increementby", "1")
                    : new SqlParameter("@increementby", "0"));
                _param.Add(new SqlParameter("@sessionname", Session["SessionName"].ToString()));
                _param.Add(new SqlParameter("@branchcode", Session["BranchCode"].ToString()));
                _param.Add(new SqlParameter("@loginname", Session["LoginName"].ToString()));

                var para = new SqlParameter("@msg", "")
                {
                    Direction = ParameterDirection.Output,
                    Size = 0x100
                };
                _param.Add(para);

                DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("ReturnBookFineproc", _param);
            }
        }

        private void UpdateRecord()
        {
            var flag = 0;
            var msg = "";
            var receiptno = "";
            var rowcount = 0;
            int sts = 0; int sts2 = 0;
            foreach (GridViewRow gvr in grdReturn.Rows)
            {

                _param = new List<SqlParameter>();
                var lblaccno = (Label)gvr.FindControl("lblaccessinno");
                var studentId = "";
                if (rdotype.SelectedValue == "Student")
                {
                    studentId = Request.Form[hfStudentId.UniqueID];
                    if (studentId == string.Empty)
                    {
                        studentId = txtSearchStudent.Text.Trim();
                    }
                }
                else
                {
                    studentId = Request.Form[hfStaffId.UniqueID];
                    if (studentId == string.Empty)
                    {
                        studentId = txtSearchStaff.Text.Trim();
                    }
                }
                //comment
                var lblFine = (Label)gvr.FindControl("lblFine");
                var drpCondition = (DropDownList)gvr.FindControl("drpCondition");
                var chkReturn = (CheckBox)gvr.FindControl("chkReturn");

                var lblBiRid = (Label)gvr.FindControl("lblId");
                var txtDamageAmount = (TextBox)gvr.FindControl("txtDamageAmount");

                if (chkReturn.Checked)
                {
                    sts = sts + 1;
                    flag = 1;
                    _param.Add(new SqlParameter("@Queryfor", "U"));
                    _param.Add(new SqlParameter("@srno", studentId));
                    _param.Add(new SqlParameter("@accessionno", lblaccno.Text.Trim()));
                    if (lblFine.Text != "0")
                    {
                        _param.Add(new SqlParameter("@latefine", "Yes"));
                    }
                    if (drpCondition.SelectedIndex != 0)
                    {
                        _param.Add(new SqlParameter("@damageid", drpCondition.SelectedValue));
                    }
                    _param.Add(new SqlParameter("@BIRid", lblBiRid.Text.Trim()));

                    if (lblFine.Text != "0")
                    {
                        _param.Add(new SqlParameter("@FineAmount", lblFine.Text.Trim()));
                    }
                    if (drpCondition.SelectedIndex != 0)
                    {
                        _param.Add(new SqlParameter("@DamaageAmount", txtDamageAmount.Text.Trim()));
                    }
                    _param.Add(new SqlParameter("@prifix", "LB/" + Session["SessionName"] + "/"));
                    _param.Add(new SqlParameter("@tableName", "ReturnBookFine"));
                    if (rowcount == 0)
                    {
                        _param.Add(new SqlParameter("@increementby", "1"));
                    }
                    else
                    {
                        _param.Add(new SqlParameter("@increementby", "0"));
                    }
                    _param.Add(new SqlParameter("@sessionname", Session["SessionName"].ToString()));
                    _param.Add(new SqlParameter("@branchcode", Session["BranchCode"].ToString()));
                    _param.Add(new SqlParameter("@loginname", Session["LoginName"].ToString()));
                    var date = DDYear.SelectedItem.Text + "/" + DDMonth.SelectedItem.Text + "/" + DDDate.SelectedItem.Text+" "+ DateTime.Now.ToString("hh:mm tt");
                    _param.Add(new SqlParameter("@returndate", date));

                    if (lblFine.Text != "0" || (drpCondition.SelectedIndex != 0 && txtDamageAmount.Text != "0" && txtDamageAmount.Text != string.Empty))
                    {
                        _param.Add(new SqlParameter("@genrateReceipt", "Yes"));
                        rowcount = 1;
                    }
                    else
                    {
                        _param.Add(new SqlParameter("@genrateReceipt", "No"));
                    }
                    if (txtConcession.Text != string.Empty)
                    {
                        _param.Add(new SqlParameter("@Concession", txtConcession.Text.Trim()));
                    }
                    _param.Add(new SqlParameter("@BookIssueFor", rdotype.SelectedValue));
                    _param.Add(new SqlParameter("@Receiptno", _oo.FindRecieptNo()));
                    _param.Add(new SqlParameter("@MOD", DropDownMOD.SelectedValue));
                    if (DropDownMOD.SelectedValue == "Cheque")
                    {
                        _param.Add(new SqlParameter("@Status", ddlChequeStatus.SelectedValue));
                    }
                    else
                    {
                        _param.Add(new SqlParameter("@Status", "Paid"));
                    }
                    _param.Add(new SqlParameter("@Remark", txtRemark.Text.Trim()));
                    var para = new SqlParameter("@msg", "");
                    para.Direction = ParameterDirection.Output;
                    para.Size = 0x100;
                    _param.Add(para);
               
                    msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("BookIssueReturnProc", _param);
                    
                    if (rowcount == 1 & receiptno==string.Empty)
                    {
                        receiptno = msg;
                    }
              
                }
            }
            if (sts == 0)
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox2, "Please check atleast onecheckbox!", "W");
                return;
            }
            else if (sts == 1)
            {
                if (msg == string.Empty)
                {
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sorry, Book not return!", "W");
                }
                else
                {
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "U");
                }
            }
            
            if (receiptno == string.Empty) return;
            Session["LibRecieptNo"] = receiptno;
            ScriptManager.RegisterStartupScript(this, GetType(), "redirect", "window.location='" + BLL.GetSiteRoot() + "/6/LibraryReceipet.aspx?print=1';", true);
        }

        private void InsertRecord()
        {
            try
            {
                if (grdIssued.Rows.Count <= 0) return;
                var msg = ""; int sts = 0; int sts2 = 0;
                foreach (GridViewRow gvr in grdIssued.Rows)
                {
                    //CheckBox chkReturn = (CheckBox)gvr.FindControl("chkReturn");

                    //if (chkReturn.Checked)
                    //{
                        sts = sts + 1;

                        _param = new List<SqlParameter>();
                        var lblaccno = (Label)gvr.FindControl("lblaccessinno");
                        var studentId = "";
                        if (rdotype.SelectedValue == "Student")
                        {
                            studentId = Request.Form[hfStudentId.UniqueID];
                            if (studentId == string.Empty)
                            {
                                studentId = txtSearchStudent.Text.Trim();
                            }
                        }
                        else
                        {
                            studentId = Request.Form[hfStaffId.UniqueID];
                            if (studentId == string.Empty)
                            {
                                studentId = txtSearchStaff.Text.Trim();
                            }
                        }
                        _param.Add(new SqlParameter("@Queryfor", "I"));
                        _param.Add(new SqlParameter("@srno", studentId));
                        _param.Add(new SqlParameter("@accessionno", lblaccno.Text.Trim()));
                        _param.Add(new SqlParameter("@sessionname", Session["SessionName"].ToString()));
                        _param.Add(new SqlParameter("@branchcode", Session["BranchCode"].ToString()));
                        _param.Add(new SqlParameter("@loginname", Session["LoginName"].ToString()));
                        _param.Add(new SqlParameter("@BookIssueFor", rdotype.SelectedValue));

                        var para = new SqlParameter("@msg", "")
                        {
                            Direction = ParameterDirection.Output,
                            Size = 0x100
                        };
                        _param.Add(para);

                        msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("BookIssueReturnProc", _param);
                        if (msg == "S")
                        {
                            sts2 = sts2 + 1;
                        }
                    //}
                }
                if (sts == 0)
                {
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox2, "Please check atleast onecheckbox!", "W");
                    return;
                }
                else
                {
                    if (sts2 == 0)
                    {
                        Campus camp = new Campus(); camp.msgbox(Page, msgbox2, "Sorry, Record not submitted!", "W");
                    }
                    else
                    {
                        Campus camp = new Campus(); camp.msgbox(Page, msgbox2, "Submitted successfully.", "U");
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void LoadGrid()
        {
            string studentId = ""; 
            if (rdotype.SelectedValue == "Student")
            {
                studentId = Request.Form[hfStudentId.UniqueID];
                if (studentId == string.Empty)
                {
                    studentId = txtSearchStudent.Text.Trim();
                }
            }
            else
            {
                studentId = Request.Form[hfStaffId.UniqueID];
                if (studentId == string.Empty)
                {
                    studentId = txtSearchStaff.Text.Trim();
                }
            }
            _param = new List<SqlParameter> {new SqlParameter("@QueryFor", "S"), new SqlParameter("@BookIssueFor", rdotype.SelectedValue), new SqlParameter("@srno", studentId), new SqlParameter("@branchcode", Session["BranchCode"].ToString())};

            var accessionno = DLL.objDll.Sp_SelectRecord_usingExecuteScalar("BookIssueReturnProc", _param).ToString();
            
            //Loadbookbyacc(grdReturn, accessionno);
            
            DataTable dt = new DataTable();
            dt= _oo.Fetchdata("EXEC GetLibraryItemIssued '" + studentId + "', '" + accessionno + "', " + rdotype.SelectedItem.Text.Trim() + ",'" + Session["SessionName"].ToString() + "', " + Session["BranchCode"].ToString());
            grdReturn.DataSource = dt;
            grdReturn.DataBind();

            if (grdReturn.Rows.Count > 0)
            {

                div4.Visible = true;
                div7.Visible = true;
                LoadBookCondition();
                var srno = studentId;

                if (Convert.ToInt16(hfmaxbookallowed.Value) > IsOvermaximumlimtatatime(srno))
                {
                    div3.Visible = true;
                }
                else
                {
                    grdIssued.DataSource = null;
                    grdIssued.DataBind();
                    div5.Visible = false;        
                    div3.Visible = false;
                }
                lnkIssue.Visible = true;
            }
            else
            {
                div4.Visible = false;
                div7.Visible = false;
                div3.Visible = true;
                lnkIssue.Visible = false;
            }
   
        }
        protected void DropDownMOD_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownMOD.SelectedIndex == 0)
            {
                table1.Visible = false;
                table2.Visible = false;
                table12.Visible = false;
            }
            else
            {
                table1.Visible = true;
                table2.Visible = true;
                table12.Visible = true;
                if (DropDownMOD.SelectedValue == "Cheque" || DropDownMOD.SelectedValue == "DD")
                {
                    Label42.Text = "Instrument No.";
                    Label4.Text = "Instrument Date";
                    Label43.Text = "Issuer";
                }
                if (DropDownMOD.SelectedValue == "Card")
                {
                    Label42.Text = "Card No.";
                    Label4.Text = "Card Date";
                    Label43.Text = "Issuer";
                }
                if (DropDownMOD.SelectedValue == "Online Transfer" || DropDownMOD.SelectedValue == "Other")
                {
                    Label42.Text = "Ref. No.";
                    Label4.Text = "Ref. Date";
                    Label43.Text = "Issuer";
                }
                if (DropDownMOD.SelectedValue == "Other")
                {
                    Label43.Text = "Reference Name";
                }
                if (DropDownMOD.SelectedValue == "Cheque" || DropDownMOD.SelectedValue == "Other")
                {
                    table4.Visible = true;
                }
                else
                {
                    table4.Visible = false;
                }
            }
        }
        public void LoadBookCondition()
        {
            int fyn = 0;
            var studentId = Request.Form[hfStudentId.UniqueID];
            if (studentId == string.Empty)
            {
                studentId = txtSearchStudent.Text.Trim();
            }
            foreach (GridViewRow gvr in grdReturn.Rows)
            {
                _param = new List<SqlParameter>
                {
                    new SqlParameter("@QueryFor", "S"),
                    new SqlParameter("@SessionName", Session["SessionName"].ToString()),
                    new SqlParameter("@branchcode", Session["BranchCode"].ToString())
                };

                var drpCondition = (DropDownList)gvr.FindControl("drpCondition");
                drpCondition.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("DamageCategoryMasterProc", _param);
                drpCondition.DataTextField = "DamageCategory";
                drpCondition.DataValueField = "id";
                drpCondition.DataBind();

                drpCondition.Items.Insert(0, new ListItem("Good", "-1"));


                var lblaccessinno = (Label)gvr.FindControl("lblaccessinno");
                var lblFine = (Label)gvr.FindControl("lblFine");

                lblFine.Text = LoadFine(studentId, lblaccessinno.Text);
                fyn += Convert.ToInt32(lblFine.Text);
            }
            //if (fyn == 0) { Label41.Visible = false; DropDownMOD.Visible = false; }
            //else { Label41.Visible = true; DropDownMOD.Visible = true; }
        }

        public string LoadFine(string srno,string accno)
        {
            var date = DDYear.SelectedItem.Text + "/" + DDMonth.SelectedItem.Text + "/" + DDDate.SelectedItem.Text;
            _param = new List<SqlParameter>
            {
                new SqlParameter("@SRNO", srno),
                new SqlParameter("@ACCESSIONNO", accno),
                new SqlParameter("@date", date),
                new SqlParameter("@branchcode", Session["BranchCode"].ToString())
            };

            return DLL.objDll.Sp_SelectRecord_usingExecuteScalar("GETLIBFINEPROC", _param).ToString();
        }

        protected void drpCondition_SelectedIndexChanged(object sender, EventArgs e)
        {
            var drpCondition = (DropDownList)sender;
            var lblaccessinno = (Label)drpCondition.NamingContainer.FindControl("lblaccessinno");
            var txtDamageAmount = (TextBox)drpCondition.NamingContainer.FindControl("txtDamageAmount");
            if (drpCondition.SelectedIndex != 0)
            {
                txtDamageAmount.Visible = true;
                txtDamageAmount.Text = GetDamageAmount(drpCondition.SelectedValue, lblaccessinno.Text.Trim());
            }
            else
            {
                txtDamageAmount.Visible = false;
                txtDamageAmount.Text = string.Empty;
            }
        }

        public string GetDamageAmount(string damageid, string accno)
        {
            _param = new List<SqlParameter>
            {
                new SqlParameter("@Id", damageid),
                new SqlParameter("@AccessionNo", accno),
                new SqlParameter("@branchcode", Session["BranchCode"].ToString())
            };

            return DLL.objDll.Sp_SelectRecord_usingExecuteScalar("GETDamageAmountPROC", _param).ToString();
        }
        protected void lnkprint_Click(object sender, EventArgs e)
        {
            var lnk = (LinkButton)sender;
            var lblReceipetNo = (Label)lnk.NamingContainer.FindControl("Label2");

            Session["LibRecieptNo"] = lblReceipetNo.Text.Trim();
            Response.Redirect("LibraryReceipetDuplicate.aspx?print=1");
        }

        protected void DDDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadBookCondition();
        }

        public override void Dispose()
        {
            _oo.Dispose();
        }

        /* Hari Om Thapa Date : 20-Mar-21 */
        public void GetBookDetails()
        {
            var str = "";
            foreach (GridViewRow gvr in grdaccsnogrd.Rows)
            {
                var lblaccno = (Label)gvr.FindControl("lblaccessinno");
                var chkbox = (CheckBox)gvr.FindControl("chkbox");
                if (!chkbox.Checked) continue;
                if (str == string.Empty)
                {
                    str = lblaccno.Text;
                }
                else
                {
                    str = str + ',' + lblaccno.Text;
                }
            }
            GetBookByAcc(grdIssued, str);
            if (grdIssued.Rows.Count > 0)
            {
                lnkIssue.Visible = true;
                div5.Visible = true;


            }
            else
            {
                div5.Visible = false;

                lnkIssue.Visible = grdReturn.Rows.Count > 0;
            }
        }

        private void GetBookByAcc(GridView grd, string accno)
        {
            var studentId = "";
            if (rdotype.SelectedValue == "Student")
            {
                studentId = Request.Form[hfStudentId.UniqueID];
                if (studentId == string.Empty)
                {
                    studentId = txtSearchStudent.Text.Trim();
                }
            }
            else
            {
                studentId = Request.Form[hfStaffId.UniqueID];
                if (studentId == string.Empty)
                {
                    studentId = txtSearchStaff.Text.Trim();
                }
            }
            _param = new List<SqlParameter>
            {
                new SqlParameter("@srno", studentId),
                new SqlParameter("@rulesFor", rdotype.SelectedItem.Text.Trim()),
                new SqlParameter("@AccessionNo", accno),
                new SqlParameter("@SessionName", Session["SessionName"].ToString()),
                new SqlParameter("@BranchCode", Session["BranchCode"].ToString())
            };

            if (accno != string.Empty)
            {
                var ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("GetBookDetail", _param);
                //SelectedDatesCollection 
                //if (ds.Tables[0].Rows[0]["maxbookallowed"].ToString()=="")
                //{

                //}
                grd.DataSource = ds;
                grd.DataBind();
                // ReSharper disable once UseNullPropagation
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        var dt = ds.Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            hfmaxbookallowed.Value = dt.Rows[0]["maxbookallowed"].ToString();
                        }
                    }
                }
            }
            else
            {
                grd.DataSource = null;
                grd.DataBind();
            }

            foreach (GridViewRow gvr in grdaccsnogrd.Rows)
            {
                var lbltitle = (Label)gvr.FindControl("lbltitle");
                var isissued = (Label)gvr.FindControl("lblisissued");
                var chkbox = (CheckBox)gvr.FindControl("chkbox");
                if (lbltitle.Text == "No Record found!" || isissued.Text == "Yes")
                {
                    chkbox.Visible = false;
                }
            }
        }

    }
}