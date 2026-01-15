using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI;
public partial class PaperMaster : Page
{
    private SqlConnection _con;
    private readonly Campus _oo;
    private string _sql, _sql1 = String.Empty;
    public PaperMaster()
    {
        _con = new SqlConnection();
        _oo = new Campus();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Session["LoginName"] as string))
        {
            Response.Redirect("~/default.aspx");
        }
        if (Session["Logintype"].ToString().ToLower() != "admin")
        {
            Response.Redirect("~/default.aspx");
        }
        _con = _oo.dbGet_connection();
        _oo.LoadLoader(loader); 

        if (!IsPostBack)
        {
            loadClass();
            txtPaperName.Focus();
            ddlSubject.Items.Insert(0, new ListItem("<--Select-->", ""));
            LoadData();
        }
    }
    protected void SubjectLoad()
    {
        _sql = "select Id, Subject from OT_SubjectMaster where classId="+ ddlClass.SelectedValue + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        _oo.FillDropDown_withValue(_sql, ddlSubject, "Subject", "Id");
        _oo.FillDropDown_withValue(_sql, ddlSubjectPanel, "Subject", "Id");
        ddlSubject.Items.Insert(0, new ListItem("<--Select-->", ""));

    }
    private void loadClass()
    {
        _sql = "select Id, ClassName from ClassMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " order by CIDOrder";
        _oo.FillDropDown_withValue(_sql, ddlClass, "ClassName", "Id");
        _oo.FillDropDown_withValue(_sql, ddlClassPanel, "ClassName", "Id");
        ddlClass.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        SubjectLoad();
    }
    private void LoadData()
    {
        _sql = "select PM.id, cm.ClassName, Paper, SM.Subject, PM.Remark, PM.SessionName, PM.Loginname, PM.Recordeddate from OT_PaperMaster PM ";
        _sql = _sql + " inner join OT_SubjectMaster SM on SM.id = PM.SubjectId  and  sm.SessionName='" + Session["SessionName"] + "' and sm.BranchCode=" + Session["BranchCode"] + "";
        _sql = _sql + " inner join ClassMaster cm on cm.id=SM.classId and cm.SessionName='" + Session["SessionName"] + "' and cm.BranchCode=" + Session["BranchCode"] + " ";
        _sql = _sql + " where PM.SubjectId=case when '" + ddlSubject.SelectedValue+ "'='' then PM.SubjectId else '" + ddlSubject.SelectedValue + "' end and  pm.SessionName='" + Session["SessionName"] + "' and pm.BranchCode=" + Session["BranchCode"] + " order by id asc";
        Grd.DataSource = _oo.GridFill(_sql);
        Grd.DataBind();
        for (int i = 0; i < Grd.Rows.Count; i++)
        {
            Label Label37 = (Label)Grd.Rows[i].FindControl("Label37");
            LinkButton Edit = (LinkButton)Grd.Rows[i].FindControl("LinkButton2");
            LinkButton LinkButton3 = (LinkButton)Grd.Rows[i].FindControl("LinkButton3");
            _sql = "select count(*) cnt from OT_ExamMaster where PaperId=" + Label37.Text + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            if (_oo.ReturnTag(_sql, "cnt") != "0")
            {
                Edit.Enabled = false;
                Edit.Text = "<i class='fa fa-lock'></i>";
                LinkButton3.Enabled = false;
                LinkButton3.Text = "<i class='fa fa-lock'></i>";
            }
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        _sql = "select Paper from OT_PaperMaster where Paper='" + txtPaperName.Text + "' and classid=" + ddlClass.SelectedValue + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";

        if (_oo.Duplicate(_sql))
        {
            _oo.msgbox(Page, msgbox, "Duplicate Paper Name!", "S");
        }
        else
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "OT_PaperMasterProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = _con;
                cmd.Parameters.AddWithValue("@classId", ddlClass.SelectedValue);
                cmd.Parameters.AddWithValue("@SubjectId", ddlSubject.SelectedValue);
                cmd.Parameters.AddWithValue("@Paper", txtPaperName.Text);
                cmd.Parameters.AddWithValue("@Remark", txtRemark.Text);
                cmd.Parameters.AddWithValue("@Loginname", Session["LoginName"].ToString());
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@Action", "insert");
                try
                {
                    _con.Open();
                    cmd.ExecuteNonQuery();
                    _con.Close();
                    _oo.msgbox(Page, msgbox, "Submitted successfully.", "S");
                    _oo.ClearControls(Page);
                    LoadData();
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }

    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        Button8.Focus();
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label37");
        var ss = lblId.Text;
        lblvalue.Text = ss;
        Panel2_ModalPopupExtender.Show();
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label36");
        string ss = lblId.Text;
        lblID.Text = ss;

        _sql = "select classId, SubjectId, Paper, Remark from OT_PaperMaster where  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and id=" + ss;
        var classId = _oo.ReturnTag(_sql, "classId");
        var SubjectId = _oo.ReturnTag(_sql, "SubjectId");
        var Paper = _oo.ReturnTag(_sql, "Paper");
        var Remark = _oo.ReturnTag(_sql, "Remark");

        _sql = "select Id, Subject from OT_SubjectMaster where classId=" + classId + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        _oo.FillDropDown_withValue(_sql, ddlSubjectPanel, "Subject", "Id");

        ddlClassPanel.SelectedValue = classId;
        ddlSubjectPanel.SelectedValue = SubjectId;
        txtPaperNamePanel.Text = Paper;
        txtRemarkPanel.Text = Remark;
        Panel1_ModalPopupExtender.Show();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "OT_PaperMasterProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", lblID.Text);
            cmd.Parameters.AddWithValue("@classId", ddlClassPanel.SelectedValue);
            cmd.Parameters.AddWithValue("@SubjectId", ddlSubjectPanel.SelectedValue);
            cmd.Parameters.AddWithValue("@Paper", txtPaperNamePanel.Text);
            cmd.Parameters.AddWithValue("@Remark", txtRemarkPanel.Text);
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@Loginname", Session["LoginName"].ToString());
            cmd.Parameters.AddWithValue("@Action", "update");

            cmd.Connection = _con;
            try
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
                _oo.msgbox(Page, msgbox, "Updated successfully.", "S");
                LoadData();
                Panel1_ModalPopupExtender.Hide();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
    protected void Button4_Click(object sender, EventArgs e)
    {
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        _sql = "Delete from OT_PaperMaster where Id=" + lblvalue.Text+ "";

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
                _oo.msgbox(Page, msgbox, "Deleted successfully.", "S");
                LoadData();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    protected void Button8_Click(object sender, EventArgs e)
    {
    }
    public override void Dispose()
    {
        _con.Dispose();
        _oo.Dispose();
    }

    protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadData();
    }
}