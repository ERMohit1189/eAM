using System.Data.SqlClient;
using System;

public partial class admin_EmployeeIdcard : System.Web.UI.Page
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
        //con.ConnectionString = ConfigurationManager.ConnectionStrings["Menu"].ConnectionString;
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file
        if (!IsPostBack)
        {
            Label33.Visible = false;
            Label17.Visible = false;
            Label18.Visible = false;
            Label24.Visible = false;
            Label26.Visible = false;
            Label27.Visible = false;
            Label34.Visible = false;
            Label28.Visible = false;
            Label29.Visible = false;
            Label32.Visible = false;
            Image1.Visible = false;
            Label35.Visible = false;
        }
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        getDetails();
    }

    public void getDetails()
    {
        sql = "Select EO.EmpId,convert(nvarchar,EO.RegistrationDate,106) as RegistrationDate,EG.EFirstName+' '+ EG.EMiddleName+' '+ EG.ELastName as Name,EG.EFatherName,EG.EmergencyContactNo,EG.EGender,ED.EPhotoPath";
        sql = sql + "  from EmpployeeOfficialDetails EO";
        sql = sql + " left join EmpGeneralDetail EG on EO.EmpId=EG.EmpId";
        sql = sql + " left join EmpDocuments ED on EO.EmpId=ED.EmpId";
        sql = sql + "    where  EO.EmpId='" + TxtEnter.Text.Trim() + "'";
        sql = sql + " and EO.SessionName='" + Session["SessionName"].ToString() + "' and Eg.BranchCode=" + Session["BranchCode"].ToString() + " and Ed.BranchCode=" + Session["BranchCode"].ToString() + " and EO.BranchCode=" + Session["BranchCode"].ToString() + "";
        if (oo.Duplicate(sql))
        {


            Label33.Visible = true;
            Label17.Visible = true;
            Label18.Visible = true;
            Label24.Visible = true;
            Label26.Visible = true;
            Label27.Visible = true;
            Label34.Visible = true;
            Label28.Visible = true;
            Label29.Visible = true;
            Label32.Visible = true;
            Image1.Visible = true;
            Label35.Visible = true;
            //IdCard1.Visible = true;
            try
            {
                Label27.Text = oo.ReturnTag(sql, "EmpId");
                Label34.Text = oo.ReturnTag(sql, "RegistrationDate");

                string ss = "";
                ss =
                Label28.Text = oo.ReturnTag(sql, "Name");


#pragma warning disable 219
                string S = "";
#pragma warning restore 219
#pragma warning disable 219
                string D = "";
#pragma warning restore 219
#pragma warning disable 219
                string o = "";
#pragma warning restore 219
                string kk = "";



                kk = oo.ReturnTag(sql, "EGender");
                if (kk == "Female")
                {
                    Label18.Text = "D/o";
                }
                else
                {
                    Label18.Text = "S/o";
                }

                Label29.Text = oo.ReturnTag(sql, "EFatherName");
                Label32.Text = oo.ReturnTag(sql, "EmergencyContactNo");
                Image1.ImageUrl = oo.ReturnTag(sql, "EPhotoPath");
                Label35.Text = "*" + Label27.Text + "*";
            }
            catch (Exception) { }
        }
        else
        {
            //oo.MessageBox("Sorry, No Record(s) found!", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox1, "Sorry, No Record(s) found!", "A");



            Label33.Visible = false;
            Label17.Visible = false;
            Label18.Visible = false;
            Label24.Visible = false;
            Label26.Visible = false;
            Label27.Visible = false;
            Label34.Visible = false;
            Label28.Visible = false;
            Label29.Visible = false;
            Label32.Visible = false;
            Image1.Visible = false;
            Label35.Visible = false;
            //IdCard1.Visible = false;
        }
    }
    
    protected void DrpEnter_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void TxtEnter_TextChanged(object sender, EventArgs e)
    {
        getDetails();
    }
}