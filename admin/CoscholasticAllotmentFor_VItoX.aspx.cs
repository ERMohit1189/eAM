using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class staff_CoscholasticAllotmentFor_VItoX : Page
{
    string sql;
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    SqlCommand cmd;
    int k;

    public staff_CoscholasticAllotmentFor_VItoX()
    {
        k = 0;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        con = oo.dbGet_connection();
        if (!IsPostBack)
        {
            loadclass();
            loadSection();
            loadMedium();
            loadCoGroup();
            loadCoscholastic();
            //loadCoscholasticActivity();
            applyCondition();
            //enablelinkbutton();
        }
    }
    protected void loadStudent()
    {
        sql = "select SG.Id, SC.SectionName,SO.Card,SO.Medium as Medium,CM.ClassName,convert(nvarchar,So.DateOfAdmiission,106) as DateOfAdmiission ,SO.SectionId,Sf.FatherName,SF.MotherName,(SG.FirstName+' '+SG.MiddleName+' '+SG.LastName) as Name,so.StEnRCode as StEnRCode,sg.srno  as srno,case  when so.TransportRequired='Yes' then 'Yes' else 'No' end as TransportRequired,so.wayamount as wayamount from StudentGenaralDetail SG ";
        sql = sql + "    left join StudentFamilyDetails SF on SG.srno=SF.srno";
        sql = sql + "   left join StudentOfficialDetails SO on SG.srno=SO.srno";
        sql = sql + "   left join ClassMaster CM on SO.AdmissionForClassId=CM.Id";
        sql = sql + "   left join SectionMaster SC on SO.SectionId=SC.Id";
        sql = sql + "    where sg.SessionName='" + Session["SessionName"].ToString() + "' and ";
        sql = sql + "     so.SessionName='" + Session["SessionName"].ToString() + "' and sf.SessionName='" + Session["SessionName"].ToString() + "' and cm.SessionName='" + Session["SessionName"].ToString() + "'";
        sql = sql + "    and SC.SessionName='" + Session["SessionName"].ToString() + "'  and";
        sql = sql + " sg.BranchCode=" + Session["BranchCode"].ToString() + "";
        sql = sql + "   and SO.Withdrwal is null and SO.AdmissionForClassId='" + drpClass.SelectedValue.ToString() + "' and SO.SectionId='" + drpsection.SelectedValue.ToString() + "' and Medium='"+drpMedium.SelectedItem.Text+"'";

        GridView1.DataSource = oo.GridFill(sql);
        GridView1.DataBind();

       
    }
    public void checkdata()
    {
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            Label srno = (Label)GridView1.Rows[i].FindControl("Label2");
            CheckBox chk = (CheckBox)GridView1.Rows[i].FindControl("Chk");
            LinkButton LinkButton1 = (LinkButton)GridView1.Rows[i].FindControl("LinkButton1");
            sql = "Select *from CoScholasticAllotmentForStudent where Srno='" + srno.Text + "' and GroupId='" + drpCoscholasticGroup.SelectedValue.ToString() + "'  and BranchCode=" + Session["BranchCode"] + "";
            if (oo.Duplicate(sql))
            {
                chk.Enabled = false;
                chk.Checked = true;
                LinkButton1.Enabled = true;
            }
            else
            {
                chk.Enabled = true;
                chk.Checked = false;
                LinkButton1.Enabled = false;
            }
        }
    }
    //public void enablelinkbutton()
    //{
    //    for (int i = 0; i < GridView1.Rows.Count; i++)
    //    {
    //        Label srno = (Label)GridView1.Rows[i].FindControl("Label2");
    //        CheckBox chk = (CheckBox)GridView1.Rows[i].FindControl("Chk");
    //        LinkButton LinkButton1 = (LinkButton)GridView1.Rows[i].FindControl("LinkButton1");
    //        sql = "Select *from CoScholasticAllotmentForStudent where Srno='" + srno.Text + "' and GroupId='" + drpCoscholastic.SelectedValue.ToString() + "' ";
    //        if (oo.Duplicate(sql))
    //        {

                
    //        }
    //        else
    //        {

                
    //        }
    //    }
    //}
    protected void loadCoGroup()
    {
        sql = "Select CoscholasticGroup,Id From CoscholasticGroupMaster where SessionName='" + Session["SessionName"].ToString() + "'";
        sql = sql + " and Classid='" + drpClass.SelectedValue.ToString() + "' and partId='" + drpPart.SelectedValue.ToString() + "'";
        sql = sql + " and Medium='" + drpMedium.SelectedItem.ToString() + "' and BranchCode=" + Session["BranchCode"] + "";
        oo.FillDropDown_withValue(sql, drpCoscholasticGroup, "CoscholasticGroup", "Id");
        drpCoscholasticGroup.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
    }
    protected void loadMedium()
    {
        sql = "Select Medium,Id From MediumMaster where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"] + "";
        oo.FillDropDown_withValue(sql, drpMedium, "Medium", "Id");
    }
    public void loadclass()
    {
        sql = "Select Distinct ClassName,cm.Id as Id,CIDOrder from ClassMaster cm ";
        sql = sql + " inner join dt_ClassGroupMaster t1 on t1.ClassId=cm.Id";
        sql = sql + " where cm.SessionName='" + Session["SessionName"] + "'  and t1.BranchCode=" + Session["BranchCode"] + " and cm.BranchCode=" + Session["BranchCode"] + " and GroupId='G4' Order by CIDOrder";
        oo.FillDropDown_withValue(sql, drpClass, "ClassName", "Id");
    }
    //public void loadClass()
    //{
    //    sql = "Select Distinct ClassName,sctm.ClassId as Id,CIDOrder from SubjectClassTeacherMaster sctm";
    //    sql = sql + " inner join ClassMaster cm on cm.Id=sctm.ClassId";
    //    sql = sql + " inner join dt_ClassGroupMaster t1 on t1.ClassId=sctm.ClassId";
    //    sql = sql + " where (GroupId='G4' or GroupId='G5')  and cm.SessionName='" + Session["SessionName"] + "' and sctm.SessionName='" + Session["SessionName"] + "' and Ecode='" + Session["LoginName"].ToString() + "' Order by CIDOrder";
    //    oo.FillDropDown_withValue(sql, drpClass, "ClassName", "Id");
    //}


    //protected void loadClass()
    //{
    //    sql = "Select ClassName,Id From ClassMaster where SessionName='"+Session["SessionName"].ToString()+"'";
    //    oo.FillDropDown_withValue(sql, drpClass, "ClassName", "Id");
    //}

    protected void ChkAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkAll = (CheckBox)GridView1.HeaderRow.FindControl("ChkAll");
        if (chkAll.Checked)
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)GridView1.Rows[i].FindControl("Chk");
                chk.Checked = true;
            }
        }
        else
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)GridView1.Rows[i].FindControl("Chk");
                if (chk.Enabled == true)
                {
                    chk.Checked = false;
                }
            }
        }
    }

    protected void loadSection()
    {
        sql = "Select SectionName,Id From SectionMaster where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"] + "";
        sql = sql + " and ClassNameId='"+drpClass.SelectedValue.ToString()+"'";
        oo.FillDropDown_withValue(sql, drpsection, "SectionName", "Id");
    }
    //protected void loadCoscholasticActivity()
    //{
    //    sql = "Select CoscholasticName,cm.Id From CoscholasticMaster cm";
    //    sql = sql + " inner join CoscholasticGroupMaster cgm on cm.GroupId=cgm.Id";
    //    sql = sql + " where cm.SessionName='" + Session["SessionName"].ToString() + "'";
    //    sql = sql + " and cm.Classid='" + drpClass.SelectedValue.ToString() + "' and GroupId='" + drpCoscholastic.SelectedValue.ToString() + "'";
    //    sql = sql + " and cm.Medium='" + drpMedium.SelectedItem.ToString() + "'";

    //    oo.FillDropDown_withValue(sql, drpCoActivity, "CoscholasticName", "Id");
    //}
    protected void drpCoActivity_SelectedIndexChanged(object sender, EventArgs e)
    {
        applyCondition();
    }
    protected void drpCoscholastic_SelectedIndexChanged(object sender, EventArgs e)
    {
        //loadCoGroup();
        //loadCoscholasticActivity();
        loadCoscholastic();
        applyCondition();
    }

    private void applyCondition()
    {
        if (RadioButtonList1.SelectedIndex != 0)
        {
            loadStudent();
            checkdata();
        }
    }
    protected void drpsection_SelectedIndexChanged(object sender, EventArgs e)
    {        
        loadCoGroup();
        loadCoscholastic();
        //loadCoscholasticActivity();
        applyCondition();
    }
    protected void drpclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadSection();
        loadCoGroup();
        loadCoscholastic();
        //loadCoscholasticActivity();
        applyCondition();
        //enablelinkbutton();
    }
    protected void drpMedium_SelectedIndexChanged(object sender, EventArgs e)
    {       
        loadCoGroup();
        loadCoscholastic();
        //loadCoscholasticActivity();
        applyCondition();
    }
    protected void drpdrpPart_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadCoGroup();
        loadCoscholastic();
        //loadCoscholasticActivity();
        applyCondition();
    }

    protected void loadCoscholastic()
    {
        sql = "Select CoscholasticName,Id From CoscholasticMaster where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"] + "";
        sql = sql + " and GroupId='" + drpCoscholasticGroup.SelectedValue.ToString() + "'";
        //oo.FillDropDown_withValue(sql, drpCoscholastic, "CoscholasticName", "Id");
        //drpCoscholastic.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
        CheckBoxList1.DataSource = oo.GridFill(sql);
        CheckBoxList1.DataTextField = "CoscholasticName";
        CheckBoxList1.DataValueField = "Id";
        CheckBoxList1.DataBind();

    }

    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedIndex == 0)
        {

            for (int i = 0; i < CheckBoxList1.Items.Count; i++)
            {
                if (CheckBoxList1.Items[i].Selected)
                {
                    cmd = new SqlCommand();
                    cmd.CommandText = "CoScholasticAllotmentForClassProc";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@Classid", drpClass.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@Sectionid", drpsection.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@CoScholasticGroupId", drpCoscholasticGroup.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@CoScholasticId", CheckBoxList1.Items[i].Value.ToString());
                    cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                    cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                    cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    oo.MessageBoxforUpdatePanel("Submitted successfully.", lnkSubmit);
                }
            }
        }
        else
        {
            int i1 = 0;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)GridView1.Rows[i].FindControl("Chk");
                Label srno = (Label)GridView1.Rows[i].FindControl("Label2");
                if (chk.Checked)
                {
                    if (chk.Enabled)
                    {
                        i1 = 1;
                        sql = "Select *from CoScholasticAllotmentForStudent where Srno='" + srno.Text + "' and BranchCode=" + Session["BranchCode"] + " and GroupId='" + drpCoscholasticGroup.SelectedValue.ToString() + "' ";
                        if (oo.Duplicate(sql) == false)
                        {
                            cmd = new SqlCommand();
                            cmd.CommandText = "CoScholasticAllotmentForStudentProc";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Connection = con;
                            cmd.Parameters.AddWithValue("@srno", srno.Text.Trim());
                            cmd.Parameters.AddWithValue("@Classid", drpClass.SelectedValue.ToString());
                            cmd.Parameters.AddWithValue("@GroupId", drpCoscholasticGroup.SelectedValue.ToString());
                            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                            oo.MessageBoxforUpdatePanel("Submitted successfully.", lnkSubmit);
                        }
                    }

                }
                else
                {
                    sql = "Select *from CoScholasticAllotmentForStudent where Srno='" + srno.Text + "' and BranchCode=" + Session["BranchCode"] + " and GroupId='" + drpCoscholasticGroup.SelectedValue.ToString() + "' ";
                    if (oo.Duplicate(sql))
                    {
                        i1 = 1;
                        sql = "Delete From CoScholasticAllotmentForStudent where Srno='" + srno.Text + "' and BranchCode=" + Session["BranchCode"] + " and GroupId='" + drpCoscholasticGroup.SelectedValue.ToString() + "'";
                        SqlCommand cmd = new SqlCommand(sql, con);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }

            }
            if (i1 == 0)
            {
                oo.MessageBoxforUpdatePanel("Please, select Student!", lnkSubmit);
            }
           
        }
        applyCondition();
    }
    protected void LinkButton1_Click(object sender,EventArgs e)
    {
        GridViewRow currentrow = (GridViewRow)((Control)sender).Parent.Parent;
        k = currentrow.RowIndex;
        CheckBox chk = (CheckBox)GridView1.Rows[k].FindControl("Chk");
        chk.Enabled = true;
    }
    protected void lnkEditAll_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            CheckBox chk = (CheckBox)GridView1.Rows[i].FindControl("Chk");
            chk.Enabled = true;
        }
    }
}