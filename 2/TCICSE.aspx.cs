using System;
using System.Data.SqlClient;
using System.Web.UI;


public partial class TCICSE : Page
{
    private SqlConnection con;
    private readonly Campus oo;
    private string sql;
    string id = "";
    public TCICSE()
    {
        con = new SqlConnection();
        oo = new Campus();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        con = oo.dbGet_connection();
        BLL.BLLInstance.LoadHeader("Certificate", header1);
        if (!IsPostBack)
        {
            sql = "Select AffiliationNo,SchoolNo from CollegeMaster where CollegeId=" + Session["BranchCode"] + "";

            lblaffno.Text = oo.ReturnTag(sql, "AffiliationNo");
            if (oo.ReturnTag(sql, "AffiliationNo") == "")
            {
                lblaf.Visible = false;
            }
            loaddata();
        }
    }
    public void loaddata()
    {
        sql = "select tc.id sn, tcType, Name, asr.SrNo, asr.FatherName,asr.MotherName, ClassName, DateOfAdmiission, Board, DOB, convert( Varchar(11),DOB,106) as DOB1,Pen,ApaarId,ClassWithResult from TCCollection tc inner join AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ")asr on asr.SrNo = tc.srno and asr.SessionName = tc.SessionName and asr.BranchCode = tc.BranchCode and tc.RecieptNo = '" + Request.QueryString["print"].ToString() + "'";
        string srno = oo.ReturnTag(sql, "SrNo");
        lblApaarID.Text = oo.ReturnTag(sql, "ApaarId");
        lblUdisePen.Text = oo.ReturnTag(sql, "Pen");
        lblLastSchool.Text = oo.ReturnTag(sql, "ClassWithResult");
        Label31.Text = srno;

        string ss = "select top(1) SchoolName from StudentPreviousSchool where SrNo='" + srno + "' and BranchCode=" + Session["BranchCode"] + " order by id desc";
        string ss1 = "select top(1) format(FromDate, 'MMM-yyyy')sessionStartFrom, format(ToDate, 'MMM-yyyy')sessionStartTo from SessionMaster where BranchCode=" + Session["BranchCode"] + " order by SessionId desc";
        string ss2 = "select top(1) format(WithdrawalDate, 'dd-MMM-yyyy')Dateofwithdrwal, Remark, status from StudentWithdrawal where BranchCode=" + Session["BranchCode"] + " and SrNo='" + srno + "' order by WithdrawalId desc";

        tcCopy.Text = oo.ReturnTag(sql, "tcType");

        Label30.Text = oo.ReturnTag(sql, "sn");
        Label3.Text = oo.ReturnTag(sql, "Name");
        Label5.Text = oo.ReturnTag(sql, "FatherName");
        lblMotherName.Text = oo.ReturnTag(sql, "MotherName");
        Label4.Text = oo.ReturnTag(sql, "DateOfAdmiission");
        Label2.Text = oo.ReturnTag(sql, "ClassName");
        tr.Visible = false;
        if (oo.ReturnTag(ss, "SchoolName") != "")
        {
            tr.Visible = true;
            Label6.Text = oo.ReturnTag(ss, "SchoolName");
        }

        Label1.Text = oo.ReturnTag(ss2, "Dateofwithdrwal");
        Label7.Text = oo.ReturnTag(ss2, "Remark");

        Label13.Text = oo.ReturnTag(sql, "Board");
        Label8.Text = oo.ReturnTag(ss1, "sessionStartFrom");
        Label9.Text = oo.ReturnTag(ss1, "sessionStartTo");
        Label10.Text = oo.ReturnTag(sql, "DOB");
        if (oo.ReturnTag(sql, "DOB") != "")
        {
            Label11.Text = oo.Date_in_Words(Convert.ToInt32(Label10.Text.Substring(0, 2))) + " - " + oo.ReturnTag(sql, "DOB1").Substring(3, 3) + " - " + oo.Date_in_Words(Convert.ToInt32(Label10.Text.Substring(6, 5)));
        }
        Label12.Text = oo.ReturnTag(ss2, "status");
    }
    protected void lnkPrint_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }
}