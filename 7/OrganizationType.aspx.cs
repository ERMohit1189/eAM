using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class admin_OrganizationType : Page
{
    public string _sql = "";
    private readonly Campus _oo = new Campus();
    private SqlConnection _con;


    protected void Page_Load(object sender, EventArgs e)
    {
        _con = new SqlConnection();
        _con = _oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            GetOrganizationType();
        }
    }

    private void Reset()
    {
        txtOrganizationType.Text = string.Empty;
        txtOrganizationType0.Text = string.Empty;
        txtRemark.Text = string.Empty;
        txtRemark0.Text = string.Empty;
    }

   
    private void GetOrganizationType()
    {
        _sql = "select * from AccOrganizationType WHERE BranchCode="+Session["BranchCode"] +"";
        var dt = _oo.Fetchdata(_sql);
        gvOrganizationTypeList.DataSource = dt;
        gvOrganizationTypeList.DataBind();
        for (int i = 0; i < gvOrganizationTypeList.Rows.Count; i++)
        {
            Label Bankid = (Label)gvOrganizationTypeList.Rows[i].FindControl("Label36");
            LinkButton lbtnEdit = (LinkButton)gvOrganizationTypeList.Rows[i].FindControl("lbtnEdit");
            LinkButton lbtnDelete = (LinkButton)gvOrganizationTypeList.Rows[i].FindControl("lbtnDelete");
            _sql = "select VendorCode from AccVendor where OrganizationTypeId=" + Bankid.Text + " and BranchCode=" + Session["BranchCode"] + "";
            if (_oo.Duplicate(_sql))
            {
                lbtnEdit.Text = "<i class='fa fa-lock'></i>";
                lbtnDelete.Text = "<i class='fa fa-lock'></i>";
                lbtnEdit.Enabled = false;
                lbtnDelete.Enabled = false;
            }
        }
    }

  
    protected void btnInsert_Click(object sender, EventArgs e)
    {
        _sql = "SELECT OrganizationType FROM AccOrganizationType WHERE OrganizationType='" + txtOrganizationType.Text + "' and BranchCode=" + Session["BranchCode"] + "";
        if (_oo.Duplicate(_sql))
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Duplicate AccOrganization Type!", "A");
        }
        else
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "AccOrganizationTypeProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = _con;
                cmd.Parameters.AddWithValue("@OrganizationType", txtOrganizationType.Text.Trim());
                cmd.Parameters.AddWithValue("@Remark", txtRemark.Text.Trim());
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                cmd.Parameters.AddWithValue("@Action", "insert");
                try
                {
                    _con.Open();
                    cmd.ExecuteNonQuery();
                    _con.Close();
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
                    GetOrganizationType();
                    Reset();
                }
                catch (Exception ex)
                {
                }
            }
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        Reset();
    }

    protected void lbtnEdit_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label36");
        string ss = lblId.Text;
        lblID.Text = ss;

        Label OrganizationType = (Label)chk.NamingContainer.FindControl("OrganizationType");
        Label Remark = (Label)chk.NamingContainer.FindControl("Remark");

        txtOrganizationType0.Text = OrganizationType.Text;
        txtRemark0.Text = Remark.Text;
        Panel1_ModalPopupExtender.Show();
    }

    protected void lbtnDelete_Click(object sender, EventArgs e)
    {
        btnNo.Focus();
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label37");
        string ss = lblId.Text;
        lblValue.Text = ss;
        mpeDelete.Show();
    }

    protected void btnYes_Click(object sender, EventArgs e)
    {
        DeleteRecord();
    }

    public void DeleteRecord()
    {
        _sql = "Delete from AccOrganizationType where Id=" + lblValue.Text + "";

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
                GetOrganizationType();
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

    protected void Button3_Click(object sender, EventArgs e)
    {
        _sql = "SELECT OrganizationType FROM AccOrganizationType WHERE OrganizationType='" + txtOrganizationType0.Text + "' and BranchCode=" + Session["BranchCode"] + " and id<>" + lblID.Text + "";
        if (_oo.Duplicate(_sql))
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Duplicate AccOrganization Type!", "A");
            Panel1_ModalPopupExtender.Show();
        }
        else
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "AccOrganizationTypeProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = _con;
                cmd.Parameters.AddWithValue("@Id", lblID.Text.Trim());
                cmd.Parameters.AddWithValue("@OrganizationType", txtOrganizationType0.Text.Trim());
                cmd.Parameters.AddWithValue("@Remark", txtRemark0.Text.Trim());
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                cmd.Parameters.AddWithValue("@Action", "Update");
                try
                {
                    _con.Open();
                    cmd.ExecuteNonQuery();
                    _con.Close();
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Updatted successfully.", "S");
                    GetOrganizationType();
                    Reset();
                }
                catch (Exception ex)
                {
                }
            }
        }
    }
    
}