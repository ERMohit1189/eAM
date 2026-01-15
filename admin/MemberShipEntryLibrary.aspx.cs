using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;
public partial class admin_MemberShipEntryLibrary : Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        //con.ConnectionString = ConfigurationManager.ConnectionStrings["Menu"].ConnectionString;
        con = oo.dbGet_connection();
        if (!IsPostBack)
        {

            try
            {
                CheckValueADDDeleteUpdate();
            }
            catch (Exception) { }
            
            
            
            
            
            oo.AddDateMonthYearDropDown(DrpYY, DrpMM, DrpDD);
            oo.AddDateMonthYearDropDown(DrpYY0, DrpMM0, DrpDD0);
            oo.FindCurrentDateandSetinDropDown(DrpYY, DrpMM, DrpDD);
            oo.FindCurrentDateandSetinDropDown(DrpYY0, DrpMM0, DrpDD0);

            oo.AddDateMonthYearDropDown(DrpYY1, DrpMM1, DrpDD1);
            oo.AddDateMonthYearDropDown(DrpYY2, DrpMM2, DrpDD2);





            Panel1.Visible = false;
            LinkButton3.Visible = false;


            string M = "";
            try
            {

                sql = "select MAX(Id)+1 AS Srno from MemberEntryLibrary ";
                if (oo.ReturnTag(sql, "Srno") == "")
                {
                    M = "M" + "1";
                }
                else
                {
                    M = "M" + oo.ReturnTag(sql, "Srno");
                }

            }
            catch (Exception) { M = " M " + "1"; }

            TextBox1.Text = M;

        }
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        string card = string.Empty;
        sql = "select SG.Id, SC.SectionName,SO.Card,SO.Medium as Medium,CM.ClassName,convert(nvarchar,So.DateOfAdmiission,106) as DateOfAdmiission ,SO.SectionId,Sf.FatherName,SF.MotherName,SG.FirstName,SG.MiddleName,SG.LastName,sg.StEnRCode as StEnRCode,sg.srno  as srno,case  when so.TransportRequired='Yes' then 'Yes' else 'No' end as TransportRequired,so.wayamount as wayamount from StudentGenaralDetail SG ";
        sql = sql + "    left join StudentFamilyDetails SF on SG.StEnRCode=SF.StEnRCode";
        sql = sql + "   left join StudentOfficialDetails SO on SG.StEnRCode=SO.StEnRCode";
        sql = sql + "   left join ClassMaster CM on SO.AdmissionForClassId=CM.Id";
        sql = sql + "   left join SectionMaster SC on SO.SectionId=SC.Id";
        sql = sql + "    where  SG." + DrpEnter.SelectedValue.ToString() + "=" + "'" + TxtEnter.Text.Trim() + "'";
        sql = sql + "  and sg.SessionName='" + Session["SessionName"].ToString() + "' and sg.BranchCode=" + Session["BranchCode"].ToString() + "";
        sql = sql + "  and SO.SessionName='" + Session["SessionName"].ToString() + "'";
        sql = sql + "  and Sf.SessionName='" + Session["SessionName"].ToString() + "'";
        sql = sql + "  and cm.SessionName='" + Session["SessionName"].ToString() + "'";
        sql = sql + "  and Sc.SessionName='" + Session["SessionName"].ToString() + "'";
        sql = sql + "  and SO.Withdrwal is null";
         
        Grd.DataSource = oo.GridFill(sql);
        Grd.DataBind();
        Panel1.Visible = true;
        LinkButton3.Visible = true;
        string M = "";
        try
        {

            sql = "select MAX(Id)+1 AS Srno from MemberEntryLibrary ";
            if (oo.ReturnTag(sql, "Srno") == "")
            {
                M = "M" + "1";
            }
            else
            {
                M = "M" + oo.ReturnTag(sql, "Srno");
            }

        }
        catch (Exception) { M = " M " + "1"; }

        TextBox1.Text = M;

        if (Grd.Rows.Count == 0)
        {
            oo.MessageBox("Sorry, No Record(s) found!", this.Page);
            Panel1.Visible = false;
            LinkButton3.Visible = false;
            GridView1.Visible = false;
        }
        else
        {
            DisplayData();
        }
    }
    protected void DrpEnter_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void TxtEnter_TextChanged(object sender, EventArgs e)
    {

    }
    protected void DrpYY_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(DrpYY, DrpMM, DrpDD);
    }
    protected void DrpMM_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(DrpYY, DrpMM, DrpDD);
    }
    protected void DrpDD_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void DrpYY0_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(DrpYY0, DrpMM0, DrpDD0);
    }
    protected void DrpMM0_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(DrpYY0, DrpMM0, DrpDD0);
    }
    protected void DrpDD0_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        string sql1 = "";

        sql1 = "Select * from MemberEntryLibrary where  " + DrpEnter.SelectedValue.ToString() + "=" + "'" + TxtEnter.Text.Trim() + "'";
        if (oo.Duplicate(sql1))
        {
            oo.MessageBox("Already exist", this.Page);
        }
        //AlTER Procedure [dbo].[MemberEntryLibraryProce](@MemberCode nvarchar(50),@MemberClass nvarchar(50),
        //@MembershipDate smalldatetime ,@MembershipValidUpto smalldatetime,
        //@SrNo nvarchar(50),@StEnRCode nvarchar(25),@SessionName nvarchar(50),@BranchCode int,@LoginName nvarchar(50))
        else
        {

            sql = "select SG.Id, SC.SectionName,SO.Card,SO.Medium as Medium,CM.ClassName as ClassName,convert(nvarchar,So.DateOfAdmiission,106) as DateOfAdmiission ,SO.SectionId,Sf.FatherName,SF.MotherName,SG.FirstName,SG.MiddleName,SG.LastName,sg.StEnRCode as StEnRCode,sg.srno  as srno,case  when so.TransportRequired='Yes' then 'Yes' else 'No' end as TransportRequired,so.wayamount as wayamount from StudentGenaralDetail SG ";
            sql = sql + "    left join StudentFamilyDetails SF on SG.StEnRCode=SF.StEnRCode";
            sql = sql + "   left join StudentOfficialDetails SO on SG.StEnRCode=SO.StEnRCode";
            sql = sql + "   left join ClassMaster CM on SO.AdmissionForClassId=CM.Id";
            sql = sql + "   left join SectionMaster SC on SO.SectionId=SC.Id";
            sql = sql + "   where  SG." + DrpEnter.SelectedValue.ToString() + "=" + "'" + TxtEnter.Text.Trim() + "'";

            sql = sql + "  and sg.SessionName='" + Session["SessionName"].ToString() + "' and sg.BranchCode=" + Session["BranchCode"].ToString() + "";
            sql = sql + "  and SO.SessionName='" + Session["SessionName"].ToString() + "'";
            sql = sql + "  and Sf.SessionName='" + Session["SessionName"].ToString() + "'";
            sql = sql + "  and cm.SessionName='" + Session["SessionName"].ToString() + "'";
            sql = sql + "  and Sc.SessionName='" + Session["SessionName"].ToString() + "'";
            sql = sql + "  and SO.Withdrwal is null";



            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "MemberEntryLibraryProce";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MemberCode", TextBox1.Text.ToString());
            cmd.Parameters.AddWithValue("@MemberClass", oo.ReturnTag(sql, "ClassName"));

            string MDate = "";
            string MVDate = "";
            MDate = DrpYY.SelectedItem.ToString() + "/" + DrpMM.SelectedItem.ToString() + "/" + DrpDD.SelectedItem.ToString();
            MVDate = DrpYY0.SelectedItem.ToString() + "/" + DrpMM0.SelectedItem.ToString() + "/" + DrpDD0.SelectedItem.ToString();

            cmd.Parameters.AddWithValue("@MembershipDate", MDate);
            cmd.Parameters.AddWithValue("@MembershipValidUpto", MVDate);
            cmd.Parameters.AddWithValue("@SrNo", oo.ReturnTag(sql, "srno"));
            cmd.Parameters.AddWithValue("@StEnRCode", oo.ReturnTag(sql, "StEnRCode"));



            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());//Session["Login"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
            cmd.Parameters.AddWithValue("@TypeOfPerson", "Student");
            cmd.Connection = con;
            try
            {

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                oo.MessageBox("Submitted successfully.", this.Page);

                DisplayData();
                string M = "";
                try
                {

                    sql = "select MAX(Id)+1 AS Srno from MemberEntryLibrary ";
                    if (oo.ReturnTag(sql, "Srno") == "")
                    {
                        M = "M" + "1";
                    }
                    else
                    {
                        M = "M" + oo.ReturnTag(sql, "Srno");
                    }

                }
                catch (Exception) { M = " M " + "1"; }

                TextBox1.Text = M;

            }
            catch (Exception) { }
        }
    }

    public void DisplayData()
    {
        sql = "Select  Row_Number() over (order by Id Asc) as SNo,Id,MemberCode ,MemberClass ,convert(nvarchar,MembershipDate,106) as  MembershipDate,convert(nvarchar,MembershipValidUpto,106) as MembershipValidUpto ,";
        sql = sql + "   SrNo ,StEnRCode ,SessionName ,BranchCode ,LoginName,RecordDate from  MemberEntryLibrary where TypeOfPerson='Student' and   " + DrpEnter.SelectedValue.ToString() + "=" + "'" + TxtEnter.Text.Trim() + "'";
        GridView1.DataSource = oo.GridFill(sql);
        GridView1.DataBind();
        GridView1.Visible = true;




    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        string ss = chk.Text;
        lblID.Text = ss;
        sql = "Select  Row_Number() over (order by Id Asc) as SNo,Id,MemberCode ,MemberClass ,";
        sql = sql + "   left(convert(nvarchar,MembershipDate,106),2) as DD,Right(left(convert(nvarchar,MembershipDate,106),6),3) as MM , RIGHT(convert(nvarchar,MembershipDate,106),4) as YY,";
        sql = sql + "   left(convert(nvarchar,MembershipValidUpto,106),2) as DD1,Right(left(convert(nvarchar,MembershipValidUpto,106),6),3) as MM1 , RIGHT(convert(nvarchar,MembershipValidUpto,106),4) as YY1,";
        sql = sql + "   SrNo ,StEnRCode ,SessionName ,BranchCode ,LoginName,RecordDate from  MemberEntryLibrary where Id=" + ss;

        Label36.Text = oo.ReturnTag(sql, "SrNo");
        TextBox2.Text = oo.ReturnTag(sql, "MemberCode");
        DrpDD1.Text = oo.ReadDD(oo.ReturnTag(sql, "DD"));
        DrpMM1.Text = oo.ReturnTag(sql, "MM");
        DrpYY1.Text = oo.ReturnTag(sql, "YY");

        DrpDD2.Text = oo.ReadDD(oo.ReturnTag(sql, "DD1"));
        DrpMM2.Text = oo.ReturnTag(sql, "MM1");
        DrpYY2.Text = oo.ReturnTag(sql, "YY1");

        Panel2_ModalPopupExtender.Show();

    }
    protected void Button4_Click(object sender, EventArgs e)
    {

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        //      ALTER Procedure [dbo].[MemberEntryLibraryUpdateProce](@Id int ,@MemberCode nvarchar(50),@MemberClass nvarchar(50),
        //@MembershipDate smalldatetime ,@MembershipValidUpto smalldatetime,
        //@SrNo nvarchar(50),@StEnRCode nvarchar(25),@SessionName nvarchar(50),@BranchCode int,@LoginName nvarchar(50))


        sql = "select SG.Id, SC.SectionName,SO.Card,SO.Medium as Medium,CM.ClassName as ClassName,convert(nvarchar,So.DateOfAdmiission,106) as DateOfAdmiission ,SO.SectionId,Sf.FatherName,SF.MotherName,SG.FirstName,SG.MiddleName,SG.LastName,sg.StEnRCode as StEnRCode,sg.srno  as srno,case  when so.TransportRequired='Yes' then 'Yes' else 'No' end as TransportRequired,so.wayamount as wayamount from StudentGenaralDetail SG ";
        sql = sql + "    left join StudentFamilyDetails SF on SG.StEnRCode=SF.StEnRCode";
        sql = sql + "   left join StudentOfficialDetails SO on SG.StEnRCode=SO.StEnRCode";
        sql = sql + "   left join ClassMaster CM on SO.AdmissionForClassId=CM.Id";
        sql = sql + "   left join SectionMaster SC on SO.SectionId=SC.Id";
        sql = sql + "    where  SG." + DrpEnter.SelectedValue.ToString() + "=" + "'" + TxtEnter.Text.Trim() + "'";
        sql = sql + "  and SO.SessionName='" + Session["SessionName"].ToString() + "'";
        sql = sql + "  and Sf.SessionName='" + Session["SessionName"].ToString() + "'";
        sql = sql + "  and cm.SessionName='" + Session["SessionName"].ToString() + "'";
        sql = sql + "  and Sc.SessionName='" + Session["SessionName"].ToString() + "'";
        sql = sql + "  and SO.Withdrwal is null";




        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "MemberEntryLibraryUpdateProce";
        cmd.CommandType = CommandType.StoredProcedure;


        cmd.Parameters.AddWithValue("@Id", lblID.Text);
        cmd.Parameters.AddWithValue("@MemberCode", TextBox2.Text.ToString());
        cmd.Parameters.AddWithValue("@MemberClass", oo.ReturnTag(sql, "ClassName"));

        string MDate1 = "";
        string MVDate2 = "";
        MDate1 = DrpYY1.SelectedItem.ToString() + "/" + DrpMM1.SelectedItem.ToString() + "/" + DrpDD1.SelectedItem.ToString();
        MVDate2 = DrpYY2.SelectedItem.ToString() + "/" + DrpMM2.SelectedItem.ToString() + "/" + DrpDD2.SelectedItem.ToString();

        cmd.Parameters.AddWithValue("@MembershipDate", MDate1);
        cmd.Parameters.AddWithValue("@MembershipValidUpto", MVDate2);
        cmd.Parameters.AddWithValue("@SrNo", oo.ReturnTag(sql, "srno"));
        cmd.Parameters.AddWithValue("@StEnRCode", oo.ReturnTag(sql, "StEnRCode"));



        cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());//Session["Login"].ToString());
        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
        cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
        cmd.Connection = con;
        try
        {

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            oo.MessageBox("Updated successfully.", this.Page);

            DisplayData();


        }
        catch (Exception) { }
    }
    protected void DrpYY1_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(DrpYY1, DrpMM1, DrpDD1);
    }
    protected void DrpMM1_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(DrpYY1, DrpMM1, DrpDD1);
    }
    protected void DrpDD1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void DrpYY2_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(DrpYY2, DrpMM2, DrpDD2);
    }
    protected void DrpMM2_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(DrpYY2, DrpMM2, DrpDD2);
    }
    protected void DrpDD2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void LinkButton5_Click(object sender, EventArgs e)
  {
        LinkButton chk = (LinkButton)sender;
    string ss = chk.Text;
    lblvalue.Text = ss.ToString();
    Panel3_ModalPopupExtender.Show();
  }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        sql = "Delete from MemberEntryLibrary where Id=" + lblvalue.Text;

        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = sql;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            oo.MessageBox("Deleted successfully.", this.Page);
            DisplayData();
        }
        catch (SqlException)
        {
        }
    }
    protected void Button8_Click(object sender, EventArgs e)
    {

    }



    public void PermissionGrant(int add1, int delete1, int update1, LinkButton Ladd, Button Ldelete, Button LUpdate)
    {


        if (add1 == 1)
        {
            Ladd.Enabled = true;
        }
        else
        {
            Ladd.Enabled = false;
        }


        if (delete1 == 1)
        {
            Ldelete.Enabled = true;
        }
        else
        {
            Ldelete.Enabled = false;
        }

        if (update1 == 1)
        {
            LUpdate.Enabled = true;
        }
        else
        {
            LUpdate.Enabled = false;
        }


    }
    public void CheckValueADDDeleteUpdate()
    {
        sql = " select LoginId,LoginName,Pass,SessionId,BranchId,LT.LoginTypeName,ltb.add1 as add1,ltb.delete1 as delete1,ltb.update1 as update1 from LoginTab LTb";
        sql = sql + " left join LoginType Lt on LTb.LoginTypeId=Lt.LoginTypeId where LT.LoginTypeName='" + Session["Logintype"] + "' and LTb.LoginName='" + Session["LoginName"] + "'";
        int a, u, d;
        a = Convert.ToInt32(oo.ReturnTag(sql, "add1"));
        u = Convert.ToInt32(oo.ReturnTag(sql, "update1"));
        d = Convert.ToInt32(oo.ReturnTag(sql, "delete1"));

        PermissionGrant(a, d, u, (LinkButton)LinkButton3, btnDelete, Button3);
    }


}