using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class admin_Subject_OptSubjectMngnt : Page
{
    public static string MSG = "", SQL = "";
    public static int SubjectGroupMaster_ID = 0;
    public static DataTable dt = new DataTable();

    protected void Page_preinit(object sender, EventArgs e)
    {
        if ((string)Session["LoginName"] == "" || (string)Session["BranchCode"] == "" || (string)Session["SessionName"] == "" || Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("../default.aspx");
        }
        
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Campus camp = new Campus(); camp.LoadLoader(loader); 
        if (!IsPostBack)
        {
            btnInsert.Visible = true;
            btnUpdate.Visible = false;
            GetOptionalSubjectManagement();
            GetClasss();
            GetClassBranch();
            GetClassSection();
            ddlOptionalSubject.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
    }
    public void GetClasss()
    {
        Campus oo = new Campus();
        SQL = "select * from classmaster where sessionName='"+ Session["SessionName"].ToString()+ "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        oo.FillDropDown_withValue(SQL, ddlClass, "ClassName", "Id");
        ddlClass.Items.Insert(0, new ListItem("<--Select-->", ""));
    }

    public void GetClassSection()
    {
        Campus oo = new Campus();
        SQL = "select * from sectionmaster where ClassNameId='" + ddlClass.SelectedValue + "' and sessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        oo.FillDropDown_withValue(SQL, ddlClassSection, "SectionName", "Id");
        ddlClassSection.Items.Insert(0, new ListItem("<--Select-->", ""));
    }

    public void GetClassBranch()
    {
        Campus oo = new Campus();
        SQL = "select * from branchmaster where ClassId='" + ddlClass.SelectedValue + "' and sessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        oo.FillDropDown_withValue(SQL, ddlClassBranch, "BranchName", "Id");
        ddlClassBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
    }

    public void GetSubjectGroup()
    {
        Campus oo = new Campus();
        SQL = "select * from SubjectGroupMaster where ClassId='" + ddlClass.SelectedValue + "' and SectionName='" + ddlClassSection.SelectedItem.Text + "' and sessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        oo.FillDropDown_withValue(SQL, ddlOptionalSubject, "SubjectGroup", "Id");
        ddlOptionalSubject.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    public void Reset()
    {
        ddlClass.SelectedIndex = 0;
        cblCorrespondOptnSubject.Items.Clear();
        ddlOptionalSubject.Items.Clear();
    }
    public void Validation(Control cntl)
    {
        MSG = "";
        if (ddlClass.SelectedIndex<1)
        {
            MSG += "Select Class !" + "\\n";
        }
        if (ddlClassBranch.SelectedIndex < 1)
        {
            MSG += "Select Class Branch !" + "\\n";
        }
        if (ddlClassSection.SelectedIndex < 1)
        {
            MSG += "Select Class Section !" + "\\n";
        }
        if (ddlOptionalSubject.SelectedIndex<1)
        {
            MSG += "Select Optiponal Subject Group !"+"\\n";
        }
        if(cblCorrespondOptnSubject.SelectedIndex==-1)
        {
            MSG += "Select Relative Optional Subject  Group !";
        }
        if (MSG == "")
        {
            InsertShiftMaster(cntl);
        }
        else
        {
            //new Campus().MessageBoxforUpdatePanel(MSG, cntl);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, new ShowMSG().MSG(MSG), "A");       


        }
    }
    List<BAL.clsOptionalSubjectManagement> objList = new List<BAL.clsOptionalSubjectManagement>();
    public void InsertShiftMaster(Control cntr)
    {
        BAL.clsOptionalSubjectManagement obj;
        foreach(ListItem LI in cblCorrespondOptnSubject.Items)
        {
            if (LI.Selected)
            {
                obj = new BAL.clsOptionalSubjectManagement();
                obj.SubjectGroupMaster_ID = Convert.ToInt16(ddlOptionalSubject.SelectedValue);
                obj.SubjectGroupMaster_ID_Opt = Convert.ToInt16(LI.Value);
                obj.SQL = SQL;
                obj.ClassID = Convert.ToInt32(ddlClass.SelectedValue);
                obj.SectionID = Convert.ToInt32(ddlClassSection.SelectedValue);
                obj.BranchID = Convert.ToInt32(ddlClassBranch.SelectedValue);
                obj.SessionName = Session["SessionName"].ToString();

                objList.Add(obj);
            }
        }
        if (objList.Count > 0)
        {
            MSG = new DAL().SetOptionalSubjectManagement(objList);
            if (MSG == "")
            {
                btnInsert.Visible = true;
                btnUpdate.Visible = false;
                GetOptionalSubjectManagement();
                if (SQL == "I")
                    MSG = "Successfully Inserted !";
                if (SQL == "U")
                    MSG = "Successfully Updated !";
                Reset();
            }         
        }
        else
        {
            MSG = "Select Relative Optional Subject !";
        }
        //new Campus().MessageBoxforUpdatePanel(MSG, cntr);
        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, new ShowMSG().MSG(MSG), "S");       

    }
    public void GetOptionalSubjectManagement()
    {
        dt = new DAL().GetOptionalSubjectManagement(-1,Session["SessionName"].ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            dt = new BLL().GetSerialNo(ref dt, "SrNo");
            gvOptnSubject.DataSource = dt;
            gvOptnSubject.DataBind();
        }
        else
        {
            gvOptnSubject.DataSource = null;
            gvOptnSubject.DataBind();
            //new Campus().MessageBox("No Record Found !", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, new ShowMSG().MSG(MSG), "A");       

            //Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, Duplicate Entry!", "A");       

        }
    }
    protected void btnInsert_Click(object sender, EventArgs e)
    {
        SQL = "I";
        Validation(btnInsert);
    }
    protected void lbtnEdit_Click(object sender, EventArgs e)
    {
        btnInsert.Visible = false;
        btnUpdate.Visible = true;
        GridViewRow Row = (GridViewRow)(sender as LinkButton).Parent.Parent;
        LinkButton btn = (LinkButton)sender;
        SubjectGroupMaster_ID = Convert.ToInt16(btn.Text);
        int i = Row.RowIndex;
        GetClasss();    
        ddlClass.Items.FindByText(gvOptnSubject.Rows[i].Cells[1].Text.Trim()).Selected = true;
        GetClassSection();
        ddlClassSection.Items.FindByText(gvOptnSubject.Rows[i].Cells[2].Text.Trim()).Selected = true;
        GetClassBranch();
        ddlClassBranch.Items.FindByText(gvOptnSubject.Rows[i].Cells[3].Text.Trim()).Selected = true;
        GetSubjectGroup();
        ddlOptionalSubject.Items.FindByText(gvOptnSubject.Rows[i].Cells[4].Text.Trim()).Selected = true;
        GetRelativeOptSubject();
        string RelativeOptSubject = gvOptnSubject.Rows[i].Cells[5].Text.Trim();
        string[] AllSubject = RelativeOptSubject.Split(',');
        if (AllSubject.Length>0)
        {
            foreach (string I in AllSubject)
            {
                cblCorrespondOptnSubject.Items.FindByText(I).Selected = true;
            }
        }
    }
    protected void lbtnDelete_Click(object sender, EventArgs e)
    {
        btnNo.Focus();
        LinkButton lbtn = (LinkButton)sender;
        Label LabelId = (Label)lbtn.NamingContainer.FindControl("Label37");
        SubjectGroupMaster_ID = Convert.ToInt16(LabelId.Text);
        mpeDelete.Show();
    }
    protected void btnUpdate_Click1(object sender, EventArgs e)
    {
        SQL = "U";
        DeleteRecord();
        Validation(btnUpdate);
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        Reset();
    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
        DeleteRecord();
        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox,"Deleted Successfully.", "S"); 
        GetOptionalSubjectManagement();

    }
    public void DeleteRecord()
    {
        //new DAL().DeleteRecord("UPDATE S01_OptionalSubjectManagement SET IsDelete=1 WHERE SubjectGroupMaster_ID=" + SubjectGroupMaster_ID);
        new DAL().DeleteRecord("DELETE FROM S01_OptionalSubjectManagement WHERE SubjectGroupMaster_ID=" + SubjectGroupMaster_ID);
    }
    protected void btnNo_Click(object sender, EventArgs e)
    {

    }
    protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetClassSection();
        GetClassBranch();

        GetSubjectGroup();
    }
    protected void ddlOptionalSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetRelativeOptSubject();
    }
    public void GetRelativeOptSubject()
    {
        cblCorrespondOptnSubject.Items.Clear();
        if (ddlOptionalSubject.SelectedIndex > 0)
        {
            dt = new DAL().GetSubjectGroup(-1, Convert.ToInt16(ddlClass.SelectedValue), ddlClassSection.SelectedItem.Text, Session["SessionName"].ToString(), 0, Convert.ToInt32(ddlOptionalSubject.SelectedValue), Convert.ToInt32(ddlClassBranch.SelectedValue));
            if (dt != null && dt.Rows.Count > 0)
            {           
                cblCorrespondOptnSubject.DataSource = dt;
                cblCorrespondOptnSubject.DataTextField = "SubjectGroupName";
                cblCorrespondOptnSubject.DataValueField = "ID";
                cblCorrespondOptnSubject.DataBind();

                relsubgroup.Visible = true;
            }
        }
        else
        {

        }
    }
    protected void ddlClassBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetSubjectGroup();
    }
    protected void ddlClassSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetSubjectGroup();
    }
}