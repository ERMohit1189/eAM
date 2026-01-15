using c4SmsNew;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TCCollection : Page
{
    private SqlConnection _con;
    readonly Campus _oo;
    private string _sql = "";
    public TCCollection()
    {
        _con = new SqlConnection();
        _oo = new Campus();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        _con = _oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);

        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        txtSrNo.Focus();
        if (!IsPostBack)
        {
            _oo.AddDateMonthYearDropDown(DDYear, DDMonth, DDDate);
            _oo.FindCurrentDateandSetinDropDown(DDYear, DDMonth, DDDate);

            _oo.AddDateMonthYearDropDown(DDYearStruck, DDMonthStruck, DDDateStruck);
            _oo.FindCurrentDateandSetinDropDown(DDYearStruck, DDMonthStruck, DDDateStruck);

            _oo.AddDateMonthYearDropDown(DDYear1, DDMonth1, DDDate1);
            _oo.FindCurrentDateandSetinDropDown(DDYear1, DDMonth1, DDDate1);


            _oo.AddDateMonthYearDropDown(DDYearP, DDMonthP, DDDateP);
            _oo.FindCurrentDateandSetinDropDown(DDYearP, DDMonthP, DDDateP);

            _oo.AddDateMonthYearDropDown(DDYearP1, DDMonthP1, DDDateP1);
            _oo.FindCurrentDateandSetinDropDown(DDYearP1, DDMonthP1, DDDateP1);

            _oo.AddDateMonthYearDropDown(DDYearPStruck, DDMonthPStruck, DDDatePStruck);
            _oo.FindCurrentDateandSetinDropDown(DDYearPStruck, DDMonthPStruck, DDDatePStruck);

            _oo.AddDateMonthYearDropDown(DDYearFAD, DDMonthFAD, DDDateFAD);
            _oo.FindCurrentDateandSetinDropDown(DDYearFAD, DDMonthFAD, DDDateFAD);

            _oo.AddDateMonthYearDropDown(DDYearFADPanel, DDMonthFADPanel, DDDateFADPanel);
            _oo.FindCurrentDateandSetinDropDown(DDYearFADPanel, DDMonthFADPanel, DDDateFADPanel);

            BAL.objBal.AddDateMonthYearDropDown(drpYY, DrpMM, DrpDD);
            BAL.objBal.FindCurrentDateandSetinDropDown(drpYY, DrpMM, DrpDD);

            DataTable dtp = new DataTable();
            using (SqlCommand cmd = new SqlCommand())
            {
                try
                {
                    cmd.CommandText = "FeePermissionSettingProc";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = _con;
                    cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dtp);
                    if (dtp.Rows.Count > 0)
                    {

                        DDYear.Enabled = (dtp.Rows[0]["TCApplicationDate"].ToString() == "0" ? false : true);
                        DDMonth.Enabled = (dtp.Rows[0]["TCApplicationDate"].ToString() == "0" ? false : true);
                        DDDate.Enabled = (dtp.Rows[0]["TCApplicationDate"].ToString() == "0" ? false : true);
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }
    }

    protected void TxtEnter_TextChanged(object sender, EventArgs e)
    {

        Submit.Visible = false;
        var studentId = Request.Form[hfStudentId.UniqueID];
        if (studentId == string.Empty)
        {
            txtSrNo.Text = txtSrNo.Text.Replace("&", "/").Trim();
            studentId = txtSrNo.Text;
        }
        ShowDeatils();

    }
    protected void LinkButton6_Click(object sender, EventArgs e)
    {
        Submit.Visible = false;
        var studentId = Request.Form[hfStudentId.UniqueID];
        if (studentId == string.Empty)
        {
            txtSrNo.Text = txtSrNo.Text.Replace("&", "/").Trim();
            studentId = txtSrNo.Text;
        }
        ShowDeatils();
    }
    public void ShowDeatils()
    {
        Grd.DataSource = null;
        Grd.DataBind();
        lblResult.Text = "";
        var studentId = Request.Form[hfStudentId.UniqueID];
        if (studentId == string.Empty)
        {
            studentId = txtSrNo.Text.Trim();
        }
        string sq = "select top(1) srno, SessionName, admissionforclassid,streamid from StudentOfficialDetails where BranchCode=" + Session["BranchCode"] + " and SrNo='" + studentId + "' and isnull(Promotion,'')='' order by id desc";
        var admissionforclassid = _oo.ReturnTag(sq, "admissionforclassid");
        var streamid = _oo.ReturnTag(sq, "streamid");
        var sessionName = _oo.ReturnTag(sq, "SessionName");
        var branchcode = Session["BranchCode"].ToString();

        if (_oo.Duplicate(sq) && Session["SessionName"].ToString() != _oo.ReturnTag(sq, "SessionName").ToString())
        {
            Div1.Visible = false;
            divControls.Visible = false;
            Submit.Visible = false;
            grdshow.Visible = false;
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "This student is not found in the current session because his/her last session was " + _oo.ReturnTag(sq, "SessionName").ToString() + " !", "A");
            return;
        }
        _sql = "select id, classname from classmaster where SessionName='" + Session["SessionName"] + "' and branchcode=" + Session["BranchCode"] + "";
        _oo.FillDropDown_withValue(_sql, drpClass, "classname", "id");

        _sql = "Select blockedRemark from StudentOfficialDetails where srno='" + studentId + "' and SessionName='" + Session["SessionName"] + "' and branchcode=" + Session["BranchCode"] + "";
        var remark = _oo.ReturnTag(_sql, "blockedRemark");
        if (remark != "")
        {
            divMess.Visible = true;
            mess.Text = remark;
        }
        else
        {
            divMess.Visible = false;
            mess.Text = "";
        }

        _sql = "select Top 1 SessionName from StudentOfficialDetails where srno='" + studentId + "' and BranchCode=" + Session["BranchCode"] + " order By Id Desc";
        if (_oo.Duplicate(_sql))
        {
            var topSessionName = _oo.ReturnTag(_sql, "SessionName");

            _sql = "Select SectionName,Card,Gender,Medium,FatherContactNo,ClassName, combineclassname, Classid, Branchid,convert(nvarchar,DateOfAdmiission,106) as DateOfAdmiission,";
            _sql += " DatePart(year,DateOfAdmiission) as YYYY,Left(DateName(mm,DateOfAdmiission),3) as MMM,DatePart(dd,DateOfAdmiission) as DD ,SectionId,FatherName,";
            _sql += " MotherName,Name,StEnRCode,srno,";
            _sql += " case  when TransportRequired='Yes' then 'Yes' else 'No' end as TransportRequired";
            _sql += ",BranchName,FamilyContactNo,PhotoPath, Withdrwal, Board from AllStudentRecord_UDF('" + topSessionName + "'," + Session["BranchCode"] + ")";
            _sql += " where SrNo ='" + studentId + "'";
            drpClass.SelectedItem.Text = _oo.ReturnTag(_sql, "ClassName");
            string classid = _oo.ReturnTag(_sql, "Classid");
            string classname = _oo.ReturnTag(_sql, "ClassName");
            string Branchid = _oo.ReturnTag(_sql, "Branchid").ToString();
            string Withdrwal = _oo.ReturnTag(_sql, "Withdrwal").ToString();
            string YYYY = _oo.ReturnTag(_sql, "YYYY").ToString();
            string MMM = _oo.ReturnTag(_sql, "MMM").ToString();
            string DD = _oo.ReturnTag(_sql, "DD").ToString();
            string Board = _oo.ReturnTag(_sql, "Board").ToString();
            var dt = _oo.Fetchdata(_sql);

            _sql = "select top(1) Template, IsLock from TCAndReceiptTemplate where BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "'";
            if (_oo.Duplicate(_sql))
            {
                ddlTemplate.SelectedValue = _oo.ReturnTag(_sql, "Template");
                ddlTemplate.Enabled = (_oo.ReturnTag(_sql, "IsLock").ToLower() == "true" ? false : true);
            }
            else
            {
                grdshow.Visible = true;
                Div1.Visible = false;
                divControls.Visible = false;
                Submit.Visible = false;
                GrdStudent.DataSource = dt;
                GrdStudent.DataBind();
                lblResult.Text = "Sorry, Please set template first!";
                return;
            }
            if (dt.Rows.Count > 0)
            {
                loadIssuied(studentId);
                double balnc = 0;
                using (SqlCommand cmd2 = new SqlCommand())
                {
                    cmd2.Connection = _con;
                    cmd2.CommandText = "GetArrearForPromotion";
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Parameters.AddWithValue("@Srno", studentId);
                    cmd2.Parameters.AddWithValue("@SessionName", topSessionName);
                    cmd2.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString().Trim());
                    SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                    DataSet dt2 = new DataSet();
                    da2.Fill(dt2);
                    cmd2.Parameters.Clear();
                    balnc = double.Parse(dt2.Tables[0].Rows[0]["Balance"].ToString() == "" ? "0" : dt2.Tables[0].Rows[0]["Balance"].ToString());
                }
                if (balnc > 0)
                {
                    grdshow.Visible = true;
                    Div1.Visible = false;
                    divControls.Visible = false;
                    Submit.Visible = false;
                    GrdStudent.DataSource = dt;
                    GrdStudent.DataBind();
                    lblResult.Text = "Sorry, Please deposit outstanding amount first, " + balnc.ToString("0.00") + " amount is pending in session " + topSessionName + " !";
                    return;
                }

                else
                {
                    grdshow.Visible = true;
                    GrdStudent.DataSource = dt;
                    GrdStudent.DataBind();
                    string sss = "select Top(1) tctype  from TCCollection where srno='" + studentId + "' and BranchCode=" + Session["BranchCode"] + " order by id desc";
                    string tctype = _oo.ReturnTag(sss, "tctype").ToString();

                    if (!_oo.Duplicate(sss))
                    {
                        ddlCopy.SelectedIndex = 0;
                        divCopy.Visible = true;
                        if (tctype == "")
                        {
                            ddlCopy.SelectedValue = "Original";
                            ddlCopy.SelectedIndex = 1;
                        }
                    }
                    else
                    {
                        if (tctype == "Original")
                        {
                            ddlCopy.SelectedValue = "Duplicate";
                            lblResult.Text = "Original T.C. issued already!";
                            ddlCopy.SelectedIndex = 2;
                        }
                        if (tctype == "Duplicate")
                        {
                            ddlCopy.SelectedValue = "Triplicate";
                            lblResult.Text = "Duplicate T.C. issued already!";
                            ddlCopy.SelectedIndex = 3;
                        }
                        if (tctype == "Triplicate")
                        {
                            ddlCopy.SelectedValue = "Quadruplicate";
                            lblResult.Text = "Triplicate T.C. issued already!";
                            ddlCopy.SelectedIndex = 4;
                        }
                        if (tctype == "Quadruplicate")
                        {
                            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Quadruplicate  T.C. issued already, another T.C. could not be issued!", "A");
                            Div1.Visible = false;
                            divControls.Visible = false;
                            Submit.Visible = false;
                            return;
                        }
                    }

                    if (tctype != "Quadruplicate")
                    {
                        Submit.Visible = true;
                        Div1.Visible = true;
                        divControls.Visible = true;


                        _sql = "select sum(BounceCharges) BounceCharges from (select isnull(sum(isnull(BounceCharges, 0)), 0) BounceCharges from TCCollection where srno = '" + studentId + "' and Status = 'Cancelled' union all select(isnull(sum(isnull(BounceCharges, 0)), 0) * (-1)) BounceCharges from TCCollection where srno = '" + studentId + "' and BranchCode=" + Session["BranchCode"] + " and Status = 'Paid' )T1";
                        txtCBFee.Text = _oo.ReturnTag(_sql, "BounceCharges");
                        txtAmt.Text = "";
                        _sql = "select Amount from TCFormFeeMaster where SessionName='" + topSessionName.Trim() + "' and Classid=" + classid + " and Branchid=case when '" + Branchid + "'='' then Branchid else '" + Branchid + "' end  and BranchCode=" + Session["BranchCode"] + " and copytype='" + ddlCopy.SelectedValue + "'";
                        if (_oo.Duplicate(_sql))
                        {
                            Submit.Visible = true;
                            Div1.Visible = true;
                            divControls.Visible = true;
                            txtAmt.Text = _oo.ReturnTag(_sql, "Amount");
                        }
                        else
                        {
                            Div1.Visible = false;
                            divControls.Visible = false;
                            Submit.Visible = false;
                            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Please enter Master for " + ddlCopy.SelectedValue + " T.C. first!", "A");
                            return;
                        }

                        txtConcession.Text = "0";

                        if (txtAmt.Text == "")
                        {
                            txtAmt.Text = "0";
                        }
                        txtReceviedAmount.Text = ((double.Parse(txtAmt.Text) + double.Parse(txtCBFee.Text)) - double.Parse(txtConcession.Text)).ToString();
                        _oo.AddDateMonthYearDropDown(DDYearFAD, DDMonthFAD, DDDateFAD);
                        _oo.FindCurrentDateandSetinDropDown(DDYearFAD, DDMonthFAD, DDDateFAD);
                        if (DDDateFAD.Items.Count == 30)
                        {
                            DDDateFAD.Items.Insert(30, new ListItem("31", "31"));
                        }
                        try
                        {
                            DDYearFAD.Text = YYYY;
                            DDMonthFAD.Text = MMM;
                            DDDateFAD.Text = DD;
                        }
                        catch (Exception)
                        {
                        }

                        string ss = "select top(1) cm.ClassName from StudentOfficialDetails o inner join ClassMaster cm on cm.Id=o.AdmissionForClassId and o.SessionName=cm.SessionName and o.BranchCode=cm.BranchCode where SrNo = '" + studentId + "' and BranchCode=" + Session["BranchCode"] + " order by o.Id asc";
                        string classnameFirst = _oo.ReturnTag(_sql, "ClassName");
                        if (classname.Trim().Contains("XI") || classname.Trim().Contains("XIS") || classname.Trim().Contains("XIC") || classname.Trim().Contains("XIA") || classname.Trim() == "XI")
                        {
                            txtFAC.Text = "XI";
                        }
                        else if (classname.Trim().Contains("XII") || classname.Trim().Contains("XIIS") || classname.Trim().Contains("XIIC") || classname.Trim().Contains("XIIA") || classname.Trim() == "XII ")
                        {
                            txtFAC.Text = "XII";
                        }
                        else
                        {
                            txtFAC.Text = classname;
                        }

                        Permission_Values();
                        Concession_Values();

                        txtAmt.Focus();

                        CalculateAmount();
                        LoadNextClass();


                        using (SqlConnection conn = new SqlConnection())
                        {
                            conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                cmd.Connection = conn;
                                cmd.CommandText = "USP_StudentsPhotoReport";
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@sessionName", Session["SessionName"].ToString().Trim());
                                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString().Trim());
                                cmd.Parameters.AddWithValue("@SrNo", studentId.ToString().Trim());
                                cmd.Parameters.AddWithValue("@action", "details");
                                SqlDataAdapter das = new SqlDataAdapter(cmd);
                                DataSet dsPhoto = new DataSet();
                                das.Fill(dsPhoto);
                                cmd.Parameters.Clear();
                                grdshow.Visible = true;
                                if (dsPhoto.Tables[0].Rows.Count > 0)
                                {
                                    img.ImageUrl = dsPhoto.Tables[0].Rows[0]["PhotoPath"].ToString() != string.Empty ? dsPhoto.Tables[0].Rows[0]["PhotoPath"] + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture) : "../img/user-pic/user-pic.jpg" + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                                    studentImg.NavigateUrl = dsPhoto.Tables[0].Rows[0]["PhotoPath"].ToString() != string.Empty ? dsPhoto.Tables[0].Rows[0]["PhotoPath"] + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture) : "../img/user-pic/user-pic.jpg" + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                                    hylinkmoredetails.NavigateUrl = "../11/StudentRegView.aspx?print=1&id=" + dt.Rows[0]["stenrcode"];
                                }
                            }
                        }
                        if (Withdrwal.Trim().ToUpper() == "W")
                        {
                            Div1.Visible = false;
                        }
                        else
                        {
                            Div1.Visible = true;
                        }


                    }
                    else
                    {
                        Submit.Visible = false;
                        Div1.Visible = false;
                        divControls.Visible = false;
                    }
                }
            }
        }

        txtSubjects.Text = GetSubjects(admissionforclassid, streamid, sessionName, branchcode, studentId);

    }

    public string GetSubjects(string admissionForClassId, string streamID, string sessionName,
        string branchCode, string srno)
    {
        string sql = @"
        ;WITH AllSubjects AS (
            SELECT SubjectName
            FROM TTSubjectMaster 
            WHERE SessionName = @SessionName AND Classid = @ClassID AND BranchCode = @BranchCode 
            and BranchId=IIF(ISNULL(@StreamId,'')='',BranchId,@StreamId)
            UNION

            SELECT SubjectName
            FROM TTSubjectMaster 
            WHERE SessionName = @SessionName AND BranchCode = @BranchCode 
              AND id IN (
                  SELECT OptSubjectId 
                  FROM ICSEOptionalSubjectAllotment 
                  WHERE Srno = @SrNo 
                    AND SessionName = @SessionName 
                    AND BranchCode = @BranchCode
              )
        )
        SELECT STUFF((
            SELECT ', ' + SubjectName
            FROM AllSubjects
            FOR XML PATH(''), TYPE
        ).value('.', 'NVARCHAR(MAX)'), 1, 2, '') AS SubjectList
        ";
        string AdmissionForClassId = admissionForClassId.Trim();
        string SessionName = sessionName.Trim();
        string BranchCode = branchCode.Trim();
        SqlCommand cmd22 = new SqlCommand(sql, _con);
        cmd22.Parameters.AddWithValue("@SrNo", srno);
        cmd22.Parameters.AddWithValue("@ClassID", AdmissionForClassId);
        cmd22.Parameters.AddWithValue("@SessionName", SessionName);
        cmd22.Parameters.AddWithValue("@BranchCode", BranchCode);
        cmd22.Parameters.AddWithValue("@StreamID", streamID);
        SqlDataAdapter da21 = new SqlDataAdapter(cmd22);
        DataTable dtStudent1 = new DataTable();
        da21.Fill(dtStudent1);

        string subjects = dtStudent1.Rows[0]["SubjectList"].ToString();

        return (subjects == null || subjects == "") ? "N/A" : subjects;
    }
    protected void loadIssuied(string studentId)
    {
        int tcSts = 0;
        _sql = "Select id, SrNo, RecieptNo, TCIssueDate, ReceivedAmount, Status, MOP, SrNo, SessionName, Cancel, Formate, TCType, AdmissionFromDate from TCCollection where SrNo='" + studentId + "' and BranchCode=" + Session["BranchCode"].ToString() + " order by id asc";
        Grd.DataSource = _oo.GridFill(_sql);
        Grd.DataBind();
        for (int i = 0; i < Grd.Rows.Count; i++)
        {
            Label Status = (Label)Grd.Rows[i].FindControl("Status");
            Label MOP = (Label)Grd.Rows[i].FindControl("MOP");
            LinkButton LinkButton1 = (LinkButton)Grd.Rows[i].FindControl("LinkButton1");
            HyperLink LinkButton7 = (HyperLink)Grd.Rows[i].FindControl("LinkButton7");
            LinkButton LinkButton2s = (LinkButton)Grd.Rows[i].FindControl("LinkButton2s");


            if (Status.Text.Trim() == "Pending" && (MOP.Text.Trim() == "Cheque" || MOP.Text.Trim() == "Other"))
            {
                Grd.Rows[i].BackColor = System.Drawing.Color.OrangeRed;
                Grd.Rows[i].ForeColor = System.Drawing.Color.White;
                LinkButton2s.ForeColor = System.Drawing.Color.White;
                LinkButton1.Text = "<i class='fa fa-lock'></i>";
                LinkButton7.Text = "<i class='fa fa-lock'></i>";
                LinkButton1.Enabled = false;
                LinkButton7.Enabled = false;
            }

            if (Status.Text.Trim() == "Cancelled" && (MOP.Text.Trim() == "Cheque" || MOP.Text.Trim() == "Other"))
            {
                LinkButton1.Text = "<i class='fa fa-lock'></i>";
                LinkButton7.Text = "<i class='fa fa-lock'></i>";
                LinkButton1.Enabled = false;
                LinkButton7.Enabled = false;
            }
            if (Status.Text.Trim() == "Paid" && Grd.Rows.Count == 1)
            {
                LinkButton1.Text = "<i class='fa fa-pencil'></i>";
                LinkButton1.Enabled = true;
            }
            var lblId3 = (Label)Grd.Rows[i].FindControl("lblRecieptNo");
            var lblFormate = (Label)Grd.Rows[i].FindControl("lblFormate");
            var ss = lblId3.Text;
            _sql = "Select MAX(Id) as Id from StudentOfficialDetails where SrNo='" + studentId + "' and BranchCode=" + Session["BranchCode"] + " and ISNULL(Promotion,'')<>'Cancelled'";
            var maxid = _oo.ReturnTag(_sql, "Id");
            _sql = "Select SessionName from StudentOfficialDetails where Id='" + maxid + "' and BranchCode=" + Session["BranchCode"] + "";
            var session = _oo.ReturnTag(_sql, "SessionName");
            Session["Top_sessionName"] = session;
            Session["TCRecieptNo"] = ss;

            if (lblFormate.Text == "Template 1")
            {
                LinkButton7.NavigateUrl = String.Format("TCCBSE_English.aspx?print={0}", ss);
                //Response.Redirect(String.Format("TCCBSE_English.aspx?print={0}", "1"));
            }
            if (lblFormate.Text == "Template 2")
            {
                LinkButton7.NavigateUrl = String.Format("TCCBSE_Hindi.aspx?print={0}", ss);
                //Response.Redirect(String.Format("TCCBSE_Hindi.aspx?print={0}", "1"));
            }
            if (lblFormate.Text == "Template 3")
            {
                LinkButton7.NavigateUrl = String.Format("TCICSE.aspx?print={0}", ss);
                //Response.Redirect(String.Format("TCICSE.aspx?print={0}", "1"));
            }
            if (lblFormate.Text == "Template 4")
            {
                LinkButton7.NavigateUrl = String.Format("TCCounterSignHindi.aspx?print={0}", ss);
                //Response.Redirect(String.Format("TC_UPBoard.aspx?print={0}", "1"));
            }
            if (lblFormate.Text == "Template 5")
            {
                LinkButton7.NavigateUrl = String.Format("TCCounterSign.aspx?print={0}", ss);
                //Response.Redirect(String.Format("TC_UPBoard.aspx?print={0}", "1"));
            }
        }
        //for (int i = 0; i < Grd.Rows.Count; i++)
        //{
        //    LinkButton LinkButton1 = (LinkButton)Grd.Rows[i].FindControl("LinkButton1");
        //    HyperLink LinkButton7 = (HyperLink)Grd.Rows[i].FindControl("LinkButton7");
        //    Label TCType = (Label)Grd.Rows[i].FindControl("TCType");
        //    if (Grd.Rows.Count != (i + 1))
        //    {
        //        LinkButton7.Text = "<i class='fa fa-lock'></i>";
        //        LinkButton7.Enabled = false;
        //    }
        //}
    }
    protected double CheckArrierAmount(string topSessionName)
    {
        double val = 0;
        var studentId = Request.Form[hfStudentId.UniqueID];
        if (studentId == string.Empty)
        {
            studentId = txtSrNo.Text.Trim();
        }
        var param = new List<SqlParameter>();
        param.Add(new SqlParameter("@SrNo", studentId));
        param.Add(new SqlParameter("@SessionName", topSessionName));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
        var ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("sp_CheckArrearForTC", param);
        val = double.Parse((ds.Tables[0].Rows[0]["BalanceAmount"].ToString() == "" ? "0" : ds.Tables[0].Rows[0]["BalanceAmount"].ToString()));
        return val;
    }
    protected void LoadNextClass()
    {
        var studentId = Request.Form[hfStudentId.UniqueID];
        if (studentId == string.Empty)
        {
            studentId = txtSrNo.Text.Trim();
        }

        var status = "";
        _sql = "Select Status From StudentWithdrawal where srno='" + studentId + "'";
        status = _oo.ReturnTag(_sql, "Status");


        var sessionnamed = "";
        var classid = "";
        if (status.ToUpper() == "PASSED")
        {
            txtIsQualified.Text = "YES";
            _sql = "Select *from StudentOfficialDetails where SrNo='" + studentId + "' and SessionNAme='" + Session["SessionName"] + "'  and ISNULL(Promotion,'')<>'Cancelled'";
            if (_oo.Duplicate(_sql))
            {
                _sql = "Select Top 1 SessionName,AdmissionForClassId from (Select Top 2 SessionName,id,AdmissionForClassId from StudentOfficialDetails where SrNo='" + studentId + "'  and ISNULL(Promotion,'')<>'Cancelled' order by id Desc) As T1 order by id";
                sessionnamed = _oo.ReturnTag(_sql, "SessionName");
                classid = _oo.ReturnTag(_sql, "AdmissionForClassId");
            }
            else
            {
                _sql = "Select Top 1 SessionName,AdmissionForClassId from StudentOfficialDetails where SrNo='" + studentId + "'  and ISNULL(Promotion,'')<>'Cancelled' order by id Desc";
                sessionnamed = _oo.ReturnTag(_sql, "SessionName");
                classid = _oo.ReturnTag(_sql, "AdmissionForClassId");
            }

            _sql = "Select Top 1 ClassName from ClassMaster where CIDOrder=(Select CIDOrder from ClassMaster where SessionName='" + sessionnamed + "' and id='" + classid + "') and SessionName='" + sessionnamed + "'";
            if (_oo.ReturnTag(_sql, "ClassName").Contains("XI ") || _oo.ReturnTag(_sql, "ClassName") == "XI")
            {
                txtIsQualifiedtowhichclass.Text = "XII";
            }
            else if (_oo.ReturnTag(_sql, "ClassName").Contains("XII ") || _oo.ReturnTag(_sql, "ClassName") == "XII ")
            {
                txtIsQualifiedtowhichclass.Text = "HIGHER STUDIES";
            }
            else
            {
                _sql = "Select Top 1 ClassName from ClassMaster where CIDOrder=(Select CIDOrder+1 from ClassMaster where SessionName='" + sessionnamed + "' and id='" + classid + "') and SessionName='" + sessionnamed + "'";
                txtIsQualifiedtowhichclass.Text = _oo.ReturnTag(_sql, "ClassName");
            }
        }
        else if (status.ToUpper() == "FAILED" || status.ToUpper() == "COMPARTMENT")
        {
            txtIsQualified.Text = "NO";
            if (drpClass.SelectedItem.Text.Trim().Contains("XI ") || drpClass.SelectedItem.Text.Trim().Contains("XIS") || drpClass.SelectedItem.Text.Trim().Contains("XIC") || drpClass.SelectedItem.Text.Trim().Contains("XIA") || drpClass.SelectedItem.Text.Trim() == "XI")
            {
                txtIsQualifiedtowhichclass.Text = "XI";
            }
            else if (drpClass.SelectedItem.Text.Trim().Contains("XII ") || drpClass.SelectedItem.Text.Trim().Contains("XIIS") || drpClass.SelectedItem.Text.Trim().Contains("XIIC") || drpClass.SelectedItem.Text.Trim().Contains("XIIA") || drpClass.SelectedItem.Text.Trim() == "XII ")
            {
                txtIsQualifiedtowhichclass.Text = "XII";
            }
            else
            {
                txtIsQualifiedtowhichclass.Text = drpClass.SelectedItem.Text.Trim();
            }
        }
        else
        {
            txtIsQualified.Text = "";
            txtIsQualifiedtowhichclass.Text = "";
        }
        if (txtIsQualifiedtowhichclass.Text != "HIGHER STUDIES")
        {
            string inwords;
            inwords = BAL.objBal.convertRomantostring(txtIsQualifiedtowhichclass.Text);
            txtIsQualifiedtowhichclass.Text = txtIsQualifiedtowhichclass.Text + (inwords != string.Empty ? ("/ " + inwords) : "");
        }


    }
    public void CalculateAmount()
    {
        double concession;
        double amount;
        double rAmount, cbAmount;
        if (txtConcession.Text != "")
        {
            concession = Convert.ToDouble(txtConcession.Text);
        }
        else
        {
            concession = 0;
        }
        amount = Convert.ToDouble(txtAmt.Text);
        double.TryParse(txtCBFee.Text, out cbAmount);
        rAmount = (amount + cbAmount) - concession;
        txtReceviedAmount.Text = rAmount.ToString(CultureInfo.InvariantCulture);
        Submit.Visible = true;
    }

    protected void txtAmt_TextChanged(object sender, EventArgs e)
    {
        CalculateAmount();
        if (txtConcession.Enabled == false)
        {
            txtSubjects.Focus();
        }
        else
        {
            txtConcession.Focus();
        }
    }

    protected void WithdrawlStudent()
    {
        var date = drpYY.SelectedItem + "/" + DrpMM.SelectedItem + "/" + DrpDD.SelectedItem;
        var lblSrno = (Label)GrdStudent.Rows[0].FindControl("Label1");
        var param = new List<SqlParameter>
            {
                new SqlParameter("@Srno", lblSrno.Text),
                new SqlParameter("@withdrawalDate", date),
                new SqlParameter("@remark", txtReason.Text),
                new SqlParameter("@LoginName", Session["LoginName"].ToString()),
                new SqlParameter("@BranchCode", Session["BranchCode"].ToString()),
                new SqlParameter("@SessionName", Session["SessionName"]),
                new SqlParameter("@Status", DropDownList2.SelectedItem.Text)
            };
        var para = new SqlParameter("@msg", "") { Direction = ParameterDirection.Output };
        param.Add(para);
        DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("StudentWithDrawalProc", param);
    }
    protected void Submit_Click(object sender, EventArgs e)
    {
        Label lblStudentId = (Label)GrdStudent.Rows[0].FindControl("Label1");
        var studentId = lblStudentId.Text;
        //if (studentId == string.Empty)
        //{
        //    studentId = txtSrNo.Text.Trim();
        //}


        string sql = "select * from TCCollection where srno='" + studentId + "' and BranchCode=" + Session["BranchCode"] + " and tcType='" + ddlCopy.SelectedValue + "'";
        if (_oo.Duplicate(sql))
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, ddlCopy.SelectedValue + " already issued!", "A");
            Submit.Visible = false;
            return;
        }
        var xx = _oo.FindRecieptNo();
        if (xx == "")
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Please Initilize receipt no.!", "A");
            return;
        }
        if ((DropDownMOD.SelectedValue == "Cheque" || DropDownMOD.SelectedValue == "DD") && TextBox2.Text.Trim() == "")
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "please enter Instrument No.", "A");
            return;
        }
        if ((DropDownMOD.SelectedValue == "Card") && TextBox2.Text.Trim() == "")
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "please enter Card No.", "A");
            return;
        }
        if ((DropDownMOD.SelectedValue == "Online Transfer" || DropDownMOD.SelectedValue == "Other") && TextBox2.Text.Trim() == "")
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "please enter Ref. No.", "A");
            return;
        }
        _sql = "select Top 1 SessionName from StudentOfficialDetails where srno='" + studentId + "' and BranchCode=" + Session["BranchCode"] + " and ISNULL(Promotion,'')<>'Cancelled' order By Id Desc";
        if (_oo.Duplicate(_sql))
        {
            var topSessionName = _oo.ReturnTag(_sql, "SessionName");
            _sql = "Select  Withdrwal from AllStudentRecord_UDF('" + topSessionName + "'," + Session["BranchCode"] + ") where SrNo ='" + studentId + "'";
            string Withdrwal = _oo.ReturnTag(_sql, "Withdrwal");
            if (Withdrwal.Trim().ToUpper() == "W")
            {
                Div1.Visible = false;
            }
            else
            {
                WithdrawlStudent();
            }
        }

        string tcType = ddlCopy.SelectedValue;


        using (var cmd = new SqlCommand())
        {
            _sql = "select Top 1 SessionName from StudentOfficialDetails where srno='" + studentId + "' and BranchCode=" + Session["BranchCode"] + " and ISNULL(Promotion,'')<>'Cancelled' order By Id Desc";
            var topSessionName = _oo.ReturnTag(_sql, "SessionName");
            _sql = "Select SectionName,Card,Gender,Medium,FatherContactNo,ClassName, combineclassname,convert(nvarchar,DateOfAdmiission,106) as DateOfAdmiission,";
            _sql += " DatePart(year,DateOfAdmiission) as YYYY,Left(DateName(mm,DateOfAdmiission),3) as MMM,DatePart(dd,DateOfAdmiission) as DD ,SectionId,FatherName,";
            _sql += " MotherName,Name,StEnRCode,srno,";
            _sql += " case  when TransportRequired='Yes' then 'Yes' else 'No' end as TransportRequired";
            _sql += ",BranchName,FamilyContactNo,PhotoPath from AllStudentRecord_UDF('" + topSessionName + "'," + Session["BranchCode"] + ")";
            _sql += " where SrNo ='" + studentId + "'";

            string status = ""; string ParamStatus = "";
            string sqls = "Select Status From StudentWithdrawal where srno='" + studentId + "' and BranchCode=" + Session["BranchCode"] + "";
            status = _oo.ReturnTag(sqls, "Status").ToUpper();
            ParamStatus = status;
            if (status.ToUpper() == "PASSED")
            {
                ParamStatus = status;
            }
            else if (status.ToUpper() == "PROMOTED")
            {
                ParamStatus = status;
            }
            else if (status.ToUpper() == "FAILED")
            {
                if (BAL.objBal.convertRomantostring(_oo.ReturnTag(_sql, "ClassName")).ToUpper() == "TENTH" || BAL.objBal.convertRomantostring(_oo.ReturnTag(_sql, "ClassName")).ToUpper() == "TWELFTH")
                {
                    ParamStatus = "FAILED";
                }
                else
                {
                    ParamStatus = "DETAINED";
                }
            }
            else if (status.ToUpper() == "LEFT")
            {
                ParamStatus = "Appearing";
            }
            else
            {
                ParamStatus = "PASSED";
            }


            cmd.CommandText = "TCCollectionProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@srno", studentId.Trim());
            //cmd.Parameters.AddWithValue("@RecieptNo", xx);
            cmd.Parameters.AddWithValue("@StudentName", _oo.ReturnTag(_sql, "Name"));
            cmd.Parameters.AddWithValue("@FatherName", _oo.ReturnTag(_sql, "FatherName"));
            cmd.Parameters.AddWithValue("@fatherContactNo", _oo.ReturnTag(_sql, "FatherContactNo"));
            cmd.Parameters.AddWithValue("@MotherName", _oo.ReturnTag(_sql, "MotherName"));
            cmd.Parameters.AddWithValue("@Class", _oo.ReturnTag(_sql, "ClassName"));
            cmd.Parameters.AddWithValue("@sex", _oo.ReturnTag(_sql, "Gender"));
            cmd.Parameters.AddWithValue("@amount", txtAmt.Text);
            cmd.Parameters.AddWithValue("@ReceivedAmount", txtReceviedAmount.Text.Trim());
            cmd.Parameters.AddWithValue("@Dues", txtDues.Text.Trim());
            cmd.Parameters.AddWithValue("@ConcessionType", txtConcessiontype.Text.Trim());
            cmd.Parameters.AddWithValue("@Formate", ddlTemplate.SelectedValue);
            cmd.Parameters.AddWithValue("@tcType", tcType);
            cmd.Parameters.AddWithValue("@PassStatus", ParamStatus);


            if (txtConcession.Text != "")
            {
                cmd.Parameters.AddWithValue("@Concession", txtConcession.Text.Trim());
            }
            else
            {
                cmd.Parameters.AddWithValue("@Concession", "0");
            }
            cmd.Parameters.AddWithValue("@ConductandWork", txtConduct.Text.Trim());
            cmd.Parameters.AddWithValue("@Subject", txtSubjects.Text.Trim());
            cmd.Parameters.AddWithValue("@TWD", txtTWD.Text.Trim());
            cmd.Parameters.AddWithValue("@TWDP", txtTWDP.Text.Trim());
            cmd.Parameters.AddWithValue("@NCC", txtNCC.Text.Trim());
            cmd.Parameters.AddWithValue("@ECA", txtECA.Text.Trim());
            cmd.Parameters.AddWithValue("@MOP", DropDownMOD.SelectedItem.Text);
            if (DropDownMOD.SelectedValue == "Cheque" || DropDownMOD.SelectedValue == "Other")
            {
                cmd.Parameters.AddWithValue("@CheckDDNo", TextBox2.Text);
                cmd.Parameters.AddWithValue("@CheckDate", txtChequeDate.Text);
                cmd.Parameters.AddWithValue("@BankName", TextBox3.Text);
                cmd.Parameters.AddWithValue("@Status", ddlChequeStatus.SelectedValue);
                if (ddlChequeStatus.SelectedValue == "Paid")
                {
                    cmd.Parameters.AddWithValue("@ChequeStatus", "Paid");
                }
            }
            if (DropDownMOD.SelectedValue != "Cheque" && DropDownMOD.SelectedValue != "Other" && DropDownMOD.SelectedValue != "Cash")
            {
                cmd.Parameters.AddWithValue("@CheckDDNo", TextBox2.Text);
                cmd.Parameters.AddWithValue("@CheckDate", DBNull.Value);
                cmd.Parameters.AddWithValue("@BankName", TextBox3.Text);
                cmd.Parameters.AddWithValue("@Status", "Paid");
                cmd.Parameters.AddWithValue("@ChequeStatus", "Paid");
            }
            if (DropDownMOD.SelectedValue == "Cash")
            {
                cmd.Parameters.AddWithValue("@ChequeStatus", "Paid");
                cmd.Parameters.AddWithValue("@Status", "Paid");
            }
            double BounceCharges = double.Parse(txtCBFee.Text.Trim() == string.Empty ? "0" : txtCBFee.Text.Trim());
            if (BounceCharges > 0)
            {
                cmd.Parameters.AddWithValue("@BounceCharges", BounceCharges.ToString("0.00"));
            }
            cmd.Parameters.AddWithValue("@AOR", txtAOR.Text.Trim());
            var ddStruck = DDYearStruck.SelectedItem + "/" + DDMonthStruck.SelectedItem + "/" + DDDateStruck.SelectedItem;
            cmd.Parameters.AddWithValue("@DateOfStruckOff", ddStruck);
            var dd2 = DDYear1.SelectedItem + "/" + DDMonth1.SelectedItem + "/" + DDDate1.SelectedItem;
            cmd.Parameters.AddWithValue("@TCIssueDate", dd2);
            var dd = DDYear.SelectedItem + "/" + DDMonth.SelectedItem + "/" + DDDate.SelectedItem;
            cmd.Parameters.AddWithValue("@AdmissionFromDate", dd);
            cmd.Parameters.AddWithValue("@inrollmentNo", txtInrollmentNo.Text.Trim());
            cmd.Parameters.AddWithValue("@bookno", txtBookNo.Text.Trim());
            var dd3 = DDYearFAD.SelectedItem + "/" + DDMonthFAD.SelectedItem + "/" + DDDateFAD.SelectedItem;
            cmd.Parameters.AddWithValue("@DateOfFAD", dd3);
            cmd.Parameters.AddWithValue("@ClassOfFAC", txtFAC.Text.Trim());
            cmd.Parameters.AddWithValue("@IsQualified", txtIsQualified.Text.Trim());
            cmd.Parameters.AddWithValue("@IsQualifiedtowhichclass", txtIsQualifiedtowhichclass.Text.Trim());
            cmd.Parameters.AddWithValue("@ClassWithResult", _oo.ReturnTag(_sql, "ClassName"));
            cmd.Parameters.AddWithValue("@Remark", txtRemark.Text.Trim());
            cmd.Parameters.AddWithValue("@sessionName", Session["SessionName"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());

            SqlParameter outputValue = new SqlParameter("@result", "");
            outputValue.Size = 0x100;
            outputValue.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(outputValue);

            cmd.Connection = _con;
            string recieptSrNo = "";

            try
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
                recieptSrNo = cmd.Parameters["@result"].Value.ToString();
                divControls.Visible = false;
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submited Successfully.", "S");
                Session["IsDuplicate"] = "No";
                ComposeSMS(recieptSrNo);
                //Session["TCRecieptNo"] = xx;
                //SendFeesSms(_oo.ReturnTag(_sql, "FamilyContactNo").ToString(), recieptSrNo, txtAmt.Text);--Old
                string qstr = recieptSrNo.Replace("/", "__");
                Response.Redirect("TcReciept.aspx?print=1&TCRecieptNo=" + qstr);
            }
            catch (Exception ex)
            {
                // ignored
            }
        }

    }

    public void ComposeSMS(string recieptNo)
    {
        try
        {
            List<SqlParameter> param = new List<SqlParameter>()
            {
                new SqlParameter("@SessionName",Session["SessionName"]),
                new SqlParameter("@ReceiptNo",recieptNo),
                new SqlParameter("@BranchCode",Session["BranchCode"])
            };
            DataSet ds = _oo.ReturnDataSet("USP_TCFeeTemplate", param.ToArray());
            if (ds != null && ds.Tables.Count > 0)
            {
                string msg = SendSms(ds);
            }

        }
        catch
        {

        }

    }
    public string SendSms(DataSet ds)
    {
        string msg;
        try
        {
            DataTable data = new DataTable();
            data = ds.Tables[0];

            var fatherContactNo = data.Rows[0]["FatherContactNo"].ToString();

            DataTable template = new DataTable();
            template = ds.Tables[1];

            msg = template.Rows[0][0].ToString();
            string[] param = template.Rows[0][1].ToString().Split(',');

            string[] daynamicVariables = msg.Split(new char[0]);
            foreach (var para in param)
            {
                string value = data.Rows[0][para].ToString();
                for (int i = 0; i < daynamicVariables.Count(); i++)
                {
                    if (daynamicVariables[i].ToString() == "{{{}}}")
                    {
                        daynamicVariables[i] = value;
                        break;
                    }
                }
            }

            msg = string.Join(" ", daynamicVariables);

            SendFeesSms(fatherContactNo, msg, "3");

        }
        catch
        {
            msg = "";
        }
        return msg;
    }

    public string SendFeesSms(string fmobileNo, string msg, string smsPageId)
    {
        string res = "0";
        try
        {
            _sql = "Select HitValue from SMSActivateDeactivate where BranchCode=" + Session["BranchCode"] + "";
            if (_oo.ReturnTag(_sql, "HitValue") == "") return res;
            if (_oo.ReturnTag(_sql, "HitValue") != "true") return res;
            string sql1 = "Select SmsSent From SmsEmailMaster Where Id=" + smsPageId + " and BranchCode='" + Session["BranchCode"] + "'";
            if (_oo.ReturnTag(sql1, "SmsSent").Trim() == "true")
            {
                var sadpNew = new SMSAdapterNew();
                if (fmobileNo == "") return "0";
                sadpNew.Send(msg, fmobileNo, smsPageId);
                res = "1";
            }
        }
        catch (Exception ex)
        {
            // ignored
        }

        return res;
    }
    protected void LinkButton7_Click(object sender, EventArgs e)
    {
        var link = (LinkButton)sender;
        var lblId3 = (Label)link.NamingContainer.FindControl("lblRecieptNo");
        var lblFormate = (Label)link.NamingContainer.FindControl("lblFormate");
        var ss = lblId3.Text;
        var currentrow = (GridViewRow)((Control)sender).NamingContainer;
        var srno = (Label)currentrow.FindControl("Label4s");
        _sql = "Select MAX(Id) as Id from StudentOfficialDetails where SrNo='" + srno.Text + "' and BranchCode=" + Session["BranchCode"] + " and ISNULL(Promotion,'')<>'Cancelled'";
        var maxid = _oo.ReturnTag(_sql, "Id");
        _sql = "Select SessionName from StudentOfficialDetails where Id='" + maxid + "' and BranchCode=" + Session["BranchCode"] + "";
        var session = _oo.ReturnTag(_sql, "SessionName");
        Session["Top_sessionName"] = session;
        Session["TCRecieptNo"] = ss;

        if (lblFormate.Text == "Template 1")
        {
            Response.Redirect(String.Format("TCCBSE_English.aspx?print={0}", ss));
        }
        if (lblFormate.Text == "Template 2")
        {
            Response.Redirect(String.Format("TCCBSE_Hindi.aspx?print={0}", ss));
        }
        if (lblFormate.Text == "Formate 3")
        {
            Response.Redirect(String.Format("TCCounterSignHindi.aspx?print={0}", ss));
        }
        if (lblFormate.Text == "Formate 4")
        {
            Response.Redirect(String.Format("TCICSE.aspx?print={0}", ss));
        }
        if (lblFormate.Text == "Formate 5")
        {
            Response.Redirect(String.Format("TCCounterSign.aspx?print={0}", ss));
            //Response.Redirect(String.Format("TC_UPBoard.aspx?print={0}", "1"));
        }
    }
    //public void SendFeesSms(string fmobileNo, string recieptNo, string amount)
    //{
    //	_sql = "Select HitValue from SMSActivateDeactivate where BranchCode=" + Session["BranchCode"] + "";
    //	if (_oo.ReturnTag(_sql, "HitValue") != "")
    //	{
    //		if (_oo.ReturnTag(_sql, "HitValue") == "true")
    //		{
    //			var sadpNew = new SMSAdapterNew();
    //			var mess = "";
    //			var collegeTitle = "";
    //			_sql = "select StudentName+' ('+srno+')' StudentName from TCCollection where RecieptNo='" + recieptNo + "' and BranchCode=" + Session["BranchCode"] + "";
    //			string name = _oo.ReturnTag(_sql, "StudentName");
    //			mess = "INR " + amount + " received towards T.C. Fee of " + name + ". Receipt No. " + recieptNo + " ";

    //			_sql = "Select CollegeShortNa  from CollegeMaster where BranchCode=" + Session["BranchCode"] + "";
    //			collegeTitle = _oo.ReturnTag(_sql, "CollegeShortNa");

    //			if (fmobileNo != "")
    //			{
    //				_sql = "Select SmsSent From SmsEmailMaster where Id='3'  and BranchCode=" + Session["BranchCode"] + "";
    //				if (_oo.ReturnTag(_sql, "SmsSent").Trim() == "true")
    //				{
    //					sadpNew.Send(mess, fmobileNo, "3");
    //				}
    //			}
    //		}
    //	}
    //}
    public void SendFeescancleSms(string fmobileNo, string recieptNo, string amount)
    {
        _sql = "Select HitValue from SMSActivateDeactivate where BranchCode=" + Session["BranchCode"] + "";
        if (_oo.ReturnTag(_sql, "HitValue") != "")
        {
            if (_oo.ReturnTag(_sql, "HitValue") == "true")
            {
                var sadpNew = new SMSAdapterNew();
                var mess = "";
                var collegeTitle = "";

                mess = "Receipt No. " + recieptNo + " has been cancelled of T.C. Fee. Refunded Amount is INR " + amount + " ";
                var smsResponse = "";

                _sql = "Select CollegeShortNa  from CollegeMaster where BranchCode=" + Session["BranchCode"] + "";
                collegeTitle = _oo.ReturnTag(_sql, "CollegeShortNa");

                if (fmobileNo != "")
                {
                    _sql = "Select SmsSent From SmsEmailMaster where Id='3'  and BranchCode=" + Session["BranchCode"] + "";
                    if (_oo.ReturnTag(_sql, "SmsSent").Trim() == "true")
                    {
                        smsResponse = sadpNew.Send(mess, fmobileNo, "3");
                    }
                }
            }
        }
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        var chk = (LinkButton)sender;
        var lblId3 = (Label)chk.NamingContainer.FindControl("Label38");
        var ss = lblId3.Text;
        Session["IsDuplicate"] = "Yes";
        //Session["TCRecieptNo"] = ss;
        Response.Redirect("TcReciept.aspx?print=1&TCRecieptNo=" + ss);

    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        var chk = (LinkButton)sender;
        var lblId = (Label)chk.NamingContainer.FindControl("Label36");
        var ss = lblId.Text;
        lblID.Text = ss;
        _sql = "select Id,srno,bookno,RecieptNo,convert(nvarchar,AdmissionFromDate,106) as AdmissionFromDate,Concession,inrollmentNo,";
        _sql += " ReceivedAmount, StudentName,FatherName,Class, ClassWithResult,LoginName,BranchCode,IsQualified,IsQualifiedtowhichclass,";
        _sql += " left(convert(nvarchar,AdmissionFromDate,106),2) as DD,Right(left(convert(nvarchar,AdmissionFromDate,106),6),3) as MM , RIGHT(convert(nvarchar,AdmissionFromDate,106),4) as YY,";
        _sql += " left(convert(nvarchar,DateOfStruckOff,106),2) as DDStruck,Right(left(convert(nvarchar,DateOfStruckOff,106),6),3) as MMStruck, RIGHT(convert(nvarchar,DateOfStruckOff,106),4) as YYStruck, ";
        _sql += " left(convert(nvarchar,DateOfFAD,106),2) as FADDD,Right(left(convert(nvarchar,DateOfFAD,106),6),3) as FADMM , RIGHT(convert(nvarchar,DateOfFAD,106),4) as FADYY,";
        _sql += " left(convert(nvarchar,ISNULL(TCIssueDate,AdmissionFromDate),106),2) as DD1,Right(left(convert(nvarchar,ISNULL(TCIssueDate,AdmissionFromDate),106),6),3) as MM1 , RIGHT(convert(nvarchar,ISNULL(TCIssueDate,AdmissionFromDate),106),4) as YY1,";
        _sql += " ClassOfFAC,Subject,Dues,ConcessionType,TWD,TWDP,NCC,ECA,MotherName,convert(nvarchar,RecordDate,106) as RecordDate,";
        _sql += " Sex,FatherContactNo,Amount,Remark,ConductandWork,AOR, PassStatus,Formate from TCCollection where ID=" + int.Parse(lblID.Text) + "  and BranchCode=" + Session["BranchCode"] + "";

        txtSrNoPanel.Text = _oo.ReturnTag(_sql, "srno");
        txtInrollmentNoPanel.Text = _oo.ReturnTag(_sql, "inrollmentNo");
        txtBookNoPanel.Text = _oo.ReturnTag(_sql, "bookno");
        txtStudentNamePanel.Text = _oo.ReturnTag(_sql, "StudentName");
        txtIsQualifiedPanel.Text = _oo.ReturnTag(_sql, "IsQualified");
        txtIsQualifiedtowhichclassPanel.Text = _oo.ReturnTag(_sql, "IsQualifiedtowhichclass");

        try
        {
            DDDateP.Text = _oo.ReadDD(_oo.ReturnTag(_sql, "DD"));
            DDMonthP.Text = _oo.ReturnTag(_sql, "MM");
            DDYearP.Text = _oo.ReturnTag(_sql, "YY");


            DDDateP1.Text = _oo.ReadDD(_oo.ReturnTag(_sql, "DD1"));
            DDMonthP1.Text = _oo.ReturnTag(_sql, "MM1");
            DDYearP1.Text = _oo.ReturnTag(_sql, "YY1");

            DDDatePStruck.Text = _oo.ReadDD(_oo.ReturnTag(_sql, "DDStruck"));
            DDMonthPStruck.Text = _oo.ReturnTag(_sql, "MMStruck");
            DDYearPStruck.Text = _oo.ReturnTag(_sql, "YYStruck");


            DDDateFADPanel.Text = _oo.ReadDD(_oo.ReturnTag(_sql, "FADDD"));
            DDMonthFADPanel.Text = _oo.ReturnTag(_sql, "FADMM");
            DDYearFADPanel.Text = _oo.ReturnTag(_sql, "FADYY");
        }
        catch
        {
            // ignored
        }
        txtFACPanel.Text = _oo.ReturnTag(_sql, "ClassOfFAC");
        txtFatherNamePanel.Text = _oo.ReturnTag(_sql, "FatherName");
        drpClassPanel.Text = _oo.ReturnTag(_sql, "Class");
        drpClassWithResult.Text = _oo.ReturnTag(_sql, "ClassWithResult");
        drpSexPanel.SelectedItem.Text = _oo.ReturnTag(_sql, "Sex");
        txtContactNoPanel.Text = _oo.ReturnTag(_sql, "FatherContactNo");
        txtAmtPanel.Text = _oo.ReturnTag(_sql, "Amount");
        txtReceviedAmount1.Text = _oo.ReturnTag(_sql, "ReceivedAmount");
        txtConcession1.Text = _oo.ReturnTag(_sql, "Concession");
        txtRemarkPanel.Text = _oo.ReturnTag(_sql, "Remark");
        txtConductPanel.Text = _oo.ReturnTag(_sql, "ConductandWork");
        txtSubjectsPanel.Text = _oo.ReturnTag(_sql, "Subject");
        txtDuesPanel.Text = _oo.ReturnTag(_sql, "Dues");
        txtConcessiontypePanel.Text = _oo.ReturnTag(_sql, "ConcessionType");
        txtTWDPanel.Text = _oo.ReturnTag(_sql, "TWD");
        txtTWDPPanel.Text = _oo.ReturnTag(_sql, "TWDP");
        txtNCCPanel.Text = _oo.ReturnTag(_sql, "NCC");
        txtECAPanel.Text = _oo.ReturnTag(_sql, "ECA");
        txtMotherNamePanel.Text = _oo.ReturnTag(_sql, "MotherName");
        txtAORPanel.Text = _oo.ReturnTag(_sql, "AOR");
        txtResultStatus.Text = _oo.ReturnTag(_sql, "PassStatus");
        DropDownList1.SelectedValue = _oo.ReturnTag(_sql, "Formate");

        string sqlNews = "Select top 1 Pen from StudentGenaralDetail where SrNo='" + txtSrNoPanel.Text + "' and  BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "'";
        txt_pen.Text = _oo.ReturnTag(sqlNews, "Pen");

        Panel2_ModalPopupExtender.Show();


    }
    protected void LinkButtons_Click(object sender, EventArgs e)
    {

        var dd = ""; var ddStruck = "";

        var cmd = new SqlCommand();
        cmd.CommandText = "TCCollectionUpdateProc";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Id", lblID.Text);
        cmd.Parameters.AddWithValue("@srno", txtSrNoPanel.Text.Trim());
        //cmd.Parameters.AddWithValue("@inrollmentNo", txtInrollmentNoPanel.Text.Trim());
        cmd.Parameters.AddWithValue("@bookno", txtBookNoPanel.Text.Trim());
        dd = DDYearP.SelectedItem + "/" + DDMonthP.SelectedItem + "/" + DDDateP.SelectedItem;
        cmd.Parameters.AddWithValue("@AdmissionFromDate", dd);
        ddStruck = DDYearPStruck.SelectedItem + "/" + DDMonthPStruck.SelectedItem + "/" + DDDatePStruck.SelectedItem;
        cmd.Parameters.AddWithValue("@DateOfStruckOff", ddStruck);
        dd = DDYearP1.SelectedItem + "/" + DDMonthP1.SelectedItem + "/" + DDDateP1.SelectedItem;
        cmd.Parameters.AddWithValue("@TCIssueDate", dd);
        dd = DDYearFADPanel.SelectedItem + "/" + DDMonthFADPanel.SelectedItem + "/" + DDDateFADPanel.SelectedItem;
        cmd.Parameters.AddWithValue("@DateOfFAD", dd);
        cmd.Parameters.AddWithValue("@ClassOfFAC", txtFACPanel.Text.Trim());
        cmd.Parameters.AddWithValue("@StudentName", txtStudentNamePanel.Text);
        cmd.Parameters.AddWithValue("@FatherName", txtFatherNamePanel.Text);
        cmd.Parameters.AddWithValue("@Class", drpClassPanel.Text.ToString());
        cmd.Parameters.AddWithValue("@ClassWithResult", drpClassWithResult.Text.ToString());

        cmd.Parameters.AddWithValue("@sex", drpSexPanel.SelectedItem.ToString());
        cmd.Parameters.AddWithValue("@fatherContactNo", txtContactNoPanel.Text);
        cmd.Parameters.AddWithValue("@amount", txtAmtPanel.Text);
        cmd.Parameters.AddWithValue("@Remark", txtRemarkPanel.Text.Trim());
        cmd.Parameters.AddWithValue("@ConductandWork", txtConductPanel.Text.Trim());
        cmd.Parameters.AddWithValue("@Concession", txtConcession1.Text.Trim());
        cmd.Parameters.AddWithValue("@ReceivedAmount", txtReceviedAmount1.Text.Trim());
        cmd.Parameters.AddWithValue("@Subject", txtSubjectsPanel.Text.Trim());
        cmd.Parameters.AddWithValue("@Dues", txtDuesPanel.Text.Trim());
        cmd.Parameters.AddWithValue("@ConcessionType", txtConcessiontypePanel.Text.Trim());
        cmd.Parameters.AddWithValue("@TWD", txtTWDPanel.Text.Trim());
        cmd.Parameters.AddWithValue("@TWDP", txtTWDPPanel.Text.Trim());
        cmd.Parameters.AddWithValue("@NCC", txtNCCPanel.Text.Trim());
        cmd.Parameters.AddWithValue("@ECA", txtECAPanel.Text.Trim());
        cmd.Parameters.AddWithValue("@MotherName", txtMotherNamePanel.Text.Trim());
        cmd.Parameters.AddWithValue("@AOR", txtAORPanel.Text.Trim());
        cmd.Parameters.AddWithValue("@IsQualified", txtIsQualifiedPanel.Text.Trim());
        cmd.Parameters.AddWithValue("@IsQualifiedtowhichclass", txtIsQualifiedtowhichclassPanel.Text.Trim());
        cmd.Parameters.AddWithValue("@PassStatus", txtResultStatus.Text);
        cmd.Parameters.AddWithValue("@template", DropDownList1.SelectedValue);
        cmd.Parameters.AddWithValue("@Pen", txt_pen.Text);
        cmd.Connection = _con;
        try
        {
            _con.Open();
            cmd.ExecuteNonQuery();
            _con.Close();
            txtSrNo.Focus();
            divControls.Visible = false;
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Updated Successfully.", "S");
        }
        catch (Exception ex)
        {
            // ignored
        }
        ShowDeatils();
    }

    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        Button8.Focus();
        var chk = (LinkButton)sender;
        var lblId2 = (Label)chk.NamingContainer.FindControl("Label37");
        var ss = lblId2.Text;
        lblvalue.Text = ss;
        Panel1_ModalPopupExtender.Show();
    }

    protected void Button8_Click(object sender, EventArgs e)
    {

    }
    public void Concession_Values()
    {
        var lblStdSrno = (Label)GrdStudent.Rows[0].FindControl("Label1");
        _con.Open();
        var permissionCheck = new SqlCommand("select count(*) Count from Concession_Permission where TableId='6' and SrNo='" + lblStdSrno.Text + "' and concession<>0 and reset is null and SessionName='" + Session["SessionName"] + "'", _con);
        var permissionCheckCount = (int)permissionCheck.ExecuteScalar();
        _con.Close();
        if (permissionCheckCount == 1)
        {
            _sql = "select concession from Concession_Permission where TableId='6' and SrNo='" + lblStdSrno.Text + "' and concession<>0 and reset is null and SessionName='" + Session["SessionName"] + "'";
            var permission = _oo.ReturnTag(_sql, "concession");
            txtConcession.Text = permission;
        }
        else
        {
            txtConcession.Text = "0";
        }
    }
    public void Permission_Values()
    {
        string sql8;
        sql8 = "select Enable from Admin_fee_permission_setting where Tableid='6' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        if (_oo.ReturnTag(sql8, "Enable") == "" || _oo.ReturnTag(sql8, "Enable") == "No,1,2,3" || _oo.ReturnTag(sql8, "Enable") == "Yes,1,2,3" || _oo.ReturnTag(sql8, "Enable") == "Yes,1" || _oo.ReturnTag(sql8, "Enable") == "Yes,2" || _oo.ReturnTag(sql8, "Enable") == "Yes,3" || _oo.ReturnTag(sql8, "Enable") == "Yes,1,2" || _oo.ReturnTag(sql8, "Enable") == "Yes,1,3" || _oo.ReturnTag(sql8, "Enable") == "Yes,2,3")
        {

            var lblStdSrno = (Label)GrdStudent.Rows[0].FindControl("Label1");
            _con.Open();
            var permissionCheck = new SqlCommand("select count(*) from Administrator_Permission where Tableid='6' and SrNo='" + lblStdSrno.Text + "' and Permission_Session='" + Session["SessionName"] + "' and Permission like 'Yes%'", _con);
            var permissionCheckCount = (int)permissionCheck.ExecuteScalar();
            _con.Close();

            if (permissionCheckCount == 1)
            {

                _sql = "select permission from Administrator_Permission where Tableid='6' and SrNo='" + lblStdSrno.Text + "' and";
                _sql += " Permission_Session='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and Permission like 'Yes%'";
                var permission = _oo.ReturnTag(_sql, "permission").Split(',');
                if (permission.Length > 1)
                {
                    DDYear.Enabled = false;
                    DDMonth.Enabled = false;
                    DDDate.Enabled = false;
                    txtConcession.Enabled = false;
                    for (var i = 1; i <= permission.Length - 1; i++)
                    {
                        if (permission[i] == "1")
                        {
                            DDYear.Enabled = true;
                            DDMonth.Enabled = true;
                            DDDate.Enabled = true;
                        }
                        if (permission[i] == "2")
                        {
                            txtConcession.Enabled = true;
                        }

                    }
                }
                else
                {
                    var permission1 = _oo.ReturnTag(sql8, "Enable").Split(',');
                    if (permission1.Length > 1)
                    {
                        DDYear.Enabled = false;
                        DDMonth.Enabled = false;
                        DDDate.Enabled = false;
                        txtConcession.Enabled = false;

                        if (permission1[0] == "Yes")
                        {
                            for (var i = 1; i <= permission1.Length - 1; i++)
                            {
                                if (permission1[i] == "1")
                                {
                                    DDYear.Enabled = true;
                                    DDMonth.Enabled = true;
                                    DDDate.Enabled = true;
                                }
                                if (permission1[i] == "2")
                                {
                                    txtConcession.Enabled = true;
                                }

                            }
                        }
                    }
                }

            }

            else
            {

                var permission2 = _oo.ReturnTag(sql8, "Enable").Split(',');
                if (permission2.Length > 1)
                {
                    DDYear.Enabled = false;
                    DDMonth.Enabled = false;
                    DDDate.Enabled = false;
                    txtConcession.Enabled = false;

                    if (permission2[0] == "Yes")
                    {
                        for (var i = 1; i <= permission2.Length - 1; i++)
                        {
                            if (permission2[i] == "1")
                            {
                                DDYear.Enabled = true;
                                DDMonth.Enabled = true;
                                DDDate.Enabled = true;
                            }
                            if (permission2[i] == "2")
                            {
                                txtConcession.Enabled = true;
                            }

                        }
                    }
                }
            }
        }
    }

    protected void LinkRecept_Click(object sender, EventArgs e)
    {
        var chk = (LinkButton)sender;
        var lblId3 = (Label)chk.NamingContainer.FindControl("Label18");
        var ss = lblId3.Text;
        Session["IsDuplicate"] = "Yes";
        Session["TCRecieptNo"] = ss;
        Response.Redirect("TcReciept.aspx");
    }


    protected void txtConcession_TextChanged(object sender, EventArgs e)
    {
        CalculateAmount();
    }


    protected void txtConcession1_TextChanged(object sender, EventArgs e)
    {
        CalculateAmount1();
        //Panel1_ModalPopupExtender.Show();
    }
    protected void txtAmtPanel_TextChanged(object sender, EventArgs e)
    {
        CalculateAmount1();
        // Panel1_ModalPopupExtender.Show();
    }
    public void CalculateAmount1()
    {
        double concession;
        double amount;
        double rAmount, cbAmount;
        if (txtConcession1.Text != "")
        {
            concession = Convert.ToDouble(txtConcession1.Text);
        }
        else
        {
            concession = 0;
        }
        amount = Convert.ToDouble(txtAmtPanel.Text);
        double.TryParse(txtAmtPanel.Text, out cbAmount);
        rAmount = amount - concession;
        txtReceviedAmount1.Text = rAmount.ToString(CultureInfo.InvariantCulture);
    }


    protected void txtCBFee_TextChanged(object sender, EventArgs e)
    {

    }



    protected void LinkButton4_Click(object sender, EventArgs e)
    {
    }

    protected void ButtonNo_Click(object sender, EventArgs e)
    {
        Submit.Visible = false;
    }


    protected void btnDelete_Click1(object sender, EventArgs e)
    {
        _sql = "select FatherContactNo,Amount from TCCollection where RecieptNo='" + lblvalue.Text + "' and BranchCode=" + Session["BranchCode"] + " and Cancel is null";
        var fatherContactNo = _oo.ReturnTag(_sql, "FatherContactNo");
        var amount = _oo.ReturnTag(_sql, "Amount");
        _sql = "update TCCollection set Cancel='Y' where RecieptNo='" + lblvalue.Text + "' and BranchCode=" + Session["BranchCode"] + "";
        _oo.ProcedureDatabase(_sql);
        //_sql = "update TCCollection set Cancel='Y' where RecieptNo='" + lblvalue.Text + "'";
        //_oo.ProcedureDatabase(_sql);
        SendFeescancleSms(fatherContactNo, lblvalue.Text, amount);
        //oo.MessageBox("Successfully Cancled.", this.Page);
        Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Cancelled successfully.", "S");


    }



    protected void BtnNo_Click(object sender, EventArgs e)
    {
        Submit.Visible = false;
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
            }
            if (DropDownMOD.SelectedValue == "Card")
            {
                Label42.Text = "Card No.";
                Label4.Text = "Card Date";
            }
            if (DropDownMOD.SelectedValue == "Online Transfer" || DropDownMOD.SelectedValue == "Other")
            {
                Label42.Text = "Ref. No.";
                Label4.Text = "Ref. Date";
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
    protected void DDYearFADPanel_SelectedIndexChanged(object sender, EventArgs e)
    {
        _oo.YearDropDown(DDYearFADPanel, DDMonthFADPanel, DDDateFADPanel);
        Panel2_ModalPopupExtender.Show();//
    }
    protected void DDMonthFADPanel_SelectedIndexChanged(object sender, EventArgs e)
    {
        _oo.MonthDropDown(DDYearFADPanel, DDMonthFADPanel, DDDateFADPanel);
        Panel2_ModalPopupExtender.Show();//
    }
    protected void DDYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        _oo.YearDropDown(DDYear, DDMonth, DDDate);
    }

    protected void DDMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        _oo.MonthDropDown(DDYear, DDMonth, DDDate);
    }
    protected void DDYear1_SelectedIndexChanged(object sender, EventArgs e)
    {
        _oo.YearDropDown(DDYear1, DDMonth1, DDDate1);
    }

    protected void DDMonth1_SelectedIndexChanged(object sender, EventArgs e)
    {
        _oo.MonthDropDown(DDYear1, DDMonth1, DDDate1);
    }
    protected void DDYearStruck_SelectedIndexChanged(object sender, EventArgs e)
    {
        _oo.YearDropDown(DDYearStruck, DDMonthStruck, DDDateStruck);
    }

    protected void DDMonthStruck_SelectedIndexChanged(object sender, EventArgs e)
    {
        _oo.MonthDropDown(DDYearStruck, DDMonthStruck, DDDateStruck);
    }

    protected void DDYearP_SelectedIndexChanged(object sender, EventArgs e)
    {
        _oo.YearDropDown(DDYearP, DDMonthP, DDDateP);
        Panel2_ModalPopupExtender.Show();//
    }

    protected void DDMonthP_SelectedIndexChanged(object sender, EventArgs e)
    {
        _oo.MonthDropDown(DDYearP, DDMonthP, DDDateP);
        Panel2_ModalPopupExtender.Show();//
    }
    protected void DDYearP1_SelectedIndexChanged(object sender, EventArgs e)
    {
        _oo.YearDropDown(DDYearP1, DDMonthP1, DDDateP1);
        Panel2_ModalPopupExtender.Show();//
    }

    protected void DDMonthP1_SelectedIndexChanged(object sender, EventArgs e)
    {
        _oo.MonthDropDown(DDYearP1, DDMonthP1, DDDateP1);
        Panel2_ModalPopupExtender.Show();//
    }
    protected void DDYearPStruck_SelectedIndexChanged(object sender, EventArgs e)
    {
        _oo.YearDropDown(DDYearPStruck, DDMonthPStruck, DDDatePStruck);
        Panel2_ModalPopupExtender.Show();//
    }

    protected void DDMonthPStruck_SelectedIndexChanged(object sender, EventArgs e)
    {
        _oo.MonthDropDown(DDYearPStruck, DDMonthPStruck, DDDatePStruck);
        Panel2_ModalPopupExtender.Show();//
    }
    public override void Dispose()
    {
        _con.Dispose();
        _oo.Dispose();
    }


    protected void ddlCopy_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCopy.SelectedIndex == 0)
        {
            Submit.Visible = false;
        }

        var studentId = Request.Form[hfStudentId.UniqueID];
        if (studentId == string.Empty)
        {
            txtSrNo.Text = txtSrNo.Text.Replace("&", "/").Trim();
            studentId = txtSrNo.Text;
        }
        string sql = "select * from TCCollection where srno='" + studentId + "' and BranchCode=" + Session["BranchCode"] + " and tcType='" + ddlCopy.SelectedValue + "'";
        if (_oo.Duplicate(sql))
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, ddlCopy.SelectedValue + " already issued!", "A");
            Submit.Visible = false;
            return;
        }
        else
        {
            Submit.Visible = true;
        }

        _sql = "select Top 1 SessionName from StudentOfficialDetails where srno='" + studentId + "' and BranchCode=" + Session["BranchCode"] + " and ISNULL(Promotion,'')<>'Cancelled' order By Id Desc";
        if (_oo.Duplicate(_sql))
        {
            var topSessionName = _oo.ReturnTag(_sql, "SessionName");

            _sql = "Select SectionName,Card,Gender,Medium,FatherContactNo,ClassName, combineclassname, Classid, Branchid,convert(nvarchar,DateOfAdmiission,106) as DateOfAdmiission,";
            _sql += " DatePart(year,DateOfAdmiission) as YYYY,Left(DateName(mm,DateOfAdmiission),3) as MMM,DatePart(dd,DateOfAdmiission) as DD ,SectionId,FatherName,";
            _sql += " MotherName,Name,StEnRCode,srno,";
            _sql += " case  when TransportRequired='Yes' then 'Yes' else 'No' end as TransportRequired";
            _sql += ",BranchName,FamilyContactNo,PhotoPath, Withdrwal, Board from AllStudentRecord_UDF('" + topSessionName + "'," + Session["BranchCode"] + ")";
            _sql += " where SrNo ='" + studentId + "'";
            string classid = _oo.ReturnTag(_sql, "Classid");
            string Branchid = _oo.ReturnTag(_sql, "Branchid").ToString();

            txtAmt.Text = "";
            _sql = "select Amount from TCFormFeeMaster where SessionName='" + topSessionName.Trim() + "' and Classid=" + classid + " and Branchid=case when '" + Branchid + "'='' then Branchid else '" + Branchid + "' end  and BranchCode=" + Session["BranchCode"] + " and copytype='" + (ddlCopy.SelectedValue == "" ? "Original" : ddlCopy.SelectedValue) + "'";
            if (_oo.Duplicate(_sql))
            {
                Submit.Visible = true;
                txtAmt.Text = _oo.ReturnTag(_sql, "Amount");
            }
            else
            {
                Submit.Visible = false;
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Please enter Master for " + ddlCopy.SelectedValue + " T.C. first!", "A");
                return;
            }

            txtConcession.Text = "0";

            if (txtAmt.Text == "")
            {
                txtAmt.Text = "0";
            }
            txtReceviedAmount.Text = ((double.Parse(txtAmt.Text) + double.Parse(txtCBFee.Text)) - double.Parse(txtConcession.Text)).ToString();
            Submit.Focus();
        }
    }
}
