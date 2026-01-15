using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class PaperMaster : Page
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
            LoadClass();
            ddlSubject.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlMedium.Items.Insert(0, new ListItem("<--Select-->", ""));
            
        }
    }
    public void LoadClass()
    {
        sql = "select * from classmaster where BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "'";
        _oo.FillDropDown_withValue(sql, ddlClass, "ClassName", "Id");
        ddlClass.Items.Insert(0, new ListItem("<--Select-->", ""));
        
    }
    protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        sql = "Select BranchName,Id from BranchMaster where ClassId='" + ddlClass.SelectedValue.ToString() + "'";
        sql = sql + " and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "'";
        _oo.FillDropDown_withValue(sql, ddlBranch, "BranchName", "Id");
        ddlBranch.Items.Insert(0, new ListItem("<--Select-->", ""));

        sql = "Select Medium,Id from MediumMaster where BranchCode=" + Session["BranchCode"].ToString() + " ";
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
        LoadData();
    }
    public void LoadData()
    {
        Grid.DataSource = null;
        Grid.DataBind();
        sql = "select pm.*, cm.ClassName, sm.SubjectName, bm.BranchName from TTPaperMaster pm ";
        sql = sql + " inner join ClassMaster cm on cm.id=pm.Classid and pm.BranchCode=cm.BranchCode  and cm.SessionName=pm.SessionName ";
        sql = sql + " inner join BranchMaster bm on bm.id=pm.branchid and bm.BranchCode=pm.BranchCode and bm.SessionName=pm.SessionName ";
        sql = sql + " inner join MediumMaster mm on mm.medium=pm.medium and mm.BranchCode=pm.BranchCode  ";
        sql = sql + " inner join TTSubjectMaster sm on sm.id=pm.SubjectId and cm.id=sm.Classid and sm.BranchCode=pm.BranchCode  and sm.SessionName=pm.SessionName";
        sql = sql + " where pm.BranchCode=" + Session["BranchCode"] + " and pm.SessionName='" + Session["SessionName"] + "' and pm.Classid=" + ddlClass.SelectedValue + " and pm.BranchId='" + ddlBranch.SelectedValue + "'";
        if (ddlMedium.SelectedIndex > 0)
        {
            sql = sql + " and mm.medium = '" + ddlMedium.SelectedItem.Text + "'";
        }
        if (ddlSubject.SelectedIndex!=0)
        {
            sql = sql + " and pm.SubjectId=" + ddlSubject.SelectedValue + " ";
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
            Label Paperid = (Label)Grid.Rows[i].FindControl("Label38");
            LinkButton lbtnEdit = (LinkButton)Grid.Rows[i].FindControl("lbtnEdit");
            LinkButton lbtnDelete = (LinkButton)Grid.Rows[i].FindControl("lbtnDelete");
            _sql = " select T1.Id from (";
            _sql = _sql + " SELECT Id FROM CCENurtoPrep_1718 WHERE PaperId=" + Paperid.Text + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            _sql = _sql + " union all";
            _sql = _sql + " SELECT id  FROM CCEItoV WHERE PaperId=" + Paperid.Text + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            _sql = _sql + " union all";
            _sql = _sql + " SELECT id FROM CCEVItoVIII_1718 WHERE PaperId=" + Paperid.Text + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            _sql = _sql + " union all";
            _sql = _sql + " SELECT id FROM CCEIXtoX_1718 WHERE PaperId=" + Paperid.Text + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            _sql = _sql + " union all";
            _sql = _sql + " SELECT id FROM CCEXI_1718 WHERE PaperId=" + Paperid.Text + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            _sql = _sql + " union all";
            _sql = _sql + " SELECT id FROM CCEXII_1718 WHERE PaperId=" + Paperid.Text + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            _sql = _sql + " )T1";
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
        _sql = "SELECT PaperName FROM TTPaperMaster WHERE PaperName='" + txtPaper.Text.Trim() + "' and classid=" + ddlClass.SelectedValue.Trim() + " and Branchid=" + ddlBranch.SelectedValue.Trim() + " and SubjectId=" + ddlSubject.SelectedValue.Trim() + " and Medium=" + ddlMedium.SelectedValue.Trim() + " and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "'";
        if (_oo.Duplicate(_sql))
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Duplicate Paper Name!", "A");
        }
        else
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "TTPaperMasterProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = _con;
                cmd.Parameters.AddWithValue("@Classid", ddlClass.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@BranchId", ddlBranch.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@Medium", ddlMedium.SelectedItem.Text.Trim());
                cmd.Parameters.AddWithValue("@SubjectId", ddlSubject.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@PaperName", txtPaper.Text.Trim());
                cmd.Parameters.AddWithValue("@PaperCode", txtPeperCode.Text.Trim());
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
        txtPaper.Text = string.Empty;
        txtPeperCode.Text = string.Empty;
        txtEditPaper.Text = string.Empty;
        lblSubjectId.Text = string.Empty;
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
        Label LabelPaper = (Label)chk.NamingContainer.FindControl("LabelPaper");
        Label lblPaperCode = (Label)chk.NamingContainer.FindControl("lblPaperCode");
        Label LabelMedium = (Label)chk.NamingContainer.FindControl("LabelMedium");

        lblClassId.Text = lblclassId.Text;
        txtEditPaper.Text = LabelPaper.Text;
        txtEditPeperCode.Text = lblPaperCode.Text;
        lblSubjectId.Text = SubjectID.Text;
        lblBranchId.Text = lblBeanchId.Text;
        lblMedium.Text = LabelMedium.Text;
        Panel1_ModalPopupExtender.Show();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        _sql = "SELECT PaperName FROM TTPaperMaster WHERE PaperName='" + txtEditPaper.Text.Trim() + "' classid=" + lblClassId.Text.Trim() + " and Branchid=" + lblBranchId.Text.Trim() + " and SubjectId="+ lblSubjectId.Text.Trim() + " and Medium=" + lblMedium.Text.Trim() + " and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and id<>"+ lblValueId.Text.Trim() + "";
        if (_oo.Duplicate(_sql))
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Duplicate Paper Name!", "A");
        }
        else
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "TTPaperMasterProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = _con;
                cmd.Parameters.AddWithValue("@Id", lblValueId.Text.Trim());
                cmd.Parameters.AddWithValue("@Classid", lblClassId.Text.Trim());
                cmd.Parameters.AddWithValue("@BranchId", lblBranchId.Text.Trim());
                cmd.Parameters.AddWithValue("@SubjectId", lblSubjectId.Text.Trim());
                cmd.Parameters.AddWithValue("@Medium", lblMedium.Text.Trim());
                cmd.Parameters.AddWithValue("@PaperName", txtEditPaper.Text.Trim());
                cmd.Parameters.AddWithValue("@PaperCode", txtEditPeperCode.Text.Trim());
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
        _sql = "Delete from TTPaperMaster where Id=" + lblValue.Text + " and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "'";

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