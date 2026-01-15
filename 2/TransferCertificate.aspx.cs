using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using c4SmsNew;
using System.Web.UI.HtmlControls;

public partial class TransferCertificate : Page
{
    private SqlConnection _con;
    readonly Campus _oo;
    private string _sql = "";

    public TransferCertificate()
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
            

            _oo.AddDateMonthYearDropDown(DDYearP, DDMonthP, DDDateP);
            _oo.FindCurrentDateandSetinDropDown(DDYearP, DDMonthP, DDDateP);

            _oo.AddDateMonthYearDropDown(DDYearP1, DDMonthP1, DDDateP1);
            _oo.FindCurrentDateandSetinDropDown(DDYearP1, DDMonthP1, DDDateP1);
            
            _oo.AddDateMonthYearDropDown(DDYearFADPanel, DDMonthFADPanel, DDDateFADPanel);
            _oo.FindCurrentDateandSetinDropDown(DDYearFADPanel, DDMonthFADPanel, DDDateFADPanel);

            _oo.AddDateMonthYearDropDown(DDYearPStruck, DDMonthPStruck, DDDatePStruck);
            _oo.FindCurrentDateandSetinDropDown(DDYearPStruck, DDMonthPStruck, DDDatePStruck);

            BAL.objBal.AddDateMonthYearDropDown(drpYY, DrpMM, DrpDD);
            BAL.objBal.FindCurrentDateandSetinDropDown(drpYY, DrpMM, DrpDD);


        }
    }

    protected void TxtEnter_TextChanged(object sender, EventArgs e)
    {

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
        var studentId = Request.Form[hfStudentId.UniqueID];
        if (studentId == string.Empty)
        {
            studentId = txtSrNo.Text.Trim();
        }
        _sql = "select Top 1 SessionName from StudentOfficialDetails where srno='" + studentId + "' and BranchCode=" + Session["BranchCode"] + " and ISNULL(Promotion,'')<>'Cancelled' order By Id Desc";
        if (_oo.Duplicate(_sql))
        {
            var topSessionName = _oo.ReturnTag(_sql, "SessionName");

            _sql = "Select SectionName,Card,Gender,Medium,FatherContactNo,ClassName, combineclassname, Classid, Branchid,convert(nvarchar,DateOfAdmiission,106) as DateOfAdmiission,";
            _sql +=  " DatePart(year,DateOfAdmiission) as YYYY,Left(DateName(mm,DateOfAdmiission),3) as MMM,DatePart(dd,DateOfAdmiission) as DD ,SectionId,FatherName,";
            _sql +=  " MotherName,Name,StEnRCode,srno,";
            _sql +=  " case  when TransportRequired='Yes' then 'Yes' else 'No' end as TransportRequired";
            _sql +=  ",BranchName,FamilyContactNo,PhotoPath, Withdrwal, Board from AllStudentRecord_UDF('" + topSessionName + "'," + Session["BranchCode"] + ")";
            _sql +=  " where SrNo ='" + studentId + "'";
            GrdStudent.DataSource = _oo.Fetchdata(_sql);
            GrdStudent.DataBind();
            string classid = _oo.ReturnTag(_sql, "Classid");
            string classname = _oo.ReturnTag(_sql, "ClassName");
            string Branchid = _oo.ReturnTag(_sql, "Branchid").ToString();
            string Withdrwal = _oo.ReturnTag(_sql, "Withdrwal").ToString();
            string YYYY = _oo.ReturnTag(_sql, "YYYY").ToString();
            string MMM = _oo.ReturnTag(_sql, "MMM").ToString();
            string DD = _oo.ReturnTag(_sql, "DD").ToString();
            string Board = _oo.ReturnTag(_sql, "Board").ToString();
            var dt = _oo.Fetchdata(_sql);


            if (dt.Rows.Count > 0)
            {
                loadIssuied(studentId);



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
                Div1.Visible = false;
            }
        }
    }
    protected void loadIssuied(string studentId)
    {
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
                LinkButton7.NavigateUrl = String.Format("TC_UPBoard.aspx?print={0}", ss);
                //Response.Redirect(String.Format("TC_UPBoard.aspx?print={0}", "1"));
            }
            if (lblFormate.Text == "Template 4")
            {
                LinkButton7.NavigateUrl = String.Format("TCICSE.aspx?print={0}", ss);
                //Response.Redirect(String.Format("TCICSE.aspx?print={0}", "1"));
            }

        }
        for (int i = 0; i < Grd.Rows.Count; i++)
        {
            LinkButton LinkButton1 = (LinkButton)Grd.Rows[i].FindControl("LinkButton1");
            HyperLink LinkButton7 = (HyperLink)Grd.Rows[i].FindControl("LinkButton7");
            Label Status = (Label)Grd.Rows[i].FindControl("Status");
            if (Grd.Rows.Count != (i + 1))
            {
                LinkButton7.Text = "<i class='fa fa-lock'></i>";
                LinkButton7.Enabled = false;
            }
            if (Grd.Rows.Count > 1)
            {
                LinkButton1.Text = "<i class='fa fa-lock'></i>";
                LinkButton1.Enabled = false;
            }
            if (Status.Text.ToLower().Trim()!="paid")
            {
                LinkButton7.Text = "<i class='fa fa-lock'></i>";
                LinkButton7.Enabled = false;
                LinkButton1.Text = "<i class='fa fa-lock'></i>";
                LinkButton1.Enabled = false;
            }
        }
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
            Response.Redirect(String.Format("TC_UPBoard.aspx?print={0}", ss));
        }
        if (lblFormate.Text == "Formate 4")
        {
            Response.Redirect(String.Format("TCICSE.aspx?print={0}", ss));
        }
    }

    
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        var chk = (LinkButton)sender;
        var lblId3 = (Label)chk.NamingContainer.FindControl("Label38");
        var ss = lblId3.Text;
        Session["IsDuplicate"] = "Yes";
        Session["TCRecieptNo"] = ss;
        Response.Redirect(String.Format("TcReciept.aspx?print={0}", "1"));

    }
    //protected void LinkButton1_Click(object sender, EventArgs e)
    //{
    //    var chk = (LinkButton)sender;
    //    var lblId = (Label)chk.NamingContainer.FindControl("Label36");
    //    var ss = lblId.Text;
    //    lblID.Text = ss;
    //    _sql = "select Id,srno,bookno,RecieptNo,convert(nvarchar,AdmissionFromDate,106) as AdmissionFromDate,Concession,";
    //    _sql +=  " ReceivedAmount, StudentName,FatherName,Class, ClassWithResult,LoginName,BranchCode,IsQualified,IsQualifiedtowhichclass,";
    //    _sql +=  " left(convert(nvarchar,AdmissionFromDate,106),2) as DD,Right(left(convert(nvarchar,AdmissionFromDate,106),6),3) as MM , RIGHT(convert(nvarchar,AdmissionFromDate,106),4) as YY,";
    //    _sql +=  " left(convert(nvarchar,DateOfFAD,106),2) as FADDD,Right(left(convert(nvarchar,DateOfFAD,106),6),3) as FADMM , RIGHT(convert(nvarchar,DateOfFAD,106),4) as FADYY,";
    //    _sql +=  " left(convert(nvarchar,ISNULL(TCIssueDate,AdmissionFromDate),106),2) as DD1,Right(left(convert(nvarchar,ISNULL(TCIssueDate,AdmissionFromDate),106),6),3) as MM1 , RIGHT(convert(nvarchar,ISNULL(TCIssueDate,AdmissionFromDate),106),4) as YY1,";
    //    _sql +=  " ClassOfFAC,Subject,Dues,ConcessionType,TWD,TWDP,NCC,ECA,MotherName,convert(nvarchar,RecordDate,106) as RecordDate,";
    //    _sql +=  " Sex,FatherContactNo,Amount,Remark,ConductandWork,AOR, PassStatus from TCCollection where ID=" + int.Parse(lblID.Text) + "  and BranchCode=" + Session["BranchCode"] + "";

    //    txtSrNoPanel.Text = _oo.ReturnTag(_sql, "srno");
    //    txtBookNoPanel.Text = _oo.ReturnTag(_sql, "bookno");
    //    txtStudentNamePanel.Text = _oo.ReturnTag(_sql, "StudentName");
    //    txtIsQualifiedPanel.Text = _oo.ReturnTag(_sql, "IsQualified");
    //    txtIsQualifiedtowhichclassPanel.Text = _oo.ReturnTag(_sql, "IsQualifiedtowhichclass");
    //    try
    //    {
    //        DDDateP.Text = _oo.ReadDD(_oo.ReturnTag(_sql, "DD"));
    //        DDMonthP.Text = _oo.ReturnTag(_sql, "MM");
    //        DDYearP.Text = _oo.ReturnTag(_sql, "YY");


    //        DDDateP1.Text = _oo.ReadDD(_oo.ReturnTag(_sql, "DD1"));
    //        DDMonthP1.Text = _oo.ReturnTag(_sql, "MM1");
    //        DDYearP1.Text = _oo.ReturnTag(_sql, "YY1");


    //        DDDateFADPanel.Text = _oo.ReadDD(_oo.ReturnTag(_sql, "FADDD"));
    //        DDMonthFADPanel.Text = _oo.ReturnTag(_sql, "FADMM");
    //        DDYearFADPanel.Text = _oo.ReturnTag(_sql, "FADYY");
    //    }
    //    catch
    //    {
    //        // ignored
    //    }
    //    txtFACPanel.Text = _oo.ReturnTag(_sql, "ClassOfFAC");
    //    txtFatherNamePanel.Text = _oo.ReturnTag(_sql, "FatherName");
    //    drpClassPanel.Text = _oo.ReturnTag(_sql, "Class");
    //    drpClassWithResult.Text = _oo.ReturnTag(_sql, "ClassWithResult");
    //    drpSexPanel.SelectedItem.Text = _oo.ReturnTag(_sql, "Sex");
    //    txtContactNoPanel.Text = _oo.ReturnTag(_sql, "FatherContactNo");
    //    txtAmtPanel.Text = _oo.ReturnTag(_sql, "Amount");
    //    txtReceviedAmount1.Text = _oo.ReturnTag(_sql, "ReceivedAmount");
    //    txtConcession1.Text = _oo.ReturnTag(_sql, "Concession");
    //    txtRemarkPanel.Text = _oo.ReturnTag(_sql, "Remark");
    //    txtConductPanel.Text = _oo.ReturnTag(_sql, "ConductandWork");
    //    txtSubjectsPanel.Text = _oo.ReturnTag(_sql, "Subject");
    //    txtDuesPanel.Text = _oo.ReturnTag(_sql, "Dues");
    //    txtConcessiontypePanel.Text = _oo.ReturnTag(_sql, "ConcessionType");
    //    txtTWDPanel.Text = _oo.ReturnTag(_sql, "TWD");
    //    txtTWDPPanel.Text = _oo.ReturnTag(_sql, "TWDP");
    //    txtNCCPanel.Text = _oo.ReturnTag(_sql, "NCC");
    //    txtECAPanel.Text = _oo.ReturnTag(_sql, "ECA");
    //    txtMotherNamePanel.Text = _oo.ReturnTag(_sql, "MotherName");
    //    txtAORPanel.Text = _oo.ReturnTag(_sql, "AOR");
    //    txtResultStatus.Text = _oo.ReturnTag(_sql, "PassStatus");
    //    Panel2_ModalPopupExtender.Show();


    //}

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
        _sql += " Sex,FatherContactNo,Amount,Remark,ConductandWork,AOR, PassStatus,Pen from TCCollection where ID=" + int.Parse(lblID.Text) + "  and BranchCode=" + Session["BranchCode"] + "";

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
        txt_pen.Text = _oo.ReturnTag(_sql, "Pen");
        Panel2_ModalPopupExtender.Show();


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
    protected void LinkButtons_Click(object sender, EventArgs e)
    {

       // var dd = "";
        var dd = ""; var ddStruck = "";
        var cmd = new SqlCommand();
        cmd.CommandText = "TCCollectionUpdateProc";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Id", lblID.Text);
        cmd.Parameters.AddWithValue("@srno", txtSrNoPanel.Text.Trim());
        cmd.Parameters.AddWithValue("@bookno", txtBookNoPanel.Text.Trim());
        ddStruck = DDYearPStruck.SelectedItem + "/" + DDMonthPStruck.SelectedItem + "/" + DDDatePStruck.SelectedItem;
        cmd.Parameters.AddWithValue("@DateOfStruckOff", ddStruck);
        dd = DDYearP.SelectedItem + "/" + DDMonthP.SelectedItem + "/" + DDDateP.SelectedItem;
        cmd.Parameters.AddWithValue("@AdmissionFromDate", dd);
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
        cmd.Parameters.AddWithValue("@Pen", txt_pen.Text);
        cmd.Connection = _con;
        try
        {
            _con.Open();
            cmd.ExecuteNonQuery();
            _con.Close();
            txtSrNo.Focus();
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Updated Successfully.", "S");
        }
        catch (Exception ex)
        {
            // ignored
        }
        //}
        //else
        //{
        //    //oo.MessageBox("Please Do Not Press Refresh Button or back Button", this.Page);
        //    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Please Do Not Press Refresh Button or back Button", "A");       

        //}
    }
    //protected void DDYearStruck_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    _oo.YearDropDown(DDYearStruck, DDMonthStruck, DDDateStruck);
    //}
    //protected void DDMonthStruck_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    _oo.MonthDropDown(DDYearStruck, DDMonthStruck, DDDateStruck);
    //}
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
   
    protected void LinkRecept_Click(object sender, EventArgs e)
    {
        var chk = (LinkButton)sender;
        var lblId3 = (Label)chk.NamingContainer.FindControl("Label18");
        var ss = lblId3.Text;
        Session["IsDuplicate"] = "Yes";
        Session["TCRecieptNo"] = ss;
        Response.Redirect("TcReciept.aspx");
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
        //oo.MessageBox("Successfully Cancled.", this.Page);
        Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Cancelled successfully.", "S");


    }



    protected void BtnNo_Click(object sender, EventArgs e)
    {
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

    public override void Dispose()
    {
        _con.Dispose();
        _oo.Dispose();
    }

}
