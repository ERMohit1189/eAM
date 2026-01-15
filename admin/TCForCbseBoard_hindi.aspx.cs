using System;
using System.Web.UI;
using System.Data.SqlClient;

public partial class admin_TCForCbseBoard : Page
{
    SqlConnection con = new SqlConnection();
#pragma warning disable 169
    SqlDataAdapter da;
#pragma warning restore 169
    Campus oo = new Campus();
    string sql= String.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        con = oo.dbGet_connection();
        BLL.BLLInstance.LoadCertificateHeader(header1);
        if (!IsPostBack)
        {
            sql = "Select AffiliationNo,SchoolNo from CollegeMaster where CollegeId='"+Session["BranchCode"].ToString()+"'";
            Label1.Text = oo.ReturnTag(sql, "AffiliationNo");
            lblaffno.Text = oo.ReturnTag(sql, "AffiliationNo");
            Label2.Text = oo.ReturnTag(sql, "SchoolNo");
            loaddata();
        }
    }

    public void loaddata()
    {
        sql = "select Id,srno,bookno,convert(nvarchar,AdmissionFromDate,106) as AdmissionFromDate,convert(nvarchar,ISNULL(TCIssueDate,AdmissionFromDate),106) as TCIssueDate,Concession,";
        sql = sql + " ReceivedAmount,StudentName,FatherName,Class,LoginName,BranchCode,";
        sql = sql + " Subject,Dues,ConcessionType,TWD,TWDP,NCC,ECA,MotherName,convert(nvarchar,RecordDate,106) as RecordDate,convert(nvarchar,DateOfFAD,106) as DateOfFAD,ClassOfFAC,";
        sql = sql + " Sex,FatherContactNo,Amount,Remark,ConductandWork,AOR,IsQualified,IsQualifiedtowhichclass from TCCollection where RecieptNo='" + Session["TCRecieptNo"].ToString() + "'";

        Label30.Text = oo.ReturnTag(sql, "Id");
        Label31.Text = oo.ReturnTag(sql, "srno");
        Label32.Text = oo.ReturnTag(sql, "bookno");


        Label16.Text = oo.ReturnTag(sql, "Subject");
        Label20.Text = oo.ReturnTag(sql, "Dues");
        Label21.Text = oo.ReturnTag(sql, "ConcessionType");
        Label22.Text = oo.ReturnTag(sql, "TWD");
        Label23.Text = oo.ReturnTag(sql, "TWDP");
        Label24.Text = oo.ReturnTag(sql, "NCC");
        Label25.Text = oo.ReturnTag(sql, "ECA");
        Label26.Text = oo.ReturnTag(sql, "ConductandWork");
        Label27.Text = oo.ReturnTag(sql, "AdmissionFromDate");
        Label28.Text = oo.ReturnTag(sql, "TCIssueDate");
        Label29.Text = oo.ReturnTag(sql, "Remark");
           
        Label33.Text = oo.ReturnTag(sql, "AOR");

        Label17.Text = oo.ReturnTag(sql, "IsQualified");
        Label18.Text = oo.ReturnTag(sql, "IsQualifiedtowhichclass");

        Label8.Text = oo.ReturnTag(sql, "DateOfFAD");
        Label9.Text = oo.ReturnTag(sql, "ClassOfFAC");

        string oldsessionname = "";
        string Top_sessionName = Session["Top_sessionName"].ToString();

        sql = "Select *from StudentOfficialDetails where SrNo='" + Label31.Text + "' and SessionNAme='" + Session["SessionName"].ToString() + "'";
        if (oo.Duplicate(sql))
        {
            sql = "Select Top 1 SessionName from (Select Top 2 SessionName,id from StudentOfficialDetails where SrNo='" + Label31.Text + "' order by id Desc) As T1 order by id";
            oldsessionname = oo.ReturnTag(sql, "SessionName");
        }
        else
        {
            sql = "Select Top 1 SessionName from StudentOfficialDetails where SrNo='" + Label31.Text + "' order by id Desc";
            oldsessionname = oo.ReturnTag(sql, "SessionName");
        }
        sql = "Select convert(nvarchar,DateOfAdmiission,106) as DateOfAdmiission,ClassName from AllStudentRecord_UDF('" + oldsessionname + "'," + Session["BranchCode"] + ") where SrNo='" + Label31.Text + "'";
        string classname = oo.ReturnTag(sql, "ClassName");

        sql = "Select Name StudentName,FatherName,MotherName,Nationality,Category,convert(nvarchar,DOB,106) as DOB,DatePart(dd,DOB) as date,DateName(mm,DOB) as month,DatePart(yyyy,DOB) as year from AllStudentRecord_UDF('" + oldsessionname + "'," + Session["BranchCode"].ToString() + ") where SrNo='" + Label31.Text + "'";
        Label3.Text = oo.ReturnTag(sql, "StudentName");
        Label4.Text = oo.ReturnTag(sql, "FatherName");
        Label5.Text = oo.ReturnTag(sql, "MotherName");

        Label6.Text = oo.ReturnTag(sql, "Nationality");
        Label7.Text = oo.ReturnTag(sql, "Category");
        Label10.Text = oo.ReturnTag(sql, "DOB");
        Label11.Text = oo.Date_in_Words(Convert.ToInt16(oo.ReturnTag(sql, "date"))) + " " + oo.ReturnTag(sql, "month") + " " + oo.Date_in_Words(Convert.ToInt16(oo.ReturnTag(sql, "year")));

        string status = "";
        sql = "Select Status From StudentWithdrawal where srno='" + Label31.Text + "'";
        status = oo.ReturnTag(sql, "Status");

      
        if (status.ToUpper() == "PASSED")
        {

            Label13.Text = classname;
            Label14.Text = oo.ReturnTag(sql, "Status");
            Label15.Text = "NO";
        }
        else if (status.ToUpper() == "FAILED")
        {

            Label13.Text = classname;
            if (BAL.objBal.convertRomantostring(classname).ToUpper() == "TENTH" || BAL.objBal.convertRomantostring(classname).ToUpper() == "TWELTH")
            {
                Label14.Text = "FAILED";
            }
            else
            {
                Label14.Text = "DETAINED";
            }           
            Label15.Text = "YES";
               
        }
        else if (status.ToUpper() == "ABSENT")
        {

            Label13.Text = classname;
   
            Label14.Text = "ABSENT";

            Label15.Text = "ABSENT";
            
        }
        else if (status.ToUpper() == "LEFT")
        {
            string sessionname = "";
            sql = "Select *from StudentOfficialDetails where SrNo='" + Label31.Text + "' and SessionNAme='" + Session["SessionName"].ToString() + "'";
            if (oo.Duplicate(sql))
            {
                sessionname = Session["SessionName"].ToString();

                sql = "Select ClassName from StudentOfficialDetails so inner join ClassMaster cm on cm.Id=so.AdmissionForClassId";
                sql = sql + " where SrNo='" + Label31.Text + "' and cm.SessionName='" + sessionname + "' and so.SessionName='" + sessionname + "'";
                if (oo.ReturnTag(sql, "ClassName") != "")
                {
                    Label13.Text = oo.ReturnTag(sql, "ClassName");
                }
                else
                {
                    sql = "Select ClassName from StudentOfficialDetails so inner join ClassMaster cm on cm.Id=so.AdmissionForClassId";
                    sql = sql + " where SrNo='" + Label31.Text + "' and cm.SessionName='" + Top_sessionName + "' and so.SessionName='" + Top_sessionName + "'";
                    Label13.Text = oo.ReturnTag(sql, "ClassName");
                }
            }
            else
            {
                Label13.Text = classname;
            }

            Label14.Text = "Appearing";
            Label15.Text = "NO"; 
        }
        else
        {
            string secondlastsessionid="";
            sql = "Select *from StudentOfficialDetails where SrNo='" + Label31.Text + "' and SessionNAme='" + Session["SessionName"].ToString() + "'";
            if (oo.Duplicate(sql))
            {
                sql = "SELECT TOP 1 SessionId From(select Top 2 SessionId from SessionMaster ORDER BY SessionId DESC) x ORDER BY SessionId";
                secondlastsessionid = oo.ReturnTag(sql, "SessionId");
            }
            else
            {
                sql = "select Top 1 SessionId from SessionMaster ORDER BY SessionId DESC";
                secondlastsessionid = oo.ReturnTag(sql, "SessionId");
            }
            
            sql = "Select SessionName from SessionMaster where SessionId='" + secondlastsessionid + "'";
            string sessionname = oo.ReturnTag(sql, "SessionName");
            sql = "Select ClassName from StudentOfficialDetails so inner join ClassMaster cm on cm.Id=so.AdmissionForClassId";
            sql = sql + " where SrNo='" + Label31.Text + "' and cm.SessionName='" + sessionname + "' and so.SessionName='" + sessionname + "'";
            if (oo.ReturnTag(sql, "ClassName") != "")
            {
                Label13.Text = oo.ReturnTag(sql, "ClassName");
            }
            else
            {
                sql = "Select ClassName from StudentOfficialDetails so inner join ClassMaster cm on cm.Id=so.AdmissionForClassId";
                sql = sql + " where SrNo='" + Label31.Text + "' and cm.SessionName='" + Top_sessionName + "' and so.SessionName='" + Top_sessionName + "'";
                Label13.Text = oo.ReturnTag(sql, "ClassName");
            }
            Label14.Text = "PASSED";
            Label15.Text = "NO";          
        }
        string sessionnamed = "";
        sql = "Select *from StudentOfficialDetails where SrNo='" + Label31.Text + "' and SessionNAme='" + Session["SessionName"].ToString() + "'";
        if (oo.Duplicate(sql))
        {
            sql = "Select Top 1 SessionName from (Select Top 2 SessionName,id from StudentOfficialDetails where SrNo='" + Label31.Text + "' order by id Desc) As T1 order by id";
             sessionnamed = oo.ReturnTag(sql, "SessionName");
        }
        else
        {
            sql = "Select Top 1 SessionName from StudentOfficialDetails where SrNo='" + Label31.Text + "' order by id Desc";
             sessionnamed = oo.ReturnTag(sql, "SessionName");
        }
        
        sql = "Select ClassName from StudentOfficialDetails so inner join ClassMaster cm on cm.Id=so.AdmissionForClassId";
        sql = sql + " where SrNo='" + Label31.Text + "' and cm.SessionName='" + sessionnamed + "' and so.SessionName='" + sessionnamed + "'";
        Label12.Text = oo.ReturnTag(sql, "ClassName");
        
        if (Label12.Text.Contains("Nur"))
        {
            Label19.Text = Label12.Text;
        }
        else
        {
            Label19.Text = BAL.objBal.convertRomantostring(Label12.Text);
        }
    }


    private void loadNextClass()
    {
        string sessionnamed = "";
        string classid = "";
        sql = "Select *from StudentOfficialDetails where SrNo='" + Label31.Text + "' and SessionNAme='" + Session["SessionName"].ToString() + "'";
        if (oo.Duplicate(sql))
        {
            sql = "Select Top 1 SessionName,AdmissionForClassId from (Select Top 2 SessionName,id,AdmissionForClassId from StudentOfficialDetails where SrNo='" + Label31.Text + "' order by id Desc) As T1 order by id";
            sessionnamed = oo.ReturnTag(sql, "SessionName");
            classid = oo.ReturnTag(sql, "AdmissionForClassId");
        }
        else
        {
            sql = "Select Top 1 SessionName,AdmissionForClassId from StudentOfficialDetails where SrNo='" + Label31.Text + "' order by id Desc";
            sessionnamed = oo.ReturnTag(sql, "SessionName");
            classid = oo.ReturnTag(sql, "AdmissionForClassId");
        }


        sql = "Select Top 1 ClassName from ClassMaster where CIDOrder=(Select CIDOrder+1 from ClassMaster where SessionName='" + sessionnamed + "' and id='" + classid + "') and SessionName='" + sessionnamed + "'";

        if (oo.ReturnTag(sql, "ClassName") != string.Empty)
        {
            Label18.Text = oo.ReturnTag(sql, "ClassName");
        }
        else
        {
            Label18.Text = "HIGHER STUDIES";
        }
    }
           
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    //protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    //{
    //    PrintHelper_New.ctrl = abc;
    //    ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    //}

    //protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    //{      
    //    oo.ExportToWord(Response, "TC.doc", divexport);       
    //}
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }
}