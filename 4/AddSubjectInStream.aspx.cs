using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;

public partial class admin_AddSubjectInStream : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((string)Session["LoginName"] == "" || (string)Session["BranchCode"] == "" || (string)Session["SessionName"] == "" || Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        Campus camp = new Campus(); camp.LoadLoader(loader);
        //loadNotification(lnkSubmit, "Submitted successfully.", "S");
        if (!IsPostBack)
        {
            BLL.BLLInstance.loadClass(drpClass, Session["SessionName"].ToString());
            BLL.BLLInstance.loadBranch(drpBranch, Session["SessionName"].ToString(),drpClass.SelectedValue.ToString());
            BLL.BLLInstance.loadSection(drpSection, Session["SessionName"].ToString(), drpClass.SelectedValue.ToString());
            loadGrid("-1");
        }
    }

    public void loadSubjectGroup(DropDownList drp)
    {
        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
        param.Add(new SqlParameter("@Classid", drpClass.SelectedValue.ToString()));
        param.Add(new SqlParameter("@Branchid", drpBranch.SelectedValue.ToString()));
        param.Add(new SqlParameter("@SectionName", drpSection.SelectedItem.Text.Trim()));

        string sql = "Select SubjectGroup,Id from SubjectGroupMaster where SessionName=@SessionName and Classid=@Classid and Branchid=@Branchid and SectionName=@SectionName and BranchCode=@BranchCode and IsforOnlyExam=1";

        drp.DataSource = DLL.objDll.SelectRecord_usingExecuteDataset(sql, param);
        drp.DataTextField = "SubjectGroup";
        drp.DataValueField = "Id";
        drp.DataBind();

    }

    public void loadStream(DropDownList drp)
    {
        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@Classid", drpClass.SelectedValue.ToString()));
        param.Add(new SqlParameter("@Branchid", drpBranch.SelectedValue.ToString()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
        string sql = "Select Stream,Id from StreamMaster where SessionName=@SessionName and Classid=@Classid and Branchid=@Branchid and BranchCode=@BranchCode";

        drp.DataSource = DLL.objDll.SelectRecord_usingExecuteDataset(sql, param);
        drp.DataTextField = "Stream";
        drp.DataValueField = "Id";
        drp.DataBind();

    }

    public void loadGrid(string id)
    {
        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@QueryFor", "S"));
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@branchcode", Session["Branchcode"].ToString()));
        param.Add(new SqlParameter("@ClassId", drpClass.SelectedValue.ToString()));
        param.Add(new SqlParameter("@BranchId", drpBranch.SelectedValue.ToString()));
        param.Add(new SqlParameter("@Streamid", drpStream.SelectedValue.ToString()));
        param.Add(new SqlParameter("@Sectionid", drpSection.SelectedValue.ToString()));
        param.Add(new SqlParameter("@Id", id));

        DataSet ds = new DataSet();
        ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("SubjectForStreamProc", param);

        if (id == "-1")
        {
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        else
        {
            Panel1_ModalPopupExtender.Show();
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    BLL.BLLInstance.loadClass(drpClassPanel, Session["SessionName"].ToString());
                    if (drpClassPanel.Items.Count > 0)
                    {
                        drpClassPanel.SelectedValue = ds.Tables[0].Rows[0]["ClassId"].ToString();
                    }
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        BLL.BLLInstance.loadBranch(drpBranchPanel, Session["SessionName"].ToString(), ds.Tables[0].Rows[0]["ClassId"].ToString());
                        if (drpBranchPanel.Items.Count > 0)
                        {
                            drpBranchPanel.SelectedValue = ds.Tables[0].Rows[0]["BranchId"].ToString();
                        }
                        BLL.BLLInstance.loadSection(drpSectionPanel, Session["SessionName"].ToString(), ds.Tables[0].Rows[0]["ClassId"].ToString());
                        if (drpSectionPanel.Items.Count > 0)
                        {
                            drpSectionPanel.SelectedValue = ds.Tables[0].Rows[0]["SectionId"].ToString();
                        }
                        loadStream(drpStreamPanel);
                        if (drpStreamPanel.Items.Count > 0)
                        {
                            drpStreamPanel.SelectedValue = ds.Tables[0].Rows[0]["Streamid"].ToString();
                        }
                        loadSubjectGroup(drpSubjectGroupPanel);
                        if (drpSubjectGroupPanel.Items.Count > 0)
                        {
                            drpSubjectGroupPanel.SelectedValue = ds.Tables[0].Rows[0]["Subjectgroupid"].ToString();
                        }
                        txtRemarkPanel.Text = ds.Tables[0].Rows[0]["Remark"].ToString();
                    }
                }
            }
        }
    }

    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        if (drpStream.SelectedValue == string.Empty)
        {
            //BAL.objBal.MessageBoxforUpdatePanel("Please, Enter Stream!", lnkSubmit);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please, Enter Stream!", "A");       

        }
        else
        {
            Save();
        }
    }

    private void Save()
    {
        string flag = "";

        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@QueryFor", "I"));
        param.Add(new SqlParameter("@ClassId", drpClass.SelectedValue.ToString()));
        param.Add(new SqlParameter("@BranchId", drpBranch.SelectedValue.ToString()));
        param.Add(new SqlParameter("@SectionId", drpSection.SelectedValue.ToString()));
        param.Add(new SqlParameter("@Streamid", drpStream.SelectedValue.ToString()));
        param.Add(new SqlParameter("@SubjectGroupid", drpSubjectGroup.SelectedValue.ToString()));
        param.Add(new SqlParameter("@Remark", txtRemark.Text.Trim().ToString()));
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@LoginName", Session["LoginName"].ToString()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));

        SqlParameter para = new SqlParameter("@Msg", "");
        para.Direction = ParameterDirection.Output;
        para.Size = 0x100;
        param.Add(para);

        flag = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("SubjectForStreamProc", param);

        if (flag == "Du")
        {
            //BAL.objBal.MessageBoxforUpdatePanel("Duplicate Entry!", lnkSubmit);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Duplicate Entry!", "A");       

        }
        else if (flag == "S")
        {
            //BAL.objBal.MessageBoxforUpdatePanel("Submitted successfully.", lnkSubmit);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");       

        }
        else
        {
            //BAL.objBal.MessageBoxforUpdatePanel("Sorry, Record not Submitted!", lnkSubmit);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, Record not Submitted!", "A");       

        }
        loadGrid("-1");
    }

    protected void drpClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        BLL.BLLInstance.loadBranch(drpBranch, Session["SessionName"].ToString(), drpClass.SelectedValue.ToString());
        BLL.BLLInstance.loadSection(drpSection, Session["SessionName"].ToString(), drpClass.SelectedValue.ToString());
        loadGrid("-1");
    }

    protected void drpBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadSubjectGroup(drpSubjectGroup);
        loadStream(drpStream);
        loadGrid("-1");
    }

    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        LinkButton lnk=(LinkButton)sender;
        Label lblIds = (Label)lnk.NamingContainer.FindControl("lblEditid");
        lblId.Text = lblIds.Text;
        loadGrid(lblIds.Text);
    }

    protected void lnkUpdate_Click(object sender, EventArgs e)
    {
        Update();
    }

    private void Update()
    {
        string flag = "";

        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@QueryFor", "U"));
        param.Add(new SqlParameter("@ClassId", drpClassPanel.SelectedValue.ToString()));
        param.Add(new SqlParameter("@BranchId", drpBranchPanel.SelectedValue.ToString()));
        param.Add(new SqlParameter("@SectionId", drpSectionPanel.SelectedValue.ToString()));
        param.Add(new SqlParameter("@Streamid", drpStreamPanel.SelectedValue.ToString()));
        param.Add(new SqlParameter("@SubjectGroupid", drpSubjectGroupPanel.SelectedValue.ToString()));
        param.Add(new SqlParameter("@Remark", txtRemarkPanel.Text.Trim().ToString()));
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@branchcode", Session["Branchcode"].ToString()));
        param.Add(new SqlParameter("@Id", lblId.Text.Trim()));

        SqlParameter para = new SqlParameter("@Msg", "");
        para.Direction = ParameterDirection.Output;
        para.Size = 0x100;
        param.Add(para);

        flag = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("SubjectForStreamProc", param);

        if (flag == "Du")
        {
            //BAL.objBal.MessageBoxforUpdatePanel("Duplicate Entry!", lnkSubmit);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Duplicate Entry!", "A");       

        }
        else if (flag == "U")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "S");       

            //BAL.objBal.MessageBoxforUpdatePanel("Updated successfully.", lnkSubmit);
        }
        else
        {
            //BAL.objBal.MessageBoxforUpdatePanel("Sorry, Record not Submitted!", lnkSubmit);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, Record not Submitted!", "A");       

        }
        loadGrid("-1");
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        Button8.Focus();
        LinkButton lnk = (LinkButton)sender;
        Label lblIds = (Label)lnk.NamingContainer.FindControl("lblDeleteId");
        lblvalue.Text = lblIds.Text;

        Panel2_ModalPopupExtender.Show();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        Delete();
    }

    private void Delete()
    {
        string flag = "";

        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@QueryFor", "D"));
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@branchcode", Session["Branchcode"].ToString()));
        param.Add(new SqlParameter("@Id", lblvalue.Text.Trim()));

        SqlParameter para = new SqlParameter("@Msg", "");
        para.Direction = ParameterDirection.Output;
        para.Size = 0x100;
        param.Add(para);

        flag = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("SubjectForStreamProc", param);

        if (flag == "De")
        {
            //BAL.objBal.MessageBoxforUpdatePanel("Deleted successfully.", lnkSubmit);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Deleted successfully.", "S");       

        }
        else
        {
            //BAL.objBal.MessageBoxforUpdatePanel("Sorry, Record not Deleted!", lnkSubmit);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, Record not Deleted!", "A");       

        }
        loadGrid("-1");
    }

    protected void drpClassPanel_SelectedIndexChanged(object sender, EventArgs e)
    {
        BLL.BLLInstance.loadBranch(drpBranchPanel, Session["SessionName"].ToString(), drpClassPanel.SelectedValue.ToString());
        Panel1_ModalPopupExtender.Show();
    }
    protected void drpSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadSubjectGroup(drpSubjectGroup);
        loadGrid("-1");
    }
    protected void drpStream_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadGrid("-1");
    }
}