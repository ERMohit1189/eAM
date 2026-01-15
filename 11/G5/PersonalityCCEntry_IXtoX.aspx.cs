using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

public partial class PersonalityCCEntry_IXtoX : Page
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
            Response.Redirect("default.aspx");
        }
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file
        if (!IsPostBack)
        {
            loadclass();
            loadsection();
            loadgrid();
        }
    }

    
    public void loadgrid()
    {
        sql = "select   So.srno,(SG.FirstName+' '+SG.MiddleName+' '+SG.LastName) as Name,sf.FatherName from StudentGenaralDetail SG ";
        sql +=  "    left join StudentFamilyDetails SF on SG.srno=SF.srno";
        sql +=  "   left join StudentOfficialDetails SO on SG.srno=SO.srno";
        sql +=  "   left join ClassMaster CM on SO.AdmissionForClassId=CM.Id";
        sql +=  "   left join SectionMaster SC on SO.SectionId=SC.Id";
        sql +=  "    where  CM.ClassName='" + drpclass.SelectedItem.ToString() + "'";
        sql +=  "  and SC.SectionName='" + drpsection.SelectedItem.ToString() + "' and cm.BranchCode=" + Session["BranchCode"] + " and sf.BranchCode=" + Session["BranchCode"] + " and so.BranchCode=" + Session["BranchCode"] + " and sc.BranchCode=" + Session["BranchCode"] + " and sg.BranchCode=" + Session["BranchCode"] + " and sg.SessionName='" + Session["SessionName"].ToString() + "' and ";
        sql +=  "     so.SessionName='" + Session["SessionName"].ToString() + "' and sf.SessionName='" + Session["SessionName"].ToString() + "' and cm.SessionName='" + Session["SessionName"].ToString() + "'";
        sql +=  "    and SC.SessionName='" + Session["SessionName"].ToString() + "'  and";
        sql +=  " sg.BranchCode=" + Session["BranchCode"].ToString() + "";
        sql +=  "   and SO.Withdrwal is null and  isnull(SO.Promotion,'')<>'Cancelled' order by FirstName Asc";
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
            sql +=  " inner join dt_ClassGroupMaster t1 on t1.ClassId=cm.Id and t1.SessionName=cm.SessionName  and cm.BranchCode=t1.BranchCode";
            sql +=  " where cm.SessionName='" + Session["SessionName"] + "' and cm.BranchCode=" + Session["BranchCode"] + " and GroupId='G5' Order by CIDOrder";
            oo.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");
        }
        else
        {
            sql = "Select Distinct ClassName,sctm.ClassId as Id,CIDOrder from ICSESubjectTeacherAllotment sctm";
            sql +=  " inner join ClassMaster cm on cm.Id=sctm.ClassId and cm.SessionName=sctm.SessionName and cm.BranchCode=sctm.BranchCode";
            sql +=  " inner join dt_ClassGroupMaster t1 on t1.ClassId=sctm.ClassId and t1.SessionName=sctm.SessionName and t1.BranchCode=sctm.BranchCode";
            sql +=  " where GroupId='G5'  and sctm.SessionName='" + Session["SessionName"].ToString() + "' and sctm.BranchCode=" + Session["BranchCode"] + " and Ecode='" + Session["LoginName"].ToString() + "' Order by CIDOrder";
            oo.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");
        }
    }
    public void loadsection()
    {
        sql = "Select SectionName from SectionMaster where ClassNameId='" + drpclass.SelectedValue.ToString() + "'";
        sql +=  " and   BranchCode=" + Session["BranchCode"].ToString() + " and SessionName='" + Session["SessionName"].ToString() + "'";
        oo.FillDropDown(sql, drpsection, "SectionName");
    }
    protected void drpclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadsection();
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
                TextBox Text1 = (TextBox)gvr.FindControl("TextBox1");
                TextBox Text2 = (TextBox)gvr.FindControl("TextBox2");
                cmd = new SqlCommand();
                cmd.CommandText = "PersonalityCCProc_IXtoX";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Classid", drpclass.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@SectionName", drpsection.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@GroupName", drpGroupName.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@SrNo", Label16.Text);
                cmd.Parameters.AddWithValue("@Grade1", Text1.Text.Trim());
                cmd.Parameters.AddWithValue("@Grade2", Text2.Text.Trim());
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                con.Open();
                row = cmd.ExecuteNonQuery();
                con.Close();
            }
            if (row > 0)
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
                loadgrid();
                foreach (GridViewRow gvr in GridView1.Rows)
                {
                    Label Label6 = (Label)gvr.FindControl("Label6");
                    TextBox Text1 = (TextBox)gvr.FindControl("TextBox1");
                    TextBox Text2 = (TextBox)gvr.FindControl("TextBox2");
                    Label lblsrno = (Label)gvr.FindControl("Label16");
                    sql = "Select Id,Grade1,Grade2 from PersonalityCC_IXtoX where SRNO='" + lblsrno.Text + "' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and GroupName='" + drpGroupName.SelectedValue.ToString() + "'";
                    Label6.Text = oo.ReturnTag(sql, "Id");
                    Text1.Text = oo.ReturnTag(sql, "Grade1");
                    Text2.Text = oo.ReturnTag(sql, "Grade2");
                }
            }
        }
    }
    
    protected void drpsection_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadgrid();
    }
    protected void drpGroupName_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadgrid();
        if (GridView1.Rows.Count > 0)
        {
            foreach (GridViewRow gvr in GridView1.Rows)
            {
                Label Label6 = (Label)gvr.FindControl("Label6");
                TextBox Text1 = (TextBox)gvr.FindControl("TextBox1");
                TextBox Text2 = (TextBox)gvr.FindControl("TextBox2");
                Label lblsrno = (Label)gvr.FindControl("Label16");
                sql = "Select Id,Grade1,Grade2 from PersonalityCC_IXtoX where SRNO='" + lblsrno.Text + "' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and GroupName='" + drpGroupName.SelectedValue.ToString() + "'";
                Label6.Text = oo.ReturnTag(sql, "Id");
                Text1.Text = oo.ReturnTag(sql, "Grade1");
                Text2.Text = oo.ReturnTag(sql, "Grade2");
            }
        }
    }
}