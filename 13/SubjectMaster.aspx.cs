using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI;
public partial class SubjectMaster : Page
{
    private SqlConnection _con;
    private readonly Campus _oo;
    private string _sql, _sql1 = String.Empty;
    public SubjectMaster()
    {
        _con = new SqlConnection();
        _oo = new Campus();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadData();
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
            txtSubjectName.Focus();
            LoadData();
        }
    }
    private void loadClass()
    {
        _sql = "select Id, ClassName from ClassMaster where SessionName='"+Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " order by CIDOrder";
        _oo.FillDropDown_withValue(_sql, ddlClass, "ClassName", "Id");
        _oo.FillDropDown_withValue(_sql, ddlClassPanel, "ClassName", "Id");
        ddlClass.Items.Insert(0, new ListItem("All", ""));
    }
    private void LoadData()
    {
        _sql = "select id, (select ClassName from ClassMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and id=cc.classId) as ClassName, Subject, Remark, SessionName, Loginname, Recordeddate from OT_SubjectMaster cc where  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " order by id asc";
        Grd.DataSource = _oo.GridFill(_sql);
        Grd.DataBind();
        
        for (int i = 0; i < Grd.Rows.Count; i++)
        {
            Label Label37 = (Label)Grd.Rows[i].FindControl("Label37");
            LinkButton Edit = (LinkButton)Grd.Rows[i].FindControl("LinkButton2");
            LinkButton LinkButton3 = (LinkButton)Grd.Rows[i].FindControl("LinkButton3");
            _sql = "select count(*) cnt from OT_PaperMaster where SubjectId=" + Label37.Text + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
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
        if (ddlClass.SelectedIndex == 0)
        {
            int sts = 0;
            for (int i = 1; i < ddlClass.Items.Count; i++)
            {
                _sql = "select Subject from OT_SubjectMaster where Subject='" + txtSubjectName.Text + "' and classid=" + ddlClass.Items[i].Value + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                if (!_oo.Duplicate(_sql))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "OT_SubjectMasterProc";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = _con;
                        cmd.Parameters.AddWithValue("@classId", ddlClass.Items[i].Value);
                        cmd.Parameters.AddWithValue("@Subject", txtSubjectName.Text);
                        cmd.Parameters.AddWithValue("@Remark", "");
                        cmd.Parameters.AddWithValue("@Loginname", Session["LoginName"].ToString());
                        cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                        cmd.Parameters.AddWithValue("@Action", "insert");
                        try
                        {
                            _con.Open();
                            cmd.ExecuteNonQuery();
                            _con.Close();
                            sts = sts + 1;
                        }
                        catch (Exception)
                        {
                            // ignored
                        }
                    }
                }
            }
            if (sts>0)
            {
                _oo.msgbox(Page, msgbox, "Submitted successfully.", "S");
                _oo.ClearControls(Page);
                LoadData();
            }
        }
        else
        {
            _sql = "select Subject from OT_SubjectMaster where Subject='" + txtSubjectName.Text + "' and classid=" + ddlClass.SelectedValue + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            if (_oo.Duplicate(_sql))
            {
                _oo.msgbox(Page, msgbox, "Duplicate Subject!", "S");
            }
            else
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "OT_SubjectMasterProc";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = _con;
                    cmd.Parameters.AddWithValue("@classId", ddlClass.SelectedValue);
                    cmd.Parameters.AddWithValue("@Subject", txtSubjectName.Text);
                    cmd.Parameters.AddWithValue("@Remark", "");
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

        _sql = "select Subject, Remark, classId from OT_SubjectMaster where  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and id=" + ss;
        ddlClassPanel.SelectedValue = _oo.ReturnTag(_sql, "classId");
        txtSubjectNamePanel.Text = _oo.ReturnTag(_sql, "Subject");
        txtRemarkPanel.Text = _oo.ReturnTag(_sql, "Remark");
        Panel1_ModalPopupExtender.Show();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "OT_SubjectMasterProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", lblID.Text);
            cmd.Parameters.AddWithValue("@classId", ddlClassPanel.SelectedValue);
            cmd.Parameters.AddWithValue("@Subject", txtSubjectNamePanel.Text);
            cmd.Parameters.AddWithValue("@Remark", "");
            cmd.Parameters.AddWithValue("@Loginname", Session["LoginName"].ToString());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
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
        _sql = "Delete from OT_SubjectMaster where Id=" + lblvalue.Text+ " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";

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
}