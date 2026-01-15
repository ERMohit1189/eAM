using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class admin_ClassAllotment_ng : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    SqlCommand cmd = new SqlCommand();
    Campus oo = new Campus();
    string sql = "";
#pragma warning disable 169
    int ii = 0;
#pragma warning restore 169
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
            loadclass();
            loadBranch();
            loadmedium();
            loadsection();
            div1.Visible = false;
            loadclass1();
            loadmedium1();           
        }
    }
    
    
    private void loadBranch()
    {
        sql = "Select BranchName,Id from BranchMaster Where ClassId='" + drpclass.SelectedValue.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        oo.FillDropDown_withValue(sql, drpBranch, "BranchName", "Id");
        oo.fillSelectvalue(drpBranch, "<--Select-->");
    }
    public void loadclass()
    {
        sql = "Select Id,ClassName,CidOrder from ClassMaster Where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " Order by CIDOrder";
        oo.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");
        oo.fillSelectvalue(drpclass,"<--Select-->");
    }

    public void loadsection()
    {
        sql = "Select SectionName from SectionMaster";
        sql = sql + " where ClassNameId='" + drpclass.SelectedValue.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        oo.FillDropDown(sql, drpSection, "SectionName");
        oo.fillSelectvalue(drpSection, "<--Select-->");
    }
    public void loadclass1()
    {
        sql = "Select Id,ClassName,CidOrder from ClassMaster Where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " Order by CIDOrder";
        oo.FillDropDown_withValue(sql, drpclass1, "ClassName", "Id");
    }

    private void loadBranch1()
    {
        sql = "Select BranchName,Id from BranchMaster Where ClassId='" + drpclass1.SelectedValue.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        oo.FillDropDown_withValue(sql, drpBranch1, "BranchName", "Id");
    }

    public void loadmedium()
    {
        sql = "Select Medium from MediumMaster";
        sql = sql + " Where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        oo.FillDropDown(sql, drpmedium, "Medium");
        drpmedium.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
    }
    public void loadmedium1()
    {
        sql = "Select Medium from MediumMaster";
        sql = sql + " Where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        oo.FillDropDown(sql, drpmedium1, "Medium");
        drpmedium1.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
    }
    public void loadsubject()
    {
        drpSubject.Items.Clear();
        if (drpmedium.SelectedItem.ToString()!= "<--Select-->")
        {
            sql = "Select Id,SubjectName from master_subjects Where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " ";
            sql = sql + " and ClassId='" + drpclass.SelectedValue.ToString() + "'  and (Medium='" + drpmedium.SelectedItem.ToString() + "' or Medium is null)";

            oo.FillDropDown_withValue(sql, drpSubject, "SubjectName", "Id");
            drpSubject.Items.Insert(0, new ListItem("All", "All"));
        }
    }
   
    protected void drpmedium_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadsubject();
    }
    protected void drpclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadBranch();
        loadsection();
        loadsubject();
    }
    public void displayEmpInfo()
    {
        var empId = Request.Form[hfEmployeeId.UniqueID];
        if (empId == string.Empty)
        {
            empId = txtHeaderEmpId.Text.Trim();
        }
        sql = "Select eod.EmpId EmpId,eod.Ecode Ecode,egd.EFirstName+egd.EMiddleName+egd.ELastName as EmpName,egd.EFatherName FatherName,eod.Designation Designation from EmpployeeOfficialDetails eod ";
        sql = sql + " inner join EmpGeneralDetail egd on eod.Ecode=egd.Ecode and eod.EmpId=egd.EmpId where eod.Withdrwal is null ";
        sql = sql + " and egd.BranchCode=" + Session["BranchCode"].ToString() + " and eod.BranchCode=" + Session["BranchCode"].ToString() + " and eod.ECode='" + empId.Trim() + "'";
        Grd.DataSource = oo.GridFill(sql);
        Grd.DataBind();
        if (Grd.Rows.Count > 0)
        {
            div1.Visible = true;
        }
        else
        {
            //oo.MessageBoxforUpdatePanel("Sorry, No Record found!", lnkShow);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, No Record found!", "A");       

            div1.Visible = false;
        }
    }
    protected void lnkShow_Click(object sender, EventArgs e)
    {
        displayEmpInfo();
        DisplayRecord();
    }
    protected void txtHeaderEmpId_TextChanged(object sender, EventArgs e)
    {
        displayEmpInfo();
        DisplayRecord();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (drpSection.SelectedIndex != 0 || drpmedium.SelectedIndex != 0)
        {
            if (drpSubject.SelectedItem.Text.ToUpper() == "ALL")
            {
                if (drpSubject.Items.Count > 1)
                {
                    SaveAll();
                }
                else
                {
                    //oo.MessageBoxforUpdatePanel("Sorry, No Subject Activity associated with this Subject Paper!", btnSubmit);
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, No Subject Activity associated with this Subject Paper!", "A");       

                }
            }
            else
            {
                Save();
            }
            
        }
        else
        {
            //oo.MessageBoxforUpdatePanel("Please, Select all Fields!", btnSubmit);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please, Select all Fields!", "A");       

        }
      
    }
    public void Save()
    {
        var empId = Request.Form[hfEmployeeId.UniqueID];
        if (empId == string.Empty)
        {
            empId = txtHeaderEmpId.Text.Trim();
        }
        sql = "Select *from SubjectClassteacherMaster where ECode='" + empId.Trim() + "'";
        sql = sql + " and Classid='" + drpclass.SelectedValue.ToString() + "' and BranchId='" + drpBranch.SelectedValue.ToString() + "' and SubjectId='" + drpSubject.SelectedValue.ToString() + "' ";
        sql = sql + " and Medium='" + drpmedium.SelectedItem.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        sql = sql + " and SectionName='"+drpSection.SelectedItem.ToString()+"'";
        if (oo.Duplicate(sql))
        {
            //oo.MessageBoxforUpdatePanel("Duplicate Record!", btnSubmit);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Duplicate Record!", "A");       

        }
        else
        {
            Label lblEcode = (Label)Grd.Rows[0].FindControl("lblEcode");
            Label lblEmpId = (Label)Grd.Rows[0].FindControl("lblEmpId");
            Label lblEmpName = (Label)Grd.Rows[0].FindControl("lblEmpName");
            Label lblFName = (Label)Grd.Rows[0].FindControl("lblFName");
            Label lblDesi = (Label)Grd.Rows[0].FindControl("lblDesi");

            cmd = new SqlCommand();
            cmd.CommandText = "SubjectClassteacherMasterProc";
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmpId", lblEmpId.Text);
            cmd.Parameters.AddWithValue("@Ecode", lblEcode.Text);
            cmd.Parameters.AddWithValue("@EmpName", lblEmpName.Text);
            cmd.Parameters.AddWithValue("@Classid", drpclass.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@BranchId", drpBranch.SelectedValue.ToString());
            //and (BranchId='" + drpclass.SelectedValue.ToString() + "' or BranchId is Null)
            cmd.Parameters.AddWithValue("@SectionName", drpSection.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@Medium", drpmedium.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@SubjectId", drpSubject.SelectedValue.ToString());
            //if (RadioButtonList1.Items[0].Selected)
            //{
            //    cmd.Parameters.AddWithValue("@ClassTeacher", "Yes");
            //}
            //else
            //{
            //    cmd.Parameters.AddWithValue("@ClassTeacher", "No");
            //}
            cmd.Parameters.AddWithValue("@Designation", lblDesi.Text.Trim());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
            //cmd.Parameters.AddWithValue("@StudentType", RadioButtonList3.SelectedItem.ToString());
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            DisplayRecord();
            //oo.MessageBoxforUpdatePanel("Submitted successfully", btnSubmit);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully", "S");       

        }
                                 
    }

    public void SaveAll()
    {
        var empId = Request.Form[hfEmployeeId.UniqueID];
        if (empId == string.Empty)
        {
            empId = txtHeaderEmpId.Text.Trim();
        }
        for (int i = 1; i < drpSubject.Items.Count; i++)
        {
            sql = "Select * from SubjectClassteacherMaster where ECode='" + empId.Trim() + "'";
            sql = sql + " and Classid='" + drpclass.SelectedValue.ToString() + "' and BranchId='" + drpBranch.SelectedValue.ToString() + "' and SubjectId='" + drpSubject.Items[i].Value.ToString() + "' ";
            sql = sql + " and Medium='" + drpmedium.SelectedItem.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            sql = sql + " and SectionName='" + drpSection.SelectedItem.ToString() + "'";
            if (oo.Duplicate(sql) == false)
            {
                Label lblEcode = (Label)Grd.Rows[0].FindControl("lblEcode");
                Label lblEmpId = (Label)Grd.Rows[0].FindControl("lblEmpId");
                Label lblEmpName = (Label)Grd.Rows[0].FindControl("lblEmpName");
                Label lblFName = (Label)Grd.Rows[0].FindControl("lblFName");
                Label lblDesi = (Label)Grd.Rows[0].FindControl("lblDesi");

                cmd = new SqlCommand();
                cmd.CommandText = "SubjectClassteacherMasterProc";
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmpId", lblEmpId.Text);
                cmd.Parameters.AddWithValue("@Ecode", lblEcode.Text);
                cmd.Parameters.AddWithValue("@EmpName", lblEmpName.Text);
                cmd.Parameters.AddWithValue("@Classid", drpclass.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@BranchId", drpBranch.SelectedValue.ToString());

                cmd.Parameters.AddWithValue("@SectionName", drpSection.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@Medium", drpmedium.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@SubjectId", drpSubject.Items[i].Value.ToString());

                cmd.Parameters.AddWithValue("@Designation", lblDesi.Text.Trim());
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                //oo.MessageBoxforUpdatePanel("Submitted successfully", btnSubmit);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully", "S");       

            }
        }
        DisplayRecord();
           
    }

    public void DisplayRecord()
    {
        var empId = Request.Form[hfEmployeeId.UniqueID];
        if (empId == string.Empty)
        {
            empId = txtHeaderEmpId.Text.Trim();
        }
        sql = "select sctm.Id,sctm.EmpId,sctm.Ecode,sctm.EmpName,sctm.SectionName,cm.ClassName,bm.BranchName,sctm.Medium,sm.SubjectName,sctm.Classteacher, ";
        sql = sql + " cm.CidOrder from SubjectClassteacherMaster sctm ";
        sql = sql + " inner join ClassMaster cm on cm.Id=sctm.ClassId Left join BranchMaster bm on bm.Classid=cm.id and sctm.Branchid=bm.id and bm.sessionName=cm.sessionNAme inner join master_subjects sm on sm.Id=sctm.Subjectid";
        sql = sql + " inner join EmpployeeOfficialDetails eod on eod.EmpId=sctm.Empid and eod.Ecode=sctm.Ecode";
        sql = sql + " where cm.SessionName='" + Session["SessionName"].ToString() + "' and sctm.SessionName='" + Session["SessionName"].ToString() + "'";
        sql = sql + " and sm.SessionName='" + Session["SessionName"].ToString() + "' ";
        sql = sql + "  and eod.BranchCode=" + Session["BranchCode"].ToString() + " and cm.BranchCode=" + Session["BranchCode"].ToString() + " and sm.BranchCode=" + Session["BranchCode"].ToString() + " and sctm.BranchCode=" + Session["BranchCode"].ToString() + " and eod.ECode='" + empId.Trim() + "' and eod.Withdrwal is null Order by CidOrder";
        Grd1.DataSource = oo.GridFill(sql);
        Grd1.DataBind();
    }   
    public void MergeRows(GridView gridView)
        {
            for (int rowIndex = gridView.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow row = gridView.Rows[rowIndex];
                GridViewRow previousRow = gridView.Rows[rowIndex + 1];
                for (int i = 0; i < row.Cells.Count-2; i++)
                {
                    if (((Label)(row.Cells[i].Controls[1])).Text == ((Label)(previousRow.Cells[i].Controls[1])).Text)
                    {
                        row.Cells[i].RowSpan = previousRow.Cells[i].RowSpan < 2 ? 2 : previousRow.Cells[i].RowSpan + 1;                       
                        previousRow.Cells[i].Visible = false;
                    }
                }
            }
        
    }
    public void loadsection1()
    {
        sql = "Select SectionName from SectionMaster";
        sql = sql + " where ClassNameId='" + drpclass1.SelectedValue.ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        oo.FillDropDown(sql, drpSection1, "SectionName");
    }
    protected void linkEdit_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId2 = (Label)chk.NamingContainer.FindControl("Label36");

        lblid.Text = lblId2.Text;
        sql = "select ClassId from SubjectClassteacherMaster ";
        sql = sql + " where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and Id=" + lblId2.Text;

        drpclass1.SelectedValue = oo.ReturnTag(sql, "ClassId");
        loadBranch1();
        loadsection1();


        sql = "Select Medium,ClassTeacher,BranchId,SectionName,Subjectid from SubjectClassteacherMaster ";
        sql = sql + " where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and Id=" + lblId2.Text;
        drpBranch1.SelectedValue = oo.ReturnTag(sql, "BranchId");
        drpSection1.SelectedValue = oo.ReturnTag(sql, "SectionName");
        drpmedium1.SelectedValue = oo.ReturnTag(sql, "Medium");
        Session["ClassTeacher"] = oo.ReturnTag(sql, "ClassTeacher");
        string subjectid = oo.ReturnTag(sql, "Subjectid");

        sql = "Select sm.Id smid,spm.S02ID spmid,sgm.Id sgmid from SubjectClassTeacherMaster sctm";
        sql = sql + " Inner join SubjectMaster sm on sm.Id=sctm.Subjectid and sm.SessionName=sctm.SessionName";
        sql = sql + " Inner join S02_SubjectPaperMaster spm on spm.S02ID=sm.PaperID and spm.SessionName=sctm.SessionName";
        sql = sql + " Inner join SubjectGroupMaster sgm on sgm.Id=sm.GroupId and sgm.SessionName=sctm.SessionName";
        sql = sql + " where sm.id='" + subjectid + "' and sgm.BranchCode=" + Session["BranchCode"].ToString() + " and spm.BranchCode=" + Session["BranchCode"].ToString() + " and sm.BranchCode=" + Session["BranchCode"].ToString() + " and sctm.BranchCode=" + Session["BranchCode"].ToString() + "";

        string smid = oo.ReturnTag(sql, "smid");
        string spmid = oo.ReturnTag(sql, "spmid");
        string sgmid = oo.ReturnTag(sql, "sgmid");

        Panel1_ModalPopupExtender.Show();
    }
    protected void drpmedium1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Panel1_ModalPopupExtender.Show();
    }
    protected void drpclass1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Panel1_ModalPopupExtender.Show();
    }
    protected void lnkUpdate_Click(object sender, EventArgs e)
    {
        var empId = Request.Form[hfEmployeeId.UniqueID];
        if (empId == string.Empty)
        {
            empId = txtHeaderEmpId.Text.Trim();
        }
        sql = "Select *from SubjectClassteacherMaster where ECode='" + empId.Trim() + "'";
        sql = sql + " and Classid='" + drpclass1.SelectedValue.ToString() + "' and BranchId='" + drpBranch1.SelectedValue.ToString() + "' and SubjectId='" + drpSubject1.SelectedValue.ToString() + "' ";
        sql = sql + " and Medium='" + drpmedium1.SelectedItem.ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and SessionName='" + Session["SessionName"].ToString() + "'";
        sql = sql + " and Id<>'" + lblid.Text + "'";
        if (oo.Duplicate(sql))
        {
            //oo.MessageBoxforUpdatePanel("Duplicate Record!", btnSubmit);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Duplicate Record!", "A");       

        }
        else
        {
            cmd = new SqlCommand();
            cmd.CommandText = "SubjectClassteacherMasterUpdateProc";
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", lblid.Text);
            cmd.Parameters.AddWithValue("@Classid", drpclass1.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@Branchid", drpBranch1.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@SectionName", drpSection1.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@Medium", drpmedium1.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@SubjectId", drpSubject1.SelectedValue.ToString());
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            //oo.MessageBoxforUpdatePanel("Updated successfully", lnkUpdate);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully", "S");       

            DisplayRecord();
        }
    }
    protected void linkDelete_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId3 = (Label)chk.NamingContainer.FindControl("Label37");
        string ss = lblId3.Text;
        lblvalue.Text = ss.ToString();
        Panel2_ModalPopupExtender.Show();
    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
        sql = "Delete From SubjectClassteacherMaster where SessionName='" + Session["SessionName"].ToString() + "'";
        sql = sql + " and BranchCode=" + Session["BranchCode"].ToString() + " and BranchCode=" + Session["BranchCode"].ToString() + " and Id=" + lblvalue.Text;
        cmd.CommandText = sql;
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        DisplayRecord();
        //oo.MessageBoxforUpdatePanel("Deleted successfully", btnYes);
        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Deleted successfully", "S");       

       
         
    }
    protected void drpSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadmedium();
        
    }
    protected void RadioButtonList3_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    
}