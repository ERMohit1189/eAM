using System;
using System.Web.UI.WebControls;
using System.Data;

public partial class admin_OPeriodSubject : System.Web.UI.Page
{
    public static string MSG = "", SQL = "";
    public static int T07ID = 0;
    public static DataTable dt = new DataTable();

    protected void Page_Load(object sender, EventArgs e)    
    {
        if(!IsPostBack)      
        {  
            //btnInsert.Visible = true;
            //btnUpdate.Visible = false;
         
            GetClass(ddlClass);
        }
    }

    public void GetClass(DropDownList ddl)
    {
        dt = new DAL().GetClass(-1, Session["SessionName"].ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            BLL.FillDropDown(ddl, dt, "Class", "ClassID", 'S');
        }
    }

    public void GetClassSection(DropDownList ddl,DropDownList ddlC)
    {
        dt = null;
        dt = new DAL().GetClassSection(Convert.ToInt32(ddlC.SelectedValue), -1, Session["SessionName"].ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            BLL.FillDropDown(ddl, dt, "SectionName", "ClassSectionID", 'S');
        }
    }

    public void GetClassBranch(DropDownList ddl, DropDownList ddlC)
    {
        dt = null;
        dt = new DAL().GetClassBranch(Convert.ToInt32(ddlC.SelectedValue), -1, Session["SessionName"].ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            BLL.FillDropDown(ddl, dt, "BranchName", "ID", 'S');
        }
    }

    public void GetEmp()
    {
        ddlEmp.Items.Clear();
        ddlSubjectPaper.Items.Clear();

        if (ddlClass.SelectedIndex > 0)
        {
            if (ddlClassBranch.SelectedIndex > 0)
            {
                if (ddlClassSection.SelectedIndex > 0)
                {
                    if (ddlDay.SelectedIndex > 0)
                    {
                        if (Convert.ToInt32(Campus.CampusInstance.ReturnTag("SELECT ISNULL((SELECT DISTINCT 1 IsExists FROM T05_TimeTableClassPeriod T05 WHERE T05.ClassID='" + Convert.ToInt32(ddlClass.SelectedValue) + "' AND T05.SectionID='" + Convert.ToInt32(ddlClassSection.SelectedValue) + "' AND T05.BranchID='" + Convert.ToInt32(ddlClassBranch.SelectedValue) + "' AND T05.IsActive=1 AND T05.IsDelete=0),0) IsExists", "IsExists")) == 1)
                        {
                            dt = new DataTable();
                            dt = DAL.DALInstance.GetTeacherForOPeriod(Convert.ToInt32(ddlClass.SelectedValue), Convert.ToInt32(ddlClassSection.SelectedValue), Convert.ToInt32(ddlClassBranch.SelectedValue), (ddlDay.SelectedValue),-1);

                            if (dt != null && dt.Rows.Count > 0)
                            {
                                BLL.FillDropDown(ddlEmp, dt, "EmpName", "EmpId", 'S');
                            }
                        }
                        else
                        {
                            ShowMSG("Time Table Not Generated For This Class !","A");
                        }
                    }
                }
            }
        }
    }

    public void GetEmp0()
    {
        ddlEmp0.Items.Clear();
        ddlSubjectPaper0.Items.Clear();

        if (ddlClass0.SelectedIndex > 0)
        {
            if (ddlClassBranch0.SelectedIndex > 0)
            {
                if (ddlClassSection0.SelectedIndex > 0)
                {
                    if (ddlDay0.SelectedIndex > 0)
                    {
                        if (Convert.ToInt32(Campus.CampusInstance.ReturnTag("SELECT ISNULL((SELECT DISTINCT 1 IsExists FROM T05_TimeTableClassPeriod T05 WHERE T05.ClassID='" + Convert.ToInt32(ddlClass0.SelectedValue) + "' AND T05.SectionID='" + Convert.ToInt32(ddlClassSection0.SelectedValue) + "' AND T05.BranchID='" + Convert.ToInt32(ddlClassBranch0.SelectedValue) + "' AND T05.IsActive=1 AND T05.IsDelete=0),0) IsExists", "IsExists")) == 1)
                        {
                            dt = new DataTable();
                            dt = DAL.DALInstance.GetTeacherForOPeriod(Convert.ToInt32(ddlClass0.SelectedValue), Convert.ToInt32(ddlClassSection0.SelectedValue), Convert.ToInt32(ddlClassBranch0.SelectedValue), (ddlDay0.SelectedValue),-1);

                            if (dt != null && dt.Rows.Count > 0)
                            {
                                BLL.FillDropDown(ddlEmp0, dt, "EmpName", "EmpId", 'S');
                            }
                        }
                        else
                        {
                            ShowMSG("Time Table Not Generated For This Class !", "A");
                        }
                    }
                }
            }
        }
    }

