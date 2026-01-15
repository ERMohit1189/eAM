using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class ActivityMaster : Page
{
    string sql = "", _sql = "", Sql = "";
    Campus _oo = new Campus();
    private SqlConnection _con;
    DataTable dt;
    protected void Page_Load(object sender, EventArgs e)
    {
        _con = new SqlConnection();
        _con = _oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!Page.IsPostBack)
        {
            if (Session["ClassGroup"].ToString()=="G1")
            {
                notr.Visible = false;
                upMain.Visible = true;
                LoadClass();
                ddlSubject.Items.Insert(0, new ListItem("<--Select-->", ""));
                ddlBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
                ddlMedium.Items.Insert(0, new ListItem("<--Select-->", ""));
                ddlPaper.Items.Insert(0, new ListItem("<--Select-->", ""));
            }
            else
            {
                notr.Visible = false;
                upMain.Visible = true;
            }
        }
    }
    public void LoadClass()
    {
        sql = "Select Distinct ClassName,cm.Id as Id,CIDOrder from ClassMaster cm ";
        sql = sql + " inner join dt_ClassGroupMaster t1 on t1.ClassId=cm.Id";
        sql = sql + " where cm.SessionName='" + Session["SessionName"] + "' and cm.BranchCode=" + Session["BranchCode"] + " and t1.SessionName='" + Session["SessionName"] + "' and t1.BranchCode=" + Session["BranchCode"] + " and GroupId='G1' Order by CIDOrder";
        _oo.FillDropDown_withValue(sql, ddlClass, "ClassName", "Id");
        ddlClass.Items.Insert(0, new ListItem("<--Select-->", ""));
        
    }
    protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        sql = "Select BranchName,Id from BranchMaster where ClassId='" + ddlClass.SelectedValue.ToString() + "'";
        sql = sql + " and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "'";
        _oo.FillDropDown_withValue(sql, ddlBranch, "BranchName", "Id");
        ddlBranch.Items.Insert(0, new ListItem("<--Select-->", ""));

        sql = "Select Medium,Id from MediumMaster where BranchCode=" + Session["BranchCode"].ToString() + " and SessionName='" + Session["SessionName"].ToString() + "'";
        _oo.FillDropDown_withValue(sql, ddlMedium, "Medium", "Id");
        ddlMedium.Items.Insert(0, new ListItem("<--Select-->", ""));
        Grid.DataSource = null;
        Grid.DataBind();
        LoadData();
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSubject.Items.Clear();
        sql = "select * from TTSubjectMaster where BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "' and classid=" + ddlClass.SelectedValue + " and Branchid=" + ddlBranch.SelectedValue + "";
        if (ddlMedium.SelectedIndex > 0)
        {
            sql = sql + " and medium = '" + ddlMedium.SelectedItem.Text + "'";
        }
        _oo.FillDropDown_withValue(sql, ddlSubject, "SubjectName", "Id");
        ddlSubject.Items.Insert(0, new ListItem("<--Select-->", ""));
        LoadData();
    }
    protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlPaper.Items.Clear();
        sql = "select * from TTPaperMaster where BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "' and classid=" + ddlClass.SelectedValue + " and Branchid=" + ddlBranch.SelectedValue + "  and Subjectid=" + ddlSubject.SelectedValue + "";
        if (ddlMedium.SelectedIndex > 0)
        {
            sql = sql + " and medium = '" + ddlMedium.SelectedItem.Text + "'";
        }
        _oo.FillDropDown_withValue(sql, ddlPaper, "PaperName", "Id");
        ddlPaper.Items.Insert(0, new ListItem("<--Select-->", ""));
        LoadData();
    }
    protected void ddlPaper_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadData();
    }
    public void LoadData()
    {
        Grid.DataSource = null;
        Grid.DataBind();
        sql = "select Act.*, cm.ClassName, sm.SubjectName, bm.BranchName, pm.PaperName from TTActivityMaster Act ";
        sql = sql + " inner join ClassMaster cm on cm.id=Act.Classid and Act.BranchCode=cm.BranchCode  and cm.SessionName=Act.SessionName ";
        sql = sql + " inner join BranchMaster bm on bm.id=Act.branchid and bm.BranchCode=Act.BranchCode and bm.SessionName=Act.SessionName ";
        sql = sql + " inner join MediumMaster mm on mm.medium=Act.medium and mm.BranchCode=Act.BranchCode and mm.SessionName=Act.SessionName ";
        sql = sql + " inner join TTSubjectMaster sm on sm.id=Act.SubjectId and cm.id=sm.Classid and sm.BranchCode=Act.BranchCode  and sm.SessionName=Act.SessionName";
        sql = sql + " inner join TTPaperMaster pm on pm.id=Act.PaperId and sm.id=Pm.SubjectId and cm.id=sm.Classid and sm.BranchCode=Act.BranchCode  and sm.SessionName=Act.SessionName";
        sql = sql + " where Act.BranchCode=" + Session["BranchCode"] + " and Act.SessionName='" + Session["SessionName"] + "' and Act.Classid=" + ddlClass.SelectedValue + " and Act.BranchId='" + ddlBranch.SelectedValue + "'";
        if (ddlMedium.SelectedIndex > 0)
        {
            sql = sql + " and mm.medium = '" + ddlMedium.SelectedItem.Text + "'";
        }
        if (ddlSubject.SelectedIndex!=0)
        {
            sql = sql + " and Act.SubjectId=" + ddlSubject.SelectedValue + " ";
        }
        if (ddlPaper.SelectedIndex != 0)
        {
            sql = sql + " and Act.PaperId=" + ddlPaper.SelectedValue + " ";
        }
        sql = sql + "  order by cm.id asc";
        var dt = _oo.Fetchdata(sql);
        Grid.DataSource = dt;
        Grid.DataBind();
        for (int i = 0; i < Grid.Rows.Count; i++)
        {
            Label lblclassId = (Label)Grid.Rows[i].FindControl("lblclassId");
            Label lblBeanchId = (Label)Grid.Rows[i].FindControl("lblBeanchId");
            Label SubjectID = (Label)Grid.Rows[i].FindControl("SubjectID");
            Label PaperID = (Label)Grid.Rows[i].FindControl("PaperID");
            Label Activityid = (Label)Grid.Rows[i].FindControl("Label38");
            LinkButton lbtnEdit = (LinkButton)Grid.Rows[i].FindControl("lbtnEdit");
            LinkButton lbtnDelete = (LinkButton)Grid.Rows[i].FindControl("lbtnDelete");
            _sql = " SELECT Id FROM CCENurtoPrep_1718 WHERE Activityid=" + Activityid.Text + " and Branchid=" + ddlBranch.SelectedValue.Trim() + " and SubjectId=" + ddlSubject.SelectedValue.Trim() + " and Medium=" + ddlMedium.SelectedValue.Trim() + " and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and PaperId=" + ddlPaper.SelectedValue + "";
            if (_oo.Duplicate(_sql))
            {
                lbtnEdit.Text = "<i class='fa fa-lock'></i>";
                lbtnDelete.Text = "<i class='fa fa-lock'></i>";
                lbtnEdit.Enabled = false;
                lbtnDelete.Enabled = false;
            }
        }
    }

    protected void btnInserts_Click(object sender, EventArgs e)
    {
        _sql = "SELECT id FROM TTActivityMaster WHERE ActivityName='" + txtActivity.Text.Trim() + "' and classid=" + ddlClass.SelectedValue.Trim() + " and Branchid=" + ddlBranch.SelectedValue.Trim() + " and SubjectId=" + ddlSubject.SelectedValue.Trim() + " and Medium='" + ddlMedium.SelectedItem.Text.Trim() + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and PaperId=" + ddlPaper.SelectedValue + "";
        if (_oo.Duplicate(_sql))
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Duplicate Activity Name!", "A");
        }
        else
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "TTActivityMasterProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = _con;
                cmd.Parameters.AddWithValue("@Classid", ddlClass.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@BranchId", ddlBranch.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@Medium", ddlMedium.SelectedItem.Text.Trim());
                cmd.Parameters.AddWithValue("@SubjectId", ddlSubject.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@PaperId", ddlPaper.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@ActivityName", txtActivity.Text.Trim());
                cmd.Parameters.AddWithValue("@ActivityCode", txtActivityCode.Text.Trim());
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@Action", "insert");
                try
                {
                    _con.Open();
                    cmd.ExecuteNonQuery();
                    _con.Close();
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
                    LoadData();
                    Reset();
                }
                catch (Exception ex)
                {
                }
            }
        }
    }
    private void Reset()
    {
        txtActivity.Text = string.Empty;
        txtActivityCode.Text = string.Empty;
        txtEditActivity.Text = string.Empty;
        lblSubjectId.Text = string.Empty;
        lblPaper.Text = string.Empty;
        lblClassId.Text = string.Empty;
        lblValueId.Text = string.Empty;
    }


    protected void lbtnEdit_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label38");
        lblValueId.Text = lblId.Text;

        Label lblclassId = (Label)chk.NamingContainer.FindControl("lblclassId");
        Label lblBeanchId = (Label)chk.NamingContainer.FindControl("lblBeanchId");
        Label SubjectID = (Label)chk.NamingContainer.FindControl("SubjectID");
        Label PaperID = (Label)chk.NamingContainer.FindControl("PaperID");
        Label LabelActivity = (Label)chk.NamingContainer.FindControl("LabelActivity");
        Label lblActivityCode = (Label)chk.NamingContainer.FindControl("lblActivityCode");
        Label LabelMedium = (Label)chk.NamingContainer.FindControl("LabelMedium");

        lblClassId.Text = lblclassId.Text;
        lblPaper.Text = PaperID.Text;
        txtEditActivity.Text = LabelActivity.Text;
        txtEditActivityCode.Text = lblActivityCode.Text;
        lblSubjectId.Text = SubjectID.Text;
        lblBranchId.Text = lblBeanchId.Text;
        lblMedium.Text = LabelMedium.Text;
        Panel1_ModalPopupExtender.Show();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        _sql = "SELECT id FROM TTActivityMaster WHERE ActivityName='" + txtEditActivity.Text.Trim() + "' and classid=" + lblClassId.Text.Trim() + " and Branchid=" + lblBranchId.Text.Trim() + " and SubjectId="+ lblSubjectId.Text.Trim() + " and PaperId=" + lblPaper.Text.Trim() + " and Medium='" + lblMedium.Text.Trim() + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and id<>"+ lblValueId.Text.Trim() + "";
        if (_oo.Duplicate(_sql))
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Duplicate Activity Name!", "A");
        }
        else
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "TTActivityMasterProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = _con;
                cmd.Parameters.AddWithValue("@Id", lblValueId.Text.Trim());
                cmd.Parameters.AddWithValue("@Classid", lblClassId.Text.Trim());
                cmd.Parameters.AddWithValue("@BranchId", lblBranchId.Text.Trim());
                cmd.Parameters.AddWithValue("@SubjectId", lblSubjectId.Text.Trim());
                cmd.Parameters.AddWithValue("@PaperId", lblPaper.Text.Trim());
                cmd.Parameters.AddWithValue("@Medium", lblMedium.Text.Trim());
                cmd.Parameters.AddWithValue("@ActivityName", txtEditActivity.Text.Trim());
                cmd.Parameters.AddWithValue("@ActivityCode", txtEditActivityCode.Text.Trim());
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@Action", "Update");
                try
                {
                    _con.Open();
                    cmd.ExecuteNonQuery();
                    _con.Close();
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Updatted successfully.", "S");
                    LoadData();
                }
                catch (Exception ex)
                {
                }
            }
        }
    }
    protected void lbtnDelete_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label37");
        lblValue.Text = lblId.Text;
        mpeDelete.Show();
    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
        _sql = "Delete from TTActivityMaster where Id=" + lblValue.Text + " and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "'";

        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = _sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = _con;
            try
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Deleted successfully.", "S");
                LoadData();
                Reset();
            }
            catch (Exception)
            {
            }
        }
    }

    protected void btnNo_Click(object sender, EventArgs e)
    {

    }
    protected void ddlMedium_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSubject.Items.Clear();
        sql = "select * from TTSubjectMaster where BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "' and classid=" + ddlClass.SelectedValue + " and Branchid=" + ddlBranch.SelectedValue + "";
        if (ddlMedium.SelectedIndex > 0)
        {
            sql = sql + " and medium = '" + ddlMedium.SelectedItem.Text + "'";
        }
        _oo.FillDropDown_withValue(sql, ddlSubject, "SubjectName", "Id");
        ddlSubject.Items.Insert(0, new ListItem("<--Select-->", ""));
        LoadData();
    }
}