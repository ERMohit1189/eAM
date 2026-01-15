using System;
using System.Collections;
using System.Web.UI.WebControls;

public partial class admin_temp_AddDisplayOrder : System.Web.UI.Page
{
    string sql = "";
    Campus oo = new Campus();
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((string)Session["LoginName"] == "" || (string)Session["BranchCode"] == "" || (string)Session["SessionName"] == "" || Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
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

    protected void loadsection()
    {
        sql = "Select SectionName from SectionMaster";
        sql = sql + " where ClassNameId='" + drpclass.SelectedValue.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        oo.FillDropDown(sql, drpSection, "SectionName");
    }

    protected void loadSubjectGroup()
    {
        sql = "Select SubjectGroup as Subject,Id From SubjectGroupMaster where Classid='" + drpclass.SelectedValue.ToString() + "' and SectionName='" + drpSection.SelectedValue.ToString() + "' and BranchId='" + drpBranch.SelectedValue.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and IsForOnlyExam=1";
        GridView1.DataSource = oo.GridFill(sql);
        GridView1.DataBind();
        if (GridView1.Rows.Count > 0)
        {
            ArrayList listitem = new ArrayList();
            for (int i = 0; i<GridView1.Rows.Count; i++)
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
                        //oo.MessageBoxforUpdatePanel("Duplicate Value!", drp);
                        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Duplicate Value!", "A");       

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
                sql = "Update SubjectGroupMaster set DisplayOrder=" + drpOrder.SelectedValue + " where id='" + id.Text + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
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
                sql = "Select DisplayOrder From SubjectGroupMaster where id='" + id.Text + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
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
    protected void drpclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadsection();
        BLL.BLLInstance.loadBranch(drpBranch, Session["SessionName"].ToString(), drpclass.SelectedValue);
        GridView1.DataSource = null;
        GridView1.DataBind();
    }
    protected void drpBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadSubjectGroup();
    }
}