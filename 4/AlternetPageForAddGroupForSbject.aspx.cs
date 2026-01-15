using System;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class admin_AlternetPageForAddGroupForSbject : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    string sql = "";
    Campus oo = new Campus();

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
            sql = "Select Medium from MediumMaster";
            sql = sql + " where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            oo.FillDropDown(sql, drpmedium, "Medium");

            sql = "Select Id,ClassName from ClassMaster";
            sql = sql + " where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            sql = sql + "  order by CIDOrder";
            if (oo.Duplicate(sql) == false)
            {
                //oo.MessageBox("Class Master Not Fill!", this.Page);
                 camp.msgbox(this.Page, msgbox, "Class Master Not Fill!", "A");       

            }
            oo.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");
            loadsection();
            DisplayGrid();
        }
    }

    public void loadGroup()
    {
        sql = "select SubjectGroup,Id from SubjectGroupMaster where SessionName='" + Session["SessionName"].ToString() + "'";
        sql = sql + " and SectionName='" + drpSection.SelectedItem.ToString() + "' and Medium='" + drpmedium.SelectedItem.ToString() + "'  and BranchCode=" + Session["BranchCode"].ToString() + " and ClassID='" + drpclass.SelectedValue.ToString() + "'";
        oo.FillDropDown_withValue(sql, drpGroupName, "SubjectGroup", "Id");
    }

    public void DisplayGrid()
    {
        sql = "select ROW_NUMBER() OVER (ORDER BY sm.Id ASC) AS SrNo, sm.Id,sm.SectionName,";
        sql = sql + " cm.ClassName as ClassName,SubjectName,Medium";
        sql = sql + " from SubjectMaster sm inner join ClassMaster cm on cm.Id=sm.ClassId where ";
        sql = sql + " sm.SessionName='" + Session["SessionName"].ToString() + "' and cm.SessionName='" + Session["SessionName"].ToString() + "' ";
        sql = sql + " and sm.Groupid is null and cm.BranchCode=" + Session["BranchCode"].ToString() + " and sm.SectionName='" + drpSection.SelectedItem.ToString() + "' and sm.Medium='" + drpmedium.SelectedItem.ToString() + "'  and sm.BranchCode=" + Session["BranchCode"].ToString() + " and sm.ClassID='" + drpclass.SelectedValue.ToString() + "'";
        Grd.DataSource = oo.GridFill(sql);
        Grd.DataBind();
    }

    public void loadsection()
    {
        sql = "Select SectionName from SectionMaster";
        sql = sql + " where ClassNameId='" + drpclass.SelectedValue.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        oo.FillDropDown(sql, drpSection, "SectionName");
    }

    protected void DropDownList1_SelectedIndexChanged1(object sender, EventArgs e)
    {
        loadsection();
        loadGroup();
    }
    protected void drpmedium_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadGroup();
        DisplayGrid();
    }
    protected void drpSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadGroup();
        DisplayGrid();
    }
    protected void drpGroupName_SelectedIndexChanged(object sender, EventArgs e)
    {
        DisplayGrid();
    }
    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        update();
        DisplayGrid();
    }

    public void update()
    {
        for (int i = 0; i < Grd.Rows.Count; i++)
        {
            Label lblId = (Label)Grd.Rows[i].FindControl("Label6");
            CheckBox chk = (CheckBox)Grd.Rows[i].FindControl("CheckBox1");
            if (chk.Checked)
            {
                sql = "Update SubjectMaster Set GroupId='"+drpGroupName.SelectedValue.ToString()+"' where Id='"+lblId.Text+ "' and BranchCode=" + Session["BranchCode"].ToString() + "";
                cmd = new SqlCommand(sql,con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                //oo.MessageBoxforUpdatePanel("Updated Successfully", lnkSubmit);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated Successfully", "S");       

            }
        }
    }
}