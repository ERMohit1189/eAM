using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class PeriodMaster : Page
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
        if (!IsPostBack)
        {
            
            LoadData();
        }
    }
   
    public void LoadData()
    {
        sql = "select * from TTPeriodMaster where BranchCode=" + Session["BranchCode"] + " order by id asc";
        var dt = _oo.Fetchdata(sql);
        gvTimeTableRule.DataSource = dt;
        gvTimeTableRule.DataBind();
        for (int i = 0; i < gvTimeTableRule.Rows.Count; i++)
        {
            Label id = (Label)gvTimeTableRule.Rows[i].FindControl("Label37");
            LinkButton lbtnEdit = (LinkButton)gvTimeTableRule.Rows[i].FindControl("LinkButton2");
            LinkButton lbtnDelete = (LinkButton)gvTimeTableRule.Rows[i].FindControl("btnDelete");
            _sql = "SELECT Classid FROM TTPeriodAllotToStaff WHERE Periodid='" + id.Text + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "'";
            if (_oo.Duplicate(_sql))
            {
                lbtnEdit.Text = "<i class='fa fa-lock'></i>";
                lbtnEdit.Enabled = false;
                lbtnDelete.Text = "<i class='fa fa-lock'></i>";
                lbtnDelete.Enabled = false;
            }
        }
    }

    protected void btnInsert_Click(object sender, EventArgs e)
    {

        _sql = "SELECT Period FROM TTPeriodMaster WHERE BranchCode=" + Session["BranchCode"] + " and Period='"+txtPeriod.Text.Trim()+"'";
        if (_oo.Duplicate(_sql))
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Duplicate Period Name!", "A");
        }
        else
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "TTPeriodMasterProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = _con;
                cmd.Parameters.AddWithValue("@Period", txtPeriod.Text.Trim());
                cmd.Parameters.AddWithValue("@PeriodType", ddlPeriodType.SelectedItem.Text.Trim());
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
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
    public void Reset()
    {
        txtPeriod.Text = "";
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label36");
        Label LabelPeriod = (Label)chk.NamingContainer.FindControl("LabelPeriod");
        Label PeriodType = (Label)chk.NamingContainer.FindControl("PeriodType");

        string ss = lblId.Text;
        lblID.Text = ss;
        txtPeriod0.Text = LabelPeriod.Text;
        ddlPeriodType0.Text = PeriodType.Text;
        Panel1_ModalPopupExtender.Show();
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        _sql = "SELECT Period FROM TTPeriodMaster WHERE BranchCode=" + Session["BranchCode"] + " and Period='" + txtPeriod0.Text.Trim() + "' and id<>"+ lblID.Text.Trim() + "";
        if (_oo.Duplicate(_sql))
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Duplicate Period Name!", "A");
        }
        else
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "TTPeriodMasterProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = _con;
                cmd.Parameters.AddWithValue("@id", lblID.Text.Trim());
                cmd.Parameters.AddWithValue("@Period", txtPeriod0.Text.Trim());
                cmd.Parameters.AddWithValue("@PeriodType", ddlPeriodType0.SelectedItem.Text.Trim());
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@Action", "update");
                cmd.Connection = _con;
                try
                {
                    _con.Open();
                    cmd.ExecuteNonQuery();
                    _con.Close();
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Updated successfully.", "S");

                    LoadData();
                    Panel1_ModalPopupExtender.Hide();
                }
                catch (Exception)
                {
                }
            }
        }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {

    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label37");
        lblValue.Text = lblId.Text;
        mpeDelete.Show();
        btnNo.Focus();
    }

    protected void btnYes_Click(object sender, EventArgs e)
    {
        DeleteRecord();
    } 


    public void DeleteRecord()
    {
        _sql = "Delete from TTPeriodMaster where Id=" + lblValue.Text + " and BranchCode=" + Session["BranchCode"] + "";

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

    
}