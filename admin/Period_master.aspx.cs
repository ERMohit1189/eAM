using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;

public partial class Period_master : Page
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
        con = oo.dbGet_connection();
        if (!IsPostBack)
        {

            try
            {
                CheckValueADDDeleteUpdate();
            }
            catch (Exception) { }
            
            
            
            
            //sql = "Select FeeGroupName from FeeGroupMaster";


            sql = "Select GroupName from GroupMaster ";
           // sql = sql + " where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            sql = sql + "   order by Id ";
            oo.FillDropDownWithOutSelect(sql, drpgroup, "GroupName");

            sql = "Select Medium from MediumMaster";
            oo.FillDropDown(sql, drpmedium, "Medium");
            
            
            
            sql = "Select ClassName from ClassMaster";
            sql = sql + " where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            sql = sql + "  order by Id";
            oo.FillDropDownWithOutSelect(sql, drpClass, "ClassName");


            string ss = "";
            sql = "select Id from ClassMaster where  ClassName='" + drpClass.SelectedItem.ToString() + "'";
            sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            ss = oo.ReturnTag(sql, "Id");
            sql = "Select SectionName from SectionMaster where ClassNameId=" + ss;
            sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            oo.FillDropDown(sql, drpsection, "SectionName");


            LinkButton4.Visible = false;
            lblmess.Text = "";

        }




    }
    protected void txtremark_TextChanged(object sender, EventArgs e)
    {

    }
    protected void drpClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        string ss = "";
        sql = "select Id from ClassMaster where  ClassName='" + drpClass.SelectedItem.ToString() + "'";
        sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        ss = oo.ReturnTag(sql, "Id");
        
        sql = "Select SectionName from SectionMaster where Id=" + ss;
        sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        oo.FillDropDown(sql, drpsection, "SectionName");



       // LinkButton4.Visible = false;




        sql = "select ROW_NUMBER() OVER (ORDER BY Id ASC) AS SrNo, Id, FacultyName,FacultyEmpId,SubjectAlloted,Class,Section,Medium,grp,PaperTypes from TeacherSubjectAllotment";

        sql = sql + "  where Class='" + drpClass.SelectedItem.ToString() + "' and Section='" + drpsection.SelectedItem.ToString() + "' and  Medium='" + drpmedium.SelectedItem.ToString() + "' and grp='" + drpgroup.SelectedItem.ToString() + "' and Season='" + RadioButtonList1.SelectedItem.ToString() + "'";
        sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

        GridView1.DataSource = oo.GridFill(sql);
        GridView1.DataBind();
       LinkButton4.Visible = true;

       if (GridView1.Rows.Count == 0)
       {
           LinkButton4.Visible = false;
       }



    }
    public bool CheckAlloted()
    {
       

        int i=0,j;
      sql = "select ROW_NUMBER() OVER (ORDER BY Id ASC) AS SrNo, Id, FacultyName,FacultyEmpId,Subjectname as SubjectAlloted,Class,Section,Medium,grp ,totiming,fromtiming,Modeofsubject,Period,Season,days,PaperTypes  from Period_Master";
        sql = sql + "  where Class='" + drpClass.SelectedItem.ToString() + "' and Section='" + drpsection.SelectedItem.ToString() + "' and  Medium='" + drpmedium.SelectedItem.ToString() + "' and grp='" + drpgroup.SelectedItem.ToString() + "' and Season='"+RadioButtonList1.SelectedItem.ToString()+"'";
        sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and FacultyEmpId!='None'";

        if (oo.Duplicate(sql))
        {
            GridView1.DataSource = oo.GridFill(sql);
            GridView1.DataBind();
            LinkButton4.Visible = true;

            for (i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                sql = "select ROW_NUMBER() OVER (ORDER BY Id ASC) AS SrNo, Id, FacultyName,FacultyEmpId,Subjectname as SubjectAlloted,Class,Section,Medium,grp ,totiming,fromtiming,Modeofsubject,Period,Season,days,PaperTypes  from Period_Master";
                sql = sql + "  where Class='" + drpClass.SelectedItem.ToString() + "' and Section='" + drpsection.SelectedItem.ToString() + "' and  Medium='" + drpmedium.SelectedItem.ToString() + "' and grp='" + drpgroup.SelectedItem.ToString() + "' and Season='" + RadioButtonList1.SelectedItem.ToString() + "'";
                sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and FacultyEmpId!='None'";
              
                LinkButton LinkButton2 = (LinkButton)GridView1.Rows[i].FindControl("LinkButton2");
                sql = sql + " and Id=" + LinkButton2.Text;

                DropDownList DropDownList2 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList2");
                DropDownList2.SelectedItem.Text = oo.ReturnTag(sql, "Period").Trim();
                DropDownList2.Enabled = false;
                TextBox TextBox1 = (TextBox)GridView1.Rows[i].FindControl("TextBox1");
                TextBox1.Text = oo.ReturnTag(sql, "fromtiming");
                TextBox1.ReadOnly = true;
                TextBox TextBox2 = (TextBox)GridView1.Rows[i].FindControl("TextBox2");
                TextBox2.Text = oo.ReturnTag(sql, "totiming");
                TextBox2.ReadOnly = true;



                CheckBox c1 = (CheckBox)GridView1.Rows[i].FindControl("CheckBox1");
                CheckBox c2 = (CheckBox)GridView1.Rows[i].FindControl("CheckBox2");
                CheckBox c3 = (CheckBox)GridView1.Rows[i].FindControl("CheckBox3");
                CheckBox c4 = (CheckBox)GridView1.Rows[i].FindControl("CheckBox4");
                CheckBox c5 = (CheckBox)GridView1.Rows[i].FindControl("CheckBox5");
                CheckBox c6 = (CheckBox)GridView1.Rows[i].FindControl("CheckBox6");


                //For Check Box DayWise Start
                string davalue = "";
                davalue = oo.ReturnTag(sql, "days");
                for (j = 0; j <= davalue.Length - 1; j++)
                {

                    if (davalue[j] == 'M')
                    {
                        c1.Checked = true;
                    }
                    try
                    {
                        if (davalue[j] == 'T' && davalue[j + 1] == ',')
                        {
                            c2.Checked = true;
                        }
                    }
                    catch (Exception) { c2.Checked = true; }
                    if (davalue[j] == 'W')
                    {
                        c3.Checked = true;
                    }
                    try
                    {
                        if (davalue[j] == 'T' && davalue[j + 1] == 'h')
                        {
                            c4.Checked = true;
                        }
                    }
                    catch(Exception){c4.Checked = true;}
                    if (davalue[j] == 'F')
                    {
                        c5.Checked = true;
                    }
                    if (davalue[j] == 'S')
                    {
                        c6.Checked = true;
                    }

                }




            }


            sql = "select ROW_NUMBER() OVER (ORDER BY Id ASC) AS SrNo, Id, FacultyName,FacultyEmpId,Subjectname as SubjectAlloted,Class,Section,Medium,grp ,totiming,fromtiming,Modeofsubject,Period,Season,PaperTypes  from Period_Master";
            sql = sql + "  where Class='" + drpClass.SelectedItem.ToString() + "' and Section='" + drpsection.SelectedItem.ToString() + "' and  Medium='" + drpmedium.SelectedItem.ToString() + "' and grp='" + drpgroup.SelectedItem.ToString() + "' and Season='" + RadioButtonList1.SelectedItem.ToString() + "'";
            sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and FacultyEmpId='None'";
              
            Label Label7 = (Label)GridView1.FooterRow.FindControl("Label7");
            Label7.Text = oo.ReturnTag(sql, "Period");
            Label7.Enabled = false;

            TextBox TextBox3 = (TextBox)GridView1.FooterRow.FindControl("TextBox3");
            TextBox3.Text = oo.ReturnTag(sql, "fromtiming");
            TextBox3.ReadOnly = true;
            TextBox TextBox4 = (TextBox)GridView1.FooterRow.FindControl("TextBox4");
            TextBox4.Text = oo.ReturnTag(sql, "totiming");
            TextBox4.ReadOnly = true;

            LinkButton lnk =(LinkButton)GridView1.FooterRow.FindControl("LinkButton6");
            lnk.Text=oo.ReturnTag(sql,"Id");

           


            return true;
        }
        else
        {
            return false;
        }
    
    }
    public void CheckAllotedUpdate()
    {


        int i = 0, j ;
        sql = "select ROW_NUMBER() OVER (ORDER BY Id ASC) AS SrNo, Id, FacultyName,FacultyEmpId,Subjectname as SubjectAlloted,Class,Section,Medium,grp ,totiming,fromtiming,Modeofsubject,Period,Season,days,PaperTypes  from Period_Master";
        sql = sql + "  where Class='" + drpClass.SelectedItem.ToString() + "' and Section='" + drpsection.SelectedItem.ToString() + "' and  Medium='" + drpmedium.SelectedItem.ToString() + "' and grp='" + drpgroup.SelectedItem.ToString() + "' and Season='" + RadioButtonList1.SelectedItem.ToString() + "'";
        sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and FacultyEmpId!='None'";

        if (oo.Duplicate(sql))
        {
            GridView1.DataSource = oo.GridFill(sql);
            GridView1.DataBind();
            LinkButton4.Visible = true;

            for (i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                sql = "select ROW_NUMBER() OVER (ORDER BY Id ASC) AS SrNo, Id, FacultyName,FacultyEmpId,Subjectname as SubjectAlloted,Class,Section,Medium,grp ,totiming,fromtiming,Modeofsubject,Period,Season,days.PaperTypes  from Period_Master";
                sql = sql + "  where Class='" + drpClass.SelectedItem.ToString() + "' and Section='" + drpsection.SelectedItem.ToString() + "' and  Medium='" + drpmedium.SelectedItem.ToString() + "' and grp='" + drpgroup.SelectedItem.ToString() + "' and Season='" + RadioButtonList1.SelectedItem.ToString() + "'";
                sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and FacultyEmpId!='None'";

                LinkButton LinkButton2 = (LinkButton)GridView1.Rows[i].FindControl("LinkButton2");
                sql = sql + " and Id=" + LinkButton2.Text;

                DropDownList DropDownList2 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList2");
                DropDownList2.Text = oo.ReturnTag(sql, "Period").Trim();
                //DropDownList2.Enabled = false;
                TextBox TextBox1 = (TextBox)GridView1.Rows[i].FindControl("TextBox1");
                TextBox1.Text = oo.ReturnTag(sql, "fromtiming");
                TextBox1.ReadOnly = true;
                TextBox TextBox2 = (TextBox)GridView1.Rows[i].FindControl("TextBox2");
                TextBox2.Text = oo.ReturnTag(sql, "totiming");
                TextBox2.ReadOnly = true;

                CheckBox c1 = (CheckBox)GridView1.Rows[i].FindControl("CheckBox1");
                CheckBox c2 = (CheckBox)GridView1.Rows[i].FindControl("CheckBox2");
                CheckBox c3 = (CheckBox)GridView1.Rows[i].FindControl("CheckBox3");
                CheckBox c4 = (CheckBox)GridView1.Rows[i].FindControl("CheckBox4");
                CheckBox c5 = (CheckBox)GridView1.Rows[i].FindControl("CheckBox5");
                CheckBox c6 = (CheckBox)GridView1.Rows[i].FindControl("CheckBox6");


                //For Check Box DayWise Start
                string davalue = "";
                davalue = oo.ReturnTag(sql, "days");
                for (j = 0; j <= davalue.Length - 1; j++)
                {

                    if (davalue[j] == 'M')
                    {
                        c1.Checked = true;
                    }

                    if (davalue[j] == 'T' && davalue[j+1]==',')
                    {
                        c2.Checked = true;
                    }
                    if (davalue[j] == 'W')
                    {
                        c3.Checked = true;
                    }
                    if (davalue[j] == 'T' && davalue[j+1]=='h')
                    {
                        c4.Checked = true;
                    }
                    if (davalue[j] == 'F')
                    {
                        c5.Checked = true;
                    }
                    if (davalue[j] == 'S')
                    {
                        c6.Checked = true;
                    }

                }


                //For Check Box DayWise End

            }


            sql = "select ROW_NUMBER() OVER (ORDER BY Id ASC) AS SrNo, Id, FacultyName,FacultyEmpId,Subjectname as SubjectAlloted,Class,Section,Medium,grp ,totiming,fromtiming,Modeofsubject,Period,Season,days,PaperTypes  from Period_Master";
            sql = sql + "  where Class='" + drpClass.SelectedItem.ToString() + "' and Section='" + drpsection.SelectedItem.ToString() + "' and  Medium='" + drpmedium.SelectedItem.ToString() + "' and grp='" + drpgroup.SelectedItem.ToString() + "' and Season='" + RadioButtonList1.SelectedItem.ToString() + "'";
            sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and FacultyEmpId='None'";

            Label Label7 = (Label)GridView1.FooterRow.FindControl("Label7");
            Label7.Text = oo.ReturnTag(sql, "Period");
            Label7.Enabled = false;

            TextBox TextBox3 = (TextBox)GridView1.FooterRow.FindControl("TextBox3");
            TextBox3.Text = oo.ReturnTag(sql, "fromtiming");
            TextBox3.ReadOnly = true;
            TextBox TextBox4 = (TextBox)GridView1.FooterRow.FindControl("TextBox4");
            TextBox4.Text = oo.ReturnTag(sql, "totiming");
            TextBox4.ReadOnly = true;
            LinkButton lnk = (LinkButton)GridView1.FooterRow.FindControl("LinkButton6");
            lnk.Text = oo.ReturnTag(sql, "Id");



        }

    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        


        if (CheckAlloted() == false)
        {
            sql = "select ROW_NUMBER() OVER (ORDER BY Id ASC) AS SrNo, Id, FacultyName,FacultyEmpId,SubjectAlloted,Class,Section,Medium,grp,PaperTypes from TeacherSubjectAllotment";

            sql = sql + "  where Class='" + drpClass.SelectedItem.ToString() + "' and Section='" + drpsection.SelectedItem.ToString() + "' and  Medium='" + drpmedium.SelectedItem.ToString() + "' and grp='" + drpgroup.SelectedItem.ToString() + "'";
            sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

            GridView1.DataSource = oo.GridFill(sql);
            GridView1.DataBind();
            LinkButton4.Visible = true;


            if (GridView1.Rows.Count == 0)
            {
                oo.MessageBox("Sorry, No Record(s) found!", this.Page);
                LinkButton4.Visible = false;
            }
        }

    }
    protected void drpmedium_SelectedIndexChanged(object sender, EventArgs e)
    {


        sql = "select ROW_NUMBER() OVER (ORDER BY Id ASC) AS SrNo, Id, FacultyName,FacultyEmpId,SubjectAlloted,Class,Section,Medium,grp,PaperTypes from TeacherSubjectAllotment";

        sql = sql + "  where Class='" + drpClass.SelectedItem.ToString() + "' and Section='" + drpsection.SelectedItem.ToString() + "' and  Medium='" + drpmedium.SelectedItem.ToString() + "' and grp='" + drpgroup.SelectedItem.ToString() + "' and Season='"+RadioButtonList1.SelectedItem.ToString()+"'";
        sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

        GridView1.DataSource = oo.GridFill(sql);
        GridView1.DataBind();
        LinkButton4.Visible = true;


        if (GridView1.Rows.Count == 0)
        {
            LinkButton4.Visible = false;
        }



    }
    protected void drpgroup_SelectedIndexChanged(object sender, EventArgs e)
    {

        sql = "select ROW_NUMBER() OVER (ORDER BY Id ASC) AS SrNo, Id, FacultyName,FacultyEmpId,SubjectAlloted,Class,Section,Medium,grp,PaperTypes from TeacherSubjectAllotment";

        sql = sql + "  where Class='" + drpClass.SelectedItem.ToString() + "' and Section='" + drpsection.SelectedItem.ToString() + "' and  Medium='" + drpmedium.SelectedItem.ToString() + "' and grp='" + drpgroup.SelectedItem.ToString() + "' and Season='" + RadioButtonList1.SelectedItem.ToString() + "'";
        sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

        GridView1.DataSource = oo.GridFill(sql);
        GridView1.DataBind();
        LinkButton4.Visible = true;


        if (GridView1.Rows.Count == 0)
        {
            LinkButton4.Visible = false;
        }

    }
    protected void drpsection_SelectedIndexChanged(object sender, EventArgs e)
    {
        //LinkButton4.Visible = false;
        sql = "select ROW_NUMBER() OVER (ORDER BY Id ASC) AS SrNo, Id, FacultyName,FacultyEmpId,SubjectAlloted,Class,Section,Medium,grp,PaperTypes from TeacherSubjectAllotment";

        sql = sql + "  where Class='" + drpClass.SelectedItem.ToString() + "' and Section='" + drpsection.SelectedItem.ToString() + "' and  Medium='" + drpmedium.SelectedItem.ToString() + "' and grp='" + drpgroup.SelectedItem.ToString() + "' and Season='" + RadioButtonList1.SelectedItem.ToString() + "'";
        sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

        GridView1.DataSource = oo.GridFill(sql);
        GridView1.DataBind();
      LinkButton4.Visible = true;

      if (GridView1.Rows.Count == 0)
      {
          LinkButton4.Visible = false;
      }


    }
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        //@Medium nvarchar(50),@Grp nvarchar(50),@Class nvarchar(50), @Section nvarchar(50),@Subjectname 
        //nvarchar(50),@Modeofsubject nvarchar(50),@facultyname int ,@Facultyempid nvarchar(50),@Period nchar(10),
        //@fromtiming nvarchar(50),@totiming nvarchar(50),@SessionName nvarchar(50),@BranchCode int)


        if (drpClass.SelectedItem.ToString() == "<--Select-->" || drpsection.SelectedItem.ToString() == "<--Select-->" || drpgroup.SelectedItem.ToString() == "<--Select-->")
        {
            oo.MessageBox("Please Select Condition", this.Page);
        }
        else
        {



            bool flag = false;
            int i;
            string days = "";

            sql = "Select Medium,Grp,Class,Section,Season,PaperTypes from Period_Master where Section ='" + drpsection.SelectedItem.ToString() + "' and Medium='" + drpmedium.SelectedItem.ToString() + "' and Grp='" + drpgroup.SelectedItem.ToString() + "' and Class='" + drpClass.SelectedItem.ToString() + "' and Season='" + RadioButtonList1.SelectedItem.ToString() + "'";
            sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            if (oo.Duplicate(sql))
            {
                oo.MessageBox("Duplicate Record", this.Page);
                lblmess.Text = "Duplicate Record";

            }
            else
            {



                for (i = 0; i <= GridView1.Rows.Count - 1; i++)
                {
                    days = "";

                    CheckBox c1 = (CheckBox)GridView1.Rows[i].FindControl("CheckBox1");
                    CheckBox c2 = (CheckBox)GridView1.Rows[i].FindControl("CheckBox2");
                    CheckBox c3 = (CheckBox)GridView1.Rows[i].FindControl("CheckBox3");
                    CheckBox c4 = (CheckBox)GridView1.Rows[i].FindControl("CheckBox4");
                    CheckBox c5 = (CheckBox)GridView1.Rows[i].FindControl("CheckBox5");
                    CheckBox c6 = (CheckBox)GridView1.Rows[i].FindControl("CheckBox6");
                    if (c1.Checked == true)
                    {
                        days = c1.Text;
                    }


                    if (c2.Checked == true)
                    {
                        days = days+","+c2.Text;
                    }


                    if (c3.Checked == true)
                    {
                        days = days + "," + c3.Text;
                    }

                    if (c4.Checked == true)
                    {
                        days = days + "," + c4.Text;
                    }

                    if (c5.Checked == true)
                    {
                        days = days + "," + c5.Text;
                    }

                    if (c6.Checked == true)
                    {
                        days = days + "," + c6.Text;
                    }


                    Label Label2 = (Label)GridView1.Rows[i].FindControl("Label2");
                    DropDownList DropDownList1 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList1");
                    Label Label4 = (Label)GridView1.Rows[i].FindControl("Label4");
                    Label Label3 = (Label)GridView1.Rows[i].FindControl("Label3");
                    Label Label11 = (Label)GridView1.Rows[i].FindControl("Label11");
                    TextBox TextBox1 = (TextBox)GridView1.Rows[i].FindControl("TextBox1");
                    DropDownList DropDownList2 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList2");
                    TextBox TextBox2 = (TextBox)GridView1.Rows[i].FindControl("TextBox2");



                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "Period_MasterProc";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    string dd = string.Empty;

                    cmd.Parameters.AddWithValue("@Medium", drpmedium.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Grp", drpgroup.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Class", drpClass.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Section", drpsection.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Subjectname", Label2.Text.ToString());
                    cmd.Parameters.AddWithValue("@Modeofsubject", DropDownList1.SelectedItem.ToString());


                    cmd.Parameters.AddWithValue("@facultyname", Label3.Text.ToString());
                    cmd.Parameters.AddWithValue("@Facultyempid", Label4.Text.ToString());
                    cmd.Parameters.AddWithValue("@Period", DropDownList2.SelectedItem.ToString().Trim());
                    cmd.Parameters.AddWithValue("@fromtiming", TextBox1.Text.ToString());
                    cmd.Parameters.AddWithValue("@totiming", TextBox2.Text.ToString());


                    cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                    cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                    cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);

                    cmd.Parameters.AddWithValue("@Season", RadioButtonList1.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Days", days);
                    cmd.Parameters.AddWithValue("@PaperTypes", Label11.Text.ToString());


                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        flag = true;


                    }
                    catch (Exception) { con.Close(); }
                }
                if (flag == true)
                {
                    footer();
                    oo.MessageBox("Submitted successfully.", this.Page);
                    lblmess.Text = "Submitted successfully.";
                    sql = "select ROW_NUMBER() OVER (ORDER BY Id ASC) AS SrNo, Id, FacultyName,FacultyEmpId,Subjectname as SubjectAlloted,Class,Section,Medium,grp ,totiming,fromtiming,Modeofsubject,Period,Season,days,PaperTypes  from Period_Master";
                    //sql = sql + "  where Class='" + drpClass.SelectedItem.ToString() + "' and Section='" + drpsection.SelectedItem.ToString() + "' and  Medium='" + drpmedium.SelectedItem.ToString() + "' and grp='" + drpgroup.SelectedItem.ToString() + "' and Season='" + RadioButtonList1.SelectedItem.ToString() + "'";
                    //sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and FacultyEmpId!='None'";
                    //GridView1.DataSource = oo.GridFill(sql);
                    //GridView1.DataBind();
                    CheckAlloted();
                    LinkButton4.Visible = true;

                }

            }
        }
    }
    public void footer()
    {
        TextBox TextBox3 = (TextBox)GridView1.FooterRow.FindControl("TextBox3");
        TextBox TextBox4 = (TextBox)GridView1.FooterRow.FindControl("TextBox4");
        Label Label7 = (Label)GridView1.FooterRow.FindControl("Label7");


        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "Period_MasterProc";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        string dd = string.Empty;

        cmd.Parameters.AddWithValue("@Medium", drpmedium.SelectedItem.ToString());
        cmd.Parameters.AddWithValue("@Grp", drpgroup.SelectedItem.ToString());
        cmd.Parameters.AddWithValue("@Class", drpClass.SelectedItem.ToString());
        cmd.Parameters.AddWithValue("@Section", drpsection.SelectedItem.ToString());
        cmd.Parameters.AddWithValue("@Subjectname", "None");
        cmd.Parameters.AddWithValue("@Modeofsubject", "None");


        cmd.Parameters.AddWithValue("@facultyname", "None");
        cmd.Parameters.AddWithValue("@Facultyempid", "None");
        cmd.Parameters.AddWithValue("@Period", Label7.Text.ToString());
        cmd.Parameters.AddWithValue("@fromtiming", TextBox3.Text.ToString());
        cmd.Parameters.AddWithValue("@totiming", TextBox4.Text.ToString());


        cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
        cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
        cmd.Parameters.AddWithValue("@Season", RadioButtonList1.SelectedItem.ToString());
        cmd.Parameters.AddWithValue("@Days", "None");
        cmd.Parameters.AddWithValue("@PaperTypes", "None");

        try
        {
            if (con.State.ToString() == "Open")
            {
                con.Close();
            }

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

        }
        catch (Exception) { }
        
    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        
        LinkButton chk = (LinkButton)sender;
        string ss = chk.Text;
        lblID.Text = ss;
        //DropDownList3.Visible = true ;
        //Label8.Visible = true ;
        // sql = "Select  distinct ROW_NUMBER() OVER (ORDER BY p.ProductId ASC) AS  [ProductId] ,po.id as ID, pc.ProductCategoryName as[ProductCategoryName], p.ProductName as [ProductName], Pm.ProductTypeName as [ProductTypeName],PO.ProductModelName as [ProductModelName] from Productcategorymaster pc left join ProductName p on p.ProductId=pc.ProductId  left join ProductTypeMaster PM on p.ProductId=PM.ProductId left join ProductModelMaster PO on p.ProductId=PO.ProductId ";
        sql = "Select  ROW_NUMBER() OVER (ORDER BY Id ASC) AS SrNo,Id,Period,fromtiming,totiming ,days,PaperTypes from Period_Master where Id=" + ss;
      //  sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
       // DropDownList DropDownList3 = (DropDownList)GridView1.FindControl("DropDownList3");
        try
        {
            DropDownList3.Text = oo.ReturnTag(sql, "Period").Trim();
        }
        catch (Exception) { }
        TextBox6.Text = oo.ReturnTag(sql, "fromtiming").Trim();
        TextBox5.Text = oo.ReturnTag(sql, "totiming").Trim();

        Panel1_ModalPopupExtender.Show();
        DropDownList3.Visible = true;
        Label8.Visible = true;

        daysValues(oo.ReturnTag(sql, "days")+",");

    }
    public void  daysValues(string xx)
    {
        int i;
        CheckBoxPanel1.Checked = false;
        CheckBoxPanel2.Checked = false;
        CheckBoxPanel3.Checked = false;
        CheckBoxPanel4.Checked = false;
        CheckBoxPanel5.Checked = false;
        CheckBoxPanel6.Checked = false;
      
       
        for(i=0;i<=xx.Length-1;i++)            
        {
            if (xx[i] == 'M')
            {
                CheckBoxPanel1.Checked = true;
            }

            if (xx[i] == 'T'  && xx[i+1]==',')
            {
                CheckBoxPanel2.Checked = true;
            }
            if (xx[i] == 'W')
            {
                CheckBoxPanel3.Checked = true;
            }
            if (xx[i] == 'T'  && xx[i+1]=='h')
            {
                CheckBoxPanel4.Checked = true;
            }
            if (xx[i] == 'F')
            {
                CheckBoxPanel5.Checked = true;
            }

            if (xx[i] == 'S')
            {
                CheckBoxPanel6.Checked = true;
            }
        }
    
    
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
//Period_MasterupdateProc


            string daysValue = "";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Period_MasterupdateProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            string dd = string.Empty;

            if (CheckBoxPanel1.Checked)
            {
                daysValue = "M";
            }

            if (CheckBoxPanel2.Checked)
            {
                daysValue = daysValue + "," + "T";
            }

            if (CheckBoxPanel3.Checked)
            {
                daysValue = daysValue + "," + "W";
                
            }

            if (CheckBoxPanel4.Checked)
            {
                daysValue = daysValue + "," + "Th";
            }

            if (CheckBoxPanel5.Checked)
            {
                daysValue = daysValue + "," + "F";
            }
            if (CheckBoxPanel6.Checked)
            {
                daysValue = daysValue + "," + "S";
            }




            cmd.Parameters.AddWithValue("@id", lblID.Text);

            if (lbllunch.Text != "")
            {
                cmd.Parameters.AddWithValue("@Period", lbllunch.Text );
                lbllunch.Text = "";
            }
            else
            {
                cmd.Parameters.AddWithValue("@Period", DropDownList3.SelectedItem.ToString());
            }
        
            cmd.Parameters.AddWithValue("@fromtiming", TextBox6.Text.ToString());
            cmd.Parameters.AddWithValue("@totiming", TextBox5.Text.ToString());
            cmd.Parameters.AddWithValue("@Days", daysValue);


            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                oo.MessageBox("Updated successfully.", this.Page);


                sql = "select ROW_NUMBER() OVER (ORDER BY Id ASC) AS SrNo, Id, FacultyName,FacultyEmpId,Subjectname as SubjectAlloted,Class,Section,Medium,grp ,totiming,fromtiming,Modeofsubject,Period,Season,days,PaperTypes  from Period_Master";
                sql = sql + "  where Class='" + drpClass.SelectedItem.ToString() + "' and Section='" + drpsection.SelectedItem.ToString() + "' and  Medium='" + drpmedium.SelectedItem.ToString() + "' and grp='" + drpgroup.SelectedItem.ToString() + "' and Season='" + RadioButtonList1.SelectedItem.ToString() + "'";
                sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and FacultyEmpId!='None'";
                GridView1.DataSource = oo.GridFill(sql);
                GridView1.DataBind();
                CheckAllotedUpdate();
                LinkButton4.Visible = true;
                CheckAlloted();

            }
            catch (Exception) { con.Close(); }



    }
    protected void Button4_Click(object sender, EventArgs e)
    {

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {

    }
    protected void Button8_Click(object sender, EventArgs e)
    {

    }
    protected void DropDownList3_SelectedIndexChanged1(object sender, EventArgs e)
    {

    }
    protected void LinkButton6_Click(object sender, EventArgs e)
    {

        LinkButton chk = (LinkButton)sender;
        string ss = chk.Text;
        lblID.Text = ss;
        lbllunch.Text = "Lunch Time";
        // sql = "Select  distinct ROW_NUMBER() OVER (ORDER BY p.ProductId ASC) AS  [ProductId] ,po.id as ID, pc.ProductCategoryName as[ProductCategoryName], p.ProductName as [ProductName], Pm.ProductTypeName as [ProductTypeName],PO.ProductModelName as [ProductModelName] from Productcategorymaster pc left join ProductName p on p.ProductId=pc.ProductId  left join ProductTypeMaster PM on p.ProductId=PM.ProductId left join ProductModelMaster PO on p.ProductId=PO.ProductId ";
        sql = "Select  ROW_NUMBER() OVER (ORDER BY Id ASC) AS SrNo,Id,Period,fromtiming,totiming,PaperTypes from Period_Master where Id=" + ss;
        sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

        txtLunchFromTime.Text = oo.ReturnTag(sql, "fromtiming");
        txtToTimeLunch.Text = oo.ReturnTag(sql, "totiming");
        Panel4_ModalPopupExtender.Show();

    }
    protected void DropDownList3_SelectedIndexChanged2(object sender, EventArgs e)
    {

    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void LinkButton2_Click1(object sender, EventArgs e)
    {

    }
    protected void LinkButton2_Click2(object sender, EventArgs e)
    {

    }
    protected void LinkButton6_Click1(object sender, EventArgs e)
    {

    }
    protected void btnLunchUpdate_Click(object sender, EventArgs e)
    {
        string daysValue = "";
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "Period_MasterupdateProc";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        string dd = string.Empty;
        daysValue = "None";


        cmd.Parameters.AddWithValue("@id", lblID.Text);
        cmd.Parameters.AddWithValue("@Period", lbllunch.Text);
        cmd.Parameters.AddWithValue("@fromtiming", txtLunchFromTime.Text.ToString());
        cmd.Parameters.AddWithValue("@totiming", txtToTimeLunch.Text.ToString());
        cmd.Parameters.AddWithValue("@Days", daysValue);


        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            oo.MessageBox("Updated successfully.", this.Page);


            //sql = "Select ROW_NUMBER() OVER (ORDER BY Id ASC) AS SrNo, Id, Medium,Grp,Class,Section,Season from Period_Master where Section ='" + drpsection.SelectedItem.ToString() + "' and Medium='" + drpmedium.SelectedItem.ToString() + "' and Grp='" + drpgroup.SelectedItem.ToString() + "' and Class='" + drpClass.SelectedItem.ToString() + "' and Season='" + RadioButtonList1.SelectedItem.ToString() + "'";
            //sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

            //sql = "select ROW_NUMBER() OVER (ORDER BY Id ASC) AS SrNo, Id, FacultyName,FacultyEmpId,Subjectname as SubjectAlloted,Class,Section,Medium,grp ,totiming,fromtiming,Modeofsubject,Period,Season  from Period_Master";
            //sql = sql + "  where Class='" + drpClass.SelectedItem.ToString() + "' and Section='" + drpsection.SelectedItem.ToString() + "' and  Medium='" + drpmedium.SelectedItem.ToString() + "' and grp='" + drpgroup.SelectedItem.ToString() + "' and Season='" + RadioButtonList1.SelectedItem.ToString() + "'";
            //sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and FacultyEmpId!='None'";
            //GridView1.DataSource = oo.GridFill(sql);
            //GridView1.DataBind();
            CheckAllotedUpdate();
            LinkButton4.Visible = true;


        }
        catch (Exception) { con.Close(); }

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

        PermissionGrant(a, d, u, (LinkButton)LinkButton1, btnDelete, Button3);
    }
}

            