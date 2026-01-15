using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class admissionDatePermission : System.Web.UI.Page
{
    private SqlConnection _con;
    private readonly Campus _oo;
    private string _sql, sql = String.Empty;

    public admissionDatePermission()
    {
        _con = new SqlConnection();
        _oo = new Campus();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        _con = _oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader); 

        if (!IsPostBack)
        {
            sql = "Select BranchId, BranchName from Branchtab";
            var dt = _oo.Fetchdata(sql);
            ddlBranch.DataSource = dt;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "BranchId";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, new ListItem("<--Select Branch-->", ""));
        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedIndex == 0)
        {
            divControls.Visible = false;
            divControls1.Visible = false;
            divControls2.Visible = false;
            divControls3.Visible = false;
            divControls4.Visible = false;
        }
        else
        {
            LoadData();
        }
    }
    private void LoadData()
    {
        _sql = "select * from admissionDatePermission where BranchCode=" + ddlBranch.SelectedValue + "";
        Grd.DataSource = _oo.GridFill(_sql);
        Grd.DataBind();
        if (Grd.Rows.Count > 0)
        {
            divControls.Visible = false;
            divControls1.Visible = false;
            divControls2.Visible = false;
            divControls3.Visible = false;
            divControls4.Visible = false;
        }
        else
        {
            divControls.Visible = true;
            divControls1.Visible = true;
            divControls2.Visible = true;
            divControls3.Visible = true;
            divControls4.Visible = true;
        }
    }
    
    protected void LinkButton1_Click(object sender, EventArgs e)
    {

        _sql = "select todate from admissionDatePermission where BranchCode=" + ddlBranch.SelectedValue + "";
        if (_oo.Duplicate(_sql))
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Duplicate Entry!", "A");
        }
        else
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "admissionDatePermissionproc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = _con;
                cmd.Parameters.AddWithValue("@fromdate", txtfrom.Text.Trim());
                cmd.Parameters.AddWithValue("@todate", txtto.Text.Trim());
                cmd.Parameters.AddWithValue("@OnlineRegistration", rdoOnlineRegistration.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@ReceiptNocompulsory", rdoReceiptNocompulsory.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", ddlBranch.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@Action", "insert");
                try
                {
                    _con.Open();
                    cmd.ExecuteNonQuery();
                    _con.Close();
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
                    LoadData();
                }
                catch (Exception)
                {
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
        Label fromdate = (Label)chk.NamingContainer.FindControl("fromdate");
        Label todate = (Label)chk.NamingContainer.FindControl("todate");
        Label OnlineRegistration = (Label)chk.NamingContainer.FindControl("OnlineRegistration");
        Label ReceiptNocompulsory = (Label)chk.NamingContainer.FindControl("ReceiptNocompulsory");
        txtfrom0.Text = fromdate.Text;
        txtto0.Text = todate.Text;
        rdoOnlineRegistration0.SelectedValue= (OnlineRegistration.Text=="Yes"?"1":"0");
        rdoReceiptNocompulsory0.SelectedValue = (ReceiptNocompulsory.Text == "Yes" ? "1" : "0");
        Panel1_ModalPopupExtender.Show();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "admissionDatePermissionproc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", lblID.Text.Trim());
            cmd.Parameters.AddWithValue("@fromdate", txtfrom0.Text.Trim());
            cmd.Parameters.AddWithValue("@todate", txtto0.Text.Trim());
            cmd.Parameters.AddWithValue("@OnlineRegistration", rdoOnlineRegistration0.SelectedValue.Trim());
            cmd.Parameters.AddWithValue("@ReceiptNocompulsory", rdoReceiptNocompulsory0.SelectedValue.Trim());
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", ddlBranch.SelectedValue.ToString());
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
    protected void Button4_Click(object sender, EventArgs e)
    {
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        _sql = "Delete from admissionDatePermission where Id=" + lblvalue.Text + "";

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