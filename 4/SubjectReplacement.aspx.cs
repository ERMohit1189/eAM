using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class admin_SubjectReplacement : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    SqlCommand cmd = new SqlCommand();
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
    }
    protected void txtHeaderEmpId_TextChanged(object sender, EventArgs e)
    {
        var empId = Request.Form[hfEmployeeId.UniqueID];
        if (empId == string.Empty)
        {
            empId = txtHeaderEmpId.Text.Trim();
        }
        displayEmpInfo();
    }
    protected void lnkShow_Click(object sender, EventArgs e)
    {

        var empId = Request.Form[hfEmployeeId.UniqueID];
        if (empId == string.Empty)
        {
            empId = txtHeaderEmpId.Text.Trim();
        }
        displayEmpInfo();
    }
    public void displayEmpInfo()
    {
        var empId = Request.Form[hfEmployeeId.UniqueID];
        if (empId == string.Empty)
        {
            empId = txtHeaderEmpId.Text.Trim();
        }
        sql = "Select eod.EmpId EmpId,eod.Ecode Ecode,egd.EFirstName+' '+egd.EMiddleName+' '+egd.ELastName as EmpName,egd.EFatherName FatherName,eod.Designation Designation,egd.EMotherName,egd.EMobileNo,Convert(varchar(11),eod.RegistrationDate,106) as RegistrationDate  from EmpployeeOfficialDetails eod ";
        sql = sql + " inner join EmpGeneralDetail egd on eod.Ecode=egd.Ecode and eod.EmpId=egd.EmpId where ";
        sql = sql + " eod.ecode='" + empId + "'";
        Grd.DataSource = oo.GridFill(sql);
        Grd.DataBind();
        if (Grd.Rows.Count == 0)
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, Div3, "Sorry, No Record found!", "A");
            div1.Visible = false;
        }
        else
        {
            DisplayRecord();
            div1.Visible = true;
            Label lblEcodeold = (Label)Grd.Rows[0].FindControl("lblEcode");
            Label lblEmpIdold = (Label)Grd.Rows[0].FindControl("lblEmpId");
            if (Grd1.Rows.Count > 0)
            {
               
                //Label lblEcodenew = (Label)GridView1.Rows[0].FindControl("lblEcode");
                //Label lblEmpIdnew = (Label)GridView1.Rows[0].FindControl("lblEmpId");
                //if (lblEcodeold.Text == lblEcodenew.Text && lblEmpIdold.Text == lblEmpIdnew.Text)
                //{
                //    Grd.DataSource = null;
                //    Grd.DataBind();
                //    lnkSubmit.Visible = false;
                //    //oo.MessageBoxforUpdatePanel("Both, old and new " + drpEnter.Text + " are same!", lnkShowNew);
                //    Campus camp = new Campus(); camp.msgbox(this.Page, Div3, "Both, old and new username are same!", "A");

                //}
                //else
                //{
                //    lnkSubmit.Visible = true;
                //}
            }
        }
    }
    public void DisplayRecord()
    {
        var empId = Request.Form[hfEmployeeId.UniqueID];
        if (empId == string.Empty)
        {
            empId = txtHeaderEmpId.Text.Trim();
        }
        sql = "select sctm.Id,sctm.EmpId,sctm.Ecode,sctm.EmpName,sctm.SectionName,cm.ClassName,sctm.Medium,sctm.Subjectid,sm.SubjectName,sctm.Classteacher,";
        sql = sql + " cm.CidOrder,sgm.subjectgroup,bm.BranchName from SubjectClassteacherMaster sctm ";
        sql = sql + " inner join ClassMaster cm on cm.Id=sctm.ClassId ";
        sql = sql + " inner join BranchMaster bm on bm.Id=sctm.BranchId";
        sql = sql + " inner join SubjectMaster sm on sm.Id=sctm.Subjectid";
        sql = sql + " inner join SubjectGroupMaster sgm on sgm.Id=sm.Groupid and sgm.sessionname=sm.sessionname";
        sql = sql + " inner join EmpployeeOfficialDetails eod on eod.EmpId=sctm.Empid and eod.Ecode=sctm.Ecode";
        sql = sql + " where cm.SessionName='" + Session["SessionName"].ToString() + "' and sctm.SessionName='" + Session["SessionName"].ToString() + "'";
        sql = sql + " and sctm.BranchCode=" + Session["BranchCode"].ToString() + " and sm.BranchCode=" + Session["BranchCode"].ToString() + " and sgm.BranchCode=" + Session["BranchCode"].ToString() + " and cm.BranchCode=" + Session["BranchCode"].ToString() + " and sm.SessionName='" + Session["SessionName"].ToString() + "' ";
        sql = sql + " and eod.ecode='" + empId + "' Order by CidOrder";
        Grd1.DataSource = oo.GridFill(sql);
        Grd1.DataBind();
    }
    protected void txtHeaderEmpId2_TextChanged(object sender, EventArgs e)
    {
        var empId2 = Request.Form[hfEmployeeId2.UniqueID];
        if (empId2 == string.Empty)
        {
            empId2 = txtHeaderEmpId.Text.Trim();
        }
        displayEmpInfoNew();
    }
    protected void lnkShowNew_Click(object sender, EventArgs e)
    {
        var empId = Request.Form[hfEmployeeId.UniqueID];
        if (empId == string.Empty)
        {
            empId = txtHeaderEmpId.Text.Trim();
        }
        var empId2 = Request.Form[hfEmployeeId2.UniqueID];
        if (empId2 == string.Empty)
        {
            empId2 = txtHeaderEmpId2.Text.Trim();
        }
        if (Grd.Rows.Count > 0)
        {
            if (empId != empId2)
            {
                displayEmpInfoNew();
            }
            else
            {
                //oo.MessageBoxforUpdatePanel("Both, old and new " + drpEnter.Text +" are same!", lnkShowNew);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Both, old and new username are same!", "A");

            }
        }
        else
        {
            //oo.MessageBoxforUpdatePanel("Please, enter old "+ drpEnter.Text+"!", lnkShowNew);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please, enter old username!", "A");

        }
    }
    public void displayEmpInfoNew()
    {
        var empId2 = Request.Form[hfEmployeeId2.UniqueID];
        if (empId2 == string.Empty)
        {
            empId2 = txtHeaderEmpId2.Text.Trim();
        }
        sql = "Select eod.EmpId EmpId,eod.Ecode Ecode,egd.EFirstName+egd.EMiddleName+egd.ELastName as EmpName,egd.EFatherName FatherName,eod.Designation Designation,egd.EMotherName,egd.EMobileNo,Convert(varchar(11),eod.RegistrationDate,106) as RegistrationDate  from EmpployeeOfficialDetails eod ";
        sql = sql + " inner join EmpGeneralDetail egd on eod.Ecode=egd.Ecode and eod.EmpId=egd.EmpId where eod.Withdrwal is null ";
        sql = sql + " and egd.BranchCode=" + Session["BranchCode"].ToString() + " and eod.BranchCode=" + Session["BranchCode"].ToString() + " and eod.ecode='" + empId2 + "'";
        GridView1.DataSource = oo.GridFill(sql);
        GridView1.DataBind();
        if (GridView1.Rows.Count == 0)
        {
            //oo.MessageBoxforUpdatePanel("Sorry, No Record found!", lnkShowNew);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, No Record found!", "A");

        }
        else
        {
            Label lblEcodeold = (Label)Grd.Rows[0].FindControl("lblEcode");
            Label lblEmpIdold = (Label)Grd.Rows[0].FindControl("lblEmpId");

            Label lblEcodenew = (Label)GridView1.Rows[0].FindControl("lblEcode");
            Label lblEmpIdnew = (Label)GridView1.Rows[0].FindControl("lblEmpId");

            if (lblEcodeold.Text == lblEcodenew.Text && lblEmpIdold.Text == lblEmpIdnew.Text)
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                Grd1.DataSource = null;
                Grd1.DataBind();
                lnkSubmit.Visible = false;
                //oo.MessageBoxforUpdatePanel("Both, old and new " + drpEnter.Text + " are same!", lnkShowNew);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Both, old and new username are same!", "A");

            }
            else
            {
                lnkSubmit.Visible = true;
            }

        }
    }
    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        if (Grd1.Rows.Count > 0 && GridView1.Rows.Count > 0 && Grd.Rows.Count > 0)
        {
            int checkedchk = 0;
            for (int i = 0; i < Grd1.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)Grd1.Rows[i].FindControl("CheckBox1");
                if (chk.Checked)
                {
                    checkedchk = checkedchk + 1;
                    Label lblSubjectName = (Label)Grd1.Rows[i].FindControl("lblSubjectName");
                    Label lblSubjectId = (Label)Grd1.Rows[i].FindControl("lblSubjectId");
                    Label lblEcodeold = (Label)Grd.Rows[0].FindControl("lblEcode");
                    Label lblEmpIdold = (Label)Grd.Rows[0].FindControl("lblEmpId");
                    Label lblEcodenew = (Label)GridView1.Rows[0].FindControl("lblEcode");
                    Label lblEmpIdnew = (Label)GridView1.Rows[0].FindControl("lblEmpId");
                    Label lblEmpNamenew = (Label)GridView1.Rows[0].FindControl("lblEmpName");

                    cmd = new SqlCommand("SubjectReplacementProc", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Old_LoginId", lblEcodeold.Text);
                    cmd.Parameters.AddWithValue("@New_LoginId", lblEcodenew.Text);
                    cmd.Parameters.AddWithValue("@Subject_Id", lblSubjectId.Text);
                    cmd.Parameters.AddWithValue("@Subject_Name", lblSubjectName.Text);
                    cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                    cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                    cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    sql = "Update SubjectClassteacherMaster set Empid='" + lblEmpIdnew.Text + "', ECode='" + lblEcodenew.Text + "',EmpName='" + lblEmpNamenew.Text.Trim() + "'";
                    sql = sql + " where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and Empid='" + lblEmpIdold.Text + "' and ECode='" + lblEcodeold.Text + "' and Subjectid='" + lblSubjectId.Text + "'";
                    oo.ProcedureDatabase(sql);
                    lnkSubmit.Visible = false;
                    div1.Visible = false;
                    Grd.DataSource = null;
                    Grd.DataBind();
                    Grd1.DataSource = null;
                    Grd1.DataBind();
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    Campus camp = new Campus(); camp.msgbox(this.Page, Div3, "Updated successfully.", "S");

                }
            }
            if (checkedchk == 0)
            {
                Campus camp = new Campus(); camp.msgbox(this.Page, Div2, "Please check atleast one checkbox from subject list!", "A");
            }
            DisplayRecord();
        }
    }
}