    public void GetSubjectPaper()
    {
        ddlSubjectPaper.Items.Clear();
        if (ddlClass.SelectedIndex > 0)
        {           
            if (ddlClassBranch.SelectedIndex > 0)
            {
                if (ddlClassSection.SelectedIndex > 0)
                {
                    if (ddlDay.SelectedIndex > 0)
                    {
                        if (ddlEmp.SelectedIndex > 0)
                        {
                            dt = new DataTable();
                            dt = DAL.DALInstance.GetSubjectPaperForOPeriod(Convert.ToInt16(ddlClass.SelectedValue), Convert.ToInt16(ddlClassSection.SelectedValue), Convert.ToInt32(ddlClassBranch.SelectedValue),ddlEmp.SelectedValue);

                            if (dt != null && dt.Rows.Count > 0)
                            {
                                BLL.FillDropDown(ddlSubjectPaper, dt, "SubjectPaperName", "S02ID", 'S');
                            }
                        }
                    }
                }
            }
        }
    }

    public void GetSubjectPaper0()
    {
        ddlSubjectPaper0.Items.Clear();
        if (ddlClass0.SelectedIndex > 0)
        {
            if (ddlClassBranch0.SelectedIndex > 0)
            {
                if (ddlClassSection0.SelectedIndex > 0)
                {
                    if (ddlDay0.SelectedIndex > 0)
                    {
                        if (ddlEmp0.SelectedIndex > 0)
                        {
                            dt = new DataTable();
                            dt = DAL.DALInstance.GetSubjectPaperForOPeriod(Convert.ToInt16(ddlClass0.SelectedValue), Convert.ToInt16(ddlClassSection0.SelectedValue), Convert.ToInt32(ddlClassBranch0.SelectedValue), ddlEmp0.SelectedValue);

                            if (dt != null && dt.Rows.Count > 0)
                            {
                                BLL.FillDropDown(ddlSubjectPaper0, dt, "SubjectPaperName", "S02ID", 'S');
                            }
                        }
                    }
                }
            }
        }
        
        Panel1_ModalPopupExtender.Show();
    }

    public void Reset()
    {
        ddlDay.SelectedIndex = 0;
        //ddlClassBranch.Items.Clear();
        //ddlClassSection.Items.Clear();
        ddlEmp.Items.Clear();
        ddlSubjectPaper.Items.Clear();
    }

    public void Validation()
    {
        if (ddlClass.SelectedIndex < 1)
        {
            ShowMSG("Select Class !", "A");
            ddlClass.Focus();
        }    
        else if (ddlClassBranch.SelectedIndex < 1)
        {
            ShowMSG("Select Class Branch !", "A");
            ddlClassBranch.Focus();
        }
        else if (ddlClassSection.SelectedIndex < 1)
        {
            ShowMSG("Select Class Section !", "A");
            ddlClassSection.Focus();
        }
        else if (ddlDay.SelectedIndex < 1)
        {
            ShowMSG("Select Day !", "A");
            ddlDay.Focus();
        }
        else if (ddlEmp.SelectedIndex < 1)
        {
            ShowMSG("Select Employee !", "A");
            ddlEmp.Focus();
        }
        else if (ddlSubjectPaper.SelectedIndex < 1)
        {
            ShowMSG("Select Subject Paper !", "A");
            ddlSubjectPaper.Focus();
        }
        else
        {
            SetOPeriodSubject();
        }
    }

