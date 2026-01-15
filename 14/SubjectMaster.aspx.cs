using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class SubjectMaster : Page
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
            ddlBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlMedium.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
    }
    public void LoadClass()
    {
        sql = "select * from classmaster where BranchCode="+Session["BranchCode"]+ " and SessionName='" + Session["SessionName"].ToString() + "'";
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
    }
    public void LoadData()
    {
        if (ddlBranch.SelectedIndex==0)
        {
            Grid.DataSource = null;
            Grid.DataBind();
            return;
        }
        sql = "select sm.*, sm.id sid, cm.ClassName, bm.BranchName, case when isnull(isPractical,0)=1 then 'Yes' else 'No' end isPracticals, case when isnull(isCompulsoryForBest5,0)=1 then 'Yes' else 'No' end isCompulsoryForBest from TTSubjectMaster sm ";
        sql = sql + " inner join ClassMaster cm on cm.id=sm.Classid and cm.BranchCode=sm.BranchCode and cm.SessionName=sm.SessionName ";
        sql = sql + " inner join BranchMaster bm on bm.id=sm.BranchId and bm.BranchCode=sm.BranchCode and bm.SessionName=sm.SessionName ";
        sql = sql + " inner join MediumMaster mm on mm.medium=sm.medium and mm.BranchCode=sm.BranchCode  ";
        sql = sql + " where sm.BranchCode=" + Session["BranchCode"] + " and sm.SessionName='" + Session["SessionName"] + "' and sm.Classid=" + ddlClass.SelectedValue + " and sm.BranchId=" + ddlBranch.SelectedValue + "";
        if (ddlMedium.SelectedIndex > 0)
        {
            sql = sql + " and mm.medium = '" + ddlMedium.SelectedItem.Text + "'";
        }
        sql = sql + " order by cm.id, sm.DisplayOrder asc";
        var dt = _oo.Fetchdata(sql);
        Grid.DataSource = dt;
        Grid.DataBind();
        txtDisplayOrder.Text = Convert.ToString(Grid.Rows.Count + 1);
        for (int i = 0; i < Grid.Rows.Count; i++)
        {
            Label id = (Label)Grid.Rows[i].FindControl("Label38");
            Label lblClassId = (Label)Grid.Rows[i].FindControl("lblClassId");
            LinkButton lbtnEdit = (LinkButton)Grid.Rows[i].FindControl("lbtnEdit");
            LinkButton lbtnDelete = (LinkButton)Grid.Rows[i].FindControl("lbtnDelete");
            _sql = "SELECT Classid FROM TTPaperMaster WHERE subjectid=" + id.Text + " and classid="+ lblClassId.Text + " and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "'";
            if (_oo.Duplicate(_sql))
            {
                //lbtnEdit.Text = "<i class='fa fa-lock'></i>";
                lbtnDelete.Text = "<i class='fa fa-lock'></i>";
                //lbtnEdit.Enabled = false;
                lbtnDelete.Enabled = false;
            }
        }
    }

    protected void btnInserts_Click(object sender, EventArgs e)
    {
        _sql = "SELECT SubjectName FROM TTSubjectMaster WHERE SubjectName='" + txtSubject.Text.Trim() + "' and classid=" + ddlClass.SelectedValue.Trim() + " and Branchid=" + ddlBranch.SelectedValue.Trim() + " and Medium='" + ddlMedium.SelectedItem.Text.Trim() + "'  and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "'";
        if (_oo.Duplicate(_sql))
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Duplicate Subject!", "A");
        }
        else
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "TTSubjectMasterProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = _con;
                cmd.Parameters.AddWithValue("@Classid", ddlClass.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@BranchId", ddlBranch.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@Medium", ddlMedium.SelectedItem.Text.Trim());
                cmd.Parameters.AddWithValue("@SubjectType", rdoSubjectType.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@IsAditional", rdoAdditional.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@isPractical", rdoisPractical.SelectedValue.Trim());
                //if (rdoCompulsoryForLanguage.SelectedValue.Trim()=="1")
                //{
                //    cmd.Parameters.AddWithValue("@isCompulsoryForBest5", 1);
                //} 
                //else
                //{
                //    cmd.Parameters.AddWithValue("@isCompulsoryForBest5", 0);
                //}
                cmd.Parameters.AddWithValue("@ApplicableFor", ddlApplicableFor.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@SubjectName", txtSubject.Text.Trim());
                cmd.Parameters.AddWithValue("@SubjectCode", txtSubjectCode.Text.Trim());
                cmd.Parameters.AddWithValue("@ShortCode", txtShortCode.Text.Trim());
                cmd.Parameters.AddWithValue("@DisplayOrder", txtDisplayOrder.Text.Trim());
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
        txtSubject.Text = string.Empty;
        txtSubjectCode.Text = string.Empty;

        txtEditSubject.Text = string.Empty;
        txtEditSubjectCode.Text = string.Empty;
        txtShortCode.Text = string.Empty;
    }


    protected void lbtnEdit_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label38");
        lblEditValueId.Text = lblId.Text;

        Label lblclassId = (Label)chk.NamingContainer.FindControl("lblclassId");
        Label lblBeanchId = (Label)chk.NamingContainer.FindControl("lblBeanchId");
        Label LabelSubject = (Label)chk.NamingContainer.FindControl("LabelSubject");
        Label LabelSubjectCode = (Label)chk.NamingContainer.FindControl("LabelSubjectCode");
        Label LabelDisplayOrder = (Label)chk.NamingContainer.FindControl("LabelDisplayOrder");
        Label LabelApplicableFor = (Label)chk.NamingContainer.FindControl("LabelApplicableFor");
        Label LabelSubjectType = (Label)chk.NamingContainer.FindControl("LabelSubjectType");
        Label LabelIsAditional = (Label)chk.NamingContainer.FindControl("LabelIsAditional");
        Label LabelIsPractical = (Label)chk.NamingContainer.FindControl("LabelIsPractical");
        Label LabelIsCompulsoryForBest5 = (Label)chk.NamingContainer.FindControl("LabelIsCompulsoryForBest5");
        Label lblShortCode = (Label)chk.NamingContainer.FindControl("lblShortCode");
        Label LabelMedium = (Label)chk.NamingContainer.FindControl("LabelMedium");

        lblEditClassId.Text = lblclassId.Text;
        lblEditBranchId.Text = lblBeanchId.Text;
        txtEditSubject.Text = LabelSubject.Text;
        txtEditDisplayOrder.Text = LabelDisplayOrder.Text;
        txtEditSubjectCode.Text = LabelSubjectCode.Text;
        txtEditShortCode.Text = lblShortCode.Text;
        ddlEditApplicableFor.SelectedValue = LabelApplicableFor.Text;
        rdoEditAdditional.SelectedIndex = (LabelIsAditional.Text=="False"?1:0);
        rdoEditSubjectType.SelectedIndex = (LabelSubjectType.Text == "Compulsory" ? 0 : 1);
        rdoEditisPractical.SelectedIndex = (LabelIsPractical.Text == "Yes" ? 0 : 1);
        rdoEditCompulsoryForLanguage.SelectedValue = (LabelIsCompulsoryForBest5.Text == "Yes" ? "1" : "0");
        lblMedium.Text = LabelMedium.Text;
        Panel1_ModalPopupExtender.Show();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        _sql = "SELECT SubjectName FROM TTSubjectMaster WHERE SubjectName='" + txtEditSubject.Text.Trim() + "' and classid=" + lblEditClassId.Text.Trim() + " and Branchid=" + lblEditBranchId.Text.Trim() + " and Medium='" + lblMedium.Text.Trim() + "'  and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and id<>"+ lblEditValueId.Text.Trim() + "";
        if (_oo.Duplicate(_sql))
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Duplicate Subject!", "A");
        }
        else
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "TTSubjectMasterProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = _con;
                cmd.Parameters.AddWithValue("@Id", lblEditValueId.Text.Trim());
                cmd.Parameters.AddWithValue("@Classid", lblEditClassId.Text.Trim());
                cmd.Parameters.AddWithValue("@BranchId", lblEditBranchId.Text.Trim());
                cmd.Parameters.AddWithValue("@Medium", lblMedium.Text.Trim());
                cmd.Parameters.AddWithValue("@SubjectType", rdoEditSubjectType.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@IsAditional", rdoEditAdditional.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@isPractical", rdoEditisPractical.SelectedValue.Trim());
                if (rdoEditCompulsoryForLanguage.SelectedValue.Trim() == "1")
                {
                    cmd.Parameters.AddWithValue("@isCompulsoryForBest5", 1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@isCompulsoryForBest5", 0);
                }
                cmd.Parameters.AddWithValue("@ApplicableFor", ddlEditApplicableFor.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@SubjectName", txtEditSubject.Text.Trim());
                cmd.Parameters.AddWithValue("@SubjectCode", txtEditSubjectCode.Text.Trim());
                cmd.Parameters.AddWithValue("@ShortCode", txtEditShortCode.Text.Trim());
                cmd.Parameters.AddWithValue("@DisplayOrder", txtEditDisplayOrder.Text.Trim());
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@Action", "Update");
                try
                {
                    _con.Open();
                    cmd.ExecuteNonQuery();
                    _con.Close();
                    ddlClass.SelectedValue = lblEditClassId.Text.Trim();
                    LoadData();
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Updatted successfully.", "S");
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
        Label lblclassId = (Label)chk.NamingContainer.FindControl("lblclassId");
        lblClassids.Text = lblclassId.Text;
        lblValue.Text = lblId.Text;
        mpeDelete.Show();
    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
        _sql = "Delete from TTSubjectMaster where Id=" + lblValue.Text + " and classid="+ lblClassids.Text.Trim() + " and BranchCode="+Session["BranchCode"]+ " and SessionName='" + Session["SessionName"] + "'";

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





    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadData();
    }
  

    protected void rdoAdditional_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdoSubjectType.SelectedIndex==0)
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Please select optional from subject type!", "A");
            rdoAdditional.SelectedValue = "0";
        }
    }

    protected void ddlMedium_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadData();
    }
}