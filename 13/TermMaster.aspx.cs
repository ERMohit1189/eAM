using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI;
public partial class TermMaster : Page
{
    private SqlConnection _con;
    private readonly Campus _oo;
    private string _sql, _sql1 = String.Empty;
    public TermMaster()
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
            txtTermName.Focus();
            LoadData();
        }
    }
    
    private void LoadData()
    {
        _sql = "select * from OT_TermMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " order by id asc";
        Grd.DataSource = _oo.GridFill(_sql);
        Grd.DataBind();

        for (int i = 0; i < Grd.Rows.Count; i++)
        {
            Label Label37 = (Label)Grd.Rows[i].FindControl("Label37");
            LinkButton Edit = (LinkButton)Grd.Rows[i].FindControl("LinkButton2");
            LinkButton LinkButton3 = (LinkButton)Grd.Rows[i].FindControl("LinkButton3");
            _sql = "select ExamName from OT_ExamMaster where TermId=" + Label37.Text + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            if (_oo.Duplicate(_sql))
            {
                LinkButton3.Text = "<i class='fa fa-lock'></i>";
                LinkButton3.Enabled = false;
                Edit.Text = "<i class='fa fa-lock'></i>";
                Edit.Enabled = false;
            }
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        _sql = "select TermName from OT_TermMaster where TermName='" + txtTermName.Text.Trim() + "' and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";

        if (_oo.Duplicate(_sql))
        {
           _oo.msgbox(Page, msgbox, "Duplicate Test Name!", "S");
        }
        else
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "OT_TermMasterProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = _con;
                cmd.Parameters.AddWithValue("@TermName", txtTermName.Text);
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
        _sql = "select TermName from OT_TermMaster where  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and id=" + ss;
        txtTermNamePanel.Text = _oo.ReturnTag(_sql, "TermName");
        Panel1_ModalPopupExtender.Show();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "OT_TermMasterProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", lblID.Text);
            cmd.Parameters.AddWithValue("@TermName", txtTermNamePanel.Text);
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
        _sql = "Delete from OT_TermMaster where Id=" + lblvalue.Text+ " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";

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