    public void Validation0()
    {
        if (ddlClass0.SelectedIndex < 1)
        {
            ShowMSG("Select Class !", "A");
            ddlClass0.Focus();
        }
        else if (ddlClassBranch0.SelectedIndex < 1)
        {
            ShowMSG("Select Class Branch !", "A");
            ddlClassBranch0.Focus();
        }
        else if (ddlClassSection0.SelectedIndex < 1)
        {
            ShowMSG("Select Class Section !", "A");
            ddlClassSection0.Focus();
        }
        else if (ddlDay0.SelectedIndex < 1)
        {
            ShowMSG("Select Day !", "A");
            ddlDay0.Focus();
        }
        else if (ddlEmp0.SelectedIndex < 1)
        {
            ShowMSG("Select Employee !", "A");
            ddlEmp0.Focus();
        }
        else if (ddlSubjectPaper0.SelectedIndex < 1)
        {
            ShowMSG("Select Subject Paper !", "A");
            ddlSubjectPaper0.Focus();
        }
        else
        {
            SetOPeriodSubject0();
        }
    }

    public void SetOPeriodSubject()
    {
        BAL.clsOPeriodSubject obj=new BAL.clsOPeriodSubject();
        obj.ClassID = Convert.ToInt32(ddlClass.SelectedValue);
        obj.SectionID= Convert.ToInt32(ddlClassSection.SelectedValue);
        obj.BranchID = Convert.ToInt32(ddlClassBranch.SelectedValue);
        obj.S02ID = Convert.ToInt32(ddlSubjectPaper.SelectedValue);
        obj.SelectedDay = ddlDay.SelectedValue;
        obj.SQL = SQL;
        obj.EmpID=ddlEmp.SelectedValue;
  
        MSG = DAL.DALInstance.SetOPeriodSubject(obj);
        if (MSG == "")
        {
            //btnInsert.Visible = true;
            //btnUpdate.Visible = false;
            GetOPeriodSubject();
            MSG = SQL;
            Reset();
            ShowMSG(MSG, "S");
        }
        else
        {
            ShowMSG(MSG, "A");
        }
    }

    public void SetOPeriodSubject0()
    {
        BAL.clsOPeriodSubject obj = new BAL.clsOPeriodSubject();
        obj.ClassID = Convert.ToInt32(ddlClass0.SelectedValue);
        obj.SectionID = Convert.ToInt32(ddlClassSection0.SelectedValue);
        obj.BranchID = Convert.ToInt32(ddlClassBranch0.SelectedValue);
        obj.S02ID = Convert.ToInt32(ddlSubjectPaper0.SelectedValue);
        obj.SelectedDay = ddlDay0.SelectedValue;
        obj.SQL = SQL;
        obj.EmpID = ddlEmp0.SelectedValue;

        MSG = DAL.DALInstance.SetOPeriodSubject(obj);
        if (MSG == "")
        {
            GetOPeriodSubject();
            MSG = SQL;
            Reset();
            Panel1_ModalPopupExtender.Hide();
            ShowMSG(MSG, "S");
        }
        else
        {
            ShowMSG(MSG, "A");
        }
    }

