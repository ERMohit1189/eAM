using System;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class admin_AdmitCardEntry : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    Campus oo = new Campus();
    string sql = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((string)Session["LoginName"] == "" || (string)Session["BranchCode"] == "" || (string)Session["SessionName"] == "" || Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader); 
        if (!IsPostBack)
        {
            oo.AddDateMonthYearDropDown(DDYear, DDMonth, DDDate);
            FindCurrentDateandSetinDropDown();
            loadclass();
            loadsection();
            loadSubject();
            loadgrid();
        }
    }
    public void loadgrid()
    {
        if (RadioButtonList1.SelectedIndex == 1)
        {
            sql = "Select ace.Id,Convert(varchar(11),ace.DateofEval,106) as Date,DATENAME(dw,DateofEval) as day,sbm.SubjectName as Subject,";
            sql = sql + " Replace(Convert(varchar(15),FromTime,100),RIGHT(CONVERT(VARCHAR(30), FromTime, 9),2),' ') + '- '+Replace(Convert(varchar(15),ToTime,100),RIGHT(CONVERT(VARCHAR(30), ToTime, 9),2),'') as Time,";
            sql = sql + " convert(nvarchar(50),Sum(DATEDIFF(MINUTE,FromTime,ToTime)))+ ' ' +'min.' as TotalTime from AdmitCardEntry ace";
            sql = sql + " inner join ClassMaster cm on cm.Id=ace.ClassId";
            sql = sql + " inner join SectionMaster sm on sm.ClassNameId=ace.ClassId";
            sql = sql + " inner join SubjectMaster sbm on sbm.ClassId=ace.ClassId and sbm.Id=ace.Subject";
            sql = sql + " where ace.Eval='" + drpEval.SelectedItem.Text + "' and ace.ClassId='" + drpClass.SelectedValue.ToString() + "' and ace.SectionName='" + drpsection.SelectedItem.ToString() + "'and cm.SessionName='" + Session["SessionName"].ToString() + "'";
            sql = sql + " and sm.SessionName='" + Session["SessionName"].ToString() + "' and sm.BranchCode=" + Session["BranchCode"].ToString() + " and cm.BranchCode=" + Session["BranchCode"].ToString() + "  and ace.BranchCode=" + Session["BranchCode"].ToString() + " and ace.SessionName='" + Session["SessionName"].ToString() + "' and sbm.SessionName='" + Session["SessionName"].ToString() + "' and sbm.BranchCode=" + Session["BranchCode"].ToString() + "";
            sql = sql + " group by ace.Id,ace.DateofEval,FromTime,ToTime,sbm.SubjectName";

            GridView1.DataSource = oo.GridFill(sql);
            GridView1.DataBind();
        }
        else if (RadioButtonList1.SelectedIndex == 0)
        {
            sql = "Select ace.Id,Convert(varchar(11),ace.DateofEval,106) as Date,DATENAME(dw,DateofEval) as day,sgm.SubjectGroup as Subject,";
            sql = sql + " Replace(Convert(varchar(15),FromTime,100),RIGHT(CONVERT(VARCHAR(30), FromTime, 9),2),' ') + '- '+Replace(Convert(varchar(15),ToTime,100),RIGHT(CONVERT(VARCHAR(30), ToTime, 9),2),'') as Time,";
            sql = sql + " convert(nvarchar(50),Sum(DATEDIFF(MINUTE,FromTime,ToTime)))+ ' ' +'min.' as TotalTime from AdmitCardEntry ace";
            sql = sql + " inner join ClassMaster cm on cm.Id=ace.ClassId";
            sql = sql + " inner join SectionMaster sm on sm.ClassNameId=ace.ClassId";
            sql = sql + " left join SubjectGroupMaster sgm on sgm.Id=ace.SubjectGroup";
            sql = sql + " where ace.Eval='"+drpEval.SelectedItem.Text+"' and ace.ClassId='" + drpClass.SelectedValue.ToString() + "' and ace.SectionName='" + drpsection.SelectedItem.ToString() + "'and cm.SessionName='" + Session["SessionName"].ToString() + "'";
            sql = sql + " and sm.SessionName='" + Session["SessionName"].ToString() + "' and sm.BranchCode=" + Session["BranchCode"].ToString() + " and cm.BranchCode=" + Session["BranchCode"].ToString() + " and ace.BranchCode=" + Session["BranchCode"].ToString() + " and sgm.BranchCode=" + Session["BranchCode"].ToString() + " and ace.SessionName='" + Session["SessionName"].ToString() + "' and sgm.SessionName='" + Session["SessionName"].ToString() + "'";
            sql = sql + " group by ace.Id,ace.DateofEval,FromTime,ToTime,sgm.SubjectGroup";

            GridView1.DataSource = oo.GridFill(sql);
            GridView1.DataBind();
        }
        else
        {

            GridView1.DataSource = null;
            GridView1.DataBind();
        }

        
       
    }
    public void loadSubject()
    {
        if (RadioButtonList1.SelectedIndex == 0)
        {
            sql = "Select SubjectGroup,Id from SubjectGroupMaster where ClassId='" + drpClass.SelectedValue.ToString() + "' and SectionName='" + drpsection.SelectedItem.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            oo.FillDropDown_withValue(sql, drpSubject, "SubjectGroup", "Id");
            drpSubject.Items.Insert(0, "<--Select-->");
        }
        else if (RadioButtonList1.SelectedIndex == 1)
        {
            sql = "Select SubjectName,Id from SubjectMaster where ClassId='" + drpClass.SelectedValue.ToString() + "' and SectionName='" + drpsection.SelectedItem.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            oo.FillDropDown_withValue(sql, drpSubject, "SubjectName", "Id");
            drpSubject.Items.Insert(0, "<--Select-->");
        }

    }
    public void FindCurrentDateandSetinDropDown()
    {
        string dd = "", mm = "", yy = "";


        dd = oo.ReturnTag("Select day(getdate()) as DateDD", "DateDD");
        mm = oo.ReturnTag("Select Month(getdate())as MonthMM", "MonthMM");
        yy = oo.ReturnTag("Select Year(getdate()) as YearYY ", "YearYY");

        DDYear.Text = yy;
        if (mm == "1")
        {
            DDMonth.Text = "Jan";
        }
        else if (mm == "2")
        {
            DDMonth.Text = "Feb";
        }
        else if (mm == "3")
        {
            DDMonth.Text = "Mar";
        }
        else if (mm == "4")
        {
            DDMonth.Text = "Apr";
        }
        else if (mm == "5")
        {
            DDMonth.Text = "May";
        }
        else if (mm == "6")
        {
            DDMonth.Text = "Jun";

        }
        else if (mm == "7")
        {
            DDMonth.Text = "Jul";
        }
        else if (mm == "8")
        {
            DDMonth.Text = "Aug";
        }
        else if (mm == "9")
        {
            DDMonth.Text = "Sep";
        }
        else if (mm == "10")
        {
            DDMonth.Text = "Oct";
        }
        else if (mm == "11")
        {
            DDMonth.Text = "Nov";
        }
        else if (mm == "12")
        {
            DDMonth.Text = "Dec";
        }


        DDDate.Text = dd;
    }
    public void loadclass()
    {
        sql = "Select Id,ClassName,CidOrder from ClassMaster Where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " Order by CIDOrder";
        oo.FillDropDown_withValue(sql, drpClass, "ClassName", "Id");
    }
    public void loadsection()
    {
        sql = "Select SectionName,Id from SectionMaster where ClassNameId='" + drpClass.SelectedValue.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        oo.FillDropDown_withValue(sql, drpsection, "SectionName", "Id");
        drpsection.Items.Insert(0, "<--Select-->");
    }
    protected void drpClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadsection();
        loadgrid();
    }
    protected void drpsection_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadSubject();
        loadgrid();
    }

    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedIndex == 1)
        {
            sql = "Select *from AdmitCardEntry where ClassId='" + drpClass.SelectedValue.ToString() + "' and SectionName='" + drpsection.SelectedItem.ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and Eval='" + drpEval.SelectedItem.ToString() + "' and Subject='" + drpSubject.SelectedValue.ToString() + "'";
        }
        else if (RadioButtonList1.SelectedIndex == 0)
        {
            sql = "Select *from AdmitCardEntry where ClassId='" + drpClass.SelectedValue.ToString() + "' and SectionName='" + drpsection.SelectedItem.ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and Eval='" + drpEval.SelectedItem.ToString() + "' and SubjectGroup='" + drpSubject.SelectedValue.ToString() + "'";
        }
        if (oo.Duplicate(sql))
        {
            //oo.MessageBoxforUpdatePanel("Duplicate Entry!",lnkSubmit);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Duplicate Entry!", "A");       

        }
        else
        {
            if (drpsection.SelectedIndex != 0)
            {
                if (drpSubject.SelectedIndex != 0)
                {
                    if (checkSubjectMode() == 1)
                    {
                        //oo.MessageBoxforUpdatePanel("Please delete all recors(s) of Subject wise!",lnkSubmit);
                        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please delete all recors(s) of Subject wise!", "A");       

                    }
                    else if (checkSubjectMode() == 2)
                    {
                        //oo.MessageBoxforUpdatePanel("Please delete all recors(s) of Subject Group wise!", lnkSubmit);
                        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please delete all recors(s) of Subject wise!", "A");       

                    }
                    else
                    {
                        save();
                    }
                    
                }
                else
                {
                    //oo.MessageBoxforUpdatePanel("Select Subject!", lnkSubmit);
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Select Subject!", "A");       

                }
            }
            else
            {
                //oo.MessageBoxforUpdatePanel("Select Section!", lnkSubmit);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Select Section!", "A");       

            }
            
        }
    }

    private int checkSubjectMode()
    {
        int i = 0;
        sql="Select *from AdmitCardEntry where ClassId='" + drpClass.SelectedValue.ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and SectionName='" + drpsection.SelectedItem.ToString() + "' and Eval='" + drpEval.SelectedItem.ToString() + "'";
        if (oo.Duplicate(sql))
        {
            if (RadioButtonList1.SelectedIndex == 0)
            {
                sql = "Select *from AdmitCardEntry where ClassId='" + drpClass.SelectedValue.ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and SectionName='" + drpsection.SelectedItem.ToString() + "' and Eval='" + drpEval.SelectedItem.ToString() + "' and Subject is not null";
                if (oo.Duplicate(sql))
                {
                    i = 1;
                }
                else
                {
                    i = 0;
                }
            }
            if (RadioButtonList1.SelectedIndex == 1)
            {
                sql = "Select *from AdmitCardEntry where ClassId='" + drpClass.SelectedValue.ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and SectionName='" + drpsection.SelectedItem.ToString() + "' and Eval='" + drpEval.SelectedItem.ToString() + "' and SubjectGroup is not null";
                if (oo.Duplicate(sql))
                {
                    i = 2;
                }
                else
                {
                    i = 0;
                }
            }
        }
        else
        {
            i = 0;
        }
        return i;
    }

    public void save()
    {
        try
        {
            string date = DDYear.SelectedValue.ToString() + "/" + DDMonth.SelectedValue.ToString() + "/" + DDDate.SelectedValue.ToString();
            string FromTime = txtFromHour.Text.Trim() + TextBox1.Text.Trim() + txtFromMinute.Text.Trim();
            string ToTime = txtToHour.Text.Trim() + TextBox2.Text.Trim() + txtToMinute.Text.Trim();
            cmd = new SqlCommand();
            cmd.CommandText = "AdmitCardEntryProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@ClassId", drpClass.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@SectionName", drpsection.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@Eval", drpEval.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@DateofEval", date);
            if (RadioButtonList1.SelectedIndex == 0)
            {
                cmd.Parameters.AddWithValue("@Subject", "");
                cmd.Parameters.AddWithValue("@SubjectGroup", drpSubject.SelectedValue.ToString());
            }
            else if (RadioButtonList1.SelectedIndex == 1)
            {
                cmd.Parameters.AddWithValue("@Subject", drpSubject.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@SubjectGroup", "");
            }            
            cmd.Parameters.AddWithValue("@FromTime", FromTime);
            cmd.Parameters.AddWithValue("@ToTime", ToTime);
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            loadgrid();
            //oo.MessageBoxforUpdatePanel("Submitted successfully", lnkSubmit);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully", "S");       

        }
        catch
        {
        }

    }
    protected void DDYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(DDYear, DDMonth, DDDate);
    }
    protected void DDMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(DDYear, DDMonth, DDDate);
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId3 = (Label)chk.NamingContainer.FindControl("Label37");
        lblvalue.Text = lblId3.Text;
        Panel1_ModalPopupExtender.Show();
        Button8.Focus();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        sql = "Delete From AdmitCardEntry where Id='"+lblvalue.Text+ "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        cmd = new SqlCommand(sql,con);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        loadgrid();
        //oo.MessageBoxforUpdatePanel("Deleted successfully", btnDelete);
        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Deleted successfully", "S");       


    }
    protected void txtFromHour_TextChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(txtFromHour.Text) > 24)
        {
            txtFromHour.Text = "";
            txtFromHour.Focus();
        }
        else
        {
            txtFromMinute.Focus();
        }
    }
    protected void txtFromMinute_TextChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(txtFromMinute.Text) > 60)
        {
            txtFromMinute.Text = "";
            txtFromMinute.Focus();
        }
        else
        {
            txtToHour.Focus();
        }
    }
    protected void txtToHour_TextChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(txtToHour.Text) > 24)
        {
            txtToHour.Text = "";
            txtToHour.Focus();
        }
        else
        {
            txtToMinute.Focus();
        }
    }
    protected void txtToMinute_TextChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(txtToMinute.Text) > 60)
        {
            txtToMinute.Text = "";
            txtToMinute.Focus();
        }
        else
        {
            DDYear.Focus();
        }
    }

    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadSubject();
        loadgrid();
    }

    protected void drpEval_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadgrid();
    }
}