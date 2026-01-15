using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;


public partial class QuickSMSTemplets : Page
{
    private SqlConnection _con;
    private readonly Campus _oo;
    private string _sql = String.Empty;
    public QuickSMSTemplets()
    {
        _con = new SqlConnection();
        _oo = new Campus();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        _con = _oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            LoadGrid();
        }
    }


    public void LoadGrid()
    {
        _sql = "select id, Title, SMS from tbl_QuickSMSTemplets where BranchCode="+Session["BranchCode"] +"";
        Grd.DataSource = _oo.GridFill(_sql);
        Grd.DataBind();
        if (Grd.Rows.Count == 0)
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sorry, No Record found!", "A");
        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        _sql = _sql + "select id, Title, SMS from tbl_QuickSMSTemplets where (Title='" + txtMessageTitle.Text.Trim() + "' or SMS='" + txtMessage.Text.Trim() + "') and BranchCode=" + Session["BranchCode"] + "";
        if (_oo.Duplicate(_sql))
        {
            //oo.MessageBox("Duplicate Enntry", this.Page);
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Duplicate Entry!", "A");
        }
        else if (txtMessageTitle.Text.Trim() == "" || txtMessage.Text.Trim() == "")
        {
            //oo.MessageBox("Duplicate Enntry", this.Page);
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Enter message title and message!", "A");
        }
        else if (txtMessageTitle.Text.Trim().Length >100)
        {
            //oo.MessageBox("Duplicate Enntry", this.Page);
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Enter maximum 100 characters message title!", "A");
        }
        else if (txtMessage.Text.Trim().Length >500)
        {
            //oo.MessageBox("Duplicate Enntry", this.Page);
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Enter maximum 500 characters message!", "A");
        }
        else
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SP_QuickSMSTemplets";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = _con;
            cmd.Parameters.AddWithValue("@Title ", txtMessageTitle.Text.Trim());
            cmd.Parameters.AddWithValue("@SMS", txtMessage.Text.Trim());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"]);
            cmd.Parameters.AddWithValue("@action", "insert");
            try
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
                _oo.ClearControls(Page);
                LoadGrid();
            }
            catch (Exception)
            {
            }
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtMessageTitle.Text = "";
        txtMessage.Text = "";
    }

    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        GridViewRow currentrow = (GridViewRow)lnk.NamingContainer;

        Label lblId = (Label)currentrow.FindControl("lblId");

        lblvalue.Text = lblId.Text.Trim();

        _sql = "select id, Title, SMS from tbl_QuickSMSTemplets where Id='" + lblId.Text.Trim() + "' and BranchCode=" + Session["BranchCode"] + "";
        txtMessageTitle0.Text = _oo.ReturnTag(_sql, "Title");
        txtMessage0.Text = _oo.ReturnTag(_sql, "SMS");
        Panel1_ModalPopupExtender.Show();
    }



    protected void lnkUpdate_Click(object sender, EventArgs e)
    {
        if (txtMessageTitle0.Text.Trim() == "" || txtMessage0.Text.Trim() == "")
        {
            Panel1_ModalPopupExtender.Show();
            Campus camp = new Campus(); camp.msgbox(Page, msgbox2, "Enter message title and message!", "A");
        }
        else if (txtMessageTitle0.Text.Trim().Length > 100)
        {
            //oo.MessageBox("Duplicate Enntry", this.Page);
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Enter maximum 100 characters message title!", "A");
        }
        else if (txtMessage0.Text.Trim().Length > 500)
        {
            //oo.MessageBox("Duplicate Enntry", this.Page);
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Enter maximum 500 characters message!", "A");
        }
        else
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SP_QuickSMSTemplets";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = _con;
            cmd.Parameters.AddWithValue("@id ", lblvalue.Text.Trim());
            cmd.Parameters.AddWithValue("@Title ", txtMessageTitle0.Text.Trim());
            cmd.Parameters.AddWithValue("@SMS", txtMessage0.Text.Trim());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"]);
            cmd.Parameters.AddWithValue("@action", "update");
            try
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Updated successfully.", "S");
                _oo.ClearControls(Page);
                LoadGrid();
            }
            catch (Exception)
            {
            }
        }
        lblvalue.Text = "";
    }

    protected void lnkConfirmDelete_Click(object sender, EventArgs e)
    {
        lnkDeleteNo.Focus();
        LinkButton lnk = (LinkButton)sender;
        GridViewRow currentrow = (GridViewRow)lnk.NamingContainer;

        Label lblId = (Label)currentrow.FindControl("lblId");

        lblvalue.Text = lblId.Text.Trim();

        Panel2_ModalPopupExtender.Show();
    }
    protected void lnkDeleteYes_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "SP_QuickSMSTemplets";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = _con;
        cmd.Parameters.AddWithValue("@id ", lblvalue.Text.Trim());
        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"]);
        cmd.Parameters.AddWithValue("@action", "delete");
        try
        {
            _con.Open();
            cmd.ExecuteNonQuery();
            _con.Close();
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Deleted successfully.", "S");
            _oo.ClearControls(Page);
            LoadGrid();
        }
        catch (Exception)
        {
        }
        lblvalue.Text = "";
    }


    protected void btnDelateAll_Click(object sender, EventArgs e)
    {
        Panel3_ModalPopupExtender.Show();
    }
    protected void lnkDeleteYesAll_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "SP_QuickSMSTemplets";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = _con;
        cmd.Parameters.AddWithValue("@action", "deleteAll");
        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"]);
        try
        {
            _con.Open();
            cmd.ExecuteNonQuery();
            _con.Close();
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "All templet(s) deleted successfully.", "S");
            _oo.ClearControls(Page);
            LoadGrid();
        }
        catch (Exception)
        {
        }
        lblvalue.Text = "";
    }
    public override void Dispose()
    {
        _con.Dispose();
        _oo.Dispose();
    }
}