    public void GetOPeriodSubject()
    {
        gvOPeriodSubject.DataSource = null;
        gvOPeriodSubject.DataBind();

        if (ddlClass.SelectedIndex > 0)
        {
            if (ddlClassBranch.SelectedIndex > 0)
            {
                if (ddlClassSection.SelectedIndex > 0)
                {
                    BAL.clsOPeriodSubject obj = new BAL.clsOPeriodSubject();
                    obj.ClassID = Convert.ToInt32(ddlClass.SelectedValue);
                    obj.SectionID = Convert.ToInt32(ddlClassSection.SelectedValue);
                    obj.BranchID = Convert.ToInt32(ddlClassBranch.SelectedValue);
                    obj.SelectedDay = "";

                    dt = DAL.DALInstance.GetOPeriodSubject(obj);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        dt = new BLL().GetSerialNo(ref dt, "SrNo");
                        gvOPeriodSubject.DataSource = dt;
                        gvOPeriodSubject.DataBind();
                    }
                    else
                    {
                        gvOPeriodSubject.DataSource = null;
                        gvOPeriodSubject.DataBind();
                        ShowMSG("N", "A");
                    }
                }
            }
        }
    }

    protected void btnInsert_Click(object sender, EventArgs e)
    {
        SQL = "I";
        Validation();
    }

    protected void lbtnEdit_Click(object sender, EventArgs e)
    {
        GridViewRow Row = (GridViewRow)(sender as LinkButton).Parent.Parent;

        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label36");
        string ss = lblId.Text;

        //LinkButton btn = (LinkButton)sender;
        T07ID = Convert.ToInt16(ss);
        int i = Row.RowIndex;

        GetClass(ddlClass0);
        ddlClass0.Items.FindByText(gvOPeriodSubject.Rows[i].Cells[1].Text.Trim()).Selected = true;
        GetClassSection(ddlClassSection0, ddlClass0);
        ddlClassSection0.Items.FindByText(gvOPeriodSubject.Rows[i].Cells[3].Text.Trim()).Selected = true;
        GetClassBranch(ddlClassBranch0, ddlClass0);
        ddlClassBranch0.Items.FindByText(gvOPeriodSubject.Rows[i].Cells[2].Text.Trim()).Selected = true;
        
        //ddlDay0.Items.FindByValue(gvOPeriodSubject.Rows[i].Cells[4].Text.Trim()).Selected = true;
        ddlDay0.SelectedValue = gvOPeriodSubject.Rows[i].Cells[4].Text.Trim();

        GetEmp0();
        //ddlEmp0.SelectedValue = HttpUtility.HtmlDecode(gvOPeriodSubject.Rows[i].Cells[5].Text.Trim());

        //GetSubjectPaper0();
        //ddlSubjectPaper0.SelectedValue = HttpUtility.HtmlDecode(gvOPeriodSubject.Rows[i].Cells[6].Text.Trim());
        
        Panel1_ModalPopupExtender.Show();
    } 

    protected void lbtnDelete_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label37");
        string ss = lblId.Text;
        T07ID = Convert.ToInt16(ss);
        mpeDelete.Show();
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        Reset();
    }

    protected void btnYes_Click(object sender, EventArgs e)
    {
        DeleteRecord();
        GetOPeriodSubject();
    }

    public void DeleteRecord()
    {
        SQL = "D";
        BAL.clsOPeriodSubject obj = new BAL.clsOPeriodSubject();
        obj.SQL = SQL;
        obj.T07ID = T07ID;

        MSG = DAL.DALInstance.SetOPeriodSubject(obj);
        if (MSG == "")
        {
            GetOPeriodSubject();
            MSG = SQL;
            ShowMSG(MSG, "S");
        }
        else
        {
            ShowMSG(MSG, "A");
        }
    }

    protected void btnNo_Click(object sender, EventArgs e)
    {

    }

    protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetClassSection(ddlClassSection,ddlClass);
        GetClassBranch(ddlClassBranch,ddlClass);

        GetOPeriodSubject();
        GetSubjectPaper();
        GetEmp();
    }

    protected void ddlClassBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetOPeriodSubject();
        GetSubjectPaper();
        GetEmp();
    }

    protected void ddlClassSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetOPeriodSubject();
        GetSubjectPaper();
        GetEmp();
    }

    protected void ddlClass0_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetClassSection(ddlClassSection0, ddlClass0);
        GetClassBranch(ddlClassBranch0, ddlClass0);

        GetSubjectPaper0();
        Panel1_ModalPopupExtender.Show();
    }

    protected void ddlClassBranch0_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetSubjectPaper0();
        Panel1_ModalPopupExtender.Show();
    }

    protected void ddlClassSection0_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetEmp0();
        GetSubjectPaper0();
        Panel1_ModalPopupExtender.Show();
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {

    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        SQL = "U";
        Panel1_ModalPopupExtender.Show();
        Validation0();
    }

    protected void ddlDay_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetEmp();
    }

    protected void ddlSubjectPaper_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void ddlEmp_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetSubjectPaper();
    }

    protected void ddlDay0_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetEmp0();
        Panel1_ModalPopupExtender.Show();
    }

    protected void ddlEmp0_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetSubjectPaper0();
        Panel1_ModalPopupExtender.Show();
    }

    private void ShowMSG(string MSG, string Type)
    {
        Campus camp1 = new Campus(); camp1.msgbox(this.Page, dvMSG, BLL.BLLInstance.FetchMSG(MSG), Type);
    }

    protected void lbtnEdit_Click1(object sender, EventArgs e)
    {

    }
}