using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

public partial class staff_CoscholasticGradeEntry_VItoX : Page
{
    SqlConnection con = new SqlConnection();
    SqlCommand cmd = new SqlCommand();
    Campus oo = new Campus();
    string sql = "";
    protected void Page_PreInIt(object sender, EventArgs e)
    {
        if (Session["Logintype"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (Session["Logintype"].ToString() == "Admin")
        {
            this.MasterPageFile = "~/Master/admin_root-manager.master";
        }
        else if (Session["Logintype"].ToString() != "Staff")
        {
            this.MasterPageFile = "~/Staff/staff_root-manager.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null || Session["SessionName"].ToString() == "" || Session["LoginName"].ToString() == "" || Session["BranchCode"].ToString() == "")
        {
            Response.Redirect("~/default.aspx");
        }
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file
        if (!IsPostBack)
        {
            string sql1 = "Select Medium from MediumMaster";
            sql1 = sql1 + " where BranchCode=" + Session["BranchCode"].ToString() + "";
            oo.FillDropDown(sql1, drpmedium, "Medium");
            loadEval();
            loadclass();
            loadsection();
            loadCoscholasticGroup();
            loadCoscholastic();
            loadgrid();
        }
    }

    public void loadEval()
    {
        drpEval.Items.Add(new ListItem("TERM 1", "TERM1"));
        drpEval.Items.Add(new ListItem("TERM 2", "TERM2"));
    }
    public void loadgrid()
    {
        sql = "select  srno, Name,FatherName from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") ";
        sql +=  "   where  ClassId='" + drpclass.SelectedValue + "'";
        sql +=  "   and SectionName='" + drpsection.SelectedValue + "' and Medium='" + drpmedium.SelectedValue.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and ";
        sql +=  "   BranchCode=" + Session["BranchCode"].ToString() + " and Withdrwal is null and  isnull(Promotion,'')<>'Cancelled' order by Name Asc";
        GridView1.DataSource = oo.GridFill(sql);
        GridView1.DataBind();
        if (GridView1.Rows.Count > 0)
        {
            lnkSubmit.Visible = true;
        }
        else
        {
            lnkSubmit.Visible = false;
        }
    }
    public void loadclass()
    {
        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select Distinct ClassName,cm.Id as Id,CIDOrder from ClassMaster cm ";
            sql +=  " inner join dt_ClassGroupMaster t1 on t1.ClassId=cm.Id and cm.SessionName=t1.SessionName";
            sql +=  " where cm.SessionName='" + Session["SessionName"] + "' and t1.BranchCode=" + Session["BranchCode"] + " and cm.BranchCode=" + Session["BranchCode"] + " and GroupId='G4' Order by CIDOrder";
            oo.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");
        }
        else
        {
            sql = "Select  Distinct ClassName,cm.Id,CIDOrder from ClassTeacherMaster T1";
            sql +=  " inner join ClassMaster cm on cm.Id=T1.ClassId and cm.SessionName=t1.SessionName and cm.BranchCode=t1.BranchCode";
            sql +=  " inner join dt_ClassGroupMaster T2 on T2.ClassId=T1.ClassId and cm.SessionName=T2.SessionName and T1.BranchCode=t2.BranchCode";
            sql +=  " where GroupId='G4' and EmpCode='" + Session["LoginName"].ToString() + "' and T1.BranchCode=" + Session["BranchCode"] + " and T1.SessionName='" + Session["SessionName"] + "' Order by CIDOrder";
            BAL.objBal.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");
        }
    }
    public void loadCoscholasticGroup()
    {
        sql = "select CoscholasticGroup,Id from CoscholasticGroupMaster where SessionName='" + Session["SessionName"].ToString() + "'";
        sql +=  "  and BranchCode=" + Session["BranchCode"].ToString() + " and BranchCode=" + Session["BranchCode"] + " and ClassID='" + drpclass.SelectedValue.ToString() + "' and Medium='" + drpmedium.SelectedValue.ToString() + "' order by id Asc";
        oo.FillDropDown_withValue(sql, drpGroupName, "CoscholasticGroup", "Id");
    }
    public void loadCoscholastic()
    {
        sql = "Select CoscholasticName,cm.id as Id from CoscholasticMaster cm";
        sql +=  " inner join CoscholasticGroupMaster cgm on cgm.Id=cm.GroupId";
        sql +=  " where cm.ClassId='" + drpclass.SelectedValue.ToString() + "' and cgm.BranchCode=" + Session["BranchCode"] + " and cm.BranchCode=" + Session["BranchCode"] + " and Groupid='" + drpGroupName.SelectedValue.ToString() + "' and cm.Medium='" + drpmedium.SelectedValue.ToString() + "'";
        sql +=  " except";
        sql +=  " Select CoscholasticName,cm.id as Id from CoscholasticMaster cm";
        sql +=  " inner join CoscholasticGroupMaster cgm on cgm.Id=cm.GroupId where cm.ClassId='" + drpclass.SelectedValue.ToString() + "'";
        sql +=  " and Groupid='" + drpGroupName.SelectedValue.ToString() + "' and cgm.BranchCode=" + Session["BranchCode"] + " and cm.BranchCode=" + Session["BranchCode"] + " and SelectedItem=0 and cm.Medium='" + drpmedium.SelectedValue.ToString() + "' order by id Asc ";
        oo.FillDropDown_withValue(sql, drpCoscholastic, "CoscholasticName", "Id");
        drpCoscholastic.Items.Insert(0, "<--Select-->");
    }
    public void loadsection()
    {
        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select SectionName from SectionMaster where ClassNameId='" + drpclass.SelectedValue.ToString() + "'";
            sql +=  " and   BranchCode=" + Session["BranchCode"].ToString() + " and SessionName='" + Session["SessionName"].ToString() + "'";
            oo.FillDropDown(sql, drpsection, "SectionName");
        }
        else
        {
            sql = "Select  Distinct cm.SectionName from ClassTeacherMaster T1";
            sql +=  " inner join SectionMaster cm on cm.ClassNameId=T1.ClassId and cm.id=T1.SectionId and cm.SessionName=t1.SessionName and cm.BranchCode=t1.BranchCode";
            sql +=  " where  T1.ClassId=" + drpclass.SelectedValue + " and EmpCode='" + Session["LoginName"].ToString() + "' and T1.BranchCode=" + Session["BranchCode"] + " and T1.SessionName='" + Session["SessionName"] + "'";
            BAL.objBal.FillDropDown_withValue(sql, drpsection, "SectionName", "SectionName");
        }
    }
    protected void drpclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadsection();
        loadCoscholasticGroup();
        loadCoscholastic();
        loadgrid();
    }
    
    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
       
        if (GridView1.Rows.Count > 0)
        {
            int row = 0;
            foreach (GridViewRow gvr in GridView1.Rows)
            {
                Label Label16 = (Label)gvr.FindControl("Label16");
                Label lblGrade = (Label)gvr.FindControl("lblGrade");
                cmd = new SqlCommand();
                cmd.CommandText = "CCEVItoXCoscholasticProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Classid", drpclass.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@SectionName", drpsection.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@Evaluation", drpEval.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@CoscholasticId", drpCoscholastic.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@SrNo", Label16.Text);
                cmd.Parameters.AddWithValue("@Grade", lblGrade.Text.Trim());
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                con.Open();
                cmd.ExecuteNonQuery();
                row = row + 1;
                con.Close();
            }
            if (row > 0)
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
                oo.ClearControls(table1);
                drpCoscholastic.SelectedIndex = 0;
                txtMax.Text = "";
                foreach (GridViewRow gvr in GridView1.Rows)
                {
                    Label lblGrade = (Label)gvr.FindControl("lblGrade");
                    lblGrade.Text = "";
                    TextBox TextBox1 = (TextBox)gvr.FindControl("TextBox1");
                    TextBox1.Text = "";
                }

            }
        }
    }
   
    protected void drpsection_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadCoscholastic();
        loadgrid();
    }
    protected void drpCoscholastic_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadgrid();
        if (GridView1.Rows.Count > 0)
        {
            loadmaxmark();
            sql = "Select *from CCEVItoXCoscholastic where ClassId='" + drpclass.SelectedValue.ToString() + "' and BranchCode=" + Session["BranchCode"] + " and CoscholasticId='" + drpCoscholastic.SelectedValue.ToString() + "' and Evaluation='" + drpEval.SelectedValue.ToString() + "' and SectionName='" + drpsection.SelectedItem.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "'";
            if (oo.Duplicate(sql))
            {
                checkpermission(drpCoscholastic);
            }
            else
            {
                foreach (GridViewRow gvr in GridView1.Rows)
                {
                    TextBox Text1 = (TextBox)gvr.FindControl("TextBox1");
                    if (drpCoscholastic.SelectedIndex != 0)
                    {
                        Text1.Enabled = true;
                    }
                    else
                    {
                        Text1.Enabled = false;
                    }
                }

            }
            foreach (GridViewRow gvr in GridView1.Rows)
            {
                Label Label6 = (Label)gvr.FindControl("Label6");
                Label lblGrade = (Label)gvr.FindControl("lblGrade");
                TextBox Text1 = (TextBox)gvr.FindControl("TextBox1");
                Label lblsrno = (Label)gvr.FindControl("Label16");

                sql = "Select Id,Grade,GradePoint from CCEVItoXCoscholastic where SRNO='" + lblsrno.Text + "' and BranchCode=" + Session["BranchCode"] + " and Evaluation='" + drpEval.SelectedItem.Text + "' and CoscholasticId='" + drpCoscholastic.SelectedValue.ToString() + "'";
                Label6.Text = oo.ReturnTag(sql, "Id");
                Text1.Text = oo.ReturnTag(sql, "Grade");
                lblGrade.Text = oo.ReturnTag(sql, "Grade");
            }


        }
    }
    public void checkpermission(Control ctrl)
    {
        sql = "Select Permission from PermissionForMarksUpdate where EmpCode='" + Session["LoginName"].ToString() + "' and BranchCode=" + Session["BranchCode"] + "";
        if (oo.ReturnTag(sql, "Permission") == "true")
        {
            if (GridView1.Rows.Count > 0)
            {
                foreach (GridViewRow gvr in GridView1.Rows)
                {
                    TextBox TextBox1 = (TextBox)gvr.FindControl("TextBox1");
                    TextBox1.Enabled = true;
                }
            }
        }
        else
        {
            if (GridView1.Rows.Count > 0)
            {
                foreach (GridViewRow gvr in GridView1.Rows)
                {
                    TextBox TextBox1 = (TextBox)gvr.FindControl("TextBox1");
                    TextBox1.Enabled = false;
                }
                oo.MessageBoxforUpdatePanel("Permission not granted", ctrl);
            }
        }
    }
    public void loadmaxmark()
    {
        sql = "Select MaxMarks from CoscholasticMaster where Id='" + drpCoscholastic.SelectedValue.ToString() + "' and BranchCode=" + Session["BranchCode"] + "";
        txtMax.Text = oo.ReturnTag(sql, "MaxMarks");
    }
    protected void drpGroupName_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadCoscholastic();
    }

    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {
        TextBox txt = (TextBox)sender;
        GridViewRow currentrow = (GridViewRow)((Control)sender).NamingContainer;
        TextBox Text1 = (TextBox)currentrow.FindControl("TextBox1");
        Label lblGrade = (Label)currentrow.FindControl("lblGrade");
        Text1.Text = Text1.Text.Trim().ToUpper();
        if (Text1.Text == "A" || Text1.Text == "B" || Text1.Text == "C" || Text1.Text == "D" || Text1.Text == "E" || Text1.Text == "-" || Text1.Text == "")
        {
            lblGrade.Text = Text1.Text;
            int rowindex = currentrow.RowIndex + 1;
            try
            {
                TextBox test = (TextBox)GridView1.Rows[rowindex].FindControl(txt.ID);
                test.Focus();
            }
            catch { }
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Please enter grads A,B,C,D,E or (-) only!", "A");
            lblGrade.Text = Text1.Text="";
            Text1.Focus();
            return;
        }
    }

    protected void drpmedium_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadCoscholasticGroup();
        loadCoscholastic();
        loadgrid();
    }
}