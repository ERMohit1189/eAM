using System;
using System.Collections;
using System.Web.UI.WebControls;

public partial class admin_temp_AddDisplayOrder : System.Web.UI.Page
{
    string sql = "";
    Campus oo = new Campus();
    protected void Page_Load(object sender, EventArgs e)
    {
        Campus camp = new Campus(); camp.LoadLoader(loader); 
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        if (!IsPostBack)
        {
            loadClass();
            
        }
    }

    protected void loadClass()
    {
        sql = "Select Id,ClassName from ClassMaster";
        sql = sql + " where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        sql = sql + "  order by CIDOrder";
        oo.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");
        drpclass.Items.Insert(0, new ListItem("<--Select-->"));
    }

    protected void loadSubjectGroup()
    {
        sql = "Select SubjectGroup as SubjectGroup,Id From SubjectGroupMaster where Classid='" + drpclass.SelectedValue.ToString() + "' and SectionName='" + drpSection.SelectedValue.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " order by DisplayOrder Asc";
        oo.FillDropDown_withValue(sql, drpSubjectGroup, "SubjectGroup", "Id");
        drpSubjectGroup.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
        drpSubjectGroup.Items.Add(new ListItem("Other", "Other"));
    }

    protected void loadsection()
    {
        sql = "Select SectionName from SectionMaster";
        sql = sql + " where ClassNameId='" + drpclass.SelectedValue.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        oo.FillDropDown(sql, drpSection, "SectionName");
    }
   
    protected void drpDisplayNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList drp=(DropDownList)sender;
        var current=(GridViewRow)drp.NamingContainer;
        if (drp.SelectedIndex != 0)
        {
            string drpValue = drp.SelectedValue;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                if (current.RowIndex != i)
                {
                    DropDownList drpcompare = (DropDownList)GridView1.Rows[i].FindControl("drpDisplayNo");
                    if (drpValue == drpcompare.SelectedValue)
                    {
                        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Duplicate Value!", "A");       

                            //oo.MessageBoxforUpdatePanel("Duplicate Value!", drp);
                            drp.SelectedIndex = 0;
                            break;
                    }
                }
            }
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        bool flag=true;
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {           
                DropDownList drpcompare = (DropDownList)GridView1.Rows[i].FindControl("drpDisplayNo");
                if (drpcompare.SelectedIndex==0)
                {
                    //oo.MessageBoxforUpdatePanel("Please set dispaly order for each subject!", LinkButton1);
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please set dispaly order for each subject!", "A");       

                    drpcompare.Focus();
                    flag = false;
                    break;
                }
           
        }
        if (flag)
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                Label id = (Label)GridView1.Rows[i].FindControl("Label3");
                DropDownList drpOrder = (DropDownList)GridView1.Rows[i].FindControl("drpDisplayNo");
                sql = "Update SubjectMaster set DisplayOrder=" + drpOrder.SelectedValue + " where id='" + id.Text + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
                oo.ProcedureDatabase(sql);
                //oo.MessageBoxforUpdatePanel("Submitted successfully.", LinkButton1);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");       

            }            
        }
        loadGrid();
    }

    protected void loadGrid()
    {      
        if (GridView1.Rows.Count > 0)
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                DropDownList drpDisplayNo = (DropDownList)GridView1.Rows[i].FindControl("drpDisplayNo");
                Label id = (Label)GridView1.Rows[i].FindControl("Label3");
                sql = "Select DisplayOrder From SubjectMaster where id='" + id.Text + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
                if (oo.ReturnTag(sql, "DisplayOrder") != string.Empty)
                {
                    drpDisplayNo.SelectedValue = oo.ReturnTag(sql, "DisplayOrder");
                }
            }
        }
    }
  
    protected void drpSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadSubjectGroup();
    }
       
    protected void loadSubject()
    {
        if (drpSubjectGroup.SelectedIndex == drpSubjectGroup.Items.Count - 1)
        {
            sql = "select Id,SubjectName as Subject from SubjectMaster where ";
            sql = sql + " (GroupId is null or GroupId='') and SessionName='" + Session["SessionName"].ToString() + "' and SectionName='" + drpSection.SelectedValue.ToString() + "' and ClassID='" + drpclass.SelectedValue.ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " Order by Displayorder Asc";
        }
        else
        {
            sql = "select Id,SubjectName as Subject from SubjectMaster where ";
            sql = sql + " GroupId='" + drpSubjectGroup.SelectedValue.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and SectionName='" + drpSection.SelectedValue.ToString() + "' and ClassID='" + drpclass.SelectedValue.ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " Order by Displayorder Asc";
        }
        GridView1.DataSource = oo.GridFill(sql);
        GridView1.DataBind();
        if (GridView1.Rows.Count > 0)
        {
            ArrayList listitem = new ArrayList();
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                listitem.Add(i + 1);
            }

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                DropDownList drpDisplayNo = (DropDownList)GridView1.Rows[i].FindControl("drpDisplayNo");
                for (int j = 0; j < listitem.Count; j++)
                {
                    drpDisplayNo.Items.Add(new ListItem(listitem[j].ToString(), listitem[j].ToString()));
                }
                drpDisplayNo.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
            }
        }
        loadGrid();
    }

    protected void drpclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadsection();
        GridView1.DataSource = null;
        GridView1.DataBind();
    }
   
    protected void drpSubjectGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadSubject();
    }
}